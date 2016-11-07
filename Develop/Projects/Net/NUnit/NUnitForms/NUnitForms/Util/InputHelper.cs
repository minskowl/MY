using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace NUnit.Extensions.Forms
{
    public static class InputHelper
    {

        /// <summary>
        /// Inputs the text.
        /// </summary>
        /// <param name="form">The form.</param>
        /// <param name="controlId">The control id.</param>
        /// <param name="text">The text.</param>
        public static void Text(Form form, string controlId, string text)
        {
            new Finder<Control>(controlId, form).Find().Text = text;
        }
    }
}
