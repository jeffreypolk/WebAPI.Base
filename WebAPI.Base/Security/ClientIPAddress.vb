Imports System.Net

Namespace Security
    Public Class ClientIPAddress

        Public Shared Function GetIPAddress(Optional GetLan As Boolean = False) As String

            Dim context As System.Web.HttpContext = System.Web.HttpContext.Current
            Dim visitorIPAddress As String = context.Request.ServerVariables("HTTP_X_FORWARDED_FOR")


            If [String].IsNullOrEmpty(visitorIPAddress) Then
                visitorIPAddress = context.Request.ServerVariables("REMOTE_ADDR")
            End If

            If String.IsNullOrEmpty(visitorIPAddress) Then
                visitorIPAddress = context.Request.UserHostAddress
            End If

            If String.IsNullOrEmpty(visitorIPAddress) OrElse visitorIPAddress.Trim() = "::1" Then
                GetLan = True
                visitorIPAddress = String.Empty
            End If

            If GetLan AndAlso String.IsNullOrEmpty(visitorIPAddress) Then
                'This is for Local(LAN) Connected ID Address
                Dim stringHostName As String = Dns.GetHostName()
                'Get Ip Host Entry
                Dim ipHostEntries As IPHostEntry = Dns.GetHostEntry(stringHostName)
                'Get Ip Address From The Ip Host Entry Address List
                Dim arrIpAddress As IPAddress() = ipHostEntries.AddressList

                Try
                    visitorIPAddress = arrIpAddress(arrIpAddress.Length - 2).ToString()
                Catch
                    Try
                        visitorIPAddress = arrIpAddress(0).ToString()
                    Catch
                        Try
                            arrIpAddress = Dns.GetHostAddresses(stringHostName)
                            visitorIPAddress = arrIpAddress(0).ToString()
                        Catch
                            visitorIPAddress = "127.0.0.1"
                        End Try
                    End Try

                End Try
            End If
            Return visitorIPAddress
        End Function

    End Class
End Namespace

