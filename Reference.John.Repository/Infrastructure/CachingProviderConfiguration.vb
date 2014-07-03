''Imports EFTracingProvider
''Imports EFCachingProvider.Caching

''Imports Microsoft.ApplicationServer.Caching

'Namespace Infrastructure

'    Public Class CachingProviderConfiguration

'        Public Shared Sub Init()
'            'Configure in memory or app fabric provider
'            'Configure the entities to cache
'            'TODO move this hardcoded settings into configuration.
'            'cache init 
'            Dim _cache As New InMemoryCache
'            Dim _cachingPolicy As New CustomCachingPolicy()
'            'by default all tables are cached, exclude tables below
'            _cachingPolicy.NonCacheableTables.Add("Address")
'            'cache config defaults setup
'            EFCachingProvider.EFCachingProviderConfiguration.DefaultCache = _cache
'            EFCachingProvider.EFCachingProviderConfiguration.DefaultCachingPolicy = _cachingPolicy
'        End Sub

'        'appfabric cache config
'        'cache init 
'        '_cache = CreateAppFabricCache
'        '_cachingPolicy = CachingPolicy.CacheAll
'        ''cache config defaults setup
'        'EFCachingProvider.EFCachingProviderConfiguration.DefaultCache = _cache
'        'EFCachingProvider.EFCachingProviderConfiguration.DefaultCachingPolicy = _cachingPolicy

'        Function CreateAppFabricCache() As ICache
'            'TODO move these hard coded local dev settings into config
'            ' Declare an array for the cache host
'            Dim server = New List(Of DataCacheServerEndpoint)()
'            server.Add(New DataCacheServerEndpoint("localhost", 22233))
'            ' Set up the DataCacheFactory configuration
'            Dim conf = New DataCacheFactoryConfiguration()
'            conf.Servers = server
'            Dim fac As New DataCacheFactory(conf)
'            Return New EFAppFabricCacheAdapter.AppFabricCache(fac.GetDefaultCache())

'        End Function

'    End Class

'End Namespace