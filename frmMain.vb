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
End Class
