
Public Class ValidationSesrvice
    Implements IValidationService

    Public Function ExecuteAllValidations(item As Object) As Object Implements IValidationService.ExecuteAllValidations
        Return Nothing
    End Function

    Public Sub ExecuteValidation(item As Object) Implements IValidationService.ExecuteValidation
        Throw New NotImplementedException
    End Sub

End Class