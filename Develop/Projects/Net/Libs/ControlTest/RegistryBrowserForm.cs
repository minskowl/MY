using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Microsoft.Win32;
using Savchin.Core;


namespace ControlTest
{
    public partial class RegistryBrowserForm : Form
    {
        public RegistryBrowserForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Handles the Load event of the RegistryBrowserForm control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void RegistryBrowserForm_Load(object sender, EventArgs e)
        {
            //HKEY_LOCAL_MACHINE\SOFTWARE\NewWayMedia\WAM
            button1.ShowRootNode = false;
            button1.RootKey = RegistryHelper.OpenWithCreateKey(@"HKEY_LOCAL_MACHINE\SOFTWARE\Test");
        }
    }
}