namespace DirectoryHelpersLibrary.Models
{
    public class Psdrive
    {
        public string CurrentLocation { get; set; }
        public string Name { get; set; }
        public string Provider { get; set; }
        public string Root { get; set; }
        public string Description { get; set; }
        public object MaximumSize { get; set; }
        public string Credential { get; set; }
        public object DisplayRoot { get; set; }
    }
}