Namespace Infrastructure
    Public Class DbContextInitializer
        Private Shared ReadOnly _syncLock As New Object()
        Private Shared _instance As DbContextInitializer

        Protected Sub New()
        End Sub

        Private _isInitialized As Boolean = False

        Public Shared Function Instance() As DbContextInitializer
            If _instance Is Nothing Then
                SyncLock _syncLock
                    If _instance Is Nothing Then
                        _instance = New DbContextInitializer()
                    End If
                End SyncLock
            End If

            Return _instance
        End Function

        ''' <summary>
        ''' This is the method which should be given the call to intialize the DbContext; e.g.,
        ''' DbContextInitializer.Instance().InitializeDbContextOnce(() => InitializeDbContext());
        ''' where InitializeDbContext() is a method which calls DbContextManager.Init()
        ''' </summary>
        ''' <param name="initMethod"></param>
        Public Sub InitializeDbContextOnce(initMethod As Action)
            SyncLock _syncLock
                If Not _isInitialized Then
                    initMethod()
                    _isInitialized = True
                End If
            End SyncLock
        End Sub
    End Class
End Namespace