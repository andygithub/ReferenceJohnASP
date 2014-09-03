Imports Reference.John.Domain

Public Interface IEventService
    Function ProcessEvents(actionType As Reference.John.Domain.ActionTypeOptionList, item As IEventEntity) As IEnumerable(Of Reference.John.Domain.FormAlertTemplate_xref)
    Sub PublishEvents()
End Interface



