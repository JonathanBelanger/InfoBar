Public Class InfoBarModuleMain
  ' We inherit the base class. We DO NOT return any of the base classes values.
  Inherits InfoBarModule

  ' Unique ID. If same as other module, the first module will be loaded, the next will be ignored.
  Private Const InfoBarModuleGUID As String = "{94261832-1913-4eda-928F-9F9573795EDC}"

#Region "Windows API"
  <DllImport("user32.dll", CallingConvention:=CallingConvention.StdCall, CharSet:=CharSet.Unicode, EntryPoint:="keybd_event", ExactSpelling:=True, SetLastError:=True)> _
  Public Shared Function keybd_event(ByVal bVk As Int32, ByVal bScan As Int32, ByVal dwFlags As Int32, ByVal dwExtraInfo As Int32) As Boolean
  End Function
#End Region

#Region "InfoBar Service Objects"
  Private Skin As New InfoBar.Services.Skinning
  Private Tooltip As New InfoBar.Services.Tooltips
  Private MainMenu As New InfoBar.Services.PopupMenus.MainMenu
  Private Settings As New InfoBar.Services.Settings
  Private IconTheme As New InfoBar.Services.IconTheme
#End Region

#Region "Private Variables"
  Private m_ModuleToolbarBitmap As Bitmap
  Private m_ModuleTooltipBitmap As Bitmap
  Private m_SettingsDialog As New Settings
  Private m_Bounds As Rectangle
  Private m_Enabled As Boolean
  Private m_TooltipText As String
  Private m_ScrollLockOn As Boolean
  Private m_CapsLockOn As Boolean
  Private m_NumLockOn As Boolean
  Private m_ScrollLockOnButtonState As Integer
  Private m_CapsLockOnButtonState As Integer
  Private m_NumLockOnButtonState As Integer
  Private m_ScrollLockOnButtonBounds As Rectangle
  Private m_CapsLockOnButtonBounds As Rectangle
  Private m_NumLockOnButtonBounds As Rectangle
#End Region

