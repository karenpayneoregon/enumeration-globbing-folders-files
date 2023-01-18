using Serilog;
using Serilog.Core;
using DirectoryHelpersLibrary.Classes;
using DirectoryHelpersLibrary.Models;
using Microsoft.Extensions.Configuration;
using WindowsFormsLibrary.Classes;
using Serilog.Events;

namespace GlobbingProject
{
    public partial class MainForm : Form
    {
        private static Logger Logger;
        public MainForm()
        {
            InitializeComponent();

            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", true, true)
                .Build();

            Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(configuration)
                .CreateLogger();

            GlobbingOperations.TraverseFileMatch += TraverseFileMatch;
            GlobbingOperations.Done += Done;

            
        }

        private void Done(string message)
        {
            Logger.Write(LogEventLevel.Information, "Done\n");
            Dialogs.AutoCloseDialog(this, message, Properties.Resources.blueInformation_32, 2);
        }

        private void TraverseFileMatch(FileMatchItem sender)
        {
            Logger.Write(LogEventLevel.Information, Path.Combine(sender.Folder, sender.FileName));
        }

        /// <summary>
        /// Provides what to find via include array and what to exclude
        /// using the exclude array
        /// </summary>
        private async void ExecuteButton_Click(object sender, EventArgs e)
        {
            string path =  DirectoryHelper.SolutionFolder();
            //string path = "C:\\OED\\DotnetLand\\VS2019";

            string[] include = { "**/*.cs" };
            string[] exclude =
            {
                "**/*Assembly*.cs", 
                "**/*Designer*.cs", 
                "**/*.g.i.cs", 
                "**/*.g.cs", 
                "**/TemporaryGeneratedFile*.cs"
            };

            Logger.Write(LogEventLevel.Information, "starting");

            await GlobbingOperations.Asynchronous(path, include, exclude);
        }
    }
}
