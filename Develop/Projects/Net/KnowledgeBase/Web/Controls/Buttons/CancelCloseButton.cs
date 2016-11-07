#region Version & Copyright
/* 
 * $Id: CancelCloseButton.cs 18283 2007-06-26 11:12:32Z svd $ 
 *
 * <:COPYRIGHT_LICENSE:>
 */
#endregion
using System;
using System.Collections.Generic;
using System.Text;

namespace KnowledgeBase.Controls
{
    public class CancelCloseButton : CancelButton
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CancelCloseButton"/> class.
        /// </summary>
        public CancelCloseButton()
        {
            UseSubmitBehavior = false;
            OnClientClick = "window.close();";
            Text = "Cancel";
        }
    }
}
