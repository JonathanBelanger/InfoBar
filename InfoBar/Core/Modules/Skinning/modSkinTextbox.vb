Module modSkinTextbox

  Public Sub Skinning_DrawTextBox(ByRef GR As Graphics, ByVal BoxRect As Rectangle)
    GR.InterpolationMode = InterpolationMode.Default
    Dim srcRect As New Rectangle(New Point(0, 0), CurrentSkinInfo.TextBox.Image.Size)
    Skinning_DrawNineSlice(GR, CurrentSkinInfo.TextBox.Image, CurrentSkinInfo.TextBox.SizingType, _
                           CurrentSkinInfo.TextBox.SizingMargins, srcRect, BoxRect)
    GR.InterpolationMode = InterpolationMode.HighQualityBicubic
  End Sub

End Module
