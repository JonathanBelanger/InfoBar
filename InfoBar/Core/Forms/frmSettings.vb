Friend Class frmSettings

#Region "Main Form Events"

  Public DefaultVisibleModule As String

  Private ModulesToDelete As New Collections.Generic.List(Of String)

  Private Sub frmSettings_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    ' TURN OFF FOR TESTING
    radGeneral_Location_Left.Visible = False
    radGeneral_Location_Right.Visible = False
    ' END TURN OFF FOR TESTING

    frmSettings_SystemColorsChanged(sender, e)
    Me.CenterToScreen()

    Dim LI As ListViewItem

    ' General Settings
    Select Case IBSettings.General.Position
      Case ABEdge.ABE_TOP
        radGeneral_Location_Top.Checked = True
      Case ABEdge.ABE_BOTTOM
        radGeneral_Location_Bottom.Checked = True
      Case ABEdge.ABE_LEFT
        radGeneral_Location_Left.Checked = True
      Case ABEdge.ABE_RIGHT
        radGeneral_Location_Right.Checked = True
    End Select

    Select Case IBSettings.General.ModuleAlignment
      Case ModuleAlignment.Left
        radModuleAlignmentLeft.Checked = True
      Case ModuleAlignment.Center
        radModuleAlignmentCenter.Checked = True
      Case ModuleAlignment.Right
        radModuleAlignmentRight.Checked = True
      Case ModuleAlignment.Justify
        radModuleAlignmentJustify.Checked = True
      Case ModuleAlignment.JustifyCenter
        radModuleAlignmentJustifyCenter.Checked = True
    End Select

    chkGeneral_Location_AlwaysOnTop.Checked = IBSettings.General.AlwaysOnTop
    chkAutohide.Checked = IBSettings.General.AutoHide
    chkDisableToolbarDocking.Checked = IBSettings.General.DisableToolbarDocking
    chkGeneral_Startup_RunAtWindowsStartup.Checked = IBSettings.General.RunAtWindowsStartup
    chkShowSeparators.Checked = IBSettings.General.ShowSeparators

    chkEnableTooltipFade.Checked = IBSettings.Advanced.EnableTooltipFade
    chkOverrideSkinBackgroundOpacity.Checked = IBSettings.Advanced.OverrideBackgroundOpacity
    nudSkinBGOpacity.Value = IBSettings.Advanced.BackgroundOpacity
    nudAutohideDelay.Value = IBSettings.Advanced.AutohideDelay
    chkAutoHideAnimation.Checked = IBSettings.Advanced.AutohideAnimation
    nudAutohideAnimationSpeed.Value = IBSettings.Advanced.AutohideAnimationSpeed
    chkAutohide_IgnoreMaximizedState.Checked = IBSettings.Advanced.AutohideIgnoreMaximizedState

    BuildModulesList()

    grpModuleInfo.Text = vbNullString
    txtModuleDescription.Text = vbNullString
    txtModuleAuthor.Text = vbNullString
    txtModuleVersion.Text = vbNullString
    lblModuleEmail.Text = vbNullString
    lblModuleHomepage.Text = vbNullString
    txtModuleGUID.Text = vbNullString

    ' Skins
    For Each SI As SkinInfo In SkinCollection
      LI = New ListViewItem
      LI.Name = "IBSKIN_" & SI.Key
      LI.Text = SI.Name
      LI.SubItems.Add(SI.Author)
      LI.SubItems.Add(SI.Version)
      LI.SubItems.Add(SI.Website)
      lvSkins.Items.Add(LI)
    Next
    lvSkins.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize)
    lvSkins.Items("IBSKIN_" & IBSettings.CurrentSkin).Selected = True

    ' Icons
    For Each ITI As IconThemeInfo In IconThemeCollection
      LI = New ListViewItem
      LI.Name = "IBICON_" & ITI.Name
      LI.Text = ITI.Title
      LI.SubItems.Add(ITI.Author)
      LI.SubItems.Add(ITI.Version)
      LI.SubItems.Add(ITI.Website)
      lvIcons.Items.Add(LI)
    Next
    lvIcons.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize)
    lvIcons.Items("IBICON_" & IBSettings.CurrentIconTheme).Selected = True

    ' About
    lblVersion.Text = String.Format("Version {0}.{1}.{2} Build {3}", Application.ProductVersion.Split("."))

    GetFileVersionInformation()

    If CheckFileAssociations() = True Then
      lblFileAssociationStatus.Text = "Status: One or more file associations need repair."
      btnRepairFileAssociations.Enabled = True
    Else
      lblFileAssociationStatus.Text = "Status: OK."
    End If

    If DefaultVisibleModule <> vbNullString Then
      tvSettings.SelectedNode = tvSettings.Nodes("MODULES").Nodes(DefaultVisibleModule)
    Else
      tvSettings.SelectedNode = tvSettings.Nodes("GENERAL")
    End If

    btnApply.Enabled = False
  End Sub

  Private Sub btnApply_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnApply.Click

    btnApply.Enabled = False

    ' General Settings
    If IBSettings.General.DisableToolbarDocking <> chkDisableToolbarDocking.Checked Then
      IBSettings.General.DisableToolbarDocking = chkDisableToolbarDocking.Checked
      ABHideBar(chkDisableToolbarDocking.Checked)
    End If

    Dim bNewPos As Boolean = False, bNewOnTop As Boolean = False
    Select Case True
      Case radGeneral_Location_Top.Checked
        If IBSettings.General.Position <> ABEdge.ABE_TOP Then bNewPos = True
        IBSettings.General.Position = ABEdge.ABE_TOP
      Case radGeneral_Location_Bottom.Checked
        If IBSettings.General.Position <> ABEdge.ABE_BOTTOM Then bNewPos = True
        IBSettings.General.Position = ABEdge.ABE_BOTTOM
      Case radGeneral_Location_Left.Checked
        If IBSettings.General.Position <> ABEdge.ABE_LEFT Then bNewPos = True
        IBSettings.General.Position = ABEdge.ABE_LEFT
      Case radGeneral_Location_Right.Checked
        If IBSettings.General.Position <> ABEdge.ABE_RIGHT Then bNewPos = True
        IBSettings.General.Position = ABEdge.ABE_RIGHT
    End Select
    If bNewPos Then ABSetPos(IBSettings.General.Position)

    If IBSettings.General.DisableToolbarDocking = True Then

      If IBSettings.General.AutoHide = True Then
        IBSettings.General.AutoHide = False
        fMain.tmrAutohideAnimation.Stop()
        fMain.tmrAutohide.Stop()
        ABSetAutoHide(False)
        SetTopMost(fMain.Handle, IBSettings.General.AlwaysOnTop)
      End If

    Else

      If IBSettings.General.AutoHide <> chkAutohide.Checked Then
        ABSetAutoHide(chkAutohide.Checked)
        IBSettings.General.AutoHide = chkAutohide.Checked
        If IBSettings.General.AutoHide Then
          fMain.tmrAutohide.Start()
          ABSetAutoHide(True)
          SetTopMost(fMain.Handle, True)
        Else
          fMain.tmrAutohideAnimation.Stop()
          fMain.tmrAutohide.Stop()
          ABSetAutoHide(False)
          SetTopMost(fMain.Handle, IBSettings.General.AlwaysOnTop)
        End If
      End If

      If IBSettings.Advanced.AutohideIgnoreMaximizedState <> chkAutohide_IgnoreMaximizedState.Checked Then
        IBSettings.Advanced.AutohideIgnoreMaximizedState = chkAutohide_IgnoreMaximizedState.Checked
        ToggleBlur(CurrentSkinInfo.Background.EnableGlass)
      End If

    End If

    If IBSettings.General.AlwaysOnTop <> chkGeneral_Location_AlwaysOnTop.Checked Then
      IBSettings.General.AlwaysOnTop = chkGeneral_Location_AlwaysOnTop.Checked
      SetTopMost(fMain.Handle, IBSettings.General.AlwaysOnTop)
    End If

    Dim malign As ModuleAlignment
    Select Case True
      Case radModuleAlignmentLeft.Checked
        malign = ModuleAlignment.Left
      Case radModuleAlignmentCenter.Checked
        malign = ModuleAlignment.Center
      Case radModuleAlignmentRight.Checked
        malign = ModuleAlignment.Right
      Case radModuleAlignmentJustify.Checked
        malign = ModuleAlignment.Justify
      Case radModuleAlignmentJustifyCenter.Checked
        malign = ModuleAlignment.JustifyCenter
    End Select
    IBSettings.General.ModuleAlignment = malign

    If IBSettings.General.RunAtWindowsStartup <> chkGeneral_Startup_RunAtWindowsStartup.Checked Then
      IBSettings.General.RunAtWindowsStartup = chkGeneral_Startup_RunAtWindowsStartup.Checked
      Settings_SetRunAtWindowsStartup(IBSettings.General.RunAtWindowsStartup)
    End If

    IBSettings.General.ShowSeparators = chkShowSeparators.Checked

    IBSettings.Advanced.EnableTooltipFade = chkEnableTooltipFade.Checked
    IBSettings.Advanced.OverrideBackgroundOpacity = chkOverrideSkinBackgroundOpacity.Checked
    IBSettings.Advanced.BackgroundOpacity = nudSkinBGOpacity.Value
    IBSettings.Advanced.AutohideDelay = nudAutohideDelay.Value
    fMain.tmrAutohide.Interval = IBSettings.Advanced.AutohideDelay
    IBSettings.Advanced.AutohideAnimation = chkAutoHideAnimation.Checked
    IBSettings.Advanced.AutohideAnimationSpeed = nudAutohideAnimationSpeed.Value

    ' Skins
    Dim sSkinName As String = lvSkins.SelectedItems(0).Name.Replace("IBSKIN_", "")
    IBSettings.CurrentSkin = sSkinName
    Skinning_LoadSkin()
    MainForm_backBitmapSize = New Size(0, 0)
    Skinning_UpdateWindow()

    ' Icons
    Dim sIconName As String = lvIcons.SelectedItems(0).Name.Replace("IBICON_", "")
    IBSettings.CurrentIconTheme = sIconName
    Icons_LoadIconTheme()

    ' Modules
    IBSettings.SelectedModules.Clear()
    SkinUpdateInProgress = True
    Dim iRow As Integer = 1
    For Each li As ListViewItem In lvModules.Items
      If li.Text = "Row Separator" Then
        iRow += 1
      Else
        If li.Checked = True Then
          Dim curMod As InfoBarModule = AvailableModules(li.Name)
          If curMod.ModuleEnabled = False Then
            curMod.InitializeModule()
            AvailableModules(li.Name).ModuleEnabled = True
          End If
          curMod.ApplySettings()

          Dim selMod As New SelectedModuleType
          selMod.GUID = li.Name
          selMod.Row = iRow
          IBSettings.SelectedModules.Add(selMod)
        Else
          Dim curMod As InfoBarModule = AvailableModules(li.Name)
          If curMod.ModuleEnabled = True Then
            AvailableModules(li.Name).ModuleEnabled = False
            curMod.ApplySettings()
            curMod.FinalizeModule()
          End If
        End If
      End If
    Next
    IBSettings.General.Rows = iRow
    SkinUpdateInProgress = False
    Skinning_UpdateWindow()

    If ModulesToDelete.Count > 0 Then
      For Each sGUID As String In ModulesToDelete
        Modules_Delete(sGUID)
      Next
      Modules_Delete_Finalize()
      ModulesToDelete.Clear()
      BuildModulesList()
      btnApply.Enabled = False
    End If
  End Sub

  Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
    Close()
  End Sub

  Private Sub btnOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOK.Click
    If btnApply.Enabled = True Then btnApply_Click(sender, e)
    Close()
  End Sub

  Private Sub frmSettings_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
    Me.Hide()
    Application.DoEvents()

    For Each ibMod As InfoBarModule In AvailableModules
      If ibMod.HasSettingsDialog AndAlso ibMod.SettingsDialog IsNot Nothing Then
        ibMod.ResetSettings()
        ibMod.SettingsDialog.Parent = Nothing
      End If
    Next

    If IBSettings.General.AutoHide Then fMain.tmrAutohide.Start()
  End Sub

  Private Sub frmSettings_SystemColorsChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.SystemColorsChanged
    Me.Font = SystemFonts.MessageBoxFont
    If IsWindowsVistaOrAbove() Then
      If VisualStyles.VisualStyleInformation.IsEnabledByUser = True Then
        EnableVistaControlEffects(Me, True)
      Else
        EnableVistaControlEffects(Me, False)
      End If
    Else
      EnableVistaControlEffects(Me, False)
    End If
  End Sub

  Private Sub tvSettings_AfterSelect(ByVal sender As Object, ByVal e As System.Windows.Forms.TreeViewEventArgs) Handles tvSettings.AfterSelect
    panelGeneral.Visible = False
    panelModules.Visible = False
    panelModuleSettings.Visible = False
    panelSkins.Visible = False
    panelIcons.Visible = False
    panelAdvanced.Visible = False
    panelAbout.Visible = False
    Select Case e.Node.Name
      Case "GENERAL"
        panelGeneral.Visible = True
      Case "MODULES"
        panelModules.Visible = True
      Case "SKINS"
        panelSkins.Visible = True
      Case "ICONS"
        panelIcons.Visible = True
      Case "ADVANCED"
        panelAdvanced.Visible = True
      Case "ABOUT"
        panelAbout.Visible = True
      Case Else
        For Each ibm As InfoBarModule In AvailableModules
          ibm.SettingsDialog.Visible = False
        Next
        Dim ibMod As InfoBarModule = AvailableModules(e.Node.Name)
        If ibMod.SettingsDialog IsNot Nothing Then
          ibMod.SettingsDialog.Visible = True
          panelModuleSettings.Visible = True
        End If
    End Select
  End Sub

  Private Sub tvSettings_BeforeCollapse(ByVal sender As Object, ByVal e As System.Windows.Forms.TreeViewCancelEventArgs) Handles tvSettings.BeforeCollapse
    e.Cancel = True
  End Sub

