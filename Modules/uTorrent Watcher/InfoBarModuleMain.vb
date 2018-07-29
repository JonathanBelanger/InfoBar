Public Class InfoBarModuleMain
  Inherits InfoBarModule

  ' Unique ID. If same as other module, the first module will be loaded, the next will be ignored.
  Private Const InfoBarModuleGUID As String = "{618BD62F-1CE2-40fd-93F2-22DB757CE226}"

  Private Const uTorrent_NoJobsText = "There are no torrent jobs running."

#Region "Windows API"
  Private Declare Function ShellExecute Lib "shell32.dll" Alias "ShellExecuteA" (ByVal hWnd As IntPtr, ByVal lpOperation As String, ByVal lpFile As String, ByVal lpParameters As String, ByVal lpDirectory As String, ByVal nShowCmd As Integer) As IntPtr
#End Region

#Region "InfoBar Service Objects"
  Private Skin As New InfoBar.Services.Skinning
  Private Tooltip As New InfoBar.Services.Tooltips
  Private IconTheme As New InfoBar.Services.IconTheme
  Private MainMenu As New InfoBar.Services.PopupMenus.MainMenu
  Private Settings As New InfoBar.Services.Settings
  Private Utilities As New InfoBar.Services.Utilities
#End Region

#Region "Private Classes and Variables"
  Private Class Torrent
    Public Hash As String
    Public Status As String
    Public Name As String
    Public Size As String
    Public PercentProgress As String
    Public Downloaded As String
    Public Uploaded As String
    Public Ratio As String
    Public UploadSpeed As String
    Public DownloadSpeed As String
    Public ETA As String
    Public Label As String
    Public PeersConnected As String
    Public PeersInSwarm As String
    Public SeedsConnected As String
    Public SeedsInSwarm As String
    Public Availability As String
    Public TorrentQueueOrder As String
    Public Remaining As String
  End Class

  Private WithEvents webRequest As New WebClient
  Private Torrents As New Collection
  Private CacheID As String
  Private m_ModuleToolbarBitmap As Bitmap
  Private m_ModuleTooltipBitmap As Bitmap
  Private m_SettingsDialog As New Settings
  Private m_Enabled As Boolean
  Private m_IsManualRefresh As Boolean
  Private m_UpdateInProgress As Boolean
  Private m_ErrorChecking As Boolean
  Private m_Text As String
  Private m_ErrorMessage As String
  Private m_uTorrentButtonBounds As Rectangle
  Private m_uTorrentButtonState As Integer
  Private m_LastCheckTime As DateTime
  Private ttCol1 As Integer, ttCol2 As Integer, ttCol3 As Integer, ttCol4 As Integer, ttCol5 As Integer
#End Region

#Region "Settings Variables"
  Private Setting_Path As String
  Private Setting_HostName As String
  Private Setting_Port As Integer
  Private Setting_Username As String
  Private Setting_Password As String
  Private Setting_RefreshInterval As Integer
#End Region

#Region "Module Information"

  Dim ModuleInfo As New ApplicationServices.AssemblyInfo(System.Reflection.Assembly.GetExecutingAssembly)

  ' This gives InfoBar your unique ID for the module. Generate it with the GUID tool, use registry format.
  Public Overrides ReadOnly Property ModuleGUID() As String
    Get
      Return InfoBarModuleGUID
    End Get
  End Property

  ' This gives InfoBar a friendly name for the module.
  Public Overrides ReadOnly Property ModuleName() As String
    Get
      Return ModuleInfo.Title
    End Get
  End Property

  ' This gives InfoBar a copy of the modules icon for the settings dialog.
  Public Overrides ReadOnly Property ModuleIcon() As Image
    Get
      Return My.Resources.icon
    End Get
  End Property

  ' This gives InfoBar the author of the module, used in settings dialog.
  Public Overrides ReadOnly Property ModuleAuthor() As String
    Get
      Return ModuleInfo.CompanyName
    End Get
  End Property

  ' This gives InfoBar the version of the module, used in settings dialog.
  ' This must be the same as the file version.
  Public Overrides ReadOnly Property ModuleVersion() As String
    Get
      Return ModuleInfo.Version.ToString
    End Get
  End Property

  ' InfoBar retrieves this for the settings page.
  Public Overrides ReadOnly Property ModuleDescription() As String
    Get
      Return "Watches the status of torrents being downloaded by uTorrent."
    End Get
  End Property

  ' InfoBar retrieves this for the settings page.
  Public Overrides ReadOnly Property ModuleCopyright() As String
    Get
      Return ModuleInfo.Copyright
    End Get
  End Property

  ' InfoBar retrieves this for the settings page.
  Public Overrides ReadOnly Property ModuleEmail() As String
    Get
      Return "info@nightiguana.com"
    End Get
  End Property

  ' InfoBar retrieves this for the settings page.
  Public Overrides ReadOnly Property ModuleHomepage() As String
    Get
      Return "http://www.nightiguana.com"
    End Get
  End Property

  ' InfoBar will use this to determine if the module is shown on the UI.
  ' It will be set to true/false when the user enables/disables the module, respectively.
  Public Overrides Property ModuleEnabled() As Boolean
    Get
      Return m_Enabled
    End Get
    Set(ByVal value As Boolean)
      m_Enabled = value
    End Set
  End Property

#End Region

#Region "Module Initialization/Finalization/Updates"

  ' InfoBar will call this when your module is enabled.
  Public Overrides Sub InitializeModule()
    Torrents.Clear()
    CacheID = Nothing

    m_IsManualRefresh = True
    m_UpdateInProgress = True
    uTorrent_Refresh()
  End Sub

  ' InfoBar will call this when your module is disabled.
  Public Overrides Sub FinalizeModule()
    '
  End Sub

  ' InfoBar will call this every 1 second. Make sure to do work only when needed.
  ' TIP: If m_Enabled is set to false, don't do any work to save CPU time unless really needed.
  Public Overrides Sub TimerTick(ByRef bModuleIsDirty As Boolean)
    uTorrent_Refresh()
  End Sub

