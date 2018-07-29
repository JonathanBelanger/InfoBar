Friend Class Settings

  Private IBSettings As New InfoBar.Services.Settings

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

  Private Sub chkTextDownloadSpeed_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkTextDownloadSpeed.CheckedChanged
    IBSettings.EnableSettingsDialogApplyButton()
  End Sub

  Private Sub chkTextUploadSpeed_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkTextUploadSpeed.CheckedChanged
    IBSettings.EnableSettingsDialogApplyButton()
  End Sub

  Private Sub chkTextTotal_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
    IBSettings.EnableSettingsDialogApplyButton()
  End Sub

  Private Sub chkTextDownloadTotal_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkTextDownloadTotal.CheckedChanged
    IBSettings.EnableSettingsDialogApplyButton()
  End Sub

  Private Sub chkTextUploadTotal_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkTextUploadTotal.CheckedChanged
    IBSettings.EnableSettingsDialogApplyButton()
  End Sub

  Private Sub radTextDisplayUnitKB_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
    IBSettings.EnableSettingsDialogApplyButton()
  End Sub

  Private Sub radTextDisplayUnitMB_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
    IBSettings.EnableSettingsDialogApplyButton()
  End Sub

  Private Sub radTextDisplayUnitGB_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
    IBSettings.EnableSettingsDialogApplyButton()
  End Sub

  Private Sub radGraphDisplayFree_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
    IBSettings.EnableSettingsDialogApplyButton()
  End Sub

  Private Sub radGraphDisplayUsed_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
    IBSettings.EnableSettingsDialogApplyButton()
  End Sub

  Private Sub chkTooltipDownloadSpeed_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkTooltipDownloadSpeed.CheckedChanged
    IBSettings.EnableSettingsDialogApplyButton()
  End Sub

  Private Sub chkTooltipUploadSpeed_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkTooltipUploadSpeed.CheckedChanged
    IBSettings.EnableSettingsDialogApplyButton()
  End Sub

  Private Sub chkTooltipDownloadTotal_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkTooltipDownloadTotal.CheckedChanged
    IBSettings.EnableSettingsDialogApplyButton()
  End Sub

  Private Sub chkTooltipUploadTotal_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkTooltipUploadTotal.CheckedChanged
    IBSettings.EnableSettingsDialogApplyButton()
  End Sub

  Private Sub chkTooltipUploadAvg_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkTooltipDownloadAvg.CheckedChanged, chkTooltipUploadAvg.CheckedChanged
    IBSettings.EnableSettingsDialogApplyButton()
  End Sub

  Private Sub radTooltipDisplayUnitKB_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
    IBSettings.EnableSettingsDialogApplyButton()
  End Sub

  Private Sub radTooltipDisplayUnitMB_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
    IBSettings.EnableSettingsDialogApplyButton()
  End Sub

  Private Sub radTooltipDisplayUnitGB_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
    IBSettings.EnableSettingsDialogApplyButton()
  End Sub

  Private Sub nudGraphUpdateTime_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles nudGraphUpdateTime.ValueChanged
    IBSettings.EnableSettingsDialogApplyButton()
  End Sub

  Private Sub comGraphUpdateTime_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles comGraphUpdateTime.SelectedIndexChanged
    IBSettings.EnableSettingsDialogApplyButton()
  End Sub

  Private Sub comGraphDisplay_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles comGraphDisplay.SelectedIndexChanged
    IBSettings.EnableSettingsDialogApplyButton()
  End Sub

  Private Sub comInterface_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles comInterface.SelectedIndexChanged
    IBSettings.EnableSettingsDialogApplyButton()
  End Sub

  Private Sub comTextSpeedDisplayUnit_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles comTextSpeedDisplayUnit.SelectedIndexChanged
    IBSettings.EnableSettingsDialogApplyButton()
  End Sub

  Private Sub comTooltipSpeedDisplayUnit_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles comTooltipSpeedDisplayUnit.SelectedIndexChanged
    IBSettings.EnableSettingsDialogApplyButton()
  End Sub

  Private Sub picGraphDownloadColor_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles picGraphDownloadColor.Click
    Dim cp As New ColorDialog
    cp.Color = picGraphDownloadColor.BackColor
    If cp.ShowDialog = Windows.Forms.DialogResult.OK Then
      picGraphDownloadColor.BackColor = cp.Color
      IBSettings.EnableSettingsDialogApplyButton()
    End If
    cp.Dispose()
  End Sub

  Private Sub picGraphUploadColor_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles picGraphUploadColor.Click
    Dim cp As New ColorDialog
    cp.Color = picGraphUploadColor.BackColor
    If cp.ShowDialog = Windows.Forms.DialogResult.OK Then
      picGraphUploadColor.BackColor = cp.Color
      IBSettings.EnableSettingsDialogApplyButton()
    End If
    cp.Dispose()
  End Sub

  Private Sub comTextTotalDisplayUnit_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles comTextTotalDisplayUnit.SelectedIndexChanged
    IBSettings.EnableSettingsDialogApplyButton()
  End Sub

  Private Sub comTooltipTotalDisplayUnit_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles comTooltipTotalDisplayUnit.SelectedIndexChanged
    IBSettings.EnableSettingsDialogApplyButton()
  End Sub

  Private Sub chkSaveAverageSpeeds_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkSaveAverageSpeeds.CheckedChanged
    IBSettings.EnableSettingsDialogApplyButton()
  End Sub

  Private Sub chkSaveMaxSpeeds_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkSaveMaxSpeeds.CheckedChanged
    IBSettings.EnableSettingsDialogApplyButton()
  End Sub

  Private Sub chkTooltipDownloadMax_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkTooltipDownloadMax.CheckedChanged
    IBSettings.EnableSettingsDialogApplyButton()
  End Sub

  Private Sub chkTooltipUploadMax_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkTooltipUploadMax.CheckedChanged
    IBSettings.EnableSettingsDialogApplyButton()
  End Sub

  Private Sub chkResetAvgSpeeds_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkResetAvgSpeeds.CheckedChanged
    IBSettings.EnableSettingsDialogApplyButton()
  End Sub

  Private Sub chkResetMaxSpeeds_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkResetMaxSpeeds.CheckedChanged
    IBSettings.EnableSettingsDialogApplyButton()
  End Sub

  Private Sub chkResetTransferTotals_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkResetTransferTotals.CheckedChanged
    IBSettings.EnableSettingsDialogApplyButton()
  End Sub

  Private Sub radTextHorz_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles radTextHorz.CheckedChanged
    IBSettings.EnableSettingsDialogApplyButton()
  End Sub

  Private Sub radTextVert_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles radTextVert.CheckedChanged
    IBSettings.EnableSettingsDialogApplyButton()
  End Sub

  Private Sub chkStaticMaxValues_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkStaticMaxValues.CheckedChanged
    IBSettings.EnableSettingsDialogApplyButton()

    lblStaticMaxDown.Enabled = chkStaticMaxValues.Checked
    txtGraphStaticMaxDown.Enabled = chkStaticMaxValues.Checked
    lblStaticMaxDownKbs.Enabled = chkStaticMaxValues.Checked
    lblStaticMaxUp.Enabled = chkStaticMaxValues.Checked
    txtGraphStaticMaxUp.Enabled = chkStaticMaxValues.Checked
    lblStaticMaxUpKbs.Enabled = chkStaticMaxValues.Checked
  End Sub

  Private Sub txtGraphStaticMaxDown_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtGraphStaticMaxDown.TextChanged
    IBSettings.EnableSettingsDialogApplyButton()
  End Sub

  Private Sub txtGraphStaticMaxUp_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtGraphStaticMaxUp.TextChanged
    IBSettings.EnableSettingsDialogApplyButton()
  End Sub

  Private Sub chkToolbarExternalIP_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkToolbarExternalIP.CheckedChanged
    IBSettings.EnableSettingsDialogApplyButton()
  End Sub

  Private Sub chkToolbarInternalIP_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkToolbarInternalIP.CheckedChanged
    IBSettings.EnableSettingsDialogApplyButton()
  End Sub

  Private Sub chkTooltipExternalIP_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkTooltipExternalIP.CheckedChanged
    IBSettings.EnableSettingsDialogApplyButton()
  End Sub

  Private Sub chkTooltipInternalIP_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkTooltipInternalIP.CheckedChanged
    IBSettings.EnableSettingsDialogApplyButton()
  End Sub

End Class