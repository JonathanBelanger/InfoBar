Friend Class Settings

  Private IBSettings As New InfoBar.Services.Settings

  Private Sub picGraphLineColor_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles picGraphLineColor.Click
    Dim cp As New ColorDialog
    cp.Color = picGraphLineColor.BackColor
    If cp.ShowDialog = Windows.Forms.DialogResult.OK Then
      picGraphLineColor.BackColor = cp.Color
      IBSettings.EnableSettingsDialogApplyButton()
    End If
    cp.Dispose()
  End Sub

  Private Sub chkShowIcon_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkShowIcon.CheckedChanged
    If chkShowIcon.Checked = False And chkShowText.Checked = False And chkShowGraph.Checked = False Then
      chkShowIcon.Checked = True
      MsgBox("At least one display option must be selected.", MsgBoxStyle.Information Or MsgBoxStyle.OkOnly, "Memory Usage")
    Else
      IBSettings.EnableSettingsDialogApplyButton()
    End If
  End Sub

  Private Sub chkShowText_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkShowText.CheckedChanged
    If chkShowIcon.Checked = False And chkShowText.Checked = False And chkShowGraph.Checked = False Then
      chkShowText.Checked = True
      MsgBox("At least one display option must be selected.", MsgBoxStyle.Information Or MsgBoxStyle.OkOnly, "Memory Usage")
    Else
      IBSettings.EnableSettingsDialogApplyButton()
    End If
  End Sub

  Private Sub chkShowGraph_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkShowGraph.CheckedChanged
    If chkShowIcon.Checked = False And chkShowText.Checked = False And chkShowGraph.Checked = False Then
      chkShowGraph.Checked = True
      MsgBox("At least one display option must be selected.", MsgBoxStyle.Information Or MsgBoxStyle.OkOnly, "Memory Usage")
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

  Private Sub chkTextFree_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkTextFree.CheckedChanged
    IBSettings.EnableSettingsDialogApplyButton()
  End Sub

  Private Sub chkTextUsed_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkTextUsed.CheckedChanged
    IBSettings.EnableSettingsDialogApplyButton()
  End Sub

  Private Sub chkTextTotal_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkTextTotal.CheckedChanged
    IBSettings.EnableSettingsDialogApplyButton()
  End Sub

  Private Sub chkTextPctFree_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkTextPctFree.CheckedChanged
    IBSettings.EnableSettingsDialogApplyButton()
  End Sub

  Private Sub chkTextPctUsed_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkTextPctUsed.CheckedChanged
    IBSettings.EnableSettingsDialogApplyButton()
  End Sub

  Private Sub radTextDisplayUnitKB_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles radTextDisplayUnitKB.CheckedChanged
    IBSettings.EnableSettingsDialogApplyButton()
  End Sub

  Private Sub radTextDisplayUnitMB_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles radTextDisplayUnitMB.CheckedChanged
    IBSettings.EnableSettingsDialogApplyButton()
  End Sub

  Private Sub radTextDisplayUnitGB_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles radTextDisplayUnitGB.CheckedChanged
    IBSettings.EnableSettingsDialogApplyButton()
  End Sub

  Private Sub radGraphDisplayFree_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles radGraphDisplayFree.CheckedChanged
    IBSettings.EnableSettingsDialogApplyButton()
  End Sub

  Private Sub radGraphDisplayUsed_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles radGraphDisplayUsed.CheckedChanged
    IBSettings.EnableSettingsDialogApplyButton()
  End Sub

  Private Sub chkTooltipFree_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkTooltipFree.CheckedChanged
    IBSettings.EnableSettingsDialogApplyButton()
  End Sub

  Private Sub chkTooltipPctFree_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkTooltipPctFree.CheckedChanged
    IBSettings.EnableSettingsDialogApplyButton()
  End Sub

  Private Sub chkTooltipUsed_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkTooltipUsed.CheckedChanged
    IBSettings.EnableSettingsDialogApplyButton()
  End Sub

  Private Sub chkTooltipPctUsed_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkTooltipPctUsed.CheckedChanged
    IBSettings.EnableSettingsDialogApplyButton()
  End Sub

  Private Sub chkTooltipTotal_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkTooltipTotal.CheckedChanged
    IBSettings.EnableSettingsDialogApplyButton()
  End Sub

  Private Sub radTooltipDisplayUnitKB_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles radTooltipDisplayUnitKB.CheckedChanged
    IBSettings.EnableSettingsDialogApplyButton()
  End Sub

  Private Sub radTooltipDisplayUnitMB_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles radTooltipDisplayUnitMB.CheckedChanged
    IBSettings.EnableSettingsDialogApplyButton()
  End Sub

  Private Sub radTooltipDisplayUnitGB_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles radTooltipDisplayUnitGB.CheckedChanged
    IBSettings.EnableSettingsDialogApplyButton()
  End Sub

  Private Sub nudGraphUpdateTime_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles nudGraphUpdateTime.ValueChanged
    IBSettings.EnableSettingsDialogApplyButton()
  End Sub

  Private Sub comGraphUpdateTime_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles comGraphUpdateTime.SelectedIndexChanged
    IBSettings.EnableSettingsDialogApplyButton()
  End Sub

End Class