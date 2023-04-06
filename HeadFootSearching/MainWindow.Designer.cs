
namespace HeadFootSearchingStyles
{
    partial class MainWindow
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainWindow));
            this.GetFilesRecursiveButton = new System.Windows.Forms.Button();
            this.ResultsView = new System.Windows.Forms.ListView();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
            this.ExtensionsCheckedListBox = new System.Windows.Forms.CheckedListBox();
            this.FolderListBox = new System.Windows.Forms.ListBox();
            this.GetImagesButton = new System.Windows.Forms.Button();
            this.SelectedButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // GetFilesRecursiveButton
            // 
            this.GetFilesRecursiveButton.Image = global::HeadFootSearchingStyles.Properties.Resources.Search;
            this.GetFilesRecursiveButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.GetFilesRecursiveButton.Location = new System.Drawing.Point(16, 19);
            this.GetFilesRecursiveButton.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.GetFilesRecursiveButton.Name = "GetFilesRecursiveButton";
            this.GetFilesRecursiveButton.Size = new System.Drawing.Size(154, 36);
            this.GetFilesRecursiveButton.TabIndex = 0;
            this.GetFilesRecursiveButton.Text = "Search";
            this.GetFilesRecursiveButton.UseVisualStyleBackColor = true;
            this.GetFilesRecursiveButton.Click += new System.EventHandler(this.GetFilesRecursiveButton_Click);
            // 
            // ResultsView
            // 
            this.ResultsView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ResultsView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2});
            this.ResultsView.FullRowSelect = true;
            this.ResultsView.Location = new System.Drawing.Point(0, 307);
            this.ResultsView.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.ResultsView.MultiSelect = false;
            this.ResultsView.Name = "ResultsView";
            this.ResultsView.Size = new System.Drawing.Size(655, 409);
            this.ResultsView.TabIndex = 1;
            this.ResultsView.UseCompatibleStateImageBehavior = false;
            this.ResultsView.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Folder";
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "File name";
            // 
            // ExtensionsCheckedListBox
            // 
            this.ExtensionsCheckedListBox.CheckOnClick = true;
            this.ExtensionsCheckedListBox.FormattingEnabled = true;
            this.ExtensionsCheckedListBox.Items.AddRange(new object[] {
            "boot*.css",
            "kendo*.css",
            "agency.css"});
            this.ExtensionsCheckedListBox.Location = new System.Drawing.Point(176, 19);
            this.ExtensionsCheckedListBox.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.ExtensionsCheckedListBox.Name = "ExtensionsCheckedListBox";
            this.ExtensionsCheckedListBox.Size = new System.Drawing.Size(158, 70);
            this.ExtensionsCheckedListBox.TabIndex = 3;
            // 
            // FolderListBox
            // 
            this.FolderListBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.FolderListBox.FormattingEnabled = true;
            this.FolderListBox.ItemHeight = 20;
            this.FolderListBox.Location = new System.Drawing.Point(14, 113);
            this.FolderListBox.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.FolderListBox.Name = "FolderListBox";
            this.FolderListBox.Size = new System.Drawing.Size(627, 124);
            this.FolderListBox.TabIndex = 5;
            // 
            // GetImagesButton
            // 
            this.GetImagesButton.Image = global::HeadFootSearchingStyles.Properties.Resources.Search;
            this.GetImagesButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.GetImagesButton.Location = new System.Drawing.Point(493, 19);
            this.GetImagesButton.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.GetImagesButton.Name = "GetImagesButton";
            this.GetImagesButton.Size = new System.Drawing.Size(154, 36);
            this.GetImagesButton.TabIndex = 6;
            this.GetImagesButton.Text = ".png/.ico";
            this.GetImagesButton.UseVisualStyleBackColor = true;
            this.GetImagesButton.Click += new System.EventHandler(this.GetImagesButton_Click);
            // 
            // SelectedButton
            // 
            this.SelectedButton.Image = global::HeadFootSearchingStyles.Properties.Resources.SelectRow;
            this.SelectedButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.SelectedButton.Location = new System.Drawing.Point(14, 257);
            this.SelectedButton.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.SelectedButton.Name = "SelectedButton";
            this.SelectedButton.Size = new System.Drawing.Size(137, 36);
            this.SelectedButton.TabIndex = 7;
            this.SelectedButton.Text = "Selected";
            this.SelectedButton.UseVisualStyleBackColor = true;
            this.SelectedButton.Click += new System.EventHandler(this.SelectedButton_Click);
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(657, 717);
            this.Controls.Add(this.SelectedButton);
            this.Controls.Add(this.GetImagesButton);
            this.Controls.Add(this.FolderListBox);
            this.Controls.Add(this.ExtensionsCheckedListBox);
            this.Controls.Add(this.ResultsView);
            this.Controls.Add(this.GetFilesRecursiveButton);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.Name = "MainWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Globbing/Enumerate/PowerShell samples";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button GetFilesRecursiveButton;
        private System.Windows.Forms.ListView ResultsView;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.CheckedListBox ExtensionsCheckedListBox;
        private System.Windows.Forms.ListBox FolderListBox;
        private System.Windows.Forms.Button GetImagesButton;
        private System.Windows.Forms.Button SelectedButton;
    }
}