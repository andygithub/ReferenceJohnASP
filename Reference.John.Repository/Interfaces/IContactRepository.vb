
Public Interface IFormContactZeroRepository
    Inherits IRepository
    Function NewlyCreated() As IList(Of Domain.FormSimpleZero)

    Function FindByName(firstName As String, lastName As String) As Domain.FormSimpleZero

End Interface
