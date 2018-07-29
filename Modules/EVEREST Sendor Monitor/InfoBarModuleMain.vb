Public Class InfoBarModuleMain
  ' We inherit the base class. We DO NOT return any of the base classes values.
  Inherits InfoBarModule

  ' Unique ID. If same as other module, the first module will be loaded, the next will be ignored.
  Private Const InfoBarModuleGUID As String = "{1DEA3385-33F2-4a68-87D0-ECAA9C753E21}"

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
  Private m_Text As String
  Private m_TooltipText As String
  Private m_TimerTickCount As Integer
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
      Return "This module displays information about sensors built into your computer's components. It uses an application called EVEREST to get this information. EVEREST must be running for this module to work."
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
    Everest_Update()
  End Sub

  ' InfoBar will call this when your module is disabled.
  Public Overrides Sub FinalizeModule()
    '
  End Sub

  ' InfoBar will call this every 1 second. Make sure to do work only when needed.
  ' TIP: If m_Enabled is set to false, don't do any work to save CPU time unless really needed.
  Public Overrides Sub TimerTick(ByRef bModuleIsDirty As Boolean)
    If m_Enabled = False Then Exit Sub

    ' We want to update information every 5 seconds.
    m_TimerTickCount += 1
    If m_TimerTickCount = 5 Then
      m_TimerTickCount = 0
      bModuleIsDirty = Everest_Update()
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
        ico = IconTheme.GetThemeIcon(InfoBarModuleGUID, "EVEREST")
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

    If width < m_Bounds.Width Then width = m_Bounds.Width

    ' Draw Module
    If width <= 1 Or height <= 1 Then Exit Sub
    Dim bmpTemp As New Bitmap(width, height, System.Drawing.Imaging.PixelFormat.Format32bppArgb)
    Dim GR As Graphics = Graphics.FromImage(bmpTemp)

    Dim curX As Integer = 0, lHeight As Integer = bmpTemp.Height

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

  ' InfoBar will call this when it needs to draw your tooltip.
  ' Do all of your graphics or text drawing here.
  Private Sub DrawTooltip(ByVal ObjectID As String)
    Select Case ObjectID
      Case "EVERESTSensorMonitor"
        If m_TooltipText = vbNullString Then Exit Select

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

  ' Check to see if the mouse is on any of your UI rectangles.
  ' Set bWindowIsDirty to true if you need a redraw.
  Public Overrides Sub ProcessMouseMove(ByVal e As System.Windows.Forms.MouseEventArgs, ByRef bWindowIsDirty As Boolean)
    Tooltip.SetTooltipOwner(InfoBarModuleGUID, "EVERESTSensorMonitor")
    DrawTooltip("EVERESTSensorMonitor")
    Tooltip.UpdateTooltip()
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
    MainMenu.AddMenuItem(InfoBarModuleGUID & "::" & "SETTINGS", "EVEREST Sensor Monitor Settings...", My.Resources.icon, True, True)

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
    Setting_ShowIcon = m_SettingsDialog.chkShowIcon.Checked
    Setting_ShowText = m_SettingsDialog.chkShowText.Checked

    Try

      For Each entry As EverestDataEntry In EverestData.Temperatures
        entry.Enabled = (m_SettingsDialog.lvItems.Items(entry.ID).SubItems(2).Text = "True")
        entry.ShowOnToolbar = (m_SettingsDialog.lvItems.Items(entry.ID).SubItems(3).Text = "True")
        entry.ShowOnTooltip = (m_SettingsDialog.lvItems.Items(entry.ID).SubItems(4).Text = "True")
      Next

      For Each entry As EverestDataEntry In EverestData.FanSpeeds
        entry.Enabled = (m_SettingsDialog.lvItems.Items(entry.ID).SubItems(2).Text = "True")
        entry.ShowOnToolbar = (m_SettingsDialog.lvItems.Items(entry.ID).SubItems(3).Text = "True")
        entry.ShowOnTooltip = (m_SettingsDialog.lvItems.Items(entry.ID).SubItems(4).Text = "True")
      Next

      For Each entry As EverestDataEntry In EverestData.Voltages
        entry.Enabled = (m_SettingsDialog.lvItems.Items(entry.ID).SubItems(2).Text = "True")
        entry.ShowOnToolbar = (m_SettingsDialog.lvItems.Items(entry.ID).SubItems(3).Text = "True")
        entry.ShowOnTooltip = (m_SettingsDialog.lvItems.Items(entry.ID).SubItems(4).Text = "True")
      Next

    Catch ex As Exception
    End Try

    m_Bounds.Width = 1
    Everest_Update()
    DrawModule()
  End Sub

  ' InfoBar will call this when the application loads. Use this to set your internal settings.
  Public Overrides Sub LoadSettings(ByRef Doc As XmlDocument)
    ' Always set defaults first.
    Setting_ShowIcon = True
    Setting_ShowText = True

    ' We'll need to call these to properly show settings.
    Everest_ResetData()
    Everest_GetData()

    ' Now use the InfoBar Settings Service to load your settings.
    If Doc IsNot Nothing Then
      Setting_ShowIcon = Settings.GetSetting(Doc, "showicon", "True")
      Setting_ShowText = Settings.GetSetting(Doc, "showtext", "True")

      Dim Node As XmlNode = Doc.DocumentElement.SelectSingleNode("values/temp")
      If Node IsNot Nothing Then
        If Node.ChildNodes.Count > 0 Then
          For Each childNode As XmlNode In Node.ChildNodes
            If EverestData.Temperatures.Contains(childNode.Name) = False Then
              Dim ed As New EverestDataEntry
              ed.ID = childNode.Name
              ed.IsDirty = True
              ed.Enabled = childNode.Attributes("enabled").Value
              ed.ShowOnToolbar = childNode.Attributes("toolbar").Value
              ed.ShowOnTooltip = childNode.Attributes("tooltip").Value
              EverestData.Temperatures.Add(ed, ed.ID)
            Else
              EverestData.Temperatures.Item(childNode.Name).Enabled = childNode.Attributes("enabled").Value
              EverestData.Temperatures.Item(childNode.Name).ShowOnToolbar = childNode.Attributes("toolbar").Value
              EverestData.Temperatures.Item(childNode.Name).ShowOnTooltip = childNode.Attributes("tooltip").Value
            End If
          Next
        End If
      End If

      Node = Doc.DocumentElement.SelectSingleNode("values/fan")
      If Node IsNot Nothing Then
        If Node.ChildNodes.Count > 0 Then
          For Each childNode As XmlNode In Node.ChildNodes
            If EverestData.FanSpeeds.Contains(childNode.Name) = False Then
              Dim ed As New EverestDataEntry
              ed.ID = childNode.Name
              ed.IsDirty = True
              ed.Enabled = childNode.Attributes("enabled").Value
              ed.ShowOnToolbar = childNode.Attributes("toolbar").Value
              ed.ShowOnTooltip = childNode.Attributes("tooltip").Value
              EverestData.FanSpeeds.Add(ed, ed.ID)
            Else
              EverestData.FanSpeeds.Item(childNode.Name).Enabled = childNode.Attributes("enabled").Value
              EverestData.FanSpeeds.Item(childNode.Name).ShowOnToolbar = childNode.Attributes("toolbar").Value
              EverestData.FanSpeeds.Item(childNode.Name).ShowOnTooltip = childNode.Attributes("tooltip").Value
            End If
          Next
        End If
      End If

      Node = Doc.DocumentElement.SelectSingleNode("values/volt")
      If Node IsNot Nothing Then
        If Node.ChildNodes.Count > 0 Then
          For Each childNode As XmlNode In Node.ChildNodes
            If EverestData.Voltages.Contains(childNode.Name) = False Then
              Dim ed As New EverestDataEntry
              ed.ID = childNode.Name
              ed.IsDirty = False
              ed.Enabled = childNode.Attributes("enabled").Value
              ed.ShowOnToolbar = childNode.Attributes("toolbar").Value
              ed.ShowOnTooltip = childNode.Attributes("tooltip").Value
              EverestData.Voltages.Add(ed, ed.ID)
            Else
              EverestData.Voltages.Item(childNode.Name).Enabled = childNode.Attributes("enabled").Value
              EverestData.Voltages.Item(childNode.Name).ShowOnToolbar = childNode.Attributes("toolbar").Value
              EverestData.Voltages.Item(childNode.Name).ShowOnTooltip = childNode.Attributes("tooltip").Value
            End If
          Next
        End If
      End If
    End If

    ResetSettings()
  End Sub

  Public Overrides Sub ResetSettings()
    m_SettingsDialog.Caller = Me
    m_SettingsDialog.chkShowIcon.Checked = Setting_ShowIcon
    m_SettingsDialog.chkShowText.Checked = Setting_ShowText
    m_SettingsDialog.lvItems.Items.Clear()

    Dim LI As ListViewItem
    For Each entry As EverestDataEntry In EverestData.Temperatures
      If m_SettingsDialog.lvItems.Items.ContainsKey(entry.ID) = False Then
        LI = New ListViewItem
        LI.Name = entry.ID
        LI.Text = entry.Label
        LI.SubItems.Add(entry.Value & "°C")

        Dim lvsi As New ListViewItem.ListViewSubItem
        lvsi.Name = "Enabled_" & LI.Name
        lvsi.Text = entry.Enabled.ToString
        LI.SubItems.Add(lvsi)

        lvsi = New ListViewItem.ListViewSubItem
        lvsi.Name = "ShowText_" & LI.Name
        lvsi.Text = entry.ShowOnToolbar.ToString
        LI.SubItems.Add(lvsi)

        lvsi = New ListViewItem.ListViewSubItem
        lvsi.Name = "ShowTooltip_" & LI.Name
        lvsi.Text = entry.ShowOnTooltip.ToString
        LI.SubItems.Add(lvsi)

        LI.Group = m_SettingsDialog.lvItems.Groups("lvgTemps")
        LI.Checked = entry.Enabled
        m_SettingsDialog.lvItems.Items.Add(LI)
      End If
    Next

    For Each entry As EverestDataEntry In EverestData.FanSpeeds
      If m_SettingsDialog.lvItems.Items.ContainsKey(entry.ID) = False Then
        LI = New ListViewItem
        LI.Name = entry.ID
        LI.Text = entry.Label
        LI.SubItems.Add(entry.Value & " RPM")

        Dim lvsi As New ListViewItem.ListViewSubItem
        lvsi.Name = "Enabled_" & LI.Name
        lvsi.Text = entry.Enabled.ToString
        LI.SubItems.Add(lvsi)

        lvsi = New ListViewItem.ListViewSubItem
        lvsi.Name = "ShowText_" & LI.Name
        lvsi.Text = entry.ShowOnToolbar.ToString
        LI.SubItems.Add(lvsi)

        lvsi = New ListViewItem.ListViewSubItem
        lvsi.Name = "ShowTooltip_" & LI.Name
        lvsi.Text = entry.ShowOnTooltip.ToString
        LI.SubItems.Add(lvsi)

        LI.Group = m_SettingsDialog.lvItems.Groups("lvgFans")
        LI.Checked = entry.Enabled
        m_SettingsDialog.lvItems.Items.Add(LI)
      End If
    Next

    For Each entry As EverestDataEntry In EverestData.Voltages
      If m_SettingsDialog.lvItems.Items.ContainsKey(entry.ID) = False Then
        LI = New ListViewItem
        LI.Name = entry.ID
        LI.Text = entry.Label
        LI.SubItems.Add(entry.Value & "V")

        Dim lvsi As New ListViewItem.ListViewSubItem
        lvsi.Name = "Enabled_" & LI.Name
        lvsi.Text = entry.Enabled.ToString
        LI.SubItems.Add(lvsi)

        lvsi = New ListViewItem.ListViewSubItem
        lvsi.Name = "ShowText_" & LI.Name
        lvsi.Text = entry.ShowOnToolbar.ToString
        LI.SubItems.Add(lvsi)

        lvsi = New ListViewItem.ListViewSubItem
        lvsi.Name = "ShowTooltip_" & LI.Name
        lvsi.Text = entry.ShowOnTooltip.ToString
        LI.SubItems.Add(lvsi)

        LI.Group = m_SettingsDialog.lvItems.Groups("lvgVoltages")
        LI.Checked = entry.Enabled
        m_SettingsDialog.lvItems.Items.Add(LI)
      End If
    Next
  End Sub

  ' InfoBar will call this when the application exits. Use this to save your internal settings.
  Public Overrides Sub SaveSettings(ByRef Doc As XmlDocument)
    Dim Node As XmlNode

    ' Show Icon
    Node = Doc.CreateNode(XmlNodeType.Element, "Node", "showicon", "")
    Node.InnerText = Setting_ShowIcon
    Doc.DocumentElement.AppendChild(Node)

    ' Show Text
    Node = Doc.CreateNode(XmlNodeType.Element, "Node", "showtext", "")
    Node.InnerText = Setting_ShowText
    Doc.DocumentElement.AppendChild(Node)

    ' Save Monitoring Values
    If EverestData.Temperatures.Count > 0 Or EverestData.FanSpeeds.Count > 0 Or EverestData.Voltages.Count > 0 Then
      Node = Doc.CreateNode(XmlNodeType.Element, "Node", "values", "")

      If EverestData.Temperatures.Count > 0 Then
        Dim ChildNode As XmlNode = Doc.CreateNode(XmlNodeType.Element, "Node", "temp", "")
        Dim cNode As XmlNode, attrib As XmlAttribute
        For Each entry As EverestDataEntry In EverestData.Temperatures
          cNode = Doc.CreateNode(XmlNodeType.Element, "Node", entry.ID, "")

          attrib = Doc.CreateAttribute("enabled")
          attrib.Value = entry.Enabled
          cNode.Attributes.Append(attrib)

          attrib = Doc.CreateAttribute("toolbar")
          attrib.Value = entry.ShowOnToolbar
          cNode.Attributes.Append(attrib)

          attrib = Doc.CreateAttribute("tooltip")
          attrib.Value = entry.ShowOnTooltip
          cNode.Attributes.Append(attrib)

          ChildNode.AppendChild(cNode)
        Next
        Node.AppendChild(ChildNode)
      End If

      If EverestData.FanSpeeds.Count > 0 Then
        Dim ChildNode As XmlNode = Doc.CreateNode(XmlNodeType.Element, "Node", "fan", "")
        Dim cNode As XmlNode, attrib As XmlAttribute
        For Each entry As EverestDataEntry In EverestData.FanSpeeds
          cNode = Doc.CreateNode(XmlNodeType.Element, "Node", entry.ID, "")

          attrib = Doc.CreateAttribute("enabled")
          attrib.Value = entry.Enabled
          cNode.Attributes.Append(attrib)

          attrib = Doc.CreateAttribute("toolbar")
          attrib.Value = entry.ShowOnToolbar
          cNode.Attributes.Append(attrib)

          attrib = Doc.CreateAttribute("tooltip")
          attrib.Value = entry.ShowOnTooltip
          cNode.Attributes.Append(attrib)

          ChildNode.AppendChild(cNode)
        Next
        Node.AppendChild(ChildNode)
      End If

      If EverestData.Voltages.Count > 0 Then
        Dim ChildNode As XmlNode = Doc.CreateNode(XmlNodeType.Element, "Node", "volt", "")
        Dim cNode As XmlNode, attrib As XmlAttribute
        For Each entry As EverestDataEntry In EverestData.Voltages
          cNode = Doc.CreateNode(XmlNodeType.Element, "Node", entry.ID, "")

          attrib = Doc.CreateAttribute("enabled")
          attrib.Value = entry.Enabled
          cNode.Attributes.Append(attrib)

          attrib = Doc.CreateAttribute("toolbar")
          attrib.Value = entry.ShowOnToolbar
          cNode.Attributes.Append(attrib)

          attrib = Doc.CreateAttribute("tooltip")
          attrib.Value = entry.ShowOnTooltip
          cNode.Attributes.Append(attrib)

          ChildNode.AppendChild(cNode)
        Next
        Node.AppendChild(ChildNode)
      End If

      Doc.DocumentElement.AppendChild(Node)
    End If
  End Sub

