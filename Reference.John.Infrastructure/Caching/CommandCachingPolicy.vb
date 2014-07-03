Namespace Cache
    ''' <summary>
    ''' Custom caching policy on per-command basis.
    ''' </summary>
    Public Class CommandCachingPolicy
        Implements ICachingPolicy

        ''' <summary>
        ''' Initializes a new instance of the CustomCachingPolicy class.
        ''' </summary>
        Public Sub New()
            Me.CacheableCommands = New HashSet(Of CacheCommandDefinition)
        End Sub


        ''' <summary>
        ''' Gets the collection of cacheable commands.
        ''' </summary>
        ''' <value>The cacheable tables.</value>
        Public Property CacheableCommands() As ICollection(Of CacheCommandDefinition) Implements ICachingPolicy.CacheableCommands

        ''' <summary>
        ''' Determines whether the specified command definition can be cached.
        ''' </summary>
        ''' <param name="command">The command definition.</param>
        ''' <returns>
        ''' A value of <c>true</c> if the specified command definition can be cached; otherwise, <c>false</c>.
        ''' </returns>
        Public Function CanBeCached(ByVal command As CacheCommand) As Boolean Implements ICachingPolicy.CanBeCached
            If command Is Nothing Then Throw New ArgumentNullException("command")
            'map the cachecommand from the execution to the configured cachecommanddefintion if it is present in any items thern return true
            Return (From f In CacheableCommands Where f.CacheMethodName.Contains(command.FullMethodName)).Any
        End Function

        ''' <summary>
        ''' Returns the command defintion based on the cache command parameter.
        ''' </summary>
        ''' <param name="cachingCommand"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function GetCommandDefinition(cachingCommand As CacheCommand) As CacheCommandDefinition Implements ICachingPolicy.GetCommandDefinition
            Dim item = (From f In CacheableCommands Where f.CacheMethodName.Contains(cachingCommand.FullMethodName) Or f.CacheResetMethodName.Contains(cachingCommand.FullMethodName) Select f).FirstOrDefault
            If item Is Nothing Then Return New CacheCommandDefinition
            Return item
        End Function

        ''' <summary>
        ''' Determines whether the specified command definition will cause the cache to be cleared.
        ''' </summary>
        ''' <param name="cachingCommand">The command definition.</param>
        ''' <returns>
        ''' A value of <c>true</c> if the specified command definition can be will cause the cache to be cleared; otherwise, <c>false</c>.
        ''' </returns>
        Public Function IsCacheReset(cachingCommand As CacheCommand) As Boolean Implements ICachingPolicy.IsCacheReset
            If Command() Is Nothing Then Throw New ArgumentNullException("command")
            'map the cachecommand from the execution to the configured cachecommanddefintion if it is present in any items thern return true
            Return (From f In CacheableCommands Where f.CacheResetMethodName.Contains(cachingCommand.FullMethodName)).Any
        End Function
    End Class

End Namespace
