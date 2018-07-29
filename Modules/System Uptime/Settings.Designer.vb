<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Friend Class Settings
    Inherits System.Windows.Forms.UserControl

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
    Me.chkShowIcon = New System.Windows.Forms.CheckBox
    Me.chkShowText = New System.Windows.Forms.CheckBox
    Me.grpAppearance = New System.Windows.Forms.GroupBox
    Me.grpAppearance.SuspendLayout()
    Me.SuspendLayout()
    '
    'chkShowIcon
    '
    Me.chkShowIcon.AutoSize = True
    Me.chkShowIcon.Location = New System.Drawing.Point(8, 19)
    Me.chkShowIcon.Margin = New System.Windows.Forms.Padding(0, 0, 8, 0)
    Me.chkShowIcon.Name = "chkShowIcon"
    Me.chkShowIcon.Size = New System.Drawing.Size(80, 17)
    Me.chkShowIcon.TabIndex = 6
    Me.chkShowIcon.Text = "Show Icon"
    Me.chkShowIcon.UseVisualStyleBackColor = True
    '
    'chkShowText
    '
    Me.chkShowText.AutoSize = True
    Me.chkShowText.Location = New System.Drawing.Point(96, 19)
    Me.chkShowText.Margin = New System.Windows.Forms.Padding(0, 0, 8, 0)
    Me.chkShowText.Name = "chkShowText"
    Me.chkShowText.Size = New System.Drawing.Size(78, 17)
    Me.chkShowText.TabIndex = 7
    Me.chkShowText.Text = "Show Text"
    Me.chkShowText.UseVisualStyleBackColor = True
    '
    'grpAppearance
    '
    Me.grpAppearance.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.grpAppearance.Controls.Add(Me.chkShowIcon)
    Me.grpAppearance.Controls.Add(Me.chkShowText)
    Me.grpAppearance.Location = New System.Drawing.Point(0, 0)
    Me.grpAppearance.Margin = New System.Windows.Forms.Padding(0)
    Me.grpAppearance.Name = "grpAppearance"
    Me.grpAppearance.Padding = New System.Windows.Forms.Padding(8, 4, 8, 8)
    Me.grpAppearance.Size = New System.Drawing.Size(500, 44)
    Me.grpAppearance.TabIndex = 0
    Me.grpAppearance.TabStop = False
    Me.grpAppearance.Text = "Appearance"
    '
    'Settings
    '
    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit
    Me.Controls.Add(Me.grpAppearance)
    Me.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.Margin = New System.Windows.Forms.Padding(0)
    Me.Name = "Settings"
    Me.Size = New System.Drawing.Size(500, 400)
    Me.grpAppearance.ResumeLayout(False)
    Me.grpAppearance.PerformLayout()
    Me.ResumeLayout(False)

  End Sub
    Friend WithEvents chkShowIcon As System.Windows.Forms.CheckBox
    Friend WithEvents chkShowText As System.Windows.Forms.CheckBox
  Friend WithEvents grpAppearance As System.Windows.Forms.GroupBox

End Class
