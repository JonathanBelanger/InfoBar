Public Class InfoBarModuleMain
  Inherits InfoBarModule

  ' Unique ID. If same as other module, the first module will be loaded, the next will be ignored.
  Private Const InfoBarModuleGUID As String = "{D03C4859-3681-27ca-6595-3B3223AB6FA1}"

#Region "Windows API"
  Private Declare Function ShellExecute Lib "shell32.dll" Alias "ShellExecuteA" (ByVal hWnd As IntPtr, ByVal lpOperation As String, ByVal lpFile As String, ByVal lpParameters As String, ByVal lpDirectory As String, ByVal nShowCmd As Integer) As IntPtr
#End Region

#Region "InfoBar Service Objects"
  Private Skin As New InfoBar.Services.Skinning
  Private Tooltip As New InfoBar.Services.Tooltips
  Private IconTheme As New InfoBar.Services.IconTheme
  Private MainMenu As New InfoBar.Services.PopupMenus.MainMenu
  Private Settings As New InfoBar.Services.Settings
#End Region

#Region "Private Variables"
  Private m_ModuleToolbarBitmap As Bitmap
  Private m_ModuleTooltipBitmap As Bitmap
  Private m_SettingsDialog As New Settings
  Private m_Enabled As Boolean
  Private m_ModuleBounds As Rectangle
  Private m_DefaultTooltipText As String = "To add items to Quick Launch, just drag and drop them here, or right click and select add file or add folder."
  Private m_RightClickOnItem As Integer

  Public m_Items As New Collection
#End Region

#Region "Settings Variables"
  ' 0 = Icons Only, 1 = Text Only, 2 = Icons And Text, 3 = Icons For Files, Icons and Text for Folders
  Dim Setting_ButtonDisplay As Integer
  Dim Setting_ShowFoldersAsDropDownMenus As Boolean
  Dim Setting_DisplayPathInTooltip As Boolean
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
      Return "Displays shortcuts to applications, web sites and more. Drag and drop files and folders to add to Quick Launch."
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
    DrawModule()
  End Sub

  Public Overrides Sub FinalizeModule()
    '
  End Sub

  Public Overrides Sub TimerTick(ByRef bModuleIsDirty As Boolean)
    '
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
  Public Sub DrawModule()
    Dim width As Integer = 0, height As Integer = Skin.MaxModuleHeight
    Dim Gr As Graphics = Graphics.FromHwnd(IntPtr.Zero)

    If m_Items.Count = 0 Then
      ' Measure Icon
      width += 16 + 2

      ' Measure Text
      Dim trt As Rectangle
      trt = Skin.MeasureText(Gr, "Quick Launch", Skinning.SkinTextPart.BackgroundText)
      width += trt.Width
    Else
      ' Calculate width
      For Each Item As QuickLaunchItem In m_Items
        Dim trb As Rectangle
        Dim iIcon As Image = Item.Icon
        Dim sText As String = Item.Title
        If Setting_ButtonDisplay = 1 Then iIcon = Nothing
        If Setting_ButtonDisplay = 0 Or (Setting_ButtonDisplay = 3 AndAlso Item.IsFolder = False) Then sText = vbNullString
        trb = Skin.MeasureButton(Gr, trb, iIcon, sText, Item.ButtonState, Item.IsFolder And Setting_ShowFoldersAsDropDownMenus)
        width += trb.Width
      Next
    End If

    Dim curX As Integer = 0

    If width <= 1 Or height <= 1 Then Exit Sub
    Dim bmpTemp As New Bitmap(width, height, System.Drawing.Imaging.PixelFormat.Format32bppArgb)
    Gr = Graphics.FromImage(bmpTemp)

    If m_Items.Count = 0 Then
      ' Draw Icon
      Gr.DrawImage(My.Resources.icon, curX, CInt((height - My.Resources.icon.Height) / 2), My.Resources.icon.Width, My.Resources.icon.Height)
      curX += My.Resources.icon.Width + 2

      ' Draw Text
      Dim trt As New Rectangle
      trt = Skin.MeasureText(Gr, "Quick Launch", Skinning.SkinTextPart.BackgroundText)
      trt.X = curX : trt.Y = 0 : trt.Height = height
      Skin.DrawText(Gr, "Quick Launch", trt, Skinning.SkinTextPart.BackgroundText, StringAlignment.Near, StringAlignment.Center)

      curX += trt.Width
    Else
      ' Draw Items
      For Each Item As QuickLaunchItem In m_Items
        Dim tri As New Rectangle(curX, 0, 0, height)
        Dim iIcon As Image = Item.Icon
        Dim sText As String = Item.Title
        If Setting_ButtonDisplay = 1 Then iIcon = Nothing
        If (Setting_ButtonDisplay = 0) Or (Setting_ButtonDisplay = 3 AndAlso Item.IsFolder = False) Then sText = vbNullString
        tri = Skin.DrawButton(Gr, tri, iIcon, sText, Item.ButtonState, Item.IsFolder And Setting_ShowFoldersAsDropDownMenus)
        Item.Bounds = tri
        curX += tri.Width
      Next
    End If

    ' Cache our module's bounding rectangle for future use.
    m_ModuleBounds = New Rectangle(0, 0, curX, height)

    Gr.Dispose()
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

  Private Sub DrawTooltip(ByVal ObjectID As String)
    Dim sText As String
    If ObjectID = "QuickLaunch" Then
      sText = m_DefaultTooltipText
    Else
      Dim aObjectID() As String = ObjectID.Split("_")
      Dim iIndex As Integer = CInt(aObjectID(1))
      sText = m_Items(iIndex).Title
      If Setting_DisplayPathInTooltip = True Then sText &= vbCrLf & vbCrLf & m_Items(iIndex).Path
    End If

    Dim tr As New Rectangle
    Dim GR As Graphics = Graphics.FromHwnd(IntPtr.Zero)
    tr = Skin.MeasureText(GR, sText, Skinning.SkinTextPart.TooltipText)
    GR.Dispose()

    Dim bmpTemp As New Bitmap(tr.Width, tr.Height, System.Drawing.Imaging.PixelFormat.Format32bppArgb)
    GR = Graphics.FromImage(bmpTemp)
    Skin.DrawText(GR, sText, tr, Skinning.SkinTextPart.TooltipText, StringAlignment.Near, StringAlignment.Near)

    GR.Dispose()
    m_ModuleTooltipBitmap = bmpTemp.Clone
    bmpTemp.Dispose()
  End Sub

