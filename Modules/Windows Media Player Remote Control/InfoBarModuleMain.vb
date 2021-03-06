Public Class InfoBarModuleMain
  ' We inherit the base class. We DO NOT return any of the base classes values.
  Inherits InfoBarModule

  ' Unique ID. If same as other module, the first module will be loaded, the next will be ignored.
  Private Const InfoBarModuleGUID As String = "{749941B7-BE53-48fd-A2B1-FECFFB12B008}"

#Region "Windows API"
  Private Structure POINTAPI
    Public x As Integer
    Public y As Integer
  End Structure

  Private Structure RECT
    Public Left As Integer
    Public Top As Integer
    Public Right As Integer
    Public Bottom As Integer
  End Structure

  Private Structure WINDOWPLACEMENT
    Public Length As Integer
    Public flags As Integer
    Public showCmd As Integer
    Public ptMinPosition As POINTAPI
    Public ptMaxPosition As POINTAPI
    Public rcNormalPosition As RECT
  End Structure

  Private Declare Function ShellExecute Lib "shell32.dll" Alias "ShellExecuteA" (ByVal hWnd As IntPtr, ByVal lpOperation As String, ByVal lpFile As String, ByVal lpParameters As String, ByVal lpDirectory As String, ByVal nShowCmd As Integer) As IntPtr
  Private Declare Function FindWindow Lib "user32.dll" Alias "FindWindowA" (ByVal lpClassName As String, ByVal lpWindowName As String) As IntPtr
  Private Declare Function ShowWindow Lib "user32.dll" (ByVal hWnd As IntPtr, ByVal nCmdShow As Int32) As Boolean
  Private Declare Sub SetForegroundWindow Lib "user32.dll" (ByVal hWnd As IntPtr)
  Private Declare Function GetWindowPlacement Lib "user32" (ByVal hWnd As IntPtr, ByRef lpwndpl As WINDOWPLACEMENT) As Integer
  Private Declare Function SetWindowPlacement Lib "user32" (ByVal hWnd As IntPtr, ByRef lpwndpl As WINDOWPLACEMENT) As Integer
#End Region

#Region "InfoBar Service Objects"
  Private Skin As New InfoBar.Services.Skinning
  Private Tooltip As New InfoBar.Services.Tooltips
  Private IconTheme As New InfoBar.Services.IconTheme
  Private MainMenu As New InfoBar.Services.PopupMenus.MainMenu
  Private Settings As New InfoBar.Services.Settings
  Private Utilities As New InfoBar.Services.Utilities
#End Region

#Region "Private Variables"
  Dim m_ModuleToolbarBitmap As Bitmap
  Dim m_ModuleTooltipBitmap As Bitmap
  Dim m_SettingsDialog As New Settings
  Dim m_Bounds As Rectangle
  Dim m_Enabled As Boolean
  Dim m_WMP As WMPApp
  Dim m_IsRunning As Boolean
  Dim m_IsPlaying As Boolean
  Dim m_OpenButtonBounds As Rectangle
  Dim m_OpenButtonState As Integer
  Dim m_PrevButtonBounds As Rectangle
  Dim m_PrevButtonState As Integer
  Dim m_PlayButtonBounds As Rectangle
  Dim m_PlayButtonState As Integer
  Dim m_PauseButtonBounds As Rectangle
  Dim m_PauseButtonState As Integer
  Dim m_StopButtonBounds As Rectangle
  Dim m_StopButtonState As Integer
  Dim m_NextButtonBounds As Rectangle
  Dim m_NextButtonState As Integer
  Dim m_TrackRatingButtonBounds As Rectangle
  Dim m_TrackRatingButtonState As Integer
  Dim m_TimeDisplayBounds As Rectangle
  Dim m_TimeDisplayText As String
  Dim m_TitleDisplayBounds As Rectangle
  Dim m_TitleDisplayText As String
  Dim m_TooltipText As String
  Dim m_TooltipTextRect As Rectangle
  Dim m_AlbumArtPath As String
  Dim m_AlbumArtImage As Image
  Dim m_TrackRating As String
  Dim m_TrackRatingImage As Image
  Dim m_AlbumArtWidth As Integer
  Dim m_AlbumArtHeight As Integer
  Dim m_TextStartX As Integer
  Dim WithEvents ratingMenu As PopupMenus.PopupMenu
#End Region

