Friend Class Settings

  Private IBSettings As New InfoBar.Services.Settings

  Private Sub nudRefreshInterval_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles nudRefreshInterval.ValueChanged
    IBSettings.EnableSettingsDialogApplyButton()
  End Sub

  Private Sub txtPassword_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtPassword.TextChanged
    IBSettings.EnableSettingsDialogApplyButton()
  End Sub

  Private Sub txtUsername_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtUsername.TextChanged
    IBSettings.EnableSettingsDialogApplyButton()
  End Sub

  Private Sub txtHostname_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtHostname.TextChanged
    IBSettings.EnableSettingsDialogApplyButton()
  End Sub

  Private Sub txtPort_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtPort.TextChanged
    IBSettings.EnableSettingsDialogApplyButton()
  End Sub

  Private Sub btnLocationBrowse_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLocationBrowse.Click
    Dim ofd As New OpenFileDialog
    ofd.AddExtension = True
    ofd.CheckFileExists = True
    ofd.CheckPathExists = True
    ofd.DefaultExt = "exe"
    ofd.FileName = "uTorrent.exe"
    ofd.Filter = "Executables|*.exe"
    ofd.Multiselect = False
    If ofd.ShowDialog = DialogResult.OK Then
      txtLocation.Text = ofd.FileName
      IBSettings.EnableSettingsDialogApplyButton()
    End If
  End Sub

End Class