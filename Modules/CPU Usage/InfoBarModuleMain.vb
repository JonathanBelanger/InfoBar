Public Class InfoBarModuleMain
  ' We inherit the base class. We DO NOT return any of the base classes values.
  Inherits InfoBarModule

  ' Unique ID. If same as other module, the first module will be loaded, the next will be ignored.
  Private Const InfoBarModuleGUID As String = "{C41B6165-1860-4a26-9D76-9E17FF22C6C8}"

#Region "Private Classes"
  Private Class ProcessInfo
    Public Name As String
    Public Usage As Integer
    Public Counter As PerformanceCounter
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
  Friend Class CoreData
    Public perfCounter As PerformanceCounter
    Public showText As Boolean
    Public showTooltip As Boolean
    Public showGraph As Boolean
    Public graphColor As Color
    Public graphHistory As New ArrayList
    Public graphAvgVals As ULong
    Public graphAvgCount As UInteger
  End Class

  Private m_ModuleToolbarBitmap As Bitmap
  Private m_ModuleTooltipBitmap As Bitmap
  Private m_SettingsDialog As New Settings
  Private m_Bounds As Rectangle
  Private m_Enabled As Boolean
  Private m_Text As String
  Private m_TooltipText As String
  Private m_Processes As New Collection
  Private m_DebugPrivsEnabled As Boolean
  Private m_CoreData As New List(Of CoreData)
#End Region

