Imports Reference.John.Repository
Imports Reference.John.WebMVC.Extensions
Imports System.Linq.Dynamic
Imports System.Data.Entity
Imports Reference.John.WebMVC.Models
Imports AutoMapper.QueryableExtensions
Imports Reference.John.Infrastructure.Logging


Namespace Controllers
    Public Class FormController
        Inherits System.Web.Mvc.Controller

        Private _repository As IRepository
        Private _logger As ILogger

        Public Sub New(repository As IRepository, logger As ILogger)
            If repository Is Nothing Then Throw New ArgumentNullException("repository")
            If logger Is Nothing Then Throw New ArgumentNullException("logger")
            _repository = repository
            _logger = logger
        End Sub


        Function Index() As ActionResult
            Return View()
        End Function


        Function Create() As ActionResult
            Return View(InitializeViewModel())
        End Function

        <HttpPost()>
        <ValidateAntiForgeryToken()>
        Function Create(ByVal item As Models.ViewFormSimpleZero) As ActionResult
            If ModelState.IsValid Then
                'map view model into domain model 
                Dim _item As New Reference.John.Domain.FormSimpleZero
                With _item
                    .FirstName = item.FirstName
                    .LastName = item.LastName
                    .GenderId = item.GenderId
                    .RaceId = item.RaceId
                    .RegionId = item.RegionId
                    .EthnicityId = item.EthnicityId
                    .LastChangeUser = "ui"
                End With
                _repository.Add(_item)
                Try
                    _repository.UnitOfWork.SaveChanges()
                    Return RedirectToAction("Index")
                Catch ex As Entity.Validation.DbEntityValidationException
                    For Each eve In ex.EntityValidationErrors
                        Debug.WriteLine("Entity of type {0} in state {1} has the following validation errors:", eve.Entry.Entity.GetType().Name, eve.Entry.State)
                        For Each ve In eve.ValidationErrors
                            Debug.WriteLine("- Property: {0}, Error: {1}", ve.PropertyName, ve.ErrorMessage)
                        Next
                    Next
                    Throw
                End Try
            End If
            UpdateViewModel(item)
            Return View(item)
        End Function

        Function Edit(Optional ByVal ClientToken As Guid = Nothing) As ActionResult
            Dim item As Models.ViewFormSimpleZero = _repository.GetQuery(Of Reference.John.Domain.FormSimpleZero)(Function(x) x.ClientToken = ClientToken) _
                                    .Project.To(Of Models.ViewFormSimpleZero)().FirstOrDefault()
            If IsNothing(item) Then Return HttpNotFound()
            UpdateViewModel(item)
            Return View(item)
        End Function

        <HttpPost()>
        <ValidateAntiForgeryToken()>
        Function Edit(ByVal item As Models.ViewFormSimpleZero) As ActionResult
            If ModelState.IsValid Then
                'load with not tracking for attachment purposees and to set the original value of the rowversion to the value from the client.
                'the asnotracking on the lookup and the attach rather than the update are for concurrency purposes and so that the rowversion from the 
                'browser client will be pushed into the entity as the orginal version rather than whatever value is loaded from the data store
                'http://stackoverflow.com/questions/21994266/concurrency-check-with-web-api-not-checking
                'http://stackoverflow.com/questions/10353589/how-to-handle-ef-4-3-1-setting-modified-a-rowversion-row
                Dim _item As Reference.John.Domain.FormSimpleZero = _repository.GetQuery(Of Reference.John.Domain.FormSimpleZero)(Function(x) x.ClientToken = item.ClientToken).AsNoTracking.FirstOrDefault '_repository.FindOne(Of Reference.John.Domain.FormSimpleZero)(Function(x) x.ClientToken = item.ClientToken)
                If IsNothing(item) Then Return HttpNotFound()
                'map posted properties into domain model
                Dim _temp = New Byte() {&H20, &H20, &H20, &H20, &H20, &H20, &H20, &H20}

                With _item
                    .FirstName = item.FirstName
                    .LastName = item.LastName
                    .GenderId = item.GenderId
                    .RaceId = item.RaceId
                    .RegionId = item.RegionId
                    .EthnicityId = item.EthnicityId
                    .RowVersion = item.RowVersion
                    .LastChangeUser = "ui" & Now.Minute
                End With
                _repository.Attach(_item)
                Try
                    _repository.UnitOfWork.SaveChanges()
                    Return RedirectToAction("Index")
                Catch ex As Entity.Infrastructure.DbUpdateConcurrencyException
                    'probably log this execption since it should be a fairly rare occurence for support purposes.
                    ModelState.AddModelError("", Reference.John.Resources.Resources.ValidationMessages.ConcurrencyException)
                End Try
            End If
            UpdateViewModel(item)
            Return View(item)
        End Function

        Function Delete(Optional ByVal ClientToken As Guid = Nothing) As ActionResult
            Dim item As Reference.John.Domain.FormSimpleZero = _repository.FindOne(Of Reference.John.Domain.FormSimpleZero)(Function(x) x.ClientToken = ClientToken)
            If IsNothing(item) Then
                Return HttpNotFound()
            End If
            Return View(item)
        End Function

        <HttpPost()> _
        <ActionName("Delete")>
        <ValidateAntiForgeryToken()>
        Function DeleteConfirmed(ByVal ClientToken As Guid) As RedirectToRouteResult
            Dim item As Reference.John.Domain.FormSimpleZero = _repository.FindOne(Of Reference.John.Domain.FormSimpleZero)(Function(x) x.ClientToken = ClientToken)
            _repository.Delete(item)
            _repository.UnitOfWork.SaveChanges()
            Return RedirectToAction("Index")
        End Function


        <ActionName(Reference.John.Resources.MVCConstants.GridAction)> _
        Public Function AjaxGrid(page As String, sort As String, sortdir As String) As ActionResult
            Dim pageNo As Integer = 0
            If Not String.IsNullOrEmpty(page) Then
                pageNo = Integer.Parse(page) - 1
            End If
            'set sorting defaults
            If String.IsNullOrWhiteSpace(sort) Then
                sort = Reference.John.Resources.MVCConstants.DefaultSortColumn
                sortdir = Reference.John.Resources.MVCConstants.DefaultSortDirection
            End If

            Dim model = New GridModel(Of Models.SearchResult)
            model.Data = (From n In _repository.GetQuery(Of Reference.John.Domain.SearchResult).OrderBy(sort & " " & sortdir).Skip(pageNo * Reference.John.Resources.MVCConstants.PageSize).Take(Reference.John.Resources.MVCConstants.PageSize)).AsNoTracking.Project.To(Of Models.SearchResult)().ToList
            model.TotalRows = _repository.Count(Of Reference.John.Domain.SearchResult)()
            model.CurrentPage = pageNo
            Return PartialView(model)
        End Function

        Public Function Download() As FileResult
            Return File(Encoding.UTF8.GetBytes(_repository.GetQuery(Of Reference.John.Domain.SearchResult).AsNoTracking.Project.To(Of Models.SearchResult)().ToDelimitedText), Reference.John.Resources.MVCConstants.CSVFileType, "forms.csv")
        End Function

        Private Function InitializeViewModel() As Models.ViewFormSimpleZero
            Return UpdateViewModel(New Models.ViewFormSimpleZero)
        End Function

        Private Function UpdateViewModel(item As Models.ViewFormSimpleZero) As Models.ViewFormSimpleZero
            With item
                'TODO look at caching the select list as well as the EF item. also look at apply autofilter to reduce fields 
                .GenderList = _repository.GetAll(Of Reference.John.Domain.GenderOptionList).ToSelectList(Function(x) x.GenderId, Function(x) x.Name)
                .RaceList = _repository.GetAll(Of Reference.John.Domain.RaceOptionList).ToSelectList(Function(x) x.RaceId, Function(x) x.Name)
                .RegionList = _repository.GetAll(Of Reference.John.Domain.RegionOptionList).ToSelectList(Function(x) x.RegionId, Function(x) x.Name)
                .EthnicityList = _repository.GetAll(Of Reference.John.Domain.EthnicityOptionList).ToSelectList(Function(x) x.EthnicityId, Function(x) x.Name)
            End With
            Return item
        End Function


    End Class