#End Region

#Region "Module Drawing"

  Public Overrides Function GetModuleBitmap() As System.Drawing.Bitmap
    Try
      Return m_ModuleToolbarBitmap.Clone
    Catch ex As Exception
      Return Nothing
    End Try
  End Function

  ' This gets called every time InfoBar updates the main toolbar window UI.
  ' Update XPos with the total width that you need to show your module.
  Private Sub DrawModule()
    Dim width As Integer = 0, height As Integer = Skin.MaxModuleHeight

    Dim uTorrentIcon As Image
    If IconTheme.IsDefaultTheme = True Then
      uTorrentIcon = My.Resources.icon
    Else
      uTorrentIcon = IconTheme.GetThemeIcon(InfoBarModuleGUID, "uTorrent")
      If uTorrentIcon Is Nothing Then uTorrentIcon = My.Resources.icon
    End If

    Dim grm As Graphics = Graphics.FromHwnd(IntPtr.Zero)

    ' Draw Gmail Button
    Dim bR As New Rectangle(0, 0, 1, height)
    Dim bFR As Rectangle
    bFR = Skin.MeasureButton(grm, bR, uTorrentIcon, m_Text, m_uTorrentButtonState, False)
    width += bFR.Width

    grm.Dispose()

    If width <= 1 Or height <= 1 Then Exit Sub
    Dim bmpTemp As New Bitmap(width, height, System.Drawing.Imaging.PixelFormat.Format32bppArgb)
    Dim GR As Graphics = Graphics.FromImage(bmpTemp)

    ' Get skin height for vertical centering
    Dim curX As Integer = 0

    ' Draw Gmail Button
    bR = New Rectangle(curX, 0, 1, height)
    bFR = Skin.DrawButton(Gr, bR, uTorrentIcon, m_Text, m_uTorrentButtonState, False)
    m_uTorrentButtonBounds = bFR
    curX = curX + bFR.Width

    GR.Dispose()
    m_ModuleToolbarBitmap = bmpTemp.Clone
    bmpTemp.Dispose()
  End Sub

  Public Overrides Function GetTooltipBitmap() As System.Drawing.Bitmap
    Try
      Return m_ModuleTooltipBitmap
    Catch ex As Exception
      Return Nothing
    End Try
  End Function

  ' InfoBar will call this when it needs to draw your tooltip.
  ' Do all of your graphics or text drawing here.
  Private Sub DrawTooltip(ByVal ObjectID As String)
    Select Case ObjectID
      Case "uTorrentWatcher"
        Dim nextX As Integer = 0, nextY As Integer = 0
        Dim maxWidth As Integer, maxHeight As Integer
        Dim tr As Rectangle

        Dim maxTTWidth As Integer = Screen.PrimaryScreen.WorkingArea.Width - (Skin.ContentMargins(Skinning.SkinMarginPart.Tooltip).Left + Skin.ContentMargins(Skinning.SkinMarginPart.Tooltip).Right)
        Dim maxTTHeight As Integer = Screen.PrimaryScreen.WorkingArea.Height - (Skin.ContentMargins(Skinning.SkinMarginPart.Tooltip).Top + Skin.ContentMargins(Skinning.SkinMarginPart.Tooltip).Bottom)

        Dim GR As Graphics = Graphics.FromHwnd(IntPtr.Zero)

        ' Error Message
        If m_ErrorChecking Then

          tr = Skin.MeasureText(Gr, m_ErrorMessage, Skinning.SkinTextPart.TooltipText, maxTTWidth)
          maxWidth = tr.Width
          nextY += tr.Height
          maxWidth = tr.Width
          maxHeight = nextY

        Else

          ' Last Checked
          Dim sText As String = "Last Checked: " & m_LastCheckTime.ToString
          tr = Skin.MeasureText(Gr, sText, Skinning.SkinTextPart.TooltipText)
          If tr.Width > maxWidth Then maxWidth = tr.Width
          nextY += tr.Height + Skin.TooltipSeparatorHeight

          If Torrents.Count > 0 Then

            Dim tCount As Integer = 0
            For Each t As Torrent In Torrents
              tCount += 1

              ' Name: Row 0, Columns 1-5
              tr = Skin.MeasureText(Gr, t.Name, Skinning.SkinTextPart.TooltipText, 600)
              If tr.Width > maxWidth Then maxWidth = tr.Width
              nextY += tr.Height

              ' Status: Row 1, Column 1
              tr = Skin.MeasureText(Gr, "Status: " & t.Status, Skinning.SkinTextPart.TooltipText, 600)
              If tr.Width > ttCol1 Then ttCol1 = tr.Width

              ' Size: Row 1, Column 2
              tr = Skin.MeasureText(Gr, "Size: " & t.Size, Skinning.SkinTextPart.TooltipText, 600)
              If tr.Width > ttCol2 Then ttCol2 = tr.Width

              ' Done: Row 1, Column 3
              tr = Skin.MeasureText(Gr, "Done: " & t.PercentProgress, Skinning.SkinTextPart.TooltipText, 600)
              If tr.Width > ttCol3 Then ttCol3 = tr.Width

              ' Downloaded: Row 1, Column 4
              tr = Skin.MeasureText(Gr, "Downloaded: " & t.Downloaded, Skinning.SkinTextPart.TooltipText, 600)
              If tr.Width > ttCol4 Then ttCol4 = tr.Width

              ' Uploaded: Row 1, Column 5
              tr = Skin.MeasureText(Gr, "Uploaded: " & t.Uploaded, Skinning.SkinTextPart.TooltipText, 600)
              If tr.Width > ttCol5 Then ttCol5 = tr.Width

              ' Move to next row
              nextY += tr.Height

              ' Ratio: Row 2, Column 1
              tr = Skin.MeasureText(Gr, "Ratio: " & t.Ratio, Skinning.SkinTextPart.TooltipText, 600)
              If tr.Width > ttCol1 Then ttCol1 = tr.Width

              ' Download Speed: Row 2, Column 2
              tr = Skin.MeasureText(Gr, "Download Speed: " & t.DownloadSpeed, Skinning.SkinTextPart.TooltipText, 600)
              If tr.Width > ttCol2 Then ttCol2 = tr.Width

              ' Upload Speed: Row 2, Column 3
              tr = Skin.MeasureText(Gr, "Upload Speed: " & t.UploadSpeed, Skinning.SkinTextPart.TooltipText, 600)
              If tr.Width > ttCol3 Then ttCol3 = tr.Width

              ' ETA: Row 2, Column 4
              tr = Skin.MeasureText(Gr, "ETA: " & t.ETA, Skinning.SkinTextPart.TooltipText, 600)
              If tr.Width > ttCol4 Then ttCol4 = tr.Width

              ' Label: Row 2, Column 5
              tr = Skin.MeasureText(Gr, "Label: " & t.Label, Skinning.SkinTextPart.TooltipText, 600)
              If tr.Width > ttCol5 Then ttCol5 = tr.Width

              ' Move to next row
              nextY += tr.Height

              ' Peers Connected: Row 3, Column 1
              tr = Skin.MeasureText(Gr, "Peers Connected: " & t.PeersConnected, Skinning.SkinTextPart.TooltipText, 600)
              If tr.Width > ttCol1 Then ttCol1 = tr.Width

              ' Peers In Swarm: Row 3, Column 2
              tr = Skin.MeasureText(Gr, "Peers In Swarm: " & t.PeersInSwarm, Skinning.SkinTextPart.TooltipText, 600)
              If tr.Width > ttCol2 Then ttCol2 = tr.Width

              ' Seeds Connected: Row 3, Column 3
              tr = Skin.MeasureText(Gr, "Seeds Connected: " & t.SeedsConnected, Skinning.SkinTextPart.TooltipText, 600)
              If tr.Width > ttCol3 Then ttCol3 = tr.Width

              ' Seeds In Swarm: Row 3, Column 4
              tr = Skin.MeasureText(Gr, "Seeds In Swarm: " & t.SeedsInSwarm, Skinning.SkinTextPart.TooltipText, 600)
              If tr.Width > ttCol4 Then ttCol4 = tr.Width

              ' Availability: Row 3, Column 5
              tr = Skin.MeasureText(Gr, "Availability: " & t.Availability, Skinning.SkinTextPart.TooltipText, 600)
              If tr.Width > ttCol5 Then ttCol5 = tr.Width

              ' Move to next row
              nextY += tr.Height

              ' Remaining: Row 4, Column 1
              tr = Skin.MeasureText(Gr, "Remaining: " & t.Remaining, Skinning.SkinTextPart.TooltipText, 600)
              If tr.Width > ttCol1 Then ttCol1 = tr.Width

              ' Finish this row
              nextY += tr.Height

              If tCount < Torrents.Count Then nextY += Skin.TooltipSeparatorHeight
            Next

            ' Pad columns
            If (ttCol1 + ttCol2 + ttCol3 + ttCol4 + ttCol5 + 25) > maxWidth Then maxWidth = (ttCol1 + ttCol2 + ttCol3 + ttCol4 + ttCol5 + 25)

          Else

            tr = Skin.MeasureText(Gr, uTorrent_NoJobsText, Skinning.SkinTextPart.TooltipText, 600)
            If tr.Width > maxWidth Then maxWidth = tr.Width
            nextY += tr.Height

          End If

          maxHeight = nextY

        End If

        GR.Dispose()

        Dim bmpTemp As New Bitmap(maxWidth, maxHeight, System.Drawing.Imaging.PixelFormat.Format32bppArgb)
        GR = Graphics.FromImage(bmpTemp)

        Dim rect As Rectangle, curX As Integer, curY As Integer

        curX = 0
        curY = 0

        ' Error Message
        If m_ErrorChecking Then

          rect = Skin.MeasureText(GR, m_ErrorMessage, Skinning.SkinTextPart.TooltipText, maxWidth)
          rect.X = curX
          rect.Y = curY
          Skin.DrawText(Gr, m_ErrorMessage, rect, Skinning.SkinTextPart.TooltipText, StringAlignment.Near, StringAlignment.Near, True)

        Else

          ' Last Checked
          rect = Skin.MeasureText(GR, "Last Checked: " & m_LastCheckTime.ToString, Skinning.SkinTextPart.TooltipText, maxWidth)
          rect.X = curX
          rect.Y = curY
          Skin.DrawText(Gr, "Last Checked: " & m_LastCheckTime.ToString, rect, Skinning.SkinTextPart.TooltipText, StringAlignment.Near, StringAlignment.Near)
          curY = curY + rect.Height

          rect.Y = curY
          rect.Width = maxWidth
          rect.Height = Skin.TooltipSeparatorHeight
          Skin.DrawTooltipSeparator(Gr, rect)
          curY += Skin.TooltipSeparatorHeight

          If Torrents.Count > 0 Then
            Dim tCount As Integer = 0
            For Each t As Torrent In Torrents
              tCount += 1

              ' Name: Row 0, Columns 1-5
              rect = Skin.MeasureText(Gr, t.Name, Skinning.SkinTextPart.TooltipText, 600)
              rect.X = curX
              rect.Y = curY
              Skin.DrawText(Gr, t.Name, rect, Skinning.SkinTextPart.TooltipText, StringAlignment.Near, StringAlignment.Near, True)
              curY += rect.Height

              ' Status: Row 1, Column 1
              rect = Skin.MeasureText(Gr, "Status: " & t.Status, Skinning.SkinTextPart.TooltipText, 600)
              rect.X = curX
              rect.Y = curY
              Skin.DrawText(Gr, "Status: " & t.Status, rect, Skinning.SkinTextPart.TooltipText, StringAlignment.Near, StringAlignment.Near, True)

              ' Size: Row 1, Column 2
              rect = Skin.MeasureText(Gr, "Size: " & t.Size, Skinning.SkinTextPart.TooltipText, 600)
              rect.X = curX + ttCol1 + 5
              rect.Y = curY
              Skin.DrawText(Gr, "Size: " & t.Size, rect, Skinning.SkinTextPart.TooltipText, StringAlignment.Near, StringAlignment.Near, True)

              ' Done: Row 1, Column 3
              rect = Skin.MeasureText(Gr, "Done: " & t.PercentProgress, Skinning.SkinTextPart.TooltipText, 600)
              rect.X = curX + ttCol1 + ttCol2 + 5
              rect.Y = curY
              Skin.DrawText(Gr, "Done: " & t.PercentProgress, rect, Skinning.SkinTextPart.TooltipText, StringAlignment.Near, StringAlignment.Near, True)

              ' Downloaded: Row 1, Column 4
              rect = Skin.MeasureText(Gr, "Downloaded: " & t.Downloaded, Skinning.SkinTextPart.TooltipText, 600)
              rect.X = curX + ttCol1 + ttCol2 + ttCol3 + 5
              rect.Y = curY
              Skin.DrawText(Gr, "Downloaded: " & t.Downloaded, rect, Skinning.SkinTextPart.TooltipText, StringAlignment.Near, StringAlignment.Near, True)

              ' Uploaded: Row 1, Column 5
              rect = Skin.MeasureText(Gr, "Uploaded: " & t.Uploaded, Skinning.SkinTextPart.TooltipText, 600)
              rect.X = curX + ttCol1 + ttCol2 + ttCol3 + ttCol4 + 5
              rect.Y = curY
              Skin.DrawText(Gr, "Uploaded: " & t.Uploaded, rect, Skinning.SkinTextPart.TooltipText, StringAlignment.Near, StringAlignment.Near, True)

              ' Move to next row
              curY += rect.Height

              ' Ratio: Row 2, Column 1
              rect = Skin.MeasureText(Gr, "Ratio: " & t.Ratio, Skinning.SkinTextPart.TooltipText, 600)
              rect.X = curX
              rect.Y = curY
              Skin.DrawText(Gr, "Ratio: " & t.Ratio, rect, Skinning.SkinTextPart.TooltipText, StringAlignment.Near, StringAlignment.Near, True)

              ' Download Speed: Row 2, Column 2
              rect = Skin.MeasureText(Gr, "Download Speed: " & t.DownloadSpeed, Skinning.SkinTextPart.TooltipText, 600)
              rect.X = curX + ttCol1 + 5
              rect.Y = curY
              Skin.DrawText(Gr, "Download Speed: " & t.DownloadSpeed, rect, Skinning.SkinTextPart.TooltipText, StringAlignment.Near, StringAlignment.Near, True)

              ' Upload Speed: Row 2, Column 3
              rect = Skin.MeasureText(Gr, "Upload Speed: " & t.UploadSpeed, Skinning.SkinTextPart.TooltipText, 600)
              rect.X = curX + ttCol1 + ttCol2 + 5
              rect.Y = curY
              Skin.DrawText(Gr, "Upload Speed: " & t.UploadSpeed, rect, Skinning.SkinTextPart.TooltipText, StringAlignment.Near, StringAlignment.Near, True)

              ' ETA: Row 2, Column 4
              rect = Skin.MeasureText(Gr, "ETA: " & t.ETA, Skinning.SkinTextPart.TooltipText, 600)
              rect.X = curX + ttCol1 + ttCol2 + ttCol3 + 5
              rect.Y = curY
              Skin.DrawText(Gr, "ETA: " & t.ETA, rect, Skinning.SkinTextPart.TooltipText, StringAlignment.Near, StringAlignment.Near, True)

              ' Label: Row 2, Column 5
              rect = Skin.MeasureText(Gr, "Label: " & t.Label, Skinning.SkinTextPart.TooltipText, 600)
              rect.X = curX + ttCol1 + ttCol2 + ttCol3 + ttCol4 + 5
              rect.Y = curY
              Skin.DrawText(Gr, "Label: " & t.Label, rect, Skinning.SkinTextPart.TooltipText, StringAlignment.Near, StringAlignment.Near, True)

              ' Move to next row
              curY += rect.Height

              ' Peers Connected: Row 3, Column 1
              rect = Skin.MeasureText(Gr, "Peers Connected: " & t.PeersConnected, Skinning.SkinTextPart.TooltipText, 600)
              rect.X = curX
              rect.Y = curY
              Skin.DrawText(Gr, "Peers Connected: " & t.PeersConnected, rect, Skinning.SkinTextPart.TooltipText, StringAlignment.Near, StringAlignment.Near, True)

              ' Peers In Swarm: Row 3, Column 2
              rect = Skin.MeasureText(Gr, "Peers In Swarm: " & t.PeersInSwarm, Skinning.SkinTextPart.TooltipText, 600)
              rect.X = curX + ttCol1 + 5
              rect.Y = curY
              Skin.DrawText(Gr, "Peers In Swarm: " & t.PeersInSwarm, rect, Skinning.SkinTextPart.TooltipText, StringAlignment.Near, StringAlignment.Near, True)

              ' Seeds Connected: Row 3, Column 3
              rect = Skin.MeasureText(Gr, "Seeds Connected: " & t.SeedsConnected, Skinning.SkinTextPart.TooltipText, 600)
              rect.X = curX + ttCol1 + ttCol2 + 5
              rect.Y = curY
              Skin.DrawText(Gr, "Seeds Connected: " & t.SeedsConnected, rect, Skinning.SkinTextPart.TooltipText, StringAlignment.Near, StringAlignment.Near, True)

              ' Seeds In Swarm: Row 3, Column 4
              rect = Skin.MeasureText(Gr, "Seeds In Swarm: " & t.SeedsInSwarm, Skinning.SkinTextPart.TooltipText, 600)
              rect.X = curX + ttCol1 + ttCol2 + ttCol3 + 5
              rect.Y = curY
              Skin.DrawText(Gr, "Seeds In Swarm: " & t.SeedsInSwarm, rect, Skinning.SkinTextPart.TooltipText, StringAlignment.Near, StringAlignment.Near, True)

              ' Availability: Row 3, Column 5
              rect = Skin.MeasureText(Gr, "Availability: " & t.Availability, Skinning.SkinTextPart.TooltipText, 600)
              rect.X = curX + ttCol1 + ttCol2 + ttCol3 + ttCol4 + 5
              rect.Y = curY
              Skin.DrawText(Gr, "Availability: " & t.Availability, rect, Skinning.SkinTextPart.TooltipText, StringAlignment.Near, StringAlignment.Near, True)

              ' Move to next row
              curY += rect.Height

              ' Remaining: Row 4, Column 1
              rect = Skin.MeasureText(Gr, "Remaining: " & t.Remaining, Skinning.SkinTextPart.TooltipText, 600)
              rect.X = curX
              rect.Y = curY
              Skin.DrawText(Gr, "Remaining: " & t.Remaining, rect, Skinning.SkinTextPart.TooltipText, StringAlignment.Near, StringAlignment.Near, True)

              ' Finish this row
              curY += rect.Height

              If tCount < Torrents.Count Then
                rect.X = curX
                rect.Y = curY
                rect.Width = maxWidth
                rect.Height = Skin.TooltipSeparatorHeight
                Skin.DrawTooltipSeparator(Gr, rect)
                curY += Skin.TooltipSeparatorHeight
              End If
            Next

          Else

            rect = Skin.MeasureText(Gr, uTorrent_NoJobsText, Skinning.SkinTextPart.TooltipText, 600)
            rect.X = curX
            rect.Y = curY
            Skin.DrawText(Gr, uTorrent_NoJobsText, rect, Skinning.SkinTextPart.TooltipText, StringAlignment.Near, StringAlignment.Near, True)

          End If

        End If

        GR.Dispose()
        m_ModuleTooltipBitmap = bmpTemp.Clone
        bmpTemp.Dispose()

    End Select
  End Sub

