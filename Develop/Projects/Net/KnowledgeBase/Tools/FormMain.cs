using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using KnowledgeBase.KbTools.Controls;
using KnowledgeBase.KbTools.Core;
using Savchin.Core;
using Savchin.Forms.Core.Commands;


namespace KnowledgeBase.KbTools
{
    public partial class FormMain : Form
    {
        private BaseControl activeControl;
        public FormMain()
        {
            InitializeComponent();
        }


        /// <summary>
        /// Handles the Click event of the toToolStripMenuItem control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void toToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var item = (ToolStripMenuItem)sender;
            var command = AppCore.Commands[(string)item.Tag];
            if (command != null)
            {
                command.Execute(null, sender);
            }
        }
        /// <summary>
        /// Shows the control.
        /// </summary>
        /// <param name="control">The control.</param>
        public void ShowControl(BaseControl control)
        {
            if (activeControl != null)
            {
                CloseControl(activeControl);
            }
            control.Close += new EventHandler(control_Close);
            control.Dock = DockStyle.Fill;
            panel1.Controls.Add(control);
            activeControl = control;

        }

        void control_Close(object sender, EventArgs e)
        {
            CloseControl((BaseControl)sender);
        }
        private void CloseControl(BaseControl control)
        {
            control.Close -= control_Close;
            panel1.Controls.Remove(control);
            control.Dispose();
        }
    }
}
