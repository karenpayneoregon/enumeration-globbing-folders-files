Imports System.IO
Public Class Form1
    Public Async Function FileCount(sender As String, allowedExtensions() As String) As Task(Of Integer)
        Return Await Task.Run(
            Function()
                Return Task.FromResult(
                    Directory.EnumerateFiles(
                        sender, 
                        "*.*", 
                        SearchOption.AllDirectories).Count(
                            Function (file)
                                Return allowedExtensions.Contains(Path.GetExtension(file))
                            End Function))
            End Function)
    End Function

    Private Async Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim watch As Stopwatch = Stopwatch.StartNew()
        Dim count = Await FileCount("C:\OED\Dotnetland\VS2019", { ".cs" })
        watch.Stop()
        Dim ts As TimeSpan = TimeSpan.FromMilliseconds(watch.Elapsed.TotalMilliseconds)

        MessageBox.Show($"File count: {count,-10}{ts.TotalSeconds}")

    End Sub
End Class
