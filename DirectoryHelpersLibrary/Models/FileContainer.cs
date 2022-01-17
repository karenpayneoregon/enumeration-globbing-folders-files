using System;

namespace DirectoryHelpersLibrary.Models
{
    public class FileContainer
    {
        public string Name { get; set; }
        public long Length { get; set; }
        public string DirectoryName { get; set; }
        public Directory Directory { get; set; }
        public bool IsReadOnly { get; set; }
        public bool Exists { get; set; }
        public string FullName { get; set; }
        public string Extension { get; set; }
        public DateTime CreationTime { get; set; }
        public DateTime CreationTimeUtc { get; set; }
        public DateTime LastAccessTime { get; set; }
        public DateTime LastAccessTimeUtc { get; set; }
        public DateTime LastWriteTime { get; set; }
        public DateTime LastWriteTimeUtc { get; set; }
        public int Attributes { get; set; }
        public string PSPath { get; set; }
        public string PSParentPath { get; set; }
        public string PSChildName { get; set; }
        public Psdrive PSDrive { get; set; }
        public Psprovider PSProvider { get; set; }
        public bool PSIsContainer { get; set; }
        public string Mode { get; set; }
        public Versioninfo VersionInfo { get; set; }
        public string BaseName { get; set; }
        public object[] Target { get; set; }
        public object LinkType { get; set; }

        public override string ToString() => Name;
    }
}