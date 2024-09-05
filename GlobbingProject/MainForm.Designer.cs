
namespace GlobbingProject
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            ExecuteButton = new Button();
            button1 = new Button();
            SuspendLayout();
            // 
            // ExecuteButton
            // 
            ExecuteButton.Location = new Point(161, 62);
            ExecuteButton.Margin = new Padding(3, 4, 3, 4);
            ExecuteButton.Name = "ExecuteButton";
            ExecuteButton.Size = new Size(117, 31);
            ExecuteButton.TabIndex = 1;
            ExecuteButton.Text = "Execute";
            ExecuteButton.UseVisualStyleBackColor = true;
            ExecuteButton.Click += ExecuteButton_Click;
            // 
            // button1
            // 
            button1.Location = new Point(161, 109);
            button1.Name = "button1";
            button1.Size = new Size(94, 29);
            button1.TabIndex = 2;
            button1.Text = "button1";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(439, 180);
            Controls.Add(button1);
            Controls.Add(ExecuteButton);
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            Icon = (Icon)resources.GetObject("$this.Icon");
            Margin = new Padding(3, 4, 3, 4);
            Name = "MainForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Globbing";
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Button ExecuteButton;
        private Button button1;
    }
}

