using System;
using System.Windows.Forms;
using KnowledgeBase.KbTools.Core;
using KnowledgeBase.KbTools.Export;

namespace KnowledgeBase.KbTools.Controls
{
    public partial class ExportToHtmlControl : BaseControl
    {
        BaseToHtmlBuilder builder = new BaseToHtmlBuilder();

        /// <summary>
        /// Initializes a new instance of the <see cref="ExportToHtmlControl"/> class.
        /// </summary>
        public ExportToHtmlControl()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Handles the Click event of the button control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void button_Click(object sender, EventArgs e)
        {
           builder.Build(
               textBoxDestintionFolder.Text.Trim(),
               textBoxUrlBase.Text.Trim());
            MessageBox.Show(AppCore.MainForm, "Exported succesful");

        }


        /// <summary>
        /// Handles the Click event of the buttonBrowseDestintionFolder control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void buttonBrowseDestintionFolder_Click(object sender, EventArgs e)
        {
            folderBrowserDialog1.SelectedPath = textBoxDestintionFolder.Text;
            if (folderBrowserDialog1.ShowDialog(AppCore.MainForm) == DialogResult.OK)
                textBoxDestintionFolder.Text = folderBrowserDialog1.SelectedPath;
        }
    }
}
