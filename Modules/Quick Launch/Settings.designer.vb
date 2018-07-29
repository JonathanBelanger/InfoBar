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
    Me.tagpageGeneral = New System.Windows.Forms.TabPage
    Me.grpButtonAppearance = New System.Windows.Forms.GroupBox
    Me.radButtonAppearance_ShowIconsOnly = New System.Windows.Forms.RadioButton
    Me.radButtonAppearance_ShowTextOnly = New System.Windows.Forms.RadioButton
    Me.radButtonAppearance_ShowIconsAndText = New System.Windows.Forms.RadioButton
    Me.radButtonAppearanceIconsFilesIconsAndTextFolders = New System.Windows.Forms.RadioButton
    Me.grpMisc = New System.Windows.Forms.GroupBox
    Me.chkShowPathInTooltip = New System.Windows.Forms.CheckBox
    Me.chkShowFoldersAsDropDownMenus = New System.Windows.Forms.CheckBox
    Me.Tabs.SuspendLayout()
    Me.tagpageGeneral.SuspendLayout()
    Me.grpButtonAppearance.SuspendLayout()
    Me.grpMisc.SuspendLayout()
    Me.SuspendLayout()
    '
    'Tabs
    '
    Me.Tabs.Controls.Add(Me.tagpageGeneral)
    Me.Tabs.Dock = System.Windows.Forms.DockStyle.Fill
    Me.Tabs.Location = New System.Drawing.Point(0, 0)
    Me.Tabs.Name = "Tabs"
    Me.Tabs.SelectedIndex = 0
    Me.Tabs.Size = New System.Drawing.Size(500, 400)
    Me.Tabs.TabIndex = 2
    '
    'tagpageGeneral
    '
    Me.tagpageGeneral.Controls.Add(Me.grpMisc)
    Me.tagpageGeneral.Controls.Add(Me.grpButtonAppearance)
    Me.tagpageGeneral.Location = New System.Drawing.Point(4, 22)
    Me.tagpageGeneral.Margin = New System.Windows.Forms.Padding(0)
    Me.tagpageGeneral.Name = "tagpageGeneral"
    Me.tagpageGeneral.Padding = New System.Windows.Forms.Padding(8)
    Me.tagpageGeneral.Size = New System.Drawing.Size(492, 374)
    Me.tagpageGeneral.TabIndex = 0
    Me.tagpageGeneral.Text = "General"
    Me.tagpageGeneral.UseVisualStyleBackColor = True
    '
    'grpButtonAppearance
    '
    Me.grpButtonAppearance.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.grpButtonAppearance.Controls.Add(Me.radButtonAppearance_ShowIconsOnly)
    Me.grpButtonAppearance.Controls.Add(Me.radButtonAppearance_ShowTextOnly)
    Me.grpButtonAppearance.Controls.Add(Me.radButtonAppearanceIconsFilesIconsAndTextFolders)
    Me.grpButtonAppearance.Controls.Add(Me.radButtonAppearance_ShowIconsAndText)
    Me.grpButtonAppearance.Location = New System.Drawing.Point(8, 8)
    Me.grpButtonAppearance.Name = "grpButtonAppearance"
    Me.grpButtonAppearance.Padding = New System.Windows.Forms.Padding(8, 4, 8, 8)
    Me.grpButtonAppearance.Size = New System.Drawing.Size(476, 95)
    Me.grpButtonAppearance.TabIndex = 0
    Me.grpButtonAppearance.TabStop = False
    Me.grpButtonAppearance.Text = "Button Appearance"
    '
    'radButtonAppearance_ShowIconsOnly
    '
    Me.radButtonAppearance_ShowIconsOnly.AutoSize = True
    Me.radButtonAppearance_ShowIconsOnly.Location = New System.Drawing.Point(8, 19)
    Me.radButtonAppearance_ShowIconsOnly.Margin = New System.Windows.Forms.Padding(0)
    Me.radButtonAppearance_ShowIconsOnly.Name = "radButtonAppearance_ShowIconsOnly"
    Me.radButtonAppearance_ShowIconsOnly.Size = New System.Drawing.Size(111, 17)
    Me.radButtonAppearance_ShowIconsOnly.TabIndex = 0
    Me.radButtonAppearance_ShowIconsOnly.TabStop = True
    Me.radButtonAppearance_ShowIconsOnly.Text = "Show Icons Only"
    Me.radButtonAppearance_ShowIconsOnly.UseVisualStyleBackColor = True
    '
    'radButtonAppearance_ShowTextOnly
    '
    Me.radButtonAppearance_ShowTextOnly.AutoSize = True
    Me.radButtonAppearance_ShowTextOnly.Location = New System.Drawing.Point(8, 36)
    Me.radButtonAppearance_ShowTextOnly.Margin = New System.Windows.Forms.Padding(0)
    Me.radButtonAppearance_ShowTextOnly.Name = "radButtonAppearance_ShowTextOnly"
    Me.radButtonAppearance_ShowTextOnly.Size = New System.Drawing.Size(104, 17)
    Me.radButtonAppearance_ShowTextOnly.TabIndex = 0
    Me.radButtonAppearance_ShowTextOnly.TabStop = True
    Me.radButtonAppearance_ShowTextOnly.Text = "Show Text Only"
    Me.radButtonAppearance_ShowTextOnly.UseVisualStyleBackColor = True
    '
    'radButtonAppearance_ShowIconsAndText
    '
    Me.radButtonAppearance_ShowIconsAndText.AutoSize = True
    Me.radButtonAppearance_ShowIconsAndText.Location = New System.Drawing.Point(8, 53)
    Me.radButtonAppearance_ShowIconsAndText.Margin = New System.Windows.Forms.Padding(0)
    Me.radButtonAppearance_ShowIconsAndText.Name = "radButtonAppearance_ShowIconsAndText"
    Me.radButtonAppearance_ShowIconsAndText.Size = New System.Drawing.Size(130, 17)
    Me.radButtonAppearance_ShowIconsAndText.TabIndex = 0
    Me.radButtonAppearance_ShowIconsAndText.TabStop = True
    Me.radButtonAppearance_ShowIconsAndText.Text = "Show Icons and Text"
    Me.radButtonAppearance_ShowIconsAndText.UseVisualStyleBackColor = True
    '
    'radButtonAppearanceIconsFilesIconsAndTextFolders
    '
    Me.radButtonAppearanceIconsFilesIconsAndTextFolders.AutoSize = True
    Me.radButtonAppearanceIconsFilesIconsAndTextFolders.Location = New System.Drawing.Point(8, 70)
    Me.radButtonAppearanceIconsFilesIconsAndTextFolders.Margin = New System.Windows.Forms.Padding(0)
    Me.radButtonAppearanceIconsFilesIconsAndTextFolders.Name = "radButtonAppearanceIconsFilesIconsAndTextFolders"
    Me.radButtonAppearanceIconsFilesIconsAndTextFolders.Size = New System.Drawing.Size(293, 17)
    Me.radButtonAppearanceIconsFilesIconsAndTextFolders.TabIndex = 0
    Me.radButtonAppearanceIconsFilesIconsAndTextFolders.TabStop = True
    Me.radButtonAppearanceIconsFilesIconsAndTextFolders.Text = "Show Icons Only for Files, Icons and Text for Folders"
    Me.radButtonAppearanceIconsFilesIconsAndTextFolders.UseVisualStyleBackColor = True
    '
    'grpMisc
    '
    Me.grpMisc.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.grpMisc.Controls.Add(Me.chkShowPathInTooltip)
    Me.grpMisc.Controls.Add(Me.chkShowFoldersAsDropDownMenus)
    Me.grpMisc.Location = New System.Drawing.Point(8, 109)
    Me.grpMisc.Name = "grpMisc"
    Me.grpMisc.Padding = New System.Windows.Forms.Padding(8, 4, 8, 8)
    Me.grpMisc.Size = New System.Drawing.Size(476, 65)
    Me.grpMisc.TabIndex = 1
    Me.grpMisc.TabStop = False
    Me.grpMisc.Text = "Other Settings"
    '
    'chkShowPathInTooltip
    '
    Me.chkShowPathInTooltip.AutoSize = True
    Me.chkShowPathInTooltip.Location = New System.Drawing.Point(8, 19)
    Me.chkShowPathInTooltip.Margin = New System.Windows.Forms.Padding(0, 0, 0, 4)
    Me.chkShowPathInTooltip.Name = "chkShowPathInTooltip"
    Me.chkShowPathInTooltip.Size = New System.Drawing.Size(206, 17)
    Me.chkShowPathInTooltip.TabIndex = 1
    Me.chkShowPathInTooltip.Text = "Show the item's path in the tooltip"
    Me.chkShowPathInTooltip.UseVisualStyleBackColor = True
    '
    'chkShowFoldersAsDropDownMenus
    '
    Me.chkShowFoldersAsDropDownMenus.AutoSize = True
    Me.chkShowFoldersAsDropDownMenus.Location = New System.Drawing.Point(8, 40)
    Me.chkShowFoldersAsDropDownMenus.Margin = New System.Windows.Forms.Padding(0)
    Me.chkShowFoldersAsDropDownMenus.Name = "chkShowFoldersAsDropDownMenus"
    Me.chkShowFoldersAsDropDownMenus.Size = New System.Drawing.Size(206, 17)
    Me.chkShowFoldersAsDropDownMenus.TabIndex = 1
    Me.chkShowFoldersAsDropDownMenus.Text = "Show folders as drop down menus"
    Me.chkShowFoldersAsDropDownMenus.UseVisualStyleBackColor = True
    '
    'Settings
    '
    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit
    Me.Controls.Add(Me.Tabs)
    Me.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.Name = "Settings"
    Me.Size = New System.Drawing.Size(500, 400)
    Me.Tabs.ResumeLayout(False)
    Me.tagpageGeneral.ResumeLayout(False)
    Me.grpButtonAppearance.ResumeLayout(False)
    Me.grpButtonAppearance.PerformLayout()
    Me.grpMisc.ResumeLayout(False)
    Me.grpMisc.PerformLayout()
    Me.ResumeLayout(False)

  End Sub
  Friend WithEvents Tabs As System.Windows.Forms.TabControl
  Friend WithEvents tagpageGeneral As System.Windows.Forms.TabPage
  Friend WithEvents grpButtonAppearance As System.Windows.Forms.GroupBox
  Friend WithEvents radButtonAppearance_ShowIconsOnly As System.Windows.Forms.RadioButton
  Friend WithEvents radButtonAppearance_ShowTextOnly As System.Windows.Forms.RadioButton
  Friend WithEvents radButtonAppearance_ShowIconsAndText As System.Windows.Forms.RadioButton
  Friend WithEvents radButtonAppearanceIconsFilesIconsAndTextFolders As System.Windows.Forms.RadioButton
  Friend WithEvents chkShowPathInTooltip As System.Windows.Forms.CheckBox
  Friend WithEvents grpMisc As System.Windows.Forms.GroupBox
  Friend WithEvents chkShowFoldersAsDropDownMenus As System.Windows.Forms.CheckBox
End Class
