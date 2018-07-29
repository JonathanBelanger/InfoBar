Name "InfoBar"
InstallDir "$PROGRAMFILES\InfoBar"
InstallDirRegKey HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\InfoBar" "InstallLocation"
BrandingText "� 2001-2011 Jim Laski"
SetCompressor /SOLID lzma
OutFile "InfoBarSetup.exe"
RequestExecutionLevel admin

!include "MUI2.nsh"
!include "FileFunc.nsh"

!define MUI_ABORTWARNING
!define MUI_UI_COMPONENTSPAGE_NODESC "${NSISDIR}\Contrib\UIs\modern_nodesc.exe"
!define MUI_HEADERIMAGE
!define MUI_HEADERIMAGE_RIGHT
!define MUI_COMPONENTSPAGE_NODESC
!define MUI_FINISHPAGE_RUN "$INSTDIR\InfoBar.exe"
!define /date DATE "%Y%m%d"

!insertmacro MUI_PAGE_WELCOME
!insertmacro MUI_PAGE_COMPONENTS
!insertmacro MUI_PAGE_DIRECTORY
!insertmacro MUI_PAGE_INSTFILES
!insertmacro MUI_PAGE_FINISH
!insertmacro MUI_UNPAGE_CONFIRM
!insertmacro MUI_UNPAGE_INSTFILES
!insertmacro MUI_LANGUAGE "English"
!insertmacro GetFileVersion

# ========================================================================================================================================================================================================================================================================
#	InfoBar Main Section
# ========================================================================================================================================================================================================================================================================

Section "!InfoBar"
	SectionIn RO
	SetOverwrite try

	SetOutPath "$INSTDIR\"

	File "InfoBar.exe"
	File "InfoBar.exe.config"
	File "InfoBar.pdb"
	File "Unzip.dll"

	CreateDirectory "$INSTDIR\Icons\"
	CreateDirectory "$INSTDIR\Skins\"
	CreateDirectory "$INSTDIR\Modules\"

	# Setup InfoBarModule file type
	WriteRegStr HKCR ".InfoBarModule" "" "InfoBar.InfoBarModule"
	WriteRegStr HKCR "InfoBar.InfoBarModule" "" "InfoBar Module Package"
	WriteRegStr HKCR "InfoBar.InfoBarModule\shell\Install\command" "" "$INSTDIR\InfoBar.exe /installmodule %1"

	# Setup InfoBarSkin file type
	WriteRegStr HKCR ".InfoBarSkin" "" "InfoBar.InfoBarSkin"
	WriteRegStr HKCR "InfoBar.InfoBarSkin" "" "InfoBar Skin Package"
	WriteRegStr HKCR "InfoBar.InfoBarSkin\shell\Install\command" "" "$INSTDIR\InfoBar.exe /installskin %1"

	# Setup InfoBarIcons file type
	WriteRegStr HKCR ".InfoBarIcons" "" "InfoBar.InfoBarIcons"
	WriteRegStr HKCR "InfoBar.InfoBarIcons" "" "InfoBar Icon Package"
	WriteRegStr HKCR "InfoBar.InfoBarIcons\shell\Install\command" "" "$INSTDIR\InfoBar.exe /installicons %1"

SectionEnd

