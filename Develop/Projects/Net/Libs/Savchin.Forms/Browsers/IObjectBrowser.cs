using System;

namespace Savchin.Forms.Browsers
{
    public interface IObjectBrowser
    {
        /// <summary>
        /// Gets the selected object.
        /// </summary>
        /// <value>The selected object.</value>
        object SelectedObject { get;}
        /// <summary>
        /// Gets or sets a value indicating whether [check boxes].
        /// </summary>
        /// <value><c>true</c> if [check boxes]; otherwise, <c>false</c>.</value>
        bool CheckBoxes { get; set;}

        event EventHandler AfterSelect;

        /// <summary>
        /// Clears vbrowser view.
        /// </summary>
        void Clear();

        /// <summary>
        /// Opens the file.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        void OpenFile(string fileName);
    }
}
