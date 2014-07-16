Imports System.Data.Entity
Imports AutoMapper.QueryableExtensions

Public Class ListRecord
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

    Public Function GetForms() As IQueryable(Of Models.SearchResult)
        Return _repository.GetQuery(Of Reference.John.Domain.SearchResult)().AsNoTracking.Project.To(Of Models.SearchResult)()
    End Function

End Class