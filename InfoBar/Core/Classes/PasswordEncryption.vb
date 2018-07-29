Friend Class PasswordEncryption

  Private Shared key() As Byte = {23, 67, 123, 97, 53, 56, 93, 30, 42, 12, 74, 190, 34, 46, 37, 238, 238, 128, 92, 27, 79, 125, 239, 29}
  Private Shared iv() As Byte = {65, 110, 68, 26, 69, 178, 200, 219}

  Public Shared Function Encrypt(ByVal s As String) As String
    If (s Is Nothing) Or (s = vbNullString) Or (s = " ") Then Return vbNullString
    Try
      Dim utf8encoder As UTF8Encoding = New UTF8Encoding()
      Dim inputInBytes() As Byte = utf8encoder.GetBytes(s)
      Dim tdesProvider As Security.Cryptography.TripleDESCryptoServiceProvider = New Security.Cryptography.TripleDESCryptoServiceProvider()
      Dim cryptoTransform As Security.Cryptography.ICryptoTransform = tdesProvider.CreateEncryptor(key, iv)
      Dim encryptedStream As MemoryStream = New MemoryStream()
      Dim cryptStream As Security.Cryptography.CryptoStream = New Security.Cryptography.CryptoStream(encryptedStream, cryptoTransform, Security.Cryptography.CryptoStreamMode.Write)
      cryptStream.Write(inputInBytes, 0, inputInBytes.Length)
      cryptStream.FlushFinalBlock()
      encryptedStream.Position = 0
      Dim result(encryptedStream.Length - 1) As Byte
      encryptedStream.Read(result, 0, encryptedStream.Length)
      cryptStream.Close()
      Return System.Convert.ToBase64String(result)
    Catch
      Return vbNullString
    End Try
  End Function

  Public Shared Function Decrypt(ByVal s As String) As String
    If (s Is Nothing) Or (s = vbNullString) Or (s = " ") Then Return vbNullString
    Try
      Dim utf8encoder As UTF8Encoding = New UTF8Encoding()
      Dim inputBytes As Byte() = System.Convert.FromBase64String(s)
      Dim tdesProvider As Security.Cryptography.TripleDESCryptoServiceProvider = New Security.Cryptography.TripleDESCryptoServiceProvider()
      Dim cryptoTransform As Security.Cryptography.ICryptoTransform = tdesProvider.CreateDecryptor(key, iv)
      Dim decryptedStream As MemoryStream = New MemoryStream()
      Dim cryptStream As Security.Cryptography.CryptoStream = New Security.Cryptography.CryptoStream(decryptedStream, cryptoTransform, Security.Cryptography.CryptoStreamMode.Write)
      cryptStream.Write(inputBytes, 0, inputBytes.Length)
      cryptStream.FlushFinalBlock()
      decryptedStream.Position = 0
      Dim result(decryptedStream.Length - 1) As Byte
      decryptedStream.Read(result, 0, decryptedStream.Length)
      cryptStream.Close()
      Return utf8encoder.GetString(result)
    Catch
      Return vbNullString
    End Try
  End Function

End Class
