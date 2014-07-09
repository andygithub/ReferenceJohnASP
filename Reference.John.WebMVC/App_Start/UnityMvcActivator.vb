Imports System.Linq
Imports System.Web.Mvc


<Assembly: WebActivatorEx.PreApplicationStartMethod(GetType(Reference.John.WebMVC.UnityWebActivator), "Start")> 

Namespace Reference.John.WebMVC
    ''' <summary>Provides the bootstrapping for integrating Unity with ASP.NET MVC.</summary>
    Public NotInheritable Class UnityWebActivator
        Private Sub New()
        End Sub
        ''' <summary>Integrates Unity when the application starts.</summary>
        Public Shared Sub Start()
            Dim _container = Container.ContainerFactory.GetConfiguredContainer()

            FilterProviders.Providers.Remove(FilterProviders.Providers.OfType(Of FilterAttributeFilterProvider)().First())
            FilterProviders.Providers.Add(New Microsoft.Practices.Unity.Mvc.UnityFilterAttributeFilterProvider(_container))

            DependencyResolver.SetResolver(New Unity.Mvc5.UnityDependencyResolver(_container))

            ' TODO: Uncomment if you want to use PerRequestLifetimeManager
            Microsoft.Web.Infrastructure.DynamicModuleHelper.DynamicModuleUtility.RegisterModule(GetType(Microsoft.Practices.Unity.Mvc.UnityPerRequestHttpModule))
        End Sub
    End Class
End Namespace