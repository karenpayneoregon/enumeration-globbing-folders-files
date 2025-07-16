namespace ColdFusionTool1.Models;

/// <summary>
/// Represents information about a line in a text file, including its index and content.
/// </summary>
public class LineInfo
{
    public int Index { get; set; }
    public string LineText { get; set; } = string.Empty;
}