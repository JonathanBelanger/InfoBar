Module modSkinSeparators

  Public Sub Skinning_DrawVerticalSeparator(ByRef Gr As Graphics, ByVal tbRect As Rectangle)
    Dim srcRect As New Rectangle(New Point(0, 0), CurrentSkinInfo.VerticalSeparator.Image.Size)
    Skinning_DrawNineSlice(Gr, CurrentSkinInfo.VerticalSeparator.Image, _
      CurrentSkinInfo.VerticalSeparator.SizingType, CurrentSkinInfo.VerticalSeparator.SizingMargins, _
      srcRect, tbRect)
  End Sub

  Public Sub Skinning_DrawSeparator()
    MainForm_sepBitmap = New Bitmap(CurrentSkinInfo.Separator.ImageSize.Width, CurrentSkinInfo.Separator.ImageSize.Height, PixelFormat.Format32bppArgb)
    Dim GR As Graphics = Graphics.FromImage(MainForm_sepBitmap)
    GR.DrawImageUnscaled(CurrentSkinInfo.Separator.Image, 0, 0)
    GR.Dispose()
  End Sub

End Module
