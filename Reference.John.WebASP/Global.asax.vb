Imports System.Web.Optimization

Public Class Global_asax
    Inherits HttpApplication

    'ELmah https://code.google.com/p/elmah/wiki/Downloads?tm=2


    Sub Application_Start(sender As Object, e As EventArgs)
        ' Fires when the application is started
        RouteConfig.RegisterRoutes(RouteTable.Routes)
        BundleConfig.RegisterBundles(BundleTable.Bundles)
        'automapper init
        MappingConfig.RegisterDomainMapping()
    End Sub

    Dim _logger As Reference.John.Infrastructure.Logging.ILogger = Reference.John.Infrastructure.Container.ContainerFactory.GetConfiguredContainer.Resolve(Of Reference.John.Infrastructure.Logging.ILogger)()

    Private _storage As Reference.John.Repository.Infrastructure.WebDbContextStorage

    'TODO move these events into a custom activator

    Protected Sub Application_BeginRequest(sender As Object, e As EventArgs) Handles Me.BeginRequest
        'Debug.WriteLine(HttpContext.Current.Request.Url)
        _logger.Info(Reference.John.Resources.Resources.LogMessages.DbContextInitializerInstanceInitializeDbContextOnceStart)
        Reference.John.Repository.Infrastructure.DbContextInitializer.Instance.InitializeDbContextOnce(Sub()
                                                                                                           Reference.John.Repository.Infrastructure.DbContextManager.InitStorage(_storage)
                                                                                                           Reference.John.Repository.Infrastructure.DbContextManager.Init(Reference.John.Resources.Constants.ConnectionStringKey, False, (Sub(x) _logger.Trace(x)))
                                                                                                       End Sub)
        _logger.Info(Reference.John.Resources.Resources.LogMessages.DbContextInitializerInstanceInitializeDbContextOnceEnd)
    End Sub

    Protected Sub Application_EndRequest(sender As Object, e As EventArgs) Handles Me.EndRequest
        _logger.Info(Reference.John.Resources.Resources.LogMessages.DbContextManagerCloseAllDbContextsStart)
        Reference.John.Repository.Infrastructure.DbContextManager.CloseAllDbContexts()
        HttpContext.Current.Items.Remove(Reference.John.Resources.Constants.HttpContextStorageKey)
        _logger.Info(Reference.John.Resources.Resources.LogMessages.DbContextManagerCloseAllDbContextsEnd)
    End Sub

    Public Overrides Sub Init()
        MyBase.Init()
        _logger.Info(Reference.John.Resources.Resources.LogMessages.WebContextStorageConstructorStart)
        _storage = New Reference.John.Repository.Infrastructure.WebDbContextStorage()
        _logger.Info(Reference.John.Resources.Resources.LogMessages.WebContextStorageConstructorEnd)
    End Sub

End Class