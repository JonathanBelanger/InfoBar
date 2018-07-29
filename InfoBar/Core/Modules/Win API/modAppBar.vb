Module modAppBar

#Region "API Stuff"

  Enum WindowStyles As UInteger
    WS_DLGFRAME = &H400000
  End Enum

  Enum WindowExStyles As UInteger
    WS_EX_LAYERED = &H80000
    WS_EX_NOACTIVATE = &H8000000
  End Enum

  Public Const HWND_BOTTOM As Integer = 1
  Public Const HWND_TOPMOST As Integer = -1
  Public Const SWP_NOSIZE As Integer = &H1
  Public Const SWP_NOMOVE As Integer = &H2
  Public Const SWP_NOREDRAW As Integer = &H8
  Public Const SWP_NOACTIVATE As Integer = &H10
  Public Const SWP_NOOWNERZORDER As Integer = &H200
  Public Const SWP_NOSENDCHANGING As Integer = &H400
  Public Const SWP_ASYNCWINDOWPOS As Integer = &H4000

  <StructLayout(LayoutKind.Sequential)> Structure RECT
    Public left As Integer
    Public top As Integer
    Public right As Integer
    Public bottom As Integer
  End Structure

  <StructLayout(LayoutKind.Sequential)> Structure APPBARDATA
    Public cbSize As Integer
    Public hWnd As IntPtr
    Public uCallbackMessage As Integer
    Public uEdge As Integer
    Public rc As RECT
    Public lParam As Integer
  End Structure

  Enum ABMsg As Integer
    ABM_NEW = 0
    ABM_REMOVE = 1
    ABM_QUERYPOS = 2
    ABM_SETPOS = 3
    ABM_GETAUTOHIDEBAR = 7
    ABM_SETAUTOHIDEBAR = 8
  End Enum

  Enum ABNotify
    ABN_STATECHANGE = 0
    ABN_POSCHANGED
    ABN_FULLSCREENAPP
    ABN_WINDOWARRANGE
  End Enum

  Enum ABEdge
    ABE_LEFT = 0
    ABE_TOP = 1
    ABE_RIGHT = 2
    ABE_BOTTOM = 3
  End Enum

  <DllImport("SHELL32", CallingConvention:=CallingConvention.StdCall)> _
  Private Function SHAppBarMessage(ByVal dwMessage As Integer, ByRef BarrData As APPBARDATA) As Integer
  End Function

  <DllImport("USER32")> _
  Private Function GetSystemMetric(ByVal Index As Integer) As Integer
  End Function

  <DllImport("User32.dll", ExactSpelling:=True, CharSet:=System.Runtime.InteropServices.CharSet.Auto)> _
  Private Function MoveWindow(ByVal hWnd As IntPtr, ByVal x As Integer, ByVal y As Integer, ByVal cX As Integer, ByVal cY As Integer, ByVal repaint As Boolean) As Boolean
  End Function

  <DllImport("User32.dll", CharSet:=CharSet.Auto)> _
  Public Function RegisterWindowMessage(ByVal msg As String) As Integer
  End Function

  <DllImport("user32.dll", CharSet:=CharSet.Auto)> _
  Public Sub SetForegroundWindow(ByVal hWnd As IntPtr)
  End Sub

  <DllImport("user32.dll", CharSet:=CharSet.Auto)> _
  Public Function SetWindowPos(ByVal hWnd As IntPtr, ByVal hWndInsertAfter As Integer, ByVal X As Integer, ByVal Y As Integer, ByVal cx As Integer, ByVal cy As Integer, ByVal uFlags As Integer) As Boolean
  End Function

