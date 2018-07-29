Public Class InfoBarModuleMain
  ' We inherit the base class. We DO NOT return any of the base classes values.
  Inherits InfoBarModule

  ' Unique ID. If same as other module, the first module will be loaded, the next will be ignored.
  Private Const InfoBarModuleGUID As String = "{F767157C-1608-4aac-9C5C-99B1E1890760}"

#Region "InfoBar Service Objects"
  Private Skin As New InfoBar.Services.Skinning
  Private Tooltip As New InfoBar.Services.Tooltips
  Private IconTheme As New InfoBar.Services.IconTheme
  Private MainMenu As New InfoBar.Services.PopupMenus.MainMenu
  Private Settings As New InfoBar.Services.Settings
  Private Utilities As New InfoBar.Services.Utilities
#End Region

#Region "Private Variables"
  Private m_ModuleToolbarBitmap As Bitmap
  Private m_ModuleTooltipBitmap As Bitmap
  Private m_SettingsDialog As New Settings
  Private m_Bounds As Rectangle
  Private m_TextLastWidth As Integer
  Private m_Enabled As Boolean
  Private m_Icon As Image
  Private m_Text As String
  Private m_TooltipText As String

  Private m_Interface As NetworkInterface
  Private m_DownloadPrevTotal As Long
  Private m_DownloadSpeed As Long
  Private m_DownloadAvg As Long
  Private m_DownloadAvgTotal As Long
  Private m_DownloadAvgCount As Long
  Private m_DownloadTotal As Long
  Private m_DownloadMax As Long
  Private m_DownloadGraphHistory As New Collections.ArrayList(100)
  Private m_DownloadGraphAvgVals As Long
  Private m_DownloadGraphAvgCount As Integer
  Private m_UploadPrevTotal As Long
  Private m_UploadSpeed As Long
  Private m_UploadAvg As Long
  Private m_UploadAvgTotal As Long
  Private m_UploadAvgCount As Long
  Private m_UploadTotal As Long
  Private m_UploadMax As Long
  Private m_UploadGraphHistory As New Collections.ArrayList(100)
  Private m_UploadGraphAvgVals As Long
  Private m_UploadGraphAvgCount As Integer

  Private m_ExternalIPAddress As String
  Private m_InternalIPAddress As String
  Private m_ExternalIPAddressSecondsSinceLastRequest As Integer
#End Region

