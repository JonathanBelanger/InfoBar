Module modPackageProcessing

  Public bInfoBarLoaded As Boolean
  Public bPostProcessShowMessage As Integer

  Public Sub ProcessModulePackage(ByVal sFile As String)
    If File.Exists(sFile) Then
      Dim sFolderName As String = sFile
      sFolderName = sFolderName.Substring(sFolderName.LastIndexOf("\") + 1)
      sFolderName = sFolderName.Substring(0, sFolderName.LastIndexOf("."))
      Dim modulesDir As String = Application.StartupPath & "\Modules\" & sFolderName & "\"

      If ProcessPackage(sFile, modulesDir) Then
        If bInfoBarLoaded Then
          fMain.tmrUpdate.Stop()

          ' Finalize Modules
          For Each sModule As SelectedModuleType In IBSettings.SelectedModules
            AvailableModules(sModule.GUID).FinalizeModule()
          Next

          CurrentTooltipOwnerGUID = vbNullString
          CurrentTooltipOwnerObjectID = vbNullString

          ' Save Settings
          Settings_SaveToXML()

          IBSettings.SelectedModules.Clear()
          AvailableModules.Clear()

          ' Enumerate Modules
          Modules_EnumerateModules()

          ' Load Settings
          Settings_CheckRunAtWindowsStartup()
          Settings_LoadFromXML()

          ' Load Module Settings
          Settings_LoadModuleSettings()
          frmSettings.BuildModulesList()

          ' Initialize Modules
          For Each sModule As SelectedModuleType In IBSettings.SelectedModules
            Application.DoEvents()
            If AvailableModules.Contains(sModule.GUID) = True Then
              AvailableModules(sModule.GUID).ModuleEnabled = True
              AvailableModules(sModule.GUID).InitializeModule()
            End If
          Next

          ' Update Window
          Skinning_UpdateWindow()

          fMain.tmrUpdate.Start()

          With fMain.tiMain
            .BalloonTipIcon = ToolTipIcon.Info
            .BalloonTipText = "Click here to open the InfoBar settings dialog to enable the new module."
            .BalloonTipTitle = "New Module Installed"
            .Visible = True
            .ShowBalloonTip(10000)
          End With
        Else
          bPostProcessShowMessage = 1
        End If

      Else
        MsgBox("InfoBar could not install the following module package: " & vbCrLf & vbCrLf & sFile, MsgBoxStyle.Exclamation Or MsgBoxStyle.OkOnly, "InfoBar")
      End If

    End If
  End Sub

  Public Sub ProcessSkinPackage(ByVal sFile As String)
    If File.Exists(sFile) Then
      Dim skinsDir As String = Application.StartupPath & "\Skins\"
      If ProcessPackage(sFile, skinsDir) Then
        If bInfoBarLoaded Then
          Skinning_EnumerateSkins()

          With fMain.tiMain
            .BalloonTipIcon = ToolTipIcon.Info
            .BalloonTipText = "Click here to open the InfoBar settings dialog to choose the new skin."
            .BalloonTipTitle = "New Skin Installed"
            .Visible = True
            .ShowBalloonTip(10000)
          End With
        Else
          bPostProcessShowMessage = 2
        End If
      Else
        MsgBox("InfoBar could not install the following skin package: " & vbCrLf & vbCrLf & sFile, MsgBoxStyle.Exclamation Or MsgBoxStyle.OkOnly, "InfoBar")
      End If

    End If
  End Sub

  Public Sub ProcessIconPackage(ByVal sFile As String)
    If File.Exists(sFile) Then
      Dim iconsDir As String = Application.StartupPath & "\Icons\"
      If ProcessPackage(sFile, iconsDir) Then
        If bInfoBarLoaded Then
          Icons_EnumerateThemes()

          With fMain.tiMain
            .BalloonTipIcon = ToolTipIcon.Info
            .BalloonTipText = "Click here to apply this icon theme now."
            .BalloonTipTitle = "New Icon Theme Installed"
            .Visible = True
            .ShowBalloonTip(10000)
          End With
        Else
          bPostProcessShowMessage = 3
        End If

      Else
        MsgBox("InfoBar could not install the following icon theme package: " & vbCrLf & vbCrLf & sFile, MsgBoxStyle.Exclamation Or MsgBoxStyle.OkOnly, "InfoBar")
      End If

    End If
  End Sub

  Private Function ProcessPackage(ByVal sFile As String, ByVal sDestination As String) As Boolean
    sDestination = PreparePath(sDestination)
    If Directory.Exists(sDestination) = False Then Directory.CreateDirectory(sDestination)
    Try
      Dim zFile As ZipFile = New ZipFile(sFile)
      Dim OutStream As System.IO.FileStream
      For Each zEntry As ZipEntry In zFile
        If zEntry.IsDirectory AndAlso Directory.Exists(sDestination & zEntry.Name) = False Then Directory.CreateDirectory(sDestination & zEntry.Name)       
        If zEntry.IsFile = True Then
          If File.Exists(sDestination & zEntry.Name) Then File.Delete(sDestination & zEntry.Name)
          Dim InStream As System.IO.Stream = zFile.GetInputStream(zEntry)
          OutStream = New System.IO.FileStream(sDestination & zEntry.Name, FileMode.Create, FileAccess.ReadWrite, FileShare.ReadWrite, 4096)
          Dim data(4096) As Byte
          Dim readSize As Integer
          Dim dataIndex As Integer
          While True
            readSize = InStream.Read(data, 0, data.Length)
            dataIndex += readSize
            If readSize > 0 Then
              OutStream.Write(data, 0, readSize)
            Else
              Exit While
            End If
          End While
          OutStream.Close()
          InStream.Close()
        End If
      Next
      zFile.Close()
      Return True
    Catch ex As ZipException
      Return False
    Catch ex As Exception
      Return False
    End Try
  End Function

End Module