#End Region

  Public AppBarRegistered As Boolean = False
  Public AppBarMessageCallBack As Integer
  Public TaskbarRestartCallBack As Integer

  Public fMain As frmMain
  Public fTooltip As frmTooltip

  Public Sub RegisterBar()
    Dim abd As New APPBARDATA
    Dim ret As Integer
    abd.cbSize = Marshal.SizeOf(abd)
    abd.hWnd = fMain.Handle
    If AppBarRegistered = False Then
      Dim uniqueMessageString As String = Guid.NewGuid().ToString()
      AppBarMessageCallBack = RegisterWindowMessage(uniqueMessageString)
      abd.uCallbackMessage = AppBarMessageCallBack
      ret = SHAppBarMessage(ABMsg.ABM_NEW, abd)
      AppBarRegistered = True
    End If
  End Sub

  Public Sub UnregisterBar()
    Dim abd As New APPBARDATA
    Dim ret As Integer
    abd.cbSize = Marshal.SizeOf(abd)
    abd.hWnd = fMain.Handle
    ret = SHAppBarMessage(ABMsg.ABM_REMOVE, abd)
  End Sub

  Public Sub ABSetPos(ByVal dockEdge As ABEdge)
    Dim abd As New APPBARDATA
    abd.cbSize = Marshal.SizeOf(abd)
    abd.hWnd = fMain.Handle
    abd.uEdge = dockEdge

    Dim waRect As Size = SystemInformation.PrimaryMonitorSize

    Select Case abd.uEdge
      Case ABEdge.ABE_LEFT
        abd.rc.left = 0
        abd.rc.top = 0
        abd.rc.right = MainForm_Size.Width
        abd.rc.bottom = waRect.Height
      Case ABEdge.ABE_TOP
        abd.rc.left = 0
        abd.rc.top = 0
        abd.rc.right = waRect.Width
        abd.rc.bottom = MainForm_Size.Height
      Case ABEdge.ABE_RIGHT
        abd.rc.left = waRect.Width - MainForm_Size.Width
        abd.rc.top = 0
        abd.rc.right = waRect.Width
        abd.rc.bottom = waRect.Height
      Case ABEdge.ABE_BOTTOM
        abd.rc.left = 0
        abd.rc.top = waRect.Height - MainForm_Size.Height
        abd.rc.right = waRect.Width
        abd.rc.bottom = waRect.Height
    End Select

    SHAppBarMessage(ABMsg.ABM_QUERYPOS, abd)

    Select Case abd.uEdge
      Case ABEdge.ABE_LEFT
        abd.rc.right = abd.rc.left + MainForm_Size.Width
      Case ABEdge.ABE_TOP
        abd.rc.bottom = abd.rc.top + MainForm_Size.Height
      Case ABEdge.ABE_RIGHT
        abd.rc.left = abd.rc.right - MainForm_Size.Width
      Case ABEdge.ABE_BOTTOM
        abd.rc.top = abd.rc.bottom - MainForm_Size.Height
    End Select

    If IBSettings.General.DisableToolbarDocking = False Then SHAppBarMessage(ABMsg.ABM_SETPOS, abd)
    MoveWindow(fMain.Handle, abd.rc.left, abd.rc.top, abd.rc.right - abd.rc.left, abd.rc.bottom - abd.rc.top, False)
  End Sub

  Public Sub ABSetAutoHide(ByVal autoHide As Boolean)
    Dim abd As New APPBARDATA
    abd.cbSize = Marshal.SizeOf(abd)
    abd.hWnd = fMain.Handle
    abd.uEdge = IBSettings.General.Position
    abd.lParam = IIf(autoHide, 1, 0)

    If autoHide Then
      SHAppBarMessage(ABMsg.ABM_REMOVE, abd)
      fMain.tmrAutohide.Start()
    Else
      SHAppBarMessage(ABMsg.ABM_NEW, abd)
      ABSetPos(IBSettings.General.Position)
    End If
    ToggleBlur(CurrentSkinInfo.Background.EnableGlass)
  End Sub

  Public Sub ABHideBar(ByVal HideBar As Boolean)
    Dim abd As New APPBARDATA
    abd.cbSize = Marshal.SizeOf(abd)
    abd.hWnd = fMain.Handle
    abd.uEdge = IBSettings.General.Position
    abd.lParam = 0

    If HideBar Then
      SHAppBarMessage(ABMsg.ABM_REMOVE, abd)
    Else
      SHAppBarMessage(ABMsg.ABM_NEW, abd)
    End If
  End Sub

  Public Sub SetTopMost(ByVal hWnd As IntPtr, ByVal TopMost As Boolean)
    SetWindowPos(hWnd, IIf(TopMost, HWND_TOPMOST, HWND_BOTTOM), 0, 0, 0, 0, SWP_NOSIZE Or SWP_NOMOVE Or SWP_NOACTIVATE Or SWP_ASYNCWINDOWPOS Or SWP_NOOWNERZORDER Or SWP_NOREDRAW Or SWP_NOSENDCHANGING)
  End Sub

End Module
