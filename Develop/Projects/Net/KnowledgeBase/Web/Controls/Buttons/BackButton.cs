using System;
using System.Collections.Generic;
using System.Text;
using Savchin.Web.UI;

namespace KnowledgeBase.Controls
{
    /// <summary>
    /// BackButton
    /// </summary>
    public class BackButton : ButtonEx
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BackButton"/> class.
        /// </summary>
        public BackButton()
        {
            Text = "Back";
            CausesValidation = false;
        }
    }
}
