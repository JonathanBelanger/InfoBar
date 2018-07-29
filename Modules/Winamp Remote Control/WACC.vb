Imports System.Runtime.InteropServices
Imports System.Text
Imports System.Windows.Forms
Imports Microsoft.Win32

Public Class clsWACC

#Region " WinAPI "
  Private Class WinAPI
    Public Const PAGE_READWRITE As Integer = &H4
    Public Const MEM_COMMIT As Integer = &H1000
    Public Const MEM_DECOMMIT As Integer = &H4000
    Public Const MEM_RELEASE As Integer = &H8000

    Public Enum Msg As Integer
      WM_COMMAND = &H111
      WM_USER = &H400
    End Enum

    <Flags()> _
    Public Enum DAccess
      PROCESS_ALL_ACCESS = &H1F0FFF
    End Enum

    <DllImport("user32.dll", SetLastError:=True, CharSet:=CharSet.Auto)> _
    Public Shared Function FindWindow(ByVal lpClassName As String, ByVal lpWindowName As String) As IntPtr
    End Function

    <DllImport("user32.dll", SetLastError:=True)> _
    Public Shared Function GetWindowThreadProcessId(ByVal hwnd As IntPtr, ByRef lpdwProcessId As Integer) As Int32
    End Function

    <DllImport("user32.dll", SetLastError:=True, CharSet:=CharSet.Auto)> _
    Public Shared Function SendMessage(ByVal hWnd As IntPtr, ByVal Msg As Msg, ByVal wParam As IntPtr, ByVal lParam As IntPtr) As IntPtr
    End Function

    <DllImport("kernel32", CallingConvention:=CallingConvention.Winapi, EntryPoint:="ReadProcessMemory", ExactSpelling:=True, SetLastError:=True)> _
    Public Shared Function ReadProcessMemory(<InAttribute()> ByVal hProcess As IntPtr, <InAttribute()> ByVal lpBaseAddress As IntPtr, <OutAttribute()> ByVal lpBuffer As IntPtr, <InAttribute()> ByVal nSize As UInt32, <OutAttribute()> ByRef lpNumberOfBytesRead As UInt32) As <MarshalAs(UnmanagedType.Bool)> Boolean
    End Function

    <DllImport("kernel32.dll", SetLastError:=True)> _
    Public Shared Function WriteProcessMemory(ByVal hProcess As IntPtr, ByVal lpBaseAddress As IntPtr, ByVal lpBuffer As IntPtr, ByVal nSize As Int32, <OutAttribute()> ByRef lpNumberOfBytesWritten As UInt32) As <MarshalAs(UnmanagedType.Bool)> Boolean
    End Function

    <DllImport("kernel32.dll", SetLastError:=True, ExactSpelling:=True)> _
    Public Shared Function VirtualAllocEx(ByVal hProcess As IntPtr, ByVal lpAddress As IntPtr, <MarshalAs(UnmanagedType.U4)> ByVal dwSize As UInt32, <MarshalAs(UnmanagedType.U4)> ByVal flAllocationType As Integer, <MarshalAs(UnmanagedType.U4)> ByVal flProtect As Integer) As IntPtr
    End Function

    <DllImport("kernel32.dll", SetLastError:=True, ExactSpelling:=True)> _
    Public Shared Function VirtualFreeEx(ByVal hProcess As IntPtr, ByVal lpAddress As IntPtr, ByVal dwSize As UIntPtr, ByVal dwFreeType As Integer) As Boolean
    End Function

    <DllImport("kernel32.dll", CallingConvention:=CallingConvention.Winapi, EntryPoint:="OpenProcess", ExactSpelling:=True, SetLastError:=True)> _
    Public Shared Function OpenProcess(<MarshalAs(UnmanagedType.U4)> ByVal dwDesiredAccess As DAccess, <MarshalAs(UnmanagedType.Bool)> ByVal bInheritHandle As Boolean, <MarshalAs(UnmanagedType.U4)> ByVal dwProcessId As Integer) As IntPtr
    End Function

    <DllImport("kernel32", CallingConvention:=CallingConvention.Winapi, ExactSpelling:=True, SetLastError:=True)> _
    Public Shared Function CloseHandle(<InAttribute()> ByVal Handle As IntPtr) As Boolean
    End Function
  End Class
