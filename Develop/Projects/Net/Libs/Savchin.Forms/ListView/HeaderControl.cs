using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Savchin.WinApi;


namespace Savchin.Forms.ListView
{
    /// <summary>
    /// Class used to capture window messages for the header of the list view
    /// control.
    /// </summary>
    public class HeaderControl : NativeWindow
    {
        private readonly ObjectListView parentListView;
        private readonly MyToolTip tooltip;
        private int columnShowingTip = -1;

        public HeaderControl(ObjectListView olv)
        {
            parentListView = olv;
            AssignHandle(NativeMethods.GetHeaderControl(olv));
            tooltip = new MyToolTip();
            tooltip.AddTool(this);
        }

        /// <summary>
        /// Return the Windows handle behind this control
        /// </summary>
        /// <remarks>
        /// When an ObjectListView is initialized as part of a UserControl, the
        /// GetHeaderControl() method returns 0 until the UserControl is
        /// completely initialized. So the AssignHandle() call in the constructor
        /// doesn't work. So we override the Handle property so value is always
        /// current.
        /// </remarks>
        public new IntPtr Handle
        {
            get { return NativeMethods.GetHeaderControl(parentListView); }
        }

        protected bool IsCursorOverLockedDivider
        {
            get
            {
                Point pt = parentListView.PointToClient(Cursor.Position);
                pt.X += User32.GetScrollPosition(parentListView.Handle, true);
                int dividerIndex = NativeMethods.GetDividerUnderPoint(Handle, pt);
                if (dividerIndex >= 0 && dividerIndex < parentListView.Columns.Count)
                {
                    OLVColumn column = parentListView.GetColumn(dividerIndex);
                    return column.IsFixedWidth || column.FillsFreeSpace;
                }
                else
                    return false;
            }
        }

        /// <summary>
        /// Return the index of the column under the current cursor position,
        /// or -1 if the cursor is not over a column
        /// </summary>
        /// <returns>Index of the column under the cursor, or -1</returns>
        public int ColumnIndexUnderCursor
        {
            get
            {
                Point pt = parentListView.PointToClient(Cursor.Position);
                pt.X += User32.GetScrollPosition(parentListView.Handle, true);
                return NativeMethods.GetColumnUnderPoint(Handle, pt);
            }
        }

        //TODO: The Handle property may no longer be necessary. CHECK! 2008/11/28

        protected override void WndProc(ref Message m)
        {
            switch ((WM) m.Msg)
            {
                case WM.WM_SETCURSOR:
                    if (IsCursorOverLockedDivider)
                    {
                        m.Result = (IntPtr) 1; // Don't change the cursor
                        return;
                    }
                    break;

                case WM.WM_NOTIFY:
                    if (!HandleNotify(ref m))
                        return;
                    break;

                case WM.WM_MOUSEMOVE:
                    HandleMouseMove(ref m);
                    break;
            }

            base.WndProc(ref m);
        }

        protected void HandleMouseMove(ref Message m)
        {
            int columnIndex = ColumnIndexUnderCursor;

            // If the mouse has moved to a different header, pop the current tip (if any)
            if (columnIndex != columnShowingTip)
            {
                tooltip.PopToolTip(this);
                columnShowingTip = columnIndex;
            }
        }

        protected unsafe bool HandleNotify(ref Message m)
        {
            //const int TTN_SHOW = -521;
            //const int TTN_POP = -522;
            const int TTN_GETDISPINFO = -530;

            if (m.LParam == IntPtr.Zero)
                return false;

            var lParam = (NativeMethods.NMHDR*) m.LParam;
            switch (lParam->code)
            {
                case TTN_GETDISPINFO:
                    return HandleGetDispInfo(ref m);
            }

            return false;
        }

        protected bool HandleGetDispInfo(ref Message m)
        {
            int columnIndex = ColumnIndexUnderCursor;
            if (columnIndex < 0)
                return false;

            string text = parentListView.GetHeaderToolTip(columnIndex);
            if (String.IsNullOrEmpty(text))
                return false;

            var tooltipText = (NativeMethods.TOOLTIPTEXT) m.GetLParam(typeof (NativeMethods.TOOLTIPTEXT));
            tooltipText.lpszText = text;
            if (parentListView.RightToLeft == RightToLeft.Yes)
                tooltipText.uFlags |= 4;

            Marshal.StructureToPtr(tooltipText, m.LParam, false);
            return true;
        }
    }
}