Imports System.Threading
Imports System.Net

Module Server
    Public LogQueue As New List(Of String)
    Private ServerThread As Thread
    Private PleaseExit As Boolean = False
    Private Exited As Boolean = False
    Private Listener As New HttpListener
    Private _serverrunning As Boolean = False
    Public ReadOnly Property ServerRunning As Boolean
        Get
            Return _serverrunning
        End Get
    End Property
    Public Sub StartServer()

        ServerThread = New Thread(AddressOf RunServerThreadLoop)
        ServerThread.Start()
    End Sub
    Public Sub StopServer()
        PleaseExit = True
        Listener.Stop()
        Do While Exited = False
            Thread.Sleep(0)
        Loop
    End Sub
    Private Sub RunServerThreadLoop()
        _serverrunning = True
        PleaseExit = False
        Exited = False

        Listener.Prefixes.Add("http://*:5041/")
        Try
            Listener.Start()
        Catch ex As Exception
            MsgBox("Cannot listen for any hostname. Please run as administrator. In the meantime, I will listen for localhost.")
            Listener = New HttpListener
            Listener.Prefixes.Clear()
            Listener.Prefixes.Add("http://localhost:5041/")
        End Try
        'Listener.Prefixes.Add("http://192.168.1.102:5041/")
        Listener.Start()
        Dim c As HttpListenerContext
        Do While PleaseExit = False
            Try
                c = Listener.GetContext()
                Respond(c)
            Catch ex As System.Net.HttpListenerException
                'Listener.Stop was (probably) called, so no worries here.
            Catch ex As Exception
                frmMain.Log("Error: " & ex.ToString)
            End Try
        Loop
        Listener.Stop()

        Exited = True
        _serverrunning = False
    End Sub

    Private Sub Respond(c As HttpListenerContext)
        Dim inputbuffer() As Byte = {}
        Dim inputstring As String
        c.Request.InputStream.Read(inputbuffer, 0, c.Request.ContentLength64)
        inputstring = System.Text.UTF8Encoding.UTF8.GetString(inputbuffer)
        Dim responseString As String = ""
        If Strings.Left(c.Request.RawUrl, 14) = "/student/auth-" Then '----------------------------------Auth Password
            Dim buffer() As String = Strings.Split(Decrypt(c.Request.RawUrl), "/")
            Dim buffer2() As String = Strings.Split(buffer(buffer.Length - 1), "-") 'should result in 'auth', 'username', 'code'
            If AuthUser(buffer2(1), Encrypt(buffer2(2))) = True Then
                responseString = "true"
            Else
                responseString = "false"
            End If
        ElseIf Strings.Left(c.Request.RawUrl, 13) = "/student/get-" Then '-----------------------------------Get Password
            Dim name As String = Strings.Right(c.Request.RawUrl, Len(c.Request.RawUrl) - 13)
            responseString = GetUserPassword(name)
        ElseIf Strings.Left(c.Request.RawUrl, 13) = "/student/set-" Then '-----------------------------------Set Password
            Dim buffer() As String = Strings.Split(Decrypt(c.Request.RawUrl), "/")
            Dim buffer2() As String = Strings.Split(buffer(buffer.Length - 1), "-") 'should result in 'auth', 'username', 'code'
            SetUserPassword(buffer2(1), buffer2(2))
        ElseIf Strings.Left(c.Request.RawUrl, 16) = "/student/remove-" Then '--------------------------------Remove Student
            Dim name As String = Strings.Right(c.Request.RawUrl, Len(c.Request.RawUrl) - 16)
            RemoveUser(Decrypt(name))
        ElseIf Strings.Left(c.Request.RawUrl, 13) = "/student/add-" Then '-----------------------------------Add Student
            Dim buffer() As String = Strings.Split(Decrypt(c.Request.RawUrl), "/")
            Dim buffer2() As String = Strings.Split(buffer(buffer.Length - 1), "-") 'should result in 'auth', 'username', 'code'
            AddUser(buffer2(1), buffer2(2))
        ElseIf c.Request.RawUrl = "/student/list" Then
            For Each s As String In GetUserList()
                responseString &= s & vbNewLine
            Next
        ElseIf Strings.Left(c.Request.RawUrl, 10) = "/ptime/get" Then
            responseString = GetPracticeTime()
        ElseIf Strings.Left(c.Request.RawUrl, 10) = "/rtime/get" Then
            responseString = GetRecordingTime()
        ElseIf Strings.Left(c.Request.RawUrl, 10) = "/ptime/set" Then
            SetPracticeTime(Decrypt(Strings.Right(c.Request.RawUrl, Len(c.Request.RawUrl) - 11)))
        ElseIf Strings.Left(c.Request.RawUrl, 10) = "/rtime/set" Then
            SetRecordingTime(Decrypt(Strings.Right(c.Request.RawUrl, Len(c.Request.RawUrl) - 11)))
        ElseIf Strings.Left(c.Request.RawUrl, 12) = "/img/remove-" Then
            Dim name As String = Strings.Right(c.Request.RawUrl, Len(c.Request.RawUrl) - 12)
            RemoveImage(name)
        ElseIf Strings.Left(c.Request.RawUrl, 9) = "/img/get-" Then '----------------------------------------Get Image
            Dim imagename As String = Decrypt(Strings.Right(c.Request.RawUrl, Len(c.Request.RawUrl) - 9))
            Dim img As Image = GetImage(imagename)
            If IsNothing(img) Then
                c.Response.ContentLength64 = 0
                c.Response.OutputStream.Close()
                frmMain.Log(c.Request.RemoteEndPoint.Address.ToString() & " asked for nonexistant image """ & imagename & """.")
                Exit Sub
            End If
            Dim temp As New IO.MemoryStream()
            img.Save(temp, Imaging.ImageFormat.Png)
            img.Dispose()
            temp.WriteTo(c.Response.OutputStream)
            c.Response.ContentLength64 = temp.Length
            temp.Dispose()
            c.Response.OutputStream.Close()
            frmMain.Log(c.Request.RemoteEndPoint.Address.ToString() & " downloaded image """ & imagename & """.")
            Exit Sub
        ElseIf Strings.Left(c.Request.RawUrl, 9) = "/img/list" Then
            For Each s As String In GetImages()
                responseString &= s & vbNewLine
            Next
        End If

        Dim response() As Byte = System.Text.Encoding.UTF8.GetBytes(responseString)
        c.Response.ContentLength64 = response.Length
        c.Response.OutputStream.Write(response, 0, response.Length)
        c.Response.OutputStream.Close()
    End Sub
End Module
