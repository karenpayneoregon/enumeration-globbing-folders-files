﻿using System.Runtime.CompilerServices;

// ReSharper disable once CheckNamespace
namespace GlobbingIncludeExcludeApp;
internal partial class Program
{
    [ModuleInitializer]
    public static void Init()
    {
        AnsiConsole.MarkupLine("");
        Console.Title = "Code sample";
        WindowUtility.SetConsoleWindowPosition(WindowUtility.AnchorWindow.Center);
    }
}
