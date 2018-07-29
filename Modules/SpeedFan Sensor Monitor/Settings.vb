Friend Class Settings

  Public Caller As SpeedFanSensorMonitor.InfoBarModuleMain
  Private IBSettings As New InfoBar.Services.Settings

  Private Sub btnReset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReset.Click
    ResetSpeedFanData()
  End Sub

  Private Sub btnSelectAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSelectAll.Click
    For Each lvi As ListViewItem In lvItems.Items
      lvi.SubItems(2).Text = "True"
      lvi.SubItems(4).Text = "True"
    Next
    IBSettings.EnableSettingsDialogApplyButton()
  End Sub

  Private Sub btnSelectNone_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSelectNone.Click
    For Each lvi As ListViewItem In lvItems.Items
      lvi.SubItems(2).Text = "False"
      lvi.SubItems(4).Text = "True"
    Next
    IBSettings.EnableSettingsDialogApplyButton()
  End Sub

  Private Sub chkShowIcon_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkShowIcon.CheckedChanged
    If chkShowIcon.Checked = False And chkShowText.Checked = False Then
      chkShowIcon.Checked = True
      MsgBox("At least one display option must be selected.", MsgBoxStyle.Information Or MsgBoxStyle.OkOnly, "SpeedFan Sensor Monitor")
    Else
      IBSettings.EnableSettingsDialogApplyButton()
    End If
  End Sub

  Private Sub chkShowText_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkShowText.CheckedChanged
    If chkShowIcon.Checked = False And chkShowText.Checked = False Then
      chkShowText.Checked = True
      MsgBox("At least one display option must be selected.", MsgBoxStyle.Information Or MsgBoxStyle.OkOnly, "SpeedFan Sensor Monitor")
    Else
      IBSettings.EnableSettingsDialogApplyButton()
    End If
  End Sub

  Private Sub lvItems_AfterLabelEdit(ByVal sender As Object, ByVal e As System.Windows.Forms.LabelEditEventArgs) Handles lvItems.AfterLabelEdit
    If e.Label = vbNullString Then
      e.CancelEdit = True
    Else
      e.CancelEdit = False
      IBSettings.EnableSettingsDialogApplyButton()
    End If
  End Sub

  Private Sub lvItems_BeforeLabelEdit(ByVal sender As Object, ByVal e As System.Windows.Forms.LabelEditEventArgs) Handles lvItems.BeforeLabelEdit
    e.CancelEdit = False
  End Sub

  Private Sub lvItems_DrawColumnHeader(ByVal sender As Object, ByVal e As System.Windows.Forms.DrawListViewColumnHeaderEventArgs) Handles lvItems.DrawColumnHeader
    e.DrawDefault = True
  End Sub

  Private Sub lvItems_DrawItem(ByVal sender As Object, ByVal e As System.Windows.Forms.DrawListViewItemEventArgs) Handles lvItems.DrawItem
    e.DrawDefault = False
  End Sub

  Private Sub lvItems_DrawSubItem(ByVal sender As Object, ByVal e As System.Windows.Forms.DrawListViewSubItemEventArgs) Handles lvItems.DrawSubItem
    If e.SubItem.Name.StartsWith("Enabled_") = True OrElse _
    e.SubItem.Name.StartsWith("ShowText_") = True OrElse _
    e.SubItem.Name.StartsWith("ShowTooltip_") = True Then
      e.DrawDefault = False
      Dim pCheck As Point, glyphSize As Size
      Select Case e.SubItem.Text
        Case "True"
          glyphSize = CheckBoxRenderer.GetGlyphSize(e.Graphics, VisualStyles.CheckBoxState.CheckedNormal)
          pCheck.X = e.SubItem.Bounds.Left + ((e.SubItem.Bounds.Width - glyphSize.Width) / 2)
          pCheck.Y = e.SubItem.Bounds.Top + ((e.SubItem.Bounds.Height - glyphSize.Height) / 2)
          CheckBoxRenderer.DrawCheckBox(e.Graphics, pCheck, VisualStyles.CheckBoxState.CheckedNormal)
        Case "False"
          glyphSize = CheckBoxRenderer.GetGlyphSize(e.Graphics, VisualStyles.CheckBoxState.UncheckedNormal)
          pCheck.X = e.SubItem.Bounds.Left + ((e.SubItem.Bounds.Width - glyphSize.Width) / 2)
          pCheck.Y = e.SubItem.Bounds.Top + ((e.SubItem.Bounds.Height - glyphSize.Height) / 2)
          CheckBoxRenderer.DrawCheckBox(e.Graphics, pCheck, VisualStyles.CheckBoxState.UncheckedNormal)
      End Select
    Else
      e.DrawDefault = True
    End If
  End Sub

  Private Sub lvItems_ItemChecked(ByVal sender As Object, ByVal e As System.Windows.Forms.ItemCheckedEventArgs) Handles lvItems.ItemChecked
    IBSettings.EnableSettingsDialogApplyButton()
  End Sub

  Private Sub lvItems_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles lvItems.KeyUp
    If e.KeyCode = Keys.F2 AndAlso lvItems.SelectedItems.Count = 1 Then
      lvItems.SelectedItems(0).BeginEdit()
    End If
  End Sub

  Private Sub lvItems_MouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles lvItems.MouseClick
    Dim hti As ListViewHitTestInfo = lvItems.HitTest(e.Location)
    If hti.Item IsNot Nothing Then
      If hti.SubItem IsNot Nothing Then
        If hti.SubItem.Name.StartsWith("Enabled_") = True OrElse _
        hti.SubItem.Name.StartsWith("ShowText_") = True OrElse _
        hti.SubItem.Name.StartsWith("ShowTooltip_") = True Then
          Dim rCheck As Rectangle, glyphSize As Size
          If hti.SubItem.Text = "True" Then
            glyphSize = CheckBoxRenderer.GetGlyphSize(Graphics.FromHwnd(IntPtr.Zero), VisualStyles.CheckBoxState.CheckedNormal)
            rCheck.X = hti.SubItem.Bounds.Left + ((hti.SubItem.Bounds.Width - glyphSize.Width) / 2)
            rCheck.Y = hti.SubItem.Bounds.Top + ((hti.SubItem.Bounds.Height - glyphSize.Height) / 2)
            rCheck.Width = glyphSize.Width
            rCheck.Height = glyphSize.Height
            If rCheck.Contains(e.Location) = True Then
              hti.SubItem.Text = "False"
              IBSettings.EnableSettingsDialogApplyButton()
            End If
          Else
            glyphSize = CheckBoxRenderer.GetGlyphSize(Graphics.FromHwnd(IntPtr.Zero), VisualStyles.CheckBoxState.UncheckedNormal)
            rCheck.X = hti.SubItem.Bounds.Left + ((hti.SubItem.Bounds.Width - glyphSize.Width) / 2)
            rCheck.Y = hti.SubItem.Bounds.Top + ((hti.SubItem.Bounds.Height - glyphSize.Height) / 2)
            rCheck.Width = glyphSize.Width
            rCheck.Height = glyphSize.Height
            If rCheck.Contains(e.Location) = True Then
              hti.SubItem.Text = "True"
              IBSettings.EnableSettingsDialogApplyButton()
            End If
          End If
        End If
      End If
    End If
  End Sub

  Public Sub ResetSpeedFanData()
    UseWaitCursor = True
    btnSelectAll.Enabled = False
    btnSelectNone.Enabled = False
    btnReset.Enabled = False
    lvItems.Items.Clear()

    Caller.SpeedFan_ResetData()

    If Caller.SpeedFan_GetData = True Then

      Dim LI As ListViewItem
      For Each entry As SpeedFanSensorMonitor.InfoBarModuleMain.SpeedFanDataEntry In Caller.SpeedFanData.Temperatures
        LI = New ListViewItem
        LI.Name = entry.ID
        LI.Text = entry.Label
        LI.SubItems.Add(entry.Value & "°C")

        Dim lvsi As New ListViewItem.ListViewSubItem
        lvsi.Name = "Enabled_" & LI.Name
        lvsi.Text = entry.Enabled.ToString
        LI.SubItems.Add(lvsi)

        lvsi = New ListViewItem.ListViewSubItem
        lvsi.Name = "ShowText_" & LI.Name
        lvsi.Text = entry.ShowOnToolbar.ToString
        LI.SubItems.Add(lvsi)

        lvsi = New ListViewItem.ListViewSubItem
        lvsi.Name = "ShowTooltip_" & LI.Name
        lvsi.Text = entry.ShowOnTooltip.ToString
        LI.SubItems.Add(lvsi)

        LI.Group = lvItems.Groups("lvgTemps")
        LI.Checked = entry.Enabled
        lvItems.Items.Add(LI)
      Next

      For Each entry As SpeedFanSensorMonitor.InfoBarModuleMain.SpeedFanDataEntry In Caller.SpeedFanData.FanSpeeds
        LI = New ListViewItem
        LI.Name = entry.ID
        LI.Text = entry.Label
        LI.SubItems.Add(entry.Value & " RPM")

        Dim lvsi As New ListViewItem.ListViewSubItem
        lvsi.Name = "Enabled_" & LI.Name
        lvsi.Text = entry.Enabled.ToString
        LI.SubItems.Add(lvsi)

        lvsi = New ListViewItem.ListViewSubItem
        lvsi.Name = "ShowText_" & LI.Name
        lvsi.Text = entry.ShowOnToolbar.ToString
        LI.SubItems.Add(lvsi)

        lvsi = New ListViewItem.ListViewSubItem
        lvsi.Name = "ShowTooltip_" & LI.Name
        lvsi.Text = entry.ShowOnTooltip.ToString
        LI.SubItems.Add(lvsi)

        LI.Group = lvItems.Groups("lvgFans")
        LI.Checked = entry.Enabled
        lvItems.Items.Add(LI)
      Next

      For Each entry As SpeedFanSensorMonitor.InfoBarModuleMain.SpeedFanDataEntry In Caller.SpeedFanData.Voltages
        LI = New ListViewItem
        LI.Name = entry.ID
        LI.Text = entry.Label
        LI.SubItems.Add(entry.Value & "V")

        Dim lvsi As New ListViewItem.ListViewSubItem
        lvsi.Name = "Enabled_" & LI.Name
        lvsi.Text = entry.Enabled.ToString
        LI.SubItems.Add(lvsi)

        lvsi = New ListViewItem.ListViewSubItem
        lvsi.Name = "ShowText_" & LI.Name
        lvsi.Text = entry.ShowOnToolbar.ToString
        LI.SubItems.Add(lvsi)

        lvsi = New ListViewItem.ListViewSubItem
        lvsi.Name = "ShowTooltip_" & LI.Name
        lvsi.Text = entry.ShowOnTooltip.ToString
        LI.SubItems.Add(lvsi)

        LI.Group = lvItems.Groups("lvgVoltages")
        LI.Checked = entry.Enabled
        lvItems.Items.Add(LI)
      Next

      If lvItems.Items.Count > 0 Then
        btnSelectAll.Enabled = True
        btnSelectNone.Enabled = True
      End If
      lvItems.Enabled = True

    Else

      lvItems.Enabled = False

    End If

    UseWaitCursor = False
    btnReset.Enabled = True
  End Sub

  Private Sub Settings_SystemColorsChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.SystemColorsChanged
    lvItems.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize)
  End Sub

  Private Sub lvItems_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lvItems.SelectedIndexChanged

  End Sub
End Class