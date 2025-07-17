using ColdFusionTool1.Models;
using System.Text.RegularExpressions;

namespace ColdFusionTool1.Classes;
public class FileOperations
{
    /// <summary>
    /// Asynchronously reads a text file and returns a list of line information.
    /// </summary>
    /// <remarks>
    /// This method reads the file line by line, capturing both the line number and the line text. 
    /// It is suitable for processing large files as it reads the file asynchronously.
    /// </remarks>
    /// <param name="fileName">
    /// The path to the text file to be read. Cannot be null or empty.
    /// </param>
    /// <returns>
    /// A task representing the asynchronous operation. The task result contains a list of 
    /// <see cref="LineInfo"/> objects, each representing a line in the file 
    /// with its index and text.
    /// </returns>
    public static async Task<List<LineInfo>> ReadTexFileAsync(string fileName)
    {
        List<LineInfo> list = [];

        await using var fileStream = File.OpenRead(fileName);
        using var streamReader = new StreamReader(fileStream);

        var index = 1;

        while (await streamReader.ReadLineAsync() is { } line)
        {
            list.Add(new LineInfo { Index = index++, LineText = line });
        }

        return list;
    }

    /// <summary>
    /// Determines whether the specified input string contains a CGI prefix followed by the target word.
    /// </summary>
    /// <param name="input">
    /// The input string to search. Cannot be null, empty, or consist only of whitespace.
    /// </param>
    /// <param name="targetWord">
    /// The target word to check for after the CGI prefix. Cannot be null, empty, or consist only of whitespace.
    /// </param>
    /// <returns>
    /// <c>true</c> if the input string contains the CGI prefix followed by the target word (case-insensitive); otherwise, <c>false</c>.
    /// </returns>
    public static bool HasCgiPrefix(string input, string targetWord)
    {
        if (string.IsNullOrWhiteSpace(input) || string.IsNullOrWhiteSpace(targetWord))
            return false;

        return Regex.IsMatch(input, $@"\bCGI\.{Regex.Escape(targetWord)}\b", RegexOptions.IgnoreCase);
    }


}
