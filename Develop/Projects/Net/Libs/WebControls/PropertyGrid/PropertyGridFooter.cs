#region Version & Copyright
/* 
 * $Id: PropertyGridFooter.cs 389 2008-06-09 14:03:35Z svd $ 
 *
 * <:COPYRIGHT_LICENSE:>
 */
#endregion
using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI;

namespace Savchin.Web.UI.PropertyGrid
{
    /// <summary>
    /// PropertyGridFooter
    /// </summary>
    class PropertyGridFooter : GridControl
    {
        /// <summary>
        /// Raises the <see cref="E:System.Web.UI.Control.Load"/> event.
        /// </summary>
        /// <param name="e">The <see cref="T:System.EventArgs"/> object that contains the event data.</param>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            ID = "foot";
        }

        /// <summary>
        /// Sends server control content to a provided <see cref="T:System.Web.UI.HtmlTextWriter"/> object, which writes the content to be rendered on the client.
        /// </summary>
        /// <param name="writer">The <see cref="T:System.Web.UI.HtmlTextWriter"/> object that receives the server control content.</param>
        protected override void Render(HtmlTextWriter writer)
        {
            writer.Write(@"<div class=""PGF PGF_{2}""{1}><span id=""{0}""></span></div>", ClientID,
              ParentGrid.ShowHelp ? "" : "style='display:none'", ParentGrid.ClientID);
            writer.Write(@"
<div class=""PGF2 PGF2_{1}"">
<span style=""float:right;margin-right:1px""><img id=""{1}_active"" src=""{2}active.gif""" +
    @" style=""display:none"" title=""Busy..."" alt=""BUSY""/>{0}<img style=""cursor:pointer""" +
    @" src=""{2}info.gif"" onclick=""PGShowInfo()"" title=""Visit author's blog"" alt=""INFO""/></span>
<span style=""float:left;margin-left:2px""><img src=""{2}pg.gif"" alt=""xacc.propertygrid""/></span>
</div>", ParentGrid.ReadOnly ?
              @"<img src=""" + ParentGrid.ResPath + @"lock.gif"" title=""Values cannot be modified"" alt=""LOCK""/>" : "",
              Parent.ClientID, ParentGrid.ResPath);
        }
    }

}
