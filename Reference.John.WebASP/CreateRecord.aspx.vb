Public Class CreateRecord
    Inherits System.Web.UI.Page

    Private _repository As Reference.John.Repository.IRepository
    Private _logger As Reference.John.Core.Logging.ILogger
    Private _service As Reference.John.Services.IWorkFlowService

    Private Sub ListRecord_Init(sender As Object, e As EventArgs) Handles Me.Init
        _repository = Container.ContainerFactory.GetConfiguredContainer.Resolve(Of Reference.John.Repository.IRepository)()
        _logger = Container.ContainerFactory.GetConfiguredContainer.Resolve(Of Reference.John.Core.Logging.ILogger)()
        _service = Container.ContainerFactory.GetConfiguredContainer.Resolve(Of Reference.John.Services.IWorkFlowService)()
        _logger.Info(Reference.John.Resources.Resources.LogMessages.PageInitEnded)
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Private Sub ListRecord_Unload(sender As Object, e As EventArgs) Handles Me.Unload
        _logger.Info(Reference.John.Resources.Resources.LogMessages.PageUnloadStarted)
        'tear down repository
        _repository = Nothing
        _logger = Nothing
        _service = Nothing
    End Sub

    Public Sub InsertFormItem(item As Reference.John.Domain.FormSimpleZero)

        If ModelState.IsValid Then
            item.DateCreated = Now
            item.LastChangeDate = Now
            item.LastChangeUser = "as"
            'map form view properties into domain object

            _repository.Add(item)
            Try
                _repository.UnitOfWork.SaveChanges()
                'redirect here or show success message
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
    End Sub

    Public Sub cancelButton_Click(sender As Object, e As EventArgs)
        Response.Redirect("~/", True)
    End Sub

    Public Function GetRaceOptionList() As IEnumerable(Of Reference.John.Domain.RaceOptionList)
        Return _repository.GetAll(Of Reference.John.Domain.RaceOptionList)()
    End Function

    Public Function GetGenderOptionList() As IEnumerable(Of Reference.John.Domain.GenderOptionList)
        Return _repository.GetAllAsync(Of Reference.John.Domain.GenderOptionList)()
    End Function

    Public Function GetRegionOptionList() As IEnumerable(Of Reference.John.Domain.RegionOptionList)
        Return _repository.GetAll(Of Reference.John.Domain.RegionOptionList)()
    End Function

    Public Function GetEthnicityOptionList() As IEnumerable(Of Reference.John.Domain.EthnicityOptionList)
        Return _repository.GetAll(Of Reference.John.Domain.EthnicityOptionList)()
    End Function

End Class