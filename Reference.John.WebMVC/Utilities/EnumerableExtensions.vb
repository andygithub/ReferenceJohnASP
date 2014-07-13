Public Module EnumerableExtensions

    ''' <summary>
    ''' Converts the source sequence into an IEnumerable of SelectListItem
    ''' </summary>
    <System.Runtime.CompilerServices.Extension> _
    Public Function ToSelectList(Of TItem, TValue)(items As IEnumerable(Of TItem), valueSelector As Func(Of TItem, TValue), nameSelector As Func(Of TItem, String)) As IEnumerable(Of SelectListItem)
        Return items.ToSelectList(valueSelector, nameSelector, Function(x) False)
    End Function

    ''' <summary>
    ''' Converts the source sequence into an IEnumerable of SelectListItem
    ''' </summary>
    <System.Runtime.CompilerServices.Extension> _
    Public Function ToSelectList(Of TItem, TValue)(items As IEnumerable(Of TItem), valueSelector As Func(Of TItem, TValue), nameSelector As Func(Of TItem, String), selectedItems As IEnumerable(Of TValue)) As IEnumerable(Of SelectListItem)
        Return items.ToSelectList(valueSelector, nameSelector, Function(x) selectedItems IsNot Nothing AndAlso selectedItems.Contains(valueSelector(x)))
    End Function

    ''' <summary>
    ''' Converts the source sequence into an IEnumerable of SelectListItem
    ''' </summary>
    <System.Runtime.CompilerServices.Extension>
    Public Function ToSelectList(Of TItem, TValue)(items As IEnumerable(Of TItem), valueSelector As Func(Of TItem, TValue), nameSelector As Func(Of TItem, String), selectedValueSelector As Func(Of TItem, Boolean)) As IEnumerable(Of SelectListItem)
        Dim _list As New List(Of SelectListItem)
        For Each item In items
            Dim value = valueSelector(item)
            _list.Add(New SelectListItem() With {.Text = nameSelector(item), .Value = value.ToString(), .Selected = selectedValueSelector(item)})
        Next
        Return _list
    End Function

End Module