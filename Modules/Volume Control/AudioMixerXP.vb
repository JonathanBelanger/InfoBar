Public Class AudioMixerXP

  Public Event VolumeChanged()
  Public Event MuteChanged()

  Dim m_Mixer As New Mixers
  Dim m_Line As MixerLine
  Dim m_Volume As Integer
  Dim m_Muted As Boolean

  Public Sub New()
    m_Line = m_Mixer.Playback.Lines.GetMixerFirstLineByComponentType(MIXERLINE_COMPONENTTYPE.DST_SPEAKERS)
    AddHandler m_Mixer.Playback.MixerLineChanged, AddressOf MixerChanged
  End Sub

  Public Property Volume() As Integer
    Get
      If m_Line Is Nothing Then Exit Property
      Return CInt((m_Line.Volume / (m_Line.VolumeMax - m_Line.VolumeMin)) * 100)
    End Get
    Set(ByVal value As Integer)
      If m_Line Is Nothing Then Exit Property
      m_Line.Volume = ((m_Line.VolumeMax - m_Line.VolumeMin) * (value / 100))
    End Set
  End Property

  Public Property Mute() As Boolean
    Get
      If m_Line Is Nothing Then Exit Property
      Return m_Line.Mute
    End Get
    Set(ByVal value As Boolean)
      If m_Line Is Nothing Then Exit Property
      m_Line.Mute = value
    End Set
  End Property

  Private Sub MixerChanged(ByVal mixer As Mixer, ByVal line As MixerLine)
    If line.Volume <> m_Volume Then
      m_Volume = line.Volume
      RaiseEvent VolumeChanged()
    End If
    If line.Mute <> m_Muted Then
      m_Muted = line.Mute
      RaiseEvent MuteChanged()
    End If
  End Sub

End Class