#Region "Settings Variables"
  Friend Setting_ShowIcon As Boolean
  Friend Setting_ShowCapsLockIcon As Boolean
  Friend Setting_ShowNumLockIcon As Boolean
  Friend Setting_ShowScrollLockIcon As Boolean
  Friend Setting_ShowInsertModeIcon As Boolean
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
      Return "This module displays the status of your caps lock, num lock, scroll lock and insert keyboard modes."
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

  Public Overrides Sub InitializeModule()
    m_TooltipText = "Keyboard Status"
    KeyboardStatus_Update()
    DrawModule()
  End Sub

  Public Overrides Sub FinalizeModule()
    '
  End Sub

  Public Overrides Sub TimerTick(ByRef bModuleIsDirty As Boolean)
    If m_Enabled = False Then Exit Sub

    If KeyboardStatus_Update() Then
      bModuleIsDirty = True

      DrawModule()

      m_TooltipText = "Num Lock: " & IIf(m_NumLockOn, " On", " Off") & vbCrLf
      m_TooltipText &= "Caps Lock: " & IIf(m_CapsLockOn, " On", " Off") & vbCrLf
      m_TooltipText &= "Scroll Lock: " & IIf(m_ScrollLockOn, " On", "Off")

      If Tooltip.GetTooltipOwnerGUID = InfoBarModuleGUID _
      AndAlso Tooltip.GetTooltipOwnerObjectID = "KeyboardStatus" Then
        DrawTooltip("KeyboardStatus")
        Tooltip.UpdateTooltip()
      End If

    End If
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

    Dim icoKb As Image, icoCapsOn As Image, icoCapsOff As Image, icoNumOn As Image, icoNumOff As Image, icoScrollOn As Image, icoScrollOff As Image
    If IconTheme.IsDefaultTheme = True Then
      icoKb = My.Resources.icon
      icoCapsOn = My.Resources.caps_on
      icoCapsOff = My.Resources.caps_off
      icoNumOn = My.Resources.num_on
      icoNumOff = My.Resources.num_off
      icoScrollOn = My.Resources.scroll_on
      icoScrollOff = My.Resources.scroll_off
    Else
      icoKb = IconTheme.GetThemeIcon(InfoBarModuleGUID, "KeyboardStatus")
      If icoKb Is Nothing Then icoKb = My.Resources.icon

      icoCapsOn = IconTheme.GetThemeIcon(InfoBarModuleGUID, "CapsLockOn")
      If icoCapsOn Is Nothing Then icoCapsOn = My.Resources.caps_on

      icoCapsOff = IconTheme.GetThemeIcon(InfoBarModuleGUID, "CapsLockOff")
      If icoCapsOff Is Nothing Then icoCapsOff = My.Resources.caps_off

      icoNumOn = IconTheme.GetThemeIcon(InfoBarModuleGUID, "NumLockOn")
      If icoNumOn Is Nothing Then icoNumOn = My.Resources.num_on

      icoNumOff = IconTheme.GetThemeIcon(InfoBarModuleGUID, "NumLockOff")
      If icoNumOff Is Nothing Then icoNumOff = My.Resources.num_off

      icoScrollOn = IconTheme.GetThemeIcon(InfoBarModuleGUID, "ScrollLockOn")
      If icoScrollOn Is Nothing Then icoScrollOn = My.Resources.scroll_on

      icoScrollOff = IconTheme.GetThemeIcon(InfoBarModuleGUID, "ScrollLockOff")
      If icoScrollOff Is Nothing Then icoScrollOff = My.Resources.scroll_off
    End If

    ' Draw Icon
    If Setting_ShowIcon = True Then width += icoKb.Width + 4

    Dim tr As Rectangle, trm As New Rectangle(0, 0, 1, height)
    Dim grTemp As Graphics = Graphics.FromHwnd(IntPtr.Zero)

    ' Num
    If Setting_ShowNumLockIcon Then
      If m_NumLockOn Then
        tr = Skin.MeasureButton(grTemp, trm, icoNumOn, Nothing, m_NumLockOnButtonState, False)
      Else
        tr = Skin.MeasureButton(grTemp, trm, icoNumOff, Nothing, m_NumLockOnButtonState, False)
      End If
      width += tr.Width
    End If

    ' Caps
    If Setting_ShowCapsLockIcon Then
      If m_CapsLockOn Then
        tr = Skin.MeasureButton(grTemp, trm, icoCapsOn, Nothing, m_CapsLockOnButtonState, False)
      Else
        tr = Skin.MeasureButton(grTemp, trm, icoCapsOff, Nothing, m_CapsLockOnButtonState, False)
      End If
      width += tr.Width
    End If

    ' Scroll
    If Setting_ShowScrollLockIcon Then
      If m_ScrollLockOn Then
        tr = Skin.MeasureButton(grTemp, trm, icoScrollOn, Nothing, m_ScrollLockOnButtonState, False)
      Else
        tr = Skin.MeasureButton(grTemp, trm, icoScrollOff, Nothing, m_ScrollLockOnButtonState, False)
      End If
      width += tr.Width
    End If

    grTemp.Dispose()

    If width <= 1 Or height <= 1 Then Exit Sub
    Dim bmpTemp As New Bitmap(width, height, System.Drawing.Imaging.PixelFormat.Format32bppArgb)
    Dim GR As Graphics = Graphics.FromImage(bmpTemp)
    Dim curX As Integer = 0    

    ' Draw Icon
    If Setting_ShowIcon = True Then
      GR.DrawImage(icoKb, curX, (CSng((height - icoKb.Height)) / 2), icoKb.Width, icoKb.Height)
      curX += icoKb.Width + 4
    End If

    ' Num
    If Setting_ShowNumLockIcon Then
      If m_NumLockOn Then
        tr = Skin.DrawButton(GR, New Rectangle(curX, 0, 1, height), icoNumOn, Nothing, m_NumLockOnButtonState, False)
      Else
        tr = Skin.DrawButton(GR, New Rectangle(curX, 0, 1, height), icoNumOff, Nothing, m_NumLockOnButtonState, False)
      End If
      m_NumLockOnButtonBounds = tr
      curX += tr.Width
    Else
      m_NumLockOnButtonBounds = Nothing
    End If

    ' Caps
    If Setting_ShowCapsLockIcon Then
      If m_CapsLockOn Then
        tr = Skin.DrawButton(GR, New Rectangle(curX, 0, 1, height), icoCapsOn, Nothing, m_CapsLockOnButtonState, False)
      Else
        tr = Skin.DrawButton(GR, New Rectangle(curX, 0, 1, height), icoCapsOff, Nothing, m_CapsLockOnButtonState, False)
      End If
      m_CapsLockOnButtonBounds = tr
      curX += tr.Width
    Else
      m_CapsLockOnButtonBounds = Nothing
    End If

    ' Scroll
    If Setting_ShowScrollLockIcon Then
      If m_ScrollLockOn Then
        tr = Skin.DrawButton(GR, New Rectangle(curX, 0, 1, height), icoScrollOn, Nothing, m_ScrollLockOnButtonState, False)
      Else
        tr = Skin.DrawButton(GR, New Rectangle(curX, 0, 1, height), icoScrollOff, Nothing, m_ScrollLockOnButtonState, False)
      End If
      m_ScrollLockOnButtonBounds = tr
      curX += tr.Width
    Else
      m_ScrollLockOnButtonBounds = Nothing
    End If

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

  Private Sub DrawTooltip(ByVal ObjectID As String)
    Select Case ObjectID
      Case "KeyboardStatus"
        Dim GR As Graphics
        GR = Graphics.FromHwnd(IntPtr.Zero)
        Dim tr As Rectangle = Skin.MeasureText(GR, m_TooltipText, Skinning.SkinTextPart.TooltipText)
        GR.Dispose()

        Dim bmpTemp As New Bitmap(tr.Width, tr.Height, System.Drawing.Imaging.PixelFormat.Format32bppArgb)
        GR = Graphics.FromImage(bmpTemp)
        Skin.DrawText(GR, m_TooltipText, tr, Skinning.SkinTextPart.TooltipText, StringAlignment.Near, StringAlignment.Near)

        GR.Dispose()
        m_ModuleTooltipBitmap = bmpTemp.Clone
        bmpTemp.Dispose()
    End Select
  End Sub

