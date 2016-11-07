using System;
using System.ComponentModel;
using System.Runtime.InteropServices;

namespace Savchin.WinApi
{
    public class Win32
    {

        public const int BM_CLICK = 0x00F5; //Button
        public const int GENERIC_ALL = 0x10000000;
        public const int INPUT_KEYBOARD = 1;
        public const int INPUT_MOUSE = 0;
        public const int KEYEVENTF_KEYUP = 0x0002;
        public const int MOUSEEVENTF_ABSOLUTE = 0x8000;
        public const int MOUSEEVENTF_LEFTDOWN = 0x0002;
        public const int MOUSEEVENTF_LEFTUP = 0x0004;
        public const int MOUSEEVENTF_MIDDLEDOWN = 0x0020;
        public const int MOUSEEVENTF_MIDDLEUP = 0x0040;
        public const int MOUSEEVENTF_MOVE = 0x0001;
        public const int MOUSEEVENTF_RIGHTDOWN = 0x0008;
        public const int MOUSEEVENTF_RIGHTUP = 0x0010;
        //public const int MOUSEEVENTF_WHEEL = 0x80;
        public const int MOUSEEVENTF_WHEEL = 0x0800;
        //public const int MOUSEEVENTF_XDOWN = 0x100;
        public const int MOUSEEVENTF_XDOWN = 0x0080;
        public const int MOUSEEVENTF_XUP = 0x0100;
        public const int SPI_GETMOUSEHOVERTIME = 102;
        public const short VK_CONTROL = 0x11;
        public const short VK_MENU = 0x12;
        public const short VK_SHIFT = 0x10;
        public const int WHEEL_DELTA = 120;

        public const int XBUTTON1 = 0x1;
        //public const int XBUTTON1 = 8388608;
        public const int XBUTTON2 = 0x2; //16777216





        /// <summary>
        /// Gets the last win32 error.
        /// </summary>
        /// <returns></returns>
        public static Win32Exception GetLastWin32Error()
        {
            return new Win32Exception(Marshal.GetLastWin32Error());
        }



        public static bool EnumDesktopsCallback(string desktop, IntPtr lParam)
        {
            return lParam != IntPtr.Zero;
        }



        public static bool EnumWindowsProc(IntPtr hWnd, int lParam)
        {
            //StringBuilder title = new StringBuilder(255);
            //int titleLength = GetWindowText(hWnd, title, title.Capacity + 1);
            //title.Length = titleLength;

            return true;
        }


     

        public enum WindowMessages : uint
        {
            WM_CLOSE = 0x0010,
            WM_COMMAND = 0x0111
        }










    }
}
