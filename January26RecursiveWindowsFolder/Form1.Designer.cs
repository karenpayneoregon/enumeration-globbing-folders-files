namespace January26RecursiveWindowsFolder;

partial class Form1
{
    /// <summary>
    ///  Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    ///  Clean up any resources being used.
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
    ///  Required method for Designer support - do not modify
    ///  the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
        panel1 = new Panel();
        CancelButton = new Button();
        StartButton = new Button();
        listBox1 = new ListBox();
        panel1.SuspendLayout();
        SuspendLayout();
        // 
        // panel1
        // 
        panel1.Controls.Add(CancelButton);
        panel1.Controls.Add(StartButton);
        panel1.Dock = DockStyle.Bottom;
        panel1.Location = new Point(0, 381);
        panel1.Name = "panel1";
        panel1.Size = new Size(800, 69);
        panel1.TabIndex = 0;
        // 
        // CancelButton
        // 
        CancelButton.Image = Properties.Resources.ASX_Cancel_blue_16x;
        CancelButton.ImageAlign = ContentAlignment.MiddleLeft;
        CancelButton.Location = new Point(179, 17);
        CancelButton.Name = "CancelButton";
        CancelButton.Size = new Size(128, 29);
        CancelButton.TabIndex = 2;
        CancelButton.Text = "Cancel";
        CancelButton.UseVisualStyleBackColor = true;
        CancelButton.Click += CancelButton_Click;
        // 
        // StartButton
        // 
        StartButton.Image = Properties.Resources.ASX_Run_blue_16x;
        StartButton.ImageAlign = ContentAlignment.MiddleLeft;
        StartButton.Location = new Point(31, 17);
        StartButton.Name = "StartButton";
        StartButton.Size = new Size(128, 29);
        StartButton.TabIndex = 1;
        StartButton.Text = "Start";
        StartButton.UseVisualStyleBackColor = true;
        StartButton.Click += StartButton_Click;
        // 
        // listBox1
        // 
        listBox1.Dock = DockStyle.Fill;
        listBox1.FormattingEnabled = true;
        listBox1.Location = new Point(0, 0);
        listBox1.Name = "listBox1";
        listBox1.Size = new Size(800, 381);
        listBox1.TabIndex = 1;
        // 
        // Form1
        // 
        AutoScaleDimensions = new SizeF(8F, 20F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(800, 450);
        Controls.Add(listBox1);
        Controls.Add(panel1);
        Name = "Form1";
        StartPosition = FormStartPosition.CenterScreen;
        Text = "Traverse C:\\Windows";
        panel1.ResumeLayout(false);
        ResumeLayout(false);
    }

    #endregion

    private Panel panel1;
    private Button CancelButton;
    private Button StartButton;
    private ListBox listBox1;
}
