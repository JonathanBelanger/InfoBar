Module modSkinEnumeration

  Public Sub Skinning_EnumerateSkins()
    SkinCollection.Clear()

    ' Add Default Skin
    Dim skinsDir As String = Application.StartupPath & "\Skins\"
    If Directory.Exists(skinsDir) = True Then
      Dim files() As String = Directory.GetFiles(skinsDir, "skin.xml", SearchOption.AllDirectories)
      Dim Doc As New XmlDocument
      For Each sFile As String In files
        Application.DoEvents()
        Try
          Doc.Load(sFile)
          Dim Info As New SkinInfo
          Info.Key = sFile
          Info.Name = Doc.DocumentElement.SelectSingleNode("/skin").Attributes("name").Value
          Info.Author = Doc.DocumentElement.SelectSingleNode("/skin").Attributes("author").Value
          Info.Version = Doc.DocumentElement.SelectSingleNode("/skin").Attributes("version").Value
          Info.Website = Doc.DocumentElement.SelectSingleNode("/skin").Attributes("website").Value
          Info.Path = sFile
          SkinCollection.Add(Info, sFile)
        Catch ex As Exception
        End Try
      Next
    End If

    If SkinCollection.Count = 0 Then
      MsgBox("There are no skins installed. InfoBar can not run without skins. Please install a skin first.")
      Application.Exit()
    End If
  End Sub

End Module
