namespace DirectoryHelpersLibrary.Models;

public class Psprovider
{
    public string ImplementingType { get; set; }
    public string HelpFile { get; set; }
    public string Name { get; set; }
    public string PSSnapIn { get; set; }
    public string ModuleName { get; set; }
    public object Module { get; set; }
    public string Description { get; set; }
    public int Capabilities { get; set; }
    public string Home { get; set; }
    public string Drives { get; set; }
}