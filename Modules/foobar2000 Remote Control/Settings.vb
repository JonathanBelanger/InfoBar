Friend Class Settings

    Private IBSettings As New InfoBar.Services.Settings

    Private Sub btnBrowse_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBrowse.Click
        Dim OFD As New OpenFileDialog
        OFD.AddExtension = True
        OFD.CheckFileExists = True
        OFD.CheckPathExists = True
        OFD.DefaultExt = ".exe"
        OFD.DereferenceLinks = True
        OFD.Filter = "Execuatable File (*.exe)|*.exe"
        OFD.Multiselect = False
        OFD.ShowReadOnly = False
        If OFD.ShowDialog = Windows.Forms.DialogResult.OK Then
            txtLocation.Text = OFD.FileName
            IBSettings.EnableSettingsDialogApplyButton()
        End If        
    End Sub

    Private Sub radButtonDisplayIcons_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles radButtonDisplayIcons.CheckedChanged
        IBSettings.EnableSettingsDialogApplyButton()
    End Sub

    Private Sub radButtonDisplayText_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles radButtonDisplayText.CheckedChanged
        IBSettings.EnableSettingsDialogApplyButton()
    End Sub

    Private Sub radButtonDisplayIconsAndText_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles radButtonDisplayIconsAndText.CheckedChanged
        IBSettings.EnableSettingsDialogApplyButton()
    End Sub

    Private Sub chkShowNowPlayingTooltip_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkShowNowPlayingTooltip.CheckedChanged
        IBSettings.EnableSettingsDialogApplyButton()
    End Sub

    Private Sub chkShowTrackRatingButton_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkShowTrackRatingButton.CheckedChanged
        IBSettings.EnableSettingsDialogApplyButton()
    End Sub

    Private Sub chkShowSongTimeInfo_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkShowSongTimeInfo.CheckedChanged
        IBSettings.EnableSettingsDialogApplyButton()
    End Sub

    Private Sub chkShowDuration_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkShowDuration.CheckedChanged
        IBSettings.EnableSettingsDialogApplyButton()
    End Sub

    Private Sub radSongTimeModeElapsed_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles radSongTimeModeElapsed.CheckedChanged
        IBSettings.EnableSettingsDialogApplyButton()
    End Sub

    Private Sub radSongTimeModeRemaining_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles radSongTimeModeRemaining.CheckedChanged
        IBSettings.EnableSettingsDialogApplyButton()
    End Sub

    Private Sub nudWidth_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles nudWidth.ValueChanged
        IBSettings.EnableSettingsDialogApplyButton()
    End Sub

    Private Sub chkShowSongTagInfo_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkShowSongTagInfo.CheckedChanged
        IBSettings.EnableSettingsDialogApplyButton()
    End Sub

    Private Sub txtTagStringFormat_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtTagStringFormat.TextChanged
        IBSettings.EnableSettingsDialogApplyButton()
    End Sub

    Private Sub nudSongTagWidth_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles nudSongTagWidth.ValueChanged
        IBSettings.EnableSettingsDialogApplyButton()
    End Sub

    Private Sub chkShowAlbumArt_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkShowAlbumArt.CheckedChanged
        IBSettings.EnableSettingsDialogApplyButton()
    End Sub

    Private Sub txtAlbumArtFormattingString_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtAlbumArtFormattingString.TextChanged
        IBSettings.EnableSettingsDialogApplyButton()
    End Sub

    Private Sub nudAlbumArtWidth_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles nudAlbumArtWidth.ValueChanged
        IBSettings.EnableSettingsDialogApplyButton()
    End Sub

    Private Sub chkShowTrackRating_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkShowTrackRating.CheckedChanged
        IBSettings.EnableSettingsDialogApplyButton()
    End Sub

    Private Sub txtTooltipFormatString_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtTooltipFormatString.TextChanged
        IBSettings.EnableSettingsDialogApplyButton()
    End Sub

    Private Sub nudTooltipTextMaxWidth_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles nudTooltipTextMaxWidth.ValueChanged
        IBSettings.EnableSettingsDialogApplyButton()
    End Sub

    Private Sub chkSongTitleShowTrackRating_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkSongTitleShowTrackRating.CheckedChanged
        If chkSongTitleShowTrackRating.Checked = True Then
            lblSongTitleTrackRatingOpacity.Enabled = True
            nudSongTitleTrackRatingOpacity.Enabled = True
        Else
            lblSongTitleTrackRatingOpacity.Enabled = False
            nudSongTitleTrackRatingOpacity.Enabled = False
        End If
        IBSettings.EnableSettingsDialogApplyButton()
    End Sub

    Private Sub nudSongTitleTrackRatingOpacity_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles nudSongTitleTrackRatingOpacity.ValueChanged
        IBSettings.EnableSettingsDialogApplyButton()
    End Sub

    Private Sub btnGoTitleFormatting_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGoTitleFormatting.Click
        IBSettings.OpenWebLink("http://wiki.hydrogenaudio.org/index.php?title=Foobar2000:Titleformat_Reference")
    End Sub

    Private Sub btnDownloadComServer_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDownloadComServer.Click
        IBSettings.OpenWebLink("http://foosion.foobar2000.org/0.9/")
    End Sub

End Class