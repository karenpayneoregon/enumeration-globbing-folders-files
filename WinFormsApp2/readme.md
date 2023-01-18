# About

Get count of files in a folder and all sub-folders.

```vbnet
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
```