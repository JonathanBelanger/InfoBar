Module modSkinning

  Private lastRows As Integer
  Public SkinCollection As New Collection
  Public CurrentTooltipOwnerGUID As String
  Public CurrentTooltipOwnerObjectID As String
  Public NewTooltipOwnerGUID As String
  Public NewTooltipOwnerObjectID As String
  Public SkinUpdateInProgress As Boolean
  Public RightClickedModule As String
  Public MainFormGraphics As Graphics

  Public Sub Skinning_UpdateWindow()
    If MainForm_memDC = IntPtr.Zero Then Exit Sub
    If MainFormGraphics IsNot Nothing Then Exit Sub
    If SkinUpdateInProgress = True Then Exit Sub
    SkinUpdateInProgress = True

    Dim lNewHeight As Integer
    If IBSettings.SelectedModules.Count = 0 Then
      lNewHeight = CurrentSkinInfo.Height
    Else
      lNewHeight = CurrentSkinInfo.Height * IBSettings.General.Rows + (CurrentSkinInfo.VerticalSeparator.ImageSize.Height * (IBSettings.General.Rows - 1))
    End If
    If fMain.Height <> lNewHeight Then fMain.Height = lNewHeight

    Dim bitmapsRecreated As Boolean = False
    If MainForm_backBitmapSize <> MainForm_Size Then
      ABSetPos(IBSettings.General.Position)
      fMain.RecreateBitmaps(MainForm_Size)
      Skinning_DrawBackground()
      Skinning_DrawSeparator()
      bitmapsRecreated = True
    End If

    'Setup Our Bitmap And Graphics
    MainFormGraphics = Graphics.FromHdc(MainForm_memDC)
    MainFormGraphics.CompositingQuality = CompositingQuality.AssumeLinear
    MainFormGraphics.InterpolationMode = InterpolationMode.Default
    MainFormGraphics.PixelOffsetMode = PixelOffsetMode.None
    MainFormGraphics.SmoothingMode = SmoothingMode.AntiAlias
    MainFormGraphics.TextContrast = 0
    MainFormGraphics.Clear(Color.Transparent)

    'Draw Bar Background
    MainFormGraphics.DrawImageUnscaled(MainForm_backBitmap, 0, 0)

    If IBSettings.SelectedModules.Count = 0 Then

      ' Draw No Module Text If Necessary
      Skinning_DrawText(MainFormGraphics, "There are no modules loaded. Right click here and click settings to begin.", _
      New Rectangle(CurrentSkinInfo.Background.ContentMargins.Left, _
      CurrentSkinInfo.Background.ContentMargins.Top, _
      MainForm_backBitmapSize.Width - (CurrentSkinInfo.Background.ContentMargins.Left + CurrentSkinInfo.Background.ContentMargins.Right), _
      MainForm_backBitmapSize.Height - (CurrentSkinInfo.Background.ContentMargins.Top + CurrentSkinInfo.Background.ContentMargins.Bottom)), _
      CurrentSkinInfo.Background.Text, StringAlignment.Center, StringAlignment.Center)

    Else

      Dim rowWidth(IBSettings.General.Rows) As Integer
      Dim modulesInRow(IBSettings.General.Rows) As Integer
      Dim modulePaddingLeft As Integer = 0, modulePaddingRight As Integer = 0 ' Used only for justify alignment mode

      If IBSettings.General.ModuleAlignment > ModuleAlignment.Left Then
        For Each selMod As SelectedModuleType In IBSettings.SelectedModules
          Try
            rowWidth(selMod.Row - 1) += AvailableModules(selMod.GUID).GetModuleBitmap.Width
          Catch ex As Exception
            'Debug.Print("Could not get width for module: " & selMod.GUID)
          End Try
          modulesInRow(selMod.Row - 1) += 1
        Next

        Dim I As Integer
        For I = 0 To modulesInRow.Length - 1
          If IBSettings.General.ShowSeparators AndAlso modulesInRow(I) > 1 Then
            rowWidth(I) += (CurrentSkinInfo.Separator.ContentMargins.Left + CurrentSkinInfo.Separator.ImageSize.Width + CurrentSkinInfo.Separator.ContentMargins.Right) * (modulesInRow(I) - 1)
          End If
        Next
      End If

      ' Draw the modules
      Dim nextX As Integer, nextY As Integer
      Dim iCurRow As Integer, iCurMod As Integer
      For Each selMod As SelectedModuleType In IBSettings.SelectedModules
        If selMod.Row > (iCurRow + 1) Then
          iCurMod = 0
          iCurRow += 1
        End If

        If iCurMod = 0 Then
          Select Case IBSettings.General.ModuleAlignment
            Case ModuleAlignment.Left
              nextX = CurrentSkinInfo.Background.ContentMargins.Left
            Case ModuleAlignment.Center
              ' align center, measure width and subtract from bar width, then divide by 2, this will be nextX
              nextX = (MainForm_Size.Width - rowWidth(iCurRow)) / 2
            Case ModuleAlignment.Right
              ' align right, measure width and subtract from bar width, this will be nextX        
              nextX = MainForm_Size.Width - (rowWidth(iCurRow) + CurrentSkinInfo.Background.ContentMargins.Right)
            Case ModuleAlignment.Justify
              nextX = CurrentSkinInfo.Background.ContentMargins.Left
              modulePaddingLeft = 0
              modulePaddingRight = (MainForm_Size.Width - (CurrentSkinInfo.Background.ContentMargins.Left + rowWidth(iCurRow)) + CurrentSkinInfo.Background.ContentMargins.Right) / modulesInRow(iCurRow)
            Case ModuleAlignment.JustifyCenter
              nextX = CurrentSkinInfo.Background.ContentMargins.Left
              Dim tmpPadding As Integer = ((MainForm_Size.Width - (CurrentSkinInfo.Background.ContentMargins.Left + rowWidth(iCurRow) + CurrentSkinInfo.Background.ContentMargins.Right)) / modulesInRow(iCurRow)) / 2
              modulePaddingLeft = tmpPadding
              modulePaddingRight = tmpPadding
          End Select

          nextY = (CurrentSkinInfo.Background.ContentMargins.Top * (iCurRow + 1)) + _
            (CurrentSkinInfo.VerticalSeparator.ImageSize.Height * iCurRow) + _
            ((CurrentSkinInfo.Height - CurrentSkinInfo.Background.ContentMargins.Top) * iCurRow)
        End If

        If iCurMod > 0 Then

          If IBSettings.General.ModuleAlignment = ModuleAlignment.Justify _
          Or IBSettings.General.ModuleAlignment = ModuleAlignment.JustifyCenter Then nextX += modulePaddingRight

          If IBSettings.General.ShowSeparators Then
            nextX += CurrentSkinInfo.Separator.ContentMargins.Left
            MainFormGraphics.DrawImageUnscaled(MainForm_sepBitmap, nextX, nextY)
            nextX += (MainForm_sepBitmap.Width + CurrentSkinInfo.Separator.ContentMargins.Right)
          End If

        End If

        Dim modBitmap As Bitmap = AvailableModules(selMod.GUID).GetModuleBitmap
        If modBitmap IsNot Nothing Then
          If IBSettings.General.ModuleAlignment = ModuleAlignment.JustifyCenter Then nextX += modulePaddingLeft
          MainFormGraphics.DrawImageUnscaled(modBitmap, nextX, nextY)
          AvailableModules(selMod.GUID).ModuleBounds = New Rectangle(nextX, nextY, modBitmap.Width, modBitmap.Height)
          nextX += modBitmap.Width
          modBitmap.Dispose()
          iCurMod += 1
        End If

      Next
    End If

    'MainFormGraphics.ReleaseHdc(MainForm_memDC)
    MainFormGraphics.Dispose()
    MainFormGraphics = Nothing

    If fMain.IsDisposed = False Then
      Dim del As New frmMain.SetWindowBitmapDelegate(AddressOf fMain.SetWindowBitmap)
      fMain.Invoke(del)
    End If

    SkinUpdateInProgress = False
  End Sub

End Module