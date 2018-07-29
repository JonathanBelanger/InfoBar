Public Class InfoBarModuleMain
  ' We inherit the base class. We DO NOT return any of the base classes values.
  Inherits InfoBarModule

  ' Unique ID. If same as other module, the first module will be loaded, the next will be ignored.
  Private Const InfoBarModuleGUID As String = "{63183F82-B3D8-4ff0-BB0E-12A5B3F97B86}"

#Region "Private Classes"
  Private Class ProcessInfo
    Public ID As Integer
    Public Name As String
    Public Used As ULong
    Public Pct As Integer
    Public IsDirty As Boolean
  End Class
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
  Private m_ModuleToolbarBitmap As Bitmap
  Private m_ModuleTooltipBitmap As Bitmap
  Private m_SettingsDialog As New Settings
  Private m_Bounds As Rectangle
  Private m_TextLastWidth As Integer
  Private m_Enabled As Boolean
  Private m_Text As String
  Private m_TooltipText As String
  Private m_Processes As New Collection

  Private m_GraphHistory As New Collections.ArrayList(100)
  Private m_GraphAvgVals As ULong
  Private m_GraphAvgCount As UInteger
#End Region

#Region "Settings Variables"
  Friend Enum DisplayUnit
    KB
    MB
    GB
  End Enum

  Friend Enum TimeMode
    Seconds
    Minutes
    Hours
  End Enum

  Friend Setting_Icon_Show As Boolean

  Friend Setting_Text_Show As Boolean
  Friend Setting_Text_ShowFree As Boolean
  Friend Setting_Text_ShowPctFree As Boolean
  Friend Setting_Text_ShowUsed As Boolean
  Friend Setting_Text_ShowPctUsed As Boolean
  Friend Setting_Text_ShowTotal As Boolean
  Friend Setting_Text_DisplayUnit As DisplayUnit

  Friend Setting_Tooltip_ShowFree As Boolean
  Friend Setting_Tooltip_ShowPctFree As Boolean
  Friend Setting_Tooltip_ShowUsed As Boolean
  Friend Setting_Tooltip_ShowPctUsed As Boolean
  Friend Setting_Tooltip_ShowTotal As Boolean
  Friend Setting_Tooltip_ShowTopProcesses As Boolean
  Friend Setting_Tooltip_MaxTopProcesses As Integer
  Friend Setting_Tooltip_DisplayUnit As DisplayUnit

  Friend Setting_Graph_Show As Boolean
  Friend Setting_Graph_LineColor As Color
  Friend Setting_Graph_ShowFree As Boolean
  Friend Setting_Graph_UpdateTime As Integer
  Friend Setting_Graph_UpdateTimeMode As TimeMode
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
      Return "Displays information about your physical memory usage."
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
    m_GraphHistory.Clear()
    Dim I As Integer
    For I = 0 To 99
      m_GraphHistory.Add(0)
    Next

    UpdateMemoryUsage()
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

    Dim bDirty As Boolean = UpdateMemoryUsage()
    If bDirty Then
      DrawModule()
      bModuleIsDirty = True
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

    Dim ico As Image
    If IconTheme.IsDefaultTheme = True Then
      ico = My.Resources.icon
    Else
      ico = IconTheme.GetThemeIcon(InfoBarModuleGUID, "Memory")
      If ico Is Nothing Then ico = My.Resources.icon
    End If

    Dim grm As Graphics = Graphics.FromHwnd(IntPtr.Zero)

    If Setting_Icon_Show = True Then width += ico.Width

    If Setting_Text_Show = True Then
      If Setting_Icon_Show Then width += 2
      Dim tr As Rectangle = Skin.MeasureText(grm, m_Text, Skinning.SkinTextPart.BackgroundText)
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

    If width <= 1 Or height <= 1 Then Exit Sub
    Dim bmpTemp As New Bitmap(width, height, System.Drawing.Imaging.PixelFormat.Format32bppArgb)
    Dim GR As Graphics = Graphics.FromImage(bmpTemp)

    ' Get skin height for vertical centering
    Dim curX As Integer = 0

    If Setting_Icon_Show = True Then
      GR.DrawImage(ico, curX, CInt((height - ico.Height) / 2), ico.Width, ico.Height)
      curX += ico.Width
    End If

    If Setting_Text_Show = True Then
      If Setting_Icon_Show Then curX += 2
      Dim tr As Rectangle
      tr = Skin.MeasureText(Gr, m_Text, Skinning.SkinTextPart.BackgroundText)
      tr.X = curX
      tr.Y = 0
      tr.Height = height
      If tr.Width > m_TextLastWidth Then
        m_TextLastWidth = tr.Width
      Else
        tr.Width = m_TextLastWidth
      End If
      Skin.DrawText(Gr, m_Text, tr, Skinning.SkinTextPart.BackgroundText, StringAlignment.Near, StringAlignment.Center)
      curX += tr.Width
    End If

    If Setting_Graph_Show = True Then
      If Setting_Icon_Show Or Setting_Text_Show Then curX += 2
      Dim rg As Rectangle
      rg.X = curX
      rg.Y = 0
      rg.Width = 100
      rg.Height = height
      Skin.DrawGraphBackground(Gr, rg)

      Dim gcm As Skinning.SkinMargins = Skin.ContentMargins(Skinning.SkinMarginPart.Graph)

      rg.X = rg.X + gcm.Left
      rg.Y = rg.Y + gcm.Top
      rg.Width = rg.Width - (gcm.Left + gcm.Right)
      rg.Height = rg.Height - (gcm.Top + gcm.Bottom)

      ' Temorarily Change Rendering Settings For Blended Lines

      ' Save Old Settings
      Dim oldCompositingQuality As CompositingQuality = Gr.CompositingQuality
      Dim oldSmoothingMode As SmoothingMode = Gr.SmoothingMode

      ' Apply Perfered Settings
      Gr.CompositingQuality = CompositingQuality.HighQuality
      Gr.SmoothingMode = SmoothingMode.AntiAlias

      ' Draw Value Lines
      Dim gv As Integer
      Dim x1 As Integer, y1 As Integer, x2 As Integer, y2 As Integer
      Dim gvp As New Pen(Setting_Graph_LineColor)
      For gv = 0 To m_GraphHistory.Count - 2
        x1 = rg.X + ((gv / 100) * rg.Width)
        y1 = rg.Y + (rg.Height - ((m_GraphHistory(gv) / 100) * rg.Height))
        x2 = rg.X + (((gv + 1) / 100) * rg.Width)
        y2 = rg.Y + (rg.Height - ((m_GraphHistory(gv + 1) / 100) * rg.Height))
        Gr.DrawLine(gvp, x1, y1, x2, y2)
      Next
      gvp.Dispose()

      ' Reset Rendering Settings To Old Values
      Gr.CompositingQuality = oldCompositingQuality
      Gr.SmoothingMode = oldSmoothingMode

      curX += 100
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
      Case "MemoryUsage"
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

  Public Overrides Sub ProcessMouseMove(ByVal e As System.Windows.Forms.MouseEventArgs, ByRef bWindowIsDirty As Boolean)
    If m_Bounds.Contains(e.Location) Then
      DrawTooltip("MemoryUsage")
      Tooltip.SetTooltipOwner(InfoBarModuleGUID, "MemoryUsage")
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
    Dim ico As Image
    If IconTheme.IsDefaultTheme = True Then
      ico = My.Resources.icon
    Else
      ico = IconTheme.GetThemeIcon(InfoBarModuleGUID, "CPU")
      If ico Is Nothing Then ico = My.Resources.icon
    End If
    MainMenu.AddMenuItem(InfoBarModuleGUID & "::" & "SETTINGS", "Memory Usage Settings...", ico, True, True)
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
    Setting_Text_Show = m_SettingsDialog.chkShowText.Checked
    Setting_Graph_Show = m_SettingsDialog.chkShowGraph.Checked

    Setting_Text_ShowFree = m_SettingsDialog.chkTextFree.Checked
    Setting_Text_ShowPctFree = m_SettingsDialog.chkTextPctFree.Checked
    Setting_Text_ShowUsed = m_SettingsDialog.chkTextUsed.Checked
    Setting_Text_ShowPctUsed = m_SettingsDialog.chkTextPctUsed.Checked
    Setting_Text_ShowTotal = m_SettingsDialog.chkTextTotal.Checked
    If m_SettingsDialog.radTextDisplayUnitKB.Checked Then
      Setting_Text_DisplayUnit = DisplayUnit.KB
    ElseIf m_SettingsDialog.radTextDisplayUnitMB.Checked Then
      Setting_Text_DisplayUnit = DisplayUnit.MB
    Else
      Setting_Text_DisplayUnit = DisplayUnit.GB
    End If

    Setting_Tooltip_ShowFree = m_SettingsDialog.chkTooltipFree.Checked
    Setting_Tooltip_ShowPctFree = m_SettingsDialog.chkTooltipPctFree.Checked
    Setting_Tooltip_ShowUsed = m_SettingsDialog.chkTooltipUsed.Checked
    Setting_Tooltip_ShowPctUsed = m_SettingsDialog.chkTooltipPctUsed.Checked
    Setting_Tooltip_ShowTotal = m_SettingsDialog.chkTooltipTotal.Checked
    If m_SettingsDialog.radTooltipDisplayUnitKB.Checked Then
      Setting_Tooltip_DisplayUnit = DisplayUnit.KB
    ElseIf m_SettingsDialog.radTooltipDisplayUnitMB.Checked Then
      Setting_Tooltip_DisplayUnit = DisplayUnit.MB
    Else
      Setting_Tooltip_DisplayUnit = DisplayUnit.GB
    End If

    Setting_Graph_LineColor = m_SettingsDialog.picGraphLineColor.BackColor

    If m_SettingsDialog.radGraphDisplayFree.Checked <> Setting_Graph_ShowFree OrElse _
    m_SettingsDialog.nudGraphUpdateTime.Value <> Setting_Graph_UpdateTime OrElse _
    m_SettingsDialog.comGraphUpdateTime.SelectedIndex <> Setting_Graph_UpdateTimeMode Then
      m_GraphAvgVals = 0
      m_GraphAvgCount = 0
      m_GraphHistory.Clear()
      Dim I As Integer
      For I = 0 To 99
        m_GraphHistory.Add(0)
      Next
    End If

    Setting_Graph_ShowFree = m_SettingsDialog.radGraphDisplayFree.Checked
    Setting_Graph_UpdateTime = m_SettingsDialog.nudGraphUpdateTime.Value
    Setting_Graph_UpdateTimeMode = m_SettingsDialog.comGraphUpdateTime.SelectedIndex

    Setting_Tooltip_ShowTopProcesses = m_SettingsDialog.chkShowTopProcesses.Checked
    Setting_Tooltip_MaxTopProcesses = m_SettingsDialog.nudTopProcesses.Value

    DrawModule()
  End Sub

  ' InfoBar will call this when the application loads. Use this to load your internal settings.
  Public Overrides Sub LoadSettings(ByRef Doc As XmlDocument)
    Setting_Icon_Show = Settings.GetSetting(Doc, "showicon", True)
    Setting_Text_Show = Settings.GetSetting(Doc, "showtext", True)
    Setting_Graph_Show = Settings.GetSetting(Doc, "showgraph", False)

    Setting_Text_ShowFree = Settings.GetSetting(Doc, "showtextfree", False)
    Setting_Text_ShowPctFree = Settings.GetSetting(Doc, "showtextpctfree", False)
    Setting_Text_ShowUsed = Settings.GetSetting(Doc, "showtextused", False)
    Setting_Text_ShowPctUsed = Settings.GetSetting(Doc, "showtextpctused", True)
    Setting_Text_ShowTotal = Settings.GetSetting(Doc, "showtexttotal", False)
    Setting_Text_DisplayUnit = Settings.GetSetting(Doc, "textdisplayunit", DisplayUnit.MB)

    Setting_Tooltip_ShowFree = Settings.GetSetting(Doc, "showtooltipfree", True)
    Setting_Tooltip_ShowPctFree = Settings.GetSetting(Doc, "showtooltippctfree", True)
    Setting_Tooltip_ShowUsed = Settings.GetSetting(Doc, "showtooltipused", True)
    Setting_Tooltip_ShowPctUsed = Settings.GetSetting(Doc, "showtooltippctused", True)
    Setting_Tooltip_ShowTotal = Settings.GetSetting(Doc, "showtooltiptotal", True)
    Setting_Tooltip_DisplayUnit = Settings.GetSetting(Doc, "tooltipdisplayunit", DisplayUnit.MB)

    Setting_Graph_LineColor = Color.FromArgb(Settings.GetSetting(Doc, "graphcolor", Color.Green.ToArgb))
    Setting_Graph_ShowFree = Settings.GetSetting(Doc, "showgraphfree", False)
    Setting_Graph_UpdateTime = Settings.GetSetting(Doc, "graphupdatetime", 1)
    Setting_Graph_UpdateTimeMode = Settings.GetSetting(Doc, "graphupdatetimemode", 1)

    Setting_Tooltip_ShowTopProcesses = Settings.GetSetting(Doc, "showtopprocesses", True)
    Setting_Tooltip_MaxTopProcesses = Settings.GetSetting(Doc, "numberoftopprocessestoshow", 10)

    ResetSettings()
  End Sub

  Public Overrides Sub ResetSettings()
    m_SettingsDialog.chkShowIcon.Checked = Setting_Icon_Show
    m_SettingsDialog.chkShowText.Checked = Setting_Text_Show
    m_SettingsDialog.chkShowGraph.Checked = Setting_Graph_Show
    m_SettingsDialog.chkTextFree.Checked = Setting_Text_ShowFree
    m_SettingsDialog.chkTextPctFree.Checked = Setting_Text_ShowPctFree
    m_SettingsDialog.chkTextUsed.Checked = Setting_Text_ShowUsed
    m_SettingsDialog.chkTextPctUsed.Checked = Setting_Text_ShowPctUsed
    m_SettingsDialog.chkTextTotal.Checked = Setting_Text_ShowTotal
    Select Case Setting_Text_DisplayUnit
      Case DisplayUnit.KB
        m_SettingsDialog.radTextDisplayUnitKB.Checked = True
      Case DisplayUnit.MB
        m_SettingsDialog.radTextDisplayUnitMB.Checked = True
      Case DisplayUnit.GB
        m_SettingsDialog.radTextDisplayUnitGB.Checked = True
    End Select
    m_SettingsDialog.chkTooltipFree.Checked = Setting_Tooltip_ShowFree
    m_SettingsDialog.chkTooltipPctFree.Checked = Setting_Tooltip_ShowPctFree
    m_SettingsDialog.chkTooltipUsed.Checked = Setting_Tooltip_ShowUsed
    m_SettingsDialog.chkTooltipPctUsed.Checked = Setting_Tooltip_ShowPctUsed
    m_SettingsDialog.chkTooltipTotal.Checked = Setting_Tooltip_ShowTotal
    Select Case Setting_Tooltip_DisplayUnit
      Case DisplayUnit.KB
        m_SettingsDialog.radTooltipDisplayUnitKB.Checked = True
      Case DisplayUnit.MB
        m_SettingsDialog.radTooltipDisplayUnitMB.Checked = True
      Case DisplayUnit.GB
        m_SettingsDialog.radTooltipDisplayUnitGB.Checked = True
    End Select
    m_SettingsDialog.picGraphLineColor.BackColor = Setting_Graph_LineColor
    If Setting_Graph_ShowFree Then
      m_SettingsDialog.radGraphDisplayFree.Checked = True
    Else
      m_SettingsDialog.radGraphDisplayUsed.Checked = True
    End If
    If Setting_Graph_UpdateTime < m_SettingsDialog.nudGraphUpdateTime.Minimum Then Setting_Graph_UpdateTime = m_SettingsDialog.nudGraphUpdateTime.Minimum
    If Setting_Graph_UpdateTime > m_SettingsDialog.nudGraphUpdateTime.Maximum Then Setting_Graph_UpdateTime = m_SettingsDialog.nudGraphUpdateTime.Maximum
    m_SettingsDialog.nudGraphUpdateTime.Value = Setting_Graph_UpdateTime
    m_SettingsDialog.comGraphUpdateTime.SelectedIndex = Setting_Graph_UpdateTimeMode
    m_SettingsDialog.chkShowTopProcesses.Checked = Setting_Tooltip_ShowTopProcesses
    If Setting_Tooltip_MaxTopProcesses < m_SettingsDialog.nudTopProcesses.Minimum Then Setting_Tooltip_MaxTopProcesses = m_SettingsDialog.nudTopProcesses.Minimum
    If Setting_Tooltip_MaxTopProcesses > m_SettingsDialog.nudTopProcesses.Maximum Then Setting_Tooltip_MaxTopProcesses = m_SettingsDialog.nudTopProcesses.Maximum
    m_SettingsDialog.nudTopProcesses.Value = Setting_Tooltip_MaxTopProcesses
  End Sub

  ' InfoBar will call this when the application exits. Use this to save your internal settings.
  Public Overrides Sub SaveSettings(ByRef Doc As XmlDocument)
    Settings.SaveSetting(Doc, "showicon", Setting_Icon_Show)
    Settings.SaveSetting(Doc, "showtext", Setting_Text_Show)
    Settings.SaveSetting(Doc, "showgraph", Setting_Graph_Show)

    Settings.SaveSetting(Doc, "showtextfree", Setting_Text_ShowFree)
    Settings.SaveSetting(Doc, "showtextpctfree", Setting_Text_ShowPctFree)
    Settings.SaveSetting(Doc, "showtextused", Setting_Text_ShowUsed)
    Settings.SaveSetting(Doc, "showtextpctused", Setting_Text_ShowPctUsed)
    Settings.SaveSetting(Doc, "showtexttotal", Setting_Text_ShowTotal)
    Settings.SaveSetting(Doc, "textdisplayunit", Setting_Text_DisplayUnit)

    Settings.SaveSetting(Doc, "showtooltipfree", Setting_Tooltip_ShowFree)
    Settings.SaveSetting(Doc, "showtooltippctfree", Setting_Tooltip_ShowPctFree)
    Settings.SaveSetting(Doc, "showtooltipused", Setting_Tooltip_ShowUsed)
    Settings.SaveSetting(Doc, "showtooltippctused", Setting_Tooltip_ShowPctUsed)
    Settings.SaveSetting(Doc, "showtooltiptotal", Setting_Tooltip_ShowTotal)
    Settings.SaveSetting(Doc, "tooltipdisplayunit", Setting_Tooltip_DisplayUnit)

    Settings.SaveSetting(Doc, "graphcolor", Setting_Graph_LineColor.ToArgb)
    Settings.SaveSetting(Doc, "showgraphfree", Setting_Graph_ShowFree)
    Settings.SaveSetting(Doc, "graphupdatetime", Setting_Graph_UpdateTime)
    Settings.SaveSetting(Doc, "graphupdatetimemode", Setting_Graph_UpdateTimeMode)

    Settings.SaveSetting(Doc, "showtopprocesses", Setting_Tooltip_ShowTopProcesses)
    Settings.SaveSetting(Doc, "numberoftopprocessestoshow", Setting_Tooltip_MaxTopProcesses)
  End Sub

