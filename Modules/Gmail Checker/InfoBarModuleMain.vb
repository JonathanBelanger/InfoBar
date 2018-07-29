Public Class InfoBarModuleMain
  Inherits InfoBarModule

  Private Const GmailURL As String = "http://mail.google.com/"
  Private Const GmailFeedURL As String = "https://mail.google.com/mail/feed/atom"
  Private Const GmailRefreshText As String = "Please wait while your Gmail account is checked for new messages..."
  Private Const GmailNoMessagesText As String = "No New Messages."

  ' Unique ID. If same as other module, the first module will be loaded, the next will be ignored.
  Private Const InfoBarModuleGUID As String = "{B93A47BD-7B44-4bac-8E18-784639139A4F}"

#Region "InfoBar Service Objects"
  Private Skin As New InfoBar.Services.Skinning
  Private Tooltip As New InfoBar.Services.Tooltips
  Private IconTheme As New InfoBar.Services.IconTheme
  Private MainMenu As New InfoBar.Services.PopupMenus.MainMenu
  Private Settings As New InfoBar.Services.Settings
#End Region

#Region "Private Classes and Variables"
  Private Class GmailEntry
    Dim _SenderName As String
    Public Property SenderName() As String
      Get
        Return _SenderName
      End Get
      Set(ByVal value As String)
        _SenderName = value
      End Set
    End Property

    Dim _SenderEmail As String
    Public Property SenderEmail() As String
      Get
        Return _SenderEmail
      End Get
      Set(ByVal value As String)
        _SenderEmail = value
      End Set
    End Property

    Dim _Title As String
    Public Property Title() As String
      Get
        Return _Title
      End Get
      Set(ByVal value As String)
        _Title = value
      End Set
    End Property

    Dim _Summary As String
    Public Property Summary() As String
      Get
        Return _Summary
      End Get
      Set(ByVal value As String)
        _Summary = value
      End Set
    End Property

    Dim _Link As String
    Public Property Link() As String
      Get
        Return _Link
      End Get
      Set(ByVal value As String)
        _Link = value
      End Set
    End Property

    Dim _DateReceived As String
    Public Property DateReceived() As String
      Get
        Return _DateReceived
      End Get
      Set(ByVal value As String)
        Dim tempDate As Date
        If Date.TryParse(value, tempDate) = True Then
          _DateReceived = tempDate.ToString
        Else
          _DateReceived = value
        End If
      End Set
    End Property

  End Class

  Private Class GmailData
    Dim _UnreadCount As Integer
    Public Property UnreadCount() As Integer
      Get
        Return _UnreadCount
      End Get
      Set(ByVal value As Integer)
        _UnreadCount = value
      End Set
    End Property

    Dim _Link As String
    Public Property Link() As String
      Get
        Return _Link
      End Get
      Set(ByVal value As String)
        _Link = value
      End Set
    End Property

    Dim _Entries As New Collections.ObjectModel.Collection(Of GmailEntry)
    Public ReadOnly Property Entries() As Collections.ObjectModel.Collection(Of GmailEntry)
      Get
        Return _Entries
      End Get
    End Property
  End Class

  Private WithEvents webRequest As WebClient
  Private gData As New GmailData

  Private m_ModuleToolbarBitmap As Bitmap
  Private m_ModuleTooltipBitmap As Bitmap
  Private m_Bounds As Rectangle
  Private m_SettingsDialog As New Settings
  Private m_Enabled As Boolean
  Private m_IsManualUpdate As Boolean
  Private m_UpdateInProgress As Boolean
  Private m_ErrorChecking As Boolean
  Private m_Text As String
  Private m_ErrorMessage As String
  Private m_GmailButtonBounds As Rectangle
  Private m_GmailButtonState As Integer
  Private m_LastCheckTime As DateTime
  Private ttCol1 As Integer, ttCol2 As Integer
#End Region

