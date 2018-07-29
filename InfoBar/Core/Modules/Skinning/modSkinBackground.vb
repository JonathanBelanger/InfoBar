Module modSkinBackground

  Public Sub Skinning_DrawBackground()
    If MainForm_backBitmapSize.Width = 0 Or MainForm_backBitmapSize.Height = 0 Then Exit Sub

    MainForm_backBitmap = New Bitmap(MainForm_backBitmapSize.Width, MainForm_backBitmapSize.Height, PixelFormat.Format32bppArgb)
    Dim Gr As Graphics = Graphics.FromImage(MainForm_backBitmap)
    'Gr.CompositingQuality = CompositingQuality.GammaCorrected
    'Gr.InterpolationMode = InterpolationMode.Default
    'Gr.PixelOffsetMode = PixelOffsetMode.None
    'Gr.SmoothingMode = SmoothingMode.Default
    Gr.Clear(Color.Transparent)

    Dim tbRect As New Rectangle(0, 0, MainForm_backBitmapSize.Width, MainForm_backBitmapSize.Height)

    Dim imgAttrib As New ImageAttributes
    Dim iAlpha As Integer
    If IBSettings.Advanced.OverrideBackgroundOpacity = True Then
      iAlpha = IBSettings.Advanced.BackgroundOpacity
    Else
      iAlpha = CurrentSkinInfo.Background.Alpha
    End If
    If iAlpha > 255 Then iAlpha = 255
    If iAlpha < 0 Then iAlpha = 0

    Dim CM As New ColorMatrix
    Dim sAlpha As Single = (iAlpha / 255)
    CM.Matrix33 = sAlpha
    imgAttrib.SetColorMatrix(CM, ColorMatrixFlag.Default, ColorAdjustType.Bitmap)

    Dim img As Bitmap
    If IsCompositionEnabled() = False AndAlso CurrentSkinInfo.Background.ImageNoGlass IsNot Nothing Then
      img = CurrentSkinInfo.Background.ImageNoGlass
    Else
      img = CurrentSkinInfo.Background.Image
    End If

    Dim srcRect As New Rectangle(New Point(0, 0), img.Size)
    Dim dstRect As New Rectangle(New Point(0, 0), MainForm_backBitmapSize)
    Skinning_DrawNineSlice(Gr, img, CurrentSkinInfo.Background.SizingType, _
                           CurrentSkinInfo.Background.SizingMargins, srcRect, dstRect, imgAttrib)

    ' Draw Vertical Separators
    If IBSettings.General.Rows > 1 Then
      Dim I As Integer, rect As Rectangle
      For I = 1 To IBSettings.General.Rows - 1
        rect = New Rectangle(0, (CurrentSkinInfo.Height * I) + (CurrentSkinInfo.VerticalSeparator.ImageSize.Height * (I - 1)), MainForm_backBitmapSize.Width, CurrentSkinInfo.VerticalSeparator.ImageSize.Height)
        Skinning_DrawVerticalSeparator(Gr, rect)
      Next
    End If

    Gr.CompositingQuality = CompositingQuality.Default
  End Sub

End Module
