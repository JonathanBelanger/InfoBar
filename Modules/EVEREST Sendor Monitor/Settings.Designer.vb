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
    Dim ListViewGroup1 As System.Windows.Forms.ListViewGroup = New System.Windows.Forms.ListViewGroup("Temperatures", System.Windows.Forms.HorizontalAlignment.Left)
    Dim ListViewGroup2 As System.Windows.Forms.ListViewGroup = New System.Windows.Forms.ListViewGroup("Cooling Fans", System.Windows.Forms.HorizontalAlignment.Left)
    Dim ListViewGroup3 As System.Windows.Forms.ListViewGroup = New System.Windows.Forms.ListViewGroup("Voltage Values", System.Windows.Forms.HorizontalAlignment.Left)
    Me.chkShowIcon = New System.Windows.Forms.CheckBox
    Me.chkShowText = New System.Windows.Forms.CheckBox
    Me.grpAppearance = New System.Windows.Forms.GroupBox
    Me.grpItems = New System.Windows.Forms.GroupBox
    Me.lvItems = New System.Windows.Forms.ListView
    Me.colhdrName = New System.Windows.Forms.ColumnHeader
    Me.colhdrValue = New System.Windows.Forms.ColumnHeader
    Me.colhdrEnabled = New System.Windows.Forms.ColumnHeader
    Me.colhdrText = New System.Windows.Forms.ColumnHeader
    Me.colhdrTooltip = New System.Windows.Forms.ColumnHeader
    Me.btnSelectAll = New System.Windows.Forms.Button
    Me.btnReset = New System.Windows.Forms.Button
    Me.btnSelectNone = New System.Windows.Forms.Button
    Me.grpAppearance.SuspendLayout()
    Me.grpItems.SuspendLayout()
    Me.SuspendLayout()
    '
    'chkShowIcon
    '
    Me.chkShowIcon.AutoSize = True
    Me.chkShowIcon.Location = New System.Drawing.Point(11, 20)
    Me.chkShowIcon.Name = "chkShowIcon"
    Me.chkShowIcon.Size = New System.Drawing.Size(76, 17)
    Me.chkShowIcon.TabIndex = 2
    Me.chkShowIcon.Text = "Show Icon"
    Me.chkShowIcon.UseVisualStyleBackColor = True
    '
    'chkShowText
    '
    Me.chkShowText.AutoSize = True
    Me.chkShowText.Location = New System.Drawing.Point(93, 20)
    Me.chkShowText.Name = "chkShowText"
    Me.chkShowText.Size = New System.Drawing.Size(77, 17)
    Me.chkShowText.TabIndex = 3
    Me.chkShowText.Text = "Show Text"
    Me.chkShowText.UseVisualStyleBackColor = True
    '
    'grpAppearance
    '
    Me.grpAppearance.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.grpAppearance.Controls.Add(Me.chkShowText)
    Me.grpAppearance.Controls.Add(Me.chkShowIcon)
    Me.grpAppearance.Location = New System.Drawing.Point(0, 0)
    Me.grpAppearance.Name = "grpAppearance"
    Me.grpAppearance.Size = New System.Drawing.Size(500, 44)
    Me.grpAppearance.TabIndex = 5
    Me.grpAppearance.TabStop = False
    Me.grpAppearance.Text = "Appearance Settings"
    '
    'grpItems
    '
    Me.grpItems.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.grpItems.Controls.Add(Me.lvItems)
    Me.grpItems.Controls.Add(Me.btnSelectAll)
    Me.grpItems.Controls.Add(Me.btnReset)
    Me.grpItems.Controls.Add(Me.btnSelectNone)
    Me.grpItems.Location = New System.Drawing.Point(0, 50)
    Me.grpItems.Name = "grpItems"
    Me.grpItems.Size = New System.Drawing.Size(500, 350)
    Me.grpItems.TabIndex = 6
    Me.grpItems.TabStop = False
    Me.grpItems.Text = "Monitoring Values"
    '
    'lvItems
    '
    Me.lvItems.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.lvItems.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.colhdrName, Me.colhdrValue, Me.colhdrEnabled, Me.colhdrText, Me.colhdrTooltip})
    Me.lvItems.FullRowSelect = True
    ListViewGroup1.Header = "Temperatures"
    ListViewGroup1.Name = "lvgTemps"
    ListViewGroup2.Header = "Cooling Fans"
    ListViewGroup2.Name = "lvgFans"
    ListViewGroup3.Header = "Voltage Values"
    ListViewGroup3.Name = "lvgVoltages"
    Me.lvItems.Groups.AddRange(New System.Windows.Forms.ListViewGroup() {ListViewGroup1, ListViewGroup2, ListViewGroup3})
    Me.lvItems.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable
    Me.lvItems.Location = New System.Drawing.Point(11, 20)
    Me.lvItems.MultiSelect = False
    Me.lvItems.Name = "lvItems"
    Me.lvItems.OwnerDraw = True
    Me.lvItems.Size = New System.Drawing.Size(479, 289)
    Me.lvItems.TabIndex = 0
    Me.lvItems.UseCompatibleStateImageBehavior = False
    Me.lvItems.View = System.Windows.Forms.View.Details
    '
    'colhdrName
    '
    Me.colhdrName.Text = "Name"
    Me.colhdrName.Width = 183
    '
    'colhdrValue
    '
    Me.colhdrValue.Text = "Sample Value"
    Me.colhdrValue.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
    Me.colhdrValue.Width = 76
    '
    'colhdrEnabled
    '
    Me.colhdrEnabled.Text = "Enabled"
    Me.colhdrEnabled.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
    Me.colhdrEnabled.Width = 50
    '
    'colhdrText
    '
    Me.colhdrText.Text = "Text"
    Me.colhdrText.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
    Me.colhdrText.Width = 34
    '
    'colhdrTooltip
    '
    Me.colhdrTooltip.Text = "Tooltip"
    Me.colhdrTooltip.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
    Me.colhdrTooltip.Width = 44
    '
    'btnSelectAll
    '
    Me.btnSelectAll.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.btnSelectAll.AutoSize = True
    Me.btnSelectAll.Location = New System.Drawing.Point(334, 315)
    Me.btnSelectAll.MinimumSize = New System.Drawing.Size(75, 23)
    Me.btnSelectAll.Name = "btnSelectAll"
    Me.btnSelectAll.Size = New System.Drawing.Size(75, 23)
    Me.btnSelectAll.TabIndex = 2
    Me.btnSelectAll.Text = "Select All"
    '
    'btnReset
    '
    Me.btnReset.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
    Me.btnReset.AutoSize = True
    Me.btnReset.Location = New System.Drawing.Point(11, 315)
    Me.btnReset.MinimumSize = New System.Drawing.Size(75, 23)
    Me.btnReset.Name = "btnReset"
    Me.btnReset.Size = New System.Drawing.Size(75, 23)
    Me.btnReset.TabIndex = 3
    Me.btnReset.Text = "Reset"
    '
    'btnSelectNone
    '
    Me.btnSelectNone.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.btnSelectNone.AutoSize = True
    Me.btnSelectNone.Location = New System.Drawing.Point(414, 315)
    Me.btnSelectNone.MinimumSize = New System.Drawing.Size(75, 23)
    Me.btnSelectNone.Name = "btnSelectNone"
    Me.btnSelectNone.Size = New System.Drawing.Size(76, 23)
    Me.btnSelectNone.TabIndex = 1
    Me.btnSelectNone.Text = "Select None"
    '
    'Settings
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
    Me.Controls.Add(Me.grpItems)
    Me.Controls.Add(Me.grpAppearance)
    Me.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.Name = "Settings"
    Me.Size = New System.Drawing.Size(500, 400)
    Me.grpAppearance.ResumeLayout(False)
    Me.grpAppearance.PerformLayout()
    Me.grpItems.ResumeLayout(False)
    Me.grpItems.PerformLayout()
    Me.ResumeLayout(False)

  End Sub
  Friend WithEvents chkShowIcon As System.Windows.Forms.CheckBox
  Friend WithEvents chkShowText As System.Windows.Forms.CheckBox
  Friend WithEvents grpAppearance As System.Windows.Forms.GroupBox
  Friend WithEvents grpItems As System.Windows.Forms.GroupBox
  Friend WithEvents btnSelectAll As System.Windows.Forms.Button
  Friend WithEvents btnSelectNone As System.Windows.Forms.Button
  Friend WithEvents lvItems As System.Windows.Forms.ListView
  Friend WithEvents colhdrName As System.Windows.Forms.ColumnHeader
  Friend WithEvents colhdrValue As System.Windows.Forms.ColumnHeader
  Friend WithEvents colhdrText As System.Windows.Forms.ColumnHeader
  Friend WithEvents colhdrEnabled As System.Windows.Forms.ColumnHeader
  Friend WithEvents colhdrTooltip As System.Windows.Forms.ColumnHeader
  Friend WithEvents btnReset As System.Windows.Forms.Button
End Class
