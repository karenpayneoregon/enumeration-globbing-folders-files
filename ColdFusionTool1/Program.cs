using ColdFusionTool1.Classes;
using ColdFusionTool1.Models;

namespace ColdFusionTool1;

internal partial class Program
{
    static async Task Main(string[] args)
    {
        GlobbingOperations.TraverseFileMatch += GlobbingOperations_TraverseFileMatch;
        GlobbingOperations.Done += message => AnsiConsole.MarkupLine($"[white on blue]{message}[/]");
        InitializeConfiguration();
        var settings = SetupConfiguration.LoadSettingsFromFile();
        await GlobbingOperations.GetFiles(
            settings.RootFolder,
            settings.FilePatterns.Include,
            settings.FilePatterns.Exclude);

        Console.ReadLine();
    }

    /// <summary>
    /// Initializes the application configuration by setting up default settings 
    /// and saving them to a file.
    /// </summary>
    /// <remarks>
    /// This method internally calls <see cref="SetupConfiguration.SetAppSettings"/> 
    /// to initialize the default application settings and 
    /// <see cref="SetupConfiguration.SaveSettingsToFile(string)"/> to persist them.
    /// </remarks>
    private static void InitializeConfiguration()
    {
        SetupConfiguration.SetAppSettings();
        SetupConfiguration.SaveSettingsToFile();
    }

    /// <summary>
    /// Handles the <see cref="GlobbingOperations.TraverseFileMatch"/> event, which is triggered
    /// when a file match is found during the globbing operation.
    /// </summary>
    /// <param name="sender">
    /// An instance of <see cref="FileMatchItem"/> representing the matched file, including its folder and file name.
    /// </param>
    private static void GlobbingOperations_TraverseFileMatch(FileMatchItem sender)
    {
        AnsiConsole.MarkupLine($"[green]Scanning:[/] {sender}");
    }
}