#Region "Settings Variables"
  Private Setting_Email As String
  Private Setting_Password As String
  Private Setting_CheckInterval As Integer
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
      Return "Checks Gmail for unread messages and notifies you."
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
    m_IsManualUpdate = True
    m_UpdateInProgress = True
    gData.Link = GmailURL
    Gmail_Update()
  End Sub

  ' InfoBar will call this when your module is disabled.
  Public Overrides Sub FinalizeModule()
    '
  End Sub

  ' InfoBar will call this every 1 second. Make sure to do work only when needed.
  ' TIP: If m_Enabled is set to false, don't do any work to save CPU time unless really needed.
  Public Overrides Sub TimerTick(ByRef bModuleIsDirty As Boolean)
    Gmail_Update()
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

    Dim ico As Image
    If IconTheme.IsDefaultTheme = True Then
      ico = My.Resources.icon
    Else
      ico = IconTheme.GetThemeIcon(InfoBarModuleGUID, "Gmail")
      If ico Is Nothing Then ico = My.Resources.icon
    End If

    Dim grm As Graphics = Graphics.FromHwnd(IntPtr.Zero)
    Dim bR As New Rectangle(0, 0, 1, height)
    Dim bFR As Rectangle
    bFR = Skin.MeasureButton(grm, bR, ico, m_Text, m_GmailButtonState, False)
    grm.Dispose()
    width += bFR.Width

    If width <= 1 Or height <= 1 Then Exit Sub
    Dim bmpTemp As New Bitmap(width, height, System.Drawing.Imaging.PixelFormat.Format32bppArgb)
    Dim GR As Graphics = Graphics.FromImage(bmpTemp)

    ' Get skin height for vertical centering
    Dim curX As Integer = 0
    Dim lHeight As Integer = bmpTemp.Height

    ' Draw Gmail Button
    bR = New Rectangle(curX, 0, 1, lHeight)
    bFR = Skin.DrawButton(GR, bR, ico, m_Text, m_GmailButtonState, False)
    m_GmailButtonBounds = bFR
    curX = curX + bFR.Width

    GR.Dispose()
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
    Select Case ObjectID
      Case "GmailChecker"
        Dim nextX As Integer = 0, nextY As Integer = 0
        Dim maxWidth As Integer, maxHeight As Integer
        Dim GR As Graphics
        Dim tr As Rectangle
        Dim maxTTWidth As Integer = Screen.PrimaryScreen.WorkingArea.Width - (Skin.ContentMargins(Skinning.SkinMarginPart.Tooltip).Left + Skin.ContentMargins(Skinning.SkinMarginPart.Tooltip).Right)
        Dim maxTTHeight As Integer = Screen.PrimaryScreen.WorkingArea.Height - (Skin.ContentMargins(Skinning.SkinMarginPart.Tooltip).Top + Skin.ContentMargins(Skinning.SkinMarginPart.Tooltip).Bottom)

        GR = Graphics.FromHwnd(IntPtr.Zero)

        ' Error Message
        If m_ErrorChecking Then

          tr = Skin.MeasureText(Gr, m_ErrorMessage, Skinning.SkinTextPart.TooltipText, maxTTWidth)
          maxWidth = tr.Width
          nextY += tr.Height
          maxWidth = tr.Width
          maxHeight = nextY

        ElseIf m_UpdateInProgress = True Then

          tr = Skin.MeasureText(Gr, GmailRefreshText, Skinning.SkinTextPart.TooltipText, maxTTWidth)
          maxWidth = tr.Width
          nextY += tr.Height
          maxWidth = tr.Width
          maxHeight = nextY

        Else

          ' Last Checked
          Dim sText As String = "Last Checked: " & m_LastCheckTime.ToString
          tr = Skin.MeasureText(Gr, sText, Skinning.SkinTextPart.TooltipText)
          If tr.Width > maxWidth Then maxWidth = tr.Width
          nextY += tr.Height + Skin.TooltipSeparatorHeight

          If gData.UnreadCount > 0 Then

            For I As Integer = 0 To gData.Entries.Count - 1
              ' Sender Name <Sender Email>
              tr = Skin.MeasureText(Gr, gData.Entries(I).SenderName & " <" & gData.Entries(I).SenderEmail & ">", Skinning.SkinTextPart.TooltipText, maxTTWidth)
              If tr.Width > ttCol1 Then ttCol1 = tr.Width
              nextY += tr.Height

              ' Date & Time Received
              tr = Skin.MeasureText(Gr, gData.Entries(I).DateReceived, Skinning.SkinTextPart.TooltipText)
              If tr.Width > ttCol2 Then ttCol2 = tr.Width

              ' Set maxWidth
              Dim tempMaxWidth As Integer = ttCol1 + ttCol2 + 50
              If tempMaxWidth > maxWidth Then maxWidth = tempMaxWidth

              ' Subject
              tr = Skin.MeasureText(Gr, gData.Entries(I).Title, Skinning.SkinTextPart.TooltipText, maxTTWidth)
              If tr.Width > maxWidth Then maxWidth = tr.Width
              nextY += tr.Height

              ' Summary
              tr = Skin.MeasureText(Gr, gData.Entries(I).Summary, Skinning.SkinTextPart.TooltipText, maxTTWidth)
              If tr.Width > maxWidth Then maxWidth = tr.Width
              nextY += tr.Height

              If I < (gData.Entries.Count - 1) Then nextY += Skin.TooltipSeparatorHeight
            Next

          Else

            tr = Skin.MeasureText(Gr, GmailNoMessagesText, Skinning.SkinTextPart.TooltipText, maxTTWidth)
            If tr.Width > maxWidth Then maxWidth = tr.Width
            nextY += tr.Height

          End If

          maxHeight = nextY

        End If

        GR.Dispose()

        Dim bmpTemp As New Bitmap(maxWidth, maxHeight, System.Drawing.Imaging.PixelFormat.Format32bppArgb)
        GR = Graphics.FromImage(bmpTemp)

        Dim rect As Rectangle, curX As Integer, curY As Integer

        curX = 0
        curY = 0

        ' Error Message
        If m_ErrorChecking Then

          rect = Skin.MeasureText(GR, m_ErrorMessage, Skinning.SkinTextPart.TooltipText, tr.Width)
          rect.X = curX
          rect.Y = curY
          Skin.DrawText(GR, m_ErrorMessage, rect, Skinning.SkinTextPart.TooltipText, StringAlignment.Near, StringAlignment.Near, True)

        ElseIf m_UpdateInProgress = True Then

          rect = Skin.MeasureText(GR, GmailRefreshText, Skinning.SkinTextPart.TooltipText, tr.Width)
          rect.X = curX
          rect.Y = curY
          Skin.DrawText(GR, GmailRefreshText, rect, Skinning.SkinTextPart.TooltipText, StringAlignment.Near, StringAlignment.Near)

        Else

          ' Last Checked
          rect = Skin.MeasureText(GR, "Last Checked: " & m_LastCheckTime.ToString, Skinning.SkinTextPart.TooltipText, maxWidth)
          rect.X = curX
          rect.Y = curY
          Skin.DrawText(GR, "Last Checked: " & m_LastCheckTime.ToString, rect, Skinning.SkinTextPart.TooltipText, StringAlignment.Near, StringAlignment.Near)
          curY = curY + rect.Height

          rect.Y = curY
          rect.Width = maxWidth '- (Skin.ContentMargins(Skinning.SkinMarginPart.Tooltip).Left + Skin.ContentMargins(Skinning.SkinMarginPart.Tooltip).Right)
          rect.Height = Skin.TooltipSeparatorHeight
          Skin.DrawTooltipSeparator(GR, rect)
          curY = curY + Skin.TooltipSeparatorHeight

          If gData.UnreadCount > 0 Then

            For I As Integer = 0 To gData.Entries.Count - 1
              ' Sender Name <Sender Email>
              rect = Skin.MeasureText(GR, gData.Entries(I).SenderName & " <" & gData.Entries(I).SenderEmail & ">", Skinning.SkinTextPart.TooltipText, ttCol1)
              rect.X = curX
              rect.Y = curY
              Skin.DrawText(GR, gData.Entries(I).SenderName & " <" & gData.Entries(I).SenderEmail & ">", rect, Skinning.SkinTextPart.TooltipText, StringAlignment.Near, StringAlignment.Near, False)

              ' Date & Time Received
              rect = Skin.MeasureText(GR, gData.Entries(I).DateReceived, Skinning.SkinTextPart.TooltipText)
              rect.X = curX
              rect.Y = curY
              rect.Width = tr.Width - (Skin.ContentMargins(Skinning.SkinMarginPart.Tooltip).Left + Skin.ContentMargins(Skinning.SkinMarginPart.Tooltip).Right)
              Try
                Skin.DrawText(GR, gData.Entries(I).DateReceived, rect, Skinning.SkinTextPart.TooltipText, StringAlignment.Far, StringAlignment.Near, False)
              Catch ex As Exception
              End Try
              curY += rect.Height

              curX = 0

              ' Subject
              rect = Skin.MeasureText(GR, gData.Entries(I).Title, Skinning.SkinTextPart.TooltipText, maxWidth)
              rect.X = curX
              rect.Y = curY
              Skin.DrawText(GR, gData.Entries(I).Title, rect, Skinning.SkinTextPart.TooltipText, StringAlignment.Near, StringAlignment.Near, True)
              curY += rect.Height

              ' Summary
              rect = Skin.MeasureText(GR, gData.Entries(I).Summary, Skinning.SkinTextPart.TooltipText, maxWidth)
              rect.X = curX
              rect.Y = curY
              Skin.DrawText(GR, gData.Entries(I).Summary, rect, Skinning.SkinTextPart.TooltipText, StringAlignment.Near, StringAlignment.Near, True)
              curY += rect.Height

              If I < (gData.Entries.Count - 1) Then
                rect.X = curX
                rect.Y = curY
                rect.Width = maxWidth '- (Skin.ContentMargins(Skinning.SkinMarginPart.Tooltip).Left + Skin.ContentMargins(Skinning.SkinMarginPart.Tooltip).Right)
                rect.Height = Skin.TooltipSeparatorHeight
                Skin.DrawTooltipSeparator(GR, rect)
                curY += Skin.TooltipSeparatorHeight
              End If

            Next

          Else

            rect = Skin.MeasureText(GR, GmailNoMessagesText, Skinning.SkinTextPart.TooltipText, maxWidth)
            rect.X = curX
            rect.Y = curY
            Skin.DrawText(GR, GmailNoMessagesText, rect, Skinning.SkinTextPart.TooltipText, StringAlignment.Near, StringAlignment.Near, False)
            curY += rect.Height

          End If

        End If

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
    Dim pt As Point = Skin.ToolbarPointToModulePoint(e.Location, m_Bounds)

    ' Gmail Button
    If m_GmailButtonBounds.Contains(pt) Then
      If e.Button = Windows.Forms.MouseButtons.None Then
        If m_GmailButtonState <> 1 Then
          m_GmailButtonState = 1
          DrawModule()
          bWindowIsDirty = True
        End If
        Tooltip.SetTooltipOwner(InfoBarModuleGUID, "GmailChecker")
        DrawTooltip("GmailChecker")
        Tooltip.UpdateTooltip()
      ElseIf e.Button = Windows.Forms.MouseButtons.Left Then
        If m_GmailButtonState <> 2 Then
          m_GmailButtonState = 2
          DrawModule()
          bWindowIsDirty = True
        End If
      End If
    Else
      If m_GmailButtonState <> 0 Then
        m_GmailButtonState = 0
        DrawModule()
        bWindowIsDirty = True
      End If
    End If
  End Sub

  ' Check to see if the mouse was depressed on any of your UI rectangles.
  ' Set bWindowIsDirty to true if you need a redraw.
  Public Overrides Sub ProcessMouseUp(ByVal e As MouseEventArgs, ByRef bWindowIsDirty As Boolean)
    Dim pt As Point = Skin.ToolbarPointToModulePoint(e.Location, m_Bounds)

    ' Gmail Button
    If m_GmailButtonBounds.Contains(pt) = True Then
      If m_GmailButtonState <> 1 Then
        m_GmailButtonState = 1
        DrawModule()
        bWindowIsDirty = True
        Gmail_VisitWebSite()
      End If
    Else
      If m_GmailButtonState <> 0 Then
        m_GmailButtonState = 0
        DrawModule()
        bWindowIsDirty = True
      End If
    End If
  End Sub

  ' Check to see if the mouse was pressed down on any of your UI rectangles.
  ' Set bWindowIsDirty to true if you need a redraw.
  Public Overrides Sub ProcessMouseDown(ByVal e As MouseEventArgs, ByRef bWindowIsDirty As Boolean)
    Dim pt As Point = Skin.ToolbarPointToModulePoint(e.Location, m_Bounds)

    ' Gmail Button
    If m_GmailButtonBounds.Contains(pt) = True Then
      If m_GmailButtonState <> 2 Then
        m_GmailButtonState = 2
        DrawModule()
        bWindowIsDirty = True
      End If
    Else
      If m_GmailButtonState <> 0 Then
        m_GmailButtonState = 0
        DrawModule()
        bWindowIsDirty = True
      End If
    End If
  End Sub

  ' Check to see if the mouse left any of your UI rectangles.
  ' Set bWindowIsDirty to true if you need a redraw.
  Public Overrides Sub ProcessMouseLeave(ByRef bWindowIsDirty As Boolean)
    ' Gmail Button
    If m_GmailButtonState <> 0 Then
      m_GmailButtonState = 0
      DrawModule()
      bWindowIsDirty = True
    End If
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

    Dim goIcon As Image, refreshIcon As Image, GmailIcon As Image
    If IconTheme.IsDefaultTheme = True Then
      goIcon = My.Resources.go
      refreshIcon = My.Resources.refresh
      GmailIcon = My.Resources.icon
    Else
      goIcon = IconTheme.GetThemeIcon(InfoBarModuleGUID, "Go")
      If goIcon Is Nothing Then goIcon = My.Resources.go

      refreshIcon = IconTheme.GetThemeIcon(InfoBarModuleGUID, "Refresh")
      If refreshIcon Is Nothing Then refreshIcon = My.Resources.refresh

      GmailIcon = IconTheme.GetThemeIcon(InfoBarModuleGUID, "Gmail")
      If GmailIcon Is Nothing Then GmailIcon = My.Resources.icon
    End If

    MainMenu.AddMenuItem(InfoBarModuleGUID & "::" & "VISIT", "Visit Gmail...", goIcon, True, True)
    MainMenu.AddSeparator()
    MainMenu.AddMenuItem(InfoBarModuleGUID & "::" & "UPDATE", "Update Now", refreshIcon, Not m_UpdateInProgress)
    MainMenu.AddMenuItem(InfoBarModuleGUID & "::" & "SETTINGS", "Gmail Checker Settings...", GmailIcon)

    ' Always add a separator when you're done.
    MainMenu.AddSeparator()
  End Sub

  ' InfoBar calls this when one of your module's menu items are selected.
  Public Overrides Sub ProcessMenuItemClick(ByVal Key As String)
    Select Case Key
      Case InfoBarModuleGUID & "::" & "VISIT"
        Gmail_VisitWebSite()
      Case InfoBarModuleGUID & "::" & "UPDATE"
        m_IsManualUpdate = True
        Gmail_Update()
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

  Public Overrides ReadOnly Property SettingsDialog() As System.Windows.Forms.UserControl
    Get
      Return m_SettingsDialog
    End Get
  End Property

  Public Overrides Sub ApplySettings()
    Setting_Email = m_SettingsDialog.txtEmail.Text
    Setting_Password = m_SettingsDialog.txtPassword.Text
    Setting_CheckInterval = m_SettingsDialog.nudCheckInterval.Value

    m_IsManualUpdate = True
    m_UpdateInProgress = True
    Gmail_Update()
  End Sub

  ' InfoBar will call this when the application loads. Use this to set your internal settings.
  Public Overrides Sub LoadSettings(ByRef Doc As XmlDocument)
    ' Always set defaults first.
    Setting_Email = "@gmail.com"
    Setting_Password = Nothing
    Setting_CheckInterval = 10

    ' Now use the InfoBar Settings Service to load your settings.
    If Doc IsNot Nothing Then

      Setting_Email = Settings.GetSetting(Doc, "email", "@gmail.com")

      Setting_Password = Settings.GetSetting(Doc, "password", Nothing)
      If Setting_Password IsNot Nothing Then
        Dim pe As New Utilities.PasswordEncryption
        Setting_Password = pe.DecryptPassword(Setting_Password)
      End If

      Setting_CheckInterval = Settings.GetSetting(Doc, "checkinterval", 10)
      If Setting_CheckInterval < 10 Then Setting_CheckInterval = 10
      If Setting_CheckInterval > 1440 Then Setting_CheckInterval = 1440

    End If

    ResetSettings()
  End Sub

  Public Overrides Sub ResetSettings()
    m_SettingsDialog.txtEmail.Text = Setting_Email
    m_SettingsDialog.txtPassword.Text = Setting_Password
    m_SettingsDialog.nudCheckInterval.Value = Setting_CheckInterval
  End Sub

  ' InfoBar will call this when the application exits. Use this to save your internal settings.
  Public Overrides Sub SaveSettings(ByRef Doc As XmlDocument)
    Dim Node As XmlNode

    ' Email
    Node = Doc.CreateNode(XmlNodeType.Element, "Node", "email", "")
    Node.InnerText = Setting_Email
    Doc.DocumentElement.AppendChild(Node)

    ' Password
    Dim pe As New Utilities.PasswordEncryption
    Node = Doc.CreateNode(XmlNodeType.Element, "Node", "password", "")
    Node.InnerText = pe.EncryptPassword(Setting_Password)
    Doc.DocumentElement.AppendChild(Node)

    ' Check Interval
    Node = Doc.CreateNode(XmlNodeType.Element, "Node", "checkinterval", "")
    Node.InnerText = Setting_CheckInterval
    Doc.DocumentElement.AppendChild(Node)

  End Sub

