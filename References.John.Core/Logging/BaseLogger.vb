Namespace Logging
    Public MustInherit Class LoggerBase
        Implements ILogger
        ''' <summary>
        ''' Log message at Trace level.
        ''' </summary>
        ''' <param name="message">The message.</param>
        Public MustOverride Sub Trace(message As String) Implements ILogger.Trace

        ''' <summary>
        ''' Log message at Debug level.
        ''' </summary>
        ''' <param name="message">The message.</param>
        Public MustOverride Sub Debug(message As String) Implements ILogger.Debug

        ''' <summary>
        ''' Log message at Info level.
        ''' </summary>
        ''' <param name="message">The message.</param>
        Public MustOverride Sub Info(message As String) Implements ILogger.Info

        ''' <summary>
        ''' Log message at Warn level.
        ''' </summary>
        ''' <param name="message">The message.</param>
        Public MustOverride Sub Warn(message As String) Implements ILogger.Warn

        ''' <summary>
        ''' Log message at Error level.
        ''' </summary>
        ''' <param name="message">The message.</param>
        Public MustOverride Sub [Error](message As String) Implements ILogger.Error

        ''' <summary>
        ''' Log message at Fatal level.
        ''' </summary>
        ''' <param name="message">The message.</param>
        Public MustOverride Sub Fatal(message As String) Implements ILogger.Fatal

        ''' <summary>
        ''' Log message at Trace level.
        ''' </summary>
        ''' <param name="message">The message.</param>
        ''' <param name="exception">The exception.</param>
        Public MustOverride Sub Trace(message As String, exception As Exception) Implements ILogger.Trace

        ''' <summary>
        ''' Log message at Debug level.
        ''' </summary>
        ''' <param name="message">The message.</param>
        ''' <param name="exception">The exception.</param>
        Public MustOverride Sub Debug(message As String, exception As Exception) Implements ILogger.Debug

        ''' <summary>
        ''' Log message at Info level.
        ''' </summary>
        ''' <param name="message">The message.</param>
        ''' <param name="exception">The exception.</param>
        Public MustOverride Sub Info(message As String, exception As Exception) Implements ILogger.Info

        ''' <summary>
        ''' Log message at Warn level.
        ''' </summary>
        ''' <param name="message">The message.</param>
        ''' <param name="exception">The exception.</param>
        Public MustOverride Sub Warn(message As String, exception As Exception) Implements ILogger.Warn

        ''' <summary>
        ''' Log message at Error level.
        ''' </summary>
        ''' <param name="message">The message.</param>
        ''' <param name="exception">The exception.</param>
        Public MustOverride Sub [Error](message As String, exception As Exception) Implements ILogger.Error

        ''' <summary>
        ''' Log message at Fatal level.
        ''' </summary>
        ''' <param name="message">The message.</param>
        ''' <param name="exception">The exception.</param>
        Public MustOverride Sub Fatal(message As String, exception As Exception) Implements ILogger.Fatal

        ''' <summary>
        ''' Log message at Trace level.
        ''' </summary>
        ''' <param name="messageFormat">The message format.</param>
        ''' <param name="args">The args.</param>
        Public Sub Trace(messageFormat As String, ParamArray args As Object()) Implements ILogger.Trace
            Trace(String.Format(messageFormat, args))
        End Sub

        ''' <summary>
        ''' Log message at Debug level.
        ''' </summary>
        ''' <param name="messageFormat">The message format.</param>
        ''' <param name="args">The args.</param>
        Public Sub Debug(messageFormat As String, ParamArray args As Object()) Implements ILogger.Debug
            Debug(String.Format(messageFormat, args))
        End Sub

        ''' <summary>
        ''' Log message at Info level.
        ''' </summary>
        ''' <param name="messageFormat">The message format.</param>
        ''' <param name="args">The args.</param>
        Public Sub Info(messageFormat As String, ParamArray args As Object()) Implements ILogger.Info
            Info(String.Format(messageFormat, args))
        End Sub

        ''' <summary>
        ''' Log message at Warn level.
        ''' </summary>
        ''' <param name="messageFormat">The message format.</param>
        ''' <param name="args">The args.</param>
        Public Sub Warn(messageFormat As String, ParamArray args As Object()) Implements ILogger.Warn
            Warn(String.Format(messageFormat, args))
        End Sub

        ''' <summary>
        ''' Log message at Error level.
        ''' </summary>
        ''' <param name="messageFormat">The message format.</param>
        ''' <param name="args">The args.</param>
        Public Sub [Error](messageFormat As String, ParamArray args As Object()) Implements ILogger.Error
            [Error](String.Format(messageFormat, args))
        End Sub

        ''' <summary>
        ''' Log message at Fatal level.
        ''' </summary>
        ''' <param name="messageFormat">The message format.</param>
        ''' <param name="args">The args.</param>
        Public Sub Fatal(messageFormat As String, ParamArray args As Object()) Implements ILogger.Fatal
            Fatal(String.Format(messageFormat, args))
        End Sub

        ''' <summary>
        ''' Log message at Trace level.
        ''' </summary>
        ''' <param name="messageFormat">The message format.</param>
        ''' <param name="exception">The exception.</param>
        ''' <param name="args">The args.</param>
        Public Sub Trace(messageFormat As String, exception As Exception, ParamArray args As Object()) Implements ILogger.Trace
            Trace(String.Format(messageFormat, args), exception)
        End Sub

        ''' <summary>
        ''' Log message at Debug level.
        ''' </summary>
        ''' <param name="messageFormat">The message format.</param>
        ''' <param name="exception">The exception.</param>
        ''' <param name="args">The args.</param>
        Public Sub Debug(messageFormat As String, exception As Exception, ParamArray args As Object()) Implements ILogger.Debug
            Debug(String.Format(messageFormat, args), exception)
        End Sub

        ''' <summary>
        ''' Log message at Info level.
        ''' </summary>
        ''' <param name="messageFormat">The message format.</param>
        ''' <param name="exception">The exception.</param>
        ''' <param name="args">The args.</param>
        Public Sub Info(messageFormat As String, exception As Exception, ParamArray args As Object()) Implements ILogger.Info
            Info(String.Format(messageFormat, args), exception)
        End Sub

        ''' <summary>
        ''' Log message at Warn level.
        ''' </summary>
        ''' <param name="messageFormat">The message format.</param>
        ''' <param name="exception">The exception.</param>
        ''' <param name="args">The args.</param>
        Public Sub Warn(messageFormat As String, exception As Exception, ParamArray args As Object()) Implements ILogger.Warn
            Warn(String.Format(messageFormat, args), exception)
        End Sub

        ''' <summary>
        ''' Log message at Error level.
        ''' </summary>
        ''' <param name="messageFormat">The message format.</param>
        ''' <param name="exception">The exception.</param>
        ''' <param name="args">The args.</param>
        Public Sub [Error](messageFormat As String, exception As Exception, ParamArray args As Object()) Implements ILogger.Error
            [Error](String.Format(messageFormat, args), exception)
        End Sub

        ''' <summary>
        ''' Log message at Fatal level.
        ''' </summary>
        ''' <param name="messageFormat">The message format.</param>
        ''' <param name="exception">The exception.</param>
        ''' <param name="args">The args.</param>
        Public Sub Fatal(messageFormat As String, exception As Exception, ParamArray args As Object()) Implements ILogger.Fatal
            Fatal(String.Format(messageFormat, args), exception)
        End Sub
    End Class

End Namespace