using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BotvaSpider.Controls
{
    public partial class FormFileSelect : Form
    {
        /// <summary>
        /// Gets or sets the name of the file.
        /// </summary>
        /// <value>The name of the file.</value>
        public string FileName
        {
            get { return textBox1.Text; }
            set { textBox1.Text = value; }
        }

        public FormFileSelect()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Handles the Click event of the buttonBrowse control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void buttonBrowse_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
                textBox1.Text = openFileDialog1.FileName;
        }

        /// <summary>
        /// Creates the specified caption.
        /// </summary>
        /// <param name="caption">The caption.</param>
        /// <param name="description">The description.</param>
        /// <param name="fileName">Name of the file.</param>
        /// <returns></returns>
        public static FormFileSelect Create(string caption, string description, string fileName)
        {
            var result = new FormFileSelect();
            result.Text = caption;
            result.label1.Text = description;
            result.FileName = fileName;
            return result;
        }

        /// <summary>
        /// Handles the Click event of the buttonOK control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void buttonOK_Click(object sender, EventArgs e)
        {
            if (File.Exists(textBox1.Text))
            {
                DialogResult = DialogResult.OK;
                Close();
            }
            else
            {
                MessageBox.Show("Файл не выбран.");
            }
        }
    }
}
