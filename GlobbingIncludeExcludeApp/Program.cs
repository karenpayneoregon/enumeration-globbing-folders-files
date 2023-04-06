using DirectoryHelpersLibrary.Classes;

namespace GlobbingIncludeExcludeApp;

internal partial class Program
{
    private static string RootPath = DirectoryHelper.SolutionFolder();
    static async Task Main(string[] args)
    {
        GlobbingOperations.TraverseFileMatch += GlobbingOperations_TraverseFileMatch;
        GlobbingOperations.Done += GlobbingOperationsOnDone;

        AnsiConsole.MarkupLine("[yellow]Hello[/]");


        string[] include = { "**/*.cs" };
        string[] exclude =
        {
            "**/*Assembly*.cs", 
            "**/*Designer*.cs",
            "**/*Global*.cs",
            "**/*g.cs"
        };

        // /glob/src/Glob/AST
        await GlobbingOperations.GetFiles(RootPath, include, exclude);

        Console.ReadLine();
        
    }

    private static void GlobbingOperationsOnDone(string message)
    {
        AnsiConsole.MarkupLine($"[white on blue]{message}[/]");
    }

    private static void GlobbingOperations_TraverseFileMatch(DirectoryHelpersLibrary.Models.FileMatchItem sender)
    {
        Console.WriteLine(Path.Combine(sender.Folder, sender.FileName));
    }
}