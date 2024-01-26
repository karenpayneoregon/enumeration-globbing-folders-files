using DirectoryHelpersLibrary.Models;
using GlobbingRazorProject.Models;
using Microsoft.Extensions.FileSystemGlobbing;


namespace GlobbingRazorProject.Classes;

public class Operations
{
    public static List<CheckedFile> IncludeExclude(string parentFolder, string[] patterns, string[] excludePatterns)
    {
        List<CheckedFile> files = new();
        Matcher matcher = new();
        matcher.AddIncludePatterns(patterns);
        matcher.AddExcludePatterns(excludePatterns);
        var test = matcher.ResultsInFullPath(parentFolder);
        foreach (string file in matcher.GetResultsInFullPath(parentFolder))
        {
            files.Add(new CheckedFile { FileMatchItem = new FileMatchItem(file) });
        }

        return files;

    }
}

public static class DirectoryExtensions
{
    public static IEnumerable<string> ResultsInFullPath(this Matcher matcher, string path)
        => matcher.GetResultsInFullPath(path);
}