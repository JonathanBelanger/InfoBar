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
    Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Settings))
    Me.chkShowIcon = New System.Windows.Forms.CheckBox
    Me.lblToolbarTextDisplayUnit = New System.Windows.Forms.Label
    Me.comDisplayUnit = New System.Windows.Forms.ComboBox
    Me.lblTooltipDisplayUnit = New System.Windows.Forms.Label
    Me.comTooltipDisplayUnit = New System.Windows.Forms.ComboBox
    Me.lblTextFormatString = New System.Windows.Forms.Label
    Me.txtTextFormatString = New System.Windows.Forms.TextBox
    Me.txtTextFormatHelp = New System.Windows.Forms.TextBox
    Me.chkShowText = New System.Windows.Forms.CheckBox
    Me.Tabs = New System.Windows.Forms.TabControl
    Me.tabPageGeneral = New System.Windows.Forms.TabPage
    Me.lblDrives = New System.Windows.Forms.Label
    Me.lvDrives = New System.Windows.Forms.ListView
    Me.colhdrDrive = New System.Windows.Forms.ColumnHeader
    Me.tabPageDisplay = New System.Windows.Forms.TabPage
    Me.Tabs.SuspendLayout()
    Me.tabPageGeneral.SuspendLayout()
    Me.tabPageDisplay.SuspendLayout()
    Me.SuspendLayout()
    '
    'chkShowIcon
    '
    Me.chkShowIcon.AutoSize = True
    Me.chkShowIcon.Location = New System.Drawing.Point(8, 12)
    Me.chkShowIcon.Margin = New System.Windows.Forms.Padding(0, 0, 8, 0)
    Me.chkShowIcon.Name = "chkShowIcon"
    Me.chkShowIcon.Size = New System.Drawing.Size(80, 17)
    Me.chkShowIcon.TabIndex = 9
    Me.chkShowIcon.Text = "Show Icon"
    Me.chkShowIcon.UseVisualStyleBackColor = True
    '
    'lblToolbarTextDisplayUnit
    '
    Me.lblToolbarTextDisplayUnit.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
    Me.lblToolbarTextDisplayUnit.AutoSize = True
    Me.lblToolbarTextDisplayUnit.Location = New System.Drawing.Point(5, 348)
    Me.lblToolbarTextDisplayUnit.Margin = New System.Windows.Forms.Padding(0, 2, 3, 8)
    Me.lblToolbarTextDisplayUnit.Name = "lblToolbarTextDisplayUnit"
    Me.lblToolbarTextDisplayUnit.Size = New System.Drawing.Size(95, 13)
    Me.lblToolbarTextDisplayUnit.TabIndex = 0
    Me.lblToolbarTextDisplayUnit.Text = "Text Display Unit:"
    Me.lblToolbarTextDisplayUnit.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
    Me.lblToolbarTextDisplayUnit.UseMnemonic = False
    '
    'comDisplayUnit
    '
    Me.comDisplayUnit.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
    Me.comDisplayUnit.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
    Me.comDisplayUnit.FormattingEnabled = True
    Me.comDisplayUnit.Items.AddRange(New Object() {"Auto", "Bits", "Kilobits", "Megabits", "Gigabits", "Terabits", "Bytes", "Kilobytes", "Megabytes", "Gigabytes", "Terabytes"})
    Me.comDisplayUnit.Location = New System.Drawing.Point(103, 345)
    Me.comDisplayUnit.Margin = New System.Windows.Forms.Padding(0, 0, 0, 8)
    Me.comDisplayUnit.Name = "comDisplayUnit"
    Me.comDisplayUnit.Size = New System.Drawing.Size(121, 21)
    Me.comDisplayUnit.TabIndex = 4
    '
    'lblTooltipDisplayUnit
    '
    Me.lblTooltipDisplayUnit.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
    Me.lblTooltipDisplayUnit.AutoSize = True
    Me.lblTooltipDisplayUnit.Location = New System.Drawing.Point(232, 348)
    Me.lblTooltipDisplayUnit.Margin = New System.Windows.Forms.Padding(8, 2, 3, 10)
    Me.lblTooltipDisplayUnit.Name = "lblTooltipDisplayUnit"
    Me.lblTooltipDisplayUnit.Size = New System.Drawing.Size(111, 13)
    Me.lblTooltipDisplayUnit.TabIndex = 0
    Me.lblTooltipDisplayUnit.Text = "Tooltip Display Unit:"
    Me.lblTooltipDisplayUnit.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
    '
    'comTooltipDisplayUnit
    '
    Me.comTooltipDisplayUnit.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
    Me.comTooltipDisplayUnit.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
    Me.comTooltipDisplayUnit.FormattingEnabled = True
    Me.comTooltipDisplayUnit.Items.AddRange(New Object() {"Auto", "Bits", "Kilobits", "Megabits", "Gigabits", "Terabits", "Bytes", "Kilobytes", "Megabytes", "Gigabytes", "Terabytes"})
    Me.comTooltipDisplayUnit.Location = New System.Drawing.Point(346, 345)
    Me.comTooltipDisplayUnit.Margin = New System.Windows.Forms.Padding(0)
    Me.comTooltipDisplayUnit.Name = "comTooltipDisplayUnit"
    Me.comTooltipDisplayUnit.Size = New System.Drawing.Size(121, 21)
    Me.comTooltipDisplayUnit.TabIndex = 5
    '
    'lblTextFormatString
    '
    Me.lblTextFormatString.AutoSize = True
    Me.lblTextFormatString.Location = New System.Drawing.Point(5, 41)
    Me.lblTextFormatString.Margin = New System.Windows.Forms.Padding(0)
    Me.lblTextFormatString.Name = "lblTextFormatString"
    Me.lblTextFormatString.Size = New System.Drawing.Size(103, 13)
    Me.lblTextFormatString.TabIndex = 0
    Me.lblTextFormatString.Text = "Text Format String:"
    '
    'txtTextFormatString
    '
    Me.txtTextFormatString.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.txtTextFormatString.Location = New System.Drawing.Point(8, 58)
    Me.txtTextFormatString.Margin = New System.Windows.Forms.Padding(0, 4, 0, 0)
    Me.txtTextFormatString.Multiline = True
    Me.txtTextFormatString.Name = "txtTextFormatString"
    Me.txtTextFormatString.ScrollBars = System.Windows.Forms.ScrollBars.Both
    Me.txtTextFormatString.Size = New System.Drawing.Size(476, 144)
    Me.txtTextFormatString.TabIndex = 1
    '
    'txtTextFormatHelp
    '
    Me.txtTextFormatHelp.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.txtTextFormatHelp.Location = New System.Drawing.Point(8, 210)
    Me.txtTextFormatHelp.Margin = New System.Windows.Forms.Padding(0, 8, 0, 0)
    Me.txtTextFormatHelp.Multiline = True
    Me.txtTextFormatHelp.Name = "txtTextFormatHelp"
    Me.txtTextFormatHelp.ReadOnly = True
    Me.txtTextFormatHelp.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
    Me.txtTextFormatHelp.Size = New System.Drawing.Size(476, 122)
    Me.txtTextFormatHelp.TabIndex = 2
    Me.txtTextFormatHelp.Text = resources.GetString("txtTextFormatHelp.Text")
    '
    'chkShowText
    '
    Me.chkShowText.AutoSize = True
    Me.chkShowText.Location = New System.Drawing.Point(96, 12)
    Me.chkShowText.Margin = New System.Windows.Forms.Padding(0)
    Me.chkShowText.Name = "chkShowText"
    Me.chkShowText.Size = New System.Drawing.Size(78, 17)
    Me.chkShowText.TabIndex = 9
    Me.chkShowText.Text = "Show Text"
    Me.chkShowText.UseVisualStyleBackColor = True
    '
    'Tabs
    '
    Me.Tabs.Controls.Add(Me.tabPageGeneral)
    Me.Tabs.Controls.Add(Me.tabPageDisplay)
    Me.Tabs.Dock = System.Windows.Forms.DockStyle.Fill
    Me.Tabs.Location = New System.Drawing.Point(0, 0)
    Me.Tabs.Name = "Tabs"
    Me.Tabs.SelectedIndex = 0
    Me.Tabs.Size = New System.Drawing.Size(500, 400)
    Me.Tabs.TabIndex = 14
    '
    'tabPageGeneral
    '
    Me.tabPageGeneral.Controls.Add(Me.lblDrives)
    Me.tabPageGeneral.Controls.Add(Me.lvDrives)
    Me.tabPageGeneral.Location = New System.Drawing.Point(4, 22)
    Me.tabPageGeneral.Margin = New System.Windows.Forms.Padding(0)
    Me.tabPageGeneral.Name = "tabPageGeneral"
    Me.tabPageGeneral.Size = New System.Drawing.Size(492, 374)
    Me.tabPageGeneral.TabIndex = 0
    Me.tabPageGeneral.Text = "General"
    Me.tabPageGeneral.UseVisualStyleBackColor = True
    '
    'lblDrives
    '
    Me.lblDrives.AutoSize = True
    Me.lblDrives.Location = New System.Drawing.Point(3, 13)
    Me.lblDrives.Name = "lblDrives"
    Me.lblDrives.Size = New System.Drawing.Size(94, 13)
    Me.lblDrives.TabIndex = 1
    Me.lblDrives.Text = "Drives to display:"
    '
    'lvDrives
    '
    Me.lvDrives.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.lvDrives.CheckBoxes = True
    Me.lvDrives.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.colhdrDrive})
    Me.lvDrives.FullRowSelect = True
    Me.lvDrives.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable
    Me.lvDrives.Location = New System.Drawing.Point(6, 29)
    Me.lvDrives.MultiSelect = False
    Me.lvDrives.Name = "lvDrives"
    Me.lvDrives.ShowGroups = False
    Me.lvDrives.Size = New System.Drawing.Size(480, 342)
    Me.lvDrives.TabIndex = 0
    Me.lvDrives.UseCompatibleStateImageBehavior = False
    Me.lvDrives.View = System.Windows.Forms.View.Details
    '
    'colhdrDrive
    '
    Me.colhdrDrive.Text = "Drive"
    '
    'tabPageDisplay
    '
    Me.tabPageDisplay.Controls.Add(Me.comTooltipDisplayUnit)
    Me.tabPageDisplay.Controls.Add(Me.lblTooltipDisplayUnit)
    Me.tabPageDisplay.Controls.Add(Me.comDisplayUnit)
    Me.tabPageDisplay.Controls.Add(Me.lblToolbarTextDisplayUnit)
    Me.tabPageDisplay.Controls.Add(Me.txtTextFormatHelp)
    Me.tabPageDisplay.Controls.Add(Me.txtTextFormatString)
    Me.tabPageDisplay.Controls.Add(Me.lblTextFormatString)
    Me.tabPageDisplay.Controls.Add(Me.chkShowText)
    Me.tabPageDisplay.Controls.Add(Me.chkShowIcon)
    Me.tabPageDisplay.Location = New System.Drawing.Point(4, 22)
    Me.tabPageDisplay.Margin = New System.Windows.Forms.Padding(0)
    Me.tabPageDisplay.Name = "tabPageDisplay"
    Me.tabPageDisplay.Size = New System.Drawing.Size(492, 374)
    Me.tabPageDisplay.TabIndex = 1
    Me.tabPageDisplay.Text = "Display"
    Me.tabPageDisplay.UseVisualStyleBackColor = True
    '
    'Settings
    '
    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit
    Me.Controls.Add(Me.Tabs)
    Me.DoubleBuffered = True
    Me.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.Margin = New System.Windows.Forms.Padding(0)
    Me.Name = "Settings"
    Me.Size = New System.Drawing.Size(500, 400)
    Me.Tabs.ResumeLayout(False)
    Me.tabPageGeneral.ResumeLayout(False)
    Me.tabPageGeneral.PerformLayout()
    Me.tabPageDisplay.ResumeLayout(False)
    Me.tabPageDisplay.PerformLayout()
    Me.ResumeLayout(False)

  End Sub
  Friend WithEvents chkShowIcon As System.Windows.Forms.CheckBox
  Friend WithEvents lblToolbarTextDisplayUnit As System.Windows.Forms.Label
  Friend WithEvents comDisplayUnit As System.Windows.Forms.ComboBox
  Friend WithEvents lblTooltipDisplayUnit As System.Windows.Forms.Label
  Friend WithEvents comTooltipDisplayUnit As System.Windows.Forms.ComboBox
  Friend WithEvents lblTextFormatString As System.Windows.Forms.Label
  Friend WithEvents txtTextFormatString As System.Windows.Forms.TextBox
  Friend WithEvents chkShowText As System.Windows.Forms.CheckBox
  Friend WithEvents txtTextFormatHelp As System.Windows.Forms.TextBox
  Friend WithEvents Tabs As System.Windows.Forms.TabControl
  Friend WithEvents tabPageGeneral As System.Windows.Forms.TabPage
  Friend WithEvents tabPageDisplay As System.Windows.Forms.TabPage
  Friend WithEvents lblDrives As System.Windows.Forms.Label
  Friend WithEvents lvDrives As System.Windows.Forms.ListView
  Friend WithEvents colhdrDrive As System.Windows.Forms.ColumnHeader
End Class
