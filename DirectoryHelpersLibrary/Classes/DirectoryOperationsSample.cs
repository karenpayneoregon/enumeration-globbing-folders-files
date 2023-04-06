namespace DirectoryHelpersLibrary.Classes;
internal class DirectoryOperationsSample
{
    public static bool Cancelled;
    public delegate void OnException(Exception exception);
    /// <summary>
    /// Callback for subscribers to know about a problem
    /// </summary>
    public static event OnException OnExceptionEvent;

    public delegate void OnTraverseExcludeFolder(string sender);
    /// <summary>
    /// Called each time a folder is being traversed
    /// </summary>
    public static event OnTraverseExcludeFolder OnTraverseIncludeFolderEvent;
    public delegate void OnTraverse(string status);
    /// <summary>
    /// Callback for subscribers to see what is being worked on
    /// </summary>
    public static event OnTraverse OnTraverseEvent;
    public delegate void OnUnauthorizedAccessException(string message);
    /// <summary>
    /// Raised when attempting to access a folder the user does not have permissions too
    /// </summary>
    public static event OnUnauthorizedAccessException UnauthorizedAccessEvent;
    public static async Task RecursiveFolder(DirectoryInfo directoryInfo, CancellationToken cancellationToken)
    {
        
        OnTraverseEvent?.Invoke(directoryInfo.Name);

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
                        (folder.Attributes & FileAttributes.ReparsePoint) == FileAttributes.ReparsePoint)
                    {

                        OnTraverseIncludeFolderEvent?.Invoke($"* {folder.FullName}");

                        continue;

                    }

                    OnTraverseIncludeFolderEvent?.Invoke($"{folder.FullName}");

                    if (!Cancelled)
                    {
                        await Task.Delay(1, cancellationToken);
                        await RecursiveFolder(folder, cancellationToken);
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
