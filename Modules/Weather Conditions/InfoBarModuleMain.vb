Public Class InfoBarModuleMain
  Inherits InfoBarModule

  ' Unique ID. If same as other module, the first module will be loaded, the next will be ignored.
  Private Const InfoBarModuleGUID As String = "{E03C4859-2681-47ca-9595-3B5223AB6FA5}"

#Region "InfoBar Service Objects"
  Private Skin As New InfoBar.Services.Skinning
  Private Tooltip As New InfoBar.Services.Tooltips
  Private IconTheme As New InfoBar.Services.IconTheme
  Private MainMenu As New InfoBar.Services.PopupMenus.MainMenu
  Private Settings As New InfoBar.Services.Settings
#End Region

#Region "Private Variables"
  Private Class DailyWeatherForecast
    Public Day As String
    Public DayDate As String
    Public Condition As String
    Public ConditionImage As Image
    Public HighTemp As String
    Public LowTemp As String
  End Class

  Dim uTemp As String
  Dim curCondImgStartY As Integer
  Dim fdcWidth1 As Integer, fdcWidth2 As Integer
  Dim fdrHeight(4) As Integer

  Private m_ModuleToolbarBitmap As Bitmap
  Private m_ModuleTooltipBitmap As Bitmap
  Private m_Bounds As Rectangle
  Private m_SettingsDialog As New Settings
  Private m_SettingsDialogInit As Boolean
  Private m_Enabled As Boolean
  Private m_IsManualUpdate As Boolean
  Private m_UpdateInProgress As Boolean
  Private m_ErrorUpdating As Boolean
  Private m_ErrorMessage As String
  Private WithEvents m_WorkerThread As New System.ComponentModel.BackgroundWorker
  Private m_WeatherButtonBounds As Rectangle
  Private m_WeatherButtonState As Integer
  Private m_LastUpdateCheck As DateTime
  Private m_CurrentConditionImage As Image
  Private m_CurrentTemp As String
  Private m_CurrentCondition As String
  Private m_FeelsLike As String
  Private m_DewPoint As String
  Private m_Pressure As String
  Private m_Humidity As String
  Private m_Visibility As String
  Private m_UVIndex As String
  Private m_Wind As String
  Private m_Sunrise As String
  Private m_Sunset As String
  Private m_Moon As String
  Private m_MoonImage As Image
  Private m_LastUpdated As String
  Private m_DayToDayForecast(0 To 4) As DailyWeatherForecast
#End Region

#Region "Settings Variables"
  Private Setting_WeatherLocation As String
  Private Setting_WeatherLocationID As String
  Private Setting_MeasurementSystem As Integer
  Private Setting_UpdateAtIntervals As Boolean
  Private Setting_UpdateInterval As Integer
  Private Setting_ShowIcon As Boolean
  Private Setting_ShowTextTemp As Boolean
  Private Setting_ShowTextCondition As Boolean
  Private Setting_WeatherImageSet As String
  Private Setting_TooltipContents_Location As Boolean
  Private Setting_TooltipContents_LastUpdated As Boolean
  Private Setting_TooltipContents_CurrentConditionsImage As Boolean
  Private Setting_TooltipContents_CurrentConditions As Boolean
  Private Setting_TooltipContents_CurrentTemperature As Boolean
  Private Setting_TooltipContents_FeelsLikeTemperature As Boolean
  Private Setting_TooltipContents_Humidity As Boolean
  Private Setting_TooltipContents_Visibility As Boolean
  Private Setting_TooltipContents_UVIndex As Boolean
  Private Setting_TooltipContents_DewPoint As Boolean
  Private Setting_TooltipContents_Pressure As Boolean
  Private Setting_TooltipContents_Wind As Boolean
  Private Setting_TooltipContents_SunriseAndSunset As Boolean
  Private Setting_TooltipContents_MoonPhase As Boolean
  Private Setting_TooltipContents_FiveDayForecast As Boolean
#End Region

#Region "Module Information"

  Dim ModuleInfo As New ApplicationServices.AssemblyInfo(System.Reflection.Assembly.GetExecutingAssembly)

  ' This gives InfoBar your unique ID for the module. Generate it with the GUID tool, use registry format.
  Public Overrides ReadOnly Property ModuleGUID() As String
    Get
      Return InfoBarModuleGUID
    End Get
  End Property

  ' This gives InfoBar a friendly name for the module.
  Public Overrides ReadOnly Property ModuleName() As String
    Get
      Return ModuleInfo.Title
    End Get
  End Property

  ' This gives InfoBar a copy of the modules icon for the settings dialog.
  Public Overrides ReadOnly Property ModuleIcon() As Image
    Get
      Return My.Resources.icon
    End Get
  End Property

  ' This gives InfoBar the author of the module, used in settings dialog.
  Public Overrides ReadOnly Property ModuleAuthor() As String
    Get
      Return ModuleInfo.CompanyName
    End Get
  End Property

  ' This gives InfoBar the version of the module, used in settings dialog.
  ' This must be the same as the file version.
  Public Overrides ReadOnly Property ModuleVersion() As String
    Get
      Return ModuleInfo.Version.ToString
    End Get
  End Property

  ' InfoBar retrieves this for the settings page.
  Public Overrides ReadOnly Property ModuleDescription() As String
    Get
      Return "Displays the current temperature and current condition icon, and more information in the tooltip, for your local area."
    End Get
  End Property

  ' InfoBar retrieves this for the settings page.
  Public Overrides ReadOnly Property ModuleCopyright() As String
    Get
      Return ModuleInfo.Copyright
    End Get
  End Property

  ' InfoBar retrieves this for the settings page.
  Public Overrides ReadOnly Property ModuleEmail() As String
    Get
      Return "info@nightiguana.com"
    End Get
  End Property

  ' InfoBar retrieves this for the settings page.
  Public Overrides ReadOnly Property ModuleHomepage() As String
    Get
      Return "http://www.nightiguana.com"
    End Get
  End Property

  ' InfoBar will use this to determine if the module is shown on the UI.
  ' It will be set to true/false when the user enables/disables the module, respectively.
  Public Overrides Property ModuleEnabled() As Boolean
    Get
      Return m_Enabled
    End Get
    Set(ByVal value As Boolean)
      m_Enabled = value
    End Set
  End Property

#End Region

#Region "Module Initialization/Finalization/Updates"

  ' InfoBar will call this when your module is enabled.
  Public Overrides Sub InitializeModule()
    m_WorkerThread.WorkerSupportsCancellation = False
    m_IsManualUpdate = True
    Weather_Update()
  End Sub

  ' InfoBar will call this when your module is disabled.
  Public Overrides Sub FinalizeModule()
    '
  End Sub

  ' InfoBar will call this every 1 second. Make sure to do work only when needed.
  ' TIP: If m_Enabled is set to false, don't do any work to save CPU time unless really needed.
  Public Overrides Sub TimerTick(ByRef bModuleIsDirty As Boolean)
    m_IsManualUpdate = False
    Weather_Update()
    bModuleIsDirty = False
  End Sub

#End Region

