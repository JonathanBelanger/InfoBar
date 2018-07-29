Imports InfoBar.ShellDll

Friend Class ShItem
    Implements IDisposable, IComparable

    Private Shared m_strSystemFolder As String
    Private Shared m_strMyComputer As String
    Private Shared m_strMyDocuments As String
    Private Shared DesktopBase As ShItem
    Private Shared OpenFolderIconIndex As Integer = -1
    Private Shared XPorAbove As Boolean
    Private Shared Win2KOrAbove As Boolean
    Private Shared m_DeskTopDirectory As ShItem
    Private m_Folder As IShellFolder
    Private m_Pidl As IntPtr
    Private m_DisplayName As String = ""
    Private m_Path As String
    Private m_TypeName As String
    Private m_Parent As ShItem
    Private m_IconIndexNormal As Integer
    Private m_IconIndexOpen As Integer
    Private m_IsBrowsable As Boolean
    Private m_IsFileSystem As Boolean
    Private m_IsFolder As Boolean
    Private m_HasSubFolders As Boolean
    Private m_IsLink As Boolean
    Private m_IsDisk As Boolean
    Private m_IsShared As Boolean
    Private m_IsHidden As Boolean
    Private m_IsNetWorkDrive As Boolean
    Private m_IsRemovable As Boolean
    Private m_IsReadOnly As Boolean
    Private m_Attributes As SFGAO
    Private m_SortFlag As Integer
    Private m_Directories As ArrayList
    Private m_XtrInfo As Boolean
    Private m_LastWriteTime As DateTime
    Private m_CreationTime As DateTime
    Private m_LastAccessTime As DateTime
    Private m_Length As Long
    Private m_HasDispType As Boolean
    Private m_IsReadOnlySetup As Boolean
    Private m_cPidl As cPidl
    Private m_Disposed As Boolean

    Public Overloads Sub Dispose() Implements IDisposable.Dispose
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub

    Protected Overridable Overloads Sub Dispose(ByVal disposing As Boolean)
        If Not (m_Disposed) Then
            m_Disposed = True
            If Not IsNothing(m_Folder) Then
                Marshal.ReleaseComObject(m_Folder)
            End If
            If Not m_Pidl.Equals(IntPtr.Zero) Then
                Marshal.FreeCoTaskMem(m_Pidl)
            End If
        End If
    End Sub

    Protected Overrides Sub Finalize()
        Dispose(False)
    End Sub

    Private Sub New(ByVal folder As IShellFolder, ByVal pidl As IntPtr, ByVal parent As ShItem)
        If IsNothing(DesktopBase) Then
            DesktopBase = New ShItem()
        End If
        m_Parent = parent
        m_Pidl = concatPidls(parent.PIDL, pidl)
        SetUpAttributes(folder, pidl)
        m_IconIndexNormal = -1
        m_IconIndexOpen = -1
        If m_IsFolder Then
            Dim HR As Integer
            HR = folder.BindToObject(pidl, IntPtr.Zero, IID_IShellFolder, m_Folder)
        End If
    End Sub

    Private Sub New()
        Dim HR As Integer
        Dim tmpPidl As IntPtr
        HR = SHGetSpecialFolderLocation(0, CSIDL.DRIVES, tmpPidl)
        Dim shfi As New SHFILEINFO()
        Dim dwflag As Integer = SHGFI.DISPLAYNAME Or SHGFI.TYPENAME Or SHGFI.PIDL
        Dim dwAttr As Integer = 0
        SHGetFileInfo(tmpPidl, dwAttr, shfi, cbFileInfo, dwflag)
        m_strSystemFolder = shfi.szTypeName
        m_strMyComputer = shfi.szDisplayName
        Marshal.FreeCoTaskMem(tmpPidl)
        XPorAbove = ShellDll.IsXpOrAbove()
        Win2KOrAbove = ShellDll.Is2KOrAbove
        m_Path = "::{" & DesktopGUID.ToString & "}"
        m_IsFolder = True
        m_HasSubFolders = True
        m_IsBrowsable = False
        HR = SHGetDesktopFolder(m_Folder)
        m_Pidl = GetSpecialFolderLocation(CSIDL.DESKTOP)
        dwflag = SHGFI.DISPLAYNAME Or SHGFI.TYPENAME Or SHGFI.SYSICONINDEX Or SHGFI.PIDL
        dwAttr = 0
        SHGetFileInfo(m_Pidl, dwAttr, shfi, cbFileInfo, dwflag)
        m_DisplayName = shfi.szDisplayName
        m_TypeName = strSystemFolder
        m_IconIndexNormal = shfi.iIcon
        m_IconIndexOpen = shfi.iIcon
        m_HasDispType = True
        m_IsReadOnly = False
        m_IsReadOnlySetup = True
        Dim pchEaten As Integer
        tmpPidl = IntPtr.Zero
        HR = m_Folder.ParseDisplayName(Nothing, Nothing, "::{450d8fba-ad25-11d0-98a8-0800361b1103}", pchEaten, tmpPidl, Nothing)
        shfi = New SHFILEINFO()
        dwflag = SHGFI.DISPLAYNAME Or SHGFI.TYPENAME Or SHGFI.PIDL
        dwAttr = 0
        SHGetFileInfo(tmpPidl, dwAttr, shfi, cbFileInfo, dwflag)
        m_strMyDocuments = shfi.szDisplayName
        Marshal.FreeCoTaskMem(tmpPidl)
        m_SortFlag = ComputeSortFlag()
        DesktopBase = Me
        m_DeskTopDirectory = New ShItem(CSIDL.DESKTOPDIRECTORY)
    End Sub

    Sub New(ByVal ID As CSIDL)
        If IsNothing(DesktopBase) Then
            DesktopBase = New ShItem()
        End If
        Dim HR As Integer
        If ID = CSIDL.MYDOCUMENTS Then
            Dim pchEaten As Integer
            HR = DesktopBase.m_Folder.ParseDisplayName(Nothing, Nothing, "::{450d8fba-ad25-11d0-98a8-0800361b1103}", pchEaten, m_Pidl, Nothing)
        Else
            HR = SHGetSpecialFolderLocation(0, ID, m_Pidl)
        End If
        If HR = NOERROR Then
            Dim pParent As IShellFolder
            Dim relPidl As IntPtr = IntPtr.Zero
            pParent = GetParentOf(m_Pidl, relPidl)
            SetUpAttributes(pParent, relPidl)
            m_IconIndexNormal = -1
            m_IconIndexOpen = -1
            If m_IsFolder Then
                HR = pParent.BindToObject(relPidl, IntPtr.Zero, IID_IShellFolder, m_Folder)
                If HR <> NOERROR Then
                    Marshal.ThrowExceptionForHR(HR)
                End If
            End If
            Marshal.ReleaseComObject(pParent)
            If PidlCount(m_Pidl) > 1 Then Marshal.FreeCoTaskMem(relPidl)
        Else
            Marshal.ThrowExceptionForHR(HR)
        End If
    End Sub

    Sub New(ByVal path As String)
        If IsNothing(DesktopBase) Then
            DesktopBase = New ShItem()
        End If
        Dim HR As Integer
        HR = DesktopBase.m_Folder.ParseDisplayName(0, IntPtr.Zero, path, 0, m_Pidl, 0)
        If Not HR = NOERROR Then Marshal.ThrowExceptionForHR(HR)
        Dim pParent As IShellFolder
        Dim relPidl As IntPtr = IntPtr.Zero
        pParent = GetParentOf(m_Pidl, relPidl)
        SetUpAttributes(pParent, relPidl)
        m_IconIndexNormal = -1
        m_IconIndexOpen = -1
        If m_IsFolder Then
            HR = pParent.BindToObject(relPidl, IntPtr.Zero, IID_IShellFolder, m_Folder)
            If HR <> NOERROR Then
                Marshal.ThrowExceptionForHR(HR)
            End If
        End If
        Marshal.ReleaseComObject(pParent)
        If PidlCount(m_Pidl) > 1 Then
            Marshal.FreeCoTaskMem(relPidl)
        End If
    End Sub

    Sub New(ByVal FoldBytes() As Byte, ByVal ItemBytes() As Byte)
        If IsNothing(DesktopBase) Then
            DesktopBase = New ShItem()
        End If
        Dim pParent As IShellFolder = MakeFolderFromBytes(FoldBytes)
        If IsNothing(pParent) Then
            GoTo XIT
        End If
        Dim ipParent As IntPtr = cPidl.BytesToPidl(FoldBytes)
        Dim ipItem As IntPtr = cPidl.BytesToPidl(ItemBytes)
        If ipParent.Equals(IntPtr.Zero) Or ipItem.Equals(IntPtr.Zero) Then
            GoTo XIT
        End If
        m_Pidl = concatPidls(ipParent, ipItem)
        SetUpAttributes(pParent, ipItem)
        m_IconIndexNormal = -1
        m_IconIndexOpen = -1
        If m_IsFolder Then
            Dim HR As Integer
            HR = pParent.BindToObject(ipItem, IntPtr.Zero, IID_IShellFolder, m_Folder)
        End If
