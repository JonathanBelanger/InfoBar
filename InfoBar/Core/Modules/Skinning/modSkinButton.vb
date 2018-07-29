Module modSkinButton

  Public Function Skinning_MeasureButton(ByRef GR As Graphics, ByVal Rect As Rectangle, ByVal Icon As Image, ByVal Text As String, ByVal State As Integer, ByVal isDropDown As Boolean) As Rectangle
    GR.InterpolationMode = InterpolationMode.Default

    ' Measure To Get Button Size
    Dim iWidth As Integer, iHeight As Integer
    Dim mtr As Rectangle

    iWidth = CurrentSkinInfo.Button.ContentMargins.Left

    Dim bHasIcon As Boolean = (Icon IsNot Nothing)
    Dim bHasText As Boolean = (Text <> vbNullString)

    If bHasIcon And bHasText And isDropDown Then
      iWidth = iWidth + 4
    ElseIf bHasIcon And bHasText Then
      iWidth = iWidth + 2
    ElseIf bHasIcon And isDropDown Then
      iWidth = iWidth + 2
    ElseIf bHasText And isDropDown Then
      iWidth = iWidth + 2
    End If

    ' Measure Icon
    If Icon IsNot Nothing Then iWidth = iWidth + 16

    ' Measure Text
    If Text <> vbNullString Then
      mtr = Skinning_MeasureText(GR, Text, CurrentSkinInfo.Button.NormalText)
      iWidth = iWidth + mtr.Width
    End If

    ' Measure DropDown Arrow
    If isDropDown Then iWidth = iWidth + CurrentSkinInfo.Button.DropDownArrow.Width

    iWidth = iWidth + CurrentSkinInfo.Button.ContentMargins.Right
    iHeight = CurrentSkinInfo.Height - (CurrentSkinInfo.Background.ContentMargins.Top + CurrentSkinInfo.Background.ContentMargins.Bottom)

    Return New Rectangle(0, 0, iWidth, iHeight)
  End Function

  Public Function Skinning_DrawButton(ByRef GR As Graphics, ByVal Rect As Rectangle, ByVal Icon As Image, ByVal Text As String, ByVal State As Integer, ByVal isDropDown As Boolean) As Rectangle
    GR.InterpolationMode = InterpolationMode.Default

    ' Measure To Get Button Size
    Dim iWidth As Integer, iHeight As Integer
    Dim mtr As Rectangle
    Dim nextX As Integer

    iWidth = CurrentSkinInfo.Button.ContentMargins.Left

    Dim bHasIcon As Boolean = (Icon IsNot Nothing)
    Dim bHasText As Boolean = (Text <> vbNullString)

    If bHasIcon And bHasText And isDropDown Then
      iWidth = iWidth + 4
    ElseIf bHasIcon And bHasText Then
      iWidth = iWidth + 2
    ElseIf bHasIcon And isDropDown Then
      iWidth = iWidth + 2
    ElseIf bHasText And isDropDown Then
      iWidth = iWidth + 2
    End If

    ' Measure Icon
    If Icon IsNot Nothing Then iWidth = iWidth + 16

    ' Measure Text
    If Text <> vbNullString Then
      mtr = Skinning_MeasureText(GR, Text, CurrentSkinInfo.Button.NormalText)
      iWidth = iWidth + mtr.Width
    End If

    ' Measure DropDown Arrow
    If isDropDown Then iWidth = iWidth + CurrentSkinInfo.Button.DropDownArrow.Width

    iWidth = iWidth + CurrentSkinInfo.Button.ContentMargins.Right
    iHeight = CurrentSkinInfo.Height - (CurrentSkinInfo.Background.ContentMargins.Top + CurrentSkinInfo.Background.ContentMargins.Bottom)

    Dim srcHeight As Integer = CurrentSkinInfo.Button.ImageSize.Height / 3
    Dim srcRect As New Rectangle(0, srcHeight * State, CurrentSkinInfo.Button.ImageSize.Width, srcHeight)
    Dim dstRect As New Rectangle(Rect.X, Rect.Y, iWidth, iHeight)

    ' Draw Button Image
    Skinning_DrawNineSlice(GR, CurrentSkinInfo.Button.Image, CurrentSkinInfo.Button.SizingType, _
                           CurrentSkinInfo.Button.SizingMargins, srcRect, dstRect)

    nextX = Rect.X + CurrentSkinInfo.Button.ContentMargins.Left

    GR.InterpolationMode = InterpolationMode.HighQualityBicubic

    ' Draw Icon
    If Icon IsNot Nothing Then
      GR.DrawImage(Icon, CSng(nextX), CSng(Rect.Y + ((iHeight - 16) / 2)), 16, 16)
      nextX = nextX + 16
      If bHasText Or isDropDown Then nextX = nextX + 2
    End If

    ' Draw Text
    If Text <> vbNullString Then
      mtr.X = nextX
      mtr.Y = Rect.Y + CurrentSkinInfo.Button.ContentMargins.Top
      mtr.Height = Rect.Height - (CurrentSkinInfo.Button.ContentMargins.Top + CurrentSkinInfo.Button.ContentMargins.Bottom)
      Select Case State
        Case 0 ' Normal
          Skinning_DrawText(GR, Text, mtr, CurrentSkinInfo.Button.NormalText, StringAlignment.Near, StringAlignment.Center)
        Case 1 ' Mouseover
          Skinning_DrawText(GR, Text, mtr, CurrentSkinInfo.Button.HoverText, StringAlignment.Near, StringAlignment.Center)
        Case 2 ' Mousedown
          Skinning_DrawText(GR, Text, mtr, CurrentSkinInfo.Button.DownText, StringAlignment.Near, StringAlignment.Center)
      End Select
      nextX = nextX + mtr.Width
      If isDropDown Then nextX = nextX + 2
    End If

    ' Draw Dropdown arrow
    If isDropDown = True Then
      Dim destRect As New Rectangle(nextX, Rect.Y + ((iHeight - (CurrentSkinInfo.Button.DropDownArrow.Height / 3)) / 2), CurrentSkinInfo.Button.DropDownArrow.Width, CurrentSkinInfo.Button.DropDownArrow.Height / 3)
      GR.DrawImage(CurrentSkinInfo.Button.DropDownArrow, destRect, 0, CInt((CurrentSkinInfo.Button.DropDownArrow.Height / 3) * State), CurrentSkinInfo.Button.DropDownArrow.Width, CInt(CurrentSkinInfo.Button.DropDownArrow.Height / 3), GraphicsUnit.Pixel)
    End If

    Return New Rectangle(Rect.X, Rect.Y, iWidth, iHeight)
  End Function

End Module
