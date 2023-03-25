using DirectoryHelpersLibrary.Models;
using Microsoft.Extensions.FileSystemGlobbing;
using Microsoft.Extensions.FileSystemGlobbing.Abstractions;
using Directory = System.IO.Directory;

namespace DirectoryHelpersLibrary.Classes;

public class GlobbingOperations
{
    public delegate void OnTraverse(string sender);
    /// <summary>
    /// Provides listeners with current file being processed
    /// </summary>
    public static event OnTraverse Traverse;

    public delegate void OnTraverseFileMatch(FileMatchItem sender);
    /// <summary>
    /// Informs listener of a <see cref="FileMatchItem"/>
    /// </summary>
    public static event OnTraverseFileMatch TraverseFileMatch;

    public delegate void OnNoMatches();
    /// <summary>
    /// Informs listener there is no match <see cref="PatternMatchingResult"/> for a pattern
    /// </summary>
    public static event OnNoMatches NoMatches;

    public delegate void OnDone(string message);
    /// <summary>
    /// Indicates processing has completed
    /// </summary>
    public static event OnDone Done;

    public static string FolderNotExistsText => "Folder does not exists";

    /// <summary>
    /// Folder to search/filter 
    /// </summary>
    /// <param name="path">folder to traverse</param>
    /// <param name="includePatterns">
    /// pattern match to filter e.g. **/s*.cs for all .cs files beginning with s in all folders under folderName
    /// </param>
    public static void GenericSearch(string path, string[] includePatterns)
    {

        if (!Directory.Exists(path))
        {
            Traverse?.Invoke(FolderNotExistsText);
            return;
        }

        Matcher matcher = new ();
        matcher.AddIncludePatterns(includePatterns);

        PatternMatchingResult matchingResult = matcher.Execute(new DirectoryInfoWrapper(new DirectoryInfo(path)));

        if (matchingResult.HasMatches)
        {

            foreach (var file in matchingResult.Files)
            {
                Traverse?.Invoke(Path.Combine(path, file.Path).Replace("/","\\"));
            }

            Done?.Invoke($"Match count {matchingResult.Files.Count()}");

        }
        else
        {
            Done?.Invoke("No matches");
        }

    }
    /// <summary>
    /// Folder to search/filter 
    /// </summary>
    /// <param name="path">folder to traverse</param>
    /// <param name="includePatterns">pattern match to filter e.g. **/s*.cs for all .cs files beginning with s in all folders under folderName </param>
    /// <param name="fileExtensions">file extensions without period to get e.g. cs for csharp, txt for text file etc</param>
    /// <remarks>
    /// Code sample to use will follow, needed to take it out as it had sensitive information
    /// </remarks>
    public static void FindStyleSheets(string path, string[] includePatterns, string[] fileExtensions)
    {

        if (!Directory.Exists(path))
        {
            Traverse?.Invoke(FolderNotExistsText);
            return;
        }

        IEnumerable<string> filterFiles = Utilities.FilterFiles(path, fileExtensions);
        InMemoryDirectoryInfo dirInfo = new(path, filterFiles);

        Matcher matcher = new ();

        matcher.AddIncludePatterns(includePatterns);
 
        PatternMatchingResult patternMatching =  matcher.Execute(dirInfo);

        if (!patternMatching.HasMatches)
        {
            NoMatches?.Invoke();
        }

        foreach (var match in patternMatching.Files)
        {
            Traverse?.Invoke(Path.Combine(path, match.Stem!).Replace("/", "\\"));
        }

        Done?.Invoke($"Match count {patternMatching.Files.Count()}");

    }

    /// <summary>
    /// Simple example to find files matching a pattern in a folder
    /// </summary>
    /// <param name="path">folder to traverse</param>
    /// <param name="patterns">include pattern</param>
    /// <returns>list of FileMatchItem</returns>
    public static List<FileMatchItem> Synchronous(string path, string[] patterns)
    {
        List<FileMatchItem> list = new();

        Matcher matcher = new();
        matcher.AddIncludePatterns(patterns);
            
        foreach (string file in matcher.GetResultsInFullPath(path))
        {
            list.Add(new FileMatchItem(file));
        }

        return list;

    }
    /// <summary>
    /// Simple example to find files matching a pattern in a folder
    /// </summary>
    /// <param name="path">folder to traverse</param>
    /// <param name="patterns">include pattern</param>
    /// <returns>list of FileMatchItem</returns>
    public static async Task<List<FileMatchItem>> Asynchronous(string path, string[] patterns)
    {
            
        List<FileMatchItem> list = new();

        Matcher matcher = new();
        matcher.AddIncludePatterns(patterns);

        return await Task.Run(async () =>
        {
            await Task.Delay(1);
            foreach (string file in matcher.GetResultsInFullPath(path))
            {
                list.Add(new FileMatchItem(file));
            }
            return list;
        });

    }
    /// <summary>
    /// Pass back an object which can represent path and file name
    /// </summary>
    /// <param name="parentFolder">folder to start in</param>
    /// <param name="patterns">search include pattern</param>
    /// <param name="excludePatterns">pattern to exclude</param>
    public static async Task Asynchronous(string parentFolder, string[] patterns, string[] excludePatterns)
    {

        Matcher matcher = new();
        matcher.AddIncludePatterns(patterns);
        matcher.AddExcludePatterns(excludePatterns);

        await Task.Run( () =>
        {
                
            foreach (string file in matcher.GetResultsInFullPath(parentFolder))
            {
                TraverseFileMatch?.Invoke(new FileMatchItem(file));
            }
        });

        Done?.Invoke("Finished - see log file");

    }
    /// <summary>
    /// Pass back full path and file name
    /// </summary>
    /// <param name="parentFolder">folder to start in</param>
    /// <param name="patterns">search include pattern</param>
    /// <param name="excludePatterns">pattern to exclude</param>
    public static async Task Asynchronous2(string parentFolder, string[] patterns, string[] excludePatterns)
    {

        List<FileMatchItem> list = new();

        Matcher matcher = new();
        matcher.AddIncludePatterns(patterns);
        matcher.AddExcludePatterns(excludePatterns);

        using var enumerator = await Task.Run(() => 
            matcher.GetResultsInFullPath(parentFolder).GetEnumerator());

        while (await Task.Run(() => enumerator.MoveNext()))
        {
            Traverse?.Invoke(enumerator.Current);
        }

        Done?.Invoke("Finished");

    }
}