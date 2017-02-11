using System;
using System.IO;
using System.Windows.Forms;

namespace FileTools.Controls
{
    public partial class FormFileSelectorWithResult : Form
    {
        private FileInfo[] files;

        public string FileFilter
        {
            get { return fileSelector1.FileFilter; }
            set { fileSelector1.FileFilter = value; }
        }

        public string FileResult
        {
            get { return textBoxResultFile.Text; }
            set { textBoxResultFile.Text = value; }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FormFileSelector"/> class.
        /// </summary>
        public FormFileSelectorWithResult()
        {
            InitializeComponent();
        }



        /// <summary>
        /// Gets the files.
        /// </summary>
        /// <returns></returns>
        public FileInfo[] GetFiles()
        {
            return files;
        }

        /// <summary>
        /// Handles the Click event of the buttonOK control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void buttonOK_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(FileResult))
                return;

            files = fileSelector1.GetFiles();
            if (files != null)
            {
                DialogResult = System.Windows.Forms.DialogResult.OK;
                Close();
            }

        }


    }
}
