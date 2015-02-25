Module Storage
    Public Sub RefreshData()
        Try
            If Not IO.Directory.Exists("Data") Then IO.Directory.CreateDirectory("Data")
        Catch ex As Exception
            frmMain.Log("Error 007: " & ex.ToString)
        End Try
        Try
            If Not System.IO.Directory.Exists("Data/Images") Then
                System.IO.Directory.CreateDirectory("Data/Images")
            End If
        Catch ex As Exception
            frmMain.Log("Error 015: " & ex.ToString)
        End Try
        Try
            If Not System.IO.Directory.Exists("Data/Students") Then
                System.IO.Directory.CreateDirectory("Data/Students")
            End If
        Catch ex As Exception
            frmMain.Log("Error 016: " & ex.ToString)
        End Try
        Try
            If Not IO.File.Exists("Data/teachercode.dat") Then
                Try
                    Dim i As New IO.StreamWriter(IO.File.OpenWrite("Data/teachercode.dat"))
                    i.Write("")
                    i.Close()
                    i.Dispose()
                Catch ex As Exception
                    frmMain.Log("Error 009: " & ex.ToString)
                End Try
            End If
        Catch ex As Exception
            frmMain.Log("Error 008: " & ex.ToString)
        End Try
        Try
            _teachercode = Decrypt(System.IO.File.ReadAllText("Data/teachercode.dat"))
        Catch ex As Exception
            frmMain.Log("Error 010: " & ex.ToString)
        End Try
        Dim oldselectionname As String = frmMain.lstImages.SelectedItem
        frmMain.lstImages.Items.Clear()
        For Each s As String In GetImages()
            frmMain.lstImages.Items.Add(s)
        Next
        Try
            frmMain.lstImages.SelectedItem = oldselectionname
        Catch ex As Exception
        End Try
    End Sub

#Region "Teacher Code"
    Private _teachercode As String
    Public Function GetTeacherCode() As String
        Return _teachercode
    End Function

    Public Sub SetTeacherCode(NewCode As String)
        Try
            System.IO.File.Delete("Data/teachercode.dat")
        Catch ex As Exception
            frmMain.Log("Error 005: " & ex.ToString)
        End Try
        Try
            Dim writer As IO.StreamWriter = System.IO.File.CreateText("Data/teachercode.dat")
            writer.Write(Encrypt(NewCode))
            writer.Close()
            writer.Dispose()
        Catch ex As Exception
            frmMain.Log("Error 006: " & ex.ToString)
        End Try
        RefreshData()
    End Sub
#End Region