#End Region

#Region " Private members "

  Private Enum Message As Integer
    IPC_STARTPLAY = 102         'starts playback. almost like hitting play in Winamp    
    IPC_ISPLAYING = 104         '0 - not playing, 1 - playing, 3 - paused
    IPC_GETOUTPUTTIME = 105     'mode 1 -  position in milliseconds of the current track, mode 2 - track length, in seconds
    IPC_GETLISTLENGTH = 124     'returns the length of the current playlist, in tracks
    IPC_GETLISTPOS = 125        'returns the playlist position
    IPC_GETPLAYLISTFILE = 211   '-gets a pointer in the remote memory to the file path of the playlist entry [index]
    IPC_GETWND = 260            'returns the HWND of the window specified.
    IPC_ISWNDVISIBLE = 261      'returns 1 if specified window is visible
    IPC_GET_EXTENDED_FILE_INFO = 290
    IPC_SETRATING = 639         'sets the current item's rating (0..5)
    IPC_GETRATING = 640         'gets the current item's rating (0..5)
  End Enum

  Private Enum Command As Integer
    WINAMP_BUTTON3 = 40046
    WINAMP_BUTTON4 = 40047
    WINAMP_BUTTON5 = 40048
    WINAMP_PREVSONG = 40198
    PL_SONG_5_STAR_RATING = 40402
    PL_SONG_4_STAR_RATING = 40403
    PL_SONG_3_STAR_RATING = 40404
    PL_SONG_2_STAR_RATING = 40405
    PL_SONG_1_STAR_RATING = 40406
    PL_SONG_NO_RATING = 40407
  End Enum

  Private Enum Outputtime_Mode As Integer
    Position = 0
    Length = 1
  End Enum

  Private Const lpClassName As String = "Winamp v1.x"
  Private Shared hWnd_Winamp As IntPtr
  Private Shared waPID As Integer
  Private WithEvents waProcess As Process

  Private Shared Sub SendCOMMAND(ByVal CommandName As Command)
    WinAPI.SendMessage(hWnd_Winamp, WinAPI.Msg.WM_COMMAND, New IntPtr(CommandName), IntPtr.Zero)
  End Sub

  Private Shared Function SendWA_IPC(ByVal param As Int64, ByVal MessageName As Message) As IntPtr
    Return WinAPI.SendMessage(hWnd_Winamp, WinAPI.Msg.WM_USER, New IntPtr(param), New IntPtr(MessageName))
  End Function

  Private Shared Function SendWA_IPC(ByVal param As IntPtr, ByVal MessageName As Message) As IntPtr
    Return WinAPI.SendMessage(hWnd_Winamp, WinAPI.Msg.WM_USER, param, New IntPtr(MessageName))
  End Function

  Private Shared Function AllocWinamp(ByVal handle As IntPtr, ByVal bufsize As UInt32) As IntPtr
    Dim RemoteBuf As IntPtr
    Dim hWinamp As IntPtr = handle
    If hWinamp.Equals(IntPtr.Zero) Then Exit Function
    RemoteBuf = WinAPI.VirtualAllocEx(hWinamp, Nothing, bufsize, WinAPI.MEM_COMMIT, WinAPI.PAGE_READWRITE)
    Return RemoteBuf
  End Function

  Private Shared Function ReadRemoteString(ByVal remoteBuf As IntPtr, ByVal MaxStrLen As Integer) As String
    If remoteBuf.Equals(IntPtr.Zero) Then
      Return Nothing
    Else
      Dim returnVal As IntPtr = Marshal.AllocHGlobal(MaxStrLen)
      ReadWinampToLocal(remoteBuf, returnVal, Convert.ToUInt32(MaxStrLen))
      Dim RetStr As String = Marshal.PtrToStringAnsi(returnVal)
      Marshal.FreeHGlobal(returnVal)
      Return RetStr
    End If
  End Function

  Private Shared Function ReadWinampToLocal(ByVal remoteBuf As IntPtr, ByRef localBuf As IntPtr, ByVal bufsize As UInt32) As UInt32
    Dim isError As Boolean
    Dim hWinamp As IntPtr
    hWinamp = WinAPI.OpenProcess(WinAPI.DAccess.PROCESS_ALL_ACCESS, False, waPID)
    If hWinamp.Equals(IntPtr.Zero) Then Return Convert.ToUInt32(0)
    isError = WinAPI.ReadProcessMemory(hWinamp, remoteBuf, localBuf, bufsize, Nothing)
    WinAPI.CloseHandle(hWinamp)
    If Not isError Then
      Return Convert.ToUInt32(0)
    Else
      Return bufsize
    End If
  End Function

  Private Class RemoteString
    Private hWinamp As IntPtr
    Private remoteBuf As IntPtr
    Private remStr As String

    Public Sub New(ByVal handle As IntPtr, ByVal str As String)
      hWinamp = WinAPI.OpenProcess(WinAPI.DAccess.PROCESS_ALL_ACCESS, False, waPID)
      remStr = str
      remoteBuf = AllocWinamp(handle, Convert.ToUInt32(str.Length + 1))
      Dim localBuf As IntPtr = Marshal.StringToHGlobalAnsi(str)
      WinAPI.WriteProcessMemory(hWinamp, remoteBuf, localBuf, str.Length + 1, Nothing)
      Marshal.FreeHGlobal(localBuf)
    End Sub

    Private Function FreeRemoteString(ByVal remoteBuf As IntPtr, ByVal bufsize As UIntPtr) As Boolean
      Dim err As Boolean
      err = WinAPI.VirtualFreeEx(hWinamp, remoteBuf, bufsize, WinAPI.MEM_DECOMMIT)
      If Not err Then Return False
      err = WinAPI.VirtualFreeEx(hWinamp, remoteBuf, UIntPtr.Zero, WinAPI.MEM_RELEASE)
      If Not err Then Return False
      Return True
    End Function

    Public Sub Delete()
      FreeRemoteString(remoteBuf, New UIntPtr(Convert.ToUInt32(remStr.Length + 1)))
      WinAPI.CloseHandle(hWinamp)
    End Sub

    Public ReadOnly Property Ptr() As IntPtr
      Get
        Return remoteBuf
      End Get
    End Property

  End Class
