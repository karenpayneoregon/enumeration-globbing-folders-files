using System.Diagnostics;
using System.Text;
using DirectoryHelpersLibrary.Classes;
using HeadFootSearchingStyles.Classes;
using static HeadFootSearchingStyles.Classes.Dialogs;
using Directory = System.IO.Directory;

namespace HeadFootSearchingStyles;

public partial class MainWindow : Form
{

    public MainWindow()
    {
        InitializeComponent();

        DirectoryOperations1.UnauthorizedAccessEvent += DirectoryOperations1UnauthorizedAccess;
        Shown += OnShown!;

        var fileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Folders.txt");

        /*
         * By default, there are no folder names in the ListBox,
         * you need to populate the ListBox with one or more existing folders
         */

        if (File.Exists(fileName))
        {
            FolderListBox.DataSource = File.ReadAllLines(fileName).ToList();
            FolderListBox.SelectedIndex = 0;
        }
        else
        {
            SelectedButton.Enabled = false;
        }
            
    }

    private void DirectoryOperations1UnauthorizedAccess(string message)
    {
        // TODO - decide how to present to user or log to a file etc.
    }

    /// <summary>
    /// Setup events for search results, check all items in the CheckedListBox
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void OnShown(object sender, EventArgs e)
    {
            
        GlobbingOperations.Traverse += GlobbingOperationsOnTraverseHandler;
        GlobbingOperations.NoMatches += NoMatches;
        GlobbingOperations.Done += Done;

        for (int index = 0; index < ExtensionsCheckedListBox.Items.Count; index++)
        {
            ExtensionsCheckedListBox.SetItemChecked(index, true);
        }
    }

    /// <summary>
    /// After last file has been added size ListView columns,
    /// select the ListView as active control.
    /// </summary>
    /// <param name="message"></param>
    private void Done(string message)
    {
        ResultsView.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
        ResultsView.Items[0].Selected = true;
        ResultsView.EnsureVisible(0);
        ActiveControl = ResultsView;
    }

    private void NoMatches()
    {
        Alert(this,"No matches for current selections","Close");
    }

    /// <summary>
    /// Get files by CheckedListBox selections
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void GetFilesRecursiveButton_Click(object sender, EventArgs e)
    {
            
        ResultsView.Items.Clear();

        if (Directory.Exists(FolderListBox.Text))
        {
            var items = ExtensionsCheckedListBox.CheckedList();

            if (items.Count >0)
            {
                /*
                 * Create pattern to search folder and sub-folders selected in the CheckedListBox
                 */
                string[] includes = items.Select(pattern => $"**/*{pattern}").ToArray();

                /*
                 * Type of files for InMemoryDirectoryInfo
                 */
                string[] fileExtensions = { "css" };

                GlobbingOperations.FindItems(
                    FolderListBox.Text, 
                    includes, 
                    fileExtensions);

            }
        }
        else
        {
            Alert(this, $"{FolderListBox.Text}\nnot found");
        }
    }

    /// <summary>
    /// Example to get all .png and .ico files in a folder and sub-folders
    /// </summary>
    private void GetImagesButton_Click(object sender, EventArgs e)
    {
        /*
         * Replace with an existing folder
         */
        string folder = @"C:\Users\paynek\Documents\Snagit";

        if (!Directory.Exists(folder))
        {
            Alert(this, $"{folder}\nnot found");
            return;
        }

        /*
         * find all these extensions in folder and sub-folders
         */
        string[] patterns = { "**/*.png", "**/*.ico" };

        var list = GlobbingOperations.Synchronous(folder, patterns);
        StringBuilder builder = new();
        foreach (var item in list)
        {
            builder.AppendLine(item.ToString());
        }

        var fileName = "results.txt";

        File.WriteAllText(fileName, builder.ToString());

        OpenAlert(this, 
            $"Results have been saved to {fileName}", 
            "OK", 
            Path.Combine(AppDomain.CurrentDomain.BaseDirectory, fileName));

    }
    private void GlobbingOperationsOnTraverseHandler(string sender)
    {
        var folder = Path.GetDirectoryName(sender);

        ResultsView.Items.Add(string.IsNullOrWhiteSpace(folder) ?
            new ListViewItem(new[] { "", sender }) :
            new ListViewItem(new[] { Path.GetDirectoryName(sender), Path.GetFileName(sender) }));

        ResultsView.Items[^1].Tag = sender;
    }

    private void SelectedButton_Click(object sender, EventArgs e)
    {
        if (ResultsView.Items.Count >0)
        {
            InformationalMessage(Convert.ToString(ResultsView.Items.SelectedRows()[0].Tag)!);
        }
    }

    private void OnEmptyFolderFound(string folder)
    {
        Debug.WriteLine(folder);
    }

}