#Region "Settings Variables"
  Friend Enum DisplayUnit
    Auto
    Bits
    Kilobits
    Megabits
    Gigabits
    Terabits
    Bytes
    Kilobytes
    Megabytes
    Gigabytes
    Terabytes
  End Enum

  Friend Enum TimeMode
    Seconds
    Minutes
    Hours
  End Enum

  Friend Setting_Interface As String
  Friend Setting_SaveAverageSpeeds As Boolean
  Friend Setting_SaveMaxSpeeds As Boolean
  Friend Setting_SaveTransferTotals As Boolean

  Friend Setting_Icon_Show As Boolean

  Friend Setting_Text_Show As Boolean
  Friend Setting_Text_ShowDownloadSpeed As Boolean
  Friend Setting_Text_ShowDownloadTotal As Boolean
  Friend Setting_Text_ShowUploadSpeed As Boolean
  Friend Setting_Text_ShowUploadTotal As Boolean
  Friend Setting_Text_SpeedDisplayUnit As DisplayUnit
  Friend Setting_Text_TotalDisplayUnit As DisplayUnit
  Friend Setting_Text_Orientation As Integer
  Friend Setting_Text_ShowExternalIP As Boolean
  Friend Setting_Text_ShowInternalIP As Boolean

  Friend Setting_Tooltip_ShowDownloadSpeed As Boolean
  Friend Setting_Tooltip_ShowDownloadTotal As Boolean
  Friend Setting_Tooltip_ShowDownloadAvg As Boolean
  Friend Setting_Tooltip_ShowDownloadMax As Boolean
  Friend Setting_Tooltip_ShowUploadSpeed As Boolean
  Friend Setting_Tooltip_ShowUploadTotal As Boolean
  Friend Setting_Tooltip_ShowUploadAvg As Boolean
  Friend Setting_Tooltip_ShowUploadMax As Boolean
  Friend Setting_Tooltip_SpeedDisplayUnit As DisplayUnit
  Friend Setting_Tooltip_TotalDisplayUnit As DisplayUnit
  Friend Setting_Tooltip_ShowExternalIP As Boolean
  Friend Setting_Tooltip_ShowInternalIP As Boolean

  Friend Setting_Graph_Show As Boolean
  Friend Setting_Graph_DisplayMode As Integer
  Friend Setting_Graph_DownloadLineColor As Color
  Friend Setting_Graph_UploadLineColor As Color
  Friend Setting_Graph_ShowDownloadSpeed As Boolean
  Friend Setting_Graph_ShowUploadSpeed As Boolean
  Friend Setting_Graph_UpdateTime As Integer
  Friend Setting_Graph_UpdateTimeMode As TimeMode
  Friend Setting_Graph_UseStaticMaxValues As Boolean
  Friend Setting_Graph_StaticMaxDown As Double
  Friend Setting_Graph_StaticMaxUp As Double
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
      Return "Displays information about your network usage."
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
    m_ExternalIPAddressSecondsSinceLastRequest = 500

    InitNetworkUsage()
    UpdateNetworkUsage()
    DrawModule()
  End Sub

  ' InfoBar will call this when your module is disabled.
  Public Overrides Sub FinalizeModule()
    UninitNetworkUsage()
  End Sub

  ' InfoBar will call this every 1 second. Make sure to do work only when needed.
  ' TIP: If m_Enabled is set to false, don't do any work to save CPU time unless really needed.
  Public Overrides Sub TimerTick(ByRef bModuleIsDirty As Boolean)
    If m_Enabled = False Then Exit Sub
    If UpdateNetworkUsage() Then
      DrawModule()
      bModuleIsDirty = True
    End If

    If Tooltip.GetTooltipOwnerGUID = InfoBarModuleGUID Then
      If Tooltip.GetTooltipOwnerObjectID = "NetworkUsage" Then
        DrawTooltip("NetworkUsage")
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

    Dim grm As Graphics = Graphics.FromHwnd(IntPtr.Zero)

    If Setting_Icon_Show = True Then width += m_Icon.Width

    If Setting_Text_Show = True Then
      If Setting_Icon_Show Then width += 2
      Dim tr As Rectangle
      tr = Skin.MeasureText(grm, m_Text, Skinning.SkinTextPart.BackgroundText, -1, -1.5)
      If tr.Width > m_TextLastWidth Then
        m_TextLastWidth = tr.Width
      Else
        tr.Width = m_TextLastWidth
      End If
      width += tr.Width
    End If

    If Setting_Graph_Show = True Then
      If Setting_Icon_Show Or Setting_Text_Show Then width += 2
      width += 100
    End If

    grm.Dispose()

    If width <= 1 Or height <= 1 Then Exit Sub
    Dim bmpTemp As New Bitmap(width, height, System.Drawing.Imaging.PixelFormat.Format32bppArgb)
    Dim GR As Graphics = Graphics.FromImage(bmpTemp)

    Dim curX As Integer = 0

    If Setting_Icon_Show = True Then
      GR.DrawImage(m_Icon, curX, CInt((height - m_Icon.Height) / 2), m_Icon.Width, m_Icon.Height)
      curX += m_Icon.Width
    End If

    If Setting_Text_Show = True Then
      If Setting_Icon_Show Then curX += 2
      Dim tr As Rectangle
      If Setting_Text_Orientation = 0 Then
        tr = Skin.MeasureText(GR, m_Text, Skinning.SkinTextPart.BackgroundText, -1)
        tr.X = curX
        tr.Y = 0
      Else
        tr = Skin.MeasureText(GR, m_Text, Skinning.SkinTextPart.BackgroundText, -1, -1.5)
        tr.X = curX
        tr.Y = (height - tr.Height) / 2
      End If
      tr.Height = height
      If tr.Width > m_TextLastWidth Then
        m_TextLastWidth = tr.Width
      Else
        tr.Width = m_TextLastWidth
      End If
      If Setting_Text_Orientation = 0 Then
        Skin.DrawText(GR, m_Text, tr, Skinning.SkinTextPart.BackgroundText, StringAlignment.Near, StringAlignment.Center, False)
      Else
        Skin.DrawText(GR, m_Text, tr, Skinning.SkinTextPart.BackgroundText, StringAlignment.Near, StringAlignment.Center, True, -1.5)
      End If
      curX += tr.Width
    End If

    If Setting_Graph_Show = True Then
      If Setting_Icon_Show Or Setting_Text_Show Then curX = curX + 2
      Dim rg As Rectangle
      rg.X = curX
      rg.Y = 0
      rg.Width = 100
      rg.Height = height
      Skin.DrawGraphBackground(GR, rg)

      Dim gcm As Skinning.SkinMargins = Skin.ContentMargins(Skinning.SkinMarginPart.Graph)

      rg.X = rg.X + gcm.Left
      rg.Y = rg.Y + gcm.Top
      rg.Width = rg.Width - (gcm.Left + gcm.Right)
      rg.Height = rg.Height - (gcm.Top + gcm.Bottom)

      ' Temorarily Change Rendering Settings For Blended Lines

      ' Save Old Settings
      Dim oldCompositingQuality As CompositingQuality = GR.CompositingQuality
      Dim oldSmoothingMode As SmoothingMode = GR.SmoothingMode

      ' Apply Perfered Settings
      GR.CompositingQuality = CompositingQuality.HighQuality
      GR.SmoothingMode = SmoothingMode.AntiAlias

      ' Draw Value Lines
      Dim gv As Integer
      Dim x1 As Integer, y1 As Integer, x2 As Integer, y2 As Integer
      Dim gdp As New Pen(Setting_Graph_DownloadLineColor)
      Dim gup As New Pen(Setting_Graph_UploadLineColor)
      For gv = 0 To m_DownloadGraphHistory.Count - 2
        Dim val1 As Integer, val2 As Integer

        ' Convert big numbers to percentages
        ' Formula: (gv / max) * 100
        If Setting_Graph_UseStaticMaxValues Then
          val1 = (m_DownloadGraphHistory(gv) / (Setting_Graph_StaticMaxDown * 1024)) * 100
          val2 = (m_DownloadGraphHistory(gv + 1) / (Setting_Graph_StaticMaxDown * 1024)) * 100
        Else
          If m_DownloadMax > 0 Then
            val1 = (m_DownloadGraphHistory(gv) / m_DownloadMax) * 100
            val2 = (m_DownloadGraphHistory(gv + 1) / m_DownloadMax) * 100
          Else
            val1 = 0
            val2 = 0
          End If
        End If

        x1 = rg.X + ((gv / 100) * rg.Width)
        y1 = rg.Y + (rg.Height - ((val1 / 100) * rg.Height))
        If y1 < rg.Y Then y1 = rg.Y
        x2 = rg.X + (((gv + 1) / 100) * rg.Width)
        y2 = rg.Y + (rg.Height - ((val2 / 100) * rg.Height))
        If y2 < rg.Y Then y2 = rg.Y
        GR.DrawLine(gdp, x1, y1, x2, y2)

        If Setting_Graph_UseStaticMaxValues Then
          val1 = (m_UploadGraphHistory(gv) / (Setting_Graph_StaticMaxUp * 1024)) * 100
          val2 = (m_UploadGraphHistory(gv + 1) / (Setting_Graph_StaticMaxUp * 1024)) * 100
        Else
          If m_UploadMax > 0 Then
            val1 = (m_UploadGraphHistory(gv) / m_UploadMax) * 100
            val2 = (m_UploadGraphHistory(gv + 1) / m_UploadMax) * 100
          Else
            val1 = 0
            val2 = 0
          End If
        End If

        x1 = rg.X + ((gv / 100) * rg.Width)
        y1 = rg.Y + (rg.Height - ((val1 / 100) * rg.Height))
        If y1 < rg.Y Then y1 = rg.Y
        x2 = rg.X + (((gv + 1) / 100) * rg.Width)
        y2 = rg.Y + (rg.Height - ((val2 / 100) * rg.Height))
        If y2 < rg.Y Then y2 = rg.Y
        GR.DrawLine(gup, x1, y1, x2, y2)
      Next
      gdp.Dispose()
      gup.Dispose()

      ' Reset Rendering Settings To Old Values
      GR.CompositingQuality = oldCompositingQuality
      GR.SmoothingMode = oldSmoothingMode

      curX = curX + 100
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
      Case "NetworkUsage"
        Dim GR As Graphics = Graphics.FromHwnd(IntPtr.Zero)
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

  ' Check to see if the mouse is on any of your UI rectangles.
  ' Set bWindowIsDirty to true if you need a redraw.
  Public Overrides Sub ProcessMouseMove(ByVal e As System.Windows.Forms.MouseEventArgs, ByRef bWindowIsDirty As Boolean)
    If m_Bounds.Contains(e.Location) Then
      Tooltip.SetTooltipOwner(InfoBarModuleGUID, "NetworkUsage")
      DrawTooltip("NetworkUsage")
      Tooltip.UpdateTooltip()
    End If
  End Sub

  ' Check to see if the mouse was depressed on any of your UI rectangles.
  ' Set bWindowIsDirty to true if you need a redraw.
  Public Overrides Sub ProcessMouseUp(ByVal e As System.Windows.Forms.MouseEventArgs, ByRef bWindowIsDirty As Boolean)
    '
  End Sub

  ' Check to see if the mouse was pressed down on any of your UI rectangles.
  ' Set bWindowIsDirty to true if you need a redraw.
  Public Overrides Sub ProcessMouseDown(ByVal e As System.Windows.Forms.MouseEventArgs, ByRef bWindowIsDirty As Boolean)
    '
  End Sub

  ' Check to see if the mouse left any of your UI rectangles.
  ' Set bWindowIsDirty to true if you need a redraw.
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

  ' InfoBar will use this to determine if the mouse was in your module's bounds.
  Public Overrides Property ModuleBounds() As System.Drawing.Rectangle
    Set(ByVal value As System.Drawing.Rectangle)
      m_Bounds = value
    End Set
    Get
      Return m_Bounds
    End Get
  End Property

  ' InfoBar calls this when your module is right clicked. The popup menu will be shown.
  ' You can create menu items that appear at the top of the popup menu.
  Public Overrides Sub AddMainPopupMenuItems()
    ' A recommended standard feature is to allow users to access your module's settings
    ' dialog from the popup menu.
    Dim ico As Image
    If IconTheme.IsDefaultTheme = True Then
      ico = My.Resources.icon
    Else
      ico = IconTheme.GetThemeIcon(InfoBarModuleGUID, "CPU")
      If ico Is Nothing Then ico = My.Resources.icon
    End If
    MainMenu.AddMenuItem(InfoBarModuleGUID & "::" & "SETTINGS", "Network Usage Settings...", ico, True, True)

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

  ' InfoBar will call this when the user clicks on the module's tree node in the main settings
  ' dialog, or when it is selected from the toolbar's right click menu.
  Overrides ReadOnly Property SettingsDialog() As UserControl
    Get
      Return m_SettingsDialog
    End Get
  End Property

  ' InfoBar will call this when the user clicks on the apply button in the settings dialog, or
  ' clicks the OK button in the settings dialog when the apply button is enabled.
  Public Overrides Sub ApplySettings()
    m_TextLastWidth = 0

    Setting_Icon_Show = m_SettingsDialog.chkShowIcon.Checked
    If Setting_Interface <> m_SettingsDialog.comInterface.Text Then
      Setting_Interface = m_SettingsDialog.comInterface.Text
      InitNetworkUsage()
    End If

    Setting_Text_Show = m_SettingsDialog.chkShowText.Checked
    Setting_Text_ShowDownloadSpeed = m_SettingsDialog.chkTextDownloadSpeed.Checked
    Setting_Text_ShowDownloadTotal = m_SettingsDialog.chkTextDownloadTotal.Checked
    Setting_Text_ShowUploadSpeed = m_SettingsDialog.chkTextUploadSpeed.Checked
    Setting_Text_ShowUploadTotal = m_SettingsDialog.chkTextUploadTotal.Checked
    Setting_Text_SpeedDisplayUnit = m_SettingsDialog.comTextSpeedDisplayUnit.SelectedIndex
    Setting_Text_TotalDisplayUnit = m_SettingsDialog.comTextTotalDisplayUnit.SelectedIndex
    Setting_Text_Orientation = IIf(m_SettingsDialog.radTextHorz.Checked, 0, 1)
    Setting_Text_ShowExternalIP = m_SettingsDialog.chkToolbarExternalIP.Checked
    Setting_Text_ShowInternalIP = m_SettingsDialog.chkToolbarInternalIP.Checked

    Setting_Tooltip_ShowDownloadSpeed = m_SettingsDialog.chkTooltipDownloadSpeed.Checked
    Setting_Tooltip_ShowDownloadTotal = m_SettingsDialog.chkTooltipDownloadTotal.Checked
    Setting_Tooltip_ShowDownloadAvg = m_SettingsDialog.chkTooltipDownloadAvg.Checked
    Setting_Tooltip_ShowDownloadMax = m_SettingsDialog.chkTooltipDownloadMax.Checked
    Setting_Tooltip_ShowUploadSpeed = m_SettingsDialog.chkTooltipUploadSpeed.Checked
    Setting_Tooltip_ShowUploadTotal = m_SettingsDialog.chkTooltipUploadTotal.Checked
    Setting_Tooltip_ShowUploadAvg = m_SettingsDialog.chkTooltipUploadAvg.Checked
    Setting_Tooltip_ShowUploadMax = m_SettingsDialog.chkTooltipUploadMax.Checked
    Setting_Tooltip_SpeedDisplayUnit = m_SettingsDialog.comTooltipSpeedDisplayUnit.SelectedIndex
    Setting_Tooltip_TotalDisplayUnit = m_SettingsDialog.comTooltipTotalDisplayUnit.SelectedIndex
    Setting_Tooltip_ShowExternalIP = m_SettingsDialog.chkTooltipExternalIP.Checked
    Setting_Tooltip_ShowInternalIP = m_SettingsDialog.chkTooltipInternalIP.Checked

    Setting_Graph_Show = m_SettingsDialog.chkShowGraph.Checked
    Setting_Graph_DisplayMode = m_SettingsDialog.comGraphDisplay.SelectedIndex
    Setting_Graph_DownloadLineColor = m_SettingsDialog.picGraphDownloadColor.BackColor
    Setting_Graph_UploadLineColor = m_SettingsDialog.picGraphUploadColor.BackColor
    Setting_Graph_UpdateTime = m_SettingsDialog.nudGraphUpdateTime.Value
    Setting_Graph_UpdateTimeMode = m_SettingsDialog.comGraphUpdateTime.SelectedIndex

    If m_SettingsDialog.comGraphDisplay.SelectedIndex <> Setting_Graph_DisplayMode OrElse _
    m_SettingsDialog.nudGraphUpdateTime.Value <> Setting_Graph_UpdateTime OrElse _
    m_SettingsDialog.comGraphUpdateTime.SelectedIndex <> Setting_Graph_UpdateTimeMode Then
      Dim I As Integer

      m_DownloadGraphAvgVals = 0
      m_DownloadGraphAvgCount = 0
      m_DownloadGraphHistory.Clear()
      For I = 0 To 99
        m_DownloadGraphHistory.Add(0)
      Next

      m_UploadGraphAvgVals = 0
      m_UploadGraphAvgCount = 0
      m_UploadGraphHistory.Clear()
      For I = 0 To 99
        m_UploadGraphHistory.Add(0)
      Next
    End If

    Setting_Graph_UpdateTime = m_SettingsDialog.nudGraphUpdateTime.Value
    Setting_Graph_UpdateTimeMode = m_SettingsDialog.comGraphUpdateTime.SelectedIndex

    Setting_Graph_UseStaticMaxValues = m_SettingsDialog.chkStaticMaxValues.Checked
    Setting_Graph_StaticMaxDown = CDbl(m_SettingsDialog.txtGraphStaticMaxDown.Text)
    Setting_Graph_StaticMaxUp = CDbl(m_SettingsDialog.txtGraphStaticMaxUp.Text)

    Setting_SaveAverageSpeeds = m_SettingsDialog.chkSaveAverageSpeeds.Checked
    If m_SettingsDialog.chkResetAvgSpeeds.Checked Then
      m_SettingsDialog.chkResetAvgSpeeds.Checked = False
      m_DownloadAvgTotal = 0
      m_DownloadAvgCount = 0
      m_UploadAvgTotal = 0
      m_UploadAvgCount = 0
    End If

    Setting_SaveMaxSpeeds = m_SettingsDialog.chkSaveMaxSpeeds.Checked
    If m_SettingsDialog.chkResetMaxSpeeds.Checked Then
      m_SettingsDialog.chkResetMaxSpeeds.Checked = False
      m_DownloadMax = 0
      m_UploadMax = 0
    End If

    Setting_SaveTransferTotals = m_SettingsDialog.chkSaveTransferTotals.Checked
    If m_SettingsDialog.chkResetTransferTotals.Checked Then
      m_SettingsDialog.chkResetTransferTotals.Checked = False
      m_DownloadTotal = 0
      m_UploadTotal = 0
    End If

    m_Bounds.Width = 1
    UpdateNetworkUsage()
    DrawModule()
  End Sub

  ' InfoBar will call this when the application loads. Use this to load your internal settings.
  Public Overrides Sub LoadSettings(ByRef Doc As XmlDocument)
    Setting_Interface = Settings.GetSetting(Doc, "interface", vbNullString)
    Setting_Icon_Show = Settings.GetSetting(Doc, "showicon", True)

    Setting_Text_Show = Settings.GetSetting(Doc, "showtext", True)
    Setting_Text_ShowDownloadSpeed = Settings.GetSetting(Doc, "showtextdownloadspeed", True)
    Setting_Text_ShowDownloadTotal = Settings.GetSetting(Doc, "showtextdownloadtotal", False)
    Setting_Text_ShowUploadSpeed = Settings.GetSetting(Doc, "showtextuploadspeed", True)
    Setting_Text_ShowUploadTotal = Settings.GetSetting(Doc, "showtextuploadtotal", False)
    Setting_Text_SpeedDisplayUnit = Settings.GetSetting(Doc, "textspeeddisplayunit", DisplayUnit.Kilobytes)
    Setting_Text_TotalDisplayUnit = Settings.GetSetting(Doc, "texttotaldisplayunit", DisplayUnit.Megabytes)
    Setting_Text_Orientation = Settings.GetSetting(Doc, "textorientation", 0)
    Setting_Text_ShowExternalIP = Settings.GetSetting(Doc, "showtextexternalip", False)
    Setting_Text_ShowInternalIP = Settings.GetSetting(Doc, "showtextinternalip", False)

    Setting_Tooltip_ShowDownloadSpeed = Settings.GetSetting(Doc, "showtooltipdownloadspeed", True)
    Setting_Tooltip_ShowDownloadTotal = Settings.GetSetting(Doc, "showtooltipdownloadtotal", True)
    Setting_Tooltip_ShowDownloadAvg = Settings.GetSetting(Doc, "showtooltipdownloadavg", True)
    Setting_Tooltip_ShowDownloadMax = Settings.GetSetting(Doc, "showtooltipdownloadmax", True)
    Setting_Tooltip_ShowUploadSpeed = Settings.GetSetting(Doc, "showtooltipuploadspeed", True)
    Setting_Tooltip_ShowUploadTotal = Settings.GetSetting(Doc, "showtooltipuploadtotal", True)
    Setting_Tooltip_ShowUploadAvg = Settings.GetSetting(Doc, "showtooltipuploadavg", True)
    Setting_Tooltip_ShowUploadMax = Settings.GetSetting(Doc, "showtooltipuploadmax", True)
    Setting_Tooltip_SpeedDisplayUnit = Settings.GetSetting(Doc, "tooltipspeeddisplayunit", DisplayUnit.Kilobytes)
    Setting_Tooltip_TotalDisplayUnit = Settings.GetSetting(Doc, "tooltiptotaldisplayunit", DisplayUnit.Megabytes)
    Setting_Tooltip_ShowExternalIP = Settings.GetSetting(Doc, "showtooltipexternalip", True)
    Setting_Tooltip_ShowInternalIP = Settings.GetSetting(Doc, "showtooltipinternalip", True)

    Setting_Graph_Show = Settings.GetSetting(Doc, "showgraph", False)
    Setting_Graph_DownloadLineColor = Color.FromArgb(Settings.GetSetting(Doc, "graphdownloadlinecolor", Color.Green.ToArgb))
    Setting_Graph_UploadLineColor = Color.FromArgb(Settings.GetSetting(Doc, "graphuploadlinecolor", Color.Red.ToArgb))
    Setting_Graph_DisplayMode = Settings.GetSetting(Doc, "graphdisplaymode", 2)
    Setting_Graph_UpdateTime = Settings.GetSetting(Doc, "graphupdatetime", 1)
    Setting_Graph_UpdateTimeMode = Settings.GetSetting(Doc, "graphupdatetimemode", 1)
    Setting_Graph_UseStaticMaxValues = Settings.GetSetting(Doc, "graphusestaticmaxvalues", 0)
    Setting_Graph_StaticMaxDown = Settings.GetSetting(Doc, "graphstaticmaxdown", 2000)
    Setting_Graph_StaticMaxUp = Settings.GetSetting(Doc, "graphstaticmaxup", 200)

    Setting_SaveAverageSpeeds = Settings.GetSetting(Doc, "saveaveragespeeds", True)
    If Setting_SaveAverageSpeeds Then
      m_DownloadAvgTotal = Settings.GetSetting(Doc, "downloadavgtotal", 0)
      m_DownloadAvgCount = Settings.GetSetting(Doc, "downloadavgcount", 0)
      m_UploadAvgTotal = Settings.GetSetting(Doc, "uploadavgtotal", 0)
      m_UploadAvgCount = Settings.GetSetting(Doc, "uploadavgtotal", 0)
    End If

    Setting_SaveMaxSpeeds = Settings.GetSetting(Doc, "savemaxspeeds", True)
    If Setting_SaveMaxSpeeds Then
      m_DownloadMax = Settings.GetSetting(Doc, "downloadmax", 0)
      m_UploadMax = Settings.GetSetting(Doc, "uploadmax", 0)
    End If

    Setting_SaveTransferTotals = Settings.GetSetting(Doc, "savetransfertotals", True)
    If Setting_SaveTransferTotals Then
      m_DownloadTotal = Settings.GetSetting(Doc, "downloadtotal", 0)
      m_UploadTotal = Settings.GetSetting(Doc, "uploadtotal", 0)
    End If

    InitNetworkUsage()
    ResetSettings()
  End Sub

  Public Overrides Sub ResetSettings()
    m_SettingsDialog.comInterface.Text = Setting_Interface
    m_SettingsDialog.chkShowIcon.Checked = Setting_Icon_Show

    m_SettingsDialog.chkShowText.Checked = Setting_Text_Show
    m_SettingsDialog.chkTextDownloadSpeed.Checked = Setting_Text_ShowDownloadSpeed
    m_SettingsDialog.chkTextDownloadTotal.Checked = Setting_Text_ShowDownloadTotal
    m_SettingsDialog.chkTextUploadSpeed.Checked = Setting_Text_ShowUploadSpeed
    m_SettingsDialog.chkTextUploadTotal.Checked = Setting_Text_ShowUploadTotal
    m_SettingsDialog.comTextSpeedDisplayUnit.SelectedIndex = Setting_Text_SpeedDisplayUnit
    m_SettingsDialog.comTextTotalDisplayUnit.SelectedIndex = Setting_Text_TotalDisplayUnit
    m_SettingsDialog.radTextHorz.Checked = (Setting_Text_Orientation = 0)
    m_SettingsDialog.radTextVert.Checked = (Setting_Text_Orientation = 1)
    m_SettingsDialog.chkToolbarExternalIP.Checked = Setting_Text_ShowExternalIP
    m_SettingsDialog.chkToolbarInternalIP.Checked = Setting_Text_ShowInternalIP

    m_SettingsDialog.chkTooltipDownloadSpeed.Checked = Setting_Tooltip_ShowDownloadSpeed
    m_SettingsDialog.chkTooltipDownloadTotal.Checked = Setting_Tooltip_ShowDownloadTotal
    m_SettingsDialog.chkTooltipDownloadAvg.Checked = Setting_Tooltip_ShowDownloadAvg
    m_SettingsDialog.chkTooltipDownloadMax.Checked = Setting_Tooltip_ShowDownloadMax
    m_SettingsDialog.chkTooltipUploadSpeed.Checked = Setting_Tooltip_ShowUploadSpeed
    m_SettingsDialog.chkTooltipUploadTotal.Checked = Setting_Tooltip_ShowUploadTotal
    m_SettingsDialog.chkTooltipUploadAvg.Checked = Setting_Tooltip_ShowUploadAvg
    m_SettingsDialog.chkTooltipUploadMax.Checked = Setting_Tooltip_ShowUploadMax
    m_SettingsDialog.comTooltipSpeedDisplayUnit.SelectedIndex = Setting_Tooltip_SpeedDisplayUnit
    m_SettingsDialog.comTooltipTotalDisplayUnit.SelectedIndex = Setting_Tooltip_TotalDisplayUnit
    m_SettingsDialog.chkTooltipExternalIP.Checked = Setting_Tooltip_ShowExternalIP
    m_SettingsDialog.chkTooltipInternalIP.Checked = Setting_Tooltip_ShowInternalIP

    m_SettingsDialog.chkShowGraph.Checked = Setting_Graph_Show
    m_SettingsDialog.comGraphDisplay.SelectedIndex = Setting_Graph_DisplayMode
    m_SettingsDialog.picGraphDownloadColor.BackColor = Setting_Graph_DownloadLineColor
    m_SettingsDialog.picGraphUploadColor.BackColor = Setting_Graph_UploadLineColor

    If Setting_Graph_UpdateTime < m_SettingsDialog.nudGraphUpdateTime.Minimum Then Setting_Graph_UpdateTime = m_SettingsDialog.nudGraphUpdateTime.Minimum
    If Setting_Graph_UpdateTime > m_SettingsDialog.nudGraphUpdateTime.Maximum Then Setting_Graph_UpdateTime = m_SettingsDialog.nudGraphUpdateTime.Maximum
    m_SettingsDialog.nudGraphUpdateTime.Value = Setting_Graph_UpdateTime

    m_SettingsDialog.comGraphUpdateTime.SelectedIndex = Setting_Graph_UpdateTimeMode

    m_SettingsDialog.chkStaticMaxValues.Checked = Setting_Graph_UseStaticMaxValues
    m_SettingsDialog.txtGraphStaticMaxDown.Text = Setting_Graph_StaticMaxDown
    m_SettingsDialog.txtGraphStaticMaxUp.Text = Setting_Graph_StaticMaxUp
    m_SettingsDialog.lblStaticMaxDown.Enabled = m_SettingsDialog.chkStaticMaxValues.Checked
    m_SettingsDialog.txtGraphStaticMaxDown.Enabled = m_SettingsDialog.chkStaticMaxValues.Checked
    m_SettingsDialog.lblStaticMaxDownKbs.Enabled = m_SettingsDialog.chkStaticMaxValues.Checked
    m_SettingsDialog.lblStaticMaxUp.Enabled = m_SettingsDialog.chkStaticMaxValues.Checked
    m_SettingsDialog.txtGraphStaticMaxUp.Enabled = m_SettingsDialog.chkStaticMaxValues.Checked
    m_SettingsDialog.lblStaticMaxUpKbs.Enabled = m_SettingsDialog.chkStaticMaxValues.Checked

    m_SettingsDialog.chkSaveAverageSpeeds.Checked = Setting_SaveAverageSpeeds
    m_SettingsDialog.chkResetAvgSpeeds.Checked = False
    m_SettingsDialog.chkSaveMaxSpeeds.Checked = Setting_SaveMaxSpeeds
    m_SettingsDialog.chkResetMaxSpeeds.Checked = False
    m_SettingsDialog.chkSaveTransferTotals.Checked = Setting_SaveTransferTotals
    m_SettingsDialog.chkResetTransferTotals.Checked = False
  End Sub

  ' InfoBar will call this when the application exits. Use this to save your internal settings.
  Public Overrides Sub SaveSettings(ByRef Doc As XmlDocument)
    Settings.SaveSetting(Doc, "interface", Setting_Interface)
    Settings.SaveSetting(Doc, "showicon", Setting_Icon_Show)

    Settings.SaveSetting(Doc, "showtext", Setting_Text_Show)
    Settings.SaveSetting(Doc, "showtextdownloadspeed", Setting_Text_ShowDownloadSpeed)
    Settings.SaveSetting(Doc, "showtextdownloadtotal", Setting_Text_ShowDownloadTotal)
    Settings.SaveSetting(Doc, "showtextuploadspeed", Setting_Text_ShowUploadSpeed)
    Settings.SaveSetting(Doc, "showtextuploadtotal", Setting_Text_ShowUploadTotal)
    Settings.SaveSetting(Doc, "textspeeddisplayunit", Setting_Text_SpeedDisplayUnit)
    Settings.SaveSetting(Doc, "texttotaldisplayunit", Setting_Text_TotalDisplayUnit)
    Settings.SaveSetting(Doc, "textorientation", Setting_Text_Orientation)
    Settings.SaveSetting(Doc, "showtextexternalip", Setting_Text_ShowExternalIP)
    Settings.SaveSetting(Doc, "showtextinternalip", Setting_Text_ShowInternalIP)

    Settings.SaveSetting(Doc, "showtooltipdownloadspeed", Setting_Tooltip_ShowDownloadSpeed)
    Settings.SaveSetting(Doc, "showtooltipdownloadtotal", Setting_Tooltip_ShowDownloadTotal)
    Settings.SaveSetting(Doc, "showtooltipdownloadavg", Setting_Tooltip_ShowDownloadAvg)
    Settings.SaveSetting(Doc, "showtooltipdownloadmax", Setting_Tooltip_ShowDownloadMax)
    Settings.SaveSetting(Doc, "showtooltipuploadspeed", Setting_Tooltip_ShowUploadSpeed)
    Settings.SaveSetting(Doc, "showtooltipuploadtotal", Setting_Tooltip_ShowUploadTotal)
    Settings.SaveSetting(Doc, "showtooltipuploadavg", Setting_Tooltip_ShowUploadAvg)
    Settings.SaveSetting(Doc, "showtooltipuploadmax", Setting_Tooltip_ShowUploadMax)
    Settings.SaveSetting(Doc, "tooltipspeeddisplayunit", Setting_Tooltip_SpeedDisplayUnit)
    Settings.SaveSetting(Doc, "tooltiptotaldisplayunit", Setting_Tooltip_TotalDisplayUnit)
    Settings.SaveSetting(Doc, "showtooltipexternalip", Setting_Tooltip_ShowExternalIP)
    Settings.SaveSetting(Doc, "showtooltipinternalip", Setting_Tooltip_ShowInternalIP)

    Settings.SaveSetting(Doc, "showgraph", Setting_Graph_Show)
    Settings.SaveSetting(Doc, "graphdownloadlinecolor", Setting_Graph_DownloadLineColor.ToArgb)
    Settings.SaveSetting(Doc, "graphuploadlinecolor", Setting_Graph_UploadLineColor.ToArgb)
    Settings.SaveSetting(Doc, "graphdisplaymode", Setting_Graph_DisplayMode)
    Settings.SaveSetting(Doc, "graphupdatetime", Setting_Graph_UpdateTime)
    Settings.SaveSetting(Doc, "graphupdatetimemode", Setting_Graph_UpdateTimeMode)
    Settings.SaveSetting(Doc, "graphusestaticmaxvalues", Setting_Graph_UseStaticMaxValues)
    Settings.SaveSetting(Doc, "graphstaticmaxdown", Setting_Graph_StaticMaxDown)
    Settings.SaveSetting(Doc, "graphstaticmaxup", Setting_Graph_StaticMaxUp)

    Settings.SaveSetting(Doc, "saveaveragespeeds", Setting_SaveAverageSpeeds)
    If Setting_SaveAverageSpeeds Then
      Settings.SaveSetting(Doc, "downloadavgtotal", m_DownloadAvgTotal)
      Settings.SaveSetting(Doc, "downloadavgcount", m_DownloadAvgCount)
      Settings.SaveSetting(Doc, "uploadavgtotal", m_UploadAvgTotal)
      Settings.SaveSetting(Doc, "uploadavgcount", m_UploadAvgCount)
    End If

    Settings.SaveSetting(Doc, "savemaxspeeds", Setting_SaveMaxSpeeds)
    If Setting_SaveMaxSpeeds Then
      Settings.SaveSetting(Doc, "uploadmax", m_UploadMax)
      Settings.SaveSetting(Doc, "uploadtotal", m_UploadTotal)
    End If

    Settings.SaveSetting(Doc, "savetransfertotals", Setting_SaveTransferTotals)
    If Setting_SaveTransferTotals Then
      Settings.SaveSetting(Doc, "downloadmax", m_DownloadMax)
      Settings.SaveSetting(Doc, "downloadtotal", m_DownloadTotal)
    End If
  End Sub

