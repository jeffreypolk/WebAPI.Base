Imports System.Net
Imports System.Net.Http

Public Class Helper

    Public Shared ReadOnly Property InstallationKey As Guid
        Get
            Return New Guid("876172af-03db-46a8-829a-1f8aa07995f2")
        End Get
    End Property

    'Public Shared Function CreateHttpResponseMessage(Request As HttpRequestMessage, StatusCode As HttpStatusCode, Result As Models.BaseResult) As HttpResponseMessage

    '    Dim response As New HttpResponseMessage(StatusCode)
    '    Dim jsonFormatter As System.Net.Http.Formatting.MediaTypeFormatter = New System.Net.Http.Formatting.JsonMediaTypeFormatter()
    '    response.Content = New System.Net.Http.ObjectContent(Of Object)(Result, jsonFormatter)
    '    response.RequestMessage = Request
    '    'response.ReasonPhrase = "unauthorized"
    '    Return response

    'End Function

    'Public Shared Function CreateActionResult(Request As HttpRequestMessage, StatusCode As HttpStatusCode, Result As Models.BaseResult) As HttpResponseMessage

    '    Dim a As New 
    '    Dim response As New HttpResponseMessage(StatusCode)
    '    Dim jsonFormatter As System.Net.Http.Formatting.MediaTypeFormatter = New System.Net.Http.Formatting.JsonMediaTypeFormatter()
    '    response.Content = New System.Net.Http.ObjectContent(Of Object)(Result, jsonFormatter)
    '    response.RequestMessage = Request
    '    'response.ReasonPhrase = "unauthorized"
    '    Return response

    'End Function
End Class


