namespace ColdFusionTool1.Models;
public class ResultContainer
{
    public string PathOnly => Path.GetDirectoryName(PathAndFileName);
    public string FileName => Path.GetFileName(PathAndFileName);
    public string PathAndFileName { get; set; }
    public string Term { get; set; }
}