#End Region

#Region "Mouse/Keyboard/Menu Processing"

  ' Check to see if the mouse is on any of your UI rectangles.
  ' Set bWindowIsDirty to true if you need a redraw.
  Public Overrides Sub ProcessMouseMove(ByVal e As System.Windows.Forms.MouseEventArgs, ByRef bWindowIsDirty As Boolean)
    ' Gmail Button
    If m_uTorrentButtonBounds.Contains(e.Location) Then
      If e.Button = Windows.Forms.MouseButtons.None Then
        If m_uTorrentButtonState <> 1 Then
          m_uTorrentButtonState = 1
          DrawModule()
          bWindowIsDirty = True
        End If
        Tooltip.SetTooltipOwner(InfoBarModuleGUID, "uTorrentWatcher")
      ElseIf e.Button = Windows.Forms.MouseButtons.Left Then
        If m_uTorrentButtonState <> 2 Then
          m_uTorrentButtonState = 2
          DrawModule()
          bWindowIsDirty = True
        End If
      End If
    Else
      If m_uTorrentButtonState <> 0 Then
        m_uTorrentButtonState = 0
        DrawModule()
        bWindowIsDirty = True
      End If
    End If
  End Sub

  ' Check to see if the mouse was depressed on any of your UI rectangles.
  ' Set bWindowIsDirty to true if you need a redraw.
  Public Overrides Sub ProcessMouseUp(ByVal e As MouseEventArgs, ByRef bWindowIsDirty As Boolean)
    ' Gmail Button
    If m_uTorrentButtonBounds.Contains(e.X, e.Y) = True Then
      If m_uTorrentButtonState <> 1 Then
        m_uTorrentButtonState = 1
        bWindowIsDirty = True
        DrawModule()
        uTorrent_Open()
      End If
    Else
      If m_uTorrentButtonState <> 0 Then
        m_uTorrentButtonState = 0
        DrawModule()
        bWindowIsDirty = True
      End If
    End If
  End Sub

  ' Check to see if the mouse was pressed down on any of your UI rectangles.
  ' Set bWindowIsDirty to true if you need a redraw.
  Public Overrides Sub ProcessMouseDown(ByVal e As MouseEventArgs, ByRef bWindowIsDirty As Boolean)
    ' Gmail Button
    If m_uTorrentButtonBounds.Contains(e.X, e.Y) = True Then
      If m_uTorrentButtonState <> 2 Then
        m_uTorrentButtonState = 2
        DrawModule()
        bWindowIsDirty = True
      End If
    Else
      If m_uTorrentButtonState <> 0 Then
        m_uTorrentButtonState = 0
        DrawModule()
        bWindowIsDirty = True
      End If
    End If
  End Sub

  ' Check to see if the mouse left any of your UI rectangles.
  ' Set bWindowIsDirty to true if you need a redraw.
  Public Overrides Sub ProcessMouseLeave(ByRef bWindowIsDirty As Boolean)
    ' Gmail Button
    If m_uTorrentButtonState <> 0 Then
      m_uTorrentButtonState = 0
      DrawModule()
      bWindowIsDirty = True
    End If
  End Sub

  Public Overrides Sub ProcessDragDrop(ByVal e As System.Windows.Forms.DragEventArgs, ByRef bWindowIsDirty As Boolean)
    '
  End Sub

  Public Overrides Sub ProcessDragEnter(ByVal e As System.Windows.Forms.DragEventArgs, ByRef bWindowIsDirty As Boolean)
    '
  End Sub

  Public Overrides Sub ProcessDragLeave(ByVal e As System.EventArgs, ByRef bWindowIsDirty As Boolean)
    '
  End Sub

  Public Overrides Sub ProcessDragOver(ByVal e As System.Windows.Forms.DragEventArgs, ByRef bWindowIsDirty As Boolean)
    '
  End Sub

  ' InfoBar will use this to determine if the mouse was in your module's bounds.
  Public Overrides Property ModuleBounds() As System.Drawing.Rectangle
    Set(ByVal value As System.Drawing.Rectangle)
      m_uTorrentButtonBounds = value
    End Set
    Get
      Return m_uTorrentButtonBounds
    End Get
  End Property

  ' InfoBar calls this when your module is right clicked. The popup menu will be shown.
  ' You can create menu items that appear at the top of the popup menu.
  Public Overrides Sub AddMainPopupMenuItems()
    ' A recommended standard feature is to allow users to access your module's settings
    ' dialog from the popup menu.

    Dim refreshIcon As Image, uTorrentIcon As Image
    If IconTheme.IsDefaultTheme = True Then
      refreshIcon = My.Resources.refresh
      uTorrentIcon = My.Resources.icon
    Else
      refreshIcon = IconTheme.GetThemeIcon(InfoBarModuleGUID, "Refresh")
      If refreshIcon Is Nothing Then refreshIcon = My.Resources.refresh

      uTorrentIcon = IconTheme.GetThemeIcon(InfoBarModuleGUID, "uTorrent")
      If uTorrentIcon Is Nothing Then uTorrentIcon = My.Resources.icon
    End If

    MainMenu.AddMenuItem(InfoBarModuleGUID & "::" & "OPEN", "Open uTorrent...", uTorrentIcon, True, True)
    MainMenu.AddSeparator()
    MainMenu.AddMenuItem(InfoBarModuleGUID & "::" & "REFRESH", "Refresh Now", refreshIcon, Not m_UpdateInProgress)
    MainMenu.AddMenuItem(InfoBarModuleGUID & "::" & "SETTINGS", "uTorrent Watcher Settings...", uTorrentIcon)

    ' Always add a separator when you're done.
    MainMenu.AddSeparator()
  End Sub

  ' InfoBar calls this when one of your module's menu items are selected.
  Public Overrides Sub ProcessMenuItemClick(ByVal Key As String)
    Select Case Key
      Case InfoBarModuleGUID & "::" & "OPEN"
        uTorrent_Open()
      Case InfoBarModuleGUID & "::" & "REFRESH"
        m_IsManualRefresh = True
        uTorrent_Refresh()
      Case InfoBarModuleGUID & "::" & "SETTINGS"
        Settings.ShowSettingsDialog(InfoBarModuleGUID)
    End Select
  End Sub

