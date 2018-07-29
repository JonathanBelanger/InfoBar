Module modModules

  Public ModulePaths As New Collection
  Public AvailableModules As New Collection

  Public Sub Modules_EnumerateModules()
    ModulePaths.Clear()
    If Directory.Exists(Application.StartupPath & "\Modules\") = False Then Directory.CreateDirectory(Application.StartupPath & "\Modules\")
    Dim files() As String = System.IO.Directory.GetFiles(Application.StartupPath & "\Modules\", "*.dll", SearchOption.AllDirectories)
    Array.Sort(files)
    For Each file As String In files
      Try
        AppDomain.CurrentDomain.AppendPrivatePath(Path.GetDirectoryName(file))
        Dim assemblyBytes() As Byte = IO.File.ReadAllBytes(file)
        Dim assembly As Reflection.Assembly = AppDomain.CurrentDomain.Load(assemblyBytes)
        Dim assemblySimpleName As String = assembly.FullName.Split(", ")(0)
        Dim instance As InfoBarModule = assembly.CreateInstance(assemblySimpleName & ".InfoBarModuleMain")
        If instance IsNot Nothing Then
          If AvailableModules.Contains(instance.ModuleGUID) = False Then
            AvailableModules.Add(instance, instance.ModuleGUID)
            ModulePaths.Add(Path.GetDirectoryName(file), instance.ModuleGUID)
          End If
        End If
      Catch ex As Exception
        Debug.Print(file & vbCrLf & ex.ToString)
      End Try
    Next
  End Sub

  Public Sub Modules_Delete(ByVal ModuleGUID As String)
    Dim sPath As String = ModulePaths(ModuleGUID)
    If Directory.Exists(sPath) Then
      Try
        Directory.Delete(sPath, True)
      Catch ex As Exception
        MsgBox("The module could not be deleted.")
      End Try
    End If
  End Sub

  Public Sub Modules_Delete_Finalize()
    fMain.tmrUpdate.Stop()

    ' Finalize Modules
    For Each sModule As SelectedModuleType In IBSettings.SelectedModules
      AvailableModules(sModule.GUID).FinalizeModule()
      AvailableModules(sModule.GUID).SettingsDialog.Dispose()
    Next
    If frmSettings.Visible Then
      frmSettings.panelModuleSettings.Controls.Clear()
    End If

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
  End Sub

End Module
