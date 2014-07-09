Imports System.Runtime.caching

Namespace Cache
    ''' <summary>
    ''' Cache implementation that wraps the .net framework cache implementation.
    ''' </summary>
    Public NotInheritable Class FrameworkCache
        Implements ICache

        Private _cache As ObjectCache

        ''' <summary>
        ''' 
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub New()
            _cache = MemoryCache.[Default]
        End Sub

        ''' <summary>
        ''' Constructor that takes an already initialized data cache instance.
        ''' </summary>
        ''' <param name="cache"></param>
        ''' <remarks></remarks>
        Public Sub New(cache As ObjectCache)
            If cache Is Nothing Then Throw New ArgumentNullException("cache")
            _cache = cache
        End Sub

        ''' <summary>
        ''' 
        ''' </summary>
        ''' <param name="key"></param>
        ''' <param name="value"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function GetItem(key As String, ByRef value As Object) As Boolean Implements ICache.GetItem
            Dim _item = _cache.GetCount
            value = _cache.Get(key)
            Return value IsNot Nothing
        End Function

        ''' <summary>
        ''' Remove an item from the cache.
        ''' </summary>
        ''' <param name="key"></param>
        ''' <remarks></remarks>
        Public Sub InvalidateItem(key As String) Implements ICache.InvalidateItem
            _cache.Remove(key)

        End Sub

        ''' <summary>
        ''' Remove a set of items from the cache.
        ''' </summary>
        ''' <param name="entitySets"></param>
        ''' <remarks></remarks>
        Public Sub InvalidateSets(entitySets As IEnumerable(Of String)) Implements ICache.InvalidateSets
            For Each item In entitySets
                InvalidateItem(item)
            Next
        End Sub

        ''' <summary>
        ''' Method to put an item into cache.
        ''' </summary>
        ''' <param name="key"></param>
        ''' <param name="value"></param>
        ''' <param name="dependentEntitySets"></param>
        ''' <param name="slidingExpiration"></param>
        ''' <param name="absoluteExpiration"></param>
        ''' <remarks></remarks>
        Public Sub PutItem(key As String, value As Object, dependentEntitySets As IEnumerable(Of String), slidingExpiration As TimeSpan, absoluteExpiration As Date) Implements ICache.PutItem
            Dim _cacheItem As New CacheItem(key, value)
            Dim _cachePolicy As New CacheItemPolicy
            If IsNothing(slidingExpiration) OrElse slidingExpiration = TimeSpan.Zero Then
                _cachePolicy.AbsoluteExpiration = New DateTimeOffset(absoluteExpiration)
            Else
                _cachePolicy.SlidingExpiration = slidingExpiration
            End If
            _cache.Add(_cacheItem, _cachePolicy)
            'TODO implement a custom implementation that supports regions in a similar manner to appfabric.

        End Sub

    End Class

    Public Class CustomRegionCache
        Inherits MemoryCache

        Public Sub New()
            MyBase.New("defaultCustomCache")
        End Sub

        Public Overrides Sub [Set](ByVal item As CacheItem, ByVal policy As CacheItemPolicy)
            [Set](item.Key, item.Value, policy, item.RegionName)
        End Sub

        Public Overrides Sub [Set](ByVal key As String, ByVal value As Object, ByVal absoluteExpiration As DateTimeOffset, Optional ByVal regionName As String = Nothing)
            [Set](key, value, New CacheItemPolicy With {.AbsoluteExpiration = absoluteExpiration}, regionName)
        End Sub

        Public Overrides Sub [Set](ByVal key As String, ByVal value As Object, ByVal policy As CacheItemPolicy, Optional ByVal regionName As String = Nothing)
            MyBase.Set(CreateKeyWithRegion(key, regionName), value, policy)
        End Sub

        Public Overrides Function GetCacheItem(ByVal key As String, Optional ByVal regionName As String = Nothing) As CacheItem
            Dim temporary As CacheItem = MyBase.GetCacheItem(CreateKeyWithRegion(key, regionName))
            Return New CacheItem(key, temporary.Value, regionName)
        End Function

        Public Overrides Function [Get](ByVal key As String, Optional ByVal regionName As String = Nothing) As Object
            Return MyBase.Get(CreateKeyWithRegion(key, regionName))
        End Function

        Public Overrides ReadOnly Property DefaultCacheCapabilities() As DefaultCacheCapabilities
            Get
                Return (MyBase.DefaultCacheCapabilities Or System.Runtime.Caching.DefaultCacheCapabilities.CacheRegions)
            End Get
        End Property

        Private Function CreateKeyWithRegion(ByVal key As String, ByVal region As String) As String
            Return "region:" & (If(region Is Nothing, "null_region", region)) & ";key=" & key
        End Function

    End Class

End Namespace