SectionGroup /e "Modules"

	Section "Battery Status"
		SetOverwrite try
		SetOutPath "$INSTDIR\Modules\Battery Status\"
		File "Modules\Battery Status\BatteryStatus.dll"
		File "Modules\Battery Status\BatteryStatus.pdb"
	SectionEnd

	Section "CPU Usage"
		SetOverwrite try
		SetOutPath "$INSTDIR\Modules\CPU Usage\"
		File "Modules\CPU Usage\CPUUsage.dll"
		File "Modules\CPU Usage\CPUUsage.pdb"
	SectionEnd

	Section "EVEREST Sensor Monitor"
	  SetOverwrite try
	  SetOutPath "$INSTDIR\Modules\EVEREST Sensor Monitor\"
	  File "Modules\EVEREST Sensor Monitor\EVERESTSensorMonitor.dll"
	  File "Modules\EVEREST Sensor Monitor\EVERESTSensorMonitor.pdb"
	SectionEnd

	Section "foobar2000 Remote Control"
	  SetOverwrite try
	  SetOutPath "$INSTDIR\Modules\foobar2000 Remote Control\"
	  File "Modules\foobar2000 Remote Control\foobar2000RemoteControl.dll"
	  File "Modules\foobar2000 Remote Control\foobar2000RemoteControl.pdb"
	  File "Modules\foobar2000 Remote Control\Interop.Foobar2000.dll"
	  File "Modules\foobar2000 Remote Control\Interop.Foobar2000Helper.dll"
	SectionEnd

  Section "Gmail Checker"
	  SetOverwrite try
	  SetOutPath "$INSTDIR\Modules\Gmail Checker\"
	  File "Modules\Gmail Checker\GmailChecker.dll"
	  File "Modules\Gmail Checker\GmailChecker.pdb"
  SectionEnd

	Section "Hard Disk Usage"
		SetOverwrite try
		SetOutPath "$INSTDIR\Modules\Hard Disk Usage\"
		File "Modules\Hard Disk Usage\HardDiskUsage.dll"
		File "Modules\Hard Disk Usage\HardDiskUsage.pdb"
	SectionEnd

	Section "Keyboard Status"
		SetOverwrite try
		SetOutPath "$INSTDIR\Modules\Keyboard Status\"
		File "Modules\Keyboard Status\KeyboardStatus.dll"
		File "Modules\Keyboard Status\KeyboardStatus.pdb"
	SectionEnd

	Section "Memory Usage"
		SetOverwrite try
		SetOutPath "$INSTDIR\Modules\Memory Usage\"
		File "Modules\Memory Usage\MemoryUsage.dll"
		File "Modules\Memory Usage\MemoryUsage.pdb"
	SectionEnd

	Section "Network Usage"
		SetOverwrite try
		SetOutPath "$INSTDIR\Modules\Network Usage\"
		File "Modules\Network Usage\NetworkUsage.dll"
		File "Modules\Network Usage\NetworkUsage.pdb"
	SectionEnd

	Section "Quick Launch"
		SetOverwrite try
		SetOutPath "$INSTDIR\Modules\Quick Launch\"
		File "Modules\Quick Launch\QuickLaunch.dll"
		File "Modules\Quick Launch\QuickLaunch.pdb"
	SectionEnd

	Section "SpeedFan Sensor Monitor"
		SetOverwrite try
		SetOutPath "$INSTDIR\Modules\SpeedFan Sensor Monitor\"
		File "Modules\SpeedFan Sensor Monitor\SpeedFanSensorMonitor.dll"
		File "Modules\SpeedFan Sensor Monitor\SpeedFanSensorMonitor.pdb"
  SectionEnd

	Section "System Uptime"
		SetOverwrite try
		SetOutPath "$INSTDIR\Modules\System Uptime\"
		File "Modules\System Uptime\SystemUptime.dll"
		File "Modules\System Uptime\SystemUptime.pdb"
	SectionEnd

	Section "uTorrent Watcher"
		SetOverwrite try
		SetOutPath "$INSTDIR\Modules\uTorrent Watcher\"
		File "Modules\uTorrent Watcher\Json.dll"
		File "Modules\uTorrent Watcher\uTorrentWatcher.dll"
		File "Modules\uTorrent Watcher\uTorrentWatcher.pdb"
  SectionEnd

	Section "Volume Control"
		SetOverwrite try
		SetOutPath "$INSTDIR\Modules\Volume Control\"
		File "Modules\Volume Control\CoreAudioApi.dll"
		File "Modules\Volume Control\CoreAudioApi.pdb"
		File "Modules\Volume Control\VolumeControl.dll"
		File "Modules\Volume Control\VolumeControl.pdb"
		File "Modules\Volume Control\WaveLibMixer.dll"
	SectionEnd

	Section "Weather Conditions"
		SetOverwrite try
		SetOutPath "$INSTDIR\Modules\Weather Conditions\"
		File "Modules\Weather Conditions\WeatherConditions.dll"
		File "Modules\Weather Conditions\WeatherConditions.pdb"
	SectionEnd

	Section "Winamp Remote Control"
		SetOverwrite try
		SetOutPath "$INSTDIR\Modules\Winamp Remote Control\"
		File "Modules\Winamp Remote Control\WinampRemoteControl.dll"
		File "Modules\Winamp Remote Control\WinampRemoteControl.pdb"
	SectionEnd

	Section "Windows Media Player Remote Control"
		SetOverwrite try
		SetOutPath "$INSTDIR\Modules\Windows Media Player Remote Control\"
		File "Modules\Windows Media Player Remote Control\Interop.wmpuiceLib.dll"
		File "Modules\Windows Media Player Remote Control\WindowsMediaPlayerRemoteControl.dll"
		File "Modules\Windows Media Player Remote Control\WindowsMediaPlayerRemoteControl.pdb"
		File "Modules\Windows Media Player Remote Control\wmpuice.dll"
	SectionEnd