#Region "Images"
    Public Function ImageExists(name As String) As Boolean
        Return IO.File.Exists(ImageNameToPath(name))
    End Function

    Public Function GetImage(name As String) As Bitmap
        If IO.File.Exists(ImageNameToPath(name)) Then
            Try
                Return New Bitmap(ImageNameToPath(name))
            Catch ex As Exception
                frmMain.Log("Error 001: " & ex.ToString)
                Return Nothing
            End Try
        Else
            Return Nothing
        End If
    End Function

    Public Function GetImages() As List(Of String)
        Dim result As New List(Of String)
        Try
            For Each s As String In IO.Directory.EnumerateFiles("Data/Images/")
                If Strings.Left(IO.Path.GetFileNameWithoutExtension(s), 4) = "img-" Then
                    result.Add(ImagePathToName(s))
                End If
            Next
        Catch ex As Exception
            frmMain.Log("Error 002: " & ex.ToString)
            result.Add("<Error getting images>")
        End Try
        Return result
    End Function

    Public Sub AddImage(Image As Image, Name As String)
        Try
            Image.Save(ImageNameToPath(Name))
            frmMain.Log("Added Image """ & Name & """.")
        Catch ex As Exception
            frmMain.Log("Error 003: " & ex.ToString)
        End Try
        RefreshData()
    End Sub

    Public Sub RemoveImage(Name As String)
        If IO.File.Exists(ImageNameToPath(Name)) Then
            Try
                IO.File.Delete(ImageNameToPath(Name))
                frmMain.Log("Removed Image """ & Name & """.")
            Catch ex As Exception
                frmMain.Log("Error 004: " & ex.ToString)
            End Try
        End If
        RefreshData()
    End Sub
#End Region

#Region "Users"
    Public Function GetUserList() As List(Of String)
        Dim result As New List(Of String)
        Try
            For Each s As String In IO.Directory.EnumerateFiles("Data/Students/")
                If Strings.Left(IO.Path.GetFileNameWithoutExtension(s), 8) = "student-" Then
                    result.Add(StudentPathToName(s))
                End If
            Next
        Catch ex As Exception
            frmMain.Log("Error 011: " & ex.ToString)
            result.Add("<Error getting students>")
        End Try
        Return result
    End Function

    Public Function GetUserPassword(id As String) As String
        If IO.File.Exists(StudentNameToPath(id)) Then
            Return Strings.Trim(Decrypt(IO.File.ReadLines(StudentNameToPath(id))(0)))
        Else
            frmMain.Log("Error 012: Asked for student """ & id & """, who does not exist.")
            Return ""
        End If
    End Function

    Public Sub SetUserPassword(id As String, newcode As String)
        Dim writer As New IO.StreamWriter(StudentNameToPath(id), False)
        writer.WriteLine(Encrypt(newcode))
        writer.Close()
        writer.Dispose()
    End Sub

    Public Sub AddUser(id As String, newcode As String)
        SetUserPassword(id, newcode)
    End Sub

    Public Sub RemoveUser(id As String)
        IO.File.Delete(StudentNameToPath(id))
    End Sub

    Public Function AuthUser(id As String, code As String) As Boolean
        Return (Not (id = "" OrElse code = "")) AndAlso GetUserPassword(id) = code
    End Function
#End Region

#Region "Timing"
    Public Sub SetPracticeTime(TimeString As String)
        Dim rtime As String = GetRecordingTime()
        Dim writer As New IO.StreamWriter(TimePath(), False)
        writer.WriteLine(TimeString)
        writer.WriteLine(rtime)
        writer.Close()
        writer.Dispose()
    End Sub
    Public Function GetPracticeTime() As String
        Return System.IO.File.ReadLines(TimePath())(0)
    End Function
    Public Sub SetRecordingTime(TimeString As String)
        Dim ptime As String = GetPracticeTime()
        Dim writer As New IO.StreamWriter(TimePath(), False)
        writer.WriteLine(ptime)
        writer.WriteLine(TimeString)
        writer.Close()
        writer.Dispose()
    End Sub
    Public Function GetRecordingTime() As String
        Return System.IO.File.ReadLines(TimePath())(1)
    End Function
#End Region

#Region "Encryption"
    Public Function Encrypt(Input As String) As String
        Return Uri.EscapeDataString(Input)
    End Function
    Public Function Decrypt(Input As String) As String
        Return Uri.UnescapeDataString(Input)
    End Function
#End Region

#Region "Path Creation Helpers"
    Private Function ImageNameToPath(Name As String) As String
        Return "Data/Images/img-" & Name & ".png"
    End Function
    Private Function ImagePathToName(Path As String) As String
        Return Strings.Right(IO.Path.GetFileNameWithoutExtension(Path), Len(IO.Path.GetFileNameWithoutExtension(Path)) - 4)
    End Function
    Private Function StudentNameToPath(id As String) As String
        Return "Data/Students/student-" & id & ".dat"
    End Function
    Private Function StudentPathToName(Path As String) As String
        Return Strings.Right(IO.Path.GetFileNameWithoutExtension(Path), Len(IO.Path.GetFileNameWithoutExtension(Path)) - 8)
    End Function
    Private Function TimePath() As String
        Return "Data/Time.dat"
    End Function
#End Region

End Module
