Imports System.Collections.Generic
Imports System.Data.Entity
Imports System.Web

Namespace Infrastructure
    Public Class WebDbContextStorage
        Implements IDbContextStorage

        'Public Sub New(app As HttpApplication)
        '    'This handler was not getting fired so it was moved into the web project.
        '    'AddHandler app.EndRequest, Sub(sender, args)
        '    '                               DbContextManager.CloseAllDbContexts()
        '    '                               HttpContext.Current.Items.Remove(STORAGE_KEY)

        '    '                           End Sub
        'End Sub

        Public Function GetDbContextForKey(key As String) As DbContext Implements IDbContextStorage.GetDbContextForKey
            Dim storage As SimpleDbContextStorage = GetSimpleDbContextStorage()
            Return storage.GetDbContextForKey(key)
        End Function

        Public Sub SetDbContextForKey(factoryKey As String, context As DbContext) Implements IDbContextStorage.SetDbContextForKey
            Dim storage As SimpleDbContextStorage = GetSimpleDbContextStorage()
            storage.SetDbContextForKey(factoryKey, context)
        End Sub

        Public Function GetAllDbContexts() As IEnumerable(Of DbContext) Implements IDbContextStorage.GetAllDbContexts
            Dim storage As SimpleDbContextStorage = GetSimpleDbContextStorage()
            Return storage.GetAllDbContexts()
        End Function

        Private Function GetSimpleDbContextStorage() As SimpleDbContextStorage
            Dim context As HttpContext = HttpContext.Current
            Dim storage As SimpleDbContextStorage = TryCast(context.Items(STORAGE_KEY), SimpleDbContextStorage)
            If storage Is Nothing Then
                storage = New SimpleDbContextStorage()
                context.Items(STORAGE_KEY) = storage
            End If
            Return storage
        End Function

        Private Const STORAGE_KEY As String = "HttpContextObjectContextStorageKey"
    End Class
End Namespace