Friend Class Settings

  ' This allows us to talk to the InfoBar settings dialog.
  Private IBSettings As New InfoBar.Services.Settings

  Private Sub chkShowIcon_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkShowIcon.CheckedChanged
    IBSettings.EnableSettingsDialogApplyButton()
  End Sub

  Private Sub chkShowText_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkShowText.CheckedChanged
    IBSettings.EnableSettingsDialogApplyButton()
  End Sub

  Private Sub nudSliderWidth_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles nudSliderWidth.ValueChanged
    IBSettings.EnableSettingsDialogApplyButton()
  End Sub

End Class