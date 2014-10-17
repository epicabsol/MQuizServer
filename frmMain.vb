Public Class frmMain
    Private Sub frmMain_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        If ServerRunning Then StopServer()
    End Sub

    Private Sub frmMain_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        RefreshData()
        If Not txtLog.IsHandleCreated Then
            CreateHandle()
        End If
        If My.Application.CommandLineArgs.Contains("-s") Then
            cmdStartStop.Text = "Stop"
            StartServer()
            PictureBox1.Image = My.Resources.online_button_x128
        End If
    End Sub

    Private Sub tmrRefreshData_Tick(sender As Object, e As EventArgs) Handles tmrRefreshData.Tick
        If txtLog.InvokeRequired Then
            Me.Invoke(New Action(Of Object, EventArgs)(AddressOf tmrRefreshData_Tick), sender, e)
        Else
            For i As Long = LogQueue.Count - 1 To 0 Step -1
                txtLog.AppendText(vbNewLine & LogQueue(i))
                LogQueue.RemoveAt(i)
            Next
            RefreshData()
        End If
    End Sub

    Private Sub cmdSetPassword_Click(sender As Object, e As EventArgs) Handles cmdSetPassword.Click
        Dim s As String = InputBox("New Password:", "Password Change", "")
        Select Case MsgBox("Change password to """ & s & """?", MsgBoxStyle.YesNo)
            Case MsgBoxResult.Yes
                SetTeacherCode(s)
            Case MsgBoxResult.No
                Exit Sub
        End Select
    End Sub

    Public Sub Log(Message As String)
        If txtLog.InvokeRequired Then
            Invoke(New Action(Of String)(AddressOf Log), Message)
        Else
            LogQueue.Add(Message & vbNewLine)
            Dim s As New IO.StreamWriter(IO.File.OpenWrite("Data\log.txt"))
            s.WriteLine(Now.ToString() & " - " & Message)
            s.Close()
            s.Dispose()
        End If
    End Sub

    Private Sub cmdStartStop_Click(sender As Object, e As EventArgs) Handles cmdStartStop.Click
        If ServerRunning Then
            cmdStartStop.Text = "Start"
            StopServer()
            PictureBox1.Image = My.Resources.offline_button_x128
        Else
            cmdStartStop.Text = "Stop"
            StartServer()
            PictureBox1.Image = My.Resources.online_button_x128
        End If
    End Sub

    Private Sub lstImages_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lstImages.SelectedIndexChanged

    End Sub

    Private Sub cmdAdd_Click(sender As Object, e As EventArgs) Handles cmdAdd.Click
        Dim browser As New OpenFileDialog
        browser.Filter = "Images (*.png, *.bmp, *.jpg, *.gif)|*.png;*.bmp;*.jpg;*.gif|All Files (*.*)|*.*"
        browser.ShowDialog()
        browser.Dispose()
        If browser.FileName = "" Then Exit Sub
        Dim name As String = InputBox("What do you want to call this image?", "Add Image", "")
        If name = "" Then Exit Sub
        Dim newpic As New Bitmap(browser.FileName)
        AddImage(newpic, name)
        newpic.Dispose()
    End Sub

    Private Sub cmdRemoveImage_Click(sender As Object, e As EventArgs) Handles cmdRemoveImage.Click
        If lstImages.SelectedIndex = -1 Then Exit Sub
        RemoveImage(lstImages.SelectedItem)
    End Sub
End Class
