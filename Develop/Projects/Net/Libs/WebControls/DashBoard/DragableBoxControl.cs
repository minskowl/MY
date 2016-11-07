#region Version & Copyright
/* 
 * $Id: DragableBoxControl.cs 18842 2007-07-11 13:00:21Z svd $ 
 *
 * <:COPYRIGHT_LICENSE:>
 */
#endregion
using System;
using System.Collections.Generic;
using System.Security.Permissions;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Savchin.Web.UI
{
    [
    PersistChildren(true),
    ParseChildren(false),
    AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal),
    AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)
    ]
    public class DragableBoxControl : WebControl
    {
        /// <summary>
        /// Gets the name of the control tag. This property is used primarily by control developers.
        /// </summary>
        /// <value></value>
        /// <returns>The name of the control tag.</returns>
        protected override HtmlTextWriterTag TagKey
        {
            get
            {
                return HtmlTextWriterTag.Div;
            }
        }
        /// <summary>
        /// Adds HTML attributes and styles that need to be rendered to the specified <see cref="T:System.Web.UI.HtmlTextWriterTag"/>. This method is used primarily by control developers.
        /// </summary>
        /// <param name="writer">A <see cref="T:System.Web.UI.HtmlTextWriter"/> that represents the output stream to render HTML content on the client.</param>
        protected override void AddAttributesToRender(HtmlTextWriter writer)
        {
            base.AddAttributesToRender(writer);

            Attributes.CssStyle.Add("margin", "0px");
            Attributes.CssStyle.Add("padding", "0px");
            Attributes.CssStyle.Add("float", "left");
        }

        /// <summary>
        /// Renders the contents of the control to the specified writer. This method is used primarily by control developers.
        /// </summary>
        /// <param name="writer">A <see cref="T:System.Web.UI.HtmlTextWriter"/> that represents the output stream to render HTML content on the client.</param>
        protected override void RenderContents(HtmlTextWriter writer)
        {
            writer.Write("<hr style='clear: both; visibility: hidden;'/>");
            base.RenderContents(writer);
        }

    }
}
