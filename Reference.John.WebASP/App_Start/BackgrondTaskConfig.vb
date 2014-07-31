Public Class BackgrondTaskConfig

    Private _logger As Reference.John.Infrastructure.Logging.ILogger

    Public Sub New(logger As Reference.John.Infrastructure.Logging.ILogger)
        If logger Is Nothing Then Throw New ArgumentNullException("logger")
        _logger = logger
    End Sub

    Public Sub RunBackgroundTasks(tasks As IEnumerable(Of Reference.John.Infrastructure.Tasks.IBackgroundTask))
        If tasks Is Nothing OrElse tasks.Count = 0 Then Throw New ArgumentNullException("tasks")
        _logger.Trace(Reference.John.Resources.Resources.LogMessages.LaunchingBackgroundTasks)
        'these tasks are expected to be fire and forget.
        'rather than have internal methods being fired reosolve unity to a backgroud task and execute.
        'can't throw the list of methods on the pool so loop instead
        For Each _item In tasks
            _logger.Trace(Reference.John.Resources.Resources.LogMessages.ExecutingTask, _item.GetType.FullName)
            Threading.ThreadPool.QueueUserWorkItem(AddressOf _item.Execute)
        Next
        _logger.Trace(Reference.John.Resources.Resources.LogMessages.LaunchedBackgroundTasks)
    End Sub

End Class
