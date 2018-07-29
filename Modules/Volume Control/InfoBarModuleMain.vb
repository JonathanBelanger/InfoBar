Public Class InfoBarModuleMain
  ' We inherit the base class. We DO NOT return any of the base classes values.
  Inherits InfoBarModule

  ' Unique ID. If same as other module, the first module will be loaded, the next will be ignored.
  Private Const InfoBarModuleGUID As String = "{AE5FFC06-D00B-404c-AE25-8D244C23C669}"

#Region "InfoBar Service Objects"
  Private Skin As New InfoBar.Services.Skinning
  Private Tooltip As New InfoBar.Services.Tooltips
  Private MainMenu As New InfoBar.Services.PopupMenus.MainMenu
  Private Settings As New InfoBar.Services.Settings
  Private IconTheme As New InfoBar.Services.IconTheme
  Private Utilities As New InfoBar.Services.Utilities
#End Region

#Region "Private Variables"
  Private m_ModuleToolbarBitmap As Bitmap
  Private m_ModuleTooltipBitmap As Bitmap
  Private m_SettingsDialog As New Settings
  Private m_Bounds As Rectangle
  Private m_Enabled As Boolean
  Private m_CurrentVolume As Integer
  Private m_Muted As Boolean
  Private m_Text As String
  Private m_TooltipText As String
  Private m_ButtonState As Integer
  Private m_ButtonBounds As Rectangle
  Private m_SliderBounds As Rectangle
  Private m_SliderButtonState As Integer
  Private m_SliderButtonBounds As Rectangle

  Private m_VolControl As Object
#End Region

#Region "Settings Variables"
  Friend Setting_ShowIcon As Boolean
  Friend Setting_SliderWidth As Integer
  Friend Setting_ShowText As Boolean
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
      Return "This is a volume control module. It displays and allows you to set your system volume."
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
  ' It will also be modified by InfoBar when the module does not need to perform any processing or handle events.
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
    If Utilities.IsWindowsVistaOrAbove Then
      Dim devEnum As New MMDeviceEnumerator
      m_VolControl = devEnum.GetDefaultAudioEndpoint(EDataFlow.eRender, ERole.eMultimedia)
      AddHandler DirectCast(m_VolControl, MMDevice).AudioEndpointVolume.OnVolumeNotification, AddressOf OnVolumeChanged
      m_CurrentVolume = CInt(m_VolControl.AudioEndpointVolume.MasterVolumeLevelScalar * 100)
      m_Muted = m_VolControl.AudioEndpointVolume.Mute
    Else
      m_VolControl = New AudioMixerXP
      AddHandler DirectCast(m_VolControl, AudioMixerXP).VolumeChanged, AddressOf XPOnVolumeChanged
      AddHandler DirectCast(m_VolControl, AudioMixerXP).MuteChanged, AddressOf XPOnMuteChanged
      m_CurrentVolume = m_VolControl.Volume
      m_Muted = m_VolControl.Mute
    End If
    DrawModule()
  End Sub

  ' InfoBar will call this when your module is disabled.
  Public Overrides Sub FinalizeModule()
    '
  End Sub

  Public Overrides Sub TimerTick(ByRef bModuleIsDirty As Boolean)
    If m_Enabled = False Then Exit Sub

    ' Do our drawing now
    DrawModule()
    bModuleIsDirty = True

    ' If we have the tooltip, update it.
    Dim TooltipOwnerGUID As String = Tooltip.GetTooltipOwnerGUID
    If Tooltip.GetTooltipOwnerGUID = InfoBarModuleGUID Then
      Dim TooltipOwnerObjectID As String = Tooltip.GetTooltipOwnerObjectID
      DrawTooltip(TooltipOwnerObjectID)
      Tooltip.UpdateTooltip()
    End If
  End Sub

#End Region

