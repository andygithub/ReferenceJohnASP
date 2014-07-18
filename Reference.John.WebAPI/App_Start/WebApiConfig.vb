Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Web.Http

Public Module WebApiConfig
    Public Sub Register(ByVal config As HttpConfiguration)
        ' Web API configuration and services

        ' Web API routes
        config.MapHttpAttributeRoutes()

        config.Routes.MapHttpRoute(
            name:="DefaultApi",
            routeTemplate:="api/{controller}/{id}",
            defaults:=New With {.id = RouteParameter.Optional}
        )

        ' There can be multiple exception loggers. (By default, no exception loggers are registered.)
        config.Services.Add(GetType(Http.ExceptionHandling.IExceptionLogger), New ExceptionHandling.ElmahExceptionLogger())

        'There must be exactly one exception handler. (There is a default one that may be replaced.)
        config.Services.Replace(GetType(Http.ExceptionHandling.IExceptionHandler), New ExceptionHandling.GenericTextExceptionHandler())
    End Sub
End Module
