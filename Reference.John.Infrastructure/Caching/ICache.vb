
Namespace Cache
    ''' <summary>
    ''' Interface to be implemented by cache implementations.
    ''' </summary>
    Public Interface ICache
        ''' <summary>
        ''' Tries to the get cached entry by key.
        ''' </summary>
        ''' <param name="key">The cache key.</param>
        ''' <param name="value">The retrieved value.</param>
        ''' <returns>A value of <c>true</c> if entry was found in the cache, <c>false</c> otherwise.</returns>
        Function GetItem(key As String, ByRef value As Object) As Boolean

        ''' <summary>
        ''' Adds the specified entry to the cache.
        ''' </summary>
        ''' <param name="key">The entry key.</param>
        ''' <param name="value">The entry value.</param>
        ''' <param name="dependentEntitySets">The list of dependent entity sets.</param>
        ''' <param name="slidingExpiration">The sliding expiration.</param>
        ''' <param name="absoluteExpiration">The absolute expiration.</param>
        Sub PutItem(key As String, value As Object, dependentEntitySets As IEnumerable(Of String), slidingExpiration As TimeSpan, absoluteExpiration As DateTime)

        ''' <summary>
        ''' Invalidates all cache entries which are dependent on any of the specified entity sets.
        ''' </summary>
        ''' <param name="entitySets">The entity sets.</param>
        Sub InvalidateSets(entitySets As IEnumerable(Of String))

        ''' <summary>
        ''' Invalidates cache entry with a given key.
        ''' </summary>
        ''' <param name="key">The cache key.</param>
        Sub InvalidateItem(key As String)
    End Interface
End Namespace






