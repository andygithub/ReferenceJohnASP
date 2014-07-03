Namespace Cache

    ''' <summary>
    ''' Caching policy.
    ''' </summary>
    Public Interface ICachingPolicy
        ''' <summary>
        ''' Gets the collection of cacheable commands.
        ''' </summary>
        ''' <value>The cacheable tables.</value>
        Property CacheableCommands() As ICollection(Of CacheCommandDefinition)

        ''' <summary>
        ''' Determines whether the specified command definition can be cached.
        ''' </summary>
        ''' <param name="cachingCommand">The command definition.</param>
        ''' <returns>
        ''' A value of <c>true</c> if the specified command definition can be cached; otherwise, <c>false</c>.
        ''' </returns>
        Function CanBeCached(cachingCommand As CacheCommand) As Boolean
        ''' <summary>
        ''' Determines whether the specified command definition will cause the cache to be cleared.
        ''' </summary>
        ''' <param name="cachingCommand">The command definition.</param>
        ''' <returns>
        ''' A value of <c>true</c> if the specified command definition can be will cause the cache to be cleared; otherwise, <c>false</c>.
        ''' </returns>
        Function IsCacheReset(cachingCommand As CacheCommand) As Boolean

        ''' <summary>
        ''' Returns the command defintion based on the cache command parameter.
        ''' </summary>
        ''' <param name="cachingCommand"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Function GetCommandDefinition(cachingCommand As CacheCommand) As Cache.CacheCommandDefinition
        
    End Interface

End Namespace
