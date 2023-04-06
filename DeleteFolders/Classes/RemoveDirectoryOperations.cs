
using System.Runtime.CompilerServices;

namespace DeleteFolders.Classes;

/// <summary>
/// Duplication of DirectoryHelpersLibrary.Classes.DirectoryOperations1 were this one
/// added [CallerMemberName] on several events
/// </summary>
public class RemoveDirectoryOperations
{
    public delegate void OnDelete(string status, [CallerMemberName] string callerName = null);
    /// <summary>
    /// Callback for subscribers to see what is being worked on
    /// </summary>
    public static event OnDelete OnDeleteEvent;

    public delegate void OnException(Exception exception, [CallerMemberName] string callerName = null);
    /// <summary>
    /// Callback for subscribers to know about a problem
    /// </summary>
    public static event OnException OnExceptionEvent;

    public delegate void OnUnauthorizedAccessException(string message, [CallerMemberName] string callerName = null);
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
    /// <param name="execute">true to perform delete, false not to perform delete</param>
    public static async Task RecursiveDelete(DirectoryInfo directoryInfo, CancellationToken cancellationToken, bool execute  = false)
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

                        OnTraverseIncludeFolderEvent?.Invoke($"Nope {folder.FullName}");

                        continue;

                    }

                    OnTraverseIncludeFolderEvent?.Invoke($"Delete this folder: {folder.FullName}");

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
                
                if (execute)
                {
                    try
                    {
                        directoryInfo.Delete(true);
                    }
                    catch (Exception exception)
                    {
                        OnExceptionEvent?.Invoke(exception);
                    }
                }

                OnDeleteEvent?.Invoke($"Fake delete {directoryInfo.Name}");

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