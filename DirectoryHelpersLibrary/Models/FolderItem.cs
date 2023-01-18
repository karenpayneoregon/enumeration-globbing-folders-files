namespace DirectoryHelpersLibrary.Models;

public class FolderItem
{
    public string Name { get; set; }
    public int Count { get; set; }
    public string[] ItemArray => new[] { Name, Count.ToString() };
    public override string ToString() => Name;

}