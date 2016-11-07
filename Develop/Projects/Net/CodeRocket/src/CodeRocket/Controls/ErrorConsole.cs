using Savchin.Forms.Docking;


namespace CodeRocket.Controls
{
    partial class ErrorConsole : DockContent
    {

        /// <summary>
        /// Gets the browser.
        /// </summary>
        /// <value>The browser.</value>
        public ErrorViewer Browser
        {
            get { return errorViewer1; }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PropertyConsole"/> class.
        /// </summary>
        public ErrorConsole()
        {
            InitializeComponent();

            HideOnClose = true;
            ShowHint = DockState.DockBottom;
            Text=TabText = "Errors";
        }

     


    }
}