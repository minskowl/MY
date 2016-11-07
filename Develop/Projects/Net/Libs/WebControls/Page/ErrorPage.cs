using System;
using log4net;

namespace Savchin.Web.UI
{
    /// <summary>
    /// ErrorPage
    /// </summary>
    public class ErrorPage : PageEx
    {
        /// <summary>
        /// exception
        /// </summary>
        protected Exception exception;
        /// <summary>
        /// pageNotExists
        /// </summary>
        protected bool pageNotExists = false;
        /// <summary>
        /// exceptionType
        /// </summary>
        protected string exceptionType;

        
        /// <summary>
        /// Raises the <see cref="E:System.Web.UI.Control.Init"></see> event to initialize the page.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs"></see> that contains the event data.</param>
        protected override void OnInit(EventArgs e)
        {
            exception = Server.GetLastError();
            if (exception == null)
                return;

            exceptionType = exception.GetType().Name;

            pageNotExists = (exceptionType == "HttpException" &&
                             exception.Message.EndsWith("does not exist.") &&
                             exception.Message.StartsWith("The file"));



            base.OnInit(e);

        }

    }
}
