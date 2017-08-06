
Public Class Settings

    Private Shared _RequireSSL As Boolean?
    Private Shared _TokenSigningSecret As String
    Private Shared _TokenExpireSeconds As Integer?

    Public Shared ReadOnly Property RequireSSL As Boolean
        Get
            If _RequireSSL.HasValue = False Then
                _RequireSSL = System.Configuration.ConfigurationManager.AppSettings("RequireSSL")
            End If
            Return _RequireSSL.Value
        End Get
    End Property

    Public Shared ReadOnly Property TokenSigningSecret As String
        Get
            If String.IsNullOrEmpty(_TokenSigningSecret) Then
                _TokenSigningSecret = "ltYvCGSBmIFlZAOdhWL88tuzHNVz4MKaCgzaSdSv4H9Mv9b95v69k4FOh5wPXhvkbinPzL5jiXZBPALGHIa5Do8FPDbznqo9uYSREeucKitXjXsvhgdib9LbX2bzuBzD"
            End If
            Return _TokenSigningSecret
        End Get
    End Property

    Public Shared ReadOnly Property TokenExpireSeconds As Integer
        Get
            If _TokenExpireSeconds.HasValue = False Then
                _TokenExpireSeconds = 60 * 15
            End If
            Return _TokenExpireSeconds
        End Get
    End Property

End Class


