Module modMediaPlayerControl

    Private foobarApp As Object
    Private MediaMonkeyApp As Object

    Private MediaPlayer_IsRunning As Boolean
    Private MediaPlayer_IsPlaying As Boolean

    Dim albumArtWidth As Integer, albumArtHeight As Integer, textStartX As Integer

    Private Function MediaPlayerControl_IsMusicPlaying() As Boolean
        Select Case IBSettings.Modules.MediaPlayerControl.MediaPlayer

            Case "foobar2000"
                If IBSettings.Modules.MediaPlayerControl.Foobar2000InfoAccessMethod = 0 Then

                    Dim fhWnd As IntPtr
                    fhWnd = FindWindow("{DA7CD0DE-1602-45e6-89A1-C2CA151E008E}/1", vbNullString)
                    If fhWnd <> IntPtr.Zero Then                       
                        If foobarApp Is Nothing Then
                            Try
                                foobarApp = GetObject(, "Foobar2000.Application.0.7")
                                If MediaPlayer_IsRunning = False Then
                                    MediaPlayer_IsRunning = True
                                    CurrentSkinInfo.WindowIsDirty = True
                                End If
                            Catch ex As Exception
                                foobarApp = Nothing
                                MediaPlayer_IsRunning = False
                                Return False
                                Exit Function
                            End Try
                        End If
                    Else
                        foobarApp = Nothing
                        If MediaPlayer_IsRunning = True Then
                            MediaPlayer_IsRunning = False
                            CurrentSkinInfo.WindowIsDirty = True
                        End If
                    End If

                    If MediaPlayer_IsRunning Then
                        Try
                            If foobarApp.Playback.IsPlaying = True Or foobarApp.Playback.IsPaused = True Then
                                Return True
                            Else
                                Return False
                            End If
                        Catch ex As Exception
                            Return False
                        End Try
                    Else
                        Return False
                    End If

                End If

            Case "MediaMonkey"
                Dim mmhWnd As IntPtr
                mmhWnd = FindWindow("TFMainWindow", "MediaMonkey")
                If mmhWnd <> IntPtr.Zero Then
                    If MediaMonkeyApp Is Nothing Then
                        Try
                            MediaMonkeyApp = GetObject(, "SongsDB.SDBApplication")
                            MediaMonkeyApp.ShutdownAfterDisconnect = False
                            If MediaPlayer_IsRunning = False Then
                                MediaPlayer_IsRunning = True
                                CurrentSkinInfo.WindowIsDirty = True
                            End If
                        Catch ex As Exception
                            Debug.Print(ex.ToString)
                            MediaPlayer_IsRunning = False
                            Return False
                            Exit Function
                        End Try
                    End If
                Else
                    MediaMonkeyApp = Nothing
                    If MediaPlayer_IsRunning = True Then
                        MediaPlayer_IsRunning = False
                        CurrentSkinInfo.WindowIsDirty = True
                    End If
                    Return False
                    Exit Function
                End If

                If MediaPlayer_IsRunning Then
                    Try
                        If MediaMonkeyApp.Player.isPlaying = True Or MediaMonkeyApp.Player.isPaused = True Then
                            Return True
                        Else
                            Return False
                        End If
                    Catch ex As Exception
                        Return False
                    End Try
                Else
                    Return False
                End If

            Case "Winamp"

            Case "Windows Media Player"

        End Select
    End Function

    Public Sub MediaPlayerControl_Update()
        MediaPlayer_IsPlaying = MediaPlayerControl_IsMusicPlaying()

        Dim bRefreshTooltip As Boolean = False

        If MediaPlayer_IsRunning AndAlso MediaPlayer_IsPlaying Then

            MediaPlayerControl_GetSongTimeInfo()
            MediaPlayerControl_GetSongTitle()
            If MediaPlayerControl_GetAlbumArt() = True Then bRefreshTooltip = True
            If MediaPlayerControl_GetRating() = True Then bRefreshTooltip = True
            If MediaPlayerControl_GetTooltipText() = True Then bRefreshTooltip = True

        Else

            ModuleCache.MediaPlayerControl.TimeDisplayText = vbNullString
            ModuleCache.MediaPlayerControl.TitleDisplayText = vbNullString
            ModuleCache.MediaPlayerControl.AlbumArtPath = vbNullString
            ModuleCache.MediaPlayerControl.AlbumArtImage = Nothing
            ModuleCache.MediaPlayerControl.TrackRating = vbNullString
            ModuleCache.MediaPlayerControl.TrackRatingImage = Nothing
            ModuleCache.MediaPlayerControl.TooltipText = vbNullString

        End If

        If CurrentTooltipOwner = "MediaPlayerControlNowPlaying" AndAlso bRefreshTooltip Then
            CurrentSkinInfo.TooltipWindowIsDirty = True
        End If

    End Sub

    Private Sub MediaPlayerControl_GetSongTimeInfo()

        If IBSettings.Modules.MediaPlayerControl.ShowSongTimeInfo Then

            Select Case IBSettings.Modules.MediaPlayerControl.MediaPlayer

                Case "foobar2000"

                    If IBSettings.Modules.MediaPlayerControl.Foobar2000InfoAccessMethod = 0 Then

                        Dim sText As String = vbNullString
                        If IBSettings.Modules.MediaPlayerControl.SongTimeInfoMode = 0 Then
                            ' Elapsed
                            Try
                                sText = foobarApp.Playback.FormatTitleEx("%playback_time%", 3)
                            Catch ex As Exception
                                sText = vbNullString
                            End Try
                        Else
                            Try
                                sText = foobarApp.Playback.FormatTitleEx("-%playback_time_remaining%", 3)
                            Catch ex As Exception
                                sText = vbNullString
                            End Try
                            ' Remaining
                        End If
                        If sText <> vbNullString AndAlso IBSettings.Modules.MediaPlayerControl.ShowSongDuration Then
                            Try
                                sText = sText & " / " & foobarApp.Playback.FormatTitleEx("%length%", 3)
                            Catch ex As Exception
                                sText = vbNullString
                            End Try
                        End If

                        If ModuleCache.MediaPlayerControl.TimeDisplayText <> sText Then
                            ModuleCache.MediaPlayerControl.TimeDisplayText = sText
                            CurrentSkinInfo.WindowIsDirty = True
                        End If

                    End If

                Case "MediaMonkey"

                    Dim sText As String = vbNullString
                    If IBSettings.Modules.MediaPlayerControl.SongTimeInfoMode = 0 Then
                        ' Elapsed
                        sText = FormatTime(MediaMonkeyApp.Player.PlaybackTime / 1000)
                    Else
                        ' Remaining
                        sText = "-" & FormatTime((MediaMonkeyApp.Player.CurrentSong.SongLength - MediaMonkeyApp.Player.PlaybackTime) / 1000)
                    End If
                    If sText <> vbNullString AndAlso IBSettings.Modules.MediaPlayerControl.ShowSongDuration Then
                        sText = sText & " / " & FormatTime(MediaMonkeyApp.Player.CurrentSongLength / 1000)
                    End If

                    If ModuleCache.MediaPlayerControl.TimeDisplayText <> sText Then
                        ModuleCache.MediaPlayerControl.TimeDisplayText = sText
                        CurrentSkinInfo.WindowIsDirty = True
                    End If

            End Select

        Else

            If ModuleCache.MediaPlayerControl.TimeDisplayText <> vbNullString Then
                ModuleCache.MediaPlayerControl.TimeDisplayText = vbNullString
                CurrentSkinInfo.WindowIsDirty = True
            End If

        End If

    End Sub

    Private Sub MediaPlayerControl_GetSongTitle()

        If IBSettings.Modules.MediaPlayerControl.ShowSongTagInfo Then
            Select Case IBSettings.Modules.MediaPlayerControl.MediaPlayer

                Case "foobar2000"

                    If IBSettings.Modules.MediaPlayerControl.Foobar2000InfoAccessMethod = 0 Then

                        Dim sText As String = vbNullString
                        sText = foobarApp.Playback.FormatTitleEx(IBSettings.Modules.MediaPlayerControl.SongTagFormattingString, 3)
                        If ModuleCache.MediaPlayerControl.TitleDisplayText <> sText Then
                            ModuleCache.MediaPlayerControl.TitleDisplayText = sText
                            CurrentSkinInfo.WindowIsDirty = True
                        End If

                    End If

                Case "MediaMonkey"

                    Dim sText As String = vbNullString
                    sText = FormatTitleMM(IBSettings.Modules.MediaPlayerControl.SongTagFormattingString)
                    If ModuleCache.MediaPlayerControl.TitleDisplayText <> sText Then
                        ModuleCache.MediaPlayerControl.TitleDisplayText = sText
                        CurrentSkinInfo.WindowIsDirty = True
                    End If

            End Select

        Else

            ModuleCache.MediaPlayerControl.TitleDisplayText = vbNullString

        End If

    End Sub

    Private Function MediaPlayerControl_GetAlbumArt() As Boolean

        If IBSettings.Modules.MediaPlayerControl.TooltipShowAlbumArt = True Then

            Dim sAlbumArtPath As String = vbNullString
            Select Case IBSettings.Modules.MediaPlayerControl.MediaPlayer

                Case "foobar2000"
                    If IBSettings.Modules.MediaPlayerControl.Foobar2000InfoAccessMethod = 0 Then
                        Try
                            sAlbumArtPath = foobarApp.Playback.FormatTitleEx(IBSettings.Modules.MediaPlayerControl.Foobar2000AlbumArtFormattingString, 3)
                        Catch ex As Exception
                            sAlbumArtPath = vbNullString
                        End Try

                    End If

                Case "MediaMonkey"
                    Try
                        If MediaMonkeyApp.Player.CurrentSong.AlbumArt.Count > 0 Then
                            If MediaMonkeyApp.Player.CurrentSong.AlbumArt.Item(0).ItemStorage = 0 Then
                                sAlbumArtPath = GetAlbumArtMM()
                            Else
                                sAlbumArtPath = MediaMonkeyApp.Player.CurrentSong.AlbumArt.Item(0).PicturePath
                            End If
                        Else
                            sAlbumArtPath = vbNullString
                        End If
                    Catch ex As Exception
                        sAlbumArtPath = vbNullString
                    End Try

            End Select

            If ModuleCache.MediaPlayerControl.AlbumArtPath <> sAlbumArtPath Then
                ModuleCache.MediaPlayerControl.AlbumArtPath = sAlbumArtPath
                If sAlbumArtPath <> vbNullString Then
                    If File.Exists(sAlbumArtPath) Then
                        Try
                            ModuleCache.MediaPlayerControl.AlbumArtImage = LoadImageFromFile(sAlbumArtPath)
                        Catch ex As Exception
                            ModuleCache.MediaPlayerControl.AlbumArtImage = Nothing
                        End Try
                    Else
                        ModuleCache.MediaPlayerControl.AlbumArtImage = Nothing
                    End If
                Else
                    ModuleCache.MediaPlayerControl.AlbumArtImage = Nothing
                End If
                Return True
            Else
                Return False
            End If

        Else
            Return False
        End If

    End Function

    Private Function MediaPlayerControl_GetRating() As Boolean

        If IBSettings.Modules.MediaPlayerControl.TooltipShowTrackRating = True Then

            ' Get Rating
            Dim iRating As Single
            Select Case IBSettings.Modules.MediaPlayerControl.MediaPlayer

                Case "foobar2000"
                    If IBSettings.Modules.MediaPlayerControl.Foobar2000InfoAccessMethod = 0 Then
                        Try
                            iRating = CSng(foobarApp.Playback.FormatTitleEx("%rating%", 3))
                        Catch
                            iRating = 0
                        End Try
                    End If

                Case "MediaMonkey"
                    iRating = MediaMonkeyApp.Player.CurrentSong.Rating / 20

            End Select

            If ModuleCache.MediaPlayerControl.TrackRating <> iRating Then
                ModuleCache.MediaPlayerControl.TrackRating = iRating
                If iRating >= 0 AndAlso iRating < 0.5 Then
                    ModuleCache.MediaPlayerControl.TrackRatingImage = CurrentIconTheme.MediaPlayerTrackRating0
                ElseIf iRating >= 0.5 AndAlso iRating < 1 Then
                    ModuleCache.MediaPlayerControl.TrackRatingImage = CurrentIconTheme.MediaPlayerTrackRating0pt5
                ElseIf iRating >= 1 AndAlso iRating < 1.5 Then
                    ModuleCache.MediaPlayerControl.TrackRatingImage = CurrentIconTheme.MediaPlayerTrackRating1
                ElseIf iRating >= 1.5 AndAlso iRating < 2 Then
                    ModuleCache.MediaPlayerControl.TrackRatingImage = CurrentIconTheme.MediaPlayerTrackRating1pt5
                ElseIf iRating >= 2 AndAlso iRating < 2.5 Then
                    ModuleCache.MediaPlayerControl.TrackRatingImage = CurrentIconTheme.MediaPlayerTrackRating2
                ElseIf iRating >= 2.5 AndAlso iRating < 3 Then
                    ModuleCache.MediaPlayerControl.TrackRatingImage = CurrentIconTheme.MediaPlayerTrackRating2pt5
                ElseIf iRating >= 3 AndAlso iRating < 3.5 Then
                    ModuleCache.MediaPlayerControl.TrackRatingImage = CurrentIconTheme.MediaPlayerTrackRating3
                ElseIf iRating >= 3.5 AndAlso iRating < 4 Then
                    ModuleCache.MediaPlayerControl.TrackRatingImage = CurrentIconTheme.MediaPlayerTrackRating3pt5
                ElseIf iRating >= 4 AndAlso iRating < 4.5 Then
                    ModuleCache.MediaPlayerControl.TrackRatingImage = CurrentIconTheme.MediaPlayerTrackRating4
                ElseIf iRating >= 4.5 AndAlso iRating < 5 Then
                    ModuleCache.MediaPlayerControl.TrackRatingImage = CurrentIconTheme.MediaPlayerTrackRating4pt5
                ElseIf iRating = 5 Then
                    ModuleCache.MediaPlayerControl.TrackRatingImage = CurrentIconTheme.MediaPlayerTrackRating5
                Else
                    ModuleCache.MediaPlayerControl.TrackRatingImage = CurrentIconTheme.MediaPlayerTrackRating0
                End If
                Return True
            Else
                Return False
            End If

        Else
            Return False
        End If

    End Function

    Private Function MediaPlayerControl_GetTooltipText() As Boolean

        Dim sTooltipText As String = vbNullString
        Select Case IBSettings.Modules.MediaPlayerControl.MediaPlayer

            Case "foobar2000"
                If IBSettings.Modules.MediaPlayerControl.Foobar2000InfoAccessMethod = 0 Then
                    Try
                        ' Hack to preserve line feeds
                        Dim sTemp As String
                        sTemp = IBSettings.Modules.MediaPlayerControl.TooltipTextFormatString.Replace(vbCrLf, "<vbCrLf>")
                        sTemp = foobarApp.Playback.FormatTitleEx(sTemp, 3)
                        sTemp = sTemp.Replace("<vbCrLf>", vbCrLf)
                        sTooltipText = sTemp
                    Catch ex As Exception
                        sTooltipText = vbNullString
                    End Try

                End If

            Case "MediaMonkey"
                sTooltipText = FormatTitleMM(IBSettings.Modules.MediaPlayerControl.TooltipTextFormatString)

        End Select

        If ModuleCache.MediaPlayerControl.TooltipText <> sTooltipText Then
            ModuleCache.MediaPlayerControl.TooltipText = sTooltipText
            Return True
        Else
            Return False
        End If

    End Function

    Public Sub MediaPlayerControl_DisposeResources()
        foobarApp = Nothing
        MediaMonkeyApp = Nothing
    End Sub

