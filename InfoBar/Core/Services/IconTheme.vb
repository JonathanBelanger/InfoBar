Namespace Services

  Public Class IconTheme

#Region "Win32 API imports"

    Private Const MAX_PATH As Integer = 260
    Private Const MAX_TYPE As Integer = 80

    <Flags()> Private Enum SHGFI
      ICON = &H100
      DISPLAYNAME = &H200
      TYPENAME = &H400
      ATTRIBUTES = &H800
      ICONLOCATION = &H1000
      EXETYPE = &H2000
      SYSICONINDEX = &H4000
      LINKOVERLAY = &H8000
      SELECTED = &H10000
      ATTR_SPECIFIED = &H20000
      LARGEICON = &H0
      SMALLICON = &H1
      OPENICON = &H2
      SHELLICONSIZE = &H4
      PIDL = &H8
      USEFILEATTRIBUTES = &H10
      ADDOVERLAYS = &H20
      OVERLAYINDEX = &H40
    End Enum

    <StructLayout(LayoutKind.Sequential, CharSet:=CharSet.Auto)> Private Structure SHFILEINFO
      Public hIcon As IntPtr
      Public iIcon As Integer
      Public dwAttributes As Integer
      <MarshalAs(UnmanagedType.ByValTStr, SizeConst:=MAX_PATH)> Public szDisplayName As String
      <MarshalAs(UnmanagedType.ByValTStr, SizeConst:=MAX_TYPE)> Public szTypeName As String
    End Structure

    Private Declare Auto Function SHGetFileInfo Lib "shell32" (ByVal pszPath As String, ByVal dwFileAttributes As Integer, ByRef sfi As SHFILEINFO, ByVal cbsfi As Integer, ByVal uFlags As Integer) As IntPtr
    Private Declare Function DestroyIcon Lib "user32.dll" (ByVal hIcon As IntPtr) As Integer
#End Region

    Public Function IsDefaultTheme() As Boolean
      IsDefaultTheme = (IBSettings.CurrentIconTheme = "InfoBarInternalIconTheme")
    End Function

    Public Function GetThemeIcon(ByVal moduleGUID As String, ByVal moduleElement As String) As Image
      Dim sKey As String = moduleGUID & "::" & moduleElement
      If CurrentIconTheme.Icons.Contains(sKey) = True Then
        Return CurrentIconTheme.Icons(sKey).Icon
      Else
        Return Nothing
      End If
    End Function

    Public Function GetSystemIconForFile(ByVal sFilename As String) As Image
      Try
        Dim info As New SHFILEINFO
        Dim cbFileInfo As Integer = Marshal.SizeOf(info)
        Dim flags As SHGFI = (SHGFI.ICON Or SHGFI.SMALLICON Or SHGFI.USEFILEATTRIBUTES) And Not SHGFI.ADDOVERLAYS
        Dim fileAttribs = FileAttribute.Normal
        SHGetFileInfo(sFilename, fileAttribs, info, cbFileInfo, flags)
        Dim img As Image = Bitmap.FromHicon(info.hIcon)
        DestroyIcon(info.hIcon)
        Return img
      Catch ex As Exception
        Return Nothing
      End Try
    End Function

    Public Function GetSystemIconByExtension(ByVal sExtension As String) As Image
      Try
        Dim info As New SHFILEINFO
        Dim cbFileInfo As Integer = Marshal.SizeOf(info)
        Dim flags As SHGFI = (SHGFI.ICON Or SHGFI.SMALLICON Or SHGFI.USEFILEATTRIBUTES) And Not SHGFI.ADDOVERLAYS
        Dim fileAttribs As FileAttribute
        If sExtension = "Folder" Or sExtension = "Directory" Then
          fileAttribs = FileAttribute.Directory
        Else
          fileAttribs = FileAttribute.Normal
        End If
        SHGetFileInfo(String.Format("0.{0}", sExtension), fileAttribs, info, cbFileInfo, flags)
        Dim img As Image = Bitmap.FromHicon(info.hIcon)
        DestroyIcon(info.hIcon)
        Return img
      Catch ex As Exception
        Return Nothing
      End Try
    End Function

  End Class

End Namespace