XIT:
        If Not ipParent.Equals(IntPtr.Zero) Then
            Marshal.FreeCoTaskMem(ipParent)
        End If
        If Not ipItem.Equals(IntPtr.Zero) Then
            Marshal.FreeCoTaskMem(ipItem)
        End If
    End Sub

    Public Shared Function IsValidPidl(ByVal b() As Byte) As Boolean
        IsValidPidl = False
        Dim bMax As Integer = b.Length - 1
        If bMax < 1 Then Exit Function
        Dim cb As Integer = b(0) + (b(1) * 256)
        Dim indx As Integer = 0
        Do While cb > 0
            If (indx + cb + 1) > bMax Then Exit Function
            indx += cb
            cb = b(indx) + (b(indx + 1) * 256)
        Loop
        IsValidPidl = True
    End Function

    Public Shared Function MakeFolderFromBytes(ByVal b As Byte()) As ShellDll.IShellFolder
        MakeFolderFromBytes = Nothing
        If Not IsValidPidl(b) Then Return Nothing
        If b.Length = 2 AndAlso ((b(0) = 0) And (b(1) = 0)) Then
            Return DesktopBase.Folder
        ElseIf b.Length = 0 Then
            Return DesktopBase.Folder
        Else
            Dim ptr As IntPtr = Marshal.AllocCoTaskMem(b.Length)
            If ptr.Equals(IntPtr.Zero) Then Return Nothing
            Marshal.Copy(b, 0, ptr, b.Length)
            Dim hr As Integer = DesktopBase.Folder.BindToObject(ptr, IntPtr.Zero, IID_IShellFolder, MakeFolderFromBytes)
            If hr <> 0 Then MakeFolderFromBytes = Nothing
            Marshal.FreeCoTaskMem(ptr)
        End If
    End Function

    Private Shared Function GetParentOf(ByVal pidl As IntPtr, ByRef relPidl As IntPtr) As IShellFolder
        GetParentOf = Nothing
        Dim HR As Integer
        Dim itemCnt As Integer = PidlCount(pidl)
        If itemCnt = 1 Then
            HR = SHGetDesktopFolder(GetParentOf)
            relPidl = pidl
        Else
            Dim tmpPidl As IntPtr
            tmpPidl = TrimPidl(pidl, relPidl)
            HR = DesktopBase.m_Folder.BindToObject(tmpPidl, IntPtr.Zero, IID_IShellFolder, GetParentOf)
            Marshal.FreeCoTaskMem(tmpPidl)
        End If
        If Not HR = NOERROR Then Marshal.ThrowExceptionForHR(HR)
    End Function

    Private Sub SetUpAttributes(ByVal folder As IShellFolder, ByVal pidl As IntPtr)
        Dim attrFlag As SFGAO
        attrFlag = SFGAO.BROWSABLE
        attrFlag = attrFlag Or SFGAO.FILESYSTEM
        attrFlag = attrFlag Or SFGAO.HASSUBFOLDER
        attrFlag = attrFlag Or SFGAO.FOLDER
        attrFlag = attrFlag Or SFGAO.LINK
        attrFlag = attrFlag Or SFGAO.SHARE
        attrFlag = attrFlag Or SFGAO.HIDDEN
        attrFlag = attrFlag Or SFGAO.REMOVABLE
        attrFlag = attrFlag Or SFGAO.CANCOPY
        attrFlag = attrFlag Or SFGAO.CANDELETE
        attrFlag = attrFlag Or SFGAO.CANLINK
        attrFlag = attrFlag Or SFGAO.CANMOVE
        attrFlag = attrFlag Or SFGAO.DROPTARGET
        Dim aPidl(0) As IntPtr
        aPidl(0) = pidl
        folder.GetAttributesOf(1, aPidl, attrFlag)
        m_Attributes = attrFlag
        m_IsBrowsable = CBool(attrFlag And SFGAO.BROWSABLE)
        m_IsFileSystem = CBool(attrFlag And SFGAO.FILESYSTEM)
        m_HasSubFolders = CBool(attrFlag And SFGAO.HASSUBFOLDER)
        m_IsFolder = CBool(attrFlag And SFGAO.FOLDER)
        m_IsLink = CBool(attrFlag And SFGAO.LINK)
        m_IsShared = CBool(attrFlag And SFGAO.SHARE)
        m_IsHidden = CBool(attrFlag And SFGAO.HIDDEN)
        m_IsRemovable = CBool(attrFlag And SFGAO.REMOVABLE)
        Dim strr As IntPtr = Marshal.AllocCoTaskMem(MAX_PATH * 2 + 4)
        Marshal.WriteInt32(strr, 0, 0)
        Dim buf As New StringBuilder(MAX_PATH)
        Dim itemflags As SHGDN = SHGDN.FORPARSING
        folder.GetDisplayNameOf(pidl, itemflags, strr)
        Dim HR As Integer = StrRetToBuf(strr, pidl, buf, MAX_PATH)
        Marshal.FreeCoTaskMem(strr)
        If HR = NOERROR Then
            m_Path = buf.ToString
            If m_IsFolder AndAlso m_IsFileSystem AndAlso XPorAbove Then
                aPidl(0) = pidl
                attrFlag = SFGAO.STREAM
                folder.GetAttributesOf(1, aPidl, attrFlag)
        If attrFlag And SFGAO.STREAM Then
          m_IsFolder = False
        End If
            End If
            If m_Path.Length = 3 AndAlso m_Path.Substring(1).Equals(":\") Then
                m_IsDisk = True
            End If
        Else
            Marshal.ThrowExceptionForHR(HR)
        End If
    End Sub

    Public Shared Function GetCShItem(ByVal path As String) As ShItem
        GetCShItem = Nothing
        Dim HR As Integer
        Dim tmpPidl As IntPtr
        HR = GetDeskTop.Folder.ParseDisplayName(0, IntPtr.Zero, path, 0, tmpPidl, 0)
        If HR = 0 Then
            GetCShItem = FindCShItem(tmpPidl)
            If IsNothing(GetCShItem) Then
                Try
                    GetCShItem = New ShItem(path)
                Catch
                    GetCShItem = Nothing
                End Try
            End If
        End If
        If Not tmpPidl.Equals(IntPtr.Zero) Then
            Marshal.FreeCoTaskMem(tmpPidl)
        End If
    End Function

    Public Shared Function GetCShItem(ByVal ID As CSIDL) As ShItem
        GetCShItem = Nothing
        If ID = CSIDL.DESKTOP Then
            Return GetDeskTop()
        End If
        Dim HR As Integer
        Dim tmpPidl As IntPtr
        If ID = CSIDL.MYDOCUMENTS Then
            Dim pchEaten As Integer
            HR = GetDeskTop.Folder.ParseDisplayName(Nothing, Nothing, "::{450d8fba-ad25-11d0-98a8-0800361b1103}", pchEaten, tmpPidl, Nothing)
        Else
            HR = SHGetSpecialFolderLocation(0, ID, tmpPidl)
        End If
        If HR = NOERROR Then
            GetCShItem = FindCShItem(tmpPidl)
            If IsNothing(GetCShItem) Then
                Try
                    GetCShItem = New ShItem(ID)
                Catch
                    GetCShItem = Nothing
                End Try
            End If
        End If
        If Not tmpPidl.Equals(IntPtr.Zero) Then
            Marshal.FreeCoTaskMem(tmpPidl)
        End If
    End Function

    Public Shared Function GetCShItem(ByVal FoldBytes() As Byte, ByVal ItemBytes() As Byte) As ShItem
        GetCShItem = Nothing
        Dim b() As Byte = cPidl.JoinPidlBytes(FoldBytes, ItemBytes)
        If IsNothing(b) Then Exit Function
        Dim thisPidl As IntPtr = Marshal.AllocCoTaskMem(b.Length)
        If thisPidl.Equals(IntPtr.Zero) Then Return Nothing
        Marshal.Copy(b, 0, thisPidl, b.Length)
        GetCShItem = FindCShItem(thisPidl)
        Marshal.FreeCoTaskMem(thisPidl)
        If IsNothing(GetCShItem) Then
            Try
                GetCShItem = New ShItem(FoldBytes, ItemBytes)
            Catch
            End Try
        End If
        If GetCShItem.PIDL.Equals(IntPtr.Zero) Then GetCShItem = Nothing
    End Function

    Public Shared Function FindCShItem(ByVal b() As Byte) As ShItem
        If Not IsValidPidl(b) Then Return Nothing
        Dim thisPidl As IntPtr = Marshal.AllocCoTaskMem(b.Length)
        If thisPidl.Equals(IntPtr.Zero) Then Return Nothing
        Marshal.Copy(b, 0, thisPidl, b.Length)
        FindCShItem = FindCShItem(thisPidl)
        Marshal.FreeCoTaskMem(thisPidl)
    End Function

    Public Shared Function FindCShItem(ByVal ptr As IntPtr) As ShItem
        FindCShItem = Nothing
        Dim BaseItem As ShItem = ShItem.GetDeskTop
        Dim CSI As ShItem
        Dim FoundIt As Boolean = False
        Do Until FoundIt
            For Each CSI In BaseItem.GetDirectories
                If IsAncestorOf(CSI.PIDL, ptr) Then
                    If ShItem.IsEqual(CSI.PIDL, ptr) Then
                        Return CSI
                    Else
                        BaseItem = CSI
                        FoundIt = True
                        Exit For
                    End If
                End If
            Next
            If Not FoundIt Then Return Nothing
            If Not IsAncestorOf(BaseItem.PIDL, ptr, True) Then
                FoundIt = False
            Else
                For Each CSI In BaseItem.GetItems
                    If ShItem.IsEqual(CSI.PIDL, ptr) Then
                        Return CSI
                    End If
                Next
                Return Nothing
            End If
        Loop
    End Function

    Private Function ComputeSortFlag() As Integer
        Dim rVal As Integer = 0
        If m_IsDisk Then rVal = &H100000
        If m_TypeName.Equals(strSystemFolder) Then
            If Not m_IsBrowsable Then
                rVal = rVal Or &H10000
                If m_strMyDocuments.Equals(m_DisplayName) Then
                    rVal = rVal Or &H1
                End If
            Else
                rVal = rVal Or &H1000
            End If
        End If
        If m_IsFolder Then rVal = rVal Or &H100
        Return rVal
    End Function

    Public Overridable Overloads Function CompareTo(ByVal obj As Object) As Integer _
            Implements IComparable.CompareTo
        If IsNothing(obj) Then Return 1
        Dim Other As ShItem = obj
        If Not m_HasDispType Then SetDispType()
        Dim cmp As Integer = Other.SortFlag - m_SortFlag
        If cmp <> 0 Then
            Return cmp
        Else
            If m_IsDisk Then
                Return String.Compare(m_Path, Other.Path)
            Else
                Return String.Compare(m_DisplayName, Other.DisplayName)
            End If
        End If
    End Function

    Public Shared ReadOnly Property strMyComputer() As String
        Get
            Return m_strMyComputer
        End Get
    End Property

    Public Shared ReadOnly Property strSystemFolder() As String
        Get
            Return m_strSystemFolder
        End Get
    End Property

    Public Shared ReadOnly Property DesktopDirectoryPath() As String
        Get
            Return m_DeskTopDirectory.Path
        End Get
    End Property

    Public ReadOnly Property PIDL() As IntPtr
        Get
            Return m_Pidl
        End Get
    End Property

    Public ReadOnly Property Folder() As IShellFolder
        Get
            Return m_Folder
        End Get
    End Property

    Public ReadOnly Property Path() As String
        Get
            Return m_Path
        End Get
    End Property

    Public ReadOnly Property Parent() As ShItem
        Get
            Return m_Parent
        End Get
    End Property

    Public ReadOnly Property Attributes() As SFGAO
        Get
            Return m_Attributes
        End Get
    End Property

    Public ReadOnly Property IsBrowsable() As Boolean
        Get
            Return m_IsBrowsable
        End Get
    End Property

    Public ReadOnly Property IsFileSystem() As Boolean
        Get
            Return m_IsFileSystem
        End Get
    End Property

    Public ReadOnly Property IsFolder() As Boolean
        Get
            Return m_IsFolder
        End Get
    End Property

    Public ReadOnly Property HasSubFolders() As Boolean
        Get
            Return m_HasSubFolders
        End Get
    End Property

    Public ReadOnly Property IsDisk() As Boolean
        Get
            Return m_IsDisk
        End Get
    End Property

    Public ReadOnly Property IsLink() As Boolean
        Get
            Return m_IsLink
        End Get
    End Property

    Public ReadOnly Property IsShared() As Boolean
        Get
            Return m_IsShared
        End Get
    End Property

    Public ReadOnly Property IsHidden() As Boolean
        Get
            Return m_IsHidden
        End Get
    End Property

    Public ReadOnly Property IsRemovable() As Boolean
        Get
            Return m_IsRemovable
        End Get
    End Property

    Private Sub SetDispType()
        Dim shfi As New SHFILEINFO()
        Dim dwflag As Integer = SHGFI.DISPLAYNAME Or SHGFI.TYPENAME Or SHGFI.PIDL
        Dim dwAttr As Integer = 0
        If m_IsFileSystem And Not m_IsFolder Then
            dwflag = dwflag Or SHGFI.USEFILEATTRIBUTES
            dwAttr = FILE_ATTRIBUTE_NORMAL
        End If
        SHGetFileInfo(m_Pidl, dwAttr, shfi, cbFileInfo, dwflag)
        m_DisplayName = shfi.szDisplayName
        m_TypeName = shfi.szTypeName
        If m_DisplayName.Equals("") Then
            m_DisplayName = m_Path
        End If
        m_SortFlag = ComputeSortFlag()
        m_HasDispType = True
    End Sub

    Public ReadOnly Property DisplayName() As String
        Get
            If Not m_HasDispType Then SetDispType()
            Return m_DisplayName
        End Get
    End Property

    Private ReadOnly Property SortFlag() As Integer
        Get
            If Not m_HasDispType Then SetDispType()
            Return m_SortFlag
        End Get
    End Property

    Public ReadOnly Property TypeName() As String
        Get
            If Not m_HasDispType Then SetDispType()
            Return m_TypeName
        End Get
    End Property

    Public ReadOnly Property IconIndexNormal() As Integer
        Get
            If m_IconIndexNormal < 0 Then
                If Not m_HasDispType Then SetDispType()
                Dim shfi As New SHFILEINFO()
                Dim dwflag As Integer = SHGFI.PIDL Or SHGFI.SYSICONINDEX
                Dim dwAttr As Integer = 0
                If m_IsFileSystem And Not m_IsFolder Then
                    dwflag = dwflag Or SHGFI.USEFILEATTRIBUTES
                    dwAttr = FILE_ATTRIBUTE_NORMAL
                End If
                SHGetFileInfo(m_Pidl, dwAttr, shfi, cbFileInfo, dwflag)
                m_IconIndexNormal = shfi.iIcon
            End If
            Return m_IconIndexNormal
        End Get
    End Property

    Public ReadOnly Property IconIndexOpen() As Integer
        Get
            If m_IconIndexOpen < 0 Then
                If Not m_HasDispType Then SetDispType()
                If Not m_IsDisk And m_IsFileSystem And m_IsFolder Then
                    If OpenFolderIconIndex < 0 Then
                        Dim dwflag As Integer = SHGFI.SYSICONINDEX Or SHGFI.PIDL
                        Dim shfi As New SHFILEINFO()
                        SHGetFileInfo(m_Pidl, 0, shfi, cbFileInfo, dwflag Or SHGFI.OPENICON)
                        m_IconIndexOpen = shfi.iIcon
                    Else
                        m_IconIndexOpen = OpenFolderIconIndex
                    End If
                Else
                    m_IconIndexOpen = m_IconIndexNormal
                End If
            End If
            Return m_IconIndexOpen
        End Get
    End Property

    Private Sub FillDemandInfo()
        If m_IsDisk Then
            Try
                Dim disk As New Management.ManagementObject("win32_logicaldisk.deviceid=""" & m_Path.Substring(0, 2) & """")
                m_Length = CType(disk("Size"), UInt64).ToString
                If CType(disk("DriveType"), UInt32).ToString = CStr(4) Then
                    m_IsNetWorkDrive = True
                End If
            Catch ex As Exception
                m_IsNetWorkDrive = True
            Finally
                m_XtrInfo = True
            End Try
        ElseIf Not m_IsDisk And m_IsFileSystem And Not m_IsFolder Then
            If File.Exists(m_Path) Then
                Dim fi As New FileInfo(m_Path)
                m_LastWriteTime = fi.LastWriteTime
                m_LastAccessTime = fi.LastAccessTime
                m_CreationTime = fi.CreationTime
                m_Length = fi.Length
                m_XtrInfo = True
            End If
        Else
            If m_IsFileSystem And m_IsFolder Then
                If Directory.Exists(m_Path) Then
                    Dim di As New DirectoryInfo(m_Path)
                    m_LastWriteTime = di.LastWriteTime
                    m_LastAccessTime = di.LastAccessTime
                    m_CreationTime = di.CreationTime
                    m_XtrInfo = True
                End If
            End If
        End If
    End Sub

    Public ReadOnly Property LastWriteTime() As DateTime
        Get
            If Not m_XtrInfo Then
                FillDemandInfo()
            End If
            Return m_LastWriteTime
        End Get
    End Property

    Public ReadOnly Property LastAccessTime() As DateTime
        Get
            If Not m_XtrInfo Then
                FillDemandInfo()
            End If
            Return m_LastAccessTime
        End Get
    End Property

    Public ReadOnly Property CreationTime() As DateTime
        Get
            If Not m_XtrInfo Then
                FillDemandInfo()
            End If
            Return m_CreationTime
        End Get
    End Property

    Public ReadOnly Property Length() As Long
        Get
            If Not m_XtrInfo Then
                FillDemandInfo()
            End If
            Return m_Length
        End Get
    End Property

    Public ReadOnly Property IsNetworkDrive() As Boolean
        Get
            If Not m_XtrInfo Then
                FillDemandInfo()
            End If
            Return m_IsNetWorkDrive
        End Get
    End Property

    Public ReadOnly Property clsPidl() As cPidl
        Get
            If IsNothing(m_cPidl) Then
                m_cPidl = New cPidl(m_Pidl)
            End If
            Return m_cPidl
        End Get
    End Property

    Public ReadOnly Property IsReadOnly() As Boolean
        Get
            If m_IsReadOnlySetup Then
                Return m_IsReadOnly
            Else
                Dim shfi As New SHFILEINFO()
                shfi.dwAttributes = SFGAO.RDONLY
                Dim dwflag As Integer = SHGFI.PIDL Or SHGFI.ATTRIBUTES Or SHGFI.ATTR_SPECIFIED
                Dim dwAttr As Integer = 0
                Dim H As IntPtr = SHGetFileInfo(m_Pidl, dwAttr, shfi, cbFileInfo, dwflag)
                If H.ToInt32 <> NOERROR AndAlso H.ToInt32 <> 1 Then
                    Marshal.ThrowExceptionForHR(H.ToInt32)
                End If
                m_IsReadOnly = CBool(shfi.dwAttributes And SFGAO.RDONLY)
                m_IsReadOnlySetup = True
                Return m_IsReadOnly
            End If
        End Get
    End Property

    Public ReadOnly Property IsSystem() As Boolean
        Get
            Static HaveSysInfo As Boolean
            Static m_IsSystem As Boolean
            If Not HaveSysInfo Then
                Try
                    m_IsSystem = (File.GetAttributes(m_Path) And FileAttributes.System) = FileAttributes.System
                    HaveSysInfo = True
                Catch ex As Exception
                    HaveSysInfo = True
                End Try
            End If
            Return m_IsSystem
        End Get
    End Property

    Public Shared Function GetDeskTop() As ShItem
        If IsNothing(DesktopBase) Then
            DesktopBase = New ShItem()
        End If
        Return DesktopBase
    End Function

    Public Shared Function IsAncestorOf(ByVal ancestor As ShItem, ByVal current As ShItem, Optional ByVal fParent As Boolean = False) As Boolean
        Return IsAncestorOf(ancestor.PIDL, current.PIDL, fParent)
    End Function

    Public Shared Function IsAncestorOf(ByVal AncestorPidl As IntPtr, ByVal ChildPidl As IntPtr, Optional ByVal fParent As Boolean = False) As Boolean
        If Is2KOrAbove() Then
            Return ILIsParent(AncestorPidl, ChildPidl, fParent)
        Else
            Dim Child As New cPidl(ChildPidl)
            Dim Ancestor As New cPidl(AncestorPidl)
            IsAncestorOf = Child.StartsWith(Ancestor)
            If Not IsAncestorOf Then Exit Function
            If fParent Then
                Dim oAncBytes() As Object = Ancestor.Decompose
                Dim oChildBytes() As Object = Child.Decompose
                If oAncBytes.Length <> (oChildBytes.Length - 1) Then
                    IsAncestorOf = False
                End If
            End If
        End If
    End Function

    Public Delegate Function WalkAllCallBack(ByVal info As ShItem, ByVal UserLevel As Integer, ByVal Tag As Integer) As Boolean

    Public Shared Function AllFolderWalk(ByVal cStart As ShItem, ByVal cback As WalkAllCallBack, ByVal UserLevel As Integer, ByVal Tag As Integer) As Boolean
        If Not IsNothing(cStart) AndAlso cStart.IsFolder Then
            Dim cItem As ShItem
            For Each cItem In cStart.GetFiles
                If Not cback(cItem, UserLevel, Tag) Then
                    Return False
                End If
            Next
            For Each cItem In cStart.GetDirectories
                If Not cback(cItem, UserLevel + 1, Tag) Then
                    Return False
                Else
                    If Not AllFolderWalk(cItem, cback, UserLevel + 1, Tag) Then
                        Return False
                    End If
                End If
            Next
            Return True
        Else
            Throw New ApplicationException("AllFolderWalk -- Invalid Start Directory")
        End If
    End Function

    Public Overloads Function Equals(ByVal other As ShItem) As Boolean
        Equals = Me.Path.Equals(other.Path)
    End Function

    Public Function GetDirectories(Optional ByVal doRefresh As Boolean = True) As ArrayList
        If m_IsFolder Then
            If doRefresh Then
                RefreshDirectories()
            ElseIf m_Directories Is Nothing Then
                RefreshDirectories()
            End If
            Return m_Directories
        Else
            Return New ArrayList()
        End If
    End Function

    Public Function GetFiles() As ArrayList
        If m_IsFolder Then
            Return GetContents(SHCONTF.NONFOLDERS Or SHCONTF.INCLUDEHIDDEN)
        Else
            Return New ArrayList()
        End If
    End Function

    Public Function GetFiles(ByVal Filter As String) As ArrayList
        If m_IsFolder Then
            Dim dummy As New ArrayList()
            Dim fileentries() As String
            fileentries = Directory.GetFiles(m_Path, Filter)
            Dim vFile As String
            For Each vFile In fileentries
                dummy.Add(New ShItem(vFile))
            Next
            Return dummy
        Else
            Return New ArrayList()
        End If
    End Function

    Public Function GetItems() As ArrayList
        Dim rVal As New ArrayList()
        If m_IsFolder Then
            rVal.AddRange(Me.GetDirectories)
            rVal.AddRange(Me.GetContents(SHCONTF.NONFOLDERS Or SHCONTF.INCLUDEHIDDEN))
            rVal.Sort()
            Return rVal
        Else
            Return rVal
        End If
    End Function

    Public Function GetFileName() As String
        If m_Path.StartsWith("::{") Then
            Return Me.DisplayName
        Else
            If m_IsDisk Then
                Return m_Path.Substring(0, 1)
            Else
                Return IO.Path.GetFileName(m_Path)
            End If
        End If
    End Function

    Public Function RefreshDirectories() As Boolean
        RefreshDirectories = False
        If m_IsFolder Then
            Dim InvalidDirs As New ArrayList()
            If IsNothing(m_Directories) Then
                m_Directories = GetContents(SHCONTF.FOLDERS Or SHCONTF.INCLUDEHIDDEN)
                RefreshDirectories = True
            Else
                Dim curPidls As ArrayList = GetContents(SHCONTF.FOLDERS Or SHCONTF.INCLUDEHIDDEN, True)
                Dim iptr As IntPtr
                If curPidls.Count < 1 Then
                    If m_Directories.Count > 0 Then
                        m_Directories = New ArrayList()
                        RefreshDirectories = True
                    End If
                Else
                    If m_Directories.Count < 1 Then
                        m_Directories = GetContents(SHCONTF.FOLDERS Or SHCONTF.INCLUDEHIDDEN)
                        RefreshDirectories = True
                    Else
                        Dim compList As New ArrayList(curPidls)
                        Dim iOld As Integer
                        Dim OldRel(m_Directories.Count - 1) As IntPtr
                        For iOld = 0 To m_Directories.Count - 1
                            OldRel(iOld) = GetLastID(CType(m_Directories(iOld), ShItem).PIDL)
                        Next
                        Dim iNew As Integer
                        For iOld = 0 To m_Directories.Count - 1
                            For iNew = 0 To compList.Count - 1
                                If IsEqual(CType(compList(iNew), IntPtr), OldRel(iOld)) Then
                                    compList.RemoveAt(iNew)
                                    GoTo NXTOLD
                                End If
                            Next
                            InvalidDirs.Add(m_Directories(iOld))
                            RefreshDirectories = True
NXTOLD:                 Next
                        Dim csi As ShItem
                        For Each csi In InvalidDirs
                            m_Directories.Remove(csi)
                        Next
                        If compList.Count > 0 Then
                            RefreshDirectories = True
                            For Each iptr In compList
                                m_Directories.Add(New ShItem(m_Folder, iptr, Me))
                            Next
                        End If
                        If RefreshDirectories Then
                            m_Directories.Sort()
                        End If
                    End If
                    For Each iptr In curPidls
                        Marshal.FreeCoTaskMem(iptr)
                    Next
                End If
            End If
        End If
    End Function

    Public Overrides Function ToString() As String
        Return m_DisplayName
    End Function

    Private Function GetContents(ByVal flags As SHCONTF, Optional ByVal IntPtrOnly As Boolean = False) As ArrayList
        Dim rVal As New ArrayList()
        Dim HR As Integer
        Dim IEnum As IEnumIDList = Nothing
        HR = m_Folder.EnumObjects(0, flags, IEnum)
        If HR = NOERROR Then
            Dim item As IntPtr = IntPtr.Zero
            Dim itemCnt As Integer
            HR = IEnum.GetNext(1, item, itemCnt)
            If HR = NOERROR Then
                Do While itemCnt > 0 AndAlso Not item.Equals(IntPtr.Zero)
                    If Not CBool(flags And SHCONTF.FOLDERS) Then
                        Dim attrFlag As SFGAO
                        attrFlag = attrFlag Or SFGAO.FOLDER Or SFGAO.STREAM
                        Dim aPidl(0) As IntPtr
                        aPidl(0) = item
                        m_Folder.GetAttributesOf(1, aPidl, attrFlag)
                        If Not XPorAbove Then
                            If CBool(attrFlag And SFGAO.FOLDER) Then
                                GoTo SKIPONE
                            End If
                        Else
                            If CBool(attrFlag And SFGAO.FOLDER) AndAlso Not CBool(attrFlag And SFGAO.STREAM) Then
                                GoTo SKIPONE
                            End If
                        End If
                    End If
                    If IntPtrOnly Then
                        rVal.Add(PIDLClone(item))
                    Else
                        rVal.Add(New ShItem(m_Folder, item, Me))
                    End If
SKIPONE:            Marshal.FreeCoTaskMem(item)
                    item = IntPtr.Zero
                    itemCnt = 0
                    HR = IEnum.GetNext(1, item, itemCnt)
                Loop
            Else
                If HR <> 1 Then GoTo HRError
            End If
        Else : GoTo HRError
        End If
NORMAL: If Not IsNothing(IEnum) Then
            Marshal.ReleaseComObject(IEnum)
        End If
        rVal.TrimToSize()
        Return rVal
HRError:
        If Not IsNothing(IEnum) Then Marshal.ReleaseComObject(IEnum)
        rVal = New ArrayList()
        GoTo NORMAL
    End Function

    Private Shared Function ItemIDSize(ByVal pidl As IntPtr) As Integer
        If Not pidl.Equals(IntPtr.Zero) Then
            Dim b(1) As Byte
            Marshal.Copy(pidl, b, 0, 2)
            Return b(1) * 256 + b(0)
        Else
            Return 0
        End If
    End Function

    Public Shared Function ItemIDListSize(ByVal pidl As IntPtr) As Integer
        If Not pidl.Equals(IntPtr.Zero) Then
            Dim i As Integer = ItemIDSize(pidl)
            Dim b As Integer = Marshal.ReadByte(pidl, i) + (Marshal.ReadByte(pidl, i + 1) * 256)
            Do While b > 0
                i += b
                b = Marshal.ReadByte(pidl, i) + (Marshal.ReadByte(pidl, i + 1) * 256)
            Loop
            Return i
        Else : Return 0
        End If
    End Function

    Public Shared Function PidlCount(ByVal pidl As IntPtr) As Integer
        If Not pidl.Equals(IntPtr.Zero) Then
            Dim cnt As Integer = 0
            Dim i As Integer = 0
            Dim b As Integer = Marshal.ReadByte(pidl, i) + (Marshal.ReadByte(pidl, i + 1) * 256)
            Do While b > 0
                cnt += 1
                i += b
                b = Marshal.ReadByte(pidl, i) + (Marshal.ReadByte(pidl, i + 1) * 256)
            Loop
            Return cnt
        Else : Return 0
        End If
    End Function

    Public Shared Function GetLastID(ByVal pidl As IntPtr) As IntPtr
        If Not pidl.Equals(IntPtr.Zero) Then
            Dim prev As Integer = 0
            Dim i As Integer = 0
            Dim b As Integer = Marshal.ReadByte(pidl, i) + (Marshal.ReadByte(pidl, i + 1) * 256)
            Do While b > 0
                prev = i
                i += b
                b = Marshal.ReadByte(pidl, i) + (Marshal.ReadByte(pidl, i + 1) * 256)
            Loop
            Return New IntPtr(pidl.ToInt32 + prev)
        Else : Return IntPtr.Zero
        End If
    End Function

    Public Shared Function DecomposePIDL(ByVal pidl As IntPtr) As IntPtr()
        Dim lim As Integer = ItemIDListSize(pidl)
        Dim PIDLs(PidlCount(pidl) - 1) As IntPtr
        Dim i As Integer = 0
        Dim curB As Integer
        Dim offSet As Integer = 0
        Do While curB < lim
            Dim thisPtr As IntPtr = New IntPtr(pidl.ToInt32 + curB)
            offSet = Marshal.ReadByte(thisPtr) + (Marshal.ReadByte(thisPtr, 1) * 256)
            PIDLs(i) = Marshal.AllocCoTaskMem(offSet + 2)
            Dim b(offSet + 1) As Byte
            Marshal.Copy(thisPtr, b, 0, offSet)
            b(offSet) = 0 : b(offSet + 1) = 0
            Marshal.Copy(b, 0, PIDLs(i), offSet + 2)
            curB += offSet
            i += 1
        Loop
        Return PIDLs
    End Function

    Private Shared Function PIDLClone(ByVal pidl As IntPtr) As IntPtr
        Dim cb As Integer = ItemIDListSize(pidl)
        Dim b(cb + 1) As Byte
        Marshal.Copy(pidl, b, 0, cb)
        b(cb) = 0 : b(cb + 1) = 0
        PIDLClone = Marshal.AllocCoTaskMem(cb + 2)
        Marshal.Copy(b, 0, PIDLClone, cb + 2)
    End Function

    Public Shared Function IsEqual(ByVal Pidl1 As IntPtr, ByVal Pidl2 As IntPtr) As Boolean
        If Win2KOrAbove Then
            Return ILIsEqual(Pidl1, Pidl2)
        Else
            Dim cb1 As Integer, cb2 As Integer
            cb1 = ItemIDListSize(Pidl1)
            cb2 = ItemIDListSize(Pidl2)
            If cb1 <> cb2 Then Return False
            Dim lim32 As Integer = cb1 \ 4
            Dim i As Integer
            For i = 0 To lim32 - 1
                If Marshal.ReadInt32(Pidl1, i) <> Marshal.ReadInt32(Pidl2, i) Then
                    Return False
                End If
            Next
            Dim limB As Integer = cb1 Mod 4
            Dim offset As Integer = lim32 * 4
            For i = 0 To limB - 1
                If Marshal.ReadByte(Pidl1, offset + i) <> Marshal.ReadByte(Pidl2, offset + i) Then
                    Return False
                End If
            Next
            Return True
        End If
    End Function

    Public Shared Function concatPidls(ByVal pidl1 As IntPtr, ByVal pidl2 As IntPtr) As IntPtr
        Dim cb1 As Integer, cb2 As Integer
        cb1 = ItemIDListSize(pidl1)
        cb2 = ItemIDListSize(pidl2)
        Dim rawCnt As Integer = cb1 + cb2
        If (rawCnt) > 0 Then
            Dim b(rawCnt + 1) As Byte
            If cb1 > 0 Then
                Marshal.Copy(pidl1, b, 0, cb1)
            End If
            If cb2 > 0 Then
                Marshal.Copy(pidl2, b, cb1, cb2)
            End If
            Dim rVal As IntPtr = Marshal.AllocCoTaskMem(cb1 + cb2 + 2)
            b(rawCnt) = 0 : b(rawCnt + 1) = 0
            Marshal.Copy(b, 0, rVal, rawCnt + 2)
            Return rVal
        Else
            Return IntPtr.Zero
        End If
    End Function

    Public Shared Function TrimPidl(ByVal pidl As IntPtr, ByRef relPidl As IntPtr) As IntPtr
        Dim cb As Integer = ItemIDListSize(pidl)
        Dim b(cb + 1) As Byte
        Marshal.Copy(pidl, b, 0, cb)
        Dim prev As Integer = 0
        Dim i As Integer = b(0) + (b(1) * 256)
        Do While i > 0 AndAlso i < cb
            prev = i
            i += b(i) + (b(i + 1) * 256)
        Loop
        If (prev + 1) < cb Then
            b(cb) = 0
            b(cb + 1) = 0
            Dim cb1 As Integer = b(prev) + (b(prev + 1) * 256)
            relPidl = Marshal.AllocCoTaskMem(cb1 + 2)
            Marshal.Copy(b, prev, relPidl, cb1 + 2)
            b(prev) = 0 : b(prev + 1) = 0
            Dim rVal As IntPtr = Marshal.AllocCoTaskMem(prev + 2)
            Marshal.Copy(b, 0, rVal, prev + 2)
            Return rVal
        Else
            Return IntPtr.Zero
        End If
    End Function

    Public Class TagComparer
        Implements IComparer
        Public Function Compare(ByVal x As Object, ByVal y As Object) As Integer Implements IComparer.Compare
            Dim xTag As ShItem = x.tag
            'Dim yTag As CShItem = y.tag
            Return xTag.CompareTo(y.tag)
        End Function
    End Class

    Public Class cPidl
        Implements IEnumerable

        Dim m_bytes() As Byte
        Dim m_ItemCount As Integer

        Sub New(ByVal pidl As IntPtr)
            Dim cb As Integer = ItemIDListSize(pidl)
            If cb > 0 Then
                ReDim m_bytes(cb + 1)
                Marshal.Copy(pidl, m_bytes, 0, cb)
            Else
                ReDim m_bytes(1)
            End If
            m_bytes(m_bytes.Length - 2) = 0 : m_bytes(m_bytes.Length - 1) = 0
            m_ItemCount = PidlCount(pidl)
        End Sub

        Public ReadOnly Property PidlBytes() As Byte()
            Get
                Return m_bytes
            End Get
        End Property

        Public ReadOnly Property Length() As Integer
            Get
                Return m_bytes.Length
            End Get
        End Property

        Public ReadOnly Property ItemCount() As Integer
            Get
                Return m_ItemCount
            End Get
        End Property

        Public Function ToPIDL() As IntPtr
            ToPIDL = BytesToPidl(m_bytes)
        End Function

        Public Function Decompose() As Object()
            Dim bArrays(Me.ItemCount - 1) As Object
            Dim eByte As ICPidlEnumerator = Me.GetEnumerator()
            Dim i As Integer
            Do While eByte.MoveNext
                bArrays(i) = eByte.Current
                i += 1
            Loop
            Return bArrays
        End Function

        Public Function IsEqual(ByVal other As cPidl) As Boolean
            IsEqual = False
            If other.Length <> Me.Length Then Exit Function
            Dim ob() As Byte = other.PidlBytes
            Dim i As Integer
            For i = 0 To Me.Length - 1
                If ob(i) <> m_bytes(i) Then Exit Function
            Next
            Return True
        End Function

        Public Shared Function JoinPidlBytes(ByVal b1() As Byte, ByVal b2() As Byte) As Byte()
            If IsValidPidl(b1) And IsValidPidl(b2) Then
                Dim b(b1.Length + b2.Length - 3) As Byte
                Array.Copy(b1, b, b1.Length - 2)
                Array.Copy(b2, 0, b, b1.Length - 2, b2.Length)
                If IsValidPidl(b) Then
                    Return b
                Else
                    Return Nothing
                End If
            Else
                Return Nothing
            End If
        End Function

        Public Shared Function BytesToPidl(ByVal b() As Byte) As IntPtr
            BytesToPidl = IntPtr.Zero
            If IsValidPidl(b) Then
                Dim bLen As Integer = b.Length
                BytesToPidl = Marshal.AllocCoTaskMem(bLen)
                If BytesToPidl.Equals(IntPtr.Zero) Then Exit Function
                Marshal.Copy(b, 0, BytesToPidl, bLen)
            End If
        End Function

        Public Shared Function StartsWith(ByVal pidlA As IntPtr, ByVal pidlB As IntPtr) As Boolean
            Return cPidl.StartsWith(New cPidl(pidlA), New cPidl(pidlB))
        End Function

        Public Shared Function StartsWith(ByVal A As cPidl, ByVal B As cPidl) As Boolean
            Return A.StartsWith(B)
        End Function

        Public Function StartsWith(ByVal cp As cPidl) As Boolean
            Dim b() As Byte = cp.PidlBytes
            If b.Length > m_bytes.Length Then Return False
            Dim i As Integer
            For i = 0 To b.Length - 3
                If b(i) <> m_bytes(i) Then Return False
            Next
            Return True
        End Function

        Public Function GetEnumerator() As System.Collections.IEnumerator Implements System.Collections.IEnumerable.GetEnumerator
            Return New ICPidlEnumerator(m_bytes)
        End Function

        Private Class ICPidlEnumerator
            Implements IEnumerator

            Private m_sPos As Integer
            Private m_ePos As Integer
            Private m_bytes() As Byte
            Private m_NotEmpty As Boolean = False

            Sub New(ByVal b() As Byte)
                m_bytes = b
                If b.Length > 0 Then m_NotEmpty = True
                m_sPos = -1 : m_ePos = -1
            End Sub

            Public ReadOnly Property Current() As Object Implements System.Collections.IEnumerator.Current
                Get
                    If m_sPos < 0 Then Throw New InvalidOperationException("ICPidlEnumerator --- attempt to get Current with invalidated list")
                    Dim b((m_ePos - m_sPos) + 2) As Byte
                    Array.Copy(m_bytes, m_sPos, b, 0, b.Length - 2)
                    b(b.Length - 2) = 0 : b(b.Length - 1) = 0
                    Return b
                End Get
            End Property

            Public Function MoveNext() As Boolean Implements System.Collections.IEnumerator.MoveNext
                If m_NotEmpty Then
                    If m_sPos < 0 Then
                        m_sPos = 0 : m_ePos = -1
                    Else
                        m_sPos = m_ePos + 1
                    End If
                    Dim cb As Integer = m_bytes(m_sPos) + m_bytes(m_sPos + 1) * 256
                    If cb = 0 Then
                        Return False
                    Else
                        m_ePos += cb
                    End If
                Else
                    m_sPos = 0 : m_ePos = 0
                    Return False
                End If
                Return True
            End Function

            Public Sub Reset() Implements System.Collections.IEnumerator.Reset
                m_sPos = -1 : m_ePos = -1
            End Sub
        End Class

    End Class

End Class