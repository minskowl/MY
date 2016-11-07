using System;
using System.Windows.Forms;
using Savchin.Core;
using Savchin.SystemEnvironment;

namespace BotvaSpider.Controls
{
    /// <summary>
    /// FormErrorReport class
    /// </summary>
    public partial class FormErrorReport : Form
    {
        private ErrorReport report;

        /// <summary>
        /// Initializes a new instance of the <see cref="FormErrorReport"/> class.
        /// </summary>
        public FormErrorReport()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Handles the Click event of the btnCancel control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        /// <summary>
        /// Sends the report.
        /// </summary>
        /// <param name="ex">The ex.</param>
        /// <param name="title">The title.</param>
        /// <param name="version">The version.</param>
        public static void ShowErrorReport(Exception ex, string title, Version version)
        {
            using (var form = new FormErrorReport())
            {
                form.report = ErrorReport.CreateReport(ex, title, version);
                form.ShowDialog();
            }

        }



        private void btnSend_Click(object sender, EventArgs e)
        {
            using (var sfd = new SaveFileDialog())
            {
                sfd.FileName = "Error.rep";
                sfd.CheckPathExists = true;
                sfd.AddExtension = true;
                sfd.DefaultExt = ".rep";
                if (sfd.ShowDialog() != DialogResult.OK) return;
                using (var stream = sfd.OpenFile())
                {
                    report.Save(stream);
                    stream.Flush();
                    stream.Close();
                }

            }
            DialogResult = DialogResult.OK;
            Close();
        }

        /// <summary>
        /// Handles the Click event of the btnViewReport control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void btnViewReport_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxReport.Text))
                textBoxReport.Text = report.GetXml();

            if (textBoxReport.Visible)
            {
                textBoxReport.Visible = true;
                btnViewReport.Text = "View Report";
                WindowState = FormWindowState.Normal;
            }
            else
            {
                textBoxReport.Visible = true;
                btnViewReport.Text = "Hide Report";
                WindowState = FormWindowState.Maximized;
            }
        }


    }
}