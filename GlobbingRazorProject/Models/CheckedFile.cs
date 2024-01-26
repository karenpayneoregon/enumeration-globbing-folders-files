#pragma warning disable CS8618
using DirectoryHelpersLibrary.Models;
using System.Reflection;

namespace GlobbingRazorProject.Models;

public class CheckedFile
{
    public int Id { get; set; }
    public FileMatchItem FileMatchItem { get; set; }
    //public string Item => {
    //    return Path.Combine(FileMatchItem.Folder, FileMatchItem.FileName);
    //}

    public string Item => Path.Combine(FileMatchItem.Folder, FileMatchItem.FileName);

    public bool Checked { get; set; }
    public override string ToString() => $"{Id}   {Checked}";

}