Imports System.Text
Imports Microsoft.VisualStudio.TestTools.UnitTesting
Imports Microsoft.Practices.Unity
Imports Microsoft.Practices.Unity.InterceptionExtension
Imports System.Configuration


<TestClass()> Public Class CachingInterceptorTest

    <TestMethod()> Public Sub ExecuteCacheWithDefaultFactorySettings()
        'build container with interceptor
        Using container As New UnityContainer()
            container.AddNewExtension(Of Interception)()
            container.RegisterType(GetType(Reference.John.Core.Logging.ILogger), GetType(Reference.John.Core.Logging.TrageLogger))
            'register the configuration settings class for the behavior constructor
            'factory should only be called the first time and then the instance should be returned all times thereafter.
            container.RegisterType(Of Reference.John.Infrastructure.Cache.ICacheProviderConfiguration, Reference.John.Infrastructure.Cache.CacheProviderConfiguration)(New ContainerControlledLifetimeManager,
                                                                                                                         New InjectionFactory(Function(c) Reference.John.Infrastructure.Cache.CacheProviderConfigurationFactory.Create))
            'container.RegisterInstance(GetType(Reference.John.Infrastructure.Cache.ICacheProviderConfiguration), Reference.John.Infrastructure.Cache.CacheProviderConfigurationFactory.Create, New  )
            'register the repository with the necessary interceptors
            container.RegisterType(GetType(Mocks.ISimpleRepository),
                                      GetType(Mocks.SimpleRepository),
                                      New TransientLifetimeManager,
                                      New Interceptor(Of InterfaceInterceptor)(),
                                      New InterceptionBehavior(Of Reference.John.WebASP.Container.CachingInterceptorBehavior)(),
                                      New InterceptionBehavior(Of Reference.John.WebASP.Container.CachingResetInterceptorBehavior)()
                                      )
            'resolve item
            Dim repos As Mocks.ISimpleRepository = container.Resolve(Of Mocks.ISimpleRepository)()

            Assert.IsInstanceOfType(repos, GetType(Mocks.ISimpleRepository))
            'execute item
            Dim _list = repos.GetItems
            Assert.IsNotNull(_list)
            'execute again
            _list = repos.GetItems
            Assert.IsNotNull(_list)
            Assert.AreEqual(1, repos.ExecutionCount)
            'check stats if the cache provider is inmemory
            Dim cachesettings = container.Resolve(Of Reference.John.Infrastructure.Cache.ICacheProviderConfiguration)()
            Assert.IsInstanceOfType(cachesettings.DefaultCache, GetType(Reference.John.Infrastructure.Cache.InMemoryCache))
            Assert.AreEqual(1, DirectCast(cachesettings.DefaultCache, Reference.John.Infrastructure.Cache.InMemoryCache).CacheHits)
        End Using
    End Sub

    <TestMethod()> Public Sub ExecuteGetWithCacheDisabled()
        Using container As New UnityContainer()
            container.AddNewExtension(Of Interception)()
            container.RegisterType(GetType(Reference.John.Core.Logging.ILogger), GetType(Reference.John.Core.Logging.TrageLogger))
            container.RegisterType(Of Reference.John.Infrastructure.Cache.ICacheProviderConfiguration, Reference.John.Infrastructure.Cache.CacheProviderConfiguration)(New ContainerControlledLifetimeManager,
                                                                                                                         New InjectionFactory(Function(c)
                                                                                                                                                  Dim item As New Reference.John.Infrastructure.Cache.CacheProviderConfiguration With {.IsCachingEnabled = False, .DefaultCachingPolicy = New Reference.John.Infrastructure.Cache.CommandCachingPolicy, .DefaultCache = New Reference.John.Infrastructure.Cache.InMemoryCache}
                                                                                                                                                  item.DefaultCachingPolicy.CacheableCommands = Reference.John.Infrastructure.Cache.CacheProviderConfigurationFactory.LoadCommandDefinitions
                                                                                                                                                  Return item
                                                                                                                                              End Function))

            container.RegisterType(GetType(Mocks.ISimpleRepository),
                                      GetType(Mocks.SimpleRepository),
                                      New TransientLifetimeManager,
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
            Assert.IsInstanceOfType(cachesettings.DefaultCache, GetType(Reference.John.Infrastructure.Cache.InMemoryCache))
            Assert.AreEqual(0, DirectCast(cachesettings.DefaultCache, Reference.John.Infrastructure.Cache.InMemoryCache).CacheHits)
        End Using
    End Sub

    <TestMethod()> Public Sub ExecuteGetWithNoCommandMatch()
        Using container As New UnityContainer()
            container.AddNewExtension(Of Interception)()
            container.RegisterType(GetType(Reference.John.Core.Logging.ILogger), GetType(Reference.John.Core.Logging.TrageLogger))
            container.RegisterType(Of Reference.John.Infrastructure.Cache.ICacheProviderConfiguration, Reference.John.Infrastructure.Cache.CacheProviderConfiguration)(New ContainerControlledLifetimeManager,
                                                                                                                         New InjectionFactory(Function(c)
                                                                                                                                                  Dim item As New Reference.John.Infrastructure.Cache.CacheProviderConfiguration With {.IsCachingEnabled = True, .DefaultCachingPolicy = New Reference.John.Infrastructure.Cache.CommandCachingPolicy, .DefaultCache = New Reference.John.Infrastructure.Cache.InMemoryCache}
                                                                                                                                                  item.DefaultCachingPolicy.CacheableCommands = New List(Of Reference.John.Infrastructure.Cache.CacheCommandDefinition) From {New Reference.John.Infrastructure.Cache.CacheCommandDefinition With {.AbsoluteExpiration = DateTime.MaxValue, .CacheMethodName = New List(Of String) From {"Reference.EIM.Repository.ISimpleRepository.GetItems111"}, .DependentEntities = New List(Of String) From {"SimpleRepository", "Address"}, .EntityName = "SimpleRepository", .SlidingExpiration = TimeSpan.Zero}}
                                                                                                                                                  Return item
                                                                                                                                              End Function))
            container.RegisterType(GetType(Mocks.ISimpleRepository),
                                      GetType(Mocks.SimpleRepository),
                                      New TransientLifetimeManager,
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
            Assert.IsInstanceOfType(cachesettings.DefaultCache, GetType(Reference.John.Infrastructure.Cache.InMemoryCache))
            Assert.AreEqual(0, DirectCast(cachesettings.DefaultCache, Reference.John.Infrastructure.Cache.InMemoryCache).CacheHits)
        End Using
    End Sub


    <TestMethod()> Public Sub ExecuteGetWithCommandMatch()
        Using container As New UnityContainer()
            container.AddNewExtension(Of Interception)()
            container.RegisterType(GetType(Reference.John.Core.Logging.ILogger), GetType(Reference.John.Core.Logging.TrageLogger))
            container.RegisterType(Of Reference.John.Infrastructure.Cache.ICacheProviderConfiguration, Reference.John.Infrastructure.Cache.CacheProviderConfiguration)(New ContainerControlledLifetimeManager,
                                                                                                                         New InjectionFactory(Function(c)
                                                                                                                                                  Dim item As New Reference.John.Infrastructure.Cache.CacheProviderConfiguration With {.IsCachingEnabled = True, .DefaultCachingPolicy = New Reference.John.Infrastructure.Cache.CommandCachingPolicy, .DefaultCache = New Reference.John.Infrastructure.Cache.InMemoryCache}
                                                                                                                                                  item.DefaultCachingPolicy.CacheableCommands = New List(Of Reference.John.Infrastructure.Cache.CacheCommandDefinition) From {New Reference.John.Infrastructure.Cache.CacheCommandDefinition With {.AbsoluteExpiration = DateTime.MaxValue, .CacheMethodName = New List(Of String) From {GetType(Mocks.ISimpleRepository).ToString & ".GetItems"}, .DependentEntities = New List(Of String) From {"SimpleRepository", "Address"}, .EntityName = "SimpleRepository", .SlidingExpiration = TimeSpan.Zero}}
                                                                                                                                                  Return item
                                                                                                                                              End Function))
            container.RegisterType(GetType(Mocks.ISimpleRepository),
                                      GetType(Mocks.SimpleRepository),
                                      New TransientLifetimeManager,
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
            Assert.AreEqual(1, repos.ExecutionCount)
            Dim cachesettings = container.Resolve(Of Reference.John.Infrastructure.Cache.ICacheProviderConfiguration)()
            Assert.IsInstanceOfType(cachesettings.DefaultCache, GetType(Reference.John.Infrastructure.Cache.InMemoryCache))
            Assert.AreEqual(1, DirectCast(cachesettings.DefaultCache, Reference.John.Infrastructure.Cache.InMemoryCache).CacheHits)
        End Using
    End Sub

    <TestMethod()> Public Sub ExecuteGetWithCommandMatchComplexType()
        Using container As New UnityContainer()
            container.AddNewExtension(Of Interception)()
            container.RegisterType(GetType(Reference.John.Core.Logging.ILogger), GetType(Reference.John.Core.Logging.TrageLogger))
            container.RegisterType(Of Reference.John.Infrastructure.Cache.ICacheProviderConfiguration, Reference.John.Infrastructure.Cache.CacheProviderConfiguration)(New ContainerControlledLifetimeManager,
                                                                                                                         New InjectionFactory(Function(c)
                                                                                                                                                  Dim item As New Reference.John.Infrastructure.Cache.CacheProviderConfiguration With {.IsCachingEnabled = True, .DefaultCachingPolicy = New Reference.John.Infrastructure.Cache.CommandCachingPolicy, .DefaultCache = New Reference.John.Infrastructure.Cache.InMemoryCache}
                                                                                                                                                  item.DefaultCachingPolicy.CacheableCommands = New List(Of Reference.John.Infrastructure.Cache.CacheCommandDefinition) From {New Reference.John.Infrastructure.Cache.CacheCommandDefinition With {.AbsoluteExpiration = DateTime.MaxValue, .CacheMethodName = New List(Of String) From {GetType(Mocks.ISimpleRepository).ToString & ".GetItemsComplex"}, .DependentEntities = New List(Of String) From {"SimpleRepository", "Address"}, .EntityName = "SimpleRepository", .SlidingExpiration = TimeSpan.Zero}}
                                                                                                                                                  Return item
                                                                                                                                              End Function))
            container.RegisterType(GetType(Mocks.ISimpleRepository),
                                      GetType(Mocks.SimpleRepository),
                                      New TransientLifetimeManager,
                                      New Interceptor(Of InterfaceInterceptor)(),
                                      New InterceptionBehavior(Of Reference.John.WebASP.Container.CachingInterceptorBehavior)(),
                                      New InterceptionBehavior(Of Reference.John.WebASP.Container.CachingResetInterceptorBehavior)()
                                      )
            Dim repos As Mocks.ISimpleRepository = container.Resolve(Of Mocks.ISimpleRepository)()

            Assert.IsInstanceOfType(repos, GetType(Mocks.ISimpleRepository))
            Dim _list = repos.GetItemsComplex
            Assert.IsNotNull(_list)
            _list = repos.GetItemsComplex
            Assert.IsNotNull(_list)
            Assert.AreEqual(1, repos.ExecutionCount)
            Dim cachesettings = container.Resolve(Of Reference.John.Infrastructure.Cache.ICacheProviderConfiguration)()
            Assert.IsInstanceOfType(cachesettings.DefaultCache, GetType(Reference.John.Infrastructure.Cache.InMemoryCache))
            Assert.AreEqual(1, DirectCast(cachesettings.DefaultCache, Reference.John.Infrastructure.Cache.InMemoryCache).CacheHits)
        End Using
    End Sub

    <TestMethod()> Public Sub ExecuteGetWithCommandMatchLargeResult()
        Using container As New UnityContainer()
            container.AddNewExtension(Of Interception)()
            container.RegisterType(GetType(Reference.John.Core.Logging.ILogger), GetType(Reference.John.Core.Logging.TrageLogger))
            container.RegisterType(Of Reference.John.Infrastructure.Cache.ICacheProviderConfiguration, Reference.John.Infrastructure.Cache.CacheProviderConfiguration)(New ContainerControlledLifetimeManager,
                                                                                                                         New InjectionFactory(Function(c)
                                                                                                                                                  Dim item As New Reference.John.Infrastructure.Cache.CacheProviderConfiguration With {.IsCachingEnabled = True, .DefaultCachingPolicy = New Reference.John.Infrastructure.Cache.CommandCachingPolicy, .DefaultCache = New Reference.John.Infrastructure.Cache.InMemoryCache}
                                                                                                                                                  item.DefaultCachingPolicy.CacheableCommands = New List(Of Reference.John.Infrastructure.Cache.CacheCommandDefinition) From {New Reference.John.Infrastructure.Cache.CacheCommandDefinition With {.AbsoluteExpiration = DateTime.MaxValue, .CacheMethodName = New List(Of String) From {GetType(Mocks.ISimpleRepository).ToString & ".GetItemsLargeResult"}, .DependentEntities = New List(Of String) From {"SimpleRepository", "Address"}, .EntityName = "SimpleRepository", .SlidingExpiration = TimeSpan.Zero}}
                                                                                                                                                  Return item
                                                                                                                                              End Function))
            container.RegisterType(GetType(Mocks.ISimpleRepository),
                                      GetType(Mocks.SimpleRepository),
                                      New TransientLifetimeManager,
                                      New Interceptor(Of InterfaceInterceptor)(),
                                      New InterceptionBehavior(Of Reference.John.WebASP.Container.CachingInterceptorBehavior)(),
                                      New InterceptionBehavior(Of Reference.John.WebASP.Container.CachingResetInterceptorBehavior)()
                                      )
            Dim repos As Mocks.ISimpleRepository = container.Resolve(Of Mocks.ISimpleRepository)()

            Assert.IsInstanceOfType(repos, GetType(Mocks.ISimpleRepository))
            Dim _list = repos.GetItemsLargeResult
            Assert.IsNotNull(_list)
            _list = repos.GetItemsLargeResult
            Assert.IsNotNull(_list)
            Assert.AreEqual(2, repos.ExecutionCount)
            Dim cachesettings = container.Resolve(Of Reference.John.Infrastructure.Cache.ICacheProviderConfiguration)()
            Assert.IsInstanceOfType(cachesettings.DefaultCache, GetType(Reference.John.Infrastructure.Cache.InMemoryCache))
            Assert.AreEqual(0, DirectCast(cachesettings.DefaultCache, Reference.John.Infrastructure.Cache.InMemoryCache).CacheHits)
        End Using
    End Sub

    <TestMethod()> Public Sub ExecuteGetWithSimpleParameter()
        Using container As New UnityContainer()
            container.AddNewExtension(Of Interception)()
            container.RegisterType(GetType(Reference.John.Core.Logging.ILogger), GetType(Reference.John.Core.Logging.TrageLogger))
            container.RegisterType(Of Reference.John.Infrastructure.Cache.ICacheProviderConfiguration, Reference.John.Infrastructure.Cache.CacheProviderConfiguration)(New ContainerControlledLifetimeManager,
                                                                                                                         New InjectionFactory(Function(c)
                                                                                                                                                  Dim item As New Reference.John.Infrastructure.Cache.CacheProviderConfiguration With {.IsCachingEnabled = True, .DefaultCachingPolicy = New Reference.John.Infrastructure.Cache.CommandCachingPolicy, .DefaultCache = New Reference.John.Infrastructure.Cache.InMemoryCache}
                                                                                                                                                  item.DefaultCachingPolicy.CacheableCommands = New List(Of Reference.John.Infrastructure.Cache.CacheCommandDefinition) From {New Reference.John.Infrastructure.Cache.CacheCommandDefinition With {.AbsoluteExpiration = DateTime.MaxValue, .CacheMethodName = New List(Of String) From {GetType(Mocks.ISimpleRepository).ToString & ".GetItemsById"}, .DependentEntities = New List(Of String) From {"SimpleRepository", "Address"}, .EntityName = "SimpleRepository", .SlidingExpiration = TimeSpan.Zero}}
                                                                                                                                                  Return item
                                                                                                                                              End Function))
            container.RegisterType(GetType(Mocks.ISimpleRepository),
                                      GetType(Mocks.SimpleRepository),
                                      New TransientLifetimeManager,
                                      New Interceptor(Of InterfaceInterceptor)(),
                                      New InterceptionBehavior(Of Reference.John.WebASP.Container.CachingInterceptorBehavior)(),
                                      New InterceptionBehavior(Of Reference.John.WebASP.Container.CachingResetInterceptorBehavior)()
                                      )
            Dim repos As Mocks.ISimpleRepository = container.Resolve(Of Mocks.ISimpleRepository)()

            Assert.IsInstanceOfType(repos, GetType(Mocks.ISimpleRepository))
            Dim _getParameter As Integer = -100
            Dim _list = repos.GetItemsById(_getParameter)
            Assert.IsNotNull(_list)
            Assert.AreEqual(_getParameter, _list(0).Id)
            _getParameter = -300
            _list = repos.GetItemsById(_getParameter)
            Assert.IsNotNull(_list)
            Assert.AreEqual(_getParameter, _list(0).FormSimpleZeroId)
            Assert.AreEqual(2, repos.ExecutionCount)
            Dim cachesettings = container.Resolve(Of Reference.John.Infrastructure.Cache.ICacheProviderConfiguration)()
            Assert.IsInstanceOfType(cachesettings.DefaultCache, GetType(Reference.John.Infrastructure.Cache.InMemoryCache))
            Assert.AreEqual(0, DirectCast(cachesettings.DefaultCache, Reference.John.Infrastructure.Cache.InMemoryCache).CacheHits)
        End Using
    End Sub

    <TestMethod()> Public Sub ExecuteGetWithComplexParameter()
        Using container As New UnityContainer()
            container.AddNewExtension(Of Interception)()
            container.RegisterType(GetType(Reference.John.Core.Logging.ILogger), GetType(Reference.John.Core.Logging.TrageLogger))
            container.RegisterType(Of Reference.John.Infrastructure.Cache.ICacheProviderConfiguration, Reference.John.Infrastructure.Cache.CacheProviderConfiguration)(New ContainerControlledLifetimeManager,
                                                                                                                         New InjectionFactory(Function(c)
                                                                                                                                                  Dim item As New Reference.John.Infrastructure.Cache.CacheProviderConfiguration With {.IsCachingEnabled = True, .DefaultCachingPolicy = New Reference.John.Infrastructure.Cache.CommandCachingPolicy, .DefaultCache = New Reference.John.Infrastructure.Cache.InMemoryCache}
                                                                                                                                                  item.DefaultCachingPolicy.CacheableCommands = New List(Of Reference.John.Infrastructure.Cache.CacheCommandDefinition) From {New Reference.John.Infrastructure.Cache.CacheCommandDefinition With {.AbsoluteExpiration = DateTime.MaxValue, .CacheMethodName = New List(Of String) From {GetType(Mocks.ISimpleRepository).ToString & ".GetItemsByObject"}, .DependentEntities = New List(Of String) From {"SimpleRepository", "Address"}, .EntityName = "SimpleRepository", .SlidingExpiration = TimeSpan.Zero}}
                                                                                                                                                  Return item
                                                                                                                                              End Function))
            container.RegisterType(GetType(Mocks.ISimpleRepository),
                                      GetType(Mocks.SimpleRepository),
                                      New TransientLifetimeManager,
                                      New Interceptor(Of InterfaceInterceptor)(),
                                      New InterceptionBehavior(Of Reference.John.WebASP.Container.CachingInterceptorBehavior)(),
                                      New InterceptionBehavior(Of Reference.John.WebASP.Container.CachingResetInterceptorBehavior)()
                                      )
            Dim repos As Mocks.ISimpleRepository = container.Resolve(Of Mocks.ISimpleRepository)()

            Assert.IsInstanceOfType(repos, GetType(Mocks.ISimpleRepository))
            Dim _getParameter As New Reference.John.Domain.FormSimpleZero With {.Id = -200}
            Dim _list = repos.GetItemsByObject(_getParameter)
            Assert.IsNotNull(_list)
            Assert.AreEqual(_getParameter.Id, _list(0).FormSimpleZeroId)
            _getParameter = New Reference.John.Domain.FormSimpleZero With {.Id = -300}
            _list = repos.GetItemsByObject(_getParameter)
            Assert.IsNotNull(_list)
            Assert.AreEqual(_getParameter.Id, _list(0).FormSimpleZeroId)
            Assert.AreEqual(2, repos.ExecutionCount)
            Dim cachesettings = container.Resolve(Of Reference.John.Infrastructure.Cache.ICacheProviderConfiguration)()
            Assert.IsInstanceOfType(cachesettings.DefaultCache, GetType(Reference.John.Infrastructure.Cache.InMemoryCache))
            Assert.AreEqual(0, DirectCast(cachesettings.DefaultCache, Reference.John.Infrastructure.Cache.InMemoryCache).CacheHits)
        End Using
    End Sub

    <TestMethod()> Public Sub ExecuteGetWithComplexParameterAndResetCache()
        Using container As New UnityContainer()
            container.AddNewExtension(Of Interception)()
            container.RegisterType(GetType(Reference.John.Core.Logging.ILogger), GetType(Reference.John.Core.Logging.TrageLogger))
            container.RegisterType(Of Reference.John.Infrastructure.Cache.ICacheProviderConfiguration, Reference.John.Infrastructure.Cache.CacheProviderConfiguration)(New ContainerControlledLifetimeManager,
                                                                                                                         New InjectionFactory(Function(c)
                                                                                                                                                  Dim item As New Reference.John.Infrastructure.Cache.CacheProviderConfiguration With {.IsCachingEnabled = True, .DefaultCachingPolicy = New Reference.John.Infrastructure.Cache.CommandCachingPolicy, .DefaultCache = New Reference.John.Infrastructure.Cache.InMemoryCache}
                                                                                                                                                  item.DefaultCachingPolicy.CacheableCommands = New List(Of Reference.John.Infrastructure.Cache.CacheCommandDefinition) From {New Reference.John.Infrastructure.Cache.CacheCommandDefinition With {.AbsoluteExpiration = DateTime.MaxValue, .CacheMethodName = New List(Of String) From {GetType(Mocks.ISimpleRepository).ToString & ".GetItemsByObject"}, .CacheResetMethodName = New List(Of String) From {GetType(Mocks.ISimpleRepository).ToString & ".UpdateItem"},
                                                                                                                                                                                                                                                                                                     .DependentEntities = New List(Of String) From {"SimpleRepository", "Address"}, .EntityName = "SimpleRepository", .SlidingExpiration = TimeSpan.Zero}}
                                                                                                                                                  Return item
                                                                                                                                              End Function))
            container.RegisterType(GetType(Mocks.ISimpleRepository),
                                      GetType(Mocks.SimpleRepository),
                                      New TransientLifetimeManager,
                                      New Interceptor(Of InterfaceInterceptor)(),
                                      New InterceptionBehavior(Of Reference.John.WebASP.Container.CachingInterceptorBehavior)(),
                                      New InterceptionBehavior(Of Reference.John.WebASP.Container.CachingResetInterceptorBehavior)()
                                      )
            Dim repos As Mocks.ISimpleRepository = container.Resolve(Of Mocks.ISimpleRepository)()

            Assert.IsInstanceOfType(repos, GetType(Mocks.ISimpleRepository))
            Dim _getParameter As New Reference.John.Domain.FormSimpleZero With {.Id = -200}
            Dim _list = repos.GetItemsByObject(_getParameter)
            Assert.IsNotNull(_list)
            Assert.AreEqual(_getParameter.Id, _list(0).FormSimpleZeroId)
            _getParameter = New Reference.John.Domain.FormSimpleZero With {.Id = -300}
            _list = repos.GetItemsByObject(_getParameter)
            Assert.IsNotNull(_list)
            Assert.AreEqual(_getParameter.Id, _list(0).FormSimpleZeroId)
            Assert.AreEqual(2, repos.ExecutionCount)
            Dim cachesettings = container.Resolve(Of Reference.John.Infrastructure.Cache.ICacheProviderConfiguration)()
            Assert.IsInstanceOfType(cachesettings.DefaultCache, GetType(Reference.John.Infrastructure.Cache.InMemoryCache))
            Assert.AreEqual(0, DirectCast(cachesettings.DefaultCache, Reference.John.Infrastructure.Cache.InMemoryCache).CacheHits)
            'call an update that is configured to reset the cache.
            Assert.AreEqual(0, DirectCast(cachesettings.DefaultCache, Reference.John.Infrastructure.Cache.InMemoryCache).CacheItemInvalidations)
            repos.UpdateItem(Nothing)
            Assert.AreEqual(2, DirectCast(cachesettings.DefaultCache, Reference.John.Infrastructure.Cache.InMemoryCache).CacheItemInvalidations)
        End Using
    End Sub


    <TestMethod()> Public Sub CommandDefinitionFactoryNullCheck()
        Assert.IsNotNull(Reference.John.Infrastructure.Cache.CacheProviderConfigurationFactory.LoadCommandDefinitions)
    End Sub

    <TestMethod()> Public Sub CommandDefinitionFactoryCreation()
        Dim item = Reference.John.Infrastructure.Cache.CacheProviderConfigurationFactory.Create
        Assert.AreEqual(ConfigurationManager.AppSettings(Reference.John.Resources.Constants.CacheProviderConfigurationIsCachingEnabled), item.IsCachingEnabled.ToString)
        Assert.AreEqual(ConfigurationManager.AppSettings(Reference.John.Resources.Constants.CacheProviderConfigurationDefaultCache), item.DefaultCache.GetType.ToString)
        Assert.AreEqual(ConfigurationManager.AppSettings(Reference.John.Resources.Constants.CacheProviderConfigurationDefaultCachingPolicy), item.DefaultCachingPolicy.GetType.ToString)
    End Sub

    <TestMethod()> Public Sub LoadCommandDefintionFromFile()
        Dim file As String = IO.Path.GetTempFileName
        Dim item As New List(Of Reference.John.Infrastructure.Cache.CacheCommandDefinition) From
            {New Reference.John.Infrastructure.Cache.CacheCommandDefinition With
             {.AbsoluteExpiration = DateTime.MaxValue,
              .CacheMethodName = New List(Of String) From {"Reference.John.Fixture.Mocks.ISimpleRepository.GetItemsByObject"},
              .CacheResetMethodName = New List(Of String) From {"Reference.John.Fixture.Mocks.ISimpleRepository.UpdateItem"},
                .DependentEntities = New List(Of String) From {"SimpleRepository", "Address"}, .EntityName = "SimpleRepository", .SlidingExpiration = New TimeSpan(0, 10, 0)}}
        Dim value As String = Newtonsoft.Json.JsonConvert.SerializeObject(item)
        IO.File.WriteAllText(file, value)
        Dim factoryitem As IEnumerable(Of Reference.John.Infrastructure.Cache.CacheCommandDefinition) = Reference.John.Infrastructure.Cache.CacheProviderConfigurationFactory.LoadCommandDefinitions(file)
        Assert.AreEqual(1, factoryitem.Count)
        IO.File.Delete(file)
    End Sub

End Class
