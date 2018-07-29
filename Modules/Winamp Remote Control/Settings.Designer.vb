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
    Me.TabPage1 = New System.Windows.Forms.TabPage
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
    Me.nudSongTitleTrackRatingOpacity = New System.Windows.Forms.NumericUpDown
    Me.lblSongTitleTrackRatingOpacity = New System.Windows.Forms.Label
    Me.chkSongTitleShowTrackRating = New System.Windows.Forms.CheckBox
    Me.grpSongTitleFormat = New System.Windows.Forms.GroupBox
    Me.txtSongTitleFormat = New System.Windows.Forms.TextBox
    Me.nudSongTagWidth = New System.Windows.Forms.NumericUpDown
    Me.lblSongTagWidth = New System.Windows.Forms.Label
    Me.chkShowSongTagInfo = New System.Windows.Forms.CheckBox
    Me.TabPage2 = New System.Windows.Forms.TabPage
    Me.grpText = New System.Windows.Forms.GroupBox
    Me.txtTooltipText = New System.Windows.Forms.TextBox
    Me.nudTooltipTextMaxWidth = New System.Windows.Forms.NumericUpDown
    Me.Label4 = New System.Windows.Forms.Label
    Me.grpTooltipTrackRating = New System.Windows.Forms.GroupBox
    Me.chkShowTrackRating = New System.Windows.Forms.CheckBox
    Me.grpTooltipAlbumArt = New System.Windows.Forms.GroupBox
    Me.txtAlbumArtFormattingString = New System.Windows.Forms.TextBox
    Me.Label1 = New System.Windows.Forms.Label
    Me.nudAlbumArtWidth = New System.Windows.Forms.NumericUpDown
    Me.Label2 = New System.Windows.Forms.Label
    Me.chkShowAlbumArt = New System.Windows.Forms.CheckBox
    Me.tabpageHelp = New System.Windows.Forms.TabPage
    Me.txtHelp = New System.Windows.Forms.TextBox
    Me.lblHelp = New System.Windows.Forms.Label
    Me.Tabs.SuspendLayout()
    Me.TabPage1.SuspendLayout()
    Me.grpTrackRatingButton.SuspendLayout()
    Me.grpButtonDisplay.SuspendLayout()
    Me.TabPage4.SuspendLayout()
    CType(Me.nudWidth, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.grpSongTimeInfo.SuspendLayout()
    Me.TabPage5.SuspendLayout()
    CType(Me.nudSongTitleTrackRatingOpacity, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.grpSongTitleFormat.SuspendLayout()
    CType(Me.nudSongTagWidth, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.TabPage2.SuspendLayout()
    Me.grpText.SuspendLayout()
    CType(Me.nudTooltipTextMaxWidth, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.grpTooltipTrackRating.SuspendLayout()
    Me.grpTooltipAlbumArt.SuspendLayout()
    CType(Me.nudAlbumArtWidth, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.tabpageHelp.SuspendLayout()
    Me.SuspendLayout()
    '
    'Tabs
    '
    Me.Tabs.Controls.Add(Me.TabPage1)
    Me.Tabs.Controls.Add(Me.TabPage4)
    Me.Tabs.Controls.Add(Me.TabPage5)
    Me.Tabs.Controls.Add(Me.TabPage2)
    Me.Tabs.Controls.Add(Me.tabpageHelp)
    Me.Tabs.Dock = System.Windows.Forms.DockStyle.Fill
    Me.Tabs.Location = New System.Drawing.Point(0, 0)
    Me.Tabs.Name = "Tabs"
    Me.Tabs.SelectedIndex = 0
    Me.Tabs.Size = New System.Drawing.Size(500, 400)
    Me.Tabs.TabIndex = 8
    '
    'TabPage1
    '
    Me.TabPage1.Controls.Add(Me.grpTrackRatingButton)
    Me.TabPage1.Controls.Add(Me.grpButtonDisplay)
    Me.TabPage1.Location = New System.Drawing.Point(4, 22)
    Me.TabPage1.Name = "TabPage1"
    Me.TabPage1.Padding = New System.Windows.Forms.Padding(3, 3, 6, 6)
    Me.TabPage1.Size = New System.Drawing.Size(492, 374)
    Me.TabPage1.TabIndex = 0
    Me.TabPage1.Text = "General"
    Me.TabPage1.UseVisualStyleBackColor = True
    '
    'grpTrackRatingButton
    '
    Me.grpTrackRatingButton.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.grpTrackRatingButton.Controls.Add(Me.chkShowTrackRatingButton)
    Me.grpTrackRatingButton.Location = New System.Drawing.Point(9, 85)
    Me.grpTrackRatingButton.Name = "grpTrackRatingButton"
    Me.grpTrackRatingButton.Size = New System.Drawing.Size(474, 48)
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
    Me.grpButtonDisplay.Location = New System.Drawing.Point(9, 7)
    Me.grpButtonDisplay.Name = "grpButtonDisplay"
    Me.grpButtonDisplay.Size = New System.Drawing.Size(474, 71)
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
    Me.nudWidth.Location = New System.Drawing.Point(136, 108)
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
    Me.grpSongTimeInfo.Size = New System.Drawing.Size(470, 41)
    Me.grpSongTimeInfo.TabIndex = 8
    Me.grpSongTimeInfo.TabStop = False
    Me.grpSongTimeInfo.Text = "Display Mode"
    '
    'radSongTimeModeRemaining
    '
    Me.radSongTimeModeRemaining.AutoSize = True
    Me.radSongTimeModeRemaining.Location = New System.Drawing.Point(82, 16)
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
    Me.radSongTimeModeElapsed.Location = New System.Drawing.Point(11, 16)
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
    Me.lblMediaPlayerSongTimeWidth.Location = New System.Drawing.Point(8, 110)
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
    Me.TabPage5.Controls.Add(Me.nudSongTitleTrackRatingOpacity)
    Me.TabPage5.Controls.Add(Me.lblSongTitleTrackRatingOpacity)
    Me.TabPage5.Controls.Add(Me.chkSongTitleShowTrackRating)
    Me.TabPage5.Controls.Add(Me.grpSongTitleFormat)
    Me.TabPage5.Controls.Add(Me.nudSongTagWidth)
    Me.TabPage5.Controls.Add(Me.lblSongTagWidth)
    Me.TabPage5.Controls.Add(Me.chkShowSongTagInfo)
    Me.TabPage5.Location = New System.Drawing.Point(4, 22)
    Me.TabPage5.Name = "TabPage5"
    Me.TabPage5.Padding = New System.Windows.Forms.Padding(8)
    Me.TabPage5.Size = New System.Drawing.Size(492, 374)
    Me.TabPage5.TabIndex = 4
    Me.TabPage5.Text = "Song Title Display"
    Me.TabPage5.UseVisualStyleBackColor = True
    '
    'nudSongTitleTrackRatingOpacity
    '
    Me.nudSongTitleTrackRatingOpacity.Location = New System.Drawing.Point(59, 177)
    Me.nudSongTitleTrackRatingOpacity.Margin = New System.Windows.Forms.Padding(0)
    Me.nudSongTitleTrackRatingOpacity.Maximum = New Decimal(New Integer() {255, 0, 0, 0})
    Me.nudSongTitleTrackRatingOpacity.Name = "nudSongTitleTrackRatingOpacity"
    Me.nudSongTitleTrackRatingOpacity.Size = New System.Drawing.Size(49, 22)
    Me.nudSongTitleTrackRatingOpacity.TabIndex = 10
    '
    'lblSongTitleTrackRatingOpacity
    '
    Me.lblSongTitleTrackRatingOpacity.AutoSize = True
    Me.lblSongTitleTrackRatingOpacity.Location = New System.Drawing.Point(8, 179)
    Me.lblSongTitleTrackRatingOpacity.Margin = New System.Windows.Forms.Padding(0, 2, 3, 0)
    Me.lblSongTitleTrackRatingOpacity.Name = "lblSongTitleTrackRatingOpacity"
    Me.lblSongTitleTrackRatingOpacity.Size = New System.Drawing.Size(49, 13)
    Me.lblSongTitleTrackRatingOpacity.TabIndex = 11
    Me.lblSongTitleTrackRatingOpacity.Text = "Opacity:"
    '
    'chkSongTitleShowTrackRating
    '
    Me.chkSongTitleShowTrackRating.AutoSize = True
    Me.chkSongTitleShowTrackRating.Location = New System.Drawing.Point(11, 152)
    Me.chkSongTitleShowTrackRating.Margin = New System.Windows.Forms.Padding(0, 0, 0, 8)
    Me.chkSongTitleShowTrackRating.Name = "chkSongTitleShowTrackRating"
    Me.chkSongTitleShowTrackRating.Size = New System.Drawing.Size(210, 17)
    Me.chkSongTitleShowTrackRating.TabIndex = 9
    Me.chkSongTitleShowTrackRating.Text = "Show Track Rating in Song Title Box"
    Me.chkSongTitleShowTrackRating.UseVisualStyleBackColor = True
    '
    'grpSongTitleFormat
    '
    Me.grpSongTitleFormat.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.grpSongTitleFormat.Controls.Add(Me.txtSongTitleFormat)
    Me.grpSongTitleFormat.Location = New System.Drawing.Point(11, 82)
    Me.grpSongTitleFormat.Name = "grpSongTitleFormat"
    Me.grpSongTitleFormat.Size = New System.Drawing.Size(470, 58)
    Me.grpSongTitleFormat.TabIndex = 3
    Me.grpSongTitleFormat.TabStop = False
    Me.grpSongTitleFormat.Text = "Text Format"
    '
    'txtSongTitleFormat
    '
    Me.txtSongTitleFormat.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.txtSongTitleFormat.Location = New System.Drawing.Point(7, 22)
    Me.txtSongTitleFormat.Name = "txtSongTitleFormat"
    Me.txtSongTitleFormat.Size = New System.Drawing.Size(454, 22)
    Me.txtSongTitleFormat.TabIndex = 0
    Me.txtSongTitleFormat.Text = "%artist% - %title%"
    '
    'nudSongTagWidth
    '
    Me.nudSongTagWidth.Location = New System.Drawing.Point(139, 45)
    Me.nudSongTagWidth.Maximum = New Decimal(New Integer() {2000, 0, 0, 0})
    Me.nudSongTagWidth.Minimum = New Decimal(New Integer() {50, 0, 0, 0})
    Me.nudSongTagWidth.Name = "nudSongTagWidth"
    Me.nudSongTagWidth.Size = New System.Drawing.Size(63, 22)
    Me.nudSongTagWidth.TabIndex = 2
    Me.nudSongTagWidth.Value = New Decimal(New Integer() {50, 0, 0, 0})
    '
    'lblSongTagWidth
    '
    Me.lblSongTagWidth.AutoSize = True
    Me.lblSongTagWidth.Location = New System.Drawing.Point(11, 47)
    Me.lblSongTagWidth.Name = "lblSongTagWidth"
    Me.lblSongTagWidth.Size = New System.Drawing.Size(125, 13)
    Me.lblSongTagWidth.TabIndex = 1
    Me.lblSongTagWidth.Text = "Text Box Width (pixels):"
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
    Me.TabPage2.Controls.Add(Me.grpText)
    Me.TabPage2.Controls.Add(Me.grpTooltipTrackRating)
    Me.TabPage2.Controls.Add(Me.grpTooltipAlbumArt)
    Me.TabPage2.Location = New System.Drawing.Point(4, 22)
    Me.TabPage2.Name = "TabPage2"
    Me.TabPage2.Padding = New System.Windows.Forms.Padding(3, 3, 6, 6)
    Me.TabPage2.Size = New System.Drawing.Size(492, 374)
    Me.TabPage2.TabIndex = 1
    Me.TabPage2.Text = "Tooltip"
    Me.TabPage2.UseVisualStyleBackColor = True
    '
    'grpText
    '
    Me.grpText.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.grpText.Controls.Add(Me.txtTooltipText)
    Me.grpText.Controls.Add(Me.nudTooltipTextMaxWidth)
    Me.grpText.Controls.Add(Me.Label4)
    Me.grpText.Location = New System.Drawing.Point(6, 108)
    Me.grpText.Name = "grpText"
    Me.grpText.Size = New System.Drawing.Size(477, 203)
    Me.grpText.TabIndex = 8
    Me.grpText.TabStop = False
    Me.grpText.Text = "Tooltip Text"
    '
    'txtTooltipText
    '
    Me.txtTooltipText.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.txtTooltipText.Location = New System.Drawing.Point(9, 22)
    Me.txtTooltipText.Multiline = True
    Me.txtTooltipText.Name = "txtTooltipText"
    Me.txtTooltipText.Size = New System.Drawing.Size(461, 142)
    Me.txtTooltipText.TabIndex = 0
    Me.txtTooltipText.Text = "%title% (%length%)" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Artist: %artist%" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Album: %album% (%year%)" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Genre: %genre%"
    '
    'nudTooltipTextMaxWidth
    '
    Me.nudTooltipTextMaxWidth.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.nudTooltipTextMaxWidth.Location = New System.Drawing.Point(411, 170)
    Me.nudTooltipTextMaxWidth.Maximum = New Decimal(New Integer() {5000, 0, 0, 0})
    Me.nudTooltipTextMaxWidth.Minimum = New Decimal(New Integer() {10, 0, 0, 0})
    Me.nudTooltipTextMaxWidth.Name = "nudTooltipTextMaxWidth"
    Me.nudTooltipTextMaxWidth.Size = New System.Drawing.Size(60, 22)
    Me.nudTooltipTextMaxWidth.TabIndex = 7
    Me.nudTooltipTextMaxWidth.Value = New Decimal(New Integer() {400, 0, 0, 0})
    '
    'Label4
    '
    Me.Label4.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.Label4.AutoSize = True
    Me.Label4.Location = New System.Drawing.Point(284, 172)
    Me.Label4.Name = "Label4"
    Me.Label4.Size = New System.Drawing.Size(117, 13)
    Me.Label4.TabIndex = 6
    Me.Label4.Text = "Maximum Text Width:"
    '
    'grpTooltipTrackRating
    '
    Me.grpTooltipTrackRating.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.grpTooltipTrackRating.Controls.Add(Me.chkShowTrackRating)
    Me.grpTooltipTrackRating.Location = New System.Drawing.Point(6, 317)
    Me.grpTooltipTrackRating.Name = "grpTooltipTrackRating"
    Me.grpTooltipTrackRating.Size = New System.Drawing.Size(477, 48)
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
    Me.grpTooltipAlbumArt.Location = New System.Drawing.Point(6, 6)
    Me.grpTooltipAlbumArt.Name = "grpTooltipAlbumArt"
    Me.grpTooltipAlbumArt.Size = New System.Drawing.Size(477, 95)
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
    Me.txtAlbumArtFormattingString.Size = New System.Drawing.Size(462, 22)
    Me.txtAlbumArtFormattingString.TabIndex = 10
    Me.txtAlbumArtFormattingString.Text = "%songpath%\folder.*"
    '
    'Label1
    '
    Me.Label1.AutoSize = True
    Me.Label1.Location = New System.Drawing.Point(6, 46)
    Me.Label1.Name = "Label1"
    Me.Label1.Size = New System.Drawing.Size(108, 13)
    Me.Label1.TabIndex = 9
    Me.Label1.Text = "Album Art Location:"
    '
    'nudAlbumArtWidth
    '
    Me.nudAlbumArtWidth.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.nudAlbumArtWidth.Location = New System.Drawing.Point(407, 20)
    Me.nudAlbumArtWidth.Maximum = New Decimal(New Integer() {1000, 0, 0, 0})
    Me.nudAlbumArtWidth.Minimum = New Decimal(New Integer() {25, 0, 0, 0})
    Me.nudAlbumArtWidth.Name = "nudAlbumArtWidth"
    Me.nudAlbumArtWidth.Size = New System.Drawing.Size(63, 22)
    Me.nudAlbumArtWidth.TabIndex = 8
    Me.nudAlbumArtWidth.Value = New Decimal(New Integer() {100, 0, 0, 0})
    '
    'Label2
    '
    Me.Label2.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.Label2.AutoSize = True
    Me.Label2.Location = New System.Drawing.Point(267, 22)
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
    'tabpageHelp
    '
    Me.tabpageHelp.Controls.Add(Me.txtHelp)
    Me.tabpageHelp.Controls.Add(Me.lblHelp)
    Me.tabpageHelp.Location = New System.Drawing.Point(4, 22)
    Me.tabpageHelp.Name = "tabpageHelp"
    Me.tabpageHelp.Padding = New System.Windows.Forms.Padding(3)
    Me.tabpageHelp.Size = New System.Drawing.Size(492, 374)
    Me.tabpageHelp.TabIndex = 5
    Me.tabpageHelp.Text = "Help"
    Me.tabpageHelp.UseVisualStyleBackColor = True
    '
    'txtHelp
    '
    Me.txtHelp.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.txtHelp.Location = New System.Drawing.Point(21, 51)
    Me.txtHelp.Multiline = True
    Me.txtHelp.Name = "txtHelp"
    Me.txtHelp.ReadOnly = True
    Me.txtHelp.Size = New System.Drawing.Size(453, 301)
    Me.txtHelp.TabIndex = 1
    Me.txtHelp.Text = resources.GetString("txtHelp.Text")
    '
    'lblHelp
    '
    Me.lblHelp.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.lblHelp.Location = New System.Drawing.Point(18, 16)
    Me.lblHelp.Name = "lblHelp"
    Me.lblHelp.Size = New System.Drawing.Size(456, 31)
    Me.lblHelp.TabIndex = 0
    Me.lblHelp.Text = "The following variables can be used for the song title display, tooltip text, and" & _
        " album art path."
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
    Me.grpSongTitleFormat.ResumeLayout(False)
    Me.grpSongTitleFormat.PerformLayout()
    CType(Me.nudSongTagWidth, System.ComponentModel.ISupportInitialize).EndInit()
    Me.TabPage2.ResumeLayout(False)
    Me.grpText.ResumeLayout(False)
    Me.grpText.PerformLayout()
    CType(Me.nudTooltipTextMaxWidth, System.ComponentModel.ISupportInitialize).EndInit()
    Me.grpTooltipTrackRating.ResumeLayout(False)
    Me.grpTooltipTrackRating.PerformLayout()
    Me.grpTooltipAlbumArt.ResumeLayout(False)
    Me.grpTooltipAlbumArt.PerformLayout()
    CType(Me.nudAlbumArtWidth, System.ComponentModel.ISupportInitialize).EndInit()
    Me.tabpageHelp.ResumeLayout(False)
    Me.tabpageHelp.PerformLayout()
    Me.ResumeLayout(False)

  End Sub
    Friend WithEvents Tabs As System.Windows.Forms.TabControl
    Friend WithEvents TabPage1 As System.Windows.Forms.TabPage
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
    Friend WithEvents chkShowNowPlayingTooltip As System.Windows.Forms.CheckBox
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
    Friend WithEvents lblSongTagWidth As System.Windows.Forms.Label
    Friend WithEvents chkShowSongTagInfo As System.Windows.Forms.CheckBox
    Friend WithEvents txtAlbumArtFormattingString As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents grpTrackRatingButton As System.Windows.Forms.GroupBox
    Friend WithEvents chkShowTrackRatingButton As System.Windows.Forms.CheckBox
    Friend WithEvents nudTooltipTextMaxWidth As System.Windows.Forms.NumericUpDown
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents grpText As System.Windows.Forms.GroupBox
    Friend WithEvents txtTooltipText As System.Windows.Forms.TextBox
    Friend WithEvents grpSongTitleFormat As System.Windows.Forms.GroupBox
    Friend WithEvents txtSongTitleFormat As System.Windows.Forms.TextBox
    Friend WithEvents tabpageHelp As System.Windows.Forms.TabPage
    Friend WithEvents txtHelp As System.Windows.Forms.TextBox
  Friend WithEvents lblHelp As System.Windows.Forms.Label
  Friend WithEvents nudSongTitleTrackRatingOpacity As System.Windows.Forms.NumericUpDown
  Friend WithEvents lblSongTitleTrackRatingOpacity As System.Windows.Forms.Label
  Friend WithEvents chkSongTitleShowTrackRating As System.Windows.Forms.CheckBox
End Class
