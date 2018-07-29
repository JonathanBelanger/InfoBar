Friend Class Settings

  Private IBSettings As New InfoBar.Services.Settings

  Private Sub nudCheckInterval_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles nudCheckInterval.ValueChanged
    IBSettings.EnableSettingsDialogApplyButton()
  End Sub

  Private Sub txtEmail_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtEmail.TextChanged
    IBSettings.EnableSettingsDialogApplyButton()
  End Sub

  Private Sub txtPassword_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtPassword.TextChanged
    IBSettings.EnableSettingsDialogApplyButton()
  End Sub

End Class