Module modFileAssociations

  Public Function CheckFileAssociations() As Boolean
    Dim needsRepair As Boolean

    needsRepair = CheckFileAssociation(".InfoBarModule", "InfoBar Module Package", "Install", "installmodule")
    If needsRepair Then Return True

    needsRepair = CheckFileAssociation(".InfoBarSkin", "InfoBar Skin Package", "Install", "installskin")
    If needsRepair Then Return True

    needsRepair = CheckFileAssociation(".InfoBarIcons", "InfoBar Icon Package", "Install", "installicons")
    If needsRepair Then Return True

    Return False
  End Function

  Public Sub RegisterFileAssociations()
    RegisterFileAssociation(".InfoBarModule", "InfoBar Module Package", "Install", "installmodule")
    RegisterFileAssociation(".InfoBarSkin", "InfoBar Skin Package", "Install", "installskin")
    RegisterFileAssociation(".InfoBarIcons", "InfoBar Icon Package", "Install", "installicons")
  End Sub

  Public Function CheckFileAssociation(ByVal ext As String, ByVal description As String, ByVal verb As String, ByVal cmdLineSwitch As String) As Boolean
    Dim regKey As RegistryKey
    regKey = Registry.ClassesRoot.OpenSubKey(ext)
    If regKey Is Nothing Then
      Return True
    Else
      Dim val As String = regKey.GetValue("")
      regKey.Close()
      If val <> ("InfoBar" & ext) Then Return True
    End If

    regKey = Registry.ClassesRoot.OpenSubKey("InfoBar" & ext)
    If regKey Is Nothing Then
      Return True
    Else
      If regKey.GetValue("") <> description Then
        regKey.Close()
        Return True
      Else
        regKey = Registry.ClassesRoot.OpenSubKey("InfoBar" & ext & "\shell")
        If regKey Is Nothing Then
          Return True
        Else
          regKey.Close()
          regKey = Registry.ClassesRoot.OpenSubKey("InfoBar" & ext & "\shell\" & verb)
          If regKey Is Nothing Then
            Return True
          Else
            regKey.Close()
            regKey = Registry.ClassesRoot.OpenSubKey("InfoBar" & ext & "\shell\" & verb & "\command")
            If regKey Is Nothing Then
              Return True
            Else
              Dim val As String = regKey.GetValue("")
              regKey.Close()
              If val <> Application.StartupPath & "\InfoBar.exe /" & cmdLineSwitch & " " & Chr(34) & "%1" & Chr(34) Then
                Return True
              End If
            End If
          End If
        End If
      End If
    End If
    Return False
  End Function

  Public Sub RegisterFileAssociation(ByVal ext As String, ByVal description As String, ByVal verb As String, ByVal cmdLineSwitch As String)
    Dim regKey As RegistryKey

    regKey = Registry.ClassesRoot.CreateSubKey(ext, RegistryKeyPermissionCheck.ReadWriteSubTree)
    regKey.SetValue("", "InfoBar" & ext, RegistryValueKind.String)
    regKey.Close()

    regKey = Registry.ClassesRoot.CreateSubKey("InfoBar" & ext, RegistryKeyPermissionCheck.ReadWriteSubTree)
    regKey.SetValue("", description)
    regKey.Close()

    regKey = Registry.ClassesRoot.CreateSubKey("InfoBar" & ext & "\shell\" & verb & "\command", RegistryKeyPermissionCheck.ReadWriteSubTree)
    regKey.SetValue("", Application.StartupPath & "\InfoBar.exe /" & cmdLineSwitch & " " & Chr(34) & "%1" & Chr(34))
    regKey.Close()
  End Sub

End Module
