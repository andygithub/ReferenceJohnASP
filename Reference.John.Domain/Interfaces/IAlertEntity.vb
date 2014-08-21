''' <summary>
''' Interface for an entity that can be used to generate alerts.
''' </summary>
''' <remarks>It's expected to be used by any entity that needs to be passed into the alert service.</remarks>
Public Interface IAlertEntity
    Property Id As Integer
    Property ClientToken As Guid
    Property LastName As String
    Property EntityId As Integer
    Property LastChangeUser As String
End Interface

Public Class AlertEntity
    Implements IAlertEntity

    Public Property ClientToken As Guid Implements IAlertEntity.ClientToken

    Public Property EntityId As Integer Implements IAlertEntity.EntityId

    Public Property Id As Integer Implements IAlertEntity.Id

    Public Property LastName As String Implements IAlertEntity.LastName

    Public Property LastChangeUser As String Implements IAlertEntity.LastChangeUser
End Class