Imports System.Data.Entity


Public Class FormContactZeroReadOnlyRepository
    Inherits GenericRepository
    Implements IFormContactZeroRepository
    Public Sub New()
        MyBase.New()
    End Sub

    ''' <summary>
    ''' Initializes a new instance of the <see cref="GenericRepository"/> class.
    ''' </summary>
    ''' <param name="connectionStringName">Name of the connection string.</param>
    Public Sub New(connectionStringName As String)
        MyBase.New(connectionStringName)
    End Sub

    Public Sub New(context As System.Data.Entity.DbContext)
        MyBase.New(context)
    End Sub

    Public Function NewlyCreated() As IList(Of Domain.FormSimpleZero) Implements IFormContactZeroRepository.NewlyCreated
        Dim lastMonth = DateTime.Now.[Date].AddMonths(-1)

        Return GetQuery(Of Domain.FormSimpleZero)().Where(Function(c) c.DateCreated >= lastMonth).AsNoTracking.ToList()
    End Function

    Public Function FindByName(firstname As String, lastname As String) As Domain.FormSimpleZero Implements IFormContactZeroRepository.FindByName
        Return GetQuery(Of Domain.FormSimpleZero)().Where(Function(c) c.FirstName = firstname AndAlso c.LastName = lastname).AsNoTracking.FirstOrDefault()
    End Function
End Class
