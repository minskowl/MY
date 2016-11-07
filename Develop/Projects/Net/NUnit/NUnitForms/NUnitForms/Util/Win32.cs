#region Copyright (c) 2003-2005, Luke T. Maxon

/********************************************************************************************************************
'
' Copyright (c) 2003-2005, Luke T. Maxon
' All rights reserved.
' 
' Redistribution and use in source and binary forms, with or without modification, are permitted provided
' that the following conditions are met:
' 
' * Redistributions of source code must retain the above copyright notice, this list of conditions and the
' 	following disclaimer.
' 
' * Redistributions in binary form must reproduce the above copyright notice, this list of conditions and
' 	the following disclaimer in the documentation and/or other materials provided with the distribution.
' 
' * Neither the name of the author nor the names of its contributors may be used to endorse or 
' 	promote products derived from this software without specific prior written permission.
' 
' THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND ANY EXPRESS OR IMPLIED
' WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A
' PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT OWNER OR CONTRIBUTORS BE LIABLE FOR
' ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT
' LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS
' INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY,
' OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN
' IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
'
'*******************************************************************************************************************/

#endregion

using System;
using System.Runtime.InteropServices;
using System.Text;

namespace NUnit.Extensions.Forms
{
    internal class Win32
    {
        #region Delegates

        public delegate bool EnumDesktopsDelegate(string desktop, IntPtr lParam);

        public delegate bool EnumThreadDelegate(IntPtr hwnd, IntPtr lParam);

        #endregion

        internal const int BM_CLICK = 0x00F5; //Button
        public const int GENERIC_ALL = 0x10000000;
        internal const int INPUT_KEYBOARD = 1;
        internal const int INPUT_MOUSE = 0;
        internal const int KEYEVENTF_KEYUP = 0x0002;
        internal const int MOUSEEVENTF_ABSOLUTE = 0x8000;
        internal const int MOUSEEVENTF_LEFTDOWN = 0x0002;
        internal const int MOUSEEVENTF_LEFTUP = 0x0004;
        internal const int MOUSEEVENTF_MIDDLEDOWN = 0x0020;
        internal const int MOUSEEVENTF_MIDDLEUP = 0x0040;
        internal const int MOUSEEVENTF_MOVE = 0x0001;
        internal const int MOUSEEVENTF_RIGHTDOWN = 0x0008;
        internal const int MOUSEEVENTF_RIGHTUP = 0x0010;
        //internal const int MOUSEEVENTF_WHEEL = 0x80;
        internal const int MOUSEEVENTF_WHEEL = 0x0800;
        //internal const int MOUSEEVENTF_XDOWN = 0x100;
        internal const int MOUSEEVENTF_XDOWN = 0x0080;
        internal const int MOUSEEVENTF_XUP = 0x0100;
        internal const int SPI_GETMOUSEHOVERTIME = 102;
        internal const short VK_CONTROL = 0x11;
        internal const short VK_MENU = 0x12;
        internal const short VK_SHIFT = 0x10;
        internal const int WHEEL_DELTA = 120;
        private const UInt32 WM_CLOSE = 0x0010;
        internal const int XBUTTON1 = 0x1;
        //internal const int XBUTTON1 = 8388608;
        internal const int XBUTTON2 = 0x2; //16777216

        public const uint WM_KEYDOWN = 0x0100;
        public const uint WM_KEYUP = 0x0101;
        public const uint WM_SYSKEYDOWN = 0x104;
        public const uint WM_SYSKEYUP = 0x105;





    
        [DllImport("user32.Dll")]
        internal static extern IntPtr SendDlgItemMessage(IntPtr handleToWindow, int dlgItem, uint message,
                                                         UIntPtr wParam, IntPtr lParam);

        [DllImport("user32.dll")]
        internal static extern int GetCursorPos(out Point lpWinPoint);


        [DllImport("user32.dll")]
        internal static extern int SetCursorPos(int x, int y);



        [DllImport("user32.dll", EntryPoint = "SendInput", SetLastError = true)]
        internal static extern int SendMouseInput(int cInputs, ref MOUSEINPUT pInputs, int cbSize);

        [DllImport("user32.dll", EntryPoint = "SendInput", SetLastError = true)]
        internal static extern int SendKeyboardInput(int cInputs, ref KEYBDINPUT pInputs, int cbSize);

        [DllImport("user32.dll")]
        internal static extern IntPtr GetMessageExtraInfo();



        internal const int KEYEVENTF_KEYDOWN = 0x0000;






        [DllImport("user32.dll", SetLastError = true)]
        internal static extern bool SetDlgItemText(IntPtr hDlg, int nIDDlgItem, string lpString);

        [DllImport("user32.dll")]
        internal static extern int GetDlgItemText(IntPtr hDlg, int nIDDlgItem, StringBuilder lpString, int maxCount);





        #region Nested type: CBTCallback

        internal delegate IntPtr CBTCallback(int code, IntPtr wParam, IntPtr lParam);

        #endregion



        #region Nested type: KEYBDINPUT

        /// <summary>
        /// Another example can be found on http://www.pinvoke.net/default.aspx/user32/SendInput.html. Notice
        /// that KEYBDINPUT on pinvoke.net is split in two structs : INPUT and KEYBDINPUT. The KEYBDINPUT that is
        /// used here, contains both structs of pinvoke.net.
        /// </summary>
        [StructLayout(LayoutKind.Explicit, Size = 28)]
        internal struct KEYBDINPUT
        {
            [FieldOffset(16)]
            internal IntPtr dwExtraInfo;
            [FieldOffset(8)]
            internal int dwFlags;

            [FieldOffset(12)]
            internal int time;
            [FieldOffset(0)]
            internal int type;
            [FieldOffset(6)]
            internal short wScan;
            [FieldOffset(4)]
            internal short wVk;
        }

