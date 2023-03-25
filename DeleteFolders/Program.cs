using System;
using System.Runtime.CompilerServices;
using Spectre.Console;
using static DeleteFolders.Classes.RemoveDirectoryOperations;

namespace DeleteFolders;

internal partial class Program
{
    private static readonly CancellationTokenSource cancellationToken = new();
    static async Task Main(string[] args)
    {
        DirectoryInfo info = new("C:\\Work");
        AnsiConsole.MarkupLine($"[yellow]Traversing[/] {info.FullName}");

        // setup listeners 
        OnDeleteEvent += RemoveDirectoryOperations_OnDeleteEvent;
        OnExceptionEvent += RemoveDirectoryOperations_OnExceptionEvent;
        UnauthorizedAccessEvent += RemoveDirectoryOperations_UnauthorizedAccessEvent;
        OnTraverseIncludeFolderEvent += RemoveDirectoryOperations_OnTraverseIncludeFolderEvent;

        // perform mocked delete
        await RecursiveDelete(info, cancellationToken.Token );

        Console.ReadLine();
    }

    private static void RemoveDirectoryOperations_OnTraverseIncludeFolderEvent(string sender)
    {
        AnsiConsole.MarkupLine($"\t[white]{sender}[/]");
    }

    private static void RemoveDirectoryOperations_UnauthorizedAccessEvent(
        string message, [CallerMemberName] string callerName = null)
    {
        AnsiConsole.MarkupLine($"[yellow on red]{callerName}[/] {message}");
    }

    private static void RemoveDirectoryOperations_OnExceptionEvent(
        Exception exception, [CallerMemberName] string callerName = null)
    {
        AnsiConsole.MarkupLine($"[white on red]{callerName}[/] {exception.Message}");
    }

    private static void RemoveDirectoryOperations_OnDeleteEvent(
        string status, [CallerMemberName] string callerName = null)
    {
        AnsiConsole.MarkupLine($"[cyan]{callerName}[/] {status}");
    }
}