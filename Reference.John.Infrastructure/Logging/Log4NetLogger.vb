Namespace Logging
    ''' <summary>
    ''' An implementation of <see cref="ILogger"/> for a specific logging library.
    ''' </summary>
    Public Class Log4NetLogger
        Inherits LoggerBase
        ''' <summary>
        ''' Initializes a new instance of the log4net logging class.
        ''' </summary>
        ''' <param name="logger">The logger.</param>
        Public Sub New(logger As log4net.ILog)
            _logger = logger
        End Sub

        Private _logger As log4net.ILog

        ''' <summary>
        ''' Log message at Trace level.
        ''' </summary>
        ''' <param name="message">The message.</param>
        Public Overrides Sub Trace(message As String)
            _logger.Debug(message)
        End Sub

        ''' <summary>
        ''' Log message at Debug level.
        ''' </summary>
        ''' <param name="message">The message.</param>
        Public Overrides Sub Debug(message As String)
            _logger.Debug(message)
        End Sub

        ''' <summary>
        ''' Log message at Info level.
        ''' </summary>
        ''' <param name="message">The message.</param>
        Public Overrides Sub Info(message As String)
            _logger.Info(message)
        End Sub

        ''' <summary>
        ''' Log message at Warn level.
        ''' </summary>
        ''' <param name="message">The message.</param>
        Public Overrides Sub Warn(message As String)
            _logger.Warn(message)
        End Sub

        ''' <summary>
        ''' Log message at Error level.
        ''' </summary>
        ''' <param name="message">The message.</param>
        Public Overrides Sub [Error](message As String)
            _logger.[Error](message)
        End Sub

        ''' <summary>
        ''' Log message at Fatal level.
        ''' </summary>
        ''' <param name="message">The message.</param>
        Public Overrides Sub Fatal(message As String)
            _logger.Fatal(message)
        End Sub

        ''' <summary>
        ''' Log message at Trace level.
        ''' </summary>
        ''' <param name="message">The message.</param>
        ''' <param name="exception">The exception.</param>
        Public Overrides Sub Trace(message As String, exception As Exception)
            _logger.Debug(message, exception)
        End Sub

        ''' <summary>
        ''' Log message at Debug level.
        ''' </summary>
        ''' <param name="message">The message.</param>
        ''' <param name="exception">The exception.</param>
        Public Overrides Sub Debug(message As String, exception As Exception)
            _logger.Debug(message, exception)
        End Sub

        ''' <summary>
        ''' Log message at Info level.
        ''' </summary>
        ''' <param name="message">The message.</param>
        ''' <param name="exception">The exception.</param>
        Public Overrides Sub Info(message As String, exception As Exception)
            _logger.Info(message, exception)
        End Sub

        ''' <summary>
        ''' Log message at Warn level.
        ''' </summary>
        ''' <param name="message">The message.</param>
        ''' <param name="exception">The exception.</param>
        Public Overrides Sub Warn(message As String, exception As Exception)
            _logger.Warn(message, exception)
        End Sub

        ''' <summary>
        ''' Log message at Error level.
        ''' </summary>
        ''' <param name="message">The message.</param>
        ''' <param name="exception">The exception.</param>
        Public Overrides Sub [Error](message As String, exception As Exception)
            _logger.Error(message, exception)
        End Sub

        ''' <summary>
        ''' Log message at Fatal level.
        ''' </summary>
        ''' <param name="message">The message.</param>
        ''' <param name="exception">The exception.</param>
        Public Overrides Sub Fatal(message As String, exception As Exception)
            _logger.Fatal(message, exception)
        End Sub
    End Class

End Namespace