SectionGroupEnd

#Section "Module SDK"
#	CreateDirectory "$INSTDIR\Module SDK\"
#	SetOutPath "$INSTDIR\Module SDK\"
#	File /r "Module SDK\*.*"
#SectionEnd

# ========================================================================================================================================================================================================================================================================
#	Skins Section
# ========================================================================================================================================================================================================================================================================

SectionGroup /e "Skins"

  Section "Luna"
    SetOverwrite try
    SetOutPath "$INSTDIR\Skins\Luna\"
    File /r "Skins\Luna\*.*"
  SectionEnd

  Section "Luna Element"
    SetOverwrite try
    SetOutPath "$INSTDIR\Skins\Luna Element\"
    File /r "Skins\Luna Element\*.*"
  SectionEnd

  Section "Vista"
    SetOverwrite try
    SetOutPath "$INSTDIR\Skins\Vista\"
    File /r "Skins\Vista\*.*"
  SectionEnd

  Section "Watercolor Emico"
    SetOverwrite try
    SetOutPath "$INSTDIR\Skins\Watercolor Emico\"
    File /r "Skins\Watercolor Emico\*.*"
  SectionEnd

  Section "Windows 7"
    SetOverwrite try
    SetOutPath "$INSTDIR\Skins\Windows 7\"
    File /r "Skins\Windows 7\*.*"
  SectionEnd

SectionGroupEnd

# ========================================================================================================================================================================================================================================================================
#	Shortcuts Section
# ========================================================================================================================================================================================================================================================================

SectionGroup /e "Shortcuts"

	Section "Add shortcuts to the Start Menu"
		SetOverwrite try
		SetShellVarContext all
		SetOutPath "$INSTDIR\"
		CreateDirectory "$SMPROGRAMS\InfoBar"
		CreateShortCut "$SMPROGRAMS\InfoBar\InfoBar.lnk" "$INSTDIR\InfoBar.exe"
		CreateShortCut "$SMPROGRAMS\InfoBar\Uninstall.lnk" "$INSTDIR\uninstall.exe"
	SectionEnd

	Section /o "Add a shortcut to the Desktop"
		SetOverwrite try
		SetOutPath "$INSTDIR\"
		CreateShortCut "$DESKTOP\InfoBar.lnk" "$INSTDIR\InfoBar.exe"
	SectionEnd

SectionGroupEnd

# ========================================================================================================================================================================================================================================================================
#	Finished Section
# ========================================================================================================================================================================================================================================================================

Section -FinishSection
	${GetFileVersion} "$INSTDIR\InfoBar.exe" $R0
	WriteRegStr HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\InfoBar" "DisplayIcon" "$INSTDIR\InfoBar.exe"
	WriteRegStr HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\InfoBar" "DisplayName" "InfoBar"
	WriteRegStr HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\InfoBar" "DisplayVersion" "$R0"
	WriteRegDWORD HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\InfoBar" "EstimatedSize" 477
	WriteRegStr HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\InfoBar" "InstallDate" "${DATE}"
	WriteRegStr HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\InfoBar" "InstallLocation" "$INSTDIR"
	WriteRegStr HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\InfoBar" "Publisher" "Jim Laski"
	WriteRegStr HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\InfoBar" "UninstallString" "$INSTDIR\uninstall.exe"
	WriteUninstaller "$INSTDIR\uninstall.exe"
SectionEnd

# ========================================================================================================================================================================================================================================================================
#	Uninstall Section
# ========================================================================================================================================================================================================================================================================

Section Uninstall
	DeleteRegKey HKCR ".InfoBarSkin"
	DeleteRegKey HKCR "InfoBar.InfoBarSkin"

	DeleteRegKey HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\InfoBar"

	RMDir /r /REBOOTOK "$INSTDIR"

	SetShellVarContext all
	RMDir /r "$SMPROGRAMS\InfoBar"
	Delete /REBOOTOK "$DESKTOP\InfoBar.lnk"
SectionEnd