#Region "InfoBar Drawing Routines"

    Public Sub MediaPlayerControl_Draw(ByRef Gr As Graphics, ByRef nextX As Integer)
        Dim btnIcon As Image = Nothing, btnText As String = vbNullString

        ' Draw Open Button
        Select Case IBSettings.Modules.MediaPlayerControl.MediaPlayer
            Case "foobar2000"
                If IBSettings.Modules.MediaPlayerControl.PlaybackButtonDisplay = 0 Or IBSettings.Modules.MediaPlayerControl.PlaybackButtonDisplay = 2 Then
                    btnIcon = CurrentIconTheme.MediaPlayerControlOpenFoobar2000
                End If
                If IBSettings.Modules.MediaPlayerControl.PlaybackButtonDisplay = 1 Or IBSettings.Modules.MediaPlayerControl.PlaybackButtonDisplay = 2 Then
                    btnText = "Open/Show Foobar2000"
                End If
            Case "MediaMonkey"
                If IBSettings.Modules.MediaPlayerControl.PlaybackButtonDisplay = 0 Or IBSettings.Modules.MediaPlayerControl.PlaybackButtonDisplay = 2 Then
                    btnIcon = CurrentIconTheme.MediaPlayerControlOpenMediaMonkey
                End If
                If IBSettings.Modules.MediaPlayerControl.PlaybackButtonDisplay = 1 Or IBSettings.Modules.MediaPlayerControl.PlaybackButtonDisplay = 2 Then
                    btnText = "Open/Show MediaMonkey"
                End If
            Case "Winamp"
                If IBSettings.Modules.MediaPlayerControl.PlaybackButtonDisplay = 0 Or IBSettings.Modules.MediaPlayerControl.PlaybackButtonDisplay = 2 Then
                    btnIcon = CurrentIconTheme.MediaPlayerControlOpenWinamp
                End If
                If IBSettings.Modules.MediaPlayerControl.PlaybackButtonDisplay = 1 Or IBSettings.Modules.MediaPlayerControl.PlaybackButtonDisplay = 2 Then
                    btnText = "Open/Show Winamp"
                End If
            Case "Windows Media Player"
                If IBSettings.Modules.MediaPlayerControl.PlaybackButtonDisplay = 0 Or IBSettings.Modules.MediaPlayerControl.PlaybackButtonDisplay = 2 Then
                    btnIcon = CurrentIconTheme.MediaPlayerControlOpenWMP
                End If
                If IBSettings.Modules.MediaPlayerControl.PlaybackButtonDisplay = 1 Or IBSettings.Modules.MediaPlayerControl.PlaybackButtonDisplay = 2 Then
                    btnText = "Open/Show Windows Media Player"
                End If
        End Select
        Dim btnR As New Rectangle(nextX, CurrentSkinInfo.Background.ContentMargins.Top, 1, CurrentSkinInfo.Height - (CurrentSkinInfo.Background.ContentMargins.Top + CurrentSkinInfo.Background.ContentMargins.Bottom))
        Dim btnFR As Rectangle = Skinning_DrawButton(Gr, btnR, btnIcon, btnText, ModuleCache.MediaPlayerControl.OpenButtonState, False)
        ModuleCache.MediaPlayerControl.OpenButtonBounds = btnFR
        nextX = nextX + btnFR.Width

        If MediaPlayer_IsRunning Then

            ' Draw Previous Button
            btnIcon = Nothing
            btnText = vbNullString
            If IBSettings.Modules.MediaPlayerControl.PlaybackButtonDisplay = 0 Or IBSettings.Modules.MediaPlayerControl.PlaybackButtonDisplay = 2 Then
                btnIcon = CurrentIconTheme.MediaPlayerControlPrev
            End If
            If IBSettings.Modules.MediaPlayerControl.PlaybackButtonDisplay = 1 Or IBSettings.Modules.MediaPlayerControl.PlaybackButtonDisplay = 2 Then
                btnText = "Previous"
            End If
            btnR = New Rectangle(nextX, CurrentSkinInfo.Background.ContentMargins.Top, 1, CurrentSkinInfo.Height - (CurrentSkinInfo.Background.ContentMargins.Top + CurrentSkinInfo.Background.ContentMargins.Bottom))
            btnFR = Skinning_DrawButton(Gr, btnR, btnIcon, btnText, ModuleCache.MediaPlayerControl.PrevButtonState, False)
            ModuleCache.MediaPlayerControl.PrevButtonBounds = btnFR
            nextX = nextX + btnFR.Width

            ' Draw Play Button
            btnIcon = Nothing
            btnText = vbNullString
            If IBSettings.Modules.MediaPlayerControl.PlaybackButtonDisplay = 0 Or IBSettings.Modules.MediaPlayerControl.PlaybackButtonDisplay = 2 Then
                btnIcon = CurrentIconTheme.MediaPlayerControlPlay
            End If
            If IBSettings.Modules.MediaPlayerControl.PlaybackButtonDisplay = 1 Or IBSettings.Modules.MediaPlayerControl.PlaybackButtonDisplay = 2 Then
                btnText = "Play"
            End If
            btnR = New Rectangle(nextX, CurrentSkinInfo.Background.ContentMargins.Top, 1, CurrentSkinInfo.Height - (CurrentSkinInfo.Background.ContentMargins.Top + CurrentSkinInfo.Background.ContentMargins.Bottom))
            btnFR = Skinning_DrawButton(Gr, btnR, btnIcon, btnText, ModuleCache.MediaPlayerControl.PlayButtonState, False)
            ModuleCache.MediaPlayerControl.PlayButtonBounds = btnFR
            nextX = nextX + btnFR.Width

            ' Draw Pause Button
            btnIcon = Nothing
            btnText = vbNullString
            If IBSettings.Modules.MediaPlayerControl.PlaybackButtonDisplay = 0 Or IBSettings.Modules.MediaPlayerControl.PlaybackButtonDisplay = 2 Then
                btnIcon = CurrentIconTheme.MediaPlayerControlPause
            End If
            If IBSettings.Modules.MediaPlayerControl.PlaybackButtonDisplay = 1 Or IBSettings.Modules.MediaPlayerControl.PlaybackButtonDisplay = 2 Then
                btnText = "Pause"
            End If
            btnR = New Rectangle(nextX, CurrentSkinInfo.Background.ContentMargins.Top, 1, CurrentSkinInfo.Height - (CurrentSkinInfo.Background.ContentMargins.Top + CurrentSkinInfo.Background.ContentMargins.Bottom))
            btnFR = Skinning_DrawButton(Gr, btnR, btnIcon, btnText, ModuleCache.MediaPlayerControl.PauseButtonState, False)
            ModuleCache.MediaPlayerControl.PauseButtonBounds = btnFR
            nextX = nextX + btnFR.Width

            ' Draw Stop Button
            btnIcon = Nothing
            btnText = vbNullString
            If IBSettings.Modules.MediaPlayerControl.PlaybackButtonDisplay = 0 Or IBSettings.Modules.MediaPlayerControl.PlaybackButtonDisplay = 2 Then
                btnIcon = CurrentIconTheme.MediaPlayerControlStop
            End If
            If IBSettings.Modules.MediaPlayerControl.PlaybackButtonDisplay = 1 Or IBSettings.Modules.MediaPlayerControl.PlaybackButtonDisplay = 2 Then
                btnText = "Stop"
            End If
            btnR = New Rectangle(nextX, CurrentSkinInfo.Background.ContentMargins.Top, 1, CurrentSkinInfo.Height - (CurrentSkinInfo.Background.ContentMargins.Top + CurrentSkinInfo.Background.ContentMargins.Bottom))
            btnFR = Skinning_DrawButton(Gr, btnR, btnIcon, btnText, ModuleCache.MediaPlayerControl.StopButtonState, False)
            ModuleCache.MediaPlayerControl.StopButtonBounds = btnFR
            nextX = nextX + btnFR.Width

            ' Draw Next Button
            btnIcon = Nothing
            btnText = vbNullString
            If IBSettings.Modules.MediaPlayerControl.PlaybackButtonDisplay = 0 Or IBSettings.Modules.MediaPlayerControl.PlaybackButtonDisplay = 2 Then
                btnIcon = CurrentIconTheme.MediaPlayerControlNext
            End If
            If IBSettings.Modules.MediaPlayerControl.PlaybackButtonDisplay = 1 Or IBSettings.Modules.MediaPlayerControl.PlaybackButtonDisplay = 2 Then
                btnText = "Next"
            End If
            btnR = New Rectangle(nextX, CurrentSkinInfo.Background.ContentMargins.Top, 1, CurrentSkinInfo.Height - (CurrentSkinInfo.Background.ContentMargins.Top + CurrentSkinInfo.Background.ContentMargins.Bottom))
            btnFR = Skinning_DrawButton(Gr, btnR, btnIcon, btnText, ModuleCache.MediaPlayerControl.NextButtonState, False)
            ModuleCache.MediaPlayerControl.NextButtonBounds = btnFR
            nextX = nextX + btnFR.Width

        Else

            ModuleCache.MediaPlayerControl.PrevButtonBounds = Nothing
            ModuleCache.MediaPlayerControl.PlayButtonBounds = Nothing
            ModuleCache.MediaPlayerControl.PauseButtonBounds = Nothing
            ModuleCache.MediaPlayerControl.StopButtonBounds = Nothing
            ModuleCache.MediaPlayerControl.NextButtonBounds = Nothing

        End If

        ' Draw Time Textbox
        If IBSettings.Modules.MediaPlayerControl.ShowSongTimeInfo AndAlso MediaPlayer_IsPlaying Then
            nextX = nextX + 2

            Dim trs As Rectangle
            trs.X = nextX
            trs.Y = CurrentSkinInfo.Background.ContentMargins.Top
            trs.Width = IBSettings.Modules.MediaPlayerControl.SongTimeWidth
            trs.Height = CurrentSkinInfo.Height - (CurrentSkinInfo.Background.ContentMargins.Top + CurrentSkinInfo.Background.ContentMargins.Bottom)
            Skinning_DrawTextBox(Gr, trs)
            ModuleCache.MediaPlayerControl.TimeDisplayBounds = trs
            nextX = nextX + trs.Width + 2
            trs.X = trs.X + CurrentSkinInfo.TextBox.ContentMargins.Left
            trs.Y = trs.Y + CurrentSkinInfo.TextBox.ContentMargins.Top
            trs.Width = trs.Width - (CurrentSkinInfo.TextBox.ContentMargins.Left + CurrentSkinInfo.TextBox.ContentMargins.Right)
            trs.Height = trs.Height - (CurrentSkinInfo.TextBox.ContentMargins.Top + CurrentSkinInfo.TextBox.ContentMargins.Bottom)
            Skinning_DrawText(Gr, ModuleCache.MediaPlayerControl.TimeDisplayText, trs, CurrentSkinInfo.TextBox.Text, StringAlignment.Center)
        Else
            ModuleCache.MediaPlayerControl.TimeDisplayBounds = Nothing
        End If

        ' Draw Title Textbox
        If IBSettings.Modules.MediaPlayerControl.ShowSongTagInfo AndAlso MediaPlayer_IsPlaying Then
            nextX = nextX + 2

            Dim trs As Rectangle
            trs.X = nextX
            trs.Y = CurrentSkinInfo.Background.ContentMargins.Top
            trs.Width = IBSettings.Modules.MediaPlayerControl.SongTagWidth
            trs.Height = CurrentSkinInfo.Height - (CurrentSkinInfo.Background.ContentMargins.Top + CurrentSkinInfo.Background.ContentMargins.Bottom)
            Skinning_DrawTextBox(Gr, trs)
            ModuleCache.MediaPlayerControl.TitleDisplayBounds = trs
            nextX = nextX + trs.Width + 2
            trs.X = trs.X + CurrentSkinInfo.TextBox.ContentMargins.Left
            trs.Y = trs.Y + CurrentSkinInfo.TextBox.ContentMargins.Top
            trs.Width = trs.Width - (CurrentSkinInfo.TextBox.ContentMargins.Left + CurrentSkinInfo.TextBox.ContentMargins.Right)
            trs.Height = trs.Height - (CurrentSkinInfo.TextBox.ContentMargins.Top + CurrentSkinInfo.TextBox.ContentMargins.Bottom)
            Skinning_DrawText(Gr, ModuleCache.MediaPlayerControl.TitleDisplayText, trs, CurrentSkinInfo.TextBox.Text, StringAlignment.Near)
        Else
            ModuleCache.MediaPlayerControl.TitleDisplayBounds = Nothing
        End If

    End Sub

