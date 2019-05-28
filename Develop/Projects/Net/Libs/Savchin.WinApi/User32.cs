using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using Savchin.WinApi.Enums;
using Savchin.WinApi.Shell;

namespace Savchin.WinApi
{
    public class User32
    {
        #region Window Hook

        /// <summary>
        /// The UnhookWindowsHookEx function removes a hook procedure installed in a hook chain by the SetWindowsHookEx function. 
        /// </summary>
        /// <param name="idHook">
        /// [in] Handle to the hook to be removed. This parameter is a hook handle obtained by a previous call to SetWindowsHookEx. 
        /// </param>
        /// <returns>
        /// If the function succeeds, the return value is nonzero.
        /// If the function fails, the return value is zero. To get extended error information, call GetLastError.
        /// </returns>
        /// <remarks>
        /// http://msdn.microsoft.com/library/default.asp?url=/library/en-us/winui/winui/windowsuserinterface/windowing/hooks/hookreference/hookfunctions/setwindowshookex.asp
        /// </remarks>
        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern bool UnhookWindowsHookEx(int idHook);


        /// <summary>
        /// The CallNextHookEx function passes the hook information to the next hook procedure in the current hook chain. 
        /// A hook procedure can call this function either before or after processing the hook information. 
        /// </summary>
        /// <param name="idHook">Ignored.</param>
        /// <param name="nCode">
        /// [in] Specifies the hook code passed to the current hook procedure. 
        /// The next hook procedure uses this code to determine how to process the hook information.
        /// </param>
        /// <param name="wParam">
        /// [in] Specifies the wParam value passed to the current hook procedure. 
        /// The meaning of this parameter depends on the type of hook associated with the current hook chain. 
        /// </param>
        /// <param name="lParam">
        /// [in] Specifies the lParam value passed to the current hook procedure. 
        /// The meaning of this parameter depends on the type of hook associated with the current hook chain. 
        /// </param>
        /// <returns>
        /// This value is returned by the next hook procedure in the chain. 
        /// The current hook procedure must also return this value. The meaning of the return value depends on the hook type. 
        /// For more information, see the descriptions of the individual hook procedures.
        /// </returns>
        /// <remarks>
        /// http://msdn.microsoft.com/library/default.asp?url=/library/en-us/winui/winui/windowsuserinterface/windowing/hooks/hookreference/hookfunctions/setwindowshookex.asp
        /// </remarks>
        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern int CallNextHookEx(int idHook, int nCode, IntPtr wParam, IntPtr lParam);
        /// <summary>
        /// The SetWindowsHookEx function installs an application-defined hook procedure into a hook chain. 
        /// You would install a hook procedure to monitor the system for certain types of events. These events 
        /// are associated either with a specific thread or with all threads in the same desktop as the calling thread. 
        /// </summary>
        /// <param name="idHook">
        /// [in] Specifies the type of hook procedure to be installed. This parameter can be one of the following values.
        /// </param>
        /// <param name="lpfn">
        /// [in] Pointer to the hook procedure. If the dwThreadId parameter is zero or specifies the identifier of a 
        /// thread created by a different process, the lpfn parameter must point to a hook procedure in a dynamic-link 
        /// library (DLL). Otherwise, lpfn can point to a hook procedure in the code associated with the current process.
        /// </param>
        /// <param name="hInstance">
        /// [in] Handle to the DLL containing the hook procedure pointed to by the lpfn parameter. 
        /// The hMod parameter must be set to NULL if the dwThreadId parameter specifies a thread created by 
        /// the current process and if the hook procedure is within the code associated with the current process. 
        /// </param>
        /// <param name="threadId">
        /// [in] Specifies the identifier of the thread with which the hook procedure is to be associated. 
        /// If this parameter is zero, the hook procedure is associated with all existing threads running in the 
        /// same desktop as the calling thread. 
        /// </param>
        /// <returns>
        /// If the function succeeds, the return value is the handle to the hook procedure.
        /// If the function fails, the return value is NULL. To get extended error information, call GetLastError.
        /// </returns>
        /// <remarks>
        /// http://msdn.microsoft.com/library/default.asp?url=/library/en-us/winui/winui/windowsuserinterface/windowing/hooks/hookreference/hookfunctions/setwindowshookex.asp
        /// </remarks>
        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern int SetWindowsHookEx(WH idHook, HookProc lpfn, IntPtr hInstance, uint threadId);

