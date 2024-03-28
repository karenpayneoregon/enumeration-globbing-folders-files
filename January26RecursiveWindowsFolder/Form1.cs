using System.Diagnostics;
using January26RecursiveWindowsFolder.Classes;

namespace January26RecursiveWindowsFolder;

public partial class Form1 : Form
{
    private CancellationTokenSource cancellationTokenSource = new();
    private Operations operations = new();
    private List<string> List = new();
    public Form1()
    {
        InitializeComponent();

        operations.Traverse += Operations_Traverse;
        operations.Done += Operations_Done;
        operations.Cancelled += Operations_Cancelled;
        Closing += Form1_Closing;
    }

    private void Form1_Closing(object? sender, System.ComponentModel.CancelEventArgs e)
    {
        cancellationTokenSource.Dispose();
    }

    private void Operations_Cancelled()
    {
        listBox1.InvokeIfRequired(x =>
        {
            x.Items.Add($"Operation cancelled at {DateTime.Now:HH:mm:ss}");
            x.SelectedIndex = x.Items.Count - 1;
        });
    }

    private void Operations_Done()
    {
        listBox1.InvokeIfRequired(x =>
        {
            x.Items.Add($"Operation completed at {DateTime.Now:HH:mm:ss}");
            x.SelectedIndex = x.Items.Count - 1;
        });
    }

    private void Operations_Traverse(string sender)
    {
        listBox1.InvokeIfRequired(x =>
        {
            x.Items.Add(sender);
            x.SelectedIndex = x.Items.Count - 1;
        });
    }

    private async void StartButton_Click(object sender, EventArgs e)
    {
        StartButton.Enabled = false;
        listBox1.Items.Clear();
        try
        {
            if (cancellationTokenSource.IsCancellationRequested)
            {
                cancellationTokenSource.Dispose();
                cancellationTokenSource = new CancellationTokenSource();
            }

            await operations.Example1Async("C:\\Windows", "*.dll", cancellationTokenSource.Token);
        }
        finally
        {
            StartButton.Enabled = true;
        }
    }

    private void CancelButton_Click(object sender, EventArgs e)
    {
        cancellationTokenSource.Cancel();
        StartButton.Enabled = true;
    }
}