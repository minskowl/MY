using System;
using System.Collections.Generic;
using System.Text;
using Savchin.Web.UI;

namespace KnowledgeBase.Controls
{
    public class RefreshButton : ButtonEx
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RefreshButton"/> class.
        /// </summary>
        public RefreshButton()
        {
            Mode = ButtonType.Link;
            ImageUrl = ImagePathProvider.RefreshImage;
        }
    }
}