#End Region

#Region "Gmail Checker Routines"

  Private Sub Gmail_Update()
    Dim curTime As DateTime = DateTime.Now
    Dim timeDiff As Integer = Setting_CheckInterval - curTime.Subtract(m_LastCheckTime).Minutes
    If (timeDiff <= 0) Or m_IsManualUpdate = True Then
      m_ErrorChecking = False
      m_UpdateInProgress = True

      If m_IsManualUpdate = True Then
        m_IsManualUpdate = False
        m_Text = "Checking..."
        DrawModule()
        Skin.UpdateWindow()
        If Tooltip.GetTooltipOwnerGUID = InfoBarModuleGUID AndAlso Tooltip.GetTooltipOwnerObjectID = "GmailChecker" Then
          Tooltip.UpdateTooltip()
        End If
      End If

      webRequest = New WebClient
      m_LastCheckTime = curTime
      Dim uriGmail As New Uri(GmailFeedURL)
      Dim wrCache As New CredentialCache()
      wrCache.Add(uriGmail, "Basic", New NetworkCredential(Setting_Email, Setting_Password))
      webRequest.Credentials = wrCache
      webRequest.DownloadStringAsync(uriGmail)
    End If
  End Sub

  Private Sub webRequest_DownloadStringCompleted(ByVal sender As Object, ByVal e As System.Net.DownloadStringCompletedEventArgs) Handles WebRequest.DownloadStringCompleted
    If e.Error Is Nothing Then
      Gmail_ParseInfo(e.Result)
    Else
      Gmail_ParseError(e.Error)
    End If
  End Sub

  Private Sub Gmail_ParseInfo(ByVal sXML As String)
    gData.Entries.Clear()
    Dim xml As New XmlDocument
    Try
      xml.LoadXml(sXML)
      For Each node As XmlNode In xml.DocumentElement
        Select Case node.Name
          Case "fullcount"
            gData.UnreadCount = CInt(node.InnerText)
          Case "link"
            gData.Link = node.Attributes("href").Value
          Case "entry"
            Dim entry As New GmailEntry
            For Each entryNode As XmlNode In node.ChildNodes
              Select Case entryNode.Name
                Case "title"
                  entry.Title = entryNode.InnerText
                Case "summary"
                  entry.Summary = entryNode.InnerText
                Case "link"
                  entry.Link = entryNode.Attributes("href").Value
                Case "issued"
                  entry.DateReceived = entryNode.InnerText
                Case "author"
                  For Each authorNode As XmlNode In entryNode.ChildNodes
                    Select Case authorNode.Name
                      Case "name"
                        entry.SenderName = authorNode.InnerText
                      Case "email"
                        entry.SenderEmail = authorNode.InnerText
                    End Select
                  Next
              End Select
            Next
            ' Add to our unread mail collection
            gData.Entries.Add(entry)
        End Select
      Next

      m_UpdateInProgress = False
      m_ErrorChecking = False

      Dim sText As String
      If gData.UnreadCount > 0 Then
        sText = gData.UnreadCount
      Else
        sText = vbNullString
      End If

      If m_Text <> sText Then
        m_Text = sText
        DrawModule()
        Skin.UpdateWindow()
      End If      
      DrawTooltip("GmailChecker")

      If Tooltip.GetTooltipOwnerGUID = InfoBarModuleGUID _
      AndAlso Tooltip.GetTooltipOwnerObjectID = "GmailChecker" Then
        Tooltip.UpdateTooltip()
      End If

    Catch ex As Exception
      Gmail_ParseError(ex)
    End Try
  End Sub

  Private Sub Gmail_ParseError(ByVal ex As Exception)
    ' We'll just set the button tooltip to ex.Message
    ' and set the button text to "Error"
    m_ErrorMessage = ex.ToString

    ' This should be fine for now until we know almost
    ' every error message possible, and can then provide
    ' friendly errors to users.
    m_UpdateInProgress = False
    m_ErrorChecking = True

    m_Text = "Error"
    DrawModule()
    DrawTooltip("GmailChecker")
    Skin.UpdateWindow()

    If Tooltip.GetTooltipOwnerGUID = InfoBarModuleGUID AndAlso Tooltip.GetTooltipOwnerObjectID = "GmailChecker" Then
      Tooltip.UpdateTooltip()
    End If
  End Sub

  Private Sub Gmail_VisitWebSite()
    If gData.Link IsNot Nothing Then
      Settings.OpenWebLink(gData.Link)
    Else
      Settings.OpenWebLink(GmailURL)
    End If
  End Sub

#End Region

End Class
