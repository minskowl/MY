using System.Windows.Forms;
using Savchin.WinApi;

namespace Savchin.Forms.Helpers
{
    /// <summary>
    /// FormHelper
    /// </summary>
    public static class FormHelper
    {

        /// <summary>
        /// Flashes the window.
        /// </summary>
        /// <param name="form">The form.</param>
        /// <param name="type">The type.</param>
        /// <param name="count">The count.</param>
        /// <param name="timeout">The timeout.</param>
        /// <returns></returns>
        public static bool Flash(this  Form form ,FLASHW type, int count, int timeout)
        {
            return User32.FlashWindow(form.Handle, type, count, timeout);
        }
        /// <summary>
        /// Stops the flash window.
        /// </summary>
        /// <param name="form">The form.</param>
        /// <returns></returns>
        public static bool StopFlash(this  Form form)
        {
            return User32.StopFlashWindow(form.Handle);
        }
    }
}