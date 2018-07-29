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
    Me.Tabs = New System.Windows.Forms.TabControl
    Me.tabpageGeneral = New System.Windows.Forms.TabPage
    Me.grpTrackRatingButton = New System.Windows.Forms.GroupBox
    Me.chkShowTrackRatingButton = New System.Windows.Forms.CheckBox
    Me.grpButtonDisplay = New System.Windows.Forms.GroupBox
    Me.chkShowNowPlayingTooltip = New System.Windows.Forms.CheckBox
    Me.radButtonDisplayIconsAndText = New System.Windows.Forms.RadioButton
    Me.radButtonDisplayText = New System.Windows.Forms.RadioButton
    Me.radButtonDisplayIcons = New System.Windows.Forms.RadioButton
    Me.TabPage4 = New System.Windows.Forms.TabPage
    Me.nudWidth = New System.Windows.Forms.NumericUpDown
    Me.grpSongTimeInfo = New System.Windows.Forms.GroupBox
    Me.radSongTimeModeRemaining = New System.Windows.Forms.RadioButton
    Me.radSongTimeModeElapsed = New System.Windows.Forms.RadioButton
    Me.lblMediaPlayerSongTimeWidth = New System.Windows.Forms.Label
    Me.chkShowSongTimeInfo = New System.Windows.Forms.CheckBox
    Me.chkShowDuration = New System.Windows.Forms.CheckBox
    Me.TabPage5 = New System.Windows.Forms.TabPage
    Me.lblSongTitleTrackRatingOpacity = New System.Windows.Forms.Label
    Me.nudSongTitleTrackRatingOpacity = New System.Windows.Forms.NumericUpDown
    Me.chkSongTitleShowTrackRating = New System.Windows.Forms.CheckBox
    Me.nudSongTagWidth = New System.Windows.Forms.NumericUpDown
    Me.txtTagStringFormat = New System.Windows.Forms.TextBox
    Me.lblSongTagWidth = New System.Windows.Forms.Label
    Me.Label5 = New System.Windows.Forms.Label
    Me.chkShowSongTagInfo = New System.Windows.Forms.CheckBox
    Me.TabPage2 = New System.Windows.Forms.TabPage
    Me.grpTooltipTrackRating = New System.Windows.Forms.GroupBox
    Me.chkShowTrackRating = New System.Windows.Forms.CheckBox
    Me.grpTooltipAlbumArt = New System.Windows.Forms.GroupBox
    Me.txtAlbumArtFormattingString = New System.Windows.Forms.TextBox
    Me.Label1 = New System.Windows.Forms.Label
    Me.nudAlbumArtWidth = New System.Windows.Forms.NumericUpDown
    Me.Label2 = New System.Windows.Forms.Label
    Me.chkShowAlbumArt = New System.Windows.Forms.CheckBox
    Me.grpTooltipTextFormatString = New System.Windows.Forms.GroupBox
    Me.nudTooltipTextMaxWidth = New System.Windows.Forms.NumericUpDown
    Me.Label4 = New System.Windows.Forms.Label
    Me.txtTooltipFormatString = New System.Windows.Forms.TextBox
    Me.TabPage3 = New System.Windows.Forms.TabPage
    Me.GroupBox1 = New System.Windows.Forms.GroupBox
    Me.TextBox1 = New System.Windows.Forms.TextBox
    Me.Tabs.SuspendLayout()
    Me.tabpageGeneral.SuspendLayout()
    Me.grpTrackRatingButton.SuspendLayout()
    Me.grpButtonDisplay.SuspendLayout()
    Me.TabPage4.SuspendLayout()
    CType(Me.nudWidth, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.grpSongTimeInfo.SuspendLayout()
    Me.TabPage5.SuspendLayout()
    CType(Me.nudSongTitleTrackRatingOpacity, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.nudSongTagWidth, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.TabPage2.SuspendLayout()
    Me.grpTooltipTrackRating.SuspendLayout()
    Me.grpTooltipAlbumArt.SuspendLayout()
    CType(Me.nudAlbumArtWidth, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.grpTooltipTextFormatString.SuspendLayout()
    CType(Me.nudTooltipTextMaxWidth, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.TabPage3.SuspendLayout()
    Me.GroupBox1.SuspendLayout()
    Me.SuspendLayout()
    '
    'Tabs
    '
    Me.Tabs.Controls.Add(Me.tabpageGeneral)
    Me.Tabs.Controls.Add(Me.TabPage4)
    Me.Tabs.Controls.Add(Me.TabPage5)
    Me.Tabs.Controls.Add(Me.TabPage2)
    Me.Tabs.Controls.Add(Me.TabPage3)
    Me.Tabs.Dock = System.Windows.Forms.DockStyle.Fill
    Me.Tabs.Location = New System.Drawing.Point(0, 0)
    Me.Tabs.Name = "Tabs"
    Me.Tabs.SelectedIndex = 0
    Me.Tabs.Size = New System.Drawing.Size(500, 400)
    Me.Tabs.TabIndex = 8
    '
    'tabpageGeneral
    '
    Me.tabpageGeneral.Controls.Add(Me.grpTrackRatingButton)
    Me.tabpageGeneral.Controls.Add(Me.grpButtonDisplay)
    Me.tabpageGeneral.Location = New System.Drawing.Point(4, 22)
    Me.tabpageGeneral.Margin = New System.Windows.Forms.Padding(8)
    Me.tabpageGeneral.Name = "tabpageGeneral"
    Me.tabpageGeneral.Padding = New System.Windows.Forms.Padding(8)
    Me.tabpageGeneral.Size = New System.Drawing.Size(492, 374)
    Me.tabpageGeneral.TabIndex = 0
    Me.tabpageGeneral.Text = "General"
    Me.tabpageGeneral.UseVisualStyleBackColor = True
    '
    'grpTrackRatingButton
    '
    Me.grpTrackRatingButton.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.grpTrackRatingButton.Controls.Add(Me.chkShowTrackRatingButton)
    Me.grpTrackRatingButton.Location = New System.Drawing.Point(11, 89)
    Me.grpTrackRatingButton.Name = "grpTrackRatingButton"
    Me.grpTrackRatingButton.Size = New System.Drawing.Size(472, 48)
    Me.grpTrackRatingButton.TabIndex = 11
    Me.grpTrackRatingButton.TabStop = False
    Me.grpTrackRatingButton.Text = "Track Rating"
    '
    'chkShowTrackRatingButton
    '
    Me.chkShowTrackRatingButton.AutoSize = True
    Me.chkShowTrackRatingButton.Location = New System.Drawing.Point(12, 22)
    Me.chkShowTrackRatingButton.Name = "chkShowTrackRatingButton"
    Me.chkShowTrackRatingButton.Size = New System.Drawing.Size(160, 17)
    Me.chkShowTrackRatingButton.TabIndex = 0
    Me.chkShowTrackRatingButton.Text = "Show Track Rating Button"
    Me.chkShowTrackRatingButton.UseVisualStyleBackColor = True
    '
    'grpButtonDisplay
    '
    Me.grpButtonDisplay.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.grpButtonDisplay.Controls.Add(Me.chkShowNowPlayingTooltip)
    Me.grpButtonDisplay.Controls.Add(Me.radButtonDisplayIconsAndText)
    Me.grpButtonDisplay.Controls.Add(Me.radButtonDisplayText)
    Me.grpButtonDisplay.Controls.Add(Me.radButtonDisplayIcons)
    Me.grpButtonDisplay.Location = New System.Drawing.Point(11, 11)
    Me.grpButtonDisplay.Name = "grpButtonDisplay"
    Me.grpButtonDisplay.Size = New System.Drawing.Size(472, 71)
    Me.grpButtonDisplay.TabIndex = 9
    Me.grpButtonDisplay.TabStop = False
    Me.grpButtonDisplay.Text = "Playback Button Display"
    '
    'chkShowNowPlayingTooltip
    '
    Me.chkShowNowPlayingTooltip.AutoSize = True
    Me.chkShowNowPlayingTooltip.Location = New System.Drawing.Point(12, 45)
    Me.chkShowNowPlayingTooltip.Name = "chkShowNowPlayingTooltip"
    Me.chkShowNowPlayingTooltip.Size = New System.Drawing.Size(274, 17)
    Me.chkShowNowPlayingTooltip.TabIndex = 3
    Me.chkShowNowPlayingTooltip.Text = "Use ""Now Playing"" tooltip when music is playing"
    Me.chkShowNowPlayingTooltip.UseVisualStyleBackColor = True
    '
    'radButtonDisplayIconsAndText
    '
    Me.radButtonDisplayIconsAndText.AutoSize = True
    Me.radButtonDisplayIconsAndText.Location = New System.Drawing.Point(121, 21)
    Me.radButtonDisplayIconsAndText.Name = "radButtonDisplayIconsAndText"
    Me.radButtonDisplayIconsAndText.Size = New System.Drawing.Size(98, 17)
    Me.radButtonDisplayIconsAndText.TabIndex = 2
    Me.radButtonDisplayIconsAndText.Text = "Icons and Text"
    Me.radButtonDisplayIconsAndText.UseVisualStyleBackColor = True
    '
    'radButtonDisplayText
    '
    Me.radButtonDisplayText.AutoSize = True
    Me.radButtonDisplayText.Location = New System.Drawing.Point(70, 21)
    Me.radButtonDisplayText.Name = "radButtonDisplayText"
    Me.radButtonDisplayText.Size = New System.Drawing.Size(45, 17)
    Me.radButtonDisplayText.TabIndex = 1
    Me.radButtonDisplayText.Text = "Text"
    Me.radButtonDisplayText.UseVisualStyleBackColor = True
    '
    'radButtonDisplayIcons
    '
    Me.radButtonDisplayIcons.AutoSize = True
    Me.radButtonDisplayIcons.Checked = True
    Me.radButtonDisplayIcons.Location = New System.Drawing.Point(12, 21)
    Me.radButtonDisplayIcons.Name = "radButtonDisplayIcons"
    Me.radButtonDisplayIcons.Size = New System.Drawing.Size(52, 17)
    Me.radButtonDisplayIcons.TabIndex = 0
    Me.radButtonDisplayIcons.TabStop = True
    Me.radButtonDisplayIcons.Text = "Icons"
    Me.radButtonDisplayIcons.UseVisualStyleBackColor = True
    '
    'TabPage4
    '
    Me.TabPage4.Controls.Add(Me.nudWidth)
    Me.TabPage4.Controls.Add(Me.grpSongTimeInfo)
    Me.TabPage4.Controls.Add(Me.lblMediaPlayerSongTimeWidth)
    Me.TabPage4.Controls.Add(Me.chkShowSongTimeInfo)
    Me.TabPage4.Controls.Add(Me.chkShowDuration)
    Me.TabPage4.Location = New System.Drawing.Point(4, 22)
    Me.TabPage4.Name = "TabPage4"
    Me.TabPage4.Padding = New System.Windows.Forms.Padding(8)
    Me.TabPage4.Size = New System.Drawing.Size(492, 374)
    Me.TabPage4.TabIndex = 3
    Me.TabPage4.Text = "Song Time Display"
    Me.TabPage4.UseVisualStyleBackColor = True
    '
    'nudWidth
    '
    Me.nudWidth.Location = New System.Drawing.Point(136, 110)
    Me.nudWidth.Maximum = New Decimal(New Integer() {500, 0, 0, 0})
    Me.nudWidth.Minimum = New Decimal(New Integer() {25, 0, 0, 0})
    Me.nudWidth.Name = "nudWidth"
    Me.nudWidth.Size = New System.Drawing.Size(63, 22)
    Me.nudWidth.TabIndex = 6
    Me.nudWidth.Value = New Decimal(New Integer() {25, 0, 0, 0})
    '
    'grpSongTimeInfo
    '
    Me.grpSongTimeInfo.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.grpSongTimeInfo.Controls.Add(Me.radSongTimeModeRemaining)
    Me.grpSongTimeInfo.Controls.Add(Me.radSongTimeModeElapsed)
    Me.grpSongTimeInfo.Location = New System.Drawing.Point(11, 57)
    Me.grpSongTimeInfo.Name = "grpSongTimeInfo"
    Me.grpSongTimeInfo.Padding = New System.Windows.Forms.Padding(8)
    Me.grpSongTimeInfo.Size = New System.Drawing.Size(470, 45)
    Me.grpSongTimeInfo.TabIndex = 8
    Me.grpSongTimeInfo.TabStop = False
    Me.grpSongTimeInfo.Text = "Display Mode"
    '
    'radSongTimeModeRemaining
    '
    Me.radSongTimeModeRemaining.AutoSize = True
    Me.radSongTimeModeRemaining.Location = New System.Drawing.Point(82, 17)
    Me.radSongTimeModeRemaining.Name = "radSongTimeModeRemaining"
    Me.radSongTimeModeRemaining.Size = New System.Drawing.Size(80, 17)
    Me.radSongTimeModeRemaining.TabIndex = 2
    Me.radSongTimeModeRemaining.Text = "Remaining"
    Me.radSongTimeModeRemaining.UseVisualStyleBackColor = True
    '
    'radSongTimeModeElapsed
    '
    Me.radSongTimeModeElapsed.AutoSize = True
    Me.radSongTimeModeElapsed.Checked = True
    Me.radSongTimeModeElapsed.Location = New System.Drawing.Point(11, 17)
    Me.radSongTimeModeElapsed.Name = "radSongTimeModeElapsed"
    Me.radSongTimeModeElapsed.Size = New System.Drawing.Size(65, 17)
    Me.radSongTimeModeElapsed.TabIndex = 1
    Me.radSongTimeModeElapsed.TabStop = True
    Me.radSongTimeModeElapsed.Text = "Elapsed"
    Me.radSongTimeModeElapsed.UseVisualStyleBackColor = True
    '
    'lblMediaPlayerSongTimeWidth
    '
    Me.lblMediaPlayerSongTimeWidth.AutoSize = True
    Me.lblMediaPlayerSongTimeWidth.Location = New System.Drawing.Point(8, 112)
    Me.lblMediaPlayerSongTimeWidth.Name = "lblMediaPlayerSongTimeWidth"
    Me.lblMediaPlayerSongTimeWidth.Size = New System.Drawing.Size(125, 13)
    Me.lblMediaPlayerSongTimeWidth.TabIndex = 4
    Me.lblMediaPlayerSongTimeWidth.Text = "Text Box Width (pixels):"
    '
    'chkShowSongTimeInfo
    '
    Me.chkShowSongTimeInfo.AutoSize = True
    Me.chkShowSongTimeInfo.Location = New System.Drawing.Point(11, 11)
    Me.chkShowSongTimeInfo.Name = "chkShowSongTimeInfo"
    Me.chkShowSongTimeInfo.Size = New System.Drawing.Size(175, 17)
    Me.chkShowSongTimeInfo.TabIndex = 0
    Me.chkShowSongTimeInfo.Text = "Show Song Time Information"
    Me.chkShowSongTimeInfo.UseVisualStyleBackColor = True
    '
    'chkShowDuration
    '
    Me.chkShowDuration.AutoSize = True
    Me.chkShowDuration.Location = New System.Drawing.Point(11, 34)
    Me.chkShowDuration.Name = "chkShowDuration"
    Me.chkShowDuration.Size = New System.Drawing.Size(133, 17)
    Me.chkShowDuration.TabIndex = 3
    Me.chkShowDuration.Text = "Show Track Duration"
    Me.chkShowDuration.UseVisualStyleBackColor = True
    '
    'TabPage5
    '
    Me.TabPage5.Controls.Add(Me.lblSongTitleTrackRatingOpacity)
    Me.TabPage5.Controls.Add(Me.nudSongTitleTrackRatingOpacity)
    Me.TabPage5.Controls.Add(Me.chkSongTitleShowTrackRating)
    Me.TabPage5.Controls.Add(Me.nudSongTagWidth)
    Me.TabPage5.Controls.Add(Me.txtTagStringFormat)
    Me.TabPage5.Controls.Add(Me.lblSongTagWidth)
    Me.TabPage5.Controls.Add(Me.Label5)
    Me.TabPage5.Controls.Add(Me.chkShowSongTagInfo)
    Me.TabPage5.Location = New System.Drawing.Point(4, 22)
    Me.TabPage5.Name = "TabPage5"
    Me.TabPage5.Padding = New System.Windows.Forms.Padding(8)
    Me.TabPage5.Size = New System.Drawing.Size(492, 374)
    Me.TabPage5.TabIndex = 4
    Me.TabPage5.Text = "Song Title Display"
    Me.TabPage5.UseVisualStyleBackColor = True
    '
    'lblSongTitleTrackRatingOpacity
    '
    Me.lblSongTitleTrackRatingOpacity.AutoSize = True
    Me.lblSongTitleTrackRatingOpacity.Location = New System.Drawing.Point(32, 167)
    Me.lblSongTitleTrackRatingOpacity.Name = "lblSongTitleTrackRatingOpacity"
    Me.lblSongTitleTrackRatingOpacity.Size = New System.Drawing.Size(49, 13)
    Me.lblSongTitleTrackRatingOpacity.TabIndex = 8
    Me.lblSongTitleTrackRatingOpacity.Text = "Opacity:"
    '
    'nudSongTitleTrackRatingOpacity
    '
    Me.nudSongTitleTrackRatingOpacity.Location = New System.Drawing.Point(87, 165)
    Me.nudSongTitleTrackRatingOpacity.Maximum = New Decimal(New Integer() {255, 0, 0, 0})
    Me.nudSongTitleTrackRatingOpacity.Name = "nudSongTitleTrackRatingOpacity"
    Me.nudSongTitleTrackRatingOpacity.Size = New System.Drawing.Size(49, 22)
    Me.nudSongTitleTrackRatingOpacity.TabIndex = 7
    '
    'chkSongTitleShowTrackRating
    '
    Me.chkSongTitleShowTrackRating.AutoSize = True
    Me.chkSongTitleShowTrackRating.Location = New System.Drawing.Point(14, 142)
    Me.chkSongTitleShowTrackRating.Name = "chkSongTitleShowTrackRating"
    Me.chkSongTitleShowTrackRating.Size = New System.Drawing.Size(210, 17)
    Me.chkSongTitleShowTrackRating.TabIndex = 6
    Me.chkSongTitleShowTrackRating.Text = "Show Track Rating in Song Title Box"
    Me.chkSongTitleShowTrackRating.UseVisualStyleBackColor = True
    '
    'nudSongTagWidth
    '
    Me.nudSongTagWidth.Location = New System.Drawing.Point(139, 104)
    Me.nudSongTagWidth.Maximum = New Decimal(New Integer() {2000, 0, 0, 0})
    Me.nudSongTagWidth.Minimum = New Decimal(New Integer() {50, 0, 0, 0})
    Me.nudSongTagWidth.Name = "nudSongTagWidth"
    Me.nudSongTagWidth.Size = New System.Drawing.Size(63, 22)
    Me.nudSongTagWidth.TabIndex = 2
    Me.nudSongTagWidth.Value = New Decimal(New Integer() {50, 0, 0, 0})
    '
    'txtTagStringFormat
    '
    Me.txtTagStringFormat.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.txtTagStringFormat.Location = New System.Drawing.Point(11, 64)
    Me.txtTagStringFormat.Name = "txtTagStringFormat"
    Me.txtTagStringFormat.Size = New System.Drawing.Size(470, 22)
    Me.txtTagStringFormat.TabIndex = 3
    '
    'lblSongTagWidth
    '
    Me.lblSongTagWidth.AutoSize = True
    Me.lblSongTagWidth.Location = New System.Drawing.Point(11, 106)
    Me.lblSongTagWidth.Name = "lblSongTagWidth"
    Me.lblSongTagWidth.Size = New System.Drawing.Size(125, 13)
    Me.lblSongTagWidth.TabIndex = 1
    Me.lblSongTagWidth.Text = "Text Box Width (pixels):"
    '
    'Label5
    '
    Me.Label5.AutoSize = True
    Me.Label5.Location = New System.Drawing.Point(11, 48)
    Me.Label5.Name = "Label5"
    Me.Label5.Size = New System.Drawing.Size(141, 13)
    Me.Label5.TabIndex = 5
    Me.Label5.Text = "Display Formatting String:"
    '
    'chkShowSongTagInfo
    '
    Me.chkShowSongTagInfo.AutoSize = True
    Me.chkShowSongTagInfo.Location = New System.Drawing.Point(11, 11)
    Me.chkShowSongTagInfo.Name = "chkShowSongTagInfo"
    Me.chkShowSongTagInfo.Size = New System.Drawing.Size(173, 17)
    Me.chkShowSongTagInfo.TabIndex = 0
    Me.chkShowSongTagInfo.Text = "Show Song Title Information"
    Me.chkShowSongTagInfo.UseVisualStyleBackColor = True
    '
    'TabPage2
    '
    Me.TabPage2.Controls.Add(Me.grpTooltipTrackRating)
    Me.TabPage2.Controls.Add(Me.grpTooltipAlbumArt)
    Me.TabPage2.Controls.Add(Me.grpTooltipTextFormatString)
    Me.TabPage2.Location = New System.Drawing.Point(4, 22)
    Me.TabPage2.Name = "TabPage2"
    Me.TabPage2.Padding = New System.Windows.Forms.Padding(8)
    Me.TabPage2.Size = New System.Drawing.Size(492, 374)
    Me.TabPage2.TabIndex = 1
    Me.TabPage2.Text = "Tooltip"
    Me.TabPage2.UseVisualStyleBackColor = True
    '
    'grpTooltipTrackRating
    '
    Me.grpTooltipTrackRating.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.grpTooltipTrackRating.Controls.Add(Me.chkShowTrackRating)
    Me.grpTooltipTrackRating.Location = New System.Drawing.Point(8, 308)
    Me.grpTooltipTrackRating.Name = "grpTooltipTrackRating"
    Me.grpTooltipTrackRating.Size = New System.Drawing.Size(473, 47)
    Me.grpTooltipTrackRating.TabIndex = 1
    Me.grpTooltipTrackRating.TabStop = False
    Me.grpTooltipTrackRating.Text = "Track Rating"
    '
    'chkShowTrackRating
    '
    Me.chkShowTrackRating.AutoSize = True
    Me.chkShowTrackRating.Location = New System.Drawing.Point(11, 20)
    Me.chkShowTrackRating.Name = "chkShowTrackRating"
    Me.chkShowTrackRating.Size = New System.Drawing.Size(121, 17)
    Me.chkShowTrackRating.TabIndex = 0
    Me.chkShowTrackRating.Text = "Show Track Rating"
    Me.chkShowTrackRating.UseVisualStyleBackColor = True
    '
    'grpTooltipAlbumArt
    '
    Me.grpTooltipAlbumArt.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.grpTooltipAlbumArt.Controls.Add(Me.txtAlbumArtFormattingString)
    Me.grpTooltipAlbumArt.Controls.Add(Me.Label1)
    Me.grpTooltipAlbumArt.Controls.Add(Me.nudAlbumArtWidth)
    Me.grpTooltipAlbumArt.Controls.Add(Me.Label2)
    Me.grpTooltipAlbumArt.Controls.Add(Me.chkShowAlbumArt)
    Me.grpTooltipAlbumArt.Location = New System.Drawing.Point(8, 12)
    Me.grpTooltipAlbumArt.Name = "grpTooltipAlbumArt"
    Me.grpTooltipAlbumArt.Size = New System.Drawing.Size(473, 120)
    Me.grpTooltipAlbumArt.TabIndex = 0
    Me.grpTooltipAlbumArt.TabStop = False
    Me.grpTooltipAlbumArt.Text = "Album Art"
    '
    'txtAlbumArtFormattingString
    '
    Me.txtAlbumArtFormattingString.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.txtAlbumArtFormattingString.Location = New System.Drawing.Point(9, 62)
    Me.txtAlbumArtFormattingString.Name = "txtAlbumArtFormattingString"
    Me.txtAlbumArtFormattingString.Size = New System.Drawing.Size(458, 22)
    Me.txtAlbumArtFormattingString.TabIndex = 10
    '
    'Label1
    '
    Me.Label1.AutoSize = True
    Me.Label1.Location = New System.Drawing.Point(6, 46)
    Me.Label1.Name = "Label1"
    Me.Label1.Size = New System.Drawing.Size(122, 13)
    Me.Label1.TabIndex = 9
    Me.Label1.Text = "File Formatting String:"
    '
    'nudAlbumArtWidth
    '
    Me.nudAlbumArtWidth.Location = New System.Drawing.Point(146, 87)
    Me.nudAlbumArtWidth.Maximum = New Decimal(New Integer() {1000, 0, 0, 0})
    Me.nudAlbumArtWidth.Minimum = New Decimal(New Integer() {25, 0, 0, 0})
    Me.nudAlbumArtWidth.Name = "nudAlbumArtWidth"
    Me.nudAlbumArtWidth.Size = New System.Drawing.Size(63, 22)
    Me.nudAlbumArtWidth.TabIndex = 8
    Me.nudAlbumArtWidth.Value = New Decimal(New Integer() {100, 0, 0, 0})
    '
    'Label2
    '
    Me.Label2.AutoSize = True
    Me.Label2.Location = New System.Drawing.Point(6, 89)
    Me.Label2.Name = "Label2"
    Me.Label2.Size = New System.Drawing.Size(134, 13)
    Me.Label2.TabIndex = 7
    Me.Label2.Text = "Album Art Width (pixels):"
    '
    'chkShowAlbumArt
    '
    Me.chkShowAlbumArt.AutoSize = True
    Me.chkShowAlbumArt.Location = New System.Drawing.Point(9, 21)
    Me.chkShowAlbumArt.Name = "chkShowAlbumArt"
    Me.chkShowAlbumArt.Size = New System.Drawing.Size(109, 17)
    Me.chkShowAlbumArt.TabIndex = 0
    Me.chkShowAlbumArt.Text = "Show Album Art"
    Me.chkShowAlbumArt.UseVisualStyleBackColor = True
    '
    'grpTooltipTextFormatString
    '
    Me.grpTooltipTextFormatString.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.grpTooltipTextFormatString.Controls.Add(Me.nudTooltipTextMaxWidth)
    Me.grpTooltipTextFormatString.Controls.Add(Me.Label4)
    Me.grpTooltipTextFormatString.Controls.Add(Me.txtTooltipFormatString)
    Me.grpTooltipTextFormatString.Location = New System.Drawing.Point(8, 138)
    Me.grpTooltipTextFormatString.Name = "grpTooltipTextFormatString"
    Me.grpTooltipTextFormatString.Size = New System.Drawing.Size(473, 164)
    Me.grpTooltipTextFormatString.TabIndex = 2
    Me.grpTooltipTextFormatString.TabStop = False
    Me.grpTooltipTextFormatString.Text = "Text Format String"
    '
    'nudTooltipTextMaxWidth
    '
    Me.nudTooltipTextMaxWidth.Location = New System.Drawing.Point(135, 129)
    Me.nudTooltipTextMaxWidth.Maximum = New Decimal(New Integer() {5000, 0, 0, 0})
    Me.nudTooltipTextMaxWidth.Minimum = New Decimal(New Integer() {10, 0, 0, 0})
    Me.nudTooltipTextMaxWidth.Name = "nudTooltipTextMaxWidth"
    Me.nudTooltipTextMaxWidth.Size = New System.Drawing.Size(60, 22)
    Me.nudTooltipTextMaxWidth.TabIndex = 5
    Me.nudTooltipTextMaxWidth.Value = New Decimal(New Integer() {400, 0, 0, 0})
    '
    'Label4
    '
    Me.Label4.AutoSize = True
    Me.Label4.Location = New System.Drawing.Point(12, 131)
    Me.Label4.Name = "Label4"
    Me.Label4.Size = New System.Drawing.Size(117, 13)
    Me.Label4.TabIndex = 4
    Me.Label4.Text = "Maximum Text Width:"
    '
    'txtTooltipFormatString
    '
    Me.txtTooltipFormatString.AcceptsReturn = True
    Me.txtTooltipFormatString.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.txtTooltipFormatString.Location = New System.Drawing.Point(8, 22)
    Me.txtTooltipFormatString.Multiline = True
    Me.txtTooltipFormatString.Name = "txtTooltipFormatString"
    Me.txtTooltipFormatString.ScrollBars = System.Windows.Forms.ScrollBars.Both
    Me.txtTooltipFormatString.Size = New System.Drawing.Size(459, 101)
    Me.txtTooltipFormatString.TabIndex = 0
    '
    'TabPage3
    '
    Me.TabPage3.Controls.Add(Me.GroupBox1)
    Me.TabPage3.Location = New System.Drawing.Point(4, 22)
    Me.TabPage3.Name = "TabPage3"
    Me.TabPage3.Padding = New System.Windows.Forms.Padding(8)
    Me.TabPage3.Size = New System.Drawing.Size(492, 374)
    Me.TabPage3.TabIndex = 2
    Me.TabPage3.Text = "Help"
    Me.TabPage3.UseVisualStyleBackColor = True
    '
    'GroupBox1
    '
    Me.GroupBox1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.GroupBox1.Controls.Add(Me.TextBox1)
    Me.GroupBox1.Location = New System.Drawing.Point(11, 11)
    Me.GroupBox1.Margin = New System.Windows.Forms.Padding(0)
    Me.GroupBox1.Name = "GroupBox1"
    Me.GroupBox1.Padding = New System.Windows.Forms.Padding(8, 4, 8, 8)
    Me.GroupBox1.Size = New System.Drawing.Size(470, 352)
    Me.GroupBox1.TabIndex = 0
    Me.GroupBox1.TabStop = False
    Me.GroupBox1.Text = "Song Title and Tooltip Text Formatting"
    '
    'TextBox1
    '
    Me.TextBox1.Dock = System.Windows.Forms.DockStyle.Fill
    Me.TextBox1.Location = New System.Drawing.Point(8, 19)
    Me.TextBox1.Multiline = True
    Me.TextBox1.Name = "TextBox1"
    Me.TextBox1.Size = New System.Drawing.Size(454, 325)
    Me.TextBox1.TabIndex = 0
    Me.TextBox1.Text = resources.GetString("TextBox1.Text")
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
    Me.grpTrackRatingButton.ResumeLayout(False)
    Me.grpTrackRatingButton.PerformLayout()
    Me.grpButtonDisplay.ResumeLayout(False)
    Me.grpButtonDisplay.PerformLayout()
    Me.TabPage4.ResumeLayout(False)
    Me.TabPage4.PerformLayout()
    CType(Me.nudWidth, System.ComponentModel.ISupportInitialize).EndInit()
    Me.grpSongTimeInfo.ResumeLayout(False)
    Me.grpSongTimeInfo.PerformLayout()
    Me.TabPage5.ResumeLayout(False)
    Me.TabPage5.PerformLayout()
    CType(Me.nudSongTitleTrackRatingOpacity, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.nudSongTagWidth, System.ComponentModel.ISupportInitialize).EndInit()
    Me.TabPage2.ResumeLayout(False)
    Me.grpTooltipTrackRating.ResumeLayout(False)
    Me.grpTooltipTrackRating.PerformLayout()
    Me.grpTooltipAlbumArt.ResumeLayout(False)
    Me.grpTooltipAlbumArt.PerformLayout()
    CType(Me.nudAlbumArtWidth, System.ComponentModel.ISupportInitialize).EndInit()
    Me.grpTooltipTextFormatString.ResumeLayout(False)
    Me.grpTooltipTextFormatString.PerformLayout()
    CType(Me.nudTooltipTextMaxWidth, System.ComponentModel.ISupportInitialize).EndInit()
    Me.TabPage3.ResumeLayout(False)
    Me.GroupBox1.ResumeLayout(False)
    Me.GroupBox1.PerformLayout()
    Me.ResumeLayout(False)

  End Sub

    Friend WithEvents Tabs As System.Windows.Forms.TabControl
    Friend WithEvents tabpageGeneral As System.Windows.Forms.TabPage
    Friend WithEvents TabPage2 As System.Windows.Forms.TabPage
    Friend WithEvents grpButtonDisplay As System.Windows.Forms.GroupBox
    Friend WithEvents radButtonDisplayIconsAndText As System.Windows.Forms.RadioButton
    Friend WithEvents radButtonDisplayText As System.Windows.Forms.RadioButton
    Friend WithEvents radButtonDisplayIcons As System.Windows.Forms.RadioButton
    Friend WithEvents grpTooltipAlbumArt As System.Windows.Forms.GroupBox
    Friend WithEvents chkShowAlbumArt As System.Windows.Forms.CheckBox
    Friend WithEvents nudAlbumArtWidth As System.Windows.Forms.NumericUpDown
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents grpTooltipTrackRating As System.Windows.Forms.GroupBox
    Friend WithEvents chkShowTrackRating As System.Windows.Forms.CheckBox
    Friend WithEvents grpTooltipTextFormatString As System.Windows.Forms.GroupBox
    Friend WithEvents txtTooltipFormatString As System.Windows.Forms.TextBox
    Friend WithEvents nudTooltipTextMaxWidth As System.Windows.Forms.NumericUpDown
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents chkShowNowPlayingTooltip As System.Windows.Forms.CheckBox
    Friend WithEvents TabPage3 As System.Windows.Forms.TabPage
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents TabPage4 As System.Windows.Forms.TabPage
    Friend WithEvents TabPage5 As System.Windows.Forms.TabPage
    Friend WithEvents nudWidth As System.Windows.Forms.NumericUpDown
    Friend WithEvents grpSongTimeInfo As System.Windows.Forms.GroupBox
    Friend WithEvents radSongTimeModeRemaining As System.Windows.Forms.RadioButton
    Friend WithEvents radSongTimeModeElapsed As System.Windows.Forms.RadioButton
    Friend WithEvents lblMediaPlayerSongTimeWidth As System.Windows.Forms.Label
    Friend WithEvents chkShowSongTimeInfo As System.Windows.Forms.CheckBox
    Friend WithEvents chkShowDuration As System.Windows.Forms.CheckBox
    Friend WithEvents nudSongTagWidth As System.Windows.Forms.NumericUpDown
    Friend WithEvents txtTagStringFormat As System.Windows.Forms.TextBox
    Friend WithEvents lblSongTagWidth As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents chkShowSongTagInfo As System.Windows.Forms.CheckBox
    Friend WithEvents txtAlbumArtFormattingString As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents grpTrackRatingButton As System.Windows.Forms.GroupBox
    Friend WithEvents chkShowTrackRatingButton As System.Windows.Forms.CheckBox
    Friend WithEvents chkSongTitleShowTrackRating As System.Windows.Forms.CheckBox
    Friend WithEvents nudSongTitleTrackRatingOpacity As System.Windows.Forms.NumericUpDown
    Friend WithEvents lblSongTitleTrackRatingOpacity As System.Windows.Forms.Label
    Friend WithEvents TextBox1 As System.Windows.Forms.TextBox
End Class