        #endregion

        #region Nested type: MOUSEINPUT

        internal struct MOUSEINPUT
        {
            internal int dwExtraInfo;
            internal int dwFlags;
            internal int dx;

            internal int dy;

            internal int mouseData;

            internal int time;
            internal int type;

            ///<summary>
            /// 
            /// </summary>
            /// <remarks>
            ///dx
            ///Specifies the absolute position of the mouse, or the amount of motion since the last mouse event was generated, depending on the value of the dwFlags member. Absolute data is specified as the x coordinate of the mouse; relative data is specified as the number of pixels moved. 
            ///dy
            ///Specifies the absolute position of the mouse, or the amount of motion since the last mouse event was generated, depending on the value of the dwFlags member. Absolute data is specified as the y coordinate of the mouse; relative data is specified as the number of pixels moved. 
            ///mouseData
            ///If dwFlags contains MOUSEEVENTF_WHEEL, then mouseData specifies the amount of wheel movement. A positive value indicates that the wheel was rotated forward, away from the user; a negative value indicates that the wheel was rotated backward, toward the user. One wheel click is defined as WHEEL_DELTA, which is 120. 
            ///Windows 2000/XP: IfdwFlags does not contain MOUSEEVENTF_WHEEL, MOUSEEVENTF_XDOWN, or MOUSEEVENTF_XUP, then mouseData should be zero. 
            ///
            ///If dwFlags contains MOUSEEVENTF_XDOWN or MOUSEEVENTF_XUP, then mouseData specifies which X buttons were pressed or released. This value may be any combination of the following flags. 
            ///
            ///XBUTTON1
            ///Set if the first X button is pressed or released.
            ///XBUTTON2
            ///Set if the second X button is pressed or released.
            ///dwFlags
            ///A set of bit flags that specify various aspects of mouse motion and button clicks. The bits in this member can be any reasonable combination of the following values. 
            ///The bit flags that specify mouse button status are set to indicate changes in status, not ongoing conditions. For example, if the left mouse button is pressed and held down, MOUSEEVENTF_LEFTDOWN is set when the left button is first pressed, but not for subsequent motions. Similarly, MOUSEEVENTF_LEFTUP is set only when the button is first released. 
            ///
            ///You cannot specify both the MOUSEEVENTF_WHEEL flag and either MOUSEEVENTF_XDOWN or MOUSEEVENTF_XUP flags simultaneously in the dwFlags parameter, because they both require use of the mouseData field. 
            ///
            ///MOUSEEVENTF_ABSOLUTE
            ///Specifies that the dx and dy members contain normalized absolute coordinates. If the flag is not set, dxand dy contain relative data (the change in position since the last reported position). This flag can be set, or not set, regardless of what kind of mouse or other pointing device, if any, is connected to the system. For further information about relative mouse motion, see the following Remarks section.
            ///MOUSEEVENTF_MOVE
            ///Specifies that movement occurred.
            ///MOUSEEVENTF_LEFTDOWN
            ///Specifies that the left button was pressed.
            ///MOUSEEVENTF_LEFTUP
            ///Specifies that the left button was released.
            ///MOUSEEVENTF_RIGHTDOWN
            ///Specifies that the right button was pressed.
            ///MOUSEEVENTF_RIGHTUP
            ///Specifies that the right button was released.
            ///MOUSEEVENTF_MIDDLEDOWN
            ///Specifies that the middle button was pressed.
            ///MOUSEEVENTF_MIDDLEUP
            ///Specifies that the middle button was released.
            ///MOUSEEVENTF_VIRTUALDESK
            ///Windows 2000/XP:Maps coordinates to the entire desktop. Must be used with MOUSEEVENTF_ABSOLUTE.
            ///MOUSEEVENTF_WHEEL
            ///Windows NT/2000/XP: Specifies that the wheel was moved, if the mouse has a wheel. The amount of movement is specified in mouseDataÃ‚Â .
            ///MOUSEEVENTF_XDOWN
            ///Windows 2000/XP: Specifies that an X button was pressed.
            ///MOUSEEVENTF_XUP
            ///Windows 2000/XP: Specifies that an X button was released.
            ///time
            ///Time stamp for the event, in milliseconds. If this parameter is 0, the system will provide its own time stamp. 
            ///dwExtraInfo
            ///</remarks>
            public MOUSEINPUT(int mouseEvent)
            {
                type = INPUT_MOUSE;
                dx = 0;
                dy = 0;
                mouseData = 0;
                dwFlags = mouseEvent;
                time = 0;
                dwExtraInfo = 0;
            }
        } ;

        #endregion

        #region Nested type: Point

        internal struct Point
        {
            internal int x;

            internal int y;

            public Point(int x, int y)
            {
                this.x = x;
                this.y = y;
            }
        }

        #endregion



        #region Nested type: WindowMessages

        internal enum WindowMessages : uint
        {
            WM_CLOSE = 0x0010,
            WM_COMMAND = 0x0111
        }

        #endregion

        [DllImport("user32.dll")]
        public static extern short VkKeyScanEx(char ch, IntPtr dwhkl);

        [DllImport("user32.dll")]
        public static extern IntPtr GetKeyboardLayout(int idThread);


        [DllImport("user32.dll")]
        public static extern uint MapVirtualKeyEx(uint uCode, uint uMapType, IntPtr dwhkl);


        [DllImport("user32.dll")]
        public static extern void keybd_event(byte bVk, byte bScan, uint dwFlags, UIntPtr dwExtraInfo);



        [DllImport("user32.dll")]
        public static extern bool PostThreadMessage(uint idThread, uint Msg, UIntPtr wParam, IntPtr lParam);





    }
}
