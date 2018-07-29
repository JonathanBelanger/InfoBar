Module modSkinGraph

  Public Sub Skinning_DrawGraphBackground(ByRef GR As Graphics, ByVal BoxRect As Rectangle)
    GR.InterpolationMode = InterpolationMode.Default
    Dim srcRect As New Rectangle(New Point(0, 0), CurrentSkinInfo.Graph.Image.Size)
    Skinning_DrawNineSlice(GR, CurrentSkinInfo.Graph.Image, CurrentSkinInfo.Graph.SizingType, _
                           CurrentSkinInfo.Graph.SizingMargins, srcRect, BoxRect)

    ' Draw Grid Lines
    Dim gcm As SkinMargins = CurrentSkinInfo.Graph.ContentMargins
    BoxRect.X = BoxRect.X + gcm.Left
    BoxRect.Y = BoxRect.Y + gcm.Top
    BoxRect.Width = BoxRect.Width - (gcm.Left + gcm.Right)
    BoxRect.Height = BoxRect.Height - (gcm.Top + gcm.Bottom)

    Dim gi As Integer
    Dim gp As New Pen(CurrentSkinInfo.Graph.GridLineColor)
    For gi = BoxRect.X To (BoxRect.X + BoxRect.Width) Step 4
      GR.DrawLine(gp, gi, BoxRect.Y, gi, BoxRect.Y + BoxRect.Height)
    Next
    For gi = BoxRect.Y To (BoxRect.Y + BoxRect.Height) Step 4
      GR.DrawLine(gp, BoxRect.X, gi, BoxRect.X + BoxRect.Width, gi)
    Next
    gp.Dispose()

    GR.InterpolationMode = InterpolationMode.HighQualityBicubic
  End Sub

End Module
