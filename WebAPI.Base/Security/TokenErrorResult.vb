Imports System.Web.Http
Imports System.Net
Imports System.Net.Http
Imports System.Threading
Imports System.Threading.Tasks

Namespace Security
    Public Class TokenErrorResult
        Implements IHttpActionResult
        Private ResponseMessage As Object
        Public Sub New(reasonPhrase__1 As String, request__2 As HttpRequestMessage, responseMessage__3 As Object)
            ReasonPhrase = reasonPhrase__1
            Request = request__2
            ResponseMessage = responseMessage__3
        End Sub

        Public Property ReasonPhrase() As String
            Get
                Return m_ReasonPhrase
            End Get
            Private Set
                m_ReasonPhrase = Value
            End Set
        End Property
        Private m_ReasonPhrase As String

        Public Property Request() As HttpRequestMessage
            Get
                Return m_Request
            End Get
            Private Set
                m_Request = Value
            End Set
        End Property
        Private m_Request As HttpRequestMessage

        Public Function ExecuteAsync(cancellationToken As CancellationToken) As Task(Of HttpResponseMessage) Implements IHttpActionResult.ExecuteAsync
            Return Task.FromResult(Execute())
        End Function

        Private Function Execute() As HttpResponseMessage
            Dim response As New HttpResponseMessage(HttpStatusCode.Unauthorized)
            Dim jsonFormatter As System.Net.Http.Formatting.MediaTypeFormatter = New System.Net.Http.Formatting.JsonMediaTypeFormatter()
            response.Content = New System.Net.Http.ObjectContent(Of Object)(ResponseMessage, jsonFormatter)
            response.RequestMessage = Request
            response.ReasonPhrase = ReasonPhrase
            Return response
        End Function
    End Class

End Namespace
