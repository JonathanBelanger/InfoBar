<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmMain
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
    Me.components = New System.ComponentModel.Container
    Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmMain))
    Me.mnuTray = New System.Windows.Forms.ContextMenu
    Me.mnuTray_Restore = New System.Windows.Forms.MenuItem
    Me.mnuTray_Sep01 = New System.Windows.Forms.MenuItem
    Me.mnuTray_Close = New System.Windows.Forms.MenuItem
    Me.tiMain = New System.Windows.Forms.NotifyIcon(Me.components)
    Me.tmrAutohide = New System.Windows.Forms.Timer(Me.components)
    Me.tmrAutohideAnimation = New System.Windows.Forms.Timer(Me.components)
    Me.tmrUpdate = New System.Windows.Forms.Timer(Me.components)
    Me.mnuInfoBar = New InfoBar.CustomContextMenu
    Me.SuspendLayout()
    '
    'mnuTray
    '
    Me.mnuTray.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.mnuTray_Restore, Me.mnuTray_Sep01, Me.mnuTray_Close})
    '
    'mnuTray_Restore
    '
    Me.mnuTray_Restore.DefaultItem = True
    Me.mnuTray_Restore.Index = 0
    Me.mnuTray_Restore.Text = "&Restore"
    '
    'mnuTray_Sep01
    '
    Me.mnuTray_Sep01.Index = 1
    Me.mnuTray_Sep01.Text = "-"
    '
    'mnuTray_Close
    '
    Me.mnuTray_Close.Index = 2
    Me.mnuTray_Close.Text = "&Close"
    '
    'tiMain
    '
    Me.tiMain.BalloonTipIcon = System.Windows.Forms.ToolTipIcon.Info
    Me.tiMain.BalloonTipText = "InfoBar has been hidden and this icon has been placed here. You can get InfoBar b" & _
        "ack by double clicking this icon." & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Click here to not show this message again."
    Me.tiMain.BalloonTipTitle = "Where did InfoBar go?"
    Me.tiMain.ContextMenu = Me.mnuTray
    Me.tiMain.Icon = CType(resources.GetObject("tiMain.Icon"), System.Drawing.Icon)
    Me.tiMain.Text = "InfoBar"
    '
    'tmrAutohide
    '
    Me.tmrAutohide.Interval = 1000
    '
    'tmrAutohideAnimation
    '
    Me.tmrAutohideAnimation.Interval = 1
    '
    'tmrUpdate
    '
    Me.tmrUpdate.Interval = 1000
    '
    'mnuInfoBar
    '
    Me.mnuInfoBar.Name = "mnuInfoBar"
    Me.mnuInfoBar.Size = New System.Drawing.Size(61, 4)
    '
    'frmMain
    '
    Me.AllowDrop = True
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
    Me.ClientSize = New System.Drawing.Size(8, 8)
    Me.ControlBox = False
    Me.DoubleBuffered = True
    Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
    Me.Location = New System.Drawing.Point(-10000, -10000)
    Me.MaximizeBox = False
    Me.MinimizeBox = False
    Me.Name = "frmMain"
    Me.ShowIcon = False
    Me.ShowInTaskbar = False
    Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
    Me.ResumeLayout(False)

  End Sub

  Friend WithEvents mnuInfoBar As CustomContextMenu
    Friend WithEvents mnuTray As ContextMenu
    Friend WithEvents tiMain As System.Windows.Forms.NotifyIcon
    Friend WithEvents mnuTray_Restore As System.Windows.Forms.MenuItem
    Friend WithEvents mnuTray_Sep01 As System.Windows.Forms.MenuItem
  Friend WithEvents mnuTray_Close As System.Windows.Forms.MenuItem
  Friend WithEvents tmrAutohide As System.Windows.Forms.Timer
  Friend WithEvents tmrAutohideAnimation As System.Windows.Forms.Timer
  Friend WithEvents tmrUpdate As System.Windows.Forms.Timer
End Class