#Region "Module Drawing"

  Public Overrides Function GetModuleBitmap() As System.Drawing.Bitmap
    Try
      Return m_ModuleToolbarBitmap.Clone
    Catch ex As Exception
      Return Nothing
    End Try
  End Function

  Private Sub DrawModule()
    Dim width As Integer = 0, height As Integer = Skin.MaxModuleHeight

    Dim GR As Graphics = Graphics.FromHwnd(IntPtr.Zero)

    ' Draw Weather Button
    Dim bR As New Rectangle(0, 0, 1, height)
    Dim sText As String = vbNullString
    Dim weatherIcon As Image = Nothing
    If m_UpdateInProgress = True Then
      sText = "Updating..."
    Else
      If m_ErrorUpdating = True Then
        sText = "Error"
      Else
        If Setting_ShowTextTemp Then sText = sText & m_CurrentTemp
        If Setting_ShowTextTemp AndAlso Setting_ShowTextCondition Then sText = sText & " - "
        If Setting_ShowTextCondition Then sText = sText & m_CurrentCondition
      End If
    End If
    Dim bFR As Rectangle
    If m_CurrentConditionImage IsNot Nothing Then
      bFR = Skin.MeasureButton(Gr, bR, m_CurrentConditionImage, sText, m_WeatherButtonState, False)
    Else
      If m_ErrorUpdating = True Then
        If IconTheme.IsDefaultTheme = True Then
          weatherIcon = My.Resources.error_icon
        Else
          weatherIcon = IconTheme.GetThemeIcon(InfoBarModuleGUID, "Error")
          If weatherIcon Is Nothing Then weatherIcon = My.Resources.error_icon
        End If
      ElseIf m_UpdateInProgress = True Then
        If IconTheme.IsDefaultTheme = True Then
          weatherIcon = My.Resources.refresh
        Else
          weatherIcon = IconTheme.GetThemeIcon(InfoBarModuleGUID, "Refresh")
          If weatherIcon Is Nothing Then weatherIcon = My.Resources.refresh
        End If
        bFR = Skin.MeasureButton(GR, bR, weatherIcon, sText, m_WeatherButtonState, False)
      End If
    End If
    width = bFR.Width
    GR.Dispose()

    If width <= 1 Or height <= 1 Then Exit Sub
    Dim bmpTemp As New Bitmap(width, height, System.Drawing.Imaging.PixelFormat.Format32bppArgb)
    GR = Graphics.FromImage(bmpTemp)
    Dim curX As Integer = 0

    ' Draw Weather Button
    bR = New Rectangle(curX, 0, 1, height)
    If m_CurrentConditionImage IsNot Nothing Then
      bFR = Skin.DrawButton(GR, bR, m_CurrentConditionImage, sText, m_WeatherButtonState, False)
    Else
      If m_ErrorUpdating = True Then
      ElseIf m_UpdateInProgress = True Then
        bFR = Skin.DrawButton(GR, bR, weatherIcon, sText, m_WeatherButtonState, False)
      End If
    End If
    m_WeatherButtonBounds = bFR

    GR.Dispose()
    m_ModuleToolbarBitmap = bmpTemp.Clone
    bmpTemp.Dispose()
  End Sub

  Public Overrides Function GetTooltipBitmap() As System.Drawing.Bitmap
    Try
      Return m_ModuleTooltipBitmap
    Catch ex As Exception
      Return Nothing
    End Try
  End Function

  Private Sub DrawTooltip(ByVal ObjectID As String)
    Select Case ObjectID
      Case "WeatherConditions"
        Dim GR As Graphics
        GR = Graphics.FromHwnd(IntPtr.Zero)

        Dim nextX As Integer = 0, nextY As Integer = 0
        Dim maxWidth As Integer, maxHeight As Integer
        Dim tr As Rectangle
        Dim bNeedSep As Boolean
        fdcWidth1 = 0 : fdcWidth2 = 0

        ' Error Message
        If m_ErrorUpdating Then

          tr = Skin.MeasureText(Gr, m_ErrorMessage, Skinning.SkinTextPart.TooltipText, 400)
          maxWidth = tr.Width
          nextY += tr.Height
          maxHeight = nextY

        ElseIf m_UpdateInProgress = True Then

          tr = Skin.MeasureText(Gr, "Please wait while new weather information is downloaded...", Skinning.SkinTextPart.TooltipText, 400)
          maxWidth = tr.Width
          nextY += tr.Height
          maxHeight = nextY

        Else

          ' Location and Last Updated
          If Setting_TooltipContents_Location Or Setting_TooltipContents_LastUpdated Then
            Dim sText As String = vbNullString
            If Setting_TooltipContents_Location Then sText &= "Current weather for " & Setting_WeatherLocation & vbCrLf
            If Setting_TooltipContents_LastUpdated Then sText &= m_LastUpdated & vbCrLf
            tr = Skin.MeasureText(Gr, sText, Skinning.SkinTextPart.TooltipText)
            If tr.Width > maxWidth Then maxWidth = tr.Width
            nextY += tr.Height + Skin.TooltipSeparatorHeight
          End If

          ' Current Conditions Image
          If Setting_TooltipContents_CurrentConditionsImage Then
            curCondImgStartY = nextY
            tr = New Rectangle(0, nextY, 100, 100)
            If (nextX + tr.Width + 5) > maxWidth Then maxWidth = nextX + tr.Width + 5
            If (nextY + tr.Height + 5) > maxHeight Then maxHeight = nextY + tr.Height + 5
            nextX += tr.Width + 5
            bNeedSep = True
          Else
            curCondImgStartY = 0
            nextX = 0
          End If

          ' Current Conditions
          If Setting_TooltipContents_CurrentConditions Then
            tr = Skin.MeasureText(Gr, m_CurrentCondition, Skinning.SkinTextPart.TooltipText)
            If nextX + tr.Width > maxWidth Then maxWidth = nextX + tr.Width
            nextY += tr.Height
            If nextX <> 0 Then
              If nextY - curCondImgStartY >= 105 Then nextX = 0
            Else
              maxHeight = nextY
            End If
            bNeedSep = True
          End If

          ' Current Temperature
          If Setting_TooltipContents_CurrentTemperature Then
            tr = Skin.MeasureText(Gr, m_CurrentTemp, Skinning.SkinTextPart.TooltipText)
            If nextX + tr.Width > maxWidth Then maxWidth = nextX + tr.Width
            nextY += tr.Height
            If nextX <> 0 Then
              If nextY - curCondImgStartY >= 105 Then nextX = 0
            Else
              maxHeight = nextY
            End If
            bNeedSep = True
          End If

          ' Feels Like Temperature
          If Setting_TooltipContents_FeelsLikeTemperature Then
            tr = Skin.MeasureText(Gr, m_FeelsLike, Skinning.SkinTextPart.TooltipText)
            If nextX + tr.Width > maxWidth Then maxWidth = nextX + tr.Width
            nextY = nextY + tr.Height
            If nextX <> 0 Then
              If nextY - curCondImgStartY >= 105 Then nextX = 0
            Else
              maxHeight = nextY
            End If
            bNeedSep = True
          End If

          ' Humidity
          If Setting_TooltipContents_Humidity Then
            tr = Skin.MeasureText(Gr, m_Humidity, Skinning.SkinTextPart.TooltipText)
            If nextX + tr.Width > maxWidth Then maxWidth = nextX + tr.Width
            nextY = nextY + tr.Height
            If nextX <> 0 Then
              If nextY - curCondImgStartY >= 105 Then nextX = 0
            Else
              maxHeight = nextY
            End If
            bNeedSep = True
          End If

          ' Visibility
          If Setting_TooltipContents_Visibility Then
            tr = Skin.MeasureText(Gr, m_Visibility, Skinning.SkinTextPart.TooltipText)
            If nextX + tr.Width > maxWidth Then maxWidth = nextX + tr.Width
            nextY = nextY + tr.Height
            If nextX <> 0 Then
              If nextY - curCondImgStartY >= 105 Then nextX = 0
            Else
              maxHeight = nextY
            End If
            bNeedSep = True
          End If

          ' UV Index
          If Setting_TooltipContents_UVIndex Then
            tr = Skin.MeasureText(Gr, m_UVIndex, Skinning.SkinTextPart.TooltipText)
            If nextX + tr.Width > maxWidth Then maxWidth = nextX + tr.Width
            nextY = nextY + tr.Height
            If nextX <> 0 Then
              If nextY - curCondImgStartY >= 105 Then nextX = 0
            Else
              maxHeight = nextY
            End If
            bNeedSep = True
          End If

          ' Dew Point
          If Setting_TooltipContents_DewPoint Then
            tr = Skin.MeasureText(Gr, m_DewPoint, Skinning.SkinTextPart.TooltipText)
            If nextX + tr.Width > maxWidth Then maxWidth = nextX + tr.Width
            nextY = nextY + tr.Height
            If nextX <> 0 Then
              If nextY - curCondImgStartY >= 105 Then nextX = 0
            Else
              maxHeight = nextY
            End If
            bNeedSep = True
          End If

          ' Pressure
          If Setting_TooltipContents_Pressure Then
            tr = Skin.MeasureText(Gr, m_Pressure, Skinning.SkinTextPart.TooltipText)
            If nextX + tr.Width > maxWidth Then maxWidth = nextX + tr.Width
            nextY = nextY + tr.Height
            If nextX <> 0 Then
              If nextY - curCondImgStartY >= 105 Then nextX = 0
            Else
              maxHeight = nextY
            End If
            bNeedSep = True
          End If

          ' Wind
          If Setting_TooltipContents_Wind Then
            tr = Skin.MeasureText(Gr, m_Wind, Skinning.SkinTextPart.TooltipText)
            If nextX + tr.Width > maxWidth Then maxWidth = nextX + tr.Width
            nextY = nextY + tr.Height
            If nextX <> 0 Then
              If nextY - curCondImgStartY >= 105 Then nextX = 0
            Else
              maxHeight = nextY
            End If
            bNeedSep = True
          End If

          ' Sunrise and Sunset
          If Setting_TooltipContents_SunriseAndSunset Then
            tr = Skin.MeasureText(Gr, m_Sunrise & "  -  " & m_Sunset, Skinning.SkinTextPart.TooltipText)
            If nextX + tr.Width > maxWidth Then maxWidth = nextX + tr.Width
            nextY = nextY + tr.Height
            If nextX <> 0 Then
              If nextY - curCondImgStartY >= 105 Then nextX = 0
            Else
              maxHeight = nextY
            End If
            bNeedSep = True
          End If

          ' Moon Phase
          If Setting_TooltipContents_MoonPhase Then
            If bNeedSep = True Then nextY = nextY + Skin.TooltipSeparatorHeight

            tr = Skin.MeasureText(Gr, m_Moon, Skinning.SkinTextPart.TooltipText)
            If (tr.Width + tr.Height + 2) > maxWidth Then maxWidth = (tr.Width + tr.Height + 2)
            nextY = nextY + tr.Height
            If nextX <> 0 Then
              maxHeight = curCondImgStartY + 105 + tr.Height
              nextY = maxHeight
            Else
              maxHeight = nextY
            End If
            nextX = 0
          End If

          ' Five Day Forecast
          If Setting_TooltipContents_FiveDayForecast Then
            If bNeedSep = True Then nextY = nextY + Skin.TooltipSeparatorHeight

            ' Draw Title
            tr = Skin.MeasureText(Gr, "5-Day Forecast:", Skinning.SkinTextPart.TooltipText)
            If maxWidth < tr.Width Then maxWidth = tr.Width
            nextY += tr.Height + 5

            Dim iTotalWidth As Integer, iTotalHeight As Integer

            Dim I As Integer
            For I = 0 To 4
              ' First Column
              tr = Skin.MeasureText(Gr, m_DayToDayForecast(I).Day & vbCrLf & m_DayToDayForecast(I).DayDate, Skinning.SkinTextPart.TooltipText)
              If tr.Width > fdcWidth1 Then fdcWidth1 = tr.Width
              fdrHeight(I) = tr.Height

              ' Second Column
              tr = Skin.MeasureText(Gr, m_DayToDayForecast(I).Condition & vbCrLf & m_DayToDayForecast(I).HighTemp & " / " & m_DayToDayForecast(I).LowTemp, Skinning.SkinTextPart.TooltipText)
              If (tr.Width + 37) > fdcWidth2 Then fdcWidth2 = tr.Width + 37
              If tr.Height > fdrHeight(I) Then fdrHeight(I) = tr.Height

              iTotalHeight += fdrHeight(I)
            Next

            ' Add space to end of first column
            fdcWidth1 += 10
            iTotalWidth = fdcWidth1 + fdcWidth2

            ' Add space to end of each row
            iTotalHeight += 20

            If iTotalWidth > maxWidth Then maxWidth = iTotalWidth
            If nextX <> 0 Then
              maxHeight = curCondImgStartY + 105 + iTotalHeight
              nextY = maxHeight
            Else
              maxHeight = nextY + iTotalHeight
            End If
            nextX = 0
          End If

        End If

        GR.Dispose()

        Dim bmpTemp As New Bitmap(maxWidth, maxHeight, System.Drawing.Imaging.PixelFormat.Format32bppArgb)
        GR = Graphics.FromImage(bmpTemp)

        Dim rect As Rectangle, curX As Integer, curY As Integer
        'Dim tcm As Skinning.SkinMargins = Skin.ContentMargins(Skinning.SkinMarginPart.Tooltip)

        curX = 0
        curY = 0

        ' Error Message
        If m_ErrorUpdating Then

          curX = 0
          rect = Skin.MeasureText(Gr, m_ErrorMessage, Skinning.SkinTextPart.TooltipText, 400)
          rect.X = curX
          rect.Y = curY
          Skin.DrawText(GR, m_ErrorMessage, rect, Skinning.SkinTextPart.TooltipText, StringAlignment.Near, StringAlignment.Near, True)

        ElseIf m_UpdateInProgress = True Then

          curX = 0
          rect = Skin.MeasureText(Gr, "Please wait while new weather information is downloaded...", Skinning.SkinTextPart.TooltipText, 400)
          rect.X = curX
          rect.Y = curY
          Skin.DrawText(GR, "Please wait while new weather information is downloaded...", rect, Skinning.SkinTextPart.TooltipText, StringAlignment.Near, StringAlignment.Near)

        Else

          ' Location and Last Updated
          If Setting_TooltipContents_Location Or Setting_TooltipContents_LastUpdated Then

            Dim sText As String = vbNullString
            If Setting_TooltipContents_Location Then sText = sText & "Current weather for " & Setting_WeatherLocation & vbCrLf
            If Setting_TooltipContents_LastUpdated Then sText = sText & m_LastUpdated & vbCrLf
            rect = Skin.MeasureText(Gr, sText, Skinning.SkinTextPart.TooltipText)
            rect.X = curX
            rect.Y = curY
            Skin.DrawText(GR, sText, rect, Skinning.SkinTextPart.TooltipText, StringAlignment.Near, StringAlignment.Near, True)
            curY = curY + rect.Height

            ' Draw Separator
            rect = New Rectangle(curX, curY, maxWidth, Skin.TooltipSeparatorHeight)
            Skin.DrawTooltipSeparator(Gr, rect)
            curY = curY + Skin.TooltipSeparatorHeight

          End If

          ' Current Conditions Image
          If Setting_TooltipContents_CurrentConditionsImage Then

            rect = New Rectangle(curX, curY, 100, 100)
            Gr.DrawImage(m_CurrentConditionImage, rect)
            curX = curX + 105
            bNeedSep = True

          End If

          ' Current Conditions
          If Setting_TooltipContents_CurrentConditions Then

            rect = Skin.MeasureText(Gr, m_CurrentCondition, Skinning.SkinTextPart.TooltipText)
            rect.X = curX
            rect.Y = curY
            Skin.DrawText(GR, m_CurrentCondition, rect, Skinning.SkinTextPart.TooltipText, StringAlignment.Near, StringAlignment.Near, True)
            curY = curY + rect.Height
            If curX <> 0 Then If curY - curCondImgStartY >= 105 Then curX = 0
            bNeedSep = True

          End If

          ' Current Temperature
          If Setting_TooltipContents_CurrentTemperature Then

            rect = Skin.MeasureText(Gr, m_CurrentTemp, Skinning.SkinTextPart.TooltipText)
            rect.X = curX
            rect.Y = curY
            Skin.DrawText(GR, m_CurrentTemp, rect, Skinning.SkinTextPart.TooltipText, StringAlignment.Near, StringAlignment.Near, True)
            curY = curY + rect.Height
            If curX <> 0 Then If curY - curCondImgStartY >= 105 Then curX = 0
            bNeedSep = True

          End If

          ' Feels Like Temperature
          If Setting_TooltipContents_FeelsLikeTemperature Then

            rect = Skin.MeasureText(Gr, m_FeelsLike, Skinning.SkinTextPart.TooltipText)
            rect.X = curX
            rect.Y = curY
            Skin.DrawText(GR, m_FeelsLike, rect, Skinning.SkinTextPart.TooltipText, StringAlignment.Near, StringAlignment.Near, True)
            curY = curY + rect.Height
            If curX <> 0 Then If curY - curCondImgStartY >= 105 Then curX = 0
            bNeedSep = True

          End If

          ' Humidity
          If Setting_TooltipContents_Humidity Then

            rect = Skin.MeasureText(Gr, m_Humidity, Skinning.SkinTextPart.TooltipText)
            rect.X = curX
            rect.Y = curY
            Skin.DrawText(GR, m_Humidity, rect, Skinning.SkinTextPart.TooltipText, StringAlignment.Near, StringAlignment.Near, True)
            curY = curY + rect.Height
            If curX <> 0 Then If curY - curCondImgStartY >= 105 Then curX = 0
            bNeedSep = True

          End If

          ' Visibility
          If Setting_TooltipContents_Visibility Then

            rect = Skin.MeasureText(Gr, m_Visibility, Skinning.SkinTextPart.TooltipText)
            rect.X = curX
            rect.Y = curY
            Skin.DrawText(GR, m_Visibility, rect, Skinning.SkinTextPart.TooltipText, StringAlignment.Near, StringAlignment.Near, True)
            curY = curY + rect.Height
            If curX <> 0 Then If curY - curCondImgStartY >= 105 Then curX = 0
            bNeedSep = True

          End If

          ' UV Index
          If Setting_TooltipContents_UVIndex Then

            rect = Skin.MeasureText(Gr, m_UVIndex, Skinning.SkinTextPart.TooltipText)
            rect.X = curX
            rect.Y = curY
            Skin.DrawText(GR, m_UVIndex, rect, Skinning.SkinTextPart.TooltipText, StringAlignment.Near, StringAlignment.Near, True)
            curY = curY + rect.Height
            If curX <> 0 Then If curY - curCondImgStartY >= 105 Then curX = 0
            bNeedSep = True

          End If

          ' Dew Point
          If Setting_TooltipContents_DewPoint Then

            rect = Skin.MeasureText(Gr, m_DewPoint, Skinning.SkinTextPart.TooltipText)
            rect.X = curX
            rect.Y = curY
            Skin.DrawText(GR, m_DewPoint, rect, Skinning.SkinTextPart.TooltipText, StringAlignment.Near, StringAlignment.Near, True)
            curY = curY + rect.Height
            If curX <> 0 Then If curY - curCondImgStartY >= 105 Then curX = 0
            bNeedSep = True

          End If

          ' Pressure
          If Setting_TooltipContents_Pressure Then

            rect = Skin.MeasureText(Gr, m_Pressure, Skinning.SkinTextPart.TooltipText)
            rect.X = curX
            rect.Y = curY
            Skin.DrawText(GR, m_Pressure, rect, Skinning.SkinTextPart.TooltipText, StringAlignment.Near, StringAlignment.Near, True)
            curY = curY + rect.Height
            If curX <> 0 Then If curY - curCondImgStartY >= 105 Then curX = 0
            bNeedSep = True

          End If

          ' Wind
          If Setting_TooltipContents_Wind Then

            rect = Skin.MeasureText(Gr, m_Wind, Skinning.SkinTextPart.TooltipText)
            rect.X = curX
            rect.Y = curY
            Skin.DrawText(GR, m_Wind, rect, Skinning.SkinTextPart.TooltipText, StringAlignment.Near, StringAlignment.Near, True)
            curY = curY + rect.Height
            If curX <> 0 Then If curY - curCondImgStartY >= 105 Then curX = 0
            bNeedSep = True

          End If

          ' Sunrise and Sunset
          If Setting_TooltipContents_SunriseAndSunset Then

            rect = Skin.MeasureText(Gr, m_Sunrise & "  -  " & m_Sunset, Skinning.SkinTextPart.TooltipText)
            rect.X = curX
            rect.Y = curY
            Skin.DrawText(GR, m_Sunrise & "  -  " & m_Sunset, rect, Skinning.SkinTextPart.TooltipText, StringAlignment.Near, StringAlignment.Near, True)
            curY = curY + rect.Height
            If curX <> 0 Then If curY - curCondImgStartY >= 105 Then curX = 0
            bNeedSep = True

          End If

          ' Moon Phase
          If Setting_TooltipContents_MoonPhase Then

            If curX <> 0 Then curY = curCondImgStartY + 105
            curX = 0

            If bNeedSep = True Then
              ' Draw Separator
              rect = New Rectangle(curX, curY, maxWidth, Skin.TooltipSeparatorHeight)
              Skin.DrawTooltipSeparator(Gr, rect)
              curY = curY + Skin.TooltipSeparatorHeight
            End If

            rect = Skin.MeasureText(Gr, m_Moon, Skinning.SkinTextPart.TooltipText)
            rect.X = curX
            rect.Y = curY
            Gr.DrawImage(m_MoonImage, curX, curY, rect.Height, rect.Height)
            rect.X = rect.X + rect.Height + 5
            Skin.DrawText(GR, m_Moon, rect, Skinning.SkinTextPart.TooltipText, StringAlignment.Near, StringAlignment.Near, True)

            curY = curY + rect.Height

          End If

          ' Five Day Forecast
          If Setting_TooltipContents_FiveDayForecast Then

            If curX <> 0 Then curY = curCondImgStartY + 105
            curX = 0

            If bNeedSep = True Then
              ' Draw Separator
              rect = New Rectangle(curX, curY, maxWidth, Skin.TooltipSeparatorHeight)
              Skin.DrawTooltipSeparator(Gr, rect)
              curY = curY + Skin.TooltipSeparatorHeight
            End If

            ' Draw Title
            rect = Skin.MeasureText(Gr, "5-Day Forecast:", Skinning.SkinTextPart.TooltipText)
            rect.X = curX
            rect.Y = curY
            Skin.DrawText(GR, "5-Day Forecast:", rect, Skinning.SkinTextPart.TooltipText, StringAlignment.Near, StringAlignment.Near, True)
            curY = curY + rect.Height + 5

            ' Move to first row
            curX = 0

            Dim I As Integer
            For I = 0 To 4
              ' First Column
              rect = Skin.MeasureText(GR, m_DayToDayForecast(I).Day & vbCrLf & m_DayToDayForecast(I).DayDate, Skinning.SkinTextPart.TooltipText)
              rect.X = curX
              rect.Y = curY
              Skin.DrawText(GR, m_DayToDayForecast(I).Day & vbCrLf & m_DayToDayForecast(I).DayDate, rect, Skinning.SkinTextPart.TooltipText, StringAlignment.Near, StringAlignment.Near, True)
              curX += fdcWidth1

              ' Second Column
              GR.DrawImage(m_DayToDayForecast(I).ConditionImage, curX, curY + CInt(((fdrHeight(I) / 2) - 16)), 32, 32)

              rect = Skin.MeasureText(GR, m_DayToDayForecast(I).Condition & vbCrLf & m_DayToDayForecast(I).HighTemp & " / " & m_DayToDayForecast(I).LowTemp, Skinning.SkinTextPart.TooltipText)
              rect.X = curX + 37
              rect.Y = curY
              If rect.Height < 32 Then rect.Height = 32
              Skin.DrawText(GR, m_DayToDayForecast(I).Condition & vbCrLf & m_DayToDayForecast(I).HighTemp & " / " & m_DayToDayForecast(I).LowTemp, rect, Skinning.SkinTextPart.TooltipText, StringAlignment.Near, StringAlignment.Near, True)
              curX += fdcWidth2

              ' Move to next row
              curX = 0
              curY += fdrHeight(I) + 5
            Next

          End If

        End If

        GR.Dispose()
        m_ModuleTooltipBitmap = bmpTemp.Clone
        bmpTemp.Dispose()

    End Select
  End Sub

