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
    public partial class FormFileSelector : Form
    {
        private FileInfo[] files;


        /// <summary>
        /// Initializes a new instance of the <see cref="FormFileSelector"/> class.
        /// </summary>
        public FormFileSelector()
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
            files = fileSelector1.GetFiles();
            if (files != null)
            {
                DialogResult = System.Windows.Forms.DialogResult.OK;
                Close();
            }

        }

    
    }
}
