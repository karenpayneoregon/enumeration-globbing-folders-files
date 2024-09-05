using Serilog;
using DirectoryHelpersLibrary.Classes;
using DirectoryHelpersLibrary.Models;
using WindowsFormsLibrary.Classes;

namespace GlobbingProject
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();

            GlobbingOperations.TraverseFileMatch += TraverseFileMatch;
            GlobbingOperations.Done += Done;

        }

        private void Done(string message)
        {
            Log.Information("Done\n");
            Dialogs.AutoCloseDialog(this, message, Properties.Resources.blueInformation_32, 2);
        }

        private void TraverseFileMatch(FileMatchItem sender)
        {
            Log.Information(Path.Combine(sender.Folder, sender.FileName));
        }

        /// <summary>
        /// Provides what to find via include array and what to exclude
        /// using the exclude array
        /// </summary>
        private async void ExecuteButton_Click(object sender, EventArgs e)
        {
            string path = DirectoryHelper.SolutionFolder();

            string[] include = { "**/*.cs" };
            string[] exclude =
            {
                "**/*Assembly*.cs",
                "**/*Designer*.cs",
                "**/*.g.i.cs",
                "**/*.g.cs",
                "**/TemporaryGeneratedFile*.cs"
            };

            Log.Information("starting");

            await GlobbingOperations.IncludeExclude(path, include, exclude);
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            var path = "C:\\OED\\WebApps\\IDTheftReporting"; // .vscode"
            

            string[] include = { "**/*.cfm", "**/*.json" };
            string[] exclude =
            {
                "**/*.vscode"
            };

            await GlobbingOperations.IncludeExclude(path, include, exclude);
        }
    }
}
