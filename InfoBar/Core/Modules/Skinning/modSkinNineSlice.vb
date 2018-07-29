Module modSkinNineSlice

  Public Sub Skinning_DrawNineSlice(ByRef GR As Graphics, ByRef Image As Image, ByRef sizingType As SkinSizingType, ByRef sizingMargins As SkinMargins, ByRef srcRect As Rectangle, ByRef dstRect As Rectangle, Optional ByRef imgAttrib As ImageAttributes = Nothing)
    On Error Resume Next

    Dim destRect As Rectangle
    If imgAttrib Is Nothing Then imgAttrib = New ImageAttributes
    If sizingType = SkinSizingType.Tile Then
      imgAttrib.SetWrapMode(WrapMode.Tile)
    Else
      imgAttrib.SetWrapMode(WrapMode.Clamp)
    End If

    ' Draw Top Left
    If sizingMargins.Left > 0 AndAlso sizingMargins.Top > 0 Then
      destRect = New Rectangle(dstRect.X, dstRect.Y, sizingMargins.Left, sizingMargins.Top)
      GR.DrawImage(Image, destRect, 0, srcRect.Y, sizingMargins.Left, sizingMargins.Top, GraphicsUnit.Pixel, imgAttrib)
    End If

    ' Draw Top Middle
    If sizingMargins.Top > 0 Then
      destRect = New Rectangle(dstRect.X + sizingMargins.Left, dstRect.Y, dstRect.Width - (sizingMargins.Left + sizingMargins.Right), sizingMargins.Top)
      GR.DrawImage(Image, destRect, sizingMargins.Left, srcRect.Y, srcRect.Width - (sizingMargins.Left + sizingMargins.Right), sizingMargins.Top, GraphicsUnit.Pixel, imgAttrib)
    End If

    ' Draw Top Right
    If sizingMargins.Right > 0 AndAlso sizingMargins.Top > 0 Then
      destRect = New Rectangle(dstRect.X + (dstRect.Width - sizingMargins.Right), dstRect.Y, sizingMargins.Right, sizingMargins.Top)
      GR.DrawImage(Image, destRect, srcRect.Width - sizingMargins.Right, srcRect.Y, sizingMargins.Right, sizingMargins.Top, GraphicsUnit.Pixel, imgAttrib)
    End If

    ' Draw Middle Left
    If sizingMargins.Left > 0 Then
      destRect = New Rectangle(dstRect.X, dstRect.Y + sizingMargins.Top, sizingMargins.Left, dstRect.Height - (sizingMargins.Top + sizingMargins.Bottom))
      GR.DrawImage(Image, destRect, 0, srcRect.Y + sizingMargins.Top, sizingMargins.Left, srcRect.Height - (sizingMargins.Top + sizingMargins.Bottom), GraphicsUnit.Pixel, imgAttrib)
    End If

    ' Draw Middle
    destRect = New Rectangle(dstRect.X + sizingMargins.Left, dstRect.Y + sizingMargins.Top, dstRect.Width - (sizingMargins.Left + sizingMargins.Right), dstRect.Height - (sizingMargins.Top + sizingMargins.Bottom))
    GR.DrawImage(Image, destRect, sizingMargins.Left, srcRect.Y + sizingMargins.Top, srcRect.Width - (sizingMargins.Left + sizingMargins.Right), srcRect.Height - (sizingMargins.Top + sizingMargins.Bottom), GraphicsUnit.Pixel, imgAttrib)

    ' Draw Middle Right
    If sizingMargins.Right > 0 Then
      destRect = New Rectangle(dstRect.X + (dstRect.Width - sizingMargins.Right), dstRect.Y + sizingMargins.Top, sizingMargins.Right, dstRect.Height - (sizingMargins.Top + sizingMargins.Bottom))
      GR.DrawImage(Image, destRect, srcRect.Width - sizingMargins.Right, srcRect.Y + sizingMargins.Top, sizingMargins.Right, srcRect.Height - (sizingMargins.Top + sizingMargins.Bottom), GraphicsUnit.Pixel, imgAttrib)
    End If

    ' Draw Bottom Left
    If sizingMargins.Left > 0 AndAlso sizingMargins.Bottom > 0 Then
      destRect = New Rectangle(dstRect.X, dstRect.Y + (dstRect.Height - sizingMargins.Bottom), sizingMargins.Left, sizingMargins.Bottom)
      GR.DrawImage(Image, destRect, 0, (srcRect.Y + srcRect.Height) - sizingMargins.Bottom, sizingMargins.Left, sizingMargins.Bottom, GraphicsUnit.Pixel, imgAttrib)
    End If

    ' Draw Bottom Middle
    If sizingMargins.Bottom > 0 Then
      destRect = New Rectangle(dstRect.X + sizingMargins.Left, dstRect.Y + (dstRect.Height - sizingMargins.Bottom), dstRect.Width - (sizingMargins.Left + sizingMargins.Right), sizingMargins.Bottom)
      GR.DrawImage(Image, destRect, sizingMargins.Left, (srcRect.Y + srcRect.Height) - sizingMargins.Bottom, srcRect.Width - (sizingMargins.Left + sizingMargins.Right), sizingMargins.Bottom, GraphicsUnit.Pixel, imgAttrib)
    End If

    ' Draw Bottom Right
    If sizingMargins.Right > 0 AndAlso sizingMargins.Bottom > 0 Then
      destRect = New Rectangle(dstRect.X + (dstRect.Width - sizingMargins.Right), dstRect.Y + (dstRect.Height - sizingMargins.Bottom), sizingMargins.Right, sizingMargins.Bottom)
      GR.DrawImage(Image, destRect, srcRect.Width - sizingMargins.Right, (srcRect.Y + srcRect.Height) - sizingMargins.Bottom, sizingMargins.Right, sizingMargins.Bottom, GraphicsUnit.Pixel, imgAttrib)
    End If

  End Sub

End Module
