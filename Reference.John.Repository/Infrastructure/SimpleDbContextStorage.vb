Imports System.Collections.Generic
Imports System.Data.Entity

Namespace Infrastructure
    Public Class SimpleDbContextStorage
        Implements IDbContextStorage
        Private _storage As New Dictionary(Of String, DbContext)()

        ''' <summary>
        ''' Initializes a new instance of the <see cref="SimpleDbContextStorage"/> class.
        ''' </summary>
        Public Sub New()
        End Sub

        ''' <summary>
        ''' Returns the db context associated with the specified key or
        ''' null if the specified key is not found.
        ''' </summary>
        ''' <param name="key">The key.</param>
        ''' <returns></returns>
        Public Function GetDbContextForKey(key As String) As DbContext Implements IDbContextStorage.GetDbContextForKey
            Dim context As DbContext = Nothing
            If Not Me._storage.TryGetValue(key, context) Then
                Return Nothing
            End If
            Return context
        End Function


        ''' <summary>
        ''' Stores the db context into a dictionary using the specified key.
        ''' If an object context already exists by the specified key, 
        ''' it gets overwritten by the new object context passed in.
        ''' </summary>
        ''' <param name="key">The key.</param>
        ''' <param name="context">The object context.</param>
        Public Sub SetDbContextForKey(key As String, context As DbContext) Implements IDbContextStorage.SetDbContextForKey
            Me._storage.Add(key, context)
        End Sub

        ''' <summary>
        ''' Returns all the values of the internal dictionary of db contexts.
        ''' </summary>
        ''' <returns></returns>
        Public Function GetAllDbContexts() As IEnumerable(Of DbContext) Implements IDbContextStorage.GetAllDbContexts
            Return Me._storage.Values
        End Function
    End Class
End Namespace