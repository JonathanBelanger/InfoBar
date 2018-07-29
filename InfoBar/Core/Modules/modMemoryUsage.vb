Module modMemoryUsage

    <StructLayout(LayoutKind.Sequential, CharSet:=CharSet.Auto)> _
    Private Class MEMORYSTATUS
        Public dwLength As UInt32
        Public dwMemoryLoad As UInt32
        Public dwTotalPhys As UInt32
        Public dwAvailPhys As UInt32
        Public dwTotalPageFile As UInt32
        Public dwAvailPageFile As UInt32
        Public dwTotalVirtual As UInt32
        Public dwAvailVirtual As UInt32
    End Class

    <DllImport("kernel32", CharSet:=CharSet.Auto, SetLastError:=True)> _
    Private Sub GlobalMemoryStatus(ByVal lpBuffer As MEMORYSTATUS)
    End Sub

    Public Sub MemoryUsage_Update()
        If ModuleCache.MemoryUsage.Enabled = False Then Exit Sub

        Dim MS As MEMORYSTATUS = New MEMORYSTATUS()
        MS.dwLength = Marshal.SizeOf(MS)
        GlobalMemoryStatus(MS)

        ' TODO: Show Usage Text based on choice of user (% free or % used)
        Dim usageText As String
        If IBSettings.Modules.MemoryUsage.TextMode = 0 Then
            usageText = CInt((MS.dwAvailPhys / MS.dwTotalPhys) * 100) & "%"
        Else
            usageText = CInt(((MS.dwTotalPhys - MS.dwAvailPhys) / MS.dwTotalPhys) * 100) & "%"
        End If
        If ModuleCache.MemoryUsage.UsageText <> usageText Then
            ModuleCache.MemoryUsage.UsageText = usageText
            CurrentSkinInfo.WindowIsDirty = True
        End If

        ModuleCache.MemoryUsage.TooltipText = ""

        Dim sTooltip As String = ""

        If IBSettings.Modules.MemoryUsage.TextMode = 0 Then
            ' Physical Memory
            If IBSettings.Modules.MemoryUsage.TooltipPhysicalMemory Then
                sTooltip &= "Physical Memory: "
                sTooltip &= FormatFileSize(MS.dwAvailPhys) & " free of "
                sTooltip &= FormatFileSize(MS.dwTotalPhys) & " (" & usageText & ")"
            End If

            ' Virtual Memory
            If IBSettings.Modules.MemoryUsage.TooltipVirtualMemory Then
                sTooltip &= vbCrLf
                sTooltip &= "Virtual Memory: " & FormatFileSize(MS.dwAvailVirtual) & " free of "
                sTooltip &= FormatFileSize(MS.dwTotalVirtual) & " ("
                sTooltip &= CInt((MS.dwAvailVirtual / MS.dwTotalVirtual) * 100) & "%)"
            End If

            ' Page File
            If IBSettings.Modules.MemoryUsage.TooltipPageFile Then
                sTooltip &= vbCrLf
                sTooltip &= "Page File: " & FormatFileSize(MS.dwAvailPageFile) & " free of "
                sTooltip &= FormatFileSize(MS.dwTotalPageFile) & " ("
                sTooltip &= CInt((MS.dwAvailPageFile / MS.dwTotalPageFile) * 100) & "%)"
            End If
        Else

            ' Physical Memory
            If IBSettings.Modules.MemoryUsage.TooltipPhysicalMemory Then
                sTooltip &= "Physical Memory: "
                sTooltip &= FormatFileSize(MS.dwTotalPhys - MS.dwAvailPhys) & " used of "
                sTooltip &= FormatFileSize(MS.dwTotalPhys) & " (" & usageText & ")"
            End If

            ' Virtual Memory
            If IBSettings.Modules.MemoryUsage.TooltipVirtualMemory Then
                sTooltip &= vbCrLf
                sTooltip &= "Virtual Memory: " & FormatFileSize(MS.dwTotalVirtual - MS.dwAvailVirtual) & " used of "
                sTooltip &= FormatFileSize(MS.dwTotalVirtual) & " ("
                sTooltip &= CInt(((MS.dwTotalVirtual - MS.dwAvailVirtual) / MS.dwTotalVirtual) * 100) & "%)"
            End If

            ' Page File
            If IBSettings.Modules.MemoryUsage.TooltipPageFile Then
                sTooltip &= vbCrLf
                sTooltip &= "Page File: " & FormatFileSize(MS.dwTotalPageFile - MS.dwAvailPageFile) & " used of "
                sTooltip &= FormatFileSize(MS.dwTotalPageFile) & " ("
                sTooltip &= CInt(((MS.dwTotalPageFile - MS.dwAvailPageFile) / MS.dwTotalPageFile) * 100) & "%)"
            End If
        End If

        If IBSettings.Modules.MemoryUsage.TooltipTop5Processes Then
            SortCollectionNumeric(ProcessInfoCol, "WorkingSetSize", False, "ProcessID")
            sTooltip &= vbCrLf & vbCrLf & "Top 5 Processes:" & vbCrLf
            Dim i As Integer
            For i = 1 To 5
                sTooltip &= ProcessInfoCol(i).Name & ": " & FormatFileSize(ProcessInfoCol(i).WorkingSetSize)
                If i < 5 Then sTooltip &= vbCrLf
            Next
        End If

        ModuleCache.MemoryUsage.TooltipText = sTooltip
        If CurrentTooltipOwner = "MemoryUsage" Then CurrentSkinInfo.TooltipWindowIsDirty = True
    End Sub

    Public Sub MemoryUsage_Draw(ByRef Gr As Graphics, ByRef nextX As Integer)
        If ModuleCache.MemoryUsage.Enabled = False Then Exit Sub

        ModuleCache.MemoryUsage.Bounds.X = nextX
        ModuleCache.MemoryUsage.Bounds.Y = CurrentSkinInfo.Background.ContentMargins.Top

        If IBSettings.Modules.MemoryUsage.ShowIcon = True Then
            Gr.DrawImage(CurrentIconTheme.MemoryUsage, nextX, CInt((CurrentSkinInfo.Height - CurrentIconTheme.MemoryUsage.Height) / 2))
            nextX = nextX + CurrentIconTheme.MemoryUsage.Width
            ModuleCache.MemoryUsage.Bounds.Width = nextX
        End If

        If IBSettings.Modules.MemoryUsage.ShowText = True Then
            Dim tr As Rectangle
            tr = Skinning_MeasureText(Gr, "100%", CurrentSkinInfo.Background.Text)
            tr.X = nextX + 2
            tr.Y = CurrentSkinInfo.Background.ContentMargins.Top
            tr.Width = tr.Width + 2
            tr.Height = CurrentSkinInfo.Height - (CurrentSkinInfo.Background.ContentMargins.Top + CurrentSkinInfo.Background.ContentMargins.Bottom)
            Skinning_DrawText(Gr, ModuleCache.MemoryUsage.UsageText, tr, CurrentSkinInfo.Background.Text, StringAlignment.Far)
            nextX = nextX + tr.Width + 2
        End If
        ModuleCache.MemoryUsage.Bounds.Width = nextX - ModuleCache.MemoryUsage.Bounds.X
        ModuleCache.MemoryUsage.Bounds.Height = CurrentSkinInfo.Height - (CurrentSkinInfo.Background.ContentMargins.Top + CurrentSkinInfo.Background.ContentMargins.Bottom)
    End Sub

    Public Sub MemoryUsage_ProcessMouseMove(ByRef e As MouseEventArgs, ByRef bWindowIsDirty As Boolean)
        If ModuleCache.MemoryUsage.Enabled = False Then Exit Sub
        If ModuleCache.MemoryUsage.Bounds.Contains(e.X, e.Y) = True Then
            If e.Button = MouseButtons.None Then NewTooltipOwner = "MemoryUsage"
        End If
    End Sub

    Public Function MemoryUsage_MeasureTooltip() As Rectangle
        Dim Gr As Graphics = Graphics.FromHwnd(IntPtr.Zero)
        Dim rect As Rectangle
        Gr.TextRenderingHint = TextRenderingHint.AntiAlias
        rect = Skinning_MeasureText(Gr, ModuleCache.MemoryUsage.TooltipText, CurrentSkinInfo.Tooltip.Text)
        Gr.Dispose()
        Return rect
    End Function

    Public Sub MemoryUsage_DrawTooltip(ByRef Gr As Graphics, ByRef tr As Rectangle)
        Skinning_DrawText(Gr, ModuleCache.MemoryUsage.TooltipText, tr, CurrentSkinInfo.Tooltip.Text, StringAlignment.Near)
    End Sub

End Module
