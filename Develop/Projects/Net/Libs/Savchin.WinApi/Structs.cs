using System;
using System.Runtime.InteropServices;

namespace Savchin.WinApi
{
    // Note: backing fields were added because structs don't automatically supply them.

    /// <summary>
    /// MENUITEMINFO
    /// x2bd8504e8b749c8a
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct MENUITEMINFO
    {
        internal uint x2e94540690ec6f24;
        internal uint x8240369a843c7611;
        internal uint x482c55f56771fee8;
        internal uint xcaac28f758874f3c;
        internal uint x9f8d45ce61065766;
        internal IntPtr x66504ce4f96688f4;
        internal IntPtr x1355ba7c26ff63b9;
        internal IntPtr xc8db0de70706565b;
        internal IntPtr xcf9ea57ca9e76901;
        internal string xf29b74a7173844ea;
        internal uint x9b53e6ccab7822be;
        internal IntPtr x4fb19abfca87d638;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct FLASHWINFO
    {
        public UInt32 cbSize;
        public IntPtr hwnd;
        public FLASHW dwFlags;
        public UInt32 uCount;
        public UInt32 dwTimeout;
    }

    //Declare the wrapper managed MouseHookStruct class.
    [StructLayout(LayoutKind.Sequential)]
    public class MOUSEHOOKSTRUCT
    {
        public POINT pt;
        public int hwnd;
        public int wHitTestCode;
        public int dwExtraInfo;
    }


    /// <summary>
    /// The MSLLHOOKSTRUCT structure contains information about a low-level keyboard input event. 
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct MSLLHOOKSTRUCT
    {
        /// <summary>
        /// Specifies a Point structure that contains the X- and Y-coordinates of the cursor, in screen coordinates. 
        /// </summary>
        public POINT Point;
        /// <summary>
        /// If the message is WM_MOUSEWHEEL, the high-order word of this member is the wheel delta. 
        /// The low-order word is reserved. A positive value indicates that the wheel was rotated forward, 
        /// away from the user; a negative value indicates that the wheel was rotated backward, toward the user. 
        /// One wheel click is defined as WHEEL_DELTA, which is 120. 
        ///If the message is WM_XBUTTONDOWN, WM_XBUTTONUP, WM_XBUTTONDBLCLK, WM_NCXBUTTONDOWN, WM_NCXBUTTONUP,
        /// or WM_NCXBUTTONDBLCLK, the high-order word specifies which X button was pressed or released, 
        /// and the low-order word is reserved. This value can be one or more of the following values. Otherwise, MouseData is not used. 
        ///XBUTTON1
        ///The first X button was pressed or released.
        ///XBUTTON2
        ///The second X button was pressed or released.
        /// </summary>
        public int MouseData;
        /// <summary>
        /// Specifies the event-injected flag. An application can use the following value to test the mouse Flags. Value Purpose 
        ///LLMHF_INJECTED Test the event-injected flag.  
        ///0
        ///Specifies whether the event was injected. The value is 1 if the event was injected; otherwise, it is 0.
        ///1-15
        ///Reserved.
        /// </summary>
        public int Flags;
        /// <summary>
        /// Specifies the Time stamp for this message.
        /// </summary>
        public int Time;
        /// <summary>
        /// Specifies extra information associated with the message. 
        /// </summary>
        public int ExtraInfo;
    }

    //Declare the wrapper managed POINT class.
    [StructLayout(LayoutKind.Sequential)]
    public class POINT
    {
        public int X;
        public int Y;
        /// <summary>
        /// Initializes a new instance of the <see cref="POINT"/> class.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        public POINT(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }
    }

    /// <summary>
    /// Another example can be found on http://www.pinvoke.net/default.aspx/user32/SendInput.html. Notice
    /// that KEYBDINPUT on pinvoke.net is split in two structs : INPUT and KEYBDINPUT. The KEYBDINPUT that is
    /// used here, contains both structs of pinvoke.net.
    /// </summary>
    [StructLayout(LayoutKind.Explicit, Size = 28)]
    public struct KEYBDINPUT
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

    /// <summary>
    /// The KBDLLHOOKSTRUCT structure contains information about a low-level keyboard input event. 
    /// </summary>
    /// <remarks>
    /// http://msdn.microsoft.com/library/default.asp?url=/library/en-us/winui/winui/windowsuserinterface/windowing/hooks/hookreference/hookstructures/cwpstruct.asp
    /// </remarks>
    [StructLayout(LayoutKind.Sequential)]
    public struct KeyboardHookStruct
    {
        /// <summary>
        /// Specifies a virtual-key code. The code must be a value in the range 1 to 254. 
        /// </summary>
        public int VirtualKeyCode;
        /// <summary>
        /// Specifies a hardware scan code for the key. 
        /// </summary>
        public int ScanCode;
        /// <summary>
        /// Specifies the extended-key flag, event-injected flag, context code, and transition-state flag.
        /// </summary>
        public int Flags;
        /// <summary>
        /// Specifies the Time stamp for this message.
        /// </summary>
        public int Time;
        /// <summary>
        /// Specifies extra information associated with the message. 
        /// </summary>
        public int ExtraInfo;
    }

    [StructLayout(LayoutKind.Sequential)]
    public class SCROLLINFO
    {
        public int cbSize = Marshal.SizeOf(typeof(SCROLLINFO));
        public int fMask;
        public int nMin;
        public int nMax;
        public int nPage;
        public int nPos;
        public int nTrackPos;
    }

    /// <summary>
    /// The MSG structure contains message information from a thread's message queue. 
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct MSG
    {
        public IntPtr hwnd;
        public uint message;
        public IntPtr wParam;
        public IntPtr lParam;
        public uint time;
        public POINT pt;
    }

    public struct MOUSEINPUT
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
        ///Windows NT/2000/XP: Specifies that the wheel was moved, if the mouse has a wheel. The amount of movement is specified in mouseDataA? .
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
            type = Win32.INPUT_MOUSE;
            dx = 0;
            dy = 0;
            mouseData = 0;
            dwFlags = mouseEvent;
            time = 0;
            dwExtraInfo = 0;
        }
    } ;
}