        #endregion

        #region Keyboard
        /// <summary>
        /// The GetKeyboardState function copies the status of the 256 virtual keys to the 
        /// specified buffer. 
        /// </summary>
        /// <param name="pbKeyState">
        /// [in] Pointer to a 256-byte array that contains keyboard key states. 
        /// </param>
        /// <returns>
        /// If the function succeeds, the return value is nonzero.
        /// If the function fails, the return value is zero. To get extended error information, call GetLastError. 
        /// </returns>
        /// <remarks>
        /// http://msdn.microsoft.com/library/default.asp?url=/library/en-us/winui/winui/windowsuserinterface/userinput/keyboardinput/keyboardinputreference/keyboardinputfunctions/toascii.asp
        /// </remarks>
        [DllImport("user32")]
        public static extern int GetKeyboardState(byte[] pbKeyState);

        [DllImport("user32.dll")]
        public static extern bool SetKeyboardState(byte[] lpKeyState);

        /// <summary>
        /// The GetKeyState function retrieves the status of the specified virtual key. The status specifies whether the key is up, down, or toggled 
        /// (on, off—alternating each time the key is pressed). 
        /// </summary>
        /// <param name="vKey">
        /// [in] Specifies a virtual key. If the desired virtual key is a letter or digit (A through Z, a through z, or 0 through 9), nVirtKey must be set to the ASCII value of that character. For other keys, it must be a virtual-key code. 
        /// </param>
        /// <returns>
        /// The return value specifies the status of the specified virtual key, as follows: 
        ///If the high-order bit is 1, the key is down; otherwise, it is up.
        ///If the low-order bit is 1, the key is toggled. A key, such as the CAPS LOCK key, is toggled if it is turned on. The key is off and untoggled if the low-order bit is 0. A toggle key's indicator light (if any) on the keyboard will be on when the key is toggled, and off when the key is untoggled.
        /// </returns>
        /// <remarks>http://msdn.microsoft.com/en-us/library/ms646301.aspx</remarks>
        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern short GetKeyState(int vKey);

        /// <summary>
        /// This function synthesizes a keystroke. The system can use such a synthesized keystroke to generate a 
        /// WM_KEYUP or WM_KEYDOWN message. 
        /// </summary>
        /// <param name="bVk">The b vk.</param>
        /// <param name="bScan">The b scan.</param>
        /// <param name="dwFlags">The dw flags.</param>
        /// <param name="dwExtraInfo">The dw extra info.</param>
        [DllImport("user32.dll")]
        public static extern void keybd_event(byte bVk, byte bScan, uint dwFlags, UIntPtr dwExtraInfo);

        [DllImport("user32.dll")]
        public static extern short VkKeyScanEx(char ch, IntPtr dwhkl);

        [DllImport("user32.dll")]
        public static extern byte VkKeyScan(char ch);


        [DllImport("user32.dll")]
        public static extern IntPtr GetKeyboardLayout(uint idThread);

