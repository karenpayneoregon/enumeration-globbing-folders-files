using System.Collections.Concurrent;
using System.Text;
using DirectoryHelpersLibrary.Models;
using Directory = System.IO.Directory;

namespace DirectoryHelpersLibrary.Classes;

public class DirectoryOperations2
{
    public delegate void OnTraverse(string sender);
    /// <summary>
    /// Provides files found iterating folders
    /// </summary>
    public static event OnTraverse Traverse;

    public delegate void OnTraverseFolder(FolderItem sender);

    /// <summary>
    /// Provides files found iterating folders
    /// </summary>
    public static event OnTraverseFolder TraverseFolder;

    public delegate void OnDone();
    /// <summary>
    /// Alters listener processing has completed 
    /// </summary>
    public static event OnDone Done;

    public delegate void OnDoneFiles();
    /// <summary>
    /// Alters listener processing has completed 
    /// </summary>
    public static event  OnDoneFiles DoneFiles;
        
    public delegate void OnFolderException(string sender);
    /// <summary>
    /// Raised when there is a runtime exception
    /// </summary>
    public static event OnFolderException FolderException;

    /// <summary>
    /// Enumerate files asynchronous using events for listeners to do whatever they want
    /// </summary>
    /// <param name="path">Path to iterate</param>
    /// <param name="searchPattern">pattern to filter with</param>
    /// <param name="searchOption">top level or deep <seealso cref="SearchOption"/></param>
    public static async Task Example1Async(string path, string searchPattern, SearchOption searchOption = SearchOption.AllDirectories)
    {
            
        using var enumerator = await Task.Run(() => Directory.EnumerateFiles(path, searchPattern, searchOption).GetEnumerator());

        while (await Task.Run(() => enumerator.MoveNext()))
        {
            Traverse?.Invoke(enumerator.Current);
        }

        Done?.Invoke();

    }
    public static async Task CollectFiles(string path, string searchPattern, SearchOption searchOption = SearchOption.TopDirectoryOnly)
    {
        using var enumerator = await Task.Run(() => Directory.EnumerateFiles(path, searchPattern, searchOption).GetEnumerator());
        while (await Task.Run(() => enumerator.MoveNext()))
        {
            Traverse?.Invoke(enumerator.Current);
        }
    }
    /// <summary>
    /// Enumerate files asynchronous using events for listeners to do whatever they want
    /// </summary>
    /// <param name="path">Path to iterate</param>
    /// <param name="searchPattern">pattern to filter with</param>
    /// <param name="searchOption">top level or deep <seealso cref="SearchOption"/> which defaults to SearchOption.AllDirectories</param>
    public static async Task Example2Async(string path, string searchPattern, SearchOption searchOption = SearchOption.AllDirectories)
    {

        using var enumerator = await Task.Run(() => Directory.EnumerateFiles(path, searchPattern, searchOption).GetEnumerator());

        while (await Task.Run(() => enumerator.MoveNext()))
        {
            Traverse?.Invoke(enumerator.Current);
        }

        DoneFiles?.Invoke();

    }
    /// <summary>
    /// Iterate file with a Func&lt;string, Task&gt;
    /// </summary>
    /// <param name="path">Path to iterate</param>
    /// <param name="searchPattern">Pattern-filter</param>
    /// <param name="searchOption"><see cref="SearchOption"/></param>
    /// <param name="workAsync">Perform work on each file</param>
    public static async Task Example3Async(string path, string searchPattern, SearchOption searchOption, Func<string, Task> workAsync)
    {
        using var enumerator = await Task.Run(() => Directory.EnumerateFiles(path, searchPattern, searchOption).GetEnumerator());

        while (await Task.Run(() => enumerator.MoveNext()))
        {
            await workAsync(enumerator.Current);
        }

        DoneFiles?.Invoke();
    }

    /// <summary>
    /// Enumerate files synchronous using events for listeners to do whatever they want
    /// </summary>
    /// <param name="path">Path to iterate</param>
    /// <param name="searchPattern">Pattern-filter</param>
    /// <param name="searchOption"><see cref="SearchOption"/></param>
    public static void Example1Sync(string path, string searchPattern, SearchOption searchOption = SearchOption.AllDirectories)
    {
        var filePaths = Directory.EnumerateFiles(path, searchPattern, searchOption);
        foreach (var file in filePaths)
        {
            Traverse?.Invoke(file);
        }

        Done?.Invoke();

    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="path"></param>
    /// <param name="searchPattern"></param>
    /// <param name="options"></param>
    /// <param name="searchOption"></param>
    public static void Example2Sync(string path, string searchPattern, EnumerationOptions options, SearchOption searchOption = SearchOption.AllDirectories )
    {
            
        var filePaths = Directory.EnumerateFiles(path, searchPattern, searchOption);
        foreach (var file in filePaths)
        {
            Traverse?.Invoke(file);
        }

        Done?.Invoke();
    }

    /// <summary>
    /// Enumerate files synchronous using events for listeners to do whatever they want
    /// </summary>
    /// <param name="path">Path to iterate</param>
    /// <param name="searchPattern">Pattern-filter</param>
    /// <returns>List of <see cref="FileItem"/> </returns>
    public static List<FileItem> GetFilesSync(string path, string searchPattern)
    {

        List<FileItem> list = new List<FileItem>();

        var filePaths = Directory.EnumerateFiles(path, searchPattern, SearchOption.AllDirectories);

        foreach (var file in filePaths)
        {
            FileInfo info = new (file);
            list.Add(new FileItem() {Name = file, Length = info.Length});
        }

        return list;
    }

    public static int _numberOfFolders { get; set; }
    private static readonly ConcurrentBag<Task> _concurrentBagTasks = new();

    /// <summary>
    /// Demonstration for returning folders asynchronous
    /// </summary>
    /// <param name="path">Path to iterate</param>
    public static async Task CollectFolders(string path)
    {

        await Task.Delay(1);

        DirectoryInfo directoryInfo = new(path);
        _concurrentBagTasks.Add(Task.Run(() => CrawlFolder(directoryInfo)));

        while (_concurrentBagTasks.TryTake(out var taskToWaitFor))
        {
            taskToWaitFor.Wait();
        }

        Done?.Invoke();
    }


    /// <summary>
    /// Recursive method to enumerate files
    /// </summary>
    /// <param name="dir">path to start processing</param>
    private static void CrawlFolder(DirectoryInfo dir)
    {
        try
        {
            DirectoryInfo[] directoryInfos = dir.GetDirectories();
            
            foreach (DirectoryInfo childInfo in directoryInfos)
            {
                DirectoryInfo di = childInfo;
                _concurrentBagTasks.Add(Task.Run(() => CrawlFolder(di)));
            }

            var fileCount = Directory.EnumerateFiles(dir.FullName, "*.*", SearchOption.TopDirectoryOnly).Count();
            TraverseFolder?.Invoke(new FolderItem() {Name = dir.FullName, Count = fileCount});

            _numberOfFolders++;

        }
        catch (Exception ex)
        {
            StringBuilder builder = new();

            while (ex != null)
            {
                builder.AppendLine($"{ex.GetType()} {ex.Message}\n{ex.StackTrace}");
                ex = ex.InnerException;
            }

            FolderException?.Invoke(builder.ToString());
        }
    }
}