#End Region

#Region "Settings Routines"

  ' InfoBar will call this when it needs to know if the module can show a settings dialog.
  Overrides ReadOnly Property HasSettingsDialog() As Boolean
    Get
      HasSettingsDialog = True
    End Get
  End Property

  Public Overrides ReadOnly Property SettingsDialog() As System.Windows.Forms.UserControl
    Get
      Return m_SettingsDialog
    End Get
  End Property

  Public Overrides Sub ApplySettings()
    Setting_HostName = m_SettingsDialog.txtHostname.Text
    Setting_Port = CInt(m_SettingsDialog.txtPort.Text)
    Setting_Username = m_SettingsDialog.txtUsername.Text
    Setting_Password = m_SettingsDialog.txtPassword.Text
    Setting_RefreshInterval = m_SettingsDialog.nudRefreshInterval.Value
    Setting_Path = m_SettingsDialog.txtLocation.Text

    CacheID = Nothing
    m_IsManualRefresh = True
    m_UpdateInProgress = True
    uTorrent_Refresh()
  End Sub

  ' InfoBar will call this when the application loads. Use this to set your internal settings.
  Public Overrides Sub LoadSettings(ByRef Doc As XmlDocument)
    ' Always set defaults first.
    Setting_HostName = "localhost"
    Setting_Port = 8080
    Setting_Username = Nothing
    Setting_Password = Nothing
    Setting_RefreshInterval = 5
    Setting_Path = Nothing

    ' Now use the InfoBar Settings Service to load your settings.
    If Doc IsNot Nothing Then
      Setting_HostName = Settings.GetSetting(Doc, "hostname", "localhost")
      Setting_Port = CInt(Settings.GetSetting(Doc, "port", 8080))
      Setting_Username = Settings.GetSetting(Doc, "username", Nothing)

      Setting_Password = Settings.GetSetting(Doc, "password", Nothing)
      If Setting_Password IsNot Nothing Then
        Dim pe As New Utilities.PasswordEncryption
        Setting_Password = pe.DecryptPassword(Setting_Password)
      End If

      Setting_RefreshInterval = Settings.GetSetting(Doc, "refreshinterval", 5)
      If Setting_RefreshInterval < 1 Then Setting_RefreshInterval = 1
      If Setting_RefreshInterval > 1440 Then Setting_RefreshInterval = 1440

      Setting_Path = Settings.GetSetting(Doc, "location", Nothing)
    End If

    ResetSettings()
  End Sub

  Public Overrides Sub ResetSettings()
    m_SettingsDialog.txtHostname.Text = Setting_HostName
    m_SettingsDialog.txtPort.Text = Setting_Port
    m_SettingsDialog.txtUsername.Text = Setting_Username
    m_SettingsDialog.txtPassword.Text = Setting_Password
    m_SettingsDialog.nudRefreshInterval.Value = Setting_RefreshInterval
    m_SettingsDialog.txtLocation.Text = Setting_Path
  End Sub

  ' InfoBar will call this when the application exits. Use this to save your internal settings.
  Public Overrides Sub SaveSettings(ByRef Doc As XmlDocument)
    Dim Node As XmlNode

    ' Hostname
    Node = Doc.CreateNode(XmlNodeType.Element, "Node", "hostname", "")
    Node.InnerText = Setting_HostName
    Doc.DocumentElement.AppendChild(Node)

    ' Port
    Node = Doc.CreateNode(XmlNodeType.Element, "Node", "port", "")
    Node.InnerText = Setting_Port
    Doc.DocumentElement.AppendChild(Node)

    ' Username
    Node = Doc.CreateNode(XmlNodeType.Element, "Node", "username", "")
    Node.InnerText = Setting_Username
    Doc.DocumentElement.AppendChild(Node)

    ' Password
    Dim pe As New Utilities.PasswordEncryption
    Node = Doc.CreateNode(XmlNodeType.Element, "Node", "password", "")
    Node.InnerText = pe.EncryptPassword(Setting_Password)
    Doc.DocumentElement.AppendChild(Node)

    ' Refresh Interval
    Node = Doc.CreateNode(XmlNodeType.Element, "Node", "refreshinterval", "")
    Node.InnerText = Setting_RefreshInterval
    Doc.DocumentElement.AppendChild(Node)

    ' uTorrent Application Path
    Node = Doc.CreateNode(XmlNodeType.Element, "Node", "location", "")
    Node.InnerText = Setting_Path
    Doc.DocumentElement.AppendChild(Node)
  End Sub

