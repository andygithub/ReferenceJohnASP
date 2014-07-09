﻿Imports Microsoft.Practices.Unity
Imports Microsoft.Practices.Unity.InterceptionExtension

Namespace Container

    ''' <summary>
    ''' Class that handles logging for a defined set of interfaces.
    ''' </summary>
    ''' <remarks>This class is expected to be used as part of a container infrastructure</remarks>
    Public Class LoggingInterceptorBehavior
        Implements IInterceptionBehavior

        Dim _logger As John.Core.Logging.ILogger

        Sub New(logger As John.Core.Logging.ILogger)
            If logger Is Nothing Then Throw New ArgumentNullException("logger")
            _logger = logger
        End Sub

        ''' <summary>
        ''' Implementation of required method for interface.
        ''' </summary>
        ''' <returns>Returns a list of types that are required for this invoke method to be part of the interception chain.</returns>
        ''' <remarks></remarks>
        Public Function GetRequiredInterfaces() As System.Collections.Generic.IEnumerable(Of System.Type) Implements Microsoft.Practices.Unity.InterceptionExtension.IInterceptionBehavior.GetRequiredInterfaces
            Return Type.EmptyTypes 'New List(Of Type)(New Type() {GetType(Type.emptytypes)}) ' Type.EmptyTypes
        End Function

        ''' <summary>
        ''' Implementation of the invoke method which does some simple logging of the defined interface parameters.
        ''' </summary>
        ''' <param name="input"></param>
        ''' <param name="getNext"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function Invoke(input As Microsoft.Practices.Unity.InterceptionExtension.IMethodInvocation, getNext As Microsoft.Practices.Unity.InterceptionExtension.GetNextInterceptionBehaviorDelegate) As Microsoft.Practices.Unity.InterceptionExtension.IMethodReturn Implements Microsoft.Practices.Unity.InterceptionExtension.IInterceptionBehavior.Invoke

            _logger.Info(John.Resources.Resources.LogMessages.LoggingInterceptorStart, input.MethodBase.DeclaringType.FullName, input.MethodBase.ToString)
            Dim result = getNext.Invoke(input, getNext)
            _logger.Info(John.Resources.Resources.LogMessages.LoggingInterceptorEnd, input.MethodBase.DeclaringType.FullName, input.MethodBase.ToString)
            'If result IsNot Nothing AndAlso result.ReturnValue IsNot Nothing Then
            '    _logger.Info(Reference.John.Resources.Resources.LogMessages.LoggingReturnValue, result.ReturnValue.ToString)
            'End If
            Return result

        End Function

        ''' <summary>
        ''' Method to determine if the behavior will execute.
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public ReadOnly Property WillExecute As Boolean Implements Microsoft.Practices.Unity.InterceptionExtension.IInterceptionBehavior.WillExecute
            Get
                Return True
            End Get
        End Property
    End Class

End Namespace
