Module modWebSearch

    Public Class WebSearchProvidersType
        Public Key As String
        Public Name As String
        Public URL As String
        Public Icon As String
    End Class

    Public Sub WebSearch_ProcessSearch()
        Dim sURL As String
        sURL = IBSettings.Modules.WebSearch.SearchProviders(IBSettings.Modules.WebSearch.SelectedSearchProvider).url
        'sURL = sURL.Replace("{QUERY}", frmMain.txtWebSearch.Text)
        'frmMain.txtWebSearch.Text = vbNullString
        'frmMain.txtWebSearch.Tag = False
        Skinning_UpdateWindow()
        ShellExecute(frmMain.Handle, "open", "" & sURL, "", "", AppWinStyle.NormalFocus)
    End Sub

    Public Sub WebSearch_Draw(ByRef Gr As Graphics, ByRef nextX As Integer)
        If ModuleCache.WebSearch.Enabled = False Then Exit Sub

        ' Draw Search Engine Switcher Button
        Dim sesR As New Rectangle(nextX, CurrentSkinInfo.Background.ContentMargins.Top, 1, CurrentSkinInfo.Height - (CurrentSkinInfo.Background.ContentMargins.Top + CurrentSkinInfo.Background.ContentMargins.Bottom))
        Dim sesFR As Rectangle = Skinning_DrawButton(Gr, sesR, ModuleCache.WebSearch.SearchEngineSelectorButtonEngineIcon, vbNullString, ModuleCache.WebSearch.SearchEngineSelectorButtonState, True)
        ModuleCache.WebSearch.SearchEngineSelectorButtonBounds = sesFR
        nextX = nextX + sesFR.Width + 2

        ' Draw Textbox
        Dim trs As Rectangle
        trs.X = nextX
        trs.Y = CurrentSkinInfo.Background.ContentMargins.Top
        trs.Width = 200
        trs.Height = CurrentSkinInfo.Height - (CurrentSkinInfo.Background.ContentMargins.Top + CurrentSkinInfo.Background.ContentMargins.Bottom)
        Skinning_DrawTextBox(Gr, trs)
        nextX = nextX + trs.Width '+ 2
        trs.X = trs.X + CurrentSkinInfo.TextBox.ContentMargins.Left
        trs.Y = trs.Y + CurrentSkinInfo.TextBox.ContentMargins.Top
        trs.Width = trs.Width - (CurrentSkinInfo.TextBox.ContentMargins.Left + CurrentSkinInfo.TextBox.ContentMargins.Right)
        trs.Height = trs.Height - (CurrentSkinInfo.TextBox.ContentMargins.Top + CurrentSkinInfo.TextBox.ContentMargins.Bottom)
        'If frmMain.txtWebSearch.Left <> trs.X Or frmMain.txtWebSearch.Top <> trs.Y Or frmMain.txtWebSearch.Width <> trs.Width Or frmMain.txtWebSearch.Height <> trs.Height Then
        'frmMain.txtWebSearch.SetBounds(trs.X, trs.Y, trs.Width, trs.Height)
        'End If
        'If frmMain.txtWebSearch.Tag = True Then
        'Skinning_DrawTextFromTextbox(Gr, trs, frmMain.txtWebSearch)
        'Else
        'Skinning_DrawText(Gr, IBSettings.Modules.WebSearch.SelectedSearchProvider, trs, CurrentSkinInfo.TextBox.InactiveText, StringAlignment.Near)
        'End If

        ' Draw Go Button
        If IBSettings.Modules.WebSearch.ShowGoButton Then
            nextX = nextX + 2
            Dim bR As New Rectangle(nextX, CurrentSkinInfo.Background.ContentMargins.Top, 1, CurrentSkinInfo.Height - (CurrentSkinInfo.Background.ContentMargins.Top + CurrentSkinInfo.Background.ContentMargins.Bottom))
            Dim bFR As Rectangle = Skinning_DrawButton(Gr, bR, CurrentIconTheme.WebSearchGo, "Go", ModuleCache.WebSearch.GoButtonState, False)
            ModuleCache.WebSearch.GoButtonBounds = bFR
            nextX = nextX + bFR.Width '+ 2
        End If
    End Sub

    Public Sub WebSearch_ProcessMouseMove(ByRef e As MouseEventArgs, ByRef bWindowIsDirty As Boolean)
        If ModuleCache.WebSearch.Enabled = False Then Exit Sub

        ' Web Search Engine Selector Button
        If ModuleCache.WebSearch.SearchEngineSelectorButtonBounds.Contains(e.X, e.Y) Then
            If ModuleCache.WebSearch.SearchEngineSelectorMenuVisible Then
                If ModuleCache.WebSearch.SearchEngineSelectorButtonState <> 2 Then
                    ModuleCache.WebSearch.SearchEngineSelectorButtonState = 2
                    bWindowIsDirty = True
                End If
            Else
                If e.Button = Windows.Forms.MouseButtons.None Then
                    If ModuleCache.WebSearch.SearchEngineSelectorButtonState <> 1 Then
                        ModuleCache.WebSearch.SearchEngineSelectorButtonState = 1
                        bWindowIsDirty = True
                    End If
                    NewTooltipOwner = "WebSearchSearchEngineSelectorButton"
                ElseIf e.Button = Windows.Forms.MouseButtons.Left Then
                    If ModuleCache.WebSearch.SearchEngineSelectorButtonState <> 2 Then
                        ModuleCache.WebSearch.SearchEngineSelectorButtonState = 2
                        bWindowIsDirty = True
                    End If
                End If
            End If
        Else
            If ModuleCache.WebSearch.SearchEngineSelectorButtonState <> 0 Then
                ModuleCache.WebSearch.SearchEngineSelectorButtonState = 0
                bWindowIsDirty = True
            End If
        End If

        ' Web Search Go Button
        If IBSettings.Modules.WebSearch.ShowGoButton Then
            If ModuleCache.WebSearch.GoButtonBounds.Contains(e.X, e.Y) Then
                If e.Button = Windows.Forms.MouseButtons.None Then
                    If ModuleCache.WebSearch.GoButtonState <> 1 Then
                        ModuleCache.WebSearch.GoButtonState = 1
                        bWindowIsDirty = True
                    End If
                    NewTooltipOwner = "WebSearchGoButton"
                ElseIf e.Button = Windows.Forms.MouseButtons.Left Then
                    If ModuleCache.WebSearch.GoButtonState <> 2 Then
                        ModuleCache.WebSearch.GoButtonState = 2
                        bWindowIsDirty = True
                    End If
                End If
            Else
                If ModuleCache.WebSearch.GoButtonState <> 0 Then
                    ModuleCache.WebSearch.GoButtonState = 0
                    bWindowIsDirty = True
                End If
            End If
        End If
    End Sub

    Public Sub WebSearch_ProcessMouseUp(ByRef e As MouseEventArgs, ByRef bWindowIsDirty As Boolean)
        If ModuleCache.WebSearch.Enabled = False Then Exit Sub

        ' Web Search Engine Selector Button
        If ModuleCache.WebSearch.SearchEngineSelectorButtonBounds.Contains(e.X, e.Y) = True Then
            If ModuleCache.WebSearch.SearchEngineSelectorButtonState <> 1 Then
                ModuleCache.WebSearch.SearchEngineSelectorButtonState = 1
                bWindowIsDirty = True
                Menus_ShowWebSearchMenu()
            End If
        Else
            If ModuleCache.WebSearch.SearchEngineSelectorButtonState <> 0 Then
                ModuleCache.WebSearch.SearchEngineSelectorButtonState = 0
                bWindowIsDirty = True
            End If
        End If

        ' Web Search Go Button
        If IBSettings.Modules.WebSearch.ShowGoButton Then
            If ModuleCache.WebSearch.GoButtonBounds.Contains(e.X, e.Y) = True Then
                If ModuleCache.WebSearch.GoButtonState <> 1 Then
                    ModuleCache.WebSearch.GoButtonState = 1
                    bWindowIsDirty = True
                    'If frmMain.txtWebSearch.Text <> IBSettings.Modules.WebSearch.SelectedSearchProvider AndAlso frmMain.txtWebSearch.Text <> "" Then
                    'WebSearch_ProcessSearch()
                    'End If
                End If
            Else
                If ModuleCache.WebSearch.GoButtonState <> 0 Then
                    ModuleCache.WebSearch.GoButtonState = 0
                    bWindowIsDirty = True
                End If
            End If
        End If

    End Sub

    Public Sub WebSearch_ProcessMouseDown(ByRef e As MouseEventArgs, ByRef bWindowIsDirty As Boolean)
        If ModuleCache.WebSearch.Enabled = False Then Exit Sub

        ' Web Search Engine Selector Button
        If ModuleCache.WebSearch.SearchEngineSelectorButtonBounds.Contains(e.X, e.Y) = True Then
            If ModuleCache.WebSearch.SearchEngineSelectorButtonState <> 2 Then
                ModuleCache.WebSearch.SearchEngineSelectorButtonState = 2
                bWindowIsDirty = True
            End If
        Else
            If ModuleCache.WebSearch.SearchEngineSelectorButtonState <> 0 Then
                ModuleCache.WebSearch.SearchEngineSelectorButtonState = 0
                bWindowIsDirty = True
            End If
        End If

        ' Web Search Go Button
        If IBSettings.Modules.WebSearch.ShowGoButton Then
            If ModuleCache.WebSearch.GoButtonBounds.Contains(e.X, e.Y) = True Then
                If ModuleCache.WebSearch.GoButtonState <> 2 Then
                    ModuleCache.WebSearch.GoButtonState = 2
                    bWindowIsDirty = True
                End If
            Else
                If ModuleCache.WebSearch.GoButtonState <> 0 Then
                    ModuleCache.WebSearch.GoButtonState = 0
                    bWindowIsDirty = True
                End If
            End If
        End If

    End Sub

    Public Sub WebSearch_ProcessMouseLeave(ByRef bWindowIsDirty As Boolean)
        If ModuleCache.WebSearch.Enabled = False Then Exit Sub

        ' Web Search Engine Selector Button
        If ModuleCache.WebSearch.SearchEngineSelectorMenuVisible Then
            If ModuleCache.WebSearch.SearchEngineSelectorButtonState <> 2 Then
                ModuleCache.WebSearch.SearchEngineSelectorButtonState = 2
                bWindowIsDirty = True
            End If
        Else
            If ModuleCache.WebSearch.SearchEngineSelectorButtonState <> 0 Then
                ModuleCache.WebSearch.SearchEngineSelectorButtonState = 0
                bWindowIsDirty = True
            End If
        End If

        ' Web Search Go Button
        If IBSettings.Modules.WebSearch.ShowGoButton Then
            If ModuleCache.WebSearch.GoButtonState <> 0 Then
                ModuleCache.WebSearch.GoButtonState = 0
                bWindowIsDirty = True
            End If
        End If

    End Sub

    Public Function WebSearch_MeasureSelectorButtonTooltip() As Rectangle
        Dim Gr As Graphics = Graphics.FromHwnd(IntPtr.Zero)
        Dim rect As Rectangle
        Gr.TextRenderingHint = TextRenderingHint.AntiAlias
        rect = Skinning_MeasureText(Gr, "Switch Search Engine", CurrentSkinInfo.Tooltip.Text)
        Gr.Dispose()
        Return rect
    End Function

    Public Function WebSearch_MeasureGoButtonTooltip() As Rectangle
        Dim Gr As Graphics = Graphics.FromHwnd(IntPtr.Zero)
        Dim rect As Rectangle
        Gr.TextRenderingHint = TextRenderingHint.AntiAlias
        rect = Skinning_MeasureText(Gr, "Click here to search.", CurrentSkinInfo.Tooltip.Text)
        Gr.Dispose()
        Return rect
    End Function

    Public Sub WebSearch_DrawSelectorButtonTooltip(ByRef Gr As Graphics, ByRef tr As Rectangle)
        Skinning_DrawText(Gr, "Switch Search Engine", tr, CurrentSkinInfo.Tooltip.Text, StringAlignment.Near)
    End Sub

    Public Sub WebSearch_DrawGoButtonTooltip(ByRef Gr As Graphics, ByRef tr As Rectangle)
        Skinning_DrawText(Gr, "Click here to search.", tr, CurrentSkinInfo.Tooltip.Text, StringAlignment.Near)
    End Sub

End Module
