Friend Class Settings
  Private IBSettings As New InfoBar.Services.Settings

  Private Sub radButtonAppearance_ShowIconsOnly_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles radButtonAppearance_ShowIconsOnly.CheckedChanged
    IBSettings.EnableSettingsDialogApplyButton()
  End Sub

  Private Sub radButtonAppearance_ShowTextOnly_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles radButtonAppearance_ShowTextOnly.CheckedChanged
    IBSettings.EnableSettingsDialogApplyButton()
  End Sub

  Private Sub radButtonAppearance_ShowIconsAndText_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles radButtonAppearance_ShowIconsAndText.CheckedChanged
    IBSettings.EnableSettingsDialogApplyButton()
  End Sub

  Private Sub radButtonAppearanceIconsFilesIconsAndTextFolders_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles radButtonAppearanceIconsFilesIconsAndTextFolders.CheckedChanged
    IBSettings.EnableSettingsDialogApplyButton()
  End Sub

  Private Sub chkShowPathInTooltip_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkShowPathInTooltip.CheckedChanged
    IBSettings.EnableSettingsDialogApplyButton()
  End Sub

  Private Sub chkShowFoldersAsDropDownMenus_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkShowFoldersAsDropDownMenus.CheckedChanged
    IBSettings.EnableSettingsDialogApplyButton()
  End Sub

End Class