#End Region

#Region "Mouse/Keyboard/Menu Processing"

  Public Overrides Sub ProcessMouseMove(ByVal e As System.Windows.Forms.MouseEventArgs, ByRef bWindowIsDirty As Boolean)
    Dim pt As Point = Skin.ToolbarPointToModulePoint(e.Location, m_ModuleBounds)

    If m_Items.Count > 0 Then
      For I As Integer = 1 To m_Items.Count
        If m_Items(I).Bounds.Contains(pt) Then
          If e.Button = MouseButtons.Left Then
            If m_Items(I).ButtonState <> 2 Then
              m_Items(I).ButtonState = 2
              bWindowIsDirty = True
            End If
          ElseIf e.Button = MouseButtons.None Then
            If m_Items(I).ButtonState <> 1 Then
              m_Items(I).ButtonState = 1
              bWindowIsDirty = True
            End If
            Tooltip.SetTooltipOwner(InfoBarModuleGUID, "ITEMINDEX_" & I)
            DrawTooltip("ITEMINDEX_" & I)
          Else
            If m_Items(I).ButtonState <> 0 Then
              m_Items(I).ButtonState = 0
              bWindowIsDirty = True
            End If
          End If
        Else
          If m_Items(I).ButtonState <> 0 Then
            m_Items(I).ButtonState = 0
            bWindowIsDirty = True
          End If
        End If
      Next
    Else
      If m_ModuleBounds.Contains(pt) Then
        Tooltip.SetTooltipOwner(InfoBarModuleGUID, "QuickLaunch")
        DrawTooltip("QuickLaunch")
      End If
    End If

    If bWindowIsDirty Then DrawModule()
  End Sub

  Public Overrides Sub ProcessMouseUp(ByVal e As MouseEventArgs, ByRef bWindowIsDirty As Boolean)
    Dim pt As Point = Skin.ToolbarPointToModulePoint(e.Location, m_ModuleBounds)
    m_RightClickOnItem = -1
    If m_Items.Count > 0 Then
      For I As Integer = 1 To m_Items.Count
        If m_Items(I).Bounds.Contains(pt) Then
          If e.Button = MouseButtons.Left Then
            If m_Items(I).ButtonState <> 1 Then
              m_Items(I).ButtonState = 1
              bWindowIsDirty = True
              If m_Items(I).IsFolder = True AndAlso Setting_ShowFoldersAsDropDownMenus = True Then
                ' Show Dropdown?
              Else
                ' Execute Item
                ShellExecute(IntPtr.Zero, "open", m_Items(I).Path, m_Items(I).CommandLineArgs, m_Items(I).WorkingDirectory, AppWinStyle.NormalFocus)
              End If
            End If
          ElseIf e.Button = MouseButtons.Right Then
            m_RightClickOnItem = I
            If m_Items(I).ButtonState <> 0 Then
              m_Items(I).ButtonState = 0
              bWindowIsDirty = True
            End If
          Else
            If m_Items(I).ButtonState <> 1 Then
              m_Items(I).ButtonState = 1
              bWindowIsDirty = True
            End If
          End If
        Else
          If m_Items(I).ButtonState <> 0 Then
            m_Items(I).ButtonState = 0
            bWindowIsDirty = True
          End If
        End If
      Next
    End If

    If bWindowIsDirty Then DrawModule()
  End Sub

  Public Overrides Sub ProcessMouseDown(ByVal e As MouseEventArgs, ByRef bWindowIsDirty As Boolean)
    Dim pt As Point = Skin.ToolbarPointToModulePoint(e.Location, m_ModuleBounds)
    If m_Items.Count > 0 Then
      For I As Integer = 1 To m_Items.Count
        If m_Items(I).Bounds.Contains(pt) Then
          If m_Items(I).ButtonState <> 2 Then
            m_Items(I).ButtonState = 2
            bWindowIsDirty = True
          End If
        Else
          If m_Items(I).ButtonState <> 0 Then
            m_Items(I).ButtonState = 0
            bWindowIsDirty = True
          End If
        End If
      Next
    End If
    If bWindowIsDirty Then DrawModule()
  End Sub

  Public Overrides Sub ProcessMouseLeave(ByRef bWindowIsDirty As Boolean)
    If m_Items.Count > 0 Then
      For I As Integer = 1 To m_Items.Count
        If m_Items(I).ButtonState <> 0 Then
          m_Items(I).ButtonState = 0
          bWindowIsDirty = True
        End If
      Next
    End If
    If bWindowIsDirty Then DrawModule()
  End Sub

  Public Overrides Sub ProcessDragDrop(ByVal e As System.Windows.Forms.DragEventArgs, ByRef bWindowIsDirty As Boolean)
    Dim pt As Point = Skin.ToolbarPointToModulePoint(New Point(e.X, e.Y), m_ModuleBounds)
    If m_ModuleBounds.Contains(pt) Then
      'Dim sFormats() As String = e.Data.GetFormats()
      'Dim sText As String = vbNullString
      'For Each sFormat As String In sFormats
      'sText &= sFormat & ", "
      'Next
      'If sText <> vbNullString Then
      'sText = sText.Substring(0, sText.Length - 2)
      'Else
      'sText = "(None)"
      'End If

      If e.Data.GetDataPresent(DataFormats.FileDrop) = True Then
        Dim files() As String = DirectCast(e.Data.GetData(DataFormats.FileDrop), String())
        For Each file As String In files
          If AddFileToItems(file) Then bWindowIsDirty = True
        Next
      ElseIf e.Data.GetDataPresent("UniformResourceLocator", True) = True Then
        Dim sUrl As String = e.Data.GetData(GetType(String))
        If AddURLToItems(sUrl) Then bWindowIsDirty = True
      End If
    End If

    If bWindowIsDirty Then DrawModule()
  End Sub

  Public Overrides Sub ProcessDragEnter(ByVal e As System.Windows.Forms.DragEventArgs, ByRef bWindowIsDirty As Boolean)
    '
  End Sub

  Public Overrides Sub ProcessDragLeave(ByVal e As System.EventArgs, ByRef bWindowIsDirty As Boolean)
    '
  End Sub

  Public Overrides Sub ProcessDragOver(ByVal e As System.Windows.Forms.DragEventArgs, ByRef bWindowIsDirty As Boolean)
    Dim pt As Point = Skin.ToolbarPointToModulePoint(New Point(e.X, e.Y), m_ModuleBounds)
    If m_ModuleBounds.Contains(pt) Then
      ' Check for supported data formats
      If e.Data.GetDataPresent(DataFormats.FileDrop) = True Then
        e.Effect = DragDropEffects.Copy
      ElseIf e.Data.GetDataPresent("UniformResourceLocator", True) = True Then
        e.Effect = DragDropEffects.Copy
      Else
        e.Effect = DragDropEffects.None
      End If
    Else
      e.Effect = DragDropEffects.None
    End If
  End Sub

  Public Overrides Property ModuleBounds() As System.Drawing.Rectangle
    Get
      Return m_ModuleBounds
    End Get
    Set(ByVal value As System.Drawing.Rectangle)
      m_ModuleBounds = value
    End Set
  End Property

  Public Overrides Sub AddMainPopupMenuItems()
    ' A recommended standard feature is to allow users to access your module's settings
    ' dialog from the popup menu.

    Dim addFileIcon As Image, addFolderIcon As Image, editIcon As Image, quicklaunchIcon As Image
    Dim deleteFileIcon As Image, deleteFolderIcon As Image, addURLIcon As Image
    If IconTheme.IsDefaultTheme = True Then
      addFileIcon = IconTheme.GetSystemIconByExtension("File")
      addFolderIcon = IconTheme.GetSystemIconByExtension("Folder")
      addURLIcon = IconTheme.GetSystemIconByExtension(".htm")
      editIcon = My.Resources.edit
      deleteFileIcon = My.Resources.file_delete
      deleteFolderIcon = My.Resources.folder_delete
      quicklaunchIcon = My.Resources.icon
    Else
      addFileIcon = IconTheme.GetThemeIcon(InfoBarModuleGUID, "File")
      If addFileIcon Is Nothing Then addFileIcon = IconTheme.GetSystemIconByExtension("File")

      addFolderIcon = IconTheme.GetThemeIcon(InfoBarModuleGUID, "Folder")
      If addFolderIcon Is Nothing Then addFolderIcon = IconTheme.GetSystemIconByExtension("Folder")

      addURLIcon = IconTheme.GetThemeIcon(InfoBarModuleGUID, "URL")
      If addURLIcon Is Nothing Then addURLIcon = IconTheme.GetSystemIconByExtension(".htm")

      editIcon = IconTheme.GetThemeIcon(InfoBarModuleGUID, "Edit")
      If editIcon Is Nothing Then editIcon = My.Resources.edit

      deleteFileIcon = IconTheme.GetThemeIcon(InfoBarModuleGUID, "DeleteFile")
      If deleteFileIcon Is Nothing Then deleteFileIcon = My.Resources.file_delete

      deleteFolderIcon = IconTheme.GetThemeIcon(InfoBarModuleGUID, "DeleteFolder")
      If deleteFolderIcon Is Nothing Then deleteFolderIcon = My.Resources.folder_delete

      quicklaunchIcon = IconTheme.GetThemeIcon(InfoBarModuleGUID, "QuickLaunch")
      If quicklaunchIcon Is Nothing Then quicklaunchIcon = My.Resources.icon
    End If

    MainMenu.AddMenuItem(InfoBarModuleGUID & "::" & "ADD_FILE", "Add File...", addFileIcon, True, True)
    MainMenu.AddMenuItem(InfoBarModuleGUID & "::" & "ADD_FOLDER", "Add Folder...", addFolderIcon, True)
    MainMenu.AddMenuItem(InfoBarModuleGUID & "::" & "ADD_URL", "Add Web Address...", addURLIcon, True)
    If m_RightClickOnItem > -1 Then
      MainMenu.AddSeparator()
      MainMenu.AddMenuItem(InfoBarModuleGUID & "::" & "EDIT", "Edit...", editIcon, False)
      MainMenu.AddMenuItem(InfoBarModuleGUID & "::" & "DELETE", "Delete", IIf(m_Items(m_RightClickOnItem).IsFolder, deleteFolderIcon, deleteFileIcon))
    End If
    MainMenu.AddSeparator()
    MainMenu.AddMenuItem(InfoBarModuleGUID & "::" & "SETTINGS", "Quick Launch Settings...", quicklaunchIcon)

    ' Always add a separator when you're done.
    MainMenu.AddSeparator()
  End Sub

  Public Overrides Sub ProcessMenuItemClick(ByVal Key As String)
    Select Case Key
      Case InfoBarModuleGUID & "::" & "ADD_FILE"
        Dim f As New frmAddFile
        f.QuickLaunch_ClassRef = Me
        f.QuickLaunch_EntryEditMode = False
        f.Show()

      Case InfoBarModuleGUID & "::" & "ADD_FOLDER"
        Dim fbd As New FolderBrowserDialog
        If fbd.ShowDialog = DialogResult.OK Then AddFileToItems(fbd.SelectedPath)

      Case InfoBarModuleGUID & "::" & "DELETE"
        m_Items.Remove(m_RightClickOnItem)
        DrawModule()
        Skin.UpdateWindow()

      Case InfoBarModuleGUID & "::" & "SETTINGS"
        Settings.ShowSettingsDialog(InfoBarModuleGUID)

    End Select
  End Sub

