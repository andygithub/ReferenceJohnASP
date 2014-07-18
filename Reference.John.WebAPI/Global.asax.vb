Imports System.Web.Http
Imports System.Web.Optimization
Imports Microsoft.Practices.Unity

Public Class WebApiApplication
    Inherits System.Web.HttpApplication

    Sub Application_Start()
        AreaRegistration.RegisterAllAreas()
        GlobalConfiguration.Configure(AddressOf WebApiConfig.Register)
        FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters)
        FilterConfig.RegisterWebApiFilters(GlobalConfiguration.Configuration.Filters)
        RouteConfig.RegisterRoutes(RouteTable.Routes)
        BundleConfig.RegisterBundles(BundleTable.Bundles)
        'unity initialization is taking place in UnityMvcActivator
        'automapper init
        Reference.John.UI.Model.MappingConfig.RegisterDomainMapping()
        'this is in place for the circular reference exceptions are being thrown by web api serialization,  ideally the message structure would change so this is not necessary
        ' Dim json = GlobalConfiguration.Configuration.Formatters.JsonFormatter
        'json.SerializerSettings.PreserveReferencesHandling = Newtonsoft.Json.PreserveReferencesHandling.All
    End Sub

    Dim _logger As John.Infrastructure.Logging.ILogger = John.Infrastructure.Container.ContainerFactory.GetConfiguredContainer.Resolve(Of Reference.John.Infrastructure.Logging.ILogger)()

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
