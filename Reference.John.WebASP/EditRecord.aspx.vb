Public Class EditRecord
    Inherits System.Web.UI.Page

    Private _repository As Reference.John.Repository.IRepository
    Private _logger As Reference.John.Infrastructure.Logging.ILogger

    Private Sub ListRecord_Init(sender As Object, e As EventArgs) Handles Me.Init
        _repository = Reference.John.Infrastructure.Container.ContainerFactory.GetConfiguredContainer.Resolve(Of Reference.John.Repository.IRepository)()
        _logger = Reference.John.Infrastructure.Container.ContainerFactory.GetConfiguredContainer.Resolve(Of Reference.John.Infrastructure.Logging.ILogger)()
        _logger.Info(Reference.John.Resources.Resources.LogMessages.PageInitEnded)
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Private Sub ListRecord_Unload(sender As Object, e As EventArgs) Handles Me.Unload
        _logger.Info(Reference.John.Resources.Resources.LogMessages.PageUnloadStarted)
        'tear down repository
        _repository = Nothing
        _logger = Nothing
    End Sub

    Public Function SelectFormItem(<ModelBinding.QueryString> ClientTokenFormZero As Guid) As Reference.John.Domain.FormSimpleZero
        'if the parameter doesn't have a nullable type then the framework will throw an exception,  if a custom message is desired make the type nullable and do a check like this.
        If ClientTokenFormZero = Nothing Then Throw New ArgumentNullException("ClientTokenFormZero")
        Return _repository.FindOne(Of Reference.John.Domain.FormSimpleZero)(Function(x) x.ClientToken = ClientTokenFormZero)
    End Function

    Public Sub UpdateFormItem(ClientToken As Guid, RowVersion As Byte())
        Dim i = Request.Form
        Dim _item As Reference.John.Domain.FormSimpleZero = Nothing
        _item = _repository.FindOne(Of Reference.John.Domain.FormSimpleZero)(Function(x) x.ClientToken = ClientToken)
        'this is done in case invalid items or items that don't have security access are passed.
        If _item Is Nothing Then
            ModelState.AddModelError("", String.Format(Reference.John.Resources.Resources.ValidationMessages.ItemNotFound, ClientToken))
            Exit Sub
        End If
        'this should be manually mapped rather than using this shorthand.
        TryUpdateModel(_item)
        If ModelState.IsValid Then
            _item.LastChangeUser = "web user" & Now.Minute
            Try
                _repository.UnitOfWork.SaveChanges()
                'redirect here or show success message
            Catch ex As Entity.Infrastructure.DbUpdateConcurrencyException
                'probably log this execption since it should be a fairly rare occurence for support purposes.
                ModelState.AddModelError("", Reference.John.Resources.Resources.ValidationMessages.ConcurrencyException)
                Exit Sub
            End Try
        End If
    End Sub

    Public Sub cancelButton_Click(sender As Object, e As EventArgs)
        Response.Redirect("~/", True)
    End Sub

    Public Function GetRaceOptionList() As IEnumerable(Of Reference.John.Domain.RaceOptionList)
        Return _repository.GetAll(Of Reference.John.Domain.RaceOptionList)()
    End Function

    Public Function GetGenderOptionList() As IEnumerable(Of Reference.John.Domain.GenderOptionList)
        Return _repository.GetAll(Of Reference.John.Domain.GenderOptionList)()
    End Function

    Public Function GetRegionOptionList() As IEnumerable(Of Reference.John.Domain.RegionOptionList)
        Return _repository.GetAll(Of Reference.John.Domain.RegionOptionList)()
    End Function

    Public Function GetEthnicityOptionList() As IEnumerable(Of Reference.John.Domain.EthnicityOptionList)
        Return _repository.GetAll(Of Reference.John.Domain.EthnicityOptionList)()
    End Function

End Class

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