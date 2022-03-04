using System;
using System.Windows.Forms;
using DirectoryHelpersLibrary.Classes;
using TraverseFoldersBackwardsApp.Classes;

namespace TraverseFoldersBackwardsApp
{
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();
            DirectoryOperations2.Traverse += OnTraverse;
            Shown += OnShown;
        }

        private void OnTraverse(string fileName)
        {
            ResultsListBox.Items.Add(fileName);
            ResultsListBox.SelectedIndex = ResultsListBox.Items.Count - 1;
        }

        private async void OnShown(object sender, EventArgs e)
        {
            
            var folder = 
                "C:\\OED\\Dotnetland\\VS2019\\GlobbingSolution\\" + 
                "TraverseFoldersBackwardsApp\\bin\\Debug\\net5.0-windows\\Stuff\\MoreStuff";

            await Operations.TraverseFolderBackwards(folder, "C:\\OED");

        }

    }
}
