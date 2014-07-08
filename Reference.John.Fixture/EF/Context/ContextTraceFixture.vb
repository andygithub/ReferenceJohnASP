Imports System.Linq.Expressions
Imports System.Data.Entity
Imports Reference.John

<TestClass()> Public Class ContextTraceFixture

    'For logging from EF 6 context see - http://msdn.microsoft.com/en-us/data/dn469464.aspx 



    <TestMethod()>
    Public Sub ContextTraceFixture()
        Console.WriteLine("Test Starting EF Context:" & Now)
        DoAction(Sub() CreateForm())
        DoAction(Sub() CreateAddress())
        DoAction(Sub() FindOneContact())
        DoAction(Sub() FindByKey())
        DoAction(Sub() FindByKeyNoTracking())
        DoAction(Sub() GetContactsWithPaging())
        DoAction(Sub() FindContactWithInclude())
        Console.WriteLine("Test Ending EF Context:" & Now)
    End Sub


    Private Sub FindContactWithInclude()
        Using _context As New Model.Reference_JohnEntities
            _context.Configuration.LazyLoadingEnabled = False
            _context.Database.Log = Sub(val) Console.Write(val)
            Dim _list = (From c In _context.FormSimpleZeroes.Include(Function(x) x.GenderOptionList) Where c.FirstName = "Robert").ToList
            Console.Write("Found {0} forms with {1} value on included entity", _list.Count(), _list(0).GenderOptionList.Name)
        End Using
    End Sub

    Private Sub FindByKey()
        Using _context As New Model.Reference_JohnEntities
            _context.Configuration.LazyLoadingEnabled = False
            _context.Database.Log = Sub(val) Console.Write(val)
            Dim item = _context.FormSimpleZeroes.FirstOrDefault
            Assert.IsNotNull(item)
            Dim _localCache = _context.FormSimpleZeroes.Find(item.Id)
            Assert.IsNotNull(_localCache)
            Console.Write("Found record by its PK: {0}", _localCache IsNot Nothing)
        End Using
    End Sub

    Private Sub FindByKeyNoTracking()
        Using _context As New Model.Reference_JohnEntities
            _context.Configuration.LazyLoadingEnabled = False
            _context.Database.Log = Sub(val) Console.Write(val)
            Dim item = _context.FormSimpleZeroes.AsNoTracking.FirstOrDefault
            Assert.IsNotNull(item)
            Dim _localCache = _context.FormSimpleZeroes.Find(item.Id)
            Assert.IsNotNull(_localCache)
            Console.Write("Found record by its PK: {0}", _localCache IsNot Nothing)
        End Using
    End Sub

    Private Sub CreateForm()
        Const FirstName As String = "CreatFirst"
        Using _context As New Model.Reference_JohnEntities
            _context.Database.Log = Sub(val) Console.Write(val)

            _context.FormSimpleZeroes.Add(New Domain.FormSimpleZero With {.LastName = "CreateTest", .FirstName = FirstName, .EthnicityId = 1, .GenderId = 1, .LastChangeUser = "unit test", .RaceId = 1, .RegionId = 1})
            _context.SaveChanges()
            Console.Write("Saved one record")
            'check to see that the record was added
            Dim _list = (From c In _context.FormSimpleZeroes Where c.FirstName = FirstName)
            Assert.AreNotEqual(0, _list.Count)
            'remove the found record
            _context.FormSimpleZeroes.Remove(_list.First)
            _context.SaveChanges()
            Console.Write("Removed one record")
            'check to see that the record was added
            Dim _listempty = (From c In _context.FormSimpleZeroes Where c.FirstName = FirstName)
            Assert.AreEqual(0, _listempty.Count)
        End Using
    End Sub

    Private Sub CreateAddress()
        Const Street1 As String = "unique name"
        Using _context As New Model.Reference_JohnEntities
            _context.Database.Log = Sub(val) Console.Write(val)
            'get contact to use to relate new address to 
            Dim _contact = (From c In _context.FormSimpleZeroes).FirstOrDefault
            _contact.Addresses.Add(New Domain.Address With {.AddressTypeId = 2, .City = "Camp Hill", .LastChangeUser = "unit test", .State = "PA", .Zip = "17011", .AddressLine1 = Street1})
            _context.SaveChanges()
            Console.Write("Saved one address")
            'check to see that the record was added
            Dim _list = (From c In _context.Addresses Where c.AddressLine1 = Street1).ToList
            Assert.AreNotEqual(0, _list.Count)
            'remove the found record
            _list.ForEach(Sub(x)
                              _context.Addresses.Remove(x)
                          End Sub)
            _context.SaveChanges()
            Console.Write("Removed one address")
            'check to see that the record was added
            Dim _listempty = (From c In _context.Addresses Where c.AddressLine1 = Street1)
            Assert.AreEqual(0, _listempty.Count)
        End Using
    End Sub

    Private Sub FindOneContact()
        Using _context As New Model.Reference_JohnEntities
            _context.Configuration.LazyLoadingEnabled = False
            _context.Database.Log = Sub(val) Console.Write(val)
            Dim _list = (From c In _context.FormSimpleZeroes Where c.FirstName = "Robert").FirstOrDefault
            Assert.IsNotNull(_list)
            Console.Write("Found record: {0} {1}", _list.FirstName, _list.LastName)
        End Using
    End Sub

    Private Sub GetContactsWithPaging()
        Using _context As New Model.Reference_JohnEntities
            _context.Configuration.LazyLoadingEnabled = False
            _context.Database.Log = Sub(val) Console.Write(val)
            Dim _list = (From c In _context.FormSimpleZeroes).Take(17)
            Assert.IsNotNull(_list)
            Console.Write("Pulled page set of record: {0}", _list.Count)
        End Using

    End Sub

    Private Shared Sub DoAction(action As Expression(Of Action))
        Console.Write("Executing {0} ... ", action.Body.ToString())

        Dim act = action.Compile()
        act.Invoke()

        Console.WriteLine()
    End Sub

End Class
