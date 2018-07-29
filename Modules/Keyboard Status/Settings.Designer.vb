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
    Me.grpAppearance = New System.Windows.Forms.GroupBox
    Me.chkShowCapsLockIcon = New System.Windows.Forms.CheckBox
    Me.chkShowScrollLockIcon = New System.Windows.Forms.CheckBox
    Me.chkShowNumLockIcon = New System.Windows.Forms.CheckBox
    Me.grpAppearance.SuspendLayout()
    Me.SuspendLayout()
    '
    'chkShowIcon
    '
    Me.chkShowIcon.AutoSize = True
    Me.chkShowIcon.Location = New System.Drawing.Point(8, 19)
    Me.chkShowIcon.Margin = New System.Windows.Forms.Padding(0, 0, 8, 16)
    Me.chkShowIcon.Name = "chkShowIcon"
    Me.chkShowIcon.Size = New System.Drawing.Size(131, 17)
    Me.chkShowIcon.TabIndex = 6
    Me.chkShowIcon.Text = "Show Keyboard Icon"
    Me.chkShowIcon.UseVisualStyleBackColor = True
    '
    'grpAppearance
    '
    Me.grpAppearance.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.grpAppearance.Controls.Add(Me.chkShowIcon)
    Me.grpAppearance.Controls.Add(Me.chkShowCapsLockIcon)
    Me.grpAppearance.Controls.Add(Me.chkShowScrollLockIcon)
    Me.grpAppearance.Controls.Add(Me.chkShowNumLockIcon)
    Me.grpAppearance.Location = New System.Drawing.Point(0, 0)
    Me.grpAppearance.Margin = New System.Windows.Forms.Padding(0)
    Me.grpAppearance.Name = "grpAppearance"
    Me.grpAppearance.Padding = New System.Windows.Forms.Padding(8, 4, 8, 8)
    Me.grpAppearance.Size = New System.Drawing.Size(500, 119)
    Me.grpAppearance.TabIndex = 11
    Me.grpAppearance.TabStop = False
    Me.grpAppearance.Text = "Appearance"
    '
    'chkShowCapsLockIcon
    '
    Me.chkShowCapsLockIcon.AutoSize = True
    Me.chkShowCapsLockIcon.Location = New System.Drawing.Point(8, 52)
    Me.chkShowCapsLockIcon.Margin = New System.Windows.Forms.Padding(0, 0, 0, 4)
    Me.chkShowCapsLockIcon.Name = "chkShowCapsLockIcon"
    Me.chkShowCapsLockIcon.Size = New System.Drawing.Size(134, 17)
    Me.chkShowCapsLockIcon.TabIndex = 7
    Me.chkShowCapsLockIcon.Text = "Show Caps Lock Icon"
    Me.chkShowCapsLockIcon.UseVisualStyleBackColor = True
    '
    'chkShowScrollLockIcon
    '
    Me.chkShowScrollLockIcon.AutoSize = True
    Me.chkShowScrollLockIcon.Location = New System.Drawing.Point(8, 94)
    Me.chkShowScrollLockIcon.Margin = New System.Windows.Forms.Padding(0, 0, 0, 4)
    Me.chkShowScrollLockIcon.Name = "chkShowScrollLockIcon"
    Me.chkShowScrollLockIcon.Size = New System.Drawing.Size(137, 17)
    Me.chkShowScrollLockIcon.TabIndex = 9
    Me.chkShowScrollLockIcon.Text = "Show Scroll Lock Icon"
    Me.chkShowScrollLockIcon.UseVisualStyleBackColor = True
    '
    'chkShowNumLockIcon
    '
    Me.chkShowNumLockIcon.AutoSize = True
    Me.chkShowNumLockIcon.Location = New System.Drawing.Point(8, 73)
    Me.chkShowNumLockIcon.Margin = New System.Windows.Forms.Padding(0, 0, 0, 4)
    Me.chkShowNumLockIcon.Name = "chkShowNumLockIcon"
    Me.chkShowNumLockIcon.Size = New System.Drawing.Size(133, 17)
    Me.chkShowNumLockIcon.TabIndex = 8
    Me.chkShowNumLockIcon.Text = "Show Num Lock Icon"
    Me.chkShowNumLockIcon.UseVisualStyleBackColor = True
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
  Friend WithEvents grpAppearance As System.Windows.Forms.GroupBox
  Friend WithEvents chkShowCapsLockIcon As System.Windows.Forms.CheckBox
  Friend WithEvents chkShowNumLockIcon As System.Windows.Forms.CheckBox
  Friend WithEvents chkShowScrollLockIcon As System.Windows.Forms.CheckBox

End Class