#End Region

#Region "Memory Usage Routines"

  Private Function UpdateMemoryUsage() As Boolean
    Dim bWindowIsDirty As Boolean

    Dim iTotal As ULong, iFree As ULong, iUsed As ULong, iPctFree As Integer, iPctUsed As Integer
    iTotal = My.Computer.Info.TotalPhysicalMemory
    iFree = My.Computer.Info.AvailablePhysicalMemory
    iUsed = iTotal - iFree
    If iFree <> 0 Then iPctFree = CInt((iFree / iTotal) * 100) Else iPctFree = 0
    If iUsed <> 0 Then iPctUsed = CInt((iUsed / iTotal) * 100) Else iPctUsed = 0

    Dim sText As String = vbNullString

    If Setting_Text_ShowFree Then
      sText &= "Free: " & FormatMemoryString(iFree, Setting_Text_DisplayUnit)
      If Setting_Text_ShowPctFree Then sText &= " (" & iPctFree & "%)"
      sText &= ", "
    Else
      If Setting_Text_ShowPctFree Then sText &= "Free: " & iPctFree & "%, "
    End If
    If Setting_Text_ShowUsed Then
      sText &= "Used: " & FormatMemoryString(iUsed, Setting_Text_DisplayUnit)
      If Setting_Text_ShowPctUsed Then sText &= " (" & iPctUsed & "%)"
      sText &= ", "
    Else
      If Setting_Text_ShowPctUsed Then sText &= "Used: " & iPctUsed & "%, "
    End If
    If Setting_Text_ShowTotal Then sText &= "Total: " & FormatMemoryString(iTotal, Setting_Text_DisplayUnit) & ", "

    ' Clean excess commas and spaces
    Try
      If sText.EndsWith(", ") Then sText = sText.Substring(0, sText.Length - 2)
      If sText.EndsWith(",") Then sText = sText.Substring(0, sText.Length - 1)
    Catch ex As Exception
    End Try

    If m_Text <> sText Then
      bWindowIsDirty = True
      m_Text = sText
    End If

    Dim sTooltipText As String = "Memory Usage: " & vbCrLf

    If Setting_Tooltip_ShowFree Then
      sTooltipText &= "Free: " & FormatMemoryString(iFree, Setting_Tooltip_DisplayUnit)
      If Setting_Tooltip_ShowPctFree Then sTooltipText &= " (" & iPctFree & "%)"
      sTooltipText &= vbCrLf
    Else
      If Setting_Tooltip_ShowPctFree Then sTooltipText &= "Free: " & iPctFree & "%, "
    End If
    If Setting_Tooltip_ShowUsed Then
      sTooltipText &= "Used: " & FormatMemoryString(iUsed, Setting_Tooltip_DisplayUnit)
      If Setting_Tooltip_ShowPctUsed Then sTooltipText &= " (" & iPctUsed & "%)"
      sTooltipText &= vbCrLf
    Else
      If Setting_Tooltip_ShowPctUsed Then sTooltipText &= "Used: " & iPctUsed & "%" & vbCrLf
    End If
    If Setting_Tooltip_ShowTotal Then sTooltipText &= "Total: " & FormatMemoryString(iTotal, Setting_Tooltip_DisplayUnit) & vbCrLf

    ' Clean excess line feeds
    If sTooltipText.EndsWith(vbCrLf) Then sTooltipText = sTooltipText.Substring(0, sTooltipText.Length - 1)

    ' Get Top Processes
    If Setting_Tooltip_ShowTopProcesses = True Then
      Dim pi As ProcessInfo
      For Each pi In m_Processes
        pi.IsDirty = True
      Next

      For Each proc As Process In Process.GetProcesses
        ' Grab our ProcessInfo class
        If m_Processes.Contains(proc.Id) = False Then
          pi = New ProcessInfo
          pi.ID = proc.Id
          pi.Name = proc.ProcessName
          m_Processes.Add(pi, proc.Id)
        Else
          pi = m_Processes(Key:=proc.Id)
        End If

        m_Processes(Key:=proc.Id).Used = proc.WorkingSet64
        m_Processes(Key:=proc.Id).Pct = CInt((proc.WorkingSet64 / My.Computer.Info.TotalPhysicalMemory) * 100)
        m_Processes(Key:=proc.Id).isDirty = False
      Next

      ' Clean Collection
      For Each pi In m_Processes
        If pi.IsDirty = True Then m_Processes.Remove(Key:=pi.ID)
      Next

      ' Fix to save CPU when tooltip is not shown
      If Tooltip.GetTooltipOwnerGUID = InfoBarModuleGUID Then
        Utilities.SortCollection(m_Processes, "Used", False, "ID")
      End If

      sTooltipText &= vbCrLf & vbCrLf & "Top Processes:"

      Dim p As Integer, iTotalProc As Integer
      If Setting_Tooltip_MaxTopProcesses > m_Processes.Count Then
        iTotalProc = m_Processes.Count
      Else
        iTotalProc = Setting_Tooltip_MaxTopProcesses
      End If
      For p = 1 To iTotalProc
        If m_Processes(Index:=p).Used <> 0 Then
          sTooltipText &= vbCrLf & m_Processes(Index:=p).Name & ": " & FormatMemoryString(m_Processes(Index:=p).Used, Setting_Tooltip_DisplayUnit) & " (" & m_Processes(Index:=p).Pct & "%)"
        End If
      Next
    End If

    If m_TooltipText <> sTooltipText Then
      m_TooltipText = sTooltipText
      If Tooltip.GetTooltipOwnerGUID = InfoBarModuleGUID AndAlso _
      Tooltip.GetTooltipOwnerObjectID = "MemoryUsage" Then
        DrawTooltip("MemoryUsage")
        Tooltip.UpdateTooltip()
      End If
    End If

    If Setting_Graph_Show = True Then
      Dim bUpdateNow As Boolean = False
      Dim iFinalVal As Integer
      Select Case Setting_Graph_UpdateTimeMode
        Case TimeMode.Seconds
          If Setting_Graph_UpdateTime > 1 Then
            If m_GraphAvgCount = Setting_Graph_UpdateTime Then
              iFinalVal = m_GraphAvgVals / m_GraphAvgCount
              m_GraphAvgVals = 0
              m_GraphAvgCount = 0
              bUpdateNow = True
            Else
              If Setting_Graph_ShowFree Then
                m_GraphAvgVals += iPctFree
              Else
                m_GraphAvgVals += iPctUsed
              End If
              m_GraphAvgCount += 1
            End If
          Else
            If Setting_Graph_ShowFree Then
              iFinalVal = iPctFree
            Else
              iFinalVal = iPctUsed
            End If
            bUpdateNow = True
          End If
        Case TimeMode.Minutes
          If m_GraphAvgCount = (Setting_Graph_UpdateTime * 60) Then
            iFinalVal = m_GraphAvgVals / m_GraphAvgCount
            m_GraphAvgVals = 0
            m_GraphAvgCount = 0
            bUpdateNow = True
          Else
            If Setting_Graph_ShowFree Then
              m_GraphAvgVals += iPctFree
            Else
              m_GraphAvgVals += iPctUsed
            End If
            m_GraphAvgCount += 1
          End If
        Case TimeMode.Hours
          If m_GraphAvgCount = (Setting_Graph_UpdateTime * 3600) Then
            iFinalVal = m_GraphAvgVals / m_GraphAvgCount
            m_GraphAvgVals = 0
            m_GraphAvgCount = 0
            bUpdateNow = True
          Else
            If Setting_Graph_ShowFree Then
              m_GraphAvgVals += iPctFree
            Else
              m_GraphAvgVals += iPctUsed
            End If
            m_GraphAvgCount += 1
          End If
      End Select

      If bUpdateNow = True Then
        m_GraphHistory.RemoveAt(0)
        m_GraphHistory.Add(iFinalVal)
        bWindowIsDirty = True
      End If
    End If

    Return bWindowIsDirty
  End Function

  Private Function FormatMemoryString(ByVal memory As ULong, ByVal unit As DisplayUnit) As String
    Select Case unit
      Case DisplayUnit.KB
        Return FormatNumber(memory / 1024, 2, TriState.False, TriState.False, TriState.True) & " KB"
      Case DisplayUnit.MB
        Return FormatNumber(memory / 1048576, 2, TriState.False, TriState.False, TriState.True) & " MB"
      Case DisplayUnit.GB
        Return FormatNumber(memory / 1073741824, 2, TriState.False, TriState.False, TriState.True) & " GB"
      Case Else
        Return FormatNumber(memory / 1048576, 2, TriState.False, TriState.False, TriState.True) & " MB"
    End Select
  End Function

#End Region

End Class