#End Region

#Region "Mouse Event Subs"

    Public Sub MediaPlayerControl_ProcessMouseMove(ByRef e As MouseEventArgs, ByRef bWindowIsDirty As Boolean)

        ' Open Button
        If ModuleCache.MediaPlayerControl.OpenButtonBounds.Contains(e.X, e.Y) Then
            If e.Button = MouseButtons.None Then
                If ModuleCache.MediaPlayerControl.OpenButtonState <> 1 Then
                    ModuleCache.MediaPlayerControl.OpenButtonState = 1
                    bWindowIsDirty = True
                End If
                If MediaPlayer_IsPlaying AndAlso IBSettings.Modules.MediaPlayerControl.ShowNowPlayingTooltip Then
                    NewTooltipOwner = "MediaPlayerControlNowPlaying"
                Else
                    NewTooltipOwner = "MediaPlayerControlOpenButton"
                End If
            ElseIf e.Button = MouseButtons.Left Then
                If ModuleCache.MediaPlayerControl.OpenButtonState <> 2 Then
                    ModuleCache.MediaPlayerControl.OpenButtonState = 2
                    bWindowIsDirty = True
                End If
            End If
        Else
            If ModuleCache.MediaPlayerControl.OpenButtonState <> 0 Then
                ModuleCache.MediaPlayerControl.OpenButtonState = 0
                bWindowIsDirty = True
            End If
        End If

        ' Previous Button
        If ModuleCache.MediaPlayerControl.PrevButtonBounds.Contains(e.X, e.Y) Then
            If e.Button = MouseButtons.None Then
                If ModuleCache.MediaPlayerControl.PrevButtonState <> 1 Then
                    ModuleCache.MediaPlayerControl.PrevButtonState = 1
                    bWindowIsDirty = True
                End If
                If MediaPlayer_IsPlaying AndAlso IBSettings.Modules.MediaPlayerControl.ShowNowPlayingTooltip Then
                    NewTooltipOwner = "MediaPlayerControlNowPlaying"
                Else
                    NewTooltipOwner = "MediaPlayerControlPrevButton"
                End If
            ElseIf e.Button = MouseButtons.Left Then
                If ModuleCache.MediaPlayerControl.PrevButtonState <> 2 Then
                    ModuleCache.MediaPlayerControl.PrevButtonState = 2
                    bWindowIsDirty = True
                End If
            End If
        Else
            If ModuleCache.MediaPlayerControl.PrevButtonState <> 0 Then
                ModuleCache.MediaPlayerControl.PrevButtonState = 0
                bWindowIsDirty = True
            End If
        End If

        ' Play Button
        If ModuleCache.MediaPlayerControl.PlayButtonBounds.Contains(e.X, e.Y) Then
            If e.Button = MouseButtons.None Then
                If ModuleCache.MediaPlayerControl.PlayButtonState <> 1 Then
                    ModuleCache.MediaPlayerControl.PlayButtonState = 1
                    bWindowIsDirty = True
                End If
                If MediaPlayer_IsPlaying AndAlso IBSettings.Modules.MediaPlayerControl.ShowNowPlayingTooltip Then
                    NewTooltipOwner = "MediaPlayerControlNowPlaying"
                Else
                    NewTooltipOwner = "MediaPlayerControlPlayButton"
                End If
            ElseIf e.Button = MouseButtons.Left Then
                If ModuleCache.MediaPlayerControl.PlayButtonState <> 2 Then
                    ModuleCache.MediaPlayerControl.PlayButtonState = 2
                    bWindowIsDirty = True
                End If
            End If
        Else
            If ModuleCache.MediaPlayerControl.PlayButtonState <> 0 Then
                ModuleCache.MediaPlayerControl.PlayButtonState = 0
                bWindowIsDirty = True
            End If
        End If

        ' Pause Button
        If ModuleCache.MediaPlayerControl.PauseButtonBounds.Contains(e.X, e.Y) Then
            If e.Button = MouseButtons.None Then
                If ModuleCache.MediaPlayerControl.PauseButtonState <> 1 Then
                    ModuleCache.MediaPlayerControl.PauseButtonState = 1
                    bWindowIsDirty = True
                End If
                If MediaPlayer_IsPlaying AndAlso IBSettings.Modules.MediaPlayerControl.ShowNowPlayingTooltip Then
                    NewTooltipOwner = "MediaPlayerControlNowPlaying"
                Else
                    NewTooltipOwner = "MediaPlayerControlPauseButton"
                End If
            ElseIf e.Button = MouseButtons.Left Then
                If ModuleCache.MediaPlayerControl.PauseButtonState <> 2 Then
                    ModuleCache.MediaPlayerControl.PauseButtonState = 2
                    bWindowIsDirty = True
                End If
            End If
        Else
            If ModuleCache.MediaPlayerControl.PauseButtonState <> 0 Then
                ModuleCache.MediaPlayerControl.PauseButtonState = 0
                bWindowIsDirty = True
            End If
        End If

        ' Stop Button
        If ModuleCache.MediaPlayerControl.StopButtonBounds.Contains(e.X, e.Y) Then
            If e.Button = MouseButtons.None Then
                If ModuleCache.MediaPlayerControl.StopButtonState <> 1 Then
                    ModuleCache.MediaPlayerControl.StopButtonState = 1
                    bWindowIsDirty = True
                End If
                If MediaPlayer_IsPlaying AndAlso IBSettings.Modules.MediaPlayerControl.ShowNowPlayingTooltip Then
                    NewTooltipOwner = "MediaPlayerControlNowPlaying"
                Else
                    NewTooltipOwner = "MediaPlayerControlStopButton"
                End If
            ElseIf e.Button = MouseButtons.Left Then
                If ModuleCache.MediaPlayerControl.StopButtonState <> 2 Then
                    ModuleCache.MediaPlayerControl.StopButtonState = 2
                    bWindowIsDirty = True
                End If
            End If
        Else
            If ModuleCache.MediaPlayerControl.StopButtonState <> 0 Then
                ModuleCache.MediaPlayerControl.StopButtonState = 0
                bWindowIsDirty = True
            End If
        End If

        ' Next Button
        If ModuleCache.MediaPlayerControl.NextButtonBounds.Contains(e.X, e.Y) Then
            If e.Button = MouseButtons.None Then
                If ModuleCache.MediaPlayerControl.NextButtonState <> 1 Then
                    ModuleCache.MediaPlayerControl.NextButtonState = 1
                    bWindowIsDirty = True
                End If
                If MediaPlayer_IsPlaying AndAlso IBSettings.Modules.MediaPlayerControl.ShowNowPlayingTooltip Then
                    NewTooltipOwner = "MediaPlayerControlNowPlaying"
                Else
                    NewTooltipOwner = "MediaPlayerControlNextButton"
                End If
            ElseIf e.Button = MouseButtons.Left Then
                If ModuleCache.MediaPlayerControl.NextButtonState <> 2 Then
                    ModuleCache.MediaPlayerControl.NextButtonState = 2
                    bWindowIsDirty = True
                End If
            End If
        Else
            If ModuleCache.MediaPlayerControl.NextButtonState <> 0 Then
                ModuleCache.MediaPlayerControl.NextButtonState = 0
                bWindowIsDirty = True
            End If
        End If

        ' Time Display Textbox
        If ModuleCache.MediaPlayerControl.TimeDisplayBounds.Contains(e.X, e.Y) Then
            NewTooltipOwner = "MediaPlayerControlNowPlaying"
        End If

        ' Title Display Textbox
        If ModuleCache.MediaPlayerControl.TitleDisplayBounds.Contains(e.X, e.Y) Then
            NewTooltipOwner = "MediaPlayerControlNowPlaying"
        End If

    End Sub

    Public Sub MediaPlayerControl_ProcessMouseUp(ByRef e As MouseEventArgs, ByRef bWindowIsDirty As Boolean)

        ' Open Button
        If ModuleCache.MediaPlayerControl.OpenButtonBounds.Contains(e.X, e.Y) = True Then
            If ModuleCache.MediaPlayerControl.OpenButtonState <> 1 Then
                ModuleCache.MediaPlayerControl.OpenButtonState = 1
                bWindowIsDirty = True
                MediaPlayerControl_OpenButtonPressed()
            End If
        Else
            If ModuleCache.MediaPlayerControl.OpenButtonState <> 0 Then
                ModuleCache.MediaPlayerControl.OpenButtonState = 0
                bWindowIsDirty = True
            End If
        End If

        ' Prev Button
        If ModuleCache.MediaPlayerControl.PrevButtonBounds.Contains(e.X, e.Y) = True Then
            If ModuleCache.MediaPlayerControl.PrevButtonState <> 1 Then
                ModuleCache.MediaPlayerControl.PrevButtonState = 1
                bWindowIsDirty = True
                MediaPlayerControl_PrevButtonPressed()
            End If
        Else
            If ModuleCache.MediaPlayerControl.PrevButtonState <> 0 Then
                ModuleCache.MediaPlayerControl.PrevButtonState = 0
                bWindowIsDirty = True
            End If
        End If

        ' Play Button
        If ModuleCache.MediaPlayerControl.PlayButtonBounds.Contains(e.X, e.Y) = True Then
            If ModuleCache.MediaPlayerControl.PlayButtonState <> 1 Then
                ModuleCache.MediaPlayerControl.PlayButtonState = 1
                bWindowIsDirty = True
                MediaPlayerControl_PlayButtonPressed()
            End If
        Else
            If ModuleCache.MediaPlayerControl.PlayButtonState <> 0 Then
                ModuleCache.MediaPlayerControl.PlayButtonState = 0
                bWindowIsDirty = True
            End If
        End If

        ' Pause Button
        If ModuleCache.MediaPlayerControl.PauseButtonBounds.Contains(e.X, e.Y) = True Then
            If ModuleCache.MediaPlayerControl.PauseButtonState <> 1 Then
                ModuleCache.MediaPlayerControl.PauseButtonState = 1
                bWindowIsDirty = True
                MediaPlayerControl_PauseButtonPressed()
            End If
        Else
            If ModuleCache.MediaPlayerControl.PauseButtonState <> 0 Then
                ModuleCache.MediaPlayerControl.PauseButtonState = 0
                bWindowIsDirty = True
            End If
        End If

        ' Stop Button
        If ModuleCache.MediaPlayerControl.StopButtonBounds.Contains(e.X, e.Y) = True Then
            If ModuleCache.MediaPlayerControl.StopButtonState <> 1 Then
                ModuleCache.MediaPlayerControl.StopButtonState = 1
                bWindowIsDirty = True
                MediaPlayerControl_StopButtonPressed()
            End If
        Else
            If ModuleCache.MediaPlayerControl.StopButtonState <> 0 Then
                ModuleCache.MediaPlayerControl.StopButtonState = 0
                bWindowIsDirty = True
            End If
        End If

        ' Next Button
        If ModuleCache.MediaPlayerControl.NextButtonBounds.Contains(e.X, e.Y) = True Then
            If ModuleCache.MediaPlayerControl.NextButtonState <> 1 Then
                ModuleCache.MediaPlayerControl.NextButtonState = 1
                bWindowIsDirty = True
                MediaPlayerControl_NextButtonPressed()
            End If
        Else
            If ModuleCache.MediaPlayerControl.NextButtonState <> 0 Then
                ModuleCache.MediaPlayerControl.NextButtonState = 0
                bWindowIsDirty = True
            End If
        End If

    End Sub

    Public Sub MediaPlayerControl_ProcessMouseDown(ByRef e As MouseEventArgs, ByRef bWindowIsDirty As Boolean)

        ' Open Button
        If ModuleCache.MediaPlayerControl.OpenButtonBounds.Contains(e.X, e.Y) = True Then
            If ModuleCache.MediaPlayerControl.OpenButtonState <> 2 Then
                ModuleCache.MediaPlayerControl.OpenButtonState = 2
                bWindowIsDirty = True
            End If
        Else
            If ModuleCache.MediaPlayerControl.OpenButtonState <> 0 Then
                ModuleCache.MediaPlayerControl.OpenButtonState = 0
                bWindowIsDirty = True
            End If
        End If

        ' Prev Button
        If ModuleCache.MediaPlayerControl.PrevButtonBounds.Contains(e.X, e.Y) = True Then
            If ModuleCache.MediaPlayerControl.PrevButtonState <> 2 Then
                ModuleCache.MediaPlayerControl.PrevButtonState = 2
                bWindowIsDirty = True
            End If
        Else
            If ModuleCache.MediaPlayerControl.PrevButtonState <> 0 Then
                ModuleCache.MediaPlayerControl.PrevButtonState = 0
                bWindowIsDirty = True
            End If
        End If

        ' Play Button
        If ModuleCache.MediaPlayerControl.PlayButtonBounds.Contains(e.X, e.Y) = True Then
            If ModuleCache.MediaPlayerControl.PlayButtonState <> 2 Then
                ModuleCache.MediaPlayerControl.PlayButtonState = 2
                bWindowIsDirty = True
            End If
        Else
            If ModuleCache.MediaPlayerControl.PlayButtonState <> 0 Then
                ModuleCache.MediaPlayerControl.PlayButtonState = 0
                bWindowIsDirty = True
            End If
        End If

        ' Pause Button
        If ModuleCache.MediaPlayerControl.PauseButtonBounds.Contains(e.X, e.Y) = True Then
            If ModuleCache.MediaPlayerControl.PauseButtonState <> 2 Then
                ModuleCache.MediaPlayerControl.PauseButtonState = 2
                bWindowIsDirty = True
            End If
        Else
            If ModuleCache.MediaPlayerControl.PauseButtonState <> 0 Then
                ModuleCache.MediaPlayerControl.PauseButtonState = 0
                bWindowIsDirty = True
            End If
        End If

        ' Stop Button
        If ModuleCache.MediaPlayerControl.StopButtonBounds.Contains(e.X, e.Y) = True Then
            If ModuleCache.MediaPlayerControl.StopButtonState <> 2 Then
                ModuleCache.MediaPlayerControl.StopButtonState = 2
                bWindowIsDirty = True
            End If
        Else
            If ModuleCache.MediaPlayerControl.StopButtonState <> 0 Then
                ModuleCache.MediaPlayerControl.StopButtonState = 0
                bWindowIsDirty = True
            End If
        End If

        ' Next Button
        If ModuleCache.MediaPlayerControl.NextButtonBounds.Contains(e.X, e.Y) = True Then
            If ModuleCache.MediaPlayerControl.NextButtonState <> 2 Then
                ModuleCache.MediaPlayerControl.NextButtonState = 2
                bWindowIsDirty = True
            End If
        Else
            If ModuleCache.MediaPlayerControl.NextButtonState <> 0 Then
                ModuleCache.MediaPlayerControl.NextButtonState = 0
                bWindowIsDirty = True
            End If
        End If

    End Sub

    Public Sub MediaPlayerControl_ProcessMouseLeave(ByRef bWindowIsDirty As Boolean)

        ' Open Button
        If ModuleCache.MediaPlayerControl.OpenButtonState <> 0 Then
            ModuleCache.MediaPlayerControl.OpenButtonState = 0
            bWindowIsDirty = True
        End If

        ' Prev Button
        If ModuleCache.MediaPlayerControl.PrevButtonState <> 0 Then
            ModuleCache.MediaPlayerControl.PrevButtonState = 0
            bWindowIsDirty = True
        End If

        ' Play Button
        If ModuleCache.MediaPlayerControl.PlayButtonState <> 0 Then
            ModuleCache.MediaPlayerControl.PlayButtonState = 0
            bWindowIsDirty = True
        End If

        ' Pause Button
        If ModuleCache.MediaPlayerControl.PauseButtonState <> 0 Then
            ModuleCache.MediaPlayerControl.PauseButtonState = 0
            bWindowIsDirty = True
        End If

        ' Stop Button
        If ModuleCache.MediaPlayerControl.StopButtonState <> 0 Then
            ModuleCache.MediaPlayerControl.StopButtonState = 0
            bWindowIsDirty = True
        End If

        ' Next Button
        If ModuleCache.MediaPlayerControl.NextButtonState <> 0 Then
            ModuleCache.MediaPlayerControl.NextButtonState = 0
            bWindowIsDirty = True
        End If

    End Sub

    Public Sub MediaPlayerControl_OpenButtonPressed()

        Select Case IBSettings.Modules.MediaPlayerControl.MediaPlayer

            Case "foobar2000"
                If IBSettings.Modules.MediaPlayerControl.Foobar2000FileLocation <> vbNullString Then
                    ShellExecute(frmMain.Handle, "open", IBSettings.Modules.MediaPlayerControl.Foobar2000FileLocation, vbNullString, vbNullString, vbNormalFocus)
                End If

            Case "MediaMonkey"
                If IBSettings.Modules.MediaPlayerControl.MediaMonkeyFileLocation <> vbNullString Then
                    ShellExecute(frmMain.Handle, "open", IBSettings.Modules.MediaPlayerControl.MediaMonkeyFileLocation, vbNullString, vbNullString, vbNormalFocus)
                End If

            Case "Winamp"
                If IBSettings.Modules.MediaPlayerControl.WinampFileLocation <> vbNullString Then
                    ShellExecute(frmMain.Handle, "open", IBSettings.Modules.MediaPlayerControl.WinampFileLocation, vbNullString, vbNullString, vbNormalFocus)
                End If

            Case "Windows Media Player"

        End Select

        If MediaPlayer_IsPlaying AndAlso IBSettings.Modules.MediaPlayerControl.ShowNowPlayingTooltip Then
            NewTooltipOwner = "MediaPlayerControlNowPlaying"
        Else
            NewTooltipOwner = "MediaPlayerControlOpenButton"
        End If

    End Sub

    Public Sub MediaPlayerControl_PrevButtonPressed()

        Select Case IBSettings.Modules.MediaPlayerControl.MediaPlayer

            Case "foobar2000"
                If IBSettings.Modules.MediaPlayerControl.Foobar2000InfoAccessMethod = 0 Then
                    If MediaPlayer_IsRunning Then
                        foobarApp.Playback.Previous()
                    End If
                    'Else
                End If

            Case "MediaMonkey"
                If MediaPlayer_IsRunning Then
                    MediaMonkeyApp.Player.Previous()
                End If

            Case "Winamp"

            Case "Windows Media Player"

        End Select

        If MediaPlayer_IsPlaying AndAlso IBSettings.Modules.MediaPlayerControl.ShowNowPlayingTooltip Then
            NewTooltipOwner = "MediaPlayerControlNowPlaying"
        Else
            NewTooltipOwner = "MediaPlayerControlPrevButton"
        End If

    End Sub

    Public Sub MediaPlayerControl_PlayButtonPressed()

        Select Case IBSettings.Modules.MediaPlayerControl.MediaPlayer

            Case "foobar2000"
                If IBSettings.Modules.MediaPlayerControl.Foobar2000InfoAccessMethod = 0 Then
                    If MediaPlayer_IsRunning Then
                        foobarApp.Playback.Start(False)
                    End If
                End If

            Case "MediaMonkey"
                If MediaPlayer_IsRunning Then
                    MediaMonkeyApp.Player.Play()
                End If

            Case "Winamp"

            Case "Windows Media Player"

        End Select

        If MediaPlayer_IsPlaying AndAlso IBSettings.Modules.MediaPlayerControl.ShowNowPlayingTooltip Then
            NewTooltipOwner = "MediaPlayerControlNowPlaying"
        Else
            NewTooltipOwner = "MediaPlayerControlPlayButton"
        End If

    End Sub

    Public Sub MediaPlayerControl_PauseButtonPressed()

        Select Case IBSettings.Modules.MediaPlayerControl.MediaPlayer

            Case "foobar2000"
                If IBSettings.Modules.MediaPlayerControl.Foobar2000InfoAccessMethod = 0 Then
                    If MediaPlayer_IsRunning Then
                        foobarApp.Playback.Pause()
                    End If
                End If

            Case "MediaMonkey"
                If MediaPlayer_IsRunning Then
                    MediaMonkeyApp.Player.Pause()
                End If

            Case "Winamp"

            Case "Windows Media Player"

        End Select

        If MediaPlayer_IsPlaying AndAlso IBSettings.Modules.MediaPlayerControl.ShowNowPlayingTooltip Then
            NewTooltipOwner = "MediaPlayerControlNowPlaying"
        Else
            NewTooltipOwner = "MediaPlayerControlPauseButton"
        End If

    End Sub

    Public Sub MediaPlayerControl_StopButtonPressed()

        Select Case IBSettings.Modules.MediaPlayerControl.MediaPlayer

            Case "foobar2000"
                If IBSettings.Modules.MediaPlayerControl.Foobar2000InfoAccessMethod = 0 Then
                    If MediaPlayer_IsRunning Then
                        foobarApp.Playback.Stop()
                    End If
                    'Else
                End If

            Case "MediaMonkey"
                If MediaPlayer_IsRunning Then
                    MediaMonkeyApp.Player.Stop()
                End If

            Case "Winamp"

            Case "Windows Media Player"

        End Select

        If MediaPlayer_IsPlaying AndAlso IBSettings.Modules.MediaPlayerControl.ShowNowPlayingTooltip Then
            NewTooltipOwner = "MediaPlayerControlNowPlaying"
        Else
            NewTooltipOwner = "MediaPlayerControlStopButton"
        End If

    End Sub

    Public Sub MediaPlayerControl_NextButtonPressed()

        Select Case IBSettings.Modules.MediaPlayerControl.MediaPlayer

            Case "foobar2000"
                If IBSettings.Modules.MediaPlayerControl.Foobar2000InfoAccessMethod = 0 Then
                    If MediaPlayer_IsRunning Then
                        foobarApp.Playback.Next()
                    End If
                    'Else
                End If

            Case "MediaMonkey"
                If MediaPlayer_IsRunning Then
                    MediaMonkeyApp.Player.Next()
                End If

            Case "Winamp"

            Case "Windows Media Player"

        End Select

        If MediaPlayer_IsPlaying AndAlso IBSettings.Modules.MediaPlayerControl.ShowNowPlayingTooltip Then
            NewTooltipOwner = "MediaPlayerControlNowPlaying"
        Else
            NewTooltipOwner = "MediaPlayerControlNextButton"
        End If

    End Sub

