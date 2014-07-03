Namespace Cache
    ''' <summary>
    ''' Interface that is a container for all caching settings and the cache provider implementation.
    ''' </summary>
    ''' <remarks></remarks>
    Public Interface ICacheProviderConfiguration

        ''' <summary>
        ''' Flag to determine if caching is enabled.
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Property IsCachingEnabled As Boolean

        ''' <summary>
        ''' Cache implementation that is used to get items from cache and put items in cache.
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Property DefaultCache As ICache

        ''' <summary>
        ''' Instance of the caching policy that is configured.
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Property DefaultCachingPolicy As ICachingPolicy

    End Interface

End Namespace