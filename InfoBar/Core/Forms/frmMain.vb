Friend Class frmMain

  Dim bFullScreen As Boolean

#Region "Form Load and Unload Subs"

  Protected Overrides ReadOnly Property CreateParams() As CreateParams
    Get
      Dim cp As CreateParams = MyBase.CreateParams
      cp.Style = cp.Style And Not WindowStyles.WS_DLGFRAME
      cp.ExStyle = cp.ExStyle Or (WindowExStyles.WS_EX_LAYERED Or WindowExStyles.WS_EX_NOACTIVATE)
      Return cp
    End Get
  End Property

  Public Sub New()
    InitializeComponent()
    ToolStripManager.Renderer = New MenuRenderer
    CreateBufferBitmaps()
  End Sub

  Private Sub frmMain_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
    tmrUpdate.Stop()

    ' Finalize Modules
    For Each sModule As SelectedModuleType In IBSettings.SelectedModules
      AvailableModules(sModule.GUID).ModuleEnabled = False
      AvailableModules(sModule.GUID).FinalizeModule()
    Next

    CurrentTooltipOwnerGUID = vbNullString
    CurrentTooltipOwnerObjectID = vbNullString

    ' Hide and Unregister InfoBar
    Me.Hide()

    fTooltip.Dispose()
    fTooltip = Nothing

    frmSettings.Dispose()

    UnregisterBar()
    tiMain.Dispose()

    ' Save Settings
    Settings_SaveToXML()

    ' Destroy/Dispose of Objects
    DisposeResources()
  End Sub

  Private Sub frmMain_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Resize
    MainForm_Size = Me.Size
  End Sub

  Private Sub tmrUpdate_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tmrUpdate.Tick
    Dim bWindowIsDirty As Boolean = False
    Dim bModIsDirty As Boolean = False
    For Each selMod As SelectedModuleType In IBSettings.SelectedModules
      bModIsDirty = False
      Try
        AvailableModules(selMod.GUID).TimerTick(bModIsDirty)
      Catch ex As Exception
      End Try
      If bModIsDirty Then bWindowIsDirty = True
    Next

    If bWindowIsDirty Then Skinning_UpdateWindow()
  End Sub

#End Region

