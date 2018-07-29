Friend Class frmTooltip

  Dim fadeIn As Boolean
  Dim curOpacity As Integer

  Public Sub New()
    InitializeComponent()
    CreateBufferBitmaps()
  End Sub

  Protected Overrides ReadOnly Property CreateParams() As CreateParams
    Get
      Dim cp As CreateParams = MyBase.CreateParams
      cp.ExStyle = cp.ExStyle Or (WindowExStyles.WS_EX_LAYERED Or WindowExStyles.WS_EX_NOACTIVATE)
      Return cp
    End Get
  End Property

  Private Sub frmTooltip_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Me.MouseMove
    If Me.Bounds.Contains(Cursor.Position) Then
      CurrentTooltipOwnerGUID = Nothing
      CurrentTooltipOwnerObjectID = Nothing
      HideTooltip(True)
    End If
  End Sub

  Private Sub frmTooltip_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
    tmrFade.Stop()
    tmrFade.Dispose()
    DisposeResources()
    Me.Dispose()
  End Sub

  Public Sub ShowTooltip()
    If (CurrentTooltipOwnerGUID <> NewTooltipOwnerGUID) Or (CurrentTooltipOwnerObjectID <> NewTooltipOwnerObjectID) Then
      CurrentTooltipOwnerGUID = NewTooltipOwnerGUID
      CurrentTooltipOwnerObjectID = NewTooltipOwnerObjectID

      Skinning_DrawTooltip()

      Me.Left = Cursor.Position.X
      If IBSettings.General.Position = ABEdge.ABE_TOP Then
        Me.Top = Cursor.Position.Y + (Cursor.Size.Height / 2)
        If Me.Top < (fMain.Top + fMain.Height) Then Me.Top = fMain.Top + fMain.Height
      Else
        Me.Top = fMain.Top - Me.Height
      End If
      If Me.Left + Me.Width > Screen.PrimaryScreen.Bounds.Width Then Me.Left = Screen.PrimaryScreen.Bounds.Width - Me.Width
      If IBSettings.General.Position = ABEdge.ABE_BOTTOM Then
        If Me.Top + Me.Height > Screen.PrimaryScreen.Bounds.Height Then Me.Top = Screen.PrimaryScreen.Bounds.Height - Me.Height
        If Me.Top + Me.Height > fMain.Top Then Me.Top = fMain.Top - Me.Height
        If Me.Top < (fMain.Top - fMain.Height) Then Me.Top = fMain.Top - Me.Height
      End If

      SetTopMost(Me.Handle, True)

      If curOpacity <> 255 Then
        If IBSettings.Advanced.EnableTooltipFade Then
          fadeIn = True
          tmrFade.Start()
        Else
          curOpacity = 255
          If fTooltip.IsDisposed = False Then
            Dim del As New frmTooltip.SetWindowBitmapDelegate(AddressOf fTooltip.SetWindowBitmap)
            fTooltip.Invoke(del, New Object() {curOpacity})
          End If
        End If
      Else
        If fTooltip.IsDisposed = False Then
          Dim del As New frmTooltip.SetWindowBitmapDelegate(AddressOf fTooltip.SetWindowBitmap)
          fTooltip.Invoke(del, New Object() {curOpacity})
        End If
      End If

    End If

  End Sub

  Public Sub UpdateTooltip()
    Skinning_DrawTooltip()

    If Me.Left + Me.Width > Screen.PrimaryScreen.Bounds.Width Then Me.Left = Screen.PrimaryScreen.Bounds.Width - Me.Width
    If IBSettings.General.Position = ABEdge.ABE_BOTTOM Then
      If Me.Top + Me.Height > Screen.PrimaryScreen.Bounds.Height Then Me.Top = Screen.PrimaryScreen.Bounds.Height - Me.Height
      If Me.Top + Me.Height > fMain.Top Then Me.Top = fMain.Top - Me.Height
      If Me.Top < (fMain.Top - fMain.Height) Then Me.Top = fMain.Top - Me.Height
    End If

    If fTooltip.IsDisposed = False Then
      Dim del As New frmTooltip.SetWindowBitmapDelegate(AddressOf fTooltip.SetWindowBitmap)
      fTooltip.Invoke(del, New Object() {curOpacity})
    End If
  End Sub

  Public Sub HideTooltip(ByVal SkipFade As Boolean)
    fadeIn = False
    If SkipFade Or IBSettings.Advanced.EnableTooltipFade = False Then
      curOpacity = 0
      If fTooltip.IsDisposed = False Then
        Dim del As New frmTooltip.SetWindowBitmapDelegate(AddressOf fTooltip.SetWindowBitmap)
        fTooltip.Invoke(del, New Object() {curOpacity})
      End If
    Else
      tmrFade.Start()
    End If
    CurrentTooltipOwnerGUID = vbNullString
    CurrentTooltipOwnerObjectID = vbNullString
  End Sub

  Private Sub tmrFade_Tick(ByVal sender As Object, ByVal e As System.EventArgs) Handles tmrFade.Tick
    If fadeIn = True Then
      If (CurrentTooltipOwnerGUID = vbNullString) AndAlso (CurrentTooltipOwnerObjectID = vbNullString) Then
        fadeIn = False
      End If
      curOpacity += 20
      If curOpacity >= 255 Then
        curOpacity = 255
        tmrFade.Stop()
      End If
    Else
      If (CurrentTooltipOwnerGUID <> vbNullString) AndAlso (CurrentTooltipOwnerObjectID <> vbNullString) Then
        fadeIn = True
      End If
      curOpacity -= 20
      If curOpacity <= 0 Then
        curOpacity = 0
        tmrFade.Stop()
      End If
    End If

    If fTooltip.IsDisposed = False Then
      Dim del As New frmTooltip.SetWindowBitmapDelegate(AddressOf fTooltip.SetWindowBitmap)
      fTooltip.Invoke(del, New Object() {curOpacity})
    End If
  End Sub

