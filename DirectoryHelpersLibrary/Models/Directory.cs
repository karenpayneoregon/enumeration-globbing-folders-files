using System;

namespace DirectoryHelpersLibrary.Models
{
    /// <summary>
    /// Child of <see cref="FileContainer"/>
    /// </summary>
    public class Directory
    {
        public string Name { get; set; }
        public string FullName { get; set; }
        public string Parent { get; set; }
        public bool Exists { get; set; }
        public string Root { get; set; }
        public string Extension { get; set; }
        public DateTime CreationTime { get; set; }
        public DateTime CreationTimeUtc { get; set; }
        public DateTime LastAccessTime { get; set; }
        public DateTime LastAccessTimeUtc { get; set; }
        public DateTime LastWriteTime { get; set; }
        public DateTime LastWriteTimeUtc { get; set; }
        public int Attributes { get; set; }
        public override string ToString() => Name;

    }
}