#End Region

#Region "General Page Events"

  Private Sub radGeneral_Location_Top_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles radGeneral_Location_Top.CheckedChanged
    btnApply.Enabled = True
  End Sub

  Private Sub radGeneral_Location_Bottom_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles radGeneral_Location_Bottom.CheckedChanged
    btnApply.Enabled = True
  End Sub

  Private Sub radGeneral_Location_Left_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles radGeneral_Location_Left.CheckedChanged
    btnApply.Enabled = True
  End Sub

  Private Sub radGeneral_Location_Right_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles radGeneral_Location_Right.CheckedChanged
    btnApply.Enabled = True
  End Sub

  Private Sub chkGeneral_Location_AlwaysOnTop_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkGeneral_Location_AlwaysOnTop.CheckedChanged
    btnApply.Enabled = True
  End Sub

  Private Sub chkAutohide_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkAutohide.CheckedChanged
    btnApply.Enabled = True
  End Sub

  Private Sub radModuleAlignmentLeft_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles radModuleAlignmentLeft.CheckedChanged
    btnApply.Enabled = True
  End Sub

  Private Sub radModuleAlignmentCenter_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles radModuleAlignmentCenter.CheckedChanged
    btnApply.Enabled = True
  End Sub

  Private Sub radModuleAlignmentRight_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles radModuleAlignmentRight.CheckedChanged
    btnApply.Enabled = True
  End Sub

  Private Sub radModuleAlignmentJustify_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles radModuleAlignmentJustify.CheckedChanged
    btnApply.Enabled = True
  End Sub

  Private Sub radModuleAlignmentJustifyCenter_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles radModuleAlignmentJustifyCenter.CheckedChanged
    btnApply.Enabled = True
  End Sub

  Private Sub chkGeneral_Startup_RunAtWindowsStartup_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkGeneral_Startup_RunAtWindowsStartup.CheckedChanged
    btnApply.Enabled = True
  End Sub

  Private Sub chkDisableToolbarDocking_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkDisableToolbarDocking.CheckedChanged
    btnApply.Enabled = True
    If chkDisableToolbarDocking.Checked = True Then
      chkAutohide.Checked = False
      chkAutohide.Enabled = False
    Else
      chkAutohide.Enabled = True
    End If
  End Sub

  Private Sub chkShowSeparators_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkShowSeparators.CheckedChanged
    btnApply.Enabled = True
  End Sub

