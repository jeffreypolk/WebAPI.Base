Namespace Models
    Public Class BaseResult
        Property Result As Boolean = True
        Property Messages As New List(Of Message)
        Property ExceptionId As Integer

        Public Sub AddError(Text As String)
            Result = False
            Messages.Add(New Message(MessageType.Error, Text))
        End Sub
    End Class

    Public Class Message

        Public Property Type() As MessageType

        Public Property Text() As String = String.Empty

        Public Property Code() As String = String.Empty


        Public Sub New(ByVal MessageType As MessageType, ByVal MessageText As String)
            Me.New(MessageType, MessageText, String.Empty)
        End Sub

        Public Sub New(ByVal SourceMessage As Message)
            Me.New(SourceMessage.Type, SourceMessage.Text, SourceMessage.Code)
        End Sub

        Public Sub New(MessageType As MessageType, MessageText As String, MessageCode As String)
            With Me
                .Type = MessageType
                .Text = MessageText
                .Code = MessageCode
            End With
        End Sub

    End Class

    Public Enum MessageType
        [Error] = 0
        Warning = 1
        Success = 2
        Information = 3
    End Enum

End Namespace