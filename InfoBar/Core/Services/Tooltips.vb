Namespace Services

  Public Class Tooltips

    Public Sub SetTooltipOwner(ByVal sModuleGUID As String, ByVal sModuleObjectID As String)
      NewTooltipOwnerGUID = sModuleGUID
      NewTooltipOwnerObjectID = sModuleObjectID
    End Sub

    Public Function GetTooltipOwnerGUID() As String
      Return CurrentTooltipOwnerGUID
    End Function

    Public Function GetTooltipOwnerObjectID() As String
      Return CurrentTooltipOwnerObjectID
    End Function

    Public Sub UpdateTooltip()
      fTooltip.UpdateTooltip()
    End Sub

  End Class

End Namespace