        /// <summary>
        /// The ToAscii function translates the specified virtual-key code and keyboard 
        /// state to the corresponding character or characters. The function translates the code 
        /// using the input language and physical keyboard layout identified by the keyboard layout handle.
        /// </summary>
        /// <param name="uVirtKey">
        /// [in] Specifies the virtual-key code to be translated. 
        /// </param>
        /// <param name="uScanCode">
        /// [in] Specifies the hardware scan code of the key to be translated. 
        /// The high-order bit of this value is set if the key is up (not pressed). 
        /// </param>
        /// <param name="lpbKeyState">
        /// [in] Pointer to a 256-byte array that contains the current keyboard state. 
        /// Each element (byte) in the array contains the state of one key. 
        /// If the high-order bit of a byte is set, the key is down (pressed). 
        /// The low bit, if set, indicates that the key is toggled on. In this function, 
        /// only the toggle bit of the CAPS LOCK key is relevant. The toggle state 
        /// of the NUM LOCK and SCROLL LOCK keys is ignored.
        /// </param>
        /// <param name="lpwTransKey">
        /// [out] Pointer to the buffer that receives the translated character or characters. 
        /// </param>
        /// <param name="fuState">
        /// [in] Specifies whether a menu is active. This parameter must be 1 if a menu is active, or 0 otherwise. 
        /// </param>
        /// <returns>
        /// If the specified key is a dead key, the return value is negative. Otherwise, it is one of the following values. 
        /// Value Meaning 
        /// 0 The specified virtual key has no translation for the current state of the keyboard. 
        /// 1 One character was copied to the buffer. 
        /// 2 Two characters were copied to the buffer. This usually happens when a dead-key character 
        /// (accent or diacritic) stored in the keyboard layout cannot be composed with the specified 
        /// virtual key to form a single character. 
        /// </returns>
        /// <remarks>
        /// http://msdn.microsoft.com/library/default.asp?url=/library/en-us/winui/winui/windowsuserinterface/userinput/keyboardinput/keyboardinputreference/keyboardinputfunctions/toascii.asp
        /// </remarks>
        [DllImport("user32")]
        public static extern int ToAscii(int uVirtKey, int uScanCode, byte[] lpbKeyState, byte[] lpwTransKey, int fuState);


        #endregion

        #region Cursor

        /// <summary>
        /// Gets the cursor pos.
        /// </summary>
        /// <param name="lpPoint">The lp point.</param>
        /// <returns></returns>
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool GetCursorPos(out POINT lpPoint);

        [DllImport("user32.dll")]
        public static extern bool ShowCursor(bool bShow);

        [DllImport("user32.dll")]
        public static extern int SetCursorPos(int x, int y);

        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern void mouse_event(long dwFlags, long dx, long dy, long cButtons, long dwExtraInfo);
        private const int MOUSEEVENTF_LEFTDOWN = 0x02;
        private const int MOUSEEVENTF_LEFTUP = 0x04;
        private const int MOUSEEVENTF_MOVE = 0x0001;

        public static void DoMouseClick()
        {
            //X = Cursor.Position.X;
            //Y = Cursor.Position.Y;

            ////move to coordinates
            //pt = (Point)pc.ConvertFromString(X + "," + Y);
            //Cursor.Position = pt;

            //perform click            
            mouse_event(MOUSEEVENTF_LEFTDOWN, 0, 0, 0, 0);
            mouse_event(MOUSEEVENTF_LEFTUP, 0, 0, 0, 0);
        }
        #endregion


        /// <summary>
        /// Specifies the function's purpose. If this parameter is TRUE, keyboard and mouse input events are 
        /// blocked. If this parameter is FALSE, keyboard and mouse events are unblocked. Note that only 
        /// the thread that blocked input can successfully unblock input. 
        /// </summary>
        /// <param name="blockIt"></param>
        /// <returns></returns>
        /// <remarks>
        /// When input is blocked, real physical input from the mouse or keyboard will not affect the input queue's 
        /// synchronous key state (reported by GetKeyState and GetKeyboardState), nor will it affect the asynchronous 
        /// key state (reported by GetAsyncKeyState). However, the thread that is blocking input can affect both of 
        /// these key states by calling SendInput. No other thread can do this.
        /// The system will unblock input in the following cases: 
        /// The thread that blocked input unexpectedly exits without calling BlockUserInput with fBlock set to FALSE. 
        /// In this case, the system cleans up properly and re-enables input. Windows 95/98/Me: The system displays the
        /// Close Program/Fault dialog box. This can occur if the thread faults or if the user presses CTRL+ALT+DEL. 
        /// Windows 2000/XP: The user presses CTRL+ALT+DEL or the system invokes the Hard System Error modal message
        /// box (for example, when a program faults or a device fails). 
        /// </remarks>
        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool BlockInput(bool blockIt);