#End Region

#Region "MediaMonkey Helper Functions"

    Public Function FormatTitleMM(ByVal sFormat As String) As String
        Dim curSong As Object = MediaMonkeyApp.Player.CurrentSong
        sFormat = sFormat.Replace("%album artist%", curSong.AlbumArtistName)
        sFormat = sFormat.Replace("%album%", curSong.AlbumName)
        sFormat = sFormat.Replace("%artist%", curSong.ArtistName)
        'sFormat = sFormat.Replace("%disc%", "%disc% is not supported")
        'sFormat = sFormat.Replace("%discnumber%", "%discnumber% is not supported")
        sFormat = sFormat.Replace("%track artist%", curSong.ArtistName)
        sFormat = sFormat.Replace("%title%", curSong.Title)
        'sFormat = sFormat.Replace("%track%", curSong.TrackOrder)
        sFormat = sFormat.Replace("%tracknumber%", curSong.TrackOrder)
        sFormat = sFormat.Replace("%genre%", curSong.Genre)
        sFormat = sFormat.Replace("%date%", curSong.Year)
        'sFormat = sFormat.Replace("%bitrate%", curSong.Bitrate)
        'sFormat = sFormat.Replace("%channels%", curSong.Channels + 1)
        'sFormat = sFormat.Replace("%filesize%", curSong.FileLength)
        'sFormat = sFormat.Replace("%samplerate%", curSong.SampleRate)
        'sFormat = sFormat.Replace("%codec%", curSong.Encoder)
        'sFormat = sFormat.Replace("%playback_time%", FormatTime(MediaMonkeyApp.Player.PlaybackTime / 1000))
        'sFormat = sFormat.Replace("%playback_time_seconds%", (MediaMonkeyApp.Player.PlaybackTime / 1000))
        'sFormat = sFormat.Replace("%playback_time_remaining%", FormatTime((MediaMonkeyApp.Player.CurrentSong.SongLength - MediaMonkeyApp.Player.PlaybackTime) / 1000))
        'sFormat = sFormat.Replace("%playback_time_remaining_seconds%", (MediaMonkeyApp.Player.CurrentSong.SongLength - MediaMonkeyApp.Player.PlaybackTime) / 1000)
        sFormat = sFormat.Replace("%length%", FormatTime(curSong.SongLength / 1000))
        ' %length_ex%: Returns the length of the track formatted as hours, minutes, seconds, and milliseconds.
        'sFormat = sFormat.Replace("%length_seconds%", MediaMonkeyApp.Player.CurrentSong.SongLength / 1000)
        ' %length_seconds_fp%: Returns the length of the track in seconds as floating point number. (Not supported?)
        ' %length_samples%: Returns the length of the track in samples. (Not supported?)
        'sFormat = sFormat.Replace("%isplaying%", MediaMonkeyApp.Player.isPlaying)
        'sFormat = sFormat.Replace("%ispaused%", MediaMonkeyApp.Player.isPaused)
        sFormat = sFormat.Replace("%list_index%", curSong.PlaylistOrder)
        sFormat = sFormat.Replace("%list_total%", MediaMonkeyApp.Player.CurrentPlaylist.Count)
        ' %playlist_name%: Returns the name of the playlist containing the specified item. (Not supported?)
        Return sFormat
    End Function

    Public Function GetAlbumArtMM() As String
        If MediaMonkeyApp.Player.CurrentSong.AlbumArt.Count > 0 Then
            Dim artItem As Object = MediaMonkeyApp.Player.CurrentSong.AlbumArt.Item(0)
            If artItem IsNot Nothing Then
                Dim path As String = artItem.PicturePath
                Dim artImage As Object = artItem.Image
                If artImage IsNot Nothing Then
                    Dim format As String, data As Long, len As Long
                    format = artImage.ImageFormat
                    data = artImage.ImageData
                    len = artImage.ImageDataLen
                    Dim dbTools As Object = MediaMonkeyApp.Tools
                    If dbTools IsNot Nothing Then
                        Dim fs As Object = dbTools.FileSystem
                        If fs IsNot Nothing Then
                            Select Case format
                                Case "image/jpeg"
                                    path = path & "folder.jpg"
                                Case "image/png"
                                    path = path & "folder.png"
                                Case "image/x-bmp"
                                    path = path & "folder.bmp"
                                Case "image/gif"
                                    path = path & "folder.gif"
                            End Select
                            If File.Exists(path) = False Then
                                Dim txtFile As Object = fs.CreateTextFile(path, True)
                                If txtFile IsNot Nothing Then
                                    txtFile.WriteData(data, len)
                                    txtFile.Close()
                                    Return path
                                Else
                                    Return vbNullString
                                End If
                            Else
                                Return path
                            End If
                        Else
                            Return vbNullString
                        End If
                    Else
                        Return vbNullString
                    End If
                Else
                    Return vbNullString
                End If
            Else
                Return vbNullString
            End If
        Else
            Return vbNullString
        End If
    End Function

