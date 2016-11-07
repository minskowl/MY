using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace KnowledgeBase.KbTools.Controls
{

    public class BaseControl : UserControl
    {
        public event EventHandler Close;
        /// <summary>
        /// Raises the <see cref="E:Close"/> event.
        /// </summary>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected virtual void OnClose(EventArgs e)
        {
            if (Close != null)
                Close(this, e);
        }
    }
}