#Region "Settings Variables"
  Dim m_PlaybackButtonDisplay As Integer
  Dim m_ShowTrackRatingButton As Boolean
  Dim m_ShowTrackRating As Boolean
  Dim m_SongTitleTrackRatingOpacity As Integer
  Dim m_ShowNowPlayingTooltip As Boolean
  Dim m_ShowSongTimeInfo As Boolean
  Dim m_SongTimeInfoMode As Integer
  Dim m_ShowSongDuration As Boolean
  Dim m_SongTimeWidth As Integer
  Dim m_ShowSongTagInfo As Boolean
  Dim m_SongTagWidth As Integer
  Dim m_SongTagFormattingString As String
  Dim m_TooltipShowAlbumArt As Boolean
  Dim m_AlbumArtFormattingString As String
  Dim m_TooltipAlbumArtWidth As Integer
  Dim m_TooltipShowTrackRating As Boolean
  Dim m_TooltipTextFormatString As String
  Dim m_TooltipTextMaxWidth As Integer
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
      Return "This module allows you to control playback of music in Windows Media Player. It also shows file metadata and album art in the tooltip."
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
    ' Setup menu        
    ratingMenu = New PopupMenus.PopupMenu
    ratingMenu.ShowImageMargin = False
    If IconTheme.IsDefaultTheme = True Then
      ratingMenu.AddMenuItem("5Stars", vbNullString, My.Resources.rating5, , , True)
      ratingMenu.AddMenuItem("4Stars", vbNullString, My.Resources.rating4, , , True)
      ratingMenu.AddMenuItem("3Stars", vbNullString, My.Resources.rating3, , , True)
      ratingMenu.AddMenuItem("2Stars", vbNullString, My.Resources.rating2, , , True)
      ratingMenu.AddMenuItem("1Star", vbNullString, My.Resources.rating1, , , True)
    Else
      ratingMenu.AddMenuItem("5Stars", vbNullString, IconTheme.GetThemeIcon(InfoBarModuleGUID, "Rating5"), , , True)
      ratingMenu.AddMenuItem("4Stars", vbNullString, IconTheme.GetThemeIcon(InfoBarModuleGUID, "Rating4"), , , True)
      ratingMenu.AddMenuItem("3Stars", vbNullString, IconTheme.GetThemeIcon(InfoBarModuleGUID, "Rating3"), , , True)
      ratingMenu.AddMenuItem("2Stars", vbNullString, IconTheme.GetThemeIcon(InfoBarModuleGUID, "Rating2"), , , True)
      ratingMenu.AddMenuItem("1Star", vbNullString, IconTheme.GetThemeIcon(InfoBarModuleGUID, "Rating1"), , , True)
    End If
    ratingMenu.AddSeparator()
    ratingMenu.AddMenuItem("NoRating", "No Rating")

    Try
      m_WMP = New WMPApp 'CreateObject("WMPuICE.WMPApp.1") ' 
    Catch
      Debug.Print("Couldn't create WMPuICE.WMPApp.1 object, trying to register wmpuice.dll.")
      Dim sRegSvrPath As String = Utilities.GetSpecialFolder(Services.Utilities.ShellCSIDL.SYSTEM) & "\regsvr32.exe"
      Dim sArgs As String = " /s " & ChrW(34) & Application.StartupPath & "\Modules\Windows Media Player Remote Control\wmpuice.dll" & ChrW(34)
      Dim proc As Process = Process.Start(sRegSvrPath, sArgs)
      proc.WaitForExit()
      If proc.ExitCode <> 0 Then
        MsgBox("wmpuice.dll could not be registered automatically. Please register this DLL file using regsvr32, with Administrator privledges.", MsgBoxStyle.Critical Or MsgBoxStyle.OkOnly, "InfoBar")
      Else
        m_WMP = New WMPApp 'CreateObject("WMPuICE.WMPApp.1")
      End If
    End Try

    DrawModule()
  End Sub

  ' InfoBar will call this when your module is disabled.
  Public Overrides Sub FinalizeModule()
    Try
      m_WMP = Nothing
    Catch ex As Exception
    End Try
  End Sub

  ' InfoBar will call this every 1 second. Make sure to do work only when needed.
  ' TIP: If m_Enabled is set to false, don't do any work to save CPU time unless really needed.
  Public Overrides Sub TimerTick(ByRef bIsModuleDirty As Boolean)
    If m_Enabled = False Then Exit Sub

    If m_WMP Is Nothing Then
      m_IsRunning = False
      DrawModule()
      bIsModuleDirty = False
      Exit Sub
    End If

    Try
      m_IsRunning = m_WMP.Running
    Catch ex As Exception
      m_IsRunning = False
      DrawModule()
      bIsModuleDirty = False
      Exit Sub
    End Try

    m_IsPlaying = IsMusicPlaying()

    Dim bRefreshModule As Boolean = False
    Dim bRefreshTooltip As Boolean = False

    If m_IsRunning AndAlso m_IsPlaying Then

      If GetSongTimeInfo() = True Then bRefreshModule = True
      If GetSongTitle() = True Then bRefreshModule = True
      If GetAlbumArt() = True Then bRefreshTooltip = True
      If GetRating() = True Then bRefreshTooltip = True
      If GetTooltipText() = True Then bRefreshTooltip = True

    Else

      If m_TimeDisplayText <> vbNullString Then
        m_TimeDisplayText = vbNullString
        bRefreshModule = True
      End If
      If m_TitleDisplayText <> vbNullString Then
        m_TitleDisplayText = vbNullString
        bRefreshModule = True
      End If
      m_AlbumArtPath = vbNullString
      m_AlbumArtImage = Nothing
      m_TrackRating = vbNullString
      m_TrackRatingImage = Nothing
      m_TooltipText = vbNullString

    End If

    If Tooltip.GetTooltipOwnerGUID = InfoBarModuleGUID AndAlso _
    Tooltip.GetTooltipOwnerObjectID = "NowPlaying" AndAlso _
    bRefreshTooltip = True Then
      Tooltip.UpdateTooltip()
    End If

    DrawModule()
    bIsModuleDirty = bRefreshModule
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

  Private Sub DrawModule()
    Dim width As Integer = 0, height As Integer = Skin.MaxModuleHeight
    Dim icoWMP As Image, icoPrev As Image, icoPlay As Image, icoPause As Image
    Dim icoStop As Image, icoNext As Image, icoRating As Image

    ' Get Icons
    If IconTheme.IsDefaultTheme Then
      icoWMP = My.Resources.icon
      icoPrev = My.Resources.prev
      icoPlay = My.Resources.play
      icoPause = My.Resources.pause
      icoStop = My.Resources._stop
      icoNext = My.Resources._next
      icoRating = My.Resources.rating
    Else
      icoWMP = IconTheme.GetThemeIcon(InfoBarModuleGUID, "Windows Media Player")
      If icoWMP Is Nothing Then icoWMP = My.Resources.icon

      icoPrev = IconTheme.GetThemeIcon(InfoBarModuleGUID, "Previous")
      If icoPrev Is Nothing Then icoPrev = My.Resources.prev

      icoPlay = IconTheme.GetThemeIcon(InfoBarModuleGUID, "Play")
      If icoPlay Is Nothing Then icoPlay = My.Resources.play

      icoPause = IconTheme.GetThemeIcon(InfoBarModuleGUID, "Pause")
      If icoPause Is Nothing Then icoPause = My.Resources.pause

      icoStop = IconTheme.GetThemeIcon(InfoBarModuleGUID, "Stop")
      If icoStop Is Nothing Then icoPause = My.Resources._stop

      icoNext = IconTheme.GetThemeIcon(InfoBarModuleGUID, "Next")
      If icoNext Is Nothing Then icoNext = My.Resources._next

      icoRating = IconTheme.GetThemeIcon(InfoBarModuleGUID, "Rating")
      If icoRating Is Nothing Then icoRating = My.Resources.rating
    End If

    ' Measure Module
    Dim grm As Graphics = Graphics.FromHwnd(IntPtr.Zero)

    ' Get skin height for vertical centering
    Dim btnIcon As Image = Nothing, btnText As String = vbNullString

    If m_PlaybackButtonDisplay = 0 Or m_PlaybackButtonDisplay = 2 Then btnIcon = icoWMP
    If m_PlaybackButtonDisplay = 1 Or m_PlaybackButtonDisplay = 2 Then btnText = "Open/Show Windows Media Player"
    Dim btnR As New Rectangle(0, 0, 1, height)
    Dim btnFR As Rectangle = Skin.MeasureButton(grm, btnR, btnIcon, btnText, m_OpenButtonState, False)
    width += btnFR.Width

    If m_IsRunning Then
      btnIcon = Nothing : btnText = vbNullString
      If m_PlaybackButtonDisplay = 0 Or m_PlaybackButtonDisplay = 2 Then btnIcon = icoPrev
      If m_PlaybackButtonDisplay = 1 Or m_PlaybackButtonDisplay = 2 Then btnText = "Previous"
      btnR = New Rectangle(0, 0, 1, height)
      btnFR = Skin.MeasureButton(grm, btnR, btnIcon, btnText, m_PrevButtonState, False)
      width += btnFR.Width

      btnIcon = Nothing : btnText = vbNullString
      If m_PlaybackButtonDisplay = 0 Or m_PlaybackButtonDisplay = 2 Then btnIcon = icoPlay
      If m_PlaybackButtonDisplay = 1 Or m_PlaybackButtonDisplay = 2 Then btnText = "Play"
      btnR = New Rectangle(0, 0, 1, height)
      btnFR = Skin.MeasureButton(grm, btnR, btnIcon, btnText, m_PlayButtonState, False)
      width += btnFR.Width

      btnIcon = Nothing : btnText = vbNullString
      If m_PlaybackButtonDisplay = 0 Or m_PlaybackButtonDisplay = 2 Then btnIcon = icoPause
      If m_PlaybackButtonDisplay = 1 Or m_PlaybackButtonDisplay = 2 Then btnText = "Pause"
      btnR = New Rectangle(0, 0, 1, height)
      btnFR = Skin.MeasureButton(grm, btnR, btnIcon, btnText, m_PauseButtonState, False)
      width += btnFR.Width

      btnIcon = Nothing : btnText = vbNullString
      If m_PlaybackButtonDisplay = 0 Or m_PlaybackButtonDisplay = 2 Then btnIcon = icoStop
      If m_PlaybackButtonDisplay = 1 Or m_PlaybackButtonDisplay = 2 Then btnText = "Stop"
      btnR = New Rectangle(0, 0, 1, height)
      btnFR = Skin.MeasureButton(grm, btnR, btnIcon, btnText, m_StopButtonState, False)
      width += btnFR.Width

      btnIcon = Nothing : btnText = vbNullString
      If m_PlaybackButtonDisplay = 0 Or m_PlaybackButtonDisplay = 2 Then btnIcon = icoNext
      If m_PlaybackButtonDisplay = 1 Or m_PlaybackButtonDisplay = 2 Then btnText = "Next"
      btnR = New Rectangle(0, 0, 1, height)
      btnFR = Skin.MeasureButton(grm, btnR, btnIcon, btnText, m_NextButtonState, False)
      width += btnFR.Width

      If m_ShowTrackRatingButton = True AndAlso m_IsPlaying = True Then
        btnIcon = Nothing : btnText = vbNullString
        If IconTheme.IsDefaultTheme = True Then btnIcon = icoRating
        If m_PlaybackButtonDisplay = 1 Or m_PlaybackButtonDisplay = 2 Then btnText = "Track Rating"
        btnR = New Rectangle(0, 0, 1, height)
        btnFR = Skin.MeasureButton(grm, btnR, btnIcon, btnText, m_TrackRatingButtonState, True)
        width += btnFR.Width
      End If

    End If

    If m_ShowSongTimeInfo AndAlso m_IsPlaying Then width += m_SongTimeWidth + 4
    If m_ShowSongTagInfo AndAlso m_IsPlaying Then width += m_SongTagWidth + 4

    ' Draw Module
    If width <= 1 Or height <= 1 Then Exit Sub
    Dim bmpTemp As New Bitmap(width, height, System.Drawing.Imaging.PixelFormat.Format32bppArgb)
    Dim GR As Graphics = Graphics.FromImage(bmpTemp)

    ' Get skin height for vertical centering
    Dim curX As Integer = 0, lHeight As Integer = bmpTemp.Height
    Dim textBoxMargins As Skinning.SkinMargins = Skin.ContentMargins(Skinning.SkinMarginPart.TextBox)

    ' Draw Open/Show Button
    If m_PlaybackButtonDisplay = 0 Or m_PlaybackButtonDisplay = 2 Then btnIcon = icoWMP
    If m_PlaybackButtonDisplay = 1 Or m_PlaybackButtonDisplay = 2 Then btnText = "Open/Show Windows Media Player"
    btnR = New Rectangle(curX, 0, 1, lHeight)
    btnFR = Skin.DrawButton(GR, btnR, btnIcon, btnText, m_OpenButtonState, False)
    m_OpenButtonBounds = btnFR
    curX = curX + btnFR.Width

    If m_IsRunning Then
      ' Draw Previous Button
      btnIcon = Nothing : btnText = vbNullString
      If m_PlaybackButtonDisplay = 0 Or m_PlaybackButtonDisplay = 2 Then btnIcon = icoPrev
      If m_PlaybackButtonDisplay = 1 Or m_PlaybackButtonDisplay = 2 Then btnText = "Previous"
      btnR = New Rectangle(curX, 0, 1, lHeight)
      btnFR = Skin.DrawButton(GR, btnR, btnIcon, btnText, m_PrevButtonState, False)
      m_PrevButtonBounds = btnFR
      curX = curX + btnFR.Width

      ' Draw Play Button
      btnIcon = Nothing : btnText = vbNullString
      If m_PlaybackButtonDisplay = 0 Or m_PlaybackButtonDisplay = 2 Then btnIcon = icoPlay
      If m_PlaybackButtonDisplay = 1 Or m_PlaybackButtonDisplay = 2 Then btnText = "Play"
      btnR = New Rectangle(curX, 0, 1, lHeight)
      btnFR = Skin.DrawButton(GR, btnR, btnIcon, btnText, m_PlayButtonState, False)
      m_PlayButtonBounds = btnFR
      curX = curX + btnFR.Width

      ' Draw Pause Button
      btnIcon = Nothing : btnText = vbNullString
      If m_PlaybackButtonDisplay = 0 Or m_PlaybackButtonDisplay = 2 Then btnIcon = icoPause
      If m_PlaybackButtonDisplay = 1 Or m_PlaybackButtonDisplay = 2 Then btnText = "Pause"
      btnR = New Rectangle(curX, 0, 1, lHeight)
      btnFR = Skin.DrawButton(GR, btnR, btnIcon, btnText, m_PauseButtonState, False)
      m_PauseButtonBounds = btnFR
      curX = curX + btnFR.Width

      ' Draw Stop Button
      btnIcon = Nothing : btnText = vbNullString
      If m_PlaybackButtonDisplay = 0 Or m_PlaybackButtonDisplay = 2 Then btnIcon = icoStop
      If m_PlaybackButtonDisplay = 1 Or m_PlaybackButtonDisplay = 2 Then btnText = "Stop"
      btnR = New Rectangle(curX, 0, 1, lHeight)
      btnFR = Skin.DrawButton(GR, btnR, btnIcon, btnText, m_StopButtonState, False)
      m_StopButtonBounds = btnFR
      curX = curX + btnFR.Width

      ' Draw Next Button
      btnIcon = Nothing : btnText = vbNullString
      If m_PlaybackButtonDisplay = 0 Or m_PlaybackButtonDisplay = 2 Then btnIcon = icoNext
      If m_PlaybackButtonDisplay = 1 Or m_PlaybackButtonDisplay = 2 Then btnText = "Next"
      btnR = New Rectangle(curX, 0, 1, lHeight)
      btnFR = Skin.DrawButton(GR, btnR, btnIcon, btnText, m_NextButtonState, False)
      m_NextButtonBounds = btnFR
      curX = curX + btnFR.Width

      ' Draw Track Rating Button
      If m_ShowTrackRatingButton = True AndAlso m_IsPlaying = True Then
        btnIcon = Nothing : btnText = vbNullString
        If IconTheme.IsDefaultTheme = True Then btnIcon = icoRating
        If m_PlaybackButtonDisplay = 1 Or m_PlaybackButtonDisplay = 2 Then btnText = "Track Rating"
        btnR = New Rectangle(curX, 0, 1, lHeight)
        btnFR = Skin.DrawButton(GR, btnR, btnIcon, btnText, m_TrackRatingButtonState, True)
        m_TrackRatingButtonBounds = btnFR
        curX = curX + btnFR.Width
      End If

    Else

      m_PrevButtonBounds = Nothing
      m_PlayButtonBounds = Nothing
      m_PauseButtonBounds = Nothing
      m_StopButtonBounds = Nothing
      m_NextButtonBounds = Nothing
      m_TrackRatingButtonBounds = Nothing

    End If

    ' Draw Time Textbox
    If m_ShowSongTimeInfo AndAlso m_IsPlaying Then
      curX = curX + 2

      Dim trs As Rectangle
      trs.X = curX
      trs.Y = 0
      trs.Width = m_SongTimeWidth
      trs.Height = lHeight
      Skin.DrawTextBox(GR, trs)
      m_TimeDisplayBounds = trs
      curX = curX + trs.Width + 2
      trs.X = trs.X + textBoxMargins.Left
      trs.Y = trs.Y + textBoxMargins.Top
      trs.Width = trs.Width - (textBoxMargins.Left + textBoxMargins.Right)
      trs.Height = trs.Height - (textBoxMargins.Top + textBoxMargins.Bottom)
      Skin.DrawText(GR, m_TimeDisplayText, trs, Skinning.SkinTextPart.TextBoxText, StringAlignment.Center, StringAlignment.Center)
    Else
      m_TimeDisplayBounds = Nothing
    End If

    ' Draw Title Textbox
    If m_ShowSongTagInfo AndAlso m_IsPlaying Then
      curX = curX + 2

      Dim trs As Rectangle
      trs.X = curX
      trs.Y = 0
      trs.Width = m_SongTagWidth
      trs.Height = lHeight
      Skin.DrawTextBox(GR, trs)

      m_TitleDisplayBounds = trs
      curX = curX + trs.Width + 2
      trs.X = trs.X + textBoxMargins.Left
      trs.Y = trs.Y + textBoxMargins.Top
      trs.Width = trs.Width - (textBoxMargins.Left + textBoxMargins.Right)
      trs.Height = trs.Height - (textBoxMargins.Top + textBoxMargins.Bottom)

      ' Draw Rating
      If m_ShowTrackRating AndAlso m_TrackRatingImage IsNot Nothing Then
        Dim imgAttrib As New Drawing.Imaging.ImageAttributes
        Dim CM As New Drawing.Imaging.ColorMatrix
        Dim sAlpha As Single = (m_SongTitleTrackRatingOpacity / 255)
        CM.Matrix33 = sAlpha
        imgAttrib.SetColorMatrix(CM, Drawing.Imaging.ColorMatrixFlag.Default, Drawing.Imaging.ColorAdjustType.Bitmap)

        Dim triX As Integer = trs.X + (trs.Width - m_TrackRatingImage.Width)
        Dim triY As Integer = trs.Y + ((trs.Height - m_TrackRatingImage.Height) / 2)
        Dim triRect As New Rectangle(triX, triY, m_TrackRatingImage.Width, m_TrackRatingImage.Height)
        GR.DrawImage(m_TrackRatingImage, triRect, 0, 0, m_TrackRatingImage.Width, m_TrackRatingImage.Height, GraphicsUnit.Pixel, imgAttrib)
      End If

      Skin.DrawText(GR, m_TitleDisplayText, trs, Skinning.SkinTextPart.TextBoxText, StringAlignment.Near, StringAlignment.Center)
    Else
      m_TitleDisplayBounds = Nothing
    End If

    GR.Dispose()
    m_ModuleToolbarBitmap = bmpTemp.Clone
    bmpTemp.Dispose()
  End Sub

  Public Overrides Function GetTooltipBitmap() As System.Drawing.Bitmap
    Try
      Return m_ModuleTooltipBitmap.Clone
    Catch ex As Exception
      Return Nothing
    End Try
  End Function

  Private Sub DrawTooltip(ByVal ObjectID As String)

    ' Measure Tooltip
    Dim GR As Graphics
    Dim tr As Rectangle
    GR = Graphics.FromHwnd(IntPtr.Zero)

    Select Case ObjectID

      Case "OpenButton"
        tr = Skin.MeasureText(GR, "Open/Show Windows Media Player", Skinning.SkinTextPart.TooltipText)

      Case "PrevButton"
        tr = Skin.MeasureText(GR, "Previous Track", Skinning.SkinTextPart.TooltipText)

      Case "PlayButton"
        tr = Skin.MeasureText(GR, "Play", Skinning.SkinTextPart.TooltipText)

      Case "PauseButton"
        tr = Skin.MeasureText(GR, "Pause/Unpause", Skinning.SkinTextPart.TooltipText)

      Case "StopButton"
        tr = Skin.MeasureText(GR, "Stop", Skinning.SkinTextPart.TooltipText)

      Case "NextButton"
        tr = Skin.MeasureText(GR, "Next Track", Skinning.SkinTextPart.TooltipText)

      Case "TrackRatingButton"
        tr = Skin.MeasureText(GR, "Track Rating", Skinning.SkinTextPart.TooltipText)

      Case "NowPlaying"
        Dim maxWidth As Integer = 0, maxHeight As Integer = 0
        Dim trText As Rectangle

        If m_TooltipShowAlbumArt AndAlso m_AlbumArtImage IsNot Nothing Then
          m_AlbumArtWidth = m_TooltipAlbumArtWidth
          m_AlbumArtHeight = (m_TooltipAlbumArtWidth / m_AlbumArtImage.Width) * m_AlbumArtImage.Height
          maxWidth = m_AlbumArtWidth
          m_TextStartX = m_AlbumArtWidth + 5
        Else
          m_AlbumArtWidth = 0
          m_AlbumArtHeight = 0
          maxWidth = 0
          m_TextStartX = 0
        End If

        If m_TooltipShowTrackRating AndAlso (m_TrackRatingImage IsNot Nothing) Then
          maxWidth = m_TextStartX + m_TrackRatingImage.Width
        End If

        trText = Skin.MeasureText(GR, m_TooltipText, Skinning.SkinTextPart.TooltipText, m_TooltipTextMaxWidth)
        m_TooltipTextRect = trText
        If m_TextStartX + trText.Width > maxWidth Then maxWidth = m_TextStartX + trText.Width
        maxHeight = trText.Height

        If m_TooltipShowTrackRating AndAlso (m_TrackRatingImage IsNot Nothing) Then
          maxHeight = maxHeight + 5 + m_TrackRatingImage.Height + 5
        End If

        If m_TooltipShowAlbumArt AndAlso m_AlbumArtImage IsNot Nothing Then
          If m_AlbumArtHeight > maxHeight Then maxHeight = m_AlbumArtHeight
        End If

        tr = New Rectangle(0, 0, maxWidth, maxHeight)

    End Select
    GR.Dispose()

    ' Draw Tooltip
    Dim bmpTemp As New Bitmap(tr.Width, tr.Height, System.Drawing.Imaging.PixelFormat.Format32bppArgb)
    GR = Graphics.FromImage(bmpTemp)

    Select Case ObjectID

      Case "OpenButton"
        Skin.DrawText(GR, "Open/Show Windows Media Player", tr, Skinning.SkinTextPart.TooltipText, StringAlignment.Near, StringAlignment.Near)

      Case "PrevButton"
        Skin.DrawText(GR, "Previous Track", tr, Skinning.SkinTextPart.TooltipText, StringAlignment.Near, StringAlignment.Near)

      Case "PlayButton"
        Skin.DrawText(GR, "Play", tr, Skinning.SkinTextPart.TooltipText, StringAlignment.Near, StringAlignment.Near)

      Case "PauseButton"
        Skin.DrawText(GR, "Pause/Unpause", tr, Skinning.SkinTextPart.TooltipText, StringAlignment.Near, StringAlignment.Near)

      Case "StopButton"
        Skin.DrawText(GR, "Stop", tr, Skinning.SkinTextPart.TooltipText, StringAlignment.Near, StringAlignment.Near)

      Case "NextButton"
        Skin.DrawText(GR, "Next Track", tr, Skinning.SkinTextPart.TooltipText, StringAlignment.Near, StringAlignment.Near)

      Case "TrackRatingButton"
        Skin.DrawText(GR, "Track Rating", tr, Skinning.SkinTextPart.TooltipText, StringAlignment.Near, StringAlignment.Near)

      Case "NowPlaying"
        Dim nextY As Integer

        If m_TooltipShowAlbumArt AndAlso m_AlbumArtImage IsNot Nothing Then
          m_AlbumArtWidth = m_TooltipAlbumArtWidth
          m_AlbumArtHeight = (m_TooltipAlbumArtWidth / m_AlbumArtImage.Width) * m_AlbumArtImage.Height
          GR.CompositingQuality = CompositingQuality.HighQuality
          GR.SmoothingMode = SmoothingMode.HighQuality
          GR.InterpolationMode = InterpolationMode.HighQualityBicubic
          GR.DrawImage(m_AlbumArtImage, 0, 0, m_AlbumArtWidth, m_AlbumArtHeight)
          GR.CompositingQuality = CompositingQuality.Default
          GR.SmoothingMode = SmoothingMode.HighSpeed
          GR.InterpolationMode = InterpolationMode.Default
          nextY = m_AlbumArtHeight + 10
        End If

        nextY = 0
        tr = m_TooltipTextRect
        tr.Offset(m_TextStartX, 0)
        nextY = nextY + tr.Height
        Skin.DrawText(GR, m_TooltipText, tr, Skinning.SkinTextPart.TooltipText, StringAlignment.Near, StringAlignment.Near, True)

        If m_TooltipShowTrackRating AndAlso m_TrackRatingImage IsNot Nothing Then
          GR.DrawImage(m_TrackRatingImage, m_TextStartX, nextY + 5, m_TrackRatingImage.Width, m_TrackRatingImage.Height)
        End If

    End Select

    GR.Dispose()
    m_ModuleTooltipBitmap = bmpTemp.Clone
    bmpTemp.Dispose()
  End Sub

