Namespace Cache
    ''' <summary>
    ''' Factory class that will handle instantiating the ICacheProviderConfiguration contract.
    ''' </summary>
    ''' <remarks></remarks>
    Public NotInheritable Class CacheProviderConfigurationFactory

        ''' <summary>
        ''' Factory to initialize the cache provider configuration.  The factory is setup to use configuation file values but if the configuration is not present then defaults will be defined.
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function Create() As ICacheProviderConfiguration
            'set defaults for configuration
            Dim item As New CacheProviderConfiguration With {.IsCachingEnabled = True, .DefaultCachingPolicy = New Cache.CommandCachingPolicy, .DefaultCache = New Cache.InMemoryCache}
            'attempt to load items from the configuration file
            Dim value As String = Configuration.ConfigurationManager.AppSettings(Reference.John.Resources.Constants.CacheProviderConfigurationIsCachingEnabled)
            If Not String.IsNullOrWhiteSpace(value) Then item.IsCachingEnabled = value
            value = Configuration.ConfigurationManager.AppSettings(Reference.John.Resources.Constants.CacheProviderConfigurationDefaultCache)
            If Not String.IsNullOrWhiteSpace(value) Then item.DefaultCache = Activator.CreateInstance(Type.GetType(value))
            value = Configuration.ConfigurationManager.AppSettings(Reference.John.Resources.Constants.CacheProviderConfigurationDefaultCachingPolicy)
            If Not String.IsNullOrWhiteSpace(value) Then item.DefaultCachingPolicy = Activator.CreateInstance(Type.GetType(value))
            item.DefaultCachingPolicy.CacheableCommands = CacheProviderConfigurationFactory.LoadCommandDefinitions(Configuration.ConfigurationManager.AppSettings(Reference.John.Resources.Constants.CacheProviderConfigurationDefaultCachingPolicyCacheableCommands))
            Return item
        End Function

        ''' <summary>
        ''' Static function that will deserialize all of the command definitions from the specified file.  
        ''' </summary>
        ''' <param name="file"></param>
        ''' <returns></returns>
        ''' <remarks>If the file is null or doesn't exist than the default command defintion will be used.  No exception is thrown in that instance.</remarks>
        Public Shared Function LoadCommandDefinitions(file As String) As ICollection(Of CacheCommandDefinition)
            If file Is Nothing Then Return CacheProviderConfigurationFactory.LoadCommandDefinitions
            'if file exists then load it and serialize it and return it
            Dim _item As String = IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, file)
            If IO.File.Exists(file) Then Return Newtonsoft.Json.JsonConvert.DeserializeObject(Of ICollection(Of CacheCommandDefinition))(IO.File.ReadAllText(file))
            file = IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, file)
            If IO.File.Exists(file) Then Return Newtonsoft.Json.JsonConvert.DeserializeObject(Of ICollection(Of CacheCommandDefinition))(IO.File.ReadAllText(file))
            Return CacheProviderConfigurationFactory.LoadCommandDefinitions
        End Function

        ''' <summary>
        ''' Static function that will load all of the local embedded command definitions.  This is the default set that is used when an external file is not specified in the configuration file.
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks>This is the default set that is used when an external file is not specified in the configuration file.</remarks>
        Public Shared Function LoadCommandDefinitions() As ICollection(Of CacheCommandDefinition)
            'embedded hard coded list
            Return New List(Of CacheCommandDefinition) From {New CacheCommandDefinition With {.AbsoluteExpiration = DateTime.MaxValue,
                                                                                              .CacheMethodName = New List(Of String) From {"System.Collections.Generic.IEnumerable`1[Reference.John.Domain.Address] GetItems()"},
                                                    .DependentEntities = New List(Of String) From {"SimpleRepository", "Address"}, .EntityName = "SimpleRepository",
                                                     .SlidingExpiration = New TimeSpan(0, 10, 0)}}
        End Function

    End Class

End Namespace