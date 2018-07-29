Namespace Services

  Public Class Settings

    Public Function GetSetting(ByVal Doc As XmlDocument, ByVal Path As String, ByVal DefaultValue As String) As String
      On Error Resume Next
      If Doc IsNot Nothing Then
        Dim Node As XmlNode = Nothing
        Node = Doc.DocumentElement.SelectSingleNode(Path)
        If Node IsNot Nothing Then
          Return Node.InnerText
        Else
          Return DefaultValue
        End If
      Else
        Return DefaultValue
      End If
    End Function

    Public Sub SaveSetting(ByVal Doc As XmlDocument, ByVal Name As String, ByVal Value As String)
      Dim Node As XmlNode
      Node = Doc.CreateNode(XmlNodeType.Element, "Node", Name, "")
      Node.InnerText = Value
      Doc.DocumentElement.AppendChild(Node)
    End Sub

    Public Sub ShowSettingsDialog(Optional ByVal ModuleGUID As String = vbNullString)
      frmSettings.DefaultVisibleModule = ModuleGUID
      If frmSettings.IsHandleCreated Then
        frmSettings.Activate()
      Else
        frmSettings.Show()
      End If
    End Sub

    Public Sub EnableSettingsDialogApplyButton()
      frmSettings.btnApply.Enabled = True
    End Sub

    Public Sub OpenWebLink(ByVal URL As String)
      ShellExecute(IntPtr.Zero, "OPEN", URL, vbNullString, vbNullString, vbNormalFocus)
    End Sub

  End Class

End Namespace