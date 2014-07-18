
Imports System.Collections.Generic
Imports System.Linq
Imports System.Net
Imports System.Net.Http
Imports System.Web.Http.Controllers
Imports System.Web.Http.Filters
Imports System.Web.Http.ModelBinding

Namespace Filters

    <AttributeUsage(AttributeTargets.Method)>
    Public Class ValidateModelAttribute
        Inherits System.Web.Http.Filters.ActionFilterAttribute

        Public Overrides Sub OnActionExecuting(actionContext As HttpActionContext)
            If actionContext.ModelState.IsValid = False Then
                actionContext.Response = actionContext.Request.CreateErrorResponse(HttpStatusCode.BadRequest, actionContext.ModelState)
            End If
        End Sub

    End Class

End Namespace

