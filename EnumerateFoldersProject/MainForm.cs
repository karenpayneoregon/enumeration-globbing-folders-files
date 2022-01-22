using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DirectoryHelpersLibrary.Classes;
using DirectoryHelpersLibrary.Models;
using static DirectoryHelpersLibrary.Classes.DirectoryOperations2;

namespace EnumerateFoldersProject
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();

            TraverseFolder += OnTraverseFolder;
            Done += OnDone;
            DoneFiles += OnDoneFiles;
            Traverse += TraverseFiles;
        }

        /// <summary>
        /// Present files found under Classes/selected project folder
        /// </summary>
        private void OnDoneFiles()
        {
            if (_files.Count <= 0) return;
            var filesForm = new FilesForm(_files);
            try
            {
                filesForm.ShowDialog();
            }
            finally
            {
                filesForm.Dispose();
            }
        }

        private List<string> _files = new();
        /// <summary>
        /// Store files from selected Classes folder
        /// </summary>
        /// <param name="sender"></param>
        private void TraverseFiles(string sender)
        {
            _files.Add(sender);
        }

        /// <summary>
        /// Set found folders in the ListBox
        /// </summary>
        private void OnDone()
        {
            listBox1.DataSource = _classFolders;
        }

        private List<string> _classFolders = new ();
        /// <summary>
        /// Filter for folders named Classes
        /// </summary>
        /// <param name="sender"></param>
        private void OnTraverseFolder(FolderItem sender)
        {

            if (sender.Name.Contains("Classes"))
            {
                _classFolders.Add(sender.Name);
            }
        }

        /// <summary>
        /// Get folders under solution folder
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void ExecuteButton_Click(object sender, EventArgs e)
        {
            _classFolders = new List<string>();
            listBox1.DataSource = null;

            string path = DirectoryHelper.SolutionFolder();
            await CollectFolders(path);

        }
        /// <summary>
        /// Get files for current ListBox selection
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void GetFilesButton_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex <= -1) return;

            _files = new List<string>();
            await Example2Async(listBox1.Text, "*.*");
        }
    }
}
