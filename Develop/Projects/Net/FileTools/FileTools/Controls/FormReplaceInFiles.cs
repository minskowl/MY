using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FileTools.Controls
{
    public partial class FormReplaceInFiles : Form
    {
        /// <summary>
        /// Gets the search text.
        /// </summary>
        /// <value>The search text.</value>
        public string SearchText
        {
            get { return textBoxSearch.Text; }
            set { textBoxSearch.Text = value; }
        }
        /// <summary>
        /// Gets or sets the replace text.
        /// </summary>
        /// <value>The replace text.</value>
        public string ReplaceText
        {
            get { return textBoxReplace.Text; }
            set { textBoxReplace.Text = value; }
        }
        /// <summary>
        /// Gets a value indicating whether [use reg exp].
        /// </summary>
        /// <value><c>true</c> if [use reg exp]; otherwise, <c>false</c>.</value>
        public bool UseRegExp
        {
            get { return checkBoxUseRegExp.Checked; }
        }
        private FileInfo[] files;
        /// <summary>
        /// Gets the selected files.
        /// </summary>
        /// <value>The selected files.</value>
        public FileInfo[] SelectedFiles
        {
            get { return files; }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FormReplaceInFiles"/> class.
        /// </summary>
        public FormReplaceInFiles()
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