#End Region

#Region "Mouse/Keyboard/Menu Processing"

  ' Check to see if the mouse is on any of your UI rectangles.
  ' Set bWindowIsDirty to true if you need a redraw.
  Public Overrides Sub ProcessMouseMove(ByVal e As System.Windows.Forms.MouseEventArgs, ByRef bWindowIsDirty As Boolean)
    Dim pt As Point = Skin.ToolbarPointToModulePoint(e.Location, m_Bounds)
    ' Weather Button
    If m_WeatherButtonBounds.Contains(pt) Then
      If e.Button = Windows.Forms.MouseButtons.None Then
        If m_WeatherButtonState <> 1 Then
          m_WeatherButtonState = 1
          DrawModule()
          bWindowIsDirty = True
        End If
        Tooltip.SetTooltipOwner(InfoBarModuleGUID, "WeatherConditions")
        DrawTooltip("WeatherConditions")
        Tooltip.UpdateTooltip()
      ElseIf e.Button = Windows.Forms.MouseButtons.Left Then
        If m_WeatherButtonState <> 2 Then
          m_WeatherButtonState = 2
          DrawModule()
          bWindowIsDirty = True
        End If
      End If
    Else
      If m_WeatherButtonState <> 0 Then
        m_WeatherButtonState = 0
        DrawModule()
        bWindowIsDirty = True
      End If
    End If
  End Sub

  ' Check to see if the mouse was depressed on any of your UI rectangles.
  ' Set bWindowIsDirty to true if you need a redraw.
  Public Overrides Sub ProcessMouseUp(ByVal e As MouseEventArgs, ByRef bWindowIsDirty As Boolean)
    Dim pt As Point = Skin.ToolbarPointToModulePoint(e.Location, m_Bounds)
    ' Weather Button
    If m_WeatherButtonBounds.Contains(pt) = True Then
      If m_WeatherButtonState <> 1 Then
        m_WeatherButtonState = 1
        bWindowIsDirty = True
        DrawModule()
        Weather_VisitWebSite()
      End If
    Else
      If m_WeatherButtonState <> 0 Then
        m_WeatherButtonState = 0
        bWindowIsDirty = True
        DrawModule()
      End If
    End If
  End Sub

  ' Check to see if the mouse was pressed down on any of your UI rectangles.
  ' Set bWindowIsDirty to true if you need a redraw.
  Public Overrides Sub ProcessMouseDown(ByVal e As MouseEventArgs, ByRef bWindowIsDirty As Boolean)
    Dim pt As Point = Skin.ToolbarPointToModulePoint(e.Location, m_Bounds)
    ' Weather Button
    If m_WeatherButtonBounds.Contains(pt) = True Then
      If m_WeatherButtonState <> 2 Then
        m_WeatherButtonState = 2
        bWindowIsDirty = True
        DrawModule()
      End If
    Else
      If m_WeatherButtonState <> 0 Then
        m_WeatherButtonState = 0
        bWindowIsDirty = True
        DrawModule()
      End If
    End If
  End Sub

  ' Check to see if the mouse left any of your UI rectangles.
  ' Set bWindowIsDirty to true if you need a redraw.
  Public Overrides Sub ProcessMouseLeave(ByRef bWindowIsDirty As Boolean)
    ' Weather Button
    If m_WeatherButtonState <> 0 Then
      m_WeatherButtonState = 0
      bWindowIsDirty = True
      DrawModule()
    End If
  End Sub

  Public Overrides Sub ProcessDragDrop(ByVal e As System.Windows.Forms.DragEventArgs, ByRef bWindowIsDirty As Boolean)
    '
  End Sub

  Public Overrides Sub ProcessDragEnter(ByVal e As System.Windows.Forms.DragEventArgs, ByRef bWindowIsDirty As Boolean)
    '
  End Sub

  Public Overrides Sub ProcessDragLeave(ByVal e As System.EventArgs, ByRef bWindowIsDirty As Boolean)
    '
  End Sub

  Public Overrides Sub ProcessDragOver(ByVal e As System.Windows.Forms.DragEventArgs, ByRef bWindowIsDirty As Boolean)
    '
  End Sub

  Public Overrides Property ModuleBounds() As System.Drawing.Rectangle
    Set(ByVal value As System.Drawing.Rectangle)
      m_Bounds = value
    End Set
    Get
      Return m_Bounds
    End Get
  End Property

  ' InfoBar calls this when your module is right clicked. The popup menu will be shown.
  ' You can create menu items that appear at the top of the popup menu.
  Public Overrides Sub AddMainPopupMenuItems()
    ' A recommended standard feature is to allow users to access your module's settings
    ' dialog from the popup menu.

    Dim goIcon As Image, refreshIcon As Image, weatherIcon As Image
    If IconTheme.IsDefaultTheme = True Then
      goIcon = My.Resources.go
      refreshIcon = My.Resources.refresh
      weatherIcon = My.Resources.icon
    Else
      goIcon = IconTheme.GetThemeIcon(InfoBarModuleGUID, "Go")
      If goIcon Is Nothing Then goIcon = My.Resources.go

      refreshIcon = IconTheme.GetThemeIcon(InfoBarModuleGUID, "Refresh")
      If refreshIcon Is Nothing Then refreshIcon = My.Resources.refresh

      weatherIcon = IconTheme.GetThemeIcon(InfoBarModuleGUID, "Weather")
      If weatherIcon Is Nothing Then weatherIcon = My.Resources.icon
    End If

    MainMenu.AddMenuItem(InfoBarModuleGUID & "::" & "VISIT", "Visit Weather.com...", goIcon, True, True)
    MainMenu.AddSeparator()
    MainMenu.AddMenuItem(InfoBarModuleGUID & "::" & "UPDATE", "Update Now", refreshIcon, Not m_UpdateInProgress)
    MainMenu.AddMenuItem(InfoBarModuleGUID & "::" & "SETTINGS", "Weather Conditions Settings...", weatherIcon)

    ' Always add a separator when you're done.
    MainMenu.AddSeparator()
  End Sub

  ' InfoBar calls this when one of your module's menu items are selected.
  Public Overrides Sub ProcessMenuItemClick(ByVal Key As String)
    Select Case Key
      Case InfoBarModuleGUID & "::" & "VISIT"
        Weather_VisitWebSite()
      Case InfoBarModuleGUID & "::" & "UPDATE"
        m_IsManualUpdate = True
        Weather_Update()
      Case InfoBarModuleGUID & "::" & "SETTINGS"
        Settings.ShowSettingsDialog(InfoBarModuleGUID)
    End Select
  End Sub

