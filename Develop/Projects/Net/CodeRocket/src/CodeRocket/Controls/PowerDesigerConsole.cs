using Savchin.Controls.Browsers;
using Savchin.Forms.Docking;


namespace CodeRocket.Controls
{
    partial class PowerDesigerConsole : DockContent
    {

        /// <summary>
        /// Gets or sets the selected object.
        /// </summary>
        /// <value>The selected object.</value>
        public PDBrowser Browser
        {
            get { return pdBrowser1; }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PropertyConsole"/> class.
        /// </summary>
        public PowerDesigerConsole()
        {
            InitializeComponent();

            HideOnClose = true;
            ShowHint = DockState.DockLeft;
            TabText = "PowerDesigner";
        }

     


    }
}