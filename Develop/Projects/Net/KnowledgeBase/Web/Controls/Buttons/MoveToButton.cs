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
using Savchin.Web.UI;

namespace KnowledgeBase.Controls
{
    public class MoveToButton : ButtonEx
    {


        /// <summary>
        /// Initializes a new instance of the <see cref="MoveToButton"/> class.
        /// </summary>
        public MoveToButton()
        {
            Mode = ButtonType.Link;
            ImageUrl = ImagePathProvider.MoveToImage;

        }
    }
}
