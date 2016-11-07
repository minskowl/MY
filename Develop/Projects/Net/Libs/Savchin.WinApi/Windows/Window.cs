using System;
using System.Windows.Forms;

namespace Savchin.WinApi.Windows
{
    /// <summary>
    /// Window Class
    /// </summary>
    public class Window
    {
        #region Properties
        protected readonly IntPtr handle = new IntPtr(0);
        public IntPtr Handle
        {
            get { return handle; }
        }
        private string name;
        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <value>The name.</value>
        public string Name
        {
            get
            {
                if (name == null)
                    name = WindowHandle.GetCaption(handle);

                return name;
            }
        }
        private string text;
        /// <summary>
        /// Gets the text.
        /// </summary>
        /// <value>The text.</value>
        public string Text
        {
            get
            {
                if (text == null)
                    text = WindowHandle.GetText(handle);

                return text;
            }
        }

        /// <summary>
        /// Gets a value indicating whether this instance is dialog.
        /// </summary>
        /// <value><c>true</c> if this instance is dialog; otherwise, <c>false</c>.</value>
        public bool IsDialog
        {
            get
            {
                return WindowHandle.IsDialog(handle);
            }
        }
             
        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="Window"/> class.
        /// </summary>
        /// <param name="handle">The handle.</param>
        public Window(IntPtr handle)
        {
            this.handle = handle;
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="Window"/> class.
        /// </summary>
        public Window()
        {
            
        }

        /// <summary>
        /// Flashes the window.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="count">The count.</param>
        /// <param name="timeout">The timeout.</param>
        /// <returns></returns>
        public bool FlashWindow(FLASHW type, int count, int timeout)
        {
            return User32.FlashWindow(handle, type, count, timeout);
        }
        /// <summary>
        /// Stops the flash window.
        /// </summary>
        /// <returns></returns>
        public bool StopFlashWindow()
        {
            return User32.StopFlashWindow(handle);
        }

        /// <summary>
        /// Returns a <see cref="T:System.String"/> that represents the current <see cref="T:System.Object"/>.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.String"/> that represents the current <see cref="T:System.Object"/>.
        /// </returns>
        public override string ToString()
        {
            return string.IsNullOrEmpty(Name) ? "Window" : "Window " + Name;
        }

        /// <summary>
        /// Toes the control.
        /// </summary>
        /// <returns></returns>
        public Control ToControl()
        {
            return Control.FromHandle(handle);
        }
    }
}