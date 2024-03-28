using FindFoldersApp.Classes;
using GlobExpressions;

namespace FindFoldersApp;

internal partial class Program
{
    static void Main(string[] args)
    {
        var root = DirectoryOperations.GetSolutionInfo().FullName;
        AnsiConsole.MarkupLine($"[yellow]Root:[/] [b]{root}[/]");

        string[] modelsDirectories = Glob.Directories(root, "**/Models").ToArray();
        string[] classesDirectories = Glob.Directories(root, "**/Classes").ToArray();
        string[] extensionsDirectories = Glob.Directories(root, "**/*extensions", 
            GlobOptions.CaseInsensitive).ToArray();

        List<string> directories = 
            [
                .. modelsDirectories, 
                .. extensionsDirectories, 
                .. classesDirectories
            ];

        List<string> ordered = directories.OrderBy(x => x).ToList();

        File.WriteAllText("directories.txt", string.Join(Environment.NewLine, ordered));

    }
}