#End Region

#Region "Settings Routines"

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
    If m_SettingsDialog.radButtonAppearance_ShowIconsOnly.Checked = True Then
      Setting_ButtonDisplay = 0
    ElseIf m_SettingsDialog.radButtonAppearance_ShowTextOnly.Checked = True Then
      Setting_ButtonDisplay = 1
    ElseIf m_SettingsDialog.radButtonAppearance_ShowIconsAndText.Checked = True Then
      Setting_ButtonDisplay = 2
    ElseIf m_SettingsDialog.radButtonAppearanceIconsFilesIconsAndTextFolders.Checked = True Then
      Setting_ButtonDisplay = 3
    End If

    Setting_DisplayPathInTooltip = m_SettingsDialog.chkShowPathInTooltip.Checked
    Setting_ShowFoldersAsDropDownMenus = m_SettingsDialog.chkShowFoldersAsDropDownMenus.Checked

    DrawModule()
  End Sub

  Public Overrides Sub LoadSettings(ByRef Doc As XmlDocument)
    Setting_ButtonDisplay = 3
    Setting_DisplayPathInTooltip = True
    Setting_ShowFoldersAsDropDownMenus = False

    If Doc IsNot Nothing Then

      Setting_ButtonDisplay = Settings.GetSetting(Doc, "buttondisplay", 3)
      Setting_DisplayPathInTooltip = Settings.GetSetting(Doc, "displaypathintooltip", True)
      Setting_ShowFoldersAsDropDownMenus = Settings.GetSetting(Doc, "showfoldersasdropdownmenus", False)

      Dim Node As XmlNode, ChildNode As XmlNode
      Node = Doc.DocumentElement.SelectSingleNode("items")
      If Node IsNot Nothing Then
        For Each ChildNode In Node.ChildNodes
          Dim item As New QuickLaunchItem
          item.Title = ChildNode.Attributes("title").Value
          item.Path = ChildNode.Attributes("path").Value
          item.WorkingDirectory = ChildNode.Attributes("workingdir").Value
          item.CommandLineArgs = ChildNode.Attributes("args").Value
          item.Icon = ImageFromBase64String(ChildNode.Attributes("icon").Value)
          'If item.Path.StartsWith("http://") Or item.Path.StartsWith("https://") Then
          'item.Icon = GetFavoriteIconForURL(item.Path)
          'If item.Icon Is Nothing Then item.Icon = IconTheme.GetSystemIconByExtension(".htm")
          'Else
          'item.Icon = IconTheme.GetSystemIconForFile(item.Path)
          'End If
          Try
            Dim fi As IO.FileInfo = New IO.FileInfo(item.Path)
            item.IsFolder = (fi.Attributes = IO.FileAttributes.Directory)
            'If item.IsFolder Then
            ' Manually grab the folder icon
            'item.Icon = IconTheme.GetSystemIconByExtension("Folder")
            'End If
          Catch
            item.IsFolder = False
          End Try

          m_Items.Add(item)
        Next
      End If
    End If

    ResetSettings()
  End Sub

  Public Overrides Sub ResetSettings()
    If Setting_ButtonDisplay = 0 Then
      m_SettingsDialog.radButtonAppearance_ShowIconsOnly.Checked = True
    ElseIf Setting_ButtonDisplay = 1 Then
      m_SettingsDialog.radButtonAppearance_ShowTextOnly.Checked = True
    ElseIf Setting_ButtonDisplay = 2 Then
      m_SettingsDialog.radButtonAppearance_ShowIconsAndText.Checked = True
    ElseIf Setting_ButtonDisplay = 3 Then
      m_SettingsDialog.radButtonAppearanceIconsFilesIconsAndTextFolders.Checked = True
    End If

    m_SettingsDialog.chkShowPathInTooltip.Checked = Setting_DisplayPathInTooltip
    m_SettingsDialog.chkShowFoldersAsDropDownMenus.Checked = Setting_ShowFoldersAsDropDownMenus
  End Sub

  Public Overrides Sub SaveSettings(ByRef Doc As XmlDocument)
    Dim Node As XmlNode, ChildNode As XmlNode, Attrib As XmlAttribute

    Settings.SaveSetting(Doc, "buttondisplay", Setting_ButtonDisplay)
    Settings.SaveSetting(Doc, "displaypathintooltip", Setting_DisplayPathInTooltip)
    Settings.SaveSetting(Doc, "showfoldersasdropdownmenus", Setting_ShowFoldersAsDropDownMenus)

    ' Time to save the quick launch items
    Node = Doc.CreateNode(XmlNodeType.Element, "Node", "items", "")

    For I As Integer = 1 To m_Items.Count
      ChildNode = Doc.CreateNode(XmlNodeType.Element, "Node", "item", "")

      Attrib = Doc.CreateAttribute("title")
      Attrib.Value = m_Items(I).Title
      ChildNode.Attributes.Append(Attrib)

      Attrib = Doc.CreateAttribute("path")
      Attrib.Value = m_Items(I).Path
      ChildNode.Attributes.Append(Attrib)

      Attrib = Doc.CreateAttribute("workingdir")
      Attrib.Value = m_Items(I).WorkingDirectory
      ChildNode.Attributes.Append(Attrib)

      Attrib = Doc.CreateAttribute("args")
      Attrib.Value = m_Items(I).CommandLineArgs
      ChildNode.Attributes.Append(Attrib)

      Attrib = Doc.CreateAttribute("icon")
      Attrib.Value = ImageToBase64String(m_Items(I).Icon, Imaging.ImageFormat.Png)
      ChildNode.Attributes.Append(Attrib)

      Node.AppendChild(ChildNode)
    Next
    Doc.DocumentElement.AppendChild(Node)

    ' Weather Location
    'Node = Doc.CreateNode(XmlNodeType.Element, "Node", "location", "")
    'Node.InnerText = Setting_WeatherLocation
    'Doc.DocumentElement.AppendChild(Node)
  End Sub

