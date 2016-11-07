using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;
using System.Web.UI;

namespace Savchin.Web.UI.TreeView
{
    /// <summary>
    /// A container for the context menu template of the OdcTreeView.
    /// </summary>
    public class OdcTreeViewContextMenuContainer:WebControl,INamingContainer
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OdcTreeViewContextMenuContainer"/> class.
        /// </summary>
        public OdcTreeViewContextMenuContainer()
            : base(HtmlTextWriterTag.Span)
        {
            // make the control hidden with no gap:
            Style.Add(HtmlTextWriterStyle.Display,"none");
            Style.Add(HtmlTextWriterStyle.Visibility,"collapse");
        }
    }
}
