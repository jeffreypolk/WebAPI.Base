Imports System.Web.Http

Public Class WebApiApplication
    Inherits System.Web.HttpApplication

    Sub Application_Start()
        GlobalConfiguration.Configure(AddressOf WebApiConfig.Register)

        'get the keys loaded
        Security.APIKeys.LoadKeys()

    End Sub

    Sub Application_PreSendRequestHeaders(ByVal sender As Object, ByVal e As EventArgs)
        ' Remove the "Server" HTTP Header from response
        Dim app As HttpApplication = sender
        If Not IsNothing(app) AndAlso Not IsNothing(app.Request) AndAlso Not IsNothing(app.Context) AndAlso Not IsNothing(app.Context.Response) Then
            Dim headers As NameValueCollection = app.Context.Response.Headers
            If Not IsNothing(headers) Then
                headers.Remove("Server")
            End If
        End If
    End Sub

End Class
