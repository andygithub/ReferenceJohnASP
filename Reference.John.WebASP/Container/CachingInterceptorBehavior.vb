Imports Microsoft.Practices.Unity
Imports Microsoft.Practices.Unity.InterceptionExtension
Imports System.Linq
Imports Reference.John.Infrastructure

Namespace Container

    ''' <summary>
    ''' Class that handles caching for a defined set of interfaces.
    ''' </summary>
    ''' <remarks>This class is expected to be used as part of a container infrastructure</remarks>
    Public Class CachingInterceptorBehavior
        Implements IInterceptionBehavior

        Dim _configuration As Cache.ICacheProviderConfiguration
        Dim _logger As Reference.John.Core.Logging.ILogger

        Sub New(item As Cache.ICacheProviderConfiguration, logger As Reference.John.Core.Logging.ILogger)
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

            'determine if the method should have been cached
            Dim result = Nothing
            Dim command As New Cache.CacheCommand(input)

            If Not _configuration.DefaultCachingPolicy.CanBeCached(command) Then
                _logger.Info("CanBeCached False {0}", command.FullMethodName)
                'result shouldn't be cached just pass through and execute method.
                Return getNext.Invoke(input, getNext)
            End If
            'determine if the value is present in cache and return that value
            Dim cacheKey As String = command.GetCacheKey
            _configuration.DefaultCache.GetItem(cacheKey, result)

            'if found then return cache result
            If result IsNot Nothing Then
                _logger.Info("CanBeCached True - Item is in cache and not continuing with interception delegates.")
                Return input.CreateMethodReturn(result)
            End If
            'if not present in cache then continue to invoke
            _logger.Info("CanBeCached True - Item is not in cache.")
            Dim methodReturn = getNext.Invoke(input, getNext)
            'load configured cache settings
            Dim _commandSettings As Cache.CacheCommandDefinition = _configuration.DefaultCachingPolicy.GetCommandDefinition(command)
            'the return value should be serialized into cache
            'only put the item into cache if the return value doesn't exceed the row thresholds
            'may have to play around with this cast depending on the types of sets being returned.
            Dim collection = TryCast(methodReturn.ReturnValue, IEnumerable(Of Object))
            If collection Is Nothing Then
                _configuration.DefaultCache.PutItem(cacheKey, methodReturn.ReturnValue, _commandSettings.DependentEntities, _commandSettings.SlidingExpiration, _commandSettings.AbsoluteExpiration)
                _logger.Info("Item placed in cache. Bounds not checked")
            Else
                If collection.Count < _commandSettings.MinCacheableRows OrElse _commandSettings.MaxCacheableRows < collection.Count Then
                    _logger.Info("Item not placed in cache because it excedded the defined bounds.")
                Else
                    _configuration.DefaultCache.PutItem(cacheKey, methodReturn.ReturnValue, _commandSettings.DependentEntities, _commandSettings.SlidingExpiration, _commandSettings.AbsoluteExpiration)
                    _logger.Info("Item placed in cache. Bounds validate")
                End If
            End If
            Return methodReturn

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
