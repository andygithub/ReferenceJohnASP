Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Web.Http
Imports System.Web.Http.OData.Builder
Imports System.Web.Http.OData.Extensions

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

        'odata setup
        Dim builder As New ODataConventionModelBuilder
        builder.EntitySet(Of Reference.John.Domain.FormSimpleZero)("FormSimpleZero")
        builder.EntitySet(Of Reference.John.Domain.Address)("Addresses")
        builder.EntitySet(Of Reference.John.Domain.AddressTypeOptionList)("AddressTypeOptionLists")
        builder.EntitySet(Of Reference.John.Domain.EthnicityOptionList)("EthnicityOptionLists")
        builder.EntitySet(Of Reference.John.Domain.FormEntity_xref)("FormEntity_xref")
        builder.EntitySet(Of Reference.John.Domain.GenderOptionList)("GenderOptionLists")
        builder.EntitySet(Of Reference.John.Domain.RaceOptionList)("RaceOptionLists")
        builder.EntitySet(Of Reference.John.Domain.RegionOptionList)("RegionOptionLists")

        builder.EntitySet(Of Reference.John.Domain.FormAlertTemplate_xref)("FormSimpleZero_Alert")
        builder.EntitySet(Of Reference.John.Domain.AlertTemplate)("AlertTemplate")
        builder.EntitySet(Of Reference.John.Domain.AlertTypeOptionList)("AlertTemplateOptionLists")

        builder.EntitySet(Of Reference.John.Domain.Entity)("EntityForm_xref")
        builder.EntitySet(Of Reference.John.Domain.UserEntity_xref)("UserEntity_xref")

        config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel())
    End Sub
End Module