#Region "Settings Variables"
  Friend Enum TimeMode
    Seconds
    Minutes
    Hours
  End Enum

  Friend Setting_ShowIcon As Boolean
  Friend Setting_ShowText As Boolean
  Friend Setting_ShowGraph As Boolean
  Friend Setting_Graph_UpdateTime As Integer
  Friend Setting_Graph_UpdateTimeMode As TimeMode
  Friend Setting_ShowTopProcesses As Boolean
  Friend Setting_NumberOfTopProcessesToShow As Integer
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
      Return "Displays information about your processor and the percentage of time that it is not idle."
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

  Public Sub New()
    Dim I As Integer, J As Integer
    For I = 0 To System.Environment.ProcessorCount - 1
      Try
        Dim cd As New CoreData
        cd.showText = True
        cd.showTooltip = True
        cd.showGraph = True
        cd.graphColor = Color.Green
        cd.perfCounter = New PerformanceCounter("Processor", "% Processor Time", I.ToString)
        cd.graphHistory.Clear()
        For J = 0 To 99
          cd.graphHistory.Add(0)
        Next
        m_CoreData.Add(cd)
      Catch ex As Exception
      End Try
    Next
  End Sub

  Public Overrides Sub InitializeModule()
    Try
      If m_DebugPrivsEnabled = False Then
        m_DebugPrivsEnabled = True
        Process.EnterDebugMode()
      End If
    Catch ex As Exception
    End Try

    UpdateCPUUsage()
    DrawModule()
  End Sub

  Public Overrides Sub FinalizeModule()
    For Each cd As CoreData In m_CoreData
      cd.perfCounter.Close()
    Next

    Try
      If m_DebugPrivsEnabled Then
        m_DebugPrivsEnabled = False
        Process.LeaveDebugMode()
      End If
    Catch ex As Exception
    End Try
  End Sub

  Public Overrides Sub TimerTick(ByRef bModuleIsDirty As Boolean)
    If m_Enabled = False Then Exit Sub

    If UpdateCPUUsage() Then
      DrawModule()
      bModuleIsDirty = True

      ' If we have the tooltip, update it.
      Dim TooltipOwnerGUID As String = Tooltip.GetTooltipOwnerGUID
      If Tooltip.GetTooltipOwnerGUID = InfoBarModuleGUID Then
        Dim TooltipOwnerObjectID As String = Tooltip.GetTooltipOwnerObjectID
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
    Dim ico As Image = Nothing

    If Setting_ShowIcon = True Then
      If IconTheme.IsDefaultTheme = True Then
        ico = My.Resources.icon
      Else
        ico = IconTheme.GetThemeIcon(InfoBarModuleGUID, "CPU")
        If ico Is Nothing Then ico = My.Resources.icon
      End If

      width += ico.Width
    End If

    ' Measure Module
    Dim grm As Graphics = Graphics.FromHwnd(IntPtr.Zero)

    Dim trText As Rectangle
    If Setting_ShowText = True Then
      If Setting_ShowIcon Then width += 2
      trText = Skin.MeasureText(grm, m_Text, Skinning.SkinTextPart.BackgroundText)
      width += trText.Width
    End If

    If Setting_ShowGraph = True Then
      If Setting_ShowIcon Or Setting_ShowText Then width += 2
      width += 100
    End If

    If width < m_Bounds.Width Then width = m_Bounds.Width

    ' Draw Module
    If width <= 1 Or height <= 1 Then Exit Sub
    Dim bmpTemp As New Bitmap(width, height, System.Drawing.Imaging.PixelFormat.Format32bppArgb)
    Dim GR As Graphics = Graphics.FromImage(bmpTemp)

    Dim curX As Integer = 0
    Dim lHeight As Integer = bmpTemp.Height

    If Setting_ShowIcon = True Then
      GR.DrawImage(ico, curX, CInt((lHeight - ico.Height) / 2), ico.Width, ico.Height)
      curX += ico.Width
    End If

    If Setting_ShowText = True Then
      If Setting_ShowIcon Then curX += 2
      trText.X = curX : trText.Y = 0 : trText.Height = lHeight
      Skin.DrawText(GR, m_Text, trText, Skinning.SkinTextPart.BackgroundText, StringAlignment.Near, StringAlignment.Center)
      curX += trText.Width
    End If

    If Setting_ShowGraph = True Then
      Dim rg As Rectangle
      rg.X = width - 100
      rg.Y = 0
      rg.Width = 100
      rg.Height = lHeight
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
      GR.CompositingQuality = CompositingQuality.Default
      GR.SmoothingMode = SmoothingMode.AntiAlias

      ' Draw Value Line
      Dim i As Integer, gv As Integer
      Dim x1 As Integer, y1 As Integer, x2 As Integer, y2 As Integer
      For i = 0 To m_CoreData.Count - 1
        Dim gvp As New Pen(m_CoreData(i).graphColor)
        For gv = 0 To m_CoreData(i).graphHistory.Count - 2
          x1 = rg.X + ((gv / 100) * rg.Width)
          y1 = rg.Y + (rg.Height - ((m_CoreData(i).graphHistory(gv) / 100) * rg.Height))
          x2 = rg.X + (((gv + 1) / 100) * rg.Width)
          y2 = rg.Y + (rg.Height - ((m_CoreData(i).graphHistory(gv + 1) / 100) * rg.Height))
          GR.DrawLine(gvp, x1, y1, x2, y2)
        Next
        gvp.Dispose()
      Next

      ' Reset Rendering Settings To Old Values
      GR.CompositingQuality = oldCompositingQuality
      GR.SmoothingMode = oldSmoothingMode
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
    Select Case ObjectID
      Case "CPUUsage"
        Dim GR As Graphics
        Dim tr As Rectangle
        GR = Graphics.FromHwnd(IntPtr.Zero)
        tr = Skin.MeasureText(GR, m_TooltipText, Skinning.SkinTextPart.TooltipText)
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
    Tooltip.SetTooltipOwner(InfoBarModuleGUID, "CPUUsage")
    DrawTooltip("CPUUsage")
    Tooltip.UpdateTooltip()
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
    Get
      Return m_Bounds
    End Get
    Set(ByVal value As System.Drawing.Rectangle)
      m_Bounds = value
    End Set
  End Property

  Public Overrides Sub AddMainPopupMenuItems()
    Dim ico As Image
    If IconTheme.IsDefaultTheme = True Then
      ico = My.Resources.icon
    Else
      ico = IconTheme.GetThemeIcon(InfoBarModuleGUID, "CPU")
      If ico Is Nothing Then ico = My.Resources.icon
    End If
    MainMenu.AddMenuItem(InfoBarModuleGUID & "::" & "SETTINGS", "CPU Usage Settings...", ico, True, True)
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
    Setting_ShowIcon = m_SettingsDialog.chkShowIcon.Checked
    Setting_ShowText = m_SettingsDialog.chkShowText.Checked
    Setting_ShowGraph = m_SettingsDialog.chkShowGraph.Checked

    Dim bResetGraph As Boolean = False
    If m_SettingsDialog.nudGraphUpdateTime.Value <> Setting_Graph_UpdateTime OrElse _
    m_SettingsDialog.comGraphUpdateTime.SelectedIndex <> Setting_Graph_UpdateTimeMode Then
      Dim I As Integer, J As Integer
      For I = 0 To m_CoreData.Count - 1
        m_CoreData(I).graphAvgVals = 0
        m_CoreData(I).graphAvgCount = 0
        m_CoreData(I).graphHistory.Clear()
        For J = 0 To 99
          m_CoreData(I).graphHistory.Add(0)
        Next
      Next
    End If

    Setting_Graph_UpdateTime = m_SettingsDialog.nudGraphUpdateTime.Value
    Setting_Graph_UpdateTimeMode = m_SettingsDialog.comGraphUpdateTime.SelectedIndex

    Setting_ShowTopProcesses = m_SettingsDialog.chkShowTopProcesses.Checked
    Setting_NumberOfTopProcessesToShow = m_SettingsDialog.nudTopProcesses.Value

    For I As Integer = 0 To m_CoreData.Count - 1
      m_CoreData(I).showText = CBool(m_SettingsDialog.lvMulticore.Items(I).SubItems(1).Text)
      m_CoreData(I).showTooltip = CBool(m_SettingsDialog.lvMulticore.Items(I).SubItems(2).Text)
      m_CoreData(I).showGraph = CBool(m_SettingsDialog.lvMulticore.Items(I).SubItems(3).Text)
      m_CoreData(I).graphColor = Color.FromArgb(CInt(m_SettingsDialog.lvMulticore.Items(I).SubItems(4).Text))
    Next

    m_Bounds.Width = 1
    UpdateCPUUsage()
    DrawModule()
  End Sub

  ' InfoBar will call this when the application loads. Use this to set your internal settings.
  Public Overrides Sub LoadSettings(ByRef Doc As XmlDocument)
    Setting_ShowIcon = Settings.GetSetting(Doc, "showicon", True)
    Setting_ShowText = Settings.GetSetting(Doc, "showtext", True)
    Setting_ShowGraph = Settings.GetSetting(Doc, "showgraph", False)

    Setting_Graph_UpdateTime = Settings.GetSetting(Doc, "graphupdatetime", 1)
    If Setting_Graph_UpdateTime < 1 Then Setting_Graph_UpdateTime = 1
    If Setting_Graph_UpdateTime > 60 Then Setting_Graph_UpdateTime = 60

    Setting_Graph_UpdateTimeMode = Settings.GetSetting(Doc, "graphupdatetimemode", 1)
    Setting_ShowTopProcesses = Settings.GetSetting(Doc, "showtopprocesses", True)

    Setting_NumberOfTopProcessesToShow = Settings.GetSetting(Doc, "numberoftopprocessestoshow", 10)
    If Setting_NumberOfTopProcessesToShow < 2 Then Setting_NumberOfTopProcessesToShow = 2
    If Setting_NumberOfTopProcessesToShow > 100 Then Setting_NumberOfTopProcessesToShow = 100

    For I As Integer = 0 To m_CoreData.Count - 1
      m_CoreData(I).showText = CBool(Settings.GetSetting(Doc, "core" & I & "showtext", True))
      m_CoreData(I).showTooltip = CBool(Settings.GetSetting(Doc, "core" & I & "showtooltip", True))
      m_CoreData(I).showGraph = CBool(Settings.GetSetting(Doc, "core" & I & "showgraph", True))
      m_CoreData(I).graphColor = Color.FromArgb(CInt(Settings.GetSetting(Doc, "core" & I & "graphcolor", Color.Green.ToArgb)))
    Next

    ResetSettings()
  End Sub

  ' InfoBar will call this when it needs to reset the settings dialog.
  Public Overrides Sub ResetSettings()
    m_SettingsDialog.chkShowIcon.Checked = Setting_ShowIcon
    m_SettingsDialog.chkShowText.Checked = Setting_ShowText
    m_SettingsDialog.chkShowGraph.Checked = Setting_ShowGraph
    m_SettingsDialog.nudGraphUpdateTime.Value = Setting_Graph_UpdateTime
    m_SettingsDialog.comGraphUpdateTime.SelectedIndex = Setting_Graph_UpdateTimeMode
    m_SettingsDialog.chkShowTopProcesses.Checked = Setting_ShowTopProcesses
    m_SettingsDialog.nudTopProcesses.Value = Setting_NumberOfTopProcessesToShow

    m_SettingsDialog.lvMulticore.Items.Clear()
    For I As Integer = 0 To m_CoreData.Count - 1
      If m_SettingsDialog.lvMulticore.Items.ContainsKey("Core_" & I) Then
        m_SettingsDialog.lvMulticore.Items(I).SubItems(1).Text = m_CoreData(I).showText.ToString
        m_SettingsDialog.lvMulticore.Items(I).SubItems(2).Text = m_CoreData(I).showTooltip.ToString
        m_SettingsDialog.lvMulticore.Items(I).SubItems(3).Text = m_CoreData(I).showGraph.ToString
        m_SettingsDialog.lvMulticore.Items(I).SubItems(4).Text = m_CoreData(I).graphColor.ToArgb.ToString
      Else
        Dim LI As ListViewItem
        Dim lvsi As New ListViewItem.ListViewSubItem

        LI = New ListViewItem
        LI.Name = "Core_" & I
        LI.Text = "Core " & (I + 1)

        lvsi = New ListViewItem.ListViewSubItem
        lvsi.Name = "ShowText_" & I
        lvsi.Text = m_CoreData(I).showText.ToString
        LI.SubItems.Add(lvsi)

        lvsi = New ListViewItem.ListViewSubItem
        lvsi.Name = "ShowTooltip_" & I
        lvsi.Text = m_CoreData(I).showTooltip.ToString
        LI.SubItems.Add(lvsi)

        lvsi = New ListViewItem.ListViewSubItem
        lvsi.Name = "ShowGraph_" & I
        lvsi.Text = m_CoreData(I).showGraph.ToString
        LI.SubItems.Add(lvsi)

        lvsi = New ListViewItem.ListViewSubItem
        lvsi.Name = "GraphColor_" & I
        lvsi.Text = m_CoreData(I).graphColor.ToArgb.ToString
        LI.SubItems.Add(lvsi)

        m_SettingsDialog.lvMulticore.Items.Add(LI)
      End If
    Next
  End Sub

  ' InfoBar will call this when the application exits. Use this to save your internal settings.
  Public Overrides Sub SaveSettings(ByRef Doc As XmlDocument)
    Settings.SaveSetting(Doc, "showicon", Setting_ShowIcon)
    Settings.SaveSetting(Doc, "showtext", Setting_ShowText)
    Settings.SaveSetting(Doc, "showgraph", Setting_ShowGraph)
    Settings.SaveSetting(Doc, "graphupdatetime", Setting_Graph_UpdateTime)
    Settings.SaveSetting(Doc, "graphupdatetimemode", Setting_Graph_UpdateTimeMode)
    Settings.SaveSetting(Doc, "showtopprocesses", Setting_ShowTopProcesses)
    Settings.SaveSetting(Doc, "numberoftopprocessestoshow", Setting_NumberOfTopProcessesToShow)

    For I As Integer = 0 To m_CoreData.Count - 1
      Settings.SaveSetting(Doc, "core" & I & "showtext", m_CoreData(I).showText)
      Settings.SaveSetting(Doc, "core" & I & "showtooltip", m_CoreData(I).showTooltip)
      Settings.SaveSetting(Doc, "core" & I & "showgraph", m_CoreData(I).showGraph)
      Settings.SaveSetting(Doc, "core" & I & "graphcolor", m_CoreData(I).graphColor.ToArgb.ToString)
    Next
  End Sub

