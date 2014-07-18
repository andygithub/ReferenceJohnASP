Imports System.Web
Imports System.Web.Mvc

Public Module FilterConfig
    Public Sub RegisterGlobalFilters(ByVal filters As GlobalFilterCollection)
        filters.Add(New HandleErrorAttribute())
    End Sub

    Public Sub RegisterWebApiFilters(filters As System.Web.Http.Filters.HttpFilterCollection)
        'filters.Add(New Filters.UnhandledExceptionFilter)
        filters.Add(New Filters.ValidateModelAttribute())
    End Sub
End Module

