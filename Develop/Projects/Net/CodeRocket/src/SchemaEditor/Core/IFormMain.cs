using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Savchin.Controls.Docking;
using Savchin.Data.Schema.Controls;
using SchemaEditor.Controls;

namespace SchemaEditor.Core
{
    interface IFormMain: IWin32Window
    {
        /// <summary>
        /// Gets the schema browser.
        /// </summary>
        /// <value>The schema browser.</value>
        SchemaBrowser SchemaBrowser { get; }

        /// <summary>
        /// Gets the exception viewer.
        /// </summary>
        /// <value>The exception viewer.</value>
        ExceptionViewer ExceptionViewer { get; }
        /// <summary>
        /// Gets the dock panel.
        /// </summary>
        /// <value>The dock panel.</value>
        DockPanel DockPanel { get; }


        /// <summary>
        /// Closes this instance.
        /// </summary>
        void Close();

        /// <summary>
        /// Adds the control.
        /// </summary>
        /// <param name="control">The control.</param>
        void AddControl(Control control);
    }
}
