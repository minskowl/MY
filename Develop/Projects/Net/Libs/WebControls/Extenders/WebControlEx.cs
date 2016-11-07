using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI.WebControls;

namespace Savchin.Web.UI
{
    public class WebControlEx : WebControl
    {
        /// <summary>
        /// Gets the web resource URL.
        /// </summary>
        /// <param name="resourceName">Name of the resource.</param>
        /// <returns></returns>
        protected string GetWebResourceUrl(string resourceName)
        {
            return Page == null ? null : 
                Page.ClientScript.GetWebResourceUrl(this.GetType(), resourceName);
        }
    }
}
