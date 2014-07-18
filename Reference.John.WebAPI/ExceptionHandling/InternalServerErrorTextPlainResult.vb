Imports System.Web.Http
Imports System.Net.Http
Imports System.Threading
Imports System.Net
Imports System.Threading.Tasks

Namespace ExceptionHandling

    Public Class InternalServerErrorTextPlainResult
        Implements IHttpActionResult

        Public Sub New(contentValue As String, encodingValue As Encoding, requestValue As HttpRequestMessage)
            If contentValue Is Nothing Then Throw New ArgumentNullException("content")
            If encodingValue Is Nothing Then Throw New ArgumentNullException("encoding")
            If requestValue Is Nothing Then Throw New ArgumentNullException("request")

            Content = contentValue
            Encoding = encodingValue
            Request = requestValue
        End Sub

        Public Property Content() As String

        Public Property Encoding() As Encoding

        Public Property Request() As HttpRequestMessage

        Public Function ExecuteAsync(cancellationToken As CancellationToken) As Task(Of HttpResponseMessage) Implements IHttpActionResult.ExecuteAsync
            Return Task.FromResult(Execute())
        End Function

        Private Function Execute() As HttpResponseMessage
            Dim response As New HttpResponseMessage(HttpStatusCode.InternalServerError)
            response.RequestMessage = Request
            response.Content = New StringContent(Content, Encoding)
            Return response
        End Function

    End Class

End Namespace