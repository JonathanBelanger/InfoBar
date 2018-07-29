Module modIcons

#Region "Icon Structures"

  Public Class IconThemeInfo
    Public Name As String
    Public Title As String
    Public Author As String
    Public Version As String
    Public Website As String
    Public Path As String
  End Class

  Public Class IconThemeIcon
    Public ModuleGUID As String
    Public ModuleElement As String
    Public Icon As Image
  End Class

  Public Class CurrentIconTheme
    Public Shared Path As String
    Public Shared Icons As New Collection
  End Class

#End Region

  Public IconThemeCollection As New Collection

  Public Sub Icons_EnumerateThemes()
    ' Add Default Theme
    Dim Info As New IconThemeInfo
    Info.Name = "InfoBarInternalIconTheme"
    Info.Title = "Vista (Default)"
    Info.Author = "Jim Laski"
    Info.Version = "0.1"
    Info.Website = "http://www.nightiguana.com"
    IconThemeCollection.Add(Info, "InfoBarInternalIconTheme")

    Dim iconsDir As String = Application.StartupPath & "\Icons\"
    If Directory.Exists(iconsDir) = True Then
      Dim files() As String = Directory.GetFiles(iconsDir, "theme.xml", SearchOption.AllDirectories)
      Dim Doc As New XmlDocument
      For Each sFile As String In files
        Application.DoEvents()
        Try
          Doc.Load(sFile)
          Info = New IconThemeInfo
          Info.Name = Doc.DocumentElement.SelectSingleNode("/theme").Attributes("name").Value
          Info.Title = Doc.DocumentElement.SelectSingleNode("/theme").Attributes("title").Value
          Info.Author = Doc.DocumentElement.SelectSingleNode("/theme").Attributes("author").Value
          Info.Version = Doc.DocumentElement.SelectSingleNode("/theme").Attributes("version").Value
          Info.Website = Doc.DocumentElement.SelectSingleNode("/theme").Attributes("website").Value
          Info.Path = sFile.Replace("theme.xml", "")
          If IconThemeCollection.Contains(Info.Name) = True Then
            MsgBox("There is already an icon theme with the unique name of " & Info.Name & ". Change the name in the icon theme definition file, or remove the icon theme.", MsgBoxStyle.Information Or MsgBoxStyle.OkOnly, "InfoBar")
          Else
            IconThemeCollection.Add(Info, Info.Name)
          End If
        Catch ex As Exception
        End Try
      Next
    End If
  End Sub

  Public Sub Icons_LoadIconTheme()
    Dim ITI As New IconThemeIcon
    CurrentIconTheme.Icons.Clear()

    If IconThemeCollection.Contains(IBSettings.CurrentIconTheme) = False Then IBSettings.CurrentIconTheme = "InfoBarInternalIconTheme"
    If IBSettings.CurrentIconTheme = "InfoBarInternalIconTheme" Then
      CurrentIconTheme.Path = "InfoBarInternal"

      ' Internal Icons
      ITI = New IconThemeIcon
      ITI.ModuleGUID = "InfoBarInternal"
      ITI.ModuleElement = "Close"
      ITI.Icon = My.Resources.defaulticons_exit.Clone
      CurrentIconTheme.Icons.Add(ITI, ITI.ModuleGUID & "::" & ITI.ModuleElement)

      ITI = New IconThemeIcon
      ITI.ModuleGUID = "InfoBarInternal"
      ITI.ModuleElement = "Settings"
      ITI.Icon = My.Resources.defaulticons_settings.Clone
      CurrentIconTheme.Icons.Add(ITI, ITI.ModuleGUID & "::" & ITI.ModuleElement)

      ITI = New IconThemeIcon
      ITI.ModuleGUID = "InfoBarInternal"
      ITI.ModuleElement = "Minimize"
      ITI.Icon = My.Resources.defaulticons_hide.Clone
      CurrentIconTheme.Icons.Add(ITI, ITI.ModuleGUID & "::" & ITI.ModuleElement)

    Else

      CurrentIconTheme.Path = IconThemeCollection(IBSettings.CurrentIconTheme).Path

      Dim Doc As New XmlDocument
      Try
        Doc.Load(CurrentIconTheme.Path & "\theme.xml")
      Catch ex As Exception
        MsgBox("The current icon theme could not be loaded.", MsgBoxStyle.Exclamation Or MsgBoxStyle.OkOnly, "Cannot Load Icon Theme")
        IBSettings.CurrentIconTheme = "InfoBarInternalIconTheme"
        Icons_LoadIconTheme()
        Exit Sub
      End Try

      ' Sample XML line: <icon moduleGUID="InfoBarInternal" moduleElement="minimize" image="minimize.png" />
      Try
        For Each Node As XmlNode In Doc.DocumentElement.ChildNodes
          Application.DoEvents()
          If Node.NodeType = XmlNodeType.Element Then
            ITI = New IconThemeIcon
            ITI.ModuleGUID = Node.Attributes("moduleGUID").Value
            ITI.ModuleElement = Node.Attributes("moduleElement").Value
            ITI.Icon = LoadImageFromFile(CurrentIconTheme.Path & Node.Attributes("image").Value)
            CurrentIconTheme.Icons.Add(ITI, ITI.ModuleGUID & "::" & ITI.ModuleElement)
          End If
        Next
      Catch ex As Exception
        MsgBox("The current icon theme could not be loaded due to a schema error.", MsgBoxStyle.Exclamation Or MsgBoxStyle.OkOnly, "Cannot Load Icon Theme")
        IBSettings.CurrentIconTheme = "InfoBarInternalIconTheme"
        Icons_LoadIconTheme()
        Exit Sub
      End Try
    End If

  End Sub

End Module
