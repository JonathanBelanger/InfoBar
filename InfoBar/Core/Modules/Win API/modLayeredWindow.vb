Module modLayeredWindow

#Region "Win API"

  Public Const GWL_EXSTYLE As Integer = -20
  Public Const ULW_COLORKEY As Byte = 1
  Public Const ULW_ALPHA As Byte = 2
  Public Const ULW_OPAQUE As Byte = 4
  Public Const AC_SRC_OVER As Byte = 0
  Public Const AC_SRC_ALPHA As Byte = 1
  Public Const RGN_AND = 1
  Public Const RGN_COPY = 5
  Public Const RGN_DIFF = 4
  Public Const RGN_OR = 2
  Public Const RGN_XOR = 3

  <StructLayout(LayoutKind.Sequential, Pack:=1)> _
  Structure ARGB
    Public Blue As Byte
    Public Green As Byte
    Public Red As Byte
    Public Alpha As Byte
  End Structure

  <StructLayout(LayoutKind.Sequential, Pack:=1)> _
  Public Structure BLENDFUNCTION
    Public BlendOp As Byte
    Public BlendFlags As Byte
    Public SourceConstantAlpha As Byte
    Public AlphaFormat As Byte
  End Structure

  <StructLayout(LayoutKind.Sequential, Pack:=2)> _
  Public Class BITMAPINFOHEADER
    Public biSize As Integer
    Public biWidth As Integer
    Public biHeight As Integer
    Public biPlanes As Short
    Public biBitCount As Short
    Public biCompression As Integer
    Public biSizeImage As Integer
    Public biXPelsPerMeter As Integer
    Public biYPelsPerMeter As Integer
    Public biClrUsed As Integer
    Public biClrImportant As Integer
  End Class

  <StructLayout(LayoutKind.Sequential)> _
      Public Structure BITMAPINFO_FLAT
    Public bmiHeader_biSize As Integer
    Public bmiHeader_biWidth As Integer
    Public bmiHeader_biHeight As Integer
    Public bmiHeader_biPlanes As Short
    Public bmiHeader_biBitCount As Short
    Public bmiHeader_biCompression As Integer
    Public bmiHeader_biSizeImage As Integer
    Public bmiHeader_biXPelsPerMeter As Integer
    Public bmiHeader_biYPelsPerMeter As Integer
    Public bmiHeader_biClrUsed As Integer
    Public bmiHeader_biClrImportant As Integer
    <MarshalAs(UnmanagedType.ByValArray, SizeConst:=&H400)> _
    Public bmiColors() As Byte
  End Structure

  <StructLayout(LayoutKind.Sequential)> _
  Public Class RGBQUAD
    Public rgbBlue As Integer
    Public rgbGreen As Integer
    Public rgbRed As Integer
    Public rgbReserved As Integer
  End Class

  <StructLayout(LayoutKind.Sequential)> _
  Public Class BITMAPINFO
    Public bmiHeader As BITMAPINFOHEADER
    Public bmiColors As RGBQUAD
  End Class

  Public Declare Function UpdateLayeredWindow Lib "user32.dll" (ByVal hwnd As IntPtr, ByVal hdcDst As IntPtr, ByRef pptDst As Point, ByRef psize As Size, ByVal hdcSrc As IntPtr, ByRef pprSrc As Point, ByVal crKey As Int32, ByRef pblend As BLENDFUNCTION, ByVal dwFlags As Int32) As Boolean
  Public Declare Function GetDC Lib "user32.dll" (ByVal hWnd As IntPtr) As IntPtr
  Public Declare Function ReleaseDC Lib "user32.dll" (ByVal hWnd As IntPtr, ByVal hDC As IntPtr) As Integer
  Public Declare Function CreateCompatibleDC Lib "gdi32.dll" (ByVal hDC As IntPtr) As IntPtr
  Public Declare Function DeleteDC Lib "gdi32.dll" (ByVal hdc As IntPtr) As Boolean
  Public Declare Function SelectObject Lib "gdi32.dll" (ByVal hDC As IntPtr, ByVal hObject As IntPtr) As IntPtr
  Public Declare Function DeleteObject Lib "gdi32.dll" (ByVal hObject As IntPtr) As Boolean
  Public Declare Function CreateDIBSection Lib "gdi32.dll" (ByVal hdc As IntPtr, ByRef bmi As BITMAPINFO_FLAT, ByVal iUsage As Integer, ByRef ppvBits As IntPtr, ByVal hSection As IntPtr, ByVal dwOffset As Integer) As IntPtr
  Public Declare Function GetWindowLong Lib "user32.dll" Alias "GetWindowLongA" (ByVal hWnd As IntPtr, ByVal nIndex As Integer) As Integer
  Public Declare Function SetWindowLong Lib "user32.dll" Alias "SetWindowLongA" (ByVal hWnd As IntPtr, ByVal nIndex As Integer, ByVal dwNewLong As Integer) As Integer

#End Region

  Public MainForm_backBitmap As Bitmap
  Public MainForm_Size As Size
  Public MainForm_backBitmapSize As Size
  Public MainForm_memDC As IntPtr
  Public MainForm_hBackBitmap As IntPtr
  Public MainForm_pDIBRawBits As IntPtr
  Public MainForm_oldBitmap As IntPtr
  Public MainForm_screenDC As IntPtr
  Public MainForm_sepBitmap As Bitmap

  Public TooltipForm_backBitmap As Bitmap
  Public TooltipForm_backBitmapSize As Size  
  Public TooltipForm_memDC As IntPtr
  Public TooltipForm_hBackBitmap As IntPtr
  Public TooltipForm_pDIBRawBits As IntPtr
  Public TooltipForm_oldBitmap As IntPtr
  Public TooltipForm_screenDC As IntPtr

End Module