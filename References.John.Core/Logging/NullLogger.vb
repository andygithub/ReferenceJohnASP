Namespace Logging
    ''' <summary>
    ''' An <see cref="ILogger"/> implementation of the Null Object pattern for use when configured to not use logging.
    ''' </summary>
    Public Class NullLogger
        Implements ILogger
        ''' <summary>
        ''' Log message at Trace level.
        ''' </summary>
        ''' <param name="message">The message.</param>
        Public Sub Trace(message As String) Implements ILogger.Trace
        End Sub

        ''' <summary>
        ''' Log message at Debug level.
        ''' </summary>
        ''' <param name="message">The message.</param>
        Public Sub Debug(message As String) Implements ILogger.Debug
        End Sub

        ''' <summary>
        ''' Log message at Info level.
        ''' </summary>
        ''' <param name="message">The message.</param>
        Public Sub Info(message As String) Implements ILogger.Info
        End Sub

        ''' <summary>
        ''' Log message at Warn level.
        ''' </summary>
        ''' <param name="message">The message.</param>
        Public Sub Warn(message As String) Implements ILogger.Warn
        End Sub

        ''' <summary>
        ''' Log message at Error level.
        ''' </summary>
        ''' <param name="message">The message.</param>
        Public Sub [Error](message As String) Implements ILogger.Error
        End Sub

        ''' <summary>
        ''' Log message at Fatal level.
        ''' </summary>
        ''' <param name="message">The message.</param>
        Public Sub Fatal(message As String) Implements ILogger.Fatal
        End Sub

        ''' <summary>
        ''' Log message at Trace level.
        ''' </summary>
        ''' <param name="message">The message.</param>
        ''' <param name="exception">The exception.</param>
        Public Sub Trace(message As String, exception As Exception) Implements ILogger.Trace
        End Sub

        ''' <summary>
        ''' Log message at Debug level.
        ''' </summary>
        ''' <param name="message">The message.</param>
        ''' <param name="exception">The exception.</param>
        Public Sub Debug(message As String, exception As Exception) Implements ILogger.Debug
        End Sub

        ''' <summary>
        ''' Log message at Info level.
        ''' </summary>
        ''' <param name="message">The message.</param>
        ''' <param name="exception">The exception.</param>
        Public Sub Info(message As String, exception As Exception) Implements ILogger.Info
        End Sub

        ''' <summary>
        ''' Log message at Warn level.
        ''' </summary>
        ''' <param name="message">The message.</param>
        ''' <param name="exception">The exception.</param>
        Public Sub Warn(message As String, exception As Exception) Implements ILogger.Warn
        End Sub

        ''' <summary>
        ''' Log message at Error level.
        ''' </summary>
        ''' <param name="message">The message.</param>
        ''' <param name="exception">The exception.</param>
        Public Sub [Error](message As String, exception As Exception) Implements ILogger.Error
        End Sub

        ''' <summary>
        ''' Log message at Fatal level.
        ''' </summary>
        ''' <param name="message">The message.</param>
        ''' <param name="exception">The exception.</param>
        Public Sub Fatal(message As String, exception As Exception) Implements ILogger.Fatal
        End Sub

        ''' <summary>
        ''' Log message at Trace level.
        ''' </summary>
        ''' <param name="messageFormat">The message format.</param>
        ''' <param name="args">The args.</param>
        Public Sub Trace(messageFormat As String, ParamArray args As Object()) Implements ILogger.Trace
        End Sub

        ''' <summary>
        ''' Log message at Debug level.
        ''' </summary>
        ''' <param name="messageFormat">The message format.</param>
        ''' <param name="args">The args.</param>
        Public Sub Debug(messageFormat As String, ParamArray args As Object()) Implements ILogger.Debug
        End Sub

        ''' <summary>
        ''' Log message at Info level.
        ''' </summary>
        ''' <param name="messageFormat">The message format.</param>
        ''' <param name="args">The args.</param>
        Public Sub Info(messageFormat As String, ParamArray args As Object()) Implements ILogger.Info
        End Sub

        ''' <summary>
        ''' Log message at Warn level.
        ''' </summary>
        ''' <param name="messageFormat">The message format.</param>
        ''' <param name="args">The args.</param>
        Public Sub Warn(messageFormat As String, ParamArray args As Object()) Implements ILogger.Warn
        End Sub

        ''' <summary>
        ''' Log message at Error level.
        ''' </summary>
        ''' <param name="messageFormat">The message format.</param>
        ''' <param name="args">The args.</param>
        Public Sub [Error](messageFormat As String, ParamArray args As Object()) Implements ILogger.Error
        End Sub

        ''' <summary>
        ''' Log message at Fatal level.
        ''' </summary>
        ''' <param name="messageFormat">The message format.</param>
        ''' <param name="args">The args.</param>
        Public Sub Fatal(messageFormat As String, ParamArray args As Object()) Implements ILogger.Fatal
        End Sub

        ''' <summary>
        ''' Log message at Trace level.
        ''' </summary>
        ''' <param name="messageFormat">The message format.</param>
        ''' <param name="exception">The exception.</param>
        ''' <param name="args">The args.</param>
        Public Sub Trace(messageFormat As String, exception As Exception, ParamArray args As Object()) Implements ILogger.Trace
        End Sub

        ''' <summary>
        ''' Log message at Debug level.
        ''' </summary>
        ''' <param name="messageFormat">The message format.</param>
        ''' <param name="exception">The exception.</param>
        ''' <param name="args">The args.</param>
        Public Sub Debug(messageFormat As String, exception As Exception, ParamArray args As Object()) Implements ILogger.Debug
        End Sub

        ''' <summary>
        ''' Log message at Info level.
        ''' </summary>
        ''' <param name="messageFormat">The message format.</param>
        ''' <param name="exception">The exception.</param>
        ''' <param name="args">The args.</param>
        Public Sub Info(messageFormat As String, exception As Exception, ParamArray args As Object()) Implements ILogger.Info
        End Sub

        ''' <summary>
        ''' Log message at Warn level.
        ''' </summary>
        ''' <param name="messageFormat">The message format.</param>
        ''' <param name="exception">The exception.</param>
        ''' <param name="args">The args.</param>
        Public Sub Warn(messageFormat As String, exception As Exception, ParamArray args As Object()) Implements ILogger.Warn
        End Sub

        ''' <summary>
        ''' Log message at Error level.
        ''' </summary>
        ''' <param name="messageFormat">The message format.</param>
        ''' <param name="exception">The exception.</param>
        ''' <param name="args">The args.</param>
        Public Sub [Error](messageFormat As String, exception As Exception, ParamArray args As Object()) Implements ILogger.Error
        End Sub

        ''' <summary>
        ''' Log message at Fatal level.
        ''' </summary>
        ''' <param name="messageFormat">The message format.</param>
        ''' <param name="exception">The exception.</param>
        ''' <param name="args">The args.</param>
        Public Sub Fatal(messageFormat As String, exception As Exception, ParamArray args As Object()) Implements ILogger.Fatal
        End Sub
    End Class

End Namespace