#End Region

#Region "Network Usage Routines"

  Private Sub InitNetworkUsage()
    Dim I As Integer

    m_DownloadGraphHistory.Clear()
    For I = 0 To 99
      m_DownloadGraphHistory.Add(0)
    Next

    m_UploadGraphHistory.Clear()
    For I = 0 To 99
      m_UploadGraphHistory.Add(0)
    Next

    Dim mHaveOldSetting As Boolean = False
    m_SettingsDialog.comInterface.Items.Clear()
    For Each ni As NetworkInterface In NetworkInterface.GetAllNetworkInterfaces
      m_SettingsDialog.comInterface.Items.Add(ni.Description)
      If ni.Description = Setting_Interface Then
        m_Interface = ni
        mHaveOldSetting = True
      End If
    Next
    If mHaveOldSetting = False Then
      Dim nitemp As NetworkInterface = NetworkInterface.GetAllNetworkInterfaces(0)
      Setting_Interface = nitemp.Description
      m_Interface = nitemp
    End If

    m_SettingsDialog.comInterface.Text = Setting_Interface
  End Sub

  Private Sub UninitNetworkUsage()
    m_Interface = Nothing
  End Sub

  Private Function UpdateNetworkUsage() As Boolean
    Dim bWindowIsDirty As Boolean
    Dim sErrors As String = vbNullString

    Dim iDownloadTotal As Long
    Try
      iDownloadTotal = m_Interface.GetIPv4Statistics.BytesReceived
      If iDownloadTotal > 0 Then
        If m_DownloadPrevTotal > 0 Then
          m_DownloadSpeed = iDownloadTotal - m_DownloadPrevTotal
          If m_DownloadSpeed > m_Interface.Speed Then m_DownloadSpeed = 0
          If m_DownloadSpeed < 0 Then m_DownloadSpeed = 0
          m_DownloadPrevTotal = iDownloadTotal
          If m_DownloadSpeed > m_DownloadMax Then m_DownloadMax = m_DownloadSpeed
          m_DownloadAvgTotal += m_DownloadSpeed
          m_DownloadAvgCount += 1
          If m_DownloadAvgTotal > 0 Then
            m_DownloadAvg = m_DownloadAvgTotal / m_DownloadAvgCount
          Else
            m_DownloadAvg = 0
          End If
          m_DownloadTotal += m_DownloadSpeed
        Else
          m_DownloadPrevTotal = iDownloadTotal
        End If
      End If
    Catch ex As Exception
      sErrors &= ex.ToString & vbCrLf & vbCrLf
    End Try

    Dim iUploadTotal As Long
    Try
      iUploadTotal = m_Interface.GetIPv4Statistics.BytesSent
      If iUploadTotal > 0 Then
        If m_UploadPrevTotal > 0 Then
          m_UploadSpeed = iUploadTotal - m_UploadPrevTotal
          If m_UploadSpeed > m_Interface.Speed Then m_UploadSpeed = 0
          If m_UploadSpeed < 0 Then m_UploadSpeed = 0
          m_UploadPrevTotal = iUploadTotal
          If m_UploadSpeed > m_UploadMax Then m_UploadMax = m_UploadSpeed
          m_UploadAvgTotal += m_UploadSpeed
          m_UploadAvgCount += 1
          If m_UploadAvgTotal > 0 Then
            m_UploadAvg = m_UploadAvgTotal / m_UploadAvgCount
          Else
            m_UploadAvg = 0
          End If
          m_UploadTotal += m_UploadSpeed
        Else
          m_UploadPrevTotal = iUploadTotal
        End If
      End If
    Catch ex As Exception
      sErrors &= ex.ToString & vbCrLf & vbCrLf
    End Try

    If Setting_Text_ShowExternalIP Or Setting_Tooltip_ShowExternalIP Then
      If m_ExternalIPAddressSecondsSinceLastRequest >= 500 Then
        Dim matches As MatchCollection = Nothing
        Try
          Dim wc As WebClient = New WebClient()
          Dim str As String = wc.DownloadString("http://www.ipchicken.com/")
          Dim pattern As String = "\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3}"
          matches = Regex.Matches(str, pattern)
          m_ExternalIPAddress = matches(0).Value.ToString
          m_ExternalIPAddressSecondsSinceLastRequest = 0
        Catch ex As Exception
          m_ExternalIPAddress = "(Unknown)"
          'm_ExternalIPAddressSecondsSinceLastRequest = 494
        End Try
      End If
      m_ExternalIPAddressSecondsSinceLastRequest += 1
    End If

    If Setting_Text_ShowInternalIP Or Setting_Tooltip_ShowInternalIP Then
      Dim sIPs As String = vbNullString
      For Each ip As UnicastIPAddressInformation In m_Interface.GetIPProperties.UnicastAddresses
        If ip.IsTransient = False AndAlso ip.Address.IsIPv6LinkLocal = False AndAlso ip.Address.IsIPv6Multicast = False AndAlso ip.Address.IsIPv6SiteLocal = False Then
          sIPs &= ip.Address.ToString & ", "
        End If
      Next
      If sIPs.EndsWith(", ") Then sIPs = sIPs.Substring(0, sIPs.Length - 2)
      m_InternalIPAddress = sIPs
    End If

    Dim sText As String = vbNullString

    If Setting_Icon_Show Then
      If m_DownloadSpeed > 0 AndAlso m_UploadSpeed > 0 Then
        If IconTheme.IsDefaultTheme = True Then
          m_Icon = My.Resources.both
        Else
          m_Icon = IconTheme.GetThemeIcon(InfoBarModuleGUID, "Both")
          If m_Icon Is Nothing Then m_Icon = My.Resources.both
        End If

      ElseIf m_DownloadSpeed > 0 Then
        If IconTheme.IsDefaultTheme = True Then
          m_Icon = My.Resources.download
        Else
          m_Icon = IconTheme.GetThemeIcon(InfoBarModuleGUID, "Download")
          If m_Icon Is Nothing Then m_Icon = My.Resources.download
        End If

      ElseIf m_UploadSpeed > 0 Then
        If IconTheme.IsDefaultTheme = True Then
          m_Icon = My.Resources.upload
        Else
          m_Icon = IconTheme.GetThemeIcon(InfoBarModuleGUID, "Upload")
          If m_Icon Is Nothing Then m_Icon = My.Resources.upload
        End If

      Else
        If IconTheme.IsDefaultTheme = True Then
          m_Icon = My.Resources.icon
        Else
          m_Icon = IconTheme.GetThemeIcon(InfoBarModuleGUID, "Network")
          If m_Icon Is Nothing Then m_Icon = My.Resources.icon
        End If

      End If
    End If

    sText = vbNullString
    If Setting_Text_Show Then
      If Setting_Text_ShowDownloadSpeed Or Setting_Text_ShowDownloadTotal Then sText = "D: "
      If Setting_Text_ShowDownloadSpeed AndAlso Setting_Text_ShowDownloadTotal Then
        sText &= Utilities.FormatTransferRate(m_DownloadSpeed, Setting_Text_SpeedDisplayUnit) & " (" & Utilities.FormatFileSize(m_DownloadTotal, Setting_Text_TotalDisplayUnit) & ")"
      ElseIf Setting_Text_ShowDownloadSpeed Then
        sText &= Utilities.FormatTransferRate(m_DownloadSpeed, Setting_Text_SpeedDisplayUnit)
      ElseIf Setting_Text_ShowDownloadTotal Then
        sText &= Utilities.FormatFileSize(m_DownloadTotal, Setting_Text_TotalDisplayUnit)
      End If

      If Setting_Text_ShowExternalIP Then sText &= " Ext IP: " & m_ExternalIPAddress

      If sText <> vbNullString Then
        If Setting_Text_Orientation = 0 Then
          sText &= " "
        Else
          sText &= vbCrLf
        End If
      End If

      If Setting_Text_ShowUploadSpeed Or Setting_Text_ShowUploadTotal Then sText &= "U: "
      If Setting_Text_ShowUploadSpeed AndAlso Setting_Text_ShowUploadTotal Then
        sText &= Utilities.FormatTransferRate(m_UploadSpeed, Setting_Text_SpeedDisplayUnit) & " (" & Utilities.FormatFileSize(m_UploadTotal, Setting_Text_TotalDisplayUnit) & ")"
      ElseIf Setting_Text_ShowUploadSpeed Then
        sText &= Utilities.FormatTransferRate(m_UploadSpeed, Setting_Text_SpeedDisplayUnit)
      ElseIf Setting_Text_ShowUploadTotal Then
        sText &= Utilities.FormatFileSize(m_UploadTotal, Setting_Text_TotalDisplayUnit)
      End If

      If Setting_Text_ShowInternalIP Then sText &= " Int IP: " & m_InternalIPAddress
    End If

    If m_Text <> sText Then
      bWindowIsDirty = True
      m_Text = sText
    End If

    Dim sTooltipText As String = "Network Usage: "
    If Setting_Tooltip_ShowDownloadSpeed Or Setting_Tooltip_ShowDownloadAvg Or Setting_Tooltip_ShowDownloadMax Or Setting_Tooltip_ShowDownloadTotal Then sTooltipText &= vbCrLf & vbCrLf & "Download:"
    If Setting_Tooltip_ShowDownloadSpeed Then sTooltipText &= vbCrLf & "Speed: " & Utilities.FormatTransferRate(m_DownloadSpeed, Setting_Tooltip_SpeedDisplayUnit)
    If Setting_Tooltip_ShowDownloadAvg Then sTooltipText &= vbCrLf & "Average Speed: " & Utilities.FormatTransferRate(m_DownloadAvg, Setting_Tooltip_SpeedDisplayUnit)
    If Setting_Tooltip_ShowDownloadMax Then sTooltipText &= vbCrLf & "Maximum Speed: " & Utilities.FormatTransferRate(m_DownloadMax, Setting_Tooltip_SpeedDisplayUnit)
    If Setting_Tooltip_ShowDownloadTotal Then sTooltipText &= vbCrLf & "Total Downloaded: " & Utilities.FormatFileSize(m_DownloadTotal, Setting_Tooltip_TotalDisplayUnit)
    If Setting_Tooltip_ShowUploadSpeed Or Setting_Tooltip_ShowUploadAvg Or Setting_Tooltip_ShowUploadMax Or Setting_Tooltip_ShowUploadTotal Then sTooltipText &= vbCrLf & vbCrLf & "Upload:"
    If Setting_Tooltip_ShowUploadSpeed Then sTooltipText &= vbCrLf & "Speed: " & Utilities.FormatTransferRate(m_UploadSpeed, Setting_Tooltip_SpeedDisplayUnit)
    If Setting_Tooltip_ShowUploadAvg Then sTooltipText &= vbCrLf & "Average Speed: " & Utilities.FormatTransferRate(m_UploadAvg, Setting_Tooltip_SpeedDisplayUnit)
    If Setting_Tooltip_ShowUploadMax Then sTooltipText &= vbCrLf & "Maximum Speed: " & Utilities.FormatTransferRate(m_UploadMax, Setting_Tooltip_SpeedDisplayUnit)
    If Setting_Tooltip_ShowDownloadTotal Then sTooltipText &= vbCrLf & "Total Uploaded: " & Utilities.FormatFileSize(m_UploadTotal, Setting_Tooltip_TotalDisplayUnit)
    If Setting_Tooltip_ShowExternalIP Or Setting_Tooltip_ShowInternalIP Then sTooltipText &= vbCrLf
    If Setting_Tooltip_ShowExternalIP Then sTooltipText &= vbCrLf & "External IP Address: " & m_ExternalIPAddress
    If Setting_Tooltip_ShowInternalIP Then sTooltipText &= vbCrLf & "Internal IP Address: " & m_InternalIPAddress

    If sErrors <> vbNullString Then
      If sTooltipText <> vbNullString Then sTooltipText &= vbCrLf & vbCrLf
      sTooltipText &= sErrors
    End If

    If m_TooltipText <> sTooltipText Then
      m_TooltipText = sTooltipText
      If Tooltip.GetTooltipOwnerGUID = InfoBarModuleGUID AndAlso Tooltip.GetTooltipOwnerObjectID = "NetworkUsage" Then Tooltip.UpdateTooltip()
    End If

    If Setting_Graph_Show = True Then
      Dim bUpdateNow As Boolean = False
      Dim iDownloadFinalVal As Integer, iUploadFinalVal As Integer
      Select Case Setting_Graph_UpdateTimeMode
        Case TimeMode.Seconds
          If Setting_Graph_UpdateTime > 1 Then
            If m_DownloadGraphAvgCount = Setting_Graph_UpdateTime Then
              iDownloadFinalVal = m_DownloadGraphAvgVals / m_DownloadGraphAvgCount
              m_DownloadGraphAvgVals = 0
              m_DownloadGraphAvgCount = 0
              bUpdateNow = True
            Else
              m_DownloadGraphAvgVals += m_DownloadSpeed
              m_DownloadGraphAvgCount += 1
            End If

            If m_UploadGraphAvgCount = Setting_Graph_UpdateTime Then
              iUploadFinalVal = m_UploadGraphAvgVals / m_UploadGraphAvgCount
              m_UploadGraphAvgVals = 0
              m_UploadGraphAvgCount = 0
              bUpdateNow = True
            Else
              m_UploadGraphAvgVals += m_UploadSpeed
              m_UploadGraphAvgCount += 1
            End If
          Else
            iDownloadFinalVal = m_DownloadSpeed
            iUploadFinalVal = m_UploadSpeed
            bUpdateNow = True
          End If

        Case TimeMode.Minutes
          If m_DownloadGraphAvgCount = (Setting_Graph_UpdateTime * 60) Then
            iDownloadFinalVal = m_DownloadGraphAvgVals / m_DownloadGraphAvgCount
            m_DownloadGraphAvgVals = 0
            m_DownloadGraphAvgCount = 0
            bUpdateNow = True
          Else
            m_DownloadGraphAvgVals += m_DownloadSpeed
            m_DownloadGraphAvgCount += 1
          End If

          If m_UploadGraphAvgCount = (Setting_Graph_UpdateTime * 60) Then
            iUploadFinalVal = m_UploadGraphAvgVals / m_UploadGraphAvgCount
            m_UploadGraphAvgVals = 0
            m_UploadGraphAvgCount = 0
            bUpdateNow = True
          Else
            m_UploadGraphAvgVals += m_UploadSpeed
            m_UploadGraphAvgCount += 1
          End If

        Case TimeMode.Hours
          If m_DownloadGraphAvgCount = (Setting_Graph_UpdateTime * 3600) Then
            iDownloadFinalVal = m_DownloadGraphAvgVals / m_DownloadGraphAvgCount
            m_DownloadGraphAvgVals = 0
            m_DownloadGraphAvgCount = 0
            bUpdateNow = True
          Else
            m_DownloadGraphAvgVals += m_DownloadSpeed
            m_DownloadGraphAvgCount += 1
          End If

          If m_UploadGraphAvgCount = (Setting_Graph_UpdateTime * 3600) Then
            iUploadFinalVal = m_UploadGraphAvgVals / m_UploadGraphAvgCount
            m_UploadGraphAvgVals = 0
            m_UploadGraphAvgCount = 0
            bUpdateNow = True
          Else
            m_UploadGraphAvgVals += m_UploadSpeed
            m_UploadGraphAvgCount += 1
          End If
      End Select

      If bUpdateNow = True Then
        m_DownloadGraphHistory.RemoveAt(0)
        m_DownloadGraphHistory.Add(iDownloadFinalVal)

        m_UploadGraphHistory.RemoveAt(0)
        m_UploadGraphHistory.Add(iUploadFinalVal)

        bWindowIsDirty = True
      End If
    End If

    Return bWindowIsDirty
  End Function

#End Region

End Class
