Namespace Models
    Public Class TokenResult
        Inherits BaseResult
        Property Token As String
        Public Shared Function InvalidAPIKey() As TokenResult
            Dim ret As New TokenResult
            ret.AddError("Invalid API Key")
            ret.Messages(0).Code = ErrorCodes.NotAuthorized
            Return ret
        End Function
    End Class
End Namespace