Imports System.Net.Http.Headers
Imports System.Web.Http
Imports System.Web.Http.Controllers
Imports System.Web.Http.Results

Namespace Controllers
    Public Class BaseController
        Inherits ApiController

        Protected Overrides Sub Initialize(controllerContext As HttpControllerContext)
            MyBase.Initialize(controllerContext)

        End Sub

        Protected Overrides Function Unauthorized(challenges As IEnumerable(Of AuthenticationHeaderValue)) As UnauthorizedResult
            Return MyBase.Unauthorized(challenges)
        End Function
    End Class
End Namespace