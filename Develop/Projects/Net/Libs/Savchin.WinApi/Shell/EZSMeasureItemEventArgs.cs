using System.Drawing;
using System.Windows.Forms;

namespace Savchin.WinApi.Shell
{
    public class EZSMeasureItemEventArgs : MeasureItemEventArgs
    {
        // Fields
        private ShellMenuItem xbe51de0c4e655128;

        // Methods
        /// <summary>
        /// Initializes a new instance of the <see cref="EZSMeasureItemEventArgs"/> class.
        /// </summary>
        /// <param name="menuitem">The menuitem.</param>
        /// <param name="g">The g.</param>
        /// <param name="itemWidth">Width of the item.</param>
        /// <param name="itemHeight">Height of the item.</param>
        internal EZSMeasureItemEventArgs(ShellMenuItem menuitem, Graphics g, int itemWidth, int itemHeight)
            : base(g, -1, itemHeight)
        {
            this.xbe51de0c4e655128 = menuitem;
            base.ItemWidth = itemWidth;
        }

        // Properties
        public ShellMenuItem MenuItem
        {
            get
            {
                return this.xbe51de0c4e655128;
            }
        }
    }

 

}
