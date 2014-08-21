Imports Reference.John.Repository
Imports Reference.John.Infrastructure.Logging
Imports Reference.John.Domain

Public Class AlertSesrvice
    Implements IAlertService

    Private _lazyrepository As Lazy(Of IRepository)
    Private _lazylogger As Lazy(Of ILogger)

    'have these properties to hide the lazy usage from within controller 
    Private ReadOnly Property _repository As IRepository
        Get
            Return _lazyrepository.Value
        End Get
    End Property

    Private ReadOnly Property _logger As ILogger
        Get
            Return _lazylogger.Value
        End Get
    End Property

    Public Sub New(repository As Lazy(Of IRepository), logger As Lazy(Of ILogger))
        If repository Is Nothing Then Throw New ArgumentNullException("repository")
        If logger Is Nothing Then Throw New ArgumentNullException("logger")
        _lazyrepository = repository
        _lazylogger = logger
    End Sub

    Public Function ProcessAlerts(actionType As Reference.John.Domain.ActionTypeOptionList, item As IAlertEntity) As IEnumerable(Of Reference.John.Domain.FormAlertTemplate_xref) Implements IAlertService.ProcessAlerts
        'depending on the action type map to different templates and save different records to db
        'also may publish alerts as well
        'expect that any necessary id's would be passed into service
        'other option is that list of alerts could be returned and attached to the relevant entity before save and let the ORM handle the relationship.
        If actionType.ActionTypeId = 1 Then

        End If
        Return New List(Of Reference.John.Domain.FormAlertTemplate_xref) From {New Reference.John.Domain.FormAlertTemplate_xref With {.AlertTemplateId = 3, .StartDate = Domain.Providers.DefaultTimeProvider.Current.UtcNow,
                                                                                                                                      .EndDate = Domain.Providers.DefaultTimeProvider.Current.UtcNow.AddDays(20),
                                                                                                                                      .EntityId = item.EntityId, .IsActive = 1, .LastChangeUser = item.LastChangeUser}}
    End Function

    Public Sub PublishAlerts() Implements IAlertService.PublishAlerts
        Throw New NotImplementedException
    End Sub

End Class