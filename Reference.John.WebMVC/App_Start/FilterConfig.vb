Imports System.Web
Imports System.Web.Mvc

Public Module FilterConfig
    Public Sub RegisterGlobalFilters(ByVal filters As GlobalFilterCollection)
        filters.Add(New HandleErrorAttribute())
        'this would be for some nice error handling. application_error would still be handled for logging scenarios. there may not be enough context at the app error event.
        'this was removed for the generic handler configruation and controller that was put in place
        filters.Add(New HandleErrorAttribute())
        'miniprofiler hookup for action profiling
        'filters.Add(New StackExchange.Profiling.MVCHelpers.ProfilingActionFilter())
        'filters.Add(New Filters.ProfilingActionFilter)
        'logging filter would be here if deemmed useful
        'filters.Add(New Filters.ConsolerLoggingFilter(Container.ContainerFactory.GetConfiguredContainer.Resolve(Of Reference.EIM.Core.Logging.ILogger)))
        'security filter here would check to see if user had access to page.
        'filters.Add()
        '
    End Sub
End Module