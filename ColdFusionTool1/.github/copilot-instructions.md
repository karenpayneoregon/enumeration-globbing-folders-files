# Copilot Instructions for ColdFusionTool1

## Project Overview
- **ColdFusionTool1** is a .NET console application for scanning and analyzing ColdFusion files (`.cfm`, `.cfc`) in a specified root directory, using glob patterns for inclusion/exclusion.
- The main workflow is orchestrated in `Program.cs`, leveraging classes in `Classes/` and models in `Models/`.

## Architecture & Data Flow
- **Configuration**: Application settings are loaded via `AppData.Instance.Configuration`, which is initialized from `configuration.json` using `Configurations.LoadSettingsFromFile()`.
- **File Matching**: `GlobbingOperations` uses `Microsoft.Extensions.FileSystemGlobbing` to traverse directories and match files. Events (`TraverseFileMatch`, `Done`) are used for reporting progress and results.
- **File Checking**: `FileChecker.FileContainsAny` checks if files contain any of the configured search terms (from `DelimitedItems`). Results are returned as a tuple and list of `ResultContainer` objects.
- **Logging**: Logging is set up via Serilog in `SetupLogging.ToFile()`, writing to dated log files under `bin/Debug/net*/LogFiles/`.

## Key Files & Patterns
- `Classes/GlobbingOperations.cs`: Core file traversal and matching logic.
- `Classes/FileChecker.cs`: File content search logic.
- `Classes/Configurations.cs`: Configuration load/save routines.
- `Models/ApplicationConfiguration.cs`: Configuration schema.
- `configuration.json`: Main config file (root folder, patterns, terms).

## Developer Workflows
- **Build**: Use standard .NET build commands (`dotnet build`).
- **Run**: Execute via `dotnet run` or run the built `.exe` in `bin/Debug/net*/`.
- **Configuration**: Edit `configuration.json` for root folder, file patterns, and search terms. Defaults are set in `Configurations.SetAppSettings()`.
- **Logging**: Check log files in `bin/Debug/net*/LogFiles/` for diagnostics.

## Project-Specific Conventions
- Uses C# 11 features (e.g., list literals `[]`).
- All configuration and state is accessed via the singleton `AppData.Instance`.
- File patterns are always lists of glob strings (see `FilePattern` model).
- Search terms are comma-delimited in config, parsed to arrays in `AppData`.
- Events are used for reporting file matches and completion (see `GlobbingOperations`).

## Integration Points & Dependencies
- **External Libraries**: Serilog for logging, Spectre.Console for console output, Microsoft.Extensions.FileSystemGlobbing for file matching.
- **No external service calls**; all operations are local filesystem and config-based.

## Example Usage
```csharp
await GlobbingOperations.GetFiles(
    AppData.Instance.Configuration.RootFolder,
    AppData.Instance.Configuration.FilePatterns.Include,
    AppData.Instance.Configuration.FilePatterns.Exclude);
```

---

For unclear or incomplete sections, please provide feedback or specify areas needing deeper documentation.
