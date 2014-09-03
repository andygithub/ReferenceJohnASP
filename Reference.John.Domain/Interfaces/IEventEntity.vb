''' <summary>
''' Interface for an entity that can be used to generate alerts.
''' </summary>
''' <remarks>It's expected to be used by any entity that needs to be passed into the alert service.</remarks>
Public Interface IEventEntity
    Property Id As Integer
    Property ClientToken As Guid
    Property LastName As String
    Property EntityId As Integer
    Property LastChangeUser As String
End Interface

Public Class EventEntity
    Implements IEventEntity

    Public Property ClientToken As Guid Implements IEventEntity.ClientToken

    Public Property EntityId As Integer Implements IEventEntity.EntityId

    Public Property Id As Integer Implements IEventEntity.Id

    Public Property LastName As String Implements IEventEntity.LastName

    Public Property LastChangeUser As String Implements IEventEntity.LastChangeUser
End Class