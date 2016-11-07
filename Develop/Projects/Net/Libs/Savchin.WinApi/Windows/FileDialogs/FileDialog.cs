
using System;
using System.Text;
using System.Threading;
using System.Windows.Forms;


namespace Savchin.WinApi.Windows.FileDialogs
{
    ///<summary>
    /// A form tester for the <see cref="FileDialog"/>.
    ///</summary>
    public class FileDialog : Window
    {

        #region Private/Protected attributes.

        /// <summary>
        /// Control ID for the Cancel button.
        /// </summary>
        protected const int CancelButton = 2;

        /// <summary>
        /// Control ID for the file name checkbox.
        /// </summary>
        protected const int FileNameCheckBox = 1148;

        /// <summary>
        /// Control ID for the Open or Save button.
        /// </summary>
        protected const int OpenButton = 1;





        /// <summary>
        /// Name/title of the OpenFileDialog 
        /// </summary>
        protected string name = "Open";

        protected IntPtr wParam;

        #endregion



        /// <summary>
        /// Initializes a new instance of the <see cref="FileDialog"/> class.
        /// </summary>
        /// <param name="handle">The handle.</param>
        public FileDialog(IntPtr handle)
            : base(handle)
        {

        }


        /// <summary>
        /// Gets the name of the by.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        public static FileDialog GetByName(string name)
        {
            IntPtr handle = new WindowFinder().FindFirstDialog(name).Handle;
            if (handle == IntPtr.Zero)
                return null;
            return new FileDialog(handle);
        }

        /// <summary>
        /// Simulates a click on  cancel.
        /// For some reason we need to spawn a new thread because the FileDialog Caption
        /// will not change to correct name if we just posts the message.
        /// If we Calls the ClickCancelHandler directly we need to set the title
        /// of the FileDialog to "Open". (Strange)
        /// </summary>
        public virtual void ClickCancel()
        {
            IntPtr cancel_btn = User32.GetDlgItem(handle, CancelButton);
            User32.PostMessage(cancel_btn, Win32.BM_CLICK, (IntPtr)0, IntPtr.Zero);
        }


        /// <summary>
        /// Click the first button, usually "Open" or "Save".
        /// </summary>
        public void ClickOpenSaveButton()
        {
            IntPtr open_btn = User32.GetDlgItem(handle, OpenButton);
            User32.PostMessage(open_btn, Win32.BM_CLICK, (IntPtr)0, IntPtr.Zero);
        }




        /// <summary>
        /// Sets the name of the file.
        /// </summary>
        public void SetFileName(string fileName)
        {
            var setFileName = new StringBuilder(fileName.Length);

            int timeout = 1000000;

            while (!User32.IsWindowVisible(handle) || timeout == 0)
            {
                --timeout;
            }

            while (setFileName.ToString() != fileName)
            {
                User32.SetDlgItemText(handle, FileNameCheckBox, fileName);
                User32.GetDlgItemText(handle, FileNameCheckBox, setFileName, fileName.Length + 1);
            }

            var open_btn = User32.GetDlgItem(handle, OpenButton);
            User32.PostMessage(open_btn, Win32.BM_CLICK, (IntPtr)0, IntPtr.Zero);
        }


    }
}