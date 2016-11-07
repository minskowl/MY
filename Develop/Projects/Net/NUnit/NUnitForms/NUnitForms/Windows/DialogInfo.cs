using System;
using System.Collections.Generic;
using System.Text;
using Savchin.WinApi.Windows;

namespace NUnit.Extensions.Forms
{
    /// <summary>
    /// DialogInfo
    /// </summary>
    public class DialogInfo
    {
        #region Properties
      
        private readonly string name;
        private readonly IntPtr handle;

      
        /// <summary>
        /// Gets a value indicating whether this <see cref="DialogInfo"/> is name.
        /// </summary>
        /// <value><c>true</c> if name; otherwise, <c>false</c>.</value>
        public string Name
        {
            get { return name; }
        }
        /// <summary>
        /// Gets the handle.
        /// </summary>
        /// <value>The handle.</value>
        public IntPtr Handle
        {
            get { return handle; }
        } 
        #endregion
        /// <summary>
        /// Initializes a new instance of the <see cref="DialogInfo"/> class.
        /// </summary>
        /// <param name="hwnd">The HWND.</param>
        public DialogInfo(IntPtr hwnd)
        {
            handle = hwnd;
            name = WindowHandle.GetCaption(hwnd);
        }
    }
}
