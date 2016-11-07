using Savchin.CodeGeneration;
using Savchin.Forms.Docking;


namespace CodeRocket.Controls
{
    partial class ProjectConsole : DockContent
    {

        /// <summary>
        /// Gets the browser.
        /// </summary>
        /// <value>The browser.</value>
        public GeneratorProjectBrowser Browser
        {
            get { return generatorProjectBrowser1; }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PropertyConsole"/> class.
        /// </summary>
        public ProjectConsole()
        {
            InitializeComponent();

          
            HideOnClose = true;
            ShowHint = DockState.DockLeft;
            Text = TabText = "Project";
        }




    }
}