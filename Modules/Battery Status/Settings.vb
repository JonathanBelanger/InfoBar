Friend Class Settings

    ' This allows us to talk to the InfoBar settings dialog.
    Private IBSettings As New InfoBar.Services.Settings

    Private Sub chkShowIcon_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkShowIcon.CheckedChanged
        If chkShowIcon.Checked = False And chkShowText.Checked = False Then
            chkShowIcon.Checked = True
            MsgBox("At least one display option must be checked.", MsgBoxStyle.Information Or MsgBoxStyle.OkOnly, "Battery Status")
        Else
            IBSettings.EnableSettingsDialogApplyButton()
        End If
    End Sub

    Private Sub chkShowText_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkShowText.CheckedChanged
        If chkShowIcon.Checked = False And chkShowText.Checked = False Then
            chkShowText.Checked = True
            MsgBox("At least one display option must be checked.", MsgBoxStyle.Information Or MsgBoxStyle.OkOnly, "Battery Status")
        Else
            IBSettings.EnableSettingsDialogApplyButton()
        End If
    End Sub

End Class