Imports System.Web.Optimization

Public Class Global_asax
    Inherits HttpApplication

    Sub Application_Start(sender As Object, e As EventArgs)
        ' Fires when the application is started
        RouteConfig.RegisterRoutes(RouteTable.Routes)
        BundleConfig.RegisterBundles(BundleTable.Bundles)

        'tracing and caching provider init should only need to be done once per app domain
        'this could be done in a custom activator
        'Reference.John.Repository.Infrastructure.TracingProviderConfiguration.Init()
        'Reference.John.Repository.Infrastructure.CachingProviderConfiguration.Init()
    End Sub


    Private _storage As Reference.John.Repository.Infrastructure.WebDbContextStorage
    Public Const STORAGE_KEY As String = "HttpContextObjectContextStorageKey"

    'TODO move these events into a custom activator
    'TODO strongly type those actionlinks

    Protected Sub Application_BeginRequest(sender As Object, e As EventArgs)
        Debug.WriteLine("DbContextInitializer.Instance.InitializeDbContextOnce Start ")
        Reference.John.Repository.Infrastructure.DbContextInitializer.Instance.InitializeDbContextOnce(Sub()
                                                                                                           Reference.John.Repository.Infrastructure.DbContextManager.InitStorage(_storage)
                                                                                                           Reference.John.Repository.Infrastructure.DbContextManager.Init(Reference.John.Resources.Constants.ConnectionStringKey, False)
                                                                                                       End Sub)
        Debug.WriteLine("DbContextInitializer.Instance.InitializeDbContextOnce End ")
    End Sub

    Private Sub MvcApplication_EndRequest(sender As Object, e As EventArgs) Handles Me.EndRequest
        Debug.WriteLine("DbContextManager.CloseAllDbContexts Start ")
        Reference.John.Repository.Infrastructure.DbContextManager.CloseAllDbContexts()
        HttpContext.Current.Items.Remove(STORAGE_KEY)
        Debug.WriteLine("DbContextManager.CloseAllDbContexts End ")

    End Sub

    Public Overrides Sub Init()
        MyBase.Init()
        Debug.WriteLine("WebContextStorage Constructor Start ")
        _storage = New Reference.John.Repository.Infrastructure.WebDbContextStorage()
        Debug.WriteLine("WebContextStorage Constructor End ")
    End Sub

End Class