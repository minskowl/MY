using System;
using Savchin.WinApi.Windows;

namespace Savchin.EventSpy.Consoles
{
    public partial class FormsWindow : ToolWindow
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FormsWindow"/> class.
        /// </summary>
        public FormsWindow()
        {
            InitializeComponent();

            HideOnClose = true;

            Name = "PropertyWindow";
            TabText = "Forms";
            ShowHint = Forms.Docking.DockState.DockRight;

            FillFormsList();
        }

        private void FillFormsList()
        {
            foreach (var form in WindowManager.GetAllWindows())
            {
                listBox1.Items.Add(form);
            }
        }

        /// <summary>
        /// Handles the Click event of the toolStripButton1 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            FillFormsList();
        }


    }
}
