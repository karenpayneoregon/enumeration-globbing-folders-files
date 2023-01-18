
# Learn how to find files in folders and sub-folders

![image](assets/FolderToolBox.png)

At any level of programming in C# there will be a need during the career of a developer when there is a need to iterate folders searching for files.

Most examples that surface will show how to get files in a folder for one file extension and for simplicity are generally synchronous. One file extension is generally what a developer needs while enumerating a folder with a deep level of folders local or remote can cause the application to become unresponsive.

> **Note**
> Although code is presented in Windows Forms the base code will work in other project types

## Requires

- Microsoft [Visual Studio](https://visualstudio.microsoft.com/) 2022 or higher - 17.4x
- Microsoft [.NET Core](https://dotnet.microsoft.com/en-us/download/dotnet) 5 or higher
- C# 9 or higher

## Basics

**Before writing code first consider**

- Will an operation need to keep a user interface responsive?
  - Use synchronous code when keeping the user interface responsive is not an issue which can be when working with a small folder structure or running processes unattended.
  - Use asynchronous code when there is a need to keep the user interface responsive
  - Read Microsoft docs [Asynchronous file access](https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/concepts/async/using-async-for-file-access)
- Always consider that one or more folders may not be assessible because of computer or organization policies.
  - When using [Directory.EnumerateFiles](https://docs.microsoft.com/en-us/dotnet/api/system.io.directory.enumeratefiles?view=net-6.0), there is an overload with [EnumerationOptions.IgnoreInaccessible Property](https://docs.microsoft.com/en-us/dotnet/api/system.io.enumerationoptions.ignoreinaccessible?view=net-6.0#system-io-enumerationoptions-ignoreinaccessible) which gets or sets a value that indicates whether to skip files or directories when access is denied (for example, [UnauthorizedAccessException](https://docs.microsoft.com/en-us/dotnet/api/system.unauthorizedaccessexception?view=net-6.0) or [SecurityException](https://docs.microsoft.com/en-us/dotnet/api/system.security.securityexception?view=net-6.0)). The default is true.
- Make sure to test for this and when appropriate wrap code with exception handling that writes to some type of log.
- Learn the basics for using events which are widely used in code samples for the repository.
- Avoid writing code directly in the user interface e.g. WPF window, Windows Forms form, ASP.NET Core, razor files. Instead, as done in this repository create classes dedicated to folder and file operations.
- Consider using `events`, `actions` and `func` when iterating folders and files, there are several code samples include for these.


## Important on folders used in code sample

Originally folders used where to be selected by those reading/trying out the code, since than it's been changed to work against the current solution folder.

It's encouraged to first work with the solution folder followed by selecting folders on the local machine.

Note that the main code samples have been kept simple for learning while there are many complex code samples to learn from also.


# Traverse a folder structure

Let's look at a simple method to traverse a high level folders and all sub-folders

```csharp
/// <summary>
/// Find files in folder
/// </summary>
/// <param name="path">Folder to iterate</param>
/// <param name="allowedExtensions">extensions to find</param>
/// <returns></returns>
public static async Task<List<string>> EnumerateFoldersAsync(string path, string[] allowedExtensions) 
    => await Task.Run(() => Task.FromResult(Directory
        .EnumerateFiles(path)
        .Where(file => allowedExtensions.Any(file.ToLower().EndsWith))
        .ToList()));

}
```

**Usage**

```csharp
private async void EnumerateFilesButton_Click(object sender, EventArgs e)
{
    const string folder = @"C:\Users\xxxxxx\Pictures";
    string[] allowedExtensions = { ".png", ".ico"};
    List<string> list = await DirectoryOperations.EnumerateFilesAsync(folder, allowedExtensions);

    InformationalMessage(this, $"Found {list.Count}");
}
```

EnumerateFilesAsync will iterate the Pictures folder and all sub-folders for any `.png` and `.ico` files returning their nammes.

**Caveats**

If the user running the application does not have permissions to the folder and sub-folders wrap the code with a try-catch

There are many ways to deal with an exception, here is a simple version which returns a named value tuple and in the catch raises an event that the caller can listen for which returns the exception.

```csharp
public class DirectoryOperations
{
    public delegate void OnException(Exception exception);
    public static event OnException OnExceptionEvent;

    public static async Task<(bool success, List<string> list)> EnumerateFoldersAsync1(string path, string[] allowedExtensions)
    {
        try
        {
            var data = await Task.Run(() => Task.FromResult(Directory
                            .EnumerateFiles(path)
                            .Where(file => allowedExtensions.Any(file.ToLower().EndsWith))
                            .ToList()));

            return (true, data);
        }
        catch (Exception exception)
        {
            OnExceptionEvent?.Invoke(exception);
            return (false, new List<string>());
        }
    }
}
```

We could figure out the exception type and raise an event spectic to the exception type e.g.

```csharp
catch (Exception exception)
{
    switch (exception)
    {
        case OperationCanceledException _:
            Cancelled = true;
            break;
        case UnauthorizedAccessException _:
            UnauthorizedAccessEvent?.Invoke($"Access denied '{exception.Message}'");
            break;
        default:
            OnExceptionEvent?.Invoke(exception);
            break;
    }
}
```

Lesson here is not simple go a synchronous but to assume an exception may be raised from lack of permissions to scan folders.

Examples which follow will exclude exception handling to focus on finding files using alternate methods from [Globbing](https://docs.microsoft.com/en-us/dotnet/core/extensions/file-globbing) to PowerShell.

The multiple file extensions is not most developers would consider but once seen it's simple.

The reason newcomers and even intermediate developer fumble with this is not knowing about extension method [Any](https://docs.microsoft.com/en-us/dotnet/api/system.linq.enumerable.any?view=net-6.0) (and [All](https://docs.microsoft.com/en-us/dotnet/api/system.linq.enumerable.all?view=net-6.0)).

```csharp
.Where(file => allowedExtensions.Any(file.ToLower().EndsWith))
```

## With Globbing

This is one of several code samples for iterating a folder structure with enhanced patterns that allow more than simply filtering on file extensions.

### Sample patterns

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

```csharp
/// <summary>
/// Folder to search/filter 
/// </summary>
/// <param name="folderName"></param>
/// <param name="includePatterns">
/// pattern match to filter e.g. **/s*.cs for all .cs files beginning with s in all folders under folderName
/// </param>
public static void GenericSearch(string folderName, string[] includePatterns)
{

    if (!Directory.Exists(folderName))
    {
        Traverse?.Invoke(FolderNotExistsText);
        return;
    }

    Matcher matcher = new ();
    matcher.AddIncludePatterns(includePatterns);

    PatternMatchingResult matchingResult = matcher.Execute(new DirectoryInfoWrapper(new DirectoryInfo(folderName)));

    if (matchingResult.HasMatches)
    {

        foreach (var file in matchingResult.Files)
        {
            Traverse?.Invoke(Path.Combine(folderName, file.Path).Replace("/","\\"));
        }

        Done?.Invoke($"Match count {matchingResult.Files.Count()}");

    }
    else
    {
        Done?.Invoke("No matches");
    }

}
```


# Traverse folder structure with deletion intention

Suppose the task is to remove a folder structure where there may be issues with insufficient permissions or a folder is marked as a system folder the proper way is to assert along with wrapping this code in a try-catch.

The try-catch should check for [UnauthorizedAccessException](https://docs.microsoft.com/en-us/dotnet/api/system.unauthorizedaccessexception?view=net-6.0) and for [OperationCanceledException](https://docs.microsoft.com/en-us/dotnet/api/system.operationcanceledexception?view=net-6.0).

`OperationCanceledException` is when code is written to allow a user to cancel this operation or any long running operation.

The code presented is to be considered base code which a developer might use `as is` or selectively delete sub-folders use business logic.

Note the events which allow the caller to get notications.

```csharp
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace DirectoryHelpersLibrary.Classes
{
    public class DirectoryOperations1
    {
        public delegate void OnDelete(string status);
        /// <summary>
        /// Callback for subscribers to see what is being worked on
        /// </summary>
        public static event OnDelete OnDeleteEvent;

        public delegate void OnException(Exception exception);
        /// <summary>
        /// Callback for subscribers to know about a problem
        /// </summary>
        public static event OnException OnExceptionEvent;

        public delegate void OnUnauthorizedAccessException(string message);
        /// <summary>
        /// Raised when attempting to access a folder the user does not have permissions too
        /// </summary>
        public static event OnUnauthorizedAccessException UnauthorizedAccessEvent;

        public delegate void OnTraverseExcludeFolder(string sender);
        /// <summary>
        /// Called each time a folder is being traversed
        /// </summary>
        public static event OnTraverseExcludeFolder OnTraverseIncludeFolderEvent;

        public static bool Cancelled = false;

        /// <summary>
        /// Recursively remove an entire folder structure and files with events for monitoring and basic
        /// exception handling. USE WITH CARE
        /// </summary>
        /// <param name="directoryInfo"></param>
        /// <param name="cancellationToken"></param>
        public static async Task RecursiveDelete(DirectoryInfo directoryInfo, CancellationToken cancellationToken)
        {
            if (!directoryInfo.Exists)
            {
                OnDeleteEvent?.Invoke("Nothing to process");
                return;
            }

            OnDeleteEvent?.Invoke(directoryInfo.Name);

            DirectoryInfo folder = null;

            try
            {
                await Task.Run(async () =>
                {
                    foreach (DirectoryInfo dirInfo in directoryInfo.EnumerateDirectories())
                    {

                        folder = dirInfo;

                        if (
                            (folder.Attributes & FileAttributes.Hidden) == FileAttributes.Hidden || 
                            (folder.Attributes & FileAttributes.System) == FileAttributes.System || 
                            (folder.Attributes & FileAttributes.ReparsePoint) == FileAttributes.ReparsePoint) {

                            OnTraverseIncludeFolderEvent?.Invoke($"* {folder.FullName}");

                            continue;

                        }

                        OnTraverseIncludeFolderEvent?.Invoke($"Delete: {folder.FullName}");

                        if (!Cancelled)
                        {
                            await Task.Delay(1, cancellationToken);
                            await RecursiveDelete(folder, cancellationToken);
                        }
                        else
                        {
                            return;
                        }

                        if (cancellationToken.IsCancellationRequested)
                        {
                            cancellationToken.ThrowIfCancellationRequested();
                        }

                    }

                    /*
                     * assert if folder should be deleted, yes then
                     * directoryInfo.Delete(true);
                     */
                    
                }, cancellationToken);

            }
            catch (Exception exception)
            {
                switch (exception)
                {
                    case OperationCanceledException _:
                        Cancelled = true;
                        break;
                    case UnauthorizedAccessException _:
                        UnauthorizedAccessEvent?.Invoke($"Access denied '{exception.Message}'");
                        break;
                    default:
                        OnExceptionEvent?.Invoke(exception);
                        break;
                }
            }
        }
    }
}
```

Example call, in this case a windows application while the same will work with adjustments in other project types.

`_cancellationTokenSource` is a private `property` in the form.

```csharp
private async void RecursiveDeleteButton_Click(object sender, EventArgs e)
{
    if (_cancellationTokenSource.IsCancellationRequested)
    {
        _cancellationTokenSource.Dispose();
        _cancellationTokenSource = new CancellationTokenSource(TimeSpan.FromSeconds(1));
    }

    var resultsForm = new ResultsForm();
    resultsForm.Show();

    const string folder = @"C:\Users\paynek\Documents\Snagit";
    DirectoryInfo directoryInfo = new (folder);
    DirectoryOperations1.OnTraverseIncludeFolderEvent += folderName => resultsForm.Add(folderName);

    await DirectoryOperations1.RecursiveDelete(directoryInfo, _cancellationTokenSource.Token);
    
}
```

</br>

For some task [Directory.GetFiles](https://docs.microsoft.com/en-us/dotnet/api/system.io.directory.getfiles?view=net-6.0) is sufficient e.g. check if a folder has empty folders as we are simply checking the length returned for found (or shall we say not found) files.

```csharp
public static async Task FindEmptyFolders(string folderPath)
{
    await Task.Run(async () =>
    {
        await Task.Delay(1);

        foreach (var directory in Directory.GetDirectories(folderPath))
        {
            _ = FindEmptyFolders(directory);
            if (Directory.GetFiles(directory).Length == 0 && Directory.GetDirectories(directory).Length == 0)
            {
                EmptyFolderFound?.Invoke(directory);
            }
        }

    });
}
```

# Summary

Complete examples have been avoided on purpose to keep learning how to work with iterating folders and files along with performing actions to keep the learning process simple.

Take time to run through the various code samples by first reading the code then for many stepping through code using breakpoints and examining the entire process.

Once a having firm understanding to how to work with directories and files take time to first experiment then the final step is to use in a project.

# Updates

**01.22.2022** added new code samples
**01.18.2023** Moved to .NET Core 7


## Special notes

- There are several code samples taken from the web and have their links to find the original sources
- Several projects have empty class files which allow for seeing results better 