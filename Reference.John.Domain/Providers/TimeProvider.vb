Namespace Providers

    Public MustInherit Class TimeProvider
        Private Shared _current As TimeProvider = DefaultTimeProvider.Instance

        Public Shared Property Current() As TimeProvider
            Get
                Return _current
            End Get
            Set(value As TimeProvider)
                If value Is Nothing Then
                    Throw New ArgumentNullException("value")
                End If
                _current = value
            End Set
        End Property

        Public MustOverride ReadOnly Property UtcNow() As DateTime

        Public Shared Sub ResetToDefault()
            TimeProvider.Current = DefaultTimeProvider.Instance
        End Sub
    End Class


    Public Class DefaultTimeProvider
        Inherits TimeProvider
        Private Shared ReadOnly _instance As New DefaultTimeProvider()

        Private Sub New()
        End Sub

        Public Overrides ReadOnly Property UtcNow() As DateTime
            Get
                Return DateTime.UtcNow
            End Get
        End Property

        Public Shared ReadOnly Property Instance() As DefaultTimeProvider
            Get
                Return DefaultTimeProvider._instance
            End Get
        End Property
    End Class

End Namespace