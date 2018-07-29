Friend Class frmAddFile

  Public QuickLaunch_EntryEditMode As Boolean
  Public QuickLaunch_ClassRef As QuickLaunch.InfoBarModuleMain

  Dim item As QuickLaunchItem

  Private Sub frmAddFile_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    Dim IconTheme As New InfoBar.Services.IconTheme
    Dim bmp As Bitmap = Nothing
    If IconTheme.IsDefaultTheme = True Then
      bmp = IconTheme.GetSystemIconByExtension("File")
    Else
      bmp = IconTheme.GetThemeIcon("{D03C4859-3681-27ca-6595-3B3223AB6FA1}", "File")
      If bmp Is Nothing Then bmp = IconTheme.GetSystemIconByExtension("File")
    End If
    Me.Icon = Icon.FromHandle(bmp.GetHicon)
  End Sub

  Private Sub btnBrowsePath_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBrowsePath.Click
    Dim OFD As New OpenFileDialog
    OFD.CheckFileExists = True
    OFD.CheckPathExists = True
    OFD.Multiselect = False
    OFD.ShowReadOnly = False
    OFD.Title = "Choose a file..."
    OFD.ValidateNames = True
    OFD.Filter = "All Files (*.*)|*.*"
    OFD.FileName = txtPath.Text
    If OFD.ShowDialog = Windows.Forms.DialogResult.OK Then
      txtPath.Text = OFD.FileName

      ' Generate Title
      Dim sTitle As String = txtPath.Text
      sTitle = sTitle.Substring(sTitle.LastIndexOf("\") + 1)
      If sTitle.LastIndexOf(".") > 0 Then sTitle = sTitle.Substring(0, sTitle.LastIndexOf("."))
      txtName.Text = sTitle

      Dim imgIcon As Image = Nothing
      Dim it As New InfoBar.Services.IconTheme

      ' Is this a shortcut?
      If txtPath.Text.EndsWith(".lnk") = True Then
        ' Get real path and command line args
        Dim sc As New ShellShortcut(txtPath.Text)
        txtPath.Text = sc.Path
        txtCommandLineArguments.Text = sc.Arguments
        txtWorkingDirectory.Text = sc.WorkingDirectory
        If sc.Icon IsNot Nothing Then
          imgIcon = sc.Icon.ToBitmap
          If imgIcon Is Nothing Then imgIcon = My.Resources.file_delete
        Else
          imgIcon = it.GetSystemIconForFile(item.Path)
          If imgIcon Is Nothing Then imgIcon = My.Resources.file_delete
        End If

      Else
        imgIcon = it.GetSystemIconForFile(txtPath.Text)
        If imgIcon Is Nothing Then imgIcon = My.Resources.file_delete
        txtWorkingDirectory.Text = txtPath.Text.Substring(0, txtPath.Text.LastIndexOf("\") + 1)
      End If

      ' Determine if this is a folder
      Dim bFolder As Boolean
      Try
        Dim fi As New IO.DirectoryInfo(txtPath.Text)
        bFolder = fi.Exists
        If bFolder Then
          If txtPath.Text.EndsWith("\") = False Then txtPath.Text &= "\"
          imgIcon = it.GetSystemIconByExtension("Folder")
          If imgIcon Is Nothing Then imgIcon = My.Resources.folder_delete
          txtWorkingDirectory.Text = vbNullString
        End If
      Catch ex As Exception
        bFolder = False
      End Try

      item = New QuickLaunchItem
      item.Title = txtName.Text
      item.Path = txtPath.Text
      item.CommandLineArgs = txtCommandLineArguments.Text
      item.WorkingDirectory = txtWorkingDirectory.Text
      item.Icon = imgIcon
      item.IsFolder = bFolder
    End If
  End Sub

  Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
    Me.Dispose()
  End Sub

  Private Sub btnOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOK.Click
    If item Is Nothing Then
      MsgBox("The quick launch item could not be created.", MsgBoxStyle.Exclamation Or MsgBoxStyle.OkOnly)
      Exit Sub
    End If

    ' Dupe Check
    If QuickLaunch_ClassRef.m_Items.Contains(item.Path) Then
      ' Update Original
      If MsgBox("This item already exists in the quick launch list. Would you like to replace it?", MsgBoxStyle.Question Or MsgBoxStyle.YesNo, "Quick Launch") = MsgBoxResult.Yes Then
        With QuickLaunch_ClassRef.m_Items(item.Path)
          .Title = item.Title
          .Path = item.Path
          .Icon = item.Icon
          .WorkingDirectory = item.WorkingDirectory
          .IsFolder = item.IsFolder
          .CommandLineArgs = item.CommandLineArgs
        End With
      End If
    Else
      ' Add New
      QuickLaunch_ClassRef.m_Items.Add(item, item.Path)
    End If

    Me.Hide()
    QuickLaunch_ClassRef.DrawModule()
    Me.Dispose()
  End Sub

End Class