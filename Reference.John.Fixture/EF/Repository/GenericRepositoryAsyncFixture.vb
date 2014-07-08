Imports System.Collections.Generic
Imports System.Linq
Imports System.Data.Entity
Imports System.Linq.Expressions
Imports Reference.John
Imports Reference.John.Repository
Imports Reference.John.Repository.Infrastructure


''' <summary>
''' This class is used to test the repository implementation
''' </summary>
<TestClass()> Public Class GenericRepositoryAsyncFixture

    Private formRepository As IFormContactZeroRepository
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

        formRepository = New FormContactZeroRepository()
        repository = New GenericRepository()
    End Sub

    <TestMethod()>
    Public Sub GenericRepositoryAyncFixture()
        Console.WriteLine("Test Starting Repository Async:" & Now)
        DoAction(Sub() FindOneForm())
        DoAction(Sub() FindAddressByForm())
        DoAction(Sub() GetFormsWithPaging())
        Console.WriteLine("Test Ending Repository Async:" & Now)
    End Sub


    Private Async Sub FindAddressByForm()
        Dim c = formRepository.FindByName("Eric", "Lang")
        Assert.IsNotNull(c)
        Dim address As List(Of Domain.Address) = Await repository.FindAsync(Of Domain.Address)(Function(x) x.FormSimpleZeroId = c.Id)
        Assert.IsNotNull(address)
        Console.Write("Found {0} forms with {1} Address", 1, address.Count)
    End Sub


    Private Async Sub FindOneForm()
        Dim c = Await repository.FindOneAsync(Of Domain.FormSimpleZero)(Function(x) x.FirstName = "Eric" AndAlso x.LastName = "Lang")
        Assert.IsNotNull(c)
        Console.Write("Found record: {0} {1}", c.FirstName, c.LastName)
    End Sub

    Private Async Sub GetFormsWithPaging()
        Dim output = Await repository.GetAsync(Of Domain.FormSimpleZero, String)(Function(x) x.LastName, 0, 5, SortOrder.Ascending)
        Assert.IsNotNull(output)
        Console.Write("Pulled page set of record: {0}", output.Count)
    End Sub

    Private Shared Sub DoAction(action As Expression(Of Action))
        Console.Write("Executing {0} ... ", action.Body.ToString())

        Dim act = action.Compile()
        act.Invoke()

        Console.WriteLine()
    End Sub

    <ClassCleanup>
    Public Shared Sub TearDown()
        DbContextManager.CloseAllDbContexts()
    End Sub

End Class
