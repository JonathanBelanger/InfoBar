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
    Me.grpMeasurement = New System.Windows.Forms.GroupBox
    Me.radMeasurementMetric = New System.Windows.Forms.RadioButton
    Me.radMeasurementCustomary = New System.Windows.Forms.RadioButton
    Me.grpUpdates = New System.Windows.Forms.GroupBox
    Me.lblCheckMinutes = New System.Windows.Forms.Label
    Me.nudUpdateInterval = New System.Windows.Forms.NumericUpDown
    Me.lblIntervalEvery = New System.Windows.Forms.Label
    Me.chkUpdateAtIntervals = New System.Windows.Forms.CheckBox
    Me.grpAppearance = New System.Windows.Forms.GroupBox
    Me.chkTextCondition = New System.Windows.Forms.CheckBox
    Me.chkTextTemp = New System.Windows.Forms.CheckBox
    Me.lblText = New System.Windows.Forms.Label
    Me.chkShowIcon = New System.Windows.Forms.CheckBox
    Me.grpLocation = New System.Windows.Forms.GroupBox
    Me.txtLocationID = New System.Windows.Forms.TextBox
    Me.txtLocation = New System.Windows.Forms.TextBox
    Me.btnLocationSearch = New System.Windows.Forms.Button
    Me.TabPage2 = New System.Windows.Forms.TabPage
    Me.lblTooltipInfo = New System.Windows.Forms.Label
    Me.lstTooltipContents = New System.Windows.Forms.CheckedListBox
    Me.TabPage3 = New System.Windows.Forms.TabPage
    Me.picImageSetPreview = New System.Windows.Forms.PictureBox
    Me.lvImageSet = New System.Windows.Forms.ListView
    Me.colhdrName = New System.Windows.Forms.ColumnHeader
    Me.colhdrAuthor = New System.Windows.Forms.ColumnHeader
    Me.colhdrVersion = New System.Windows.Forms.ColumnHeader
    Me.TabPage4 = New System.Windows.Forms.TabPage
    Me.llWeatherDotCom = New System.Windows.Forms.LinkLabel
    Me.lblTWCi = New System.Windows.Forms.Label
    Me.picTWCi = New System.Windows.Forms.PictureBox
    Me.Tabs.SuspendLayout()
    Me.TabPage1.SuspendLayout()
    Me.grpMeasurement.SuspendLayout()
    Me.grpUpdates.SuspendLayout()
    CType(Me.nudUpdateInterval, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.grpAppearance.SuspendLayout()
    Me.grpLocation.SuspendLayout()
    Me.TabPage2.SuspendLayout()
    Me.TabPage3.SuspendLayout()
    CType(Me.picImageSetPreview, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.TabPage4.SuspendLayout()
    CType(Me.picTWCi, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.SuspendLayout()
    '
    'Tabs
    '
    Me.Tabs.Controls.Add(Me.TabPage1)
    Me.Tabs.Controls.Add(Me.TabPage2)
    Me.Tabs.Controls.Add(Me.TabPage3)
    Me.Tabs.Controls.Add(Me.TabPage4)
    Me.Tabs.Dock = System.Windows.Forms.DockStyle.Fill
    Me.Tabs.Location = New System.Drawing.Point(0, 0)
    Me.Tabs.Name = "Tabs"
    Me.Tabs.SelectedIndex = 0
    Me.Tabs.Size = New System.Drawing.Size(500, 400)
    Me.Tabs.TabIndex = 2
    '
    'TabPage1
    '
    Me.TabPage1.Controls.Add(Me.grpMeasurement)
    Me.TabPage1.Controls.Add(Me.grpUpdates)
    Me.TabPage1.Controls.Add(Me.grpAppearance)
    Me.TabPage1.Controls.Add(Me.grpLocation)
    Me.TabPage1.Location = New System.Drawing.Point(4, 22)
    Me.TabPage1.Name = "TabPage1"
    Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
    Me.TabPage1.Size = New System.Drawing.Size(492, 374)
    Me.TabPage1.TabIndex = 0
    Me.TabPage1.Text = "General"
    Me.TabPage1.UseVisualStyleBackColor = True
    '
    'grpMeasurement
    '
    Me.grpMeasurement.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.grpMeasurement.Controls.Add(Me.radMeasurementMetric)
    Me.grpMeasurement.Controls.Add(Me.radMeasurementCustomary)
    Me.grpMeasurement.Location = New System.Drawing.Point(7, 99)
    Me.grpMeasurement.Name = "grpMeasurement"
    Me.grpMeasurement.Size = New System.Drawing.Size(479, 46)
    Me.grpMeasurement.TabIndex = 3
    Me.grpMeasurement.TabStop = False
    Me.grpMeasurement.Text = "System of Measurement"
    '
    'radMeasurementMetric
    '
    Me.radMeasurementMetric.AutoSize = True
    Me.radMeasurementMetric.Location = New System.Drawing.Point(119, 18)
    Me.radMeasurementMetric.Name = "radMeasurementMetric"
    Me.radMeasurementMetric.Size = New System.Drawing.Size(57, 17)
    Me.radMeasurementMetric.TabIndex = 1
    Me.radMeasurementMetric.TabStop = True
    Me.radMeasurementMetric.Text = "Metric"
    Me.radMeasurementMetric.UseVisualStyleBackColor = True
    '
    'radMeasurementCustomary
    '
    Me.radMeasurementCustomary.AutoSize = True
    Me.radMeasurementCustomary.Location = New System.Drawing.Point(11, 18)
    Me.radMeasurementCustomary.Name = "radMeasurementCustomary"
    Me.radMeasurementCustomary.Size = New System.Drawing.Size(102, 17)
    Me.radMeasurementCustomary.TabIndex = 0
    Me.radMeasurementCustomary.TabStop = True
    Me.radMeasurementCustomary.Text = "Customary (US)"
    Me.radMeasurementCustomary.UseVisualStyleBackColor = True
    '
    'grpUpdates
    '
    Me.grpUpdates.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.grpUpdates.Controls.Add(Me.lblCheckMinutes)
    Me.grpUpdates.Controls.Add(Me.nudUpdateInterval)
    Me.grpUpdates.Controls.Add(Me.lblIntervalEvery)
    Me.grpUpdates.Controls.Add(Me.chkUpdateAtIntervals)
    Me.grpUpdates.Location = New System.Drawing.Point(7, 151)
    Me.grpUpdates.Name = "grpUpdates"
    Me.grpUpdates.Size = New System.Drawing.Size(479, 78)
    Me.grpUpdates.TabIndex = 2
    Me.grpUpdates.TabStop = False
    Me.grpUpdates.Text = "Periodic Updates"
    '
    'lblCheckMinutes
    '
    Me.lblCheckMinutes.AutoSize = True
    Me.lblCheckMinutes.Location = New System.Drawing.Point(155, 46)
    Me.lblCheckMinutes.Name = "lblCheckMinutes"
    Me.lblCheckMinutes.Size = New System.Drawing.Size(48, 13)
    Me.lblCheckMinutes.TabIndex = 3
    Me.lblCheckMinutes.Text = "minutes"
    '
    'nudUpdateInterval
    '
    Me.nudUpdateInterval.Location = New System.Drawing.Point(83, 44)
    Me.nudUpdateInterval.Maximum = New Decimal(New Integer() {1440, 0, 0, 0})
    Me.nudUpdateInterval.Minimum = New Decimal(New Integer() {15, 0, 0, 0})
    Me.nudUpdateInterval.Name = "nudUpdateInterval"
    Me.nudUpdateInterval.Size = New System.Drawing.Size(66, 22)
    Me.nudUpdateInterval.TabIndex = 2
    Me.nudUpdateInterval.Value = New Decimal(New Integer() {15, 0, 0, 0})
    '
    'lblIntervalEvery
    '
    Me.lblIntervalEvery.AutoSize = True
    Me.lblIntervalEvery.Location = New System.Drawing.Point(8, 46)
    Me.lblIntervalEvery.Name = "lblIntervalEvery"
    Me.lblIntervalEvery.Size = New System.Drawing.Size(70, 13)
    Me.lblIntervalEvery.TabIndex = 1
    Me.lblIntervalEvery.Text = "Check every:"
    '
    'chkUpdateAtIntervals
    '
    Me.chkUpdateAtIntervals.AutoSize = True
    Me.chkUpdateAtIntervals.Location = New System.Drawing.Point(11, 21)
    Me.chkUpdateAtIntervals.Name = "chkUpdateAtIntervals"
    Me.chkUpdateAtIntervals.Size = New System.Drawing.Size(277, 17)
    Me.chkUpdateAtIntervals.TabIndex = 0
    Me.chkUpdateAtIntervals.Text = "Update weather information at periodic intervals"
    Me.chkUpdateAtIntervals.UseVisualStyleBackColor = True
    '
    'grpAppearance
    '
    Me.grpAppearance.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.grpAppearance.Controls.Add(Me.chkTextCondition)
    Me.grpAppearance.Controls.Add(Me.chkTextTemp)
    Me.grpAppearance.Controls.Add(Me.lblText)
    Me.grpAppearance.Controls.Add(Me.chkShowIcon)
    Me.grpAppearance.Location = New System.Drawing.Point(7, 235)
    Me.grpAppearance.Name = "grpAppearance"
    Me.grpAppearance.Size = New System.Drawing.Size(479, 72)
    Me.grpAppearance.TabIndex = 1
    Me.grpAppearance.TabStop = False
    Me.grpAppearance.Text = "Appearance"
    '
    'chkTextCondition
    '
    Me.chkTextCondition.AutoSize = True
    Me.chkTextCondition.Location = New System.Drawing.Point(140, 47)
    Me.chkTextCondition.Name = "chkTextCondition"
    Me.chkTextCondition.Size = New System.Drawing.Size(125, 17)
    Me.chkTextCondition.TabIndex = 4
    Me.chkTextCondition.Text = "Current Conditions"
    Me.chkTextCondition.UseVisualStyleBackColor = True
    '
    'chkTextTemp
    '
    Me.chkTextTemp.AutoSize = True
    Me.chkTextTemp.Location = New System.Drawing.Point(44, 47)
    Me.chkTextTemp.Name = "chkTextTemp"
    Me.chkTextTemp.Size = New System.Drawing.Size(90, 17)
    Me.chkTextTemp.TabIndex = 3
    Me.chkTextTemp.Text = "Temperature"
    Me.chkTextTemp.UseVisualStyleBackColor = True
    '
    'lblText
    '
    Me.lblText.AutoSize = True
    Me.lblText.Location = New System.Drawing.Point(8, 48)
    Me.lblText.Name = "lblText"
    Me.lblText.Size = New System.Drawing.Size(30, 13)
    Me.lblText.TabIndex = 2
    Me.lblText.Text = "Text:"
    '
    'chkShowIcon
    '
    Me.chkShowIcon.AutoSize = True
    Me.chkShowIcon.Location = New System.Drawing.Point(11, 21)
    Me.chkShowIcon.Name = "chkShowIcon"
    Me.chkShowIcon.Size = New System.Drawing.Size(80, 17)
    Me.chkShowIcon.TabIndex = 0
    Me.chkShowIcon.Text = "Show Icon"
    Me.chkShowIcon.UseVisualStyleBackColor = True
    '
    'grpLocation
    '
    Me.grpLocation.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.grpLocation.Controls.Add(Me.txtLocationID)
    Me.grpLocation.Controls.Add(Me.txtLocation)
    Me.grpLocation.Controls.Add(Me.btnLocationSearch)
    Me.grpLocation.Location = New System.Drawing.Point(7, 7)
    Me.grpLocation.Name = "grpLocation"
    Me.grpLocation.Size = New System.Drawing.Size(479, 85)
    Me.grpLocation.TabIndex = 0
    Me.grpLocation.TabStop = False
    Me.grpLocation.Text = "Location"
    '
    'txtLocationID
    '
    Me.txtLocationID.Location = New System.Drawing.Point(11, 51)
    Me.txtLocationID.Name = "txtLocationID"
    Me.txtLocationID.Size = New System.Drawing.Size(27, 22)
    Me.txtLocationID.TabIndex = 3
    Me.txtLocationID.Visible = False
    '
    'txtLocation
    '
    Me.txtLocation.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.txtLocation.BackColor = System.Drawing.SystemColors.Window
    Me.txtLocation.Location = New System.Drawing.Point(11, 22)
    Me.txtLocation.Name = "txtLocation"
    Me.txtLocation.ReadOnly = True
    Me.txtLocation.Size = New System.Drawing.Size(457, 22)
    Me.txtLocation.TabIndex = 2
    '
    'btnLocationSearch
    '
    Me.btnLocationSearch.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.btnLocationSearch.AutoSize = True
    Me.btnLocationSearch.Location = New System.Drawing.Point(361, 50)
    Me.btnLocationSearch.MinimumSize = New System.Drawing.Size(75, 25)
    Me.btnLocationSearch.Name = "btnLocationSearch"
    Me.btnLocationSearch.Size = New System.Drawing.Size(107, 25)
    Me.btnLocationSearch.TabIndex = 1
    Me.btnLocationSearch.Text = "&Location Search..."
    Me.btnLocationSearch.UseVisualStyleBackColor = True
    '
    'TabPage2
    '
    Me.TabPage2.Controls.Add(Me.lblTooltipInfo)
    Me.TabPage2.Controls.Add(Me.lstTooltipContents)
    Me.TabPage2.Location = New System.Drawing.Point(4, 22)
    Me.TabPage2.Name = "TabPage2"
    Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
    Me.TabPage2.Size = New System.Drawing.Size(492, 374)
    Me.TabPage2.TabIndex = 1
    Me.TabPage2.Text = "Tooltip"
    Me.TabPage2.UseVisualStyleBackColor = True
    '
    'lblTooltipInfo
    '
    Me.lblTooltipInfo.AutoSize = True
    Me.lblTooltipInfo.Location = New System.Drawing.Point(5, 10)
    Me.lblTooltipInfo.Name = "lblTooltipInfo"
    Me.lblTooltipInfo.Size = New System.Drawing.Size(296, 13)
    Me.lblTooltipInfo.TabIndex = 1
    Me.lblTooltipInfo.Text = "Check the items you would like on your weather tooltip."
    '
    'lstTooltipContents
    '
    Me.lstTooltipContents.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.lstTooltipContents.CheckOnClick = True
    Me.lstTooltipContents.IntegralHeight = False
    Me.lstTooltipContents.Items.AddRange(New Object() {"Location", "Last Updated", "Current Conditions Image", "Current Conditions", "Current Temperature", "Feels Like Temperature", "Humidity", "Visibility", "UV Index", "Dew Point", "Pressure", "Wind", "Sunrise and Sunset", "Moon Phase", "Five Day Forecast"})
    Me.lstTooltipContents.Location = New System.Drawing.Point(7, 32)
    Me.lstTooltipContents.Name = "lstTooltipContents"
    Me.lstTooltipContents.Size = New System.Drawing.Size(477, 336)
    Me.lstTooltipContents.TabIndex = 0
    '
    'TabPage3
    '
    Me.TabPage3.Controls.Add(Me.picImageSetPreview)
    Me.TabPage3.Controls.Add(Me.lvImageSet)
    Me.TabPage3.Location = New System.Drawing.Point(4, 22)
    Me.TabPage3.Name = "TabPage3"
    Me.TabPage3.Padding = New System.Windows.Forms.Padding(3)
    Me.TabPage3.Size = New System.Drawing.Size(492, 374)
    Me.TabPage3.TabIndex = 2
    Me.TabPage3.Text = "Image Set"
    Me.TabPage3.UseVisualStyleBackColor = True
    '
    'picImageSetPreview
    '
    Me.picImageSetPreview.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.picImageSetPreview.Location = New System.Drawing.Point(6, 6)
    Me.picImageSetPreview.Name = "picImageSetPreview"
    Me.picImageSetPreview.Size = New System.Drawing.Size(480, 48)
    Me.picImageSetPreview.TabIndex = 2
    Me.picImageSetPreview.TabStop = False
    '
    'lvImageSet
    '
    Me.lvImageSet.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.lvImageSet.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.colhdrName, Me.colhdrAuthor, Me.colhdrVersion})
    Me.lvImageSet.FullRowSelect = True
    Me.lvImageSet.GridLines = True
    Me.lvImageSet.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable
    Me.lvImageSet.HideSelection = False
    Me.lvImageSet.Location = New System.Drawing.Point(6, 60)
    Me.lvImageSet.MultiSelect = False
    Me.lvImageSet.Name = "lvImageSet"
    Me.lvImageSet.Size = New System.Drawing.Size(480, 308)
    Me.lvImageSet.TabIndex = 1
    Me.lvImageSet.UseCompatibleStateImageBehavior = False
    Me.lvImageSet.View = System.Windows.Forms.View.Details
    '
    'colhdrName
    '
    Me.colhdrName.Text = "Name"
    '
    'colhdrAuthor
    '
    Me.colhdrAuthor.Text = "Author"
    '
    'colhdrVersion
    '
    Me.colhdrVersion.Text = "Version"
    '
    'TabPage4
    '
    Me.TabPage4.Controls.Add(Me.llWeatherDotCom)
    Me.TabPage4.Controls.Add(Me.lblTWCi)
    Me.TabPage4.Controls.Add(Me.picTWCi)
    Me.TabPage4.Location = New System.Drawing.Point(4, 22)
    Me.TabPage4.Name = "TabPage4"
    Me.TabPage4.Padding = New System.Windows.Forms.Padding(3)
    Me.TabPage4.Size = New System.Drawing.Size(492, 374)
    Me.TabPage4.TabIndex = 3
    Me.TabPage4.Text = "About"
    Me.TabPage4.UseVisualStyleBackColor = True
    '
    'llWeatherDotCom
    '
    Me.llWeatherDotCom.AutoSize = True
    Me.llWeatherDotCom.LinkColor = System.Drawing.SystemColors.HotTrack
    Me.llWeatherDotCom.Location = New System.Drawing.Point(86, 51)
    Me.llWeatherDotCom.Name = "llWeatherDotCom"
    Me.llWeatherDotCom.Size = New System.Drawing.Size(136, 13)
    Me.llWeatherDotCom.TabIndex = 11
    Me.llWeatherDotCom.TabStop = True
    Me.llWeatherDotCom.Text = "http://www.weather.com"
    '
    'lblTWCi
    '
    Me.lblTWCi.Location = New System.Drawing.Point(86, 16)
    Me.lblTWCi.Name = "lblTWCi"
    Me.lblTWCi.Size = New System.Drawing.Size(230, 35)
    Me.lblTWCi.TabIndex = 10
    Me.lblTWCi.Text = "Weather data furnished by The Weather Channel, Inc."
    '
    'picTWCi
    '
    Me.picTWCi.Image = Global.WeatherConditions.My.Resources.Resources.TWClogo_64px
    Me.picTWCi.Location = New System.Drawing.Point(16, 16)
    Me.picTWCi.Name = "picTWCi"
    Me.picTWCi.Size = New System.Drawing.Size(64, 64)
    Me.picTWCi.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize
    Me.picTWCi.TabIndex = 9
    Me.picTWCi.TabStop = False
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
    Me.grpMeasurement.ResumeLayout(False)
    Me.grpMeasurement.PerformLayout()
    Me.grpUpdates.ResumeLayout(False)
    Me.grpUpdates.PerformLayout()
    CType(Me.nudUpdateInterval, System.ComponentModel.ISupportInitialize).EndInit()
    Me.grpAppearance.ResumeLayout(False)
    Me.grpAppearance.PerformLayout()
    Me.grpLocation.ResumeLayout(False)
    Me.grpLocation.PerformLayout()
    Me.TabPage2.ResumeLayout(False)
    Me.TabPage2.PerformLayout()
    Me.TabPage3.ResumeLayout(False)
    CType(Me.picImageSetPreview, System.ComponentModel.ISupportInitialize).EndInit()
    Me.TabPage4.ResumeLayout(False)
    Me.TabPage4.PerformLayout()
    CType(Me.picTWCi, System.ComponentModel.ISupportInitialize).EndInit()
    Me.ResumeLayout(False)

  End Sub
    Friend WithEvents Tabs As System.Windows.Forms.TabControl
    Friend WithEvents TabPage1 As System.Windows.Forms.TabPage
    Friend WithEvents TabPage2 As System.Windows.Forms.TabPage
    Friend WithEvents grpLocation As System.Windows.Forms.GroupBox
    Friend WithEvents btnLocationSearch As System.Windows.Forms.Button
    Friend WithEvents TabPage3 As System.Windows.Forms.TabPage
    Friend WithEvents txtLocation As System.Windows.Forms.TextBox
    Friend WithEvents txtLocationID As System.Windows.Forms.TextBox
    Friend WithEvents grpAppearance As System.Windows.Forms.GroupBox
    Friend WithEvents chkTextCondition As System.Windows.Forms.CheckBox
    Friend WithEvents chkTextTemp As System.Windows.Forms.CheckBox
    Friend WithEvents lblText As System.Windows.Forms.Label
    Friend WithEvents chkShowIcon As System.Windows.Forms.CheckBox
    Friend WithEvents grpUpdates As System.Windows.Forms.GroupBox
    Friend WithEvents nudUpdateInterval As System.Windows.Forms.NumericUpDown
    Friend WithEvents lblIntervalEvery As System.Windows.Forms.Label
    Friend WithEvents chkUpdateAtIntervals As System.Windows.Forms.CheckBox
    Friend WithEvents lblCheckMinutes As System.Windows.Forms.Label
    Friend WithEvents grpMeasurement As System.Windows.Forms.GroupBox
    Friend WithEvents radMeasurementMetric As System.Windows.Forms.RadioButton
    Friend WithEvents radMeasurementCustomary As System.Windows.Forms.RadioButton
    Friend WithEvents lblTooltipInfo As System.Windows.Forms.Label
    Friend WithEvents lstTooltipContents As System.Windows.Forms.CheckedListBox
    Friend WithEvents lvImageSet As System.Windows.Forms.ListView
    Friend WithEvents picImageSetPreview As System.Windows.Forms.PictureBox
    Friend WithEvents colhdrName As System.Windows.Forms.ColumnHeader
    Friend WithEvents colhdrAuthor As System.Windows.Forms.ColumnHeader
    Friend WithEvents colhdrVersion As System.Windows.Forms.ColumnHeader
    Friend WithEvents TabPage4 As System.Windows.Forms.TabPage
    Friend WithEvents llWeatherDotCom As System.Windows.Forms.LinkLabel
    Friend WithEvents lblTWCi As System.Windows.Forms.Label
    Friend WithEvents picTWCi As System.Windows.Forms.PictureBox
End Class
