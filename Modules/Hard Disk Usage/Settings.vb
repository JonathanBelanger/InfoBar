Friend Class Settings

  Private IBSettings As New InfoBar.Services.Settings

  Private Sub chkShowIcon_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkShowIcon.CheckedChanged
    If chkShowIcon.Checked = False And chkShowText.Checked = False Then
      chkShowIcon.Checked = True
      MsgBox("At least one display option must be selected.", MsgBoxStyle.Information Or MsgBoxStyle.OkOnly, "Hard Disk Usage")
    Else
      IBSettings.EnableSettingsDialogApplyButton()
    End If
  End Sub

  Private Sub chkShowText_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkShowText.CheckedChanged
    If chkShowIcon.Checked = False And chkShowText.Checked = False Then
      chkShowText.Checked = True
      MsgBox("At least one display option must be selected.", MsgBoxStyle.Information Or MsgBoxStyle.OkOnly, "Hard Disk Usage")
    Else
      IBSettings.EnableSettingsDialogApplyButton()
    End If
  End Sub

  Private Sub comDisplayUnit_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles comDisplayUnit.SelectedIndexChanged
    IBSettings.EnableSettingsDialogApplyButton()
  End Sub

  Private Sub comTooltipDisplayUnit_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles comTooltipDisplayUnit.SelectedIndexChanged
    IBSettings.EnableSettingsDialogApplyButton()
  End Sub

  Private Sub txtTextFormatString_MouseEnter(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtTextFormatString.MouseEnter
    txtTextFormatString.Refresh()
  End Sub

  Private Sub txtTextFormatString_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtTextFormatString.TextChanged
    IBSettings.EnableSettingsDialogApplyButton()
  End Sub

  Private Sub txtTextFormatHelp_MouseEnter(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtTextFormatHelp.MouseEnter
    txtTextFormatHelp.Refresh()
  End Sub

  Private Sub lvDrives_ItemCheck(ByVal sender As Object, ByVal e As System.Windows.Forms.ItemCheckEventArgs) Handles lvDrives.ItemCheck
    IBSettings.EnableSettingsDialogApplyButton()
  End Sub

End Class