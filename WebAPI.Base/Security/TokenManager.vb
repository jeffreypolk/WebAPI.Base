Imports System.Security.Claims
'Imports System.IdentityModel.Tokens
Imports System.IdentityModel.Tokens.Jwt
Imports Microsoft.IdentityModel.Tokens

Namespace Security
    Public Class TokenManager

        Public Function GenerateToken(UserId As Integer) As String

            Dim securityKey = New SymmetricSecurityKey(Encoding.[Default].GetBytes(Settings.TokenSigningSecret))
            Dim signingCredentials = New SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature)

            Dim header = New JwtHeader(signingCredentials)

            Dim Now As DateTime = DateTime.UtcNow
            Dim Epoch As New DateTime(1970, 1, 1)
            Dim NowSeconds As Double = New TimeSpan(Now.Ticks - Epoch.Ticks).TotalSeconds
            Dim ExpSeconds As Double = New TimeSpan(Now.AddSeconds(Settings.TokenExpireSeconds).Ticks - Epoch.Ticks).TotalSeconds

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
                    .ClockSkew = TimeSpan.Zero,
                    .IssuerSigningKey = New SymmetricSecurityKey(Encoding.[Default].GetBytes(Settings.TokenSigningSecret))
                }

            Dim securityToken As SecurityToken
            Dim principal = tokenHandler.ValidateToken(Token, validationParameters, securityToken)

            Return principal

        End Function


    End Class
End Namespace