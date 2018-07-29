Namespace My

  ' The following events are availble for MyApplication:
  '
  ' Startup: Raised when the application starts, before the startup form is created.
  ' Shutdown: Raised after all application forms are closed.  This event is not raised if the application terminates abnormally.
  ' UnhandledException: Raised if the application encounters an unhandled exception.
  ' StartupNextInstance: Raised when launching a single-instance application and the application is already active.
  ' NetworkAvailabilityChanged: Raised when the network connection is connected or disconnected.

  Partial Friend Class MyApplication

    Private Sub MyApplication_Startup(ByVal sender As Object, ByVal e As Microsoft.VisualBasic.ApplicationServices.StartupEventArgs) Handles Me.Startup
      If e.CommandLine.Count > 0 Then ProcessCommandLine(e.CommandLine)
    End Sub

    Private Sub MyApplication_StartupNextInstance(ByVal sender As Object, ByVal e As Microsoft.VisualBasic.ApplicationServices.StartupNextInstanceEventArgs) Handles Me.StartupNextInstance
      If e.CommandLine.Count > 0 Then
        ProcessCommandLine(e.CommandLine)
      Else

        With fMain.tiMain
          .Tag = "InfoBar is already running."
          .BalloonTipIcon = ToolTipIcon.Info
          .BalloonTipTitle = "InfoBar is already running."
          If .Visible = True Then
            .BalloonTipText = "InfoBar is already running. Double click this icon to show InfoBar."
          Else
            .BalloonTipText = "InfoBar is already running. It should be displayed at the top or bottom of your screen."
            .Visible = True
          End If
          .ShowBalloonTip(10000)
        End With
      End If
    End Sub

    Private Sub ProcessCommandLine(ByVal sCommandLineArgs As System.Collections.ObjectModel.ReadOnlyCollection(Of String))
      Dim I As Integer
      For I = 0 To sCommandLineArgs.Count - 1

        If LCase(sCommandLineArgs(I)) = "/shutdown" Then
          fMain.Close()
          Exit Sub
        End If

        If LCase(sCommandLineArgs(I)) = "/installmodule" Then
          ProcessModulePackage(sCommandLineArgs(I + 1))
        End If

        If LCase(sCommandLineArgs(I)) = "/installskin" Then
          ProcessSkinPackage(sCommandLineArgs(I + 1))
          Exit Sub
        End If

        If LCase(sCommandLineArgs(I)) = "/installicons" Then
          ProcessIconPackage(sCommandLineArgs(I + 1))
        End If

      Next
    End Sub

  End Class

End Namespace