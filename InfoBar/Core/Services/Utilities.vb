Namespace Services

  Public Class Utilities
    Public Enum DisplayUnit
      Auto
      Bits
      Kilobits
      Megabits
      Gigabits
      Terabits
      Bytes
      Kilobytes
      Megabytes
      Gigabytes
      Terabytes
    End Enum

    Public Enum ShellCSIDL As Integer
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

    Public Sub SortCollection(ByVal col As Collection, ByVal psSortPropertyName As String, ByVal pbAscending As Boolean, Optional ByVal psKeyPropertyName As String = "")
      modUtilities.SortCollection(col, psSortPropertyName, pbAscending, psKeyPropertyName)
    End Sub

    Public Function GetSpecialFolder(ByVal ID As ShellCSIDL) As String
      Dim sID As ShellDll.CSIDL = ID
      Return ShItem.GetCShItem(sID).Path
    End Function

    Public Class PasswordEncryption

      Public Function EncryptPassword(ByVal password As String) As String
        Return InfoBar.PasswordEncryption.Encrypt(password)
      End Function

      Public Function DecryptPassword(ByVal encryptedPassword As String) As String
        Return InfoBar.PasswordEncryption.Decrypt(encryptedPassword)
      End Function

    End Class

    Public Function FormatTime(ByVal sngSecs As ULong) As String
      Return modUtilities.FormatTime(sngSecs)
    End Function

    Public Function FormatFileSize(ByVal Bytes As ULong, Optional ByVal Format As DisplayUnit = DisplayUnit.Auto) As String
      Return modUtilities.FormatFileSize(Bytes, Format)
    End Function

    Public Function FormatTransferRate(ByVal Bytes As ULong, Optional ByVal Format As DisplayUnit = DisplayUnit.Auto) As String
      Return modUtilities.FormatTransferRate(Bytes, Format)
    End Function

    Public Function IsWindowsVistaOrAbove() As Boolean
      Return modAeroSupport.IsWindowsVistaOrAbove
    End Function

    Public Function IsWindowsSeven() As Boolean
      Return modAeroSupport.IsWindowsSeven
    End Function

  End Class

End Namespace