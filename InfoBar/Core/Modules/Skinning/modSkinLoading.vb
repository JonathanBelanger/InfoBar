Module modSkinLoading

  Public Sub Skinning_LoadSkin()
    If SkinCollection.Contains(IBSettings.CurrentSkin) = False Then
      MsgBox("The selected skin is no longer available. InfoBar will load the first available skin.")
      IBSettings.CurrentSkin = SkinCollection(1).Key
      Skinning_LoadSkin()
      Exit Sub
    End If

    CurrentSkinInfo.Path = IBSettings.CurrentSkin.Replace("skin.xml", "")

    'Debug.Print("Skinning: Loading Skin")
    Dim Doc As New XmlDocument
    Try
      Doc.Load(IBSettings.CurrentSkin)
    Catch ex As Exception
      Skinning_RaiseSkinError()
      Exit Sub
    End Try

    Dim bTemp As Boolean, iTemp As Integer, imgTemp As Image = Nothing, stTemp As SkinSizingType
    Dim marTemp As SkinMargins, fntTemp As Font = Nothing, colTemp As Color, teTemp As SkinTextEffectType
    Dim sTemp As String = vbNullString

    'Debug.Print("Skinning: Loading Bar Height")
    If Skinning_GetIntegerSkinValue(Doc, "/skin/background", "height", iTemp) = True Then
      CurrentSkinInfo.Height = iTemp
    Else
      CurrentSkinInfo.Height = 30
    End If

    'Debug.Print("Skinning: Loading Bar Alpha")
    If Skinning_GetIntegerSkinValue(Doc, "/skin/background", "alpha", iTemp) = True Then
      CurrentSkinInfo.Background.Alpha = iTemp
    Else
      CurrentSkinInfo.Background.Alpha = 255
    End If

    'Debug.Print("Skinning: Loading Bar Enable Glass (Vista only)")
    If Skinning_GetBooleanSkinValue(Doc, "/skin/background", "enableglass", bTemp) = True Then
      CurrentSkinInfo.Background.EnableGlass = bTemp
      ToggleBlur(bTemp)
    Else
      CurrentSkinInfo.Background.EnableGlass = True
      ToggleBlur(bTemp)
    End If

    'Debug.Print("Skinning: Loading Background Image File")
    If Skinning_GetImageSkinValue(Doc, "/skin/background/image", "file", imgTemp) = True Then
      CurrentSkinInfo.Background.Image = imgTemp
      CurrentSkinInfo.Background.ImageSize = CurrentSkinInfo.Background.Image.Size
    Else
      Exit Sub
    End If

    'Debug.Print("Skinning: Loading Background Image File (No Glass)")
    If Skinning_GetImageSkinValue(Doc, "/skin/background/image", "filenoglass", imgTemp, True) = True Then
      CurrentSkinInfo.Background.ImageNoGlass = imgTemp      
    Else
      CurrentSkinInfo.Background.ImageNoGlass = CurrentSkinInfo.Background.Image
    End If
    CurrentSkinInfo.Background.ImageNoGlassSize = CurrentSkinInfo.Background.ImageNoGlass.Size

    'Debug.Print("Skinning: Loading Background Image Sizing Type")
    If Skinning_GetSizingTypeSkinValue(Doc, "/skin/background/image", stTemp) = True Then
      CurrentSkinInfo.Background.SizingType = stTemp
    Else
      CurrentSkinInfo.Background.SizingType = SkinSizingType.Stretch
    End If

    'Debug.Print("Skinning: Loading Background Image Sizing Margins")
    If CurrentSkinInfo.Background.SizingType <> SkinSizingType.None Then
      If Skinning_GetMarginsSkinValue(Doc, "/skin/background/image", "sizingmargins", marTemp) = True Then
        CurrentSkinInfo.Background.SizingMargins = marTemp
      Else
        CurrentSkinInfo.Background.SizingMargins = New SkinMargins(0, 0, 0, 0)
      End If
    End If

    'Debug.Print("Skinning: Loading Background Image Content Margins")
    If Skinning_GetMarginsSkinValue(Doc, "/skin/background/image", "contentmargins", marTemp) = True Then
      CurrentSkinInfo.Background.ContentMargins = marTemp
    Else
      CurrentSkinInfo.Background.ContentMargins = New SkinMargins(0, 0, 0, 0)
    End If

    'Debug.Print("Skinning: Loading Background Text Font")
    If Skinning_GetFontSkinValue(Doc, "/skin/background/text", fntTemp) = True Then
      CurrentSkinInfo.Background.Text.Font = fntTemp
    Else
      CurrentSkinInfo.Background.Text.Font = SystemFonts.MessageBoxFont
    End If

    'Debug.Print("Skinning: Loading Background Text Color")
    If Skinning_GetColorSkinValue(Doc, "/skin/background/text", "color", colTemp) = True Then
      CurrentSkinInfo.Background.Text.Color = colTemp
    Else
      CurrentSkinInfo.Background.Text.Color = Color.Black
    End If

    'Debug.Print("Skinning: Loading Background Text Y Offset")
    If Skinning_GetIntegerSkinValue(Doc, "/skin/background/text", "yoffset", iTemp) = True Then
      CurrentSkinInfo.Background.Text.YOffset = iTemp
    Else
      CurrentSkinInfo.Background.Text.YOffset = 0
    End If

    'Debug.Print("Skinning: Loading Background Text Effect Type")
    If Skinning_GetTextEffectTypeSkinValue(Doc, "/skin/background/text", teTemp) = True Then
      CurrentSkinInfo.Background.Text.Effect.Type = teTemp
    Else
      CurrentSkinInfo.Background.Text.Effect.Type = SkinTextEffectType.None
    End If

    'Debug.Print("Skinning: Loading Background Text Effect Color")
    If CurrentSkinInfo.Background.Text.Effect.Type <> SkinTextEffectType.None Then
      If Skinning_GetColorSkinValue(Doc, "/skin/background/text", "effectcolor", colTemp) = True Then
        CurrentSkinInfo.Background.Text.Effect.Color = colTemp
      Else
        CurrentSkinInfo.Background.Text.Effect.Color = Color.Black
      End If
    End If

    'Debug.Print("Skinning: Loading Background Text Effect Size")
    If CurrentSkinInfo.Background.Text.Effect.Type <> SkinTextEffectType.None Then
      If Skinning_GetIntegerSkinValue(Doc, "/skin/background/text", "effectsize", iTemp) = True Then
        CurrentSkinInfo.Background.Text.Effect.Size = iTemp
      Else
        CurrentSkinInfo.Background.Text.Effect.Size = 0
      End If
    End If

    'Debug.Print("Skinning: Loading Background Text Effect X Offset")
    If CurrentSkinInfo.Background.Text.Effect.Type = SkinTextEffectType.Shadow Then
      If Skinning_GetIntegerSkinValue(Doc, "/skin/background/text", "effectxoffset", iTemp) = True Then
        CurrentSkinInfo.Background.Text.Effect.XOffset = iTemp
      Else
        CurrentSkinInfo.Background.Text.Effect.XOffset = 0
      End If
    End If

    'Debug.Print("Skinning: Loading Background Text Effect Y Offset")
    If CurrentSkinInfo.Background.Text.Effect.Type = SkinTextEffectType.Shadow Then
      If Skinning_GetIntegerSkinValue(Doc, "/skin/background/text", "effectyoffset", iTemp) = True Then
        CurrentSkinInfo.Background.Text.Effect.YOffset = iTemp
      Else
        CurrentSkinInfo.Background.Text.Effect.YOffset = 0
      End If
    End If

    'Debug.Print("Skinning: Loading Separator Image")
    If Skinning_GetImageSkinValue(Doc, "/skin/separator/image", "file", imgTemp) = True Then
      CurrentSkinInfo.Separator.Image = imgTemp
      CurrentSkinInfo.Separator.ImageSize = CurrentSkinInfo.Separator.Image.Size
    Else
      Exit Sub
    End If

    'Debug.Print("Skinning: Loading Separator Image Sizing Type")
    If Skinning_GetSizingTypeSkinValue(Doc, "/skin/separator/image", stTemp) = True Then
      CurrentSkinInfo.Separator.SizingType = stTemp
    Else
      CurrentSkinInfo.Separator.SizingType = SkinSizingType.Stretch
    End If

    'Debug.Print("Skinning: Loading Separator Image Sizing Margins")
    If CurrentSkinInfo.Separator.SizingType <> SkinSizingType.None Then
      If Skinning_GetMarginsSkinValue(Doc, "/skin/separator/image", "sizingmargins", marTemp) = True Then
        CurrentSkinInfo.Separator.SizingMargins = marTemp
      Else
        CurrentSkinInfo.Separator.SizingMargins = New SkinMargins(0, 0, 0, 0)
      End If
    End If

    'Debug.Print("Skinning: Loading Separator Image Content Margins")
    If Skinning_GetMarginsSkinValue(Doc, "/skin/separator/image", "contentmargins", marTemp, True) = True Then
      CurrentSkinInfo.Separator.ContentMargins = marTemp
    Else
      CurrentSkinInfo.Separator.ContentMargins = New SkinMargins(0, 0, 0, 0)
    End If

    'Debug.Print("Skinning: Loading Vertical Separator Image")
    If Skinning_GetImageSkinValue(Doc, "/skin/verticalseparator/image", "file", imgTemp) = True Then
      CurrentSkinInfo.VerticalSeparator.Image = imgTemp
      CurrentSkinInfo.VerticalSeparator.ImageSize = CurrentSkinInfo.VerticalSeparator.Image.Size
    Else
      Exit Sub
    End If

    'Debug.Print("Skinning: Loading Vertical Separator Image Sizing Type")
    If Skinning_GetSizingTypeSkinValue(Doc, "/skin/verticalseparator/image", stTemp) = True Then
      CurrentSkinInfo.VerticalSeparator.SizingType = stTemp
    Else
      CurrentSkinInfo.VerticalSeparator.SizingType = SkinSizingType.Stretch
    End If

    'Debug.Print("Skinning: Loading Vertical Separator Image Sizing Margins")
    If CurrentSkinInfo.VerticalSeparator.SizingType <> SkinSizingType.None Then
      If Skinning_GetMarginsSkinValue(Doc, "/skin/verticalseparator/image", "sizingmargins", marTemp) = True Then
        CurrentSkinInfo.VerticalSeparator.SizingMargins = marTemp
      Else
        CurrentSkinInfo.VerticalSeparator.SizingMargins = New SkinMargins(0, 0, 0, 0)
      End If
    End If

    'Debug.Print("Skinning: Loading Button Image")
    If Skinning_GetImageSkinValue(Doc, "/skin/button/image", "file", imgTemp) = True Then
      CurrentSkinInfo.Button.Image = imgTemp
      CurrentSkinInfo.Button.ImageSize = CurrentSkinInfo.Button.Image.Size
    Else
      Exit Sub
    End If

    'Debug.Print("Skinning: Loading Button Image Sizing Type")
    If Skinning_GetSizingTypeSkinValue(Doc, "/skin/button/image", stTemp) = True Then
      CurrentSkinInfo.Button.SizingType = stTemp
    Else
      CurrentSkinInfo.Button.SizingType = SkinSizingType.Stretch
    End If

    'Debug.Print("Skinning: Loading Button Image Sizing Margins")
    If CurrentSkinInfo.Button.SizingType <> SkinSizingType.None Then
      If Skinning_GetMarginsSkinValue(Doc, "/skin/button/image", "sizingmargins", marTemp) = True Then
        CurrentSkinInfo.Button.SizingMargins = marTemp
      Else
        CurrentSkinInfo.Button.SizingMargins = New SkinMargins(0, 0, 0, 0)
      End If
    End If

    'Debug.Print("Skinning: Loading Button Image Content Margins")
    If Skinning_GetMarginsSkinValue(Doc, "/skin/button/image", "contentmargins", marTemp) = True Then
      CurrentSkinInfo.Button.ContentMargins = marTemp
    Else
      CurrentSkinInfo.Button.ContentMargins = New SkinMargins(4, 4, 4, 4)
    End If

    'Debug.Print("Skinning: Loading Button Normal Text Font")
    If Skinning_GetFontSkinValue(Doc, "/skin/button/normaltext", fntTemp) = True Then
      CurrentSkinInfo.Button.NormalText.Font = fntTemp
    Else
      CurrentSkinInfo.Button.NormalText.Font = SystemFonts.MessageBoxFont
    End If

    'Debug.Print("Skinning: Loading Button Normal Text Color")
    If Skinning_GetColorSkinValue(Doc, "/skin/button/normaltext", "color", colTemp) = True Then
      CurrentSkinInfo.Button.NormalText.Color = colTemp
    Else
      CurrentSkinInfo.Button.NormalText.Color = Color.Black
    End If

    'Debug.Print("Skinning: Loading Button Normal Text Y Offset")
    If Skinning_GetIntegerSkinValue(Doc, "/skin/button/normaltext", "yoffset", iTemp) = True Then
      CurrentSkinInfo.Button.NormalText.YOffset = iTemp
    Else
      CurrentSkinInfo.Button.NormalText.YOffset = 0
    End If

    'Debug.Print("Skinning: Loading Button Normal Text Effect Type")
    If Skinning_GetTextEffectTypeSkinValue(Doc, "/skin/button/normaltext", teTemp) = True Then
      CurrentSkinInfo.Button.NormalText.Effect.Type = teTemp
    Else
      CurrentSkinInfo.Button.NormalText.Effect.Type = SkinTextEffectType.None
    End If

    'Debug.Print("Skinning: Loading Button Normal Text Effect Color")
    If CurrentSkinInfo.Button.NormalText.Effect.Type <> SkinTextEffectType.None Then
      If Skinning_GetColorSkinValue(Doc, "/skin/button/normaltext", "effectcolor", colTemp) = True Then
        CurrentSkinInfo.Button.NormalText.Effect.Color = colTemp
      Else
        CurrentSkinInfo.Button.NormalText.Effect.Color = Color.Black
      End If
    End If

    'Debug.Print("Skinning: Loading Button Normal Text Effect Size")
    If CurrentSkinInfo.Button.NormalText.Effect.Type <> SkinTextEffectType.None Then
      If Skinning_GetIntegerSkinValue(Doc, "/skin/button/normaltext", "effectsize", iTemp) = True Then
        CurrentSkinInfo.Button.NormalText.Effect.Size = iTemp
      Else
        CurrentSkinInfo.Button.NormalText.Effect.Size = 0
      End If
    End If

    'Debug.Print("Skinning: Loading Button Normal Text Effect X Offset")
    If CurrentSkinInfo.Button.NormalText.Effect.Type = SkinTextEffectType.Shadow Then
      If Skinning_GetIntegerSkinValue(Doc, "/skin/button/normaltext", "effectxoffset", iTemp) = True Then
        CurrentSkinInfo.Button.NormalText.Effect.XOffset = iTemp
      Else
        CurrentSkinInfo.Button.NormalText.Effect.XOffset = 0
      End If
    End If

    'Debug.Print("Skinning: Loading Button Normal Text Effect Y Offset")
    If CurrentSkinInfo.Button.NormalText.Effect.Type = SkinTextEffectType.Shadow Then
      If Skinning_GetIntegerSkinValue(Doc, "/skin/button/normaltext", "effectyoffset", iTemp) = True Then
        CurrentSkinInfo.Button.NormalText.Effect.YOffset = iTemp
      Else
        CurrentSkinInfo.Button.NormalText.Effect.YOffset = 0
      End If
    End If

    'Debug.Print("Skinning: Loading Button Hover Text Font")
    If Skinning_GetFontSkinValue(Doc, "/skin/button/hovertext", fntTemp) = True Then
      CurrentSkinInfo.Button.HoverText.Font = fntTemp
    Else
      CurrentSkinInfo.Button.HoverText.Font = SystemFonts.MessageBoxFont
    End If

    'Debug.Print("Skinning: Loading Button Hover Text Color")
    If Skinning_GetColorSkinValue(Doc, "/skin/button/hovertext", "color", colTemp) = True Then
      CurrentSkinInfo.Button.HoverText.Color = colTemp
    Else
      CurrentSkinInfo.Button.HoverText.Color = Color.Black
    End If

    'Debug.Print("Skinning: Loading Button Hover Text Y Offset")
    If Skinning_GetIntegerSkinValue(Doc, "/skin/button/hovertext", "yoffset", iTemp) = True Then
      CurrentSkinInfo.Button.HoverText.YOffset = iTemp
    Else
      CurrentSkinInfo.Button.HoverText.YOffset = 0
    End If

    'Debug.Print("Skinning: Loading Button Hover Text Effect Type")
    If Skinning_GetTextEffectTypeSkinValue(Doc, "/skin/button/hovertext", teTemp) = True Then
      CurrentSkinInfo.Button.HoverText.Effect.Type = teTemp
    Else
      CurrentSkinInfo.Button.HoverText.Effect.Type = SkinTextEffectType.None
    End If

    'Debug.Print("Skinning: Loading Button Hover Text Effect Color")
    If CurrentSkinInfo.Button.HoverText.Effect.Type <> SkinTextEffectType.None Then
      If Skinning_GetColorSkinValue(Doc, "/skin/button/hovertext", "effectcolor", colTemp) = True Then
        CurrentSkinInfo.Button.HoverText.Effect.Color = colTemp
      Else
        CurrentSkinInfo.Button.HoverText.Effect.Color = Color.Black
      End If
    End If

    'Debug.Print("Skinning: Loading Button Hover Text Effect Size")
    If CurrentSkinInfo.Button.HoverText.Effect.Type <> SkinTextEffectType.None Then
      If Skinning_GetIntegerSkinValue(Doc, "/skin/button/hovertext", "effectsize", iTemp) = True Then
        CurrentSkinInfo.Button.HoverText.Effect.Size = iTemp
      Else
        CurrentSkinInfo.Button.HoverText.Effect.Size = 0
      End If
    End If

    'Debug.Print("Skinning: Loading Button Hover Text Effect X Offset")
    If CurrentSkinInfo.Button.HoverText.Effect.Type = SkinTextEffectType.Shadow Then
      If Skinning_GetIntegerSkinValue(Doc, "/skin/button/hovertext", "effectxoffset", iTemp) = True Then
        CurrentSkinInfo.Button.HoverText.Effect.XOffset = iTemp
      Else
        CurrentSkinInfo.Button.HoverText.Effect.XOffset = 0
      End If
    End If

    'Debug.Print("Skinning: Loading Button Hover Text Effect Y Offset")
    If CurrentSkinInfo.Button.HoverText.Effect.Type = SkinTextEffectType.Shadow Then
      If Skinning_GetIntegerSkinValue(Doc, "/skin/button/hovertext", "effectyoffset", iTemp) = True Then
        CurrentSkinInfo.Button.HoverText.Effect.YOffset = iTemp
      Else
        CurrentSkinInfo.Button.HoverText.Effect.YOffset = 0
      End If
    End If

    'Debug.Print("Skinning: Loading Button Down Text Font")
    If Skinning_GetFontSkinValue(Doc, "/skin/button/downtext", fntTemp) = True Then
      CurrentSkinInfo.Button.DownText.Font = fntTemp
    Else
      CurrentSkinInfo.Button.DownText.Font = SystemFonts.MessageBoxFont
    End If

    'Debug.Print("Skinning: Loading Button Down Text Color")
    If Skinning_GetColorSkinValue(Doc, "/skin/button/downtext", "color", colTemp) = True Then
      CurrentSkinInfo.Button.DownText.Color = colTemp
    Else
      CurrentSkinInfo.Button.DownText.Color = Color.Black
    End If

    'Debug.Print("Skinning: Loading Button Down Text Y Offset")
    If Skinning_GetIntegerSkinValue(Doc, "/skin/button/downtext", "yoffset", iTemp) = True Then
      CurrentSkinInfo.Button.DownText.YOffset = iTemp
    Else
      CurrentSkinInfo.Button.DownText.YOffset = 0
    End If

    'Debug.Print("Skinning: Loading Button Down Text Effect Type")
    If Skinning_GetTextEffectTypeSkinValue(Doc, "/skin/button/downtext", teTemp) = True Then
      CurrentSkinInfo.Button.DownText.Effect.Type = teTemp
    Else
      CurrentSkinInfo.Button.DownText.Effect.Type = SkinTextEffectType.None
    End If

    'Debug.Print("Skinning: Loading Button Down Text Effect Color")
    If CurrentSkinInfo.Button.DownText.Effect.Type <> SkinTextEffectType.None Then
      If Skinning_GetColorSkinValue(Doc, "/skin/button/downtext", "effectcolor", colTemp) = True Then
        CurrentSkinInfo.Button.DownText.Effect.Color = colTemp
      Else
        CurrentSkinInfo.Button.DownText.Effect.Color = Color.Black
      End If
    End If

    'Debug.Print("Skinning: Loading Button Down Text Effect Size")
    If CurrentSkinInfo.Button.DownText.Effect.Type <> SkinTextEffectType.None Then
      If Skinning_GetIntegerSkinValue(Doc, "/skin/button/downtext", "effectsize", iTemp) = True Then
        CurrentSkinInfo.Button.DownText.Effect.Size = iTemp
      Else
        CurrentSkinInfo.Button.DownText.Effect.Size = 0
      End If
    End If

    'Debug.Print("Skinning: Loading Button Down Text Effect X Offset")
    If CurrentSkinInfo.Button.DownText.Effect.Type = SkinTextEffectType.Shadow Then
      If Skinning_GetIntegerSkinValue(Doc, "/skin/button/downtext", "effectxoffset", iTemp) = True Then
        CurrentSkinInfo.Button.DownText.Effect.XOffset = iTemp
      Else
        CurrentSkinInfo.Button.DownText.Effect.XOffset = 0
      End If
    End If

    'Debug.Print("Skinning: Loading Button Down Text Effect Y Offset")
    If CurrentSkinInfo.Button.DownText.Effect.Type = SkinTextEffectType.Shadow Then
      If Skinning_GetIntegerSkinValue(Doc, "/skin/button/downtext", "effectyoffset", iTemp) = True Then
        CurrentSkinInfo.Button.DownText.Effect.YOffset = iTemp
      Else
        CurrentSkinInfo.Button.DownText.Effect.YOffset = 0
      End If
    End If

    'Debug.Print("Skinning: Loading Button Drop Down Arrow Image")
    If Skinning_GetImageSkinValue(Doc, "/skin/button/dropdownarrow", "file", imgTemp) = True Then
      CurrentSkinInfo.Button.DropDownArrow = imgTemp
    Else
      Exit Sub
    End If

    'Debug.Print("Skinning: Loading Text Box Image")
    If Skinning_GetImageSkinValue(Doc, "/skin/textbox/image", "file", imgTemp) = True Then
      CurrentSkinInfo.TextBox.Image = imgTemp
      CurrentSkinInfo.TextBox.ImageSize = CurrentSkinInfo.TextBox.Image.Size
    Else
      Exit Sub
    End If

    'Debug.Print("Skinning: Loading Text Box Image Sizing Type")
    If Skinning_GetSizingTypeSkinValue(Doc, "/skin/textbox/image", stTemp) = True Then
      CurrentSkinInfo.TextBox.SizingType = stTemp
    Else
      CurrentSkinInfo.TextBox.SizingType = SkinSizingType.Stretch
    End If

    'Debug.Print("Skinning: Loading Text Box Image Sizing Margins")
    If CurrentSkinInfo.TextBox.SizingType <> SkinSizingType.None Then
      If Skinning_GetMarginsSkinValue(Doc, "/skin/textbox/image", "sizingmargins", marTemp) = True Then
        CurrentSkinInfo.TextBox.SizingMargins = marTemp
      Else
        CurrentSkinInfo.TextBox.SizingMargins = New SkinMargins(0, 0, 0, 0)
      End If
    End If

    'Debug.Print("Skinning: Loading Text Box Image Content Margins")
    If Skinning_GetMarginsSkinValue(Doc, "/skin/textbox/image", "contentmargins", marTemp) = True Then
      CurrentSkinInfo.TextBox.ContentMargins = marTemp
    Else
      CurrentSkinInfo.TextBox.ContentMargins = New SkinMargins(0, 0, 0, 0)
    End If

    'Debug.Print("Skinning: Loading Text Box Font")
    If Skinning_GetFontSkinValue(Doc, "/skin/textbox/text", fntTemp) = True Then
      CurrentSkinInfo.TextBox.Text.Font = fntTemp
    Else
      CurrentSkinInfo.TextBox.Text.Font = SystemFonts.MessageBoxFont
    End If

    'Debug.Print("Skinning: Loading Text Box Font Color")
    If Skinning_GetColorSkinValue(Doc, "/skin/textbox/text", "color", colTemp) = True Then
      CurrentSkinInfo.TextBox.Text.Color = colTemp
    Else
      CurrentSkinInfo.TextBox.Text.Color = Color.Black
    End If

    'Debug.Print("Skinning: Loading Text Box Text Y Offset")
    If Skinning_GetIntegerSkinValue(Doc, "/skin/textbox/text", "yoffset", iTemp) = True Then
      CurrentSkinInfo.TextBox.Text.YOffset = iTemp
    Else
      CurrentSkinInfo.TextBox.Text.YOffset = 0
    End If

    'Debug.Print("Skinning: Loading Text Box Text Effect Type")
    If Skinning_GetTextEffectTypeSkinValue(Doc, "/skin/textbox/text", teTemp) = True Then
      CurrentSkinInfo.TextBox.Text.Effect.Type = teTemp
    Else
      CurrentSkinInfo.TextBox.Text.Effect.Type = SkinTextEffectType.None
    End If

    'Debug.Print("Skinning: Loading Text Box Text Effect Color")
    If CurrentSkinInfo.TextBox.Text.Effect.Type <> SkinTextEffectType.None Then
      If Skinning_GetColorSkinValue(Doc, "/skin/textbox/text", "effectcolor", colTemp) = True Then
        CurrentSkinInfo.TextBox.Text.Effect.Color = colTemp
      Else
        CurrentSkinInfo.TextBox.Text.Effect.Color = Color.Black
      End If
    End If

    'Debug.Print("Skinning: Loading Text Box Text Effect Size")
    If CurrentSkinInfo.TextBox.Text.Effect.Type <> SkinTextEffectType.None Then
      If Skinning_GetIntegerSkinValue(Doc, "/skin/textbox/text", "effectsize", iTemp) = True Then
        CurrentSkinInfo.TextBox.Text.Effect.Size = iTemp
      Else
        CurrentSkinInfo.TextBox.Text.Effect.Size = 0
      End If
    End If

    'Debug.Print("Skinning: Loading Text Box Text Effect X Offset")
    If CurrentSkinInfo.TextBox.Text.Effect.Type = SkinTextEffectType.Shadow Then
      If Skinning_GetIntegerSkinValue(Doc, "/skin/textbox/text", "effectxoffset", iTemp) = True Then
        CurrentSkinInfo.TextBox.Text.Effect.XOffset = iTemp
      Else
        CurrentSkinInfo.TextBox.Text.Effect.XOffset = 0
      End If
    End If

    'Debug.Print("Skinning: Loading Text Box Text Effect Y Offset")
    If CurrentSkinInfo.TextBox.Text.Effect.Type = SkinTextEffectType.Shadow Then
      If Skinning_GetIntegerSkinValue(Doc, "/skin/textbox/text", "effectyoffset", iTemp) = True Then
        CurrentSkinInfo.TextBox.Text.Effect.YOffset = iTemp
      Else
        CurrentSkinInfo.TextBox.Text.Effect.YOffset = 0
      End If
    End If

    'Debug.Print("Skinning: Loading Text Box Inactive Text Font")
    If Skinning_GetFontSkinValue(Doc, "/skin/textbox/inactivetext", fntTemp) = True Then
      CurrentSkinInfo.TextBox.InactiveText.Font = fntTemp
    Else
      CurrentSkinInfo.TextBox.InactiveText.Font = SystemFonts.MessageBoxFont
    End If

    'Debug.Print("Skinning: Loading Text Box Inactive Text Color")
    If Skinning_GetColorSkinValue(Doc, "/skin/textbox/inactivetext", "color", colTemp) = True Then
      CurrentSkinInfo.TextBox.InactiveText.Color = colTemp
    Else
      CurrentSkinInfo.TextBox.InactiveText.Color = Color.Gray
    End If

    'Debug.Print("Skinning: Loading Text Box Inactive Text Y Offset")
    If Skinning_GetIntegerSkinValue(Doc, "/skin/textbox/inactivetext", "yoffset", iTemp) = True Then
      CurrentSkinInfo.TextBox.InactiveText.YOffset = iTemp
    Else
      CurrentSkinInfo.TextBox.InactiveText.YOffset = 0
    End If

    'Debug.Print("Skinning: Loading Text Box Inactive Text Effect Type")
    If Skinning_GetTextEffectTypeSkinValue(Doc, "/skin/textbox/inactivetext", teTemp) = True Then
      CurrentSkinInfo.TextBox.InactiveText.Effect.Type = teTemp
    Else
      CurrentSkinInfo.TextBox.InactiveText.Effect.Type = SkinTextEffectType.None
    End If

    'Debug.Print("Skinning: Loading Text Box Inactive Text Effect Color")
    If CurrentSkinInfo.TextBox.InactiveText.Effect.Type <> SkinTextEffectType.None Then
      If Skinning_GetColorSkinValue(Doc, "/skin/textbox/inactivetext", "effectcolor", colTemp) = True Then
        CurrentSkinInfo.TextBox.InactiveText.Effect.Color = colTemp
      Else
        CurrentSkinInfo.TextBox.InactiveText.Effect.Color = Color.Gray
      End If
    End If

    'Debug.Print("Skinning: Loading Text Box Inactive Text Effect Size")
    If CurrentSkinInfo.TextBox.InactiveText.Effect.Type <> SkinTextEffectType.None Then
      If Skinning_GetIntegerSkinValue(Doc, "/skin/textbox/inactivetext", "effectsize", iTemp) = True Then
        CurrentSkinInfo.TextBox.InactiveText.Effect.Size = iTemp
      Else
        CurrentSkinInfo.TextBox.InactiveText.Effect.Size = 0
      End If
    End If

    'Debug.Print("Skinning: Loading Text Box Inactive Text Effect X Offset")
    If CurrentSkinInfo.TextBox.InactiveText.Effect.Type = SkinTextEffectType.Shadow Then
      If Skinning_GetIntegerSkinValue(Doc, "/skin/textbox/inactivetext", "effectxoffset", iTemp) = True Then
        CurrentSkinInfo.TextBox.InactiveText.Effect.XOffset = iTemp
      Else
        CurrentSkinInfo.TextBox.InactiveText.Effect.XOffset = 0
      End If
    End If

    'Debug.Print("Skinning: Loading Text Box Inactive Text Effect Y Offset")
    If CurrentSkinInfo.TextBox.InactiveText.Effect.Type = SkinTextEffectType.Shadow Then
      If Skinning_GetIntegerSkinValue(Doc, "/skin/textbox/inactivetext", "effectyoffset", iTemp) = True Then
        CurrentSkinInfo.TextBox.InactiveText.Effect.YOffset = iTemp
      Else
        CurrentSkinInfo.TextBox.InactiveText.Effect.YOffset = 0
      End If
    End If

    'Debug.Print("Skinning: Loading Tooltip Enable Glass")
    If Skinning_GetBooleanSkinValue(Doc, "/skin/tooltip", "enableglass", bTemp) = True Then
      CurrentSkinInfo.Tooltip.EnableGlass = bTemp
    Else
      CurrentSkinInfo.Tooltip.EnableGlass = True
    End If

    'Debug.Print("Skinning: Loading Tooltip Image")
    If Skinning_GetImageSkinValue(Doc, "/skin/tooltip/image", "file", imgTemp) = True Then
      CurrentSkinInfo.Tooltip.Image = imgTemp
      CurrentSkinInfo.Tooltip.ImageSize = CurrentSkinInfo.Tooltip.Image.Size
    Else
      Exit Sub
    End If

    'Debug.Print("Skinning: Loading Tooltip Mask Image")
    If CurrentSkinInfo.Tooltip.EnableGlass = True Then
      If Skinning_GetImageSkinValue(Doc, "/skin/tooltip/image", "mask", imgTemp) = True Then
        CurrentSkinInfo.Tooltip.MaskImage = imgTemp
      Else
        Exit Sub
      End If
    End If

    If Skinning_GetImageSkinValue(Doc, "/skin/tooltip/image", "filenoglass", imgTemp, True) = True Then
      CurrentSkinInfo.Tooltip.ImageNoGlass = imgTemp
    Else
      CurrentSkinInfo.Tooltip.ImageNoGlass = CurrentSkinInfo.Tooltip.Image
    End If
    CurrentSkinInfo.Tooltip.ImageNoGlassSize = CurrentSkinInfo.Tooltip.ImageNoGlass.Size

    'Debug.Print("Skinning: Loading Tooltip Sizing Type")
    If Skinning_GetSizingTypeSkinValue(Doc, "/skin/tooltip/image", stTemp) = True Then
      CurrentSkinInfo.Tooltip.SizingType = stTemp
    Else
      CurrentSkinInfo.Tooltip.SizingType = SkinSizingType.Stretch
    End If

    'Debug.Print("Skinning: Loading Tooltip Sizing Margins")
    If CurrentSkinInfo.Tooltip.SizingType <> SkinSizingType.None Then
      If Skinning_GetMarginsSkinValue(Doc, "/skin/tooltip/image", "sizingmargins", marTemp) = True Then
        CurrentSkinInfo.Tooltip.SizingMargins = marTemp
      Else
        CurrentSkinInfo.Tooltip.SizingMargins = New SkinMargins(0, 0, 0, 0)
      End If
    End If

    'Debug.Print("Skinning: Loading Tooltip Content Margins")
    If Skinning_GetMarginsSkinValue(Doc, "/skin/tooltip/image", "contentmargins", marTemp) = True Then
      CurrentSkinInfo.Tooltip.ContentMargins = marTemp
    Else
      CurrentSkinInfo.Tooltip.ContentMargins = New SkinMargins(4, 4, 4, 4)
    End If

    'Debug.Print("Skinning: Loading Tooltip Text Font")
    If Skinning_GetFontSkinValue(Doc, "/skin/tooltip/text", fntTemp) = True Then
      CurrentSkinInfo.Tooltip.Text.Font = fntTemp
    Else
      CurrentSkinInfo.Tooltip.Text.Font = SystemFonts.MessageBoxFont
    End If

    'Debug.Print("Skinning: Loading Tooltip Text Color")
    If Skinning_GetColorSkinValue(Doc, "/skin/tooltip/text", "color", colTemp) = True Then
      CurrentSkinInfo.Tooltip.Text.Color = colTemp
    Else
      CurrentSkinInfo.Tooltip.Text.Color = Color.Black
    End If

    'Debug.Print("Skinning: Loading Tooltip Text Y Offset")
    If Skinning_GetIntegerSkinValue(Doc, "/skin/tooltip/text", "yoffset", iTemp) = True Then
      CurrentSkinInfo.Tooltip.Text.YOffset = iTemp
    Else
      CurrentSkinInfo.Tooltip.Text.YOffset = 0
    End If

    'Debug.Print("Skinning: Loading Tooltip Text Effect Type")
    If Skinning_GetTextEffectTypeSkinValue(Doc, "/skin/tooltip/text", teTemp) = True Then
      CurrentSkinInfo.Tooltip.Text.Effect.Type = teTemp
    Else
      CurrentSkinInfo.Tooltip.Text.Effect.Type = SkinTextEffectType.None
    End If

    'Debug.Print("Skinning: Loading Tooltip Text Effect Color")
    If CurrentSkinInfo.Tooltip.Text.Effect.Type <> SkinTextEffectType.None Then
      If Skinning_GetColorSkinValue(Doc, "/skin/tooltip/text", "effectcolor", colTemp) = True Then
        CurrentSkinInfo.Tooltip.Text.Effect.Color = colTemp
      Else
        CurrentSkinInfo.Tooltip.Text.Effect.Color = Color.Black
      End If
    End If

    'Debug.Print("Skinning: Loading Tooltip Text Effect Size")
    If CurrentSkinInfo.Tooltip.Text.Effect.Type <> SkinTextEffectType.None Then
      If Skinning_GetIntegerSkinValue(Doc, "/skin/tooltip/text", "effectsize", iTemp) = True Then
        CurrentSkinInfo.Tooltip.Text.Effect.Size = iTemp
      Else
        CurrentSkinInfo.Tooltip.Text.Effect.Size = 0
      End If
    End If

    'Debug.Print("Skinning: Loading Tooltip Text Effect X Offset")
    If CurrentSkinInfo.Tooltip.Text.Effect.Type = SkinTextEffectType.Shadow Then
      If Skinning_GetIntegerSkinValue(Doc, "/skin/tooltip/text", "effectxoffset", iTemp) = True Then
        CurrentSkinInfo.Tooltip.Text.Effect.XOffset = iTemp
      Else
        CurrentSkinInfo.Tooltip.Text.Effect.XOffset = 0
      End If
    End If

    'Debug.Print("Skinning: Loading Tooltip Text Effect Y Offset")
    If CurrentSkinInfo.Tooltip.Text.Effect.Type = SkinTextEffectType.Shadow Then
      If Skinning_GetIntegerSkinValue(Doc, "/skin/tooltip/text", "effectyoffset", iTemp) = True Then
        CurrentSkinInfo.Tooltip.Text.Effect.YOffset = iTemp
      Else
        CurrentSkinInfo.Tooltip.Text.Effect.YOffset = 0
      End If
    End If

    'Debug.Print("Skinning: Loading Tooltip Separator Image")
    If Skinning_GetImageSkinValue(Doc, "/skin/tooltip/separator/image", "file", imgTemp) = True Then
      CurrentSkinInfo.Tooltip.Separator.Image = imgTemp
      CurrentSkinInfo.Tooltip.Separator.ImageSize = CurrentSkinInfo.Tooltip.Separator.Image.Size
    Else
      Exit Sub
    End If

    'Debug.Print("Skinning: Loading Tooltip Separator Sizing Type")
    If Skinning_GetSizingTypeSkinValue(Doc, "/skin/tooltip/separator/image", stTemp) = True Then
      CurrentSkinInfo.Tooltip.Separator.SizingType = stTemp
    Else
      CurrentSkinInfo.Tooltip.Separator.SizingType = SkinSizingType.Stretch
    End If

    'Debug.Print("Skinning: Loading Tooltip Separator Sizing Margins")
    If Skinning_GetMarginsSkinValue(Doc, "/skin/tooltip/separator/image", "sizingmargins", marTemp) = True Then
      CurrentSkinInfo.Tooltip.Separator.SizingMargins = marTemp
    Else
      CurrentSkinInfo.Tooltip.Separator.SizingMargins = New SkinMargins(0, 0, 0, 0)
    End If

    'Debug.Print("Skinning: Loading Graph Image")
    If Skinning_GetImageSkinValue(Doc, "/skin/graph/image", "file", imgTemp) = True Then
      CurrentSkinInfo.Graph.Image = imgTemp
      CurrentSkinInfo.Graph.ImageSize = CurrentSkinInfo.Graph.Image.Size
    Else
      Exit Sub
    End If

    'Debug.Print("Skinning: Loading Graph Image Sizing Type")
    If Skinning_GetSizingTypeSkinValue(Doc, "/skin/graph/image", stTemp) = True Then
      CurrentSkinInfo.Graph.SizingType = stTemp
    Else
      CurrentSkinInfo.Graph.SizingType = SkinSizingType.Stretch
    End If

    'Debug.Print("Skinning: Loading Graph Image Sizing Margins")
    If CurrentSkinInfo.Graph.SizingType <> SkinSizingType.None Then
      If Skinning_GetMarginsSkinValue(Doc, "/skin/graph/image", "sizingmargins", marTemp) = True Then
        CurrentSkinInfo.Graph.SizingMargins = marTemp
      Else
        CurrentSkinInfo.Graph.SizingMargins = New SkinMargins(0, 0, 0, 0)
      End If
    End If

    'Debug.Print("Skinning: Loading Graph Image Content Margins")
    If Skinning_GetMarginsSkinValue(Doc, "/skin/graph/image", "contentmargins", marTemp) = True Then
      CurrentSkinInfo.Graph.ContentMargins = marTemp
    Else
      CurrentSkinInfo.Graph.ContentMargins = New SkinMargins(1, 1, 1, 1)
    End If

    'Debug.Print("Skinning: Loading Graph Grid Line Color")
    If Skinning_GetColorSkinValue(Doc, "/skin/graph/gridlines", "color", colTemp) = True Then
      CurrentSkinInfo.Graph.GridLineColor = colTemp
    Else
      CurrentSkinInfo.Graph.GridLineColor = Color.Gray
    End If

    'Debug.Print("Skinning: Loading Menu Background Image")
    If Skinning_GetImageSkinValue(Doc, "/skin/menu/background/image", "file", imgTemp) = True Then
      CurrentSkinInfo.Menu.Background.Image = imgTemp
      CurrentSkinInfo.Menu.Background.ImageSize = CurrentSkinInfo.Menu.Background.Image.Size
    Else
      Exit Sub
    End If

    'Debug.Print("Skinning: Loading Menu Background Sizing Type")
    If Skinning_GetSizingTypeSkinValue(Doc, "/skin/menu/background/image", stTemp) = True Then
      CurrentSkinInfo.Menu.Background.SizingType = stTemp
    Else
      CurrentSkinInfo.Menu.Background.SizingType = SkinSizingType.Stretch
    End If

    'Debug.Print("Skinning: Loading Menu Background Sizing Margins")
    If Skinning_GetMarginsSkinValue(Doc, "/skin/menu/background/image", "sizingmargins", marTemp) = True Then
      CurrentSkinInfo.Menu.Background.SizingMargins = marTemp
    Else
      CurrentSkinInfo.Menu.Background.SizingMargins = New SkinMargins(1, 1, 1, 1)
    End If

    'Debug.Print("Skinning: Loading Menu Background Content Margins")
    If Skinning_GetMarginsSkinValue(Doc, "/skin/menu/background/image", "contentmargins", marTemp) = True Then
      CurrentSkinInfo.Menu.Background.ContentMargins = marTemp
    Else
      CurrentSkinInfo.Menu.Background.ContentMargins = New SkinMargins(2, 2, 2, 2)
    End If

    'Debug.Print("Skinning: Loading Menu Margin Image")
    If Skinning_GetImageSkinValue(Doc, "/skin/menu/margin/image", "file", imgTemp) = True Then
      CurrentSkinInfo.Menu.Margin.Image = imgTemp
      CurrentSkinInfo.Menu.Margin.ImageSize = CurrentSkinInfo.Menu.Margin.Image.Size
    Else
      Exit Sub
    End If

    'Debug.Print("Skinning: Loading Menu Margin Sizing Type")
    If Skinning_GetSizingTypeSkinValue(Doc, "/skin/menu/margin/image", stTemp) = True Then
      CurrentSkinInfo.Menu.Margin.SizingType = stTemp
    Else
      CurrentSkinInfo.Menu.Margin.SizingType = SkinSizingType.Stretch
    End If

    'Debug.Print("Skinning: Loading Menu Margin Sizing Margins")
    If Skinning_GetMarginsSkinValue(Doc, "/skin/menu/margin/image", "sizingmargins", marTemp) = True Then
      CurrentSkinInfo.Menu.Margin.SizingMargins = marTemp
    Else
      CurrentSkinInfo.Menu.Margin.SizingMargins = New SkinMargins(0, 0, 0, 0)
    End If

    'Debug.Print("Skinning: Loading Menu Item Image")
    If Skinning_GetImageSkinValue(Doc, "/skin/menu/item/image", "file", imgTemp) = True Then
      CurrentSkinInfo.Menu.Item.Image = imgTemp
      CurrentSkinInfo.Menu.Item.ImageSize = CurrentSkinInfo.Menu.Item.Image.Size
    Else
      Exit Sub
    End If

    'Debug.Print("Skinning: Loading Menu Item Sizing Type")
    If Skinning_GetSizingTypeSkinValue(Doc, "/skin/menu/item/image", stTemp) = True Then
      CurrentSkinInfo.Menu.Item.SizingType = stTemp
    Else
      CurrentSkinInfo.Menu.Item.SizingType = SkinSizingType.Stretch
    End If

    'Debug.Print("Skinning: Loading Menu Item Sizing Margins")
    If Skinning_GetMarginsSkinValue(Doc, "/skin/menu/item/image", "sizingmargins", marTemp) = True Then
      CurrentSkinInfo.Menu.Item.SizingMargins = marTemp
    Else
      CurrentSkinInfo.Menu.Item.SizingMargins = New SkinMargins(0, 0, 0, 0)
    End If

    'Debug.Print("Skinning: Loading Menu Item Content Margins")
    If Skinning_GetMarginsSkinValue(Doc, "/skin/menu/item/image", "contentmargins", marTemp) = True Then
      CurrentSkinInfo.Menu.Item.ContentMargins = marTemp
    Else
      CurrentSkinInfo.Menu.Item.ContentMargins = New SkinMargins(2, 2, 2, 2)
    End If

    'Debug.Print("Skinning: Loading Menu Item Normal Text Font")
    If Skinning_GetFontSkinValue(Doc, "/skin/menu/item/normaltext", fntTemp) = True Then
      CurrentSkinInfo.Menu.Item.NormalText.Font = fntTemp
    Else
      CurrentSkinInfo.Menu.Item.NormalText.Font = SystemFonts.MessageBoxFont
    End If

    'Debug.Print("Skinning: Loading Menu Item Normal Text Color")
    If Skinning_GetColorSkinValue(Doc, "/skin/menu/item/normaltext", "color", colTemp) = True Then
      CurrentSkinInfo.Menu.Item.NormalText.Color = colTemp
    Else
      CurrentSkinInfo.Menu.Item.NormalText.Color = Color.Black
    End If

    'Debug.Print("Skinning: Loading Menu Item Normal Text Y Offset")
    If Skinning_GetIntegerSkinValue(Doc, "/skin/menu/item/normaltext", "yoffset", iTemp) = True Then
      CurrentSkinInfo.Menu.Item.NormalText.YOffset = iTemp
    Else
      CurrentSkinInfo.Menu.Item.NormalText.YOffset = 0
    End If

    'Debug.Print("Skinning: Loading Menu Item Normal Text Effect Type")
    If Skinning_GetTextEffectTypeSkinValue(Doc, "/skin/menu/item/normaltext", teTemp) = True Then
      CurrentSkinInfo.Menu.Item.NormalText.Effect.Type = teTemp
    Else
      CurrentSkinInfo.Menu.Item.NormalText.Effect.Type = SkinTextEffectType.None
    End If

    'Debug.Print("Skinning: Loading Menu Item Normal Text Effect Color")
    If CurrentSkinInfo.Menu.Item.NormalText.Effect.Type <> SkinTextEffectType.None Then
      If Skinning_GetColorSkinValue(Doc, "/skin/menu/item/normaltext", "effectcolor", colTemp) = True Then
        CurrentSkinInfo.Menu.Item.NormalText.Effect.Color = colTemp
      Else
        CurrentSkinInfo.Menu.Item.NormalText.Effect.Color = Color.Black
      End If
    End If

    'Debug.Print("Skinning: Loading Menu Item Normal Text Effect Size")
    If CurrentSkinInfo.Menu.Item.NormalText.Effect.Type <> SkinTextEffectType.None Then
      If Skinning_GetIntegerSkinValue(Doc, "/skin/menu/item/normaltext", "effectsize", iTemp) = True Then
        CurrentSkinInfo.Menu.Item.NormalText.Effect.Size = iTemp
      Else
        CurrentSkinInfo.Menu.Item.NormalText.Effect.Size = 0
      End If
    End If

    'Debug.Print("Skinning: Loading Menu Item Normal Text Effect X Offset")
    If CurrentSkinInfo.Menu.Item.NormalText.Effect.Type = SkinTextEffectType.Shadow Then
      If Skinning_GetIntegerSkinValue(Doc, "/skin/menu/item/normaltext", "effectxoffset", iTemp) = True Then
        CurrentSkinInfo.Menu.Item.NormalText.Effect.XOffset = iTemp
      Else
        CurrentSkinInfo.Menu.Item.NormalText.Effect.XOffset = 0
      End If
    End If

    'Debug.Print("Skinning: Loading Menu Item Normal Text Effect Y Offset")
    If CurrentSkinInfo.Menu.Item.NormalText.Effect.Type = SkinTextEffectType.Shadow Then
      If Skinning_GetIntegerSkinValue(Doc, "/skin/menu/item/normaltext", "effectyoffset", iTemp) = True Then
        CurrentSkinInfo.Menu.Item.NormalText.Effect.YOffset = iTemp
      Else
        CurrentSkinInfo.Menu.Item.NormalText.Effect.YOffset = 0
      End If
    End If

    'Debug.Print("Skinning: Loading Menu Item Hover Text Font")
    If Skinning_GetFontSkinValue(Doc, "/skin/menu/item/hovertext", fntTemp) = True Then
      CurrentSkinInfo.Menu.Item.HoverText.Font = fntTemp
    Else
      CurrentSkinInfo.Menu.Item.HoverText.Font = SystemFonts.MessageBoxFont
    End If

    'Debug.Print("Skinning: Loading Menu Item Hover Text Color")
    If Skinning_GetColorSkinValue(Doc, "/skin/menu/item/hovertext", "color", colTemp) = True Then
      CurrentSkinInfo.Menu.Item.HoverText.Color = colTemp
    Else
      CurrentSkinInfo.Menu.Item.HoverText.Color = Color.Black
    End If

    'Debug.Print("Skinning: Loading Menu Item Hover Text Y Offset")
    If Skinning_GetIntegerSkinValue(Doc, "/skin/menu/item/hovertext", "yoffset", iTemp) = True Then
      CurrentSkinInfo.Menu.Item.HoverText.YOffset = iTemp
    Else
      CurrentSkinInfo.Menu.Item.HoverText.YOffset = 0
    End If

    'Debug.Print("Skinning: Loading Menu Item Hover Text Effect Type")
    If Skinning_GetTextEffectTypeSkinValue(Doc, "/skin/menu/item/hovertext", teTemp) = True Then
      CurrentSkinInfo.Menu.Item.HoverText.Effect.Type = teTemp
    Else
      CurrentSkinInfo.Menu.Item.HoverText.Effect.Type = SkinTextEffectType.None
    End If

    'Debug.Print("Skinning: Loading Menu Item Hover Text Effect Color")
    If CurrentSkinInfo.Menu.Item.HoverText.Effect.Type <> SkinTextEffectType.None Then
      If Skinning_GetColorSkinValue(Doc, "/skin/menu/item/hovertext", "effectcolor", colTemp) = True Then
        CurrentSkinInfo.Menu.Item.HoverText.Effect.Color = colTemp
      Else
        CurrentSkinInfo.Menu.Item.HoverText.Effect.Color = Color.Black
      End If
    End If

    'Debug.Print("Skinning: Loading Menu Item Hover Text Effect Size")
    If CurrentSkinInfo.Menu.Item.HoverText.Effect.Type <> SkinTextEffectType.None Then
      If Skinning_GetIntegerSkinValue(Doc, "/skin/menu/item/hovertext", "effectsize", iTemp) = True Then
        CurrentSkinInfo.Tooltip.Text.Effect.Size = iTemp
      Else
        CurrentSkinInfo.Tooltip.Text.Effect.Size = 0
      End If
    End If

    'Debug.Print("Skinning: Loading Menu Item Hover Text Effect X Offset")
    If CurrentSkinInfo.Menu.Item.HoverText.Effect.Type = SkinTextEffectType.Shadow Then
      If Skinning_GetIntegerSkinValue(Doc, "/skin/menu/item/hovertext", "effectxoffset", iTemp) = True Then
        CurrentSkinInfo.Menu.Item.HoverText.Effect.XOffset = iTemp
      Else
        CurrentSkinInfo.Menu.Item.HoverText.Effect.XOffset = 0
      End If
    End If

    'Debug.Print("Skinning: Loading Menu Item Hover Text Effect Y Offset")
    If CurrentSkinInfo.Menu.Item.HoverText.Effect.Type = SkinTextEffectType.Shadow Then
      If Skinning_GetIntegerSkinValue(Doc, "/skin/menu/item/hovertext", "effectyoffset", iTemp) = True Then
        CurrentSkinInfo.Menu.Item.HoverText.Effect.YOffset = iTemp
      Else
        CurrentSkinInfo.Menu.Item.HoverText.Effect.YOffset = 0
      End If
    End If

    'Debug.Print("Skinning: Loading Menu Separator Image")
    If Skinning_GetImageSkinValue(Doc, "/skin/menu/separator/image", "file", imgTemp) = True Then
      CurrentSkinInfo.Menu.Separator.Image = imgTemp
      CurrentSkinInfo.Menu.Separator.ImageSize = CurrentSkinInfo.Menu.Separator.Image.Size
    Else
      Exit Sub
    End If

    'Debug.Print("Skinning: Loading Menu Separator Sizing Type")
    If Skinning_GetSizingTypeSkinValue(Doc, "/skin/menu/separator/image", stTemp) = True Then
      CurrentSkinInfo.Menu.Separator.SizingType = stTemp
    Else
      CurrentSkinInfo.Menu.Separator.SizingType = SkinSizingType.Stretch
    End If

    'Debug.Print("Skinning: Loading Menu Separator Sizing Margins")
    If Skinning_GetMarginsSkinValue(Doc, "/skin/menu/separator/image", "sizingmargins", marTemp) = True Then
      CurrentSkinInfo.Menu.Separator.SizingMargins = marTemp
    Else
      CurrentSkinInfo.Menu.Separator.SizingMargins = New SkinMargins(0, 0, 0, 0)
    End If

    'Debug.Print("Skinning: Loading Menu Arrow Image")
    If Skinning_GetImageSkinValue(Doc, "/skin/menu/arrow/image", "file", imgTemp) = True Then
      CurrentSkinInfo.Menu.Arrow.Image = imgTemp
      CurrentSkinInfo.Menu.Arrow.ImageSize = CurrentSkinInfo.Menu.Arrow.Image.Size
    Else
      Exit Sub
    End If

    'Debug.Print("Skinning: Loading Menu Check/Radio Image")
    If Skinning_GetImageSkinValue(Doc, "/skin/menu/checkradio/image", "file", imgTemp) = True Then
      CurrentSkinInfo.Menu.CheckRadio.Image = imgTemp
      CurrentSkinInfo.Menu.CheckRadio.ImageSize = CurrentSkinInfo.Menu.CheckRadio.Image.Size
    Else
      Exit Sub
    End If

    'Debug.Print("Skinning: Loading Slider Background Image")
    If Skinning_GetImageSkinValue(Doc, "/skin/slider/background/image", "file", imgTemp) = True Then
      CurrentSkinInfo.Slider.Background.Image = imgTemp
      CurrentSkinInfo.Slider.Background.ImageSize = CurrentSkinInfo.Slider.Background.Image.Size
    Else
      Exit Sub
    End If

    'Debug.Print("Skinning: Loading Slider Background Sizing Type")
    If Skinning_GetSizingTypeSkinValue(Doc, "/skin/slider/background/image", stTemp) = True Then
      CurrentSkinInfo.Slider.Background.SizingType = stTemp
    Else
      CurrentSkinInfo.Slider.Background.SizingType = SkinSizingType.Stretch
    End If

    'Debug.Print("Skinning: Loading Slider Background Sizing Margins")
    If Skinning_GetMarginsSkinValue(Doc, "/skin/slider/background/image", "sizingmargins", marTemp) = True Then
      CurrentSkinInfo.Slider.Background.SizingMargins = marTemp
    Else
      CurrentSkinInfo.Slider.Background.SizingMargins = New SkinMargins(0, 0, 0, 0)
    End If

    'Debug.Print("Skinning: Loading Slider Background Content Margins")
    If Skinning_GetMarginsSkinValue(Doc, "/skin/slider/background/image", "contentmargins", marTemp) = True Then
      CurrentSkinInfo.Slider.Background.ContentMargins = marTemp
    Else
      CurrentSkinInfo.Slider.Background.ContentMargins = New SkinMargins(0, 0, 0, 0)
    End If

    'Debug.Print("Skinning: Loading Slider Button Image")
    If Skinning_GetImageSkinValue(Doc, "/skin/slider/button/image", "file", imgTemp) = True Then
      CurrentSkinInfo.Slider.Button.Image = imgTemp
      CurrentSkinInfo.Slider.Button.ImageSize = CurrentSkinInfo.Slider.Button.Image.Size
    Else
      Exit Sub
    End If

    'Debug.Print("Skinning: Loading Slider Button Sizing Type")
    If Skinning_GetSizingTypeSkinValue(Doc, "/skin/slider/button/image", stTemp) = True Then
      CurrentSkinInfo.Slider.Button.SizingType = stTemp
    Else
      CurrentSkinInfo.Slider.Button.SizingType = SkinSizingType.None
    End If

    'Debug.Print("Skinning: Loading Slider Button Sizing Margins")
    If Skinning_GetMarginsSkinValue(Doc, "/skin/slider/button/image", "sizingmargins", marTemp) = True Then
      CurrentSkinInfo.Slider.Button.SizingMargins = marTemp
    Else
      CurrentSkinInfo.Slider.Button.SizingMargins = New SkinMargins(0, 0, 0, 0)
    End If

  End Sub

  Private Function Skinning_GetIntegerSkinValue(ByRef Doc As XmlDocument, ByVal Path As String, ByVal Attribute As String, ByRef RetVal As Integer, Optional ByVal IgnoreError As Boolean = False) As Boolean
    Try
      Dim sTemp As String
      sTemp = GetSkinValue(Doc, Path, Attribute)
      If IsNumeric(sTemp) Then
        RetVal = CInt(sTemp)
        Return True
      Else
        Skinning_RaiseSkinError("Reason: The attribute must be a number." & vbCrLf & "Path: " & Path & vbCrLf & "Attribute: " & Attribute)
        Return False
      End If
    Catch ex As Exception
      If IgnoreError Then Return False
      Skinning_RaiseSkinError("Reason: Attribute does not exist." & vbCrLf & "Path: " & Path & vbCrLf & "Attribute: " & Attribute)
      Return False
    End Try
  End Function

  Private Function Skinning_GetStringSkinValue(ByRef Doc As XmlDocument, ByVal Path As String, ByVal Attribute As String, ByRef RetVal As String, Optional ByVal IgnoreError As Boolean = False) As Boolean
    Try
      Dim sTemp As String
      sTemp = GetSkinValue(Doc, Path, Attribute)
      If TypeOf sTemp Is String Then
        RetVal = sTemp
        Return True
      Else
        Skinning_RaiseSkinError("Reason: The attribute must be a string." & vbCrLf & "Path: " & Path & vbCrLf & "Attribute: " & Attribute)
        Return False
      End If
    Catch ex As Exception
      If IgnoreError Then Return False
      Skinning_RaiseSkinError("Reason: Attribute does not exist." & vbCrLf & "Path: " & Path & vbCrLf & "Attribute: " & Attribute)
      Return False
    End Try
  End Function

  Private Function Skinning_GetBooleanSkinValue(ByRef Doc As XmlDocument, ByVal Path As String, ByVal Attribute As String, ByRef RetVal As Boolean, Optional ByVal IgnoreError As Boolean = False) As Boolean
    Try
      Dim sTemp As String
      sTemp = GetSkinValue(Doc, Path, Attribute)
      Select Case LCase(sTemp)
        Case "true"
          RetVal = True
          Return True
        Case "false"
          RetVal = False
          Return True
        Case Else
          Skinning_RaiseSkinError("Reason: The attribute must be ""true"" or ""false""." & vbCrLf & "Path: " & Path & vbCrLf & "Attribute: " & Attribute)
          Return False
      End Select
    Catch ex As Exception
      If IgnoreError Then Return False
      Skinning_RaiseSkinError("Reason: Attribute does not exist." & vbCrLf & "Path: " & Path & vbCrLf & "Attribute: " & Attribute)
      Return False
    End Try
  End Function

  Private Function Skinning_GetImageSkinValue(ByRef Doc As XmlDocument, ByVal Path As String, ByVal Attribute As String, ByRef RetVal As Image, Optional ByVal IgnoreError As Boolean = False) As Boolean
    Try
      Dim sTemp As String
      sTemp = GetSkinValue(Doc, Path, Attribute)
      If File.Exists(CurrentSkinInfo.Path & sTemp) = True Then
        RetVal = LoadImageFromFile(CurrentSkinInfo.Path & sTemp)
        Return True
      Else
        Skinning_RaiseSkinError("Reason: The image file was not found." & vbCrLf & "Path: " & Path & vbCrLf & "Attribute: " & Attribute)
        Return False
      End If
    Catch ex As Exception
      If IgnoreError Then Return False
      Skinning_RaiseSkinError("Reason: Attribute does not exist." & vbCrLf & "Path: " & Path & vbCrLf & "Attribute: " & Attribute)
      Return False
    End Try
  End Function

  Private Function Skinning_GetSizingTypeSkinValue(ByRef Doc As XmlDocument, ByVal Path As String, ByRef RetVal As SkinSizingType, Optional ByVal IgnoreError As Boolean = False) As Boolean
    Try
      Dim sTemp As String
      sTemp = GetSkinValue(Doc, Path, "sizingtype")
      Select Case sTemp.ToLower
        Case "none"
          RetVal = SkinSizingType.None
          Return True
        Case "tile"
          RetVal = SkinSizingType.Tile
          Return True
        Case "stretch"
          RetVal = SkinSizingType.Stretch
          Return True
        Case Else
          RetVal = SkinSizingType.None
          Skinning_RaiseSkinError("Reason: The sizing type must be ""none"", ""tile"" or ""stretch""." & vbCrLf & "Path: " & Path & vbCrLf & "Attribute: sizingtype")
          Return False
      End Select
    Catch ex As Exception
      If IgnoreError Then Return False
      RetVal = SkinSizingType.None
      Skinning_RaiseSkinError("Reason: The attribute is missing." & vbCrLf & "Path: " & Path & vbCrLf & "Attribute: sizingtype")
      Return False
    End Try
  End Function

  Private Function Skinning_GetMarginsSkinValue(ByRef Doc As XmlDocument, ByVal Path As String, ByVal Attribute As String, ByRef RetVal As SkinMargins, Optional ByVal IgnoreError As Boolean = False) As Boolean
    Try
      Dim sTemp As String, aMargins() As String
      sTemp = GetSkinValue(Doc, Path, Attribute)
      aMargins = sTemp.Split(",")
      If aMargins.Length = 4 Then
        If IsNumeric(aMargins(0)) AndAlso IsNumeric(aMargins(1)) AndAlso IsNumeric(aMargins(2)) AndAlso IsNumeric(aMargins(3)) Then
          RetVal = New SkinMargins(aMargins(0), aMargins(1), aMargins(2), aMargins(3))
          Return True
        Else
          Skinning_RaiseSkinError("Reason: The margins must be four integers separated by commas: left,top,right,bottom." & vbCrLf & "Path: " & Path & vbCrLf & "Attribute: " & Attribute)
          Return False
        End If
      Else
        Skinning_RaiseSkinError("Reason: The margins must be four integers separated by commas: left,top,right,bottom." & vbCrLf & "Path: " & Path & vbCrLf & "Attribute: " & Attribute)
        Return False
      End If
    Catch ex As Exception
      If IgnoreError Then Return False
      Skinning_RaiseSkinError("Reason: The attribute is missing." & vbCrLf & "Path: " & Path & vbCrLf & "Attribute: " & Attribute)
      Return False
    End Try
  End Function

  Private Function Skinning_GetColorSkinValue(ByRef Doc As XmlDocument, ByVal Path As String, ByVal Attribute As String, ByRef RetVal As Color) As Boolean
    Try
      Dim sTemp As String, aColor() As String
      sTemp = GetSkinValue(Doc, Path, Attribute)
      aColor = sTemp.Split(",")
      If aColor.Length = 4 Then
        If IsNumeric(aColor(0)) AndAlso IsNumeric(aColor(1)) AndAlso IsNumeric(aColor(2)) AndAlso IsNumeric(aColor(3)) Then
          RetVal = Color.FromArgb(aColor(0), aColor(1), aColor(2), aColor(3))
          Return True
        Else
          Skinning_RaiseSkinError("Reason: The color attribute must contain four integers from 0 to 255 separated by commas: alpha,red,green,blue." & vbCrLf & "Path: " & Path & "Attribute: " & Attribute)
          Return False
        End If
      Else
        Skinning_RaiseSkinError("Reason: The color attribute must contain four integers from 0 to 255 separated by commas: alpha,red,green,blue." & vbCrLf & "Path: " & Path & "Attribute: " & Attribute)
        Return False
      End If
    Catch ex As Exception
      Skinning_RaiseSkinError("Reason: The color attribute is missing." & vbCrLf & "Path: " & Path & vbCrLf & "Attribute: " & Attribute)
      Return False
    End Try
  End Function

  Private Function Skinning_GetTextEffectTypeSkinValue(ByRef Doc As XmlDocument, ByVal Path As String, ByRef RetVal As SkinTextEffectType) As Boolean
    Try
      Dim sTemp As String
      sTemp = GetSkinValue(Doc, Path, "effect")
      Select Case sTemp
        Case "none"
          RetVal = SkinTextEffectType.None
          Return True
        Case "glow"
          RetVal = SkinTextEffectType.Glow
          Return True
        Case "shadow"
          RetVal = SkinTextEffectType.Shadow
          Return True
        Case Else
          Skinning_RaiseSkinError("Reason: The text effect type attribute must be ""none"", ""glow"" or ""shadow""." & vbCrLf & "Path: " & Path & "Attribute: effect")
          Return False
      End Select
    Catch ex As Exception
      Return False
    End Try
  End Function

  Private Function Skinning_GetFontSkinValue(ByRef Doc As XmlDocument, ByVal Path As String, ByRef RetVal As Font) As Boolean
    Dim sTemp As String, sFontName As String, sngFontSize As Single, bFontBold As Boolean, bFontItalic As Boolean, bFontUnderline As Boolean

    ' Font Name
    Try
      sFontName = GetSkinValue(Doc, Path, "font")
    Catch ex As Exception
      Skinning_RaiseSkinError("Reason: The font name must be specified." & vbCrLf & "Path: " & Path & vbCrLf & "Attribute: font")
      Return False
    End Try

    ' Font Size
    Try
      sTemp = GetSkinValue(Doc, Path, "fontsize")
      If IsNumeric(sTemp) Then
        sngFontSize = CDbl(sTemp)
      Else
        Skinning_RaiseSkinError("Reason: The font size must be an number." & vbCrLf & "Path: " & Path & vbCrLf & "Attribute: fontsize")
        Return False
      End If
    Catch ex As Exception
      Skinning_RaiseSkinError("Reason: The font size must be specified." & vbCrLf & "Path: " & Path & vbCrLf & "Attribute: fontsize")
      Return False
    End Try

    ' Font Bold
    Try
      sTemp = GetSkinValue(Doc, Path, "fontbold")
      Select Case sTemp
        Case "true"
          bFontBold = True
        Case "false"
          bFontBold = False
        Case Else
          Skinning_RaiseSkinError("Reason: The font bold attribute must be an ""true"" or ""false""." & vbCrLf & "Path: " & Path & vbCrLf & "Attribute: fontbold")
          Return False
      End Select
    Catch ex As Exception
      bFontBold = False
    End Try

    ' Font Italic
    Try
      sTemp = GetSkinValue(Doc, Path, "fontitalic")
      Select Case sTemp
        Case "true"
          bFontItalic = True
        Case "false"
          bFontItalic = False
        Case Else
          Skinning_RaiseSkinError("Reason: The font italic attribute must be an ""true"" or ""false""." & vbCrLf & "Path: " & Path & vbCrLf & "Attribute: fontitalic")
          Return False
      End Select
    Catch ex As Exception
      bFontItalic = False
    End Try

    ' Font Underline
    Try
      sTemp = GetSkinValue(Doc, Path, "fontunderline")
      Select Case sTemp
        Case "true"
          bFontUnderline = True
        Case "false"
          bFontUnderline = False
        Case Else
          Skinning_RaiseSkinError("Reason: The font underline attribute must be an ""true"" or ""false""." & vbCrLf & "Path: " & Path & vbCrLf & "Attribute: fontunderline")
          Return False
      End Select
    Catch ex As Exception
      bFontUnderline = False
    End Try

    ' Create Font
    Try
      Dim fsStyle As FontStyle
      fsStyle = FontStyle.Regular
      If bFontBold Then fsStyle = fsStyle Or FontStyle.Bold
      If bFontUnderline Then fsStyle = fsStyle Or FontStyle.Underline
      If bFontItalic Then fsStyle = fsStyle Or FontStyle.Italic
      RetVal = New Font(sFontName, sngFontSize, fsStyle, GraphicsUnit.Point)
      Return True
    Catch ex As Exception
      Skinning_RaiseSkinError("Reason: The font could not be created. Make sure the font family exists." & vbCrLf & "Path: " & Path & vbCrLf & "Attribute: fontunderline")
      Return False
    End Try
  End Function

  Private Sub Skinning_RaiseSkinError(Optional ByVal Detail As String = "")
    Dim sText As String = "The current skin could not be loaded."
    If Detail <> "" Then
      sText = sText & vbCrLf & vbCrLf & Detail
    End If
    MsgBox(sText, MsgBoxStyle.Critical Or MsgBoxStyle.OkOnly, "Skin Error")
  End Sub

  Private Function GetSkinValue(ByVal Doc As XmlDocument, ByVal Path As String, ByVal Attribute As String) As String
    Dim Node As XmlNode = Nothing
    Node = Doc.DocumentElement.SelectSingleNode(Path)
    Return Node.Attributes(Attribute).Value
  End Function

End Module
