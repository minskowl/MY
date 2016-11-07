using System;
using Savchin.WinApi.Windows.FileDialogs;

namespace Savchin.WinApi.Windows.FileDialogs
{
    /// <summary>
    /// This class is used to test the built-in OpenFileDialog. This class is not meant to be
    /// used directly. Instead you should use the ExpectOpenFileDialog and CancelOpenFileDialog functions
    /// in the NUnitFormTest
    /// class.
    /// </summary>
    public class SaveFileDialog : FileDialog
    {
        /// <summary>
        /// Default constructor...
        /// </summary>
        public SaveFileDialog(IntPtr handle)
            : base(handle)
        {

        }

        /// <summary>
        /// Saves the file.
        /// </summary>
        /// <param name="file">The file.</param>
        public void SaveFile(string file)
        {
            SetFileName(file);
        }

        /// <summary>
        /// Saves the file.
        /// </summary>
        public void SaveFile()
        {
            ClickOpenSaveButton();
        }
    }
}