#End Region

#Region " Public Members "

  Public Event WinampExited()

  Public ReadOnly Property Playback() As cPlayback
    Get
      Return New cPlayback
    End Get
  End Property

  Public ReadOnly Property Playlist() As cPlaylist
    Get
      Return New cPlaylist(Me)
    End Get
  End Property

#Region " Playlist "
  Public Class cPlaylist
    Private Parent As clsWACC

    Public Sub New(ByVal _Parent As clsWACC)
      Parent = _Parent
    End Sub

    <StructLayout(LayoutKind.Sequential)> _
    Private Structure extendedFileInfoStruct
      Dim filename As IntPtr
      Dim metadata As IntPtr
      Dim ret As IntPtr
      Dim retlen As Int32
    End Structure

    Private Ratings() As Integer = {Command.PL_SONG_NO_RATING, Command.PL_SONG_1_STAR_RATING, Command.PL_SONG_2_STAR_RATING, _
                                    Command.PL_SONG_3_STAR_RATING, Command.PL_SONG_4_STAR_RATING, Command.PL_SONG_5_STAR_RATING}

    Public ReadOnly Property Position() As Integer
      Get
        Return SendWA_IPC(0, Message.IPC_GETLISTPOS).ToInt32
      End Get
    End Property

    Public ReadOnly Property Length() As Integer
      Get
        Return SendWA_IPC(0, Message.IPC_GETLISTLENGTH).ToInt32
      End Get
    End Property

    Public Sub JumpToPreviousTrack()
      SendCOMMAND(Command.WINAMP_PREVSONG)
    End Sub

    Public Sub JumpToNextTrack()
      SendCOMMAND(Command.WINAMP_BUTTON5)
    End Sub

    Public Sub RateSelectedTracks(ByVal Rate As Integer)
      If Rate >= 0 And Rate < 6 Then
        SendCOMMAND(CType(Ratings(Rate), Command))
      End If
    End Sub

    Public Function GetItemPath(ByVal PlaylistItem As Integer) As String
      Return ReadRemoteString(SendWA_IPC(PlaylistItem, Message.IPC_GETPLAYLISTFILE), 260)
    End Function

    Public Function GetMetaData(ByVal Item As Integer, ByVal MetaData As String) As String
      Return GetMetaData(Me.GetItemPath(Item), MetaData)
    End Function

    Public Function GetMetaData(ByVal FilePath As String, ByVal MetaData As String) As String
      Const SIZE As Int32 = 256

      Dim extFIS As extendedFileInfoStruct
      Dim hWinamp As IntPtr = Me.Parent.waProcess.Handle

      Dim fn As New RemoteString(hWinamp, FilePath)
      Dim md As New RemoteString(hWinamp, MetaData)
      Dim extFISSize As Int32 = Marshal.SizeOf(extFIS)

      Dim remoteBuf As IntPtr = AllocWinamp(hWinamp, Convert.ToUInt32(SIZE))

      extFIS.filename = fn.Ptr
      extFIS.metadata = md.Ptr
      extFIS.ret = remoteBuf
      extFIS.retlen = SIZE

      'create a local buffer which will contain the structure
      Dim extFISAddr As IntPtr
      extFISAddr = Marshal.AllocHGlobal(extFISSize)
      'copy the structure to the buffer
      Marshal.StructureToPtr(extFIS, extFISAddr, True)

      Dim remoteStructBuf As IntPtr = AllocWinamp(hWinamp, Convert.ToUInt32(extFISSize))
      WinAPI.WriteProcessMemory(hWinamp, remoteStructBuf, extFISAddr, extFISSize, Nothing)

      Dim ret As String
      SendWA_IPC(remoteStructBuf, Message.IPC_GET_EXTENDED_FILE_INFO)
      ret = ReadRemoteString(remoteBuf, SIZE)

      'Cleanup------------------------------
      WinAPI.VirtualFreeEx(hWinamp, remoteStructBuf, New UIntPtr(Convert.ToUInt32(extFISSize)), WinAPI.MEM_DECOMMIT)
      WinAPI.VirtualFreeEx(hWinamp, remoteStructBuf, UIntPtr.Zero, WinAPI.MEM_RELEASE)
      WinAPI.VirtualFreeEx(hWinamp, remoteBuf, New UIntPtr(Convert.ToUInt32(SIZE)), WinAPI.MEM_DECOMMIT)
      WinAPI.VirtualFreeEx(hWinamp, remoteBuf, UIntPtr.Zero, WinAPI.MEM_RELEASE)
      Marshal.FreeHGlobal(extFISAddr)
      fn.Delete()
      md.Delete()

      Return ret
    End Function

  End Class
