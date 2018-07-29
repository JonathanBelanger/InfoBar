Public Class InfoBarModuleMain
  ' We inherit the base class. We DO NOT return any of the base classes values.
  Inherits InfoBarModule

  ' Unique ID. If same as other module, the first module will be loaded, the next will be ignored.
  Private Const InfoBarModuleGUID As String = "{3ED5962A-DBD1-4af2-BB5A-10F1970A1768}"

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
  Private m_TipColWidth(4) As Integer
  Private m_TotalSize As ULong
  Private m_TotalFree As ULong
  Private m_TotalPctFree As Integer
  Private m_TotalUsed As ULong
  Private m_TotalPctUsed As Integer
#End Region

#Region "Settings Variables"
  Friend Setting_Icon_Show As Boolean
  Friend Setting_Text_Show As Boolean
  Friend Setting_Text_FormatString As String
  Friend Setting_Text_DisplayUnit As Utilities.DisplayUnit
  Friend Setting_Tooltip_DisplayUnit As Utilities.DisplayUnit
  Friend Setting_Drive_Enabled As New Collections.Generic.Dictionary(Of String, Boolean)
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
      Return "Displays information about your hard disk usage."
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
    UpdateHardDiskUsage()
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
    If UpdateHardDiskUsage() Then
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
      Dim tr As Rectangle
      tr = Skin.MeasureText(grm, m_Text, Skinning.SkinTextPart.BackgroundText)
      If tr.Width > m_TextLastWidth Then
        m_TextLastWidth = tr.Width
      Else
        tr.Width = m_TextLastWidth
      End If
      width += tr.Width
    End If

    grm.Dispose()

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

      Case "HardDiskUsage"
        Dim GR As Graphics
        Dim tr As Rectangle
        Dim lWidth As Integer, lHeight As Integer

        GR = Graphics.FromHwnd(IntPtr.Zero)

        ' "Header" columns
        tr = Skin.MeasureText(GR, "Drive:", Skinning.SkinTextPart.TooltipText)
        If tr.Width > m_TipColWidth(0) Then m_TipColWidth(0) = tr.Width
        tr = Skin.MeasureText(GR, "Format:", Skinning.SkinTextPart.TooltipText)
        If tr.Width > m_TipColWidth(1) Then m_TipColWidth(1) = tr.Width
        tr = Skin.MeasureText(GR, "Size:", Skinning.SkinTextPart.TooltipText)
        If tr.Width > m_TipColWidth(2) Then m_TipColWidth(2) = tr.Width
        tr = Skin.MeasureText(GR, "Free:", Skinning.SkinTextPart.TooltipText)
        If tr.Width > m_TipColWidth(3) Then m_TipColWidth(3) = tr.Width
        tr = Skin.MeasureText(GR, "Used:", Skinning.SkinTextPart.TooltipText)
        If tr.Width > m_TipColWidth(4) Then m_TipColWidth(4) = tr.Width
        lHeight += tr.Height + 4

        For Each hdi As HardDriveInfoType In HardDriveInfo
          If Setting_Drive_Enabled(hdi.Letter) Then
            tr = Skin.MeasureText(GR, hdi.Label & " (" & hdi.Letter & ":)", Skinning.SkinTextPart.TooltipText)
            If tr.Width > m_TipColWidth(0) Then m_TipColWidth(0) = tr.Width
            tr = Skin.MeasureText(GR, hdi.Format, Skinning.SkinTextPart.TooltipText)
            If tr.Width > m_TipColWidth(1) Then m_TipColWidth(1) = tr.Width
            tr = Skin.MeasureText(GR, Utilities.FormatFileSize(hdi.BytesTotal, Setting_Tooltip_DisplayUnit), Skinning.SkinTextPart.TooltipText)
            If tr.Width > m_TipColWidth(2) Then m_TipColWidth(2) = tr.Width
            tr = Skin.MeasureText(GR, Utilities.FormatFileSize(hdi.BytesFree, Setting_Tooltip_DisplayUnit) & " (" & hdi.PercentFree & "%)", Skinning.SkinTextPart.TooltipText)
            If tr.Width > m_TipColWidth(3) Then m_TipColWidth(3) = tr.Width
            tr = Skin.MeasureText(GR, Utilities.FormatFileSize(hdi.BytesUsed, Setting_Tooltip_DisplayUnit) & " (" & hdi.PercentUsed & "%)", Skinning.SkinTextPart.TooltipText)
            If tr.Width > m_TipColWidth(4) Then m_TipColWidth(4) = tr.Width
            lHeight += tr.Height + 2
          End If
        Next

        If HardDriveInfo.Count > 1 Then
          lHeight += Skin.TooltipSeparatorHeight

          ' Totals
          tr = Skin.MeasureText(GR, "Total:", Skinning.SkinTextPart.TooltipText)
          If tr.Width > m_TipColWidth(0) Then m_TipColWidth(0) = tr.Width
          tr = Skin.MeasureText(GR, Utilities.FormatFileSize(m_TotalSize, Setting_Tooltip_DisplayUnit), Skinning.SkinTextPart.TooltipText)
          If tr.Width > m_TipColWidth(2) Then m_TipColWidth(2) = tr.Width
          tr = Skin.MeasureText(GR, Utilities.FormatFileSize(m_TotalFree, Setting_Tooltip_DisplayUnit) & " (" & m_TotalPctFree & "%)", Skinning.SkinTextPart.TooltipText)
          If tr.Width > m_TipColWidth(3) Then m_TipColWidth(3) = tr.Width
          tr = Skin.MeasureText(GR, Utilities.FormatFileSize(m_TotalUsed, Setting_Tooltip_DisplayUnit) & " (" & m_TotalPctUsed & "%)", Skinning.SkinTextPart.TooltipText)
          If tr.Width > m_TipColWidth(4) Then m_TipColWidth(4) = tr.Width
          lHeight += tr.Height
        End If

        lWidth = m_TipColWidth(0) + 8 + m_TipColWidth(1) + 8 + m_TipColWidth(2) + 8 + _
                m_TipColWidth(3) + 8 + m_TipColWidth(4)

        GR.Dispose()

        Dim bmpTemp As New Bitmap(lWidth, lHeight, System.Drawing.Imaging.PixelFormat.Format32bppArgb)
        GR = Graphics.FromImage(bmpTemp)

        Dim r As Rectangle, curX As Integer, curY As Integer
        curX = 0 'Skin.ContentMargins(Skinning.SkinMarginPart.Tooltip).Left
        curY = 0 'Skin.ContentMargins(Skinning.SkinMarginPart.Tooltip).Top

        ' "Header" columns
        r = Skin.MeasureText(GR, "Drive:", Skinning.SkinTextPart.TooltipText)
        r.X = curX : r.Y = curY
        Skin.DrawText(GR, "Drive:", r, Skinning.SkinTextPart.TooltipText, StringAlignment.Near, StringAlignment.Near)
        curX += m_TipColWidth(0) + 8

        r = Skin.MeasureText(GR, "Format:", Skinning.SkinTextPart.TooltipText)
        r.X = curX : r.Y = curY
        Skin.DrawText(GR, "Format:", r, Skinning.SkinTextPart.TooltipText, StringAlignment.Near, StringAlignment.Near)
        curX += m_TipColWidth(1) + 8

        r = Skin.MeasureText(GR, "Size:", Skinning.SkinTextPart.TooltipText)
        r.X = curX : r.Y = curY
        Skin.DrawText(GR, "Size:", r, Skinning.SkinTextPart.TooltipText, StringAlignment.Near, StringAlignment.Near)
        curX += m_TipColWidth(2) + 8

        r = Skin.MeasureText(GR, "Free:", Skinning.SkinTextPart.TooltipText)
        r.X = curX : r.Y = curY
        Skin.DrawText(GR, "Free:", r, Skinning.SkinTextPart.TooltipText, StringAlignment.Near, StringAlignment.Near)
        curX += m_TipColWidth(3) + 8

        r = Skin.MeasureText(GR, "Used:", Skinning.SkinTextPart.TooltipText)
        r.X = curX : r.Y = curY
        Skin.DrawText(GR, "Used:", r, Skinning.SkinTextPart.TooltipText, StringAlignment.Near, StringAlignment.Near)

        curX = 0 'Skin.ContentMargins(Skinning.SkinMarginPart.Tooltip).Left
        curY += r.Height + 4

        For Each hdi As HardDriveInfoType In HardDriveInfo
          If Setting_Drive_Enabled(hdi.Letter) Then
            r = Skin.MeasureText(GR, hdi.Label & " (" & hdi.Letter & ":)", Skinning.SkinTextPart.TooltipText)
            r.X = curX : r.Y = curY
            Skin.DrawText(GR, hdi.Label & " (" & hdi.Letter & ":)", r, Skinning.SkinTextPart.TooltipText, StringAlignment.Near, StringAlignment.Near)
            curX += m_TipColWidth(0) + 8

            r = Skin.MeasureText(GR, hdi.Format, Skinning.SkinTextPart.TooltipText)
            r.X = curX : r.Y = curY
            Skin.DrawText(GR, hdi.Format, r, Skinning.SkinTextPart.TooltipText, StringAlignment.Near, StringAlignment.Near)
            curX += m_TipColWidth(1) + 8

            r = Skin.MeasureText(GR, Utilities.FormatFileSize(hdi.BytesTotal, Setting_Tooltip_DisplayUnit), Skinning.SkinTextPart.TooltipText)
            r.X = curX : r.Y = curY
            Skin.DrawText(GR, Utilities.FormatFileSize(hdi.BytesTotal, Setting_Tooltip_DisplayUnit), r, Skinning.SkinTextPart.TooltipText, StringAlignment.Near, StringAlignment.Near)
            curX += m_TipColWidth(2) + 8

            r = Skin.MeasureText(GR, Utilities.FormatFileSize(hdi.BytesFree, Setting_Tooltip_DisplayUnit) & " (" & hdi.PercentFree & "%)", Skinning.SkinTextPart.TooltipText)
            r.X = curX : r.Y = curY
            Skin.DrawText(GR, Utilities.FormatFileSize(hdi.BytesFree, Setting_Tooltip_DisplayUnit) & " (" & hdi.PercentFree & "%)", r, Skinning.SkinTextPart.TooltipText, StringAlignment.Near, StringAlignment.Near)
            curX += m_TipColWidth(3) + 8

            r = Skin.MeasureText(GR, Utilities.FormatFileSize(hdi.BytesUsed, Setting_Tooltip_DisplayUnit) & " (" & hdi.PercentUsed & "%)", Skinning.SkinTextPart.TooltipText)
            r.X = curX : r.Y = curY
            Skin.DrawText(GR, Utilities.FormatFileSize(hdi.BytesUsed, Setting_Tooltip_DisplayUnit) & " (" & hdi.PercentUsed & "%)", r, Skinning.SkinTextPart.TooltipText, StringAlignment.Near, StringAlignment.Near)

            curX = 0 'Skin.ContentMargins(Skinning.SkinMarginPart.Tooltip).Left
            curY += r.Height + 2
          End If
        Next

        If HardDriveInfo.Count > 1 Then
          r = New Rectangle(curX, curY, lWidth, Skin.TooltipSeparatorHeight)
          Skin.DrawTooltipSeparator(GR, r)
          curY += r.Height

          ' Totals
          r = Skin.MeasureText(GR, "Total:", Skinning.SkinTextPart.TooltipText)
          r.X = curX : r.Y = curY
          Skin.DrawText(GR, "Total:", r, Skinning.SkinTextPart.TooltipText, StringAlignment.Near, StringAlignment.Near)
          curX += m_TipColWidth(0) + 8 + m_TipColWidth(1) + 8

          r = Skin.MeasureText(GR, Utilities.FormatFileSize(m_TotalSize, Setting_Tooltip_DisplayUnit), Skinning.SkinTextPart.TooltipText)
          r.X = curX : r.Y = curY
          Skin.DrawText(GR, Utilities.FormatFileSize(m_TotalSize, Setting_Tooltip_DisplayUnit), r, Skinning.SkinTextPart.TooltipText, StringAlignment.Near, StringAlignment.Near)
          curX += m_TipColWidth(2) + 8

          r = Skin.MeasureText(GR, Utilities.FormatFileSize(m_TotalFree, Setting_Tooltip_DisplayUnit) & " (" & m_TotalPctFree & "%)", Skinning.SkinTextPart.TooltipText)
          r.X = curX : r.Y = curY
          Skin.DrawText(GR, Utilities.FormatFileSize(m_TotalFree, Setting_Tooltip_DisplayUnit) & " (" & m_TotalPctFree & "%)", r, Skinning.SkinTextPart.TooltipText, StringAlignment.Near, StringAlignment.Near)
          curX += m_TipColWidth(3) + 8

          r = Skin.MeasureText(GR, Utilities.FormatFileSize(m_TotalUsed, Setting_Tooltip_DisplayUnit) & " (" & m_TotalPctUsed & "%)", Skinning.SkinTextPart.TooltipText)
          r.X = curX : r.Y = curY
          Skin.DrawText(GR, Utilities.FormatFileSize(m_TotalUsed, Setting_Tooltip_DisplayUnit) & " (" & m_TotalPctUsed & "%)", r, Skinning.SkinTextPart.TooltipText, StringAlignment.Near, StringAlignment.Near)
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
    If m_Bounds.Contains(e.Location) Then
      Tooltip.SetTooltipOwner(InfoBarModuleGUID, "HardDiskUsage")
      DrawTooltip("HardDiskUsage")
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
  Public Overrides Property ModuleBounds() As Rectangle
    Set(ByVal value As Rectangle)
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
    MainMenu.AddMenuItem(InfoBarModuleGUID & "::" & "SETTINGS", "Hard Disk Usage Settings...", ico, True, True)

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
    m_TipColWidth(0) = 0
    m_TipColWidth(1) = 0
    m_TipColWidth(2) = 0
    m_TipColWidth(3) = 0
    m_TipColWidth(4) = 0
    Setting_Icon_Show = m_SettingsDialog.chkShowIcon.Checked
    Setting_Text_Show = m_SettingsDialog.chkShowText.Checked
    Setting_Text_FormatString = m_SettingsDialog.txtTextFormatString.Text
    Setting_Text_DisplayUnit = m_SettingsDialog.comDisplayUnit.SelectedIndex
    Setting_Tooltip_DisplayUnit = m_SettingsDialog.comTooltipDisplayUnit.SelectedIndex

    For Each LI As ListViewItem In m_SettingsDialog.lvDrives.Items
      Setting_Drive_Enabled(LI.Name) = LI.Checked
    Next

    m_Bounds.Width = 1
    DrawModule()
  End Sub

  ' InfoBar will call this when the application loads. Use this to load your internal settings.
  Public Overrides Sub LoadSettings(ByRef Doc As XmlDocument)
    Setting_Icon_Show = Settings.GetSetting(Doc, "showicon", True)
    Setting_Text_Show = Settings.GetSetting(Doc, "showtext", True)
    Setting_Text_FormatString = Settings.GetSetting(Doc, "textformatstring", "%letter%: %pctfree%%startsep%, %endsep%")
    Setting_Text_DisplayUnit = Settings.GetSetting(Doc, "textdisplayunit", Utilities.DisplayUnit.Auto)
    Setting_Tooltip_DisplayUnit = Settings.GetSetting(Doc, "tooltipdisplayunit", Utilities.DisplayUnit.Auto)

    For Each D As System.IO.DriveInfo In My.Computer.FileSystem.Drives
      If D.DriveType = IO.DriveType.Fixed AndAlso D.IsReady = True Then
        Dim sName As String = D.Name.Substring(0, 1)
        Setting_Drive_Enabled.Add(sName, Settings.GetSetting(Doc, "drives/" & sName, True))
      End If
    Next

    ResetSettings()
  End Sub

  Public Overrides Sub ResetSettings()
    With m_SettingsDialog.lvDrives.Items
      .Clear()
      For Each D As System.IO.DriveInfo In My.Computer.FileSystem.Drives
        If D.DriveType = IO.DriveType.Fixed AndAlso D.IsReady = True Then
          Dim LI As New ListViewItem
          Dim sName As String = D.Name.Substring(0, 1)
          LI.Name = sName
          LI.Text = D.Name
          Try
            LI.Text &= " (" & D.VolumeLabel & ")"
          Catch ex As Exception
          End Try
          If Setting_Drive_Enabled.ContainsKey(sName) Then LI.Checked = Setting_Drive_Enabled(sName)
          .Add(LI)
        End If
      Next
    End With
    m_SettingsDialog.lvDrives.Columns(0).Width = m_SettingsDialog.lvDrives.Width - (SystemInformation.VerticalScrollBarWidth + 4)

    m_SettingsDialog.chkShowIcon.Checked = Setting_Icon_Show
    m_SettingsDialog.chkShowText.Checked = Setting_Text_Show
    m_SettingsDialog.txtTextFormatString.Text = Setting_Text_FormatString
    m_SettingsDialog.comDisplayUnit.SelectedIndex = Setting_Text_DisplayUnit
    m_SettingsDialog.comTooltipDisplayUnit.SelectedIndex = Setting_Tooltip_DisplayUnit
  End Sub

  ' InfoBar will call this when the application exits. Use this to save your internal settings.
  Public Overrides Sub SaveSettings(ByRef Doc As XmlDocument)
    Settings.SaveSetting(Doc, "showicon", Setting_Icon_Show)
    Settings.SaveSetting(Doc, "showtext", Setting_Text_Show)
    Settings.SaveSetting(Doc, "textformatstring", Setting_Text_FormatString)
    Settings.SaveSetting(Doc, "textdisplayunit", Setting_Text_DisplayUnit)
    Settings.SaveSetting(Doc, "tooltipdisplayunit", Setting_Tooltip_DisplayUnit)

    Dim drivesNode As XmlElement = Doc.CreateElement("drives")
    For Each E As KeyValuePair(Of String, Boolean) In Setting_Drive_Enabled
      Dim driveNode As XmlElement = Doc.CreateElement(E.Key)
      driveNode.InnerText = E.Value
      drivesNode.AppendChild(driveNode)
    Next
    Doc.DocumentElement.AppendChild(drivesNode)
  End Sub

