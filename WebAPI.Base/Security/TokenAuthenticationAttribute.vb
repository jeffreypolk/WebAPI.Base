Imports System.Threading
Imports System.Threading.Tasks
Imports System.Web.Http.Filters

Namespace Security
    Public Class TokenAuthenticationAttribute
        Inherits System.Attribute
        Implements System.Web.Http.Filters.IAuthenticationFilter

        Public ReadOnly Property AllowMultiple As Boolean Implements IFilter.AllowMultiple
            Get
                Throw New NotImplementedException()
            End Get
        End Property

        Public Function AuthenticateAsync(context As HttpAuthenticationContext, cancellationToken As CancellationToken) As Task Implements IAuthenticationFilter.AuthenticateAsync

            Dim principal As System.Security.Claims.ClaimsPrincipal = Nothing
            Dim authorization As Net.Http.Headers.AuthenticationHeaderValue = context.Request.Headers.Authorization
            If authorization IsNot Nothing Then
                If authorization.Scheme IsNot Nothing Then
                    If authorization.Parameter IsNot Nothing Then
                        If authorization.Scheme.ToLower = "bearer" Then
                            Dim mgr As New Security.TokenManager()
                            Dim Token As String = authorization.Parameter
                            principal = mgr.GetPrincipal(Token)
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

        Private Function GetTokenErrorResult(context As HttpAuthenticationContext) As TokenErrorResult
            Dim ret As New TokenErrorResult("unauthorized", context.Request, New With {
                 .[Error] = New With {
                    .Code = 401,
                    .Message = "Request requires authorization"
                }
            })
            Return ret
        End Function
    End Class
End Namespace
