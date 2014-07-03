

Public Class WorkFlowService
    Implements IWorkFlowService

    Public Sub ExecuteWorkFlow(item As Object) Implements IWorkFlowService.ExecuteWorkFlow
        Throw New NotImplementedException
    End Sub

    Public Function ExecuteWorkFlowItem(item As Object) As Object Implements IWorkFlowService.ExecuteWorkFlowItem
        Return Nothing
    End Function

End Class
