# About

Contains base methods for 

- Iterating folder
- Iterating files
- Reading files

## Intent

To provide basic code samples rather than complex code samples so a developer can learn the mechanics of asynchronous, synchronous methods using events to perform the above operations.

## Globbing

Globbing is another way to iterate folders and files with the ability to fine tune patterns, see sample patterns below. Not everyone will benefit from globbing, use globbing only when conventional matching folders and files does not fullfill specific needs.

More likely globbing is unknown to most developers and with that take time to work with pattern matching no different than working with regular expressions (globbing is indeed easier).

Study provided code samples than explore what can be done with `include` and `exclude` patterns..

**Example**

- Find all `.cs` files in a folder and sub-folders using `{ "**/*.cs" }`
- Exclude from the above pattern `{ "**/*Assembly*.cs", "**/*Designer*.cs" }`

```csharp
/// <summary>
/// Provides what to find via include array and what to exclude
/// using the exclude array
/// </summary>
private async void ExecuteButton_Click(object sender, EventArgs e)
{
    ResultListBox.Items.Clear();
    string path = DirectoryHelper.SolutionFolder();

    string[] include = { "**/*.cs" };
    string[] exclude = { "**/*Assembly*.cs", "**/*Designer*.cs" };

    await GlobbingOperations.Asynchronous(path, include, exclude);
}
```

| Value        | Description     |
|:------------- |:-------------|
| *.txt|All files with .txt file extension. |
| *.\* | All files with an extension|
| * | All files in top-level directory.|
| .*	| File names beginning with '.'.|
| *word\*| All files with 'word' in the filename.|
| readme.*| All files named 'readme' with any file extension.|
| styles/*.css| All files with extension '.css' in the directory 'styles/'.|
| scripts/*/\*| All files in 'scripts/' or one level of subdirectory under 'scripts/'.|
| images*/*| All files in a folder with name that is or begins with 'images'.|
| **/\*| All files in any subdirectory.|
| dir/**/\*| All files in any subdirectory under 'dir/'.|
| ../shared/*| All files in a diretory named "shared" at the sibling level to the base directory|

## Microsoft docs Globbing

[File globbing in .NET](https://docs.microsoft.com/en-us/dotnet/core/extensions/file-globbing)


