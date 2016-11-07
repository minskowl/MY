using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BotvaSpider.Controls
{
    public partial class FormContainer : Form
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FormContainer"/> class.
        /// </summary>
        public FormContainer()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Creates the specified control.
        /// </summary>
        /// <param name="control">The control.</param>
        /// <param name="caption">The caption.</param>
        /// <returns></returns>
        public static FormContainer Create(Control control, string caption)
        {
            var result = new FormContainer();
            result.Text = caption;
            control.Dock = DockStyle.Fill;
            result.tableLayoutPanel1.Controls.Add(control, 0, 0);
            return result;
        }

        /// <summary>
        /// Handles the Click event of the buttonClose control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void buttonClose_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