        [DllImport("user32.dll", EntryPoint = "SendInput", SetLastError = true)]
        public static extern int SendMouseInput(int cInputs, ref MOUSEINPUT pInputs, int cbSize);

        [DllImport("user32.dll", EntryPoint = "SendInput", SetLastError = true)]
        public static extern int SendKeyboardInput(int cInputs, ref KEYBDINPUT pInputs, int cbSize);
        /// <summary>
        /// The GetDoubleClickTime function retrieves the current double-click time for the mouse. A double-click is a series of two clicks of the 
        /// mouse button, the second occurring within a specified time after the first. The double-click time is the maximum number of 
        /// milliseconds that may occur between the first and second click of a double-click. 
        /// </summary>
        /// <returns>
        /// The return value specifies the current double-click time, in milliseconds. 
        /// </returns>
        /// <remarks>
        /// http://msdn.microsoft.com/en-us/library/ms646258(VS.85).aspx
        /// </remarks>
        [DllImport("user32")]
        public static extern int GetDoubleClickTime();

        #region Scrolling

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern int ShowScrollBar(IntPtr hWnd, int wBar, int bShow);

        // Entry points used by this code
        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        public static extern bool GetScrollInfo(IntPtr hWnd, int fnBar, SCROLLINFO si);
        /// <summary>
        /// Get the scroll position of the given scroll bar
        /// </summary>
        /// <param name="handle"></param>
        /// <param name="horizontalBar"></param>
        /// <returns></returns>
        public static int GetScrollPosition(IntPtr handle, bool horizontalBar)
        {
            int fnBar = (int)(horizontalBar ? SB.SB_HORZ : SB.SB_VERT);

            SCROLLINFO si = new SCROLLINFO();
            si.fMask = (int)SIF.SIF_POS;
            if (GetScrollInfo(handle, fnBar, si))
                return si.nPos;
            else
                return -1;
        }
        #endregion

        #region Windows

        #region Enumumerate Windows
        [DllImport("user32.dll")]
        public static extern bool EnumThreadWindows(uint dwThreadId, EnumThreadDelegate lpfn, IntPtr lParam);

        [DllImport("user32.dll")]
        public static extern bool EnumChildWindows(IntPtr hwnd, WindowEnumProc lpEnumCallbackFunction, IntPtr lParam);

        [DllImport("user32.dll", EntryPoint = "EnumDesktopWindows", ExactSpelling = false, CharSet = CharSet.Auto, SetLastError = true)]
        public static extern bool EnumDesktopWindows(IntPtr hDesktop, WindowEnumProc lpEnumCallbackFunction, IntPtr lParam);


        #endregion

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool SetForegroundWindow(IntPtr hWnd);

        [DllImport("user32.dll")]
        public static extern IntPtr FindWindow(String lpClassName, String lpWindowName);

        [DllImport("user32.dll")]
        public static extern IntPtr GetDesktopWindow();

        [DllImport("user32.dll")]
        public static extern IntPtr GetParent(IntPtr handleToWindow);

        [DllImport("user32.dll")]
        public static extern int GetWindowText(IntPtr handleToWindow, StringBuilder windowText, int maxTextLength);
        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern int GetWindowTextLength(IntPtr hWnd);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool GetWindowRect(IntPtr hWnd, out RECT rect);

        /// <summary>
        /// Creates the dialog param.
        /// </summary>
        /// <param name="hInstance">The h instance.</param>
        /// <param name="lpTemplateName">Name of the lp template.</param>
        /// <param name="hwndParent">The HWND parent.</param>
        /// <param name="lpDialogFunc">The lp dialog func.</param>
        /// <param name="dwInitParam">The dw init param.</param>
        /// <returns></returns>
        [DllImport("user32.dll")]
        public static extern IntPtr CreateDialogParam(IntPtr hInstance, string lpTemplateName, IntPtr hwndParent, DialogProc lpDialogFunc, IntPtr dwInitParam);
        [DllImport("user32.dll")]
        public static extern IntPtr CreateDialogParam(IntPtr hInstance, int idResource, IntPtr hwndParent, DialogProc lpDialogFunc, IntPtr dwInitParam);

