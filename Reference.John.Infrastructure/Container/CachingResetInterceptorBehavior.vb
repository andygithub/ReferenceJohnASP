Imports Microsoft.Practices.Unity
Imports Microsoft.Practices.Unity.InterceptionExtension
Imports Reference.John.Infrastructure

Namespace Container

    ''' <summary>
    ''' Class that handles caching for a defined set of interfaces.
    ''' </summary>
    ''' <remarks>This class is expected to be used as part of a container infrastructure</remarks>
    Public Class CachingResetInterceptorBehavior
        Implements IInterceptionBehavior

        Dim _configuration As Cache.ICacheProviderConfiguration
        Dim _logger As Reference.John.Infrastructure.Logging.ILogger

        Sub New(item As Cache.ICacheProviderConfiguration, logger As Reference.John.Infrastructure.Logging.ILogger)
            If item Is Nothing Then Throw New ArgumentNullException("item")
            If logger Is Nothing Then Throw New ArgumentNullException("logger")
            _configuration = item
            _logger = logger
        End Sub

        ''' <summary>
        ''' Implementation of required method for interface.
        ''' </summary>
        ''' <returns>Returns a list of types that are required for this invoke method to be part of the interception chain.</returns>
        ''' <remarks></remarks>
        Public Function GetRequiredInterfaces() As System.Collections.Generic.IEnumerable(Of System.Type) Implements Microsoft.Practices.Unity.InterceptionExtension.IInterceptionBehavior.GetRequiredInterfaces
            Return Type.EmptyTypes
        End Function

        ''' <summary>
        ''' Implementation of the invoke method which does result caching of the configured commands.
        ''' </summary>
        ''' <param name="input"></param>
        ''' <param name="getNext"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function Invoke(input As Microsoft.Practices.Unity.InterceptionExtension.IMethodInvocation, getNext As Microsoft.Practices.Unity.InterceptionExtension.GetNextInterceptionBehaviorDelegate) As Microsoft.Practices.Unity.InterceptionExtension.IMethodReturn Implements Microsoft.Practices.Unity.InterceptionExtension.IInterceptionBehavior.Invoke
            'determine if caching is turned on this takes place in WillExecute
            Dim result = Nothing
            Dim command As New Cache.CacheCommand(input)

            'determine if cache should be cleared for the method
            If _configuration.DefaultCachingPolicy.IsCacheReset(Command) Then
                'remove the item from the cache
                'load configured cache settings
                _logger.Info(Reference.John.Resources.Resources.LogMessages.CacheResetCommandHit)
                Dim _commandSettings As Cache.CacheCommandDefinition = _configuration.DefaultCachingPolicy.GetCommandDefinition(command)
                _configuration.DefaultCache.InvalidateSets(New List(Of String) From {_commandSettings.EntityName})
                _logger.Info(Reference.John.Resources.Resources.LogMessages.CacheItemClear)
            End If
            _logger.Info(Reference.John.Resources.Resources.LogMessages.CacheResetCommandMiss)
            Return getNext.Invoke(input, getNext)

        End Function

        ''' <summary>
        ''' Method to determine if the behavior will execute.
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public ReadOnly Property WillExecute As Boolean Implements Microsoft.Practices.Unity.InterceptionExtension.IInterceptionBehavior.WillExecute
            Get
                Return _configuration.IsCachingEnabled
            End Get
        End Property
    End Class

End Namespace
