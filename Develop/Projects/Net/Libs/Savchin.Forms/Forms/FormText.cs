using System;
using System.Windows.Forms;

namespace Savchin.Forms
{
    public partial class FormText : Form
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FormText"/> class.
        /// </summary>
        public FormText()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Gets or sets the search.
        /// </summary>
        /// <value>The search.</value>
        public string Value
        {
            get
            {
                return txtText.Text;
            }
            set
            {
                txtText.Text = value;
            }
        }

        /// <summary>
        /// Handles the Click event of the cmdOK control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void cmdOK_Click(object sender, EventArgs e)
        {
            txtText.Text = txtText.Text.Trim();
            if (txtText.Text.Length == 0)
            {
                return;
            }
            DialogResult = DialogResult.OK;
            Close();
        }

        /// <summary>
        /// Handles the Click event of the cmdCancel control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void cmdCancel_Click(object sender, EventArgs e)
        {
            DialogResult=DialogResult.Cancel;
            Close();
        }
    }
}