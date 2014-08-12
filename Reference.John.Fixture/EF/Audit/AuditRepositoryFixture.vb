Imports System.Linq.Expressions
Imports System.Data.Entity
Imports EntityFramework.Audit
Imports EntityFramework.Extensions
Imports Reference.John
Imports Reference.John.Repository
Imports Reference.John.Repository.Infrastructure


<TestClass()> Public Class AuditRepositoryFixture

    Private repository As IRepository

    <TestInitialize()>
    Public Sub Init()
        Try
            DbContextManager.InitStorage(New SimpleDbContextStorage())
        Catch ex As ApplicationException
            'catch this in case the storage has already been pinned for the app doamin.  This handles the tests being run in any order.  Currently the storage can't be torn down.
        End Try
        Try
            DbContextManager.Init(DbContextManager.DefaultConnectionStringName, True, Sub(x) Console.WriteLine(x))
            'catch this in case the connection string has already been registered.  This handles the tests being run in any order.  Currently the storage can't be torn down.
        Catch ex As ArgumentException
        End Try

        repository = New GenericRepository()
    End Sub


    ''' <summary>
    ''' Tests scenario and usage for single add, update or delete
    ''' </summary>
    ''' <remarks></remarks>
    <TestMethod()>
    Public Sub AuditUsageSingleEntity()
        Const FirstName As String = "CreatFirst"
        'see a more straightfoward usage of the audit in the context test

        repository.Add(Of Reference.John.Domain.FormSimpleZero)(New Domain.FormSimpleZero With {.LastName = "CreateTest", .FirstName = FirstName, .EthnicityId = 1, .GenderId = 1, .LastChangeUser = "unit test", .RaceId = 1, .RegionId = 1})

        repository.UnitOfWork.SaveChanges()
        'this refresh will load autonumber columns on an insert.

        Console.Write("Saved one record")
        'check to see that the record was added
        Dim _list = (From c In repository.GetQuery(Of Domain.FormSimpleZero)() Where c.FirstName = FirstName).ToList
        Assert.AreNotEqual(0, _list.Count)

        'update the found record
        _list(0).LastName = "simple updatess"

        repository.UnitOfWork.SaveChanges()

        'remove the found record
        _list.ForEach(Sub(x)
                          repository.Delete(x)
                      End Sub)

        repository.UnitOfWork.SaveChanges()

        Console.Write("Removed one record")
        'check to see that the record was removed
        Dim _listempty = (From c In repository.GetQuery(Of Domain.FormSimpleZero)() Where c.FirstName = FirstName).ToList
        Assert.AreEqual(0, _listempty.Count)
    End Sub


    <ClassCleanup>
    Public Shared Sub TearDown()
        DbContextManager.CloseAllDbContexts()
    End Sub

End Class