#End Region

#Region "Modules Page Events"

  Private Sub btnModules_InsertSeparator_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnModules_InsertSeparator.Click
    If lvModules.SelectedItems IsNot Nothing Then
      Dim iIndex As Integer = lvModules.SelectedItems(lvModules.SelectedItems.Count - 1).Index
      lvModules.Items.Insert(iIndex, "Row Separator")
      lvModules.SelectedItems.Clear()
      lvModules.Items(iIndex).Selected = True
      btnApply.Enabled = True
    End If
  End Sub

  Private Sub btnModules_Delete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnModules_Delete.Click
    For Each lv As ListViewItem In lvModules.SelectedItems
      If lv.Text = "Row Separator" Then
        lvModules.Items.RemoveAt(lv.Index)
      Else
        Dim iMod As InfoBarModule = AvailableModules(lv.Name)
        ModulesToDelete.Add(iMod.ModuleGUID)
        lvModules.Items.RemoveAt(lv.Index)
      End If
    Next
    btnApply.Enabled = True
  End Sub

  Private Sub lvModules_DragDrop(ByVal sender As Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles lvModules.DragDrop
    If lvModules.SelectedItems.Count = 0 Then Return
    Dim p As Point = lvModules.PointToClient(New Point(e.X, e.Y))
    Dim dragToItem As ListViewItem = lvModules.GetItemAt(p.X, p.Y)
    If dragToItem Is Nothing Then Return
    lvModules.InsertionMark.Index = -1
    Dim dragIndex As Integer = dragToItem.Index
    Dim i As Integer
    Dim sel(lvModules.SelectedItems.Count) As ListViewItem
    For i = 0 To lvModules.SelectedItems.Count - 1
      sel(i) = lvModules.SelectedItems.Item(i)
    Next
    For i = 0 To lvModules.SelectedItems.Count - 1
      Dim dragItem As ListViewItem = sel(i)
      Dim itemIndex As Integer = dragIndex
      If itemIndex = dragItem.Index Then Return
      If dragItem.Index < itemIndex Then
        itemIndex = itemIndex + 1
      Else
        itemIndex = dragIndex + i
      End If
      Dim insertitem As ListViewItem = dragItem.Clone
      insertitem.Name = dragItem.Name
      lvModules.Items.Insert(itemIndex, insertitem)
      lvModules.Items.Remove(dragItem)
    Next
    btnApply.Enabled = True
  End Sub

  Private Sub lvModules_DragEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles lvModules.DragEnter
    Dim i As Integer
    For i = 0 To e.Data.GetFormats().Length - 1
      If e.Data.GetFormats()(i).Equals("System.Windows.Forms.ListView+SelectedListViewItemCollection") Then
        e.Effect = DragDropEffects.Move
      End If
    Next
  End Sub

  Private Sub lvModules_DragLeave(ByVal sender As Object, ByVal e As System.EventArgs) Handles lvModules.DragLeave
    lvModules.InsertionMark.Index = -1
  End Sub

  Private Sub lvModules_DragOver(ByVal sender As Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles lvModules.DragOver

    If e.Data.GetDataPresent("System.Windows.Forms.ListView+SelectedListViewItemCollection") = False Then
      e.Effect = DragDropEffects.None
      Exit Sub
    End If

    Dim cp As Point = lvModules.PointToClient(New Point(e.X, e.Y))
    Dim hoverItem As ListViewItem = lvModules.GetItemAt(cp.X, cp.Y)
    If hoverItem Is Nothing Then
      e.Effect = DragDropEffects.None
      lvModules.InsertionMark.Index = -1
      Exit Sub
    End If

    For Each moveItem As ListViewItem In lvModules.SelectedItems
      If moveItem.Index = hoverItem.Index Then
        e.Effect = DragDropEffects.None
        lvModules.InsertionMark.Index = -1
        hoverItem.EnsureVisible()
        Exit Sub
      End If
    Next

    lvModules.InsertionMark.Index = hoverItem.Index
    If lvModules.SelectedItems(0).Bounds.Y >= hoverItem.Bounds.Y Then
      lvModules.InsertionMark.AppearsAfterItem = False
    Else
      lvModules.InsertionMark.AppearsAfterItem = True
    End If

    If lvModules.TopItem.Index = hoverItem.Index AndAlso lvModules.TopItem.Index > 0 Then
      lvModules.Items(hoverItem.Index - 1).EnsureVisible()
    Else
      hoverItem.EnsureVisible()
    End If

    e.Effect = DragDropEffects.Move
  End Sub

  Private Sub lvModules_DrawItem(ByVal sender As Object, ByVal e As System.Windows.Forms.DrawListViewItemEventArgs) Handles lvModules.DrawItem
    If e.Item.Text = "Row Separator" Then
      e.DrawDefault = False
      e.Graphics.DrawLine(SystemPens.WindowText, e.Bounds.Left + 4, e.Bounds.Top + CInt(e.Bounds.Height / 2), e.Bounds.Right - 4, e.Bounds.Top + CInt(e.Bounds.Height / 2))
    Else
      e.DrawDefault = True
    End If
  End Sub

  Private Sub lvModules_ItemCheck(ByVal sender As Object, ByVal e As System.Windows.Forms.ItemCheckEventArgs) Handles lvModules.ItemCheck
    btnApply.Enabled = True
  End Sub

  Private Sub lvModules_ItemDrag(ByVal sender As Object, ByVal e As System.Windows.Forms.ItemDragEventArgs) Handles lvModules.ItemDrag
    If e.Button = Windows.Forms.MouseButtons.Left Then
      Dim li As ListViewItem = e.Item
      If li.Text <> "Row Separator" Then
        lvModules.DoDragDrop(lvModules.SelectedItems, DragDropEffects.Move)
      End If
    End If
  End Sub

  Private Sub lvModules_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles lvModules.SelectedIndexChanged
    If lvModules.SelectedItems IsNot Nothing Then
      If lvModules.SelectedItems.Count = 1 Then
        If lvModules.SelectedItems(0).Text = "Row Separator" Then
          btnModules_InsertSeparator.Enabled = False
          btnModules_Delete.Enabled = True
          grpModuleInfo.Text = vbNullString
          txtModuleDescription.Text = vbNullString
          txtModuleAuthor.Text = vbNullString
          txtModuleVersion.Text = vbNullString
          lblModuleEmail.Text = vbNullString
          lblModuleHomepage.Text = vbNullString
          txtModuleGUID.Text = vbNullString
        Else
          btnModules_InsertSeparator.Enabled = True
          btnModules_Delete.Enabled = True
          Dim iMod As InfoBarModule = AvailableModules(lvModules.SelectedItems(0).Name)
          grpModuleInfo.Text = iMod.ModuleName
          txtModuleDescription.Text = iMod.ModuleDescription
          txtModuleAuthor.Text = iMod.ModuleAuthor
          txtModuleVersion.Text = iMod.ModuleVersion
          lblModuleEmail.Text = iMod.ModuleEmail
          lblModuleHomepage.Text = iMod.ModuleHomepage
          txtModuleGUID.Text = iMod.ModuleGUID
        End If
      Else
        btnModules_InsertSeparator.Enabled = False
        btnModules_Delete.Enabled = True
        grpModuleInfo.Text = vbNullString
        txtModuleDescription.Text = vbNullString
        txtModuleAuthor.Text = vbNullString
        txtModuleVersion.Text = vbNullString
        lblModuleEmail.Text = vbNullString
        lblModuleHomepage.Text = vbNullString
        txtModuleGUID.Text = vbNullString
      End If
    Else
      btnModules_InsertSeparator.Enabled = False
      btnModules_Delete.Enabled = True
      grpModuleInfo.Text = vbNullString
      txtModuleDescription.Text = vbNullString
      txtModuleAuthor.Text = vbNullString
      txtModuleVersion.Text = vbNullString
      lblModuleEmail.Text = vbNullString
      lblModuleHomepage.Text = vbNullString
      txtModuleGUID.Text = vbNullString
    End If
  End Sub

  Private Sub btnGetMoreModules_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
    ShellExecute(Me.Handle, "open", "http://www.nightiguana.com/software.php?action=view&id=1&tab=files&filecategory=modules", vbNullString, vbNullString, 1)
  End Sub

#End Region

#Region "Skins Page Events"

  Private Sub lvSkins_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles lvSkins.MouseUp
    If lvSkins.SelectedItems.Count = 0 Then
      lvSkins.Items("IBSKIN_" & IBSettings.CurrentSkin).Selected = True
    Else
      btnApply.Enabled = True
    End If
  End Sub

  Private Sub btnSkins_RefreshList_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSkins_RefreshList.Click
    Skinning_EnumerateSkins()

    lvSkins.Items.Clear()

    Dim LI As ListViewItem
    For Each SI As SkinInfo In SkinCollection
      LI = New ListViewItem
      LI.Name = "IBSKIN_" & SI.Key
      LI.Text = SI.Name
      LI.SubItems.Add(SI.Author)
      LI.SubItems.Add(SI.Version)
      LI.SubItems.Add(SI.Website)
      lvSkins.Items.Add(LI)
    Next
    lvSkins.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize)

    If lvSkins.Items.ContainsKey("IBSKIN_" & IBSettings.CurrentSkin) Then
      lvSkins.Items("IBSKIN_" & IBSettings.CurrentSkin).Selected = True
    Else
      lvSkins.Items(0).Selected = True
    End If
  End Sub

  Private Sub btnSkins_VisitAuthorWebsite_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSkins_VisitAuthorWebsite.Click
    ShellExecute(Handle, "open", lvSkins.SelectedItems(0).SubItems(3).Text, vbNullString, vbNullString, vbNormalFocus)
  End Sub

#End Region

#Region "Icons Page Events"

  Private Sub lvIcons_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs)
    If lvIcons.SelectedItems.Count = 0 Then
      lvIcons.Items("IBICON_" & IBSettings.CurrentIconTheme).Selected = True
    Else
      btnApply.Enabled = True
    End If
  End Sub

