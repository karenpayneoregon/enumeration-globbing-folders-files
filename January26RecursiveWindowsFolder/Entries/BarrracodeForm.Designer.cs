namespace January26RecursiveWindowsFolder.Entries;

partial class BarrracodeForm
{
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }
        base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
        ExecuteButton = new Button();
        CancelButton = new Button();
        SuspendLayout();
        // 
        // ExecuteButton
        // 
        ExecuteButton.Location = new Point(12, 35);
        ExecuteButton.Name = "ExecuteButton";
        ExecuteButton.Size = new Size(258, 29);
        ExecuteButton.TabIndex = 0;
        ExecuteButton.Text = "Execute";
        ExecuteButton.UseVisualStyleBackColor = true;
        ExecuteButton.Click += ExecuteButton_Click;
        // 
        // CancelButton
        // 
        CancelButton.Location = new Point(12, 84);
        CancelButton.Name = "CancelButton";
        CancelButton.Size = new Size(258, 29);
        CancelButton.TabIndex = 1;
        CancelButton.Text = "Cancel";
        CancelButton.UseVisualStyleBackColor = true;
        CancelButton.Click += CancelButton_Click;
        // 
        // BarrracodeForm
        // 
        AutoScaleDimensions = new SizeF(8F, 20F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(282, 166);
        Controls.Add(CancelButton);
        Controls.Add(ExecuteButton);
        FormBorderStyle = FormBorderStyle.FixedToolWindow;
        Name = "BarrracodeForm";
        StartPosition = FormStartPosition.CenterScreen;
        Text = "Barrracode";
        ResumeLayout(false);
    }

    #endregion

    private Button ExecuteButton;
    private Button CancelButton;
}