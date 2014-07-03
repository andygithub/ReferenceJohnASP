Namespace Logging
    ''' <summary>
    ''' An implementation of <see cref="ILogger"/> for a specific logging library.
    ''' </summary>
    Public Class ELLogger
        Inherits LoggerBase
        ''' <summary>
        ''' Initializes a new instance of the enterprise library logging class.
        ''' </summary>
        ''' <param name="logger">The logger.</param>
        Public Sub New(logger As Object)
            _logger = logger
        End Sub

        Private _logger() As Object

        ''' <summary>
        ''' Log message at Trace level.
        ''' </summary>
        ''' <param name="message">The message.</param>
        Public Overrides Sub Trace(message As String)
            Throw New NotImplementedException
            'Logger.Trace(message)
        End Sub

        ''' <summary>
        ''' Log message at Debug level.
        ''' </summary>
        ''' <param name="message">The message.</param>
        Public Overrides Sub Debug(message As String)
            Throw New NotImplementedException
            'Logger.Debug(message)
        End Sub

        ''' <summary>
        ''' Log message at Info level.
        ''' </summary>
        ''' <param name="message">The message.</param>
        Public Overrides Sub Info(message As String)
            Throw New NotImplementedException
            'Logger.Info(message)
        End Sub

        ''' <summary>
        ''' Log message at Warn level.
        ''' </summary>
        ''' <param name="message">The message.</param>
        Public Overrides Sub Warn(message As String)
            Throw New NotImplementedException
            'Logger.Warn(message)
        End Sub

        ''' <summary>
        ''' Log message at Error level.
        ''' </summary>
        ''' <param name="message">The message.</param>
        Public Overrides Sub [Error](message As String)
            Throw New NotImplementedException
            'Logger.[Error](message)
        End Sub

        ''' <summary>
        ''' Log message at Fatal level.
        ''' </summary>
        ''' <param name="message">The message.</param>
        Public Overrides Sub Fatal(message As String)
            Throw New NotImplementedException
            'Logger.Fatal(message)
        End Sub

        ''' <summary>
        ''' Log message at Trace level.
        ''' </summary>
        ''' <param name="message">The message.</param>
        ''' <param name="exception">The exception.</param>
        Public Overrides Sub Trace(message As String, exception As Exception)
            Throw New NotImplementedException
            'Logger.TraceException(message, exception)
        End Sub

        ''' <summary>
        ''' Log message at Debug level.
        ''' </summary>
        ''' <param name="message">The message.</param>
        ''' <param name="exception">The exception.</param>
        Public Overrides Sub Debug(message As String, exception As Exception)
            Throw New NotImplementedException
            'Logger.DebugException(message, exception)
        End Sub

        ''' <summary>
        ''' Log message at Info level.
        ''' </summary>
        ''' <param name="message">The message.</param>
        ''' <param name="exception">The exception.</param>
        Public Overrides Sub Info(message As String, exception As Exception)
            Throw New NotImplementedException
            'Logger.InfoException(message, exception)
        End Sub

        ''' <summary>
        ''' Log message at Warn level.
        ''' </summary>
        ''' <param name="message">The message.</param>
        ''' <param name="exception">The exception.</param>
        Public Overrides Sub Warn(message As String, exception As Exception)
            Throw New NotImplementedException
            'Logger.WarnException(message, exception)
        End Sub

        ''' <summary>
        ''' Log message at Error level.
        ''' </summary>
        ''' <param name="message">The message.</param>
        ''' <param name="exception">The exception.</param>
        Public Overrides Sub [Error](message As String, exception As Exception)
            Throw New NotImplementedException
            'Logger.ErrorException(message, exception)
        End Sub

        ''' <summary>
        ''' Log message at Fatal level.
        ''' </summary>
        ''' <param name="message">The message.</param>
        ''' <param name="exception">The exception.</param>
        Public Overrides Sub Fatal(message As String, exception As Exception)
            Throw New NotImplementedException
            'Logger.FatalException(message, exception)
        End Sub
    End Class

End Namespace