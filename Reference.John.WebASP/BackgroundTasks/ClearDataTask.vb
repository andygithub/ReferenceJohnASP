Namespace BackgroundTaks

    Public Class ClearDataTask
        Implements Reference.John.Infrastructure.Tasks.IBackgroundTask

        Private _logger As Reference.John.Infrastructure.Logging.ILogger

        Public Sub New(logger As Reference.John.Infrastructure.Logging.ILogger)
            If logger Is Nothing Then Throw New ArgumentNullException("logger")
            _logger = logger
        End Sub

        Public Sub Execute() Implements Infrastructure.Tasks.IBackgroundTask.Execute
            _logger.Trace("Clearing Data")
            Threading.Thread.Sleep(10000)
            _logger.Trace("Cleared Data")
        End Sub

    End Class

End Namespace