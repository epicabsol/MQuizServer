Imports System.Threading
Imports System.Net

Module Server
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
    Public Sub StopServerAsync()
        PleaseExit = True
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

        Listener.Prefixes.Add("http://localhost:5041/")
        Listener.Start()
        Dim c As HttpListenerContext
        Do While PleaseExit = False
            Try
                c = Listener.GetContext()
                Respond(c)
            Catch ex As Exception
                'Listener.Stop was (probably) called, so no worries here.
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
        If Strings.Left(c.Request.RawUrl, 6) = "/auth-" Then
            Dim code As String = Decrypt(Strings.Right(c.Request.RawUrl, Len(c.Request.RawUrl) - 6))
            If code = GetTeacherCode() Then
                responseString = "good"
            Else
                responseString = "bad"
            End If
        ElseIf Strings.Left(c.Request.RawUrl, 5) = "/img/" Then
            Dim imagename As String = Decrypt(Strings.Right(c.Request.RawUrl, Len(c.Request.RawUrl) - 5))
            Dim img As Image = GetImage(imagename)
            If IsNothing(img) Then
                c.Response.ContentLength64 = 0
                c.Response.OutputStream.Close()
                Exit Sub
            End If
            img.Save(c.Response.OutputStream, Imaging.ImageFormat.Png)
            c.Response.ContentLength64 = c.Response.OutputStream.Position
            c.Response.OutputStream.Close()
            Exit Sub
        End If

        Dim response() As Byte = System.Text.Encoding.UTF8.GetBytes(responseString)
        c.Response.ContentLength64 = response.Length
        c.Response.OutputStream.Write(response, 0, response.Length)
        c.Response.OutputStream.Close()
    End Sub
End Module
