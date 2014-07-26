Imports System.Collections.Generic
Imports System.Linq
Imports System.Data.Entity
Imports System.Linq.Expressions
'Imports EF.SimpleInitialSetup.Repository.Infrastructure.Data
Imports Reference.John
Imports Reference.John.Repository

''' <summary>
''' This test class is used to show how it it possible to run the same tests as in the <see cref="GenericRepositoryFixture"/> while using stubs for the repository.
''' </summary>
''' <remarks></remarks>
<TestClass()> Public Class FakeGenericRepositoryFixture

    'Private contactRepository As IContactRepository
    'Private repository As IRepository

    Sub New()
        'DbContextManager.InitStorage(New SimpleDbContextStorage())
        'DbContextManager.Init("DefaultDb", True)

        'contactRepository = New ContactRepository()
        'repository = New GenericRepository()
    End Sub

    <TestMethod()>
    Public Sub FakeGenericRepositoryFixture()
        Console.WriteLine("Test Starting Fakes:" & Now)
        DoAction(Sub() CreateContact())
        DoAction(Sub() CreateAddress())
        DoAction(Sub() FindOneContact())
        DoAction(Sub() FindByKey())
        DoAction(Sub() FindNewlyCreated())
        DoAction(Sub() FindAddressByContact())
        DoAction(Sub() FindAddressByContactInclude())
        DoAction(Sub() GetContactsWithPaging())
        Console.WriteLine("Test Ending Fakes:" & Now)
    End Sub

    Private Sub FindAddressByContactInclude()
        Const First As String = "Robert"
        'arrange
        Dim _rootList As New List(Of Domain.FormSimpleZero)
        _rootList.Add(New Domain.FormSimpleZero With {.LastChangeDate = Now, .Id = -101, .FirstName = First, .LastName = "lokok", .DateCreated = Now, .GenderId = 2,
                                               .Addresses = New List(Of Domain.Address) From {New Domain.Address With {.Id = -101, .FormSimpleZeroId = -212, .AddressLine1 = "mock", .LastChangeDate = Now}}
                                              })
        Dim _stub As New Reference.John.Repository.Fakes.StubIRepository
        _stub.GetQueryOf1StringInt32(Of Domain.FormSimpleZero)(Function() _rootList.AsQueryable)
        Dim _repository As IRepository = _stub

        'act
        Dim _list = (From c In _repository.GetQuery(Of Domain.FormSimpleZero).Include(Function(x) x.Addresses) Where c.FirstName = First).ToList
        'assert
        Assert.IsNotNull(_list)
        Assert.IsTrue(_list(0).Addresses.Count > 0)
        Console.Write("Found {0} records with {1} Address", _list.Count, _list(0).Addresses.Count)
    End Sub

    Private Sub FindAddressByContact()
        'arrange
        Const First As String = "Eric"
        Const Last As String = "Lang"
        Const Id As Integer = -111
        Dim _stub As New Reference.John.Repository.Fakes.StubIFormContactZeroRepository
        _stub.FindByNameStringString = Function() New Domain.FormSimpleZero With {.FirstName = First, .LastName = Last, .GenderId = -1, .Id = Id}
        _stub.FindOf1ExpressionOfFuncOfM0Boolean(Of Domain.Address)(Function() New List(Of Domain.Address) From {New Domain.Address With {.FormSimpleZeroId = Id, .Id = -212, .AddressLine1 = "mock", .LastChangeDate = Now}})
        Dim _repository As IFormContactZeroRepository = _stub
        'act
        Dim c = _repository.FindByName(First, Last)
        Assert.IsNotNull(c)
        Dim address As List(Of Domain.Address) = _repository.Find(Of Domain.Address)(Function(x) x.FormSimpleZeroId = c.Id).ToList()
        'assert
        Assert.IsNotNull(address)
        Console.Write("Found {0} records with {1} Address", 1, address.Count)
    End Sub

    Private Sub FindNewlyCreated()
        'arrange
        Const First As String = "Eric"
        Const Last As String = "Lang"
        Const Id As Integer = -111
        Dim _stub As New Reference.John.Repository.Fakes.StubIFormContactZeroRepository
        _stub.NewlyCreated = Function() New List(Of Domain.FormSimpleZero) From {New Domain.FormSimpleZero With {.Id = Id, .FirstName = First, .LastName = Last, .LastChangeDate = Now}}
        Dim _repository As IFormContactZeroRepository = _stub 
        'act
        Dim list = _repository.NewlyCreated()
        'assert
        Assert.IsNotNull(list)
        Console.Write("Found {0} new records", list.Count)
    End Sub

    Private Sub CreateAddress()
        'arrange
        Const Street1 As String = "unique name repos"
        Dim _contactStub As New List(Of Domain.FormSimpleZero) From {New Domain.FormSimpleZero With {.DateCreated = Now, .Id = -101, .FirstName = "first", .LastName = "lokok", .LastChangeDate = Now}}
        Dim _rootlist As New List(Of Domain.Address) From {New Domain.Address With {.FormSimpleZeroId = -101, .Id = -212, .AddressLine1 = Street1, .AddressLine2 = "Mock", .LastChangeDate = Now}}
        Dim _emptyList As New List(Of Domain.Address)
        Dim _stub As New Reference.John.Repository.Fakes.StubIFormContactZeroRepository
        Dim _stubUOW As New Reference.John.Repository.Infrastructure.Fakes.StubIUnitOfWork
        _stubUOW.SaveChanges = Sub() Console.Write("Save Change Stub ")
        _stub.UnitOfWorkGet = Function() _stubUOW
        _stub.GetQueryOf1StringInt32(Of Domain.FormSimpleZero)(Function() _contactStub.AsQueryable)
        _stub.GetQueryOf1StringInt32(Of Domain.Address)(Function() _rootlist.AsQueryable)
        _stub.DeleteOf1M0(Of Domain.Address)(Sub() _rootlist.Clear())
        Dim _repository As IFormContactZeroRepository = _stub  'MockRepository.GenerateMock(Of IRepository)()
        'act
        'get form to use to relate new address to 
        Dim _form = _repository.GetQuery(Of Domain.FormSimpleZero)()
        _form(0).Addresses.Add(New Domain.Address With {.AddressTypeId = 1, .City = "Camp Hill", .LastChangeDate = Now, .State = "PA", .Zip = "17011", .AddressLine1 = Street1})
        _repository.UnitOfWork.SaveChanges()
        Console.Write("Saved one address ")
        'check to see that the record was added
        Dim _list = (From c In _repository.GetQuery(Of Domain.Address)() Where c.AddressLine1 = Street1).ToList
        Assert.AreNotEqual(0, _list.Count)
        _list.First.City = "updated"
        _repository.UnitOfWork.SaveChanges()
        'remove the found record
        _repository.Delete(_list.First)
        _repository.UnitOfWork.SaveChanges()
        Console.Write("Removed one address")
        'check to see that the record was added
        Dim _listempty = (From c In _repository.GetQuery(Of Domain.Address)() Where c.AddressLine1 = Street1).ToList
        'assert
        Assert.AreEqual(0, _listempty.Count)
    End Sub

    Private Sub FindByKey()
        'arrange
        Const First As String = "Eric"
        Const Last As String = "Lang"
        Const Id As Integer = -111
        Dim _stub As New Reference.John.Repository.Fakes.StubIFormContactZeroRepository
        _stub.FindByNameStringString = Function() New Domain.FormSimpleZero With {.FirstName = First, .LastName = Last, .GenderId = -1, .Id = Id}
        _stub.GetByKeyOf1Object(Of Domain.FormSimpleZero)(Function() New Domain.FormSimpleZero With {.FirstName = First, .LastName = Last, .GenderId = -1, .Id = Id})
        Dim _repository As IFormContactZeroRepository = _stub
        'act
        Dim c = _repository.FindByName("Eric", "Lang")
        Assert.IsNotNull(c)
        Dim contact = _repository.GetByKey(Of Domain.FormSimpleZero)(c.Id)
        'assert
        Assert.IsNotNull(contact)
        Console.Write("Found record by its PK: {0}", contact IsNot Nothing)
    End Sub

    Private Sub CreateContact()
        'arrange
        Const FirstName As String = "CreatFirstRepos"
        Dim _rootList As New List(Of Domain.FormSimpleZero)
        _rootList.Add(New Domain.FormSimpleZero With {.DateCreated = Now, .Id = -101, .FirstName = FirstName, .LastName = "lokok", .LastChangeDate = Now,
                                               .Addresses = New List(Of Domain.Address) From {New Domain.Address With {.FormSimpleZeroId = -101, .Id = -212, .AddressLine1 = "mock", .LastChangeDate = Now}}
                                              })
        Dim _emptyList As New List(Of Domain.FormSimpleZero)
        Dim _stub As New Reference.John.Repository.Fakes.StubIFormContactZeroRepository
        Dim _stubUOW As New Reference.John.Repository.Infrastructure.Fakes.StubIUnitOfWork
        _stubUOW.SaveChanges = Sub() Console.Write("Save Change Stub ")
        _stub.UnitOfWorkGet = Function() _stubUOW
        _stub.GetQueryOf1StringInt32(Of Domain.FormSimpleZero)(Function() _rootList.AsQueryable)
        _stub.DeleteOf1M0(Of Domain.FormSimpleZero)(Sub() _rootList.Clear())
        Dim _repository As IFormContactZeroRepository = _stub
        'act
        _repository.Add(New Domain.FormSimpleZero With {.LastName = "CreateTest", .FirstName = FirstName, .DateCreated = Now, .LastChangeDate = Now})
        _repository.UnitOfWork.SaveChanges()
        Console.Write("Saved one records ")
        'check to see that the record was added
        Dim _list = (From c In _repository.GetQuery(Of Domain.FormSimpleZero)() Where c.FirstName = FirstName).ToList
        Assert.AreNotEqual(0, _list.Count)
        'update the record.
        _list.First.RaceId = 3
        _repository.Update(_list.First)
        _repository.UnitOfWork.SaveChanges()
        'remove the found record
        _repository.Delete(_list.First)
        _repository.UnitOfWork.SaveChanges()
        Console.Write("Removed one records")
        'check to see that the record was removed
        Dim _listempty = (From c In _repository.GetQuery(Of Domain.FormSimpleZero)() Where c.FirstName = FirstName).ToList
        'assert
        Assert.AreEqual(0, _listempty.Count)
    End Sub

    Private Sub FindOneContact()
        'arrange
        Const First As String = "Eric"
        Const Last As String = "Lang"
        Const id = -2300
        Dim _stub As New Reference.John.Repository.Fakes.StubIFormContactZeroRepository
        _stub.FindOneOf1ExpressionOfFuncOfM0Boolean(Of Domain.FormSimpleZero)(Function() New Domain.FormSimpleZero With {.FirstName = First, .LastName = Last, .GenderId = -1, .Id = id})
        Dim _repository As IFormContactZeroRepository = _stub
        'act
        Dim c = _repository.FindOne(Of Domain.FormSimpleZero)(Function(x) x.FirstName = First AndAlso x.LastName = Last)
        'assert
        Assert.IsNotNull(c)
        Console.Write("Found record: {0} {1}", c.FirstName, c.LastName)
    End Sub

    Private Sub GetContactsWithPaging()
        'arrange
        Dim _rootList As New List(Of Domain.FormSimpleZero)
        _rootList.Add(New Domain.FormSimpleZero With {.DateCreated = Now, .Id = -101, .FirstName = "lokok", .LastName = "Abel"})
        _rootList.Add(New Domain.FormSimpleZero With {.DateCreated = Now, .Id = -102, .FirstName = "lokok", .LastName = "Abercrombie"})
        _rootList.Add(New Domain.FormSimpleZero With {.DateCreated = Now, .Id = -103, .FirstName = "lokok", .LastName = "Adams"})
        _rootList.Add(New Domain.FormSimpleZero With {.DateCreated = Now, .Id = -104, .FirstName = "lokok", .LastName = "Adams"})
        _rootList.Add(New Domain.FormSimpleZero With {.DateCreated = Now, .Id = -105, .FirstName = "lokok", .LastName = "Agcaoili"})

        Dim _stub As New Reference.John.Repository.Fakes.StubIFormContactZeroRepository
        _stub.GetOf2ExpressionOfFuncOfM0M1Int32Int32SortOrder(Of Domain.FormSimpleZero, String)(Function() _rootList)
        Dim _repository As IFormContactZeroRepository = _stub
        'act
        Dim output = _repository.[Get](Of Domain.FormSimpleZero, String)(Function(x) x.LastName, 0, 5, SortOrder.Ascending).ToList
        'assert
        Assert.AreEqual(5, output.Count)
        Assert.AreEqual(output(0).LastName.Trim, "Abel")
        Assert.AreEqual(output(1).LastName.Trim, "Abercrombie")
        Assert.AreEqual(output(2).LastName.Trim, "Adams")
        Assert.AreEqual(output(3).LastName.Trim, "Adams")
        Assert.AreEqual(output(4).LastName.Trim, "Agcaoili")
    End Sub

    Private Shared Sub DoAction(action As Expression(Of Action))
        Console.Write("Executing {0} ... ", action.Body.ToString())

        Dim act = action.Compile()
        act.Invoke()

        Console.WriteLine()
    End Sub

End Class
