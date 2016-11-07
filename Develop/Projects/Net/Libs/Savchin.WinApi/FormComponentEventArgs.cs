using System;
using System.Windows.Forms;

namespace Savchin.WinApi
{
    /// <summary>
    /// FormComponentEventHandler
    /// </summary>
    public delegate void FormComponentEventHandler(object sender, FormComponentEventArgs e);

    /// <summary>
    /// FormComponentEventArgs
    /// </summary>
    public class FormComponentEventArgs : EventArgs
    {
        private readonly Form form;
        /// <summary>
        /// Gets the component.
        /// </summary>
        /// <value>The component.</value>
        public Form Form
        {
            get { return form; }
        }
        private readonly object component;
        /// <summary>
        /// Gets the component.
        /// </summary>
        /// <value>The component.</value>
        public object Component
        {
            get { return component; }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FormComponentEventArgs"/> class.
        /// </summary>
        /// <param name="form">The form.</param>
        /// <param name="component">The component.</param>
        public FormComponentEventArgs(Form form, object component)
        {
            this.component = component;
            this.form = form;
        }


    }
}