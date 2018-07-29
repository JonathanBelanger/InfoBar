Friend Class Settings

  ' This allows us to talk to the InfoBar settings dialog.
  Private IBSettings As New InfoBar.Services.Settings

  Private Sub chkShowIcon_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkShowIcon.CheckedChanged
    If chkShowIcon.Checked = False And chkShowCapsLockIcon.Checked = False And chkShowNumLockIcon.Checked = False And chkShowScrollLockIcon.Checked = False Then
      chkShowIcon.Checked = True
      MsgBox("At least one display option must be checked.", MsgBoxStyle.Information Or MsgBoxStyle.OkOnly, "Keyboard Status")
    Else
      IBSettings.EnableSettingsDialogApplyButton()
    End If
  End Sub

  Private Sub chkShowCapsLockIcon_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkShowCapsLockIcon.CheckedChanged
    If chkShowIcon.Checked = False And chkShowCapsLockIcon.Checked = False And chkShowNumLockIcon.Checked = False And chkShowScrollLockIcon.Checked = False Then
      chkShowCapsLockIcon.Checked = True
      MsgBox("At least one display option must be checked.", MsgBoxStyle.Information Or MsgBoxStyle.OkOnly, "Keyboard Status")
    Else
      IBSettings.EnableSettingsDialogApplyButton()
    End If
  End Sub

  Private Sub chkShowNumLockIcon_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkShowNumLockIcon.CheckedChanged
    If chkShowIcon.Checked = False And chkShowCapsLockIcon.Checked = False And chkShowNumLockIcon.Checked = False And chkShowScrollLockIcon.Checked = False Then
      chkShowNumLockIcon.Checked = True
      MsgBox("At least one display option must be checked.", MsgBoxStyle.Information Or MsgBoxStyle.OkOnly, "Keyboard Status")
    Else
      IBSettings.EnableSettingsDialogApplyButton()
    End If
  End Sub

  Private Sub chkShowScrollLockIcon_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkShowScrollLockIcon.CheckedChanged
    If chkShowIcon.Checked = False And chkShowCapsLockIcon.Checked = False And chkShowNumLockIcon.Checked = False And chkShowScrollLockIcon.Checked = False Then
      chkShowScrollLockIcon.Checked = True
      MsgBox("At least one display option must be checked.", MsgBoxStyle.Information Or MsgBoxStyle.OkOnly, "Keyboard Status")
    Else
      IBSettings.EnableSettingsDialogApplyButton()
    End If
  End Sub

End Class