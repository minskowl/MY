using System;
using System.Collections.Generic;
using System.Text;
using Savchin.Web.UI;

namespace KnowledgeBase.Controls
{
    public class Logo : ImageEx
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Logo"/> class.
        /// </summary>
        public Logo()
        {
            ImageUrl=ImagePathProvider.LogoImage;
        }
    }
}
