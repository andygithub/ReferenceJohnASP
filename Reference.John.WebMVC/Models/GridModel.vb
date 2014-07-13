Namespace Models
    Public Class GridModel(Of T)

        Public Property TotalRows As Integer
        Public Property CurrentPage As Integer
        Public Property Data As IEnumerable(Of T)

    End Class
End Namespace