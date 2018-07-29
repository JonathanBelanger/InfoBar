Friend Class frmSplash

  Dim windowSize As New Size(300, 200)
  Dim textRect As New Rectangle(20, 140, 260, 40)

#Region "Form Setup"

  Dim backBitmap As Bitmap
  Dim memDC As IntPtr
  Dim hBackBitmap As IntPtr
  Dim pDIBRawBits As IntPtr
  Dim oldBitmap As IntPtr
  Dim screenDC As IntPtr

  Protected Overrides ReadOnly Property CreateParams() As CreateParams
    Get
      Dim cp As CreateParams = MyBase.CreateParams
      cp.ExStyle = cp.ExStyle Or WindowExStyles.WS_EX_LAYERED
      Return cp
    End Get
  End Property

#End Region

#Region "Form Load/Unload"

  Private Sub frmSplash_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
    InitDrawing()
    UpdateStatus("Loading InfoBar...")
    Me.CenterToScreen()
    Me.Show()
    Application.DoEvents()

    fMain = New frmMain
    fMain.CreateControl()
    fMain.Bounds = New Rectangle(-100, -100, 1, 1)
    fMain.Show()

    fTooltip = New frmTooltip
    fTooltip.CreateControl()
    fTooltip.Bounds = New Rectangle(-100, -100, 1, 1)
    fTooltip.Show()

    ' Enumerate Skins and Icons
    UpdateStatus("Enumerating Icon Themes...")
    Application.DoEvents()
    Icons_EnumerateThemes()

    UpdateStatus("Enumerating Skins...")
    Application.DoEvents()
    Skinning_EnumerateSkins()

    ' Enumerate Modules
    UpdateStatus("Enumerating Modules...")
    Application.DoEvents()
    Modules_EnumerateModules()

    ' Load Settings
    UpdateStatus("Loading Settings...")
    Application.DoEvents()
    Settings_CheckRunAtWindowsStartup()
    Settings_LoadFromXML()

    ' Load Skins and Icons
    UpdateStatus("Loading Icon Theme...")
    Application.DoEvents()
    Icons_LoadIconTheme()

    UpdateStatus("Loading Skin...")
    Application.DoEvents()
    Skinning_LoadSkin()

    ' Load Module Settings
    UpdateStatus("Loading Module Settings...")
    Application.DoEvents()
    Settings_LoadModuleSettings()

    ' Initialize Modules
    UpdateStatus("Loading Modules...")
    Application.DoEvents()
    For Each sModule As SelectedModuleType In IBSettings.SelectedModules
      Application.DoEvents()
      If AvailableModules.Contains(sModule.GUID) = True Then
        AvailableModules(sModule.GUID).ModuleEnabled = True
        AvailableModules(sModule.GUID).InitializeModule()
      End If
    Next
    Skinning_UpdateWindow()

    ' Setup AppBar
    TaskbarRestartCallBack = RegisterWindowMessage("TaskbarCreated")
    If IBSettings.General.AutoHide Then
      fMain.tmrAutohide.Start()
      ABSetAutoHide(True)
      SetTopMost(fMain.Handle, True)
    Else
      fMain.tmrAutohideAnimation.Stop()
      fMain.tmrAutohide.Stop()
      ABSetAutoHide(False)
      SetTopMost(fMain.Handle, IBSettings.General.AlwaysOnTop)
    End If
    fMain.Show()
    fMain.tmrUpdate.Start()

    bInfoBarLoaded = True

    Select bPostProcessShowMessage
      Case 1 ' Module
        With fMain.tiMain
          .BalloonTipIcon = ToolTipIcon.Info
          .BalloonTipText = "Click here to open the InfoBar settings dialog to enable the new module."
          .BalloonTipTitle = "New Module Installed"
          .Visible = True
          .ShowBalloonTip(10000)
        End With

      Case 2 ' Skin
        With fMain.tiMain
          .BalloonTipIcon = ToolTipIcon.Info
          .BalloonTipText = "Click here to open the InfoBar settings dialog to choose the new skin."
          .BalloonTipTitle = "New Skin Installed"
          .Visible = True
          .ShowBalloonTip(10000)
        End With

      Case 3 ' Icon
        With fMain.tiMain
          .BalloonTipIcon = ToolTipIcon.Info
          .BalloonTipText = "Click here to apply this icon theme now."
          .BalloonTipTitle = "New Icon Theme Installed"
          .Visible = True
          .ShowBalloonTip(10000)
        End With

    End Select

    Me.Close()
  End Sub

  Private Sub frmSplash_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
    DisposeResources()
  End Sub

#End Region

