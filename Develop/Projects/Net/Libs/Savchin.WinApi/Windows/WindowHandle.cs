

using System;
using System.Text;

namespace Savchin.WinApi.Windows
{
    ///<summary>
    /// Additional methods for working with Window Handles.
    ///</summary>
    public static class WindowHandle
    {
        private const string DialogClass = "#32770";

        ///<summary>
        /// Returns true if the given window handle references a Dialog.
        ///</summary>
        ///<param name="handle"></param>
        ///<returns></returns>
        public static bool IsDialog(IntPtr handle)
        {
            return GetClassName(handle) == DialogClass;
        }

        ///<summary>
        /// Returns the text of the references control.
        ///</summary>
        ///<param name="handle">A window handle to a control.</param>
        ///<returns>The text of the control.</returns>
        public static string GetText(IntPtr handle)
        {
            IntPtr handleToDialogText = User32.GetDlgItem(handle, 0xFFFF);
            return User32.GetWindowText(handleToDialogText);
        }

        ///<summary>
        /// Gets the caption of the referenced window.
        ///</summary>
        public static string GetCaption(IntPtr handle)
        {
            var buffer = new StringBuilder(255);
            User32.GetWindowText(handle, buffer, 255);
            return buffer.ToString();
        }

        ///<summary>
        /// Returns the window class name for the referenced window.
        ///</summary>
        public static string GetClassName(IntPtr handle)
        {
            var className = new StringBuilder(255);
            User32.GetClassName(handle, className, 255);
            return className.ToString();
        }

        /// <summary>
        /// Gets the parent.
        /// </summary>
        /// <param name="handle">The handle.</param>
        /// <returns></returns>
        public static IntPtr GetParent(IntPtr handle)
        {
            return User32.GetParent(handle);
        }
    }
}