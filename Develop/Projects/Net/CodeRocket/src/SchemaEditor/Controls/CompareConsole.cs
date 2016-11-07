using Savchin.Controls.Common.Comparer;
using Savchin.Controls.Docking;

namespace SchemaEditor.Controls
{
    public partial class CompareConsole : DockContent
    {
        /// <summary>
        /// Gets the view.
        /// </summary>
        /// <value>The view.</value>
        public CompareView View
        {
            get { return compareView1;}
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="CompareConsole"/> class.
        /// </summary>
        public CompareConsole()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Handles the OkClick event of the compareView1 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void compareView1_OkClick(object sender, System.EventArgs e)
        {
            Close();
        }

        /// <summary>
        /// Handles the CancelClick event of the compareView1 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void compareView1_CancelClick(object sender, System.EventArgs e)
        {
            Close();
        }
    }
}
