Friend Class ShellDll

    Public Const MAX_PATH As Integer = 260
    Public Const FILE_ATTRIBUTE_NORMAL As Integer = &H80
    Public Const FILE_ATTRIBUTE_DIRECTORY As Integer = &H10
    Public Const NOERROR As Integer = 0
    Public Const S_OK As Integer = 0
    Public Const S_FALSE As Integer = 1

    <Flags()> Public Enum SFGAO
        CANCOPY = &H1
        CANMOVE = &H2
        CANLINK = &H4
        STORAGE = &H8
        CANRENAME = &H10
        CANDELETE = &H20
        HASPROPSHEET = &H40
        DROPTARGET = &H100
        CAPABILITYMASK = &H177
        ENCRYPTED = &H2000
        ISSLOW = &H4000
        GHOSTED = &H8000
        LINK = &H10000
        SHARE = &H20000
        RDONLY = &H40000
        HIDDEN = &H80000
        DISPLAYATTRMASK = &HFC000
        FILESYSANCESTOR = &H10000000
        FOLDER = &H20000000
        FILESYSTEM = &H40000000
        HASSUBFOLDER = &H80000000
        CONTENTSMASK = &H80000000
        VALIDATE = &H1000000
        REMOVABLE = &H2000000
        COMPRESSED = &H4000000
        BROWSABLE = &H8000000
        NONENUMERATED = &H100000
        NEWCONTENT = &H200000
        CANMONIKER = &H400000
        HASSTORAGE = &H400000
        STREAM = &H400000
        STORAGEANCESTOR = &H800000
        STORAGECAPMASK = &H70C50008
    End Enum

    <Flags()> Public Enum SHGFI
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

    Public Enum CSIDL As Integer
        DESKTOP = &H0
        INTERNET = &H1
        PROGRAMS = &H2
        CONTROLS = &H3
        PRINTERS = &H4
        PERSONAL = &H5
        FAVORITES = &H6
        STARTUP = &H7
        RECENT = &H8
        SENDTO = &H9
        BITBUCKET = &HA
        STARTMENU = &HB
        MYDOCUMENTS = &HC
        MYMUSIC = &HD
        MYVIDEO = &HE
        DESKTOPDIRECTORY = &H10
        DRIVES = &H11
        NETWORK = &H12
        NETHOOD = &H13
        FONTS = &H14
        TEMPLATES = &H15
        COMMON_STARTMENU = &H16
        COMMON_PROGRAMS = &H17
        COMMON_STARTUP = &H18
        COMMON_DESKTOPDIRECTORY = &H19
        APPDATA = &H1A
        PRINTHOOD = &H1B
        LOCAL_APPDATA = &H1C
        ALTSTARTUP = &H1D
        COMMON_ALTSTARTUP = &H1E
        COMMON_FAVORITES = &H1F
        INTERNET_CACHE = &H20
        COOKIES = &H21
        HISTORY = &H22
        COMMON_APPDATA = &H23
        WINDOWS = &H24
        SYSTEM = &H25
        PROGRAM_FILES = &H26
        MYPICTURES = &H27
        PROFILE = &H28
        SYSTEMX86 = &H29
        PROGRAM_FILESX86 = &H2A
        PROGRAM_FILES_COMMON = &H2B
        PROGRAM_FILES_COMMONX86 = &H2C
        COMMON_TEMPLATES = &H2D
        COMMON_DOCUMENTS = &H2E
        COMMON_ADMINTOOLS = &H2F
        ADMINTOOLS = &H30
        CONNECTIONS = &H31
        COMMON_MUSIC = &H35
        COMMON_PICTURES = &H36
        COMMON_VIDEO = &H37
        RESOURCES = &H38
        RESOURCES_LOCALIZED = &H39
        COMMON_OEM_LINKS = &H3A
        CDBURN_AREA = &H3B
        COMPUTERSNEARME = &H3D
        FLAG_PER_USER_INIT = &H800
        FLAG_NO_ALIAS = &H1000
        FLAG_DONT_VERIFY = &H4000
        FLAG_CREATE = &H8000
        FLAG_MASK = &HFF00
    End Enum

    <Flags()> Public Enum SHCONTF
        EMPTY = 0
        FOLDERS = &H20
        NONFOLDERS = &H40
        INCLUDEHIDDEN = &H80
        INIT_ON_FIRST_NEXT = &H100
        NETPRINTERSRCH = &H200
        SHAREABLE = &H400
        STORAGE = &H800
    End Enum

    <Flags()> Public Enum SHGDN
        NORMAL = 0
        INFOLDER = 1
        FORADDRESSBAR = 16384
        FORPARSING = 32768
    End Enum

    <Flags()> Public Enum ILD
        NORMAL = &H0
        TRANSPARENT = &H1
        BLEND25 = &H2
        SELECTED = &H4
        MASK = &H10
        IMAGE = &H20
        ROP = &H40
        PRESERVEALPHA = &H1000
        SCALE = &H2000
        DPISCALE = &H4000
    End Enum

    Public Enum ILS
        NORMAL = (&H0)
        GLOW = (&H1)
        SHADOW = (&H2)
        SATURATE = (&H4)
        ALPHA = (&H8)
    End Enum

    <Flags()> Public Enum SLR
        NO_UI = &H1
        ANY_MATCH = &H2
        UPDATE = &H4
        NOUPDATE = &H8
        NOSEARCH = &H10
        NOTRACK = &H20
        NOLINKINFO = &H40
        INVOKE_MSI = &H80
        NO_UI_WITH_MSG_PUMP = &H101
    End Enum

    <Flags()> Public Enum SLGP
        SHORTPATH = &H1
        UNCPRIORITY = &H2
        RAWPATH = &H4
    End Enum

    <Flags()> Public Enum SHGNLI
        PIDL = 1
        PREFIXNAME = 2
        NOUNIQUE = 4
        NOLNK = 8
    End Enum

    Public Shared IID_IMalloc As New Guid("{00000002-0000-0000-C000-000000000046}")
    Public Shared IID_IShellFolder As New Guid("{000214E6-0000-0000-C000-000000000046}")
    Public Shared IID_IFolderFilterSite As New Guid("{C0A651F5-B48B-11d2-B5ED-006097C686F6}")
    Public Shared IID_IFolderFilter As New Guid("{9CC22886-DC8E-11d2-B1D0-00C04F8EEB3E}")
    Public Shared DesktopGUID As New Guid("{00021400-0000-0000-C000-000000000046}")
    Public Shared CLSID_ShellLink As New Guid("{00021401-0000-0000-C000-000000000046}")
    Public Shared CLSID_InternetShortcut As New Guid("{FBF23B40-E3F0-101B-8488-00AA003E56F8}")
    Public Shared IID_IDropTarget As New Guid("{00000122-0000-0000-C000-000000000046}")
    Public Shared IID_IDataObject As New Guid("{0000010e-0000-0000-C000-000000000046}")

    <StructLayout(LayoutKind.Sequential, CharSet:=CharSet.Auto)> Public Structure SHFILEINFO
        Public hIcon As IntPtr
        Public iIcon As Integer
        Public dwAttributes As Integer
        <MarshalAs(UnmanagedType.ByValTStr, SizeConst:=MAX_PATH)> Public szDisplayName As String
        <MarshalAs(UnmanagedType.ByValTStr, SizeConst:=80)> Public szTypeName As String
    End Structure

    Private Shared shfitmp As SHFILEINFO
    Public Shared cbFileInfo As Integer = Marshal.SizeOf(shfitmp.GetType())

    <StructLayout(LayoutKind.Explicit)> Public Structure STRRET
        <FieldOffset(0)> Public uType As Integer
        <FieldOffset(4)> Public pOleStr As Integer
        <FieldOffset(4)> Public uOffset As Integer
        <FieldOffset(4)> Public pStr As Integer
    End Structure

    <StructLayoutAttribute(LayoutKind.Sequential, CharSet:=CharSet.Auto)> Public Structure WIN32_FIND_DATA
        Public dwFileAttributes As Integer
        Public ftCreationTime As System.Runtime.InteropServices.ComTypes.FILETIME
        Public ftLastAccessTime As System.Runtime.InteropServices.ComTypes.FILETIME
        Public ftLastWriteTime As System.Runtime.InteropServices.ComTypes.FILETIME
        Public nFileSizeHigh As Integer
        Public nFileSizeLow As Integer
        Public dwReserved0 As Integer
        Public dwReserved1 As Integer
        <MarshalAs(UnmanagedType.ByValTStr, SizeConst:=MAX_PATH)> Public cFileName As String
        <MarshalAs(UnmanagedType.ByValTStr, SizeConst:=14)> Public cAlternateFileName As String
    End Structure

    Declare Auto Function SHGetMalloc Lib "shell32" (ByRef pMalloc As IMalloc) As Integer
    Declare Auto Function SHGetDesktopFolder Lib "shell32.dll" (ByRef ppshf As IShellFolder) As Integer
    Declare Function SHGetSpecialFolderLocation Lib "Shell32" (ByVal hWndOwner As Integer, ByVal csidl As Integer, ByRef ppidl As IntPtr) As Integer
    Declare Auto Function SHGetFileInfo Lib "shell32" (ByVal pszPath As String, ByVal dwFileAttributes As Integer, ByRef sfi As SHFILEINFO, ByVal cbsfi As Integer, ByVal uFlags As Integer) As IntPtr
    Declare Auto Function SHGetFileInfo Lib "shell32" (ByVal ppidl As IntPtr, ByVal dwFileAttributes As Integer, ByRef sfi As SHFILEINFO, ByVal cbsfi As Integer, ByVal uFlags As Integer) As IntPtr
    Declare Ansi Function SHGetNewLinkInfo Lib "shell32" Alias "SHGetNewLinkInfoA" (ByVal pszLinkTo As String, ByVal pszDir As String, <Out(), MarshalAs(UnmanagedType.LPStr)> ByVal pszName As StringBuilder, ByRef pfMustCopy As Boolean, ByVal uFlags As SHGNLI) As Integer
    Declare Ansi Function SHGetNewLinkInfo Lib "shell32" Alias "SHGetNewLinkInfoA" (ByVal pszLinkTo As IntPtr, ByVal pszDir As String, <Out(), MarshalAs(UnmanagedType.LPStr)> ByVal pszName As StringBuilder, ByRef pfMustCopy As Boolean, ByVal uFlags As SHGNLI) As Integer
    Declare Auto Function ILIsParent Lib "shell32" Alias "#23" (ByVal pidlParent As IntPtr, ByVal pidlBelow As IntPtr, ByVal fImmediate As Boolean) As Boolean
    Declare Auto Function ILIsEqual Lib "shell32" Alias "#21" (ByVal pidl1 As IntPtr, ByVal pidl2 As IntPtr) As Boolean
    Declare Auto Function StrRetToBSTR Lib "shlwapi.dll" (ByRef pstr As STRRET, ByVal pidl As IntPtr, <MarshalAs(UnmanagedType.BStr)> ByRef pbstr As String) As Integer
    Declare Auto Function StrRetToBuf Lib "shlwapi.dll" (ByVal pstr As IntPtr, ByVal pidl As IntPtr, ByVal pszBuf As StringBuilder, <MarshalAs(UnmanagedType.U4)> ByVal cchBuf As Integer) As Integer
    Declare Auto Function SendMessage Lib "user32" (ByVal hWnd As IntPtr, ByVal wMsg As Integer, ByVal wParam As Integer, ByVal lParam As IntPtr) As Integer
    Declare Function ImageList_GetIconSize Lib "comctl32" (ByVal himl As IntPtr, ByRef cx As Integer, ByRef cy As Integer) As Integer
    Declare Auto Function ImageList_ReplaceIcon Lib "comctl32" (ByVal hImageList As IntPtr, ByVal IconIndex As Integer, ByVal hIcon As IntPtr) As Integer
    Declare Function ImageList_GetIcon Lib "comctl32" (ByVal himl As IntPtr, ByVal i As Integer, ByVal flags As Integer) As IntPtr
    Declare Function ImageList_Draw Lib "comctl32" (ByVal hIml As IntPtr, ByVal indx As Integer, ByVal hdcDst As IntPtr, ByVal x As Integer, ByVal y As Integer, ByVal fStyle As Integer) As Integer
    Declare Function DestroyIcon Lib "user32.dll" (ByVal hIcon As IntPtr) As Integer

    '<StructLayout(LayoutKind.Sequential)> Private Structure RECT
    'Dim left As Integer
    'Dim top As Integer
    'Dim right As Integer
    'Dim bottom As Integer
    'End Structure

    '<StructLayout(LayoutKind.Sequential)> Public Structure POINT
    'Dim x As Integer
    'Dim y As Integer
    'End Structure

    <ComImport(), Guid("00000000-0000-0000-C000-000000000046"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)> Interface IUnknown
        <PreserveSig()> Function QueryInterface(ByRef riid As Guid, ByRef pVoid As IntPtr) As Integer
        <PreserveSig()> Function AddRef() As Integer
        <PreserveSig()> Function Release() As Integer
    End Interface

    <ComImport(), Guid("00000002-0000-0000-C000-000000000046"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)> Public Interface IMalloc
        <PreserveSig()> Function Alloc(ByVal cb As Integer) As IntPtr
        <PreserveSig()> Function Realloc(ByVal pv As IntPtr, ByVal cb As Integer) As IntPtr
        <PreserveSig()> Sub Free(ByVal pv As IntPtr)
        <PreserveSig()> Function GetSize(ByVal pv As IntPtr) As Integer
        <PreserveSig()> Function DidAlloc(ByVal pv As IntPtr) As Int16
        <PreserveSig()> Sub HeapMinimize()
    End Interface

    <ComImportAttribute(), InterfaceTypeAttribute(ComInterfaceType.InterfaceIsIUnknown), Guid("000214E6-0000-0000-C000-000000000046")> Public Interface IShellFolder
        <PreserveSig()> Function ParseDisplayName(ByVal hwndOwner As Integer, ByVal pbcReserved As IntPtr, <MarshalAs(UnmanagedType.LPWStr)> ByVal lpszDisplayName As String, ByRef pchEaten As Integer, ByRef ppidl As IntPtr, ByRef pdwAttributes As Integer) As Integer
        <PreserveSig()> Function EnumObjects(ByVal hwndOwner As Integer, <MarshalAs(UnmanagedType.U4)> ByVal grfFlags As SHCONTF, ByRef ppenumIDList As IEnumIDList) As Integer
        <PreserveSig()> Function BindToObject(ByVal pidl As IntPtr, ByVal pbcReserved As IntPtr, ByRef riid As Guid, ByRef ppvOut As IShellFolder) As Integer
        <PreserveSig()> Function BindToStorage(ByVal pidl As IntPtr, ByVal pbcReserved As IntPtr, ByRef riid As Guid, ByVal ppvObj As IntPtr) As Integer
        <PreserveSig()> Function CompareIDs(ByVal lParam As IntPtr, ByVal pidl1 As IntPtr, ByVal pidl2 As IntPtr) As Integer
        <PreserveSig()> Function CreateViewObject(ByVal hwndOwner As IntPtr, ByRef riid As Guid, ByRef ppvOut As IUnknown) As Integer
        <PreserveSig()> Function GetAttributesOf(ByVal cidl As Integer, <MarshalAs(UnmanagedType.LPArray, sizeparamindex:=0)> ByVal apidl() As IntPtr, ByRef rgfInOut As SFGAO) As Integer
        <PreserveSig()> Function GetUIObjectOf(ByVal hwndOwner As IntPtr, ByVal cidl As Integer, <MarshalAs(UnmanagedType.LPArray, sizeparamindex:=0)> ByVal apidl() As IntPtr, ByRef riid As Guid, ByRef prgfInOut As Integer, ByRef ppvOut As IUnknown) As Integer
        <PreserveSig()> Function GetDisplayNameOf(ByVal pidl As IntPtr, <MarshalAs(UnmanagedType.U4)> ByVal uFlags As SHGDN, ByVal lpName As IntPtr) As Integer
        <PreserveSig()> Function SetNameOf(ByVal hwndOwner As Integer, ByVal pidl As IntPtr, <MarshalAs(UnmanagedType.LPWStr)> ByVal lpszName As String, <MarshalAs(UnmanagedType.U4)> ByVal uFlags As SHCONTF, ByRef ppidlOut As IntPtr) As Integer
    End Interface

    <ComImportAttribute(), InterfaceTypeAttribute(ComInterfaceType.InterfaceIsIUnknown), Guid("000214F2-0000-0000-C000-000000000046")> Public Interface IEnumIDList
        <PreserveSig()> Function GetNext(ByVal celt As Integer, ByRef rgelt As IntPtr, ByRef pceltFetched As Integer) As Integer
        <PreserveSig()> Function Skip(ByVal celt As Integer) As Integer
        <PreserveSig()> Function Reset() As Integer
        <PreserveSig()> Function Clone(ByRef ppenum As IEnumIDList) As Integer
    End Interface

    <ComImportAttribute(), InterfaceTypeAttribute(ComInterfaceType.InterfaceIsIUnknown), Guid("0000010B-0000-0000-C000-000000000046")> Public Interface IPersistFile
        Sub GetClassID(<Out()> ByRef pClassID As Guid)
        <PreserveSig()> Function IsDirty() As Integer
        Function Load(<MarshalAs(UnmanagedType.LPWStr)> ByVal pszFileName As String, ByVal dwMode As Integer) As Integer
        Function Save(<MarshalAs(UnmanagedType.LPWStr)> ByVal pszFileName As String, <MarshalAs(UnmanagedType.Bool)> ByVal fRemember As Boolean) As Integer
        Function SaveCompleted(<MarshalAs(UnmanagedType.LPWStr)> ByVal pszFileName As String) As Integer
        Function GetCurFile(<Out(), MarshalAs(UnmanagedType.LPWStr)> ByRef ppszFileName As String) As Integer
    End Interface

    <ComImportAttribute(), InterfaceType(ComInterfaceType.InterfaceIsIUnknown), Guid("000214EE-0000-0000-C000-000000000046")> Public Interface IShellLink
        Function GetPath(<Out(), MarshalAs(UnmanagedType.LPStr)> ByVal pszFile As StringBuilder, ByVal cchMaxPath As Integer, <Out()> ByRef pfd As WIN32_FIND_DATA, ByVal fFlags As SLGP) As Integer
        Function GetIDList(ByRef ppidl As IntPtr) As Integer
        Function SetIDList(ByVal pidl As IntPtr) As Integer
        Function GetDescription(<Out(), MarshalAs(UnmanagedType.LPStr)> ByVal pszName As StringBuilder, ByVal cchMaxName As Integer) As Integer
        Function SetDescription(<MarshalAs(UnmanagedType.LPStr)> ByVal pszName As String) As Integer
        Function GetWorkingDirectory(<Out(), MarshalAs(UnmanagedType.LPStr)> ByVal pszDir As StringBuilder, ByVal cchMaxPath As Integer) As Integer
        Function SetWorkingDirectory(<MarshalAs(UnmanagedType.LPStr)> ByVal pszDir As String) As Integer
        Function GetArguments(<Out(), MarshalAs(UnmanagedType.LPStr)> ByVal pszArgs As StringBuilder, ByVal cchMaxPath As Integer) As Integer
        Function SetArguments(<MarshalAs(UnmanagedType.LPStr)> ByVal pszArgs As String) As Integer
        Function GetHotkey(ByRef pwHotkey As Short) As Integer
        Function SetHotkey(ByVal wHotkey As Short) As Integer
        Function GetShowCmd(ByRef piShowCmd As Integer) As Integer
        Function SetShowCmd(ByVal iShowCmd As Integer) As Integer
        Function GetIconLocation(<Out(), MarshalAs(UnmanagedType.LPStr)> ByVal pszIconPath As StringBuilder, ByVal cchIconPath As Integer, ByRef piIcon As Integer) As Integer
        Function SetIconLocation(<MarshalAs(UnmanagedType.LPStr)> ByVal pszIconPath As String, ByVal iIcon As Integer) As Integer
        Function SetRelativePath(<MarshalAs(UnmanagedType.LPStr)> ByVal pszPathRel As String, ByVal dwReserved As Integer) As Integer
        Function Resolve(ByVal hwnd As IntPtr, ByVal fFlags As SLR) As Integer
        Function SetPath(<MarshalAs(UnmanagedType.LPStr)> ByVal pszFile As String) As Integer
    End Interface

    Public Shared Function GetSpecialFolderPath(ByVal hWnd As IntPtr, ByVal csidl As Integer) As String
        Dim res As IntPtr
        Dim ppidl As IntPtr
        ppidl = GetSpecialFolderLocation(csidl)
        Dim shfi As New SHFILEINFO()
        Dim uFlags As Integer = SHGFI.PIDL Or SHGFI.DISPLAYNAME Or SHGFI.TYPENAME
        Dim dwAttr As Integer = 0
        res = SHGetFileInfo(ppidl, dwAttr, shfi, cbFileInfo, uFlags)
        Marshal.FreeCoTaskMem(ppidl)
        Return shfi.szDisplayName & "  (" & shfi.szTypeName & ")"
    End Function

    Public Shared Function GetSpecialFolderLocation(ByVal csidl As Integer) As IntPtr
        Dim rVal As IntPtr
        Dim res As Integer
        res = SHGetSpecialFolderLocation(0, csidl, rVal)
        Return rVal
    End Function

    Public Shared Function IsXpOrAbove() As Boolean
        Dim rVal As Boolean = False
        If Environment.OSVersion.Version.Major > 5 Then
            rVal = True
        ElseIf Environment.OSVersion.Version.Major = 5 AndAlso Environment.OSVersion.Version.Minor >= 1 Then
            rVal = True
        End If
        Return rVal
    End Function

    Public Shared Function Is2KOrAbove() As Boolean
        If Environment.OSVersion.Version.Major >= 5 Then
            Return True
        Else
            Return False
        End If
    End Function

End Class
