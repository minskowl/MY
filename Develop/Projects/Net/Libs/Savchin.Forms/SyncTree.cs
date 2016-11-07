using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Security.Permissions;
using System.Text;
using System.Security;
using System.Windows.Forms;
using Savchin.Controls;
using Savchin.WinApi;

namespace Savchin.Forms
{
    public class SyncTree : TreeViewEx
    {
        [Category("Mouse")]
        public event MessageDelegate ScrollHorizontal;
        [Category("Mouse")]
        public event MessageDelegate ScrollVertical;
        [Category("Mouse")]
        public event MessageDelegate WndProcMouseWheel;


        /// <summary>
        /// Overrides <see cref="M:System.Windows.Forms.Control.WndProc(System.Windows.Forms.Message@)"></see>.
        /// </summary>
        /// <param name="m"></param>
        [PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
        protected override void WndProc(ref Message m)
        {
            switch ((WM)m.Msg)
            {
                case WM.WM_VSCROLL:
                    if (m.LParam.ToInt32() == 0)
                    {
                        OnScrollVertical(new MessageEventArgs(m));
                    }
                    break;
                case WM.WM_HSCROLL:
                    if (m.LParam.ToInt32() == 0)
                    {
                        OnScrollHorizontal(new MessageEventArgs(m));
                    }
                    break;
                case WM.WM_MOUSEWHEEL:
                    OnWndProcMouseWheel(new MessageEventArgs(m));
                    break;
            }
            base.WndProc(ref m);
        }
        /// <summary>
        /// Send_s the MSG.
        /// </summary>
        /// <param name="m">The m.</param>
        public void SendMessage(ref Message m)
        {
            WndProc(ref m);
        }

        /// <summary>
        /// Raises the <see cref="E:ScrollVertical"/> event.
        /// </summary>
        /// <param name="e">The <see cref="MessageEventArgs"/> instance containing the event data.</param>
        protected virtual void OnScrollVertical(MessageEventArgs e)
        {
            if (ScrollVertical != null)
            {
                ScrollVertical(this, e);
            }
        }
        /// <summary>
        /// Raises the <see cref="E:ScrollHorizontal"/> event.
        /// </summary>
        /// <param name="e">The <see cref="MessageEventArgs"/> instance containing the event data.</param>
        protected virtual void OnScrollHorizontal(MessageEventArgs e)
        {
            if (ScrollHorizontal != null)
            {
                ScrollHorizontal(this, e);
            }
        }
        /// <summary>
        /// Raises the <see cref="E:MouseWheel"/> event.
        /// </summary>
        /// <param name="e">The <see cref="MessageEventArgs"/> instance containing the event data.</param>
        protected virtual void OnWndProcMouseWheel(MessageEventArgs e)
        {
            if (WndProcMouseWheel != null)
            {
                WndProcMouseWheel(this, e);
            }
        }
    }
}