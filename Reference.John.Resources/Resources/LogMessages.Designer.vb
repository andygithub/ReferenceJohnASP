﻿'------------------------------------------------------------------------------
' <auto-generated>
'     This code was generated by a tool.
'     Runtime Version:4.0.30319.18444
'
'     Changes to this file may cause incorrect behavior and will be lost if
'     the code is regenerated.
' </auto-generated>
'------------------------------------------------------------------------------

Option Strict On
Option Explicit On

Imports System

Namespace Resources

    'This class was auto-generated by the StronglyTypedResourceBuilder
    'class via a tool like ResGen or Visual Studio.
    'To add or remove a member, edit your .ResX file then rerun ResGen
    'with the /str option, or rebuild your VS project.
    '''<summary>
    '''  A strongly-typed resource class, for looking up localized strings, etc.
    '''</summary>
    <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0"), _
     Global.System.Diagnostics.DebuggerNonUserCodeAttribute(), _
     Global.System.Runtime.CompilerServices.CompilerGeneratedAttribute()> _
    Public Class LogMessages

        Private Shared resourceMan As Global.System.Resources.ResourceManager

        Private Shared resourceCulture As Global.System.Globalization.CultureInfo

        <Global.System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")> _
        Friend Sub New()
            MyBase.New()
        End Sub

        '''<summary>
        '''  Returns the cached ResourceManager instance used by this class.
        '''</summary>
        <Global.System.ComponentModel.EditorBrowsableAttribute(Global.System.ComponentModel.EditorBrowsableState.Advanced)> _
        Public Shared ReadOnly Property ResourceManager() As Global.System.Resources.ResourceManager
            Get
                If Object.ReferenceEquals(resourceMan, Nothing) Then
                    Dim temp As Global.System.Resources.ResourceManager = New Global.System.Resources.ResourceManager("Reference.John.Resources.LogMessages", GetType(LogMessages).Assembly)
                    resourceMan = temp
                End If
                Return resourceMan
            End Get
        End Property

        '''<summary>
        '''  Overrides the current thread's CurrentUICulture property for all
        '''  resource lookups using this strongly typed resource class.
        '''</summary>
        <Global.System.ComponentModel.EditorBrowsableAttribute(Global.System.ComponentModel.EditorBrowsableState.Advanced)> _
        Public Shared Property Culture() As Global.System.Globalization.CultureInfo
            Get
                Return resourceCulture
            End Get
            Set(value As Global.System.Globalization.CultureInfo)
                resourceCulture = value
            End Set
        End Property

        '''<summary>
        '''  Looks up a localized string similar to DbContextInitializer.Instance.InitializeDbContextOnce End .
        '''</summary>
        Public Shared ReadOnly Property DbContextInitializerInstanceInitializeDbContextOnceEnd() As String
            Get
                Return ResourceManager.GetString("DbContextInitializerInstanceInitializeDbContextOnceEnd", resourceCulture)
            End Get
        End Property

        '''<summary>
        '''  Looks up a localized string similar to DbContextInitializer.Instance.InitializeDbContextOnce Start.
        '''</summary>
        Public Shared ReadOnly Property DbContextInitializerInstanceInitializeDbContextOnceStart() As String
            Get
                Return ResourceManager.GetString("DbContextInitializerInstanceInitializeDbContextOnceStart", resourceCulture)
            End Get
        End Property

        '''<summary>
        '''  Looks up a localized string similar to DbContextManager.CloseAllDbContexts End.
        '''</summary>
        Public Shared ReadOnly Property DbContextManagerCloseAllDbContextsEnd() As String
            Get
                Return ResourceManager.GetString("DbContextManagerCloseAllDbContextsEnd", resourceCulture)
            End Get
        End Property

        '''<summary>
        '''  Looks up a localized string similar to DbContextManager.CloseAllDbContexts Start.
        '''</summary>
        Public Shared ReadOnly Property DbContextManagerCloseAllDbContextsStart() As String
            Get
                Return ResourceManager.GetString("DbContextManagerCloseAllDbContextsStart", resourceCulture)
            End Get
        End Property

        '''<summary>
        '''  Looks up a localized string similar to LoggingInterceptorBehavior End Execute: {0},{1}.
        '''</summary>
        Public Shared ReadOnly Property LoggingInterceptorEnd() As String
            Get
                Return ResourceManager.GetString("LoggingInterceptorEnd", resourceCulture)
            End Get
        End Property

        '''<summary>
        '''  Looks up a localized string similar to LoggingInterceptorBehavior Start Execute: {0},{1}.
        '''</summary>
        Public Shared ReadOnly Property LoggingInterceptorStart() As String
            Get
                Return ResourceManager.GetString("LoggingInterceptorStart", resourceCulture)
            End Get
        End Property

        '''<summary>
        '''  Looks up a localized string similar to ReturnValue.ToString: {0}.
        '''</summary>
        Public Shared ReadOnly Property LoggingReturnValue() As String
            Get
                Return ResourceManager.GetString("LoggingReturnValue", resourceCulture)
            End Get
        End Property

        '''<summary>
        '''  Looks up a localized string similar to Page Init Event Ended..
        '''</summary>
        Public Shared ReadOnly Property PageInitEnded() As String
            Get
                Return ResourceManager.GetString("PageInitEnded", resourceCulture)
            End Get
        End Property

        '''<summary>
        '''  Looks up a localized string similar to Page Unload Event Starting..
        '''</summary>
        Public Shared ReadOnly Property PageUnloadStarted() As String
            Get
                Return ResourceManager.GetString("PageUnloadStarted", resourceCulture)
            End Get
        End Property

        '''<summary>
        '''  Looks up a localized string similar to WebContextStorage Constructor End.
        '''</summary>
        Public Shared ReadOnly Property WebContextStorageConstructorEnd() As String
            Get
                Return ResourceManager.GetString("WebContextStorageConstructorEnd", resourceCulture)
            End Get
        End Property

        '''<summary>
        '''  Looks up a localized string similar to WebContextStorage Constructor Start.
        '''</summary>
        Public Shared ReadOnly Property WebContextStorageConstructorStart() As String
            Get
                Return ResourceManager.GetString("WebContextStorageConstructorStart", resourceCulture)
            End Get
        End Property
    End Class
End Namespace
