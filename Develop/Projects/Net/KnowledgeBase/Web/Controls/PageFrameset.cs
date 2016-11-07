#region Version & Copyright
/* 
 * $Id: PageFrameset.cs 19829 2007-08-13 11:52:42Z svd $ 
 *
 * <:COPYRIGHT_LICENSE:>
 */
#endregion

using System;
using System.ComponentModel;
using System.Web.UI;
using System.Web.UI.WebControls;

using Savchin.Drawing;

namespace KnowledgeBase.Controls
{
    public class PageFrameset : WebControl
    {
        public const string ContentFrameName = "ContentFrame";
        public const string NavigationFrameName = "NavigationFrame";

        private string pageUrl;
        private int _navigationPanelWidth = 300;

        /// <summary>
        /// Gets or sets the page URL.
        /// </summary>
        /// <value>The page URL.</value>
        [Category("Appearance"),
         Description("PageUrl"),
         Themeable(false)]
        public string PageUrl
        {
            get { return pageUrl; }
            set { pageUrl = value; }
        }


        /// <summary>
        /// Gets or sets navigation panel with
        /// </summary>
        /// <value>The page URL.</value>
        [Category("Appearance"),
         Description("Width of navigation panel"),
         Themeable(false)]
        public int NavigatePanelWidth
        {
            get { return _navigationPanelWidth;  }
            set { _navigationPanelWidth = value; }
        }


        /// <summary>
        /// Sends server control content to a provided <see cref="T:System.Web.UI.HtmlTextWriter"></see> object, which writes the content to be rendered on the client.
        /// </summary>
        /// <param name="writer">The <see cref="T:System.Web.UI.HtmlTextWriter"></see> object that receives the server control content.</param>
        protected override void Render(HtmlTextWriter writer)
        {
            writer.Write(
                String.Format(
@"<frameset rows='50px,*' framespacing='0px'  border='0'  >
        <frame name='HeaderFrame' src='HeaderPage.aspx' noresize='noresize' scrolling='no' frameborder='0' marginwidth ='0' marginheight ='0' border='0' />
        <frameset  framespacing='5px' cols='{2},*'  border='5' bordercolor='{0}' >
            <frame name='" + NavigationFrameName + @"' src='NavigationPage.aspx' frameborder='1' bordercolor='{0}'   />
            <frame name='" + ContentFrameName + @"' src='{1}' frameborder='0' scrolling='no'  />
        </frameset>
    </frameset>", ConverterColor.ToHTMLColor(BorderColor), PageUrl, NavigatePanelWidth)
 );
        }
    }
}
