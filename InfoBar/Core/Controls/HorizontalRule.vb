Namespace Controls

    Partial Public Class HorizontalRule

        Private Sub HorizontalRule_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Me.Paint
            e.Graphics.DrawLine(SystemPens.ButtonShadow, 0, 0, Me.Width, 0)
            e.Graphics.DrawLine(SystemPens.ButtonHighlight, 0, 1, Me.Width, 1)
        End Sub

    End Class

End Namespace