#End Region

#Region "CPU Usage Routines"

  Private Function UpdateCPUUsage() As Boolean
    Dim bWindowIsDirty As Boolean
    Dim i As Integer, sText As String = vbNullString, sTooltipText As String = vbNullString
    Dim iCPU As Integer, iCPUAvg As Integer, bNoCoresVisible As Boolean = True
    For i = 0 To m_CoreData.Count - 1
      Try
        iCPU = CInt(Math.Round(m_CoreData(i).perfCounter.NextValue, 0))
        iCPUAvg += iCPU
      Catch ex As Exception
        iCPU = 0
      End Try

      If m_CoreData(i).showText Then
        sText &= "Core " & (i + 1) & ": " & iCPU & "% | "
        bNoCoresVisible = False
      End If

      If m_CoreData(i).showTooltip Then
        sTooltipText &= "Core " & (i + 1) & ": " & iCPU & "%" & vbCrLf
      End If

      If Setting_ShowGraph = True Then
        Dim bUpdateNow As Boolean = False
        Dim iFinalVal As Integer
        Select Case Setting_Graph_UpdateTimeMode
          Case TimeMode.Seconds
            If Setting_Graph_UpdateTime > 1 Then
              If m_CoreData(i).graphAvgCount = Setting_Graph_UpdateTime Then
                iFinalVal = m_CoreData(i).graphAvgVals / m_CoreData(i).graphAvgCount
                m_CoreData(i).graphAvgVals = 0
                m_CoreData(i).graphAvgCount = 0
                bUpdateNow = True
              Else
                m_CoreData(i).graphAvgVals += iCPU
                m_CoreData(i).graphAvgCount += 1
              End If
            Else
              iFinalVal = iCPU
              bUpdateNow = True
            End If
          Case TimeMode.Minutes
            If m_CoreData(i).graphAvgCount = (Setting_Graph_UpdateTime * 60) Then
              iFinalVal = m_CoreData(i).graphAvgVals / m_CoreData(i).graphAvgCount
              m_CoreData(i).graphAvgVals = 0
              m_CoreData(i).graphAvgCount = 0
              bUpdateNow = True
            Else
              m_CoreData(i).graphAvgVals += iCPU
              m_CoreData(i).graphAvgCount += 1
            End If
          Case TimeMode.Hours
            If m_CoreData(i).graphAvgCount = (Setting_Graph_UpdateTime * 3600) Then
              iFinalVal = m_CoreData(i).graphAvgVals / m_CoreData(i).graphAvgCount
              m_CoreData(i).graphAvgVals = 0
              m_CoreData(i).graphAvgCount = 0
              bUpdateNow = True
            Else
              m_CoreData(i).graphAvgVals += iCPU
              m_CoreData(i).graphAvgCount += 1
            End If
        End Select
        If bUpdateNow = True Then
          m_CoreData(i).graphHistory.RemoveAt(0)
          m_CoreData(i).graphHistory.Add(iFinalVal)
          bWindowIsDirty = True
        End If
      End If

    Next

    iCPUAvg = FormatNumber(iCPUAvg / m_CoreData.Count, 0)
    If bNoCoresVisible Then
      sText = iCPUAvg & "%"
    Else
      sText = sText.Substring(0, sText.LastIndexOf(" | "))
    End If
    sTooltipText = "CPU: " & iCPUAvg & "%" & vbCrLf & vbCrLf & sTooltipText

    If m_Text <> sText Then
      m_Text = sText
      bWindowIsDirty = True
    Else
      bWindowIsDirty = False
    End If

    ' Get Top Processes
    ' Includes fix to save CPU when tooltip is not shown
    ' TODO: Find a way to cut down on CPU usage when doing this... ironic, huh?
    If Setting_ShowTopProcesses = True AndAlso Tooltip.GetTooltipOwnerGUID = InfoBarModuleGUID Then
      Dim pi As ProcessInfo
      For Each pi In m_Processes
        pi.IsDirty = True
      Next

      Try
        Dim pcc As New PerformanceCounterCategory("Process")
        Dim idc As InstanceDataCollection = pcc.ReadCategory("% Processor Time")
        For Each id As InstanceData In idc.Values
          ' Don't include _Total or Idle
          If (id.InstanceName <> "_Total" AndAlso id.InstanceName <> "Idle") Then
            ' Grab our ProcessInfo class
            If m_Processes.Contains(id.InstanceName) = False Then
              pi = New ProcessInfo
              pi.Name = id.InstanceName
              pi.Counter = New PerformanceCounter("Process", "% Processor Time", id.InstanceName)
              m_Processes.Add(pi, id.InstanceName)
            Else
              pi = m_Processes(Key:=id.InstanceName)
            End If
            m_Processes(Key:=id.InstanceName).Usage = pi.Counter.NextValue
            m_Processes(Key:=id.InstanceName).isDirty = False
          End If
        Next
      Catch ex As Exception
      End Try

      ' Clean Collection
      For Each pi In m_Processes
        If pi.IsDirty = True Then m_Processes.Remove(Key:=pi.Name)
      Next

      Utilities.SortCollection(m_Processes, "Usage", False, "Name")

      sTooltipText &= vbCrLf & "Top Processes:"

      Dim p As Integer, iTotal As Integer
      If Setting_NumberOfTopProcessesToShow > m_Processes.Count Then
        iTotal = m_Processes.Count
      Else
        iTotal = Setting_NumberOfTopProcessesToShow
      End If
      For p = 1 To iTotal
        If m_Processes(Index:=p).Usage <> 0 Then
          ' Format Process Name
          Dim sName As String = m_Processes(Index:=p).Name
          ' If we have a "#2" or similar, then strip it
          Dim pIndex As Integer = sName.IndexOf("#")
          If pIndex > -1 Then sName = sName.Substring(0, pIndex)

          sTooltipText &= vbCrLf & sName & " --- " & m_Processes(Index:=p).Usage & "%"
        End If
      Next
    End If

    If m_TooltipText <> sTooltipText Then
      m_TooltipText = sTooltipText
      DrawTooltip("CPUUsage")
    End If

    Return bWindowIsDirty
  End Function

#End Region

End Class
