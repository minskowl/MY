using System;
using System.Linq;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using Savchin.Text;

namespace FileTools.Controls
{
    public partial class FileSelector : UserControl
    {

        /// <summary>
        /// Gets the folder.
        /// </summary>
        /// <value>The folder.</value>
        public string Folder
        {
            get { return textBoxFolder.Text.Trim(); }
        }
        /// <summary>
        /// Gets the file filter.
        /// </summary>
        /// <value>The file filter.</value>
        public string FileFilter
        {
            get
            {
                var text = textBoxFileFilter.Text.Trim();
                return (string.IsNullOrEmpty(text)) ? "*" : text;
            }
        }

        /// <summary>
        /// Gets the file exclude filter.
        /// </summary>
        /// <value>The file exclude filter.</value>
        public string FileExcludeFilter
        {
            get
            {
                var text = textBoxExcludeFilter.Text.Trim();
                return (string.IsNullOrWhiteSpace(text)) ? null : text;
            }
        }
        /// <summary>
        /// Gets a value indicating whether [include sub dirictories].
        /// </summary>
        /// <value>
        /// 	<c>true</c> if [include sub dirictories]; otherwise, <c>false</c>.
        /// </value>
        public bool IncludeSubDirictories
        {
            get { return checkBoxIncludeSubDir.Checked; }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FileSelector"/> class.
        /// </summary>
        public FileSelector()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Gets the files.
        /// </summary>
        /// <returns></returns>
        public FileInfo[] GetFiles()
        {
            var result = new List<FileInfo>();
            foreach (string folder in listFolders.Items)
            {
                result.AddRange(GetFiles(folder));
            }
            if (IsValidFolder()) result.AddRange(GetFiles(Folder));
            return result.ToArray();
        }

        #region Event Handlers

        /// <summary>
        /// Handles the Click event of the buttonBrowse control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void buttonBrowse_Click(object sender, EventArgs e)
        {
            folderBrowserDialog1.SelectedPath = textBoxFolder.Text.Trim();

            if (folderBrowserDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                textBoxFolder.Text = folderBrowserDialog1.SelectedPath;
        }

        /// <summary>
        /// Handles the Click event of the buttonAdd control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void buttonAdd_Click(object sender, EventArgs e)
        {
            if (!IsValidFolder()) return;
            listFolders.Items.Add(Folder);
        } 

        #endregion

        private FileInfo[] GetFiles(string folder)
        {
            try
            {
                var result= new DirectoryInfo(folder).GetFiles(FileFilter,
                    IncludeSubDirictories ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly);

                var exlude = FileExcludeFilter;
                if (exlude!=null)
                {
                    var wildcard = new Wildcard(exlude);
                    return result.Where(e => !wildcard.IsMatch(Path.GetFileName(e.FullName))).ToArray();
                }

                return result;
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Invalid params. " + ex.Message);
                textBoxFileFilter.SelectAll();
                textBoxFileFilter.Focus();
                return new FileInfo[0];
            }
        }

        private bool IsValidFolder()
        {
            if (string.IsNullOrEmpty(Folder))
            {
                MessageBox.Show(this, "Please select directory.");
                textBoxFolder.Focus();
                return false;
            }

            if (!Directory.Exists(Folder))
            {
                MessageBox.Show(this, "Please select correct directory.");
                textBoxFolder.SelectAll();
                textBoxFolder.Focus();
                return false;
            }
            return true;
        }

    }
}
