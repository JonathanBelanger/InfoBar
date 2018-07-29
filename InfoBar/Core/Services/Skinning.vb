Namespace Services

  Public Class Skinning

    Public Enum SkinTextPart
      BackgroundText
      ButtonNormalText
      ButtonHoverText
      ButtonDownText
      TextBoxText
      TextBoxInactiveText
      TooltipText
    End Enum

    Public Enum SkinMarginPart
      Background
      Button
      Graph
      TextBox
      Tooltip
    End Enum

    Public Structure SkinMargins
      Public Left As Integer
      Public Top As Integer
      Public Right As Integer
      Public Bottom As Integer
      Public Sub New(ByVal iLeft As Integer, ByVal iTop As Integer, ByVal iRight As Integer, ByVal iBottom As Integer)
        Left = iLeft
        Top = iTop
        Right = iRight
        Bottom = iBottom
      End Sub
    End Structure

    Public ReadOnly Property ContentMargins(ByVal Part As SkinMarginPart) As Services.Skinning.SkinMargins
      Get
        Select Case Part
          Case SkinMarginPart.Background
            Return New SkinMargins(CurrentSkinInfo.Background.ContentMargins.Left, CurrentSkinInfo.Background.ContentMargins.Top, CurrentSkinInfo.Background.ContentMargins.Right, CurrentSkinInfo.Background.ContentMargins.Bottom)
          Case SkinMarginPart.Button
            Return New SkinMargins(CurrentSkinInfo.Button.ContentMargins.Left, CurrentSkinInfo.Button.ContentMargins.Top, CurrentSkinInfo.Button.ContentMargins.Right, CurrentSkinInfo.Button.ContentMargins.Bottom)
          Case SkinMarginPart.Graph
            Return New SkinMargins(CurrentSkinInfo.Graph.ContentMargins.Left, CurrentSkinInfo.Graph.ContentMargins.Top, CurrentSkinInfo.Graph.ContentMargins.Right, CurrentSkinInfo.Graph.ContentMargins.Bottom)
          Case SkinMarginPart.TextBox
            Return New SkinMargins(CurrentSkinInfo.TextBox.ContentMargins.Left, CurrentSkinInfo.TextBox.ContentMargins.Top, CurrentSkinInfo.TextBox.ContentMargins.Right, CurrentSkinInfo.TextBox.ContentMargins.Bottom)
          Case SkinMarginPart.Tooltip
            Return New SkinMargins(CurrentSkinInfo.Tooltip.ContentMargins.Left, CurrentSkinInfo.Tooltip.ContentMargins.Top, CurrentSkinInfo.Tooltip.ContentMargins.Right, CurrentSkinInfo.Tooltip.ContentMargins.Bottom)
        End Select
      End Get
    End Property

    Public Sub UpdateWindow()
      Skinning_UpdateWindow()
    End Sub

    Public Function MeasureButton(ByRef GR As Graphics, ByVal Rect As Rectangle, ByVal Icon As Image, ByVal Text As String, ByVal State As Integer, ByVal isDropDown As Boolean) As Rectangle
      Return Skinning_MeasureButton(GR, Rect, Icon, Text, State, isDropDown)      
    End Function

    Public Function DrawButton(ByRef GR As Graphics, ByVal Rect As Rectangle, ByVal Icon As Image, ByVal Text As String, ByVal State As Integer, ByVal isDropDown As Boolean) As Rectangle
      Return Skinning_DrawButton(GR, Rect, Icon, Text, State, isDropDown)
    End Function

    Public Sub DrawTextBox(ByRef GR As Graphics, ByVal BoxRect As Rectangle)
      Skinning_DrawTextBox(GR, BoxRect)
    End Sub

    Public Sub DrawGraphBackground(ByRef GR As Graphics, ByVal BoxRect As Rectangle)
      Skinning_DrawGraphBackground(GR, BoxRect)
    End Sub

    Public Function MeasureText(ByRef GR As Graphics, ByVal sText As String, ByVal TextType As SkinTextPart, Optional ByVal maxWidth As Integer = -1, Optional ByVal fontSize As String = "Nothing") As Rectangle
      Dim tt As SkinText
      Select Case TextType
        Case SkinTextPart.BackgroundText
          tt = CurrentSkinInfo.Background.Text
        Case SkinTextPart.ButtonNormalText
          tt = CurrentSkinInfo.Button.NormalText
        Case SkinTextPart.ButtonHoverText
          tt = CurrentSkinInfo.Button.HoverText
        Case SkinTextPart.ButtonDownText
          tt = CurrentSkinInfo.Button.DownText
        Case SkinTextPart.TextBoxText
          tt = CurrentSkinInfo.TextBox.Text
        Case SkinTextPart.TextBoxInactiveText
          tt = CurrentSkinInfo.TextBox.InactiveText
        Case SkinTextPart.TooltipText
          tt = CurrentSkinInfo.Tooltip.Text
        Case Else
          tt = CurrentSkinInfo.Background.Text
      End Select

      Return Skinning_MeasureText(GR, sText, tt, maxWidth, False, fontSize)
    End Function

    Public Sub DrawText(ByRef GR As Graphics, ByVal sText As String, ByRef TextRect As Rectangle, ByVal TextType As SkinTextPart, ByVal HAlign As StringAlignment, ByVal VAlign As StringAlignment, Optional ByVal WordWrap As Boolean = False, Optional ByVal fontSize As String = "Nothing")
      Dim tt As SkinText
      Select Case TextType
        Case SkinTextPart.BackgroundText
          tt = CurrentSkinInfo.Background.Text
        Case SkinTextPart.ButtonNormalText
          tt = CurrentSkinInfo.Button.NormalText
        Case SkinTextPart.ButtonHoverText
          tt = CurrentSkinInfo.Button.HoverText
        Case SkinTextPart.ButtonDownText
          tt = CurrentSkinInfo.Button.DownText
        Case SkinTextPart.TextBoxText
          tt = CurrentSkinInfo.TextBox.Text
        Case SkinTextPart.TextBoxInactiveText
          tt = CurrentSkinInfo.TextBox.InactiveText
        Case SkinTextPart.TooltipText
          tt = CurrentSkinInfo.Tooltip.Text
        Case Else
          tt = CurrentSkinInfo.Background.Text
      End Select
      Skinning_DrawText(GR, sText, TextRect, tt, HAlign, VAlign, WordWrap, -1, False, fontSize)
    End Sub

    Public Sub DrawTooltipSeparator(ByRef GR As Graphics, ByVal Rect As Rectangle)
      Skinning_DrawTooltipSeparator(GR, Rect)
    End Sub

    Public Function DrawSlider(ByRef Gr As Graphics, ByVal Rect As Rectangle, ByVal State As Integer, ByVal Minimum As Integer, ByVal Maximum As Integer, ByVal Value As Integer) As Rectangle
      Return Skinning_DrawSlider(Gr, Rect, State, Minimum, Maximum, Value)
    End Function

    Public Function LoadImageFromFile(ByVal Path As String) As Image
      Return modUtilities.LoadImageFromFile(Path)
    End Function

    Public ReadOnly Property TooltipSeparatorHeight() As Integer
      Get
        Return CurrentSkinInfo.Tooltip.Separator.ImageSize.Height
      End Get
    End Property

    Public ReadOnly Property MaxModuleHeight() As Integer
      Get
        Return CurrentSkinInfo.Height - (CurrentSkinInfo.Background.ContentMargins.Top + CurrentSkinInfo.Background.ContentMargins.Bottom)
      End Get
    End Property

    Public Function ToolbarPointToModulePoint(ByVal mousePos As Point, ByVal mBounds As Rectangle) As Point
      Dim pt As Point
      pt.X = mousePos.X - mBounds.X
      pt.Y = mousePos.Y - mBounds.Y
      Return pt
    End Function

    Public Function ModulePointToToolbarPoint(ByVal mGUID As String, ByVal mPt As Point) As Point
      Dim iMod As InfoBarModule = AvailableModules(mGUID)
      Dim pt As Point
      pt.X = iMod.ModuleBounds.Left + mPt.X
      pt.Y = iMod.ModuleBounds.Top + mPt.Y
      Return pt
    End Function

  End Class

End Namespace