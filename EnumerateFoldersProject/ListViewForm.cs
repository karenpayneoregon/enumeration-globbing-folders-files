using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DirectoryHelpersLibrary.Classes;
using DirectoryHelpersLibrary.Models;
using static DirectoryHelpersLibrary.Classes.DirectoryOperations2;
using static WindowsFormsLibrary.LanguageExtensions.ControlHelper;

namespace EnumerateFoldersProject
{
    public partial class ListViewForm : Form
    {
        public ListViewForm()
        {
            InitializeComponent();

            TraverseFolder += OnTraverseFolder;
            Done += OnDone;
            listView1.MouseDoubleClick += ListView1OnMouseDoubleClick;
            listView1.KeyDown += ListView1OnKeyDown;
            
        }

        private void ListView1OnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ShowFiles();
            }
        }

        private void ListView1OnMouseDoubleClick(object sender, MouseEventArgs e)
        {
            ShowFiles();
        }

        private void ShowFiles()
        {
            if (listView1.SelectedItems.Count != 1) return;
            var current = (FolderItem)listView1.SelectedItems[0].Tag;
            var list = GetFilesSync(current.Name, "*.*");

            if (list.Count > 0)
            {
                FilesForm filesForm = new FilesForm(list);

                try
                {
                    filesForm.ShowDialog();
                }
                finally
                {
                    filesForm.Dispose();
                }
            }
        }

        private void OnDone()
        {

            ControlInvoke(listView1, () =>
            {
                listView1.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
                listView1.FocusedItem = listView1.Items[0];
                listView1.Items[0].Selected = true;
            });

        }

        /// <summary>
        /// Rather than add all items, filter out a couple to show what's possible
        /// </summary>
        /// <param name="sender"></param>
        private void OnTraverseFolder(FolderItem sender)
        {

            if (!sender.Name.Contains(".git") && !sender.Name.Contains("obj"))
            {

                ControlInvoke(listView1, () =>
                    listView1.Items.Add(new ListViewItem(sender.ItemArray)
                    {
                        Tag = sender
                    }));
            }

        }

        private async void ExecuteButton_Click(object sender, EventArgs e)
        {

            if (listView1.Items.Count >0)
            {
                listView1.Items.Clear();
                await Task.Delay(250);
            }
            
            var path = DirectoryHelper.SolutionFolder();
            await Task.Run(async () => await CollectFolders(path));

            ActiveControl = listView1;

        }

    }
}
