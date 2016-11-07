using Savchin.Controls.Docking;
using Savchin.Data.Schema.Controls;


namespace SchemaEditor.Controls
{
    /// <summary>
    /// SchemaConsole
    /// </summary>
    partial class SchemaConsole : DockContent
    {

        /// <summary>
        /// Gets the browser.
        /// </summary>
        /// <value>The browser.</value>
        public SchemaBrowser Browser
        {
            get { return schemaBrowser1; }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PropertyConsole"/> class.
        /// </summary>
        public SchemaConsole()
        {
            InitializeComponent();

            HideOnClose = true;
            ShowHint = Savchin.Controls.Docking.DockState.DockLeft;
            Text = TabText = "Data Schema";


            schemaBrowser1.CheckBoxes = true;
        }




    }
}