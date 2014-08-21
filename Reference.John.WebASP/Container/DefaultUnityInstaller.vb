Imports Microsoft.Practices.Unity
Imports Microsoft.Practices.Unity.InterceptionExtension
Imports Reference.John.Infrastructure
Imports Reference.John.Services

Namespace Container

    ''' <summary>
    ''' The class is used to bootstrap the container with defined components.
    ''' </summary>
    ''' <remarks></remarks>
    Public Class DefaultUnityInstaller
        Inherits UnityContainerExtension

        ''' <summary>
        ''' This is the method that builds the components in the container.
        ''' </summary>
        ''' <remarks></remarks>
        Protected Overrides Sub Initialize()
            'controller types do not need to be registered use extension method from Unity.MVC4
            'Me.Container.RegisterTypes(AllClasses.FromLoadedAssemblies().Where(Function(x) Not x.IsInterface AndAlso
            '                                                           (GetType(IController).IsAssignableFrom(x))),
            '                        Function(x) WithMappings.FromAllInterfacesInSameAssembly(x),
            '                        Function(x) x.FullName,
            '                        Function(x) WithLifetime.Transient(x),
            '                        getInjectionMembers:=Function(x) New InjectionMember() {New Interceptor(Of InterfaceInterceptor)(), New InterceptionBehavior(Of LoggingInterceptorBehavior)()}
            '                        )
            'simple registry default transient lifetime hierarchy.  it will be created on every resolve and the container client would be responsible for disposal.
            'Me.Container.RegisterType(GetType(Reference.John.,Core.Logging.ILogger), GetType(Reference.John.,Core.Logging.TrageLogger))
            'using the custom MVC perrequestlifetime,  instance is disposed at request end.
            'Me.Container.RegisterType(GetType(Reference.John.,Core.Logging.ILogger), GetType(Reference.John.,Core.Logging.TrageLogger), New PerRequestLifetimeManager)
            Me.Container.RegisterType(GetType(Reference.John.Infrastructure.Logging.ILogger), GetType(Reference.John.Infrastructure.Logging.TrageLogger))
            'simple registration attached to an interceptor
            Me.Container.RegisterType(GetType(Reference.John.Services.IValidationService), GetType(Reference.John.Services.ValidationSesrvice), New HierarchicalLifetimeManager, New Interceptor(Of InterfaceInterceptor)(), New InterceptionBehavior(Of Reference.John.Infrastructure.Container.LoggingInterceptorBehavior)())
            Me.Container.RegisterType(GetType(Reference.John.Services.IWorkFlowService), GetType(Reference.John.Services.WorkFlowService), New HierarchicalLifetimeManager, New Interceptor(Of InterfaceInterceptor)(), New InterceptionBehavior(Of Reference.John.Infrastructure.Container.LoggingInterceptorBehavior)())
            Me.Container.RegisterType(GetType(Reference.John.Services.IAlertService), GetType(Reference.John.Services.AlertSesrvice), New HierarchicalLifetimeManager, New Interceptor(Of InterfaceInterceptor)(), New InterceptionBehavior(Of Reference.John.Infrastructure.Container.LoggingInterceptorBehavior)())
            'registering the connection string instance that would be passed to any repository
            'Me.Container.RegisterInstance(Of String)("connectionStringName", Constants.ConnectionStringKey)
            'factory should only be called the first time and then the instance should be returned all times thereafter.
            Container.RegisterType(Of Reference.John.Infrastructure.Cache.ICacheProviderConfiguration, Reference.John.Infrastructure.Cache.CacheProviderConfiguration)(New ContainerControlledLifetimeManager,
                                                                                                                         New InjectionFactory(Function(c) Reference.John.Infrastructure.Cache.CacheProviderConfigurationFactory.Create))
            'single registration of a repository
            'Me.Container.RegisterType(GetType(Reference.John.,Repository.Infrastructure.Data.IContactRepository),
            '                          GetType(Reference.John.,Repository.Infrastructure.Data.ContactRepository),
            '                          New HierarchicalLifetimeManager,
            '                          New Interceptor(Of InterfaceInterceptor)(),
            '                          New InterceptionBehavior(Of LoggingInterceptorBehavior)(),
            '                          New InjectionConstructor(Reference.John.,Resources.Constants.ConnectionStringKey))
            'using the registertypes method for the repository class to get all repository classes and register them to the interfaces
            'Me.Container.RegisterTypes(AllClasses.FromLoadedAssemblies().Where(Function(x) Not x.IsInterface AndAlso
            '                                                           (GetType(Reference.John.,Repository.Infrastructure.Data.IRepository).IsAssignableFrom(x))),
            '                        Function(x) WithMappings.FromAllInterfacesInSameAssembly(x),
            '                        Function(x) x.FullName,
            '                        Function(x) WithLifetime.Hierarchical(x),
            '                        getInjectionMembers:=Function(x) New InjectionMember() {New Interceptor(Of InterfaceInterceptor)(), New InterceptionBehavior(Of LoggingInterceptorBehavior)()}
            '                        )
            'looping through all assemblies wehre IRepository is implemented.  Expect that all repositories have a cooresponding unique interface.
            'the repository assembly may not be loaded at this point so force a load with the registration of unit of work
            Me.Container.RegisterType(GetType(Reference.John.Repository.Infrastructure.IUnitOfWork), GetType(Reference.John.Repository.Infrastructure.UnitOfWork))
            AllClasses.FromLoadedAssemblies().Where(Function(x) Not x.IsInterface AndAlso GetType(Reference.John.Repository.IRepository).IsAssignableFrom(x)).
                ToList.ForEach(Sub(y)
                                   'Dim t As Type = GetInterfaceType(y)
                                   'Me.Container.RegisterType(GetInterfaceType(y), y, y.FullName)
                                   Me.Container.RegisterType(GetInterfaceType(y),
                                                             y,
                                                             New HierarchicalLifetimeManager,
                                                             New Interceptor(Of InterfaceInterceptor)(),
                                                             New InterceptionBehavior(Of Reference.John.Infrastructure.Container.LoggingInterceptorBehavior)(),
                                                            New InterceptionBehavior(Of Reference.John.Infrastructure.Container.CachingInterceptorBehavior)(),
                                                            New InterceptionBehavior(Of Reference.John.Infrastructure.Container.CachingResetInterceptorBehavior)(),
                                                             New InjectionConstructor(Reference.John.Resources.Constants.ConnectionStringKey))
                               End Sub)
            'session registration
            'Me.Container.RegisterType(GetType(REFJohn.ISessionExtendedRepository), GetType(REFJohn.,SessionExtendedRepository), New PerRequestLifetimeManager)
            'Me.Container.RegisterType(GetType(REFJohn.ISessionRepository), GetType(REFJohn.,SessionRepository), New PerRequestLifetimeManager)
            'Me.Container.RegisterType(GetType(REFJohn.ISessionProvider), GetType(REFJohn.,SessionProvider), New PerRequestLifetimeManager,
            'New InjectionConstructor(GetType(Reference.John.,Core.Logging.ILogger),
            '                         GetType(REFJohn.,ISessionRepository),
            '                         GetType(REFJohn.,ISessionExtendedRepository),
            '                           "injected Userid"))
            'register background tasks
            AllClasses.FromLoadedAssemblies().Where(Function(x) Not x.IsInterface AndAlso GetType(Reference.John.Infrastructure.Tasks.IBackgroundTask).IsAssignableFrom(x)).
               ToList.ForEach(Sub(y)
                                  Me.Container.RegisterType(GetInterfaceType(y), y, y.UnderlyingSystemType.FullName, New HierarchicalLifetimeManager)
                              End Sub)
        End Sub

        Private Function GetInterfaceType(type As Type) As Type
            Dim interfaces As Type() = type.GetInterfaces()
            Dim interfaceType As Type = interfaces.FirstOrDefault()

            If type.BaseType Is Nothing Then Return interfaceType

            For Each _interface As Type In interfaces
                If Not type.BaseType.ImplementsInterface(_interface) Then
                    Return _interface
                End If
            Next

            Return interfaceType
        End Function


    End Class

End Namespace