#Region "Form Mouse Events"

  Private Sub frmMain_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Me.MouseDown
    Skinning_ProcessMouseDown(e)
  End Sub

  Private Sub frmMain_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Me.MouseMove
    tmrAutohide.Stop()
    tmrAutohideAnimation.Stop()
    Skinning_ProcessMouseMove(e)
  End Sub

  Private Sub frmMain_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Me.MouseUp
    Skinning_ProcessMouseUp(e)
  End Sub

  Private Sub frmMain_MouseEnter(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.MouseEnter
    If IBSettings.General.AutoHide Then
      Dim y As Integer
      If IBSettings.General.Position = ABEdge.ABE_TOP Then
        y = 0
      Else
        y = Screen.PrimaryScreen.WorkingArea.Height - Me.Height
      End If
      SetWindowPos(Me.Handle, HWND_TOPMOST, 0, y, Me.Width, Me.Height, SWP_NOSIZE Or SWP_NOACTIVATE)
    End If
    Skinning_ProcessMouseMove(New MouseEventArgs(Windows.Forms.MouseButtons.None, 0, MousePosition.X, MousePosition.Y, 0))
  End Sub

  Private Sub frmMain_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.MouseLeave
    If IBSettings.General.AutoHide Then
      tmrAutohide.Start()
    End If
    Skinning_ProcessMouseLeave()
  End Sub

  Private Sub frmMain_DragDrop(ByVal sender As Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles Me.DragDrop
    Skinning_ProcessDragDrop(e)
  End Sub

  Private Sub frmMain_DragEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles Me.DragEnter
    Skinning_ProcessDragEnter(e)
  End Sub

  Private Sub frmMain_DragLeave(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.DragLeave
    Skinning_ProcessDragLeave(e)
  End Sub

  Private Sub frmMain_DragOver(ByVal sender As Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles Me.DragOver
    Skinning_ProcessDragOver(e)
  End Sub

#End Region

#Region "Other Form Events"

  Private Sub frmMain_SystemColorsChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.SystemColorsChanged
    ToggleBlur(CurrentSkinInfo.Background.EnableGlass)
  End Sub

#End Region

#Region "Overridden Window Procedure"

  Protected Overrides Sub WndProc(ByRef m As System.Windows.Forms.Message)
    Select Case m.Msg

      Case TaskbarRestartCallBack
        AppBarRegistered = False
        RegisterBar()
        ABSetPos(IBSettings.General.Position)

      Case AppBarMessageCallBack
        Select Case m.WParam.ToInt32

          Case ABNotify.ABN_POSCHANGED
            ABSetPos(IBSettings.General.Position)

          Case ABNotify.ABN_FULLSCREENAPP
            Dim fOpen As Boolean = CBool(m.LParam)
            bFullScreen = fOpen
            If fOpen Then
              SetTopMost(m.HWnd, False)
            Else
              If IBSettings.General.AlwaysOnTop Then SetTopMost(m.HWnd, True)
            End If

          Case ABNotify.ABN_WINDOWARRANGE
            Me.Visible = True

        End Select

      Case WM_DWMCOMPOSITIONCHANGED
        Skinning_DrawBackground()
        ToggleBlur(CurrentSkinInfo.Background.EnableGlass)
        Skinning_UpdateWindow()

    End Select
    MyBase.WndProc(m)
  End Sub

#End Region

#Region "Layered Window Routines"

  Public Delegate Sub SetWindowBitmapDelegate()

  Public Sub SetWindowBitmap(Optional ByVal Alpha As Integer = 255)
    If Alpha < 0 Then Alpha = 0
    If Alpha > 255 Then Alpha = 255
    Dim pointMemory As Point = New Point(0, 0)
    Dim blend As BLENDFUNCTION = New BLENDFUNCTION
    blend.BlendOp = AC_SRC_OVER
    blend.BlendFlags = 0
    blend.SourceConstantAlpha = Alpha
    blend.AlphaFormat = AC_SRC_ALPHA
    UpdateLayeredWindow(Me.Handle, MainForm_screenDC, Me.Location, Me.Size, MainForm_memDC, pointMemory, 0, blend, ULW_ALPHA)
  End Sub

  Public Sub CreateBufferBitmaps()
    If MainForm_hBackBitmap = IntPtr.Zero Then
      MainForm_screenDC = GetDC(IntPtr.Zero)
      MainForm_memDC = CreateCompatibleDC(MainForm_screenDC)
      FormCreateDIBSection(New Size(1, 1))
    End If
  End Sub

  Public Sub RecreateBitmaps(ByVal bufferBitmapSize As Size)
    FormDisposeDIBSection()
    FormCreateDIBSection(bufferBitmapSize)
  End Sub

  Private Sub DisposeResources()
    If MainForm_hBackBitmap <> IntPtr.Zero Then
      FormDisposeDIBSection()
      ReleaseDC(IntPtr.Zero, MainForm_screenDC)
      MainForm_screenDC = IntPtr.Zero
      DeleteDC(MainForm_memDC)
      MainForm_memDC = IntPtr.Zero
    End If
  End Sub

  Private Sub FormCreateDIBSection(ByVal bitmapSize As Size)
    Dim bitmapInfo As New BITMAPINFO_FLAT
    Dim bmh As New BITMAPINFOHEADER
    bitmapInfo.bmiHeader_biSize = Marshal.SizeOf(bmh)
    bitmapInfo.bmiHeader_biWidth = bitmapSize.Width
    bitmapInfo.bmiHeader_biHeight = bitmapSize.Height
    bitmapInfo.bmiHeader_biPlanes = 1
    bitmapInfo.bmiHeader_biBitCount = 32
    bitmapInfo.bmiHeader_biCompression = 0
    bitmapInfo.bmiHeader_biSizeImage = bitmapInfo.bmiHeader_biWidth * bitmapInfo.bmiHeader_biHeight * 4
    MainForm_hBackBitmap = CreateDIBSection(MainForm_memDC, bitmapInfo, 0, MainForm_pDIBRawBits, IntPtr.Zero, 0)
    MainForm_backBitmapSize = bitmapSize
    MainForm_oldBitmap = SelectObject(MainForm_memDC, MainForm_hBackBitmap)
  End Sub

  Private Sub FormDisposeDIBSection()
    If MainForm_hBackBitmap <> IntPtr.Zero Then
      SelectObject(MainForm_memDC, MainForm_oldBitmap)
      DeleteObject(MainForm_hBackBitmap)
      MainForm_hBackBitmap = IntPtr.Zero
      MainForm_pDIBRawBits = IntPtr.Zero
      MainForm_oldBitmap = IntPtr.Zero
    End If
  End Sub

#End Region

#Region "Menu Routines"

  Private Sub mnuInfoBar_Closed(ByVal sender As Object, ByVal e As System.Windows.Forms.ToolStripDropDownClosedEventArgs) Handles mnuInfoBar.Closed
    If IBSettings.General.AutoHide Then tmrAutohide.Start()
  End Sub

  Private Sub mnuInfoBar_ItemClicked(ByVal sender As Object, ByVal e As System.Windows.Forms.ToolStripItemClickedEventArgs) Handles mnuInfoBar.ItemClicked

    fMain.mnuInfoBar.Close()

    If TypeOf e.ClickedItem Is ToolStripSeparator Then Exit Sub

    Dim menuItem As ToolStripMenuItem = DirectCast(e.ClickedItem, ToolStripMenuItem)

    Select Case menuItem.Name

      Case "mnuInfoBar_Settings"
        If frmSettings.IsHandleCreated Then
          frmSettings.Activate()
        Else
          frmSettings.Show()
        End If

      Case "mnuInfoBar_AutoHide"
        If menuItem.CheckState = CheckState.Unchecked Then
          menuItem.CheckState = CheckState.Checked
          IBSettings.General.AutoHide = True
          fMain.tmrAutohide.Start()
          ABSetAutoHide(True)
          SetTopMost(fMain.Handle, True)
        Else
          menuItem.CheckState = CheckState.Unchecked
          IBSettings.General.AutoHide = False
          fMain.tmrAutohideAnimation.Stop()
          fMain.tmrAutohide.Stop()
          ABSetAutoHide(False)
          SetTopMost(fMain.Handle, IBSettings.General.AlwaysOnTop)
        End If

      Case "mnuInfoBar_Minimize"
        MinimizeToTray(True, False)

      Case "mnuInfoBar_Exit"
        fMain.Close()

      Case Else
        AvailableModules(RightClickedModule).ProcessMenuItemClick(menuItem.Name)

    End Select

  End Sub

  Private Sub mnuTray_Close_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuTray_Close.Click
    Me.Close()
  End Sub

  Private Sub mnuTray_Restore_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuTray_Restore.Click
    MinimizeToTray(False)
  End Sub

#End Region

#Region "Tray Icon Routines"

  Private Sub tiMain_BalloonTipClicked(ByVal sender As Object, ByVal e As System.EventArgs) Handles tiMain.BalloonTipClicked
    Select Case tiMain.BalloonTipTitle

      Case "New Module Installed"
        If Me.Visible = True Then tiMain.Visible = False
        frmSettings.Show()
        frmSettings.tvSettings.SelectedNode = frmSettings.tvSettings.Nodes("MODULES")
        frmSettings.panelModules.BringToFront()
        frmSettings.Activate()
        tiMain.Tag = vbNullString

      Case "New Skin Installed"
        If Me.Visible = True Then tiMain.Visible = False
        frmSettings.Show()
        frmSettings.tvSettings.SelectedNode = frmSettings.tvSettings.Nodes("SKINS")
        frmSettings.panelModules.BringToFront()
        frmSettings.Activate()
        tiMain.Tag = vbNullString

      Case "New Icon Theme Installed"
        If Me.Visible = True Then tiMain.Visible = False
        IBSettings.CurrentIconTheme = tiMain.Tag
        Icons_LoadIconTheme()
        Skinning_UpdateWindow()
        tiMain.Tag = vbNullString

      Case "InfoBar is already running."
        If Me.Visible = True Then tiMain.Visible = False

      Case Else
        IBSettings.General.ShowBalloonPopup = False
    End Select
  End Sub

  Private Sub tiMain_BalloonTipClosed(ByVal sender As Object, ByVal e As System.EventArgs) Handles tiMain.BalloonTipClosed
    If tiMain.Tag <> vbNullString Then
      tiMain.Tag = vbNullString
      If Me.Visible = True Then tiMain.Visible = False
    End If
  End Sub

  Private Sub tiMain_MouseDoubleClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles tiMain.MouseDoubleClick
    MinimizeToTray(False)
  End Sub

#End Region

#Region "Autohide Timers"

  Private Sub tmrAutohide_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tmrAutohide.Tick
    tmrAutohide.Stop()

    If mnuInfoBar.Visible Then Exit Sub
    If frmSettings.Visible Then Exit Sub

    tmrAutohideAnimation.Start()
  End Sub

  Private Sub tmrAutohideAnimation_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tmrAutohideAnimation.Tick
    Dim bHangover As Integer = 1
    If IsWindowsSeven() Then bHangover = 2
    If IBSettings.General.Position = ABEdge.ABE_TOP Then
      If Me.Top <= -(Me.Height - bHangover) Then
        tmrAutohideAnimation.Stop()
        Me.Top = -(Me.Height - bHangover)
      Else
        SetWindowPos(Me.Handle, HWND_TOPMOST, 0, Me.Top - IBSettings.Advanced.AutohideAnimationSpeed, Me.Width, Me.Height, SWP_NOSIZE Or SWP_NOACTIVATE)
      End If
    Else
      If Me.Top >= Screen.PrimaryScreen.WorkingArea.Height - bHangover Then
        tmrAutohideAnimation.Stop()
        Me.Top = Screen.PrimaryScreen.WorkingArea.Height - bHangover
      Else
        SetWindowPos(Me.Handle, HWND_TOPMOST, 0, Me.Top + IBSettings.Advanced.AutohideAnimationSpeed, Me.Width, Me.Height, SWP_NOSIZE Or SWP_NOACTIVATE)
      End If
    End If
  End Sub

#End Region

End Class