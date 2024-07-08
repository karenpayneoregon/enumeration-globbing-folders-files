using FindFoldersApp.Classes;
using GlobExpressions;

namespace FindFoldersApp;

internal partial class Program
{
    private static void Main()
    {
        var root = DirectoryOperations.GetSolutionInfo().FullName;
        AnsiConsole.MarkupLine($"[yellow]Root:[/] [b]{root}[/]");

        List<string> directories =
            [
                    .. Glob.Directories(root, "**/Models").ToArray(),
                    .. Glob.Directories(root, "**/Classes").ToArray(),
                    .. Glob.Directories(root, "**/*extensions",
                        GlobOptions.CaseInsensitive).ToArray()
            ];

        List<string> ordered = directories.OrderBy(x => x).ToList();

        File.WriteAllText("directories.txt", string.Join(Environment.NewLine, ordered));



    }
}