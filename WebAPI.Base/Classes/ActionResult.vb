Imports System.Net.Http
Imports System.Threading
Imports System.Threading.Tasks
Imports System.Web.Http

Public Class ActionResult
    Implements IHttpActionResult

    Private _StatusCode As Net.HttpStatusCode
    Private _Result As Models.BaseResult

    Sub New(StatusCode As Net.HttpStatusCode, Result As Models.BaseResult)
        _StatusCode = StatusCode
        _Result = Result
    End Sub
    Public Function ExecuteAsync(cancellationToken As CancellationToken) As Task(Of HttpResponseMessage) Implements IHttpActionResult.ExecuteAsync

        Dim response As New HttpResponseMessage(_StatusCode)
        Dim jsonFormatter As System.Net.Http.Formatting.MediaTypeFormatter = New System.Net.Http.Formatting.JsonMediaTypeFormatter()
        response.Content = New System.Net.Http.ObjectContent(Of Object)(_Result, jsonFormatter)
        'response.RequestMessage = Request
        'response.ReasonPhrase = "unauthorized"
        Return Task.FromResult(response)

    End Function
End Class


