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
    Me.lblSliderWidthPixels = New System.Windows.Forms.Label
    Me.nudSliderWidth = New System.Windows.Forms.NumericUpDown
    Me.lblSliderWidth = New System.Windows.Forms.Label
    Me.grpAppearance.SuspendLayout()
    CType(Me.nudSliderWidth, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.SuspendLayout()
    '
    'chkShowIcon
    '
    Me.chkShowIcon.AutoSize = True
    Me.chkShowIcon.Location = New System.Drawing.Point(8, 20)
    Me.chkShowIcon.Margin = New System.Windows.Forms.Padding(0, 0, 8, 0)
    Me.chkShowIcon.Name = "chkShowIcon"
    Me.chkShowIcon.Size = New System.Drawing.Size(76, 17)
    Me.chkShowIcon.TabIndex = 6
    Me.chkShowIcon.Text = "Show Icon"
    Me.chkShowIcon.UseVisualStyleBackColor = True
    '
    'chkShowText
    '
    Me.chkShowText.AutoSize = True
    Me.chkShowText.Location = New System.Drawing.Point(92, 20)
    Me.chkShowText.Margin = New System.Windows.Forms.Padding(0)
    Me.chkShowText.Name = "chkShowText"
    Me.chkShowText.Size = New System.Drawing.Size(77, 17)
    Me.chkShowText.TabIndex = 7
    Me.chkShowText.Text = "Show Text"
    Me.chkShowText.UseVisualStyleBackColor = True
    '
    'grpAppearance
    '
    Me.grpAppearance.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.grpAppearance.Controls.Add(Me.lblSliderWidthPixels)
    Me.grpAppearance.Controls.Add(Me.nudSliderWidth)
    Me.grpAppearance.Controls.Add(Me.lblSliderWidth)
    Me.grpAppearance.Controls.Add(Me.chkShowIcon)
    Me.grpAppearance.Controls.Add(Me.chkShowText)
    Me.grpAppearance.Location = New System.Drawing.Point(0, 3)
    Me.grpAppearance.Name = "grpAppearance"
    Me.grpAppearance.Size = New System.Drawing.Size(500, 46)
    Me.grpAppearance.TabIndex = 11
    Me.grpAppearance.TabStop = False
    Me.grpAppearance.Text = "Appearance"
    '
    'lblSliderWidthPixels
    '
    Me.lblSliderWidthPixels.AutoSize = True
    Me.lblSliderWidthPixels.Location = New System.Drawing.Point(324, 21)
    Me.lblSliderWidthPixels.Name = "lblSliderWidthPixels"
    Me.lblSliderWidthPixels.Size = New System.Drawing.Size(34, 13)
    Me.lblSliderWidthPixels.TabIndex = 10
    Me.lblSliderWidthPixels.Text = "pixels"
    '
    'nudSliderWidth
    '
    Me.nudSliderWidth.Location = New System.Drawing.Point(258, 18)
    Me.nudSliderWidth.Maximum = New Decimal(New Integer() {1000, 0, 0, 0})
    Me.nudSliderWidth.Minimum = New Decimal(New Integer() {50, 0, 0, 0})
    Me.nudSliderWidth.Name = "nudSliderWidth"
    Me.nudSliderWidth.Size = New System.Drawing.Size(60, 21)
    Me.nudSliderWidth.TabIndex = 9
    Me.nudSliderWidth.Value = New Decimal(New Integer() {100, 0, 0, 0})
    '
    'lblSliderWidth
    '
    Me.lblSliderWidth.AutoSize = True
    Me.lblSliderWidth.Location = New System.Drawing.Point(184, 21)
    Me.lblSliderWidth.Name = "lblSliderWidth"
    Me.lblSliderWidth.Size = New System.Drawing.Size(68, 13)
    Me.lblSliderWidth.TabIndex = 8
    Me.lblSliderWidth.Text = "Slider Width:"
    '
    'Settings
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
    Me.Controls.Add(Me.grpAppearance)
    Me.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.Name = "Settings"
    Me.Size = New System.Drawing.Size(500, 400)
    Me.grpAppearance.ResumeLayout(False)
    Me.grpAppearance.PerformLayout()
    CType(Me.nudSliderWidth, System.ComponentModel.ISupportInitialize).EndInit()
    Me.ResumeLayout(False)

  End Sub
  Friend WithEvents chkShowIcon As System.Windows.Forms.CheckBox
    Friend WithEvents chkShowText As System.Windows.Forms.CheckBox
  Friend WithEvents grpAppearance As System.Windows.Forms.GroupBox
  Friend WithEvents lblSliderWidthPixels As System.Windows.Forms.Label
  Friend WithEvents nudSliderWidth As System.Windows.Forms.NumericUpDown
  Friend WithEvents lblSliderWidth As System.Windows.Forms.Label

End Class
