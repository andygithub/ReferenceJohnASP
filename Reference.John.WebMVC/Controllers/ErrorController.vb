Imports System.Web.Mvc

Public Class ErrorController
    Inherits Controller

    Public Function Index() As ActionResult
        Return View()
    End Function

    Public Function NotFound() As ActionResult
        Return View()
    End Function

    Public Function JsonResult() As JsonResult
        Dim item = CType(ViewData.Model, HandleErrorInfo)
        Return New JsonResult With {.Data = New With {.success = False, .error = "An awesome custom error message - " & item.ControllerName & " " & item.ActionName & " " & item.Exception.Message}, .JsonRequestBehavior = JsonRequestBehavior.AllowGet}
    End Function

End Class
