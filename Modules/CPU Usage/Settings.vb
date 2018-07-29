Friend Class Settings

  Public Caller As CPUUsage.InfoBarModuleMain
  Private IBSettings As New InfoBar.Services.Settings

  Private Sub chkShowIcon_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkShowIcon.CheckedChanged
    If chkShowIcon.Checked = False And chkShowText.Checked = False And chkShowGraph.Checked = False Then
      chkShowIcon.Checked = True
      MsgBox("At least one display option must be selected.", MsgBoxStyle.Information Or MsgBoxStyle.OkOnly, "CPU Usage")
    Else
      IBSettings.EnableSettingsDialogApplyButton()
    End If
  End Sub

  Private Sub chkShowText_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkShowText.CheckedChanged
    If chkShowIcon.Checked = False And chkShowText.Checked = False And chkShowGraph.Checked = False Then
      chkShowText.Checked = True
      MsgBox("At least one display option must be selected.", MsgBoxStyle.Information Or MsgBoxStyle.OkOnly, "CPU Usage")
    Else
      IBSettings.EnableSettingsDialogApplyButton()
    End If
  End Sub

  Private Sub chkShowGraph_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkShowGraph.CheckedChanged
    If chkShowIcon.Checked = False And chkShowText.Checked = False And chkShowGraph.Checked = False Then
      chkShowGraph.Checked = True
      MsgBox("At least one display option must be selected.", MsgBoxStyle.Information Or MsgBoxStyle.OkOnly, "CPU Usage")
    Else
      IBSettings.EnableSettingsDialogApplyButton()
    End If
  End Sub

  Private Sub chkShowTopProcesses_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkShowTopProcesses.CheckedChanged
    If chkShowTopProcesses.Checked = True Then
      nudTopProcesses.Enabled = True
    Else
      nudTopProcesses.Enabled = False
    End If
    IBSettings.EnableSettingsDialogApplyButton()
  End Sub

  Private Sub nudTopProcesses_ValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles nudTopProcesses.ValueChanged
    IBSettings.EnableSettingsDialogApplyButton()
  End Sub

  Private Sub nudGraphUpdateTime_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles nudGraphUpdateTime.ValueChanged
    IBSettings.EnableSettingsDialogApplyButton()
  End Sub

  Private Sub comGraphUpdateTime_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles comGraphUpdateTime.SelectedIndexChanged
    IBSettings.EnableSettingsDialogApplyButton()
  End Sub

  Private Sub radShowAvg_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
    IBSettings.EnableSettingsDialogApplyButton()
  End Sub

  Private Sub radShowAllCores_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
    IBSettings.EnableSettingsDialogApplyButton()
  End Sub

  Private Sub lvMulticore_DrawColumnHeader(ByVal sender As Object, ByVal e As System.Windows.Forms.DrawListViewColumnHeaderEventArgs) Handles lvMulticore.DrawColumnHeader
    e.DrawDefault = True
  End Sub

  Private Sub lvMulticore_DrawItem(ByVal sender As Object, ByVal e As System.Windows.Forms.DrawListViewItemEventArgs) Handles lvMulticore.DrawItem
    e.DrawDefault = False
  End Sub

  Private Sub lvMulticore_DrawSubItem(ByVal sender As Object, ByVal e As System.Windows.Forms.DrawListViewSubItemEventArgs) Handles lvMulticore.DrawSubItem
    Dim pCheck As Point, glyphSize As Size
    Select Case True
      Case e.SubItem.Name.StartsWith("ShowText_"), _
           e.SubItem.Name.StartsWith("ShowTooltip_"), _
           e.SubItem.Name.StartsWith("ShowGraph_")
        e.DrawDefault = False
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
      Case e.SubItem.Name.StartsWith("GraphColor_")
        ' Draw Box filled with selected color
        glyphSize = CheckBoxRenderer.GetGlyphSize(e.Graphics, VisualStyles.CheckBoxState.CheckedNormal)
        pCheck.X = e.SubItem.Bounds.Left + ((e.SubItem.Bounds.Width - glyphSize.Width) / 2)
        pCheck.Y = e.SubItem.Bounds.Top + ((e.SubItem.Bounds.Height - glyphSize.Height) / 2)
        e.Graphics.FillRectangle(New SolidBrush(Color.FromArgb(CInt(e.SubItem.Text))), New Rectangle(pCheck.X, pCheck.Y, glyphSize.Width, glyphSize.Height))
        e.Graphics.DrawRectangle(New Pen(Color.Black), New Rectangle(pCheck.X, pCheck.Y, glyphSize.Width, glyphSize.Height))
      Case Else
        e.DrawDefault = True
    End Select
  End Sub

  Private Sub lvMulticore_MouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles lvMulticore.MouseClick
    Dim hti As ListViewHitTestInfo = lvMulticore.HitTest(e.Location)
    If hti.Item IsNot Nothing Then
      If hti.SubItem IsNot Nothing Then
        Select Case True
          Case hti.SubItem.Name.StartsWith("ShowText_"), _
               hti.SubItem.Name.StartsWith("ShowTooltip_"), _
               hti.SubItem.Name.StartsWith("ShowGraph_")
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
          Case hti.SubItem.Name.StartsWith("GraphColor_")
            ' Show Color Picker
            Dim cp As New ColorDialog
            cp.Color = Color.FromArgb(CInt(hti.SubItem.Text))
            If cp.ShowDialog = DialogResult.OK Then
              hti.SubItem.Text = cp.Color.ToArgb.ToString
              IBSettings.EnableSettingsDialogApplyButton()
            End If
        End Select
      End If
    End If
  End Sub

  Private Sub Settings_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
    Dim I As Integer
    For I = 0 To lvMulticore.Columns.Count - 1
      If (I < lvMulticore.Columns.Count - 1) Then
        lvMulticore.Columns(I).AutoResize(ColumnHeaderAutoResizeStyle.HeaderSize)
      End If
      lvMulticore.Columns(I).Width += 10
    Next
  End Sub

  Private Sub Settings_SystemColorsChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.SystemColorsChanged
    Dim I As Integer
    For I = 0 To lvMulticore.Columns.Count - 1
      If (I < lvMulticore.Columns.Count - 1) Then
        lvMulticore.Columns(I).AutoResize(ColumnHeaderAutoResizeStyle.HeaderSize)
      End If
      lvMulticore.Columns(I).Width += 2
    Next
  End Sub

End Class