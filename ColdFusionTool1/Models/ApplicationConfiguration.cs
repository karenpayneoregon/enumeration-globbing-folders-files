namespace ColdFusionTool1.Models;
public class ApplicationConfiguration
{
    public string RootFolder { get; set; }
    public FilePattern FilePatterns { get; set; }
    public string LogFileName { get; set; }
    public string TokensFileName { get; set; }
}

public class FilePattern
{
    public List<string> Include { get; set; }
    public List<string> Exclude { get; set; }
}

public class FileMatchItem
{
    public FileMatchItem(string sender)
    {
        Folder = Path.GetDirectoryName(sender);
        FileName = Path.GetFileName(sender);
    }
    public string Folder { get; init; }
    public string FileName { get; init; }
    public string FullPath => Path.Combine(Folder, FileName);
    public override string ToString() => $"{Folder}\\{FileName}";

}