#End Region

#Region "Mouse/Keyboard/Menu Processing"

  Public Overrides Sub ProcessMouseMove(ByVal e As System.Windows.Forms.MouseEventArgs, ByRef bWindowIsDirty As Boolean)
    Dim pt As Point = Skin.ToolbarPointToModulePoint(e.Location, m_Bounds)

    ' Open Button
    If m_OpenButtonBounds.Contains(pt) Then
      If e.Button = MouseButtons.None Then
        If m_OpenButtonState <> 1 Then
          m_OpenButtonState = 1
          bWindowIsDirty = True
        End If
        If m_IsPlaying AndAlso m_ShowNowPlayingTooltip Then
          Tooltip.SetTooltipOwner(InfoBarModuleGUID, "NowPlaying")
          DrawTooltip("NowPlaying")
          Tooltip.UpdateTooltip()
        Else
          Tooltip.SetTooltipOwner(InfoBarModuleGUID, "OpenButton")
          DrawTooltip("OpenButton")
          Tooltip.UpdateTooltip()
        End If
      ElseIf e.Button = MouseButtons.Left Then
        If m_OpenButtonState <> 2 Then
          m_OpenButtonState = 2
          bWindowIsDirty = True
        End If
      End If
    Else
      If m_OpenButtonState <> 0 Then
        m_OpenButtonState = 0
        bWindowIsDirty = True
      End If
    End If

    ' Previous Button
    If m_PrevButtonBounds.Contains(pt) Then
      If e.Button = MouseButtons.None Then
        If m_PrevButtonState <> 1 Then
          m_PrevButtonState = 1
          bWindowIsDirty = True
        End If
        If m_IsPlaying AndAlso m_ShowNowPlayingTooltip Then
          Tooltip.SetTooltipOwner(InfoBarModuleGUID, "NowPlaying")
          DrawTooltip("NowPlaying")
          Tooltip.UpdateTooltip()
        Else
          Tooltip.SetTooltipOwner(InfoBarModuleGUID, "PrevButton")
          DrawTooltip("PrevButton")
          Tooltip.UpdateTooltip()
        End If
      ElseIf e.Button = MouseButtons.Left Then
        If m_PrevButtonState <> 2 Then
          m_PrevButtonState = 2
          bWindowIsDirty = True
        End If
      End If
    Else
      If m_PrevButtonState <> 0 Then
        m_PrevButtonState = 0
        bWindowIsDirty = True
      End If
    End If

    ' Play Button
    If m_PlayButtonBounds.Contains(pt) Then
      If e.Button = MouseButtons.None Then
        If m_PlayButtonState <> 1 Then
          m_PlayButtonState = 1
          bWindowIsDirty = True
        End If
        If m_IsPlaying AndAlso m_ShowNowPlayingTooltip Then
          Tooltip.SetTooltipOwner(InfoBarModuleGUID, "NowPlaying")
          DrawTooltip("NowPlaying")
          Tooltip.UpdateTooltip()
        Else
          Tooltip.SetTooltipOwner(InfoBarModuleGUID, "PlayButton")
          DrawTooltip("PlayButton")
          Tooltip.UpdateTooltip()
        End If
      ElseIf e.Button = MouseButtons.Left Then
        If m_PlayButtonState <> 2 Then
          m_PlayButtonState = 2
          bWindowIsDirty = True
        End If
      End If
    Else
      If m_PlayButtonState <> 0 Then
        m_PlayButtonState = 0
        bWindowIsDirty = True
      End If
    End If

    ' Pause Button
    If m_PauseButtonBounds.Contains(pt) Then
      If e.Button = MouseButtons.None Then
        If m_PauseButtonState <> 1 Then
          m_PauseButtonState = 1
          bWindowIsDirty = True
        End If
        If m_IsPlaying AndAlso m_ShowNowPlayingTooltip Then
          Tooltip.SetTooltipOwner(InfoBarModuleGUID, "NowPlaying")
          DrawTooltip("NowPlaying")
          Tooltip.UpdateTooltip()
        Else
          Tooltip.SetTooltipOwner(InfoBarModuleGUID, "PauseButton")
          DrawTooltip("PauseButton")
          Tooltip.UpdateTooltip()
        End If
      ElseIf e.Button = MouseButtons.Left Then
        If m_PauseButtonState <> 2 Then
          m_PauseButtonState = 2
          bWindowIsDirty = True
        End If
      End If
    Else
      If m_PauseButtonState <> 0 Then
        m_PauseButtonState = 0
        bWindowIsDirty = True
      End If
    End If

    ' Stop Button
    If m_StopButtonBounds.Contains(pt) Then
      If e.Button = MouseButtons.None Then
        If m_StopButtonState <> 1 Then
          m_StopButtonState = 1
          bWindowIsDirty = True
        End If
        If m_IsPlaying AndAlso m_ShowNowPlayingTooltip Then
          Tooltip.SetTooltipOwner(InfoBarModuleGUID, "NowPlaying")
          DrawTooltip("NowPlaying")
          Tooltip.UpdateTooltip()
        Else
          Tooltip.SetTooltipOwner(InfoBarModuleGUID, "StopButton")
          DrawTooltip("StopButton")
          Tooltip.UpdateTooltip()
        End If
      ElseIf e.Button = MouseButtons.Left Then
        If m_StopButtonState <> 2 Then
          m_StopButtonState = 2
          bWindowIsDirty = True
        End If
      End If
    Else
      If m_StopButtonState <> 0 Then
        m_StopButtonState = 0
        bWindowIsDirty = True
      End If
    End If

    ' Next Button
    If m_NextButtonBounds.Contains(pt) Then
      If e.Button = MouseButtons.None Then
        If m_NextButtonState <> 1 Then
          m_NextButtonState = 1
          bWindowIsDirty = True
        End If
        If m_IsPlaying AndAlso m_ShowNowPlayingTooltip Then
          Tooltip.SetTooltipOwner(InfoBarModuleGUID, "NowPlaying")
          DrawTooltip("NowPlaying")
          Tooltip.UpdateTooltip()
        Else
          Tooltip.SetTooltipOwner(InfoBarModuleGUID, "NextButton")
          DrawTooltip("NextButton")
          Tooltip.UpdateTooltip()
        End If
      ElseIf e.Button = MouseButtons.Left Then
        If m_NextButtonState <> 2 Then
          m_NextButtonState = 2
          bWindowIsDirty = True
        End If
      End If
    Else
      If m_NextButtonState <> 0 Then
        m_NextButtonState = 0
        bWindowIsDirty = True
      End If
    End If

    ' Track Rating Button
    If m_ShowTrackRatingButton = True AndAlso m_IsPlaying = True Then
      If m_TrackRatingButtonBounds.Contains(pt) Then
        If e.Button = MouseButtons.None Then
          If ratingMenu.IsMenuOpen = False Then
            If m_TrackRatingButtonState <> 1 Then
              m_TrackRatingButtonState = 1
              bWindowIsDirty = True
            End If
            If m_IsPlaying AndAlso m_ShowNowPlayingTooltip Then
              Tooltip.SetTooltipOwner(InfoBarModuleGUID, "NowPlaying")
              DrawTooltip("NowPlaying")
              Tooltip.UpdateTooltip()
            Else
              Tooltip.SetTooltipOwner(InfoBarModuleGUID, "TrackRatingButton")
              DrawTooltip("TrackRatingButton")
              Tooltip.UpdateTooltip()
            End If
          End If
        ElseIf e.Button = MouseButtons.Left Then
          If m_TrackRatingButtonState <> 2 Then
            m_TrackRatingButtonState = 2
            bWindowIsDirty = True
          End If
        End If
      Else
        If m_TrackRatingButtonState <> 0 Then
          m_TrackRatingButtonState = 0
          bWindowIsDirty = True
        End If
      End If
    End If

    ' Time / Title Display Textbox
    If m_TimeDisplayBounds.Contains(pt) Then
      Tooltip.SetTooltipOwner(InfoBarModuleGUID, "NowPlaying")
      DrawTooltip("NowPlaying")
      Tooltip.UpdateTooltip()
    End If

    If m_TitleDisplayBounds.Contains(pt) Then
      Tooltip.SetTooltipOwner(InfoBarModuleGUID, "NowPlaying")
      DrawTooltip("NowPlaying")
      Tooltip.UpdateTooltip()
    End If

    If bWindowIsDirty Then
      DrawModule()
      Skin.UpdateWindow()
    End If
  End Sub

  Public Overrides Sub ProcessMouseUp(ByVal e As System.Windows.Forms.MouseEventArgs, ByRef bWindowIsDirty As Boolean)
    Dim pt As Point = Skin.ToolbarPointToModulePoint(e.Location, m_Bounds)

    ' Open Button
    If m_OpenButtonBounds.Contains(pt) = True Then
      If m_OpenButtonState <> 1 Then
        m_OpenButtonState = 1
        ShowWMPWindow()
        If m_IsPlaying AndAlso m_ShowNowPlayingTooltip Then
          Tooltip.SetTooltipOwner(InfoBarModuleGUID, "NowPlaying")
          DrawTooltip("NowPlaying")
          Tooltip.UpdateTooltip()
        Else
          Tooltip.SetTooltipOwner(InfoBarModuleGUID, "OpenButton")
          DrawTooltip("OpenButton")
          Tooltip.UpdateTooltip()
        End If
      End If
    Else
      If m_OpenButtonState <> 0 Then
        m_OpenButtonState = 0
        bWindowIsDirty = True
      End If
    End If

    ' Prev Button
    If m_PrevButtonBounds.Contains(pt) = True Then
      If m_PrevButtonState <> 1 Then
        m_PrevButtonState = 1
        bWindowIsDirty = True
        If m_IsRunning Then m_WMP.Core.controls.previous()
        If m_IsPlaying AndAlso m_ShowNowPlayingTooltip Then
          Tooltip.SetTooltipOwner(InfoBarModuleGUID, "NowPlaying")
          DrawTooltip("NowPlaying")
          Tooltip.UpdateTooltip()
        Else
          Tooltip.SetTooltipOwner(InfoBarModuleGUID, "PrevButton")
          DrawTooltip("PrevButton")
          Tooltip.UpdateTooltip()
        End If
      End If
    Else
      If m_PrevButtonState <> 0 Then
        m_PrevButtonState = 0
        bWindowIsDirty = True
      End If
    End If

    ' Play Button
    If m_PlayButtonBounds.Contains(pt) = True Then
      If m_PlayButtonState <> 1 Then
        m_PlayButtonState = 1
        bWindowIsDirty = True
        If m_IsRunning Then
          m_WMP.Core.controls.stop()
          m_WMP.Core.controls.play()
        End If
        If m_IsPlaying AndAlso m_ShowNowPlayingTooltip Then
          Tooltip.SetTooltipOwner(InfoBarModuleGUID, "NowPlaying")
          DrawTooltip("NowPlaying")
          Tooltip.UpdateTooltip()
        Else
          Tooltip.SetTooltipOwner(InfoBarModuleGUID, "PlayButton")
          DrawTooltip("PlayButton")
          Tooltip.UpdateTooltip()
        End If
      End If
    Else
      If m_PlayButtonState <> 0 Then
        m_PlayButtonState = 0
        bWindowIsDirty = True
      End If
    End If

    ' Pause Button
    If m_PauseButtonBounds.Contains(pt) = True Then
      If m_PauseButtonState <> 1 Then
        m_PauseButtonState = 1
        bWindowIsDirty = True
        If m_IsRunning Then
          If m_WMP.Core.playState = 3 Then
            m_WMP.Core.controls.pause()
          ElseIf m_WMP.Core.playState = 2 Then
            m_WMP.Core.controls.play()
          End If
        End If
        If m_IsPlaying AndAlso m_ShowNowPlayingTooltip Then
          Tooltip.SetTooltipOwner(InfoBarModuleGUID, "NowPlaying")
          DrawTooltip("NowPlaying")
          Tooltip.UpdateTooltip()
        Else
          Tooltip.SetTooltipOwner(InfoBarModuleGUID, "PauseButton")
          DrawTooltip("PauseButton")
          Tooltip.UpdateTooltip()
        End If
      End If
    Else
      If m_PauseButtonState <> 0 Then
        m_PauseButtonState = 0
        bWindowIsDirty = True
      End If
    End If

    ' Stop Button
    If m_StopButtonBounds.Contains(pt) = True Then
      If m_StopButtonState <> 1 Then
        m_StopButtonState = 1
        bWindowIsDirty = True
        If m_IsRunning Then m_WMP.Core.controls.stop()
        If m_IsPlaying AndAlso m_ShowNowPlayingTooltip Then
          Tooltip.SetTooltipOwner(InfoBarModuleGUID, "NowPlaying")
          DrawTooltip("NowPlaying")
          Tooltip.UpdateTooltip()
        Else
          Tooltip.SetTooltipOwner(InfoBarModuleGUID, "StopButton")
          DrawTooltip("StopButton")
          Tooltip.UpdateTooltip()
        End If
      End If
    Else
      If m_StopButtonState <> 0 Then
        m_StopButtonState = 0
        bWindowIsDirty = True
      End If
    End If

    ' Next Button
    If m_NextButtonBounds.Contains(pt) = True Then
      If m_NextButtonState <> 1 Then
        m_NextButtonState = 1
        bWindowIsDirty = True
        If m_IsRunning Then m_WMP.Core.controls.next()
        If m_IsPlaying AndAlso m_ShowNowPlayingTooltip Then
          Tooltip.SetTooltipOwner(InfoBarModuleGUID, "NowPlaying")
          DrawTooltip("NowPlaying")
          Tooltip.UpdateTooltip()
        Else
          Tooltip.SetTooltipOwner(InfoBarModuleGUID, "NextButton")
          DrawTooltip("NextButton")
          Tooltip.UpdateTooltip()
        End If
      End If
    Else
      If m_NextButtonState <> 0 Then
        m_NextButtonState = 0
        bWindowIsDirty = True
      End If
    End If

    ' Track Rating Button
    If m_ShowTrackRatingButton = True AndAlso m_IsPlaying = True Then
      If m_TrackRatingButtonBounds.Contains(pt) = True Then
        If ratingMenu.IsMenuOpen = True Then
          If m_TrackRatingButtonState <> 2 Then
            m_TrackRatingButtonState = 2
            bWindowIsDirty = True
          End If
        Else
          If m_TrackRatingButtonState <> 1 Then
            m_TrackRatingButtonState = 1
            bWindowIsDirty = True
            If m_IsPlaying AndAlso m_ShowNowPlayingTooltip Then
              Tooltip.SetTooltipOwner(InfoBarModuleGUID, "NowPlaying")
              DrawTooltip("NowPlaying")
              Tooltip.UpdateTooltip()
            Else
              Tooltip.SetTooltipOwner(InfoBarModuleGUID, "TrackRatingButton")
              DrawTooltip("TrackRatingButton")
              Tooltip.UpdateTooltip()
            End If
          End If
        End If
      Else
        If m_TrackRatingButtonState <> 0 Then
          m_TrackRatingButtonState = 0
          bWindowIsDirty = True
        End If
      End If
    End If

    If bWindowIsDirty Then
      DrawModule()
      Skin.UpdateWindow()
    End If
  End Sub

  Public Overrides Sub ProcessMouseDown(ByVal e As System.Windows.Forms.MouseEventArgs, ByRef bWindowIsDirty As Boolean)
    Dim pt As Point = Skin.ToolbarPointToModulePoint(e.Location, m_Bounds)

    ' Open Button
    If m_OpenButtonBounds.Contains(pt) = True Then
      If m_OpenButtonState <> 2 Then
        m_OpenButtonState = 2
        bWindowIsDirty = True
      End If
    Else
      If m_OpenButtonState <> 0 Then
        m_OpenButtonState = 0
        bWindowIsDirty = True
      End If
    End If

    ' Prev Button
    If m_PrevButtonBounds.Contains(pt) = True Then
      If m_PrevButtonState <> 2 Then
        m_PrevButtonState = 2
        bWindowIsDirty = True
      End If
    Else
      If m_PrevButtonState <> 0 Then
        m_PrevButtonState = 0
        bWindowIsDirty = True
      End If
    End If

    ' Play Button
    If m_PlayButtonBounds.Contains(pt) = True Then
      If m_PlayButtonState <> 2 Then
        m_PlayButtonState = 2
        bWindowIsDirty = True
      End If
    Else
      If m_PlayButtonState <> 0 Then
        m_PlayButtonState = 0
        bWindowIsDirty = True
      End If
    End If

    ' Pause Button
    If m_PauseButtonBounds.Contains(pt) = True Then
      If m_PauseButtonState <> 2 Then
        m_PauseButtonState = 2
        bWindowIsDirty = True
      End If
    Else
      If m_PauseButtonState <> 0 Then
        m_PauseButtonState = 0
        bWindowIsDirty = True
      End If
    End If

    ' Stop Button
    If m_StopButtonBounds.Contains(pt) = True Then
      If m_StopButtonState <> 2 Then
        m_StopButtonState = 2
        bWindowIsDirty = True
      End If
    Else
      If m_StopButtonState <> 0 Then
        m_StopButtonState = 0
        bWindowIsDirty = True
      End If
    End If

    ' Next Button
    If m_NextButtonBounds.Contains(pt) = True Then
      If m_NextButtonState <> 2 Then
        m_NextButtonState = 2
        bWindowIsDirty = True
      End If
    Else
      If m_NextButtonState <> 0 Then
        m_NextButtonState = 0
        bWindowIsDirty = True
      End If
    End If

    ' Track Rating Button
    If m_ShowTrackRatingButton = True AndAlso m_IsPlaying = True Then
      If m_TrackRatingButtonBounds.Contains(pt) = True Then
        If m_TrackRatingButtonState <> 2 Then
          m_TrackRatingButtonState = 2
          bWindowIsDirty = True
        End If
        If ratingMenu.IsMenuOpen = True Then
          ratingMenu.Close()
        Else
          If m_IsRunning = True Then
            Dim ptm As Point = Skin.ModulePointToToolbarPoint(InfoBarModuleGUID, New Point(m_TrackRatingButtonBounds.X, m_TrackRatingButtonBounds.Y))
            ratingMenu.ShowPopupMenu(New Rectangle(ptm, m_TrackRatingButtonBounds.Size))
          End If
        End If
      Else
        If m_TrackRatingButtonState <> 0 Then
          m_TrackRatingButtonState = 0
          bWindowIsDirty = True
        End If
      End If
    End If

    If bWindowIsDirty Then
      DrawModule()
      Skin.UpdateWindow()
    End If
  End Sub

  Public Overrides Sub ProcessMouseLeave(ByRef bWindowIsDirty As Boolean)

    ' Open Button
    If m_OpenButtonState <> 0 Then
      m_OpenButtonState = 0
      bWindowIsDirty = True
    End If

    ' Prev Button
    If m_PrevButtonState <> 0 Then
      m_PrevButtonState = 0
      bWindowIsDirty = True
    End If

    ' Play Button
    If m_PlayButtonState <> 0 Then
      m_PlayButtonState = 0
      bWindowIsDirty = True
    End If

    ' Pause Button
    If m_PauseButtonState <> 0 Then
      m_PauseButtonState = 0
      bWindowIsDirty = True
    End If

    ' Stop Button
    If m_StopButtonState <> 0 Then
      m_StopButtonState = 0
      bWindowIsDirty = True
    End If

    ' Next Button
    If m_NextButtonState <> 0 Then
      m_NextButtonState = 0
      bWindowIsDirty = True
    End If

    ' Track Rating Button
    If m_ShowTrackRatingButton = True AndAlso m_IsPlaying = True Then
      If ratingMenu.IsMenuOpen = False Then
        If m_TrackRatingButtonState <> 0 Then
          m_TrackRatingButtonState = 0
          bWindowIsDirty = True
        End If
      End If
    End If

    If bWindowIsDirty Then
      DrawModule()
      Skin.UpdateWindow()
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
    Get
      Return m_Bounds
    End Get
    Set(ByVal value As System.Drawing.Rectangle)
      m_Bounds = value
    End Set
  End Property

  ' InfoBar calls this when your module is right clicked. The popup menu will be shown.
  ' You can create menu items that appear at the top of the popup menu.
  Public Overrides Sub AddMainPopupMenuItems()
    ' A recommended standard feature is to allow users to access your module's settings
    ' dialog from the popup menu.
    MainMenu.AddMenuItem(InfoBarModuleGUID & "::" & "SETTINGS", "Windows Media Player Remote Control Settings...", My.Resources.icon, True, True)

    ' Always add a separator when you're done.
    MainMenu.AddSeparator()
  End Sub

  ' InfoBar calls this when one of your module's menu items are selected.
  Public Overrides Sub ProcessMenuItemClick(ByVal Key As String)
    Select Case Key
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
    If m_SettingsDialog.radButtonDisplayIcons.Checked = True Then
      m_PlaybackButtonDisplay = 0
    ElseIf m_SettingsDialog.radButtonDisplayText.Checked = True Then
      m_PlaybackButtonDisplay = 1
    Else
      m_PlaybackButtonDisplay = 2
    End If
    m_ShowNowPlayingTooltip = m_SettingsDialog.chkShowNowPlayingTooltip.Checked
    m_ShowTrackRatingButton = m_SettingsDialog.chkShowTrackRatingButton.Checked
    m_ShowTrackRating = m_SettingsDialog.chkSongTitleShowTrackRating.Checked
    m_SongTitleTrackRatingOpacity = m_SettingsDialog.nudSongTitleTrackRatingOpacity.Value
    m_ShowSongTimeInfo = m_SettingsDialog.chkShowSongTimeInfo.Checked
    If m_SettingsDialog.radSongTimeModeElapsed.Checked = True Then
      m_SongTimeInfoMode = 0
    Else
      m_SongTimeInfoMode = 1
    End If
    m_ShowSongDuration = m_SettingsDialog.chkShowDuration.Checked
    m_SongTimeWidth = m_SettingsDialog.nudWidth.Value
    m_ShowSongTagInfo = m_SettingsDialog.chkShowSongTagInfo.Checked
    m_SongTagWidth = m_SettingsDialog.nudSongTagWidth.Value
    m_SongTagFormattingString = m_SettingsDialog.txtTagStringFormat.Text
    m_TooltipShowAlbumArt = m_SettingsDialog.chkShowAlbumArt.Checked
    m_AlbumArtFormattingString = m_SettingsDialog.txtAlbumArtFormattingString.Text
    m_TooltipAlbumArtWidth = m_SettingsDialog.nudAlbumArtWidth.Value
    m_TooltipShowTrackRating = m_SettingsDialog.chkShowTrackRating.Checked
    m_TooltipTextFormatString = m_SettingsDialog.txtTooltipFormatString.Text
    m_TooltipTextMaxWidth = m_SettingsDialog.nudTooltipTextMaxWidth.Value

    DrawModule()
  End Sub

  ' InfoBar will call this when the application loads. Use this to set your internal settings.
  Public Overrides Sub LoadSettings(ByRef Doc As XmlDocument)
    ' Always set defaults first.
    m_PlaybackButtonDisplay = 0
    m_ShowNowPlayingTooltip = True
    m_ShowTrackRatingButton = False
    m_ShowTrackRating = True
    m_SongTitleTrackRatingOpacity = 64
    m_ShowSongDuration = True
    m_ShowSongTagInfo = True
    m_ShowSongTimeInfo = True
    m_SongTagWidth = 300
    m_SongTagFormattingString = "%artist% - %title%"
    m_SongTimeInfoMode = 0
    m_SongTimeWidth = 75
    m_TooltipShowAlbumArt = True
    m_AlbumArtFormattingString = "$replace(%path%,%filename_ext%,folder.*)"
    m_TooltipAlbumArtWidth = 150
    m_TooltipShowTrackRating = True
    m_TooltipTextFormatString = "%artist%" & vbCrLf & "%title%"
    m_TooltipTextMaxWidth = 400

    ' Now use the InfoBar Settings Service to load your settings.
    If Doc IsNot Nothing Then
      m_PlaybackButtonDisplay = Settings.GetSetting(Doc, "playbackbuttondisplay", 0)
      m_ShowNowPlayingTooltip = Settings.GetSetting(Doc, "shownowplayingtooltip", True)
      m_ShowTrackRatingButton = Settings.GetSetting(Doc, "showtrackratingbutton", False)
      m_ShowTrackRating = Settings.GetSetting(Doc, "showsongtitletrackrating", True)
      m_SongTitleTrackRatingOpacity = Settings.GetSetting(Doc, "songtitletrackratingopacity", 64)
      m_ShowSongTimeInfo = Settings.GetSetting(Doc, "showsongtimeinfo", True)
      m_SongTimeInfoMode = Settings.GetSetting(Doc, "songtimeinfomode", 0)
      m_ShowSongDuration = Settings.GetSetting(Doc, "showsongduration", 1)
      m_SongTimeWidth = Settings.GetSetting(Doc, "songtimewidth", 75)
      m_ShowSongTagInfo = Settings.GetSetting(Doc, "showsongtaginfo", True)
      m_SongTagWidth = Settings.GetSetting(Doc, "songtagwidth", 300)
      m_SongTagFormattingString = Settings.GetSetting(Doc, "songtagformattingstring", "%artist% - %title%")
      m_TooltipShowAlbumArt = Settings.GetSetting(Doc, "tooltipshowalbumart", True)
      m_AlbumArtFormattingString = Settings.GetSetting(Doc, "albumartformattingstring", "$replace(%path%,%filename_ext%,folder.*)")
      m_TooltipAlbumArtWidth = Settings.GetSetting(Doc, "tooltipalbumartwidth", 150)
      m_TooltipShowTrackRating = Settings.GetSetting(Doc, "tooltipshowtrackrating", True)
      m_TooltipTextFormatString = Settings.GetSetting(Doc, "tooltiptextformatstring", "%artist%" & vbCrLf & "%title%")
      m_TooltipTextMaxWidth = Settings.GetSetting(Doc, "tooltiptextmaxwidth", 400)
    End If

    ResetSettings()
  End Sub

  Public Overrides Sub ResetSettings()
    Select Case m_PlaybackButtonDisplay
      Case 0 ' Icons Only
        m_SettingsDialog.radButtonDisplayIcons.Checked = True
      Case 1 ' Text Only
        m_SettingsDialog.radButtonDisplayText.Checked = True
      Case 2 ' Icons + Text
        m_SettingsDialog.radButtonDisplayIconsAndText.Checked = True
    End Select
    m_SettingsDialog.chkShowTrackRatingButton.Checked = m_ShowTrackRatingButton
    m_SettingsDialog.chkSongTitleShowTrackRating.Checked = m_ShowTrackRating

    If m_SongTitleTrackRatingOpacity < m_SettingsDialog.nudSongTitleTrackRatingOpacity.Minimum Then m_SongTitleTrackRatingOpacity = m_SettingsDialog.nudSongTitleTrackRatingOpacity.Minimum
    If m_SongTitleTrackRatingOpacity > m_SettingsDialog.nudSongTitleTrackRatingOpacity.Maximum Then m_SongTitleTrackRatingOpacity = m_SettingsDialog.nudSongTitleTrackRatingOpacity.Maximum
    m_SettingsDialog.nudSongTitleTrackRatingOpacity.Value = m_SongTitleTrackRatingOpacity

    m_SettingsDialog.chkShowNowPlayingTooltip.Checked = m_ShowNowPlayingTooltip
    m_SettingsDialog.chkShowSongTimeInfo.Checked = m_ShowSongTimeInfo
    Select Case m_SongTimeInfoMode
      Case 0 ' Elapsed
        m_SettingsDialog.radSongTimeModeElapsed.Checked = True
      Case 1 ' Remaining
        m_SettingsDialog.radSongTimeModeRemaining.Checked = True
    End Select
    m_SettingsDialog.chkShowDuration.Checked = m_ShowSongDuration

    If m_SongTimeWidth < m_SettingsDialog.nudWidth.Minimum Then m_SongTimeWidth = m_SettingsDialog.nudWidth.Minimum
    If m_SongTimeWidth > m_SettingsDialog.nudWidth.Maximum Then m_SongTimeWidth = m_SettingsDialog.nudWidth.Maximum
    m_SettingsDialog.nudWidth.Value = m_SongTimeWidth

    m_SettingsDialog.chkShowSongTagInfo.Checked = m_ShowSongTagInfo

    If m_SongTagWidth < m_SettingsDialog.nudSongTagWidth.Minimum Then m_SongTagWidth = m_SettingsDialog.nudSongTagWidth.Minimum
    If m_SongTagWidth > m_SettingsDialog.nudSongTagWidth.Maximum Then m_SongTagWidth = m_SettingsDialog.nudSongTagWidth.Maximum
    m_SettingsDialog.nudSongTagWidth.Value = m_SongTagWidth

    m_SettingsDialog.txtTagStringFormat.Text = m_SongTagFormattingString
    m_SettingsDialog.chkShowAlbumArt.Checked = m_TooltipShowAlbumArt
    m_SettingsDialog.txtAlbumArtFormattingString.Text = m_AlbumArtFormattingString

    If m_TooltipAlbumArtWidth < m_SettingsDialog.nudAlbumArtWidth.Minimum Then m_TooltipAlbumArtWidth = m_SettingsDialog.nudAlbumArtWidth.Minimum
    If m_TooltipAlbumArtWidth > m_SettingsDialog.nudAlbumArtWidth.Maximum Then m_TooltipAlbumArtWidth = m_SettingsDialog.nudAlbumArtWidth.Maximum
    m_SettingsDialog.nudAlbumArtWidth.Value = m_TooltipAlbumArtWidth

    m_SettingsDialog.chkShowTrackRating.Checked = m_TooltipShowTrackRating
    m_SettingsDialog.txtTooltipFormatString.Text = m_TooltipTextFormatString

    If m_TooltipTextMaxWidth < m_SettingsDialog.nudTooltipTextMaxWidth.Minimum Then m_TooltipTextMaxWidth = m_SettingsDialog.nudTooltipTextMaxWidth.Minimum
    If m_TooltipTextMaxWidth > m_SettingsDialog.nudTooltipTextMaxWidth.Maximum Then m_TooltipTextMaxWidth = m_SettingsDialog.nudTooltipTextMaxWidth.Maximum
    m_SettingsDialog.nudTooltipTextMaxWidth.Value = m_TooltipTextMaxWidth
  End Sub

  ' InfoBar will call this when the application exits. Use this to save your internal settings.
  Public Overrides Sub SaveSettings(ByRef Doc As XmlDocument)
    Dim Node As XmlNode

    ' Playback Button Display
    Node = Doc.CreateNode(XmlNodeType.Element, "Node", "playbackbuttondisplay", "")
    Node.InnerText = m_PlaybackButtonDisplay
    Doc.DocumentElement.AppendChild(Node)

    ' Show Now Playing Tooltip
    Node = Doc.CreateNode(XmlNodeType.Element, "Node", "shownowplayingtooltip", "")
    Node.InnerText = m_ShowNowPlayingTooltip
    Doc.DocumentElement.AppendChild(Node)

    ' Show Track Rating Button
    Node = Doc.CreateNode(XmlNodeType.Element, "Node", "showtrackratingbutton", "")
    Node.InnerText = m_ShowTrackRatingButton
    Doc.DocumentElement.AppendChild(Node)

    ' Show Song Title Track Rating
    Node = Doc.CreateNode(XmlNodeType.Element, "Node", "showsongtitletrackrating", "")
    Node.InnerText = m_ShowTrackRating
    Doc.DocumentElement.AppendChild(Node)

    ' Song Title Track Rating Opacity
    Node = Doc.CreateNode(XmlNodeType.Element, "Node", "songtitletrackratingopacity", "")
    Node.InnerText = m_SongTitleTrackRatingOpacity
    Doc.DocumentElement.AppendChild(Node)

    ' Show Song Time Info
    Node = Doc.CreateNode(XmlNodeType.Element, "Node", "showsongtimeinfo", "")
    Node.InnerText = m_ShowSongTimeInfo
    Doc.DocumentElement.AppendChild(Node)

    ' Song Time Info Mode
    Node = Doc.CreateNode(XmlNodeType.Element, "Node", "songtimeinfomode", "")
    Node.InnerText = m_SongTimeInfoMode
    Doc.DocumentElement.AppendChild(Node)

    ' Show Song Duration
    Node = Doc.CreateNode(XmlNodeType.Element, "Node", "showsongduration", "")
    Node.InnerText = m_ShowSongDuration
    Doc.DocumentElement.AppendChild(Node)

    ' Song Time Width
    Node = Doc.CreateNode(XmlNodeType.Element, "Node", "songtimewidth", "")
    Node.InnerText = m_SongTimeWidth
    Doc.DocumentElement.AppendChild(Node)

    ' Show Song Tag Info
    Node = Doc.CreateNode(XmlNodeType.Element, "Node", "showsongtaginfo", "")
    Node.InnerText = m_ShowSongTagInfo
    Doc.DocumentElement.AppendChild(Node)

    ' Song Tag Width
    Node = Doc.CreateNode(XmlNodeType.Element, "Node", "songtagwidth", "")
    Node.InnerText = m_SongTagWidth
    Doc.DocumentElement.AppendChild(Node)

    ' Song Tag Formatting String
    Node = Doc.CreateNode(XmlNodeType.Element, "Node", "songtagformattingstring", "")
    Node.InnerText = m_SongTagFormattingString
    Doc.DocumentElement.AppendChild(Node)

    ' Show Album Art
    Node = Doc.CreateNode(XmlNodeType.Element, "Node", "tooltipshowalbumart", "")
    Node.InnerText = m_TooltipShowAlbumArt
    Doc.DocumentElement.AppendChild(Node)

    ' Album Art Formatting String
    Node = Doc.CreateNode(XmlNodeType.Element, "Node", "albumartformattingstring", "")
    Node.InnerText = m_AlbumArtFormattingString
    Doc.DocumentElement.AppendChild(Node)

    ' Album Art Width
    Node = Doc.CreateNode(XmlNodeType.Element, "Node", "tooltipalbumartwidth", "")
    Node.InnerText = m_TooltipAlbumArtWidth
    Doc.DocumentElement.AppendChild(Node)

    ' Show Track Rating
    Node = Doc.CreateNode(XmlNodeType.Element, "Node", "tooltipshowtrackrating", "")
    Node.InnerText = m_TooltipShowTrackRating
    Doc.DocumentElement.AppendChild(Node)

    ' Tooltip Text Format String
    Node = Doc.CreateNode(XmlNodeType.Element, "Node", "tooltiptextformatstring", "")
    Node.InnerText = m_TooltipTextFormatString
    Doc.DocumentElement.AppendChild(Node)

    ' Tooltip Text Max Width
    Node = Doc.CreateNode(XmlNodeType.Element, "Node", "tooltiptextmaxwidth", "")
    Node.InnerText = m_TooltipTextMaxWidth
    Doc.DocumentElement.AppendChild(Node)

  End Sub

