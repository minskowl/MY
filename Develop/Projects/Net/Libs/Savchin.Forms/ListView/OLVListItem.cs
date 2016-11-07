using System;
using System.Windows.Forms;

namespace Savchin.Forms.ListView
{
    /// <summary>
    /// OLVListItems are specialized ListViewItems that know which row object they came from,
    /// and the row index at which they are displayed, even when in group view mode. They
    /// also know the image they should draw against themselves
    /// </summary>
    public class OLVListItem : ListViewItem
    {
        /// <summary>
        /// Create a OLVListItem for the given row object
        /// </summary>
        public OLVListItem(object rowObject)
            : base()
        {
            this.rowObject = rowObject;
        }

        /// <summary>
        /// Create a OLVListItem for the given row object, represented by the given string and image
        /// </summary>
        public OLVListItem(object rowObject, string text, Object image)
            : base(text, -1)
        {
            this.rowObject = rowObject;
            this.imageSelector = image;
        }

        /// <summary>
        /// RowObject is the model object that is source of the data for this list item.
        /// </summary>
        public object RowObject
        {
            get { return rowObject; }
            set { rowObject = value; }
        }
        private object rowObject;

        /// <summary>
        /// DisplayIndex is the index of the row where this item is displayed. For flat lists,
        /// this is the same as ListViewItem.Index, but for grouped views, it is different.
        /// </summary>
        [Obsolete("This property is no longer maintained", true)]
        public int DisplayIndex
        {
            get { return 0; }
            set {  }
        }

        /// <summary>
        /// Get or set the image that should be shown against this item
        /// </summary>
        /// <remarks><para>This can be an Image, a string or an int. A string or an int will
        /// be used as an index into the small image list.</para></remarks>
        public Object ImageSelector
        {
            get { return imageSelector; }
            set {
                imageSelector = value;
                if (value is Int32)
                    this.ImageIndex = (Int32)value;
                else if (value is String)
                    this.ImageKey = (String)value;
                else
                    this.ImageIndex = -1;
            }
        }
        private Object imageSelector;

        /// <summary>
        /// Enable tri-state checkbox.
        /// </summary>
        /// <remarks>.NET's Checked property was not built to handle tri-state checkboxes,
        /// and will return True for both Checked and Indeterminate states.</remarks>
        public CheckState CheckState
        {
            get { return this.checkState; }
            set {
                this.checkState = value;

                //THINK: I don't think we need this, since the Checked property just uses StateImageIndex, which we are about to set.
                //this.Checked = (checkState == CheckState.Checked);

                // We have to specifically set the state image
                switch (value) {
                    case System.Windows.Forms.CheckState.Unchecked:
                        this.StateImageIndex = 0;
                        break;
                    case System.Windows.Forms.CheckState.Checked:
                        this.StateImageIndex = 1;
                        break;
                    case System.Windows.Forms.CheckState.Indeterminate:
                        this.StateImageIndex = 2;
                        break;
                }
            }
        }
        private CheckState checkState;
    }
}