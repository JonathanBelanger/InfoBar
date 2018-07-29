Public Class QuickLaunchItem
  Dim m_Title As String
  Dim m_Path As String
  Dim m_CommandLineArgs As String
  Dim m_Icon As Image
  Dim m_Bounds As Rectangle
  Dim m_ButtonState As Integer
  Dim m_IsFolder As Boolean
  Dim m_WorkingDirectory As String

  Public Property Title() As String
    Get
      Return m_Title
    End Get
    Set(ByVal value As String)
      m_Title = value
    End Set
  End Property

  Public Property Path() As String
    Get
      Return m_Path
    End Get
    Set(ByVal value As String)
      m_Path = value
    End Set
  End Property

  Public Property CommandLineArgs() As String
    Get
      Return m_CommandLineArgs
    End Get
    Set(ByVal value As String)
      m_CommandLineArgs = value
    End Set
  End Property

  Public Property Icon() As Image
    Get
      Return m_Icon
    End Get
    Set(ByVal value As Image)
      m_Icon = value
    End Set
  End Property

  Public Property Bounds() As Rectangle
    Get
      Return m_Bounds
    End Get
    Set(ByVal value As Rectangle)
      m_Bounds = value
    End Set
  End Property

  Public Property ButtonState() As Integer
    Get
      Return m_ButtonState
    End Get
    Set(ByVal value As Integer)
      m_ButtonState = value
    End Set
  End Property

  Public Property IsFolder() As Boolean
    Get
      Return m_IsFolder
    End Get
    Set(ByVal value As Boolean)
      m_IsFolder = value
    End Set
  End Property

  Public Property WorkingDirectory() As String
    Get
      Return m_WorkingDirectory
    End Get
    Set(ByVal value As String)
      m_WorkingDirectory = value
    End Set
  End Property

End Class