#End Region

#Region "Settings Routines"

  ' InfoBar will call this when it needs to know if the module can show a settings dialog.
  Overrides ReadOnly Property HasSettingsDialog() As Boolean
    Get
      HasSettingsDialog = True
    End Get
  End Property

  Public Overrides ReadOnly Property SettingsDialog() As System.Windows.Forms.UserControl
    Get
      Return m_SettingsDialog
    End Get
  End Property

  Public Overrides Sub ApplySettings()
    Dim bUpdateWeather As Boolean = False

    If Setting_WeatherLocation <> m_SettingsDialog.txtLocation.Text Then
      Setting_WeatherLocation = m_SettingsDialog.txtLocation.Text
      bUpdateWeather = True
    End If

    If Setting_WeatherLocationID <> m_SettingsDialog.txtLocationID.Text Then
      Setting_WeatherLocationID = m_SettingsDialog.txtLocationID.Text
      bUpdateWeather = True
    End If

    Dim iMS As Integer = IIf(m_SettingsDialog.radMeasurementCustomary.Checked, 1, 2)
    If Setting_MeasurementSystem <> iMS Then
      Setting_MeasurementSystem = iMS
      bUpdateWeather = True
    End If

    Setting_UpdateAtIntervals = m_SettingsDialog.chkUpdateAtIntervals.Checked
    Setting_UpdateInterval = m_SettingsDialog.nudUpdateInterval.Value

    If Setting_ShowIcon <> m_SettingsDialog.chkShowIcon.Checked Then
      Setting_ShowIcon = m_SettingsDialog.chkShowIcon.Checked
      bUpdateWeather = True
    End If

    If Setting_ShowTextTemp <> m_SettingsDialog.chkTextTemp.Checked Then
      Setting_ShowTextTemp = m_SettingsDialog.chkTextTemp.Checked
      bUpdateWeather = True
    End If

    If Setting_ShowTextCondition <> m_SettingsDialog.chkTextCondition.Checked Then
      Setting_ShowTextCondition = m_SettingsDialog.chkTextCondition.Checked
      bUpdateWeather = True
    End If

    Dim I As Integer
    For I = 0 To m_SettingsDialog.lstTooltipContents.Items.Count - 1
      Dim bChecked As Boolean = m_SettingsDialog.lstTooltipContents.GetItemChecked(I)
      Dim sText As String = m_SettingsDialog.lstTooltipContents.GetItemText(m_SettingsDialog.lstTooltipContents.Items.Item(I))
      Select Case sText
        Case "Pressure"
          If Setting_TooltipContents_Pressure <> bChecked Then
            Setting_TooltipContents_Pressure = bChecked
            bUpdateWeather = True
          End If
        Case "Current Conditions"
          If Setting_TooltipContents_CurrentConditions <> bChecked Then
            Setting_TooltipContents_CurrentConditions = bChecked
            bUpdateWeather = True
          End If
        Case "Current Conditions Image"
          If Setting_TooltipContents_CurrentConditionsImage <> bChecked Then
            Setting_TooltipContents_CurrentConditionsImage = bChecked
            bUpdateWeather = True
          End If
        Case "Current Temperature"
          If Setting_TooltipContents_CurrentTemperature <> bChecked Then
            Setting_TooltipContents_CurrentTemperature = bChecked
            bUpdateWeather = True
          End If
        Case "Dew Point"
          If Setting_TooltipContents_DewPoint <> bChecked Then
            Setting_TooltipContents_DewPoint = bChecked
            bUpdateWeather = True
          End If
        Case "Feels Like Temperature"
          If Setting_TooltipContents_FeelsLikeTemperature <> bChecked Then
            Setting_TooltipContents_FeelsLikeTemperature = bChecked
            bUpdateWeather = True
          End If
        Case "Five Day Forecast"
          If Setting_TooltipContents_FiveDayForecast <> bChecked Then
            Setting_TooltipContents_FiveDayForecast = bChecked
            bUpdateWeather = True
          End If
        Case "Humidity"
          If Setting_TooltipContents_Humidity <> bChecked Then
            Setting_TooltipContents_Humidity = bChecked
            bUpdateWeather = True
          End If
        Case "Last Updated"
          If Setting_TooltipContents_LastUpdated <> bChecked Then
            Setting_TooltipContents_LastUpdated = bChecked
            bUpdateWeather = True
          End If
        Case "Location"
          If Setting_TooltipContents_Location <> bChecked Then
            Setting_TooltipContents_Location = bChecked
            bUpdateWeather = True
          End If
        Case "Moon Phase"
          If Setting_TooltipContents_MoonPhase <> bChecked Then
            Setting_TooltipContents_MoonPhase = bChecked
            bUpdateWeather = True
          End If
        Case "Sunrise and Sunset"
          If Setting_TooltipContents_SunriseAndSunset <> bChecked Then
            Setting_TooltipContents_SunriseAndSunset = bChecked
            bUpdateWeather = True
          End If
        Case "UV Index"
          If Setting_TooltipContents_UVIndex <> bChecked Then
            Setting_TooltipContents_UVIndex = bChecked
            bUpdateWeather = True
          End If
        Case "Visibility"
          If Setting_TooltipContents_Visibility <> bChecked Then
            Setting_TooltipContents_Visibility = bChecked
            bUpdateWeather = True
          End If
        Case "Wind"
          If Setting_TooltipContents_Wind <> bChecked Then
            Setting_TooltipContents_Wind = bChecked
            bUpdateWeather = True
          End If
      End Select
    Next

    If m_SettingsDialog.lvImageSet.SelectedItems.Count > 0 Then
      If Setting_WeatherImageSet <> m_SettingsDialog.lvImageSet.SelectedItems(0).Tag Then
        Setting_WeatherImageSet = m_SettingsDialog.lvImageSet.SelectedItems(0).Tag
        bUpdateWeather = True
      End If
    Else
      Setting_WeatherImageSet = Nothing
    End If

    If bUpdateWeather Then
      m_IsManualUpdate = True
      Weather_Update()
    Else
      DrawModule()
      Skin.UpdateWindow()
    End If
  End Sub

  ' InfoBar will call this when the application loads. Use this to set your internal settings.
  Public Overrides Sub LoadSettings(ByRef Doc As XmlDocument)
    ' Always set defaults first.
    Setting_WeatherLocation = "New York, NY (10010)"
    Setting_WeatherLocationID = "10010"
    Setting_MeasurementSystem = 1
    Setting_UpdateAtIntervals = True
    Setting_UpdateInterval = 15
    Setting_ShowIcon = True
    Setting_ShowTextTemp = True
    Setting_ShowTextCondition = False
    Setting_WeatherImageSet = "InfoBarInternalWeatherImageSet"

    Setting_TooltipContents_Pressure = True
    Setting_TooltipContents_CurrentConditions = True
    Setting_TooltipContents_CurrentConditionsImage = True
    Setting_TooltipContents_CurrentTemperature = True
    Setting_TooltipContents_DewPoint = True
    Setting_TooltipContents_FeelsLikeTemperature = True
    Setting_TooltipContents_FiveDayForecast = True
    Setting_TooltipContents_Humidity = True
    Setting_TooltipContents_LastUpdated = True
    Setting_TooltipContents_Location = True
    Setting_TooltipContents_MoonPhase = True
    Setting_TooltipContents_SunriseAndSunset = True
    Setting_TooltipContents_UVIndex = True
    Setting_TooltipContents_Visibility = True
    Setting_TooltipContents_Wind = True

    ' Now use the InfoBar Settings Service to load your settings.
    If Doc IsNot Nothing Then
      Setting_WeatherLocation = Settings.GetSetting(Doc, "location", "New York, NY (10010)")
      Setting_WeatherLocationID = Settings.GetSetting(Doc, "locationid", "10010")
      Setting_MeasurementSystem = Settings.GetSetting(Doc, "measurementsystem", 1)
      Setting_UpdateAtIntervals = Settings.GetSetting(Doc, "updateatintervals", True)
      Setting_UpdateInterval = Settings.GetSetting(Doc, "updateinterval", 15)
      If Setting_UpdateInterval < 15 Then Setting_UpdateInterval = 15
      If Setting_UpdateInterval > 1440 Then Setting_UpdateInterval = 1440
      Setting_ShowIcon = Settings.GetSetting(Doc, "showicon", True)
      Setting_ShowTextTemp = Settings.GetSetting(Doc, "showtexttemp", True)
      Setting_ShowTextCondition = Settings.GetSetting(Doc, "showtextcondition", False)
      Setting_WeatherImageSet = Settings.GetSetting(Doc, "imageset", "InfoBarInternalWeatherImageSet")

      Setting_TooltipContents_Pressure = Settings.GetSetting(Doc, "tooltipcontents/pressure", True)
      Setting_TooltipContents_CurrentConditions = Settings.GetSetting(Doc, "tooltipcontents/currentconditions", True)
      Setting_TooltipContents_CurrentConditionsImage = Settings.GetSetting(Doc, "tooltipcontents/currentconditionsimage", True)
      Setting_TooltipContents_CurrentTemperature = Settings.GetSetting(Doc, "tooltipcontents/currenttemperature", True)
      Setting_TooltipContents_DewPoint = Settings.GetSetting(Doc, "tooltipcontents/dewpoint", True)
      Setting_TooltipContents_FeelsLikeTemperature = Settings.GetSetting(Doc, "tooltipcontents/feelsliketemperature", True)
      Setting_TooltipContents_FiveDayForecast = Settings.GetSetting(Doc, "tooltipcontents/fivedayforecast", True)
      Setting_TooltipContents_Humidity = Settings.GetSetting(Doc, "tooltipcontents/humidity", True)
      Setting_TooltipContents_LastUpdated = Settings.GetSetting(Doc, "tooltipcontents/lastupdated", True)
      Setting_TooltipContents_Location = Settings.GetSetting(Doc, "tooltipcontents/location", True)
      Setting_TooltipContents_MoonPhase = Settings.GetSetting(Doc, "tooltipcontents/moonphase", True)
      Setting_TooltipContents_SunriseAndSunset = Settings.GetSetting(Doc, "tooltipcontents/sunriseandsunset", True)
      Setting_TooltipContents_UVIndex = Settings.GetSetting(Doc, "tooltipcontents/uvindex", True)
      Setting_TooltipContents_Visibility = Settings.GetSetting(Doc, "tooltipcontents/visibility", True)
      Setting_TooltipContents_Wind = Settings.GetSetting(Doc, "tooltipcontents/wind", True)
    End If

    ResetSettings()
  End Sub

  Public Overrides Sub ResetSettings()
    m_SettingsDialog.IsFormLoaded = False

    If m_SettingsDialogInit = False Then
      m_SettingsDialogInit = True
      m_SettingsDialog.Settings_Load()
    End If

    m_SettingsDialog.txtLocation.Text = Setting_WeatherLocation
    m_SettingsDialog.txtLocationID.Text = Setting_WeatherLocationID
    If Setting_MeasurementSystem = 1 Then
      m_SettingsDialog.radMeasurementCustomary.Checked = True
    Else
      m_SettingsDialog.radMeasurementMetric.Checked = True
    End If
    m_SettingsDialog.chkUpdateAtIntervals.Checked = Setting_UpdateAtIntervals
    m_SettingsDialog.nudUpdateInterval.Value = Setting_UpdateInterval
    m_SettingsDialog.chkShowIcon.Checked = Setting_ShowIcon
    m_SettingsDialog.chkTextTemp.Checked = Setting_ShowTextTemp
    m_SettingsDialog.chkTextCondition.Checked = Setting_ShowTextCondition

    Dim bEnabled As Boolean = m_SettingsDialog.chkUpdateAtIntervals.Checked
    m_SettingsDialog.lblIntervalEvery.Enabled = bEnabled
    m_SettingsDialog.nudUpdateInterval.Enabled = bEnabled
    m_SettingsDialog.lblCheckMinutes.Enabled = bEnabled

    Dim I As Integer
    For I = 0 To m_SettingsDialog.lstTooltipContents.Items.Count - 1
      Dim sText As String = m_SettingsDialog.lstTooltipContents.GetItemText(m_SettingsDialog.lstTooltipContents.Items.Item(I))
      Select Case sText
        Case "Pressure"
          m_SettingsDialog.lstTooltipContents.SetItemChecked(I, Setting_TooltipContents_Pressure)
        Case "Current Conditions"
          m_SettingsDialog.lstTooltipContents.SetItemChecked(I, Setting_TooltipContents_CurrentConditions)
        Case "Current Conditions Image"
          m_SettingsDialog.lstTooltipContents.SetItemChecked(I, Setting_TooltipContents_CurrentConditionsImage)
        Case "Current Temperature"
          m_SettingsDialog.lstTooltipContents.SetItemChecked(I, Setting_TooltipContents_CurrentTemperature)
        Case "Dew Point"
          m_SettingsDialog.lstTooltipContents.SetItemChecked(I, Setting_TooltipContents_DewPoint)
        Case "Feels Like Temperature"
          m_SettingsDialog.lstTooltipContents.SetItemChecked(I, Setting_TooltipContents_FeelsLikeTemperature)
        Case "Five Day Forecast"
          m_SettingsDialog.lstTooltipContents.SetItemChecked(I, Setting_TooltipContents_FiveDayForecast)
        Case "Humidity"
          m_SettingsDialog.lstTooltipContents.SetItemChecked(I, Setting_TooltipContents_Humidity)
        Case "Last Updated"
          m_SettingsDialog.lstTooltipContents.SetItemChecked(I, Setting_TooltipContents_LastUpdated)
        Case "Location"
          m_SettingsDialog.lstTooltipContents.SetItemChecked(I, Setting_TooltipContents_Location)
        Case "Moon Phase"
          m_SettingsDialog.lstTooltipContents.SetItemChecked(I, Setting_TooltipContents_MoonPhase)
        Case "Sunrise and Sunset"
          m_SettingsDialog.lstTooltipContents.SetItemChecked(I, Setting_TooltipContents_SunriseAndSunset)
        Case "UV Index"
          m_SettingsDialog.lstTooltipContents.SetItemChecked(I, Setting_TooltipContents_UVIndex)
        Case "Visibility"
          m_SettingsDialog.lstTooltipContents.SetItemChecked(I, Setting_TooltipContents_Visibility)
        Case "Wind"
          m_SettingsDialog.lstTooltipContents.SetItemChecked(I, Setting_TooltipContents_Wind)
      End Select
    Next

    For Each LI As ListViewItem In m_SettingsDialog.lvImageSet.Items
      If Setting_WeatherImageSet = LI.Tag Then
        LI.Selected = True
        Exit For
      End If
    Next

    m_SettingsDialog.IsFormLoaded = True
  End Sub

  ' InfoBar will call this when the application exits. Use this to save your internal settings.
  Public Overrides Sub SaveSettings(ByRef Doc As XmlDocument)
    Dim Node As XmlNode, ChildNode As XmlNode

    ' Weather Location
    Node = Doc.CreateNode(XmlNodeType.Element, "Node", "location", "")
    Node.InnerText = Setting_WeatherLocation
    Doc.DocumentElement.AppendChild(Node)

    ' Weather Location ID
    Node = Doc.CreateNode(XmlNodeType.Element, "Node", "locationid", "")
    Node.InnerText = Setting_WeatherLocationID
    Doc.DocumentElement.AppendChild(Node)

    ' Measurement System
    Node = Doc.CreateNode(XmlNodeType.Element, "Node", "measurementsystem", "")
    Node.InnerText = Setting_MeasurementSystem
    Doc.DocumentElement.AppendChild(Node)

    ' Update At Intervals
    Node = Doc.CreateNode(XmlNodeType.Element, "Node", "updateatintervals", "")
    Node.InnerText = Setting_UpdateAtIntervals
    Doc.DocumentElement.AppendChild(Node)

    ' Update Interval
    Node = Doc.CreateNode(XmlNodeType.Element, "Node", "updateinterval", "")
    Node.InnerText = Setting_UpdateInterval
    Doc.DocumentElement.AppendChild(Node)

    ' Show Icon
    Node = Doc.CreateNode(XmlNodeType.Element, "Node", "showicon", "")
    Node.InnerText = Setting_ShowIcon
    Doc.DocumentElement.AppendChild(Node)

    ' Show Text Temp
    Node = Doc.CreateNode(XmlNodeType.Element, "Node", "showtexttemp", "")
    Node.InnerText = Setting_ShowTextTemp
    Doc.DocumentElement.AppendChild(Node)

    ' Show Text Condition
    Node = Doc.CreateNode(XmlNodeType.Element, "Node", "showtextcondition", "")
    Node.InnerText = Setting_ShowTextCondition
    Doc.DocumentElement.AppendChild(Node)

    ' Image Set
    Node = Doc.CreateNode(XmlNodeType.Element, "Node", "imageset", "")
    Node.InnerText = Setting_WeatherImageSet
    Doc.DocumentElement.AppendChild(Node)

    ' Tooltip Contents
    Node = Doc.CreateNode(XmlNodeType.Element, "Node", "tooltipcontents", "")

    ' Air Pressure
    ChildNode = Doc.CreateNode(XmlNodeType.Element, "Node", "pressure", "")
    ChildNode.InnerText = Setting_TooltipContents_Pressure
    Node.AppendChild(ChildNode)

    ' Current Conditions
    ChildNode = Doc.CreateNode(XmlNodeType.Element, "Node", "currentconditions", "")
    ChildNode.InnerText = Setting_TooltipContents_CurrentConditions
    Node.AppendChild(ChildNode)

    ' Current Conditions Image
    ChildNode = Doc.CreateNode(XmlNodeType.Element, "Node", "currentconditionsimage", "")
    ChildNode.InnerText = Setting_TooltipContents_CurrentConditionsImage
    Node.AppendChild(ChildNode)

    ' Current Temperature
    ChildNode = Doc.CreateNode(XmlNodeType.Element, "Node", "currenttemperature", "")
    ChildNode.InnerText = Setting_TooltipContents_CurrentTemperature
    Node.AppendChild(ChildNode)

    ' Dew Point
    ChildNode = Doc.CreateNode(XmlNodeType.Element, "Node", "dewpoint", "")
    ChildNode.InnerText = Setting_TooltipContents_DewPoint
    Node.AppendChild(ChildNode)

    ' Feels Like Temperature
    ChildNode = Doc.CreateNode(XmlNodeType.Element, "Node", "feelsliketemperature", "")
    ChildNode.InnerText = Setting_TooltipContents_FeelsLikeTemperature
    Node.AppendChild(ChildNode)

    ' Five Day Forecast
    ChildNode = Doc.CreateNode(XmlNodeType.Element, "Node", "fivedayforecast", "")
    ChildNode.InnerText = Setting_TooltipContents_FiveDayForecast
    Node.AppendChild(ChildNode)

    ' Humidity
    ChildNode = Doc.CreateNode(XmlNodeType.Element, "Node", "humidity", "")
    ChildNode.InnerText = Setting_TooltipContents_Humidity
    Node.AppendChild(ChildNode)

    ' Last Updated
    ChildNode = Doc.CreateNode(XmlNodeType.Element, "Node", "lastupdated", "")
    ChildNode.InnerText = Setting_TooltipContents_LastUpdated
    Node.AppendChild(ChildNode)

    ' Location
    ChildNode = Doc.CreateNode(XmlNodeType.Element, "Node", "location", "")
    ChildNode.InnerText = Setting_TooltipContents_Location
    Node.AppendChild(ChildNode)

    ' Moon Phase
    ChildNode = Doc.CreateNode(XmlNodeType.Element, "Node", "moonphase", "")
    ChildNode.InnerText = Setting_TooltipContents_MoonPhase
    Node.AppendChild(ChildNode)

    ' Sunrise and Sunset
    ChildNode = Doc.CreateNode(XmlNodeType.Element, "Node", "sunriseandsunset", "")
    ChildNode.InnerText = Setting_TooltipContents_SunriseAndSunset
    Node.AppendChild(ChildNode)

    ' UV Index
    ChildNode = Doc.CreateNode(XmlNodeType.Element, "Node", "uvindex", "")
    ChildNode.InnerText = Setting_TooltipContents_UVIndex
    Node.AppendChild(ChildNode)

    ' Visibility
    ChildNode = Doc.CreateNode(XmlNodeType.Element, "Node", "visibility", "")
    ChildNode.InnerText = Setting_TooltipContents_Visibility
    Node.AppendChild(ChildNode)

    ' Wind
    ChildNode = Doc.CreateNode(XmlNodeType.Element, "Node", "wind", "")
    ChildNode.InnerText = Setting_TooltipContents_Wind
    Node.AppendChild(ChildNode)

    Doc.DocumentElement.AppendChild(Node)

  End Sub

