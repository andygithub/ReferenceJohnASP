Imports Reference.John.Domain

Public Interface IAlertService
    Function ProcessAlerts(actionType As Reference.John.Domain.ActionTypeOptionList, item As IAlertEntity) As IEnumerable(Of Reference.John.Domain.FormAlertTemplate_xref)
    Sub PublishAlerts()
End Interface



