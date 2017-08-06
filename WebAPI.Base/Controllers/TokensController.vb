Imports System.Net
Imports System.Web.Http

Namespace Controllers

    Public Class TokensController
        Inherits ApiController

        <AllowAnonymous()>
        Public Function PostToken(Form As Http.Formatting.FormDataCollection) As Models.TokenResult
            Dim ret As New Models.TokenResult

            'Security.APIKeys.TEMP_BuildKey()

            'verify a key was sent
            Dim APIKey As String = Form.Get("APIKey")
            If String.IsNullOrEmpty(APIKey) Then
                Return Models.TokenResult.InvalidAPIKey()
            End If
            If Security.APIKeys.Keys.ContainsKey(APIKey) = False Then
                Return Models.TokenResult.InvalidAPIKey()
            End If

            'get the key from cache
            Dim key As Models.APIKey = Security.APIKeys.Keys(APIKey)

            'get the client IP Address
            Dim clientAddress As String = Security.ClientIPAddress.GetIPAddress()

            'verify IP Addresses in the api key
            If key.IPAddresses.Contains("*") Then
                'this is allow all
            ElseIf key.IPAddresses.Contains(clientAddress) = False Then
                Return Models.TokenResult.InvalidAPIKey()
            End If

            'verify installation key in the api key
            If key.InstallationKeys.Contains(Guid.Empty) Then
                'this is allow all
            ElseIf key.InstallationKeys.Contains(Helper.InstallationKey) = False Then
                Return Models.TokenResult.InvalidAPIKey()
            End If

            'we are all good.  generate a jwt 
            Dim Mgr As New Security.TokenManager()
            ret.Token = Mgr.GenerateToken(2173)


            Return ret
        End Function


    End Class
End Namespace