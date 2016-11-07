#region Version & Copyright
/* 
 * $Id: AJAXHandler.cs 18979 2007-07-17 14:37:25Z svd $ 
 *
 * <:COPYRIGHT_LICENSE:>
 */
#endregion
using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Web.UI;

namespace Savchin.Web.UI
{
    public delegate string AJAXHandlerDelegate(DataRequestEventArgs args);

    public class AJAXHandler : Control
    {
        private AJAXHandlerDelegate contentProcessor;

        /// <summary>
        /// Gets or sets the handler.
        /// </summary>
        /// <value>The handler.</value>
        public AJAXHandlerDelegate ContentProcessor
        {
            get { return contentProcessor; }
            set { contentProcessor = value; }
        }

        private string action = null;
        /// <summary>
        /// Gets the action.
        /// </summary>
        /// <value>The action.</value>
        public string Action
        {
            get
            {
                if (action == null)
                    action = Page.Request["action"];
                return action;
            }
        }

        /// <summary>
        /// Gets the action URL.
        /// </summary>
        /// <param name="actionName">Name of the action.</param>
        /// <returns></returns>
        public string GetActionURL(string actionName)
        {
            return PageEx.GetAJAXHadlerURL(this) + "&action=" + HttpUtility.UrlEncode(actionName);
        }
    }
}
