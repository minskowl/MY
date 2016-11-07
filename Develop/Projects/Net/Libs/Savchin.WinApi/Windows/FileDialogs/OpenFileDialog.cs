using System;

namespace Savchin.WinApi.Windows.FileDialogs
{
    /// <summary>
    /// This class is used to test the built-in OpenFileDialog. This class is not meant to be
    /// used directly. Instead you should use the ExpectOpenFileDialog and CancelOpenFileDialog functions
    /// in the NUnitFormTest
    /// class.
    /// </summary>
    public class OpenFileDialog : FileDialog
    {
        /// <summary>
        /// Constructs a new OpenFileDialogTester with the given title.
        /// </summary>
        public OpenFileDialog(IntPtr handle)
            : base(handle)
        {

        }


        ///<summary>
        /// Opens the given file using this dialog, on a separate thread.
        ///</summary>
        ///<param name="file"></param>
        public void OpenFile(string file)
        {
            SetFileName(file);
        }
    }
}