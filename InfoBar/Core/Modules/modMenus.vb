Module modMenus

  Public Sub Menus_ShowMainMenu()

    fMain.Cursor = Cursors.WaitCursor
    fTooltip.HideTooltip(True)
    fMain.mnuInfoBar.Items.Clear()

    RightClickedModule = vbNullString
    If IBSettings.SelectedModules.Count > 0 Then
      For Each sModule As SelectedModuleType In IBSettings.SelectedModules
        Dim iMod As InfoBarModule = AvailableModules(sModule.GUID)
        If iMod.ModuleBounds.Contains(Cursor.Position) Then
          iMod.AddMainPopupMenuItems()
          RightClickedModule = sModule.GUID
          Exit For
        End If
      Next
    End If

    Dim mi As ToolStripMenuItem = Nothing
    Dim sep As ToolStripSeparator = Nothing
    With fMain.mnuInfoBar.Items

      Dim bHaveDefault As Boolean = False
      For Each mi In fMain.mnuInfoBar.Items
        If mi.Tag = "DEFAULT" Then
          bHaveDefault = True
          Exit For
        End If
      Next

      mi = New ToolStripMenuItem
      mi.Name = "mnuInfoBar_Settings"
      mi.Text = "&Settings..."
      If bHaveDefault = False Then mi.Tag = "DEFAULT"
      mi.Image = CurrentIconTheme.Icons("InfoBarInternal::Settings").Icon
      .Add(mi)

      sep = New ToolStripSeparator
      sep.Name = "mnuInfoBar_Sep01"
      .Add(sep)

      mi = New ToolStripMenuItem
      mi.Name = "mnuInfoBar_AutoHide"
      mi.Text = "&Autohide"
      mi.CheckOnClick = True
      mi.Checked = IBSettings.General.AutoHide
      .Add(mi)

      mi = New ToolStripMenuItem
      mi.Name = "mnuInfoBar_Minimize"
      mi.Text = "&Minimize"
      mi.Image = CurrentIconTheme.Icons("InfoBarInternal::Minimize").Icon
      .Add(mi)

      mi = New ToolStripMenuItem
      mi.Name = "mnuInfoBar_Exit"
      mi.Text = "E&xit"
      mi.Image = CurrentIconTheme.Icons("InfoBarInternal::Close").Icon
      .Add(mi)
    End With

    SetForegroundWindow(fMain.Handle)
    fMain.Cursor = Cursors.Default
    fMain.mnuInfoBar.Show(fMain, Cursor.Position)
  End Sub

End Module