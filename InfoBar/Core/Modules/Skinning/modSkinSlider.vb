Module modSkinSlider

  Public Function Skinning_DrawSlider(ByRef Gr As Graphics, ByVal Rect As Rectangle, ByVal State As Integer, ByVal Minimum As Integer, ByVal Maximum As Integer, ByVal Value As Integer) As Rectangle
    ' Draw Slider Background (Bar)
    Gr.InterpolationMode = InterpolationMode.Default
    Rect.Width -= CurrentSkinInfo.Slider.Button.ImageSize.Width

    Dim srcRect As New Rectangle(0, 0, CurrentSkinInfo.Slider.Background.ImageSize.Width, CurrentSkinInfo.Slider.Background.ImageSize.Height)
    Dim dstRect As Rectangle = Rect
    dstRect.X += (CurrentSkinInfo.Slider.Button.ImageSize.Width / 2)
    dstRect.Y = (dstRect.Height - srcRect.Height) / 2
    dstRect.Width = dstRect.Width
    dstRect.Height = srcRect.Height
    Skinning_DrawNineSlice(Gr, CurrentSkinInfo.Slider.Background.Image, _
                           CurrentSkinInfo.Slider.Background.SizingType, _
                           CurrentSkinInfo.Slider.Background.SizingMargins, _
                           srcRect, dstRect)

    ' Draw Slider Button
    Dim imgAttrib As New ImageAttributes
    If CurrentSkinInfo.Slider.Button.SizingType = SkinSizingType.Tile Then
      imgAttrib.SetWrapMode(WrapMode.Tile)
    Else
      imgAttrib.SetWrapMode(WrapMode.Clamp)
    End If
    Dim destRect As Rectangle
    Dim srcWidth As Integer = CurrentSkinInfo.Slider.Button.ImageSize.Width
    Dim srcHeight As Integer = CurrentSkinInfo.Slider.Button.ImageSize.Height / 5
    Dim srcTop As Integer = (srcHeight * State)
    Rect.X = Rect.X + (Value / (Maximum - Minimum)) * Rect.Width
    destRect = New Rectangle(Rect.X, (Rect.Height - srcHeight) / 2, srcWidth, srcHeight)
    Gr.DrawImage(CurrentSkinInfo.Slider.Button.Image, destRect, 0, srcTop, srcWidth, srcHeight, GraphicsUnit.Pixel)
    Gr.InterpolationMode = InterpolationMode.HighQualityBicubic

    Return destRect
  End Function

End Module
