using System;
using System.Drawing;
using System.Windows.Forms;

namespace Savchin.Forms.ListView
{
    /// <summary>
    /// Let the world know that a cell edit operation is beginning or ending
    /// </summary>
    public class CellEditEventArgs : EventArgs
    {
        /// <summary>
        /// Create an event args
        /// </summary>
        /// <param name="column"></param>
        /// <param name="control"></param>
        /// <param name="r"></param>
        /// <param name="item"></param>
        /// <param name="subItemIndex"></param>
        public CellEditEventArgs(OLVColumn column, Control control, Rectangle r, OLVListItem item, int subItemIndex)
        {
            this.Control = control;
            this.column = column;
            this.cellBounds = r;
            this.listViewItem = item;
            this.rowObject = item.RowObject;
            this.subItemIndex = subItemIndex;
            this.value = column.GetValue(item.RowObject);
        }

        /// <summary>
        /// Change this to true to cancel the cell editing operation.
        /// </summary>
        /// <remarks>
        /// <para>During the CellEditStarting event, setting this to true will prevent the cell from being edited.</para>
        /// <para>During the CellEditFinishing event, if this value is already true, this indicates that the user has
        /// cancelled the edit operation and that the handler should perform cleanup only. Setting this to true,
        /// will prevent the ObjectListView from trying to write the new value into the model object.</para>
        /// </remarks>
        public bool Cancel;

        /// <summary>
        /// During the CellEditStarting event, this can be modified to be the control that you want
        /// to edit the value. You must fully configure the control before returning from the event,
        /// including its bounds and the value it is showing.
        /// During the CellEditFinishing event, you can use this to get the value that the user
        /// entered and commit that value to the model. Changing the control during the finishing
        /// event has no effect.
        /// </summary>
        public Control Control;

        /// <summary>
        /// The column of the cell that is going to be or has been edited.
        /// </summary>
        public OLVColumn Column
        {
            get { return this.column; }
        }
        private OLVColumn column;

        /// <summary>
        /// The model object of the row of the cell that is going to be or has been edited.
        /// </summary>
        public Object RowObject
        {
            get { return this.rowObject; }
        }
        private Object rowObject;

        /// <summary>
        /// The listview item of the cell that is going to be or has been edited.
        /// </summary>
        public OLVListItem ListViewItem
        {
            get { return this.listViewItem; }
        }
        private OLVListItem listViewItem;

        /// <summary>
        /// The index of the cell that is going to be or has been edited.
        /// </summary>
        public int SubItemIndex
        {
            get { return this.subItemIndex; }
        }
        private int subItemIndex;

        /// <summary>
        /// The data value of the cell before the edit operation began.
        /// </summary>
        public Object Value
        {
            get { return this.value; }
        }
        private Object value;

        /// <summary>
        /// The bounds of the cell that is going to be or has been edited.
        /// </summary>
        public Rectangle CellBounds
        {
            get { return this.cellBounds; }
        }
        private Rectangle cellBounds;
    }
}