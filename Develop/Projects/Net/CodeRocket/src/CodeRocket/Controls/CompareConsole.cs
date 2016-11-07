using System;
using Savchin.Comparer;
using Savchin.Forms.Docking;

namespace CodeRocket.Controls
{
    public partial class CompareConsole : DockContent
    {
        public CompareConsole()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Handles the OkClick event of the compareView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void compareView_OkClick(object sender, EventArgs e)
        {
            Close();
        }

        private void compareView_CancelClick(object sender, EventArgs e)
        {
            Close();
        }

        /// <summary>
        /// Shows the results.
        /// </summary>
        /// <param name="result">The result.</param>
        public void ShowResults(ObjectResult result)
        {
            compareView.ShowCompareResult(result);
        }
    }
}
