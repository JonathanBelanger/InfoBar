Namespace Services

  Public Class PopupMenus

    Public Class MainMenu

      Public Sub AddMenuItem(ByVal Key As String, ByVal sText As String, Optional ByVal imgIcon As Image = Nothing, Optional ByVal isEnabled As Boolean = True, Optional ByVal isDefault As Boolean = False)
        Select Case Key
          Case "mnuInfoBar_Settings", "mnuInfoBar_Sep01", "mnuInfoBar_AutoHide", "mnuInfoBar_Minimize", "mnuInfoBar_Exit", "mnuTray_Restore", "mnuTray_Sep01", "mnuTray_Exit"
            Throw New Exception("Module menu items can not use internal key names.")
            Exit Sub
        End Select


        Dim tsi As New ToolStripMenuItem
        tsi.Name = Key
        tsi.Text = sText
        tsi.Image = imgIcon
        tsi.Enabled = isEnabled
        If isDefault Then tsi.Tag = "DEFAULT"
        fMain.mnuInfoBar.Items.Add(tsi)
      End Sub

      Public Sub AddSeparator()
        Dim tsi As New ToolStripSeparator

        fMain.mnuInfoBar.Items.Add(tsi)
      End Sub

    End Class

    Public Class PopupMenu
      Public Event ItemClicked(ByVal Key As String)
      Public Event MenuClosed()

      Dim WithEvents mnuPopup As CustomContextMenu
      Dim m_MenuOpen As Boolean

      Public Sub New()
        mnuPopup = New CustomContextMenu
        mnuPopup.Items.Clear()
      End Sub

      Public ReadOnly Property IsMenuOpen() As Boolean
        Get
          Return m_MenuOpen
        End Get
      End Property

      Public Sub Close()
        m_MenuOpen = False
        mnuPopup.Close()
      End Sub

      Public Property ShowImageMargin() As Boolean
        Get
          Return mnuPopup.ShowImageMargin
        End Get
        Set(ByVal value As Boolean)
          mnuPopup.ShowImageMargin = value
        End Set
      End Property

      Public Sub AddMenuItem(ByVal sKey As String, ByVal sText As String, Optional ByVal imgIcon As Image = Nothing, Optional ByVal isEnabled As Boolean = True, Optional ByVal isDefault As Boolean = False, Optional ByVal showImageOnly As Boolean = False)
        Dim mi As New ToolStripMenuItem
        mi.Name = sKey
        mi.Text = sText
        mi.Image = imgIcon
        mi.Enabled = isEnabled
        If isDefault Then mi.Tag = "DEFAULT"
        mnuPopup.Items.Add(mi)
      End Sub

      Public Sub AddSeparator()
        Dim mi As New ToolStripSeparator
        mnuPopup.Items.Add(mi)
      End Sub

      Public Sub ShowPopupMenu(ByVal Rect As Rectangle)
        If IsMenuOpen = True Then Close()
        fTooltip.HideTooltip(True)
        m_MenuOpen = True
        SetForegroundWindow(fMain.Handle)
        Rect.Y += fMain.Top
        mnuPopup.Show(fMain, Rect)
      End Sub

      Private Sub mnuPopup_Closed(ByVal sender As Object, ByVal e As System.Windows.Forms.ToolStripDropDownClosedEventArgs) Handles mnuPopup.Closed
        m_MenuOpen = False
        RaiseEvent MenuClosed()
      End Sub

      Private Sub mnuPopup_ItemClicked(ByVal sender As Object, ByVal e As System.Windows.Forms.ToolStripItemClickedEventArgs) Handles mnuPopup.ItemClicked
        m_MenuOpen = False
        mnuPopup.Close()
        RaiseEvent ItemClicked(e.ClickedItem.Name)
      End Sub

    End Class

  End Class

End Namespace