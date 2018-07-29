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
    Me.grpAppearance = New System.Windows.Forms.GroupBox
    Me.chkShowGraph = New System.Windows.Forms.CheckBox
    Me.chkShowText = New System.Windows.Forms.CheckBox
    Me.chkShowIcon = New System.Windows.Forms.CheckBox
    Me.grpGraphSettings = New System.Windows.Forms.GroupBox
    Me.lblGraphUpdateTime = New System.Windows.Forms.Label
    Me.nudGraphUpdateTime = New System.Windows.Forms.NumericUpDown
    Me.comGraphUpdateTime = New System.Windows.Forms.ComboBox
    Me.grpTooltipSettings = New System.Windows.Forms.GroupBox
    Me.nudTopProcesses = New System.Windows.Forms.NumericUpDown
    Me.chkShowTopProcesses = New System.Windows.Forms.CheckBox
    Me.lblProcesses = New System.Windows.Forms.Label
    Me.grpMultiCore = New System.Windows.Forms.GroupBox
    Me.lvMulticore = New System.Windows.Forms.ListView
    Me.colhdrCore = New System.Windows.Forms.ColumnHeader
    Me.colhdrShowText = New System.Windows.Forms.ColumnHeader
    Me.colhdrShowTooltip = New System.Windows.Forms.ColumnHeader
    Me.colhdrGraph = New System.Windows.Forms.ColumnHeader
    Me.colhdrGraphColor = New System.Windows.Forms.ColumnHeader
    Me.grpAppearance.SuspendLayout()
    Me.grpGraphSettings.SuspendLayout()
    CType(Me.nudGraphUpdateTime, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.grpTooltipSettings.SuspendLayout()
    CType(Me.nudTopProcesses, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.grpMultiCore.SuspendLayout()
    Me.SuspendLayout()
    '
    'grpAppearance
    '
    Me.grpAppearance.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.grpAppearance.Controls.Add(Me.chkShowGraph)
    Me.grpAppearance.Controls.Add(Me.chkShowText)
    Me.grpAppearance.Controls.Add(Me.chkShowIcon)
    Me.grpAppearance.Location = New System.Drawing.Point(0, 0)
    Me.grpAppearance.Name = "grpAppearance"
    Me.grpAppearance.Size = New System.Drawing.Size(500, 44)
    Me.grpAppearance.TabIndex = 20
    Me.grpAppearance.TabStop = False
    Me.grpAppearance.Text = "Appearance Settings"
    '
    'chkShowGraph
    '
    Me.chkShowGraph.Location = New System.Drawing.Point(177, 18)
    Me.chkShowGraph.Name = "chkShowGraph"
    Me.chkShowGraph.Size = New System.Drawing.Size(90, 17)
    Me.chkShowGraph.TabIndex = 8
    Me.chkShowGraph.Text = "Show Graph"
    '
    'chkShowText
    '
    Me.chkShowText.AutoSize = True
    Me.chkShowText.Location = New System.Drawing.Point(94, 18)
    Me.chkShowText.Name = "chkShowText"
    Me.chkShowText.Size = New System.Drawing.Size(77, 17)
    Me.chkShowText.TabIndex = 7
    Me.chkShowText.Text = "Show Text"
    Me.chkShowText.UseVisualStyleBackColor = True
    '
    'chkShowIcon
    '
    Me.chkShowIcon.AutoSize = True
    Me.chkShowIcon.Location = New System.Drawing.Point(12, 18)
    Me.chkShowIcon.Name = "chkShowIcon"
    Me.chkShowIcon.Size = New System.Drawing.Size(76, 17)
    Me.chkShowIcon.TabIndex = 9
    Me.chkShowIcon.Text = "Show Icon"
    '
    'grpGraphSettings
    '
    Me.grpGraphSettings.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.grpGraphSettings.Controls.Add(Me.lblGraphUpdateTime)
    Me.grpGraphSettings.Controls.Add(Me.nudGraphUpdateTime)
    Me.grpGraphSettings.Controls.Add(Me.comGraphUpdateTime)
    Me.grpGraphSettings.Location = New System.Drawing.Point(0, 103)
    Me.grpGraphSettings.Name = "grpGraphSettings"
    Me.grpGraphSettings.Size = New System.Drawing.Size(500, 53)
    Me.grpGraphSettings.TabIndex = 19
    Me.grpGraphSettings.TabStop = False
    Me.grpGraphSettings.Text = "Graph Settings"
    '
    'lblGraphUpdateTime
    '
    Me.lblGraphUpdateTime.AutoSize = True
    Me.lblGraphUpdateTime.Location = New System.Drawing.Point(13, 22)
    Me.lblGraphUpdateTime.Margin = New System.Windows.Forms.Padding(0, 2, 3, 0)
    Me.lblGraphUpdateTime.Name = "lblGraphUpdateTime"
    Me.lblGraphUpdateTime.Size = New System.Drawing.Size(104, 13)
    Me.lblGraphUpdateTime.TabIndex = 15
    Me.lblGraphUpdateTime.Text = "Update graph every"
    '
    'nudGraphUpdateTime
    '
    Me.nudGraphUpdateTime.Location = New System.Drawing.Point(120, 20)
    Me.nudGraphUpdateTime.Margin = New System.Windows.Forms.Padding(0)
    Me.nudGraphUpdateTime.Maximum = New Decimal(New Integer() {60, 0, 0, 0})
    Me.nudGraphUpdateTime.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
    Me.nudGraphUpdateTime.Name = "nudGraphUpdateTime"
    Me.nudGraphUpdateTime.Size = New System.Drawing.Size(46, 21)
    Me.nudGraphUpdateTime.TabIndex = 16
    Me.nudGraphUpdateTime.Value = New Decimal(New Integer() {1, 0, 0, 0})
    '
    'comGraphUpdateTime
    '
    Me.comGraphUpdateTime.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
    Me.comGraphUpdateTime.FormattingEnabled = True
    Me.comGraphUpdateTime.Items.AddRange(New Object() {"seconds", "minutes", "hours"})
    Me.comGraphUpdateTime.Location = New System.Drawing.Point(170, 20)
    Me.comGraphUpdateTime.Margin = New System.Windows.Forms.Padding(4, 0, 0, 0)
    Me.comGraphUpdateTime.Name = "comGraphUpdateTime"
    Me.comGraphUpdateTime.Size = New System.Drawing.Size(121, 21)
    Me.comGraphUpdateTime.TabIndex = 17
    '
    'grpTooltipSettings
    '
    Me.grpTooltipSettings.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.grpTooltipSettings.Controls.Add(Me.nudTopProcesses)
    Me.grpTooltipSettings.Controls.Add(Me.chkShowTopProcesses)
    Me.grpTooltipSettings.Controls.Add(Me.lblProcesses)
    Me.grpTooltipSettings.Location = New System.Drawing.Point(0, 50)
    Me.grpTooltipSettings.Name = "grpTooltipSettings"
    Me.grpTooltipSettings.Size = New System.Drawing.Size(500, 47)
    Me.grpTooltipSettings.TabIndex = 21
    Me.grpTooltipSettings.TabStop = False
    Me.grpTooltipSettings.Text = "Tooltip Settings"
    '
    'nudTopProcesses
    '
    Me.nudTopProcesses.Location = New System.Drawing.Point(101, 17)
    Me.nudTopProcesses.Minimum = New Decimal(New Integer() {2, 0, 0, 0})
    Me.nudTopProcesses.Name = "nudTopProcesses"
    Me.nudTopProcesses.Size = New System.Drawing.Size(39, 21)
    Me.nudTopProcesses.TabIndex = 2
    Me.nudTopProcesses.Value = New Decimal(New Integer() {10, 0, 0, 0})
    '
    'chkShowTopProcesses
    '
    Me.chkShowTopProcesses.Anchor = System.Windows.Forms.AnchorStyles.Left
    Me.chkShowTopProcesses.AutoSize = True
    Me.chkShowTopProcesses.Location = New System.Drawing.Point(12, 18)
    Me.chkShowTopProcesses.Margin = New System.Windows.Forms.Padding(0)
    Me.chkShowTopProcesses.Name = "chkShowTopProcesses"
    Me.chkShowTopProcesses.Size = New System.Drawing.Size(90, 17)
    Me.chkShowTopProcesses.TabIndex = 1
    Me.chkShowTopProcesses.Text = "Show the top"
    Me.chkShowTopProcesses.UseVisualStyleBackColor = True
    '
    'lblProcesses
    '
    Me.lblProcesses.Anchor = System.Windows.Forms.AnchorStyles.Left
    Me.lblProcesses.AutoSize = True
    Me.lblProcesses.Location = New System.Drawing.Point(143, 18)
    Me.lblProcesses.Margin = New System.Windows.Forms.Padding(0, 0, 0, 2)
    Me.lblProcesses.Name = "lblProcesses"
    Me.lblProcesses.Size = New System.Drawing.Size(59, 13)
    Me.lblProcesses.TabIndex = 3
    Me.lblProcesses.Text = "processes."
    Me.lblProcesses.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
    '
    'grpMultiCore
    '
    Me.grpMultiCore.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.grpMultiCore.Controls.Add(Me.lvMulticore)
    Me.grpMultiCore.Location = New System.Drawing.Point(0, 162)
    Me.grpMultiCore.Name = "grpMultiCore"
    Me.grpMultiCore.Size = New System.Drawing.Size(500, 235)
    Me.grpMultiCore.TabIndex = 21
    Me.grpMultiCore.TabStop = False
    Me.grpMultiCore.Text = "Core Settings"
    '
    'lvMulticore
    '
    Me.lvMulticore.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.lvMulticore.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.colhdrCore, Me.colhdrShowText, Me.colhdrShowTooltip, Me.colhdrGraph, Me.colhdrGraphColor})
    Me.lvMulticore.FullRowSelect = True
    Me.lvMulticore.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable
    Me.lvMulticore.Location = New System.Drawing.Point(12, 21)
    Me.lvMulticore.MultiSelect = False
    Me.lvMulticore.Name = "lvMulticore"
    Me.lvMulticore.OwnerDraw = True
    Me.lvMulticore.ShowGroups = False
    Me.lvMulticore.Size = New System.Drawing.Size(473, 200)
    Me.lvMulticore.TabIndex = 0
    Me.lvMulticore.UseCompatibleStateImageBehavior = False
    Me.lvMulticore.View = System.Windows.Forms.View.Details
    '
    'colhdrCore
    '
    Me.colhdrCore.Text = "Core"
    Me.colhdrCore.Width = 73
    '
    'colhdrShowText
    '
    Me.colhdrShowText.Text = "Text"
    Me.colhdrShowText.Width = 36
    '
    'colhdrShowTooltip
    '
    Me.colhdrShowTooltip.Text = "Tooltip"
    Me.colhdrShowTooltip.Width = 44
    '
    'colhdrGraph
    '
    Me.colhdrGraph.Text = "Graph"
    Me.colhdrGraph.Width = 42
    '
    'colhdrGraphColor
    '
    Me.colhdrGraphColor.Text = "Graph Color"
    Me.colhdrGraphColor.Width = 72
    '
    'Settings
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
    Me.Controls.Add(Me.grpGraphSettings)
    Me.Controls.Add(Me.grpMultiCore)
    Me.Controls.Add(Me.grpTooltipSettings)
    Me.Controls.Add(Me.grpAppearance)
    Me.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.Margin = New System.Windows.Forms.Padding(0)
    Me.Name = "Settings"
    Me.Size = New System.Drawing.Size(500, 400)
    Me.grpAppearance.ResumeLayout(False)
    Me.grpAppearance.PerformLayout()
    Me.grpGraphSettings.ResumeLayout(False)
    Me.grpGraphSettings.PerformLayout()
    CType(Me.nudGraphUpdateTime, System.ComponentModel.ISupportInitialize).EndInit()
    Me.grpTooltipSettings.ResumeLayout(False)
    Me.grpTooltipSettings.PerformLayout()
    CType(Me.nudTopProcesses, System.ComponentModel.ISupportInitialize).EndInit()
    Me.grpMultiCore.ResumeLayout(False)
    Me.ResumeLayout(False)

  End Sub
  Friend WithEvents grpAppearance As System.Windows.Forms.GroupBox
  Friend WithEvents chkShowIcon As System.Windows.Forms.CheckBox
  Friend WithEvents chkShowText As System.Windows.Forms.CheckBox
  Friend WithEvents chkShowGraph As System.Windows.Forms.CheckBox
  Friend WithEvents grpGraphSettings As System.Windows.Forms.GroupBox
  Friend WithEvents grpTooltipSettings As System.Windows.Forms.GroupBox
  Friend WithEvents chkShowTopProcesses As System.Windows.Forms.CheckBox
  Friend WithEvents nudTopProcesses As System.Windows.Forms.NumericUpDown
  Friend WithEvents lblProcesses As System.Windows.Forms.Label
  Friend WithEvents lblGraphUpdateTime As System.Windows.Forms.Label
  Friend WithEvents nudGraphUpdateTime As System.Windows.Forms.NumericUpDown
  Friend WithEvents comGraphUpdateTime As System.Windows.Forms.ComboBox
  Friend WithEvents grpMultiCore As System.Windows.Forms.GroupBox
  Friend WithEvents colhdrCore As System.Windows.Forms.ColumnHeader
  Friend WithEvents colhdrShowText As System.Windows.Forms.ColumnHeader
  Friend WithEvents colhdrShowTooltip As System.Windows.Forms.ColumnHeader
  Friend WithEvents colhdrGraph As System.Windows.Forms.ColumnHeader
  Friend WithEvents colhdrGraphColor As System.Windows.Forms.ColumnHeader
  Friend WithEvents lvMulticore As System.Windows.Forms.ListView
End Class
