Imports System
Imports System.IO
Imports System.Security.Cryptography

Namespace Security
    Public Class Encryption

        'TODO: make this a solution parameter
        Private Shared Key() As Byte = {250, 224, 148, 100, 179, 12, 115, 62, 93, 157, 118, 205, 239, 203, 76, 173, 219, 48, 34, 29, 255, 137, 64, 119, 122, 164, 120, 163, 82, 26, 189, 166}
        Private Shared IV() As Byte = {26, 194, 35, 99, 227, 17, 2, 194, 198, 113, 3, 35, 161, 180, 53, 191}

        Public Shared Function Encrypt(ByVal Data As String) As String

            Dim Ret As String


            ' Encrypt the string to an array of bytes.
            Ret = Convert.ToBase64String(EncryptStringToBytes(Data, Key, IV))

            Return Ret

        End Function

        Public Shared Function Decrypt(ByVal Data As String) As String

            Dim Ret As String

            ' Encrypt the string to an array of bytes.
            Ret = DecryptStringFromBytes(Convert.FromBase64String(Data), Key, IV)

            Return Ret

        End Function

        Private Shared Function EncryptStringToBytes(ByVal plainText As String, ByVal Key() As Byte, ByVal IV() As Byte) As Byte()
            ' Check arguments.
            If plainText Is Nothing OrElse plainText.Length <= 0 Then
                Throw New ArgumentNullException("plainText")
            End If
            If Key Is Nothing OrElse Key.Length <= 0 Then
                Throw New ArgumentNullException("Key")
            End If
            If IV Is Nothing OrElse IV.Length <= 0 Then
                Throw New ArgumentNullException("Key")
            End If
            Dim encrypted() As Byte
            ' Create an Aes object
            ' with the specified key and IV.
            Using aesAlg As Aes = Aes.Create()

                aesAlg.Key = Key
                aesAlg.IV = IV

                ' Create a decrytor to perform the stream transform.
                Dim encryptor As ICryptoTransform = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV)
                ' Create the streams used for encryption.
                Using msEncrypt As New MemoryStream()
                    Using csEncrypt As New CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write)
                        Using swEncrypt As New StreamWriter(csEncrypt)

                            'Write all data to the stream.
                            swEncrypt.Write(plainText)
                        End Using
                        encrypted = msEncrypt.ToArray()
                    End Using
                End Using
            End Using

            ' Return the encrypted bytes from the memory stream.
            Return encrypted

        End Function

        Private Shared Function DecryptStringFromBytes(ByVal cipherText() As Byte, ByVal Key() As Byte, ByVal IV() As Byte) As String
            ' Check arguments.
            If cipherText Is Nothing OrElse cipherText.Length <= 0 Then
                Throw New ArgumentNullException("cipherText")
            End If
            If Key Is Nothing OrElse Key.Length <= 0 Then
                Throw New ArgumentNullException("Key")
            End If
            If IV Is Nothing OrElse IV.Length <= 0 Then
                Throw New ArgumentNullException("Key")
            End If
            ' Declare the string used to hold
            ' the decrypted text.
            Dim plaintext As String = Nothing

            ' Create an Aes object
            ' with the specified key and IV.
            Using aesAlg As Aes = Aes.Create()
                aesAlg.Key = Key
                aesAlg.IV = IV

                ' Create a decrytor to perform the stream transform.
                Dim decryptor As ICryptoTransform = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV)

                ' Create the streams used for decryption.
                Using msDecrypt As New MemoryStream(cipherText)

                    Using csDecrypt As New CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read)

                        Using srDecrypt As New StreamReader(csDecrypt)


                            ' Read the decrypted bytes from the decrypting stream
                            ' and place them in a string.
                            plaintext = srDecrypt.ReadToEnd()
                        End Using
                    End Using
                End Using
            End Using

            Return plaintext

        End Function

    End Class
End Namespace