        /// <summary>
        /// <para>The DestroyWindow function destroys the specified window. The function sends WM_DESTROY and WM_NCDESTROY messages to the window to deactivate it and remove the keyboard focus from it. The function also destroys the window's menu, flushes the thread message queue, destroys timers, removes clipboard ownership, and breaks the clipboard viewer chain (if the window is at the top of the viewer chain).</para>
        /// <para>If the specified window is a parent or owner window, DestroyWindow automatically destroys the associated child or owned windows when it destroys the parent or owner window. The function first destroys child or owned windows, and then it destroys the parent or owner window.</para>
        /// <para>DestroyWindow also destroys modeless dialog boxes created by the CreateDialog function.</para>
        /// </summary>
        /// <param name="hwnd">Handle to the window to be destroyed.</param>
        /// <returns>If the function succeeds, the return value is nonzero. If the function fails, the return value is zero. To get extended error information, call GetLastError.</returns>
        [DllImport("user32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool DestroyWindow(IntPtr hwnd);

        /// <summary>
        /// Gets the client rect.
        /// </summary>
        /// <param name="hWnd">The h WND.</param>
        /// <param name="lpRect">The lp rect.</param>
        /// <returns></returns>
        [DllImport("user32.dll")]
        public static extern bool GetClientRect(IntPtr hWnd, out RECT lpRect);

        /// <summary>
        /// Gets the window text.
        /// </summary>
        /// <param name="hwnd">The HWND.</param>
        /// <returns></returns>
        public static string GetWindowText(IntPtr hwnd)
        {
            int length = GetWindowTextLength(hwnd) + 1;
            StringBuilder buffer = new StringBuilder(length);
            GetWindowText(hwnd, buffer, length);

            return buffer.ToString();
        }
        [DllImport("user32.dll")]
        public static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll")]
        public static extern bool IsWindowVisible(IntPtr hDlg);

        [DllImport("user32.dll")]
        public static extern bool IsWindow(IntPtr hWnd);

        [DllImport("user32.dll")]
        public static extern bool IsWindowEnabled(IntPtr hWnd);

        [DllImport("User32.Dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool IsIconic(IntPtr hWnd);

        [DllImport("user32.dll")]
        public static extern int GetClassName(IntPtr handleToWindow, StringBuilder className, int maxClassNameLength);


        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr GetFocus();

        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr SetFocus(IntPtr hWnd);

        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        public static extern int SetWindowPos(IntPtr hWnd, IntPtr hWndAfter, int X, int Y, int Width, int Height, SWP flags);


        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        public static extern int ShowWindow(IntPtr hWnd, WindowShowStyle cmdShow);


        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern int GetWindowLong(IntPtr hWnd, int Index);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern int SetWindowLong(IntPtr hWnd, int Index, int Value);

        //[DllImport("user32.dll", CharSet = CharSet.Auto)]
        //public static extern IntPtr SetWindowLongPtr(IntPtr hWnd, int nIndex, IntPtr dwNewLong);

        public static IntPtr SetWindowLongPtr(IntPtr hWnd, int nIndex, IntPtr dwNewLong)
        {
            return IntPtr.Size == 8 ? SetWindowLongPtr64(hWnd, nIndex, dwNewLong) : new IntPtr(SetWindowLong32(hWnd, nIndex, dwNewLong.ToInt32()));
        }

        [DllImport("user32.dll", EntryPoint = "SetWindowLong")]
        private static extern int SetWindowLong32(IntPtr hWnd, int nIndex, int dwNewLong);

        [DllImport("user32.dll", EntryPoint = "SetWindowLongPtr")]
        private static extern IntPtr SetWindowLongPtr64(IntPtr hWnd, int nIndex, IntPtr dwNewLong);


        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool FlashWindowEx(ref FLASHWINFO pwfi);

        /// <summary>
        /// Flashes the window.
        /// </summary>
        /// <param name="hWnd">The h WND.</param>
        /// <param name="type">The type.</param>
        /// <param name="count">The count.</param>
        /// <param name="timeout">The timeout. milliseconds</param>
        /// <returns></returns>
        public static bool FlashWindow(IntPtr hWnd, FLASHW type, int count, int timeout)
        {
            var fInfo = new FLASHWINFO
                            {
                                hwnd = hWnd,
                                dwFlags = type,
                                uCount = Convert.ToUInt32(count),
                                dwTimeout = Convert.ToUInt32(timeout)
                            };

            fInfo.cbSize = Convert.ToUInt32(Marshal.SizeOf(fInfo));
            return FlashWindowEx(ref fInfo);
        }
        /// <summary>
        /// Stops the flash window.
        /// </summary>
        /// <param name="hWnd">The h WND.</param>
        /// <returns></returns>
        public static bool StopFlashWindow(IntPtr hWnd)
        {
            return FlashWindow(hWnd, FLASHW.FLASHW_STOP, 0, 0);
        }

        /// <summary>
        /// Sets the parent.
        /// </summary>
        /// <param name="hWndChild">The h WND child.</param>
        /// <param name="hWndNewParent">The h WND new parent.</param>
        /// <returns></returns>
        [DllImport("user32.dll", SetLastError = true)] // SETLAST by us
        public static extern int SetParent(IntPtr hWndChild, IntPtr hWndNewParent);
        #endregion

        #region Menu
        [DllImport("user32.dll")]
        internal static extern int GetMenuItemInfo(IntPtr hMenu, uint uItem, int fByPosition, ref MENUITEMINFO lpmii);

        [DllImport("user32.dll")]
        internal static extern int SetMenuItemInfo(IntPtr hMenu, uint uItem, int fByPosition, ref MENUITEMINFO lpmii);


        #endregion

        #region Dialog Item
        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool SetDlgItemText(IntPtr hDlg, int nIDDlgItem, string lpString);

        [DllImport("user32.dll")]
        public static extern int GetDlgItemText(IntPtr hDlg, int nIDDlgItem, StringBuilder lpString, int maxCount);



        [DllImport("user32.dll")]
        public static extern IntPtr GetDlgItem(IntPtr handleToWindow, int ControlId);



        [DllImport("user32.Dll")]
        public static extern IntPtr SendDlgItemMessage(IntPtr handleToWindow, int dlgItem, uint message,
                                                         UIntPtr wParam, IntPtr lParam);

        #endregion

        #region Desktop

        [DllImport("user32.dll", SetLastError = true)]
        public static extern IntPtr OpenInputDesktop(uint dwFlags, bool fInherit, uint dwDesiredAccess);

        [DllImport("user32.dll")]
        public static extern IntPtr OpenDesktop(string lpszDesktop, uint dwFlags, bool fInherit, uint dwDesiredAccess);

        [DllImport("user32.dll")]
        public static extern bool EnumDesktops(IntPtr hwinsta, EnumDesktopsDelegate lpEnumFunc, IntPtr lParam);


        [DllImport("user32", EntryPoint = "CreateDesktopW", CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern IntPtr CreateDesktop(string lpszDesktop, IntPtr lpszDevice, IntPtr pDevmode, int dwFlags,
                                                  int dwDesiredAccess, IntPtr lpsa);

        [DllImport("user32", SetLastError = true)]
        public static extern int CloseDesktop(IntPtr hDesktop);
        internal const int KEYEVENTF_KEYDOWN = 0x0000;

        [DllImport("user32", SetLastError = true)]
        public static extern IntPtr GetThreadDesktop(uint dwThreadId);

        [DllImport("user32", SetLastError = true)]
        public static extern int SetThreadDesktop(IntPtr hDesktop);

        [DllImport("user32", SetLastError = true)]
        public static extern int SwitchDesktop(IntPtr hDesktop);
        #endregion


        #region WindowStation

        [DllImport("user32.dll", SetLastError = true)]
        public static extern IntPtr GetProcessWindowStation();

        [DllImport("user32.dll")]
        public static extern IntPtr OpenWindowStation(string lpszWinSta, bool fInherit, uint dwDesiredAccess);

        [DllImport("user32.dll")]
        public static extern IntPtr CreateWindowStation(string pwinsta, uint dwReserved, uint dwDesiredAccess, IntPtr lpsa);

        [DllImport("user32.dll")]
        public static extern bool SetProcessWindowStation(IntPtr hWinSta);

        [DllImport("user32.dll")]
        public static extern bool CloseWindowStation(IntPtr hWinSta);

        #endregion

        #region Clipboard
        [DllImport("user32.dll")]
        public static extern IntPtr SetClipboardData(uint uFormat, IntPtr hMem);
        [DllImport("user32.dll")]
        public static extern IntPtr SetClipboardData(CLIPFORMAT uFormat, IntPtr hMem);
        [DllImport("user32.dll")]
        public static extern bool OpenClipboard(IntPtr hWndNewOwner);
        [DllImport("user32.dll")]
        public static extern bool EmptyClipboard();
        [DllImport("user32.dll")]
        public static extern bool CloseClipboard();
        [DllImport("user32.dll", SetLastError = true)]
        public static extern uint RegisterClipboardFormat(string lpszFormat);

    
        public static void SetClipboardText(string text)
        {
            if (!OpenClipboard(IntPtr.Zero))
                throw new Exception("Failed to open clipboard");
            EmptyClipboard();

            var pText = IntPtr.Zero;

            try
            {
                pText = Marshal.StringToHGlobalAuto(text);
                SetClipboardData(CLIPFORMAT.CF_UNICODETEXT, pText); // Äëÿ TEXT è UNICODETEXT
            }
            finally
            {
                CloseClipboard();
                if (pText != IntPtr.Zero)
                    Marshal.FreeHGlobal(pText);
            }
        }
        #endregion

        [return: MarshalAs(UnmanagedType.Bool)]
        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool PostMessage(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);

        [DllImport("user32.dll")]
        internal static extern IntPtr SendMessage(IntPtr hWnd, int Msg, IntPtr wParam, IntPtr lParam);

        [DllImport("user32.dll")]
        public static extern IntPtr SendMessage(IntPtr handleToWindow, uint Message, UIntPtr wParam, IntPtr lParam);
        [DllImport("user32.dll")]
        public static extern bool SendMessage(IntPtr hWnd, uint Msg, int wParam, uint lParam);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern int SendMessage(IntPtr hWnd, int msg, int wParam, int lParam);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr SendMessage(IntPtr hWnd, uint Msg, int wParam, string lParam);



        [DllImport("user32.dll")]
        public static extern int GetSystemMetrics(int nIndex);

        [DllImport("user32.dll")]
        public static extern int SystemParametersInfo(int uAction, int uParam, out int lpvParam, int fuWinIni);




        [DllImport("user32.dll")]
        public static extern uint MapVirtualKeyEx(uint uCode, uint uMapType, IntPtr dwhkl);





        [DllImport("user32.dll")]
        public static extern bool PostThreadMessage(uint idThread, uint Msg, UIntPtr wParam, IntPtr lParam);

        [DllImport("user32.dll", SetLastError = true)]
        public static extern uint GetWindowThreadProcessId(IntPtr hWnd, IntPtr lpdwProcessId);

        [DllImport("user32.dll")]
        public static extern bool AttachThreadInput(int idAttach, uint idAttachTo, bool fAttach);

        [DllImport("user32.dll")]
        public static extern IntPtr GetMessageExtraInfo();

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool ClientToScreen(IntPtr hwnd, ref Point lpPoint);
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool IntersectRect(out RECT lprcDst, [In] ref RECT lprcSrc1,
           [In] ref RECT lprcSrc2);


        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool IsRectEmpty([In] ref RECT lprc);

        [DllImport("user32.dll", EntryPoint = "GetDC")]
        internal extern static IntPtr GetDC(IntPtr hWnd);

        [DllImport("user32.dll", EntryPoint = "ReleaseDC")]
        internal extern static IntPtr ReleaseDC(IntPtr hWnd, IntPtr hDc);

        [DllImport("user32.dll")]
        internal static extern IntPtr GetWindowDC(IntPtr hWnd);
    }
}
