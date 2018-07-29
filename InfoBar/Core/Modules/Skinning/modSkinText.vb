Module modSkinText

  Private txtRenderingHint As TextRenderingHint = TextRenderingHint.AntiAlias

  Public Function Skinning_MeasureText(ByRef Gr As Graphics, ByVal sText As String, ByRef TextType As SkinText, Optional ByVal maxWidth As Integer = -1, Optional ByVal isBold As Boolean = False, Optional ByVal fontSize As String = "Nothing") As Rectangle
    If sText = vbNullString Then
      Return New Rectangle(0, 0, 0, 0)
      Exit Function
    End If

    Dim fFont As Font = TextType.Font
    If fontSize <> "Nothing" Then fFont = New Font(fFont.FontFamily, fFont.SizeInPoints + CInt(fontSize), fFont.Style)
    If isBold Then fFont = New Font(fFont, FontStyle.Bold)

    Dim SF As New StringFormat(StringFormat.GenericTypographic)
    Gr.TextRenderingHint = txtRenderingHint

    Dim sz As SizeF
    If maxWidth = -1 Then
      sz = Gr.MeasureString(sText, fFont, New PointF(0, 0), SF)
    Else
      sz = Gr.MeasureString(sText, fFont, maxWidth, SF)
    End If
    Dim iWidth As Integer, iHeight As Integer
    Select Case TextType.Effect.Type
      Case SkinTextEffectType.None
        Return New Rectangle(0, 0, Math.Ceiling(sz.Width), Math.Ceiling(sz.Height))
      Case SkinTextEffectType.Glow
        iWidth = Math.Ceiling(sz.Width) + (TextType.Effect.Size * 2)
        iHeight = Math.Ceiling(sz.Height) + (TextType.Effect.Size * 2)
        Return New Rectangle(0, 0, iWidth, iHeight)
      Case SkinTextEffectType.Shadow
        iWidth = Math.Ceiling(sz.Width) + (TextType.Effect.Size * 2) + TextType.Effect.XOffset
        iHeight = Math.Ceiling(sz.Height) + (TextType.Effect.Size * 2) + TextType.Effect.YOffset
        Return New Rectangle(0, 0, iWidth, iHeight)
    End Select

  End Function

  Public Sub Skinning_DrawText(ByRef GR As Graphics, ByVal sText As String, ByRef TextRect As Rectangle, ByRef TextType As SkinText, ByVal HAlign As StringAlignment, ByVal VAlign As StringAlignment, Optional ByVal WordWrap As Boolean = False, Optional ByVal maxWidth As Integer = -1, Optional ByVal isBold As Boolean = False, Optional ByVal fontSize As String = "Nothing")
    If sText = vbNullString Then Exit Sub

    Dim fFont As Font = TextType.Font
    If fontSize <> "Nothing" Then fFont = New Font(fFont.FontFamily, fFont.SizeInPoints + CInt(fontSize), fFont.Style)
    If isBold Then fFont = New Font(fFont, FontStyle.Bold)

    Dim SF As New StringFormat(StringFormat.GenericTypographic)
    SF.Trimming = StringTrimming.EllipsisCharacter
    If WordWrap = False Then SF.FormatFlags = SF.FormatFlags Or StringFormatFlags.NoWrap

    Dim trh As TextRenderingHint = txtRenderingHint
    Select Case TextType.Effect.Type
      Case SkinTextEffectType.None
        SF.Alignment = HAlign
        SF.LineAlignment = VAlign

        Dim brFore As SolidBrush = New SolidBrush(TextType.Color)
        Dim bmp As Bitmap = New Bitmap(TextRect.Width, TextRect.Height)
        Dim gBmp As Graphics = Graphics.FromImage(bmp)
        gBmp.CompositingQuality = GR.CompositingQuality
        gBmp.InterpolationMode = GR.InterpolationMode
        gBmp.PixelOffsetMode = GR.PixelOffsetMode
        gBmp.SmoothingMode = GR.SmoothingMode
        gBmp.TextRenderingHint = trh
        gBmp.TextContrast = 0
        gBmp.DrawString(sText, fFont, brFore, New Rectangle(0, 0, TextRect.Width, TextRect.Height), SF)

        Dim xPos As Integer, yPos As Integer
        Select Case HAlign
          Case StringAlignment.Near
            xPos = TextRect.X
          Case StringAlignment.Center
            xPos = TextRect.X + (TextRect.Width - bmp.Width) / 2
          Case StringAlignment.Far
            xPos = TextRect.X + (TextRect.Width - bmp.Width)
        End Select
        Select Case VAlign
          Case StringAlignment.Near
            yPos = TextRect.Y - TextType.YOffset
          Case StringAlignment.Center
            yPos = (TextRect.Y + ((TextRect.Height - bmp.Height) / 2)) + TextType.YOffset
          Case StringAlignment.Far
            yPos = TextRect.Y + (TextRect.Height - bmp.Height)
        End Select
        GR.DrawImageUnscaled(bmp, xPos, yPos)

        gBmp.Dispose()
        bmp.Dispose()
        brFore.Dispose()

      Case SkinTextEffectType.Glow, SkinTextEffectType.Shadow
        Dim blurAmnt As Integer = TextType.Effect.Size + 1        

        Dim g As Graphics = Graphics.FromHwnd(IntPtr.Zero)
        Dim sz As SizeF = g.MeasureString(sText, fFont, TextRect.Width, SF)
        sz.Width += blurAmnt : sz.Height += blurAmnt
        g.Dispose()

        Dim bmp As Bitmap = New Bitmap(CInt(sz.Width), CInt(sz.Height))

        Dim brBack As SolidBrush = New SolidBrush(TextType.Effect.Color)
        Dim brFore As SolidBrush = New SolidBrush(TextType.Color)

        Dim gBmp As Graphics = Graphics.FromImage(bmp)
        gBmp.CompositingQuality = GR.CompositingQuality
        gBmp.InterpolationMode = GR.InterpolationMode
        gBmp.PixelOffsetMode = GR.PixelOffsetMode
        gBmp.SmoothingMode = GR.SmoothingMode
        gBmp.TextRenderingHint = trh
        gBmp.TextContrast = 0
        gBmp.Clear(Color.Transparent)
        gBmp.DrawString(sText, fFont, brBack, New Rectangle(0, 0, sz.Width, sz.Height), SF)
        gBmp.Dispose()
        brBack.Dispose()

        Dim bmpOut As Bitmap = New Bitmap(bmp.Width + blurAmnt, bmp.Height + blurAmnt)
        Dim gBmpOut As Graphics = Graphics.FromImage(bmpOut)
        gBmpOut.CompositingQuality = GR.CompositingQuality
        gBmpOut.InterpolationMode = GR.InterpolationMode
        gBmpOut.PixelOffsetMode = GR.PixelOffsetMode
        gBmpOut.SmoothingMode = GR.SmoothingMode
        gBmpOut.TextRenderingHint = trh
        gBmpOut.TextContrast = 0
        gBmpOut.Clear(Color.Transparent)

        Dim xMin As Integer, xMax As Integer, yMin As Integer, yMax As Integer
        If TextType.Effect.Type = SkinTextEffectType.Shadow Then
          xMin = TextType.Effect.XOffset : xMax = TextType.Effect.XOffset + blurAmnt + 1
          yMin = TextType.Effect.YOffset : yMax = TextType.Effect.YOffset + blurAmnt + 1
        Else
          xMin = 0 : xMax = blurAmnt + 1
          yMin = 0 : yMax = blurAmnt + 1
        End If
        For x As Integer = xMin To xMax
          For y As Integer = yMin To yMax
            gBmpOut.DrawImageUnscaled(bmp, x, y)
          Next
        Next
        bmp.Dispose()

        gBmpOut.DrawString(sText, fFont, brFore, New Rectangle(blurAmnt / 2, blurAmnt / 2, sz.Width, sz.Height), SF)

        brFore.Dispose()
        gBmpOut.Dispose()

        Dim xPos As Integer, yPos As Integer
        Select Case HAlign
          Case StringAlignment.Near
            xPos = TextRect.X
          Case StringAlignment.Center
            xPos = TextRect.X + (TextRect.Width - bmpOut.Width) / 2
          Case StringAlignment.Far
            xPos = TextRect.X + (TextRect.Width - bmpOut.Width)
        End Select
        Select Case VAlign
          Case StringAlignment.Near
            yPos = TextRect.Y - ((TextType.Effect.Size / 2) + TextType.YOffset)
          Case StringAlignment.Center
            yPos = (TextRect.Y + ((TextRect.Height - bmpOut.Height) / 2)) + TextType.YOffset
          Case StringAlignment.Far
            yPos = TextRect.Y + (TextRect.Height - bmpOut.Height)
        End Select
        GR.DrawImageUnscaled(bmpOut, xPos, yPos)

        bmpOut.Dispose()
    End Select

    SF.Dispose()

  End Sub

End Module