#End Region

#Region "Weather Info Update Routines"

  Private Sub Weather_Update()
    Dim curTime As DateTime = DateTime.Now
    Dim timeDiff As Integer = Setting_UpdateInterval - curTime.Subtract(m_LastUpdateCheck).Minutes
    If (Setting_UpdateAtIntervals = True AndAlso timeDiff <= 0) Or m_IsManualUpdate = True Then
      m_LastUpdateCheck = curTime
      m_ErrorUpdating = False
      m_CurrentConditionImage = Nothing
      If m_IsManualUpdate = True Then
        m_UpdateInProgress = True
        DrawModule()
        Skin.UpdateWindow()
        If Tooltip.GetTooltipOwnerGUID = InfoBarModuleGUID AndAlso Tooltip.GetTooltipOwnerObjectID = "WeatherConditions" Then
          DrawTooltip("WeatherConditions")
          Tooltip.UpdateTooltip()
        End If
      End If
      If m_WorkerThread.IsBusy = True Then Exit Sub
      Try
        m_WorkerThread.RunWorkerAsync()
      Catch
      End Try
    End If
  End Sub

  Private Sub m_WorkerThread_DoWork(ByVal sender As Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles m_WorkerThread.DoWork
    Dim sURL As String = "http://xoap.weather.com/weather/local/" & Setting_WeatherLocationID & "?cc=*&dayf=5&link=xoap&prod=xoap&par=" & sWeatherPartnerID & "&key=" & sWeatherPartnerKey & "&unit=" & IIf(Setting_MeasurementSystem = 1, "s", "m")
    Dim Doc As New XmlDocument
    Dim sTemp As String
    Try
      Doc.Load(sURL)

      If Doc.DocumentElement.Name = "error" Then
        m_ErrorUpdating = True
        If IconTheme.IsDefaultTheme = True Then
          m_CurrentConditionImage = My.Resources.error_icon
        Else
          m_CurrentConditionImage = IconTheme.GetThemeIcon(InfoBarModuleGUID, "Error")
        End If
        m_ErrorMessage = "There was an error while updating the weather information." & vbCrLf & vbCrLf & Doc.DocumentElement.FirstChild.InnerText

      Else

        uTemp = GetInfo(Doc, "/weather/head/ut")
        Dim uDistance As String = " " & GetInfo(Doc, "/weather/head/ud")
        Dim uSpeed As String = " " & GetInfo(Doc, "/weather/head/us")
        Dim uPressure As String = " " & GetInfo(Doc, "/weather/head/up")
        Dim uRainfall As String = " " & GetInfo(Doc, "/weather/head/ur")

        m_Sunrise = "Sunrise: " & GetInfo(Doc, "/weather/loc/sunr")
        m_Sunset = "Sunset: " & GetInfo(Doc, "/weather/loc/suns")
        m_LastUpdated = "Last Updated: " & GetInfo(Doc, "/weather/cc/lsup")
        m_CurrentTemp = GetInfo(Doc, "/weather/cc/tmp") & "°" & uTemp
        m_FeelsLike = "Feels Like: " & GetInfo(Doc, "/weather/cc/flik") & "°" & uTemp
        m_CurrentCondition = GetInfo(Doc, "/weather/cc/t")
        Dim sIcon As String = GetInfo(Doc, "/weather/cc/icon")
        If LCase(sIcon) = "n/a" Then
          sIcon = "na"
        Else
          Select Case sIcon
            Case "27", "29", "31", "33", "45", "46", "47"
              Select Case GetInfo(Doc, "/weather/cc/moon/t")
                Case "New Moon"
                  sIcon = sIcon & "_1"
                Case "Waxing Crescent"
                  sIcon = sIcon & "_2"
                Case "First Quarter"
                  sIcon = sIcon & "_3"
                Case "Waxing Gibbous"
                  sIcon = sIcon & "_4"
                Case "Full Moon"
                  sIcon = sIcon & "_5"
                Case "Waning Gibbous"
                  sIcon = sIcon & "_6"
                Case "Last Quarter"
                  sIcon = sIcon & "_7"
                Case "Waning Crescent"
                  sIcon = sIcon & "_8"
              End Select
          End Select
        End If
        If Setting_WeatherImageSet = "InfoBarInternalWeatherImageSet" Then
          m_CurrentConditionImage = My.Resources.ResourceManager.GetObject("defaultweathericonset_" & sIcon)
        Else
          If IO.File.Exists(Setting_WeatherImageSet & "\" & sIcon & ".png") Then
            m_CurrentConditionImage = Skin.LoadImageFromFile(Setting_WeatherImageSet & "\" & sIcon & ".png")
          Else
            m_CurrentConditionImage = My.Resources.ResourceManager.GetObject("defaultweathericonset_" & sIcon)
          End If
        End If
        m_Pressure = "Pressure: " & GetInfo(Doc, "/weather/cc/bar/r") & uPressure & " and " & GetInfo(Doc, "/weather/cc/bar/d")

        sTemp = GetInfo(Doc, "/weather/cc/wind/s")
        Select Case sTemp
          Case "calm"
            m_Wind = "Wind: Calm"
          Case "breezy"
            m_Wind = "Wind: Breezy"
          Case "var"
            m_Wind = "Wind: Variable"
          Case "N/A"
            m_Wind = "Wind: None"
          Case Else
            Dim sDir As String
            Select Case GetInfo(Doc, "/weather/cc/wind/t")
              Case "N"
                sDir = "north"
              Case "S"
                sDir = "south"
              Case "E"
                sDir = "east"
              Case "W"
                sDir = "west"
              Case "NW"
                sDir = "northwest"
              Case "SW"
                sDir = "southwest"
              Case "NE"
                sDir = "northeast"
              Case "SE"
                sDir = "southeast"
              Case "NNE"
                sDir = "north northeast"
              Case "SSE"
                sDir = "south southeast"
              Case "NNW"
                sDir = "north northwest"
              Case "SSW"
                sDir = "south southwest"
              Case "ENE"
                sDir = "east northeast"
              Case "ESE"
                sDir = "east southeast"
              Case "WNW"
                sDir = "west northwest"
              Case "WSW"
                sDir = "west southwest"
              Case Else
                sDir = GetInfo(Doc, "/weather/cc/wind/t")
            End Select
            m_Wind = "Wind: From the " & sDir & " at " & GetInfo(Doc, "/weather/cc/wind/s") & uSpeed
            sTemp = GetInfo(Doc, "/weather/cc/wind/gust")
            If sTemp <> "N/A" Then m_Wind = m_Wind & " with gusts of " & sTemp & uSpeed
        End Select

        m_Humidity = "Humidity: " & GetInfo(Doc, "/weather/cc/hmid") & "%"
        m_Visibility = "Visibility: " & GetInfo(Doc, "/weather/cc/vis") & uDistance
        m_UVIndex = "UV Index: " & GetInfo(Doc, "/weather/cc/uv/i") & " (" & GetInfo(Doc, "/weather/cc/uv/t") & ")"
        m_DewPoint = "Dew Point: " & GetInfo(Doc, "/weather/cc/dewp") & "°" & uTemp

        Dim sMoonImg As String = vbNullString
        If LCase(sMoonImg) = "n/a" Then
          sMoonImg = "na"
        Else
          Select Case GetInfo(Doc, "/weather/cc/moon/t")
            Case "New"
              m_Moon = "Moon Phase: New Moon"
              sMoonImg = "31_1"
            Case "Waxing Crescent"
              m_Moon = "Moon Phase: Waxing Crescent"
              sMoonImg = "31_2"
            Case "First Quarter"
              m_Moon = "Moon Phase: First Quarter"
              sMoonImg = "31_3"
            Case "Waxing Gibbous"
              m_Moon = "Moon Phase: Waxing Gibbous"
              sMoonImg = "31_4"
            Case "Full"
              m_Moon = "Moon Phase: Full Moon"
              sMoonImg = "31_5"
            Case "Waning Gibbous"
              m_Moon = "Moon Phase: Waning Gibbous"
              sMoonImg = "31_6"
            Case "Last Quarter"
              m_Moon = "Moon Phase: Last Quarter"
              sMoonImg = "31_7"
            Case "Waning Crescent"
              m_Moon = "Moon Phase: Waning Crescent"
              sMoonImg = "31_8"
          End Select
        End If
        If Setting_WeatherImageSet = "InfoBarInternalWeatherImageSet" Then
          m_MoonImage = My.Resources.ResourceManager.GetObject("defaultweathericonset_" & sMoonImg)
        Else
          If IO.File.Exists(Setting_WeatherImageSet & "\" & sMoonImg & ".png") Then
            m_MoonImage = Skin.LoadImageFromFile(Setting_WeatherImageSet & "\" & sMoonImg & ".png")
          Else
            m_MoonImage = My.Resources.ResourceManager.GetObject("defaultweathericonset_" & sMoonImg)
          End If
        End If

        Dim dayfNode As XmlNode = Doc.DocumentElement.SelectSingleNode("/weather/dayf")
        Dim I As Integer = 0
        For Each child As XmlNode In dayfNode
          If child.Name = "day" Then
            m_DayToDayForecast(I) = New DailyWeatherForecast

            m_DayToDayForecast(I).Day = child.Attributes("t").Value
            m_DayToDayForecast(I).DayDate = child.Attributes("dt").Value

            sTemp = child.SelectSingleNode("hi").InnerText
            If sTemp <> "N/A" Then
              m_DayToDayForecast(I).HighTemp = sTemp & "°" & uTemp
            Else
              m_DayToDayForecast(I).HighTemp = sTemp
            End If
            sTemp = child.SelectSingleNode("low").InnerText
            If sTemp <> "N/A" Then
              m_DayToDayForecast(I).LowTemp = sTemp & "°" & uTemp
            Else
              m_DayToDayForecast(I).HighTemp = sTemp
            End If

            Dim partNode As XmlNode
            If I = 0 AndAlso DateTime.Now.TimeOfDay.Hours >= 14 Then
              m_DayToDayForecast(0).Day = "Tonight"
              partNode = child.SelectNodes("part")(1)
            Else
              m_DayToDayForecast(0).Day = "Today"
              partNode = child.SelectNodes("part")(0)
            End If

            If partNode IsNot Nothing Then
              m_DayToDayForecast(I).Condition = partNode.SelectSingleNode("t").InnerText

              Dim sImg As String = vbNullString
              sImg = partNode.SelectSingleNode("icon").InnerText
              If LCase(sImg) = "n/a" Then sImg = "na"
              If Setting_WeatherImageSet = "InfoBarInternalWeatherImageSet" Then
                m_DayToDayForecast(I).ConditionImage = My.Resources.ResourceManager.GetObject("defaultweathericonset_" & sImg)
              Else
                If IO.File.Exists(Setting_WeatherImageSet & "\" & sImg & ".png") Then
                  m_DayToDayForecast(I).ConditionImage = Skin.LoadImageFromFile(Setting_WeatherImageSet & "\" & sImg & ".png")
                Else
                  m_DayToDayForecast(I).ConditionImage = My.Resources.ResourceManager.GetObject("defaultweathericonset_" & sImg)
                End If
              End If
            Else
              m_DayToDayForecast(I).Condition = "Error"
              m_DayToDayForecast(I).ConditionImage = Nothing
              m_DayToDayForecast(I).Day = "Error"
              m_DayToDayForecast(I).DayDate = "Error"
              m_DayToDayForecast(I).HighTemp = "Error"
              m_DayToDayForecast(I).LowTemp = "Error"
            End If

            I += 1

          End If
        Next

        m_ErrorUpdating = False

      End If

    Catch ex As Exception

      m_ErrorUpdating = True
      If IconTheme.IsDefaultTheme = True Then
        m_CurrentConditionImage = My.Resources.error_icon
      Else
        m_CurrentConditionImage = IconTheme.GetThemeIcon(InfoBarModuleGUID, "Error")
      End If
      m_ErrorMessage = "There was an error while updating the weather information." & vbCrLf & vbCrLf & ex.ToString

    End Try

    e.Result = e.Argument
  End Sub

  Private Sub m_WorkerThread_RunWorkerCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles m_WorkerThread.RunWorkerCompleted
    If e.Cancelled = True Then Exit Sub
    m_IsManualUpdate = False
    m_UpdateInProgress = False
    If Tooltip.GetTooltipOwnerGUID = InfoBarModuleGUID AndAlso Tooltip.GetTooltipOwnerObjectID = "WeatherConditions" Then
      DrawTooltip("WeatherConditions")
      Tooltip.UpdateTooltip()
    End If
    DrawModule()
    Skin.UpdateWindow()
  End Sub

  Private Function GetInfo(ByVal Doc As XmlDocument, ByVal Path As String) As String
    Dim Node As XmlNode = Nothing
    Node = Doc.DocumentElement.SelectSingleNode(Path)
    If Node IsNot Nothing Then
      Return Node.InnerText
    Else
      Return "N/A"
    End If
  End Function

#End Region

#Region "Other Weather Functions"

  Private Sub Weather_VisitWebSite()
    Settings.OpenWebLink("http://www.weather.com/weather/my?par=" & sWeatherPartnerID & "&prod=xoap")
  End Sub

#End Region

End Class
