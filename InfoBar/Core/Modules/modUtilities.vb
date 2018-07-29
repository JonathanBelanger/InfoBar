Module modUtilities

#Region "Win API"
  Public Declare Function GetSystemMetrics Lib "user32.dll" (ByVal nIndex As Integer) As Integer
  Public Declare Function GetCaretPos Lib "user32.dll" (ByRef lpPoint As Point) As Integer
  Public Declare Function ShellExecute Lib "shell32.dll" Alias "ShellExecuteA" (ByVal hWnd As IntPtr, ByVal lpOperation As String, ByVal lpFile As String, ByVal lpParameters As String, ByVal lpDirectory As String, ByVal nShowCmd As Integer) As IntPtr
  Public Declare Function FindWindow Lib "user32.dll" Alias "FindWindowA" (ByVal lpClassName As String, ByVal lpWindowName As String) As IntPtr
  Private Declare Sub Sleep Lib "kernel32.dll" (ByVal dwMilliseconds As UInteger)
#End Region

  Enum DisplayUnit
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

  Public Sub Wait(ByVal MS As Integer)
    For i As Integer = 0 To MS
      Application.DoEvents()
      Sleep(1)
    Next
  End Sub

  Public Function LoadImageFromFile(ByVal Path As String) As Image
    Try
      Dim tmpImage As Image = Image.FromFile(Path, True)
      Dim tmpBmp As Bitmap = New Bitmap(tmpImage.Width, tmpImage.Height, PixelFormat.Format32bppArgb)
      Dim tmpGr As Graphics = Graphics.FromImage(tmpBmp)
      tmpGr.DrawImage(tmpImage, 0, 0, tmpImage.Width, tmpImage.Height)
      tmpGr.Dispose()
      tmpImage.Dispose()
      Return tmpBmp.Clone
      tmpBmp.Dispose()
    Catch ex As Exception
      Return Nothing
    End Try
  End Function

  Public Function FormatFileSize(ByVal Bytes As ULong, Optional ByVal Format As DisplayUnit = DisplayUnit.Auto) As String
    Select Case Format
      Case DisplayUnit.Bits
        Return (Bytes / 8) & " Bits"
      Case DisplayUnit.Kilobits
        Return FormatNumber((Bytes / 8) / 1024, 2) & " Kb"
      Case DisplayUnit.Megabits
        Return FormatNumber((Bytes / 8) / 1048576, 2) & " Mb"
      Case DisplayUnit.Gigabits
        Return FormatNumber((Bytes / 8) / 1073741824, 2) & " Gb"
      Case DisplayUnit.Terabits
        Return FormatNumber((Bytes / 8) / 1099511627776, 2) & " Tb"
      Case DisplayUnit.Bytes
        Return Bytes & " Bytes"
      Case DisplayUnit.Kilobytes
        Return FormatNumber(Bytes / 1024, 2) & " KB"
      Case DisplayUnit.Megabytes
        Return FormatNumber(Bytes / 1048576, 2) & " MB"
      Case DisplayUnit.Gigabytes
        Return FormatNumber((Bytes / 1073741824), 2) & " GB"
      Case DisplayUnit.Terabytes
        Return FormatNumber(Bytes / 1099511627776, 2) & " TB"
      Case Else
        If Bytes >= 1099511627776 Then
          Return FormatNumber(Bytes / 1099511627776, 2) & " TB"
        ElseIf Bytes >= 1073741824 Then
          Return FormatNumber((Bytes / 1073741824), 2) & " GB"
        ElseIf Bytes >= 1048576 Then
          Return FormatNumber((Bytes / 1048576), 2) & " MB"
        ElseIf Bytes >= 1024 Then
          Return FormatNumber((Bytes / 1024), 2) & " KB"
        Else
          Return Bytes & " Bytes"
        End If
    End Select
  End Function

  Public Function FormatTransferRate(ByVal Bytes As ULong, Optional ByVal Format As DisplayUnit = DisplayUnit.Auto) As String
    Select Case Format
      Case DisplayUnit.Bits
        Return (Bytes / 10) & " b/s"
      Case DisplayUnit.Kilobits
        Return FormatNumber(((Bytes / 10) / 1000), 2) & " Kb/s"
      Case DisplayUnit.Megabits
        Return FormatNumber(((Bytes / 10) / 1000000), 2) & " Mb/s"
      Case DisplayUnit.Gigabits
        Return FormatNumber(((Bytes / 10) / 1000000000), 2) & " Gb/s"
      Case DisplayUnit.Terabits
        Return FormatNumber(((Bytes / 10) / 1000000000000), 2) & " Tb/s"
      Case DisplayUnit.Bytes
        Return Bytes & " Bps"
      Case DisplayUnit.Kilobytes
        Return FormatNumber((Bytes / 1000), 2) & " KB/s"
      Case DisplayUnit.Megabytes
        Return FormatNumber((Bytes / 1000000), 2) & " MB/s"
      Case DisplayUnit.Gigabytes
        Return FormatNumber((Bytes / 1000000000), 2) & " GB/s"
      Case DisplayUnit.Terabytes
        Return FormatNumber((Bytes / 1000000000000), 2) & " TB/s"
      Case Else
        If Bytes >= 1000000000000 Then
          Return FormatNumber((Bytes / 1000000000000), 2) & " TB/s"
        ElseIf Bytes >= 1000000000 Then
          Return FormatNumber((Bytes / 1000000000), 2) & " GB/s"
        ElseIf Bytes >= 1000000 Then
          Return FormatNumber((Bytes / 1000000), 2) & " MB/s"
        ElseIf Bytes >= 1000 Then
          Return FormatNumber((Bytes / 1000), 2) & " KB/s"
        Else
          Return Bytes & " B/s"
        End If
    End Select
  End Function

  Public Function PreparePath(ByVal Path As String) As String
    If Mid(Path, Len(Path) - 1) = "\" Then
      PreparePath = Path
    Else
      PreparePath = Path & "\"
    End If
  End Function

  Public Function GetStorageDir() As String
    Dim sPath As String
    sPath = PreparePath(ShItem.GetCShItem(ShellDll.CSIDL.APPDATA).Path) & "InfoBar\"
    If Directory.Exists(sPath) = False Then MkDir(sPath)
    Return sPath
  End Function

  Public Sub MinimizeToTray(ByVal Enabled As Boolean, Optional ByVal SkipBalloonTip As Boolean = False)
    If Enabled Then
      fMain.tmrUpdate.Stop()
      fMain.Hide()
      ABHideBar(True)
      fMain.tiMain.Visible = True
      If IBSettings.General.ShowBalloonPopup = True AndAlso SkipBalloonTip = False Then
        fMain.tiMain.BalloonTipIcon = ToolTipIcon.Info
        fMain.tiMain.BalloonTipText = "InfoBar has been hidden and this icon has been placed here. You can get InfoBar back by double clicking this icon." & vbCrLf & vbCrLf & "Click here to not show this message again."
        fMain.tiMain.BalloonTipTitle = "Where did InfoBar go?"
        fMain.tiMain.ShowBalloonTip(10000)
      End If
    Else
      ABHideBar(False)
      If IBSettings.General.AutoHide Then
        ABSetAutoHide(True)
        fMain.tmrAutohide.Start()
      Else
        ABSetPos(IBSettings.General.Position)
      End If
      fMain.Show()
      Skinning_UpdateWindow()
      fMain.tmrUpdate.Start()
      If fMain.tiMain.Tag = vbNullString Then fMain.tiMain.Visible = False
    End If
  End Sub

  Public Sub SortCollection(ByVal col As Collection, ByVal psSortPropertyName As String, ByVal pbAscending As Boolean, Optional ByVal psKeyPropertyName As String = "")
    Dim obj As Object
    Dim i As Integer
    Dim j As Integer
    Dim iMinMaxIndex As Integer
    Dim vMinMax As Object
    Dim vValue As Object
    Dim bSortCondition As Boolean
    Dim bUseKey As Boolean
    Dim sKey As String
    bUseKey = (psKeyPropertyName <> "")
    For i = 1 To col.Count - 1
      obj = col(i)
      If TypeOf obj Is String Then
        vMinMax = LCase(CallByName(obj, psSortPropertyName, vbGet))
      Else
        vMinMax = CallByName(obj, psSortPropertyName, vbGet)
      End If
      iMinMaxIndex = i
      For j = i + 1 To col.Count
        obj = col(j)
        If TypeOf obj Is String Then
          vValue = LCase(CallByName(obj, psSortPropertyName, vbGet))
        Else
          vValue = CallByName(obj, psSortPropertyName, vbGet)
        End If
        If (pbAscending) Then
          bSortCondition = (vValue < vMinMax)
        Else
          bSortCondition = (vValue > vMinMax)
        End If
        If (bSortCondition) Then
          vMinMax = vValue
          iMinMaxIndex = j
        End If
        obj = Nothing
      Next j
      If (iMinMaxIndex <> i) Then
        obj = col(iMinMaxIndex)
        col.Remove(iMinMaxIndex)
        If (bUseKey) Then
          sKey = CStr(CallByName(obj, psKeyPropertyName, vbGet))
          col.Add(obj, sKey, i)
        Else
          col.Add(obj, , i)
        End If
        obj = Nothing
      End If
      obj = Nothing
    Next i
  End Sub

  Public Function FormatTime(ByVal sngSecs As ULong) As String
    Dim intSeconds As ULong
    Dim intMinutes As ULong
    Dim intHours As ULong
    Dim strTimeResult As String = vbNullString

    intMinutes = sngSecs \ 60
    intSeconds = sngSecs Mod 60
    intHours = intMinutes \ 60
    intMinutes = intMinutes Mod 60

    If intMinutes >= 60 Then intMinutes = 0
    If intMinutes <= 0 Then intMinutes = 0

    If intHours > 0 Then strTimeResult = intHours & ":"
    If intMinutes > 9 Then
      strTimeResult = strTimeResult & Format(intMinutes, "00") & ":"
    Else
      strTimeResult = strTimeResult & intMinutes & ":"
    End If
    strTimeResult = strTimeResult & Format(intSeconds, "00")

    Return strTimeResult
  End Function

  Public Sub EnableVistaControlEffects(ByVal CtlParent As Control, ByVal Enabled As Boolean)
    For Each Ctl As Control In CtlParent.Controls
      If TypeOf Ctl Is Button Then
        Dim Btn As Button = Ctl
        Btn.FlatStyle = IIf(Enabled, FlatStyle.System, FlatStyle.Standard)
        If Enabled AndAlso Btn.Image IsNot Nothing Then
          Dim bmp As Bitmap = Btn.Image
          SendMessage(Btn.Handle, &HF7, 1, bmp.GetHicon)
        End If
      ElseIf TypeOf Ctl Is CheckBox Then
        Dim Chk As CheckBox = Ctl
        Chk.FlatStyle = IIf(Enabled, FlatStyle.System, FlatStyle.Standard)
      ElseIf TypeOf Ctl Is RadioButton Then
        Dim Rad As RadioButton = Ctl
        Rad.FlatStyle = IIf(Enabled, FlatStyle.System, FlatStyle.Standard)
      ElseIf TypeOf Ctl Is ListView Then
        SetWindowTheme(Ctl.Handle, "explorer", vbNullString)
        SendMessage(Ctl.Handle, &H1000 + 54, &H10000, &H10000)
      ElseIf TypeOf Ctl Is TreeView Then
        SetWindowTheme(Ctl.Handle, "explorer", vbNullString)
        SendMessage(Ctl.Handle, &H1000 + 54, &H10000, &H10000)
        SendMessage(Ctl.Handle, &H1100 + 44, &H20, &H20)
      ElseIf Ctl.Controls.Count > 0 Then
        EnableVistaControlEffects(Ctl, Enabled)
      End If
    Next
  End Sub

End Module