#End Region

#Region "Hard Disk Usage Routines"

  Public Class HardDriveInfoType
    Public Letter As String
    Public Label As String
    Public Format As String
    Public BytesTotal As ULong
    Public BytesFree As ULong
    Public PercentFree As Integer
    Public BytesUsed As ULong
    Public PercentUsed As Integer
  End Class

  Public HardDriveInfo As New Collection

  Private Function UpdateHardDiskUsage() As Boolean
    Dim bWindowIsDirty As Boolean
    Dim sText As String = vbNullString

    m_TotalSize = 0
    m_TotalFree = 0
    m_TotalUsed = 0

    ' Update Hard Drive Info
    For Each drive As KeyValuePair(Of String, Boolean) In Setting_Drive_Enabled
      If drive.Value = True Then
        Dim D As New System.IO.DriveInfo(drive.Key)
        Dim hdi As New HardDriveInfoType
        hdi.Letter = drive.Key
        If HardDriveInfo.Contains(drive.Key) = False Then HardDriveInfo.Add(hdi, drive.Key)
        With HardDriveInfo(drive.Key)
          Try
            If .Label <> D.VolumeLabel Then
              bWindowIsDirty = True
              .Label = D.VolumeLabel
            End If
          Catch ex As Exception
          End Try
          Try
            If .Format <> D.DriveFormat Then
              bWindowIsDirty = True
              .Format = D.DriveFormat
            End If
          Catch ex As Exception
          End Try
          Try
            If .BytesTotal <> D.TotalSize Then
              bWindowIsDirty = True
              .BytesTotal = D.TotalSize
            End If
          Catch ex As Exception
          End Try
          Try
            If .BytesFree <> D.TotalFreeSpace Then
              bWindowIsDirty = True
              .BytesFree = D.TotalFreeSpace
            End If
          Catch ex As Exception
          End Try
          .PercentFree = (.BytesFree / .BytesTotal) * 100
          .BytesUsed = (.BytesTotal - .BytesFree)
          .PercentUsed = (.BytesUsed / .BytesTotal) * 100

          m_TotalSize += .BytesTotal
          m_TotalFree += .BytesFree
          m_TotalUsed += .BytesUsed

          Dim tText As String = Setting_Text_FormatString
          tText = tText.Replace("%letter%", .Letter)
          tText = tText.Replace("%label%", .Label)
          tText = tText.Replace("%format%", .Format)
          tText = tText.Replace("%free%", Utilities.FormatFileSize(.BytesFree, Setting_Text_DisplayUnit))
          tText = tText.Replace("%pctfree%", .PercentFree & "%")
          tText = tText.Replace("%used%", Utilities.FormatFileSize(.BytesUsed, Setting_Text_DisplayUnit))
          tText = tText.Replace("%pctused%", .PercentUsed & "%")
          tText = tText.Replace("%size%", Utilities.FormatFileSize(.BytesTotal, Setting_Text_DisplayUnit))
          tText = tText.Replace("%startsep%", "")
          tText = tText.Replace("%endsep%", "")
          sText &= tText
        End With
      End If
    Next

    m_TotalPctFree = (m_TotalFree / m_TotalSize) * 100
    m_TotalPctUsed = (m_TotalUsed / m_TotalSize) * 100

    ' Strip Last Separator
    If Setting_Text_FormatString.Contains("%startsep%") AndAlso Setting_Text_FormatString.Contains("%endsep%") Then
      Dim sSep As String = Setting_Text_FormatString
      sSep = sSep.Substring(sSep.IndexOf("%startsep%"))
      sSep = sSep.Replace("%startsep%", "")
      sSep = sSep.Replace("%endsep%", "")
      If sText.EndsWith(sSep) Then sText = sText.Remove(sText.Length - sSep.Length, sSep.Length)
    End If

    If m_Text <> sText Then
      bWindowIsDirty = True
      m_Text = sText
    End If

    If bWindowIsDirty _
    AndAlso Tooltip.GetTooltipOwnerGUID = InfoBarModuleGUID _
    AndAlso Tooltip.GetTooltipOwnerObjectID = "HardDiskUsage" Then
      DrawTooltip("HardDiskUsage")
      Tooltip.UpdateTooltip()
    End If

    Return bWindowIsDirty
  End Function

#End Region

End Class
