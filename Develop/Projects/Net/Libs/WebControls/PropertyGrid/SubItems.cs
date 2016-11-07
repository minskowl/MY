using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI;

namespace Savchin.Web.UI.PropertyGrid
{
    class SubItems : Control
    {
        protected override void Render(HtmlTextWriter writer)
        {
            writer.Write(@"<div id=""{0}"" style=""display:none"">", Parent.ClientID);
            RenderChildren(writer);
            writer.Write("</div>");
        }
    }
}
