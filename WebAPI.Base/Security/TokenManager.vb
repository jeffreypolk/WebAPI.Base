Imports System.Security.Claims
'Imports System.IdentityModel.Tokens
Imports System.IdentityModel.Tokens.Jwt
Imports Microsoft.IdentityModel.Tokens

Namespace Security
    Public Class TokenManager

        'TODO: these should come from solution parameters
        Shared Property Secret As String = "ltYvCGSBmIFlZAOdhWL88tuzHNVz4MKaCgzaSdSv4H9Mv9b95v69k4FOh5wPXhvkbinPzL5jiXZBPALGHIa5Do8FPDbznqo9uYSREeucKitXjXsvhgdib9LbX2bzuBzD"
        Shared Property ExpireSeconds As Integer = 15 * 60

        Public Function GenerateToken(UserId As Integer) As String

            Dim securityKey = New SymmetricSecurityKey(Encoding.[Default].GetBytes(Secret))
            Dim signingCredentials = New SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature)

            Dim header = New JwtHeader(signingCredentials)

            Dim Now As DateTime = DateTime.UtcNow
            Dim Epoch As New DateTime(1970, 1, 1)
            Dim NowSeconds As Double = New TimeSpan(Now.Ticks - Epoch.Ticks).TotalSeconds
            Dim ExpSeconds As Double = New TimeSpan(Now.AddSeconds(ExpireSeconds).Ticks - Epoch.Ticks).TotalSeconds

            Dim payload = New JwtPayload() From {
                {"userid", UserId},
                {"iat", NowSeconds},
                {"exp", ExpSeconds}
            }

            Dim secToken = New JwtSecurityToken(header, payload)

            Dim handler = New JwtSecurityTokenHandler()
            Dim tokenString = handler.WriteToken(secToken)
            Return tokenString
        End Function

        Public Function GetPrincipal(Token As String) As ClaimsPrincipal
            Try
                Dim tokenHandler = New JwtSecurityTokenHandler()
                Dim jwtToken = TryCast(tokenHandler.ReadToken(Token), JwtSecurityToken)

                If jwtToken Is Nothing Then
                    Return Nothing
                End If

                Dim validationParameters = New TokenValidationParameters() With {
                    .RequireExpirationTime = True,
                    .ValidateIssuer = False,
                    .ValidateAudience = False,
                    .ValidateLifetime = True,
                    .IssuerSigningKey = New SymmetricSecurityKey(Encoding.[Default].GetBytes(Secret))
                }

                Dim securityToken As SecurityToken
                Dim principal = tokenHandler.ValidateToken(Token, validationParameters, securityToken)

                Return principal

            Catch generatedExceptionName As Exception
                'TODO: should write to error log
                Throw
            End Try
        End Function


    End Class
End Namespace