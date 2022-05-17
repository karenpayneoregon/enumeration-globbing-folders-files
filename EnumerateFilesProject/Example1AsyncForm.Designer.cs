
namespace EnumerateFilesProject
{
    partial class Example1AsyncForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Example1AsyncForm));
            this.ExecuteButtonAsync = new System.Windows.Forms.Button();
            this.ResultsListBox = new System.Windows.Forms.ListBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.FileCountButton = new System.Windows.Forms.Button();
            this.WithActionButton = new System.Windows.Forms.Button();
            this.CollectButton = new System.Windows.Forms.Button();
            this.ExecuteButtonSync = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // ExecuteButtonAsync
            // 
            this.ExecuteButtonAsync.Location = new System.Drawing.Point(12, 8);
            this.ExecuteButtonAsync.Name = "ExecuteButtonAsync";
            this.ExecuteButtonAsync.Size = new System.Drawing.Size(119, 23);
            this.ExecuteButtonAsync.TabIndex = 0;
            this.ExecuteButtonAsync.Text = "Execute Async";
            this.ExecuteButtonAsync.UseVisualStyleBackColor = true;
            this.ExecuteButtonAsync.Click += new System.EventHandler(this.ExecuteButtonAsync_Click);
            // 
            // ResultsListBox
            // 
            this.ResultsListBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ResultsListBox.FormattingEnabled = true;
            this.ResultsListBox.ItemHeight = 15;
            this.ResultsListBox.Location = new System.Drawing.Point(0, 0);
            this.ResultsListBox.Name = "ResultsListBox";
            this.ResultsListBox.Size = new System.Drawing.Size(769, 191);
            this.ResultsListBox.TabIndex = 1;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.FileCountButton);
            this.panel1.Controls.Add(this.WithActionButton);
            this.panel1.Controls.Add(this.CollectButton);
            this.panel1.Controls.Add(this.ExecuteButtonSync);
            this.panel1.Controls.Add(this.ExecuteButtonAsync);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 150);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(769, 41);
            this.panel1.TabIndex = 2;
            // 
            // FileCountButton
            // 
            this.FileCountButton.Location = new System.Drawing.Point(566, 8);
            this.FileCountButton.Name = "FileCountButton";
            this.FileCountButton.Size = new System.Drawing.Size(119, 23);
            this.FileCountButton.TabIndex = 4;
            this.FileCountButton.Text = "File count";
            this.FileCountButton.UseVisualStyleBackColor = true;
            this.FileCountButton.Click += new System.EventHandler(this.FileCountButton_Click);
            // 
            // WithActionButton
            // 
            this.WithActionButton.Location = new System.Drawing.Point(422, 8);
            this.WithActionButton.Name = "WithActionButton";
            this.WithActionButton.Size = new System.Drawing.Size(119, 23);
            this.WithActionButton.TabIndex = 3;
            this.WithActionButton.Text = "Func";
            this.WithActionButton.UseVisualStyleBackColor = true;
            this.WithActionButton.Click += new System.EventHandler(this.WithActionButton_Click);
            // 
            // CollectButton
            // 
            this.CollectButton.Location = new System.Drawing.Point(285, 8);
            this.CollectButton.Name = "CollectButton";
            this.CollectButton.Size = new System.Drawing.Size(119, 23);
            this.CollectButton.TabIndex = 2;
            this.CollectButton.Text = "Collect";
            this.CollectButton.UseVisualStyleBackColor = true;
            this.CollectButton.Click += new System.EventHandler(this.CollectButton_Click);
            // 
            // ExecuteButtonSync
            // 
            this.ExecuteButtonSync.Location = new System.Drawing.Point(150, 8);
            this.ExecuteButtonSync.Name = "ExecuteButtonSync";
            this.ExecuteButtonSync.Size = new System.Drawing.Size(119, 23);
            this.ExecuteButtonSync.TabIndex = 1;
            this.ExecuteButtonSync.Text = "Execute Sync";
            this.ExecuteButtonSync.UseVisualStyleBackColor = true;
            this.ExecuteButtonSync.Click += new System.EventHandler(this.ExecuteButtonSync_Click);
            // 
            // Example1AsyncForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(769, 191);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.ResultsListBox);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Example1AsyncForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Example1 Async";
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button ExecuteButtonAsync;
        private System.Windows.Forms.ListBox ResultsListBox;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button ExecuteButtonSync;
        private System.Windows.Forms.Button CollectButton;
        private System.Windows.Forms.Button WithActionButton;
        private System.Windows.Forms.Button FileCountButton;
    }
}

