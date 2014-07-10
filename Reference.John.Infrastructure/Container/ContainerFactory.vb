Imports Microsoft.Practices.Unity
Imports Microsoft.Practices.Unity.Configuration
Imports Microsoft.Practices.Unity.InterceptionExtension

Namespace Container
    ''' <summary>
    ''' Specifies the Unity configuration for the main container.
    ''' </summary>
    Public Class ContainerFactory

        Private Shared container As New Lazy(Of IUnityContainer)(Function()
                                                                     Dim container = New UnityContainer()
                                                                     container.AddNewExtension(Of Interception)()
                                                                     Dim value = System.Configuration.ConfigurationManager.AppSettings(Reference.John.Resources.Constants.ContainerInstallerExtension)
                                                                     If Not String.IsNullOrWhiteSpace(value) Then container.AddExtension(Activator.CreateInstance(Type.GetType(value)))
                                                                     'container.AddNewExtension(of DefaultUnityInstaller)()
                                                                     'use configured installer
                                                                     'RegisterTypes(container)
                                                                     Return container

                                                                 End Function)

        ''' <summary>
        ''' Gets the configured Unity container.
        ''' </summary>
        Public Shared Function GetConfiguredContainer() As IUnityContainer
            Return container.Value
        End Function

        ''' <summary>Registers the type mappings with the Unity container.</summary>
        ''' <param name="container">The unity container to configure.</param>
        ''' <remarks>There is no need to register concrete types such as controllers or API controllers (unless you want to 
        ''' change the defaults), as Unity allows resolving a concrete type even if it was not previously registered.</remarks>
        Public Shared Sub RegisterTypes(container As IUnityContainer)
            ' NOTE: To load from web.config uncomment the line below. Make sure to add a Microsoft.Practices.Unity.Configuration to the using statements.
            ' container.LoadConfiguration();

            ' TODO: Register your types here
            ' container.RegisterType<IProductRepository, ProductRepository>();
            'simple registry default transient lifetime hierarchy.  it will be created on every resolve and the container client would be responsible for disposal.
            'container.RegisterType(GetType(REFServices2013.ILoggingFacade), GetType(REFServices2013.LoggingFacade))
        End Sub

    End Class
End Namespace