#End Region

#Region "Tooltip Drawing"

    Public Function MediaPlayerControl_MeasureOpenButtonTooltip() As Rectangle
        Dim Gr As Graphics = Graphics.FromHwnd(IntPtr.Zero)
        Dim rect As Rectangle
        Gr.TextRenderingHint = TextRenderingHint.AntiAlias
        Select Case IBSettings.Modules.MediaPlayerControl.MediaPlayer
            Case "foobar2000"
                rect = Skinning_MeasureText(Gr, "Open/Show foobar2000", CurrentSkinInfo.Tooltip.Text)
            Case "MediaMonkey"
                rect = Skinning_MeasureText(Gr, "Open/Show MediaMonkey", CurrentSkinInfo.Tooltip.Text)
            Case "Winamp"
                rect = Skinning_MeasureText(Gr, "Open/Show Winamp", CurrentSkinInfo.Tooltip.Text)
            Case "Windows Media Player"
                rect = Skinning_MeasureText(Gr, "Open/Show Windows Media Player", CurrentSkinInfo.Tooltip.Text)
        End Select
        Gr.Dispose()
        Return rect
    End Function

    Public Function MediaPlayerControl_MeasurePrevButtonTooltip() As Rectangle
        Dim Gr As Graphics = Graphics.FromHwnd(IntPtr.Zero)
        Dim rect As Rectangle
        Gr.TextRenderingHint = TextRenderingHint.AntiAlias
        rect = Skinning_MeasureText(Gr, "Previous Track", CurrentSkinInfo.Tooltip.Text)
        Gr.Dispose()
        Return rect
    End Function

    Public Function MediaPlayerControl_MeasurePlayButtonTooltip() As Rectangle
        Dim Gr As Graphics = Graphics.FromHwnd(IntPtr.Zero)
        Dim rect As Rectangle
        Gr.TextRenderingHint = TextRenderingHint.AntiAlias
        rect = Skinning_MeasureText(Gr, "Play", CurrentSkinInfo.Tooltip.Text)
        Gr.Dispose()
        Return rect
    End Function

    Public Function MediaPlayerControl_MeasurePauseButtonTooltip() As Rectangle
        Dim Gr As Graphics = Graphics.FromHwnd(IntPtr.Zero)
        Dim rect As Rectangle
        Gr.TextRenderingHint = TextRenderingHint.AntiAlias
        rect = Skinning_MeasureText(Gr, "Pause/Unpause", CurrentSkinInfo.Tooltip.Text)
        Gr.Dispose()
        Return rect
    End Function

    Public Function MediaPlayerControl_MeasureStopButtonTooltip() As Rectangle
        Dim Gr As Graphics = Graphics.FromHwnd(IntPtr.Zero)
        Dim rect As Rectangle
        Gr.TextRenderingHint = TextRenderingHint.AntiAlias
        rect = Skinning_MeasureText(Gr, "Stop", CurrentSkinInfo.Tooltip.Text)
        Gr.Dispose()
        Return rect
    End Function

    Public Function MediaPlayerControl_MeasureNextButtonTooltip() As Rectangle
        Dim Gr As Graphics = Graphics.FromHwnd(IntPtr.Zero)
        Dim rect As Rectangle
        Gr.TextRenderingHint = TextRenderingHint.AntiAlias
        rect = Skinning_MeasureText(Gr, "Next Track", CurrentSkinInfo.Tooltip.Text)
        Gr.Dispose()
        Return rect
    End Function

    Public Function MediaPlayerControl_MeasureNowPlayingTooltip() As Rectangle
        Dim Gr As Graphics = Graphics.FromHwnd(IntPtr.Zero)
        Dim rect As Rectangle
        Gr.TextRenderingHint = TextRenderingHint.AntiAlias

        Dim maxWidth As Integer = 0, maxHeight As Integer = 0
        Dim trText As Rectangle

        If IBSettings.Modules.MediaPlayerControl.TooltipShowAlbumArt AndAlso ModuleCache.MediaPlayerControl.AlbumArtImage IsNot Nothing Then
            albumArtWidth = IBSettings.Modules.MediaPlayerControl.TooltipAlbumArtWidth
            albumArtHeight = (IBSettings.Modules.MediaPlayerControl.TooltipAlbumArtWidth / ModuleCache.MediaPlayerControl.AlbumArtImage.Width) * ModuleCache.MediaPlayerControl.AlbumArtImage.Height
            maxWidth = albumArtWidth
            textStartX = CurrentSkinInfo.Tooltip.ContentMargins.Left + albumArtWidth + 5
        Else
            albumArtWidth = 0
            albumArtHeight = 0
            maxWidth = 0
            textStartX = CurrentSkinInfo.Tooltip.ContentMargins.Left
        End If

        If IBSettings.Modules.MediaPlayerControl.TooltipShowTrackRating AndAlso (ModuleCache.MediaPlayerControl.TrackRatingImage IsNot Nothing) Then
            maxWidth = textStartX + ModuleCache.MediaPlayerControl.TrackRatingImage.Width
        End If

        trText = Skinning_MeasureText(Gr, ModuleCache.MediaPlayerControl.TooltipText, CurrentSkinInfo.Tooltip.Text, IBSettings.Modules.MediaPlayerControl.TooltipTextMaxWidth)
        ModuleCache.MediaPlayerControl.TooltipTextRect = trText
        If textStartX + trText.Width > maxWidth Then maxWidth = textStartX + trText.Width
        maxHeight = trText.Height

        If IBSettings.Modules.MediaPlayerControl.TooltipShowTrackRating AndAlso (ModuleCache.MediaPlayerControl.TrackRatingImage IsNot Nothing) Then
            maxHeight = maxHeight + 5 + ModuleCache.MediaPlayerControl.TrackRatingImage.Height + 5
        End If

        If IBSettings.Modules.MediaPlayerControl.TooltipShowAlbumArt AndAlso ModuleCache.MediaPlayerControl.AlbumArtImage IsNot Nothing Then
            If albumArtHeight > maxHeight Then maxHeight = albumArtHeight
        End If

        rect = New Rectangle(0, 0, maxWidth, maxHeight)

        Gr.Dispose()
        Return rect
    End Function

    Public Sub MediaPlayerControl_DrawOpenButtonTooltip(ByRef Gr As Graphics, ByRef tr As Rectangle)
        Select Case IBSettings.Modules.MediaPlayerControl.MediaPlayer
            Case "foobar2000"
                Skinning_DrawText(Gr, "Open/Show foobar2000", tr, CurrentSkinInfo.Tooltip.Text, StringAlignment.Near)
            Case "MediaMonkey"
                Skinning_DrawText(Gr, "Open/Show MediaMonkey", tr, CurrentSkinInfo.Tooltip.Text, StringAlignment.Near)
            Case "Winamp"
                Skinning_DrawText(Gr, "Open/Show Winamp", tr, CurrentSkinInfo.Tooltip.Text, StringAlignment.Near)
            Case "Windows Media Player"
                Skinning_DrawText(Gr, "Open/Show Windows Media Player", tr, CurrentSkinInfo.Tooltip.Text, StringAlignment.Near)
        End Select
    End Sub

    Public Sub MediaPlayerControl_DrawPrevButtonTooltip(ByRef Gr As Graphics, ByRef tr As Rectangle)
        Skinning_DrawText(Gr, "Previous Track", tr, CurrentSkinInfo.Tooltip.Text, StringAlignment.Near)
    End Sub

    Public Sub MediaPlayerControl_DrawPlayButtonTooltip(ByRef Gr As Graphics, ByRef tr As Rectangle)
        Skinning_DrawText(Gr, "Play", tr, CurrentSkinInfo.Tooltip.Text, StringAlignment.Near)
    End Sub

    Public Sub MediaPlayerControl_DrawPauseButtonTooltip(ByRef Gr As Graphics, ByRef tr As Rectangle)
        Skinning_DrawText(Gr, "Pause/Unpause", tr, CurrentSkinInfo.Tooltip.Text, StringAlignment.Near)
    End Sub

    Public Sub MediaPlayerControl_DrawStopButtonTooltip(ByRef Gr As Graphics, ByRef tr As Rectangle)
        Skinning_DrawText(Gr, "Stop", tr, CurrentSkinInfo.Tooltip.Text, StringAlignment.Near)
    End Sub

    Public Sub MediaPlayerControl_DrawNextButtonTooltip(ByRef Gr As Graphics, ByRef tr As Rectangle)
        Skinning_DrawText(Gr, "Next Track", tr, CurrentSkinInfo.Tooltip.Text, StringAlignment.Near)
    End Sub

    Public Sub MediaPlayerControl_DrawNowPlayingTooltip(ByRef Gr As Graphics, ByRef tr As Rectangle)
        Dim nextY As Integer

        If IBSettings.Modules.MediaPlayerControl.TooltipShowAlbumArt AndAlso ModuleCache.MediaPlayerControl.AlbumArtImage IsNot Nothing Then
            albumArtWidth = IBSettings.Modules.MediaPlayerControl.TooltipAlbumArtWidth
            albumArtHeight = (IBSettings.Modules.MediaPlayerControl.TooltipAlbumArtWidth / ModuleCache.MediaPlayerControl.AlbumArtImage.Width) * ModuleCache.MediaPlayerControl.AlbumArtImage.Height
            Gr.DrawImage(ModuleCache.MediaPlayerControl.AlbumArtImage, CurrentSkinInfo.Tooltip.ContentMargins.Left, CurrentSkinInfo.Tooltip.ContentMargins.Top, albumArtWidth, albumArtHeight)
            nextY = CurrentSkinInfo.Tooltip.ContentMargins.Top + albumArtHeight + 10
        End If

        nextY = CurrentSkinInfo.Tooltip.ContentMargins.Top
        tr = ModuleCache.MediaPlayerControl.TooltipTextRect
        tr.Offset(textStartX, nextY)
        nextY = nextY + tr.Height
        Skinning_DrawText(Gr, ModuleCache.MediaPlayerControl.TooltipText, tr, CurrentSkinInfo.Tooltip.Text, StringAlignment.Near)

        If IBSettings.Modules.MediaPlayerControl.TooltipShowTrackRating AndAlso ModuleCache.MediaPlayerControl.TrackRatingImage IsNot Nothing Then
            Gr.DrawImage(ModuleCache.MediaPlayerControl.TrackRatingImage, textStartX, nextY + 5, ModuleCache.MediaPlayerControl.TrackRatingImage.Width, ModuleCache.MediaPlayerControl.TrackRatingImage.Height)
        End If
    End Sub

#End Region

End Module
