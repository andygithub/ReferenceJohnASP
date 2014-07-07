

Public Class ListRecord
    Inherits System.Web.UI.Page

    Private _repository As Reference.John.Repository.IRepository

    Private Sub ListRecord_Init(sender As Object, e As EventArgs) Handles Me.Init
        _repository = Container.ContainerFactory.GetConfiguredContainer.Resolve(Of Reference.John.Repository.IRepository)()
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Public Function GetForms() As IQueryable(Of Reference.John.Domain.SearchResult)
        Return _repository.GetQuery(Of Reference.John.Domain.SearchResult)()
    End Function

    Private Sub ListRecord_Unload(sender As Object, e As EventArgs) Handles Me.Unload
        'tear down repository
        _repository = Nothing
    End Sub

End Class