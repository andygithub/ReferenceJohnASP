Public Class CreateRecord
    Inherits System.Web.UI.Page

    Private _lazyrepository As Lazy(Of Reference.John.Repository.IRepository)
    Private _lazylogger As Lazy(Of Reference.John.Infrastructure.Logging.ILogger)
    Private _lazyservice As Lazy(Of Reference.John.Services.IWorkFlowService)
    Private _lazyAlertService As Lazy(Of Reference.John.Services.IEventService)

    'have these properties to hide the lazy usage from within class 
    Private ReadOnly Property _repository As Reference.John.Repository.IRepository
        Get
            Return _lazyrepository.Value
        End Get
    End Property

    Private ReadOnly Property _logger As Reference.John.Infrastructure.Logging.ILogger
        Get
            Return _lazylogger.Value
        End Get
    End Property

    Private ReadOnly Property _service As Reference.John.Services.IWorkFlowService
        Get
            Return _lazyservice.Value
        End Get
    End Property

    Private ReadOnly Property _alertService As Reference.John.Services.IEventService
        Get
            Return _lazyAlertService.Value
        End Get
    End Property

    Private Sub ListRecord_Init(sender As Object, e As EventArgs) Handles Me.Init
        _lazyrepository = Reference.John.Infrastructure.Container.ContainerFactory.GetConfiguredContainer.Resolve(Of Lazy(Of Reference.John.Repository.IRepository))()
        _lazylogger = Reference.John.Infrastructure.Container.ContainerFactory.GetConfiguredContainer.Resolve(Of Lazy(Of Reference.John.Infrastructure.Logging.ILogger))()
        _lazyservice = Reference.John.Infrastructure.Container.ContainerFactory.GetConfiguredContainer.Resolve(Of Lazy(Of Reference.John.Services.IWorkFlowService))()
        _lazyAlertService = Reference.John.Infrastructure.Container.ContainerFactory.GetConfiguredContainer.Resolve(Of Lazy(Of Reference.John.Services.IEventService))()
        _logger.Info(Reference.John.Resources.Resources.LogMessages.PageInitEnded)
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Private Sub ListRecord_Unload(sender As Object, e As EventArgs) Handles Me.Unload
        _logger.Info(Reference.John.Resources.Resources.LogMessages.PageUnloadStarted)
        'tear down repository
        _lazyrepository = Nothing
        _lazylogger = Nothing
        _lazyservice = Nothing
        _lazyAlertService = Nothing
    End Sub

    Public Sub InsertFormItem(item As Reference.John.Domain.FormSimpleZero)

        If ModelState.IsValid Then
            item.LastChangeUser = "as"

            'would expect the action type option list to be load from the db, could be an enumeration instead
            _alertService.ProcessEvents(New Reference.John.Domain.ActionTypeOptionList With {.ActionTypeId = 1}, New Reference.John.Domain.EventEntity With {.EntityId = -500, .LastChangeUser = item.LastChangeUser}).ToList.ForEach(Sub(x)
                                                                                                                                                                                                                                          item.FormAlertTemplate_xref.Add(x)
                                                                                                                                                                                                                                      End Sub)
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
        Return _repository.GetAll(Of Reference.John.Domain.GenderOptionList)()
    End Function

    Public Function GetRegionOptionList() As IEnumerable(Of Reference.John.Domain.RegionOptionList)
        Return _repository.GetAll(Of Reference.John.Domain.RegionOptionList)()
    End Function

    Public Function GetEthnicityOptionList() As IEnumerable(Of Reference.John.Domain.EthnicityOptionList)
        Return _repository.GetAll(Of Reference.John.Domain.EthnicityOptionList)()
    End Function

End Class