namespace DirectoryHelpersLibrary.Classes;
public class FolderOperations
{
    public delegate void OnEmptyFolderFound(string folder);

    /// <summary>
    /// Callback for listeners to know the folder was not found
    /// </summary>
    public static event OnEmptyFolderFound EmptyFolderFound;
    /// <summary>
    /// Demo to show how to find empty folders
    /// </summary>
    /// <param name="path">folder to traverse</param>
    public static async Task FindEmptyFolders(string path)
    {
        await Task.Run(async () =>
        {
            await Task.Delay(1);

            foreach (var directory in Directory.GetDirectories(path))
            {
                _ = FindEmptyFolders(directory);
                if (Directory.GetFiles(directory).Length == 0 && Directory.GetDirectories(directory).Length == 0)
                {
                    EmptyFolderFound?.Invoke(directory);
                }
            }

        });
    }
}