#End Region

#Region "uTorrent Watcher Routines"

  Private Sub uTorrent_Refresh()
    Dim curTime As DateTime = DateTime.Now
    Dim timeDiff As Integer = Setting_RefreshInterval - curTime.Subtract(m_LastCheckTime).Seconds
    If (timeDiff <= 0) Or m_IsManualRefresh = True Then
      m_UpdateInProgress = True

      If m_IsManualRefresh = True Then
        m_IsManualRefresh = False
        DrawModule()
        Skin.UpdateWindow()
      End If

      If webRequest.IsBusy = True Then Exit Sub

      m_LastCheckTime = curTime

      If Setting_HostName.Length <= 1 Or Setting_Port <= 1 Then Exit Sub
      Dim urlWebUI As String = "http://" & Setting_HostName & ":" & Setting_Port & "/gui/?list=1"
      If CacheID IsNot Nothing Then urlWebUI &= "&cid=" & CacheID
      Dim uriWebUI As New Uri(urlWebUI)
      Dim wrCache As New CredentialCache()
      wrCache.Add(uriWebUI, "Basic", New NetworkCredential(Setting_Username, Setting_Password))
      webRequest.Credentials = wrCache
      webRequest.DownloadStringAsync(uriWebUI)
    End If
  End Sub

  Private Sub webRequest_DownloadStringCompleted(ByVal sender As Object, ByVal e As System.Net.DownloadStringCompletedEventArgs) Handles WebRequest.DownloadStringCompleted
    If e.Error Is Nothing Then
      uTorrent_ParseInfo(e.Result)
    Else
      uTorrent_ParseError(e.Error)
    End If
  End Sub

  Private Sub uTorrent_ParseInfo(ByVal sResult As String)
    Try
      m_UpdateInProgress = False
      m_ErrorChecking = False
      m_Text = vbNullString

      Dim uData = Jayrock.Json.Conversion.JsonConvert.Import(sResult)

      Dim torrentCount As Integer = 0, i As Integer = 0
      If CacheID IsNot Nothing AndAlso CacheID > 0 Then

        ' Update Existing Torrents
        torrentCount = uData("torrentp").Length
        If torrentCount > 0 Then
          For i = 0 To torrentCount - 1
            Dim sHash As String = uData("torrentp")(i)(0).ToString
            If Torrents.Contains(sHash) Then
              UpdateTorrentData(uData("torrentp")(i))
            Else
              AddTorrentData(uData("torrentp")(i))
            End If
          Next
        End If

        ' Remove Dead Torrents
        torrentCount = uData("torrentm").Length
        If torrentCount > 0 Then
          For i = 0 To torrentCount - 1
            Dim sHash As String = uData("torrentm")(i).ToString
            If Torrents.Contains(sHash) Then Torrents.Remove(sHash)
          Next
        End If

      Else
        Torrents.Clear()

        ' Create our list of torrents
        torrentCount = uData("torrents").Length
        If torrentCount > 0 Then
          For i = 0 To torrentCount - 1
            AddTorrentData(uData("torrents")(i))
          Next
        End If

      End If

      ' Get the current cache ID
      CacheID = uData("torrentc").ToString

      ' Update the torrent watch count
      m_Text = Torrents.Count

      ' Sort Data
      Utilities.SortCollection(Torrents, "TorrentQueueOrder", False, "Hash")
      DrawModule()
      DrawTooltip("uTorrentWatcher")
      Skin.UpdateWindow()

      If Tooltip.GetTooltipOwnerGUID = InfoBarModuleGUID AndAlso Tooltip.GetTooltipOwnerObjectID = "uTorrentWatcher" Then
        Tooltip.UpdateTooltip()
      End If

    Catch ex As Exception
      uTorrent_ParseError(ex)
    End Try
  End Sub

  Private Sub uTorrent_ParseError(ByVal ex As Exception)
    If TypeOf ex Is WebException Then
      Dim web_ex As WebException = ex
      Select Case web_ex.Status
        Case WebExceptionStatus.ConnectFailure
          m_ErrorMessage = "uTorrent is currently not running or WebUI is not enabled."
          m_ErrorChecking = False
          m_Text = ""

        Case Else
          ' We'll just set the button tooltip to ex.Message
          ' and set the button text to "Error"
          ' This should be fine for now until we know almost
          ' every error message possible, and can then provide
          ' friendly errors to users.
          Select Case True

            Case InStr(ex.Message, "(401) Unauthorized")
              m_ErrorMessage = "Your username or password is invalid."
              m_Text = "Error"

            Case Else
              m_ErrorMessage = ex.ToString
              m_Text = "Error"
          End Select

      End Select
    Else
      m_ErrorMessage = ex.ToString
      m_Text = "Error"
    End If

    m_UpdateInProgress = False
    m_ErrorChecking = True
    DrawModule()
    DrawTooltip("uTorrentWatcher")
    Skin.UpdateWindow()

    If Tooltip.GetTooltipOwnerGUID = InfoBarModuleGUID AndAlso Tooltip.GetTooltipOwnerObjectID = "uTorrentWatcher" Then
      Tooltip.UpdateTooltip()
    End If
  End Sub

  Private Sub uTorrent_Open()
    If Setting_Path IsNot Nothing Then
      ShellExecute(IntPtr.Zero, "OPEN", Setting_Path, "/BRINGTOFRONT", vbNullString, 0)
    Else
      MsgBox("You must set the path to uTorrent in the settings dialog.", MsgBoxStyle.Information Or MsgBoxStyle.OkOnly, "uTorrent Watcher")
    End If
  End Sub

  Private Sub AddTorrentData(ByVal oData As Object)
    Dim torrent As New Torrent
    torrent.Hash = oData(0).ToString
    torrent.Status = oData(1).ToString
    torrent.Name = oData(2).ToString
    torrent.Size = oData(3).ToString
    torrent.PercentProgress = oData(4).ToString
    torrent.Downloaded = oData(5).ToString
    torrent.Uploaded = oData(6).ToString
    torrent.Ratio = oData(7).ToString
    torrent.UploadSpeed = oData(8).ToString
    torrent.DownloadSpeed = oData(9).ToString
    torrent.ETA = oData(10).ToString
    torrent.Label = oData(11).ToString
    torrent.PeersConnected = oData(12).ToString
    torrent.PeersInSwarm = oData(13).ToString
    torrent.SeedsConnected = oData(14).ToString
    torrent.SeedsInSwarm = oData(15).ToString
    torrent.Availability = oData(16).ToString
    torrent.TorrentQueueOrder = oData(17).ToString
    torrent.Remaining = oData(18).ToString

    ' Now we have to format this stuff
    torrent.Status = FormatTorrentStatus(torrent.Status, torrent.PercentProgress)
    torrent.Size = Utilities.FormatFileSize(torrent.Size)
    torrent.PercentProgress = FormatNumber(torrent.PercentProgress / 10, 1, TriState.False, TriState.False, TriState.False) & "%"
    torrent.Downloaded = Utilities.FormatFileSize(torrent.Downloaded)
    torrent.Uploaded = Utilities.FormatFileSize(torrent.Uploaded)
    torrent.Ratio = FormatNumber(torrent.Ratio / 1000, 3, TriState.True, TriState.False, TriState.False)
    torrent.UploadSpeed = Utilities.FormatTransferRate(torrent.UploadSpeed)
    torrent.DownloadSpeed = Utilities.FormatTransferRate(torrent.DownloadSpeed)
    If torrent.ETA > -1 Then
      torrent.ETA = Utilities.FormatTime(torrent.ETA)
    Else
      torrent.ETA = ""
    End If
    torrent.Availability = FormatNumber(torrent.Availability / 65535, 3, TriState.True, TriState.False, TriState.False)
    torrent.Remaining = Utilities.FormatFileSize(torrent.Remaining)

    ' Add to our list of torrents
    Torrents.Add(torrent, torrent.Hash)
  End Sub

  Private Sub UpdateTorrentData(ByVal oData As Object)
    Dim sHash As String = oData(0).ToString
    Torrents(sHash).Status = oData(1).ToString
    Torrents(sHash).Name = oData(2).ToString
    Torrents(sHash).Size = oData(3).ToString
    Torrents(sHash).PercentProgress = oData(4).ToString
    Torrents(sHash).Downloaded = oData(5).ToString
    Torrents(sHash).Uploaded = oData(6).ToString
    Torrents(sHash).Ratio = oData(7).ToString
    Torrents(sHash).UploadSpeed = oData(8).ToString
    Torrents(sHash).DownloadSpeed = oData(9).ToString
    Torrents(sHash).ETA = oData(10).ToString
    Torrents(sHash).Label = oData(11).ToString
    Torrents(sHash).PeersConnected = oData(12).ToString
    Torrents(sHash).PeersInSwarm = oData(13).ToString
    Torrents(sHash).SeedsConnected = oData(14).ToString
    Torrents(sHash).SeedsInSwarm = oData(15).ToString
    Torrents(sHash).Availability = oData(16).ToString
    Torrents(sHash).TorrentQueueOrder = oData(17).ToString
    Torrents(sHash).Remaining = oData(18).ToString

    ' Now we have to format this stuff
    Torrents(sHash).Status = FormatTorrentStatus(Torrents(sHash).Status, Torrents(sHash).PercentProgress)
    Torrents(sHash).Size = Utilities.FormatFileSize(Torrents(sHash).Size)
    Torrents(sHash).PercentProgress = FormatNumber(Torrents(sHash).PercentProgress / 10, 1, TriState.False, TriState.False, TriState.False) & "%"
    Torrents(sHash).Downloaded = Utilities.FormatFileSize(Torrents(sHash).Downloaded)
    Torrents(sHash).Uploaded = Utilities.FormatFileSize(Torrents(sHash).Uploaded)
    Torrents(sHash).Ratio = FormatNumber(Torrents(sHash).Ratio / 1000, 3, TriState.True, TriState.False, TriState.False)
    Torrents(sHash).UploadSpeed = Utilities.FormatTransferRate(Torrents(sHash).UploadSpeed)
    Torrents(sHash).DownloadSpeed = Utilities.FormatTransferRate(Torrents(sHash).DownloadSpeed)
    Torrents(sHash).ETA = Utilities.FormatTime(Torrents(sHash).ETA)
    Torrents(sHash).Availability = FormatNumber(Torrents(sHash).Availability / 65535, 3, TriState.True, TriState.False, TriState.False)
    Torrents(sHash).Remaining = Utilities.FormatFileSize(Torrents(sHash).Remaining)
  End Sub

  Private Function FormatTorrentStatus(ByVal sStatus As String, ByVal sPct As String) As String
    Dim iStatus As Integer = CInt(sStatus)
    Dim iPct As Integer = CInt(sPct)
    If iPct = 1000 Then Return "Finished"
    If iStatus And 128 = 128 Then
      Return "Loaded"
    ElseIf iStatus And 64 = 64 Then
      Return "Queued"
    ElseIf iStatus And 32 = 32 Then
      Return "Paused"
    ElseIf iStatus And 16 = 16 Then
      Return "Error"
    ElseIf iStatus And 8 = 8 Then
      Return "Checked"
    ElseIf iStatus And 4 = 4 Then
      Return "Starting"
    ElseIf iStatus And 2 = 2 Then
      Return "Checking"
    ElseIf iStatus And 1 = 1 Then
      Return "Started"
    Else
      Return "Unknown"
    End If
  End Function

#End Region

End Class
