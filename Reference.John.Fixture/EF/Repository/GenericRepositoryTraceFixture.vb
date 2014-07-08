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
<TestClass()> Public Class GenericRepositoryTraceFixture

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
        'look at passing an overload for the logging delegate. 
        'for now set it on the context from the dbcontextbuilder.
        formRepository = New FormContactZeroRepository()
        repository = New GenericRepository()
    End Sub

    <TestMethod()>
    Public Sub GenericRepositoryTraceFixture()
        Console.WriteLine("Test Starting Repository:" & Now)
        DoAction(Sub() CreateForm())
        DoAction(Sub() CreateAddress())
        DoAction(Sub() FindOneForm())
        DoAction(Sub() FindByKey())
        DoAction(Sub() FindNewlyCreated())
        DoAction(Sub() FindAddressByForm())
        DoAction(Sub() FindAddressByFormInclude())
        DoAction(Sub() GetFormsWithPaging())
        Console.WriteLine("Test Ending Repository:" & Now)
    End Sub

    Private Sub FindAddressByFormInclude()
        Dim _list = (From c In repository.GetQuery(Of Domain.FormSimpleZero).Include(Function(x) x.Addresses) Where c.FirstName = "Robert").ToList
        Assert.IsNotNull(_list)
        Assert.IsTrue(_list(0).Addresses.Count > 0)
        Console.Write("Found {0} forms with {1} Address", _list.Count, _list(0).Addresses.Count)
    End Sub

    Private Sub FindAddressByForm()
        Dim c = formRepository.FindByName("Eric", "Lang")
        Assert.IsNotNull(c)
        Dim address As List(Of Domain.Address) = repository.Find(Of Domain.Address)(Function(x) x.FormSimpleZeroId = c.Id).ToList()
        Assert.IsNotNull(address)
        Console.Write("Found {0} forms with {1} Address", 1, address.Count)
    End Sub

    Private Sub FindNewlyCreated()
        Dim list = formRepository.NewlyCreated()

        Console.Write("Found {0} new form", list.Count)
    End Sub

    Private Sub CreateAddress()
        Const Street1 As String = "unique name repos"

        'get contact to use to relate new address to 
        Dim _contact = repository.GetQuery(Of Domain.FormSimpleZero)().FirstOrDefault
        _contact.Addresses.Add(New Domain.Address With {.AddressTypeId = 2, .City = "Camp Hill", .LastChangeUser = "unit test", .State = "PA", .Zip = "17011", .AddressLine1 = Street1})
        repository.UnitOfWork.SaveChanges()
        Console.Write("Saved one address ")
        'check to see that the record was added
        Dim _list = (From c In repository.GetQuery(Of Domain.Address)() Where c.AddressLine1 = Street1).ToList
        Assert.AreNotEqual(0, _list.Count)
        _list.First.City = "updated"
        formRepository.UnitOfWork.SaveChanges()
        'remove the found record
        _list.ForEach(Sub(x)
                          repository.Delete(x)
                      End Sub)
        repository.UnitOfWork.SaveChanges()
        Console.Write("Removed one address")
        'check to see that the record was added
        Dim _listempty = (From c In repository.GetQuery(Of Domain.Address)() Where c.AddressLine1 = Street1).ToList
        Assert.AreEqual(0, _listempty.Count)
    End Sub

    Private Sub FindByKey()
        Dim c = formRepository.FindByName("Eric", "Lang")
        Assert.IsNotNull(c)
        Dim contact = formRepository.GetByKey(Of Domain.FormSimpleZero)(c.Id)
        Assert.IsNotNull(contact)
        Console.Write("Found record by its PK: {0}", contact IsNot Nothing)
    End Sub

    Private Sub CreateForm()
        Const FirstName As String = "CreatFirstRepos"
        formRepository.Add(New Domain.FormSimpleZero With {.LastName = "CreateTest", .FirstName = FirstName, .EthnicityId = 1, .GenderId = 1, .LastChangeUser = "unit test", .RaceId = 1, .RegionId = 1})
        formRepository.UnitOfWork.SaveChanges()
        Console.Write("Saved one record ")
        'check to see that the record was added
        Dim _list = (From c In repository.GetQuery(Of Domain.FormSimpleZero)() Where c.FirstName = FirstName).ToList
        Assert.AreNotEqual(0, _list.Count)
        'update the record.
        _list.First.GenderId = 1
        formRepository.Update(_list.First)
        formRepository.UnitOfWork.SaveChanges()
        'remove the found record
        _list.ForEach(Sub(x)
                          formRepository.Delete(x)
                      End Sub)
        formRepository.UnitOfWork.SaveChanges()
        Console.Write("Removed one record")
        'check to see that the record was removed
        Dim _listempty = (From c In repository.GetQuery(Of Domain.FormSimpleZero)() Where c.FirstName = FirstName).ToList
        Assert.AreEqual(0, _listempty.Count)
    End Sub

    Private Sub FindOneForm()
        Dim c = repository.FindOne(Of Domain.FormSimpleZero)(Function(x) x.FirstName = "Eric" AndAlso x.LastName = "Lang")
        Assert.IsNotNull(c)
        Console.Write("Found record: {0} {1}", c.FirstName, c.LastName)
    End Sub

    Private Sub GetFormsWithPaging()
        Dim output = repository.[Get](Of Domain.FormSimpleZero, String)(Function(x) x.LastName, 0, 5, SortOrder.Ascending).ToList
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