#End Region

#Region "Quick Launch Functions"

  Private Function AddFileToItems(ByVal path As String) As Boolean
    Dim item As New QuickLaunchItem

    ' Is this a shortcut?
    If path.EndsWith(".lnk") = True Then
      ' Get real path and command line args
      Dim sc As New ShellShortcut(path)
      item.Path = sc.Path
      If sc.Icon IsNot Nothing Then
        item.Icon = sc.Icon.ToBitmap
        If item.Icon Is Nothing Then item.Icon = My.Resources.file_delete
      Else
        item.Icon = IconTheme.GetSystemIconForFile(item.Path)
        If item.Icon Is Nothing Then item.Icon = My.Resources.file_delete
      End If
      item.CommandLineArgs = sc.Arguments
      item.WorkingDirectory = sc.WorkingDirectory
    Else
      item.Path = path
      item.Icon = IconTheme.GetSystemIconForFile(item.Path)
      If item.Icon Is Nothing Then item.Icon = My.Resources.file_delete
      item.WorkingDirectory = path.Substring(0, path.LastIndexOf("\") + 1)
    End If

    ' Generate Title
    Dim sTitle As String = path
    ' Strip the Path, leaving the filename or last folder name
    sTitle = sTitle.Substring(sTitle.LastIndexOf("\") + 1)
    ' Strip any extension, if it exists
    If sTitle.LastIndexOf(".") > 0 Then sTitle = sTitle.Substring(0, sTitle.LastIndexOf("."))
    item.Title = sTitle

    ' Determine if this is a folder
    Try
      'MsgBox(item.Path)
      Dim fi As New IO.DirectoryInfo(item.Path)
      item.IsFolder = fi.Exists
      If item.IsFolder = True Then
        ' Force a backslash
        If item.Path.EndsWith("\") = False Then item.Path &= "\"
        ' Manually grab the folder icon
        item.Icon = IconTheme.GetSystemIconByExtension("Folder")
        If item.Icon Is Nothing Then item.Icon = My.Resources.folder_delete
      End If
    Catch
      item.IsFolder = False
    End Try

    ' Dupe Check
    If m_Items.Contains(item.Path) Then
      ' Update Original
      If MsgBox("This item already exists in the quick launch list. Would you like to replace it?", MsgBoxStyle.Question Or MsgBoxStyle.YesNo, "Quick Launch") = MsgBoxResult.Yes Then
        With m_Items(item.Path)
          .Title = item.Title
          .Path = item.Path
          .Icon = item.Icon
          .WorkingDirectory = item.WorkingDirectory
          .IsFolder = item.IsFolder
          .CommandLineArgs = item.CommandLineArgs
        End With
        Return True
      End If
    Else
      ' Add New
      m_Items.Add(item, item.Path)
      DrawModule()
      Skin.UpdateWindow()
      Return True
    End If
    Return False
  End Function

  Private Function AddURLToItems(ByVal url As String) As Boolean
    Dim item As New QuickLaunchItem
    item.Title = url
    item.Path = url
    item.Icon = IconTheme.GetSystemIconByExtension(".htm")

    ' Dupe Check
    If m_Items.Contains(item.Path) Then
      If MsgBox("This item already exists in the quick launch list. Would you like to replace it?", MsgBoxStyle.Question Or MsgBoxStyle.YesNo, "Quick Launch") = MsgBoxResult.Yes Then
        With m_Items(item.Path)
          .Title = item.Title
          .Path = item.Path
          .Icon = item.Icon
          .WorkingDirectory = item.WorkingDirectory
          .IsFolder = item.IsFolder
          .CommandLineArgs = item.CommandLineArgs
        End With
        GetTitleAndFavoriteIconForURL(item.Path)
        Return True
      End If
    Else
      m_Items.Add(item, item.Path)
      DrawModule()
      Skin.UpdateWindow()
      GetTitleAndFavoriteIconForURL(item.Path)
      Return True
    End If
    Return False
  End Function

  Private Sub GetTitleAndFavoriteIconForURL(ByVal sURL As String)
    Dim downloader As New Net.WebClient
    AddHandler downloader.DownloadStringCompleted, AddressOf Downloader_DownloadStringCompleted
    downloader.Headers.Add(Net.HttpRequestHeader.UserAgent, "Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 6.0)")
    downloader.DownloadStringAsync(New Uri(sURL), sURL)
  End Sub

  Private Sub Downloader_DownloadStringCompleted(ByVal sender As Object, ByVal e As System.Net.DownloadStringCompletedEventArgs)
    If e.Error Is Nothing Then
      Dim html As String = e.Result

      ' Search For Title
      Try
        Dim titleTagPos As Integer = html.ToLower.IndexOf("<title>") + "<title>".Length
        If titleTagPos > -1 Then
          Dim endTitleTagPos As Integer = html.ToLower.IndexOf("</title>", titleTagPos)
          If endTitleTagPos > -1 Then
            Dim title As String = html.Substring(titleTagPos, endTitleTagPos - titleTagPos)
            title = System.Web.HttpUtility.HtmlDecode(title)
            m_Items(e.UserState).Title = title
            DrawModule()
            Skin.UpdateWindow()
          End If
        End If
      Catch ex As Exception
      End Try

      ' Search For Shortcut Icon
      Dim iStartIndex As Integer = 0
      Dim favicoURL As String = Nothing
      Do
        ' Find a link tag
        Dim linkTagPos As Integer = html.ToLower.IndexOf("<link", iStartIndex)

        If linkTagPos < 0 Then
          Exit Sub
        Else
          ' Find the next > character
          Dim ltCharPos As Integer = html.ToLower.IndexOf(">", linkTagPos)

          ' Substr it
          Dim sTag As String = html.Substring(linkTagPos, ltCharPos - linkTagPos)

          ' Search for rel, see if it matches
          If sTag.ToLower.Contains("rel=""shortcut icon""") Or _
             sTag.ToLower.Contains("rel=""icon""") Then
            ' Search for and extract href

            If sTag.ToLower.Contains("href=""") Then
              Dim urlStart As Integer = sTag.ToLower.IndexOf("href=""") + 6
              Dim urlEnd As Integer = sTag.ToLower.IndexOf("""", urlStart)
              favicoURL = sTag.Substring(urlStart, urlEnd - urlStart)

              ' Is this a relative URL? If so, let's resolve it.
              If favicoURL.StartsWith("http://") = False AndAlso favicoURL.StartsWith("https://") = False Then favicoURL = ResolveRelativeURL(e.UserState, favicoURL)
              Exit Do
            Else
              iStartIndex = ltCharPos
            End If
          Else
            iStartIndex = ltCharPos
          End If
        End If

      Loop While True

      ' Download and return favico.
      If favicoURL IsNot Nothing Then
        Try
          Dim wc As New Net.WebClient
          Dim img As Image
          If favicoURL.EndsWith(".ico") Then
            Dim tempFile As String = My.Computer.FileSystem.GetTempFileName
            wc.DownloadFile(favicoURL, tempFile)
            Dim ico As Icon = New Icon(tempFile)
            If ico IsNot Nothing Then
              img = ico.ToBitmap
            Else
              img = Nothing
            End If
            Try
              IO.File.Delete(tempFile)
            Catch ex As Exception
            End Try
          Else
            Dim imgStream As System.IO.Stream = wc.OpenRead(favicoURL)
            img = Bitmap.FromStream(imgStream)
          End If
          Try
            m_Items(e.UserState).Icon = img
            DrawModule()
            Skin.UpdateWindow()
          Catch ex As Exception
          End Try
        Catch ex As Net.WebException
          Exit Sub
        End Try
      Else
        Exit Sub
      End If
    End If
  End Sub

  Private Function ResolveRelativeURL(ByVal ParentURL As String, ByVal RelativeURL As String) As String
    Dim BaseURI As Uri = New Uri(ParentURL)
    Dim BaseFragment As String = BaseURI.GetComponents(UriComponents.SchemeAndServer, UriFormat.SafeUnescaped)
    Dim BasePath As String = BaseURI.GetComponents(UriComponents.Path, UriFormat.SafeUnescaped)
    If RelativeURL.Contains("../") Then
      Dim backCount As Integer = NumOccurancesInString("../", RelativeURL)
      Dim BasePathParts() As String = BasePath.Split("/")
      If BaseFragment.EndsWith("/") = False Then BaseFragment &= "/"
      If (BasePathParts.Length - backCount) > 1 Then
        Return BaseFragment & String.Join("/", BasePathParts, 0, BasePathParts.Length - backCount) & RelativeURL.Replace("../", vbNullString)
      Else
        Return BaseFragment & RelativeURL.Replace("../", vbNullString)
      End If
    ElseIf RelativeURL.StartsWith("/") Then
      Return BaseFragment & RelativeURL
    Else
      If BaseFragment.EndsWith("/") = False Then BaseFragment &= "/"
      Return BaseFragment & RelativeURL
    End If
  End Function

  Private Function NumOccurancesInString(ByVal Needle As String, ByVal Haystack As String) As Integer
    Dim I As Integer = 0, Count As Integer = 0
    Do
      I = Haystack.IndexOf(Needle, I)
      If I <> -1 Then
        Count += 1
        I += Needle.Length
      End If
    Loop Until I = -1
    Return Count
  End Function

  Private Function ImageToBase64String(ByVal image As Image, ByVal format As Imaging.ImageFormat) As String
    Dim memory As IO.MemoryStream = New IO.MemoryStream()
    image.Save(memory, format)
    Dim base64 As String = Convert.ToBase64String(memory.ToArray())
    memory.Close()
    Return base64
  End Function

  Private Function ImageFromBase64String(ByVal base64 As String) As Image
    Dim memory As IO.MemoryStream = New IO.MemoryStream(Convert.FromBase64String(base64))
    Dim result As Image = Image.FromStream(memory)
    memory.Close()
    Return result
  End Function

#End Region

End Class