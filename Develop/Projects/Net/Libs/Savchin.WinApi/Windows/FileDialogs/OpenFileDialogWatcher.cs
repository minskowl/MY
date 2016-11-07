using System;
using System.Collections.Generic;
using System.Text;

namespace Savchin.WinApi.Windows.FileDialogs
{
    public class OpenFileDialogWatcher : FileDialogWatcher
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OpenFileDialogWatcher"/> class.
        /// </summary>
        /// <param name="title">The title.</param>
        public OpenFileDialogWatcher(string title)
            : base(title)
        {
        }

        /// <summary>
        /// Opens the file.
        /// </summary>
        /// <param name="filename">The filename.</param>
        public void OpenFile(string filename)
        {
            SetFileName(filename);
        }
    }
}