#Region "Module Drawing"

  ' InfoBar uses this to get your local cached bitmap of this module. 
  ' When your timers or events are fired, you should update this bitmap if needed,
  ' then send a call to Skin.UpdateWindow() to let InfoBar know that you need to redraw.
  Public Overrides Function GetModuleBitmap() As System.Drawing.Bitmap
    Try
      Return m_ModuleToolbarBitmap.Clone
    Catch ex As Exception
      Return Nothing
    End Try
  End Function

  Private Sub DrawModule()
    ' First we measure to find out our bitmap size
    Dim width As Integer = 0, height As Integer = Skin.MaxModuleHeight
    Dim ico As Image = Nothing

    m_Text = CInt((m_CurrentVolume / 100) * 100) & "%"
    m_TooltipText = "Current Volume: " & m_Text
    If m_Muted Then m_TooltipText &= " (Muted)"

    Dim grm As Graphics = Graphics.FromHwnd(IntPtr.Zero)
    Dim tr As Rectangle
    Dim trb As Rectangle
    Dim trt As Rectangle

    If Setting_ShowIcon Then
      If m_Muted Then
        If IconTheme.IsDefaultTheme = True Then
          ico = My.Resources.mute
        Else
          ico = IconTheme.GetThemeIcon(InfoBarModuleGUID, "VolumeControlMuted")
          If ico Is Nothing Then ico = My.Resources.mute
        End If
      Else
        Select Case m_CurrentVolume
          Case 0
            If IconTheme.IsDefaultTheme = True Then
              ico = My.Resources.vol1
            Else
              ico = IconTheme.GetThemeIcon(InfoBarModuleGUID, "VolumeControl1")
              If ico Is Nothing Then ico = My.Resources.vol1
            End If
          Case 1 To 32
            If IconTheme.IsDefaultTheme = True Then
              ico = My.Resources.vol2
            Else
              ico = IconTheme.GetThemeIcon(InfoBarModuleGUID, "VolumeControl2")
              If ico Is Nothing Then ico = My.Resources.vol2
            End If
          Case 33 To 65
            If IconTheme.IsDefaultTheme = True Then
              ico = My.Resources.vol3
            Else
              ico = IconTheme.GetThemeIcon(InfoBarModuleGUID, "VolumeControl3")
              If ico Is Nothing Then ico = My.Resources.vol3
            End If
          Case 66 To 100
            If IconTheme.IsDefaultTheme = True Then
              ico = My.Resources.vol4
            Else
              ico = IconTheme.GetThemeIcon(InfoBarModuleGUID, "VolumeControl4")
              If ico Is Nothing Then ico = My.Resources.vol4
            End If
        End Select
      End If

      If ico Is Nothing Then
        If IconTheme.IsDefaultTheme = True Then
          ico = My.Resources.icon
        Else
          ico = IconTheme.GetThemeIcon(InfoBarModuleGUID, "VolumeControl")
          If ico Is Nothing Then ico = My.Resources.icon
        End If
      End If

      trb = Skin.MeasureButton(grm, New Rectangle(0, 0, width, height), ico, vbNullString, m_ButtonState, False)
      width += trb.Width + 4
    End If

    width += Setting_SliderWidth

    If Setting_ShowText Then
      width += 4
      trt = Skin.MeasureText(grm, "100%", Services.Skinning.SkinTextPart.BackgroundText)
      width += trt.Width
    End If

    grm.Dispose()

    If width <= 1 Or height <= 1 Then Exit Sub
    Dim bmpTemp As New Bitmap(width, height, System.Drawing.Imaging.PixelFormat.Format32bppArgb)
    Dim GR As Graphics = Graphics.FromImage(bmpTemp)

    Dim curX As Integer = 0
    Dim lHeight As Integer = bmpTemp.Height

    If Setting_ShowIcon Then
      tr = New Rectangle(curX, 0, trb.Width, trb.Height)
      m_ButtonBounds = Skin.DrawButton(GR, tr, ico, vbNullString, m_ButtonState, False)
      curX += tr.Width + 4
    End If

    tr = New Rectangle(curX, 0, Setting_SliderWidth, lHeight)
    m_SliderBounds = tr
    m_SliderButtonBounds = Skin.DrawSlider(GR, tr, m_SliderButtonState, 0, 100, m_CurrentVolume)
    curX += Setting_SliderWidth

    If Setting_ShowText Then
      curX += 4
      trt.X = curX
      trt.Height = lHeight
      Skin.DrawText(GR, m_Text, trt, Services.Skinning.SkinTextPart.BackgroundText, StringAlignment.Far, StringAlignment.Center)
    End If

    GR.Dispose()
    m_ModuleToolbarBitmap = bmpTemp.Clone
    bmpTemp.Dispose()
  End Sub

  ' InfoBar uses this to get your local cached bitmap of this module's tooltip. 
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
      Case "VolumeControl"
        If m_TooltipText = vbNullString Then m_TooltipText = "(ERROR: No Info.)"

        Dim GR As Graphics
        Dim tr As Rectangle
        GR = Graphics.FromHwnd(IntPtr.Zero)
        tr = Skin.MeasureText(GR, m_TooltipText, Services.Skinning.SkinTextPart.TooltipText)
        GR.Dispose()

        Dim bmpTemp As New Bitmap(tr.Width, tr.Height, System.Drawing.Imaging.PixelFormat.Format32bppArgb)
        GR = Graphics.FromImage(bmpTemp)
        Skin.DrawText(GR, m_TooltipText, tr, Services.Skinning.SkinTextPart.TooltipText, StringAlignment.Near, StringAlignment.Near)

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
    Dim pt As Point = Skin.ToolbarPointToModulePoint(e.Location, m_Bounds)
    If m_ButtonBounds.Contains(pt) Then
      If m_ButtonState <> 1 Then
        m_ButtonState = 1
        DrawModule()
        bWindowIsDirty = True
      End If
    Else
      If m_ButtonState <> 0 Then
        m_ButtonState = 0
        DrawModule()
        bWindowIsDirty = True
      End If
    End If

    If e.Button = MouseButtons.None Then
      If m_SliderButtonBounds.Contains(pt) Then
        If m_SliderButtonState <> 1 Then
          m_SliderButtonState = 1
          DrawModule()
          bWindowIsDirty = True
        End If
      Else
        If m_SliderButtonState <> 0 Then
          m_SliderButtonState = 0
          DrawModule()
          bWindowIsDirty = True
        End If
      End If
    ElseIf e.Button = MouseButtons.Left Then
      m_SliderButtonState = 2

      m_CurrentVolume = ((pt.X - m_SliderBounds.X) / Setting_SliderWidth) * 100
      Debug.Print(m_CurrentVolume)
      If m_CurrentVolume < 0 Then m_CurrentVolume = 0
      If m_CurrentVolume > 100 Then m_CurrentVolume = 100

      SetCurrentVolume(m_CurrentVolume)

      DrawModule()
      bWindowIsDirty = True
    End If

    ' Show the tooltip.
    Tooltip.SetTooltipOwner(InfoBarModuleGUID, "VolumeControl")
    DrawTooltip("VolumeControl")
    Tooltip.UpdateTooltip()
  End Sub

  ' Check to see if the mouse was depressed on any of your UI rectangles.
  ' Set bWindowIsDirty to true if you need a redraw.
  Public Overrides Sub ProcessMouseUp(ByVal e As System.Windows.Forms.MouseEventArgs, ByRef bWindowIsDirty As Boolean)
    Dim pt As Point = Skin.ToolbarPointToModulePoint(e.Location, m_Bounds)

    If m_ButtonBounds.Contains(pt) Then
      If m_ButtonState <> 1 Then
        m_ButtonState = 1
        If Utilities.IsWindowsVistaOrAbove Then
          m_VolControl.AudioEndpointVolume.Mute = Not m_VolControl.AudioEndpointVolume.Mute
        Else
          m_VolControl.Mute = Not m_VolControl.Mute
        End If
        DrawModule()
        bWindowIsDirty = True
      End If
      Else
        If m_ButtonState <> 0 Then
          m_ButtonState = 0
          DrawModule()
          bWindowIsDirty = True
        End If
      End If

      If m_SliderButtonBounds.Contains(pt) Then
        If m_SliderButtonState <> 1 Then
          ' TODO: Figure out and set the new volume

          m_SliderButtonState = 1
          DrawModule()
          bWindowIsDirty = True
        End If
      Else
        If m_SliderButtonState <> 0 Then
          m_SliderButtonState = 0
          DrawModule()
          bWindowIsDirty = True
        End If
      End If
  End Sub

  ' Check to see if the mouse was pressed down on any of your UI rectangles.
  ' Set bWindowIsDirty to true if you need a redraw.
  Public Overrides Sub ProcessMouseDown(ByVal e As System.Windows.Forms.MouseEventArgs, ByRef bWindowIsDirty As Boolean)
    Dim pt As Point = Skin.ToolbarPointToModulePoint(e.Location, m_Bounds)
    If m_ButtonBounds.Contains(pt) Then
      If m_ButtonState <> 2 Then
        m_ButtonState = 2
        DrawModule()
        bWindowIsDirty = True
      End If
    Else
      If m_ButtonState <> 0 Then
        m_ButtonState = 0
        DrawModule()
        bWindowIsDirty = True
      End If
    End If

    If m_SliderButtonBounds.Contains(pt) Then
      If m_SliderButtonState <> 2 Then
        m_SliderButtonState = 2
        DrawModule()
        bWindowIsDirty = True
      End If
    Else
      If m_SliderButtonState <> 0 Then
        m_SliderButtonState = 0
        DrawModule()
        bWindowIsDirty = True
      End If
    End If
  End Sub

  ' Check to see if the mouse left any of your UI rectangles.
  ' Set bWindowIsDirty to true if you need a redraw.
  Public Overrides Sub ProcessMouseLeave(ByRef bWindowIsDirty As Boolean)
    If m_ButtonState <> 0 Then
      m_ButtonState = 0
      DrawModule()
      bWindowIsDirty = True
    End If

    If m_SliderButtonState <> 0 Then
      m_SliderButtonState = 0
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
    MainMenu.AddMenuItem(InfoBarModuleGUID & "::" & "SETTINGS", "Volume Control Settings...", My.Resources.icon, True, True)

    ' Always add a separator when you're done.
    MainMenu.AddSeparator()
  End Sub

  ' InfoBar calls this when one of your module's menu items are selected.
  Public Overrides Sub ProcessMenuItemClick(ByVal Key As String)
    Select Case Key
      Case InfoBarModuleGUID & "::" & "SETTINGS"    ' Show Volume Control Settings...
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

  ' InfoBar will call this when the user clicks on the module's tree node in the main settings
  ' dialog, or when it is selected from the toolbar's right click menu.
  Public Overrides ReadOnly Property SettingsDialog() As System.Windows.Forms.UserControl
    Get
      Return m_SettingsDialog
    End Get
  End Property

  ' InfoBar will call this when the user clicks on the apply button in the settings dialog, or
  ' clicks the OK button in the settings dialog when the apply button is enabled.
  Public Overrides Sub ApplySettings()
    ' Update our module with the new settings
    Setting_ShowIcon = m_SettingsDialog.chkShowIcon.Checked
    Setting_SliderWidth = m_SettingsDialog.nudSliderWidth.Value
    Setting_ShowText = m_SettingsDialog.chkShowText.Checked

    m_Bounds.Width = 1
    DrawModule()
  End Sub

  ' InfoBar will call this when the application loads. Use this to set your internal settings.
  Public Overrides Sub LoadSettings(ByRef Doc As XmlDocument)
    ' Always set defaults first.
    Setting_ShowIcon = True
    Setting_SliderWidth = 100
    Setting_ShowText = True

    ' Now use the InfoBar Settings Service to load your settings.
    If Doc IsNot Nothing Then
      Setting_ShowIcon = Settings.GetSetting(Doc, "showicon", True)
      Setting_SliderWidth = Settings.GetSetting(Doc, "sliderwidth", 100)
      Setting_ShowText = Settings.GetSetting(Doc, "showtext", True)
    End If

    ' Call ResetSettings() for the first time to setup the settings dialog.
    ResetSettings()
  End Sub

  ' InfoBar will call this when the settings dialog closes.
  Public Overrides Sub ResetSettings()
    ' Configure our settings page with the current values
    m_SettingsDialog.chkShowIcon.Checked = Setting_ShowIcon

    If Setting_SliderWidth < m_SettingsDialog.nudSliderWidth.Minimum Then Setting_SliderWidth = m_SettingsDialog.nudSliderWidth.Minimum
    If Setting_SliderWidth > m_SettingsDialog.nudSliderWidth.Maximum Then Setting_SliderWidth = m_SettingsDialog.nudSliderWidth.Maximum
    m_SettingsDialog.nudSliderWidth.Value = Setting_SliderWidth

    m_SettingsDialog.chkShowText.Checked = Setting_ShowText
  End Sub

  ' InfoBar will call this when the application exits. Use this to save your internal settings.
  Public Overrides Sub SaveSettings(ByRef Doc As XmlDocument)
    Dim Node As XmlNode

    Node = Doc.CreateNode(XmlNodeType.Element, "Node", "showicon", "")
    Node.InnerText = Setting_ShowIcon
    Doc.DocumentElement.AppendChild(Node)

    Node = Doc.CreateNode(XmlNodeType.Element, "Node", "sliderwidth", "")
    Node.InnerText = Setting_SliderWidth
    Doc.DocumentElement.AppendChild(Node)

    Node = Doc.CreateNode(XmlNodeType.Element, "Node", "showtext", "")
    Node.InnerText = Setting_ShowText
    Doc.DocumentElement.AppendChild(Node)

  End Sub

