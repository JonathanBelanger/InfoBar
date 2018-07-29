Module modSkinEvents

  Private PrevMouseMoveLocation As Point

  Public Sub Skinning_ProcessMouseMove(ByVal e As System.Windows.Forms.MouseEventArgs)
    If e.Location <> PrevMouseMoveLocation Then
      PrevMouseMoveLocation = e.Location
    Else
      Exit Sub
    End If

    Dim bWindowIsDirty As Boolean = False
    NewTooltipOwnerGUID = vbNullString
    NewTooltipOwnerObjectID = vbNullString
    Dim bDirty As Boolean = False
    For Each sModule As SelectedModuleType In IBSettings.SelectedModules
      bDirty = False
      Dim iMod As InfoBarModule = AvailableModules(sModule.GUID)
      If iMod.ModuleBounds.Contains(e.Location) Then
        iMod.ProcessMouseMove(e, bDirty)
      Else
        iMod.ProcessMouseLeave(bDirty)
      End If
      If bDirty Then bWindowIsDirty = True
    Next

    ' Update Window
    If bWindowIsDirty Then Skinning_UpdateWindow()

    ' Hide tooltip if not used
    If (NewTooltipOwnerGUID = vbNullString) AndAlso (NewTooltipOwnerObjectID = vbNullString) Then
      fTooltip.HideTooltip(False)
    Else
      fTooltip.ShowTooltip()
    End If
  End Sub

  Public Sub Skinning_ProcessMouseUp(ByVal e As System.Windows.Forms.MouseEventArgs)
    Dim bWindowIsDirty As Boolean = False

    For Each sModule As SelectedModuleType In IBSettings.SelectedModules
      Dim iMod As InfoBarModule = AvailableModules(sModule.GUID)
      If iMod.ModuleBounds.Contains(e.Location) Then
        iMod.ProcessMouseUp(e, bWindowIsDirty)
      End If
    Next

    ' Update Window
    If bWindowIsDirty Then Skinning_UpdateWindow()

    If e.Button = MouseButtons.Right Then Menus_ShowMainMenu()
  End Sub

  Public Sub Skinning_ProcessMouseLeave()
    fTooltip.HideTooltip(False)

    Dim bWindowIsDirty As Boolean = False
    Dim bDirty As Boolean = False
    For Each sModule As SelectedModuleType In IBSettings.SelectedModules
      bDirty = False
      Dim iMod As InfoBarModule = AvailableModules(sModule.GUID)
      iMod.ProcessMouseLeave(bDirty)
      If bDirty Then bWindowIsDirty = True
    Next

    If bWindowIsDirty Then Skinning_UpdateWindow()
  End Sub

  Public Sub Skinning_ProcessMouseDown(ByVal e As System.Windows.Forms.MouseEventArgs)
    If e.Button = MouseButtons.Left Then
      Dim bWindowIsDirty As Boolean = False

      For Each sModule As SelectedModuleType In IBSettings.SelectedModules
        Dim iMod As InfoBarModule = AvailableModules(sModule.GUID)
        If iMod.ModuleBounds.Contains(e.Location) Then
          iMod.ProcessMouseDown(e, bWindowIsDirty)
        End If
      Next

      If bWindowIsDirty Then Skinning_UpdateWindow()
    End If
  End Sub

  Public Sub Skinning_ProcessDragDrop(ByVal e As System.Windows.Forms.DragEventArgs)
    Dim bWindowIsDirty As Boolean = False
    For Each sModule As SelectedModuleType In IBSettings.SelectedModules
      Dim iMod As InfoBarModule = AvailableModules(sModule.GUID)
      iMod.ProcessDragDrop(e, bWindowIsDirty)
    Next

    If bWindowIsDirty Then Skinning_UpdateWindow()
  End Sub

  Public Sub Skinning_ProcessDragEnter(ByVal e As System.Windows.Forms.DragEventArgs)
    Dim bWindowIsDirty As Boolean = False
    For Each sModule As SelectedModuleType In IBSettings.SelectedModules
      Dim iMod As InfoBarModule = AvailableModules(sModule.GUID)
      iMod.ProcessDragEnter(e, bWindowIsDirty)
    Next

    If bWindowIsDirty Then Skinning_UpdateWindow()
  End Sub

  Public Sub Skinning_ProcessDragLeave(ByVal e As System.EventArgs)
    Dim bWindowIsDirty As Boolean = False
    For Each sModule As SelectedModuleType In IBSettings.SelectedModules
      Dim iMod As InfoBarModule = AvailableModules(sModule.GUID)
      iMod.ProcessDragLeave(e, bWindowIsDirty)
    Next

    If bWindowIsDirty Then Skinning_UpdateWindow()
  End Sub

  Public Sub Skinning_ProcessDragOver(ByVal e As System.Windows.Forms.DragEventArgs)
    Dim bWindowIsDirty As Boolean = False
    For Each sModule As SelectedModuleType In IBSettings.SelectedModules
      Dim iMod As InfoBarModule = AvailableModules(sModule.GUID)
      iMod.ProcessDragOver(e, bWindowIsDirty)
    Next

    If bWindowIsDirty Then Skinning_UpdateWindow()
  End Sub

End Module
