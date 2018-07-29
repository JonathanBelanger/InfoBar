<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmAddFile
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
    Me.btnOK = New System.Windows.Forms.Button
    Me.btnCancel = New System.Windows.Forms.Button
    Me.txtCommandLineArguments = New System.Windows.Forms.TextBox
    Me.lblCommandLineArgs = New System.Windows.Forms.Label
    Me.btnBrowsePath = New System.Windows.Forms.Button
    Me.txtPath = New System.Windows.Forms.TextBox
    Me.lblPath = New System.Windows.Forms.Label
    Me.txtName = New System.Windows.Forms.TextBox
    Me.lblName = New System.Windows.Forms.Label
    Me.lblWorkingDirectory = New System.Windows.Forms.Label
    Me.txtWorkingDirectory = New System.Windows.Forms.TextBox
    Me.btnBrowseWorkingDirectory = New System.Windows.Forms.Button
    Me.SuspendLayout()
    '
    'btnOK
    '
    Me.btnOK.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.btnOK.AutoSize = True
    Me.btnOK.Location = New System.Drawing.Point(225, 206)
    Me.btnOK.MinimumSize = New System.Drawing.Size(75, 25)
    Me.btnOK.Name = "btnOK"
    Me.btnOK.Size = New System.Drawing.Size(75, 25)
    Me.btnOK.TabIndex = 7
    Me.btnOK.Text = "&OK"
    Me.btnOK.UseVisualStyleBackColor = True
    '
    'btnCancel
    '
    Me.btnCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
    Me.btnCancel.AutoSize = True
    Me.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
    Me.btnCancel.Location = New System.Drawing.Point(306, 206)
    Me.btnCancel.MinimumSize = New System.Drawing.Size(75, 25)
    Me.btnCancel.Name = "btnCancel"
    Me.btnCancel.Size = New System.Drawing.Size(75, 25)
    Me.btnCancel.TabIndex = 8
    Me.btnCancel.Text = "&Cancel"
    Me.btnCancel.UseVisualStyleBackColor = True
    '
    'txtCommandLineArguments
    '
    Me.txtCommandLineArguments.Location = New System.Drawing.Point(12, 167)
    Me.txtCommandLineArguments.Margin = New System.Windows.Forms.Padding(0, 0, 0, 8)
    Me.txtCommandLineArguments.Name = "txtCommandLineArguments"
    Me.txtCommandLineArguments.Size = New System.Drawing.Size(369, 22)
    Me.txtCommandLineArguments.TabIndex = 6
    '
    'lblCommandLineArgs
    '
    Me.lblCommandLineArgs.AutoSize = True
    Me.lblCommandLineArgs.Location = New System.Drawing.Point(9, 150)
    Me.lblCommandLineArgs.Margin = New System.Windows.Forms.Padding(0, 0, 0, 4)
    Me.lblCommandLineArgs.Name = "lblCommandLineArgs"
    Me.lblCommandLineArgs.Size = New System.Drawing.Size(145, 13)
    Me.lblCommandLineArgs.TabIndex = 12
    Me.lblCommandLineArgs.Text = "Command Line Arguments:"
    '
    'btnBrowsePath
    '
    Me.btnBrowsePath.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
    Me.btnBrowsePath.FlatStyle = System.Windows.Forms.FlatStyle.System
    Me.btnBrowsePath.Location = New System.Drawing.Point(339, 73)
    Me.btnBrowsePath.Margin = New System.Windows.Forms.Padding(0)
    Me.btnBrowsePath.MinimumSize = New System.Drawing.Size(39, 21)
    Me.btnBrowsePath.Name = "btnBrowsePath"
    Me.btnBrowsePath.Size = New System.Drawing.Size(39, 21)
    Me.btnBrowsePath.TabIndex = 3
    Me.btnBrowsePath.Text = "..."
    Me.btnBrowsePath.UseVisualStyleBackColor = True
    '
    'txtPath
    '
    Me.txtPath.Location = New System.Drawing.Point(12, 72)
    Me.txtPath.Margin = New System.Windows.Forms.Padding(0, 0, 0, 8)
    Me.txtPath.Name = "txtPath"
    Me.txtPath.Size = New System.Drawing.Size(318, 22)
    Me.txtPath.TabIndex = 2
    Me.txtPath.WordWrap = False
    '
    'lblPath
    '
    Me.lblPath.AutoSize = True
    Me.lblPath.Location = New System.Drawing.Point(9, 55)
    Me.lblPath.Margin = New System.Windows.Forms.Padding(0, 0, 0, 4)
    Me.lblPath.Name = "lblPath"
    Me.lblPath.Size = New System.Drawing.Size(33, 13)
    Me.lblPath.TabIndex = 9
    Me.lblPath.Text = "Path:"
    '
    'txtName
    '
    Me.txtName.Location = New System.Drawing.Point(12, 26)
    Me.txtName.Margin = New System.Windows.Forms.Padding(0, 0, 0, 8)
    Me.txtName.Name = "txtName"
    Me.txtName.Size = New System.Drawing.Size(369, 22)
    Me.txtName.TabIndex = 1
    '
    'lblName
    '
    Me.lblName.AutoSize = True
    Me.lblName.Location = New System.Drawing.Point(9, 9)
    Me.lblName.Margin = New System.Windows.Forms.Padding(0, 0, 0, 4)
    Me.lblName.Name = "lblName"
    Me.lblName.Size = New System.Drawing.Size(39, 13)
    Me.lblName.TabIndex = 7
    Me.lblName.Text = "Name:"
    '
    'lblWorkingDirectory
    '
    Me.lblWorkingDirectory.AutoSize = True
    Me.lblWorkingDirectory.Location = New System.Drawing.Point(9, 103)
    Me.lblWorkingDirectory.Margin = New System.Windows.Forms.Padding(0, 0, 0, 4)
    Me.lblWorkingDirectory.Name = "lblWorkingDirectory"
    Me.lblWorkingDirectory.Size = New System.Drawing.Size(104, 13)
    Me.lblWorkingDirectory.TabIndex = 9
    Me.lblWorkingDirectory.Text = "Working Directory:"
    '
    'txtWorkingDirectory
    '
    Me.txtWorkingDirectory.Location = New System.Drawing.Point(12, 120)
    Me.txtWorkingDirectory.Margin = New System.Windows.Forms.Padding(0, 0, 0, 8)
    Me.txtWorkingDirectory.Name = "txtWorkingDirectory"
    Me.txtWorkingDirectory.Size = New System.Drawing.Size(318, 22)
    Me.txtWorkingDirectory.TabIndex = 4
    Me.txtWorkingDirectory.WordWrap = False
    '
    'btnBrowseWorkingDirectory
    '
    Me.btnBrowseWorkingDirectory.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
    Me.btnBrowseWorkingDirectory.FlatStyle = System.Windows.Forms.FlatStyle.System
    Me.btnBrowseWorkingDirectory.Location = New System.Drawing.Point(339, 121)
    Me.btnBrowseWorkingDirectory.Margin = New System.Windows.Forms.Padding(0)
    Me.btnBrowseWorkingDirectory.MinimumSize = New System.Drawing.Size(39, 21)
    Me.btnBrowseWorkingDirectory.Name = "btnBrowseWorkingDirectory"
    Me.btnBrowseWorkingDirectory.Size = New System.Drawing.Size(39, 21)
    Me.btnBrowseWorkingDirectory.TabIndex = 5
    Me.btnBrowseWorkingDirectory.Text = "..."
    Me.btnBrowseWorkingDirectory.UseVisualStyleBackColor = True
    '
    'frmAddFile
    '
    Me.AcceptButton = Me.btnOK
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
    Me.CancelButton = Me.btnCancel
    Me.ClientSize = New System.Drawing.Size(393, 243)
    Me.Controls.Add(Me.txtCommandLineArguments)
    Me.Controls.Add(Me.lblCommandLineArgs)
    Me.Controls.Add(Me.btnBrowseWorkingDirectory)
    Me.Controls.Add(Me.btnBrowsePath)
    Me.Controls.Add(Me.txtWorkingDirectory)
    Me.Controls.Add(Me.lblWorkingDirectory)
    Me.Controls.Add(Me.txtPath)
    Me.Controls.Add(Me.lblPath)
    Me.Controls.Add(Me.txtName)
    Me.Controls.Add(Me.lblName)
    Me.Controls.Add(Me.btnCancel)
    Me.Controls.Add(Me.btnOK)
    Me.DoubleBuffered = True
    Me.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
    Me.MaximizeBox = False
    Me.Name = "frmAddFile"
    Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
    Me.Text = "Add File..."
    Me.ResumeLayout(False)
    Me.PerformLayout()

  End Sub
  Friend WithEvents btnOK As System.Windows.Forms.Button
  Friend WithEvents btnCancel As System.Windows.Forms.Button
  Friend WithEvents txtCommandLineArguments As System.Windows.Forms.TextBox
  Friend WithEvents lblCommandLineArgs As System.Windows.Forms.Label
  Friend WithEvents btnBrowsePath As System.Windows.Forms.Button
  Friend WithEvents txtPath As System.Windows.Forms.TextBox
  Friend WithEvents lblPath As System.Windows.Forms.Label
  Friend WithEvents txtName As System.Windows.Forms.TextBox
  Friend WithEvents lblName As System.Windows.Forms.Label
  Friend WithEvents lblWorkingDirectory As System.Windows.Forms.Label
  Friend WithEvents txtWorkingDirectory As System.Windows.Forms.TextBox
  Friend WithEvents btnBrowseWorkingDirectory As System.Windows.Forms.Button
End Class
