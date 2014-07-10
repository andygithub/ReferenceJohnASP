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
        ViewEngines.Engines.Clear()
        ViewEngines.Engines.Add(New RazorViewEngine())
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

End Class

