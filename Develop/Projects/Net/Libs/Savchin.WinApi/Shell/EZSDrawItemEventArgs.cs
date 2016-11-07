using System.Drawing;
using System.Windows.Forms;

namespace Savchin.WinApi.Shell
{
    public class EZSDrawItemEventArgs : DrawItemEventArgs
    {
        // Fields
        private ShellMenuItem xbe51de0c4e655128;

        // Methods
        /// <summary>
        /// Initializes a new instance of the <see cref="EZSDrawItemEventArgs"/> class.
        /// </summary>
        /// <param name="menuitem">The menuitem.</param>
        /// <param name="graphics">The graphics.</param>
        /// <param name="font">The font.</param>
        /// <param name="rect">The rect.</param>
        /// <param name="state">The state.</param>
        internal EZSDrawItemEventArgs(ShellMenuItem menuitem, Graphics graphics, Font font, Rectangle rect, DrawItemState state)
            : base(graphics, font, rect, -1, state, ((state & DrawItemState.Selected) != DrawItemState.None) ? SystemColors.HighlightText : SystemColors.MenuText, SystemColors.Menu)
        {
            this.xbe51de0c4e655128 = menuitem;
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
