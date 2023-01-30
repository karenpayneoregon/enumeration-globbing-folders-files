namespace FindEmptyFolderExample.Classes;
public class FolderOperations
{
    public delegate void OnEmptyFolderFound(string folder);
    public delegate void OnDone();

    /// <summary>
    /// Callback for listeners to know the folder was not found
    /// </summary>
    public static event OnEmptyFolderFound EmptyFolderFound;
    public static event OnDone Done;
    /// <summary>
    /// Demo to show how to find empty folders
    /// </summary>
    /// <param name="path">folder to traverse</param>
    public static void FindEmptyFolders(string path)
    {
        foreach (var directory in Directory.GetDirectories(path))
        {
            FindEmptyFolders(directory);
            if (Directory.GetFiles(directory).Length == 0 && Directory.GetDirectories(directory).Length == 0)
            {
                EmptyFolderFound?.Invoke(directory);
            }
        }

        Done!.Invoke();
    }
}
