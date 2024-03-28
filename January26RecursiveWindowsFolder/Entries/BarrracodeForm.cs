using System.Diagnostics;

namespace January26RecursiveWindowsFolder.Entries;

/// <summary>
/// https://gist.github.com/barracoder/bc8e16ed0536bb536576783696feacbe
/// </summary>
public partial class BarrracodeForm : Form
{
    private CancellationTokenSource cancellationTokenSource = new();
    public BarrracodeForm()
    {
        InitializeComponent();
    }
    private void ExecuteButton_Click(object sender, EventArgs e)
    {
        if (cancellationTokenSource.IsCancellationRequested)
        {
            cancellationTokenSource.Dispose();
            cancellationTokenSource = new CancellationTokenSource();
        }
        foreach (var file in FindRecursive("C:\\Windows", "*.dll", cancellationTokenSource.Token))
        {
            Debug.WriteLine(file);
        }
    }
    IEnumerable<FileInfo> FindRecursive(string rootPath, string searchPattern, CancellationToken token = default)
    {
        var root = new DirectoryInfo(rootPath);
        foreach (var file in root.EnumerateFiles(searchPattern, new EnumerationOptions { RecurseSubdirectories = true }))
        {
            if (token.IsCancellationRequested)
            {
                yield break;
            }
            yield return file;
        }
    }

    private void CancelButton_Click(object sender, EventArgs e)
    {
        cancellationTokenSource.Cancel();
    }
}