#End Region

#Region "EVEREST Related Functions"

  Public Class EverestDataEntry
    Public ID As String
    Public Label As String
    Public Value As String
    Public Enabled As Boolean
    Public ShowOnToolbar As Boolean
    Public ShowOnTooltip As Boolean
    Public IsDirty As Boolean
  End Class

  Public Class EverestDataType
    Public Temperatures As New Collection
    Public FanSpeeds As New Collection
    Public Voltages As New Collection
  End Class

  Public EverestData As New EverestDataType

#Region "Windows API"
  Private Const FILE_MAP_READ As Integer = &H4
  Private Declare Function OpenFileMappingA Lib "kernel32.dll" (ByVal dwDesiredAccess As UInteger, ByVal bInheritHandle As Boolean, ByVal lpName As String) As IntPtr
  Private Declare Function MapViewOfFile Lib "kernel32.dll" (ByVal hFileMappingObject As IntPtr, ByVal dwDesiredAccess As UInteger, ByVal dwFileOffsetHigh As UInteger, ByVal dwFileOffsetLow As UInteger, ByVal dwNumberOfBytesToMap As IntPtr) As IntPtr
  Private Declare Function UnmapViewOfFile Lib "kernel32.dll" (ByVal lpBaseAddress As IntPtr) As Boolean
  Private Declare Function CloseHandle Lib "kernel32.dll" (ByVal hObject As IntPtr) As Boolean
