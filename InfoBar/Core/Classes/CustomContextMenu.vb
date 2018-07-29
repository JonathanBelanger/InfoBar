Public Class CustomContextMenu

  Private Const SWP_NOSIZE As Integer = &H1
  Private Const SWP_NOMOVE As Integer = &H2
  Private Const SWP_NOREDRAW As Integer = &H8
  Private Const SWP_NOACTIVATE As Integer = &H10
  Private Const SWP_NOOWNERZORDER As Integer = &H200

  <DllImport("user32.dll", CharSet:=CharSet.Auto)> _
  Private Shared Function SetWindowPos(ByVal hWnd As IntPtr, ByVal hWndInsertAfter As IntPtr, ByVal x As Integer, ByVal y As Integer, ByVal Width As Integer, ByVal Height As Integer, ByVal flags As UInteger) As Boolean
  End Function

  Private ctrlRect As Rectangle
  Private ctrlAttached As Boolean

  Public Overloads Sub Show(ByVal control As Control, ByVal position As Point)
    ctrlAttached = False
    MyBase.Show(control, position)
  End Sub

  Public Overloads Sub Show(ByVal control As Control, ByVal rect As Rectangle)
    ctrlRect = rect
    ctrlAttached = True
    MyBase.Show(control, rect.Location)
  End Sub

  Protected Overrides Sub SetBoundsCore(ByVal x As Integer, ByVal y As Integer, ByVal width As Integer, ByVal height As Integer, ByVal specified As System.Windows.Forms.BoundsSpecified)
    Dim pt As Point
    If ctrlAttached = True Then
      pt = ctrlRect.Location
      If pt.X + width > Screen.PrimaryScreen.WorkingArea.Width Then pt.X = ctrlRect.Left - (width - ctrlRect.Width)
      If pt.Y + height > Screen.PrimaryScreen.WorkingArea.Height Then
        pt.Y = ctrlRect.Top - height
      Else
        pt.Y = ctrlRect.Bottom
      End If
    Else
      pt = Cursor.Position
      If pt.X + width > Screen.PrimaryScreen.WorkingArea.Width Then pt.X = pt.X - width
      If pt.Y + height > Screen.PrimaryScreen.WorkingArea.Height Then pt.Y = pt.Y - height
    End If
    SetWindowPos(Me.Handle, IntPtr.Zero, pt.X, pt.Y, width, height, SWP_NOREDRAW Or SWP_NOACTIVATE Or SWP_NOOWNERZORDER)
  End Sub

End Class
