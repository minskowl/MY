using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Savchin.Wpf.Controls.Core
{
    public static class ExceptionHelper
    {
        /// <summary>
        /// Shows the report.
        /// </summary>
        /// <param name="exception">The exception.</param>
        /// <param name="title">The title.</param>
        public static void ShowReport(this Exception exception, string title)
        {
            ErrorForm.Show(title, exception);
        }
    }
}