#End Region

#Region "Advanced Page Events"

  Private Sub chkEnableTooltipFade_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkEnableTooltipFade.CheckedChanged
    btnApply.Enabled = True
  End Sub

  Private Sub chkOverrideSkinBackgroundOpacity_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkOverrideSkinBackgroundOpacity.CheckedChanged
    btnApply.Enabled = True
  End Sub

  Private Sub nudSkinBGOpacity_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles nudSkinBGOpacity.ValueChanged
    btnApply.Enabled = True
  End Sub

  Private Sub nudAutohideDelay_ValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles nudAutohideDelay.ValueChanged
    btnApply.Enabled = True
  End Sub

  Private Sub chkAutoHideAnimation_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkAutoHideAnimation.CheckedChanged
    btnApply.Enabled = True
  End Sub

  Private Sub nudAutohideAnimationSpeed_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles nudAutohideAnimationSpeed.ValueChanged
    btnApply.Enabled = True
  End Sub

  Private Sub chkAutohide_IgnoreMaximizedState_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkAutohide_IgnoreMaximizedState.CheckedChanged
    btnApply.Enabled = True
  End Sub

  Private Sub btnRepairFileAssociations_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRepairFileAssociations.Click
    RegisterFileAssociation(".InfoBarModule", "InfoBar Module Package", "Install", "installmodule")
    RegisterFileAssociation(".InfoBarSkin", "InfoBar Skin Package", "Install", "installskin")
    RegisterFileAssociation(".InfoBarIcons", "InfoBar Icon Package", "Install", "installicons")

    btnRepairFileAssociations.Enabled = False

    If CheckFileAssociations() = True Then
      lblFileAssociationStatus.Text = "Status: One or more file associations need repair."
      btnRepairFileAssociations.Enabled = True
    Else
      lblFileAssociationStatus.Text = "Status: OK."
    End If
  End Sub

