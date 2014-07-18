Imports System.Web.Optimization
Imports Microsoft.Practices.Unity

Public Class MvcApplication
    Inherits System.Web.HttpApplication

    Protected Sub Application_Start()
        AreaRegistration.RegisterAllAreas()
        FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters)
        RouteConfig.RegisterRoutes(RouteTable.Routes)
        BundleConfig.RegisterBundles(BundleTable.Bundles)
        'unity initialization is taking place in UnityMvcActivator
        'automapper init
        Reference.John.UI.Model.MappingConfig.RegisterDomainMapping()
        ViewEngines.Engines.Clear()
        ViewEngines.Engines.Add(New RazorViewEngine())
        'this is from this - http://ajdotnet.wordpress.com/2014/01/19/asp-net-mvc-i18n-part-7-model-attributes/
        'overload for the default messages.
        ClientDataTypeModelValidatorProvider.ResourceClassKey = "GlobalResources"
    End Sub

    Dim _logger As John.Infrastructure.Logging.ILogger = John.Infrastructure.Container.ContainerFactory.GetConfiguredContainer.Resolve(Of John.Infrastructure.Logging.ILogger)()

    Private _storage As John.Repository.Infrastructure.WebDbContextStorage

    'TODO move these events into a custom activator

    Protected Sub Application_BeginRequest(sender As Object, e As EventArgs) Handles Me.BeginRequest
        'Debug.WriteLine(HttpContext.Current.Request.Url)
        _logger.Info(John.Resources.Resources.LogMessages.DbContextInitializerInstanceInitializeDbContextOnceStart)
        John.Repository.Infrastructure.DbContextInitializer.Instance.InitializeDbContextOnce(Sub()
                                                                                                 John.Repository.Infrastructure.DbContextManager.InitStorage(_storage)
                                                                                                 John.Repository.Infrastructure.DbContextManager.Init(John.Resources.Constants.ConnectionStringKey, False, (Sub(x) _logger.Trace(x)))
                                                                                             End Sub)
        _logger.Info(John.Resources.Resources.LogMessages.DbContextInitializerInstanceInitializeDbContextOnceEnd)
    End Sub

    Protected Sub Application_EndRequest(sender As Object, e As EventArgs) Handles Me.EndRequest
        _logger.Info(John.Resources.Resources.LogMessages.DbContextManagerCloseAllDbContextsStart)
        John.Repository.Infrastructure.DbContextManager.CloseAllDbContexts()
        HttpContext.Current.Items.Remove(John.Resources.Constants.HttpContextStorageKey)
        _logger.Info(John.Resources.Resources.LogMessages.DbContextManagerCloseAllDbContextsEnd)
    End Sub

    Public Overrides Sub Init()
        MyBase.Init()
        _logger.Info(John.Resources.Resources.LogMessages.WebContextStorageConstructorStart)
        _storage = New John.Repository.Infrastructure.WebDbContextStorage()
        _logger.Info(John.Resources.Resources.LogMessages.WebContextStorageConstructorEnd)
    End Sub

    Private Sub MvcApplication_Error(sender As Object, e As EventArgs) Handles Me.Error

        Dim httpContext = DirectCast(sender, MvcApplication).Context

        Dim currentRouteData = RouteTable.Routes.GetRouteData(New HttpContextWrapper(httpContext))
        Dim currentController = " "
        Dim currentAction = " "

        Dim ex = Server.GetLastError()
        'log something here
        'Elmah.ErrorSignal.FromCurrentContext().Raise(ex)
        'Elmah.ErrorSignal.FromCurrentContext().Raise(ex, httpContext.Current)
        Elmah.ErrorLog.GetDefault(httpContext.Current).Log(New Elmah.Error(ex))

        If currentRouteData IsNot Nothing Then
            If currentRouteData.Values("controller") IsNot Nothing AndAlso Not [String].IsNullOrEmpty(currentRouteData.Values("controller").ToString()) Then
                currentController = currentRouteData.Values("controller").ToString()
            End If

            If currentRouteData.Values("action") IsNot Nothing AndAlso Not [String].IsNullOrEmpty(currentRouteData.Values("action").ToString()) Then
                currentAction = currentRouteData.Values("action").ToString()
            End If
        End If

        Dim controller = New ErrorController()
        Dim routeData = New RouteData()
        Dim action = "Index"
        If New HttpRequestWrapper(System.Web.HttpContext.Current.Request).IsAjaxRequest() Then
            Diagnostics.Debug.Write("is ajax")
            action = "JsonResult"
        Else
            Diagnostics.Debug.Write("is not ajax")
            If TypeOf ex Is HttpException Then
                Dim httpEx = TryCast(ex, HttpException)

                Select Case httpEx.GetHttpCode()
                    Case 404
                        action = "NotFound"
                        Exit Select
                    Case Else

                        ' others if any

                        action = "Index"
                        Exit Select
                End Select
            End If
        End If
        httpContext.ClearError()
        httpContext.Response.Clear()
        httpContext.Response.StatusCode = If(TypeOf ex Is HttpException, DirectCast(ex, HttpException).GetHttpCode(), 500)
        httpContext.Response.TrySkipIisCustomErrors = True
        routeData.Values("controller") = "Error"
        routeData.Values("action") = action

        controller.ViewData.Model = New HandleErrorInfo(ex, currentController, currentAction)
        DirectCast(controller, IController).Execute(New RequestContext(New HttpContextWrapper(httpContext), routeData))


    End Sub

End Class

