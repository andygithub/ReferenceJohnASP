Imports System.Collections.Generic
Imports System.Data.Entity

Namespace Infrastructure
    ''' <summary>
    ''' Stores object context
    ''' </summary>
    Public Interface IDbContextStorage
        ''' <summary>
        ''' Gets the db context for key.
        ''' </summary>
        ''' <param name="key">The key.</param>
        ''' <returns></returns>
        Function GetDbContextForKey(key As String) As DbContext

        ''' <summary>
        ''' Sets the db context for key.
        ''' </summary>
        ''' <param name="key">The key.</param>
        ''' <param name="objectContext">The object context.</param>
        Sub SetDbContextForKey(key As String, objectContext As DbContext)

        ''' <summary>
        ''' Gets all db contexts.
        ''' </summary>
        ''' <returns></returns>
        Function GetAllDbContexts() As IEnumerable(Of DbContext)
    End Interface
End Namespace