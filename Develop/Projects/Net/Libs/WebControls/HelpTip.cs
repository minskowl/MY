#region Version & Copyright
/* 
 * $Id$ 
 *
 * <:COPYRIGHT_LICENSE:>
 */
#endregion

using System;
using System.ComponentModel;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Savchin.Web;


[assembly: WebResource(Savchin.Web.UI.EmbeddedResources.JsHelpTip, Savchin.Web.UI.EmbeddedResources.JavaScript)]

namespace Savchin.Web.UI
{
    internal static partial class EmbeddedResources
    {
        public const string JsHelpTip = namespaceName + "HelpTip.js";
    }
    /// <summary>
    /// Provides popup tool tip which contains inner html code of control
    /// </summary>
    [PersistChildren(true)]
    [ParseChildren(false)]
    [ToolboxData("<{0}:HelpTip runat=\"server\"></{0}:HelpTip>")]
    public class HelpTip : WebControl
    {

        private const string _linkTemplate = "<a id=\"{0}\" name=\"{0}\" href=\"#\" onclick=\"showhint({1}, this, event, '{2}',{3});\">{4}</a>";
        private string _helpTipHtml = string.Empty;


        #region Properties

        /// <summary>
        /// Width of tooltip popup menu
        /// </summary>
        [Category("Appearance")]
        public string ToolTipWidth
        {
            get
            {
                if (ViewState["ToolTipWidth"] == null)
                    return "300px";
                return ViewState["ToolTipWidth"] as string;
            }
            set { ViewState["ToolTipWidth"] = value; }
        }

        /// <summary>
        /// Delay interval
        /// </summary>
        [Category("Appearance")]
        public int Delay
        {
            get
            {
                if (ViewState["PopupDelay"] == null)
                    return 1000;
                return (int)ViewState["PopupDelay"];
            }
            set { ViewState["PopupDelay"] = value * 1000; }
        }


        /// <summary>
        /// Gets or sets the tool tip control image URL.
        /// </summary>
        /// <value>The image URL.</value>
        public string ImageUrl
        {
            get
            {
                if (ViewState["ImageUrl"] == null)
                    return string.Empty;
                return ViewState["ImageUrl"] as string;
            }
            set
            {
                ViewState["ImageUrl"] = value;
            }
        }

        /// <summary>
        /// Text of link
        /// </summary>
        [Category("Appearance")]
        public string LinkText
        {
            get
            {
                object o = ViewState["LinkText"];
                if (o == null && !DesignMode)
                {
                    return string.Format("<img src=\"{0}\" border=\"0\" />",
                        ControlHelper.GetThemebleUrl(ImageUrl, Page.Theme));
                }
                return (string)o;
            }
            set { ViewState["LinkText"] = value; }
        }



        /// <summary>
        /// Gets or sets help tip html code. Is this value
        /// is specified child control html will be ignored
        /// </summary>
        public string HelpTipHtml
        {
            get { return _helpTipHtml; }
            set { _helpTipHtml = value; }
        }

        #endregion

        #region Overriden methods

        /// <summary>
        /// Renders the HTML opening tag of the control to the specified writer. This method is used primarily by control developers.
        /// </summary>
        /// <param name="writer">A <see cref="T:System.Web.UI.HtmlTextWriter"/> that represents the output stream to render HTML content on the client.</param>
        public override void RenderBeginTag(HtmlTextWriter writer)
        { }

        /// <summary>
        /// Renders the HTML closing tag of the control into the specified writer. This method is used primarily by control developers.
        /// </summary>
        /// <param name="writer">A <see cref="T:System.Web.UI.HtmlTextWriter"/> that represents the output stream to render HTML content on the client.</param>
        public override void RenderEndTag(HtmlTextWriter writer)
        { }

        /// <summary>
        /// Raises the <see cref="E:System.Web.UI.Control.PreRender"/> event.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs"/> object that contains the event data.</param>
        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);
            if (!DesignMode)
            {
                ((PageEx)Page).PageIncludes.AddJavaScript(typeof(HelpTip), EmbeddedResources.JsHelpTip);
            }
        }


        /// <summary>
        /// Peforms default and converts resulted html into tool tip
        /// </summary>
        /// <param name="writer"></param>
        protected override void Render(HtmlTextWriter writer)
        {
            if (!string.IsNullOrEmpty(HelpTipHtml))
            {
                writer.Write(MakeToolTip(HelpTipHtml));
                return;
            }

            StringWriter innerWriter = new StringWriter();
            HtmlTextWriter stubWriter = new HtmlTextWriter(innerWriter);

            base.Render(stubWriter);

            writer.Write(MakeToolTip(innerWriter.ToString()));
        }



        #endregion

        /// <summary>
        /// Converts html into tool tip
        /// </summary>
        /// <param name="srcHtml">Html code to convert</param>
        /// <returns>Tooltip html</returns>
        private string MakeToolTip(string srcHtml)
        {
            return string.Format(_linkTemplate,
                ClientID,
                HttpUtility.HtmlEncode(JavaScriptBuilder.ConvertToJavaScriptLine(srcHtml)),
                ToolTipWidth,
                Delay,
                LinkText);
        }
    }
}
