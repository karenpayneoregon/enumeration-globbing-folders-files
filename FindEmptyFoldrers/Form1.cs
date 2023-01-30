using FindEmptyFolderExample.Classes;

namespace FindEmptyFolderExample;

public partial class Form1 : Form
{
    public Form1()
    {
        InitializeComponent();
        FolderOperations.EmptyFolderFound += FolderOperations_EmptyFolderFound;
        FolderOperations.Done += FolderOperationsOnDone;
        FolderOperations.FindEmptyFolders(DirectoryHelper.SolutionFolder());

    }

    private void FolderOperationsOnDone()
    {
        if (listBox1.Items.Count > 0)
        {
            listBox1.SelectedIndex = listBox1.Items.Count -1;
        }
    }

    private void FolderOperations_EmptyFolderFound(string folder)
    {
        listBox1.Items.Add(folder);
    }
}
