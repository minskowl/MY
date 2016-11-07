using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using FileTools.Commands;
using FileTools.Controls;
using FileTools.Core;
using FileTools.Properties;
using Savchin.Forms.Core.Commands;
using Savchin.Text;

using Savchin.Core;

namespace FileTools
{


    public partial class FormMain : Form, ILog
    {


        /// <summary>
        /// Initializes a new instance of the <see cref="FormMain"/> class.
        /// </summary>
        public FormMain()
        {
            InitializeComponent();

            this.Icon = Resources.Logo2;
            Text = AppInfo.ProductLabel;

            rusCheckToolStripMenuItem.BindCommand(new TraslitDirectoryCommand(this));
            getFilesStatisticsToolStripMenuItem.BindCommand(new FileStatisticsCommand(this));
            getFileListToolStripMenuItem.BindCommand(new DirectoryListCommand(this));
            replaceTextToolStripMenuItem.BindCommand(new ReplaceTextCommand(this));
            renameFilesToolStripMenuItem.BindCommand(new RenameFilesCommand(this));
            findNameDuplicatesToolStripMenuItem.BindCommand(new FindNameDuplicatesCommand(this));
            checkExistsToolStripMenuItem.BindCommand(new DllCheckExistenceCommand(this));
            resizeToolStripMenuItem.BindCommand(new ResizeImageCommand(this));
           lockFileMenuItem.BindCommand(new LockFileCommand(this));
        }


        /// <summary>
        /// Adds the log.
        /// </summary>
        /// <param name="measage">The measage.</param>
        public void AddLog(string measage)
        {
            textBoxLog.AppendText(measage + Environment.NewLine);
        }

        private void manageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = new FormGacManager();
            form.Show();
        }

        private void mathConverterToolStripMenuItem_Click(object sender, EventArgs e)
        {
             new FormMathConvertor().Show();
        }




    }
}
