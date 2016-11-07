using System;
using System.Collections.Generic;
using System.Text;

namespace Savchin.WinApi.Windows
{
    /// <summary>
    /// WinButton
    /// </summary>
    public class WinButton
    {
        private IntPtr hWnd;

        /// <summary>
        /// Initializes a new instance of the <see cref="WinButton"/> class.
        /// </summary>
        /// <param name="Hwnd">The HWND.</param>
        public WinButton(IntPtr Hwnd)
        {
            hWnd = Hwnd;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="WinButton"/> class.
        /// </summary>
        /// <param name="buttonid">The buttonid.</param>
        /// <param name="parentHwnd">The parent HWND.</param>
        public WinButton(int buttonid, IntPtr parentHwnd)
        {
            hWnd = User32.GetDlgItem(parentHwnd, buttonid);
        }

        /// <summary>
        /// Clicks this instance.
        /// </summary>
        public void Click()
        {
            if (Exists())
            {
                User32.SendMessage(hWnd, (uint)WM.WM_ACTIVATE, (int)MA.MA_ACTIVATE, 0);
                User32.SendMessage(hWnd, Win32.BM_CLICK, 0, 0);
            }
        }

        public bool Exists()
        {
            return User32.IsWindow(hWnd);
        }

        public string Title
        {
            get { return User32.GetWindowText(hWnd); }
        }

        public bool Enabled
        {
            get { return User32.IsWindowEnabled(hWnd); }
        }

        public bool Visible
        {
            get { return User32.IsWindowVisible(hWnd); }
        }
    }
}