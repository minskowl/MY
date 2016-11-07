#region Version & Copyright
/* 
 * $Id: OutlookContentPanel.cs 24818 2007-11-30 15:56:43Z svd $ 
 *
 * <:COPYRIGHT_LICENSE:>
 */
#endregion

using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI.WebControls;
using System.Web.UI;
using System.Web;

namespace Savchin.Web.UI
{
    /// <summary>
    /// Content panel for outlook bar control
    /// </summary>
    public class OutlookContentPanel : Panel
    {
        #region Properties

        /// <summary>
        /// Gets or sets panel name
        /// </summary>
        public string PanelName
        {
            get { return ViewState["PanelName"] as string; }
            set { ViewState["PanelName"] = value; }
        }

        /// <summary>
        /// Gets or sets big image url
        /// </summary>
        public string BigImageUrl
        {
            get { return ViewState["BigImageUrl"] as string; }
            set { ViewState["BigImageUrl"] = value; }
        }

        /// <summary>
        /// Gets or sets small image url
        /// </summary>
        public string SmallImageUrl
        {
            get { return ViewState["SmallImageUrl"] as string; }
            set { ViewState["SmallImageUrl"] = value; }
        }

        #endregion

        #region Html generation

        /// <summary>
        /// Renders panel button html
        /// </summary>
        /// <param name="writer">Writer to use</param>
        /// <param name="bigImageCss">The big image CSS.</param>
        /// <param name="textCss">The text CSS.</param>
        public void RenderButtonHtml(HtmlTextWriter writer, string bigImageCss, string textCss)
        {
            writer.WriteBeginTag("table");
            writer.WriteAttribute("cellpadding", "0");
            writer.WriteAttribute("cellspacing", "0");

            writer.Write(">");

            writer.Write("<tr><td>&nbsp;&nbsp;");
            writer.WriteBeginTag("img");
            writer.WriteAttribute("src", BigImageUrl ?? string.Empty);
            writer.WriteAttribute("class", bigImageCss);
            writer.Write("/>");

            writer.Write("</td><td class=\"{0}\"  >&nbsp;&nbsp;", textCss);
            writer.WriteEncodedText(PanelName ?? string.Empty);
            writer.Write("</td></tr>");

            writer.WriteEndTag("table");
        }

        /// <summary>
        /// Generates quck link html code
        /// </summary>
        /// <param name="writer">Writer to use</param>
        public void RenderQuickLinkHtml(HtmlTextWriter writer, string smallImageCss)
        {
            writer.WriteBeginTag("img");
            writer.WriteAttribute("alt", HttpUtility.HtmlEncode(PanelName));
            writer.WriteAttribute("class", smallImageCss);
            writer.WriteAttribute("src", SmallImageUrl);
            writer.Write("/>");
        }

        /// <summary>
        /// Renders title html code
        /// </summary>
        /// <param name="writer">Writer to use</param>
        /// <param name="smallImageCss">The small image CSS.</param>
        /// <param name="titleCssClass">The title CSS class.</param>
        public void RenderTitleHtml(HtmlTextWriter writer, string smallImageCss, string titleCssClass)
        {
            writer.WriteBeginTag("table");
            writer.WriteAttribute("cellpadding", "0");
            writer.WriteAttribute("cellspacing", "0");

            writer.Write(">");

            writer.Write("<tr><td><img src='" + ControlHelper.GetThemebleUrl("PanelImages/title_lft.gif", Page.Theme) + "' height='25' width='5'></td><td>&nbsp;&nbsp;");
            writer.WriteBeginTag("img");
            writer.WriteAttribute("src", SmallImageUrl ?? string.Empty);
            writer.WriteAttribute("class", smallImageCss);
            writer.Write("/>");

            writer.Write("</td><td class='" + titleCssClass + "'>&nbsp;&nbsp;");
            writer.WriteEncodedText(PanelName ?? string.Empty);
            writer.Write("</td></tr>");

            writer.WriteEndTag("table");
        }

        #endregion
    }
}
