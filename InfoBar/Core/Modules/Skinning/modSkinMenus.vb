Module modSkinMenus

  Public Sub Skinning_DrawMenuBackground(ByRef Gr As Graphics, ByVal Rect As Rectangle)
    Dim srcRect As New Rectangle(New Point(0, 0), CurrentSkinInfo.Menu.Background.Image.Size)
    Gr.InterpolationMode = InterpolationMode.Default
    Skinning_DrawNineSlice(Gr, CurrentSkinInfo.Menu.Background.Image, CurrentSkinInfo.Menu.Background.SizingType, _
                           CurrentSkinInfo.Menu.Background.SizingMargins, srcRect, Rect)
    Gr.InterpolationMode = InterpolationMode.HighQualityBicubic
  End Sub

  Public Sub Skinning_DrawMenuMargin(ByRef Gr As Graphics, ByVal Rect As Rectangle)
    Dim srcRect As New Rectangle(New Point(0, 0), CurrentSkinInfo.Menu.Margin.Image.Size)
    Gr.InterpolationMode = InterpolationMode.Default
    Skinning_DrawNineSlice(Gr, CurrentSkinInfo.Menu.Margin.Image, CurrentSkinInfo.Menu.Margin.SizingType, _
                           CurrentSkinInfo.Menu.Margin.SizingMargins, srcRect, Rect)
    Gr.InterpolationMode = InterpolationMode.HighQualityBicubic
  End Sub

  Public Sub Skinning_DrawMenuItemBackground(ByRef Gr As Graphics, ByVal Rect As Rectangle, ByVal ShowImageMargin As Boolean)
    Dim srcHeight As Integer = CurrentSkinInfo.Menu.Item.ImageSize.Height / 2
    Dim srcRect As New Rectangle(0, IIf(ShowImageMargin, srcHeight, 0), _
                                     CurrentSkinInfo.Menu.Item.ImageSize.Width, srcHeight)
    Gr.InterpolationMode = InterpolationMode.Default
    Skinning_DrawNineSlice(Gr, CurrentSkinInfo.Menu.Item.Image, CurrentSkinInfo.Menu.Item.SizingType, _
                          CurrentSkinInfo.Menu.Item.SizingMargins, srcRect, Rect)
    Gr.InterpolationMode = InterpolationMode.HighQualityBicubic
  End Sub

  Public Sub Skinning_DrawMenuCheck(ByRef Gr As Graphics, ByVal Rect As Rectangle)
    Dim srcHeight As Integer = (CurrentSkinInfo.Menu.CheckRadio.ImageSize.Height / 2)
    Gr.DrawImage(CurrentSkinInfo.Menu.CheckRadio.Image, Rect, 0, 0, CurrentSkinInfo.Menu.CheckRadio.ImageSize.Width, srcHeight, GraphicsUnit.Pixel)
  End Sub

  Public Sub Skinning_DrawMenuRadio(ByRef Gr As Graphics, ByVal Rect As Rectangle)
    Dim srcHeight As Integer = (CurrentSkinInfo.Menu.CheckRadio.ImageSize.Height / 2)
    Gr.DrawImage(CurrentSkinInfo.Menu.CheckRadio.Image, Rect, 0, srcHeight, CurrentSkinInfo.Menu.CheckRadio.ImageSize.Width, srcHeight, GraphicsUnit.Pixel)
  End Sub

  Public Sub Skinning_DrawMenuSeparator(ByRef Gr As Graphics, ByVal Rect As Rectangle, ByVal ShowImageMargin As Boolean)
    Dim srcHeight As Integer = CurrentSkinInfo.Menu.Separator.ImageSize.Height / 2
    Dim srcRect As New Rectangle(0, IIf(ShowImageMargin, srcHeight, 0), _
                                 CurrentSkinInfo.Menu.Separator.ImageSize.Width, srcHeight)
    Gr.InterpolationMode = InterpolationMode.Default
    Skinning_DrawNineSlice(Gr, CurrentSkinInfo.Menu.Separator.Image, CurrentSkinInfo.Menu.Separator.SizingType, _
                           CurrentSkinInfo.Menu.Separator.SizingMargins, srcRect, Rect)
    Gr.InterpolationMode = InterpolationMode.HighQualityBicubic
  End Sub

End Module