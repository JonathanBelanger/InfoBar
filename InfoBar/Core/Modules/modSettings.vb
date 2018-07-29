Module modSettings

  Public bAppLoaded As Boolean
  Public sAppLoadText As String

  Public Class SelectedModuleType
    Public GUID As String
    Public Row As Integer
  End Class

  Public Enum ModuleAlignment
    Left
    Center
    Right
    Justify
    JustifyCenter
  End Enum

  Public Class IBSettings
    ' General
    Public Class General
      Public Shared Position As ABEdge
      Public Shared AutoHide As Boolean
      Public Shared AlwaysOnTop As Boolean
      Public Shared DisableToolbarDocking As Boolean
      Public Shared ModuleAlignment As ModuleAlignment
      Public Shared Rows As Integer
      Public Shared RunAtWindowsStartup As Boolean
      Public Shared ShowBalloonPopup As Boolean
      Public Shared ShowSeparators As Boolean
    End Class

    Public Shared SelectedModules As New Collection
    Public Shared CurrentSkin As String    ' Skin directory, not skin name
    Public Shared CurrentIconTheme As String ' Icon directory, not icon theme name

    Public Class Advanced
      Public Shared EnableTooltipFade As Boolean
      Public Shared OverrideBackgroundOpacity As Boolean
      Public Shared BackgroundOpacity As Integer

      Public Shared AutohideDelay As Integer
      Public Shared AutohideAnimation As Boolean
      Public Shared AutohideAnimationSpeed As Integer
      Public Shared AutohideIgnoreMaximizedState As Boolean
    End Class
  End Class

  Public Sub Settings_LoadFromXML()
    Dim stPath As String = GetStorageDir()
    If File.Exists(stPath & "Settings.xml") = False Then
      Settings_LoadDefaults()
    Else
      Dim Doc As New XmlDocument
      Try
        Doc.Load(stPath & "Settings.xml")

        ' General Settings
        IBSettings.General.AlwaysOnTop = GetSetting(Doc, "general/alwaysontop", False)
        IBSettings.General.Position = GetSetting(Doc, "general/position", ABEdge.ABE_TOP)
        IBSettings.General.AutoHide = GetSetting(Doc, "general/autohide", False)
        IBSettings.General.DisableToolbarDocking = GetSetting(Doc, "general/disabletoolbardocking", False)
        IBSettings.General.ModuleAlignment = GetSetting(Doc, "general/alignment", ModuleAlignment.Left)
        IBSettings.General.Rows = GetSetting(Doc, "general/rows", 1)
        IBSettings.General.RunAtWindowsStartup = IBSettings.General.RunAtWindowsStartup
        IBSettings.General.ShowBalloonPopup = GetSetting(Doc, "general/showballoonpopup", True)
        IBSettings.General.ShowSeparators = GetSetting(Doc, "general/showseparators", True)

        IBSettings.CurrentSkin = GetSetting(Doc, "skin", "InfoBarInternalSkin")
        If File.Exists(IBSettings.CurrentSkin) = False Then
          IBSettings.CurrentSkin = "InfoBarInternalSkin"
        End If

        IBSettings.CurrentIconTheme = GetSetting(Doc, "icontheme", "InfoBarInternalIconTheme")
        If File.Exists(IBSettings.CurrentIconTheme) = False Then
          IBSettings.CurrentIconTheme = "InfoBarInternalIconTheme"
        End If

        ' Advanced
        IBSettings.Advanced.EnableTooltipFade = GetSetting(Doc, "advanced/enabletooltipfade", True)
        IBSettings.Advanced.OverrideBackgroundOpacity = GetSetting(Doc, "advanced/overridebackgroundopacity", False)
        IBSettings.Advanced.BackgroundOpacity = GetSetting(Doc, "advanced/backgroundopacity", 255)
        IBSettings.Advanced.AutoHideDelay = GetSetting(Doc, "advanced/autohidedelay", 1000)
        IBSettings.Advanced.AutoHideAnimation = GetSetting(Doc, "advanced/autohideanimation", True)
        IBSettings.Advanced.AutohideAnimationSpeed = GetSetting(Doc, "advanced/autohideanimationspeed", 5)
        IBSettings.Advanced.AutohideIgnoreMaximizedState = GetSetting(Doc, "advanced/autohideignoremaximizedstate", True)

        fMain.tmrAutohide.Interval = IBSettings.Advanced.AutohideDelay

      Catch ex As Exception
        MsgBox("There was an error while loading settings. The default settings will be loaded.")
        Settings_LoadDefaults()
      End Try
    End If
  End Sub

  Public Sub Settings_LoadModuleSettings()
    Dim stPath As String = GetStorageDir()

    ' Modules
    Dim ibmDoc As New XmlDocument
    For Each ibMod As InfoBarModule In AvailableModules
      Application.DoEvents()
      If File.Exists(stPath & ibMod.ModuleGUID & ".xml") Then
        Try
          ibmDoc.Load(stPath & ibMod.ModuleGUID & ".xml")
          ibMod.LoadSettings(ibmDoc)
        Catch ex As Exception
          MessageBox.Show("Module settings failed to load." & vbCrLf & vbCrLf & _
                          "Module: " & ibMod.ModuleName & vbCrLf & _
                          "GUID:   " & ibMod.ModuleGUID & vbCrLf & _
                          "Reason: " & ex.Message)
        End Try
      Else
        ibMod.LoadSettings(Nothing)
      End If
    Next

    If File.Exists(stPath & "Settings.xml") Then
      Dim Doc As New XmlDocument
      Try
        Doc.Load(stPath & "Settings.xml")

        If Doc.SelectSingleNode("settings/selectedmodules") IsNot Nothing Then
          Dim nModules As XmlNodeList = Doc.SelectNodes("settings/selectedmodules/module")
          For Each nModule As XmlNode In nModules
            Application.DoEvents()
            Dim sID As String = nModule.InnerText
            Dim iRow As Integer = nModule.Attributes("row").Value
            If AvailableModules.Contains(sID) Then
              AvailableModules(sID).ModuleEnabled = True
              Dim selMod As New SelectedModuleType
              selMod.GUID = sID
              selMod.Row = iRow
              IBSettings.SelectedModules.Add(selMod, sID)
            End If
          Next
        End If

      Catch ex As Exception
        MsgBox("There was an error while loading settings. The default settings will be loaded.")
        Settings_LoadDefaults()
      End Try
    End If
  End Sub

  Public Sub Settings_SaveToXML()
    Dim stPath As String = GetStorageDir()
    Dim Doc As New XmlDocument, Node As XmlNode, ChildNode As XmlNode, Attrib As XmlAttribute
    Doc.LoadXml("<?xml version='1.0' ?>" & "<settings>" & "</settings>")

    ' ==============================================================================================================
    ' General Settings
    ' ==============================================================================================================
    Node = Doc.CreateNode(XmlNodeType.Element, "Node", "general", "")
    With Node

      ' Always On Top
      ChildNode = Doc.CreateNode(XmlNodeType.Element, "Node", "alwaysontop", "")
      ChildNode.InnerText = IBSettings.General.AlwaysOnTop
      .AppendChild(ChildNode)

      ' Position
      ChildNode = Doc.CreateNode(XmlNodeType.Element, "Node", "position", "")
      ChildNode.InnerText = IBSettings.General.Position
      .AppendChild(ChildNode)

      ' Autohide
      ChildNode = Doc.CreateNode(XmlNodeType.Element, "Node", "autohide", "")
      ChildNode.InnerText = IBSettings.General.AutoHide
      .AppendChild(ChildNode)

      ' Disable Toolbar Docking
      ChildNode = Doc.CreateNode(XmlNodeType.Element, "Node", "disabletoolbardocking", "")
      ChildNode.InnerText = IBSettings.General.DisableToolbarDocking
      .AppendChild(ChildNode)

      ' Module Alignment
      ChildNode = Doc.CreateNode(XmlNodeType.Element, "Node", "alignment", "")
      ChildNode.InnerText = IBSettings.General.ModuleAlignment
      .AppendChild(ChildNode)

      ' Max Rows
      ChildNode = Doc.CreateNode(XmlNodeType.Element, "Node", "rows", "")
      ChildNode.InnerText = IBSettings.General.Rows
      .AppendChild(ChildNode)

      ' Show Balloon Popup
      ChildNode = Doc.CreateNode(XmlNodeType.Element, "Node", "showballoonpopup", "")
      ChildNode.InnerText = IBSettings.General.ShowBalloonPopup
      .AppendChild(ChildNode)

      ' Show Separators
      ChildNode = Doc.CreateNode(XmlNodeType.Element, "Node", "showseparators", "")
      ChildNode.InnerText = IBSettings.General.ShowSeparators
      .AppendChild(ChildNode)

    End With
    Doc.DocumentElement.AppendChild(Node)

    ' ==============================================================================================================
    ' Modules
    ' ==============================================================================================================
    If IBSettings.SelectedModules.Count > 0 Then
      Node = Doc.CreateNode(XmlNodeType.Element, "Node", "selectedmodules", "")
      With Node
        For Each sModule As SelectedModuleType In IBSettings.SelectedModules
          ChildNode = Doc.CreateNode(XmlNodeType.Element, "Node", "module", "")
          ChildNode.InnerText = sModule.GUID

          Attrib = Doc.CreateAttribute("row")
          Attrib.Value = sModule.Row
          ChildNode.Attributes.Append(Attrib)

          .AppendChild(ChildNode)
        Next
      End With
      Doc.DocumentElement.AppendChild(Node)
    End If

    For Each ibMod As InfoBarModule In AvailableModules
      Dim ibmDoc As New XmlDocument
      ibmDoc.LoadXml("<?xml version='1.0' ?>" & "<settings>" & "</settings>")
      ibMod.SaveSettings(ibmDoc)
      ibmDoc.Save(stPath & ibMod.ModuleGUID & ".xml")
    Next

    ' ==============================================================================================================
    ' Current Skin
    ' ==============================================================================================================
    Node = Doc.CreateNode(XmlNodeType.Element, "Node", "skin", "")
    Node.InnerText = IBSettings.CurrentSkin
    Doc.DocumentElement.AppendChild(Node)

    ' ==============================================================================================================
    ' Current Icon Theme
    ' ==============================================================================================================
    Node = Doc.CreateNode(XmlNodeType.Element, "Node", "icontheme", "")
    Node.InnerText = IBSettings.CurrentIconTheme
    Doc.DocumentElement.AppendChild(Node)

    ' ==============================================================================================================
    ' Advanced
    ' ==============================================================================================================
    Node = Doc.CreateNode(XmlNodeType.Element, "Node", "advanced", "")
    With Node

      ' Enable Tooltip Fade
      ChildNode = Doc.CreateNode(XmlNodeType.Element, "Node", "enabletooltipfade", "")
      ChildNode.InnerText = IBSettings.Advanced.EnableTooltipFade
      .AppendChild(ChildNode)

      ' Override Background Opacity
      ChildNode = Doc.CreateNode(XmlNodeType.Element, "Node", "overridebackgroundopacity", "")
      ChildNode.InnerText = IBSettings.Advanced.OverrideBackgroundOpacity
      .AppendChild(ChildNode)

      ' Background Opacity
      ChildNode = Doc.CreateNode(XmlNodeType.Element, "Node", "backgroundopacity", "")
      ChildNode.InnerText = IBSettings.Advanced.BackgroundOpacity
      .AppendChild(ChildNode)

      ' Autohide Delay
      ChildNode = Doc.CreateNode(XmlNodeType.Element, "Node", "autohidedelay", "")
      ChildNode.InnerText = IBSettings.Advanced.AutohideDelay
      .AppendChild(ChildNode)

      ' Autohide Animation
      ChildNode = Doc.CreateNode(XmlNodeType.Element, "Node", "autohideanimation", "")
      ChildNode.InnerText = IBSettings.Advanced.AutohideAnimation
      .AppendChild(ChildNode)

      ' Autohide Animation Speed
      ChildNode = Doc.CreateNode(XmlNodeType.Element, "Node", "autohideanimationspeed", "")
      ChildNode.InnerText = IBSettings.Advanced.AutohideAnimationSpeed
      .AppendChild(ChildNode)

      ' Autohide Ignore Maximized State
      ChildNode = Doc.CreateNode(XmlNodeType.Element, "Node", "autohideignoremaximizedstate", "")
      ChildNode.InnerText = IBSettings.Advanced.AutohideIgnoreMaximizedState
      .AppendChild(ChildNode)

    End With
    Doc.DocumentElement.AppendChild(Node)

    ' ==============================================================================================================
    ' Save to XML
    ' ==============================================================================================================
    Doc.Save(stPath & "Settings.xml")
  End Sub

  Public Sub Settings_LoadDefaults()
    IBSettings.CurrentSkin = "InfoBarInternalSkin"
    IBSettings.CurrentIconTheme = "InfoBarInternalIconTheme"

    ' General Settings
    IBSettings.General.AlwaysOnTop = False
    IBSettings.General.Position = ABEdge.ABE_TOP
    IBSettings.General.AutoHide = False
    IBSettings.General.DisableToolbarDocking = False
    IBSettings.General.ModuleAlignment = ModuleAlignment.Left
    IBSettings.General.Rows = 1
    IBSettings.General.ShowBalloonPopup = True
    IBSettings.General.ShowSeparators = True

    ' Advanced
    IBSettings.Advanced.EnableTooltipFade = True
    IBSettings.Advanced.OverrideBackgroundOpacity = False
    IBSettings.Advanced.BackgroundOpacity = 255
    IBSettings.Advanced.AutohideDelay = 1000
    IBSettings.Advanced.AutohideAnimation = True
    IBSettings.Advanced.AutohideAnimationSpeed = 5
    IBSettings.Advanced.AutohideIgnoreMaximizedState = True

    fMain.tmrAutohide.Interval = IBSettings.Advanced.AutohideDelay
  End Sub

  Public Sub Settings_CheckRunAtWindowsStartup()
    Dim RegKey As RegistryKey
    RegKey = Registry.CurrentUser.OpenSubKey("SOFTWARE\Microsoft\Windows\CurrentVersion\Run", False)
    If Not (RegKey Is Nothing) Then
      IBSettings.General.RunAtWindowsStartup = (RegKey.GetValue("InfoBar") <> vbNullString)
      RegKey.Close()
    End If
  End Sub

  Public Sub Settings_SetRunAtWindowsStartup(ByVal Enabled As Boolean)
    Dim RegKey As RegistryKey
    RegKey = Registry.CurrentUser.OpenSubKey("SOFTWARE\Microsoft\Windows\CurrentVersion\Run", True)
    If Not (RegKey Is Nothing) Then
      If Enabled Then
        RegKey.SetValue("InfoBar", Application.ExecutablePath)
      Else
        RegKey.DeleteValue("InfoBar", False)
      End If
      RegKey.Close()
    End If
  End Sub

  Private Function GetSetting(ByVal Doc As XmlDocument, ByVal Path As String, ByVal DefaultValue As String) As String
    Dim Node As XmlNode = Nothing
    Node = Doc.DocumentElement.SelectSingleNode(Path)
    If Node IsNot Nothing Then
      Return Node.InnerText
    Else
      Return DefaultValue
    End If
  End Function

End Module