#End Region

#Region "Other Routines"

  Public Sub BuildModulesList()
    Dim li As ListViewItem

    lvModules.BeginUpdate()
    lvModules.Items.Clear()
    imlModules.Images.Clear()

    Dim iCurrentRow As Integer = 1
    For Each sMod As SelectedModuleType In IBSettings.SelectedModules
      If sMod.Row <> iCurrentRow Then
        iCurrentRow = sMod.Row
        lvModules.Items.Add("Row Separator")
      End If

      Dim aMod As InfoBarModule = AvailableModules(sMod.GUID)
      imlModules.Images.Add(aMod.ModuleGUID, aMod.ModuleIcon)
      li = New ListViewItem
      li.Name = aMod.ModuleGUID
      li.Text = aMod.ModuleName
      li.ImageKey = aMod.ModuleGUID
      li.Checked = True
      lvModules.Items.Add(li)
    Next

    For Each aMod As InfoBarModule In AvailableModules
      If lvModules.Items.ContainsKey(aMod.ModuleGUID) = False Then
        imlModules.Images.Add(aMod.ModuleGUID, aMod.ModuleIcon)
        li = New ListViewItem
        li.Name = aMod.ModuleGUID
        li.Text = aMod.ModuleName
        li.ImageKey = aMod.ModuleGUID
        lvModules.Items.Add(li)
      End If
    Next

    lvModules.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize)
    lvModules.EndUpdate()

    tvSettings.Nodes.Clear()
    With tvSettings.Nodes
      .Add("GENERAL", "General Settings")
      .Add("MODULES", "Modules")
      .Add("SKINS", "Skins")
      .Add("ICONS", "Icons")
      .Add("ADVANCED", "Advanced Settings")
      .Add("ABOUT", "About InfoBar")
    End With

    ' Modules
    Dim sIconKey As String, node As TreeNode
    For Each ibMod As InfoBarModule In AvailableModules
      sIconKey = vbNullString
      If ibMod.ModuleIcon IsNot Nothing Then
        imlModules.Images.Add(ibMod.ModuleGUID, ibMod.ModuleIcon)
        sIconKey = ibMod.ModuleGUID
      End If
      If ibMod.HasSettingsDialog AndAlso ibMod.SettingsDialog IsNot Nothing Then
        node = tvSettings.Nodes("MODULES").Nodes.Add(ibMod.ModuleGUID, ibMod.ModuleName)
        ibMod.SettingsDialog.Parent = panelModuleSettings
        ibMod.SettingsDialog.Dock = DockStyle.Fill
        ibMod.SettingsDialog.Font = SystemFonts.MessageBoxFont
        ibMod.SettingsDialog.Visible = False
        If IsWindowsVistaOrAbove() = True Then
          If VisualStyles.VisualStyleInformation.IsEnabledByUser = True Then
            EnableVistaControlEffects(ibMod.SettingsDialog, True)
          Else
            EnableVistaControlEffects(ibMod.SettingsDialog, False)
          End If
        Else
          EnableVistaControlEffects(ibMod.SettingsDialog, False)
        End If
      End If
    Next
    If tvSettings.Nodes("MODULES").Nodes.Count > 0 Then tvSettings.Nodes("MODULES").Expand()

    ModulesToDelete.Clear()
  End Sub

  Private Sub GetFileVersionInformation()
    Dim sFiles() As String
    Dim LVI As ListViewItem

    sFiles = Directory.GetFiles(Application.StartupPath, "*.exe")
    For Each sFile As String In sFiles
      If sFile.Contains("\~") = False AndAlso sFile.Contains("vshost") = False _
      AndAlso sFile.Contains("uninstall") = False Then
        LVI = New ListViewItem
        Dim sVersion As String = FileVersionInfo.GetVersionInfo(sFile).ProductVersion
        LVI.Text = Mid(sFile, InStrRev(sFile, "\") + 1)
        LVI.SubItems.Add(sVersion)
        lvVersionInfo.Items.Add(LVI)
      End If
    Next

    sFiles = Directory.GetFiles(Application.StartupPath, "*.dll")
    For Each sFile As String In sFiles
      If sFile.Contains("\~") = False Then
        LVI = New ListViewItem
        Dim sVersion As String = FileVersionInfo.GetVersionInfo(sFile).ProductVersion
        LVI.Text = Mid(sFile, InStrRev(sFile, "\") + 1)
        LVI.SubItems.Add(sVersion)
        lvVersionInfo.Items.Add(LVI)
      End If
    Next

    sFiles = Directory.GetFiles(Application.StartupPath & "\Modules\", "*.dll", SearchOption.AllDirectories)
    For Each sFile As String In sFiles
      If sFile.Contains("\~") = False Then
        LVI = New ListViewItem
        Dim sVersion As String = FileVersionInfo.GetVersionInfo(sFile).ProductVersion
        LVI.Text = sFile.Replace(Application.StartupPath, vbNullString).Remove(0, 1)
        LVI.SubItems.Add(sVersion)
        lvVersionInfo.Items.Add(LVI)
      End If
    Next

    lvVersionInfo.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize)
    lvVersionInfo.Sort()

  End Sub

#End Region

End Class