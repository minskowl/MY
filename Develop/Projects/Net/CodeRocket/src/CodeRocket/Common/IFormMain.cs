using CodeRocket.Controls;
using Savchin.CodeGeneration;
using Savchin.Data.Schema.Controls;
using Savchin.Forms.Docking;

namespace CodeRocket.Common
{
    interface IFormMain
    {

        /// <summary>
        /// Gets the schema browser.
        /// </summary>
        /// <value>The schema browser.</value>
        SchemaBrowser SchemaBrowser { get; }

        /// <summary>
        /// Gets the error viewer.
        /// </summary>
        /// <value>The error viewer.</value>
        ErrorViewer ErrorViewer { get; }

        /// <summary>
        /// Gets the dock panel.
        /// </summary>
        /// <value>The dock panel.</value>
        DockPanel DockPanel { get; }

        /// <summary>
        /// Gets the project browser.
        /// </summary>
        /// <value>The project browser.</value>
        GeneratorProjectBrowser ProjectBrowser { get; }
    }
}
