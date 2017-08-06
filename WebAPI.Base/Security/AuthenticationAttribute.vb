Imports System.Net
Imports System.Net.Http
Imports System.Threading
Imports System.Threading.Tasks
Imports System.Web.Http
Imports System.Web.Http.Filters

Namespace Security
    Public Class AuthenticationAttribute
        Inherits ActionFilterAttribute
        Implements System.Web.Http.Filters.IAuthenticationFilter

        Public Function AuthenticateAsync(context As HttpAuthenticationContext, cancellationToken As CancellationToken) As Task Implements IAuthenticationFilter.AuthenticateAsync

            'enforce SSL?
            If Settings.RequireSSL AndAlso HttpContext.Current.Request.IsSecureConnection = False Then
                context.ErrorResult = GetRequiresSSLError(context)
                Return Task.FromResult(0)
            End If

            'does this request allow anonymous?
            If context.ActionContext.ControllerContext.ControllerDescriptor.GetCustomAttributes(Of AllowAnonymousAttribute).Any() Then
                Return Task.FromResult(0)
            ElseIf context.ActionContext.ActionDescriptor.GetCustomAttributes(Of AllowAnonymousAttribute).Any() Then
                Return Task.FromResult(0)
            End If

            'still here...validate token
            Dim principal As System.Security.Claims.ClaimsPrincipal = Nothing
            Dim authorization As Net.Http.Headers.AuthenticationHeaderValue = context.Request.Headers.Authorization
            If authorization IsNot Nothing Then
                If authorization.Scheme IsNot Nothing Then
                    If authorization.Parameter IsNot Nothing Then
                        If authorization.Scheme.ToLower = "bearer" Then
                            Dim mgr As New Security.TokenManager()
                            Dim Token As String = authorization.Parameter
                            Try
                                principal = mgr.GetPrincipal(Token)
                            Catch ex1 As Microsoft.IdentityModel.Tokens.SecurityTokenExpiredException
                                'should we do anything?
                            Catch ex2 As Exception
                                'should we do anything?
                            End Try

                            If principal IsNot Nothing Then
                                context.Principal = principal
                            End If
                        End If
                    End If
                End If
            End If

            If principal Is Nothing Then
                context.ErrorResult = GetTokenErrorResult(context)
            End If

            Return Task.FromResult(0)
        End Function

        Public Function ChallengeAsync(context As HttpAuthenticationChallengeContext, cancellationToken As CancellationToken) As Task Implements IAuthenticationFilter.ChallengeAsync
            Return Task.FromResult(0)
        End Function

        Private Function GetTokenErrorResult(context As HttpAuthenticationContext) As IHttpActionResult
            Dim ret As New Models.BaseResult
            ret.AddError("Invalid token", Models.ErrorCodes.NotAuthorized)
            Return New ActionResult(HttpStatusCode.Unauthorized, ret)
        End Function

        Private Function GetRequiresSSLError(context As HttpAuthenticationContext) As IHttpActionResult
            Dim ret As New Models.BaseResult
            ret.AddError("Request requires a secure connection", Models.ErrorCodes.NotAuthorized)
            Return New ActionResult(HttpStatusCode.Unauthorized, ret)
        End Function

    End Class
End Namespace
