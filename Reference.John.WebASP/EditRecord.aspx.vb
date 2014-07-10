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
        If ClientTokenFormZero = Nothing Then Throw New ArgumentNullException("FormZeroId")
        Return _repository.FindOne(Of Reference.John.Domain.FormSimpleZero)(Function(x) x.ClientToken = ClientTokenFormZero)
    End Function

    Public Sub UpdateFormItem(ClientToken As Guid)

        Dim _item As Reference.John.Domain.FormSimpleZero = Nothing
        _item = _repository.FindOne(Of Reference.John.Domain.FormSimpleZero)(Function(x) x.ClientToken = ClientToken)
        If _item Is Nothing Then
            ModelState.AddModelError("", String.Format(Reference.John.Resources.Resources.ValidationMessages.ItemNotFound, ClientToken))
            Exit Sub
        End If

        TryUpdateModel(_item)
        If ModelState.IsValid Then
            _item.LastChangeUser = "web user" & Now.Minute
            _repository.UnitOfWork.SaveChanges()
            'redirect here or show success message

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