Imports System.Reflection
Imports Microsoft.Practices.Unity


<TestClass()> Public Class InstallerFixture

    <TestInitialize()>
    Public Sub Init()
        Try
            Reference.John.Repository.Infrastructure.DbContextManager.InitStorage(New Reference.John.Repository.Infrastructure.SimpleDbContextStorage())
        Catch ex As ApplicationException
            'catch this in case the storage has already been pinned for the app doamin.  This handles the tests being run in any order.  Currently the storage can't be torn down.
        End Try
        Try
            Reference.John.Repository.Infrastructure.DbContextManager.Init(Reference.John.Repository.Infrastructure.DbContextManager.DefaultConnectionStringName, True, Sub(x) Console.WriteLine(x))
            Reference.John.Repository.Infrastructure.DbContextManager.Init(Reference.John.Resources.Constants.ConnectionStringKey, True, Sub(x) Console.WriteLine(x))
            'catch this in case the connection string has already been registered.  This handles the tests being run in any order.  Currently the storage can't be torn down.
        Catch ex As ArgumentException
        End Try

    End Sub

    <TestMethod()> Public Sub InstallerFixture()
        'init contain from factory and determin if that number of registered types is greater than one.
        Dim _container = Reference.John.WebASP.Container.ContainerFactory.GetConfiguredContainer
        Assert.IsNotNull(_container)
        Assert.AreNotEqual(0, _container.Registrations.Count)
    End Sub

    <TestMethod()> Public Sub ResolveGenericRepository()
        Dim _repos = Reference.John.WebASP.Container.ContainerFactory.GetConfiguredContainer.Resolve(Of Reference.John.Repository.IRepository)()
        Assert.IsNotNull(_repos)
        Assert.IsInstanceOfType(_repos, GetType(Reference.John.Repository.IRepository))

        Assert.IsNotNull(_repos.GetAll(Of Reference.John.Domain.AddressTypeOptionList))
    End Sub

    <TestMethod()> <ExpectedException(GetType(ArgumentNullException))> Public Sub NullConstructorParameterCache()
        Dim _item As New Reference.John.WebASP.Container.CachingInterceptorBehavior(Nothing, Nothing)
    End Sub

    <TestMethod()> <ExpectedException(GetType(ArgumentNullException))> Public Sub NullConstructorParameterLogger()
        Dim _item As New Reference.John.WebASP.Container.CachingInterceptorBehavior(Reference.John.Infrastructure.Cache.CacheProviderConfigurationFactory.Create, Nothing)
    End Sub

    <TestMethod()> <ExpectedException(GetType(ArgumentNullException))> Public Sub NullConstructorParameterCacheReset()
        Dim _item As New Reference.John.WebASP.Container.CachingResetInterceptorBehavior(Nothing, Nothing)
    End Sub

    <TestMethod()> <ExpectedException(GetType(ArgumentNullException))> Public Sub NullConstructorParameterLoggerReset()
        Dim _item As New Reference.John.WebASP.Container.CachingResetInterceptorBehavior(Reference.John.Infrastructure.Cache.CacheProviderConfigurationFactory.Create, Nothing)
    End Sub

End Class
