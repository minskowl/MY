using System;

namespace Savchin.Forms
{
    public static class ExceptionHelper
    {
        /// <summary>
        /// Shows the report.
        /// </summary>
        /// <param name="exception">The exception.</param>
        /// <param name="title">The title.</param>
        /// <param name="message">The message.</param>
        public static ExceptionForm ShowReport(this Exception exception, string title, string message)
        {
            return ExceptionForm.ShowException(title, message, exception);
        }
    }
}