#End Region

#Region " Playback "
  Public Class cPlayback
    Public Enum Playback_State As Integer
      NotPlaying = 0
      Playing = 1
      Paused = 3
      Unknown = -1
    End Enum

    Public ReadOnly Property PlaybackState() As Playback_State
      Get
        Dim state As Integer = SendWA_IPC(0, Message.IPC_ISPLAYING).ToInt32
        Select Case state
          Case 0 : Return Playback_State.NotPlaying
          Case 1 : Return Playback_State.Playing
          Case 3 : Return Playback_State.Paused
          Case Else
            'SendMessage returned undefined value
            Return Playback_State.Unknown
        End Select
      End Get
    End Property

    Public ReadOnly Property TrackPosition() As Integer
      Get
        Return SendWA_IPC(Outputtime_Mode.Position, Message.IPC_GETOUTPUTTIME).ToInt32
      End Get
    End Property

    Public Function GetTrackLength() As Integer
      Return SendWA_IPC(Outputtime_Mode.Length, Message.IPC_GETOUTPUTTIME).ToInt32
    End Function

    Public Sub Play()
      SendWA_IPC(0, Message.IPC_STARTPLAY)
    End Sub

    Public Sub Pause()
      SendCOMMAND(Command.WINAMP_BUTTON3)
    End Sub

    Public Sub [Stop]()
      SendCOMMAND(Command.WINAMP_BUTTON4)
    End Sub

    Public Property Rating() As Integer
      Get
        Return SendWA_IPC(0, Message.IPC_GETRATING).ToInt32
      End Get
      Set(ByVal Rate As Integer)
        If Rate >= 0 And Rate < 6 Then
          SendWA_IPC(Rate, Message.IPC_SETRATING)
        End If
      End Set
    End Property

  End Class