#End Region

#Region "Mouse/Keyboard/Menu Processing"

  Public Overrides Sub ProcessMouseMove(ByVal e As System.Windows.Forms.MouseEventArgs, ByRef bWindowIsDirty As Boolean)
    Dim pt As Point = Skin.ToolbarPointToModulePoint(e.Location, m_Bounds)

    If m_CapsLockOnButtonBounds.Contains(pt) Then
      If e.Button = MouseButtons.None Then
        If m_CapsLockOnButtonState <> 1 Then
          m_CapsLockOnButtonState = 1
          DrawModule()
          bWindowIsDirty = True
        End If
      ElseIf e.Button = MouseButtons.Left Then
        If m_CapsLockOnButtonState <> 2 Then
          m_CapsLockOnButtonState = 2
          DrawModule()
          bWindowIsDirty = True
        End If
      End If
    Else
      If m_CapsLockOnButtonState <> 0 Then
        m_CapsLockOnButtonState = 0
        DrawModule()
        bWindowIsDirty = True
      End If
    End If

    If m_ScrollLockOnButtonBounds.Contains(pt) Then
      If e.Button = MouseButtons.None Then
        If m_ScrollLockOnButtonState <> 1 Then
          m_ScrollLockOnButtonState = 1
          DrawModule()
          bWindowIsDirty = True
        End If
      ElseIf e.Button = MouseButtons.Left Then
        If m_ScrollLockOnButtonState <> 2 Then
          m_ScrollLockOnButtonState = 2
          DrawModule()
          bWindowIsDirty = True
        End If
      End If
    Else
      If m_ScrollLockOnButtonState <> 0 Then
        m_ScrollLockOnButtonState = 0
        DrawModule()
        bWindowIsDirty = True
      End If
    End If

    If m_NumLockOnButtonBounds.Contains(pt) Then
      If e.Button = MouseButtons.None Then
        If m_NumLockOnButtonState <> 1 Then
          m_NumLockOnButtonState = 1
          DrawModule()
          bWindowIsDirty = True
        End If
      ElseIf e.Button = MouseButtons.Left Then
        If m_NumLockOnButtonState <> 2 Then
          m_NumLockOnButtonState = 2
          DrawModule()
          bWindowIsDirty = True
        End If
      End If
    Else
      If m_NumLockOnButtonState <> 0 Then
        m_NumLockOnButtonState = 0
        DrawModule()
        bWindowIsDirty = True
      End If
    End If

    If m_Bounds.Contains(e.Location) Then
      Tooltip.SetTooltipOwner(InfoBarModuleGUID, "KeyboardStatus")
      DrawTooltip("KeyboardStatus")
      Tooltip.UpdateTooltip()
    End If
  End Sub

  Public Overrides Sub ProcessMouseUp(ByVal e As System.Windows.Forms.MouseEventArgs, ByRef bWindowIsDirty As Boolean)
    Dim p As Point = Skin.ToolbarPointToModulePoint(e.Location, m_Bounds)

    If m_CapsLockOnButtonBounds.Contains(p) Then
      If m_CapsLockOnButtonState <> 1 Then
        m_CapsLockOnButtonState = 1
        m_CapsLockOn = Not m_CapsLockOn
        ToggleKeyState(Keys.CapsLock)
        bWindowIsDirty = True
        DrawModule()
      End If
    Else
      If m_CapsLockOnButtonState <> 0 Then
        m_CapsLockOnButtonState = 0
        bWindowIsDirty = True
        DrawModule()
      End If
    End If

    If m_ScrollLockOnButtonBounds.Contains(p) Then
      If m_ScrollLockOnButtonState <> 1 Then
        m_ScrollLockOnButtonState = 1
        m_ScrollLockOn = Not m_ScrollLockOn
        ToggleKeyState(Keys.Scroll)
        bWindowIsDirty = True
        DrawModule()
      End If
    Else
      If m_ScrollLockOnButtonState <> 0 Then
        m_ScrollLockOnButtonState = 0
        bWindowIsDirty = True
        DrawModule()
      End If
    End If

    If m_NumLockOnButtonBounds.Contains(p) Then
      If m_NumLockOnButtonState <> 1 Then
        m_NumLockOnButtonState = 1
        m_NumLockOn = Not m_NumLockOn
        ToggleKeyState(Keys.NumLock)
        bWindowIsDirty = True
        DrawModule()
      End If
    Else
      If m_NumLockOnButtonState <> 0 Then
        m_NumLockOnButtonState = 0
        bWindowIsDirty = True
        DrawModule()
      End If
    End If
  End Sub

  Public Overrides Sub ProcessMouseDown(ByVal e As System.Windows.Forms.MouseEventArgs, ByRef bWindowIsDirty As Boolean)
    Dim p As Point = Skin.ToolbarPointToModulePoint(e.Location, m_Bounds)

    If m_CapsLockOnButtonBounds.Contains(p) Then
      If m_CapsLockOnButtonState <> 2 Then
        m_CapsLockOnButtonState = 2
        bWindowIsDirty = True
        DrawModule()
      End If
    Else
      If m_CapsLockOnButtonState <> 0 Then
        m_CapsLockOnButtonState = 0
        bWindowIsDirty = True
        DrawModule()
      End If
    End If

    If m_NumLockOnButtonBounds.Contains(p) Then
      If m_NumLockOnButtonState <> 2 Then
        m_NumLockOnButtonState = 2
        bWindowIsDirty = True
        DrawModule()
      End If
    Else
      If m_NumLockOnButtonState <> 0 Then
        m_NumLockOnButtonState = 0
        bWindowIsDirty = True
        DrawModule()
      End If
    End If

    If m_ScrollLockOnButtonBounds.Contains(p) Then
      If m_ScrollLockOnButtonState <> 2 Then
        m_ScrollLockOnButtonState = 2
        bWindowIsDirty = True
        DrawModule()
      End If
    Else
      If m_ScrollLockOnButtonState <> 0 Then
        m_ScrollLockOnButtonState = 0
        bWindowIsDirty = True
        DrawModule()
      End If
    End If
  End Sub

  Public Overrides Sub ProcessMouseLeave(ByRef bWindowIsDirty As Boolean)
    If m_CapsLockOnButtonState <> 0 Then
      m_CapsLockOnButtonState = 0
      bWindowIsDirty = True
      DrawModule()
    End If

    If m_ScrollLockOnButtonState <> 0 Then
      m_ScrollLockOnButtonState = 0
      bWindowIsDirty = True
      DrawModule()
    End If

    If m_NumLockOnButtonState <> 0 Then
      m_NumLockOnButtonState = 0
      bWindowIsDirty = True
      DrawModule()
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

  Public Overrides Property ModuleBounds() As System.Drawing.Rectangle
    Set(ByVal value As System.Drawing.Rectangle)
      m_Bounds = value
    End Set
    Get
      Return m_Bounds
    End Get
  End Property

  Public Overrides Sub AddMainPopupMenuItems()
    MainMenu.AddMenuItem(InfoBarModuleGUID & "::" & "SETTINGS", "Keyboard Status Settings...", My.Resources.icon, True, True)
    MainMenu.AddSeparator()
  End Sub

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
    Setting_ShowCapsLockIcon = m_SettingsDialog.chkShowCapsLockIcon.Checked
    Setting_ShowNumLockIcon = m_SettingsDialog.chkShowNumLockIcon.Checked
    Setting_ShowScrollLockIcon = m_SettingsDialog.chkShowScrollLockIcon.Checked

    DrawModule()
  End Sub

  ' InfoBar will call this when the application loads. Use this to set your internal settings.
  Public Overrides Sub LoadSettings(ByRef Doc As XmlDocument)
    ' Always set defaults first.
    Setting_ShowIcon = True
    Setting_ShowCapsLockIcon = True
    Setting_ShowNumLockIcon = True
    Setting_ShowScrollLockIcon = True
    Setting_ShowInsertModeIcon = True

    ' Now use the InfoBar Settings Service to load your settings.
    If Doc IsNot Nothing Then
      Setting_ShowIcon = Settings.GetSetting(Doc, "showicon", True)
      Setting_ShowCapsLockIcon = Settings.GetSetting(Doc, "showcapslockicon", True)
      Setting_ShowNumLockIcon = Settings.GetSetting(Doc, "shownumlockicon", True)
      Setting_ShowScrollLockIcon = Settings.GetSetting(Doc, "showscrolllockicon", True)
      Setting_ShowInsertModeIcon = Settings.GetSetting(Doc, "showinsertmodeicon", True)
    End If

    ResetSettings()
  End Sub

  Public Overrides Sub ResetSettings()
    m_SettingsDialog.chkShowIcon.Checked = Setting_ShowIcon
    m_SettingsDialog.chkShowCapsLockIcon.Checked = Setting_ShowCapsLockIcon
    m_SettingsDialog.chkShowNumLockIcon.Checked = Setting_ShowNumLockIcon
    m_SettingsDialog.chkShowScrollLockIcon.Checked = Setting_ShowScrollLockIcon
  End Sub

  ' InfoBar will call this when the application exits. Use this to save your internal settings.
  Public Overrides Sub SaveSettings(ByRef Doc As XmlDocument)
    Dim Node As XmlNode

    Node = Doc.CreateNode(XmlNodeType.Element, "Node", "showicon", "")
    Node.InnerText = Setting_ShowIcon
    Doc.DocumentElement.AppendChild(Node)

    Node = Doc.CreateNode(XmlNodeType.Element, "Node", "showcapslockicon", "")
    Node.InnerText = Setting_ShowCapsLockIcon
    Doc.DocumentElement.AppendChild(Node)

    Node = Doc.CreateNode(XmlNodeType.Element, "Node", "shownumlockicon", "")
    Node.InnerText = Setting_ShowNumLockIcon
    Doc.DocumentElement.AppendChild(Node)

    Node = Doc.CreateNode(XmlNodeType.Element, "Node", "showscrolllockicon", "")
    Node.InnerText = Setting_ShowScrollLockIcon
    Doc.DocumentElement.AppendChild(Node)

    Node = Doc.CreateNode(XmlNodeType.Element, "Node", "showinsertmodeicon", "")
    Node.InnerText = Setting_ShowInsertModeIcon
    Doc.DocumentElement.AppendChild(Node)
  End Sub

#End Region

#Region "Keyboard Status Functions"

  Private Function KeyboardStatus_Update()
    Dim bNeedUpdate As Boolean = False

    Dim bCapsLockOn As Boolean = My.Computer.Keyboard.CapsLock
    If m_CapsLockOn <> bCapsLockOn Then
      m_CapsLockOn = bCapsLockOn
      bNeedUpdate = True
    End If

    Dim bNumLockOn As Boolean = My.Computer.Keyboard.NumLock
    If m_NumLockOn <> bNumLockOn Then
      m_NumLockOn = bNumLockOn
      bNeedUpdate = True
    End If

    Dim bScrollLockOn As Boolean = My.Computer.Keyboard.ScrollLock
    If m_ScrollLockOn <> bScrollLockOn Then
      m_ScrollLockOn = bScrollLockOn
      bNeedUpdate = True
    End If

    Return bNeedUpdate
  End Function

  Private Sub ToggleKeyState(ByVal KeyCode As Keys)
    keybd_event(KeyCode, 0, 0, 0)
    keybd_event(KeyCode, 0, &H2, 0)
  End Sub

#End Region

End Class
