Public Class FormController
    Inherits System.Web.Mvc.Controller

    Function ThrowError() As ActionResult
        Throw New NotImplementedException
    End Function

End Class
