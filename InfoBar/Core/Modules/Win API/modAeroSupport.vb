Module modAeroSupport

#Region "API Stuff"

  Public Const DWM_BB_ENABLE As Byte = 1
  Public Const DWM_BB_BLURREGION As Byte = 2
  Public Const DWM_BB_TRANSITIONONMAXIMIZED As Byte = 4

  Public Const WM_DWMCOMPOSITIONCHANGED As Integer = &H31E
  Public Const WM_DWMNCRENDERINGCHANGED As Integer = &H31F
  Public Const WM_DWMCOLORIZATIONCOLORCHANGED As Integer = &H320
  Public Const WM_DWMWINDOWMAXIMIZEDCHANGE As Integer = &H321

  Public Enum DWMFLIP3DWINDOWPOLICY
    DWMFLIP3D_DEFAULT      ' Hide or include the window in Flip3D based on window style and visibility.
    DWMFLIP3D_EXCLUDEBELOW ' Display the window under Flip3D and disabled.
    DWMFLIP3D_EXCLUDEABOVE ' Display the window above Flip3D and enabled.
    DWMFLIP3D_LAST
  End Enum

  Public Enum DWMWINDOWATTRIBUTE
    DWMWA_NCRENDERING_ENABLED = 1      ' [get] Is non-client rendering enabled/disabled
    DWMWA_NCRENDERING_POLICY           ' [set] Non-client rendering policy
    DWMWA_TRANSITIONS_FORCEDISABLED    ' [set] Potentially enable/forcibly disable transitions
    DWMWA_ALLOW_NCPAINT                ' [set] Allow contents rendered in the non-client area to be visible on the DWM-drawn frame.
    DWMWA_CAPTION_BUTTON_BOUNDS        ' [get] Bounds of the caption button area in window-relative space.
    DWMWA_NONCLIENT_RTL_LAYOUT         ' [set] Is non-client content RTL mirrored
    DWMWA_FORCE_ICONIC_REPRESENTATION  ' [set] Force this window to display iconic thumbnails.
    DWMWA_FLIP3D_POLICY                ' [set] Designates how Flip3D will treat the window.
    DWMWA_EXTENDED_FRAME_BOUNDS        ' [get] Gets the extended frame bounds rectangle in screen space
    DWMWA_HAS_ICONIC_BITMAP            ' [set] Indicates an available bitmap when there is no better thumbnail representation.
    DWMWA_DISALLOW_PEEK                ' [set] Don't invoke Peek on the window.
    DWMWA_LAST
  End Enum

  Public isAWindowMaximized As Boolean = False

  <StructLayout(LayoutKind.Sequential, Pack:=1)> _
  Public Structure DWM_BLURBEHIND
    Public dwFlags As Integer
    Public fEnable As Boolean
    Public hRgnBlur As IntPtr
    Public fTransitionOnMaximized As Boolean
  End Structure

  Public Declare Function DwmEnableBlurBehindWindow Lib "dwmapi.dll" (ByVal hWnd As IntPtr, ByRef pBlurBehind As DWM_BLURBEHIND) As IntPtr
  Public Declare Function DwmGetWindowAttribute Lib "dwmapi.dll" (ByVal hWnd As IntPtr, ByVal dwAttribute As DWMWINDOWATTRIBUTE, ByVal pvAttribute As IntPtr, ByVal cbAttribute As Integer) As Integer
  Public Declare Sub DwmIsCompositionEnabled Lib "dwmapi.dll" (ByRef isEnabled As Boolean)
  Public Declare Function DwmGetColorizationColor Lib "dwmapi.dll" (ByRef pcrColorization As UInteger, ByRef pfOpaqueBlend As Boolean) As Integer
  Public Declare Sub DwmSetWindowAttribute Lib "dwmapi.dll" (ByVal hWnd As IntPtr, ByVal dwAttribute As DWMWINDOWATTRIBUTE, ByVal pvAttribute As IntPtr, ByVal cbAttribute As Integer)
  Public Declare Auto Function SetWindowTheme Lib "uxtheme.dll" (ByVal hWnd As IntPtr, ByVal textSubAppName As String, ByVal textSubIdList As String) As Integer
  Public Declare Auto Function SendMessage Lib "user32.dll" (ByVal hWnd As IntPtr, ByVal msg As Integer, ByVal wParam As IntPtr, ByVal lParam As IntPtr) As IntPtr

#End Region

  Public Function IsWindowsVistaOrAbove() As Boolean
    Return (Environment.OSVersion.Version.Major >= 6)
  End Function

  Public Function IsWindowsSeven() As Boolean
    Return ( _
      (Environment.OSVersion.Version.Major = 6 AndAlso Environment.OSVersion.Version.Minor = 1) Or _
      (Environment.OSVersion.Version.Major > 6) _
    )
  End Function

  Public Function IsCompositionEnabled() As Boolean
    If IsWindowsVistaOrAbove() Then
      Dim bEnabled As Boolean
      DwmIsCompositionEnabled(bEnabled)
      If bEnabled Then
        Dim cColor As UInteger, bOpaque As Boolean
        DwmGetColorizationColor(cColor, bOpaque)
        Return Not bOpaque
      Else
        Return False
      End If
    End If
      Return False
  End Function

  Public Sub ToggleBlur(ByVal Enabled As Boolean)
    If IsCompositionEnabled() = True Then
      Dim bb As DWM_BLURBEHIND
      bb.dwFlags = DWM_BB_ENABLE Or DWM_BB_TRANSITIONONMAXIMIZED
      bb.fEnable = Enabled
      bb.fTransitionOnMaximized = ((IBSettings.General.AutoHide And Not IBSettings.Advanced.AutohideIgnoreMaximizedState) Or (Not IBSettings.General.AutoHide))
      bb.hRgnBlur = IntPtr.Zero
      DwmEnableBlurBehindWindow(fMain.Handle, bb)
      SetDWMWindowAttributes()
    End If
  End Sub

  Public Sub SetDWMWindowAttributes()
    Dim f3dpol As Integer = DWMFLIP3DWINDOWPOLICY.DWMFLIP3D_EXCLUDEABOVE
    Dim pf3dpol As IntPtr
    pf3dpol = Marshal.AllocHGlobal(Marshal.SizeOf(f3dpol))
    Marshal.StructureToPtr(f3dpol, pf3dpol, True)
    DwmSetWindowAttribute(fMain.Handle, DWMWINDOWATTRIBUTE.DWMWA_FLIP3D_POLICY, pf3dpol, Marshal.SizeOf(f3dpol))

    If IsWindowsSeven() Then
      Dim disallowPeek As Integer = 1
      Dim disallowPeekPtr As IntPtr
      disallowPeekPtr = Marshal.AllocHGlobal(Marshal.SizeOf(disallowPeek))
      Marshal.StructureToPtr(disallowPeek, disallowPeekPtr, True)
      DwmSetWindowAttribute(fMain.Handle, DWMWINDOWATTRIBUTE.DWMWA_DISALLOW_PEEK, disallowPeekPtr, Marshal.SizeOf(disallowPeekPtr))
    End If
  End Sub

End Module