#End Region

#Region "Windows Media Player Helper Routines"

  Private Function IsMusicPlaying() As Boolean
    If m_IsRunning Then
      Try
        If m_WMP.Core.playState = 2 Or m_WMP.Core.playState = 3 Then
          Return True
        Else
          Return False
        End If
      Catch ex As Exception
        Return False
      End Try
    Else
      Return False
    End If

  End Function

  Private Function GetSongTimeInfo() As Boolean

    If m_ShowSongTimeInfo Then

      Dim sText As String = vbNullString
      Dim secs As ULong = 0
      If m_SongTimeInfoMode = 0 Then
        ' Elapsed
        Try
          secs = m_WMP.Core.controls.currentPosition
        Catch ex As Exception
          secs = 0
        End Try
      Else
        ' Remaining
        Try
          secs = m_WMP.Core.currentMedia.duration - m_WMP.Core.controls.currentPosition
        Catch ex As Exception
          secs = 0
        End Try
        sText = "-"
      End If
      sText &= Utilities.FormatTime(secs)
      If sText <> vbNullString AndAlso m_ShowSongDuration Then
        Dim dur As ULong = 0
        Try
          dur = m_WMP.Core.currentMedia.duration
        Catch ex As Exception
          dur = 0
        End Try
        sText = sText & " / " & Utilities.FormatTime(dur)
      End If

      If m_TimeDisplayText <> sText Then
        m_TimeDisplayText = sText
        Return True
      Else
        Return False
      End If

    Else

      If m_TimeDisplayText <> vbNullString Then
        m_TimeDisplayText = vbNullString
        Return True
      Else
        Return False
      End If

    End If

  End Function

  Private Function GetSongTitle() As Boolean

    If m_ShowSongTagInfo Then

      Dim sText As String = vbNullString
      sText = FormatTitle(m_SongTagFormattingString)
      If m_TitleDisplayText <> sText Then
        m_TitleDisplayText = sText
        Return True
      Else
        Return False
      End If

    Else

      If m_TitleDisplayText <> vbNullString Then
        m_TitleDisplayText = vbNullString
        Return True
      Else
        Return False
      End If

    End If

  End Function

  Private Function GetAlbumArt() As Boolean

    If m_TooltipShowAlbumArt = True Then

      Dim sAlbumArtPath As String = vbNullString
      Try
        sAlbumArtPath = m_WMP.Core.currentMedia.getItemInfo("WM/AlbumCoverURL")
      Catch ex As Exception
      End Try

      If sAlbumArtPath = vbNullString Then
        Try
          Dim sSongPath As String = IO.Path.GetDirectoryName(m_WMP.Core.currentMedia.getItemInfo("SourceURL"))
          sAlbumArtPath = m_AlbumArtFormattingString.Replace("%path%", sSongPath)
          If Right(sAlbumArtPath, 2) = ".*" Then
            Dim sTemp As String
            sTemp = Mid(sAlbumArtPath, 1, sAlbumArtPath.Length - 2)
            If System.IO.File.Exists(sTemp & ".jpg") = True Then
              sAlbumArtPath = sTemp & ".jpg"
            ElseIf System.IO.File.Exists(sTemp & ".gif") = True Then
              sAlbumArtPath = sTemp & ".gif"
            ElseIf System.IO.File.Exists(sTemp & ".png") = True Then
              sAlbumArtPath = sTemp & ".png"
            ElseIf System.IO.File.Exists(sTemp & ".bmp") = True Then
              sAlbumArtPath = sTemp & ".bmp"
            Else
              sAlbumArtPath = vbNullString
            End If
          End If

        Catch ex2 As Exception
          sAlbumArtPath = vbNullString
        End Try
      End If

      If m_AlbumArtPath <> sAlbumArtPath Then
        m_AlbumArtPath = sAlbumArtPath
        If sAlbumArtPath <> vbNullString Then
          If System.IO.File.Exists(sAlbumArtPath) Then
            Try
              m_AlbumArtImage = Skin.LoadImageFromFile(sAlbumArtPath)
            Catch ex As Exception
              m_AlbumArtImage = Nothing
            End Try
          Else
            m_AlbumArtImage = Nothing
          End If
        Else
          m_AlbumArtImage = Nothing
        End If
        Return True
      Else
        Return False
      End If

    Else
      Return False
    End If

  End Function

  Private Function GetRating() As Boolean

    If m_TooltipShowTrackRating = True Then

      ' Get Rating
      Dim iRating As Integer
      Try
        iRating = CInt(m_WMP.Core.currentMedia.getItemInfo("UserRating"))
      Catch
        iRating = 0
      End Try

      If m_TrackRating <> iRating Then
        m_TrackRating = iRating
        Select Case iRating
          Case 0
            If IconTheme.IsDefaultTheme = True Then
              m_TrackRatingImage = My.Resources.rating0
            Else
              m_TrackRatingImage = IconTheme.GetThemeIcon(InfoBarModuleGUID, "Rating0")
              If m_TrackRatingImage Is Nothing Then m_TrackRatingImage = My.Resources.rating0
            End If
          Case 1 To 20
            If IconTheme.IsDefaultTheme = True Then
              m_TrackRatingImage = My.Resources.rating1
            Else
              m_TrackRatingImage = IconTheme.GetThemeIcon(InfoBarModuleGUID, "Rating1")
              If m_TrackRatingImage Is Nothing Then m_TrackRatingImage = My.Resources.rating1
            End If
          Case 21 To 40
            If IconTheme.IsDefaultTheme = True Then
              m_TrackRatingImage = My.Resources.rating2
            Else
              m_TrackRatingImage = IconTheme.GetThemeIcon(InfoBarModuleGUID, "Rating2")
              If m_TrackRatingImage Is Nothing Then m_TrackRatingImage = My.Resources.rating2
            End If
          Case 41 To 60
            If IconTheme.IsDefaultTheme = True Then
              m_TrackRatingImage = My.Resources.rating3
            Else
              m_TrackRatingImage = IconTheme.GetThemeIcon(InfoBarModuleGUID, "Rating3")
              If m_TrackRatingImage Is Nothing Then m_TrackRatingImage = My.Resources.rating3
            End If
          Case 61 To 80
            If IconTheme.IsDefaultTheme = True Then
              m_TrackRatingImage = My.Resources.rating4
            Else
              m_TrackRatingImage = IconTheme.GetThemeIcon(InfoBarModuleGUID, "Rating4")
              If m_TrackRatingImage Is Nothing Then m_TrackRatingImage = My.Resources.rating4
            End If
          Case 81 To 100
            If IconTheme.IsDefaultTheme = True Then
              m_TrackRatingImage = My.Resources.rating5
            Else
              m_TrackRatingImage = IconTheme.GetThemeIcon(InfoBarModuleGUID, "Rating5")
              If m_TrackRatingImage Is Nothing Then m_TrackRatingImage = My.Resources.rating5
            End If
          Case Else
            If IconTheme.IsDefaultTheme = True Then
              m_TrackRatingImage = My.Resources.rating0
            Else
              m_TrackRatingImage = IconTheme.GetThemeIcon(InfoBarModuleGUID, "Rating0")
              If m_TrackRatingImage Is Nothing Then m_TrackRatingImage = My.Resources.rating0
            End If
        End Select
        Return True
      Else
        Return False
      End If

    Else
      Return False
    End If

  End Function

  Private Function GetTooltipText() As Boolean
    Dim sTooltipText As String = vbNullString
    Try
      sTooltipText = FormatTitle(m_TooltipTextFormatString)
    Catch ex As Exception
      sTooltipText = vbNullString
    End Try

    If m_TooltipText <> sTooltipText Then
      m_TooltipText = sTooltipText
      Return True
    Else
      Return False
    End If
  End Function

  Private Sub ratingMenu_ItemClicked(ByVal Key As String) Handles ratingMenu.ItemClicked
    ratingMenu.Close()

    Dim sRating As String = ""
    Select Case Key
      Case "5Stars"
        sRating = 100
      Case "4Stars"
        sRating = 80
      Case "3Stars"
        sRating = 60
      Case "2Stars"
        sRating = 40
      Case "1Star"
        sRating = 20
      Case "NoRating"
        sRating = 0
    End Select
    m_WMP.Core.currentMedia.setItemInfo("UserRating", sRating)
  End Sub

  Private Sub ratingMenu_MenuClosed() Handles ratingMenu.MenuClosed
    If m_TrackRatingButtonBounds.Contains(Cursor.Position) = False Then
      m_TrackRatingButtonState = 0
      Skin.UpdateWindow()
    End If
  End Sub

  Private Function FormatTitle(ByVal sFormat As String) As String
    Try
      sFormat = sFormat.Replace("%album artist%", m_WMP.Core.currentMedia.getItemInfo("WM/AlbumArtist"))
    Catch ex As Exception
    End Try

    Try
      sFormat = sFormat.Replace("%album%", m_WMP.Core.currentMedia.getItemInfo("Album"))
    Catch ex As Exception
    End Try

    Try
      sFormat = sFormat.Replace("%artist%", m_WMP.Core.currentMedia.getItemInfo("Artist"))
    Catch ex As Exception
    End Try

    Try
      sFormat = sFormat.Replace("%title%", m_WMP.Core.currentMedia.getItemInfo("Title"))
    Catch ex As Exception
    End Try

    Try
      sFormat = sFormat.Replace("%tracknumber%", m_WMP.Core.currentMedia.getItemInfo("WM/TrackNumber"))
    Catch ex As Exception
    End Try

    Try
      sFormat = sFormat.Replace("%genre%", m_WMP.Core.currentMedia.getItemInfo("WM/Genre"))
    Catch ex As Exception
    End Try

    Try
      sFormat = sFormat.Replace("%date%", m_WMP.Core.currentMedia.getItemInfo("WM/Year"))
    Catch ex As Exception
    End Try

    Try
      sFormat = sFormat.Replace("%bitrate%", m_WMP.Core.currentMedia.getItemInfo("Bitrate"))
    Catch ex As Exception
    End Try

    Try
      sFormat = sFormat.Replace("%filesize%", m_WMP.Core.currentMedia.getItemInfo("FileSize"))
    Catch ex As Exception
    End Try

    Try
      sFormat = sFormat.Replace("%playback_time%", Utilities.FormatTime(m_WMP.Core.controls.currentPosition))
    Catch ex As Exception
    End Try

    Try
      sFormat = sFormat.Replace("%playback_time_remaining%", Utilities.FormatTime(m_WMP.Core.currentMedia.duration - m_WMP.Core.controls.currentPosition))
    Catch ex As Exception
    End Try

    Try
      sFormat = sFormat.Replace("%length%", Utilities.FormatTime(m_WMP.Core.currentMedia.duration))
    Catch ex As Exception
    End Try

    Return sFormat
  End Function

  Private Sub ShowWMPWindow()
    If m_IsRunning Then
      Dim hWnd As IntPtr
      Dim intRet As Integer
      Dim wpTemp As WINDOWPLACEMENT
      hWnd = FindWindow("WMPlayerApp", vbNullString)
      wpTemp.Length = Marshal.SizeOf(wpTemp)
      intRet = GetWindowPlacement(hWnd, wpTemp)
      If wpTemp.showCmd = 2 Then
        wpTemp.Length = Marshal.SizeOf(wpTemp)
        wpTemp.flags = 0&
        wpTemp.showCmd = 1
        SetWindowPlacement(hWnd, wpTemp)
      Else
        SetForegroundWindow(hWnd)
      End If
    Else
      ShellExecute(IntPtr.Zero, "open", "wmplayer.exe", vbNullString, vbNullString, 1)
    End If
  End Sub

#End Region

End Class
