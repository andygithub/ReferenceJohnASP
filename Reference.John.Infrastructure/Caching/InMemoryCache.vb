Imports System.Threading


Namespace Cache
    ''' <summary>
    ''' Simple cache implementation. Not very efficient, uses simple LRU strategy.
    ''' </summary>
    Public NotInheritable Class InMemoryCache
        Implements ICache
        Implements IDisposable
        Private _cacheHits As Integer
        Private _cacheMisses As Integer
        Private _cacheAdds As Integer
        Private _cacheItemInvalidations As Integer

        ' entry key -> CacheEntry
        Private entries As New Dictionary(Of String, CacheEntry)()

        Private _lruChainHead As CacheEntry
        Private _lruChainTail As CacheEntry
        Private lruLock As New Object()

        ' EntitySet -> set of cache entry keys
        Private entitySetDependencies As New Dictionary(Of String, HashSet(Of CacheEntry))()
        Private entriesLock As New ReaderWriterLockSlim()

        ''' <summary>
        ''' Initializes a new instance of the InMemoryCache class.
        ''' </summary>
        Public Sub New()
            Me.New(Int32.MaxValue)
        End Sub

        ''' <summary>
        ''' Initializes a new instance of the InMemoryCache class.
        ''' </summary>
        ''' <param name="maxItems">The maximum number of items which can be stored in the cache.</param>
        Public Sub New(maxItems As Integer)
            Me.MaxItems = maxItems
            Me.GetCurrentDate = Function() DateTime.Now
        End Sub

        ''' <summary>
        ''' Gets the number of cache hits.
        ''' </summary>
        ''' <value>The number of cache hits.</value>
        Public ReadOnly Property CacheHits() As Integer
            Get
                Return _cacheHits
            End Get
        End Property

        ''' <summary>
        ''' Gets the number of cache misses.
        ''' </summary>
        ''' <value>The number of cache misses.</value>
        Public ReadOnly Property CacheMisses() As Integer
            Get
                Return _cacheMisses
            End Get
        End Property

        ''' <summary>
        ''' Gets the number of cache adds.
        ''' </summary>
        ''' <value>The number of cache adds.</value>
        Public ReadOnly Property CacheItemAdds() As Integer
            Get
                Return _cacheAdds
            End Get
        End Property

        ''' <summary>
        ''' Gets the number of cache item invalidations.
        ''' </summary>
        ''' <value>The number of cache item invalidations.</value>
        Public ReadOnly Property CacheItemInvalidations() As Integer
            Get
                Return _cacheItemInvalidations
            End Get
        End Property

        ''' <summary>
        ''' Gets the maximum number of items the cache can hold.
        ''' </summary>
        ''' <value>The maximum number of items the cache can hold.</value>
        Public Property MaxItems() As Integer

        ''' <summary>
        ''' Gets the number of items in the cache.
        ''' </summary>
        ''' <value>The number of items in the cache.</value>
        Public ReadOnly Property Count() As Integer
            Get
                Me.entriesLock.EnterReadLock()
                Dim item As Integer = Me.entries.Count
                Me.entriesLock.ExitReadLock()
                Return item
            End Get
        End Property

        Public Property GetCurrentDate() As Func(Of DateTime)

        Friend ReadOnly Property LruChainHead() As CacheEntry
            Get
                Return _lruChainHead
            End Get
        End Property

        Friend ReadOnly Property LruChainTail() As CacheEntry
            Get
                Return _lruChainTail
            End Get
        End Property

        ''' <summary>
        ''' Tries to the get entry by key.
        ''' </summary>
        ''' <param name="key">The cache key.</param>
        ''' <param name="value">The retrieved value.</param>
        ''' <returns>
        ''' A value of <c>true</c> if entry was found in the cache, <c>false</c> otherwise.
        ''' </returns>
        Public Function GetItem(key As String, ByRef value As Object) As Boolean Implements ICache.GetItem
            Dim entry As New CacheEntry
            Dim currentDate As DateTime = Me.GetCurrentDate().Invoke

            Me.entriesLock.EnterReadLock()
            Dim success As Boolean = Me.entries.TryGetValue(key, entry)
            Me.entriesLock.ExitReadLock()

            If success Then
                If currentDate >= entry.ExpirationTime Then
                    success = False
                    Me.InvalidateExpiredEntries()
                End If
            End If

            If Not success Then
                Interlocked.Increment(_cacheMisses)
                value = Nothing
                Return False
            Else
                Interlocked.Increment(_cacheHits)
                Me.MoveToTopOfLruChain(entry)
                entry.LastAccessTime = Me.GetCurrentDate.Invoke()
                If entry.SlidingExpiration > TimeSpan.Zero Then
                    entry.ExpirationTime = Me.GetCurrentDate().Invoke.Add(entry.SlidingExpiration)
                End If

                value = entry.Value
                Return True
            End If
        End Function

        ''' <summary>
        ''' Adds the specified entry to the cache.
        ''' </summary>
        ''' <param name="key">The entry key.</param>
        ''' <param name="value">The entry value.</param>
        ''' <param name="dependentEntitySets">The list of dependent entity sets.</param>
        ''' <param name="slidingExpiration">The sliding expiration.</param>
        ''' <param name="absoluteExpiration">The absolute expiration.</param>
        Public Sub PutItem(key As String, value As Object, dependentEntitySets As IEnumerable(Of String), slidingExpiration As TimeSpan, absoluteExpiration As DateTime) Implements ICache.PutItem
            If key Is Nothing Then
                Throw New ArgumentNullException("key")
            End If

            If dependentEntitySets Is Nothing Then
                Throw New ArgumentNullException("dependentEntitySets")
            End If

            Dim newEntry = New CacheEntry() With { _
                             .Key = key, _
                             .KeyHashCode = key.GetHashCode(), _
                             .Value = value, _
                             .DependentEntitySets = dependentEntitySets, _
                             .SlidingExpiration = slidingExpiration, _
                             .ExpirationTime = absoluteExpiration _
            }

            If slidingExpiration > TimeSpan.Zero Then
                newEntry.ExpirationTime = Me.GetCurrentDate().Invoke.Add(slidingExpiration)
            Else
                newEntry.ExpirationTime = absoluteExpiration
            End If

            Me.entriesLock.EnterWriteLock()

            Dim oldEntry As New CacheEntry

            If Me.entries.TryGetValue(key, oldEntry) Then
                Me.InvalidateSingleEntry(oldEntry)
            End If

            ' too many items in the cache - invalidate the last one in LRU chain
            If Me.entries.Count >= Me.MaxItems Then
                Me.InvalidateSingleEntry(_lruChainTail)
            End If

            Me.entries.Add(key, newEntry)

            For Each entitySet As String In dependentEntitySets
                Dim queriesDependentOnSet As New HashSet(Of CacheEntry)

                If Not Me.entitySetDependencies.TryGetValue(entitySet, queriesDependentOnSet) Then
                    queriesDependentOnSet = New HashSet(Of CacheEntry)()
                    Me.entitySetDependencies(entitySet) = queriesDependentOnSet
                End If

                queriesDependentOnSet.Add(newEntry)
            Next

            Interlocked.Increment(_cacheAdds)
            Me.MoveToTopOfLruChain(newEntry)
            newEntry.LastAccessTime = Me.GetCurrentDate().Invoke
            Me.entriesLock.ExitWriteLock()
        End Sub

        ''' <summary>
        ''' Invalidates all cache entries which are dependent on any of the specified entity sets.
        ''' </summary>
        ''' <param name="entitySets">The entity sets.</param>
        Public Sub InvalidateSets(entitySets As IEnumerable(Of String)) Implements ICache.InvalidateSets
            If entitySets Is Nothing Then
                Throw New ArgumentNullException("entitySets")
            End If

            Me.entriesLock.EnterWriteLock()
            For Each entitySet As String In entitySets
                Dim dependentEntries As New HashSet(Of CacheEntry)

                If Me.entitySetDependencies.TryGetValue(entitySet, dependentEntries) Then
                    For Each entry As CacheEntry In dependentEntries.ToArray()
                        Me.InvalidateSingleEntry(entry)
                    Next
                End If
            Next

            Me.entriesLock.ExitWriteLock()
        End Sub

        ''' <summary>
        ''' Invalidates cache entry with a given key.
        ''' </summary>
        ''' <param name="key">The cache key.</param>
        Public Sub InvalidateItem(key As String) Implements ICache.InvalidateItem
            Me.entriesLock.EnterWriteLock()
            Dim entry As New CacheEntry
            If Me.entries.TryGetValue(key, entry) Then
                Me.InvalidateSingleEntry(entry)
            End If

            Me.entriesLock.ExitWriteLock()
        End Sub

        ''' <summary>
        ''' Releases unmanaged resources.
        ''' </summary>
        Public Sub Dispose() Implements IDisposable.Dispose
            Me.entriesLock.Dispose()
        End Sub

        Private Sub InvalidateSingleEntry(entry As CacheEntry)
            Me.RemoveFromLruChain(entry)

            Interlocked.Increment(_cacheItemInvalidations)
            Debug.Assert(Me.entriesLock.IsWriteLockHeld, "Must be holding write lock")
            Me.entries.Remove(entry.Key)
            For Each [set] As String In entry.DependentEntitySets
                Me.entitySetDependencies([set]).Remove(entry)
            Next
        End Sub

        Private Sub MoveToTopOfLruChain(entry As CacheEntry)
            SyncLock Me.lruLock
                If entry IsNot _lruChainHead Then
                    If entry Is _lruChainTail Then
                        _lruChainTail = _lruChainTail.PreviousEntry
                    End If

                    If entry.PreviousEntry IsNot Nothing Then
                        entry.PreviousEntry.NextEntry = entry.NextEntry
                    End If

                    If entry.NextEntry IsNot Nothing Then
                        entry.NextEntry.PreviousEntry = entry.PreviousEntry
                    End If

                    If _lruChainHead IsNot Nothing Then
                        _lruChainHead.PreviousEntry = entry
                    End If

                    entry.NextEntry = _lruChainHead
                    entry.PreviousEntry = Nothing
                    _lruChainHead = entry

                    If _lruChainTail Is Nothing Then
                        _lruChainTail = entry
                    End If
                End If
            End SyncLock
        End Sub

        Private Sub RemoveFromLruChain(entry As CacheEntry)
            SyncLock Me.lruLock
                If entry Is _lruChainHead Then
                    _lruChainHead = _lruChainHead.NextEntry
                End If

                If entry.PreviousEntry IsNot Nothing Then
                    entry.PreviousEntry.NextEntry = entry.NextEntry
                Else
                    _lruChainHead = entry.NextEntry
                End If

                If entry.NextEntry IsNot Nothing Then
                    entry.NextEntry.PreviousEntry = entry.PreviousEntry
                Else
                    _lruChainTail = entry.PreviousEntry
                End If
            End SyncLock
        End Sub

        Private Sub InvalidateExpiredEntries()
            Me.entriesLock.EnterWriteLock()

            Dim now = Me.GetCurrentDate().Invoke
            Dim nextEntry As CacheEntry
            Dim entryToExpire As CacheEntry = Me.LruChainHead
            While entryToExpire IsNot Nothing
                ' remember this reference as the invalication function will destroy it
                nextEntry = entryToExpire.NextEntry
                If now >= entryToExpire.ExpirationTime Then
                    Me.InvalidateSingleEntry(entryToExpire)
                End If
                entryToExpire = nextEntry
            End While

            Me.entriesLock.ExitWriteLock()
        End Sub

        ''' <summary>
        ''' Cache entry.
        ''' </summary>
        Friend Class CacheEntry
            Implements IEquatable(Of CacheEntry)
            Friend Property KeyHashCode() As Integer

            Friend Property Key() As String

            Friend Property Value() As Object

            Friend Property DependentEntitySets() As IEnumerable(Of String)

            Friend Property SlidingExpiration() As TimeSpan

            Friend Property ExpirationTime() As DateTime

            Friend Property LastAccessTime() As DateTime

            Friend Property NextEntry() As CacheEntry

            Friend Property PreviousEntry() As CacheEntry


            ''' <summary>
            ''' Determines whether the specified <see cref="System.Object"/> is equal to this instance.
            ''' </summary>
            ''' <param name="obj">The <see cref="System.Object"/> to compare with this instance.</param>
            ''' <returns>
            ''' A value of <c>true</c> if the specified <see cref="System.Object"/> is equal to this instance; otherwise, <c>false</c>.
            ''' </returns>
            ''' <exception cref="T:System.NullReferenceException">
            ''' The <paramref name="obj"/> parameter is null.
            ''' </exception>
            Public Overrides Function Equals(obj As Object) As Boolean
                Dim other As CacheEntry = TryCast(obj, CacheEntry)
                If other Is Nothing Then
                    Return False
                End If

                Return Me.Equals(other)
            End Function

            ''' <summary>
            ''' Determines whether the specified <see cref="CacheEntry"/> is equal to this instance.
            ''' </summary>
            ''' <param name="other">The other cache entry.</param>
            ''' <returns>
            ''' A value of <c>true</c> if the specified cache entry is equal to this instance; otherwise, <c>false</c>.
            ''' </returns>
            Public Overloads Function Equals(other As CacheEntry) As Boolean Implements IEquatable(Of Cache.InMemoryCache.CacheEntry).Equals
                If other Is Nothing Then
                    Throw New ArgumentNullException("other")
                End If

                If Me.KeyHashCode <> other.KeyHashCode Then
                    Return False
                End If

                Return Me.Key.Equals(other.Key, StringComparison.Ordinal)
            End Function

            ''' <summary>
            ''' Returns a hash code for this instance.
            ''' </summary>
            ''' <returns>
            ''' A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table. 
            ''' </returns>
            Public Overrides Function GetHashCode() As Integer
                Return Me.KeyHashCode
            End Function

        End Class
    End Class
End Namespace
