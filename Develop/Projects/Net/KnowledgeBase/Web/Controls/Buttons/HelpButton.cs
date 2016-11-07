#region Version & Copyright
/* 
 * Id 
 *
 * <:COPYRIGHT_LICENSE:>
 */
#endregion
using System;
using System.Collections.Generic;
using System.Text;
using KnowledgeBase.Controls;

namespace KnowledgeBase.Controls
{
    public class HelpButton : PDFButton
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HelpButton"/> class.
        /// </summary>
        public HelpButton()
        {
            UseSubmitBehavior = false;
            Target = "_blank";
        }
    }
}
