Imports System.Net
Imports System.Web.Http

Namespace Controllers
    Public Class TokensController
        Inherits ApiController
        Public Function PostToken(Form As Http.Formatting.FormDataCollection) As Models.TokenResult

            Dim ApiToken As String = Form.Get("ApiToken")

            Dim clientAddress As String = Security.ClientIPAddress.GetIPAddress()

            Dim ret As New Models.TokenResult

            If String.IsNullOrEmpty(ApiToken) Then
                ret.AddError("Invalid API Token")
            Else
                'generate the token
                Dim Mgr As New Security.TokenManager()
                ret.Token = Mgr.GenerateToken(2173)

            End If

            Return ret
        End Function


    End Class
End Namespace