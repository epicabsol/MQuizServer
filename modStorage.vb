Module modStorage
    Public Sub RefreshData()
        Try
            If Not IO.Directory.Exists("Data") Then IO.Directory.CreateDirectory("Data")
        Catch ex As Exception
            frmMain.Log("Error 007: " & ex.ToString)
        End Try
        Try
            If Not IO.File.Exists("Data\teachercode.dat") Then
                Try
                    Dim i As New IO.StreamWriter(IO.File.OpenWrite("Data\teachercode.dat"))
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
            _teachercode = Decrypt(System.IO.File.ReadAllText("Data\teachercode.dat"))
        Catch ex As Exception
            frmMain.Log("Error 010: " & ex.ToString)
        End Try
        frmMain.lstImages.Items.Clear()
        For Each s As String In GetImages()
            frmMain.lstImages.Items.Add(s)
        Next
    End Sub

#Region "Teacher Code"
    Private _teachercode As String
    Public Function GetTeacherCode() As String
        Return _teachercode
    End Function

    Public Sub SetTeacherCode(NewCode As String)
        Try
            System.IO.File.Delete("Data\teachercode.dat")
        Catch ex As Exception
            frmMain.Log("Error 005: " & ex.ToString)
        End Try
        Try
            Dim writer As IO.StreamWriter = System.IO.File.CreateText("Data\teachercode.dat")
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
    Public Function GetImage(name As String) As Bitmap
        If IO.File.Exists(NameToPath(name)) Then
            Try
                Return New Bitmap(NameToPath(name))
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
            For Each s As String In IO.Directory.EnumerateFiles("Data\")
                If Strings.Left(IO.Path.GetFileNameWithoutExtension(s), 4) = "img-" Then
                    result.Add(PathToName(s))
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
            Image.Save(NameToPath(Name))
            frmMain.Log("Added Image """ & Name & """.")
        Catch ex As Exception
            frmMain.Log("Error 003: " & ex.ToString)
        End Try
        RefreshData()
    End Sub

    Public Sub RemoveImage(Name As String)
        If IO.File.Exists(NameToPath(Name)) Then
            Try
                IO.File.Delete(NameToPath(Name))
                frmMain.Log("Removed Image """ & Name & """.")
            Catch ex As Exception
                frmMain.Log("Error 004: " & ex.ToString)
            End Try
        End If
        RefreshData()
    End Sub
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
    Private Function NameToPath(Name As String) As String
        Return "Data\img-" & Name & ".png"
    End Function
    Private Function PathToName(Path As String) As String
        Return Strings.Right(IO.Path.GetFileNameWithoutExtension(Path), Len(IO.Path.GetFileNameWithoutExtension(Path)) - 4)
    End Function
#End Region

End Module