#End Region

#Region "Volume Functions"

  Public Sub SetCurrentVolume(ByVal vol As Integer)
    If Utilities.IsWindowsVistaOrAbove Then
      m_VolControl.AudioEndpointVolume.MasterVolumeLevelScalar = CDbl(vol / 100.0F)
    Else
      m_VolControl.Volume = vol
    End If
  End Sub

  Private Sub OnVolumeChanged(ByVal data As CoreAudioApi.AudioVolumeNotificationData)
    m_CurrentVolume = CInt(data.MasterVolume * 100)
    m_Muted = data.Muted
    DrawModule()

    Dim TooltipOwnerGUID As String = Tooltip.GetTooltipOwnerGUID
    If Tooltip.GetTooltipOwnerGUID = InfoBarModuleGUID Then
      Dim TooltipOwnerObjectID As String = Tooltip.GetTooltipOwnerObjectID
      DrawTooltip(TooltipOwnerObjectID)
      Tooltip.UpdateTooltip()
    End If
  End Sub

  Private Sub XPOnVolumeChanged()
    m_CurrentVolume = m_VolControl.Volume
    DrawModule()

    Dim TooltipOwnerGUID As String = Tooltip.GetTooltipOwnerGUID
    If Tooltip.GetTooltipOwnerGUID = InfoBarModuleGUID Then
      Dim TooltipOwnerObjectID As String = Tooltip.GetTooltipOwnerObjectID
      DrawTooltip(TooltipOwnerObjectID)
      Tooltip.UpdateTooltip()
    End If
  End Sub

  Private Sub XPOnMuteChanged()
    m_Muted = m_VolControl.Mute
    DrawModule()

    Dim TooltipOwnerGUID As String = Tooltip.GetTooltipOwnerGUID
    If Tooltip.GetTooltipOwnerGUID = InfoBarModuleGUID Then
      Dim TooltipOwnerObjectID As String = Tooltip.GetTooltipOwnerObjectID
      DrawTooltip(TooltipOwnerObjectID)
      Tooltip.UpdateTooltip()
    End If
  End Sub

#End Region

End Class
