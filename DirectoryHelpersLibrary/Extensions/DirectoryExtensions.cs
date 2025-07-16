using Microsoft.Extensions.FileSystemGlobbing;
using System.Text.RegularExpressions;

namespace DirectoryHelpersLibrary.Extensions;

/// <summary>
/// https://www.techmikael.com/2010/02/directory-search-with-multiple-filters.html
/// One version takes a regex pattern \.mp3|\.mp4, and the other a string list and runs in parallel.
/// GetFiles
/// </summary>
public static class DirectoryExtensions
{
    /// <summary>
    /// Retrieves the full paths of files that match the patterns defined in the <see cref="Matcher"/> 
    /// within the specified directory.
    /// </summary>
    /// <param name="matcher">
    /// An instance of <see cref="Matcher"/> containing the patterns used to match file paths.
    /// </param>
    /// <param name="path">
    /// The root directory to search for matching files.
    /// </param>
    /// <returns>
    /// An enumerable collection of full file paths that match the patterns defined in the <paramref name="matcher"/>.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// Thrown when the <paramref name="matcher"/> or <paramref name="path"/> is <c>null</c>.
    /// </exception>
    /// <exception cref="DirectoryNotFoundException">
    /// Thrown when the specified <paramref name="path"/> does not exist.
    /// </exception>
    /// <exception cref="UnauthorizedAccessException">
    /// Thrown when the caller does not have the required permission to access the directory.
    /// </exception>
    /// <exception cref="IOException">
    /// Thrown when an I/O error occurs while accessing the file system.
    /// </exception>
    public static IEnumerable<string> ResultsInFullPath(this Matcher matcher,string path) 
        => matcher.GetResultsInFullPath(path);

    /// <summary>
    /// Retrieves files from the specified directory that match the given regular expression pattern.
    /// </summary>
    /// <param name="path">The directory to search for files.</param>
    /// <param name="searchPatternExpression">
    /// A regular expression pattern used to filter the files by their extensions or names.
    /// </param>
    /// <param name="searchOption">
    /// Specifies whether to search only the current directory or all subdirectories. 
    /// The default is <see cref="SearchOption.TopDirectoryOnly"/>.
    /// </param>
    /// <returns>
    /// An enumerable collection of file paths that match the specified regular expression pattern.
    /// </returns>
    /// <exception cref="ArgumentException">
    /// Thrown when the <paramref name="path"/> is null, empty, or contains invalid characters.
    /// </exception>
    /// <exception cref="RegexParseException">
    /// Thrown when the <paramref name="searchPatternExpression"/> contains an invalid regular expression.
    /// </exception>
    /// <exception cref="UnauthorizedAccessException">
    /// Thrown when the caller does not have the required permission to access one or more files or directories.
    /// </exception>
    /// <exception cref="DirectoryNotFoundException">
    /// Thrown when the specified <paramref name="path"/> does not exist.
    /// </exception>
    /// <exception cref="IOException">
    /// Thrown when an I/O error occurs while accessing the file system.
    /// </exception>
    public static IEnumerable<string> GetFiles(string path,
        string searchPatternExpression = "",
        SearchOption searchOption = SearchOption.TopDirectoryOnly)
    {

        Regex reSearchPattern = new(searchPatternExpression, RegexOptions.IgnoreCase);

        return Directory.EnumerateFiles(path, "*", searchOption)
            .Where(file =>
                reSearchPattern.IsMatch(Path.GetExtension(file)));
    }

    /// <summary>
    /// Retrieves files from the specified directory that match any of the provided search patterns.
    /// </summary>
    /// <param name="path">The directory to search for files.</param>
    /// <param name="searchPatterns">An array of search patterns to filter the files by.</param>
    /// <param name="searchOption">
    /// Specifies whether to search only the current directory or all subdirectories. 
    /// The default is <see cref="SearchOption.TopDirectoryOnly"/>.
    /// </param>
    /// <returns>
    /// An enumerable collection of file paths that match the specified search patterns.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// Thrown when the <paramref name="searchPatterns"/> parameter is null.
    /// </exception>
    public static IEnumerable<string> GetFiles(string path,
        string[] searchPatterns,
        SearchOption searchOption = SearchOption.TopDirectoryOnly)
    {
        return searchPatterns.AsParallel()
            .SelectMany(searchPattern =>
                Directory.EnumerateFiles(path, searchPattern, searchOption));
    }

    /// <summary>
    /// Retrieves files from the specified directory that match any of the provided extensions.
    /// </summary>
    /// <param name="dir">The directory to search for files.</param>
    /// <param name="extensions">An array of file extensions to filter the files by.</param>
    /// <returns>An enumerable collection of <see cref="FileInfo"/> objects representing the files with the specified extensions.</returns>
    /// <exception cref="ArgumentNullException">Thrown when the <paramref name="extensions"/> parameter is null.</exception>
    public static IEnumerable<FileInfo> GetFilesByExtensions(this DirectoryInfo dir, params string[] extensions)
    {
        if (extensions == null)
        {
            throw new ArgumentNullException(nameof(extensions));
        }

        IEnumerable<FileInfo> files = dir.EnumerateFiles();
        return files.Where((fi) => extensions.Contains(fi.Extension));
    }

    /// <summary>
    /// Renames all files in the specified folder with the given original extension
    /// to use the replacement extension.
    /// </summary>
    /// <param name="path">The directory containing the files to rename.</param>
    /// <param name="originalExtension">The current file extension to be replaced.</param>
    /// <param name="replacementExtension">The new file extension to apply.</param>
    /// <returns>Returns true if successful; otherwise, false with the raised exception.</returns>
    public static (bool Success, Exception Exception) RenameExtensions(string path, string originalExtension, string replacementExtension)
    {
        try
        {
            new DirectoryInfo(path).GetFiles($"*.{originalExtension}")
                .ToList()
                .ForEach(currentFile =>
                {
                    var filename = Path.ChangeExtension(currentFile.Name, $".{replacementExtension}");

                    var tempName = Path.Combine(path, filename!);

                    if (File.Exists(tempName))
                    {
                        File.Delete(tempName);
                    }

                    File.Move(currentFile.Name!, filename);

                });

            return (true, null);
        }
        catch (Exception exception)
        {
            return (false, exception);
        }
    }
}