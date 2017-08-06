Imports System.Web.Http
Imports System.Web.Http.Controllers

Namespace Controllers
    Public Class BaseController
        Inherits ApiController

        Protected Overrides Sub Initialize(controllerContext As HttpControllerContext)
            MyBase.Initialize(controllerContext)

        End Sub
    End Class
End Namespace