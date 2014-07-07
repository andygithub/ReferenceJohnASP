Imports System.Text
Imports Microsoft.VisualStudio.TestTools.UnitTesting
Imports Microsoft.Practices.Unity
Imports Microsoft.Practices.Unity.InterceptionExtension
Imports Microsoft.ApplicationServer.Caching

<TestClass()> Public Class CachingAppFabricProviderTest

    <TestMethod()> Public Sub SimpleAppFabricUsageEmbeddedEndpoint()
        'expect that appfabric is listening on the configured endpoint.
        'Set up the DataCacheFactory configuration
        Dim conf As New DataCacheFactoryConfiguration() With {.Servers = New List(Of DataCacheServerEndpoint) From {New DataCacheServerEndpoint("localhost", 22233)}}
        Dim fac As New DataCacheFactory(conf)
        Dim _key As String = "eee"
        Dim _value As String = "SimpleAppFabricUsageEmbeddedEndpoint"
        fac.GetDefaultCache.Put(_key, _value)
        Assert.AreEqual(_value, fac.GetDefaultCache.Get(_key))
    End Sub

    <TestMethod()> Public Sub SimpleAppFabricUsageConfiguration()
        'Set up the DataCacheFactory configuration
        Dim conf As New DataCacheFactoryConfiguration("default")
        Dim fac As New DataCacheFactory(conf)
        Dim _key As String = "eee"
        Dim _value As String = "SimpleAppFabricUsageConfiguration"
        fac.GetDefaultCache.Put(_key, _value)
        Assert.AreEqual(_value, fac.GetDefaultCache.Get(_key))
    End Sub


    <TestMethod()> Public Sub ExecuteGetWithCacheDisabled()
        Using container As New UnityContainer()
            container.AddNewExtension(Of Interception)()
            container.RegisterType(GetType(Reference.John.Core.Logging.ILogger), GetType(Reference.John.Core.Logging.TrageLogger))
            container.RegisterType(Of Reference.John.Infrastructure.Cache.ICacheProviderConfiguration, Reference.John.Infrastructure.Cache.CacheProviderConfiguration)(New ContainerControlledLifetimeManager,
                                                                                                                         New InjectionFactory(Function(c)
                                                                                                                                                  Dim item As New Reference.John.Infrastructure.Cache.CacheProviderConfiguration With {.IsCachingEnabled = False, .DefaultCachingPolicy = New Reference.John.Infrastructure.Cache.CommandCachingPolicy, .DefaultCache = New Reference.John.Infrastructure.Cache.AppFabricCache}
                                                                                                                                                  item.DefaultCachingPolicy.CacheableCommands = Reference.John.Infrastructure.Cache.CacheProviderConfigurationFactory.LoadCommandDefinitions
                                                                                                                                                  Return item
                                                                                                                                              End Function))

            container.RegisterType(GetType(Mocks.ISimpleRepository),
                                      GetType(Mocks.SimpleRepository),
                                      New HierarchicalLifetimeManager,
                                      New Interceptor(Of InterfaceInterceptor)(),
                                      New InterceptionBehavior(Of Reference.John.WebASP.Container.CachingInterceptorBehavior)(),
                                      New InterceptionBehavior(Of Reference.John.WebASP.Container.CachingResetInterceptorBehavior)()
                                      )
            Dim repos As Mocks.ISimpleRepository = container.Resolve(Of Mocks.ISimpleRepository)()
            Assert.IsInstanceOfType(repos, GetType(Mocks.ISimpleRepository))

            Dim _list = repos.GetItems
            Assert.IsNotNull(_list)
            _list = repos.GetItems
            Assert.IsNotNull(_list)
            Assert.AreEqual(2, repos.ExecutionCount)

            Dim cachesettings = container.Resolve(Of Reference.John.Infrastructure.Cache.ICacheProviderConfiguration)()
            Assert.IsInstanceOfType(cachesettings.DefaultCache, GetType(Reference.John.Infrastructure.Cache.AppFabricCache))
        End Using
    End Sub

    <TestMethod()> Public Sub ExecuteGetWithNoCommandMatch()
        Using container As New UnityContainer()
            container.AddNewExtension(Of Interception)()
            container.RegisterType(GetType(Reference.John.Core.Logging.ILogger), GetType(Reference.John.Core.Logging.TrageLogger))
            container.RegisterType(Of Reference.John.Infrastructure.Cache.ICacheProviderConfiguration, Reference.John.Infrastructure.Cache.CacheProviderConfiguration)(New ContainerControlledLifetimeManager,
                                                                                                                         New InjectionFactory(Function(c)
                                                                                                                                                  Dim item As New Reference.John.Infrastructure.Cache.CacheProviderConfiguration With {.IsCachingEnabled = True, .DefaultCachingPolicy = New Reference.John.Infrastructure.Cache.CommandCachingPolicy, .DefaultCache = New Reference.John.Infrastructure.Cache.AppFabricCache}
                                                                                                                                                  item.DefaultCachingPolicy.CacheableCommands = New List(Of Reference.John.Infrastructure.Cache.CacheCommandDefinition) From {New Reference.John.Infrastructure.Cache.CacheCommandDefinition With {.AbsoluteExpiration = DateTime.MaxValue, .CacheMethodName = New List(Of String) From {GetType(Mocks.ISimpleRepository).ToString & ".GetItems111"}, .DependentEntities = New List(Of String) From {"SimpleRepository", "Address"}, .EntityName = "SimpleRepository", .SlidingExpiration = New TimeSpan(0, 10, 0)}}
                                                                                                                                                  Return item
                                                                                                                                              End Function))
            container.RegisterType(GetType(Mocks.ISimpleRepository),
                                      GetType(Mocks.SimpleRepository),
                                      New HierarchicalLifetimeManager,
                                      New Interceptor(Of InterfaceInterceptor)(),
                                      New InterceptionBehavior(Of Reference.John.WebASP.Container.CachingInterceptorBehavior)(),
                                      New InterceptionBehavior(Of Reference.John.WebASP.Container.CachingResetInterceptorBehavior)()
                                      )
            Dim repos As Mocks.ISimpleRepository = container.Resolve(Of Mocks.ISimpleRepository)()

            Assert.IsInstanceOfType(repos, GetType(Mocks.ISimpleRepository))
            Dim _list = repos.GetItems
            Assert.IsNotNull(_list)
            _list = repos.GetItems
            Assert.IsNotNull(_list)
            Assert.AreEqual(2, repos.ExecutionCount)
            Dim cachesettings = container.Resolve(Of Reference.John.Infrastructure.Cache.ICacheProviderConfiguration)()
            Assert.IsInstanceOfType(cachesettings.DefaultCache, GetType(Reference.John.Infrastructure.Cache.AppFabricCache))
        End Using
    End Sub

    <TestMethod()> Public Sub ExecuteGetWithCommandMatch()
        Dim entitysets As New List(Of String) From {"SimpleRepository", "Address"}
        Using container As New UnityContainer()
            container.AddNewExtension(Of Interception)()
            container.RegisterType(GetType(Reference.John.Core.Logging.ILogger), GetType(Reference.John.Core.Logging.TrageLogger))
            container.RegisterType(Of Reference.John.Infrastructure.Cache.ICacheProviderConfiguration, Reference.John.Infrastructure.Cache.CacheProviderConfiguration)(New ContainerControlledLifetimeManager,
                                                                                                                         New InjectionFactory(Function(c)
                                                                                                                                                  Dim item As New Reference.John.Infrastructure.Cache.CacheProviderConfiguration With {.IsCachingEnabled = True, .DefaultCachingPolicy = New Reference.John.Infrastructure.Cache.CommandCachingPolicy, .DefaultCache = New Reference.John.Infrastructure.Cache.AppFabricCache}
                                                                                                                                                  item.DefaultCachingPolicy.CacheableCommands = New List(Of Reference.John.Infrastructure.Cache.CacheCommandDefinition) From {New Reference.John.Infrastructure.Cache.CacheCommandDefinition With {.AbsoluteExpiration = DateTime.MaxValue, .CacheMethodName = New List(Of String) From {GetType(Mocks.ISimpleRepository).ToString & ".GetItems"}, .DependentEntities = entitysets, .EntityName = "SimpleRepository", .SlidingExpiration = New TimeSpan(0, 10, 0)}}
                                                                                                                                                  Return item
                                                                                                                                              End Function))
            container.RegisterType(GetType(Mocks.ISimpleRepository),
                                      GetType(Mocks.SimpleRepository),
                                      New HierarchicalLifetimeManager,
                                      New Interceptor(Of InterfaceInterceptor)(),
                                      New InterceptionBehavior(Of Reference.John.WebASP.Container.CachingInterceptorBehavior)(),
                                      New InterceptionBehavior(Of Reference.John.WebASP.Container.CachingResetInterceptorBehavior)()
                                      )
            Dim repos As Mocks.ISimpleRepository = container.Resolve(Of Mocks.ISimpleRepository)()

            Assert.IsInstanceOfType(repos, GetType(Mocks.ISimpleRepository))
            'reset the cache in case there is something present already
            Dim cachesettings = container.Resolve(Of Reference.John.Infrastructure.Cache.ICacheProviderConfiguration)()
            cachesettings.DefaultCache.InvalidateSets(entitysets)

            Dim _list = repos.GetItems
            Assert.IsNotNull(_list)
            _list = repos.GetItems
            Assert.IsNotNull(_list)
            Assert.AreEqual(-100, _list(0).Id)
            Assert.AreEqual(1, repos.ExecutionCount)
            Assert.IsInstanceOfType(cachesettings.DefaultCache, GetType(Reference.John.Infrastructure.Cache.AppFabricCache))
            'would need to check the appfabric instance if the data was populated using the powershell interface or using the app fabric 
        End Using
    End Sub

    <TestMethod()> Public Sub ExecuteGetWithSimpleParameter()
        Dim entitysets As New List(Of String) From {"SimpleRepository", "Address"}
        Using container As New UnityContainer()
            container.AddNewExtension(Of Interception)()
            container.RegisterType(GetType(Reference.John.Core.Logging.ILogger), GetType(Reference.John.Core.Logging.TrageLogger))
            container.RegisterType(Of Reference.John.Infrastructure.Cache.ICacheProviderConfiguration, Reference.John.Infrastructure.Cache.CacheProviderConfiguration)(New ContainerControlledLifetimeManager,
                                                                                                                         New InjectionFactory(Function(c)
                                                                                                                                                  Dim item As New Reference.John.Infrastructure.Cache.CacheProviderConfiguration With {.IsCachingEnabled = True, .DefaultCachingPolicy = New Reference.John.Infrastructure.Cache.CommandCachingPolicy, .DefaultCache = New Reference.John.Infrastructure.Cache.AppFabricCache}
                                                                                                                                                  item.DefaultCachingPolicy.CacheableCommands = New List(Of Reference.John.Infrastructure.Cache.CacheCommandDefinition) From {New Reference.John.Infrastructure.Cache.CacheCommandDefinition With {.AbsoluteExpiration = DateTime.MaxValue, .CacheMethodName = New List(Of String) From {GetType(Mocks.ISimpleRepository).ToString & ".GetItemsById"}, .DependentEntities = entitysets, .EntityName = "SimpleRepository", .SlidingExpiration = New TimeSpan(0, 10, 0)}}
                                                                                                                                                  Return item
                                                                                                                                              End Function))
            container.RegisterType(GetType(Mocks.ISimpleRepository),
                                      GetType(Mocks.SimpleRepository),
                                      New HierarchicalLifetimeManager,
                                      New Interceptor(Of InterfaceInterceptor)(),
                                      New InterceptionBehavior(Of Reference.John.WebASP.Container.CachingInterceptorBehavior)(),
                                      New InterceptionBehavior(Of Reference.John.WebASP.Container.CachingResetInterceptorBehavior)()
                                      )
            Dim repos As Mocks.ISimpleRepository = container.Resolve(Of Mocks.ISimpleRepository)()

            Assert.IsInstanceOfType(repos, GetType(Mocks.ISimpleRepository))
            'reset the cache in case there is something present already
            Dim cachesettings = container.Resolve(Of Reference.John.Infrastructure.Cache.ICacheProviderConfiguration)()
            cachesettings.DefaultCache.InvalidateSets(entitysets)

            Dim _getParameter As Integer = -100
            Dim _list = repos.GetItemsById(_getParameter)
            Assert.IsNotNull(_list)
            Assert.AreEqual(_getParameter, _list(0).Id)
            _getParameter = -300
            _list = repos.GetItemsById(_getParameter)
            Assert.IsNotNull(_list)
            Assert.AreEqual(_getParameter, _list(0).FormSimpleZeroId)
            Assert.AreEqual(2, repos.ExecutionCount)
            Assert.IsInstanceOfType(cachesettings.DefaultCache, GetType(Reference.John.Infrastructure.Cache.AppFabricCache))
        End Using
    End Sub

    <TestMethod()> Public Sub ExecuteGetWithComplexParameter()
        Dim entitysets As New List(Of String) From {"SimpleRepository", "Address"}
        Using container As New UnityContainer()
            container.AddNewExtension(Of Interception)()
            container.RegisterType(GetType(Reference.John.Core.Logging.ILogger), GetType(Reference.John.Core.Logging.TrageLogger))
            container.RegisterType(Of Reference.John.Infrastructure.Cache.ICacheProviderConfiguration, Reference.John.Infrastructure.Cache.CacheProviderConfiguration)(New ContainerControlledLifetimeManager,
                                                                                                                         New InjectionFactory(Function(c)
                                                                                                                                                  Dim item As New Reference.John.Infrastructure.Cache.CacheProviderConfiguration With {.IsCachingEnabled = True, .DefaultCachingPolicy = New Reference.John.Infrastructure.Cache.CommandCachingPolicy, .DefaultCache = New Reference.John.Infrastructure.Cache.AppFabricCache}
                                                                                                                                                  item.DefaultCachingPolicy.CacheableCommands = New List(Of Reference.John.Infrastructure.Cache.CacheCommandDefinition) From {New Reference.John.Infrastructure.Cache.CacheCommandDefinition With {.AbsoluteExpiration = DateTime.MaxValue, .CacheMethodName = New List(Of String) From {GetType(Mocks.ISimpleRepository).ToString & ".GetItemsByObject"}, .DependentEntities = entitysets, .EntityName = "SimpleRepository", .SlidingExpiration = New TimeSpan(0, 10, 0)}}
                                                                                                                                                  Return item
                                                                                                                                              End Function))
            container.RegisterType(GetType(Mocks.ISimpleRepository),
                                      GetType(Mocks.SimpleRepository),
                                      New HierarchicalLifetimeManager,
                                      New Interceptor(Of InterfaceInterceptor)(),
                                      New InterceptionBehavior(Of Reference.John.WebASP.Container.CachingInterceptorBehavior)(),
                                      New InterceptionBehavior(Of Reference.John.WebASP.Container.CachingResetInterceptorBehavior)()
                                      )
            Dim repos As Mocks.ISimpleRepository = container.Resolve(Of Mocks.ISimpleRepository)()

            Assert.IsInstanceOfType(repos, GetType(Mocks.ISimpleRepository))
            'reset the cache in case there is something present already
            Dim cachesettings = container.Resolve(Of Reference.John.Infrastructure.Cache.ICacheProviderConfiguration)()
            cachesettings.DefaultCache.InvalidateSets(entitysets)

            Dim _getParameter As New Reference.John.Domain.FormSimpleZero With {.Id = -200}
            Dim _list = repos.GetItemsByObject(_getParameter)
            Assert.IsNotNull(_list)
            Assert.AreEqual(_getParameter.Id, _list(0).FormSimpleZeroId)
            _getParameter = New Reference.John.Domain.FormSimpleZero With {.Id = -300}
            _list = repos.GetItemsByObject(_getParameter)
            Assert.IsNotNull(_list)
            Assert.AreEqual(_getParameter.Id, _list(0).FormSimpleZeroId)
            Assert.AreEqual(2, repos.ExecutionCount)
            Assert.IsInstanceOfType(cachesettings.DefaultCache, GetType(Reference.John.Infrastructure.Cache.AppFabricCache))
        End Using
    End Sub

    <TestMethod()> Public Sub ExecuteGetWithComplexParameterAndResetCache()
        Dim entitysets As New List(Of String) From {"SimpleRepository", "Address"}
        Using container As New UnityContainer()
            container.AddNewExtension(Of Interception)()
            container.RegisterType(GetType(Reference.John.Core.Logging.ILogger), GetType(Reference.John.Core.Logging.TrageLogger))
            container.RegisterType(Of Reference.John.Infrastructure.Cache.ICacheProviderConfiguration, Reference.John.Infrastructure.Cache.CacheProviderConfiguration)(New ContainerControlledLifetimeManager,
                                                                                                                         New InjectionFactory(Function(c)
                                                                                                                                                  Dim item As New Reference.John.Infrastructure.Cache.CacheProviderConfiguration With {.IsCachingEnabled = True, .DefaultCachingPolicy = New Reference.John.Infrastructure.Cache.CommandCachingPolicy, .DefaultCache = New Reference.John.Infrastructure.Cache.AppFabricCache}
                                                                                                                                                  item.DefaultCachingPolicy.CacheableCommands = New List(Of Reference.John.Infrastructure.Cache.CacheCommandDefinition) From {New Reference.John.Infrastructure.Cache.CacheCommandDefinition With {.AbsoluteExpiration = DateTime.MaxValue, .CacheMethodName = New List(Of String) From {GetType(Mocks.ISimpleRepository).ToString & ".GetItemsByObject"}, .CacheResetMethodName = New List(Of String) From {GetType(Mocks.ISimpleRepository).ToString & ".UpdateItem"},
                                                                                                                                                                                                                                                                                                     .DependentEntities = entitysets, .EntityName = "SimpleRepository", .SlidingExpiration = New TimeSpan(0, 10, 0)}}
                                                                                                                                                  Return item
                                                                                                                                              End Function))
            container.RegisterType(GetType(Mocks.ISimpleRepository),
                                      GetType(Mocks.SimpleRepository),
                                      New HierarchicalLifetimeManager,
                                      New Interceptor(Of InterfaceInterceptor)(),
                                      New InterceptionBehavior(Of Reference.John.WebASP.Container.CachingInterceptorBehavior)(),
                                      New InterceptionBehavior(Of Reference.John.WebASP.Container.CachingResetInterceptorBehavior)()
                                      )
            Dim repos As Mocks.ISimpleRepository = container.Resolve(Of Mocks.ISimpleRepository)()

            Assert.IsInstanceOfType(repos, GetType(Mocks.ISimpleRepository))
            'reset the cache in case there is something present already
            Dim cachesettings = container.Resolve(Of Reference.John.Infrastructure.Cache.ICacheProviderConfiguration)()
            cachesettings.DefaultCache.InvalidateSets(entitysets)

            Dim _getParameter As New Reference.John.Domain.FormSimpleZero With {.Id = -200}
            Dim _list = repos.GetItemsByObject(_getParameter)
            Assert.IsNotNull(_list)
            Assert.AreEqual(_getParameter.Id, _list(0).FormSimpleZeroId)
            _getParameter = New Reference.John.Domain.FormSimpleZero With {.Id = -300}
            _list = repos.GetItemsByObject(_getParameter)
            Assert.IsNotNull(_list)
            Assert.AreEqual(_getParameter.Id, _list(0).FormSimpleZeroId)
            Assert.AreEqual(2, repos.ExecutionCount)

            Assert.IsInstanceOfType(cachesettings.DefaultCache, GetType(Reference.John.Infrastructure.Cache.AppFabricCache))
            'call an update that is configured to reset the cache.
            repos.UpdateItem(Nothing)
            _list = repos.GetItemsByObject(_getParameter)
            Assert.IsNotNull(_list)
            Assert.AreEqual(_getParameter.Id, _list(0).FormSimpleZeroId)
            Assert.AreEqual(3, repos.ExecutionCount)
        End Using
    End Sub

End Class