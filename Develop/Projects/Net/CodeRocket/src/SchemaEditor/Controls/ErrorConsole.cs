using Savchin.Controls.Browsers;
using Savchin.Controls.Docking;


namespace SchemaEditor.Controls
{
    partial class ErrorConsole : DockContent
    {

        /// <summary>
        /// Gets the browser.
        /// </summary>
        /// <value>The browser.</value>
        public ExceptionViewer Browser
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
            ShowHint = Savchin.Controls.Docking.DockState.DockBottom;
            Text=TabText = "Errors";
        }

     


    }
}