using System.Diagnostics;
using DirectoryHelpersLibrary.Classes;
using DirectoryHelpersLibrary.Models;
using EnumerateFilesProject.Extensions;
using WindowsFormsLibrary.Classes;
using Directory = System.IO.Directory;

namespace EnumerateFilesProject
{
    /// <summary>
    /// An example for obtaining .cs files under a specific path using
    /// Directory.EnumerateFiles wrapped in a Task which is then iterated
    /// over in a while statement which using an event to pass results
    /// back to the caller.
    ///
    /// By default, inaccessible are excluded.
    ///
    /// Any file with Assembly or Designer in the name are not included
    /// in the ListBox. This may also be done in the method while excluding
    /// these conditions make for a generic method.
    ///
    /// Code resides in the project DirectoryHelpersLibrary.
    /// </summary>
    public partial class Example1AsyncForm : Form
    {
        public Example1AsyncForm()
        {
            InitializeComponent();

            DirectoryOperations2.Traverse += OnTraverse;
            DirectoryOperations2.Done += OnDone;
        }

        private void OnDone()
        {
            Dialogs.AutoCloseDialog(this,"Done!!!",Properties.Resources.blueInformation_32,1);
        }

        private void OnTraverse(string sender)
        {
            
            string[] exclude = { "Assembly", "Designer" };

            if (sender.ContainsAny(exclude)) return;
            ResultsListBox.Items.Add(sender);
            ResultsListBox.SelectedIndex = ResultsListBox.Items.Count - 1;

        }

        private async void ExecuteButtonAsync_Click(object sender, EventArgs e)
        {
            
            ResultsListBox.Items.Clear();

            string path = DirectoryHelper.SolutionFolder();
            string searchPattern = "*.cs";
            await DirectoryOperations2.Example1Async(path, searchPattern);
        }

        private void ExecuteButtonSync_Click(object sender, EventArgs e)
        {
            ResultsListBox.Items.Clear();

            string path = DirectoryHelper.SolutionFolder();
            string searchPattern = "*.cs";

            EnumerationOptions enumerationOptions = new EnumerationOptions() { IgnoreInaccessible = true };

            DirectoryOperations2.Example2Sync(path, searchPattern, enumerationOptions);



        }

        /// <summary>
        /// Typical examples found on the web
        /// </summary>
        public static void Typical()
        {
            string path = @"C:\current";

            var txtFiles = Directory.EnumerateFiles(path, "*.txt", SearchOption.AllDirectories);

            foreach (string currentFile in txtFiles)
            {
                Console.WriteLine(currentFile.Substring(path.Length + 1));
            }


            foreach (string file in Directory.EnumerateFiles(path, "*.*", SearchOption.AllDirectories))
            {
                Console.WriteLine(file);
            }
        }

        private  void CollectButton_Click(object sender, EventArgs e)
        {
            string path = DirectoryHelper.SolutionFolder();
            _ = DirectoryOperations2.CollectFolders(path);
        }

        #region Read files in folder then read each file contents

        private List<FileLines> _fileLinesList = new ();
        private async void WithActionButton_Click(object sender, EventArgs e)
        {
            _fileLinesList = new List<FileLines>();
            
            string path = $"{DirectoryHelper.SolutionFolder()}\\DirectoryHelpersLibrary\\Classes";

            await DirectoryOperations2.Example3Async(
                path,
                "*.cs",
                SearchOption.TopDirectoryOnly, WorkAsync);

            Dialogs.Information($"Found {_fileLinesList.Count} files");

        }
        /// <summary>
        /// Here we get lines from a file, from here it's possible to extend
        /// to find text in the file.
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        private async Task WorkAsync(string file)
        {
            var lines = await FileOperations.ReadAllTextListAsync(file);
            _fileLinesList.Add(new FileLines() {Name = file, Lines = lines});
        }
        #endregion

        private async void FileCountButton_Click(object sender, EventArgs e)
        {
            string path = DirectoryHelper.SolutionFolder();
            var count = await DirectoryOperations.FileCount(path, new[] { ".cs" });
            Debug.WriteLine(count);
        }
    }


}
