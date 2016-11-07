using System;

namespace Savchin.WinApi.Windows
{
    /// <summary>
    /// Available commands you can send to the MessageBox.
    /// </summary>
    /// <remarks>
    /// There are convenience methods for OK and Cancel, so you should not need 
    /// those.
    /// </remarks>
    public enum Command : int
    {
        /// <summary>
        /// Represents an OK button on a <see cref="System.Windows.Forms.MessageBox"/>.
        /// </summary>
        OK = 1,
        /// <summary>
        /// Represents a Cancel button on a <see cref="System.Windows.Forms.MessageBox"/>.
        /// </summary>
        Cancel = 2,
        /// <summary>
        /// Represents an Abort button on a <see cref="System.Windows.Forms.MessageBox"/>.
        /// </summary>
        Abort = 3,
        /// <summary>
        /// Represents a Retry button on a <see cref="System.Windows.Forms.MessageBox"/>.
        /// </summary>
        Retry = 4,
        /// <summary>
        /// Represents an Ignore button on a <see cref="System.Windows.Forms.MessageBox"/>.
        /// </summary>
        Ignore = 5,
        /// <summary>
        /// Represents a Yes button on a <see cref="System.Windows.Forms.MessageBox"/>.
        /// </summary>
        Yes = 6,
        /// <summary>
        /// Represents a No button on a <see cref="System.Windows.Forms.MessageBox"/>.
        /// </summary>
        No = 7,
        /// <summary>
        /// Represents a Close button on a <see cref="System.Windows.Forms.MessageBox"/>.
        /// </summary>
        Close = 8,
        /// <summary>
        /// Represents a Close button on a <see cref="System.Windows.Forms.MessageBox"/>.
        /// </summary>
        Help = 9
    }

    public class MessageBox : Window
    {
        private WinButton firstButton;
        /// <summary>
        /// Gets the first button.
        /// </summary>
        /// <value>The first button.</value>
        public WinButton FirstButton
        {
            get
            {
                if (firstButton == null && handle != IntPtr.Zero)
                {
                    firstButton = new WinButton(2, handle);
                }
                return firstButton;
            }
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="MessageBox"/> class.
        /// </summary>
        /// <param name="handle">The handle.</param>
        public MessageBox(IntPtr handle)
            : base(handle)
        {

        }

        /// <summary>
        /// Sends the commnad.
        /// </summary>
        /// <param name="command">The command.</param>
        public void SendCommnad(Command command)
        {
            User32.SendMessage(handle, (uint)WM.WM_ACTIVATE, (int)MA.MA_ACTIVATE, 0);
            User32.SendMessage(handle, (uint)WM.WM_COMMAND, (UIntPtr)((uint)command), IntPtr.Zero);
        }
    }
}