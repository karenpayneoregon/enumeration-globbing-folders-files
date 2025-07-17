using ColdFusionTool1.Models;
using Serilog;

namespace ColdFusionTool1.Classes;
public class FileChecker
{
    /// <summary>
    /// Checks if the specified file contains any of the provided search terms.
    /// </summary>
    /// <param name="filePath">The path to the file to be checked.</param>
    /// <param name="searchTerms">An array of search terms to look for in the file.</param>
    /// <returns>
    /// <c>true</c> if the file contains any of the search terms; otherwise, <c>false</c>.
    /// </returns>
    /// <exception cref="FileNotFoundException">Thrown if the specified file does not exist.</exception>
    /// <exception cref="IOException">Thrown if an error occurs while reading the file.</exception>
    public static (bool yes, List<ResultContainer> list) FileContainsAny(string filePath, string[] searchTerms)
    {
        if (!File.Exists(filePath))
        {
            return (false, []);
        }

        string fileContent;

        try
        {
            fileContent = File.ReadAllText(filePath);
        }
        catch (Exception ex)
        {
            Log.Error(ex, $"Error reading file: {ex.Message}");
            return (false, new List<ResultContainer>());
        }


        List<ResultContainer> list = [];
        list.AddRange(
            from term in searchTerms where fileContent.Contains(term, StringComparison.OrdinalIgnoreCase) 
            select new ResultContainer()
            {
                PathAndFileName = filePath, 
                Term = term
            });

        return list.Count > 0 ? (true, list) : (false, new List<ResultContainer>());
        
    }
}