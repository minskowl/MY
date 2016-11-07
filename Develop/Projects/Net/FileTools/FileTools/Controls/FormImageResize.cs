using System;
using System.IO;
using System.Windows.Forms;

namespace FileTools.Controls
{
    public partial class FormImageResize : Form
    {
        public int ImageWidth { get { return int.Parse(boxWidth.Text); } }
        public int ImageHeight { get { return int.Parse(boxHeight.Text); } }

        private FileInfo[] files;
        /// <summary>
        /// Gets the selected files.
        /// </summary>
        /// <value>The selected files.</value>
        public FileInfo[] SelectedFiles
        {
            get { return files; }
        }

        public FormImageResize()
        {
            InitializeComponent();
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            files = fileSelector1.GetFiles();
            if (files != null)
            {
                DialogResult = System.Windows.Forms.DialogResult.OK;
                Close();
            }
        }
    }
}
