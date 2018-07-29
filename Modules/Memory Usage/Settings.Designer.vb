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
    Me.chkShowGraph = New System.Windows.Forms.CheckBox
    Me.picGraphLineColor = New System.Windows.Forms.PictureBox
    Me.lblGraphLineColor = New System.Windows.Forms.Label
    Me.lblGraphDisplay = New System.Windows.Forms.Label
    Me.radGraphDisplayFree = New System.Windows.Forms.RadioButton
    Me.radGraphDisplayUsed = New System.Windows.Forms.RadioButton
    Me.lblGraphUpdateTime = New System.Windows.Forms.Label
    Me.nudGraphUpdateTime = New System.Windows.Forms.NumericUpDown
    Me.comGraphUpdateTime = New System.Windows.Forms.ComboBox
    Me.chkTooltipTotal = New System.Windows.Forms.CheckBox
    Me.chkTooltipPctUsed = New System.Windows.Forms.CheckBox
    Me.chkTooltipUsed = New System.Windows.Forms.CheckBox
    Me.chkTooltipPctFree = New System.Windows.Forms.CheckBox
    Me.chkTooltipFree = New System.Windows.Forms.CheckBox
    Me.chkShowTopProcesses = New System.Windows.Forms.CheckBox
    Me.nudTopProcesses = New System.Windows.Forms.NumericUpDown
    Me.lblProcesses = New System.Windows.Forms.Label
    Me.lblTooltipDisplayUnit = New System.Windows.Forms.Label
    Me.radTooltipDisplayUnitKB = New System.Windows.Forms.RadioButton
    Me.radTooltipDisplayUnitMB = New System.Windows.Forms.RadioButton
    Me.radTooltipDisplayUnitGB = New System.Windows.Forms.RadioButton
    Me.chkTextTotal = New System.Windows.Forms.CheckBox
    Me.chkTextPctUsed = New System.Windows.Forms.CheckBox
    Me.chkTextUsed = New System.Windows.Forms.CheckBox
    Me.chkTextPctFree = New System.Windows.Forms.CheckBox
    Me.chkTextFree = New System.Windows.Forms.CheckBox
    Me.lblToolbarTextDisplayUnit = New System.Windows.Forms.Label
    Me.radTextDisplayUnitKB = New System.Windows.Forms.RadioButton
    Me.radTextDisplayUnitMB = New System.Windows.Forms.RadioButton
    Me.radTextDisplayUnitGB = New System.Windows.Forms.RadioButton
    Me.tabSettings = New System.Windows.Forms.TabControl
    Me.tabPageToolbar = New System.Windows.Forms.TabPage
    Me.tabpageTooltip = New System.Windows.Forms.TabPage
    Me.tabpageGraph = New System.Windows.Forms.TabPage
    Me.tlpGraph = New System.Windows.Forms.TableLayoutPanel
    CType(Me.picGraphLineColor, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.nudGraphUpdateTime, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.nudTopProcesses, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.tabSettings.SuspendLayout()
    Me.tabPageToolbar.SuspendLayout()
    Me.tabpageTooltip.SuspendLayout()
    Me.tabpageGraph.SuspendLayout()
    Me.SuspendLayout()
    '
    'chkShowIcon
    '
    Me.chkShowIcon.AutoSize = True
    Me.chkShowIcon.Location = New System.Drawing.Point(8, 8)
    Me.chkShowIcon.Margin = New System.Windows.Forms.Padding(0)
    Me.chkShowIcon.Name = "chkShowIcon"
    Me.chkShowIcon.Size = New System.Drawing.Size(80, 17)
    Me.chkShowIcon.TabIndex = 9
    Me.chkShowIcon.Text = "Show Icon"
    Me.chkShowIcon.UseVisualStyleBackColor = True
    '
    'chkShowText
    '
    Me.chkShowText.AutoSize = True
    Me.chkShowText.Location = New System.Drawing.Point(8, 34)
    Me.chkShowText.Margin = New System.Windows.Forms.Padding(0)
    Me.chkShowText.Name = "chkShowText"
    Me.chkShowText.Size = New System.Drawing.Size(120, 17)
    Me.chkShowText.TabIndex = 7
    Me.chkShowText.Text = "Show Toolbar Text"
    Me.chkShowText.UseVisualStyleBackColor = True
    '
    'chkShowGraph
    '
    Me.chkShowGraph.AutoSize = True
    Me.chkShowGraph.Location = New System.Drawing.Point(8, 8)
    Me.chkShowGraph.Margin = New System.Windows.Forms.Padding(0)
    Me.chkShowGraph.Name = "chkShowGraph"
    Me.chkShowGraph.Size = New System.Drawing.Size(90, 17)
    Me.chkShowGraph.TabIndex = 8
    Me.chkShowGraph.Text = "Show Graph"
    Me.chkShowGraph.UseVisualStyleBackColor = True
    '
    'picGraphLineColor
    '
    Me.picGraphLineColor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
    Me.picGraphLineColor.Location = New System.Drawing.Point(8, 63)
    Me.picGraphLineColor.Margin = New System.Windows.Forms.Padding(0, 0, 4, 0)
    Me.picGraphLineColor.Name = "picGraphLineColor"
    Me.picGraphLineColor.Size = New System.Drawing.Size(13, 13)
    Me.picGraphLineColor.TabIndex = 12
    Me.picGraphLineColor.TabStop = False
    '
    'lblGraphLineColor
    '
    Me.lblGraphLineColor.AutoSize = True
    Me.lblGraphLineColor.Location = New System.Drawing.Point(25, 63)
    Me.lblGraphLineColor.Margin = New System.Windows.Forms.Padding(0)
    Me.lblGraphLineColor.Name = "lblGraphLineColor"
    Me.lblGraphLineColor.Size = New System.Drawing.Size(59, 13)
    Me.lblGraphLineColor.TabIndex = 13
    Me.lblGraphLineColor.Text = "Line Color"
    '
    'lblGraphDisplay
    '
    Me.lblGraphDisplay.AutoSize = True
    Me.lblGraphDisplay.Location = New System.Drawing.Point(8, 37)
    Me.lblGraphDisplay.Margin = New System.Windows.Forms.Padding(0, 2, 4, 0)
    Me.lblGraphDisplay.Name = "lblGraphDisplay"
    Me.lblGraphDisplay.Size = New System.Drawing.Size(47, 13)
    Me.lblGraphDisplay.TabIndex = 3
    Me.lblGraphDisplay.Text = "Display:"
    '
    'radGraphDisplayFree
    '
    Me.radGraphDisplayFree.AutoSize = True
    Me.radGraphDisplayFree.Location = New System.Drawing.Point(59, 35)
    Me.radGraphDisplayFree.Margin = New System.Windows.Forms.Padding(0, 0, 4, 0)
    Me.radGraphDisplayFree.Name = "radGraphDisplayFree"
    Me.radGraphDisplayFree.Size = New System.Drawing.Size(91, 17)
    Me.radGraphDisplayFree.TabIndex = 4
    Me.radGraphDisplayFree.TabStop = True
    Me.radGraphDisplayFree.Text = "Free Memory"
    Me.radGraphDisplayFree.UseVisualStyleBackColor = True
    '
    'radGraphDisplayUsed
    '
    Me.radGraphDisplayUsed.AutoSize = True
    Me.radGraphDisplayUsed.Location = New System.Drawing.Point(154, 35)
    Me.radGraphDisplayUsed.Margin = New System.Windows.Forms.Padding(0, 0, 4, 0)
    Me.radGraphDisplayUsed.Name = "radGraphDisplayUsed"
    Me.radGraphDisplayUsed.Size = New System.Drawing.Size(95, 17)
    Me.radGraphDisplayUsed.TabIndex = 5
    Me.radGraphDisplayUsed.TabStop = True
    Me.radGraphDisplayUsed.Text = "Used Memory"
    Me.radGraphDisplayUsed.UseVisualStyleBackColor = True
    '
    'lblGraphUpdateTime
    '
    Me.lblGraphUpdateTime.AutoSize = True
    Me.lblGraphUpdateTime.Location = New System.Drawing.Point(8, 91)
    Me.lblGraphUpdateTime.Margin = New System.Windows.Forms.Padding(0, 2, 3, 0)
    Me.lblGraphUpdateTime.Name = "lblGraphUpdateTime"
    Me.lblGraphUpdateTime.Size = New System.Drawing.Size(108, 13)
    Me.lblGraphUpdateTime.TabIndex = 15
    Me.lblGraphUpdateTime.Text = "Update graph every"
    '
    'nudGraphUpdateTime
    '
    Me.nudGraphUpdateTime.Location = New System.Drawing.Point(119, 89)
    Me.nudGraphUpdateTime.Margin = New System.Windows.Forms.Padding(0)
    Me.nudGraphUpdateTime.Maximum = New Decimal(New Integer() {60, 0, 0, 0})
    Me.nudGraphUpdateTime.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
    Me.nudGraphUpdateTime.Name = "nudGraphUpdateTime"
    Me.nudGraphUpdateTime.Size = New System.Drawing.Size(46, 22)
    Me.nudGraphUpdateTime.TabIndex = 16
    Me.nudGraphUpdateTime.Value = New Decimal(New Integer() {1, 0, 0, 0})
    '
    'comGraphUpdateTime
    '
    Me.comGraphUpdateTime.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
    Me.comGraphUpdateTime.FormattingEnabled = True
    Me.comGraphUpdateTime.Items.AddRange(New Object() {"seconds", "minutes", "hours"})
    Me.comGraphUpdateTime.Location = New System.Drawing.Point(169, 89)
    Me.comGraphUpdateTime.Margin = New System.Windows.Forms.Padding(4, 0, 0, 0)
    Me.comGraphUpdateTime.Name = "comGraphUpdateTime"
    Me.comGraphUpdateTime.Size = New System.Drawing.Size(121, 21)
    Me.comGraphUpdateTime.TabIndex = 17
    '
    'chkTooltipTotal
    '
    Me.chkTooltipTotal.AutoSize = True
    Me.chkTooltipTotal.Location = New System.Drawing.Point(8, 50)
    Me.chkTooltipTotal.Margin = New System.Windows.Forms.Padding(0, 0, 8, 0)
    Me.chkTooltipTotal.Name = "chkTooltipTotal"
    Me.chkTooltipTotal.Size = New System.Drawing.Size(127, 17)
    Me.chkTooltipTotal.TabIndex = 4
    Me.chkTooltipTotal.Text = "Show Total Memory"
    Me.chkTooltipTotal.UseVisualStyleBackColor = True
    '
    'chkTooltipPctUsed
    '
    Me.chkTooltipPctUsed.AutoSize = True
    Me.chkTooltipPctUsed.Location = New System.Drawing.Point(144, 29)
    Me.chkTooltipPctUsed.Margin = New System.Windows.Forms.Padding(0, 0, 0, 4)
    Me.chkTooltipPctUsed.Name = "chkTooltipPctUsed"
    Me.chkTooltipPctUsed.Size = New System.Drawing.Size(169, 17)
    Me.chkTooltipPctUsed.TabIndex = 3
    Me.chkTooltipPctUsed.Text = "Show Percent Used Memory"
    Me.chkTooltipPctUsed.UseVisualStyleBackColor = True
    '
    'chkTooltipUsed
    '
    Me.chkTooltipUsed.AutoSize = True
    Me.chkTooltipUsed.Location = New System.Drawing.Point(8, 29)
    Me.chkTooltipUsed.Margin = New System.Windows.Forms.Padding(0, 0, 8, 0)
    Me.chkTooltipUsed.Name = "chkTooltipUsed"
    Me.chkTooltipUsed.Size = New System.Drawing.Size(128, 17)
    Me.chkTooltipUsed.TabIndex = 2
    Me.chkTooltipUsed.Text = "Show Used Memory"
    Me.chkTooltipUsed.UseVisualStyleBackColor = True
    '
    'chkTooltipPctFree
    '
    Me.chkTooltipPctFree.AutoSize = True
    Me.chkTooltipPctFree.Location = New System.Drawing.Point(144, 8)
    Me.chkTooltipPctFree.Margin = New System.Windows.Forms.Padding(0, 0, 0, 4)
    Me.chkTooltipPctFree.Name = "chkTooltipPctFree"
    Me.chkTooltipPctFree.Size = New System.Drawing.Size(165, 17)
    Me.chkTooltipPctFree.TabIndex = 1
    Me.chkTooltipPctFree.Text = "Show Percent Free Memory"
    Me.chkTooltipPctFree.UseVisualStyleBackColor = True
    '
    'chkTooltipFree
    '
    Me.chkTooltipFree.AutoSize = True
    Me.chkTooltipFree.Location = New System.Drawing.Point(8, 8)
    Me.chkTooltipFree.Margin = New System.Windows.Forms.Padding(0, 0, 8, 0)
    Me.chkTooltipFree.Name = "chkTooltipFree"
    Me.chkTooltipFree.Size = New System.Drawing.Size(124, 17)
    Me.chkTooltipFree.TabIndex = 0
    Me.chkTooltipFree.Text = "Show Free Memory"
    Me.chkTooltipFree.UseVisualStyleBackColor = True
    '
    'chkShowTopProcesses
    '
    Me.chkShowTopProcesses.AutoSize = True
    Me.chkShowTopProcesses.Location = New System.Drawing.Point(8, 79)
    Me.chkShowTopProcesses.Margin = New System.Windows.Forms.Padding(0, 0, 4, 0)
    Me.chkShowTopProcesses.Name = "chkShowTopProcesses"
    Me.chkShowTopProcesses.Size = New System.Drawing.Size(96, 17)
    Me.chkShowTopProcesses.TabIndex = 1
    Me.chkShowTopProcesses.Text = "Show the top"
    Me.chkShowTopProcesses.UseVisualStyleBackColor = True
    '
    'nudTopProcesses
    '
    Me.nudTopProcesses.AutoSize = True
    Me.nudTopProcesses.Location = New System.Drawing.Point(108, 77)
    Me.nudTopProcesses.Margin = New System.Windows.Forms.Padding(0, 0, 4, 0)
    Me.nudTopProcesses.Minimum = New Decimal(New Integer() {2, 0, 0, 0})
    Me.nudTopProcesses.Name = "nudTopProcesses"
    Me.nudTopProcesses.Size = New System.Drawing.Size(41, 22)
    Me.nudTopProcesses.TabIndex = 2
    Me.nudTopProcesses.Value = New Decimal(New Integer() {10, 0, 0, 0})
    '
    'lblProcesses
    '
    Me.lblProcesses.AutoSize = True
    Me.lblProcesses.Location = New System.Drawing.Point(153, 80)
    Me.lblProcesses.Margin = New System.Windows.Forms.Padding(0, 0, 4, 2)
    Me.lblProcesses.Name = "lblProcesses"
    Me.lblProcesses.Size = New System.Drawing.Size(60, 13)
    Me.lblProcesses.TabIndex = 3
    Me.lblProcesses.Text = "processes."
    Me.lblProcesses.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
    '
    'lblTooltipDisplayUnit
    '
    Me.lblTooltipDisplayUnit.AutoSize = True
    Me.lblTooltipDisplayUnit.Location = New System.Drawing.Point(8, 114)
    Me.lblTooltipDisplayUnit.Margin = New System.Windows.Forms.Padding(0, 2, 3, 0)
    Me.lblTooltipDisplayUnit.Name = "lblTooltipDisplayUnit"
    Me.lblTooltipDisplayUnit.Size = New System.Drawing.Size(72, 13)
    Me.lblTooltipDisplayUnit.TabIndex = 0
    Me.lblTooltipDisplayUnit.Text = "Display Unit:"
    '
    'radTooltipDisplayUnitKB
    '
    Me.radTooltipDisplayUnitKB.AutoSize = True
    Me.radTooltipDisplayUnitKB.Location = New System.Drawing.Point(83, 112)
    Me.radTooltipDisplayUnitKB.Margin = New System.Windows.Forms.Padding(0, 0, 4, 0)
    Me.radTooltipDisplayUnitKB.Name = "radTooltipDisplayUnitKB"
    Me.radTooltipDisplayUnitKB.Size = New System.Drawing.Size(38, 17)
    Me.radTooltipDisplayUnitKB.TabIndex = 1
    Me.radTooltipDisplayUnitKB.TabStop = True
    Me.radTooltipDisplayUnitKB.Text = "KB"
    Me.radTooltipDisplayUnitKB.UseVisualStyleBackColor = True
    '
    'radTooltipDisplayUnitMB
    '
    Me.radTooltipDisplayUnitMB.AutoSize = True
    Me.radTooltipDisplayUnitMB.Location = New System.Drawing.Point(125, 112)
    Me.radTooltipDisplayUnitMB.Margin = New System.Windows.Forms.Padding(0, 0, 4, 0)
    Me.radTooltipDisplayUnitMB.Name = "radTooltipDisplayUnitMB"
    Me.radTooltipDisplayUnitMB.Size = New System.Drawing.Size(42, 17)
    Me.radTooltipDisplayUnitMB.TabIndex = 2
    Me.radTooltipDisplayUnitMB.TabStop = True
    Me.radTooltipDisplayUnitMB.Text = "MB"
    Me.radTooltipDisplayUnitMB.UseVisualStyleBackColor = True
    '
    'radTooltipDisplayUnitGB
    '
    Me.radTooltipDisplayUnitGB.AutoSize = True
    Me.radTooltipDisplayUnitGB.Location = New System.Drawing.Point(171, 112)
    Me.radTooltipDisplayUnitGB.Margin = New System.Windows.Forms.Padding(0, 0, 4, 0)
    Me.radTooltipDisplayUnitGB.Name = "radTooltipDisplayUnitGB"
    Me.radTooltipDisplayUnitGB.Size = New System.Drawing.Size(40, 17)
    Me.radTooltipDisplayUnitGB.TabIndex = 3
    Me.radTooltipDisplayUnitGB.TabStop = True
    Me.radTooltipDisplayUnitGB.Text = "GB"
    Me.radTooltipDisplayUnitGB.UseVisualStyleBackColor = True
    '
    'chkTextTotal
    '
    Me.chkTextTotal.AutoSize = True
    Me.chkTextTotal.Location = New System.Drawing.Point(8, 95)
    Me.chkTextTotal.Margin = New System.Windows.Forms.Padding(0, 0, 8, 0)
    Me.chkTextTotal.Name = "chkTextTotal"
    Me.chkTextTotal.Size = New System.Drawing.Size(127, 17)
    Me.chkTextTotal.TabIndex = 4
    Me.chkTextTotal.Text = "Show Total Memory"
    Me.chkTextTotal.UseVisualStyleBackColor = True
    '
    'chkTextPctUsed
    '
    Me.chkTextPctUsed.AutoSize = True
    Me.chkTextPctUsed.Location = New System.Drawing.Point(140, 78)
    Me.chkTextPctUsed.Margin = New System.Windows.Forms.Padding(0, 0, 0, 4)
    Me.chkTextPctUsed.Name = "chkTextPctUsed"
    Me.chkTextPctUsed.Size = New System.Drawing.Size(169, 17)
    Me.chkTextPctUsed.TabIndex = 3
    Me.chkTextPctUsed.Text = "Show Percent Used Memory"
    Me.chkTextPctUsed.UseVisualStyleBackColor = True
    '
    'chkTextUsed
    '
    Me.chkTextUsed.AutoSize = True
    Me.chkTextUsed.Location = New System.Drawing.Point(8, 78)
    Me.chkTextUsed.Margin = New System.Windows.Forms.Padding(0, 0, 8, 0)
    Me.chkTextUsed.Name = "chkTextUsed"
    Me.chkTextUsed.Size = New System.Drawing.Size(128, 17)
    Me.chkTextUsed.TabIndex = 2
    Me.chkTextUsed.Text = "Show Used Memory"
    Me.chkTextUsed.UseVisualStyleBackColor = True
    '
    'chkTextPctFree
    '
    Me.chkTextPctFree.AutoSize = True
    Me.chkTextPctFree.Location = New System.Drawing.Point(140, 61)
    Me.chkTextPctFree.Margin = New System.Windows.Forms.Padding(0, 0, 0, 4)
    Me.chkTextPctFree.Name = "chkTextPctFree"
    Me.chkTextPctFree.Size = New System.Drawing.Size(165, 17)
    Me.chkTextPctFree.TabIndex = 1
    Me.chkTextPctFree.Text = "Show Percent Free Memory"
    Me.chkTextPctFree.UseVisualStyleBackColor = True
    '
    'chkTextFree
    '
    Me.chkTextFree.AutoSize = True
    Me.chkTextFree.Location = New System.Drawing.Point(8, 61)
    Me.chkTextFree.Margin = New System.Windows.Forms.Padding(0, 0, 8, 0)
    Me.chkTextFree.Name = "chkTextFree"
    Me.chkTextFree.Size = New System.Drawing.Size(124, 17)
    Me.chkTextFree.TabIndex = 0
    Me.chkTextFree.Text = "Show Free Memory"
    Me.chkTextFree.UseVisualStyleBackColor = True
    '
    'lblToolbarTextDisplayUnit
    '
    Me.lblToolbarTextDisplayUnit.AutoSize = True
    Me.lblToolbarTextDisplayUnit.Location = New System.Drawing.Point(8, 123)
    Me.lblToolbarTextDisplayUnit.Margin = New System.Windows.Forms.Padding(0, 2, 3, 0)
    Me.lblToolbarTextDisplayUnit.Name = "lblToolbarTextDisplayUnit"
    Me.lblToolbarTextDisplayUnit.Size = New System.Drawing.Size(72, 13)
    Me.lblToolbarTextDisplayUnit.TabIndex = 0
    Me.lblToolbarTextDisplayUnit.Text = "Display Unit:"
    '
    'radTextDisplayUnitKB
    '
    Me.radTextDisplayUnitKB.AutoSize = True
    Me.radTextDisplayUnitKB.Location = New System.Drawing.Point(83, 121)
    Me.radTextDisplayUnitKB.Margin = New System.Windows.Forms.Padding(0, 0, 8, 0)
    Me.radTextDisplayUnitKB.Name = "radTextDisplayUnitKB"
    Me.radTextDisplayUnitKB.Size = New System.Drawing.Size(38, 17)
    Me.radTextDisplayUnitKB.TabIndex = 1
    Me.radTextDisplayUnitKB.TabStop = True
    Me.radTextDisplayUnitKB.Text = "KB"
    Me.radTextDisplayUnitKB.UseVisualStyleBackColor = True
    '
    'radTextDisplayUnitMB
    '
    Me.radTextDisplayUnitMB.AutoSize = True
    Me.radTextDisplayUnitMB.Location = New System.Drawing.Point(129, 121)
    Me.radTextDisplayUnitMB.Margin = New System.Windows.Forms.Padding(0, 0, 8, 0)
    Me.radTextDisplayUnitMB.Name = "radTextDisplayUnitMB"
    Me.radTextDisplayUnitMB.Size = New System.Drawing.Size(42, 17)
    Me.radTextDisplayUnitMB.TabIndex = 2
    Me.radTextDisplayUnitMB.TabStop = True
    Me.radTextDisplayUnitMB.Text = "MB"
    Me.radTextDisplayUnitMB.UseVisualStyleBackColor = True
    '
    'radTextDisplayUnitGB
    '
    Me.radTextDisplayUnitGB.AutoSize = True
    Me.radTextDisplayUnitGB.Location = New System.Drawing.Point(179, 121)
    Me.radTextDisplayUnitGB.Margin = New System.Windows.Forms.Padding(0, 0, 8, 0)
    Me.radTextDisplayUnitGB.Name = "radTextDisplayUnitGB"
    Me.radTextDisplayUnitGB.Size = New System.Drawing.Size(40, 17)
    Me.radTextDisplayUnitGB.TabIndex = 3
    Me.radTextDisplayUnitGB.TabStop = True
    Me.radTextDisplayUnitGB.Text = "GB"
    Me.radTextDisplayUnitGB.UseVisualStyleBackColor = True
    '
    'tabSettings
    '
    Me.tabSettings.Controls.Add(Me.tabPageToolbar)
    Me.tabSettings.Controls.Add(Me.tabpageTooltip)
    Me.tabSettings.Controls.Add(Me.tabpageGraph)
    Me.tabSettings.Dock = System.Windows.Forms.DockStyle.Fill
    Me.tabSettings.Location = New System.Drawing.Point(0, 0)
    Me.tabSettings.Margin = New System.Windows.Forms.Padding(0)
    Me.tabSettings.Name = "tabSettings"
    Me.tabSettings.SelectedIndex = 0
    Me.tabSettings.Size = New System.Drawing.Size(500, 400)
    Me.tabSettings.TabIndex = 23
    '
    'tabPageToolbar
    '
    Me.tabPageToolbar.Controls.Add(Me.lblToolbarTextDisplayUnit)
    Me.tabPageToolbar.Controls.Add(Me.radTextDisplayUnitKB)
    Me.tabPageToolbar.Controls.Add(Me.chkTextTotal)
    Me.tabPageToolbar.Controls.Add(Me.radTextDisplayUnitMB)
    Me.tabPageToolbar.Controls.Add(Me.radTextDisplayUnitGB)
    Me.tabPageToolbar.Controls.Add(Me.chkTextPctUsed)
    Me.tabPageToolbar.Controls.Add(Me.chkShowIcon)
    Me.tabPageToolbar.Controls.Add(Me.chkTextUsed)
    Me.tabPageToolbar.Controls.Add(Me.chkShowText)
    Me.tabPageToolbar.Controls.Add(Me.chkTextPctFree)
    Me.tabPageToolbar.Controls.Add(Me.chkTextFree)
    Me.tabPageToolbar.Location = New System.Drawing.Point(4, 22)
    Me.tabPageToolbar.Margin = New System.Windows.Forms.Padding(0)
    Me.tabPageToolbar.Name = "tabPageToolbar"
    Me.tabPageToolbar.Padding = New System.Windows.Forms.Padding(8)
    Me.tabPageToolbar.Size = New System.Drawing.Size(492, 374)
    Me.tabPageToolbar.TabIndex = 0
    Me.tabPageToolbar.Text = "Toolbar"
    Me.tabPageToolbar.UseVisualStyleBackColor = True
    '
    'tabpageTooltip
    '
    Me.tabpageTooltip.Controls.Add(Me.nudTopProcesses)
    Me.tabpageTooltip.Controls.Add(Me.lblTooltipDisplayUnit)
    Me.tabpageTooltip.Controls.Add(Me.radTooltipDisplayUnitKB)
    Me.tabpageTooltip.Controls.Add(Me.chkShowTopProcesses)
    Me.tabpageTooltip.Controls.Add(Me.radTooltipDisplayUnitMB)
    Me.tabpageTooltip.Controls.Add(Me.radTooltipDisplayUnitGB)
    Me.tabpageTooltip.Controls.Add(Me.chkTooltipTotal)
    Me.tabpageTooltip.Controls.Add(Me.lblProcesses)
    Me.tabpageTooltip.Controls.Add(Me.chkTooltipPctUsed)
    Me.tabpageTooltip.Controls.Add(Me.chkTooltipFree)
    Me.tabpageTooltip.Controls.Add(Me.chkTooltipUsed)
    Me.tabpageTooltip.Controls.Add(Me.chkTooltipPctFree)
    Me.tabpageTooltip.Location = New System.Drawing.Point(4, 22)
    Me.tabpageTooltip.Margin = New System.Windows.Forms.Padding(0)
    Me.tabpageTooltip.Name = "tabpageTooltip"
    Me.tabpageTooltip.Padding = New System.Windows.Forms.Padding(8)
    Me.tabpageTooltip.Size = New System.Drawing.Size(492, 374)
    Me.tabpageTooltip.TabIndex = 1
    Me.tabpageTooltip.Text = "Tooltip"
    Me.tabpageTooltip.UseVisualStyleBackColor = True
    '
    'tabpageGraph
    '
    Me.tabpageGraph.Controls.Add(Me.lblGraphUpdateTime)
    Me.tabpageGraph.Controls.Add(Me.nudGraphUpdateTime)
    Me.tabpageGraph.Controls.Add(Me.picGraphLineColor)
    Me.tabpageGraph.Controls.Add(Me.comGraphUpdateTime)
    Me.tabpageGraph.Controls.Add(Me.lblGraphDisplay)
    Me.tabpageGraph.Controls.Add(Me.lblGraphLineColor)
    Me.tabpageGraph.Controls.Add(Me.radGraphDisplayFree)
    Me.tabpageGraph.Controls.Add(Me.tlpGraph)
    Me.tabpageGraph.Controls.Add(Me.radGraphDisplayUsed)
    Me.tabpageGraph.Controls.Add(Me.chkShowGraph)
    Me.tabpageGraph.Location = New System.Drawing.Point(4, 22)
    Me.tabpageGraph.Margin = New System.Windows.Forms.Padding(0)
    Me.tabpageGraph.Name = "tabpageGraph"
    Me.tabpageGraph.Padding = New System.Windows.Forms.Padding(8)
    Me.tabpageGraph.Size = New System.Drawing.Size(492, 374)
    Me.tabpageGraph.TabIndex = 2
    Me.tabpageGraph.Text = "Graph"
    Me.tabpageGraph.UseVisualStyleBackColor = True
    '
    'tlpGraph
    '
    Me.tlpGraph.AutoSize = True
    Me.tlpGraph.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
    Me.tlpGraph.ColumnCount = 1
    Me.tlpGraph.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
    Me.tlpGraph.Dock = System.Windows.Forms.DockStyle.Top
    Me.tlpGraph.Location = New System.Drawing.Point(8, 8)
    Me.tlpGraph.Margin = New System.Windows.Forms.Padding(0)
    Me.tlpGraph.Name = "tlpGraph"
    Me.tlpGraph.RowCount = 7
    Me.tlpGraph.RowStyles.Add(New System.Windows.Forms.RowStyle)
    Me.tlpGraph.RowStyles.Add(New System.Windows.Forms.RowStyle)
    Me.tlpGraph.RowStyles.Add(New System.Windows.Forms.RowStyle)
    Me.tlpGraph.RowStyles.Add(New System.Windows.Forms.RowStyle)
    Me.tlpGraph.RowStyles.Add(New System.Windows.Forms.RowStyle)
    Me.tlpGraph.RowStyles.Add(New System.Windows.Forms.RowStyle)
    Me.tlpGraph.RowStyles.Add(New System.Windows.Forms.RowStyle)
    Me.tlpGraph.Size = New System.Drawing.Size(476, 0)
    Me.tlpGraph.TabIndex = 23
    '
    'Settings
    '
    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit
    Me.Controls.Add(Me.tabSettings)
    Me.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.Margin = New System.Windows.Forms.Padding(0)
    Me.Name = "Settings"
    Me.Size = New System.Drawing.Size(500, 400)
    CType(Me.picGraphLineColor, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.nudGraphUpdateTime, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.nudTopProcesses, System.ComponentModel.ISupportInitialize).EndInit()
    Me.tabSettings.ResumeLayout(False)
    Me.tabPageToolbar.ResumeLayout(False)
    Me.tabPageToolbar.PerformLayout()
    Me.tabpageTooltip.ResumeLayout(False)
    Me.tabpageTooltip.PerformLayout()
    Me.tabpageGraph.ResumeLayout(False)
    Me.tabpageGraph.PerformLayout()
    Me.ResumeLayout(False)

  End Sub
    Friend WithEvents chkShowIcon As System.Windows.Forms.CheckBox
    Friend WithEvents chkShowText As System.Windows.Forms.CheckBox
    Friend WithEvents chkShowGraph As System.Windows.Forms.CheckBox
  Friend WithEvents chkShowTopProcesses As System.Windows.Forms.CheckBox
    Friend WithEvents nudTopProcesses As System.Windows.Forms.NumericUpDown
    Friend WithEvents lblProcesses As System.Windows.Forms.Label
  Friend WithEvents chkTextFree As System.Windows.Forms.CheckBox
    Friend WithEvents chkTextPctFree As System.Windows.Forms.CheckBox
    Friend WithEvents chkTextUsed As System.Windows.Forms.CheckBox
    Friend WithEvents chkTextPctUsed As System.Windows.Forms.CheckBox
    Friend WithEvents chkTextTotal As System.Windows.Forms.CheckBox
  Friend WithEvents lblToolbarTextDisplayUnit As System.Windows.Forms.Label
    Friend WithEvents radTextDisplayUnitKB As System.Windows.Forms.RadioButton
    Friend WithEvents radTextDisplayUnitMB As System.Windows.Forms.RadioButton
    Friend WithEvents radTextDisplayUnitGB As System.Windows.Forms.RadioButton
  Friend WithEvents chkTooltipTotal As System.Windows.Forms.CheckBox
    Friend WithEvents chkTooltipPctUsed As System.Windows.Forms.CheckBox
    Friend WithEvents chkTooltipUsed As System.Windows.Forms.CheckBox
    Friend WithEvents chkTooltipPctFree As System.Windows.Forms.CheckBox
    Friend WithEvents chkTooltipFree As System.Windows.Forms.CheckBox
  Friend WithEvents lblTooltipDisplayUnit As System.Windows.Forms.Label
    Friend WithEvents radTooltipDisplayUnitKB As System.Windows.Forms.RadioButton
    Friend WithEvents radTooltipDisplayUnitMB As System.Windows.Forms.RadioButton
    Friend WithEvents radTooltipDisplayUnitGB As System.Windows.Forms.RadioButton
    Friend WithEvents picGraphLineColor As System.Windows.Forms.PictureBox
    Friend WithEvents lblGraphLineColor As System.Windows.Forms.Label
  Friend WithEvents lblGraphDisplay As System.Windows.Forms.Label
    Friend WithEvents radGraphDisplayFree As System.Windows.Forms.RadioButton
    Friend WithEvents radGraphDisplayUsed As System.Windows.Forms.RadioButton
  Friend WithEvents lblGraphUpdateTime As System.Windows.Forms.Label
    Friend WithEvents nudGraphUpdateTime As System.Windows.Forms.NumericUpDown
    Friend WithEvents comGraphUpdateTime As System.Windows.Forms.ComboBox
    Friend WithEvents tabSettings As System.Windows.Forms.TabControl
    Friend WithEvents tabPageToolbar As System.Windows.Forms.TabPage
  Friend WithEvents tabpageTooltip As System.Windows.Forms.TabPage
    Friend WithEvents tabpageGraph As System.Windows.Forms.TabPage
    Friend WithEvents tlpGraph As System.Windows.Forms.TableLayoutPanel
End Class