#End Region

#Region "Main methods/properties"

  'gets process ID of WinAmp
  Public ReadOnly Property ProcessID() As Integer
    Get
      Return waPID
    End Get
  End Property

  'Binds to WinAmp
  Public Function Bind() As Boolean
    Return Bind("")
  End Function

  Public Function Bind(ByVal PathToExecutable As String) As Boolean
    hWnd_Winamp = WinAPI.FindWindow(lpClassName, Nothing)

    waProcess = New Process
    waProcess.EnableRaisingEvents = True

    'If WinAmp window handle not found, try to launch it
    If hWnd_Winamp.Equals(IntPtr.Zero) Then

      'if path was not specified, try to find it in the Windows registry
      If PathToExecutable = "" Then
        Dim path As String
        Dim regKey As RegistryKey = Registry.CurrentUser
        regKey = regKey.OpenSubKey("Software\Winamp", False)
        If Not regKey Is Nothing Then
          path = Convert.ToString(regKey.GetValue(""))
          waProcess.StartInfo.FileName = path & "\Winamp.exe"
          regKey.Close()
        Else
          Return False
        End If
      Else
        waProcess.StartInfo.FileName = PathToExecutable
      End If

      Try
        waProcess.Start()
      Catch ex As System.ComponentModel.Win32Exception When ex.ErrorCode = -2147467259
        Debug.WriteLine("Executable not found")
        Return False
      Catch ex As Exception
        Debug.WriteLine("unknown exception")
        Return False
      End Try

      waProcess.WaitForInputIdle()
      hWnd_Winamp = waProcess.MainWindowHandle()
      waPID = waProcess.Id
    Else
      'WinAmp handle found
      'now bind to WinAmp process
      'get PID from hWnd
      WinAPI.GetWindowThreadProcessId(hWnd_Winamp, waPID)
      If (waPID = 0) Then
        Return False
      End If

      waProcess = Process.GetProcessById(waPID)
      waProcess.EnableRaisingEvents = True

      Return True
    End If

    'if hWnd of the main window is still zero,
    'it was not possible to launch or bind to WinAmp
    If hWnd_Winamp.Equals(IntPtr.Zero) Then
      Return False
    Else
      Return True
    End If
  End Function

  Private Sub waProcess_Exited(ByVal sender As Object, ByVal e As System.EventArgs) Handles waProcess.Exited
    RaiseEvent WinampExited()
  End Sub

#End Region

#End Region

End Class