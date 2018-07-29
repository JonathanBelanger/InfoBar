Friend Class frmLocationSearch

    Friend Class WeatherLocation
        Public ID As String
        Public Name As String
    End Class

    Friend WeatherLocations As New Collection

    Private Sub btnOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOK.Click
        Me.DialogResult = Windows.Forms.DialogResult.OK
    End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Me.DialogResult = Windows.Forms.DialogResult.Cancel
    End Sub

    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        btnSearch.Enabled = False
        pbSearching.Visible = True
        lblStep2.Enabled = False
        lstResults.Enabled = False
        btnOK.Enabled = False

        WeatherLocations.Clear()
        lstResults.Items.Clear()
        lstResults.Refresh()
        Application.DoEvents()

        bwSearch.RunWorkerAsync()
    End Sub

    Private Sub txtSearch_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtSearch.TextChanged
        If Len(txtSearch.Text) > 0 Then
            btnSearch.Enabled = True
        Else
            btnSearch.Enabled = False
        End If
    End Sub

    Private Sub lstResults_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles lstResults.DoubleClick
        If lstResults.SelectedItem IsNot Nothing Then
            Me.DialogResult = Windows.Forms.DialogResult.OK
        End If
    End Sub

    Private Sub lstResults_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lstResults.SelectedIndexChanged
        If lstResults.SelectedItem Is Nothing Then
            btnOK.Enabled = False
        Else
            btnOK.Enabled = True
        End If
    End Sub

    Private Sub bwSearch_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles bwSearch.DoWork
        Dim Doc As New XmlDocument
        Try
            Doc.Load("http://xoap.weather.com/search/search?partnerid=" & sWeatherPartnerID & "&partnerkey=" & sWeatherPartnerKey & "&where=" & txtSearch.Text)
            Dim Node As XmlNode = Doc.DocumentElement.SelectSingleNode("/search")
            For Each ChildNode As XmlNode In Node.ChildNodes
                If ChildNode.Name = "loc" Then
                    Dim Loc As New WeatherLocation
                    Loc.ID = ChildNode.Attributes("id").Value
                    Loc.Name = ChildNode.InnerText
                    WeatherLocations.Add(Loc)
                End If
            Next
            e.Result = vbNullString
        Catch ex As Exception
            e.Result = ex.Message
        End Try
    End Sub

    Private Sub bwSearch_RunWorkerCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles bwSearch.RunWorkerCompleted
        pbSearching.Visible = False
        If e.Result = vbNullString Then
            If WeatherLocations.Count = 0 Then
                MsgBox("No weather locations were found.", MsgBoxStyle.Information Or MsgBoxStyle.OkOnly, "Weather Conditions")
            Else
                For Each Loc As WeatherLocation In WeatherLocations
                    lstResults.Items.Add(Loc.Name)
                Next
                lblStep2.Enabled = True
                lstResults.Enabled = True
            End If
        Else
            MsgBox("There was an error while trying to search for locations." & vbCrLf & vbCrLf & e.Result, MsgBoxStyle.Information Or MsgBoxStyle.OkOnly, "Weather Conditions")
        End If
        btnSearch.Enabled = True
    End Sub

End Class