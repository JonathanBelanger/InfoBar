Module modSkinTooltip

  Public TooltipUpdateInProgress As Boolean

  Public Sub Skinning_DrawTooltip()
    If TooltipForm_memDC = IntPtr.Zero Then Exit Sub
    If TooltipUpdateInProgress Then Exit Sub
    If CurrentTooltipOwnerGUID = vbNullString Then Exit Sub
    If AvailableModules.Contains(CurrentTooltipOwnerGUID) = False Then Exit Sub
    If CurrentTooltipOwnerObjectID = vbNullString Then Exit Sub
    TooltipUpdateInProgress = True

    Dim ttBitmap As Bitmap = AvailableModules(CurrentTooltipOwnerGUID).GetTooltipBitmap
    If ttBitmap IsNot Nothing Then
      Try

        ' Measure Tooltip Content
        Dim tr As Rectangle
        tr.X = 0 : tr.Y = 0
        tr.Width = ttBitmap.Width + CurrentSkinInfo.Tooltip.ContentMargins.Left + CurrentSkinInfo.Tooltip.ContentMargins.Right
        tr.Height = ttBitmap.Height + CurrentSkinInfo.Tooltip.ContentMargins.Top + CurrentSkinInfo.Tooltip.ContentMargins.Bottom

        Dim bitmapsRecreated As Boolean = False
        If fTooltip.Size <> tr.Size Then
          fTooltip.Size = tr.Size
          fTooltip.RecreateBitmaps(tr.Size)
          Skinning_DrawTooltipBackground()
          Skinning_DrawBlurMask()
          bitmapsRecreated = True
        End If

        'Draw Tooltip
        Dim Gr As Graphics = Graphics.FromHdc(TooltipForm_memDC)
        Gr.CompositingQuality = CompositingQuality.HighQuality
        Gr.InterpolationMode = InterpolationMode.HighQualityBicubic
        Gr.PixelOffsetMode = PixelOffsetMode.HighQuality
        Gr.SmoothingMode = SmoothingMode.HighQuality
        Gr.TextContrast = 0
        Gr.Clear(Color.Transparent)
        Gr.DrawImageUnscaled(TooltipForm_backBitmap, 0, 0)
        Gr.DrawImageUnscaled(ttBitmap, CurrentSkinInfo.Tooltip.ContentMargins.Left, CurrentSkinInfo.Tooltip.ContentMargins.Top)
        Gr.Dispose()
        ttBitmap.Dispose()

      Catch ex As Exception
      End Try
    End If

    TooltipUpdateInProgress = False
  End Sub

  Private Sub Skinning_DrawTooltipBackground()
    If TooltipForm_backBitmapSize.Width = 0 Or TooltipForm_backBitmapSize.Height = 0 Then Exit Sub

    TooltipForm_backBitmap = New Bitmap(TooltipForm_backBitmapSize.Width, TooltipForm_backBitmapSize.Height, PixelFormat.Format32bppArgb)
    Dim Gr As Graphics
    Gr = Graphics.FromImage(TooltipForm_backBitmap)
    'Gr.CompositingQuality = CompositingQuality.HighQuality
    'Gr.InterpolationMode = InterpolationMode.HighQualityBicubic
    'Gr.PixelOffsetMode = PixelOffsetMode.HighQuality
    'Gr.SmoothingMode = SmoothingMode.HighQuality
    Gr.Clear(Color.Transparent)

    Dim tbRect As New Rectangle(0, 0, TooltipForm_backBitmapSize.Width, TooltipForm_backBitmapSize.Height)

    Dim imgAttrib As New ImageAttributes
    If CurrentSkinInfo.Tooltip.SizingType = SkinSizingType.Tile Then
      imgAttrib.SetWrapMode(Drawing2D.WrapMode.Tile)
    Else
      imgAttrib.SetWrapMode(WrapMode.Clamp)
    End If

    Dim srcRect As Rectangle
    Dim dstRect As New Rectangle(New Point(0, 0), TooltipForm_backBitmapSize)
    Dim img As Bitmap

    If IsCompositionEnabled() = False AndAlso CurrentSkinInfo.Tooltip.ImageNoGlass IsNot Nothing Then
      img = CurrentSkinInfo.Tooltip.ImageNoGlass
    Else
      img = CurrentSkinInfo.Tooltip.Image
    End If
    srcRect = New Rectangle(New Point(0, 0), img.Size)
    Skinning_DrawNineSlice(Gr, img, CurrentSkinInfo.Tooltip.SizingType, _
                           CurrentSkinInfo.Tooltip.SizingMargins, srcRect, dstRect)
    Gr.Dispose()
  End Sub

  Private Sub Skinning_DrawBlurMask()
    ' Generate blur mask and apply blur if needed
    If IsCompositionEnabled() Then
      If CurrentSkinInfo.Tooltip.EnableGlass Then
        If CurrentSkinInfo.Tooltip.MaskImage IsNot Nothing Then
          Dim MyMask As Bitmap = New Bitmap(TooltipForm_backBitmapSize.Width, TooltipForm_backBitmapSize.Height, PixelFormat.Format32bppArgb)
          Dim GrMask As Graphics = Graphics.FromImage(MyMask)
          GrMask.CompositingQuality = CompositingQuality.Default
          GrMask.InterpolationMode = InterpolationMode.Default
          GrMask.SmoothingMode = SmoothingMode.Default
          GrMask.PixelOffsetMode = PixelOffsetMode.Default
          Skinning_DrawTooltipBackgroundMask(GrMask, MyMask.Size)

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
          DwmEnableBlurBehindWindow(fTooltip.Handle, bb)

          rgn.Dispose()
          GrMask.Dispose()
          MyMask.Dispose()

        End If

      Else

        Dim bb As DWM_BLURBEHIND
        bb.dwFlags = DWM_BB_ENABLE
        bb.fEnable = False
        bb.fTransitionOnMaximized = False
        DwmEnableBlurBehindWindow(fTooltip.Handle, bb)

      End If

    End If
  End Sub

  Private Sub Skinning_DrawTooltipBackgroundMask(ByRef GR As Graphics, ByVal sz As Size)
    Dim srcRect As New Rectangle(New Point(0, 0), CurrentSkinInfo.Tooltip.MaskImage.Size)
    Dim dstRect As New Rectangle(New Point(0, 0), sz)
    GR.InterpolationMode = InterpolationMode.Default
    Skinning_DrawNineSlice(GR, CurrentSkinInfo.Tooltip.MaskImage, CurrentSkinInfo.Tooltip.SizingType, _
                          CurrentSkinInfo.Tooltip.SizingMargins, srcRect, dstRect)
    GR.InterpolationMode = InterpolationMode.HighQualityBicubic
  End Sub

  Public Sub Skinning_DrawTooltipSeparator(ByRef GR As Graphics, ByVal Rect As Rectangle)
    Dim srcRect As Rectangle = New Rectangle(New Point(0, 0), CurrentSkinInfo.Tooltip.Separator.Image.Size)
    Dim dstRect As Rectangle = Rect
    GR.InterpolationMode = InterpolationMode.Default
    Skinning_DrawNineSlice(GR, CurrentSkinInfo.Tooltip.Separator.Image, _
                           CurrentSkinInfo.Tooltip.Separator.SizingType, _
                           CurrentSkinInfo.Tooltip.Separator.SizingMargins, _
                           srcRect, dstRect)
    GR.InterpolationMode = InterpolationMode.HighQualityBicubic
  End Sub

End Module
