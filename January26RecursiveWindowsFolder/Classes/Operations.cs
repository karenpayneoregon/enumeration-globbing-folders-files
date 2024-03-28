// ReSharper disable NotDisposedResourceIsReturned
namespace January26RecursiveWindowsFolder.Classes;

public class Operations
{
    public delegate void OnTraverse(string sender);
    public event OnTraverse Traverse = null!;
    public delegate void OnDone();
    public event OnDone Done = null!;
    public delegate void OnCancelled();
    public event OnCancelled Cancelled = null!;

    public async Task Example1Async(string path, string searchPattern, CancellationToken ct)
    {
        var options = new EnumerationOptions { IgnoreInaccessible = true, RecurseSubdirectories = true };

        using var enumerator = await Task.Run(() => Directory.EnumerateFiles(path, searchPattern, options)
            .GetEnumerator(), ct);

        while (await Task.Run(() => enumerator.MoveNext(), ct))
        {

            if (ct.IsCancellationRequested)
            {
                Cancelled.Invoke();
                return;
            }

            Traverse?.Invoke(enumerator.Current);
        }

        Done?.Invoke();

    }
}