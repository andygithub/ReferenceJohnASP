Namespace Cache
    ''' <summary>
    ''' Class that is a container for all caching settings and the cache provider implementation.
    ''' </summary>
    ''' <remarks></remarks>
    Public Class CacheProviderConfiguration
        Implements ICacheProviderConfiguration

        ''' <summary>
        ''' Flag to determine if caching is enabled.
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property IsCachingEnabled As Boolean Implements ICacheProviderConfiguration.IsCachingEnabled

        ''' <summary>
        ''' Cache implementation that is used to get items from cache and put items in cache.
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property DefaultCache As ICache Implements ICacheProviderConfiguration.DefaultCache

        ''' <summary>
        ''' Instance of the caching policy that is configured.
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property DefaultCachingPolicy As ICachingPolicy Implements ICacheProviderConfiguration.DefaultCachingPolicy
    End Class


End Namespace