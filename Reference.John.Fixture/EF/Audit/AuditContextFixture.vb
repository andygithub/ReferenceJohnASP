Imports System.Linq.Expressions
Imports System.Data.Entity
Imports EntityFramework.Audit
Imports EntityFramework.Extensions

<TestClass()> Public Class AuditContextFixture

    ''' <summary>
    ''' Tests scenario and usage for single add, update or delete
    ''' </summary>
    ''' <remarks></remarks>
    <TestMethod()>
    Public Sub AuditUsageSingleEntity()
        Const FirstName As String = "CreatFirst"
        Dim _auditConfiguration = AuditConfiguration.Default

        _auditConfiguration.IncludeRelationships = True
        _auditConfiguration.LoadRelationships = False
        _auditConfiguration.DefaultAuditable = True

        Using _context As New Model.Reference_JohnEntities
            Dim audit = _context.BeginAudit()

            _context.FormSimpleZeroes.Add(New Domain.FormSimpleZero With {.LastName = "CreateTest", .FirstName = FirstName, .EthnicityId = 1, .GenderId = 1, .LastChangeUser = "unit test", .RaceId = 1, .RegionId = 1})
            'the create log snapshot needs to take place before save changes cleans up the object context state manager
            'show the audit information for the task
            Dim log = audit.CreateLog()
            Assert.IsNotNull(log)

            Dim _xml = log.ToXml()
            Assert.IsNotNull(_xml)

            For Each item In log.Entities.SelectMany(Function(e) e.Properties)
                Assert.AreNotEqual(item.Current, "{error}")
                Assert.AreNotEqual(item.Original, "{error}")
            Next

            _context.SaveChanges()
            'this refresh will load autonumber columns on an insert.
            log.Refresh()

            Dim afterXml = log.ToXml()
            Assert.IsNotNull(afterXml)

            For Each item In log.Entities.SelectMany(Function(e) e.Properties)
                Assert.AreNotEqual(item.Current, "{error}")
                Assert.AreNotEqual(item.Original, "{error}")
            Next

            Console.Write("Saved one record")
            'check to see that the record was added
            Dim _list = (From c In _context.FormSimpleZeroes Where c.FirstName = FirstName).ToList
            Assert.AreNotEqual(0, _list.Count)

            'update the found record
            _list(0).LastName = "simple updatess"
            Dim updateLog = audit.CreateLog()
            Assert.IsNotNull(updateLog)

            _context.SaveChanges()

            Dim _updatexml = updateLog.ToXml()
            Assert.IsNotNull(_updatexml)

            'remove the found record
            _context.FormSimpleZeroes.Remove(_list.First)

            Dim deleteLog = audit.CreateLog()
            Assert.IsNotNull(deleteLog)

            _context.SaveChanges()

            Dim deleteXml = deleteLog.ToXml()
            Assert.IsNotNull(deleteXml)

            Console.Write("Removed one record")
            'check to see that the record was removed
            Dim _listempty = (From c In _context.FormSimpleZeroes Where c.FirstName = FirstName)
            Assert.AreEqual(0, _listempty.Count)
        End Using

    End Sub

    ''' <summary>
    ''' Tests scenario and usage for multiple entity add, update or delete
    ''' </summary>
    ''' <remarks></remarks>
    <TestMethod()>
    Public Sub AuditUsageMultipleEntities()
        Const FirstName As String = "CreatFirst"
        Const Street1 As String = "unique name"
        Dim _auditConfiguration = AuditConfiguration.Default

        _auditConfiguration.IncludeRelationships = True
        _auditConfiguration.LoadRelationships = True
        _auditConfiguration.DefaultAuditable = True

        Using _context As New Model.Reference_JohnEntities
            Dim audit = _context.BeginAudit()
            _context.FormSimpleZeroes.Add(New Domain.FormSimpleZero With {.LastName = "CreateTest", .FirstName = FirstName, .EthnicityId = 1, .GenderId = 1, .LastChangeUser = "unit test", .RaceId = 1, .RegionId = 1})
            _context.FormSimpleZeroes.Add(New Domain.FormSimpleZero With {.LastName = "CreateTest", .FirstName = FirstName, .EthnicityId = 1, .GenderId = 1, .LastChangeUser = "unit test", .RaceId = 1, .RegionId = 1})

            Dim log = audit.CreateLog()
            Assert.IsNotNull(log)

            Dim _xml = log.ToXml()
            Assert.IsNotNull(_xml)

            _context.SaveChanges()
            'this refresh will load autonumber columns on an insert.
            log.Refresh()

            Dim afterXml = log.ToXml()
            Assert.IsNotNull(afterXml)
            'create addresses , perform updates
            Dim _contact = (From c In _context.FormSimpleZeroes Where c.FirstName = FirstName).FirstOrDefault
            _contact.Addresses.Add(New Domain.Address With {.AddressTypeId = 2, .City = "Camp Hill", .LastChangeUser = "unit test", .State = "PA", .Zip = "17011", .AddressLine1 = Street1})
            _contact.LastName = "adfadfdascvzx"

            Dim updateLog = audit.CreateLog()
            Assert.IsNotNull(updateLog)

            _context.SaveChanges()

            Dim _updatexml = updateLog.ToXml()
            Assert.IsNotNull(_updatexml)

            'cleanup created items - don't have cascade enabled so need to manually cleanup the child relationships
            _context.Addresses.RemoveRange(From c In _context.Addresses Where c.AddressLine1 = Street1)
            _context.FormSimpleZeroes.RemoveRange(From c In _context.FormSimpleZeroes Where c.FirstName = FirstName)

            Dim deleteLog = audit.CreateLog()
            Assert.IsNotNull(deleteLog)

            _context.SaveChanges()

            Dim deleteXml = deleteLog.ToXml()
            Assert.IsNotNull(deleteXml)

            'check to see that the record was removed
            Dim _listempty = (From c In _context.FormSimpleZeroes Where c.FirstName = FirstName)
            Assert.AreEqual(0, _listempty.Count)
        End Using
    End Sub

End Class
