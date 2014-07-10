

Public Class WorkFlowService
    Implements IWorkFlowService

    Private _repository As Reference.John.Repository.IRepository
    Private _logger As Reference.John.infrastructure.Logging.ILogger

    Public Sub New(repository As Reference.John.Repository.IRepository, logger As Reference.John.Infrastructure.Logging.ILogger)
        If repository Is Nothing Then Throw New ArgumentNullException("repository")
        If logger Is Nothing Then Throw New ArgumentNullException("logger")
        _repository = repository
        _logger = logger
    End Sub

    Public Sub ExecuteWorkFlow(item As Object) Implements IWorkFlowService.ExecuteWorkFlow
        Throw New NotImplementedException
    End Sub

    Public Function ExecuteWorkFlowItem(item As Object) As Object Implements IWorkFlowService.ExecuteWorkFlowItem
        Return Nothing
    End Function

End Class
