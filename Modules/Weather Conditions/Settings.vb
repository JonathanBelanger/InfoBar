Friend Class Settings
  Private IBSettings As New InfoBar.Services.Settings

  Dim PrevSelection As Integer
  Public IsFormLoaded As Boolean = False

  Private Sub btnLocationSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLocationSearch.Click
    Dim dlg As New frmLocationSearch
    If dlg.ShowDialog() = Windows.Forms.DialogResult.OK Then
      txtLocationID.Text = dlg.WeatherLocations(dlg.lstResults.SelectedIndex + 1).ID
      txtLocation.Text = dlg.WeatherLocations(dlg.lstResults.SelectedIndex + 1).Name
      IBSettings.EnableSettingsDialogApplyButton()
    End If
    dlg.Dispose()
  End Sub

  Private Sub chkUpdateAtIntervals_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkUpdateAtIntervals.CheckedChanged
    Dim bEnabled As Boolean = chkUpdateAtIntervals.Checked
    lblIntervalEvery.Enabled = bEnabled
    nudUpdateInterval.Enabled = bEnabled
    lblCheckMinutes.Enabled = bEnabled
    IBSettings.EnableSettingsDialogApplyButton()
  End Sub

  Private Sub lvImageSet_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles lvImageSet.MouseUp
    Try
      If lvImageSet.SelectedItems.Count = 0 Then
        IsFormLoaded = False
        lvImageSet.Items(PrevSelection).Selected = True
        IsFormLoaded = True
      End If
    Catch ex As Exception
    End Try
  End Sub

  Private Sub lvImageSet_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lvImageSet.SelectedIndexChanged
    Try
      PrevSelection = lvImageSet.SelectedItems(0).Index
    Catch ex As Exception
    End Try
    GenerateImageSetPreview()
    If IsFormLoaded Then IBSettings.EnableSettingsDialogApplyButton()
  End Sub

  Private Sub picTWCi_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles picTWCi.Click
    IBSettings.OpenWebLink("http://www.weather.com/?par=" & sWeatherPartnerID & "&prod=xoap")
  End Sub

  Private Sub picTWCi_MouseEnter(ByVal sender As Object, ByVal e As System.EventArgs) Handles picTWCi.MouseEnter
    Me.Cursor = Cursors.Hand
  End Sub

  Private Sub picTWCi_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs) Handles picTWCi.MouseLeave
    Me.Cursor = Cursors.Default
  End Sub

  Private Sub llWeatherDotCom_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles llWeatherDotCom.LinkClicked
    IBSettings.OpenWebLink("http://www.weather.com/?par=" & sWeatherPartnerID & "&prod=xoap")
  End Sub

  Private Sub radMeasurementCustomary_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles radMeasurementCustomary.CheckedChanged
    IBSettings.EnableSettingsDialogApplyButton()
  End Sub

  Private Sub radMeasurementMetric_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles radMeasurementMetric.CheckedChanged
    IBSettings.EnableSettingsDialogApplyButton()
  End Sub

  Private Sub nudUpdateInterval_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles nudUpdateInterval.ValueChanged
    IBSettings.EnableSettingsDialogApplyButton()
  End Sub

  Private Sub chkShowIcon_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkShowIcon.CheckedChanged
    If chkShowIcon.Checked = False And chkTextTemp.Checked = False And chkTextCondition.Checked = False Then
      chkShowIcon.Checked = True
      MsgBox("At least one display option must be selected.", MsgBoxStyle.Information Or MsgBoxStyle.OkOnly, "Weather Conditions")
    Else
      IBSettings.EnableSettingsDialogApplyButton()
    End If
  End Sub

  Private Sub chkTextTemp_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkTextTemp.CheckedChanged
    If chkShowIcon.Checked = False And chkTextTemp.Checked = False And chkTextCondition.Checked = False Then
      chkTextTemp.Checked = True
      MsgBox("At least one display option must be selected.", MsgBoxStyle.Information Or MsgBoxStyle.OkOnly, "Weather Conditions")
    Else
      IBSettings.EnableSettingsDialogApplyButton()
    End If
  End Sub

  Private Sub chkTextCondition_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkTextCondition.CheckedChanged
    If chkShowIcon.Checked = False And chkTextTemp.Checked = False And chkTextCondition.Checked = False Then
      chkTextCondition.Checked = True
      MsgBox("At least one display option must be selected.", MsgBoxStyle.Information Or MsgBoxStyle.OkOnly, "Weather Conditions")
    Else
      IBSettings.EnableSettingsDialogApplyButton()
    End If
  End Sub

  Private Sub lstTooltipContents_ItemCheck(ByVal sender As Object, ByVal e As System.Windows.Forms.ItemCheckEventArgs) Handles lstTooltipContents.ItemCheck
    IBSettings.EnableSettingsDialogApplyButton()
  End Sub

  Friend Sub Settings_Load()
    ' Enumerate Image Sets
    lvImageSet.Items.Clear()

    Try
      Dim sImageDir As String = Application.StartupPath & "\Weather Icons\"
      If IO.Directory.Exists(sImageDir) = False Then IO.Directory.CreateDirectory(sImageDir)
      Dim sDirs() As String = IO.Directory.GetDirectories(sImageDir)

      Dim Doc As New XmlDocument
      Dim LVI As ListViewItem
      For Each sDir As String In sDirs
        If sDir.EndsWith(".svn") = False Then
          Try
            Doc.Load(sDir & "\iconset.xml")
            Dim sName As String = Doc.DocumentElement.GetAttribute("name")
            Dim sAuthor As String = Doc.DocumentElement.GetAttribute("author")
            Dim sVersion As String = Doc.DocumentElement.GetAttribute("version")

            LVI = New ListViewItem
            LVI.Text = sName
            LVI.SubItems.Add(sAuthor)
            LVI.SubItems.Add(sVersion)
            LVI.Tag = sDir
            lvImageSet.Items.Add(LVI)
          Catch ex As Exception
            MsgBox("The icon set xml info file for " & sDir & " is invalid or missing.", MsgBoxStyle.Exclamation Or MsgBoxStyle.OkOnly, "InfoBar")
          End Try
        End If
      Next
      lvImageSet.Sort()
    Catch ex As Exception
    End Try

    Dim LVID As New ListViewItem
    LVID.Text = "Konfabulator Plus (Default)"
    LVID.SubItems.Add("plajko")
    LVID.SubItems.Add("1.0")
    LVID.Tag = "InfoBarInternalWeatherImageSet"
    lvImageSet.Items.Insert(0, LVID)

    lvImageSet.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize)
    GenerateImageSetPreview()
  End Sub

  Friend Sub GenerateImageSetPreview()
    Dim newImg As Bitmap = New Bitmap(324, 48, Imaging.PixelFormat.Format32bppArgb)
    Dim Gr As Graphics = Graphics.FromImage(newImg)
    Gr.CompositingQuality = CompositingQuality.HighQuality
    Gr.InterpolationMode = InterpolationMode.HighQualityBicubic
    Gr.PixelOffsetMode = PixelOffsetMode.HighQuality
    Gr.SmoothingMode = SmoothingMode.HighQuality
    If lvImageSet.SelectedItems.Count = 0 Then
      Gr.Clear(Color.Transparent)
    Else
      If lvImageSet.SelectedItems(0).Tag = "InfoBarInternalWeatherImageSet" Then
        Gr.DrawImage(My.Resources.defaultweathericonset_32, 0, 0, 48, 48)
        Gr.DrawImage(My.Resources.defaultweathericonset_0, 54, 0, 48, 48)
        Gr.DrawImage(My.Resources.defaultweathericonset_25, 108, 0, 48, 48)
        Gr.DrawImage(My.Resources.defaultweathericonset_46, 162, 0, 48, 48)
        Gr.DrawImage(My.Resources.defaultweathericonset_12, 216, 0, 48, 48)
        Gr.DrawImage(My.Resources.defaultweathericonset_4, 270, 0, 48, 48)
      Else
        Dim sPath As String = lvImageSet.SelectedItems(0).Tag & "\"
        Try
          Dim Skin As New InfoBar.Services.Skinning
          Gr.DrawImage(Skin.LoadImageFromFile(sPath & "32.png"), 0, 0, 48, 48)
          Gr.DrawImage(Skin.LoadImageFromFile(sPath & "0.png"), 54, 0, 48, 48)
          Gr.DrawImage(Skin.LoadImageFromFile(sPath & "25.png"), 108, 0, 48, 48)
          Gr.DrawImage(Skin.LoadImageFromFile(sPath & "46.png"), 162, 0, 48, 48)
          Gr.DrawImage(Skin.LoadImageFromFile(sPath & "12.png"), 216, 0, 48, 48)
          Gr.DrawImage(Skin.LoadImageFromFile(sPath & "4.png"), 270, 0, 48, 48)
        Catch
          MsgBox("Not all images were found while creating a preview image for the selected image set. It is possible that the image set is incomplete.", MsgBoxStyle.Exclamation Or MsgBoxStyle.OkOnly, "InfoBar")
        End Try
      End If
    End If
    Gr.Dispose()
    Me.picImageSetPreview.Image = newImg
  End Sub

End Class