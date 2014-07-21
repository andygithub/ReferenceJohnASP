Imports System.Net.Http
Imports System.Web
Imports System.Web.Http.ExceptionHandling

Namespace ExceptionHandling

    'http://aspnet.codeplex.com/SourceControl/latest#Samples/ReadMe.txt

    Public Class ElmahExceptionLogger
        Inherits ExceptionLogger

        Private Const HttpContextBaseKey As String = "MS_HttpContext"

        Public Overrides Sub Log(context As ExceptionLoggerContext)
            ' Retrieve the current HttpContext instance for this request.
            'be aware that this shoulnd't fire for notfound etc
            Dim httpContext As HttpContext = GetHttpContext(context.Request)

            If httpContext Is Nothing Then
                Return
            End If

            ' Wrap the exception in an HttpUnhandledException so that ELMAH can capture the original error page.
            Dim exceptionToRaise As Exception = New HttpUnhandledException(message:=Nothing, innerException:=context.Exception)
            ' Send the exception to ELMAH (for logging, mailing, filtering, etc.).
            Dim signal As Elmah.ErrorSignal = Elmah.ErrorSignal.FromContext(httpContext)
            signal.Raise(exceptionToRaise, httpContext)
        End Sub

        Private Shared Function GetHttpContext(request As HttpRequestMessage) As HttpContext
            Dim contextBase As HttpContextBase = GetHttpContextBase(request)

            If contextBase Is Nothing Then
                Return Nothing
            End If

            Return ToHttpContext(contextBase)
        End Function

        Private Shared Function GetHttpContextBase(request As HttpRequestMessage) As HttpContextBase
            If request Is Nothing Then
                Return Nothing
            End If

            Dim value As Object = Nothing

            If Not request.Properties.TryGetValue(HttpContextBaseKey, value) Then
                Return Nothing
            End If

            Return TryCast(value, HttpContextBase)
        End Function

        Private Shared Function ToHttpContext(contextBase As HttpContextBase) As HttpContext
            Return contextBase.ApplicationInstance.Context
        End Function
    End Class

End Namespace

