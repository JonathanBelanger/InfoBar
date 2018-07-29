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
    Me.tabpageGeneral = New System.Windows.Forms.TabPage
    Me.grpLocation = New System.Windows.Forms.GroupBox
    Me.txtLocation = New System.Windows.Forms.TextBox
    Me.btnLocationBrowse = New System.Windows.Forms.Button
    Me.grpLogin = New System.Windows.Forms.GroupBox
    Me.txtPort = New System.Windows.Forms.TextBox
    Me.lblPort = New System.Windows.Forms.Label
    Me.lblUsername = New System.Windows.Forms.Label
    Me.txtUsername = New System.Windows.Forms.TextBox
    Me.lblHostname = New System.Windows.Forms.Label
    Me.lblPassword = New System.Windows.Forms.Label
    Me.txtHostname = New System.Windows.Forms.TextBox
    Me.txtPassword = New System.Windows.Forms.TextBox
    Me.grpCheckInterval = New System.Windows.Forms.GroupBox
    Me.lblCheckSeconds = New System.Windows.Forms.Label
    Me.lblIntervalEvery = New System.Windows.Forms.Label
    Me.nudRefreshInterval = New System.Windows.Forms.NumericUpDown
    Me.GroupBox1 = New System.Windows.Forms.GroupBox
    Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel
    Me.TextBox1 = New System.Windows.Forms.TextBox
    Me.Label1 = New System.Windows.Forms.Label
    Me.Tabs.SuspendLayout()
    Me.tabpageGeneral.SuspendLayout()
    Me.grpLocation.SuspendLayout()
    Me.grpLogin.SuspendLayout()
    Me.grpCheckInterval.SuspendLayout()
    CType(Me.nudRefreshInterval, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.TableLayoutPanel1.SuspendLayout()
    Me.SuspendLayout()
    '
    'Tabs
    '
    Me.Tabs.Controls.Add(Me.tabpageGeneral)
    Me.Tabs.Dock = System.Windows.Forms.DockStyle.Fill
    Me.Tabs.Location = New System.Drawing.Point(0, 0)
    Me.Tabs.Name = "Tabs"
    Me.Tabs.SelectedIndex = 0
    Me.Tabs.Size = New System.Drawing.Size(500, 400)
    Me.Tabs.TabIndex = 2
    '
    'tabpageGeneral
    '
    Me.tabpageGeneral.Controls.Add(Me.grpLogin)
    Me.tabpageGeneral.Controls.Add(Me.grpLocation)
    Me.tabpageGeneral.Controls.Add(Me.grpCheckInterval)
    Me.tabpageGeneral.Location = New System.Drawing.Point(4, 22)
    Me.tabpageGeneral.Margin = New System.Windows.Forms.Padding(0)
    Me.tabpageGeneral.Name = "tabpageGeneral"
    Me.tabpageGeneral.Padding = New System.Windows.Forms.Padding(8)
    Me.tabpageGeneral.Size = New System.Drawing.Size(492, 374)
    Me.tabpageGeneral.TabIndex = 0
    Me.tabpageGeneral.Text = "General"
    Me.tabpageGeneral.UseVisualStyleBackColor = True
    '
    'grpLocation
    '
    Me.grpLocation.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.grpLocation.Controls.Add(Me.btnLocationBrowse)
    Me.grpLocation.Controls.Add(Me.txtLocation)
    Me.grpLocation.Location = New System.Drawing.Point(8, 129)
    Me.grpLocation.Margin = New System.Windows.Forms.Padding(0, 0, 0, 4)
    Me.grpLocation.Name = "grpLocation"
    Me.grpLocation.Size = New System.Drawing.Size(476, 57)
    Me.grpLocation.TabIndex = 3
    Me.grpLocation.TabStop = False
    Me.grpLocation.Text = "uTorrent Location"
    '
    'txtLocation
    '
    Me.txtLocation.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.txtLocation.Location = New System.Drawing.Point(8, 23)
    Me.txtLocation.Margin = New System.Windows.Forms.Padding(4)
    Me.txtLocation.Name = "txtLocation"
    Me.txtLocation.Size = New System.Drawing.Size(399, 22)
    Me.txtLocation.TabIndex = 3
    '
    'btnLocationBrowse
    '
    Me.btnLocationBrowse.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.btnLocationBrowse.Location = New System.Drawing.Point(414, 23)
    Me.btnLocationBrowse.Name = "btnLocationBrowse"
    Me.btnLocationBrowse.Size = New System.Drawing.Size(54, 22)
    Me.btnLocationBrowse.TabIndex = 4
    Me.btnLocationBrowse.Text = "..."
    Me.btnLocationBrowse.UseVisualStyleBackColor = True
    '
    'grpLogin
    '
    Me.grpLogin.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.grpLogin.Controls.Add(Me.txtPort)
    Me.grpLogin.Controls.Add(Me.lblHostname)
    Me.grpLogin.Controls.Add(Me.txtHostname)
    Me.grpLogin.Controls.Add(Me.lblPort)
    Me.grpLogin.Controls.Add(Me.lblPassword)
    Me.grpLogin.Controls.Add(Me.txtPassword)
    Me.grpLogin.Controls.Add(Me.lblUsername)
    Me.grpLogin.Controls.Add(Me.txtUsername)
    Me.grpLogin.Location = New System.Drawing.Point(8, 8)
    Me.grpLogin.Margin = New System.Windows.Forms.Padding(0, 0, 0, 4)
    Me.grpLogin.Name = "grpLogin"
    Me.grpLogin.Padding = New System.Windows.Forms.Padding(8, 4, 8, 8)
    Me.grpLogin.Size = New System.Drawing.Size(476, 117)
    Me.grpLogin.TabIndex = 0
    Me.grpLogin.TabStop = False
    Me.grpLogin.Text = "Connection Information"
    '
    'txtPort
    '
    Me.txtPort.Location = New System.Drawing.Point(412, 23)
    Me.txtPort.Margin = New System.Windows.Forms.Padding(4)
    Me.txtPort.Name = "txtPort"
    Me.txtPort.Size = New System.Drawing.Size(52, 22)
    Me.txtPort.TabIndex = 6
    '
    'lblPort
    '
    Me.lblPort.AutoSize = True
    Me.lblPort.Location = New System.Drawing.Point(374, 26)
    Me.lblPort.Name = "lblPort"
    Me.lblPort.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.lblPort.Size = New System.Drawing.Size(31, 13)
    Me.lblPort.TabIndex = 5
    Me.lblPort.Text = "Port:"
    Me.lblPort.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
    '
    'lblUsername
    '
    Me.lblUsername.AutoSize = True
    Me.lblUsername.Location = New System.Drawing.Point(11, 56)
    Me.lblUsername.Name = "lblUsername"
    Me.lblUsername.Size = New System.Drawing.Size(61, 13)
    Me.lblUsername.TabIndex = 1
    Me.lblUsername.Text = "Username:"
    Me.lblUsername.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
    '
    'txtUsername
    '
    Me.txtUsername.Location = New System.Drawing.Point(84, 53)
    Me.txtUsername.Margin = New System.Windows.Forms.Padding(4)
    Me.txtUsername.Name = "txtUsername"
    Me.txtUsername.Size = New System.Drawing.Size(380, 22)
    Me.txtUsername.TabIndex = 3
    '
    'lblHostname
    '
    Me.lblHostname.AutoSize = True
    Me.lblHostname.Location = New System.Drawing.Point(11, 26)
    Me.lblHostname.Name = "lblHostname"
    Me.lblHostname.Size = New System.Drawing.Size(66, 13)
    Me.lblHostname.TabIndex = 4
    Me.lblHostname.Text = "Host Name:"
    Me.lblHostname.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
    '
    'lblPassword
    '
    Me.lblPassword.AutoSize = True
    Me.lblPassword.Location = New System.Drawing.Point(11, 86)
    Me.lblPassword.Name = "lblPassword"
    Me.lblPassword.Size = New System.Drawing.Size(59, 13)
    Me.lblPassword.TabIndex = 0
    Me.lblPassword.Text = "Password:"
    Me.lblPassword.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
    '
    'txtHostname
    '
    Me.txtHostname.Location = New System.Drawing.Point(84, 23)
    Me.txtHostname.Margin = New System.Windows.Forms.Padding(4)
    Me.txtHostname.Name = "txtHostname"
    Me.txtHostname.Size = New System.Drawing.Size(283, 22)
    Me.txtHostname.TabIndex = 3
    '
    'txtPassword
    '
    Me.txtPassword.Location = New System.Drawing.Point(84, 83)
    Me.txtPassword.Margin = New System.Windows.Forms.Padding(4)
    Me.txtPassword.Name = "txtPassword"
    Me.txtPassword.Size = New System.Drawing.Size(380, 22)
    Me.txtPassword.TabIndex = 2
    Me.txtPassword.UseSystemPasswordChar = True
    '
    'grpCheckInterval
    '
    Me.grpCheckInterval.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.grpCheckInterval.Controls.Add(Me.lblCheckSeconds)
    Me.grpCheckInterval.Controls.Add(Me.lblIntervalEvery)
    Me.grpCheckInterval.Controls.Add(Me.nudRefreshInterval)
    Me.grpCheckInterval.Location = New System.Drawing.Point(8, 190)
    Me.grpCheckInterval.Margin = New System.Windows.Forms.Padding(0, 0, 0, 4)
    Me.grpCheckInterval.Name = "grpCheckInterval"
    Me.grpCheckInterval.Padding = New System.Windows.Forms.Padding(8, 4, 8, 8)
    Me.grpCheckInterval.Size = New System.Drawing.Size(476, 49)
    Me.grpCheckInterval.TabIndex = 2
    Me.grpCheckInterval.TabStop = False
    Me.grpCheckInterval.Text = "Refresh Interval"
    '
    'lblCheckSeconds
    '
    Me.lblCheckSeconds.AutoSize = True
    Me.lblCheckSeconds.Location = New System.Drawing.Point(234, 21)
    Me.lblCheckSeconds.Name = "lblCheckSeconds"
    Me.lblCheckSeconds.Size = New System.Drawing.Size(55, 13)
    Me.lblCheckSeconds.TabIndex = 3
    Me.lblCheckSeconds.Text = "second(s)"
    Me.lblCheckSeconds.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
    '
    'lblIntervalEvery
    '
    Me.lblIntervalEvery.AutoSize = True
    Me.lblIntervalEvery.Location = New System.Drawing.Point(11, 21)
    Me.lblIntervalEvery.Name = "lblIntervalEvery"
    Me.lblIntervalEvery.Size = New System.Drawing.Size(151, 13)
    Me.lblIntervalEvery.TabIndex = 1
    Me.lblIntervalEvery.Text = "Refresh uTorrent data every:"
    Me.lblIntervalEvery.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
    '
    'nudRefreshInterval
    '
    Me.nudRefreshInterval.Location = New System.Drawing.Point(165, 19)
    Me.nudRefreshInterval.Margin = New System.Windows.Forms.Padding(0)
    Me.nudRefreshInterval.Maximum = New Decimal(New Integer() {1440, 0, 0, 0})
    Me.nudRefreshInterval.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
    Me.nudRefreshInterval.Name = "nudRefreshInterval"
    Me.nudRefreshInterval.Size = New System.Drawing.Size(66, 22)
    Me.nudRefreshInterval.TabIndex = 2
    Me.nudRefreshInterval.Value = New Decimal(New Integer() {1, 0, 0, 0})
    '
    'GroupBox1
    '
    Me.GroupBox1.AutoSize = True
    Me.GroupBox1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
    Me.GroupBox1.Location = New System.Drawing.Point(0, 0)
    Me.GroupBox1.Name = "GroupBox1"
    Me.GroupBox1.Size = New System.Drawing.Size(200, 100)
    Me.GroupBox1.TabIndex = 0
    Me.GroupBox1.TabStop = False
    '
    'TableLayoutPanel1
    '
    Me.TableLayoutPanel1.AutoSize = True
    Me.TableLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
    Me.TableLayoutPanel1.ColumnCount = 4
    Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle)
    Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
    Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle)
    Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 60.0!))
    Me.TableLayoutPanel1.Controls.Add(Me.TextBox1, 3, 0)
    Me.TableLayoutPanel1.Location = New System.Drawing.Point(0, 0)
    Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
    Me.TableLayoutPanel1.RowCount = 1
    Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
    Me.TableLayoutPanel1.Size = New System.Drawing.Size(200, 100)
    Me.TableLayoutPanel1.TabIndex = 0
    '
    'TextBox1
    '
    Me.TextBox1.Dock = System.Windows.Forms.DockStyle.Fill
    Me.TextBox1.Location = New System.Drawing.Point(144, 4)
    Me.TextBox1.Margin = New System.Windows.Forms.Padding(4)
    Me.TextBox1.Name = "TextBox1"
    Me.TextBox1.Size = New System.Drawing.Size(52, 20)
    Me.TextBox1.TabIndex = 6
    '
    'Label1
    '
    Me.Label1.AutoSize = True
    Me.Label1.Dock = System.Windows.Forms.DockStyle.Fill
    Me.Label1.Location = New System.Drawing.Point(350, 0)
    Me.Label1.Name = "Label1"
    Me.Label1.RightToLeft = System.Windows.Forms.RightToLeft.No
    Me.Label1.Size = New System.Drawing.Size(31, 30)
    Me.Label1.TabIndex = 5
    Me.Label1.Text = "Port:"
    Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
    '
    'Settings
    '
    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit
    Me.Controls.Add(Me.Tabs)
    Me.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.Name = "Settings"
    Me.Size = New System.Drawing.Size(500, 400)
    Me.Tabs.ResumeLayout(False)
    Me.tabpageGeneral.ResumeLayout(False)
    Me.grpLocation.ResumeLayout(False)
    Me.grpLocation.PerformLayout()
    Me.grpLogin.ResumeLayout(False)
    Me.grpLogin.PerformLayout()
    Me.grpCheckInterval.ResumeLayout(False)
    Me.grpCheckInterval.PerformLayout()
    CType(Me.nudRefreshInterval, System.ComponentModel.ISupportInitialize).EndInit()
    Me.TableLayoutPanel1.ResumeLayout(False)
    Me.TableLayoutPanel1.PerformLayout()
    Me.ResumeLayout(False)

  End Sub
  Friend WithEvents Tabs As System.Windows.Forms.TabControl
  Friend WithEvents tabpageGeneral As System.Windows.Forms.TabPage
  Friend WithEvents grpLogin As System.Windows.Forms.GroupBox
  Friend WithEvents grpCheckInterval As System.Windows.Forms.GroupBox
  Friend WithEvents nudRefreshInterval As System.Windows.Forms.NumericUpDown
  Friend WithEvents lblIntervalEvery As System.Windows.Forms.Label
  Friend WithEvents lblCheckSeconds As System.Windows.Forms.Label
  Friend WithEvents lblPassword As System.Windows.Forms.Label
  Friend WithEvents lblUsername As System.Windows.Forms.Label
  Friend WithEvents txtPassword As System.Windows.Forms.TextBox
  Friend WithEvents txtUsername As System.Windows.Forms.TextBox
  Friend WithEvents lblPort As System.Windows.Forms.Label
  Friend WithEvents lblHostname As System.Windows.Forms.Label
  Friend WithEvents txtHostname As System.Windows.Forms.TextBox
  Friend WithEvents txtPort As System.Windows.Forms.TextBox
  Friend WithEvents grpLocation As System.Windows.Forms.GroupBox
  Friend WithEvents txtLocation As System.Windows.Forms.TextBox
  Friend WithEvents btnLocationBrowse As System.Windows.Forms.Button
  Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
  Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
  Friend WithEvents TextBox1 As System.Windows.Forms.TextBox
  Friend WithEvents Label1 As System.Windows.Forms.Label
End Class
