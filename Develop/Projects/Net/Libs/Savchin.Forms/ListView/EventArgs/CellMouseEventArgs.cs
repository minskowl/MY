using System;
using System.Windows.Forms;

namespace Savchin.Forms.ListView
{
    /// <summary>
    /// CellMouseEventArgs
    /// </summary>
    public class ItemEventArgs : MouseEventArgs
    {
        // Fields
        private ListViewItem item;

        /// <summary>
        /// Gets the item.
        /// </summary>
        /// <value>The item.</value>
        public ListViewItem Item
        {
            get { return item; }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CellMouseEventArgs"/> class.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.MouseEventArgs"/> instance containing the event data.</param>
        public ItemEventArgs(ListViewItem item, MouseEventArgs e)
            : base(e.Button, e.Clicks, e.X, e.Y, e.Delta)
        {
            this.item = item;
        }


    }
}