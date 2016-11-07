/*----------------------------------------------------------------------------------------

	A-Soft Ingenieurbüro

	Copyright © 1994 - 2007. All Rights reserved.

	Related Copyrights :

			Microsoft .NET Windows Forms V2.0 library.
			Copyright (C) 2004...2006 Microsoft Corporation,
			All rights reserved.


	FILE		:	NativeMethods.cs

	PROJECT		:	A-Soft Library
	SUB			:	Standard Library
 
	SYSTEM		:	Windows-XP, (Windows 2000), C# (.NET 2.0, Visual Studio.NET 2005)

	AUTHOR		:	Joachim Holzhauer
	
	DESCRIPTION	:	Map some definitions from WIN32 native interface
 
	VERSION		:	1.0 - 2006.01.31

----------------------------------------------------------------------------------------*/

using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;

namespace NUnit.Extensions.Forms
{
    /// <summary>
    /// Helper class which maps some WIN32 values
    /// </summary>
    [SuppressUnmanagedCodeSecurity()]
    [ComVisible(false)]
    internal sealed class NativeMethods
    {
        private NativeMethods()
        {
        }

        #region Constants

        internal const int WM_SYSCOMMAND = 0x0112;
        internal const int WM_COMMAND = 0x0111;
        internal const int WM_CLOSE = 0x0010;
        internal const int SC_CLOSE = 0xF060;

        internal const int KEYEVENTF_EXTENDEDKEY = 0x1;
        internal const int KEYEVENTF_KEYUP = 0x2;
        internal const int KEYEVENTF_TAB = 0x09;

        public const Int32 SMTO_ABORTIFHUNG = 2;
        internal const int BM_CLICK = 245;
        internal const int WM_ACTIVATE = 6;
        internal const int MA_ACTIVATE = 1;

        public const int GW_CHILD = 5;
        public const int GW_HWNDNEXT = 2;

        #endregion Constants

        #region RECT structure

        [StructLayout(LayoutKind.Sequential)]
        internal struct RECT
        {
            public int bottom;
            public int left;

            public int right;
            public int top;

            public RECT(int left, int top, int right, int bottom)
            {
                this.left = left;
                this.top = top;
                this.right = right;
                this.bottom = bottom;
            }

            public Rectangle Rect
            {
                get { return new Rectangle(left, top, right - left, bottom - top); }
            }

            public static RECT FromXYWH(int x, int y, int width, int height)
            {
                return new RECT(x, y, x + width, y + height);
            }

            public static RECT FromRectangle(Rectangle rect)
            {
                return new RECT(rect.Left, rect.Top, rect.Right, rect.Bottom);
            }
        }

        #endregion RECT structure


        // TODO Not sure this declartion is safe under 64bit windows, see comments from pinvoke.net http://pinvoke.net/default.aspx/user32.SendMessage
        // Qoute: NEVER use "int" or "integer" as lParam. Your code WILL crash on 64-bit windows. ONLY use IntPtr, a "ref" structure, or an "out" structure.        
        /// <summary>
        /// Sends the message.
        /// </summary>
        /// <param name="hWnd">The h WND.</param>
        /// <param name="msg">The MSG.</param>
        /// <param name="wParam">The w param.</param>
        /// <param name="lParam">The l param.</param>
        /// <returns></returns>
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        internal static extern int SendMessage(IntPtr hWnd, int msg, int wParam, int lParam);

        /// <summary>
        /// Sends the message.
        /// </summary>
        /// <param name="hWnd">The h WND.</param>
        /// <param name="Msg">The MSG.</param>
        /// <param name="wParam">The w param.</param>
        /// <param name="lParam">The l param.</param>
        /// <returns></returns>
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = false)]
        internal static extern IntPtr SendMessage(HandleRef hWnd, uint Msg, IntPtr wParam, IntPtr lParam);

        /// <summary>
        /// Gets the DLG item.
        /// </summary>
        /// <param name="handleToWindow">The handle to window.</param>
        /// <param name="ControlId">The control id.</param>
        /// <returns></returns>
        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        internal static extern IntPtr GetDlgItem(IntPtr handleToWindow, int ControlId);
        /// <summary>
        /// Get a windows rectangle in a RECT structure
        /// </summary>
        /// <param name="hwnd">The window handle to look up</param>
        /// <param name="rect">The rectangle</param>
        /// <returns></returns>
        [DllImport("user32.dll", ExactSpelling = true, CharSet = CharSet.Auto)]
        public static extern bool GetWindowRect(IntPtr hwnd, out RECT rect);

        /// <summary>
        /// The BringWindowToTop function brings the specified window to the top of the Z order. 
        /// If the window is a top-level window, it is activated. 
        /// If the window is a child window, the top-level parent window associated 
        /// with the child window is activated. 
        /// </summary>
        /// <param name="hWnd">Handle to the window to bring to the top of the Z order. </param>
        /// <returns>If the function succeeds, the return value is nonzero.
        /// If the function fails, the return value is zero. 
        /// To get extended error information, call GetLastError. 
        /// </returns>
        [DllImport("user32.dll", ExactSpelling = true, CharSet = CharSet.Auto)]
        public static extern bool BringWindowToTop(IntPtr hWnd);

        		/// <summary>
		/// The GetForegroundWindow function returns a handle to the foreground window.
		/// </summary>
		[DllImport("user32.dll", SetLastError=true)]
		internal static extern IntPtr GetForegroundWindow();

		[DllImport("user32.dll", ExactSpelling=true, CharSet=CharSet.Auto)]
		public static extern IntPtr GetWindow(IntPtr hWnd, int uCmd);

		[DllImport("user32.dll")]
		internal static extern bool PrintWindow(IntPtr hwnd, IntPtr hdcBlt, uint nFlags);

		[DllImport("user32.dll", SetLastError=true, CharSet=CharSet.Auto)]
		internal static extern int GetWindowText(IntPtr handleToWindow, StringBuilder windowText, int maxTextLength);

		[DllImport("user32.dll", SetLastError=true, CharSet=CharSet.Auto)]
		internal static extern int GetWindowTextLength(IntPtr hWnd);




        /// <summary>
        /// Get a windows rectangle in a .NET structure
        /// </summary>
        /// <param name="hwnd">The window handle to look up</param>
        /// <returns>The rectangle</returns>
        public static Rectangle GetWindowRect(IntPtr hwnd)
        {
            RECT rect = new RECT();
            GetWindowRect(hwnd, out rect);
            return rect.Rect;
        }

        /// <summary>
        /// This method incapsulates all the details of getting
        /// the full length text in a StringBuffer and returns
        /// the StringBuffer contents as a string.
        /// </summary>
        /// <param name="hwnd">The handle to the window</param>
        /// <returns>Text of the window</returns>
        internal static string GetWindowText(IntPtr hwnd)
        {
            int length = GetWindowTextLength(hwnd) + 1;
            StringBuilder buffer = new StringBuilder(length);
            GetWindowText(hwnd, buffer, length);

            return buffer.ToString();
        }
    }
}