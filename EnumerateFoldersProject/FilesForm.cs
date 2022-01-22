using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DirectoryHelpersLibrary.Models;

namespace EnumerateFoldersProject
{
    public partial class FilesForm : Form
    {
        public FilesForm()
        {
            InitializeComponent();
        }

        public FilesForm(List<string> files)
        {
            InitializeComponent();

            listBox1.DataSource = files;
        }

        public FilesForm(List<FileItem> files)
        {
            InitializeComponent();

            listBox1.DataSource = files;
        }
    }
}
