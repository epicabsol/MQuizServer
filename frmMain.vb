Public Class frmMain

    Private Sub frmMain_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        RefreshData()
    End Sub

    Private Sub tmrRefreshData_Tick(sender As Object, e As EventArgs) Handles tmrRefreshData.Tick
        Invoke(Sub() RefreshData())
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
        txtLog.Text &= vbNewLine & Message
    End Sub

    Private Sub cmdStartStop_Click(sender As Object, e As EventArgs) Handles cmdStartStop.Click

    End Sub

    Private Sub lstImages_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lstImages.SelectedIndexChanged

    End Sub

    Private Sub cmdAdd_Click(sender As Object, e As EventArgs) Handles cmdAdd.Click
        Dim browser As New OpenFileDialog
        browser.Filter = "Images (*.png, *.bmp, *.jpg, *.gif)|*.png;*.bmp;*.jpg;*.gif|All Files (*.*)|*.*"
        browser.ShowDialog()
        browser.Dispose()
        If browser.FileName = "" Then Exit Sub
        Dim name As String = InputBox("What do you wan to call this image?", "Add Image", "")
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
