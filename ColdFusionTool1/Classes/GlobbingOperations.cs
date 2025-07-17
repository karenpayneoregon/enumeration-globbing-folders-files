using Microsoft.Extensions.FileSystemGlobbing;
using ColdFusionTool1.Models;
using Serilog;

namespace ColdFusionTool1.Classes;

/// <summary>
/// Provides operations for file system globbing, including file matching and traversal.
/// </summary>
/// <remarks>
/// This class utilizes the <see cref="Matcher"/> to perform
/// file matching based on include and exclude patterns. It also provides events to notify
/// subscribers about file matches and the completion of operations.
/// </remarks>
internal class GlobbingOperations
{
    public delegate void OnTraverseFileMatch(FileMatchItem sender);
    /// <summary>
    /// Notifies subscribers about a matched <see cref="FileMatchItem"/>.
    /// </summary>
    public static event OnTraverseFileMatch TraverseFileMatch;

    public delegate void OnDone(string message);
    /// <summary>
    /// Signals the completion of the processing operation.
    /// </summary>
    public static event OnDone Done;

    /// <summary>
    /// Asynchronously retrieves files from a specified folder based on include and exclude patterns.
    /// </summary>
    /// <param name="parentFolder">
    /// The root folder from which to start searching for files.
    /// </param>
    /// <param name="patterns">
    /// A list of glob patterns specifying the files to include in the search.
    /// </param>
    /// <param name="excludePatterns">
    /// A list of glob patterns specifying the files to exclude from the search. This parameter is optional.
    /// </param>
    /// <returns>
    /// A <see cref="Task"/> representing the asynchronous operation.
    /// </returns>
    /// <remarks>
    /// This method uses the <see cref="Matcher"/> to match files
    /// based on the provided patterns. It triggers the <see cref="TraverseFileMatch"/> event for each matched file
    /// and the <see cref="Done"/> event upon completion of the operation.
    /// </remarks>
    public static async Task GetFiles(string parentFolder, List<string> patterns, List<string> excludePatterns)
    {

        try
        {
            List<FileMatchItem> list = [];

            Matcher matcher = new();
            matcher.AddIncludePatterns(patterns);

            if (excludePatterns is not null)
            {
                matcher.AddExcludePatterns(excludePatterns);
            }

            await Task.Run(() =>
            {

                foreach (string file in matcher.GetResultsInFullPath(parentFolder))
                {
                    var item = new FileMatchItem(file);
                    TraverseFileMatch?.Invoke(item);
                    Scan(item);
                }

            });

            Done?.Invoke("Finished scanning");
        }
        catch (Exception e)
        {
            Log.Error(e,"Failed on traversing a folder");
        }

    }

    private static void Scan(FileMatchItem sender)
    {
        // Implement scanning logic here 
    }

}
