Namespace Security
    Public Class APIKeys

        Private Shared _Keys As Concurrent.ConcurrentDictionary(Of String, Models.APIKey)

        Public Shared ReadOnly Property Keys As Concurrent.ConcurrentDictionary(Of String, Models.APIKey)
            Get
                If _Keys Is Nothing Then
                    LoadKeys()
                End If
                Return _Keys
            End Get
        End Property

        Public Shared Sub LoadKeys()
            'TEMP_BuildKey()



            'TODO: use the core security service to load all API Keys from LMS



            If _Keys Is Nothing Then
                _Keys = New Concurrent.ConcurrentDictionary(Of String, Models.APIKey)
            End If
            Dim key As New Models.APIKey
            key.IPAddresses.Add("zzzfe80::29b3:51de:ad6e:9ea0%12")
            key.IPAddresses.Add("f*e80::29b3:51de:ad6e:9ea0%12")

            key.InstallationKeys.Add(New Guid("876172af-03db-46a8-829a-1f8aa07995f2"))
            _Keys.TryAdd("2nRfC4Rpz6FJ4O/TJni/MMwVqNnpfWyWKhT0aMK9CXE3HB8WnJ8vW7kqX72URtVfmHOl7Hm/t8hUs3RvvI442NMrVR9H0sXcRwnBXdfBxvKNaHvN5YjFU7JkdwmeruZHDWk3XSU8Nch/t8yT5VjzZw==", key)


            'add the univeral key
            key = New Models.APIKey
            key.IPAddresses.Add("*")
            key.InstallationKeys.Add(Guid.Empty)
            _Keys.TryAdd("2nRfC4Rpz6FJ4O/TJni/MKvekLNsLIIeCu6qEBh9438cVi63I6eJK6dIRWjUX7N+aTMzxAOEPC/zjRW1TBoLoNGeATmnj4KhkVD9t7zuxpQv7SEcvq0rToW1nmXR5fJf", key)


        End Sub

        Public Shared Sub TEMP_BuildKey()
            Dim key As New Models.APIKey

            'key.IPAddresses.Add("fe80::29b3:51de:ad6e:9ea0%12")
            'key.InstallationKeys.Add(New Guid("876172af-03db-46a8-829a-1f8aa07995f2"))


            'the global, allow all key!
            key.IPAddresses.Add("*")
            key.InstallationKeys.Add(Guid.Empty)


            Dim json As String = Newtonsoft.Json.JsonConvert.SerializeObject(key)
            Dim jsonEnc As String = Security.Encryption.Encrypt(json)

        End Sub
    End Class
End Namespace
