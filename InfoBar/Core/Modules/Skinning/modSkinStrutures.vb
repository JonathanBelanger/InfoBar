Module modSkinStrutures

  Public Enum SkinSizingType
    None
    Stretch
    Tile
  End Enum

  Public Enum SkinTextEffectType
    None
    Shadow
    Glow
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

  Public Structure SkinInfo
    Public Key As String
    Public Name As String
    Public Author As String
    Public Version As String
    Public Website As String
    Public Path As String
  End Structure

  Public Structure SkinText
    Public Color As Color
    Public Font As Font
    Public YOffset As Integer
    Public Effect As SkinTextEffect
  End Structure

  Public Structure SkinTextEffect
    Public Type As SkinTextEffectType
    Public Color As Color
    Public Size As Integer
    Public XOffset As Integer
    Public YOffset As Integer
  End Structure

  Public Class CurrentSkinInfo
    ' Skin Details
    Public Shared Height As Integer
    Public Shared Path As String

    ' Background Values
    Public Class Background
      Public Shared EnableGlass As Boolean
      Public Shared Alpha As Integer
      Public Shared Image As Image
      Public Shared ImageSize As Size
      Public Shared ImageNoGlass As Image
      Public Shared ImageNoGlassSize As Size
      Public Shared SizingType As SkinSizingType
      Public Shared SizingMargins As SkinMargins
      Public Shared ContentMargins As SkinMargins
      Public Shared Text As New SkinText
    End Class

    Public Class Separator
      Public Shared Image As Image
      Public Shared ImageSize As Size
      Public Shared SizingType As SkinSizingType
      Public Shared SizingMargins As SkinMargins
      Public Shared ContentMargins As SkinMargins
    End Class

    Public Class VerticalSeparator
      Public Shared Image As Image
      Public Shared ImageSize As Size
      Public Shared SizingType As SkinSizingType
      Public Shared SizingMargins As SkinMargins
    End Class

    ' Button Values
    Public Class Button
      Public Shared Image As Image
      Public Shared ImageSize As Size
      Public Shared SizingType As SkinSizingType
      Public Shared SizingMargins As SkinMargins
      Public Shared ContentMargins As SkinMargins
      Public Shared NormalText As New SkinText
      Public Shared HoverText As New SkinText
      Public Shared DownText As New SkinText
      Public Shared DropDownArrow As Image
    End Class

    ' Textbox Values
    Public Class TextBox
      Public Shared Image As Image
      Public Shared ImageSize As Size
      Public Shared SizingType As SkinSizingType
      Public Shared SizingMargins As SkinMargins
      Public Shared ContentMargins As SkinMargins
      Public Shared Text As New SkinText
      Public Shared InactiveText As New SkinText
    End Class

    ' Tooltip Values
    Public Class Tooltip
      Public Shared EnableGlass As Boolean
      Public Shared Image As Image
      Public Shared ImageSize As Size
      Public Shared MaskImage As Image
      Public Shared MaskImageSize As Size
      Public Shared ImageNoGlass As Image
      Public Shared ImageNoGlassSize As Size
      Public Shared SizingType As SkinSizingType
      Public Shared SizingMargins As SkinMargins
      Public Shared ContentMargins As SkinMargins
      Public Shared Text As New SkinText
      Public Class Separator
        Public Shared Image As Image
        Public Shared ImageSize As Size
        Public Shared SizingType As SkinSizingType
        Public Shared SizingMargins As SkinMargins
      End Class
    End Class

    ' Graph Values
    Public Class Graph
      Public Shared Image As Image
      Public Shared ImageSize As Size
      Public Shared SizingType As SkinSizingType
      Public Shared SizingMargins As SkinMargins
      Public Shared ContentMargins As SkinMargins
      Public Shared GridLineColor As Color
    End Class

    ' Menu Values
    Public Class Menu
      Public Class Background
        Public Shared Image As Image
        Public Shared ImageSize As Size
        Public Shared SizingType As SkinSizingType
        Public Shared SizingMargins As SkinMargins
        Public Shared ContentMargins As SkinMargins
      End Class
      Public Class Margin
        Public Shared Image As Image
        Public Shared ImageSize As Size
        Public Shared SizingType As SkinSizingType
        Public Shared SizingMargins As SkinMargins
      End Class
      Public Class Item
        Public Shared Image As Image
        Public Shared ImageSize As Size
        Public Shared SizingType As SkinSizingType
        Public Shared SizingMargins As SkinMargins
        Public Shared ContentMargins As SkinMargins
        Public Shared NormalText As New SkinText
        Public Shared HoverText As New SkinText
      End Class
      Public Class Separator
        Public Shared Image As Image
        Public Shared ImageSize As Size
        Public Shared SizingType As SkinSizingType
        Public Shared SizingMargins As SkinMargins
      End Class
      Public Class Arrow
        Public Shared Image As Image
        Public Shared ImageSize As Size
      End Class
      Public Class CheckRadio
        Public Shared Image As Image
        Public Shared ImageSize As Size
      End Class
    End Class

    Public Class Slider
      Public Class Background
        Public Shared Image As Image
        Public Shared ImageSize As Size
        Public Shared SizingType As SkinSizingType
        Public Shared SizingMargins As SkinMargins
        Public Shared ContentMargins As SkinMargins
      End Class
      Public Class Button
        Public Shared Image As Image
        Public Shared ImageSize As Size
        Public Shared SizingType As SkinSizingType
        Public Shared SizingMargins As SkinMargins
      End Class
    End Class
  End Class

End Module
