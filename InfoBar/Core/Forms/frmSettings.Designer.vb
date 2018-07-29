<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmSettings
    Inherits System.Windows.Forms.Form

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
    Me.components = New System.ComponentModel.Container()
    Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmSettings))
    Me.btnOK = New System.Windows.Forms.Button()
    Me.btnCancel = New System.Windows.Forms.Button()
    Me.btnApply = New System.Windows.Forms.Button()
    Me.grpGeneral_Location = New System.Windows.Forms.GroupBox()
    Me.radGeneral_Location_Right = New System.Windows.Forms.RadioButton()
    Me.radGeneral_Location_Left = New System.Windows.Forms.RadioButton()
    Me.chkDisableToolbarDocking = New System.Windows.Forms.CheckBox()
    Me.chkAutohide = New System.Windows.Forms.CheckBox()
    Me.chkGeneral_Location_AlwaysOnTop = New System.Windows.Forms.CheckBox()
    Me.radGeneral_Location_Bottom = New System.Windows.Forms.RadioButton()
    Me.radGeneral_Location_Top = New System.Windows.Forms.RadioButton()
    Me.lvSkins = New System.Windows.Forms.ListView()
    Me.colhdrSkins_Name = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
    Me.colhdrSkins_Author = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
    Me.colhdrSkins_Version = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
    Me.colhdrSkins_Website = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
    Me.grpModuleInfo = New System.Windows.Forms.GroupBox()
    Me.tlpModuleDetails = New System.Windows.Forms.TableLayoutPanel()
    Me.txtModuleGUID = New System.Windows.Forms.TextBox()
    Me.lblModules6 = New System.Windows.Forms.Label()
    Me.txtModuleVersion = New System.Windows.Forms.TextBox()
    Me.lblModules1 = New System.Windows.Forms.Label()
    Me.lblModuleHomepage = New System.Windows.Forms.LinkLabel()
    Me.txtModuleDescription = New System.Windows.Forms.TextBox()
    Me.lblModules4 = New System.Windows.Forms.Label()
    Me.lblModuleEmail = New System.Windows.Forms.LinkLabel()
    Me.lblModules2 = New System.Windows.Forms.Label()
    Me.txtModuleAuthor = New System.Windows.Forms.TextBox()
    Me.lblModules3 = New System.Windows.Forms.Label()
    Me.lblModules5 = New System.Windows.Forms.Label()
    Me.imlModules = New System.Windows.Forms.ImageList(Me.components)
    Me.btnSkins_VisitAuthorWebsite = New System.Windows.Forms.Button()
    Me.btnSkins_RefreshList = New System.Windows.Forms.Button()
    Me.picSkinPreview = New System.Windows.Forms.PictureBox()
    Me.grpTooltips = New System.Windows.Forms.GroupBox()
    Me.chkAutohide_IgnoreMaximizedState = New System.Windows.Forms.CheckBox()
    Me.nudSkinBGOpacity = New System.Windows.Forms.NumericUpDown()
    Me.chkOverrideSkinBackgroundOpacity = New System.Windows.Forms.CheckBox()
    Me.chkEnableTooltipFade = New System.Windows.Forms.CheckBox()
    Me.lblAutohideDelay = New System.Windows.Forms.Label()
    Me.nudAutohideDelay = New System.Windows.Forms.NumericUpDown()
    Me.lblAutohideDelayMS = New System.Windows.Forms.Label()
    Me.chkAutoHideAnimation = New System.Windows.Forms.CheckBox()
    Me.lblAutohideAnimationSpeed = New System.Windows.Forms.Label()
    Me.nudAutohideAnimationSpeed = New System.Windows.Forms.NumericUpDown()
    Me.grpVersionInfo = New System.Windows.Forms.GroupBox()
    Me.lvVersionInfo = New System.Windows.Forms.ListView()
    Me.colhdrFile = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
    Me.colhdrVersion = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
    Me.lblVersion = New System.Windows.Forms.Label()
    Me.lblInfoBar = New System.Windows.Forms.Label()
    Me.picLogo = New System.Windows.Forms.PictureBox()
    Me.tvSettings = New System.Windows.Forms.TreeView()
    Me.btnModules_InsertSeparator = New System.Windows.Forms.Button()
    Me.btnModules_Delete = New System.Windows.Forms.Button()
    Me.lvModules = New System.Windows.Forms.ListView()
    Me.colhdrModule = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
    Me.panelAbout = New System.Windows.Forms.Panel()
    Me.panelModuleSettings = New System.Windows.Forms.Panel()
    Me.panelGeneral = New System.Windows.Forms.Panel()
    Me.grpGeneral_Startup = New System.Windows.Forms.GroupBox()
    Me.chkGeneral_Startup_RunAtWindowsStartup = New System.Windows.Forms.CheckBox()
    Me.grpModuleAlignment = New System.Windows.Forms.GroupBox()
    Me.radModuleAlignmentJustifyCenter = New System.Windows.Forms.RadioButton()
    Me.radModuleAlignmentLeft = New System.Windows.Forms.RadioButton()
    Me.radModuleAlignmentCenter = New System.Windows.Forms.RadioButton()
    Me.chkShowSeparators = New System.Windows.Forms.CheckBox()
    Me.radModuleAlignmentRight = New System.Windows.Forms.RadioButton()
    Me.radModuleAlignmentJustify = New System.Windows.Forms.RadioButton()
    Me.panelModules = New System.Windows.Forms.Panel()
    Me.panelSkins = New System.Windows.Forms.Panel()
    Me.panelIcons = New System.Windows.Forms.Panel()
    Me.btnIconsVisitWebsite = New System.Windows.Forms.Button()
    Me.btnIconsRefreshList = New System.Windows.Forms.Button()
    Me.picIconsPreview = New System.Windows.Forms.PictureBox()
    Me.lvIcons = New System.Windows.Forms.ListView()
    Me.ColumnHeader1 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
    Me.ColumnHeader2 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
    Me.ColumnHeader3 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
    Me.ColumnHeader4 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
    Me.panelAdvanced = New System.Windows.Forms.Panel()
    Me.grpAutoHideAdvanced = New System.Windows.Forms.GroupBox()
    Me.grpFileAssociations = New System.Windows.Forms.GroupBox()
    Me.btnRepairFileAssociations = New System.Windows.Forms.Button()
    Me.lblFileAssociationsInfo = New System.Windows.Forms.Label()
    Me.lblFileAssociationStatus = New System.Windows.Forms.Label()
    Me.hrButtons = New InfoBar.Controls.HorizontalRule()
    Me.grpGeneral_Location.SuspendLayout()
    Me.grpModuleInfo.SuspendLayout()
    Me.tlpModuleDetails.SuspendLayout()
    CType(Me.picSkinPreview, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.grpTooltips.SuspendLayout()
    CType(Me.nudSkinBGOpacity, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.nudAutohideDelay, System.ComponentModel.ISupportInitialize).BeginInit()
    CType(Me.nudAutohideAnimationSpeed, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.grpVersionInfo.SuspendLayout()
    CType(Me.picLogo, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.panelAbout.SuspendLayout()
    Me.panelGeneral.SuspendLayout()
    Me.grpGeneral_Startup.SuspendLayout()
    Me.grpModuleAlignment.SuspendLayout()
    Me.panelModules.SuspendLayout()
    Me.panelSkins.SuspendLayout()
    Me.panelIcons.SuspendLayout()
    CType(Me.picIconsPreview, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.panelAdvanced.SuspendLayout()
    Me.grpAutoHideAdvanced.SuspendLayout()
    Me.grpFileAssociations.SuspendLayout()
    Me.SuspendLayout()
    '
    'btnOK
    '
    Me.btnOK.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.btnOK.AutoSize = True
    Me.btnOK.Location = New System.Drawing.Point(363, 431)
    Me.btnOK.MinimumSize = New System.Drawing.Size(75, 23)
    Me.btnOK.Name = "btnOK"
    Me.btnOK.Size = New System.Drawing.Size(75, 23)
    Me.btnOK.TabIndex = 1000
    Me.btnOK.Text = "&OK"
    '
    'btnCancel
    '
    Me.btnCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.btnCancel.AutoSize = True
    Me.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
    Me.btnCancel.Location = New System.Drawing.Point(444, 431)
    Me.btnCancel.MinimumSize = New System.Drawing.Size(75, 23)
    Me.btnCancel.Name = "btnCancel"
    Me.btnCancel.Size = New System.Drawing.Size(75, 23)
    Me.btnCancel.TabIndex = 1001
    Me.btnCancel.Text = "&Cancel"
    '
    'btnApply
    '
    Me.btnApply.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.btnApply.AutoSize = True
    Me.btnApply.Enabled = False
    Me.btnApply.Location = New System.Drawing.Point(525, 431)
    Me.btnApply.MinimumSize = New System.Drawing.Size(75, 23)
    Me.btnApply.Name = "btnApply"
    Me.btnApply.Size = New System.Drawing.Size(75, 23)
    Me.btnApply.TabIndex = 1002
    Me.btnApply.Text = "&Apply"
    '
    'grpGeneral_Location
    '
    Me.grpGeneral_Location.Controls.Add(Me.radGeneral_Location_Right)
    Me.grpGeneral_Location.Controls.Add(Me.radGeneral_Location_Left)
    Me.grpGeneral_Location.Controls.Add(Me.chkDisableToolbarDocking)
    Me.grpGeneral_Location.Controls.Add(Me.chkAutohide)
    Me.grpGeneral_Location.Controls.Add(Me.chkGeneral_Location_AlwaysOnTop)
    Me.grpGeneral_Location.Controls.Add(Me.radGeneral_Location_Bottom)
    Me.grpGeneral_Location.Controls.Add(Me.radGeneral_Location_Top)
    Me.grpGeneral_Location.Location = New System.Drawing.Point(0, 0)
    Me.grpGeneral_Location.Name = "grpGeneral_Location"
    Me.grpGeneral_Location.Size = New System.Drawing.Size(433, 73)
    Me.grpGeneral_Location.TabIndex = 0
    Me.grpGeneral_Location.TabStop = False
    Me.grpGeneral_Location.Text = "Location"
    '
    'radGeneral_Location_Right
    '
    Me.radGeneral_Location_Right.AutoSize = True
    Me.radGeneral_Location_Right.Location = New System.Drawing.Point(175, 20)
    Me.radGeneral_Location_Right.Name = "radGeneral_Location_Right"
    Me.radGeneral_Location_Right.Size = New System.Drawing.Size(50, 17)
    Me.radGeneral_Location_Right.TabIndex = 8
    Me.radGeneral_Location_Right.TabStop = True
    Me.radGeneral_Location_Right.Text = "Right"
    Me.radGeneral_Location_Right.UseVisualStyleBackColor = True
    '
    'radGeneral_Location_Left
    '
    Me.radGeneral_Location_Left.AutoSize = True
    Me.radGeneral_Location_Left.Location = New System.Drawing.Point(125, 20)
    Me.radGeneral_Location_Left.Name = "radGeneral_Location_Left"
    Me.radGeneral_Location_Left.Size = New System.Drawing.Size(43, 17)
    Me.radGeneral_Location_Left.TabIndex = 7
    Me.radGeneral_Location_Left.TabStop = True
    Me.radGeneral_Location_Left.Text = "Left"
    Me.radGeneral_Location_Left.UseVisualStyleBackColor = True
    '
    'chkDisableToolbarDocking
    '
    Me.chkDisableToolbarDocking.AutoSize = True
    Me.chkDisableToolbarDocking.Location = New System.Drawing.Point(190, 45)
    Me.chkDisableToolbarDocking.Margin = New System.Windows.Forms.Padding(0, 8, 8, 0)
    Me.chkDisableToolbarDocking.Name = "chkDisableToolbarDocking"
    Me.chkDisableToolbarDocking.Size = New System.Drawing.Size(143, 17)
    Me.chkDisableToolbarDocking.TabIndex = 6
    Me.chkDisableToolbarDocking.Text = "Disable Toolbar Docking"
    Me.chkDisableToolbarDocking.UseVisualStyleBackColor = True
    '
    'chkAutohide
    '
    Me.chkAutohide.AutoSize = True
    Me.chkAutohide.Location = New System.Drawing.Point(113, 45)
    Me.chkAutohide.Margin = New System.Windows.Forms.Padding(0, 4, 8, 0)
    Me.chkAutohide.Name = "chkAutohide"
    Me.chkAutohide.Size = New System.Drawing.Size(68, 17)
    Me.chkAutohide.TabIndex = 5
    Me.chkAutohide.Text = "Autohide"
    Me.chkAutohide.UseVisualStyleBackColor = True
    '
    'chkGeneral_Location_AlwaysOnTop
    '
    Me.chkGeneral_Location_AlwaysOnTop.AutoSize = True
    Me.chkGeneral_Location_AlwaysOnTop.Location = New System.Drawing.Point(11, 45)
    Me.chkGeneral_Location_AlwaysOnTop.Margin = New System.Windows.Forms.Padding(0, 4, 8, 0)
    Me.chkGeneral_Location_AlwaysOnTop.Name = "chkGeneral_Location_AlwaysOnTop"
    Me.chkGeneral_Location_AlwaysOnTop.Size = New System.Drawing.Size(92, 17)
    Me.chkGeneral_Location_AlwaysOnTop.TabIndex = 4
    Me.chkGeneral_Location_AlwaysOnTop.Text = "Always on top"
    Me.chkGeneral_Location_AlwaysOnTop.UseVisualStyleBackColor = True
    '
    'radGeneral_Location_Bottom
    '
    Me.radGeneral_Location_Bottom.AutoSize = True
    Me.radGeneral_Location_Bottom.Location = New System.Drawing.Point(60, 20)
    Me.radGeneral_Location_Bottom.Name = "radGeneral_Location_Bottom"
    Me.radGeneral_Location_Bottom.Size = New System.Drawing.Size(58, 17)
    Me.radGeneral_Location_Bottom.TabIndex = 3
    Me.radGeneral_Location_Bottom.TabStop = True
    Me.radGeneral_Location_Bottom.Text = "Bottom"
    Me.radGeneral_Location_Bottom.UseVisualStyleBackColor = True
    '
    'radGeneral_Location_Top
    '
    Me.radGeneral_Location_Top.AutoSize = True
    Me.radGeneral_Location_Top.Checked = True
    Me.radGeneral_Location_Top.Location = New System.Drawing.Point(11, 20)
    Me.radGeneral_Location_Top.Name = "radGeneral_Location_Top"
    Me.radGeneral_Location_Top.Size = New System.Drawing.Size(44, 17)
    Me.radGeneral_Location_Top.TabIndex = 2
    Me.radGeneral_Location_Top.TabStop = True
    Me.radGeneral_Location_Top.Text = "Top"
    Me.radGeneral_Location_Top.UseVisualStyleBackColor = True
    '
    'lvSkins
    '
    Me.lvSkins.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.colhdrSkins_Name, Me.colhdrSkins_Author, Me.colhdrSkins_Version, Me.colhdrSkins_Website})
    Me.lvSkins.FullRowSelect = True
    Me.lvSkins.HideSelection = False
    Me.lvSkins.Location = New System.Drawing.Point(0, 36)
    Me.lvSkins.Margin = New System.Windows.Forms.Padding(0, 4, 0, 4)
    Me.lvSkins.MultiSelect = False
    Me.lvSkins.Name = "lvSkins"
    Me.lvSkins.ShowGroups = False
    Me.lvSkins.ShowItemToolTips = True
    Me.lvSkins.Size = New System.Drawing.Size(433, 334)
    Me.lvSkins.TabIndex = 2
    Me.lvSkins.UseCompatibleStateImageBehavior = False
    Me.lvSkins.View = System.Windows.Forms.View.Details
    '
    'colhdrSkins_Name
    '
    Me.colhdrSkins_Name.Text = "Name"
    Me.colhdrSkins_Name.Width = 91
    '
    'colhdrSkins_Author
    '
    Me.colhdrSkins_Author.Text = "Author"
    Me.colhdrSkins_Author.Width = 90
    '
    'colhdrSkins_Version
    '
    Me.colhdrSkins_Version.Text = "Version"
    Me.colhdrSkins_Version.Width = 90
    '
    'colhdrSkins_Website
    '
    Me.colhdrSkins_Website.Text = "Website"
    Me.colhdrSkins_Website.Width = 115
    '
    'grpModuleInfo
    '
    Me.grpModuleInfo.Controls.Add(Me.tlpModuleDetails)
    Me.grpModuleInfo.Location = New System.Drawing.Point(0, 245)
    Me.grpModuleInfo.Margin = New System.Windows.Forms.Padding(0)
    Me.grpModuleInfo.Name = "grpModuleInfo"
    Me.grpModuleInfo.Padding = New System.Windows.Forms.Padding(8, 4, 8, 8)
    Me.grpModuleInfo.Size = New System.Drawing.Size(433, 155)
    Me.grpModuleInfo.TabIndex = 24
    Me.grpModuleInfo.TabStop = False
    Me.grpModuleInfo.Text = "Module Details"
    '
    'tlpModuleDetails
    '
    Me.tlpModuleDetails.ColumnCount = 2
    Me.tlpModuleDetails.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
    Me.tlpModuleDetails.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
    Me.tlpModuleDetails.Controls.Add(Me.txtModuleGUID, 1, 5)
    Me.tlpModuleDetails.Controls.Add(Me.lblModules6, 0, 5)
    Me.tlpModuleDetails.Controls.Add(Me.txtModuleVersion, 1, 2)
    Me.tlpModuleDetails.Controls.Add(Me.lblModules1, 0, 0)
    Me.tlpModuleDetails.Controls.Add(Me.lblModuleHomepage, 1, 4)
    Me.tlpModuleDetails.Controls.Add(Me.txtModuleDescription, 1, 0)
    Me.tlpModuleDetails.Controls.Add(Me.lblModules4, 0, 4)
    Me.tlpModuleDetails.Controls.Add(Me.lblModuleEmail, 1, 3)
    Me.tlpModuleDetails.Controls.Add(Me.lblModules2, 0, 1)
    Me.tlpModuleDetails.Controls.Add(Me.txtModuleAuthor, 1, 1)
    Me.tlpModuleDetails.Controls.Add(Me.lblModules3, 0, 3)
    Me.tlpModuleDetails.Controls.Add(Me.lblModules5, 0, 2)
    Me.tlpModuleDetails.Location = New System.Drawing.Point(7, 18)
    Me.tlpModuleDetails.Name = "tlpModuleDetails"
    Me.tlpModuleDetails.RowCount = 6
    Me.tlpModuleDetails.RowStyles.Add(New System.Windows.Forms.RowStyle())
    Me.tlpModuleDetails.RowStyles.Add(New System.Windows.Forms.RowStyle())
    Me.tlpModuleDetails.RowStyles.Add(New System.Windows.Forms.RowStyle())
    Me.tlpModuleDetails.RowStyles.Add(New System.Windows.Forms.RowStyle())
    Me.tlpModuleDetails.RowStyles.Add(New System.Windows.Forms.RowStyle())
    Me.tlpModuleDetails.RowStyles.Add(New System.Windows.Forms.RowStyle())
    Me.tlpModuleDetails.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
    Me.tlpModuleDetails.Size = New System.Drawing.Size(420, 129)
    Me.tlpModuleDetails.TabIndex = 9
    '
    'txtModuleGUID
    '
    Me.txtModuleGUID.BackColor = System.Drawing.SystemColors.Control
    Me.txtModuleGUID.BorderStyle = System.Windows.Forms.BorderStyle.None
    Me.txtModuleGUID.Dock = System.Windows.Forms.DockStyle.Fill
    Me.txtModuleGUID.Location = New System.Drawing.Point(76, 112)
    Me.txtModuleGUID.Margin = New System.Windows.Forms.Padding(2, 1, 0, 1)
    Me.txtModuleGUID.Name = "txtModuleGUID"
    Me.txtModuleGUID.ReadOnly = True
    Me.txtModuleGUID.Size = New System.Drawing.Size(344, 14)
    Me.txtModuleGUID.TabIndex = 12
    '
    'lblModules6
    '
    Me.lblModules6.Dock = System.Windows.Forms.DockStyle.Fill
    Me.lblModules6.Location = New System.Drawing.Point(0, 112)
    Me.lblModules6.Margin = New System.Windows.Forms.Padding(0, 1, 0, 1)
    Me.lblModules6.Name = "lblModules6"
    Me.lblModules6.Size = New System.Drawing.Size(74, 16)
    Me.lblModules6.TabIndex = 11
    Me.lblModules6.Text = "GUID:"
    '
    'txtModuleVersion
    '
    Me.txtModuleVersion.BackColor = System.Drawing.SystemColors.Control
    Me.txtModuleVersion.BorderStyle = System.Windows.Forms.BorderStyle.None
    Me.txtModuleVersion.Dock = System.Windows.Forms.DockStyle.Fill
    Me.txtModuleVersion.Location = New System.Drawing.Point(76, 62)
    Me.txtModuleVersion.Margin = New System.Windows.Forms.Padding(2, 1, 0, 1)
    Me.txtModuleVersion.Name = "txtModuleVersion"
    Me.txtModuleVersion.ReadOnly = True
    Me.txtModuleVersion.Size = New System.Drawing.Size(344, 14)
    Me.txtModuleVersion.TabIndex = 10
    '
    'lblModules1
    '
    Me.lblModules1.Dock = System.Windows.Forms.DockStyle.Fill
    Me.lblModules1.Location = New System.Drawing.Point(0, 0)
    Me.lblModules1.Margin = New System.Windows.Forms.Padding(0)
    Me.lblModules1.Name = "lblModules1"
    Me.lblModules1.Size = New System.Drawing.Size(74, 44)
    Me.lblModules1.TabIndex = 0
    Me.lblModules1.Text = "Description:"
    '
    'lblModuleHomepage
    '
    Me.lblModuleHomepage.ActiveLinkColor = System.Drawing.SystemColors.HotTrack
    Me.lblModuleHomepage.BackColor = System.Drawing.SystemColors.Control
    Me.lblModuleHomepage.Dock = System.Windows.Forms.DockStyle.Fill
    Me.lblModuleHomepage.LinkColor = System.Drawing.SystemColors.HotTrack
    Me.lblModuleHomepage.Location = New System.Drawing.Point(74, 95)
    Me.lblModuleHomepage.Margin = New System.Windows.Forms.Padding(0, 1, 0, 1)
    Me.lblModuleHomepage.Name = "lblModuleHomepage"
    Me.lblModuleHomepage.Size = New System.Drawing.Size(346, 15)
    Me.lblModuleHomepage.TabIndex = 8
    Me.lblModuleHomepage.UseMnemonic = False
    Me.lblModuleHomepage.VisitedLinkColor = System.Drawing.SystemColors.HotTrack
    '
    'txtModuleDescription
    '
    Me.txtModuleDescription.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.txtModuleDescription.BackColor = System.Drawing.SystemColors.Control
    Me.txtModuleDescription.BorderStyle = System.Windows.Forms.BorderStyle.None
    Me.txtModuleDescription.Location = New System.Drawing.Point(76, 0)
    Me.txtModuleDescription.Margin = New System.Windows.Forms.Padding(2, 0, 0, 4)
    Me.txtModuleDescription.Multiline = True
    Me.txtModuleDescription.Name = "txtModuleDescription"
    Me.txtModuleDescription.ReadOnly = True
    Me.txtModuleDescription.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
    Me.txtModuleDescription.Size = New System.Drawing.Size(344, 40)
    Me.txtModuleDescription.TabIndex = 5
    Me.txtModuleDescription.Text = "Module Description"
    '
    'lblModules4
    '
    Me.lblModules4.Dock = System.Windows.Forms.DockStyle.Fill
    Me.lblModules4.Location = New System.Drawing.Point(0, 95)
    Me.lblModules4.Margin = New System.Windows.Forms.Padding(0, 1, 0, 1)
    Me.lblModules4.Name = "lblModules4"
    Me.lblModules4.Size = New System.Drawing.Size(74, 15)
    Me.lblModules4.TabIndex = 4
    Me.lblModules4.Text = "Homepage:"
    '
    'lblModuleEmail
    '
    Me.lblModuleEmail.ActiveLinkColor = System.Drawing.SystemColors.HotTrack
    Me.lblModuleEmail.BackColor = System.Drawing.SystemColors.Control
    Me.lblModuleEmail.Dock = System.Windows.Forms.DockStyle.Fill
    Me.lblModuleEmail.LinkColor = System.Drawing.SystemColors.HotTrack
    Me.lblModuleEmail.Location = New System.Drawing.Point(74, 78)
    Me.lblModuleEmail.Margin = New System.Windows.Forms.Padding(0, 1, 0, 1)
    Me.lblModuleEmail.Name = "lblModuleEmail"
    Me.lblModuleEmail.Size = New System.Drawing.Size(346, 15)
    Me.lblModuleEmail.TabIndex = 7
    Me.lblModuleEmail.Tag = ""
    Me.lblModuleEmail.UseMnemonic = False
    Me.lblModuleEmail.VisitedLinkColor = System.Drawing.SystemColors.HotTrack
    '
    'lblModules2
    '
    Me.lblModules2.Dock = System.Windows.Forms.DockStyle.Fill
    Me.lblModules2.Location = New System.Drawing.Point(0, 45)
    Me.lblModules2.Margin = New System.Windows.Forms.Padding(0, 1, 0, 1)
    Me.lblModules2.Name = "lblModules2"
    Me.lblModules2.Size = New System.Drawing.Size(74, 15)
    Me.lblModules2.TabIndex = 2
    Me.lblModules2.Text = "Author(s):"
    '
    'txtModuleAuthor
    '
    Me.txtModuleAuthor.BackColor = System.Drawing.SystemColors.Control
    Me.txtModuleAuthor.BorderStyle = System.Windows.Forms.BorderStyle.None
    Me.txtModuleAuthor.Dock = System.Windows.Forms.DockStyle.Fill
    Me.txtModuleAuthor.Location = New System.Drawing.Point(76, 45)
    Me.txtModuleAuthor.Margin = New System.Windows.Forms.Padding(2, 1, 0, 1)
    Me.txtModuleAuthor.Name = "txtModuleAuthor"
    Me.txtModuleAuthor.ReadOnly = True
    Me.txtModuleAuthor.Size = New System.Drawing.Size(344, 14)
    Me.txtModuleAuthor.TabIndex = 6
    '
    'lblModules3
    '
    Me.lblModules3.Dock = System.Windows.Forms.DockStyle.Fill
    Me.lblModules3.Location = New System.Drawing.Point(0, 78)
    Me.lblModules3.Margin = New System.Windows.Forms.Padding(0, 1, 0, 1)
    Me.lblModules3.Name = "lblModules3"
    Me.lblModules3.Size = New System.Drawing.Size(74, 15)
    Me.lblModules3.TabIndex = 3
    Me.lblModules3.Text = "E-mail:"
    '
    'lblModules5
    '
    Me.lblModules5.AutoSize = True
    Me.lblModules5.Dock = System.Windows.Forms.DockStyle.Fill
    Me.lblModules5.Location = New System.Drawing.Point(0, 62)
    Me.lblModules5.Margin = New System.Windows.Forms.Padding(0, 1, 0, 1)
    Me.lblModules5.Name = "lblModules5"
    Me.lblModules5.Size = New System.Drawing.Size(74, 14)
    Me.lblModules5.TabIndex = 9
    Me.lblModules5.Text = "Version:"
    '
    'imlModules
    '
    Me.imlModules.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit
    Me.imlModules.ImageSize = New System.Drawing.Size(16, 16)
    Me.imlModules.TransparentColor = System.Drawing.Color.Transparent
    '
    'btnSkins_VisitAuthorWebsite
    '
    Me.btnSkins_VisitAuthorWebsite.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.btnSkins_VisitAuthorWebsite.AutoSize = True
    Me.btnSkins_VisitAuthorWebsite.Location = New System.Drawing.Point(208, 377)
    Me.btnSkins_VisitAuthorWebsite.Margin = New System.Windows.Forms.Padding(0, 0, 4, 0)
    Me.btnSkins_VisitAuthorWebsite.MinimumSize = New System.Drawing.Size(75, 23)
    Me.btnSkins_VisitAuthorWebsite.Name = "btnSkins_VisitAuthorWebsite"
    Me.btnSkins_VisitAuthorWebsite.Size = New System.Drawing.Size(143, 23)
    Me.btnSkins_VisitAuthorWebsite.TabIndex = 3
    Me.btnSkins_VisitAuthorWebsite.Text = "Visit Skin Author's Website"
    '
    'btnSkins_RefreshList
    '
    Me.btnSkins_RefreshList.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.btnSkins_RefreshList.AutoSize = True
    Me.btnSkins_RefreshList.Location = New System.Drawing.Point(358, 377)
    Me.btnSkins_RefreshList.MinimumSize = New System.Drawing.Size(75, 23)
    Me.btnSkins_RefreshList.Name = "btnSkins_RefreshList"
    Me.btnSkins_RefreshList.Size = New System.Drawing.Size(75, 23)
    Me.btnSkins_RefreshList.TabIndex = 4
    Me.btnSkins_RefreshList.Text = "Refresh List"
    '
    'picSkinPreview
    '
    Me.picSkinPreview.Location = New System.Drawing.Point(0, 0)
    Me.picSkinPreview.Margin = New System.Windows.Forms.Padding(0)
    Me.picSkinPreview.Name = "picSkinPreview"
    Me.picSkinPreview.Size = New System.Drawing.Size(434, 30)
    Me.picSkinPreview.TabIndex = 1
    Me.picSkinPreview.TabStop = False
    '
    'grpTooltips
    '
    Me.grpTooltips.Controls.Add(Me.chkAutohide_IgnoreMaximizedState)
    Me.grpTooltips.Controls.Add(Me.nudSkinBGOpacity)
    Me.grpTooltips.Controls.Add(Me.chkOverrideSkinBackgroundOpacity)
    Me.grpTooltips.Controls.Add(Me.chkEnableTooltipFade)
    Me.grpTooltips.Location = New System.Drawing.Point(0, 0)
    Me.grpTooltips.Name = "grpTooltips"
    Me.grpTooltips.Size = New System.Drawing.Size(433, 96)
    Me.grpTooltips.TabIndex = 0
    Me.grpTooltips.TabStop = False
    Me.grpTooltips.Text = "Visual Effects"
    '
    'chkAutohide_IgnoreMaximizedState
    '
    Me.chkAutohide_IgnoreMaximizedState.AutoSize = True
    Me.chkAutohide_IgnoreMaximizedState.Location = New System.Drawing.Point(11, 69)
    Me.chkAutohide_IgnoreMaximizedState.Name = "chkAutohide_IgnoreMaximizedState"
    Me.chkAutohide_IgnoreMaximizedState.Size = New System.Drawing.Size(193, 17)
    Me.chkAutohide_IgnoreMaximizedState.TabIndex = 11
    Me.chkAutohide_IgnoreMaximizedState.Text = "Ignore Maximized Windows (Vista+)"
    Me.chkAutohide_IgnoreMaximizedState.UseVisualStyleBackColor = True
    '
    'nudSkinBGOpacity
    '
    Me.nudSkinBGOpacity.Location = New System.Drawing.Point(204, 44)
    Me.nudSkinBGOpacity.Margin = New System.Windows.Forms.Padding(0)
    Me.nudSkinBGOpacity.Maximum = New Decimal(New Integer() {255, 0, 0, 0})
    Me.nudSkinBGOpacity.Name = "nudSkinBGOpacity"
    Me.nudSkinBGOpacity.Size = New System.Drawing.Size(46, 21)
    Me.nudSkinBGOpacity.TabIndex = 4
    Me.nudSkinBGOpacity.Value = New Decimal(New Integer() {255, 0, 0, 0})
    '
    'chkOverrideSkinBackgroundOpacity
    '
    Me.chkOverrideSkinBackgroundOpacity.AutoSize = True
    Me.chkOverrideSkinBackgroundOpacity.Location = New System.Drawing.Point(11, 45)
    Me.chkOverrideSkinBackgroundOpacity.Margin = New System.Windows.Forms.Padding(0, 0, 0, 4)
    Me.chkOverrideSkinBackgroundOpacity.Name = "chkOverrideSkinBackgroundOpacity"
    Me.chkOverrideSkinBackgroundOpacity.Size = New System.Drawing.Size(193, 17)
    Me.chkOverrideSkinBackgroundOpacity.TabIndex = 3
    Me.chkOverrideSkinBackgroundOpacity.Text = "Override Skin Background Opacity:"
    Me.chkOverrideSkinBackgroundOpacity.UseVisualStyleBackColor = True
    '
    'chkEnableTooltipFade
    '
    Me.chkEnableTooltipFade.AutoSize = True
    Me.chkEnableTooltipFade.Location = New System.Drawing.Point(11, 20)
    Me.chkEnableTooltipFade.Margin = New System.Windows.Forms.Padding(0, 0, 0, 4)
    Me.chkEnableTooltipFade.Name = "chkEnableTooltipFade"
    Me.chkEnableTooltipFade.Size = New System.Drawing.Size(124, 17)
    Me.chkEnableTooltipFade.TabIndex = 2
    Me.chkEnableTooltipFade.Text = "Fade Tooltips In/Out"
    Me.chkEnableTooltipFade.UseVisualStyleBackColor = True
    '
    'lblAutohideDelay
    '
    Me.lblAutohideDelay.AutoSize = True
    Me.lblAutohideDelay.Location = New System.Drawing.Point(8, 49)
    Me.lblAutohideDelay.Margin = New System.Windows.Forms.Padding(0, 10, 0, 0)
    Me.lblAutohideDelay.Name = "lblAutohideDelay"
    Me.lblAutohideDelay.Size = New System.Drawing.Size(84, 13)
    Me.lblAutohideDelay.TabIndex = 8
    Me.lblAutohideDelay.Text = "Autohide Delay:"
    '
    'nudAutohideDelay
    '
    Me.nudAutohideDelay.Location = New System.Drawing.Point(95, 47)
    Me.nudAutohideDelay.Margin = New System.Windows.Forms.Padding(3, 8, 3, 3)
    Me.nudAutohideDelay.Maximum = New Decimal(New Integer() {99999999, 0, 0, 0})
    Me.nudAutohideDelay.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
    Me.nudAutohideDelay.Name = "nudAutohideDelay"
    Me.nudAutohideDelay.Size = New System.Drawing.Size(67, 21)
    Me.nudAutohideDelay.TabIndex = 9
    Me.nudAutohideDelay.Value = New Decimal(New Integer() {1, 0, 0, 0})
    '
    'lblAutohideDelayMS
    '
    Me.lblAutohideDelayMS.AutoSize = True
    Me.lblAutohideDelayMS.Location = New System.Drawing.Point(165, 49)
    Me.lblAutohideDelayMS.Margin = New System.Windows.Forms.Padding(0, 10, 0, 0)
    Me.lblAutohideDelayMS.Name = "lblAutohideDelayMS"
    Me.lblAutohideDelayMS.Size = New System.Drawing.Size(62, 13)
    Me.lblAutohideDelayMS.TabIndex = 10
    Me.lblAutohideDelayMS.Text = "milliseconds"
    Me.lblAutohideDelayMS.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
    '
    'chkAutoHideAnimation
    '
    Me.chkAutoHideAnimation.AutoSize = True
    Me.chkAutoHideAnimation.Location = New System.Drawing.Point(11, 22)
    Me.chkAutoHideAnimation.Margin = New System.Windows.Forms.Padding(0, 8, 0, 0)
    Me.chkAutoHideAnimation.Name = "chkAutoHideAnimation"
    Me.chkAutoHideAnimation.Size = New System.Drawing.Size(153, 17)
    Me.chkAutoHideAnimation.TabIndex = 5
    Me.chkAutoHideAnimation.Text = "Enable Autohide Animation"
    Me.chkAutoHideAnimation.UseVisualStyleBackColor = True
    '
    'lblAutohideAnimationSpeed
    '
    Me.lblAutohideAnimationSpeed.AutoSize = True
    Me.lblAutohideAnimationSpeed.Location = New System.Drawing.Point(8, 75)
    Me.lblAutohideAnimationSpeed.Margin = New System.Windows.Forms.Padding(0, 6, 0, 0)
    Me.lblAutohideAnimationSpeed.Name = "lblAutohideAnimationSpeed"
    Me.lblAutohideAnimationSpeed.Size = New System.Drawing.Size(137, 13)
    Me.lblAutohideAnimationSpeed.TabIndex = 6
    Me.lblAutohideAnimationSpeed.Text = "Autohide Animation Speed:"
    '
    'nudAutohideAnimationSpeed
    '
    Me.nudAutohideAnimationSpeed.Location = New System.Drawing.Point(148, 73)
    Me.nudAutohideAnimationSpeed.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
    Me.nudAutohideAnimationSpeed.Name = "nudAutohideAnimationSpeed"
    Me.nudAutohideAnimationSpeed.Size = New System.Drawing.Size(53, 21)
    Me.nudAutohideAnimationSpeed.TabIndex = 7
    Me.nudAutohideAnimationSpeed.Value = New Decimal(New Integer() {1, 0, 0, 0})
    '
    'grpVersionInfo
    '
    Me.grpVersionInfo.Controls.Add(Me.lvVersionInfo)
    Me.grpVersionInfo.Location = New System.Drawing.Point(0, 79)
    Me.grpVersionInfo.Name = "grpVersionInfo"
    Me.grpVersionInfo.Size = New System.Drawing.Size(433, 321)
    Me.grpVersionInfo.TabIndex = 0
    Me.grpVersionInfo.TabStop = False
    Me.grpVersionInfo.Text = "Version Information"
    '
    'lvVersionInfo
    '
    Me.lvVersionInfo.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.colhdrFile, Me.colhdrVersion})
    Me.lvVersionInfo.FullRowSelect = True
    Me.lvVersionInfo.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable
    Me.lvVersionInfo.Location = New System.Drawing.Point(9, 19)
    Me.lvVersionInfo.MultiSelect = False
    Me.lvVersionInfo.Name = "lvVersionInfo"
    Me.lvVersionInfo.ShowGroups = False
    Me.lvVersionInfo.Size = New System.Drawing.Size(414, 291)
    Me.lvVersionInfo.Sorting = System.Windows.Forms.SortOrder.Ascending
    Me.lvVersionInfo.TabIndex = 3
    Me.lvVersionInfo.UseCompatibleStateImageBehavior = False
    Me.lvVersionInfo.View = System.Windows.Forms.View.Details
    '
    'colhdrFile
    '
    Me.colhdrFile.Text = "File"
    Me.colhdrFile.Width = 185
    '
    'colhdrVersion
    '
    Me.colhdrVersion.Text = "Version"
    Me.colhdrVersion.Width = 91
    '
    'lblVersion
    '
    Me.lblVersion.AutoSize = True
    Me.lblVersion.Location = New System.Drawing.Point(82, 36)
    Me.lblVersion.Name = "lblVersion"
    Me.lblVersion.Size = New System.Drawing.Size(81, 13)
    Me.lblVersion.TabIndex = 0
    Me.lblVersion.Text = "Version 3.0.0.0"
    '
    'lblInfoBar
    '
    Me.lblInfoBar.AutoSize = True
    Me.lblInfoBar.Font = New System.Drawing.Font("Tahoma", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.lblInfoBar.Location = New System.Drawing.Point(80, 9)
    Me.lblInfoBar.Name = "lblInfoBar"
    Me.lblInfoBar.Size = New System.Drawing.Size(82, 23)
    Me.lblInfoBar.TabIndex = 0
    Me.lblInfoBar.Text = "InfoBar"
    '
    'picLogo
    '
    Me.picLogo.Image = Global.InfoBar.My.Resources.Resources.Logo
    Me.picLogo.Location = New System.Drawing.Point(9, 9)
    Me.picLogo.Name = "picLogo"
    Me.picLogo.Size = New System.Drawing.Size(64, 64)
    Me.picLogo.TabIndex = 0
    Me.picLogo.TabStop = False
    '
    'tvSettings
    '
    Me.tvSettings.FullRowSelect = True
    Me.tvSettings.HideSelection = False
    Me.tvSettings.HotTracking = True
    Me.tvSettings.Indent = 15
    Me.tvSettings.ItemHeight = 19
    Me.tvSettings.Location = New System.Drawing.Point(8, 8)
    Me.tvSettings.Margin = New System.Windows.Forms.Padding(0)
    Me.tvSettings.Name = "tvSettings"
    Me.tvSettings.ShowNodeToolTips = True
    Me.tvSettings.ShowRootLines = False
    Me.tvSettings.Size = New System.Drawing.Size(150, 400)
    Me.tvSettings.TabIndex = 1
    '
    'btnModules_InsertSeparator
    '
    Me.btnModules_InsertSeparator.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.btnModules_InsertSeparator.AutoSize = True
    Me.btnModules_InsertSeparator.Enabled = False
    Me.btnModules_InsertSeparator.Location = New System.Drawing.Point(255, 215)
    Me.btnModules_InsertSeparator.MinimumSize = New System.Drawing.Size(75, 23)
    Me.btnModules_InsertSeparator.Name = "btnModules_InsertSeparator"
    Me.btnModules_InsertSeparator.Size = New System.Drawing.Size(97, 23)
    Me.btnModules_InsertSeparator.TabIndex = 1002
    Me.btnModules_InsertSeparator.Text = "Insert Separator"
    '
    'btnModules_Delete
    '
    Me.btnModules_Delete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.btnModules_Delete.AutoSize = True
    Me.btnModules_Delete.Enabled = False
    Me.btnModules_Delete.Location = New System.Drawing.Point(358, 215)
    Me.btnModules_Delete.MinimumSize = New System.Drawing.Size(75, 23)
    Me.btnModules_Delete.Name = "btnModules_Delete"
    Me.btnModules_Delete.Size = New System.Drawing.Size(75, 23)
    Me.btnModules_Delete.TabIndex = 1001
    Me.btnModules_Delete.Text = "Delete"
    '
    'lvModules
    '
    Me.lvModules.AllowDrop = True
    Me.lvModules.CheckBoxes = True
    Me.lvModules.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.colhdrModule})
    Me.lvModules.FullRowSelect = True
    Me.lvModules.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None
    Me.lvModules.LabelWrap = False
    Me.lvModules.Location = New System.Drawing.Point(0, 0)
    Me.lvModules.Margin = New System.Windows.Forms.Padding(0)
    Me.lvModules.Name = "lvModules"
    Me.lvModules.OwnerDraw = True
    Me.lvModules.ShowGroups = False
    Me.lvModules.Size = New System.Drawing.Size(433, 209)
    Me.lvModules.SmallImageList = Me.imlModules
    Me.lvModules.TabIndex = 25
    Me.lvModules.UseCompatibleStateImageBehavior = False
    Me.lvModules.View = System.Windows.Forms.View.Details
    '
    'colhdrModule
    '
    Me.colhdrModule.Text = "Module"
    '
    'panelAbout
    '
    Me.panelAbout.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.panelAbout.Controls.Add(Me.grpVersionInfo)
    Me.panelAbout.Controls.Add(Me.picLogo)
    Me.panelAbout.Controls.Add(Me.lblInfoBar)
    Me.panelAbout.Controls.Add(Me.lblVersion)
    Me.panelAbout.Location = New System.Drawing.Point(166, 8)
    Me.panelAbout.Name = "panelAbout"
    Me.panelAbout.Size = New System.Drawing.Size(433, 400)
    Me.panelAbout.TabIndex = 0
    Me.panelAbout.Visible = False
    '
    'panelModuleSettings
    '
    Me.panelModuleSettings.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.panelModuleSettings.Location = New System.Drawing.Point(166, 8)
    Me.panelModuleSettings.Name = "panelModuleSettings"
    Me.panelModuleSettings.Size = New System.Drawing.Size(433, 400)
    Me.panelModuleSettings.TabIndex = 0
    Me.panelModuleSettings.Visible = False
    '
    'panelGeneral
    '
    Me.panelGeneral.Controls.Add(Me.grpGeneral_Startup)
    Me.panelGeneral.Controls.Add(Me.grpGeneral_Location)
    Me.panelGeneral.Controls.Add(Me.grpModuleAlignment)
    Me.panelGeneral.Location = New System.Drawing.Point(166, 8)
    Me.panelGeneral.Name = "panelGeneral"
    Me.panelGeneral.Size = New System.Drawing.Size(433, 400)
    Me.panelGeneral.TabIndex = 1003
    Me.panelGeneral.Visible = False
    '
    'grpGeneral_Startup
    '
    Me.grpGeneral_Startup.Controls.Add(Me.chkGeneral_Startup_RunAtWindowsStartup)
    Me.grpGeneral_Startup.Location = New System.Drawing.Point(0, 151)
    Me.grpGeneral_Startup.Name = "grpGeneral_Startup"
    Me.grpGeneral_Startup.Size = New System.Drawing.Size(433, 43)
    Me.grpGeneral_Startup.TabIndex = 1009
    Me.grpGeneral_Startup.TabStop = False
    Me.grpGeneral_Startup.Text = "Startup"
    '
    'chkGeneral_Startup_RunAtWindowsStartup
    '
    Me.chkGeneral_Startup_RunAtWindowsStartup.AutoSize = True
    Me.chkGeneral_Startup_RunAtWindowsStartup.Location = New System.Drawing.Point(11, 20)
    Me.chkGeneral_Startup_RunAtWindowsStartup.Name = "chkGeneral_Startup_RunAtWindowsStartup"
    Me.chkGeneral_Startup_RunAtWindowsStartup.Size = New System.Drawing.Size(177, 17)
    Me.chkGeneral_Startup_RunAtWindowsStartup.TabIndex = 5
    Me.chkGeneral_Startup_RunAtWindowsStartup.Text = "Run InfoBar at Windows startup"
    Me.chkGeneral_Startup_RunAtWindowsStartup.UseVisualStyleBackColor = True
    '
    'grpModuleAlignment
    '
    Me.grpModuleAlignment.Controls.Add(Me.radModuleAlignmentJustifyCenter)
    Me.grpModuleAlignment.Controls.Add(Me.radModuleAlignmentLeft)
    Me.grpModuleAlignment.Controls.Add(Me.radModuleAlignmentCenter)
    Me.grpModuleAlignment.Controls.Add(Me.chkShowSeparators)
    Me.grpModuleAlignment.Controls.Add(Me.radModuleAlignmentRight)
    Me.grpModuleAlignment.Controls.Add(Me.radModuleAlignmentJustify)
    Me.grpModuleAlignment.Location = New System.Drawing.Point(0, 79)
    Me.grpModuleAlignment.Name = "grpModuleAlignment"
    Me.grpModuleAlignment.Size = New System.Drawing.Size(433, 66)
    Me.grpModuleAlignment.TabIndex = 1010
    Me.grpModuleAlignment.TabStop = False
    Me.grpModuleAlignment.Text = "Module Alignment and Separation"
    '
    'radModuleAlignmentJustifyCenter
    '
    Me.radModuleAlignmentJustifyCenter.AutoSize = True
    Me.radModuleAlignmentJustifyCenter.Location = New System.Drawing.Point(244, 17)
    Me.radModuleAlignmentJustifyCenter.Name = "radModuleAlignmentJustifyCenter"
    Me.radModuleAlignmentJustifyCenter.Size = New System.Drawing.Size(88, 17)
    Me.radModuleAlignmentJustifyCenter.TabIndex = 5
    Me.radModuleAlignmentJustifyCenter.Text = "Justify Center"
    Me.radModuleAlignmentJustifyCenter.UseVisualStyleBackColor = True
    '
    'radModuleAlignmentLeft
    '
    Me.radModuleAlignmentLeft.AutoSize = True
    Me.radModuleAlignmentLeft.Checked = True
    Me.radModuleAlignmentLeft.Location = New System.Drawing.Point(11, 17)
    Me.radModuleAlignmentLeft.Name = "radModuleAlignmentLeft"
    Me.radModuleAlignmentLeft.Size = New System.Drawing.Size(43, 17)
    Me.radModuleAlignmentLeft.TabIndex = 0
    Me.radModuleAlignmentLeft.TabStop = True
    Me.radModuleAlignmentLeft.Text = "Left"
    Me.radModuleAlignmentLeft.UseVisualStyleBackColor = True
    '
    'radModuleAlignmentCenter
    '
    Me.radModuleAlignmentCenter.AutoSize = True
    Me.radModuleAlignmentCenter.Location = New System.Drawing.Point(61, 17)
    Me.radModuleAlignmentCenter.Name = "radModuleAlignmentCenter"
    Me.radModuleAlignmentCenter.Size = New System.Drawing.Size(56, 17)
    Me.radModuleAlignmentCenter.TabIndex = 1
    Me.radModuleAlignmentCenter.Text = "Center"
    Me.radModuleAlignmentCenter.UseVisualStyleBackColor = True
    '
    'chkShowSeparators
    '
    Me.chkShowSeparators.AutoSize = True
    Me.chkShowSeparators.Location = New System.Drawing.Point(11, 40)
    Me.chkShowSeparators.Name = "chkShowSeparators"
    Me.chkShowSeparators.Size = New System.Drawing.Size(107, 17)
    Me.chkShowSeparators.TabIndex = 4
    Me.chkShowSeparators.Text = "Show Separators"
    Me.chkShowSeparators.UseVisualStyleBackColor = True
    '
    'radModuleAlignmentRight
    '
    Me.radModuleAlignmentRight.AutoSize = True
    Me.radModuleAlignmentRight.Location = New System.Drawing.Point(125, 17)
    Me.radModuleAlignmentRight.Name = "radModuleAlignmentRight"
    Me.radModuleAlignmentRight.Size = New System.Drawing.Size(50, 17)
    Me.radModuleAlignmentRight.TabIndex = 2
    Me.radModuleAlignmentRight.Text = "Right"
    Me.radModuleAlignmentRight.UseVisualStyleBackColor = True
    '
    'radModuleAlignmentJustify
    '
    Me.radModuleAlignmentJustify.AutoSize = True
    Me.radModuleAlignmentJustify.Location = New System.Drawing.Point(181, 17)
    Me.radModuleAlignmentJustify.Name = "radModuleAlignmentJustify"
    Me.radModuleAlignmentJustify.Size = New System.Drawing.Size(54, 17)
    Me.radModuleAlignmentJustify.TabIndex = 3
    Me.radModuleAlignmentJustify.Text = "Justify"
    Me.radModuleAlignmentJustify.UseVisualStyleBackColor = True
    '
    'panelModules
    '
    Me.panelModules.Controls.Add(Me.btnModules_InsertSeparator)
    Me.panelModules.Controls.Add(Me.btnModules_Delete)
    Me.panelModules.Controls.Add(Me.grpModuleInfo)
    Me.panelModules.Controls.Add(Me.lvModules)
    Me.panelModules.Location = New System.Drawing.Point(166, 8)
    Me.panelModules.Margin = New System.Windows.Forms.Padding(0)
    Me.panelModules.Name = "panelModules"
    Me.panelModules.Size = New System.Drawing.Size(433, 400)
    Me.panelModules.TabIndex = 1004
    Me.panelModules.Visible = False
    '
    'panelSkins
    '
    Me.panelSkins.Controls.Add(Me.btnSkins_RefreshList)
    Me.panelSkins.Controls.Add(Me.btnSkins_VisitAuthorWebsite)
    Me.panelSkins.Controls.Add(Me.picSkinPreview)
    Me.panelSkins.Controls.Add(Me.lvSkins)
    Me.panelSkins.Location = New System.Drawing.Point(166, 8)
    Me.panelSkins.Name = "panelSkins"
    Me.panelSkins.Size = New System.Drawing.Size(433, 400)
    Me.panelSkins.TabIndex = 1005
    Me.panelSkins.Visible = False
    '
    'panelIcons
    '
    Me.panelIcons.Controls.Add(Me.btnIconsVisitWebsite)
    Me.panelIcons.Controls.Add(Me.btnIconsRefreshList)
    Me.panelIcons.Controls.Add(Me.picIconsPreview)
    Me.panelIcons.Controls.Add(Me.lvIcons)
    Me.panelIcons.Location = New System.Drawing.Point(166, 8)
    Me.panelIcons.Name = "panelIcons"
    Me.panelIcons.Size = New System.Drawing.Size(433, 400)
    Me.panelIcons.TabIndex = 1006
    Me.panelIcons.Visible = False
    '
    'btnIconsVisitWebsite
    '
    Me.btnIconsVisitWebsite.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.btnIconsVisitWebsite.AutoSize = True
    Me.btnIconsVisitWebsite.Location = New System.Drawing.Point(168, 377)
    Me.btnIconsVisitWebsite.MinimumSize = New System.Drawing.Size(75, 23)
    Me.btnIconsVisitWebsite.Name = "btnIconsVisitWebsite"
    Me.btnIconsVisitWebsite.Size = New System.Drawing.Size(184, 23)
    Me.btnIconsVisitWebsite.TabIndex = 3
    Me.btnIconsVisitWebsite.Text = "Visit Icon Theme Author's Website"
    '
    'btnIconsRefreshList
    '
    Me.btnIconsRefreshList.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.btnIconsRefreshList.AutoSize = True
    Me.btnIconsRefreshList.Location = New System.Drawing.Point(358, 377)
    Me.btnIconsRefreshList.MinimumSize = New System.Drawing.Size(75, 23)
    Me.btnIconsRefreshList.Name = "btnIconsRefreshList"
    Me.btnIconsRefreshList.Size = New System.Drawing.Size(75, 23)
    Me.btnIconsRefreshList.TabIndex = 4
    Me.btnIconsRefreshList.Text = "Refresh List"
    '
    'picIconsPreview
    '
    Me.picIconsPreview.Location = New System.Drawing.Point(0, 0)
    Me.picIconsPreview.Margin = New System.Windows.Forms.Padding(0)
    Me.picIconsPreview.Name = "picIconsPreview"
    Me.picIconsPreview.Size = New System.Drawing.Size(433, 30)
    Me.picIconsPreview.TabIndex = 1
    Me.picIconsPreview.TabStop = False
    '
    'lvIcons
    '
    Me.lvIcons.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader1, Me.ColumnHeader2, Me.ColumnHeader3, Me.ColumnHeader4})
    Me.lvIcons.FullRowSelect = True
    Me.lvIcons.HideSelection = False
    Me.lvIcons.Location = New System.Drawing.Point(0, 36)
    Me.lvIcons.Margin = New System.Windows.Forms.Padding(0, 4, 0, 4)
    Me.lvIcons.MultiSelect = False
    Me.lvIcons.Name = "lvIcons"
    Me.lvIcons.ShowGroups = False
    Me.lvIcons.ShowItemToolTips = True
    Me.lvIcons.Size = New System.Drawing.Size(433, 334)
    Me.lvIcons.TabIndex = 2
    Me.lvIcons.UseCompatibleStateImageBehavior = False
    Me.lvIcons.View = System.Windows.Forms.View.Details
    '
    'ColumnHeader1
    '
    Me.ColumnHeader1.Text = "Name"
    Me.ColumnHeader1.Width = 91
    '
    'ColumnHeader2
    '
    Me.ColumnHeader2.Text = "Author"
    Me.ColumnHeader2.Width = 90
    '
    'ColumnHeader3
    '
    Me.ColumnHeader3.Text = "Version"
    Me.ColumnHeader3.Width = 90
    '
    'ColumnHeader4
    '
    Me.ColumnHeader4.Text = "Website"
    Me.ColumnHeader4.Width = 115
    '
    'panelAdvanced
    '
    Me.panelAdvanced.Controls.Add(Me.grpAutoHideAdvanced)
    Me.panelAdvanced.Controls.Add(Me.grpFileAssociations)
    Me.panelAdvanced.Controls.Add(Me.grpTooltips)
    Me.panelAdvanced.Location = New System.Drawing.Point(166, 8)
    Me.panelAdvanced.Name = "panelAdvanced"
    Me.panelAdvanced.Size = New System.Drawing.Size(433, 400)
    Me.panelAdvanced.TabIndex = 1007
    Me.panelAdvanced.Visible = False
    '
    'grpAutoHideAdvanced
    '
    Me.grpAutoHideAdvanced.Controls.Add(Me.chkAutoHideAnimation)
    Me.grpAutoHideAdvanced.Controls.Add(Me.nudAutohideAnimationSpeed)
    Me.grpAutoHideAdvanced.Controls.Add(Me.lblAutohideDelay)
    Me.grpAutoHideAdvanced.Controls.Add(Me.lblAutohideAnimationSpeed)
    Me.grpAutoHideAdvanced.Controls.Add(Me.nudAutohideDelay)
    Me.grpAutoHideAdvanced.Controls.Add(Me.lblAutohideDelayMS)
    Me.grpAutoHideAdvanced.Location = New System.Drawing.Point(0, 102)
    Me.grpAutoHideAdvanced.Name = "grpAutoHideAdvanced"
    Me.grpAutoHideAdvanced.Size = New System.Drawing.Size(433, 107)
    Me.grpAutoHideAdvanced.TabIndex = 1010
    Me.grpAutoHideAdvanced.TabStop = False
    Me.grpAutoHideAdvanced.Text = "Autohide Settings"
    '
    'grpFileAssociations
    '
    Me.grpFileAssociations.Controls.Add(Me.btnRepairFileAssociations)
    Me.grpFileAssociations.Controls.Add(Me.lblFileAssociationsInfo)
    Me.grpFileAssociations.Controls.Add(Me.lblFileAssociationStatus)
    Me.grpFileAssociations.Location = New System.Drawing.Point(0, 215)
    Me.grpFileAssociations.Name = "grpFileAssociations"
    Me.grpFileAssociations.Size = New System.Drawing.Size(433, 82)
    Me.grpFileAssociations.TabIndex = 1009
    Me.grpFileAssociations.TabStop = False
    Me.grpFileAssociations.Text = "File Associations"
    '
    'btnRepairFileAssociations
    '
    Me.btnRepairFileAssociations.Anchor = System.Windows.Forms.AnchorStyles.Right
    Me.btnRepairFileAssociations.AutoSize = True
    Me.btnRepairFileAssociations.Enabled = False
    Me.btnRepairFileAssociations.Location = New System.Drawing.Point(296, 46)
    Me.btnRepairFileAssociations.Margin = New System.Windows.Forms.Padding(0)
    Me.btnRepairFileAssociations.MinimumSize = New System.Drawing.Size(75, 23)
    Me.btnRepairFileAssociations.Name = "btnRepairFileAssociations"
    Me.btnRepairFileAssociations.Size = New System.Drawing.Size(129, 23)
    Me.btnRepairFileAssociations.TabIndex = 5
    Me.btnRepairFileAssociations.Text = "Repair File Associations"
    Me.btnRepairFileAssociations.UseVisualStyleBackColor = True
    '
    'lblFileAssociationsInfo
    '
    Me.lblFileAssociationsInfo.AutoSize = True
    Me.lblFileAssociationsInfo.Location = New System.Drawing.Point(8, 23)
    Me.lblFileAssociationsInfo.Margin = New System.Windows.Forms.Padding(0, 0, 0, 12)
    Me.lblFileAssociationsInfo.Name = "lblFileAssociationsInfo"
    Me.lblFileAssociationsInfo.Size = New System.Drawing.Size(375, 13)
    Me.lblFileAssociationsInfo.TabIndex = 0
    Me.lblFileAssociationsInfo.Text = "If any of InfoBar's file associations are damaged, they can be repaired here."
    '
    'lblFileAssociationStatus
    '
    Me.lblFileAssociationStatus.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.lblFileAssociationStatus.AutoSize = True
    Me.lblFileAssociationStatus.Location = New System.Drawing.Point(8, 51)
    Me.lblFileAssociationStatus.Margin = New System.Windows.Forms.Padding(0)
    Me.lblFileAssociationStatus.Name = "lblFileAssociationStatus"
    Me.lblFileAssociationStatus.Size = New System.Drawing.Size(63, 13)
    Me.lblFileAssociationStatus.TabIndex = 0
    Me.lblFileAssociationStatus.Text = "Status: OK."
    Me.lblFileAssociationStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
    '
    'hrButtons
    '
    Me.hrButtons.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.hrButtons.Location = New System.Drawing.Point(8, 420)
    Me.hrButtons.Name = "hrButtons"
    Me.hrButtons.Size = New System.Drawing.Size(592, 2)
    Me.hrButtons.TabIndex = 0
    Me.hrButtons.TabStop = False
    '
    'frmSettings
    '
    Me.AcceptButton = Me.btnOK
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
    Me.AutoSize = True
    Me.BackColor = System.Drawing.SystemColors.Control
    Me.CancelButton = Me.btnCancel
    Me.ClientSize = New System.Drawing.Size(610, 468)
    Me.Controls.Add(Me.panelAbout)
    Me.Controls.Add(Me.panelGeneral)
    Me.Controls.Add(Me.panelIcons)
    Me.Controls.Add(Me.panelModules)
    Me.Controls.Add(Me.panelSkins)
    Me.Controls.Add(Me.btnApply)
    Me.Controls.Add(Me.btnCancel)
    Me.Controls.Add(Me.btnOK)
    Me.Controls.Add(Me.hrButtons)
    Me.Controls.Add(Me.tvSettings)
    Me.Controls.Add(Me.panelModuleSettings)
    Me.Controls.Add(Me.panelAdvanced)
    Me.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
    Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
    Me.MaximizeBox = False
    Me.MinimumSize = New System.Drawing.Size(600, 400)
    Me.Name = "frmSettings"
    Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
    Me.Text = "InfoBar Settings"
    Me.grpGeneral_Location.ResumeLayout(False)
    Me.grpGeneral_Location.PerformLayout()
    Me.grpModuleInfo.ResumeLayout(False)
    Me.tlpModuleDetails.ResumeLayout(False)
    Me.tlpModuleDetails.PerformLayout()
    CType(Me.picSkinPreview, System.ComponentModel.ISupportInitialize).EndInit()
    Me.grpTooltips.ResumeLayout(False)
    Me.grpTooltips.PerformLayout()
    CType(Me.nudSkinBGOpacity, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.nudAutohideDelay, System.ComponentModel.ISupportInitialize).EndInit()
    CType(Me.nudAutohideAnimationSpeed, System.ComponentModel.ISupportInitialize).EndInit()
    Me.grpVersionInfo.ResumeLayout(False)
    CType(Me.picLogo, System.ComponentModel.ISupportInitialize).EndInit()
    Me.panelAbout.ResumeLayout(False)
    Me.panelAbout.PerformLayout()
    Me.panelGeneral.ResumeLayout(False)
    Me.grpGeneral_Startup.ResumeLayout(False)
    Me.grpGeneral_Startup.PerformLayout()
    Me.grpModuleAlignment.ResumeLayout(False)
    Me.grpModuleAlignment.PerformLayout()
    Me.panelModules.ResumeLayout(False)
    Me.panelModules.PerformLayout()
    Me.panelSkins.ResumeLayout(False)
    Me.panelSkins.PerformLayout()
    Me.panelIcons.ResumeLayout(False)
    Me.panelIcons.PerformLayout()
    CType(Me.picIconsPreview, System.ComponentModel.ISupportInitialize).EndInit()
    Me.panelAdvanced.ResumeLayout(False)
    Me.grpAutoHideAdvanced.ResumeLayout(False)
    Me.grpAutoHideAdvanced.PerformLayout()
    Me.grpFileAssociations.ResumeLayout(False)
    Me.grpFileAssociations.PerformLayout()
    Me.ResumeLayout(False)
    Me.PerformLayout()

  End Sub
  Friend WithEvents btnOK As System.Windows.Forms.Button
  Friend WithEvents btnCancel As System.Windows.Forms.Button
  Friend WithEvents btnApply As System.Windows.Forms.Button
  Friend WithEvents grpGeneral_Location As System.Windows.Forms.GroupBox
  Friend WithEvents chkGeneral_Location_AlwaysOnTop As System.Windows.Forms.CheckBox
  Friend WithEvents radGeneral_Location_Bottom As System.Windows.Forms.RadioButton
  Friend WithEvents radGeneral_Location_Top As System.Windows.Forms.RadioButton
  Friend WithEvents lvSkins As System.Windows.Forms.ListView
  Friend WithEvents colhdrSkins_Name As System.Windows.Forms.ColumnHeader
  Friend WithEvents colhdrSkins_Author As System.Windows.Forms.ColumnHeader
  Friend WithEvents colhdrSkins_Version As System.Windows.Forms.ColumnHeader
  Friend WithEvents colhdrSkins_Website As System.Windows.Forms.ColumnHeader
  Friend WithEvents picSkinPreview As System.Windows.Forms.PictureBox
  Friend WithEvents picLogo As System.Windows.Forms.PictureBox
  Friend WithEvents lblVersion As System.Windows.Forms.Label
  Friend WithEvents lblInfoBar As System.Windows.Forms.Label
  Friend WithEvents grpVersionInfo As System.Windows.Forms.GroupBox
  Friend WithEvents lvVersionInfo As System.Windows.Forms.ListView
  Friend WithEvents colhdrFile As System.Windows.Forms.ColumnHeader
  Friend WithEvents colhdrVersion As System.Windows.Forms.ColumnHeader
  Friend WithEvents grpModuleInfo As System.Windows.Forms.GroupBox
  Friend WithEvents lblModules1 As System.Windows.Forms.Label
  Friend WithEvents lblModules4 As System.Windows.Forms.Label
  Friend WithEvents lblModules3 As System.Windows.Forms.Label
  Friend WithEvents lblModules2 As System.Windows.Forms.Label
  Friend WithEvents txtModuleAuthor As System.Windows.Forms.TextBox
  Friend WithEvents txtModuleDescription As System.Windows.Forms.TextBox
  Friend WithEvents lblModuleEmail As System.Windows.Forms.LinkLabel
  Friend WithEvents lblModuleHomepage As System.Windows.Forms.LinkLabel
  Friend WithEvents btnSkins_RefreshList As System.Windows.Forms.Button
  Friend WithEvents btnSkins_VisitAuthorWebsite As System.Windows.Forms.Button
  Friend WithEvents imlModules As System.Windows.Forms.ImageList
  Friend WithEvents grpTooltips As System.Windows.Forms.GroupBox
  Friend WithEvents chkEnableTooltipFade As System.Windows.Forms.CheckBox
  Friend WithEvents chkOverrideSkinBackgroundOpacity As System.Windows.Forms.CheckBox
  Friend WithEvents nudSkinBGOpacity As System.Windows.Forms.NumericUpDown
  Friend WithEvents tvSettings As System.Windows.Forms.TreeView
  Friend WithEvents panelAbout As System.Windows.Forms.Panel
  Friend WithEvents panelModuleSettings As System.Windows.Forms.Panel
  Friend WithEvents hrButtons As InfoBar.Controls.HorizontalRule
  Friend WithEvents tlpModuleDetails As System.Windows.Forms.TableLayoutPanel
  Friend WithEvents lblModules6 As System.Windows.Forms.Label
  Friend WithEvents txtModuleVersion As System.Windows.Forms.TextBox
  Friend WithEvents lblModules5 As System.Windows.Forms.Label
  Friend WithEvents txtModuleGUID As System.Windows.Forms.TextBox
  Friend WithEvents lvModules As System.Windows.Forms.ListView
  Friend WithEvents colhdrModule As System.Windows.Forms.ColumnHeader
  Friend WithEvents btnModules_InsertSeparator As System.Windows.Forms.Button
  Friend WithEvents btnModules_Delete As System.Windows.Forms.Button
  Friend WithEvents panelGeneral As System.Windows.Forms.Panel
  Friend WithEvents panelModules As System.Windows.Forms.Panel
  Friend WithEvents panelSkins As System.Windows.Forms.Panel
  Friend WithEvents panelIcons As System.Windows.Forms.Panel
  Friend WithEvents picIconsPreview As System.Windows.Forms.PictureBox
  Friend WithEvents lvIcons As System.Windows.Forms.ListView
  Friend WithEvents ColumnHeader1 As System.Windows.Forms.ColumnHeader
  Friend WithEvents ColumnHeader2 As System.Windows.Forms.ColumnHeader
  Friend WithEvents ColumnHeader3 As System.Windows.Forms.ColumnHeader
  Friend WithEvents ColumnHeader4 As System.Windows.Forms.ColumnHeader
  Friend WithEvents btnIconsVisitWebsite As System.Windows.Forms.Button
  Friend WithEvents btnIconsRefreshList As System.Windows.Forms.Button
  Friend WithEvents panelAdvanced As System.Windows.Forms.Panel
  Friend WithEvents grpGeneral_Startup As System.Windows.Forms.GroupBox
  Friend WithEvents chkGeneral_Startup_RunAtWindowsStartup As System.Windows.Forms.CheckBox
  Friend WithEvents grpFileAssociations As System.Windows.Forms.GroupBox
  Friend WithEvents lblFileAssociationsInfo As System.Windows.Forms.Label
  Friend WithEvents btnRepairFileAssociations As System.Windows.Forms.Button
  Friend WithEvents lblFileAssociationStatus As System.Windows.Forms.Label
  Friend WithEvents grpModuleAlignment As System.Windows.Forms.GroupBox
  Friend WithEvents radModuleAlignmentLeft As System.Windows.Forms.RadioButton
  Friend WithEvents radModuleAlignmentCenter As System.Windows.Forms.RadioButton
  Friend WithEvents radModuleAlignmentRight As System.Windows.Forms.RadioButton
  Friend WithEvents radModuleAlignmentJustify As System.Windows.Forms.RadioButton
  Friend WithEvents chkAutoHideAnimation As System.Windows.Forms.CheckBox
  Friend WithEvents lblAutohideAnimationSpeed As System.Windows.Forms.Label
  Friend WithEvents nudAutohideAnimationSpeed As System.Windows.Forms.NumericUpDown
  Friend WithEvents lblAutohideDelay As System.Windows.Forms.Label
  Friend WithEvents nudAutohideDelay As System.Windows.Forms.NumericUpDown
  Friend WithEvents lblAutohideDelayMS As System.Windows.Forms.Label
  Friend WithEvents chkAutohide As System.Windows.Forms.CheckBox
  Friend WithEvents chkShowSeparators As System.Windows.Forms.CheckBox
  Friend WithEvents chkDisableToolbarDocking As System.Windows.Forms.CheckBox
  Friend WithEvents chkAutohide_IgnoreMaximizedState As System.Windows.Forms.CheckBox
  Friend WithEvents grpAutoHideAdvanced As System.Windows.Forms.GroupBox
  Friend WithEvents radModuleAlignmentJustifyCenter As System.Windows.Forms.RadioButton
  Friend WithEvents radGeneral_Location_Right As System.Windows.Forms.RadioButton
  Friend WithEvents radGeneral_Location_Left As System.Windows.Forms.RadioButton
End Class
