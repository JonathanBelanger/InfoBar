Friend Interface ModuleInterface

  ' Module Information
  ReadOnly Property ModuleGUID() As String
  ReadOnly Property ModuleName() As String
  ReadOnly Property ModuleDescription() As String
  ReadOnly Property ModuleAuthor() As String
  ReadOnly Property ModuleVersion() As String
  ReadOnly Property ModuleEmail() As String
  ReadOnly Property ModuleHomepage() As String
  ReadOnly Property ModuleCopyright() As String
  ReadOnly Property ModuleIcon() As Image
  Property ModuleEnabled() As Boolean
  Property ModuleBounds() As Rectangle

  ' Module Startup, Shutdown and Timer
  Sub InitializeModule()
  Sub FinalizeModule()
  Sub TimerTick(ByRef bModuleIsDirty As Boolean)

  ' Drawing
  Function GetModuleBitmap() As Bitmap
  Function GetTooltipBitmap() As Bitmap

  ' Mouse, Keyboard and Menu Processing
  Sub ProcessMouseMove(ByVal e As System.Windows.Forms.MouseEventArgs, ByRef bWindowIsDirty As Boolean)
  Sub ProcessMouseUp(ByVal e As System.Windows.Forms.MouseEventArgs, ByRef bWindowIsDirty As Boolean)
  Sub ProcessMouseDown(ByVal e As System.Windows.Forms.MouseEventArgs, ByRef bWindowIsDirty As Boolean)
  Sub ProcessMouseLeave(ByRef bWindowIsDirty As Boolean)
  Sub ProcessMenuItemClick(ByVal Key As String)
  Sub ProcessDragDrop(ByVal e As System.Windows.Forms.DragEventArgs, ByRef bWindowIsDirty As Boolean)
  Sub ProcessDragEnter(ByVal e As System.Windows.Forms.DragEventArgs, ByRef bWindowIsDirty As Boolean)
  Sub ProcessDragLeave(ByVal e As System.EventArgs, ByRef bWindowIsDirty As Boolean)
  Sub ProcessDragOver(ByVal e As System.Windows.Forms.DragEventArgs, ByRef bWindowIsDirty As Boolean)
  Sub AddMainPopupMenuItems()

  ' Module Settings
  ReadOnly Property HasSettingsDialog() As Boolean
  ReadOnly Property SettingsDialog() As UserControl
  Sub LoadSettings(ByRef Doc As XmlDocument)
  Sub SaveSettings(ByRef Doc As XmlDocument)
  Sub ApplySettings()
  Sub ResetSettings()
End Interface

Public MustInherit Class InfoBarModule
  Implements ModuleInterface

  ' Module Information
  Public MustOverride ReadOnly Property ModuleGUID() As String Implements ModuleInterface.ModuleGUID
  Public MustOverride ReadOnly Property ModuleName() As String Implements ModuleInterface.ModuleName
  Public MustOverride ReadOnly Property ModuleDescription() As String Implements ModuleInterface.ModuleDescription
  Public MustOverride ReadOnly Property ModuleAuthor() As String Implements ModuleInterface.ModuleAuthor
  Public MustOverride ReadOnly Property ModuleVersion() As String Implements ModuleInterface.ModuleVersion
  Public MustOverride ReadOnly Property ModuleEmail() As String Implements ModuleInterface.ModuleEmail
  Public MustOverride ReadOnly Property ModuleHomepage() As String Implements ModuleInterface.ModuleHomepage
  Public MustOverride ReadOnly Property ModuleCopyright() As String Implements ModuleInterface.ModuleCopyright
  Public MustOverride ReadOnly Property ModuleIcon() As System.Drawing.Image Implements ModuleInterface.ModuleIcon
  Public MustOverride Property ModuleEnabled() As Boolean Implements ModuleInterface.ModuleEnabled
  Public MustOverride Property ModuleBounds() As System.Drawing.Rectangle Implements ModuleInterface.ModuleBounds

  ' Module Startup, Shutdown and Timer
  Public MustOverride Sub InitializeModule() Implements ModuleInterface.InitializeModule
  Public MustOverride Sub FinalizeModule() Implements ModuleInterface.FinalizeModule
  Public MustOverride Sub TimerTick(ByRef bModuleIsDirty As Boolean) Implements ModuleInterface.TimerTick

  ' Drawing
  Public MustOverride Function GetModuleBitmap() As Bitmap Implements ModuleInterface.GetModuleBitmap
  Public MustOverride Function GetTooltipBitmap() As Bitmap Implements ModuleInterface.GetTooltipBitmap

  ' Mouse, Keyboard and Menu Processing
  Public MustOverride Sub ProcessMouseMove(ByVal e As System.Windows.Forms.MouseEventArgs, ByRef bWindowIsDirty As Boolean) Implements ModuleInterface.ProcessMouseMove
  Public MustOverride Sub ProcessMouseUp(ByVal e As System.Windows.Forms.MouseEventArgs, ByRef bWindowIsDirty As Boolean) Implements ModuleInterface.ProcessMouseUp
  Public MustOverride Sub ProcessMouseDown(ByVal e As System.Windows.Forms.MouseEventArgs, ByRef bWindowIsDirty As Boolean) Implements ModuleInterface.ProcessMouseDown
  Public MustOverride Sub ProcessMouseLeave(ByRef bWindowIsDirty As Boolean) Implements ModuleInterface.ProcessMouseLeave
  Public MustOverride Sub ProcessMenuItemClick(ByVal Key As String) Implements ModuleInterface.ProcessMenuItemClick
  Public MustOverride Sub ProcessDragDrop(ByVal e As System.Windows.Forms.DragEventArgs, ByRef bWindowIsDirty As Boolean) Implements ModuleInterface.ProcessDragDrop
  Public MustOverride Sub ProcessDragEnter(ByVal e As System.Windows.Forms.DragEventArgs, ByRef bWindowIsDirty As Boolean) Implements ModuleInterface.ProcessDragEnter
  Public MustOverride Sub ProcessDragLeave(ByVal e As System.EventArgs, ByRef bWindowIsDirty As Boolean) Implements ModuleInterface.ProcessDragLeave
  Public MustOverride Sub ProcessDragOver(ByVal e As System.Windows.Forms.DragEventArgs, ByRef bWindowIsDirty As Boolean) Implements ModuleInterface.ProcessDragOver
  Public MustOverride Sub AddMainPopupMenuItems() Implements ModuleInterface.AddMainPopupMenuItems

  ' Module Settings
  Public MustOverride ReadOnly Property HasSettingsDialog() As Boolean Implements ModuleInterface.HasSettingsDialog
  Public MustOverride ReadOnly Property SettingsDialog() As System.Windows.Forms.UserControl Implements ModuleInterface.SettingsDialog
  Public MustOverride Sub LoadSettings(ByRef Doc As System.Xml.XmlDocument) Implements ModuleInterface.LoadSettings
  Public MustOverride Sub SaveSettings(ByRef Doc As System.Xml.XmlDocument) Implements ModuleInterface.SaveSettings
  Public MustOverride Sub ApplySettings() Implements ModuleInterface.ApplySettings
  Public MustOverride Sub ResetSettings() Implements ModuleInterface.ResetSettings
End Class