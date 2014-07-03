Imports Microsoft.ApplicationServer.Caching
Imports System.Security.Cryptography

Namespace Cache
    ''' <summary>
    ''' Cache provider implementation that is for appfabric.
    ''' </summary>
    ''' <remarks></remarks>
    Public Class AppFabricCache
        Implements ICache
        Private _cache As DataCache

        ''' <summary>
        ''' Default constructor that will load the endpoint information from a default config name.
        ''' </summary>
        ''' <remarks>The default cache is used.</remarks>
        Public Sub New()
            Dim conf As New DataCacheFactoryConfiguration("default")
            Dim fac As New DataCacheFactory(conf)
            _cache = fac.GetDefaultCache
        End Sub

        ''' <summary>
        ''' Constructor that will create an endpoint from the passed parameters.
        ''' </summary>
        ''' <param name="endPoint"></param>
        ''' <param name="portNumber"></param>
        ''' <remarks>The default cache is used.</remarks>
        Public Sub New(endPoint As String, portNumber As String)
            Dim conf As New DataCacheFactoryConfiguration() With {.Servers = New List(Of DataCacheServerEndpoint) From {New DataCacheServerEndpoint(endPoint, portNumber)}}
            Dim fac As New DataCacheFactory(conf)
            _cache = fac.GetDefaultCache
        End Sub

        ''' <summary>
        ''' Constructor that will create an endpoint from the passed parameters.
        ''' </summary>
        ''' <param name="endPoint"></param>
        ''' <param name="portNumber"></param>
        ''' <param name="cacheName"></param>
        ''' <remarks>The cache name connected to is based on teh cache name parameter.</remarks>
        Public Sub New(endPoint As String, portNumber As String, cacheName As String)
            If cacheName Is Nothing Then Throw New ArgumentNullException("cacheName")
            Dim conf As New DataCacheFactoryConfiguration() With {.Servers = New List(Of DataCacheServerEndpoint) From {New DataCacheServerEndpoint(endPoint, portNumber)}}
            Dim fac As New DataCacheFactory(conf)
            _cache = fac.GetCache(cacheName)
        End Sub

        ''' <summary>
        ''' Constructor that takes an already initialized data cache instance.
        ''' </summary>
        ''' <param name="cache"></param>
        ''' <remarks></remarks>
        Public Sub New(cache As DataCache)
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
            value = _cache.Get(key)

            Return value IsNot Nothing
        End Function

        ''' <summary>
        ''' Method to put an item into cache.
        ''' </summary>
        ''' <param name="key"></param>
        ''' <param name="value"></param>
        ''' <param name="dependentEntitySets"></param>
        ''' <param name="slidingExpiration"></param>
        ''' <param name="absoluteExpiration"></param>
        ''' <remarks></remarks>
        Public Sub PutItem(key As String, value As Object, dependentEntitySets As IEnumerable(Of String), slidingExpiration As TimeSpan, absoluteExpiration As DateTime) Implements ICache.PutItem
            If IsNothing(slidingExpiration) OrElse slidingExpiration = TimeSpan.Zero Then
                _cache.Put(key, value, absoluteExpiration - DateTime.Now, dependentEntitySets.[Select](Function(c) New DataCacheTag(c)).ToList())
            Else
                _cache.Put(key, value, slidingExpiration, dependentEntitySets.Select(Function(c) New DataCacheTag(c)).ToList())
            End If

            For Each dep In dependentEntitySets
                CreateRegionIfNeeded(dep)
                _cache.Put(key, " ", dep)
            Next
        End Sub

        ''' <summary>
        ''' Method to remove all entries from a region.  The region is determined by the list of strings passed.
        ''' </summary>
        ''' <param name="entitySets"></param>
        ''' <remarks></remarks>
        Public Sub InvalidateSets(entitySets As IEnumerable(Of String)) Implements ICache.InvalidateSets
            ' Go through the list of objects in each of the set. 
            For Each dep In entitySets
                For Each item In _cache.GetObjectsInRegion(dep)
                    _cache.Remove(item.Key)
                Next
            Next
        End Sub

        ''' <summary>
        ''' Method to remove a specific key from the cache.  This will also related tags.
        ''' </summary>
        ''' <param name="key"></param>
        ''' <remarks></remarks>
        Public Sub InvalidateItem(key As String) Implements ICache.InvalidateItem

            Dim item As DataCacheItem = _cache.GetCacheItem(key)
            _cache.Remove(key)
            If item Is Nothing Then Exit Sub

            For Each tag In item.Tags
                _cache.Remove(key, tag.ToString())
            Next
        End Sub

        ''' <summary>
        ''' Method to create a region if it doesn't exist.
        ''' </summary>
        ''' <param name="regionName"></param>
        ''' <remarks></remarks>
        Private Sub CreateRegionIfNeeded(regionName As String)
            Try
                _cache.CreateRegion(regionName)
            Catch de As DataCacheException
                If de.ErrorCode <> DataCacheErrorCode.RegionAlreadyExists Then
                    Throw
                End If
            End Try
        End Sub

    End Class
End Namespace