#Region "Layered Window Routines"

  Public Delegate Sub SetWindowBitmapDelegate(ByVal Alpha As Integer)

  Public Sub SetWindowBitmap(Optional ByVal Alpha As Integer = 255)
    If Alpha < 0 Then Alpha = 0
    If Alpha > 255 Then Alpha = 255
    Dim pointMemory As Point = New Point(0, 0)
    Dim blend As BLENDFUNCTION = New BLENDFUNCTION
    blend.BlendOp = AC_SRC_OVER
    blend.BlendFlags = 0
    blend.SourceConstantAlpha = Alpha
    blend.AlphaFormat = AC_SRC_ALPHA
    UpdateLayeredWindow(Me.Handle, TooltipForm_screenDC, Me.Location, Me.Size, TooltipForm_memDC, pointMemory, 0, blend, ULW_ALPHA)
  End Sub

  Private Sub CreateBufferBitmaps()
    If TooltipForm_hBackBitmap = IntPtr.Zero Then
      TooltipForm_screenDC = GetDC(IntPtr.Zero)
      TooltipForm_memDC = CreateCompatibleDC(TooltipForm_screenDC)
      FormCreateDIBSection(New Size(1, 1))
    End If
  End Sub

  Public Sub RecreateBitmaps(ByVal bufferBitmapSize As Size)
    FormDisposeDIBSection()
    FormCreateDIBSection(bufferBitmapSize)
  End Sub

  Private Sub DisposeResources()
    If TooltipForm_hBackBitmap <> IntPtr.Zero Then
      FormDisposeDIBSection()
      ReleaseDC(IntPtr.Zero, TooltipForm_screenDC)
      TooltipForm_screenDC = IntPtr.Zero
      DeleteDC(TooltipForm_memDC)
      TooltipForm_memDC = IntPtr.Zero
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
    TooltipForm_hBackBitmap = CreateDIBSection(TooltipForm_memDC, bitmapInfo, 0, TooltipForm_pDIBRawBits, IntPtr.Zero, 0)
    TooltipForm_backBitmapSize = bitmapSize
    TooltipForm_oldBitmap = SelectObject(TooltipForm_memDC, TooltipForm_hBackBitmap)
  End Sub

  Private Sub FormDisposeDIBSection()
    If TooltipForm_hBackBitmap <> IntPtr.Zero Then
      SelectObject(TooltipForm_memDC, TooltipForm_oldBitmap)
      DeleteObject(TooltipForm_hBackBitmap)
      TooltipForm_hBackBitmap = IntPtr.Zero
      TooltipForm_pDIBRawBits = IntPtr.Zero
      TooltipForm_oldBitmap = IntPtr.Zero
    End If
  End Sub

#End Region

End Class