# About

An example to get directory names using globbing via NuGet package [Glob](https://www.nuget.org/packages/Glob/1.1.9?_src=template).

In this example specific folder types are targeted and listed in a text file.

Other uses

- Traversing found folders for specific files.
- Removing found folders


Get current Visual Studio solution folder.

```csharp
var root = DirectoryOperations.GetSolutionInfo().FullName;
```

Get all folders named Models and Classes

```csharp
string[] modelsDirectories = Glob.Directories(root, "**/Models").ToArray();
string[] classesDirectories = Glob.Directories(root, "**/Classes").ToArray();
```

Get folders named LanguageExtensions and/or Extenssions case insensitive.

```csharp
string[] extensionsDirectories = Glob.Directories(root, "**/*extensions", 
    GlobOptions.CaseInsensitive).ToArray();
```

Combine above arrays and sort


```csharp
List<string> directories = 
    [
        .. modelsDirectories, 
        .. extensionsDirectories, 
        .. classesDirectories
    ];

List<string> ordered = directories.OrderBy(x => x).ToList();
```

Save results to a text file

```csharp
File.WriteAllText("directories.txt", string.Join(Environment.NewLine, ordered));
```