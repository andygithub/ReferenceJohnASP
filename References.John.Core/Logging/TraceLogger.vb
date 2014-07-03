Namespace Logging
    Public Class TrageLogger
        Inherits LoggerBase
        ''' <summary>
        ''' Initializes a new instance of the trace logging class.
        ''' </summary>
        Public Sub New()
        End Sub

        ''' <summary>
        ''' Log message at Trace level.
        ''' </summary>
        ''' <param name="message">The message.</param>
        Public Overrides Sub Trace(message As String)
            Diagnostics.Debug.WriteLine(message, "Trace")
        End Sub

        ''' <summary>
        ''' Log message at Debug level.
        ''' </summary>
        ''' <param name="message">The message.</param>
        Public Overrides Sub Debug(message As String)
            Diagnostics.Debug.WriteLine(message, "Debug")
        End Sub

        ''' <summary>
        ''' Log message at Info level.
        ''' </summary>
        ''' <param name="message">The message.</param>
        Public Overrides Sub Info(message As String)
            Diagnostics.Debug.WriteLine(message, "Info")
        End Sub

        ''' <summary>
        ''' Log message at Warn level.
        ''' </summary>
        ''' <param name="message">The message.</param>
        Public Overrides Sub Warn(message As String)
            Diagnostics.Debug.WriteLine(message, "Warn")
        End Sub

        ''' <summary>
        ''' Log message at Error level.
        ''' </summary>
        ''' <param name="message">The message.</param>
        Public Overrides Sub [Error](message As String)
            Diagnostics.Debug.WriteLine(message, "Error")
        End Sub

        ''' <summary>
        ''' Log message at Fatal level.
        ''' </summary>
        ''' <param name="message">The message.</param>
        Public Overrides Sub Fatal(message As String)
            Diagnostics.Debug.WriteLine(message, "Fatal")
        End Sub

        ''' <summary>
        ''' Log message at Trace level.
        ''' </summary>
        ''' <param name="message">The message.</param>
        ''' <param name="exception">The exception.</param>
        Public Overrides Sub Trace(message As String, exception As Exception)
            Diagnostics.Debug.WriteLine(message)
        End Sub

        ''' <summary>
        ''' Log message at Debug level.
        ''' </summary>
        ''' <param name="message">The message.</param>
        ''' <param name="exception">The exception.</param>
        Public Overrides Sub Debug(message As String, exception As Exception)
            Diagnostics.Debug.WriteLine(message)
        End Sub

        ''' <summary>
        ''' Log message at Info level.
        ''' </summary>
        ''' <param name="message">The message.</param>
        ''' <param name="exception">The exception.</param>
        Public Overrides Sub Info(message As String, exception As Exception)
            Diagnostics.Debug.WriteLine(message)
        End Sub

        ''' <summary>
        ''' Log message at Warn level.
        ''' </summary>
        ''' <param name="message">The message.</param>
        ''' <param name="exception">The exception.</param>
        Public Overrides Sub Warn(message As String, exception As Exception)
            Diagnostics.Debug.WriteLine(message)
        End Sub

        ''' <summary>
        ''' Log message at Error level.
        ''' </summary>
        ''' <param name="message">The message.</param>
        ''' <param name="exception">The exception.</param>
        Public Overrides Sub [Error](message As String, exception As Exception)
            Diagnostics.Debug.WriteLine(message)
        End Sub

        ''' <summary>
        ''' Log message at Fatal level.
        ''' </summary>
        ''' <param name="message">The message.</param>
        ''' <param name="exception">The exception.</param>
        Public Overrides Sub Fatal(message As String, exception As Exception)
            Diagnostics.Debug.WriteLine(message)
        End Sub
    End Class

End Namespace