#End Region

  Public Sub Everest_ResetData()
    EverestData.FanSpeeds.Clear()
    EverestData.Temperatures.Clear()
    EverestData.Voltages.Clear()
  End Sub

  Public Function Everest_GetData() As Boolean
    Dim mappedData As IntPtr
    Dim th As IntPtr
    Dim bHaveData As Boolean
    Dim sOutput As String = vbNullString

    th = OpenFileMappingA(FILE_MAP_READ, False, "EVEREST_SensorValues")
    If th <> IntPtr.Zero Then
      mappedData = MapViewOfFile(th, FILE_MAP_READ, 0, 0, 0)
      If mappedData <> IntPtr.Zero Then
        sOutput = Marshal.PtrToStringAnsi(mappedData)
        If UnmapViewOfFile(mappedData) Then
          bHaveData = True
        Else
          bHaveData = False
        End If
      Else
        bHaveData = False
      End If
      CloseHandle(th)
    Else
      bHaveData = False
    End If

    If bHaveData Then

      Dim Doc As New XmlDocument
      Doc.LoadXml("<?xml version='1.0' ?><everest>" & sOutput & "</everest>")

      Dim sID As String, sName As String, sValue As String
      Dim newItem As EverestDataEntry

      ' Get Temperatures
      For Each entry As EverestDataEntry In EverestData.Temperatures
        entry.IsDirty = True
      Next
      Dim tempNodes As XmlNodeList = Doc.SelectNodes("/everest/temp")
      If tempNodes IsNot Nothing Then
        For Each Node As XmlNode In tempNodes
          sID = Node.SelectSingleNode("id").InnerText
          sName = Node.SelectSingleNode("label").InnerText
          sValue = Node.SelectSingleNode("value").InnerText
          If EverestData.Temperatures.Contains(sID) Then
            EverestData.Temperatures.Item(sID).Label = sName
            EverestData.Temperatures.Item(sID).Value = sValue
            EverestData.Temperatures.Item(sID).IsDirty = False
          Else
            newItem = New EverestDataEntry
            newItem.ID = sID
            newItem.Label = sName
            newItem.Value = sValue
            newItem.IsDirty = False
            EverestData.Temperatures.Add(newItem, sID)
          End If
        Next
        For Each entry As EverestDataEntry In EverestData.Temperatures
          If entry.IsDirty = True Then EverestData.Temperatures.Remove(entry.ID)
        Next
      End If

      ' Get Fan Speeds
      For Each entry As EverestDataEntry In EverestData.FanSpeeds
        entry.IsDirty = True
      Next
      Dim fanNodes As XmlNodeList = Doc.SelectNodes("/everest/fan")
      If fanNodes IsNot Nothing Then
        For Each node As XmlNode In fanNodes
          sID = node.SelectSingleNode("id").InnerText
          sName = node.SelectSingleNode("label").InnerText
          sValue = node.SelectSingleNode("value").InnerText
          If EverestData.FanSpeeds.Contains(sID) Then
            EverestData.FanSpeeds.Item(sID).Label = sName
            EverestData.FanSpeeds.Item(sID).Value = sValue
            EverestData.FanSpeeds.Item(sID).IsDirty = False
          Else
            newItem = New EverestDataEntry
            newItem.ID = sID
            newItem.Label = sName
            newItem.Value = sValue
            newItem.IsDirty = False
            EverestData.FanSpeeds.Add(newItem, sID)
          End If
        Next
      End If
      For Each entry As EverestDataEntry In EverestData.FanSpeeds
        If entry.IsDirty = True Then EverestData.FanSpeeds.Remove(entry.ID)
      Next

      ' Get Voltages
      For Each entry As EverestDataEntry In EverestData.Voltages
        entry.IsDirty = True
      Next
      Dim voltNodes As XmlNodeList = Doc.SelectNodes("/everest/volt")
      If voltNodes IsNot Nothing Then
        For Each node As XmlNode In voltNodes
          sID = node.SelectSingleNode("id").InnerText
          sName = node.SelectSingleNode("label").InnerText
          sValue = node.SelectSingleNode("value").InnerText
          If EverestData.Voltages.Contains(sID) Then
            EverestData.Voltages.Item(sID).Label = sName
            EverestData.Voltages.Item(sID).Value = sValue
            EverestData.Voltages.Item(sID).IsDirty = False
          Else
            newItem = New EverestDataEntry
            newItem.ID = sID
            newItem.Label = sName
            newItem.Value = sValue
            newItem.IsDirty = False
            EverestData.Voltages.Add(newItem, sID)
          End If
        Next
      End If
      For Each entry As EverestDataEntry In EverestData.Voltages
        If entry.IsDirty = True Then EverestData.Voltages.Remove(entry.ID)
      Next

      Return True
    Else
      Return False
    End If
  End Function

  Private Function Everest_Update() As Boolean
    Dim iEnabled As Integer
    Dim tempText As String, tempTooltip As String
    Dim sText As String = vbNullString, sTooltip As String = vbNullString

    Dim bHaveData As Boolean = Everest_GetData()
    If bHaveData Then

      If EverestData.Temperatures.Count > 0 Then
        iEnabled = 0
        For Each entry As EverestDataEntry In EverestData.Temperatures
          If entry.Enabled = True Then iEnabled += 1
        Next
        If iEnabled > 0 Then
          tempText = vbNullString
          tempTooltip = vbNullString
          For Each e As EverestDataEntry In EverestData.Temperatures
            If e.Enabled = True Then
              If e.ShowOnToolbar = True Then tempText &= e.Label & ": " & e.Value & "°C, "
              If e.ShowOnTooltip = True Then tempTooltip &= e.Label & ": " & e.Value & "°C" & vbCrLf
            End If
          Next
          If tempText <> vbNullString Then sText = tempText
          If tempTooltip <> vbNullString Then sTooltip = "Temperatures:" & vbCrLf & tempTooltip
        End If
      End If

      If EverestData.FanSpeeds.Count > 0 Then
        iEnabled = 0
        For Each entry As EverestDataEntry In EverestData.FanSpeeds
          If entry.Enabled = True Then iEnabled += 1
        Next
        If iEnabled > 0 Then
          tempText = vbNullString
          tempTooltip = vbNullString
          For Each e As EverestDataEntry In EverestData.FanSpeeds
            If e.Enabled = True Then
              If e.ShowOnToolbar = True Then tempText &= e.Label & ": " & e.Value & " RPM, "
              If e.ShowOnTooltip = True Then tempTooltip &= e.Label & ": " & e.Value & " RPM" & vbCrLf
            End If
          Next
          If tempText <> vbNullString Then sText &= tempText
          If tempTooltip <> vbNullString Then
            If sTooltip <> vbNullString Then sTooltip &= vbCrLf
            sTooltip &= "Fan Speeds:" & vbCrLf & tempTooltip
          End If
        End If
      End If

      If EverestData.Voltages.Count > 0 Then
        iEnabled = 0
        For Each entry As EverestDataEntry In EverestData.Voltages
          If entry.Enabled = True Then iEnabled += 1
        Next
        If iEnabled > 0 Then
          tempText = vbNullString
          tempTooltip = vbNullString
          For Each e As EverestDataEntry In EverestData.Voltages
            If e.Enabled = True Then
              If e.ShowOnToolbar = True Then tempText &= e.Label & ": " & e.Value & "V, "
              If e.ShowOnTooltip = True Then tempTooltip &= e.Label & ": " & e.Value & "V" & vbCrLf
            End If
          Next
          If tempText <> vbNullString Then sText &= tempText
          If tempTooltip <> vbNullString Then
            If sTooltip <> vbNullString Then sTooltip &= vbCrLf
            sTooltip &= "Voltages:" & vbCrLf & tempTooltip
          End If
        End If
      End If

      If sText IsNot Nothing Then
        If sText.EndsWith(", ") Then sText = sText.Substring(0, sText.Length - 2)
      End If

    Else

      sText = vbNullString
      sTooltip = "EVEREST is not running, or an unknown error has occured."

    End If

    Dim bDirty As Boolean = False

    If m_Text <> sText Then
      m_Text = sText
      DrawModule()
      bDirty = True
    End If

    If sTooltip Is Nothing Then sTooltip = "Sensor output is available, please check your settings."
    If m_TooltipText <> sTooltip Then
      m_TooltipText = sTooltip
      If Tooltip.GetTooltipOwnerGUID = InfoBarModuleGUID Then
        DrawTooltip(Tooltip.GetTooltipOwnerObjectID)
        Tooltip.UpdateTooltip()
      End If
    End If

    Return bDirty
  End Function

#End Region

End Class