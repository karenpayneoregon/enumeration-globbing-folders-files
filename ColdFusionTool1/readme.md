# TODO

- Iterate files
- Write report

```csharp
using System.IO;

public class Example
{
    public static void Main(string[] args)
    {
        string filePath = "myFile.txt";
        string textToAppend = "This is the new line to append.\n";

        // Append the text to the file
        File.AppendAllText(filePath, textToAppend);

        // Alternatively, you can use File.AppendText to get a StreamWriter
        // and append multiple lines:
        // using (StreamWriter sw = File.AppendText(filePath))
        // {
        //     sw.WriteLine("Line 1");
        //     sw.WriteLine("Line 2");
        // }
    }
}
```