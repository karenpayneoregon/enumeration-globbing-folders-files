# About

Uses GitHub repository code from  [https://github.com/kthompson/glob](https://github.com/kthompson/glob)

```csharp
static void Main(string[] args)
{
    var folder = @"C:\OED\Dotnetland\VS2019\GlobbingSolution\";

    string[] folderPatterns = { "**/bin", "**/obj" };
    string[] filePatterns = { "**/*cs"};

    var results = new List<string>();

    foreach (var pattern in folderPatterns)
    {
        results.AddRange(Glob.Directories(folder, pattern).ToArray());
    }

    foreach (var pattern in filePatterns)
    {
        results.AddRange(Glob.Files(folder, pattern).ToArray());
    }


    Console.ReadLine();
}
```