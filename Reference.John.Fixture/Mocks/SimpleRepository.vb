Namespace Mocks
    Public Interface ISimpleRepository

        Property ExecutionCount As Integer

        Sub UpdateItem(item As Domain.Address)
        Sub CreateItem(item As Domain.Address)
        Sub DeleteItem(item As Domain.Address)
        Function GetItems() As IEnumerable(Of Domain.Address)

        Function GetItemsLargeResult() As IEnumerable(Of Domain.Address)

        Function GetItemsById(id As Integer) As IEnumerable(Of Domain.Address)

        Function GetItemsByObject(item As Domain.FormSimpleZero) As IEnumerable(Of Domain.Address)

        Function GetItemsComplex() As ComplexDomainObject

    End Interface

    Public Class ComplexDomainObject
        Public Property ID As Guid
        Public Property Addresses As IEnumerable(Of Domain.Address)
        Public Property Contacts As IEnumerable(Of Domain.FormSimpleZero)
    End Class

    Public Class SimpleRepository
        Implements ISimpleRepository

        Public Sub CreateItem(item As Domain.Address) Implements ISimpleRepository.CreateItem

        End Sub

        Public Sub DeleteItem(item As Domain.Address) Implements ISimpleRepository.DeleteItem

        End Sub

        Public Function GetItems() As IEnumerable(Of Domain.Address) Implements ISimpleRepository.GetItems
            ExecutionCount = ExecutionCount + 1
            Return New List(Of Domain.Address) From {New Domain.Address With {.Id = -100, .AddressLine2 = "stas"}}
        End Function

        Public Sub UpdateItem(item As Domain.Address) Implements ISimpleRepository.UpdateItem

        End Sub

        Public Property ExecutionCount As Integer Implements ISimpleRepository.ExecutionCount

        Public Function GetItemsById(id As Integer) As IEnumerable(Of Domain.Address) Implements ISimpleRepository.GetItemsById
            ExecutionCount = ExecutionCount + 1
            Return New List(Of Domain.Address) From {New Domain.Address With {.Id = -100, .AddressLine2 = "stas", .FormSimpleZeroId = id}}
        End Function

        Public Function GetItemsByObject(item As Domain.FormSimpleZero) As IEnumerable(Of Domain.Address) Implements ISimpleRepository.GetItemsByObject
            ExecutionCount = ExecutionCount + 1
            Return New List(Of Domain.Address) From {New Domain.Address With {.Id = -100, .AddressLine2 = "stas", .FormSimpleZeroId = item.Id}}
        End Function

        Public Function GetItemsComplex() As ComplexDomainObject Implements ISimpleRepository.GetItemsComplex
            ExecutionCount = ExecutionCount + 1
            Return New ComplexDomainObject With {.ID = Guid.NewGuid}
        End Function

        Public Function GetItemsLargeResult() As IEnumerable(Of Domain.Address) Implements ISimpleRepository.GetItemsLargeResult
            ExecutionCount = ExecutionCount + 1
            Dim item As New List(Of Domain.Address) From {New Domain.Address With {.Id = -100, .AddressLine2 = "stas"}}
            For i As Integer = 0 To 1500
                item.Add(New Domain.Address With {.Id = -100, .AddressLine2 = "stas"})
            Next
            Return item
        End Function
    End Class


End Namespace