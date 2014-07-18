Imports System.Web.Http.ExceptionHandling


Namespace ExceptionHandling

    Public Class GenericTextExceptionHandler
        Inherits ExceptionHandler
        Public Overrides Sub Handle(context As ExceptionHandlerContext)
            context.Result = New InternalServerErrorTextPlainResult("An unhandled exception occurred; check the log for more information.", Encoding.UTF8, context.Request)
        End Sub
    End Class

End Namespace