Imports System.Net
Imports System.Web.Http

Public Class ApplicationsController
    Inherits ApiController

    Private Shared Store As New List(Of Models.Application)

    ' GET applications/5
    Public Function GetApplication(ByVal id As Integer) As Models.Application
        Dim app As New Models.Application
        app.RequestedAmount = 20000
        app.Term = 60
        app.PaymentAmount = 512.45
        Return app
    End Function

    ' POST applications
    Public Sub PostApplication(<FromBody()> ByVal value As String)

    End Sub

    ' PUT applications/5
    Public Sub PutApplication(ByVal id As Integer, <FromBody()> ByVal value As String)

    End Sub

    ' DELETE applications/5
    Public Sub DeleteApplication(ByVal id As Integer)

    End Sub


    ' PATCH applications/5
    Public Sub PatchApplication(ByVal id As Integer)

    End Sub
End Class
