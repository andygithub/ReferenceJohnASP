Public Class HomeController
    Inherits System.Web.Mvc.Controller

    Function Index() As ActionResult
        Return View()
    End Function

    Function AjaxError() As ActionResult
        Return View()
    End Function

    Function About() As ActionResult
        ViewData("Message") = "Your application description page."

        Return View()
    End Function

    Function Contact() As ActionResult
        ViewData("Message") = "Your contact page."

        Return View()
    End Function

    Function ShowError() As ActionResult
        Throw New NotImplementedException
    End Function

    Function ShowErrorJSON() As JsonResult
        Throw New NotImplementedException
    End Function

    Function ShowInvalidJSON() As JsonResult
        Return New JsonResult With {.Data = New With {.success = False, .error = " custom validation message "}, .JsonRequestBehavior = JsonRequestBehavior.AllowGet}
    End Function

    <HttpPost>
    Function ShowError(id As String) As ActionResult
        Throw New NotImplementedException
    End Function

    <HttpPost>
    Function ShowErrorJSON(id As String) As JsonResult
        Throw New NotImplementedException
    End Function

    <HttpPost>
    Function ShowAjaxPost(id As String) As ActionResult
        Return PartialView("SimplePost", New Reference.John.Domain.RegionOptionList With {.RegionId = id})
    End Function

End Class