#Region "Drawing Routines"

  Private Sub InitDrawing()
    screenDC = GetDC(IntPtr.Zero)
    memDC = CreateCompatibleDC(screenDC)

    Dim bitmapInfo As New BITMAPINFO_FLAT
    Dim bmh As New BITMAPINFOHEADER
    bitmapInfo.bmiHeader_biSize = Marshal.SizeOf(bmh)
    bitmapInfo.bmiHeader_biWidth = windowSize.Width
    bitmapInfo.bmiHeader_biHeight = windowSize.Height
    bitmapInfo.bmiHeader_biPlanes = 1
    bitmapInfo.bmiHeader_biBitCount = 32
    bitmapInfo.bmiHeader_biCompression = 0
    bitmapInfo.bmiHeader_biSizeImage = bitmapInfo.bmiHeader_biWidth * bitmapInfo.bmiHeader_biHeight * 4
    hBackBitmap = CreateDIBSection(memDC, bitmapInfo, 0, pDIBRawBits, IntPtr.Zero, 0)
    oldBitmap = SelectObject(memDC, hBackBitmap)

    backBitmap = New Bitmap(windowSize.Width, windowSize.Height, PixelFormat.Format32bppArgb)
    Dim Gr As Graphics = Graphics.FromImage(backBitmap)
    Gr.CompositingQuality = CompositingQuality.AssumeLinear

    If (IsCompositionEnabled()) Then
      Gr.DrawImage(My.Resources.splashBack, 0, 0, My.Resources.splashBack.Width, My.Resources.splashBack.Height)
      SetBlurMask()
    Else
      Gr.DrawImage(My.Resources.splashBackNoGlass, 0, 0, My.Resources.splashBackNoGlass.Width, My.Resources.splashBackNoGlass.Height)
    End If

    Gr.Dispose()
  End Sub

  Private Sub UpdateStatus(ByVal sText As String)
    'Setup Our Bitmap And Graphics
    Dim Gr As Graphics = Graphics.FromHdc(memDC)
    Gr.CompositingQuality = CompositingQuality.AssumeLinear
    Gr.InterpolationMode = InterpolationMode.Default
    Gr.PixelOffsetMode = PixelOffsetMode.None
    Gr.SmoothingMode = SmoothingMode.AntiAlias
    Gr.Clear(Color.Transparent)

    'Draw background
    Gr.DrawImageUnscaled(backBitmap, 0, 0)

    ' Draw status text
    Dim textStyle As SkinText
    textStyle.Font = SystemFonts.MessageBoxFont
    textStyle.Color = Color.FromArgb(255, 255, 255, 255)
    textStyle.Effect.Type = SkinTextEffectType.Glow
    textStyle.Effect.Size = 5
    textStyle.Effect.Color = Color.FromArgb(32, 0, 0, 0)
    Skinning_DrawText(Gr, sText, textRect, textStyle, StringAlignment.Center, StringAlignment.Center, True)

    Gr.Dispose()

    SetWindowBitmap()
  End Sub

  Private Sub SetBlurMask()
    Dim MyMask As Bitmap = New Bitmap(windowSize.Width, windowSize.Height, PixelFormat.Format32bppArgb)
    Dim GrMask As Graphics = Graphics.FromImage(MyMask)
    GrMask.CompositingQuality = CompositingQuality.Default
    GrMask.InterpolationMode = InterpolationMode.Default
    GrMask.SmoothingMode = SmoothingMode.Default
    GrMask.PixelOffsetMode = PixelOffsetMode.Default
    GrMask.DrawImageUnscaled(My.Resources.splashMask, 0, 0)

    Dim rgn As Region = New Region()
    rgn.MakeEmpty()
    Dim rc As Rectangle = New Rectangle(0, 0, 0, 0)
    Dim inImage As Boolean = False
    Dim x As Integer, y As Integer
    For y = 0 To MyMask.Height - 1
      For x = 0 To MyMask.Width - 1
        If inImage = False Then
          If MyMask.GetPixel(x, y).A <> 0 Then
            inImage = True
            rc.X = x
            rc.Y = y
            rc.Height = 1
          End If
        Else
          If MyMask.GetPixel(x, y).A = 0 Then
            inImage = False
            rc.Width = x - rc.X
            rgn.Union(rc)
          End If
        End If
      Next x
      If inImage Then
        inImage = False
        rc.Width = MyMask.Width - rc.X
        rgn.Union(rc)
      End If
    Next y

    Dim bb As DWM_BLURBEHIND
    bb.dwFlags = DWM_BB_ENABLE Or DWM_BB_BLURREGION
    bb.fEnable = True
    bb.fTransitionOnMaximized = False
    bb.hRgnBlur = rgn.GetHrgn(GrMask)
    DwmEnableBlurBehindWindow(Me.Handle, bb)

    rgn.Dispose()
    GrMask.Dispose()
    MyMask.Dispose()
  End Sub

#End Region

#Region "Layered Window Routines"

  Private Sub SetWindowBitmap(Optional ByVal Alpha As Integer = 255)
    If Alpha < 0 Then Alpha = 0
    If Alpha > 255 Then Alpha = 255
    Dim pointMemory As Point = New Point(0, 0)
    Dim blend As BLENDFUNCTION = New BLENDFUNCTION
    blend.BlendOp = AC_SRC_OVER
    blend.BlendFlags = 0
    blend.SourceConstantAlpha = Alpha
    blend.AlphaFormat = AC_SRC_ALPHA
    UpdateLayeredWindow(Me.Handle, screenDC, Me.Location, Me.Size, memDC, pointMemory, 0, blend, ULW_ALPHA)
  End Sub

  Private Sub DisposeResources()
    If hBackBitmap <> IntPtr.Zero Then
      If hBackBitmap <> IntPtr.Zero Then
        SelectObject(memDC, oldBitmap)
        DeleteObject(hBackBitmap)
        hBackBitmap = IntPtr.Zero
        pDIBRawBits = IntPtr.Zero
        oldBitmap = IntPtr.Zero
      End If
      ReleaseDC(IntPtr.Zero, screenDC)
      screenDC = IntPtr.Zero
      DeleteDC(memDC)
      memDC = IntPtr.Zero
    End If
  End Sub

#End Region

End Class
