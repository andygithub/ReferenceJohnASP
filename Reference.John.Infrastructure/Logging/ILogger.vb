Namespace Logging
    Public Interface ILogger

        ''' <summary>
        ''' Log message at Trace level. 
        ''' </summary>
        ''' <param name="message">The message.</param>
        Sub Trace(message As String)

        ''' <summary>
        ''' Log message at Debug level. 
        ''' </summary>
        ''' <param name="message">The message.</param>
        Sub Debug(message As String)


        ''' <summary>
        ''' Log message at Info level.
        ''' </summary>
        ''' <param name="message">The message.</param>
        Sub Info(message As String)


        ''' <summary>
        ''' Log message at Warn level. 
        ''' </summary>
        ''' <param name="message">The message.</param>
        Sub Warn(message As String)

        ''' <summary>
        ''' Log message at Error level.
        ''' </summary>
        ''' <param name="message">The message.</param>
        Sub [Error](message As String)

        ''' <summary>
        ''' Log message at Fatal level. 
        ''' </summary>
        ''' <param name="message">The message.</param>
        Sub Fatal(message As String)

        ''' <summary>
        ''' Log message at Trace level.
        ''' </summary>
        ''' <param name="message">The message.</param>
        ''' <param name="exception">The exception.</param>
        Sub Trace(message As String, exception As Exception)

        ''' <summary>
        ''' Log message at Debug level. 
        ''' </summary>
        ''' <param name="message">The message.</param>
        ''' <param name="exception">The exception.</param>
        Sub Debug(message As String, exception As Exception)

        ''' <summary>
        ''' Log message at Info level.
        ''' </summary>
        ''' <param name="message">The message.</param>
        ''' <param name="exception">The exception.</param>
        Sub Info(message As String, exception As Exception)

        ''' <summary>
        ''' Log message at Warn level.
        ''' </summary>
        ''' <param name="message">The message.</param>
        ''' <param name="exception">The exception.</param>
        Sub Warn(message As String, exception As Exception)

        ''' <summary>
        ''' Log message at Error level.
        ''' </summary>
        ''' <param name="message">The message.</param>
        ''' <param name="exception">The exception.</param>
        Sub [Error](message As String, exception As Exception)

        ''' <summary>
        ''' Log message at Fatal level. 
        ''' </summary>
        ''' <param name="message">The message.</param>
        ''' <param name="exception">The exception.</param>
        Sub Fatal(message As String, exception As Exception)

        ''' <summary>
        ''' Log message at Trace level.
        ''' </summary>
        ''' <param name="messageFormat">The message format.</param>
        ''' <param name="args">The args.</param>
        Sub Trace(messageFormat As String, ParamArray args As Object())

        ''' <summary>
        ''' Log message at Debug level. 
        ''' </summary>
        ''' <param name="messageFormat">The message format.</param>
        ''' <param name="args">The args.</param>
        Sub Debug(messageFormat As String, ParamArray args As Object())

        ''' <summary>
        ''' Log message at Info level.
        ''' </summary>
        ''' <param name="messageFormat">The message format.</param>
        ''' <param name="args">The args.</param>
        Sub Info(messageFormat As String, ParamArray args As Object())

        ''' <summary>
        ''' Log message at Warn level.
        ''' </summary>
        ''' <param name="messageFormat">The message format.</param>
        ''' <param name="args">The args.</param>
        Sub Warn(messageFormat As String, ParamArray args As Object())

        ''' <summary>
        ''' Log message at Error level.
        ''' </summary>
        ''' <param name="messageFormat">The message format.</param>
        ''' <param name="args">The args.</param>
        Sub [Error](messageFormat As String, ParamArray args As Object())

        ''' <summary>
        ''' Log message at Fatal level. 
        ''' </summary>
        ''' <param name="messageFormat">The message format.</param>
        ''' <param name="args">The args.</param>
        Sub Fatal(messageFormat As String, ParamArray args As Object())

        ''' <summary>
        ''' Log message at Trace level.
        ''' </summary>
        ''' <param name="messageFormat">The message format.</param>
        ''' <param name="exception">The exception.</param>
        ''' <param name="args">The args.</param>
        Sub Trace(messageFormat As String, exception As Exception, ParamArray args As Object())

        ''' <summary>
        ''' Log message at Debug level. 
        ''' </summary>
        ''' <param name="messageFormat">The message format.</param>
        ''' <param name="exception">The exception.</param>
        ''' <param name="args">The args.</param>
        Sub Debug(messageFormat As String, exception As Exception, ParamArray args As Object())

        ''' <summary>
        ''' Log message at Info level.
        ''' </summary>
        ''' <param name="messageFormat">The message format.</param>
        ''' <param name="exception">The exception.</param>
        ''' <param name="args">The args.</param>
        Sub Info(messageFormat As String, exception As Exception, ParamArray args As Object())

        ''' <summary>
        ''' Log message at Warn level.
        ''' </summary>
        ''' <param name="messageFormat">The message format.</param>
        ''' <param name="exception">The exception.</param>
        ''' <param name="args">The args.</param>
        Sub Warn(messageFormat As String, exception As Exception, ParamArray args As Object())

        ''' <summary>
        ''' Log message at Error level. 
        ''' </summary>
        ''' <param name="messageFormat">The message format.</param>
        ''' <param name="exception">The exception.</param>
        ''' <param name="args">The args.</param>
        Sub [Error](messageFormat As String, exception As Exception, ParamArray args As Object())

        ''' <summary>
        ''' Log message at Fatal level.
        ''' </summary>
        ''' <param name="messageFormat">The message format.</param>
        ''' <param name="exception">The exception.</param>
        ''' <param name="args">The args.</param>
        Sub Fatal(messageFormat As String, exception As Exception, ParamArray args As Object())

    End Interface

End Namespace