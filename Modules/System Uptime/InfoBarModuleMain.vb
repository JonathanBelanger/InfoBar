Public Class InfoBarModuleMain
  ' We inherit the base class. We DO NOT return any of the base classes values.
  Inherits InfoBarModule

  ' Unique ID. If same as other module, the first module will be loaded, the next will be ignored.
  Private Const InfoBarModuleGUID As String = "{3D09996C-6930-4389-8042-16BC522B769D}"

#Region "InfoBar Service Objects"
  Private Icons As New InfoBar.Services.IconTheme
  Private Skin As New InfoBar.Services.Skinning
  Private Tooltip As New InfoBar.Services.Tooltips
  Private MainMenu As New InfoBar.Services.PopupMenus.MainMenu
  Private Settings As New InfoBar.Services.Settings
  Private Utils As New InfoBar.Services.Utilities
#End Region

#Region "Private Variables"
  Private m_ModuleToolbarBitmap As Bitmap
  Private m_ModuleTooltipBitmap As Bitmap
  Private m_SettingsDialog As New Settings
  Private m_Bounds As Rectangle
  Private m_Enabled As Boolean
  Private m_Text As String
  Private m_TooltipText As String
#End Region

#Region "Settings Variables"
  Friend Setting_ShowIcon As Boolean
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
      Return "Displays the amount of time Windows has been running."
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
    GetUptime()
    DrawModule()
  End Sub

  ' InfoBar will call this when your module is disabled.
  Public Overrides Sub FinalizeModule()
    '
  End Sub

  ' InfoBar will call this every 1 second. Make sure to do work only when needed.
  ' TIP: If m_Enabled is set to false, don't do any work to save CPU time unless really needed.
  Public Overrides Sub TimerTick(ByRef bModuleIsDirty As Boolean)
    If m_Enabled = False Then Exit Sub

    ' Do any work here
    ' Return true if the UI needs to be updated.
    If GetUptime() Then
      DrawModule()
      bModuleIsDirty = True
    End If

    If Tooltip.GetTooltipOwnerGUID = InfoBarModuleGUID AndAlso _
    Tooltip.GetTooltipOwnerObjectID = "SystemUptime" Then
      DrawTooltip("SystemUptime")
      Tooltip.UpdateTooltip()
    End If

  End Sub

  Private Declare Function GetSystemTimes Lib "kernel32.dll" (ByRef idleTime As UInt64, ByRef kernelTime As UInt64, ByRef userTime As UInt64) As Boolean

  Private Function GetUptime()
    Dim sText As String = vbNullString
    Dim iIdle As UInt64, iKernel As UInt64, iUser As UInt64, iTime As UInt64
    GetSystemTimes(iIdle, iKernel, iUser)
    iTime = iKernel + iUser
    Dim iSeconds As Integer = iTime / 10000000
    ' Possible fix for multi-core
    iSeconds = iSeconds / Environment.ProcessorCount
    Dim tsUptime As New TimeSpan(0, 0, 0, iSeconds)
    If tsUptime.Days > 0 Then sText = tsUptime.Days & "d "
    If tsUptime.Hours > 0 Then sText = sText & tsUptime.Hours & "h "
    If tsUptime.Minutes > 0 Then sText = sText & tsUptime.Minutes & "m "
    If tsUptime.Seconds > 0 Then sText = sText & tsUptime.Seconds & "s "
    m_TooltipText = "System Uptime: " & sText
    If m_Text <> sText Then
      m_Text = sText
      Return True
    End If
    Return False
  End Function

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

    Dim icon As Image
    If Icons.IsDefaultTheme Then
      icon = My.Resources.icon
    Else
      icon = Icons.GetThemeIcon(InfoBarModuleGUID, "SystemUptime")
      If icon Is Nothing Then icon = My.Resources.icon
    End If

    Dim grm As Graphics = Graphics.FromHwnd(IntPtr.Zero)

    ' Draw Icon
    If Setting_ShowIcon = True Then width += icon.Width

    ' Draw Text
    If Setting_ShowText = True Then
      If Setting_ShowIcon = True Then width += 2
      Dim tr As Rectangle = Skin.MeasureText(grm, m_Text, Skinning.SkinTextPart.BackgroundText)
      width += tr.Width
    End If

    ' For this module, we want to keep the width equal or more than the previous width.
    If width < m_Bounds.Width Then width = m_Bounds.Width

    grm.Dispose()

    If width <= 1 Or height <= 1 Then Exit Sub
    Dim bmpTemp As New Bitmap(width, height, System.Drawing.Imaging.PixelFormat.Format32bppArgb)
    Dim GR As Graphics = Graphics.FromImage(bmpTemp)

    ' Get skin height for vertical centering
    Dim curX As Integer = 0

    ' Draw Icon
    If Setting_ShowIcon = True Then
      GR.DrawImage(icon, curX, (CSng((height - icon.Height)) / 2), icon.Width, icon.Height)
      curX += icon.Width
    End If

    ' Draw Text
    If Setting_ShowText = True Then
      If Setting_ShowIcon = True Then curX += 2
      Dim tr As Rectangle = Skin.MeasureText(Gr, m_Text, Skinning.SkinTextPart.BackgroundText)
      tr.X = curX
      tr.Y = 0
      tr.Height = height
      Skin.DrawText(Gr, m_Text, tr, Skinning.SkinTextPart.BackgroundText, StringAlignment.Near, StringAlignment.Center)
      curX += tr.Width
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
      Case "SystemUptime"
        Dim GR As Graphics = Graphics.FromHwnd(IntPtr.Zero)
        Dim tr As Rectangle=Skin.MeasureText(GR, m_TooltipText, Skinning.SkinTextPart.TooltipText)
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
    If m_Bounds.Contains(e.Location) Then
      Tooltip.SetTooltipOwner(InfoBarModuleGUID, "SystemUptime")
      DrawTooltip("SystemUptime")
      Tooltip.UpdateTooltip()
    End If
  End Sub

  Public Overrides Sub ProcessMouseUp(ByVal e As System.Windows.Forms.MouseEventArgs, ByRef bWindowIsDirty As Boolean)
    '
  End Sub

  Public Overrides Sub ProcessMouseDown(ByVal e As System.Windows.Forms.MouseEventArgs, ByRef bWindowIsDirty As Boolean)
    '
  End Sub

  Public Overrides Sub ProcessMouseLeave(ByRef bWindowIsDirty As Boolean)
    '
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
    MainMenu.AddMenuItem(InfoBarModuleGUID & "::" & "SETTINGS", "System Uptime Settings...", My.Resources.icon, True, True)
    MainMenu.AddSeparator()
  End Sub

  Public Overrides Sub ProcessMenuItemClick(ByVal Key As String)
    Select Case Key
      Case InfoBarModuleGUID & "::" & "SETTINGS"    ' Show Module Template Settings...
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
    Setting_ShowText = m_SettingsDialog.chkShowText.Checked

    m_Bounds.Width = 1
    DrawModule()
  End Sub

  ' InfoBar will call this when the application loads. Use this to set your internal settings.
  Public Overrides Sub LoadSettings(ByRef Doc As XmlDocument)
    ' Always set defaults first.
    Setting_ShowIcon = True
    Setting_ShowText = True

    ' Now use the InfoBar Settings Service to load your settings.
    If Doc IsNot Nothing Then
      Setting_ShowIcon = Settings.GetSetting(Doc, "showicon", True)
      Setting_ShowText = Settings.GetSetting(Doc, "showtext", True)
    End If

    ResetSettings()
  End Sub

  Public Overrides Sub ResetSettings()
    ' Configure our settings page with the current values
    m_SettingsDialog.chkShowIcon.Checked = Setting_ShowIcon
    m_SettingsDialog.chkShowText.Checked = Setting_ShowText
  End Sub

  ' InfoBar will call this when the application exits. Use this to save your internal settings.
  Public Overrides Sub SaveSettings(ByRef Doc As XmlDocument)
    Dim Node As XmlNode

    Node = Doc.CreateNode(XmlNodeType.Element, "Node", "showicon", "")
    Node.InnerText = Setting_ShowIcon
    Doc.DocumentElement.AppendChild(Node)

    Node = Doc.CreateNode(XmlNodeType.Element, "Node", "showtext", "")
    Node.InnerText = Setting_ShowText
    Doc.DocumentElement.AppendChild(Node)

  End Sub

#End Region

End Class
