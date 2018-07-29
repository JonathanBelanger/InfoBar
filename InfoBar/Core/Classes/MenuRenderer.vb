Public Class MenuRenderer
  Inherits ToolStripRenderer

  Private imageMarginBounds As Rectangle

  Protected Overloads Overrides Sub InitializeItem(ByVal item As System.Windows.Forms.ToolStripItem)
    MyBase.InitializeItem(item)
    If Not TypeOf item Is ToolStripSeparator Then
      item.Margin = New Padding(CurrentSkinInfo.Menu.Background.ContentMargins.Left, IIf(CurrentSkinInfo.Menu.Background.ContentMargins.Top > 1, CurrentSkinInfo.Menu.Background.ContentMargins.Top - 2, -1), 0, IIf(CurrentSkinInfo.Menu.Background.ContentMargins.Bottom > 1, CurrentSkinInfo.Menu.Background.ContentMargins.Bottom - 2, -1))
      item.Padding = New Padding(CurrentSkinInfo.Menu.Item.ContentMargins.Left, CurrentSkinInfo.Menu.Item.ContentMargins.Top, CurrentSkinInfo.Menu.Item.ContentMargins.Right, CurrentSkinInfo.Menu.Item.ContentMargins.Bottom)
    End If
  End Sub

  Protected Overloads Overrides Sub OnRenderToolStripBackground(ByVal e As ToolStripRenderEventArgs)
    If TypeOf e.ToolStrip Is ContextMenuStrip Then
      Try
        Dim menuStrip As ContextMenuStrip = e.ToolStrip
        Skinning_DrawMenuBackground(e.Graphics, e.AffectedBounds)
      Catch
        MyBase.OnRenderToolStripBackground(e)
      End Try
    Else
      MyBase.OnRenderToolStripBackground(e)
    End If
  End Sub

  Protected Overrides Sub OnRenderImageMargin(ByVal e As System.Windows.Forms.ToolStripRenderEventArgs)
    imageMarginBounds = e.AffectedBounds
    imageMarginBounds.Width += (CurrentSkinInfo.Menu.Background.ContentMargins.Left * 2)
    Skinning_DrawMenuMargin(e.Graphics, imageMarginBounds)
  End Sub

  Protected Overloads Overrides Sub OnRenderToolStripBorder(ByVal e As ToolStripRenderEventArgs)
    If Not TypeOf e.ToolStrip.Parent Is ContextMenuStrip Then
      MyBase.OnRenderToolStripBorder(e)
    End If
  End Sub

  Protected Overloads Overrides Sub OnRenderMenuItemBackground(ByVal e As System.Windows.Forms.ToolStripItemRenderEventArgs)
    If TypeOf e.ToolStrip Is ContextMenuStrip Then
      If (e.Item.Selected = True Or e.Item.Pressed = True) Then
        Dim menuStrip As ContextMenuStrip = e.ToolStrip
        Try
          Dim rect As New Rectangle(0, 0, e.Item.Width - (((e.Item.Margin.Left + e.Item.Margin.Right) * 2) + 1), e.Item.Height)
          Skinning_DrawMenuItemBackground(e.Graphics, rect, menuStrip.ShowImageMargin)
        Catch
          MyBase.OnRenderMenuItemBackground(e)
        End Try
      End If
    End If
  End Sub

  Protected Overloads Overrides Sub OnRenderSeparator(ByVal e As System.Windows.Forms.ToolStripSeparatorRenderEventArgs)
    If TypeOf e.ToolStrip Is ContextMenuStrip Then
      Dim menuStrip As ContextMenuStrip = e.ToolStrip
      Try
        Skinning_DrawMenuSeparator(e.Graphics, New Rectangle(0, 0, e.Item.Width, e.Item.Height), menuStrip.ShowImageMargin)
      Catch ex As Exception
        MyBase.OnRenderSeparator(e)
      End Try
    Else
      MyBase.OnRenderSeparator(e)
    End If
  End Sub

  Protected Overloads Overrides Sub OnRenderItemImage(ByVal e As System.Windows.Forms.ToolStripItemImageRenderEventArgs)
    If e.Item.Enabled = True Then
      e.Graphics.DrawImage(e.Image, e.ImageRectangle)
    Else
      ControlPaint.DrawImageDisabled(e.Graphics, e.Image, e.ImageRectangle.X, e.ImageRectangle.Y, Color.Transparent)
    End If
  End Sub

  Protected Overloads Overrides Sub OnRenderItemCheck(ByVal e As System.Windows.Forms.ToolStripItemImageRenderEventArgs)
    If TypeOf e.ToolStrip Is ContextMenuStrip Then
      Dim tsmi As ToolStripMenuItem = e.Item
      ' What we're gonna do for now is take the image rect and expand it to the skinned image rect
      Dim dstWidth As Integer = e.ImageRectangle.Width, dstHeight As Integer = e.ImageRectangle.Height
      Dim srcWidth As Integer = CurrentSkinInfo.Menu.CheckRadio.ImageSize.Width
      Dim srcHeight As Integer = CurrentSkinInfo.Menu.CheckRadio.ImageSize.Height / 2
      Dim difWidth As Integer = 0, difHeight As Integer = 0
      If srcWidth > dstWidth Then
        difWidth = (srcWidth - dstWidth) / 2
        difHeight = (srcHeight - dstHeight) / 2
      Else
        difWidth = (dstWidth - srcWidth) / 2
        difHeight = (dstHeight - srcHeight) / 2
      End If
      Dim r As Rectangle = e.ImageRectangle
      r.Inflate(difWidth, difHeight)
      r.Offset(-2, 0)
      If tsmi.CheckState = CheckState.Checked Then
        Skinning_DrawMenuCheck(e.Graphics, r)
      ElseIf tsmi.CheckState = CheckState.Indeterminate Then
        Skinning_DrawMenuRadio(e.Graphics, r)
      Else
        ' Draw nothing
      End If
    Else
      MyBase.OnRenderItemCheck(e)
    End If
  End Sub

  Protected Overloads Overrides Sub OnRenderItemText(ByVal e As System.Windows.Forms.ToolStripItemTextRenderEventArgs)
    If TypeOf e.Item Is ToolStripMenuItem Then
      If e.Item.Text = vbNullString Then
        Try
          e.Graphics.DrawImage(e.Item.Image, New Rectangle(CurrentSkinInfo.Menu.Item.ContentMargins.Left + 3, (e.TextRectangle.Top + ((e.Item.Height - e.Item.Image.Height) / 2)) - 1, e.Item.Image.Width, e.Item.Image.Height))
        Catch
          e.Item.Text = "(Image Not Found)"
          Dim trt As Rectangle = e.TextRectangle
          trt.X = CurrentSkinInfo.Menu.Item.ContentMargins.Left + 3
          Skinning_DrawText(e.Graphics, e.Text, trt, CurrentSkinInfo.Menu.Item.NormalText, StringAlignment.Near, StringAlignment.Center, False)
        End Try
        Exit Sub
      End If

      Dim sText As String
      sText = e.Text
      Dim ampPos As Integer = sText.IndexOf("&")
      If ampPos >= 0 Then
        If sText.Substring(ampPos + 1, 1) <> " " Then sText = sText.Remove(ampPos, 1)
      End If

      Dim tr As Rectangle = e.TextRectangle
      Dim mi As ContextMenuStrip = e.ToolStrip
      If mi.ShowImageMargin Then
        tr.X = CurrentSkinInfo.Menu.Item.ContentMargins.Left + imageMarginBounds.Width
      Else
        tr.X = CurrentSkinInfo.Menu.Item.ContentMargins.Left + 3
      End If
      If e.Item.Selected Or e.Item.Pressed Then
        tr.Width = Skinning_MeasureText(e.Graphics, sText, CurrentSkinInfo.Menu.Item.HoverText, isBold:=(e.Item.Tag = "DEFAULT")).Width
        Skinning_DrawText(e.Graphics, sText, tr, CurrentSkinInfo.Menu.Item.HoverText, StringAlignment.Near, StringAlignment.Center, isBold:=(e.Item.Tag = "DEFAULT"))
      Else
        tr.Width = Skinning_MeasureText(e.Graphics, sText, CurrentSkinInfo.Menu.Item.NormalText, isBold:=(e.Item.Tag = "DEFAULT")).Width
        Skinning_DrawText(e.Graphics, sText, tr, CurrentSkinInfo.Menu.Item.NormalText, StringAlignment.Near, StringAlignment.Center, isBold:=(e.Item.Tag = "DEFAULT"))
      End If
    End If
  End Sub

  Protected Overrides Sub OnRenderArrow(ByVal e As System.Windows.Forms.ToolStripArrowRenderEventArgs)
    If TypeOf e.Item Is ToolStripMenuItem Then
      Try
        ' TODO: Draw Menu Arrow
      Catch ex As Exception
        MyBase.OnRenderArrow(e)
      End Try
    Else
      MyBase.OnRenderArrow(e)
    End If
  End Sub

End Class