using System;
using System.Windows.Forms;

namespace Savchin.Forms.ListView
{
    /// <summary>
    /// A limited wrapper around a Windows tooltip window.
    /// </summary>
    public class MyToolTip
    {
        #region Properties
        private readonly MyToolTipNativeWindow window;
        /// <summary>
        /// Gets the handle.
        /// </summary>
        /// <value>The handle.</value>
        public IntPtr Handle
        {
            get
            {
                if (!IsHandleCreated)
                {
                    CreateHandle();
                }
                return window.Handle;
            }
        }

        /// <summary>
        /// Gets a value indicating whether this instance is handle created.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is handle created; otherwise, <c>false</c>.
        /// </value>
        protected bool IsHandleCreated
        {
            get { return (window != null && window.Handle != IntPtr.Zero); }
        } 
        #endregion
        /// <summary>
        /// Initializes a new instance of the <see cref="MyToolTip"/> class.
        /// </summary>
        public MyToolTip()
        {
            window = new MyToolTipNativeWindow(this);
        }



        /// <summary>
        /// Adds the tool.
        /// </summary>
        /// <param name="window">The window.</param>
        public void AddTool(IWin32Window window)
        {
            const int TTM_ADDTOOL = 0x432;

            NativeMethods.SendMessage(Handle, 0x418, 0, SystemInformation.MaxWindowTrackSize.Width);

            NativeMethods.TOOLINFO lParam = GetTOOLINFO(window);
            IntPtr result = NativeMethods.SendMessageTOOLINFO(Handle, TTM_ADDTOOL, 0, lParam);
        }

        /// <summary>
        /// Pops the tool tip.
        /// </summary>
        /// <param name="window">The window.</param>
        public void PopToolTip(IWin32Window window)
        {
            const int TTM_POP = 0x41c;
            NativeMethods.SendMessage(Handle, TTM_POP, 0, 0);
        }

        /// <summary>
        /// Removes the tool tip.
        /// </summary>
        /// <param name="window">The window.</param>
        public void RemoveToolTip(IWin32Window window)
        {
            const int TTM_DELTOOL = 0x433;
            NativeMethods.TOOLINFO lParam = GetTOOLINFO(window);
            NativeMethods.SendMessageTOOLINFO(Handle, TTM_DELTOOL, 0, lParam);
        }

        internal NativeMethods.TOOLINFO GetTOOLINFO(IWin32Window window)
        {
            const int TTF_IDISHWND = 1;
            //const int TTF_ABSOLUTE = 0x80;
            //const int TTF_CENTERTIP = 2;
            const int TTF_SUBCLASS = 0x10;
            //const int TTF_TRACK = 0x20;
            //const int TTF_TRANSPARENT = 0x100;

            var toolinfo_tooltip = new NativeMethods.TOOLINFO();
            toolinfo_tooltip.hwnd = window.Handle;
            toolinfo_tooltip.uFlags = TTF_IDISHWND | TTF_SUBCLASS;
            toolinfo_tooltip.uId = window.Handle;
            toolinfo_tooltip.lpszText = (IntPtr) (-1); // LPSTR_TEXTCALLBACK

            return toolinfo_tooltip;
        }

        protected void CreateHandle()
        {
            if (IsHandleCreated)
                return;

            const int TTS_BALLOON = 0x40;
            const int TTS_NOPREFIX = 2;

            var p = new CreateParams();
            p.ClassName = "tooltips_class32";
            p.Style = TTS_NOPREFIX;
            p.Style = TTS_NOPREFIX | TTS_BALLOON;
            window.CreateHandle(p);
        }

        public void WndProc(ref Message msg)
        {
            //System.Diagnostics.Debug.WriteLine(String.Format("xx {0:x}", m.Msg));
            //switch (m.Msg) {
            //    case 0x4E: // WM_NOTIFY
            //        if (!this.HandleNotify(ref m))
            //            return;
            //        break;
            //    case 0x204E: // WM_REFLECT_NOTIFY
            //        if (!this.HandleNotify(ref m))
            //            return;
            //        break;
            //}
            window.DefWndProc(ref msg);
        }

        #region Nested type: MyToolTipNativeWindow

        internal class MyToolTipNativeWindow : NativeWindow
        {
            private readonly MyToolTip control;

            public MyToolTipNativeWindow(MyToolTip control)
            {
                this.control = control;
            }

            protected override void WndProc(ref Message m)
            {
                if (control != null)
                {
                    control.WndProc(ref m);
                }
            }
        }

        #endregion
    }
}