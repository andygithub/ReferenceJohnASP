Imports System.Web.Optimization

Public Class Global_asax
    Inherits HttpApplication

    Sub Application_Start(sender As Object, e As EventArgs)
        ' Fires when the application is started
        RouteConfig.RegisterRoutes(RouteTable.Routes)
        BundleConfig.RegisterBundles(BundleTable.Bundles)

    End Sub


    Private _storage As Reference.John.Repository.Infrastructure.WebDbContextStorage

    'TODO move these events into a custom activator
    'TODO strongly type those actionlinks

    Protected Sub Application_BeginRequest(sender As Object, e As EventArgs) Handles Me.BeginRequest
        Debug.WriteLine(Reference.John.Resources.Resources.LogMessages.DbContextInitializerInstanceInitializeDbContextOnceStart)
        Reference.John.Repository.Infrastructure.DbContextInitializer.Instance.InitializeDbContextOnce(Sub()
                                                                                                           Reference.John.Repository.Infrastructure.DbContextManager.InitStorage(_storage)
                                                                                                           Reference.John.Repository.Infrastructure.DbContextManager.Init(Reference.John.Resources.Constants.ConnectionStringKey, False)
                                                                                                       End Sub)
        Debug.WriteLine(Reference.John.Resources.Resources.LogMessages.DbContextInitializerInstanceInitializeDbContextOnceEnd)
    End Sub

    Protected Sub Application_EndRequest(sender As Object, e As EventArgs) Handles Me.EndRequest
        Debug.WriteLine(Reference.John.Resources.Resources.LogMessages.DbContextManagerCloseAllDbContextsStart)
        Reference.John.Repository.Infrastructure.DbContextManager.CloseAllDbContexts()
        HttpContext.Current.Items.Remove(Reference.John.Resources.Constants.HttpContextStorageKey)
        Debug.WriteLine(Reference.John.Resources.Resources.LogMessages.DbContextManagerCloseAllDbContextsEnd)
    End Sub

    Public Overrides Sub Init()
        MyBase.Init()
        Debug.WriteLine(Reference.John.Resources.Resources.LogMessages.WebContextStorageConstructorStart)
        _storage = New Reference.John.Repository.Infrastructure.WebDbContextStorage()
        Debug.WriteLine(Reference.John.Resources.Resources.LogMessages.WebContextStorageConstructorEnd)
    End Sub

End Class