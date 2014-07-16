Imports System.Data.Entity
Imports AutoMapper.QueryableExtensions

Public Class ListRecord
    Inherits System.Web.UI.Page

    Private _lazyrepository As Lazy(Of Reference.John.Repository.IRepository)
    Private _lazylogger As Lazy(Of Reference.John.Infrastructure.Logging.ILogger)
    Private _lazyservice As Lazy(Of Reference.John.Services.IWorkFlowService)

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

    Private Sub ListRecord_Init(sender As Object, e As EventArgs) Handles Me.Init
        _lazyrepository = Reference.John.Infrastructure.Container.ContainerFactory.GetConfiguredContainer.Resolve(Of Lazy(Of Reference.John.Repository.IRepository))()
        _lazylogger = Reference.John.Infrastructure.Container.ContainerFactory.GetConfiguredContainer.Resolve(Of Lazy(Of Reference.John.Infrastructure.Logging.ILogger))()
        _logger.Info(Reference.John.Resources.Resources.LogMessages.PageInitEnded)
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Private Sub ListRecord_Unload(sender As Object, e As EventArgs) Handles Me.Unload
        _logger.Info(Reference.John.Resources.Resources.LogMessages.PageUnloadStarted)
        'tear down repository
        _lazyrepository = Nothing
        _lazylogger = Nothing
    End Sub

    Public Function GetForms() As IQueryable(Of Models.SearchResult)
        Return _repository.GetQuery(Of Reference.John.Domain.SearchResult)().AsNoTracking.Project.To(Of Models.SearchResult)()
    End Function

End Class