End Namespace

'code for more robust concurrency UI - http://msdn.microsoft.com/en-us/library/ms182776.aspx

'Try
'	If ModelState.IsValid Then
'		db.Entry(department).State = EntityState.Modified
'		Await db.SaveChangesAsync()
'		Return RedirectToAction("Index")
'	End If
'Catch ex As DbUpdateConcurrencyException
'Dim entry = ex.Entries.[Single]()
'Dim clientValues = DirectCast(entry.Entity, Department)
'Dim databaseEntry = entry.GetDatabaseValues()
'	If databaseEntry Is Nothing Then
'		ModelState.AddModelError(String.Empty, "Unable to save changes. The department was deleted by another user.")
'	Else
'Dim databaseValues = DirectCast(databaseEntry.ToObject(), Department)

'		If databaseValues.Name <> clientValues.Name Then
'			ModelState.AddModelError("Name", "Current value: " + databaseValues.Name)
'		End If
'		If databaseValues.Budget <> clientValues.Budget Then
'			ModelState.AddModelError("Budget", "Current value: " + [String].Format("{0:c}", databaseValues.Budget))
'		End If
'		If databaseValues.StartDate <> clientValues.StartDate Then
'			ModelState.AddModelError("StartDate", "Current value: " + [String].Format("{0:d}", databaseValues.StartDate))
'		End If
'		If databaseValues.InstructorID <> clientValues.InstructorID Then
'			ModelState.AddModelError("InstructorID", "Current value: " + db.Instructors.Find(databaseValues.InstructorID).FullName)
'		End If
'		ModelState.AddModelError(String.Empty, "The record you attempted to edit " + "was modified by another user after you got the original value. The " + "edit operation was canceled and the current values in the database " + "have been displayed. If you still want to edit this record, click " + "the Save button again. Otherwise click the Back to List hyperlink.")
'		department.RowVersion = databaseValues.RowVersion
'	End If
'End Try
