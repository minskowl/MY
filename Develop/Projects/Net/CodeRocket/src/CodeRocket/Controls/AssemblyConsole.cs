
using Savchin.Forms.Browsers;
using Savchin.Forms.Docking;


namespace CodeRocket.Controls
{
    partial class AssemblyConsole : DockContent
    {

        /// <summary>
        /// Gets the browser.
        /// </summary>
        /// <value>The browser.</value>
        public AssemblyBrowser Browser
        {
            get { return assemblyBrowser1; }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PropertyConsole"/> class.
        /// </summary>
        public AssemblyConsole()
        {
            InitializeComponent();

            assemblyBrowser1.CheckBoxes = true;
            HideOnClose = true;
            ShowHint =DockState.DockLeft;
            Text = TabText = "Assembly";
        }




    }
}