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
    Me.lblGraphUpdateTime = New System.Windows.Forms.Label
    Me.nudGraphUpdateTime = New System.Windows.Forms.NumericUpDown
    Me.comGraphUpdateTime = New System.Windows.Forms.ComboBox
    Me.lblTooltipSpeedDisplayUnit = New System.Windows.Forms.Label
    Me.comTooltipSpeedDisplayUnit = New System.Windows.Forms.ComboBox
    Me.chkTooltipDownloadAvg = New System.Windows.Forms.CheckBox
    Me.chkTooltipUploadTotal = New System.Windows.Forms.CheckBox
    Me.chkTooltipDownloadTotal = New System.Windows.Forms.CheckBox
    Me.chkTooltipUploadSpeed = New System.Windows.Forms.CheckBox
    Me.chkTooltipDownloadSpeed = New System.Windows.Forms.CheckBox
    Me.chkTooltipUploadAvg = New System.Windows.Forms.CheckBox
    Me.lblTooltipTotalDisplayUnit = New System.Windows.Forms.Label
    Me.comTooltipTotalDisplayUnit = New System.Windows.Forms.ComboBox
    Me.chkTooltipDownloadMax = New System.Windows.Forms.CheckBox
    Me.chkTooltipUploadMax = New System.Windows.Forms.CheckBox
    Me.chkTextUploadTotal = New System.Windows.Forms.CheckBox
    Me.chkTextUploadSpeed = New System.Windows.Forms.CheckBox
    Me.chkTextDownloadTotal = New System.Windows.Forms.CheckBox
    Me.chkTextDownloadSpeed = New System.Windows.Forms.CheckBox
    Me.lblTextSpeedDisplayUnit = New System.Windows.Forms.Label
    Me.comTextSpeedDisplayUnit = New System.Windows.Forms.ComboBox
    Me.tabSettings = New System.Windows.Forms.TabControl
    Me.tabpageGeneral = New System.Windows.Forms.TabPage
    Me.grpStatistics = New System.Windows.Forms.GroupBox
    Me.chkSaveTransferTotals = New System.Windows.Forms.CheckBox
    Me.chkSaveAverageSpeeds = New System.Windows.Forms.CheckBox
    Me.chkResetTransferTotals = New System.Windows.Forms.CheckBox
    Me.chkResetAvgSpeeds = New System.Windows.Forms.CheckBox
    Me.chkResetMaxSpeeds = New System.Windows.Forms.CheckBox
    Me.chkSaveMaxSpeeds = New System.Windows.Forms.CheckBox
    Me.grpInterface = New System.Windows.Forms.GroupBox
    Me.comInterface = New System.Windows.Forms.ComboBox
    Me.lblInterface = New System.Windows.Forms.Label
    Me.tabPageToolbar = New System.Windows.Forms.TabPage
    Me.chkToolbarInternalIP = New System.Windows.Forms.CheckBox
    Me.chkToolbarExternalIP = New System.Windows.Forms.CheckBox
    Me.radTextVert = New System.Windows.Forms.RadioButton
    Me.lblTextTotalDisplayUnit = New System.Windows.Forms.Label
    Me.comTextTotalDisplayUnit = New System.Windows.Forms.ComboBox
    Me.lblTextOrientation = New System.Windows.Forms.Label
    Me.radTextHorz = New System.Windows.Forms.RadioButton
    Me.tabpageTooltip = New System.Windows.Forms.TabPage
    Me.tabpageGraph = New System.Windows.Forms.TabPage
    Me.lblStaticMaxUp = New System.Windows.Forms.Label
    Me.txtGraphStaticMaxUp = New System.Windows.Forms.TextBox
    Me.lblStaticMaxUpKbs = New System.Windows.Forms.Label
    Me.lblStaticMaxDown = New System.Windows.Forms.Label
    Me.chkStaticMaxValues = New System.Windows.Forms.CheckBox
    Me.txtGraphStaticMaxDown = New System.Windows.Forms.TextBox
    Me.lblStaticMaxDownKbs = New System.Windows.Forms.Label
    Me.picGraphDownloadColor = New System.Windows.Forms.PictureBox
    Me.lblGraphDownloadColor = New System.Windows.Forms.Label
    Me.lblGraphDisplay = New System.Windows.Forms.Label
    Me.picGraphUploadColor = New System.Windows.Forms.PictureBox
    Me.comGraphDisplay = New System.Windows.Forms.ComboBox
    Me.lblGraphUploadColor = New System.Windows.Forms.Label
    Me.chkTooltipInternalIP = New System.Windows.Forms.CheckBox
    Me.chkTooltipExternalIP = New System.Windows.Forms.CheckBox
    CType(Me.nudGraphUpdateTime, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.tabSettings.SuspendLayout()
    Me.tabpageGeneral.SuspendLayout()
    Me.grpStatistics.SuspendLayout()
    Me.grpInterface.SuspendLayout()
    Me.tabPageToolbar.SuspendLayout()
    Me.tabpageTooltip.SuspendLayout()
    Me.tabpageGraph.SuspendLayout()
    CType(Me.picGraphDownloadColor, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.picGraphUploadColor, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.SuspendLayout()
    '
    'chkShowIcon
    '
    Me.chkShowIcon.AutoSize = True
    Me.chkShowIcon.Location = New System.Drawing.Point(8, 8)
    Me.chkShowIcon.Margin = New System.Windows.Forms.Padding(0, 0, 8, 0)
    Me.chkShowIcon.Name = "chkShowIcon"
    Me.chkShowIcon.Size = New System.Drawing.Size(80, 17)
    Me.chkShowIcon.TabIndex = 9
    Me.chkShowIcon.Text = "Show Icon"
    Me.chkShowIcon.UseVisualStyleBackColor = True
    '
    'chkShowText
    '
    Me.chkShowText.AutoSize = True
    Me.chkShowText.Location = New System.Drawing.Point(96, 8)
    Me.chkShowText.Margin = New System.Windows.Forms.Padding(0, 0, 8, 0)
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
    'lblGraphUpdateTime
    '
    Me.lblGraphUpdateTime.AutoSize = True
    Me.lblGraphUpdateTime.Location = New System.Drawing.Point(8, 101)
    Me.lblGraphUpdateTime.Margin = New System.Windows.Forms.Padding(0, 0, 8, 0)
    Me.lblGraphUpdateTime.Name = "lblGraphUpdateTime"
    Me.lblGraphUpdateTime.Size = New System.Drawing.Size(108, 13)
    Me.lblGraphUpdateTime.TabIndex = 15
    Me.lblGraphUpdateTime.Text = "Update graph every"
    Me.lblGraphUpdateTime.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
    '
    'nudGraphUpdateTime
    '
    Me.nudGraphUpdateTime.Location = New System.Drawing.Point(124, 99)
    Me.nudGraphUpdateTime.Margin = New System.Windows.Forms.Padding(0, 0, 8, 0)
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
    Me.comGraphUpdateTime.Location = New System.Drawing.Point(178, 99)
    Me.comGraphUpdateTime.Margin = New System.Windows.Forms.Padding(0)
    Me.comGraphUpdateTime.Name = "comGraphUpdateTime"
    Me.comGraphUpdateTime.Size = New System.Drawing.Size(121, 21)
    Me.comGraphUpdateTime.TabIndex = 17
    '
    'lblTooltipSpeedDisplayUnit
    '
    Me.lblTooltipSpeedDisplayUnit.AutoSize = True
    Me.lblTooltipSpeedDisplayUnit.Location = New System.Drawing.Point(8, 142)
    Me.lblTooltipSpeedDisplayUnit.Margin = New System.Windows.Forms.Padding(0, 0, 8, 0)
    Me.lblTooltipSpeedDisplayUnit.Name = "lblTooltipSpeedDisplayUnit"
    Me.lblTooltipSpeedDisplayUnit.Size = New System.Drawing.Size(107, 13)
    Me.lblTooltipSpeedDisplayUnit.TabIndex = 0
    Me.lblTooltipSpeedDisplayUnit.Text = "Speed Display Unit:"
    Me.lblTooltipSpeedDisplayUnit.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
    '
    'comTooltipSpeedDisplayUnit
    '
    Me.comTooltipSpeedDisplayUnit.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
    Me.comTooltipSpeedDisplayUnit.FormattingEnabled = True
    Me.comTooltipSpeedDisplayUnit.Items.AddRange(New Object() {"Auto", "Bits", "Kilobits", "Megabits", "Gigabits", "Terabits", "Bytes", "Kilobytes", "Megabytes", "Gigabytes", "Terabytes"})
    Me.comTooltipSpeedDisplayUnit.Location = New System.Drawing.Point(123, 139)
    Me.comTooltipSpeedDisplayUnit.Margin = New System.Windows.Forms.Padding(0)
    Me.comTooltipSpeedDisplayUnit.Name = "comTooltipSpeedDisplayUnit"
    Me.comTooltipSpeedDisplayUnit.Size = New System.Drawing.Size(160, 21)
    Me.comTooltipSpeedDisplayUnit.TabIndex = 5
    '
    'chkTooltipDownloadAvg
    '
    Me.chkTooltipDownloadAvg.AutoSize = True
    Me.chkTooltipDownloadAvg.Location = New System.Drawing.Point(8, 58)
    Me.chkTooltipDownloadAvg.Margin = New System.Windows.Forms.Padding(0, 0, 8, 0)
    Me.chkTooltipDownloadAvg.Name = "chkTooltipDownloadAvg"
    Me.chkTooltipDownloadAvg.Size = New System.Drawing.Size(156, 17)
    Me.chkTooltipDownloadAvg.TabIndex = 4
    Me.chkTooltipDownloadAvg.Text = "Show Download Average"
    Me.chkTooltipDownloadAvg.UseVisualStyleBackColor = True
    '
    'chkTooltipUploadTotal
    '
    Me.chkTooltipUploadTotal.AutoSize = True
    Me.chkTooltipUploadTotal.Location = New System.Drawing.Point(215, 33)
    Me.chkTooltipUploadTotal.Margin = New System.Windows.Forms.Padding(0)
    Me.chkTooltipUploadTotal.Name = "chkTooltipUploadTotal"
    Me.chkTooltipUploadTotal.Size = New System.Drawing.Size(124, 17)
    Me.chkTooltipUploadTotal.TabIndex = 3
    Me.chkTooltipUploadTotal.Text = "Show Upload Total"
    Me.chkTooltipUploadTotal.UseVisualStyleBackColor = True
    '
    'chkTooltipDownloadTotal
    '
    Me.chkTooltipDownloadTotal.AutoSize = True
    Me.chkTooltipDownloadTotal.Location = New System.Drawing.Point(8, 33)
    Me.chkTooltipDownloadTotal.Margin = New System.Windows.Forms.Padding(0, 0, 8, 0)
    Me.chkTooltipDownloadTotal.Name = "chkTooltipDownloadTotal"
    Me.chkTooltipDownloadTotal.Size = New System.Drawing.Size(140, 17)
    Me.chkTooltipDownloadTotal.TabIndex = 2
    Me.chkTooltipDownloadTotal.Text = "Show Download Total"
    Me.chkTooltipDownloadTotal.UseVisualStyleBackColor = True
    '
    'chkTooltipUploadSpeed
    '
    Me.chkTooltipUploadSpeed.AutoSize = True
    Me.chkTooltipUploadSpeed.Location = New System.Drawing.Point(215, 8)
    Me.chkTooltipUploadSpeed.Margin = New System.Windows.Forms.Padding(0)
    Me.chkTooltipUploadSpeed.Name = "chkTooltipUploadSpeed"
    Me.chkTooltipUploadSpeed.Size = New System.Drawing.Size(131, 17)
    Me.chkTooltipUploadSpeed.TabIndex = 1
    Me.chkTooltipUploadSpeed.Text = "Show Upload Speed"
    Me.chkTooltipUploadSpeed.UseVisualStyleBackColor = True
    '
    'chkTooltipDownloadSpeed
    '
    Me.chkTooltipDownloadSpeed.AutoSize = True
    Me.chkTooltipDownloadSpeed.Location = New System.Drawing.Point(8, 8)
    Me.chkTooltipDownloadSpeed.Margin = New System.Windows.Forms.Padding(0, 0, 8, 0)
    Me.chkTooltipDownloadSpeed.Name = "chkTooltipDownloadSpeed"
    Me.chkTooltipDownloadSpeed.Size = New System.Drawing.Size(147, 17)
    Me.chkTooltipDownloadSpeed.TabIndex = 0
    Me.chkTooltipDownloadSpeed.Text = "Show Download Speed"
    Me.chkTooltipDownloadSpeed.UseVisualStyleBackColor = True
    '
    'chkTooltipUploadAvg
    '
    Me.chkTooltipUploadAvg.AutoSize = True
    Me.chkTooltipUploadAvg.Location = New System.Drawing.Point(215, 58)
    Me.chkTooltipUploadAvg.Margin = New System.Windows.Forms.Padding(0)
    Me.chkTooltipUploadAvg.Name = "chkTooltipUploadAvg"
    Me.chkTooltipUploadAvg.Size = New System.Drawing.Size(140, 17)
    Me.chkTooltipUploadAvg.TabIndex = 4
    Me.chkTooltipUploadAvg.Text = "Show Upload Average"
    Me.chkTooltipUploadAvg.UseVisualStyleBackColor = True
    '
    'lblTooltipTotalDisplayUnit
    '
    Me.lblTooltipTotalDisplayUnit.AutoSize = True
    Me.lblTooltipTotalDisplayUnit.Location = New System.Drawing.Point(15, 173)
    Me.lblTooltipTotalDisplayUnit.Margin = New System.Windows.Forms.Padding(0, 0, 8, 0)
    Me.lblTooltipTotalDisplayUnit.Name = "lblTooltipTotalDisplayUnit"
    Me.lblTooltipTotalDisplayUnit.Size = New System.Drawing.Size(100, 13)
    Me.lblTooltipTotalDisplayUnit.TabIndex = 0
    Me.lblTooltipTotalDisplayUnit.Text = "Total Display Unit:"
    Me.lblTooltipTotalDisplayUnit.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
    '
    'comTooltipTotalDisplayUnit
    '
    Me.comTooltipTotalDisplayUnit.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
    Me.comTooltipTotalDisplayUnit.FormattingEnabled = True
    Me.comTooltipTotalDisplayUnit.Items.AddRange(New Object() {"Auto", "Bits", "Kilobits", "Megabits", "Gigabits", "Terabits", "Bytes", "Kilobytes", "Megabytes", "Gigabytes", "Terabytes"})
    Me.comTooltipTotalDisplayUnit.Location = New System.Drawing.Point(123, 170)
    Me.comTooltipTotalDisplayUnit.Margin = New System.Windows.Forms.Padding(0)
    Me.comTooltipTotalDisplayUnit.Name = "comTooltipTotalDisplayUnit"
    Me.comTooltipTotalDisplayUnit.Size = New System.Drawing.Size(160, 21)
    Me.comTooltipTotalDisplayUnit.TabIndex = 4
    '
    'chkTooltipDownloadMax
    '
    Me.chkTooltipDownloadMax.AutoSize = True
    Me.chkTooltipDownloadMax.Location = New System.Drawing.Point(8, 83)
    Me.chkTooltipDownloadMax.Margin = New System.Windows.Forms.Padding(0, 0, 8, 0)
    Me.chkTooltipDownloadMax.Name = "chkTooltipDownloadMax"
    Me.chkTooltipDownloadMax.Size = New System.Drawing.Size(199, 17)
    Me.chkTooltipDownloadMax.TabIndex = 0
    Me.chkTooltipDownloadMax.Text = "Show Maximum Download Speed"
    Me.chkTooltipDownloadMax.UseVisualStyleBackColor = True
    '
    'chkTooltipUploadMax
    '
    Me.chkTooltipUploadMax.AutoSize = True
    Me.chkTooltipUploadMax.Location = New System.Drawing.Point(215, 83)
    Me.chkTooltipUploadMax.Margin = New System.Windows.Forms.Padding(0, 0, 8, 0)
    Me.chkTooltipUploadMax.Name = "chkTooltipUploadMax"
    Me.chkTooltipUploadMax.Size = New System.Drawing.Size(183, 17)
    Me.chkTooltipUploadMax.TabIndex = 0
    Me.chkTooltipUploadMax.Text = "Show Maximum Upload Speed"
    Me.chkTooltipUploadMax.UseVisualStyleBackColor = True
    '
    'chkTextUploadTotal
    '
    Me.chkTextUploadTotal.AutoSize = True
    Me.chkTextUploadTotal.Location = New System.Drawing.Point(179, 93)
    Me.chkTextUploadTotal.Margin = New System.Windows.Forms.Padding(0)
    Me.chkTextUploadTotal.Name = "chkTextUploadTotal"
    Me.chkTextUploadTotal.Size = New System.Drawing.Size(124, 17)
    Me.chkTextUploadTotal.TabIndex = 3
    Me.chkTextUploadTotal.Text = "Show Upload Total"
    Me.chkTextUploadTotal.UseVisualStyleBackColor = True
    '
    'chkTextUploadSpeed
    '
    Me.chkTextUploadSpeed.AutoSize = True
    Me.chkTextUploadSpeed.Location = New System.Drawing.Point(179, 66)
    Me.chkTextUploadSpeed.Margin = New System.Windows.Forms.Padding(0)
    Me.chkTextUploadSpeed.Name = "chkTextUploadSpeed"
    Me.chkTextUploadSpeed.Size = New System.Drawing.Size(131, 17)
    Me.chkTextUploadSpeed.TabIndex = 2
    Me.chkTextUploadSpeed.Text = "Show Upload Speed"
    Me.chkTextUploadSpeed.UseVisualStyleBackColor = True
    '
    'chkTextDownloadTotal
    '
    Me.chkTextDownloadTotal.AutoSize = True
    Me.chkTextDownloadTotal.Location = New System.Drawing.Point(8, 93)
    Me.chkTextDownloadTotal.Margin = New System.Windows.Forms.Padding(0, 0, 8, 0)
    Me.chkTextDownloadTotal.Name = "chkTextDownloadTotal"
    Me.chkTextDownloadTotal.Size = New System.Drawing.Size(140, 17)
    Me.chkTextDownloadTotal.TabIndex = 1
    Me.chkTextDownloadTotal.Text = "Show Download Total"
    Me.chkTextDownloadTotal.UseVisualStyleBackColor = True
    '
    'chkTextDownloadSpeed
    '
    Me.chkTextDownloadSpeed.AutoSize = True
    Me.chkTextDownloadSpeed.Location = New System.Drawing.Point(8, 66)
    Me.chkTextDownloadSpeed.Margin = New System.Windows.Forms.Padding(0, 0, 8, 0)
    Me.chkTextDownloadSpeed.Name = "chkTextDownloadSpeed"
    Me.chkTextDownloadSpeed.Size = New System.Drawing.Size(147, 17)
    Me.chkTextDownloadSpeed.TabIndex = 0
    Me.chkTextDownloadSpeed.Text = "Show Download Speed"
    Me.chkTextDownloadSpeed.UseVisualStyleBackColor = True
    '
    'lblTextSpeedDisplayUnit
    '
    Me.lblTextSpeedDisplayUnit.AutoSize = True
    Me.lblTextSpeedDisplayUnit.Location = New System.Drawing.Point(8, 158)
    Me.lblTextSpeedDisplayUnit.Margin = New System.Windows.Forms.Padding(0, 0, 8, 0)
    Me.lblTextSpeedDisplayUnit.Name = "lblTextSpeedDisplayUnit"
    Me.lblTextSpeedDisplayUnit.Size = New System.Drawing.Size(107, 13)
    Me.lblTextSpeedDisplayUnit.TabIndex = 0
    Me.lblTextSpeedDisplayUnit.Text = "Speed Display Unit:"
    Me.lblTextSpeedDisplayUnit.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
    '
    'comTextSpeedDisplayUnit
    '
    Me.comTextSpeedDisplayUnit.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
    Me.comTextSpeedDisplayUnit.FormattingEnabled = True
    Me.comTextSpeedDisplayUnit.Items.AddRange(New Object() {"Auto", "Bits", "Kilobits", "Megabits", "Gigabits", "Terabits", "Bytes", "Kilobytes", "Megabytes", "Gigabytes", "Terabytes"})
    Me.comTextSpeedDisplayUnit.Location = New System.Drawing.Point(123, 155)
    Me.comTextSpeedDisplayUnit.Margin = New System.Windows.Forms.Padding(0)
    Me.comTextSpeedDisplayUnit.Name = "comTextSpeedDisplayUnit"
    Me.comTextSpeedDisplayUnit.Size = New System.Drawing.Size(160, 21)
    Me.comTextSpeedDisplayUnit.TabIndex = 4
    '
    'tabSettings
    '
    Me.tabSettings.Controls.Add(Me.tabpageGeneral)
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
    'tabpageGeneral
    '
    Me.tabpageGeneral.Controls.Add(Me.grpStatistics)
    Me.tabpageGeneral.Controls.Add(Me.grpInterface)
    Me.tabpageGeneral.Location = New System.Drawing.Point(4, 22)
    Me.tabpageGeneral.Margin = New System.Windows.Forms.Padding(0)
    Me.tabpageGeneral.Name = "tabpageGeneral"
    Me.tabpageGeneral.Padding = New System.Windows.Forms.Padding(8)
    Me.tabpageGeneral.Size = New System.Drawing.Size(492, 374)
    Me.tabpageGeneral.TabIndex = 3
    Me.tabpageGeneral.Text = "General"
    Me.tabpageGeneral.UseVisualStyleBackColor = True
    '
    'grpStatistics
    '
    Me.grpStatistics.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.grpStatistics.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
    Me.grpStatistics.Controls.Add(Me.chkSaveTransferTotals)
    Me.grpStatistics.Controls.Add(Me.chkSaveAverageSpeeds)
    Me.grpStatistics.Controls.Add(Me.chkResetTransferTotals)
    Me.grpStatistics.Controls.Add(Me.chkResetAvgSpeeds)
    Me.grpStatistics.Controls.Add(Me.chkResetMaxSpeeds)
    Me.grpStatistics.Controls.Add(Me.chkSaveMaxSpeeds)
    Me.grpStatistics.Location = New System.Drawing.Point(8, 81)
    Me.grpStatistics.Margin = New System.Windows.Forms.Padding(0, 4, 0, 0)
    Me.grpStatistics.Name = "grpStatistics"
    Me.grpStatistics.Padding = New System.Windows.Forms.Padding(8, 4, 8, 8)
    Me.grpStatistics.Size = New System.Drawing.Size(476, 69)
    Me.grpStatistics.TabIndex = 1
    Me.grpStatistics.TabStop = False
    Me.grpStatistics.Text = "Statistics"
    '
    'chkSaveTransferTotals
    '
    Me.chkSaveTransferTotals.AutoSize = True
    Me.chkSaveTransferTotals.Location = New System.Drawing.Point(308, 19)
    Me.chkSaveTransferTotals.Margin = New System.Windows.Forms.Padding(0, 0, 8, 0)
    Me.chkSaveTransferTotals.Name = "chkSaveTransferTotals"
    Me.chkSaveTransferTotals.Size = New System.Drawing.Size(126, 17)
    Me.chkSaveTransferTotals.TabIndex = 0
    Me.chkSaveTransferTotals.Text = "Save Transfer Totals"
    Me.chkSaveTransferTotals.UseVisualStyleBackColor = True
    '
    'chkSaveAverageSpeeds
    '
    Me.chkSaveAverageSpeeds.AutoSize = True
    Me.chkSaveAverageSpeeds.Location = New System.Drawing.Point(8, 19)
    Me.chkSaveAverageSpeeds.Margin = New System.Windows.Forms.Padding(0, 0, 8, 0)
    Me.chkSaveAverageSpeeds.Name = "chkSaveAverageSpeeds"
    Me.chkSaveAverageSpeeds.Size = New System.Drawing.Size(133, 17)
    Me.chkSaveAverageSpeeds.TabIndex = 0
    Me.chkSaveAverageSpeeds.Text = "Save Average Speeds"
    Me.chkSaveAverageSpeeds.UseVisualStyleBackColor = True
    '
    'chkResetTransferTotals
    '
    Me.chkResetTransferTotals.AutoSize = True
    Me.chkResetTransferTotals.Location = New System.Drawing.Point(308, 44)
    Me.chkResetTransferTotals.Margin = New System.Windows.Forms.Padding(0, 0, 8, 0)
    Me.chkResetTransferTotals.Name = "chkResetTransferTotals"
    Me.chkResetTransferTotals.Size = New System.Drawing.Size(131, 17)
    Me.chkResetTransferTotals.TabIndex = 0
    Me.chkResetTransferTotals.Text = "Reset Transfer Totals"
    Me.chkResetTransferTotals.UseVisualStyleBackColor = True
    '
    'chkResetAvgSpeeds
    '
    Me.chkResetAvgSpeeds.AutoSize = True
    Me.chkResetAvgSpeeds.Location = New System.Drawing.Point(8, 44)
    Me.chkResetAvgSpeeds.Margin = New System.Windows.Forms.Padding(0, 0, 8, 0)
    Me.chkResetAvgSpeeds.Name = "chkResetAvgSpeeds"
    Me.chkResetAvgSpeeds.Size = New System.Drawing.Size(138, 17)
    Me.chkResetAvgSpeeds.TabIndex = 0
    Me.chkResetAvgSpeeds.Text = "Reset Average Speeds"
    Me.chkResetAvgSpeeds.UseVisualStyleBackColor = True
    '
    'chkResetMaxSpeeds
    '
    Me.chkResetMaxSpeeds.AutoSize = True
    Me.chkResetMaxSpeeds.Location = New System.Drawing.Point(154, 44)
    Me.chkResetMaxSpeeds.Margin = New System.Windows.Forms.Padding(0, 0, 8, 0)
    Me.chkResetMaxSpeeds.Name = "chkResetMaxSpeeds"
    Me.chkResetMaxSpeeds.Size = New System.Drawing.Size(146, 17)
    Me.chkResetMaxSpeeds.TabIndex = 0
    Me.chkResetMaxSpeeds.Text = "Reset Maximum Speeds"
    Me.chkResetMaxSpeeds.UseVisualStyleBackColor = True
    '
    'chkSaveMaxSpeeds
    '
    Me.chkSaveMaxSpeeds.AutoSize = True
    Me.chkSaveMaxSpeeds.Location = New System.Drawing.Point(154, 19)
    Me.chkSaveMaxSpeeds.Margin = New System.Windows.Forms.Padding(0, 0, 8, 0)
    Me.chkSaveMaxSpeeds.Name = "chkSaveMaxSpeeds"
    Me.chkSaveMaxSpeeds.Size = New System.Drawing.Size(141, 17)
    Me.chkSaveMaxSpeeds.TabIndex = 0
    Me.chkSaveMaxSpeeds.Text = "Save Maximum Speeds"
    Me.chkSaveMaxSpeeds.UseVisualStyleBackColor = True
    '
    'grpInterface
    '
    Me.grpInterface.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.grpInterface.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
    Me.grpInterface.Controls.Add(Me.comInterface)
    Me.grpInterface.Controls.Add(Me.lblInterface)
    Me.grpInterface.Location = New System.Drawing.Point(8, 8)
    Me.grpInterface.Margin = New System.Windows.Forms.Padding(0)
    Me.grpInterface.Name = "grpInterface"
    Me.grpInterface.Padding = New System.Windows.Forms.Padding(8, 4, 8, 8)
    Me.grpInterface.Size = New System.Drawing.Size(476, 69)
    Me.grpInterface.TabIndex = 0
    Me.grpInterface.TabStop = False
    Me.grpInterface.Text = "Network Interface"
    '
    'comInterface
    '
    Me.comInterface.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.comInterface.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
    Me.comInterface.FormattingEnabled = True
    Me.comInterface.Location = New System.Drawing.Point(8, 40)
    Me.comInterface.Margin = New System.Windows.Forms.Padding(0)
    Me.comInterface.Name = "comInterface"
    Me.comInterface.Size = New System.Drawing.Size(460, 21)
    Me.comInterface.TabIndex = 0
    '
    'lblInterface
    '
    Me.lblInterface.AutoSize = True
    Me.lblInterface.Location = New System.Drawing.Point(8, 19)
    Me.lblInterface.Margin = New System.Windows.Forms.Padding(0, 0, 0, 8)
    Me.lblInterface.Name = "lblInterface"
    Me.lblInterface.Size = New System.Drawing.Size(346, 13)
    Me.lblInterface.TabIndex = 1
    Me.lblInterface.Text = "Choose the network interface that you want to view the status of."
    '
    'tabPageToolbar
    '
    Me.tabPageToolbar.Controls.Add(Me.chkToolbarInternalIP)
    Me.tabPageToolbar.Controls.Add(Me.chkToolbarExternalIP)
    Me.tabPageToolbar.Controls.Add(Me.radTextVert)
    Me.tabPageToolbar.Controls.Add(Me.lblTextTotalDisplayUnit)
    Me.tabPageToolbar.Controls.Add(Me.comTextTotalDisplayUnit)
    Me.tabPageToolbar.Controls.Add(Me.lblTextSpeedDisplayUnit)
    Me.tabPageToolbar.Controls.Add(Me.comTextSpeedDisplayUnit)
    Me.tabPageToolbar.Controls.Add(Me.lblTextOrientation)
    Me.tabPageToolbar.Controls.Add(Me.chkTextUploadTotal)
    Me.tabPageToolbar.Controls.Add(Me.radTextHorz)
    Me.tabPageToolbar.Controls.Add(Me.chkTextDownloadSpeed)
    Me.tabPageToolbar.Controls.Add(Me.chkTextUploadSpeed)
    Me.tabPageToolbar.Controls.Add(Me.chkTextDownloadTotal)
    Me.tabPageToolbar.Controls.Add(Me.chkShowIcon)
    Me.tabPageToolbar.Controls.Add(Me.chkShowText)
    Me.tabPageToolbar.Location = New System.Drawing.Point(4, 22)
    Me.tabPageToolbar.Margin = New System.Windows.Forms.Padding(0)
    Me.tabPageToolbar.Name = "tabPageToolbar"
    Me.tabPageToolbar.Padding = New System.Windows.Forms.Padding(8)
    Me.tabPageToolbar.Size = New System.Drawing.Size(492, 374)
    Me.tabPageToolbar.TabIndex = 0
    Me.tabPageToolbar.Text = "Toolbar"
    Me.tabPageToolbar.UseVisualStyleBackColor = True
    '
    'chkToolbarInternalIP
    '
    Me.chkToolbarInternalIP.AutoSize = True
    Me.chkToolbarInternalIP.Location = New System.Drawing.Point(179, 119)
    Me.chkToolbarInternalIP.Name = "chkToolbarInternalIP"
    Me.chkToolbarInternalIP.Size = New System.Drawing.Size(154, 17)
    Me.chkToolbarInternalIP.TabIndex = 11
    Me.chkToolbarInternalIP.Text = "Show Internal IP Address"
    Me.chkToolbarInternalIP.UseVisualStyleBackColor = True
    '
    'chkToolbarExternalIP
    '
    Me.chkToolbarExternalIP.AutoSize = True
    Me.chkToolbarExternalIP.Location = New System.Drawing.Point(8, 119)
    Me.chkToolbarExternalIP.Name = "chkToolbarExternalIP"
    Me.chkToolbarExternalIP.Size = New System.Drawing.Size(155, 17)
    Me.chkToolbarExternalIP.TabIndex = 10
    Me.chkToolbarExternalIP.Text = "Show External IP Address"
    Me.chkToolbarExternalIP.UseVisualStyleBackColor = True
    '
    'radTextVert
    '
    Me.radTextVert.AutoSize = True
    Me.radTextVert.Location = New System.Drawing.Point(166, 37)
    Me.radTextVert.Margin = New System.Windows.Forms.Padding(3, 0, 3, 0)
    Me.radTextVert.Name = "radTextVert"
    Me.radTextVert.Size = New System.Drawing.Size(63, 17)
    Me.radTextVert.TabIndex = 2
    Me.radTextVert.TabStop = True
    Me.radTextVert.Text = "Vertical"
    Me.radTextVert.UseVisualStyleBackColor = True
    '
    'lblTextTotalDisplayUnit
    '
    Me.lblTextTotalDisplayUnit.AutoSize = True
    Me.lblTextTotalDisplayUnit.Location = New System.Drawing.Point(15, 188)
    Me.lblTextTotalDisplayUnit.Margin = New System.Windows.Forms.Padding(0, 0, 8, 0)
    Me.lblTextTotalDisplayUnit.Name = "lblTextTotalDisplayUnit"
    Me.lblTextTotalDisplayUnit.Size = New System.Drawing.Size(100, 13)
    Me.lblTextTotalDisplayUnit.TabIndex = 0
    Me.lblTextTotalDisplayUnit.Text = "Total Display Unit:"
    Me.lblTextTotalDisplayUnit.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
    '
    'comTextTotalDisplayUnit
    '
    Me.comTextTotalDisplayUnit.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
    Me.comTextTotalDisplayUnit.FormattingEnabled = True
    Me.comTextTotalDisplayUnit.Items.AddRange(New Object() {"Auto", "Bits", "Kilobits", "Megabits", "Gigabits", "Terabits", "Bytes", "Kilobytes", "Megabytes", "Gigabytes", "Terabytes"})
    Me.comTextTotalDisplayUnit.Location = New System.Drawing.Point(123, 185)
    Me.comTextTotalDisplayUnit.Margin = New System.Windows.Forms.Padding(0)
    Me.comTextTotalDisplayUnit.Name = "comTextTotalDisplayUnit"
    Me.comTextTotalDisplayUnit.Size = New System.Drawing.Size(160, 21)
    Me.comTextTotalDisplayUnit.TabIndex = 4
    '
    'lblTextOrientation
    '
    Me.lblTextOrientation.AutoSize = True
    Me.lblTextOrientation.Location = New System.Drawing.Point(5, 39)
    Me.lblTextOrientation.Name = "lblTextOrientation"
    Me.lblTextOrientation.Size = New System.Drawing.Size(70, 13)
    Me.lblTextOrientation.TabIndex = 0
    Me.lblTextOrientation.Text = "Orientation:"
    Me.lblTextOrientation.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
    '
    'radTextHorz
    '
    Me.radTextHorz.AutoSize = True
    Me.radTextHorz.Location = New System.Drawing.Point(81, 37)
    Me.radTextHorz.Margin = New System.Windows.Forms.Padding(3, 0, 3, 0)
    Me.radTextHorz.Name = "radTextHorz"
    Me.radTextHorz.Size = New System.Drawing.Size(79, 17)
    Me.radTextHorz.TabIndex = 1
    Me.radTextHorz.TabStop = True
    Me.radTextHorz.Text = "Horizontal"
    Me.radTextHorz.UseVisualStyleBackColor = True
    '
    'tabpageTooltip
    '
    Me.tabpageTooltip.Controls.Add(Me.chkTooltipInternalIP)
    Me.tabpageTooltip.Controls.Add(Me.chkTooltipExternalIP)
    Me.tabpageTooltip.Controls.Add(Me.lblTooltipTotalDisplayUnit)
    Me.tabpageTooltip.Controls.Add(Me.comTooltipTotalDisplayUnit)
    Me.tabpageTooltip.Controls.Add(Me.lblTooltipSpeedDisplayUnit)
    Me.tabpageTooltip.Controls.Add(Me.comTooltipSpeedDisplayUnit)
    Me.tabpageTooltip.Controls.Add(Me.chkTooltipDownloadAvg)
    Me.tabpageTooltip.Controls.Add(Me.chkTooltipDownloadSpeed)
    Me.tabpageTooltip.Controls.Add(Me.chkTooltipUploadTotal)
    Me.tabpageTooltip.Controls.Add(Me.chkTooltipUploadMax)
    Me.tabpageTooltip.Controls.Add(Me.chkTooltipDownloadTotal)
    Me.tabpageTooltip.Controls.Add(Me.chkTooltipDownloadMax)
    Me.tabpageTooltip.Controls.Add(Me.chkTooltipUploadSpeed)
    Me.tabpageTooltip.Controls.Add(Me.chkTooltipUploadAvg)
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
    Me.tabpageGraph.Controls.Add(Me.lblStaticMaxUp)
    Me.tabpageGraph.Controls.Add(Me.txtGraphStaticMaxUp)
    Me.tabpageGraph.Controls.Add(Me.lblStaticMaxUpKbs)
    Me.tabpageGraph.Controls.Add(Me.lblStaticMaxDown)
    Me.tabpageGraph.Controls.Add(Me.chkStaticMaxValues)
    Me.tabpageGraph.Controls.Add(Me.txtGraphStaticMaxDown)
    Me.tabpageGraph.Controls.Add(Me.lblGraphUpdateTime)
    Me.tabpageGraph.Controls.Add(Me.lblStaticMaxDownKbs)
    Me.tabpageGraph.Controls.Add(Me.nudGraphUpdateTime)
    Me.tabpageGraph.Controls.Add(Me.picGraphDownloadColor)
    Me.tabpageGraph.Controls.Add(Me.comGraphUpdateTime)
    Me.tabpageGraph.Controls.Add(Me.lblGraphDownloadColor)
    Me.tabpageGraph.Controls.Add(Me.lblGraphDisplay)
    Me.tabpageGraph.Controls.Add(Me.picGraphUploadColor)
    Me.tabpageGraph.Controls.Add(Me.comGraphDisplay)
    Me.tabpageGraph.Controls.Add(Me.lblGraphUploadColor)
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
    'lblStaticMaxUp
    '
    Me.lblStaticMaxUp.AutoSize = True
    Me.lblStaticMaxUp.Location = New System.Drawing.Point(231, 157)
    Me.lblStaticMaxUp.Name = "lblStaticMaxUp"
    Me.lblStaticMaxUp.Size = New System.Drawing.Size(48, 13)
    Me.lblStaticMaxUp.TabIndex = 24
    Me.lblStaticMaxUp.Text = "Upload:"
    '
    'txtGraphStaticMaxUp
    '
    Me.txtGraphStaticMaxUp.Location = New System.Drawing.Point(282, 154)
    Me.txtGraphStaticMaxUp.Margin = New System.Windows.Forms.Padding(0)
    Me.txtGraphStaticMaxUp.Name = "txtGraphStaticMaxUp"
    Me.txtGraphStaticMaxUp.Size = New System.Drawing.Size(88, 22)
    Me.txtGraphStaticMaxUp.TabIndex = 22
    '
    'lblStaticMaxUpKbs
    '
    Me.lblStaticMaxUpKbs.AutoSize = True
    Me.lblStaticMaxUpKbs.Location = New System.Drawing.Point(376, 157)
    Me.lblStaticMaxUpKbs.Margin = New System.Windows.Forms.Padding(6, 0, 0, 0)
    Me.lblStaticMaxUpKbs.Name = "lblStaticMaxUpKbs"
    Me.lblStaticMaxUpKbs.Size = New System.Drawing.Size(29, 13)
    Me.lblStaticMaxUpKbs.TabIndex = 23
    Me.lblStaticMaxUpKbs.Text = "KB/s"
    Me.lblStaticMaxUpKbs.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
    '
    'lblStaticMaxDown
    '
    Me.lblStaticMaxDown.AutoSize = True
    Me.lblStaticMaxDown.Location = New System.Drawing.Point(28, 157)
    Me.lblStaticMaxDown.Name = "lblStaticMaxDown"
    Me.lblStaticMaxDown.Size = New System.Drawing.Size(64, 13)
    Me.lblStaticMaxDown.TabIndex = 21
    Me.lblStaticMaxDown.Text = "Download:"
    '
    'chkStaticMaxValues
    '
    Me.chkStaticMaxValues.AutoSize = True
    Me.chkStaticMaxValues.Location = New System.Drawing.Point(8, 130)
    Me.chkStaticMaxValues.Margin = New System.Windows.Forms.Padding(0)
    Me.chkStaticMaxValues.Name = "chkStaticMaxValues"
    Me.chkStaticMaxValues.Size = New System.Drawing.Size(140, 17)
    Me.chkStaticMaxValues.TabIndex = 17
    Me.chkStaticMaxValues.Text = "Use Static Max Values:"
    Me.chkStaticMaxValues.UseVisualStyleBackColor = True
    '
    'txtGraphStaticMaxDown
    '
    Me.txtGraphStaticMaxDown.Location = New System.Drawing.Point(95, 154)
    Me.txtGraphStaticMaxDown.Margin = New System.Windows.Forms.Padding(0)
    Me.txtGraphStaticMaxDown.Name = "txtGraphStaticMaxDown"
    Me.txtGraphStaticMaxDown.Size = New System.Drawing.Size(88, 22)
    Me.txtGraphStaticMaxDown.TabIndex = 19
    '
    'lblStaticMaxDownKbs
    '
    Me.lblStaticMaxDownKbs.AutoSize = True
    Me.lblStaticMaxDownKbs.Location = New System.Drawing.Point(189, 157)
    Me.lblStaticMaxDownKbs.Margin = New System.Windows.Forms.Padding(6, 0, 0, 0)
    Me.lblStaticMaxDownKbs.Name = "lblStaticMaxDownKbs"
    Me.lblStaticMaxDownKbs.Size = New System.Drawing.Size(29, 13)
    Me.lblStaticMaxDownKbs.TabIndex = 20
    Me.lblStaticMaxDownKbs.Text = "KB/s"
    Me.lblStaticMaxDownKbs.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
    '
    'picGraphDownloadColor
    '
    Me.picGraphDownloadColor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
    Me.picGraphDownloadColor.Location = New System.Drawing.Point(8, 69)
    Me.picGraphDownloadColor.Margin = New System.Windows.Forms.Padding(0, 0, 4, 0)
    Me.picGraphDownloadColor.Name = "picGraphDownloadColor"
    Me.picGraphDownloadColor.Size = New System.Drawing.Size(16, 16)
    Me.picGraphDownloadColor.TabIndex = 0
    Me.picGraphDownloadColor.TabStop = False
    '
    'lblGraphDownloadColor
    '
    Me.lblGraphDownloadColor.AutoSize = True
    Me.lblGraphDownloadColor.Location = New System.Drawing.Point(28, 69)
    Me.lblGraphDownloadColor.Margin = New System.Windows.Forms.Padding(0, 0, 16, 0)
    Me.lblGraphDownloadColor.Name = "lblGraphDownloadColor"
    Me.lblGraphDownloadColor.Size = New System.Drawing.Size(92, 13)
    Me.lblGraphDownloadColor.TabIndex = 3
    Me.lblGraphDownloadColor.Text = "Download Color"
    '
    'lblGraphDisplay
    '
    Me.lblGraphDisplay.AutoSize = True
    Me.lblGraphDisplay.Location = New System.Drawing.Point(8, 36)
    Me.lblGraphDisplay.Margin = New System.Windows.Forms.Padding(0, 0, 8, 0)
    Me.lblGraphDisplay.Name = "lblGraphDisplay"
    Me.lblGraphDisplay.Size = New System.Drawing.Size(47, 13)
    Me.lblGraphDisplay.TabIndex = 3
    Me.lblGraphDisplay.Text = "Display:"
    Me.lblGraphDisplay.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
    '
    'picGraphUploadColor
    '
    Me.picGraphUploadColor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
    Me.picGraphUploadColor.Location = New System.Drawing.Point(136, 69)
    Me.picGraphUploadColor.Margin = New System.Windows.Forms.Padding(0, 0, 4, 0)
    Me.picGraphUploadColor.Name = "picGraphUploadColor"
    Me.picGraphUploadColor.Size = New System.Drawing.Size(16, 16)
    Me.picGraphUploadColor.TabIndex = 0
    Me.picGraphUploadColor.TabStop = False
    '
    'comGraphDisplay
    '
    Me.comGraphDisplay.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
    Me.comGraphDisplay.FormattingEnabled = True
    Me.comGraphDisplay.Items.AddRange(New Object() {"Download Speed", "Upload Speed", "Both"})
    Me.comGraphDisplay.Location = New System.Drawing.Point(63, 36)
    Me.comGraphDisplay.Margin = New System.Windows.Forms.Padding(0)
    Me.comGraphDisplay.Name = "comGraphDisplay"
    Me.comGraphDisplay.Size = New System.Drawing.Size(151, 21)
    Me.comGraphDisplay.TabIndex = 17
    '
    'lblGraphUploadColor
    '
    Me.lblGraphUploadColor.AutoSize = True
    Me.lblGraphUploadColor.Location = New System.Drawing.Point(156, 69)
    Me.lblGraphUploadColor.Margin = New System.Windows.Forms.Padding(0, 0, 16, 0)
    Me.lblGraphUploadColor.Name = "lblGraphUploadColor"
    Me.lblGraphUploadColor.Size = New System.Drawing.Size(76, 13)
    Me.lblGraphUploadColor.TabIndex = 3
    Me.lblGraphUploadColor.Text = "Upload Color"
    '
    'chkTooltipInternalIP
    '
    Me.chkTooltipInternalIP.AutoSize = True
    Me.chkTooltipInternalIP.Location = New System.Drawing.Point(215, 109)
    Me.chkTooltipInternalIP.Name = "chkTooltipInternalIP"
    Me.chkTooltipInternalIP.Size = New System.Drawing.Size(154, 17)
    Me.chkTooltipInternalIP.TabIndex = 13
    Me.chkTooltipInternalIP.Text = "Show Internal IP Address"
    Me.chkTooltipInternalIP.UseVisualStyleBackColor = True
    '
    'chkTooltipExternalIP
    '
    Me.chkTooltipExternalIP.AutoSize = True
    Me.chkTooltipExternalIP.Location = New System.Drawing.Point(8, 109)
    Me.chkTooltipExternalIP.Name = "chkTooltipExternalIP"
    Me.chkTooltipExternalIP.Size = New System.Drawing.Size(155, 17)
    Me.chkTooltipExternalIP.TabIndex = 12
    Me.chkTooltipExternalIP.Text = "Show External IP Address"
    Me.chkTooltipExternalIP.UseVisualStyleBackColor = True
    '
    'Settings
    '
    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit
    Me.Controls.Add(Me.tabSettings)
    Me.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.Margin = New System.Windows.Forms.Padding(0)
    Me.Name = "Settings"
    Me.Size = New System.Drawing.Size(500, 400)
    CType(Me.nudGraphUpdateTime, System.ComponentModel.ISupportInitialize).EndInit()
    Me.tabSettings.ResumeLayout(False)
    Me.tabpageGeneral.ResumeLayout(False)
    Me.grpStatistics.ResumeLayout(False)
    Me.grpStatistics.PerformLayout()
    Me.grpInterface.ResumeLayout(False)
    Me.grpInterface.PerformLayout()
    Me.tabPageToolbar.ResumeLayout(False)
    Me.tabPageToolbar.PerformLayout()
    Me.tabpageTooltip.ResumeLayout(False)
    Me.tabpageTooltip.PerformLayout()
    Me.tabpageGraph.ResumeLayout(False)
    Me.tabpageGraph.PerformLayout()
    CType(Me.picGraphDownloadColor, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.picGraphUploadColor, System.ComponentModel.ISupportInitialize).EndInit()
    Me.ResumeLayout(False)

  End Sub
  Friend WithEvents chkShowIcon As System.Windows.Forms.CheckBox
  Friend WithEvents chkShowText As System.Windows.Forms.CheckBox
  Friend WithEvents chkShowGraph As System.Windows.Forms.CheckBox
  Friend WithEvents chkTextDownloadSpeed As System.Windows.Forms.CheckBox
  Friend WithEvents chkTextDownloadTotal As System.Windows.Forms.CheckBox
  Friend WithEvents chkTextUploadSpeed As System.Windows.Forms.CheckBox
  Friend WithEvents chkTextUploadTotal As System.Windows.Forms.CheckBox
  Friend WithEvents lblTextSpeedDisplayUnit As System.Windows.Forms.Label
  Friend WithEvents chkTooltipDownloadAvg As System.Windows.Forms.CheckBox
  Friend WithEvents chkTooltipUploadTotal As System.Windows.Forms.CheckBox
  Friend WithEvents chkTooltipDownloadTotal As System.Windows.Forms.CheckBox
  Friend WithEvents chkTooltipUploadSpeed As System.Windows.Forms.CheckBox
  Friend WithEvents chkTooltipDownloadSpeed As System.Windows.Forms.CheckBox
  Friend WithEvents lblTooltipSpeedDisplayUnit As System.Windows.Forms.Label
  Friend WithEvents lblGraphUpdateTime As System.Windows.Forms.Label
  Friend WithEvents nudGraphUpdateTime As System.Windows.Forms.NumericUpDown
  Friend WithEvents comGraphUpdateTime As System.Windows.Forms.ComboBox
  Friend WithEvents tabSettings As System.Windows.Forms.TabControl
  Friend WithEvents tabPageToolbar As System.Windows.Forms.TabPage
  Friend WithEvents tabpageTooltip As System.Windows.Forms.TabPage
  Friend WithEvents tabpageGraph As System.Windows.Forms.TabPage
  Friend WithEvents tabpageGeneral As System.Windows.Forms.TabPage
  Friend WithEvents grpInterface As System.Windows.Forms.GroupBox
  Friend WithEvents comInterface As System.Windows.Forms.ComboBox
  Friend WithEvents lblInterface As System.Windows.Forms.Label
  Friend WithEvents comTextSpeedDisplayUnit As System.Windows.Forms.ComboBox
  Friend WithEvents comTooltipSpeedDisplayUnit As System.Windows.Forms.ComboBox
  Friend WithEvents chkTooltipUploadAvg As System.Windows.Forms.CheckBox
  Friend WithEvents lblGraphDisplay As System.Windows.Forms.Label
  Friend WithEvents comGraphDisplay As System.Windows.Forms.ComboBox
  Friend WithEvents picGraphDownloadColor As System.Windows.Forms.PictureBox
  Friend WithEvents lblGraphDownloadColor As System.Windows.Forms.Label
  Friend WithEvents picGraphUploadColor As System.Windows.Forms.PictureBox
  Friend WithEvents lblGraphUploadColor As System.Windows.Forms.Label
  Friend WithEvents lblTextTotalDisplayUnit As System.Windows.Forms.Label
  Friend WithEvents comTextTotalDisplayUnit As System.Windows.Forms.ComboBox
  Friend WithEvents lblTooltipTotalDisplayUnit As System.Windows.Forms.Label
  Friend WithEvents comTooltipTotalDisplayUnit As System.Windows.Forms.ComboBox
  Friend WithEvents grpStatistics As System.Windows.Forms.GroupBox
  Friend WithEvents chkSaveAverageSpeeds As System.Windows.Forms.CheckBox
  Friend WithEvents chkSaveTransferTotals As System.Windows.Forms.CheckBox
  Friend WithEvents chkSaveMaxSpeeds As System.Windows.Forms.CheckBox
  Friend WithEvents chkTooltipDownloadMax As System.Windows.Forms.CheckBox
  Friend WithEvents chkTooltipUploadMax As System.Windows.Forms.CheckBox
  Friend WithEvents chkResetAvgSpeeds As System.Windows.Forms.CheckBox
  Friend WithEvents chkResetMaxSpeeds As System.Windows.Forms.CheckBox
  Friend WithEvents chkResetTransferTotals As System.Windows.Forms.CheckBox
  Friend WithEvents lblTextOrientation As System.Windows.Forms.Label
  Friend WithEvents radTextHorz As System.Windows.Forms.RadioButton
  Friend WithEvents radTextVert As System.Windows.Forms.RadioButton
  Friend WithEvents chkStaticMaxValues As System.Windows.Forms.CheckBox
  Friend WithEvents txtGraphStaticMaxDown As System.Windows.Forms.TextBox
  Friend WithEvents lblStaticMaxDownKbs As System.Windows.Forms.Label
  Friend WithEvents lblStaticMaxUp As System.Windows.Forms.Label
  Friend WithEvents txtGraphStaticMaxUp As System.Windows.Forms.TextBox
  Friend WithEvents lblStaticMaxUpKbs As System.Windows.Forms.Label
  Friend WithEvents lblStaticMaxDown As System.Windows.Forms.Label
  Friend WithEvents chkToolbarInternalIP As System.Windows.Forms.CheckBox
  Friend WithEvents chkToolbarExternalIP As System.Windows.Forms.CheckBox
  Friend WithEvents chkTooltipInternalIP As System.Windows.Forms.CheckBox
  Friend WithEvents chkTooltipExternalIP As System.Windows.Forms.CheckBox
End Class
