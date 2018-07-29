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
    Me.Tabs = New System.Windows.Forms.TabControl
    Me.TabPage1 = New System.Windows.Forms.TabPage
    Me.grpCheckInterval = New System.Windows.Forms.GroupBox
    Me.lblCheckMinutes = New System.Windows.Forms.Label
    Me.lblIntervalEvery = New System.Windows.Forms.Label
    Me.nudCheckInterval = New System.Windows.Forms.NumericUpDown
    Me.grpLogin = New System.Windows.Forms.GroupBox
    Me.lblEmail = New System.Windows.Forms.Label
    Me.txtEmail = New System.Windows.Forms.TextBox
    Me.lblPassword = New System.Windows.Forms.Label
    Me.txtPassword = New System.Windows.Forms.TextBox
    Me.Tabs.SuspendLayout()
    Me.TabPage1.SuspendLayout()
    Me.grpCheckInterval.SuspendLayout()
    CType(Me.nudCheckInterval, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.grpLogin.SuspendLayout()
    Me.SuspendLayout()
    '
    'Tabs
    '
    Me.Tabs.Controls.Add(Me.TabPage1)
    Me.Tabs.Dock = System.Windows.Forms.DockStyle.Fill
    Me.Tabs.Location = New System.Drawing.Point(0, 0)
    Me.Tabs.Name = "Tabs"
    Me.Tabs.SelectedIndex = 0
    Me.Tabs.Size = New System.Drawing.Size(500, 400)
    Me.Tabs.TabIndex = 2
    '
    'TabPage1
    '
    Me.TabPage1.Controls.Add(Me.grpCheckInterval)
    Me.TabPage1.Controls.Add(Me.grpLogin)
    Me.TabPage1.Location = New System.Drawing.Point(4, 22)
    Me.TabPage1.Margin = New System.Windows.Forms.Padding(0)
    Me.TabPage1.Name = "TabPage1"
    Me.TabPage1.Padding = New System.Windows.Forms.Padding(8)
    Me.TabPage1.Size = New System.Drawing.Size(492, 374)
    Me.TabPage1.TabIndex = 0
    Me.TabPage1.Text = "General"
    Me.TabPage1.UseVisualStyleBackColor = True
    '
    'grpCheckInterval
    '
    Me.grpCheckInterval.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.grpCheckInterval.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
    Me.grpCheckInterval.Controls.Add(Me.lblCheckMinutes)
    Me.grpCheckInterval.Controls.Add(Me.lblIntervalEvery)
    Me.grpCheckInterval.Controls.Add(Me.nudCheckInterval)
    Me.grpCheckInterval.Location = New System.Drawing.Point(8, 99)
    Me.grpCheckInterval.Margin = New System.Windows.Forms.Padding(0, 0, 0, 4)
    Me.grpCheckInterval.Name = "grpCheckInterval"
    Me.grpCheckInterval.Padding = New System.Windows.Forms.Padding(8, 4, 8, 8)
    Me.grpCheckInterval.Size = New System.Drawing.Size(476, 49)
    Me.grpCheckInterval.TabIndex = 2
    Me.grpCheckInterval.TabStop = False
    Me.grpCheckInterval.Text = "Check Interval"
    '
    'lblCheckMinutes
    '
    Me.lblCheckMinutes.AutoSize = True
    Me.lblCheckMinutes.Location = New System.Drawing.Point(220, 21)
    Me.lblCheckMinutes.Name = "lblCheckMinutes"
    Me.lblCheckMinutes.Size = New System.Drawing.Size(48, 13)
    Me.lblCheckMinutes.TabIndex = 3
    Me.lblCheckMinutes.Text = "minutes"
    Me.lblCheckMinutes.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
    '
    'lblIntervalEvery
    '
    Me.lblIntervalEvery.Location = New System.Drawing.Point(11, 16)
    Me.lblIntervalEvery.Name = "lblIntervalEvery"
    Me.lblIntervalEvery.Size = New System.Drawing.Size(137, 22)
    Me.lblIntervalEvery.TabIndex = 1
    Me.lblIntervalEvery.Text = "Check for new mail every:"
    Me.lblIntervalEvery.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
    '
    'nudCheckInterval
    '
    Me.nudCheckInterval.Location = New System.Drawing.Point(151, 19)
    Me.nudCheckInterval.Margin = New System.Windows.Forms.Padding(0)
    Me.nudCheckInterval.Maximum = New Decimal(New Integer() {1440, 0, 0, 0})
    Me.nudCheckInterval.Minimum = New Decimal(New Integer() {10, 0, 0, 0})
    Me.nudCheckInterval.Name = "nudCheckInterval"
    Me.nudCheckInterval.Size = New System.Drawing.Size(66, 22)
    Me.nudCheckInterval.TabIndex = 2
    Me.nudCheckInterval.Value = New Decimal(New Integer() {10, 0, 0, 0})
    '
    'grpLogin
    '
    Me.grpLogin.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.grpLogin.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
    Me.grpLogin.Controls.Add(Me.lblEmail)
    Me.grpLogin.Controls.Add(Me.txtEmail)
    Me.grpLogin.Controls.Add(Me.lblPassword)
    Me.grpLogin.Controls.Add(Me.txtPassword)
    Me.grpLogin.Location = New System.Drawing.Point(8, 8)
    Me.grpLogin.Margin = New System.Windows.Forms.Padding(0, 0, 0, 4)
    Me.grpLogin.Name = "grpLogin"
    Me.grpLogin.Padding = New System.Windows.Forms.Padding(8, 4, 8, 8)
    Me.grpLogin.Size = New System.Drawing.Size(476, 87)
    Me.grpLogin.TabIndex = 0
    Me.grpLogin.TabStop = False
    Me.grpLogin.Text = "Gmail Login Information"
    '
    'lblEmail
    '
    Me.lblEmail.AutoSize = True
    Me.lblEmail.Location = New System.Drawing.Point(11, 26)
    Me.lblEmail.Name = "lblEmail"
    Me.lblEmail.Size = New System.Drawing.Size(37, 13)
    Me.lblEmail.TabIndex = 0
    Me.lblEmail.Text = "Email:"
    Me.lblEmail.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
    '
    'txtEmail
    '
    Me.txtEmail.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.txtEmail.Location = New System.Drawing.Point(77, 23)
    Me.txtEmail.Margin = New System.Windows.Forms.Padding(4)
    Me.txtEmail.Name = "txtEmail"
    Me.txtEmail.Size = New System.Drawing.Size(387, 22)
    Me.txtEmail.TabIndex = 2
    '
    'lblPassword
    '
    Me.lblPassword.AutoSize = True
    Me.lblPassword.Location = New System.Drawing.Point(11, 56)
    Me.lblPassword.Name = "lblPassword"
    Me.lblPassword.Size = New System.Drawing.Size(59, 13)
    Me.lblPassword.TabIndex = 1
    Me.lblPassword.Text = "Password:"
    Me.lblPassword.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
    '
    'txtPassword
    '
    Me.txtPassword.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.txtPassword.Location = New System.Drawing.Point(77, 53)
    Me.txtPassword.Margin = New System.Windows.Forms.Padding(4)
    Me.txtPassword.Name = "txtPassword"
    Me.txtPassword.Size = New System.Drawing.Size(387, 22)
    Me.txtPassword.TabIndex = 3
    Me.txtPassword.UseSystemPasswordChar = True
    '
    'Settings
    '
    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit
    Me.Controls.Add(Me.Tabs)
    Me.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.Name = "Settings"
    Me.Size = New System.Drawing.Size(500, 400)
    Me.Tabs.ResumeLayout(False)
    Me.TabPage1.ResumeLayout(False)
    Me.grpCheckInterval.ResumeLayout(False)
    Me.grpCheckInterval.PerformLayout()
    CType(Me.nudCheckInterval, System.ComponentModel.ISupportInitialize).EndInit()
    Me.grpLogin.ResumeLayout(False)
    Me.grpLogin.PerformLayout()
    Me.ResumeLayout(False)

  End Sub
  Friend WithEvents Tabs As System.Windows.Forms.TabControl
  Friend WithEvents TabPage1 As System.Windows.Forms.TabPage
  Friend WithEvents grpLogin As System.Windows.Forms.GroupBox
  Friend WithEvents grpCheckInterval As System.Windows.Forms.GroupBox
  Friend WithEvents nudCheckInterval As System.Windows.Forms.NumericUpDown
  Friend WithEvents lblIntervalEvery As System.Windows.Forms.Label
  Friend WithEvents lblCheckMinutes As System.Windows.Forms.Label
  Friend WithEvents lblEmail As System.Windows.Forms.Label
  Friend WithEvents lblPassword As System.Windows.Forms.Label
  Friend WithEvents txtEmail As System.Windows.Forms.TextBox
  Friend WithEvents txtPassword As System.Windows.Forms.TextBox
End Class
