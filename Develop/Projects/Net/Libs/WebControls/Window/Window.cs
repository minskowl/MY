

using System;
using System.ComponentModel;
using System.Security.Permissions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

[assembly: WebResource(Savchin.Web.UI.EmbeddedResources.JsWindow, Savchin.Web.UI.EmbeddedResources.JavaScript, PerformSubstitution = true)]
[assembly: WebResource(Savchin.Web.UI.EmbeddedResources.CssWindow, Savchin.Web.UI.EmbeddedResources.Css, PerformSubstitution = true)]




namespace Savchin.Web.UI
{
    internal static partial class EmbeddedResources
    {
        internal const string JsWindow = namespaceName + "Window.Window.js";
        internal const string CssWindow = namespaceName + "Window.Window.css";
    }
    [
    PersistChildren(true),
    ToolboxData("<{0}:Window runat=\"server\" Width=\"125px\" Height=\"50px\"> </{0}:Window>"),
    ParseChildren(false),
    AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal),
    AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)
    ]
    public class Window : WebControl
    {
        private static readonly string imageUrl = ImagePathProvider.CommonImagesUrl + "window/";

        #region Properties
        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="Window"/> is hide.
        /// </summary>
        /// <value><c>true</c> if hide; otherwise, <c>false</c>.</value>
        [Category("Appearance")]
        [Themeable(false)]
        [DefaultValue(false)]
        public virtual bool Hide
        {
            get
            {
                object obj1 = ViewState["Hide"];
                if (obj1 != null)
                {
                    return (bool)obj1;
                }
                return false;
            }
            set
            {
                ViewState["Hide"] = value;
            }
        }
        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        /// <value>The title.</value>
        [Category("Appearance")]
        [Themeable(false)]
        [DefaultValue(false)]
        public virtual string Title
        {
            get
            {
                return (string)ViewState["Title"];
            }
            set
            {
                ViewState["Title"] = value;
            }
        }
        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="Window"/> is resizeble.
        /// </summary>
        /// <value><c>true</c> if resizeble; otherwise, <c>false</c>.</value>
        [Category("Behavior")]
        [Themeable(false)]
        [DefaultValue(false)]
        public virtual bool Resizeble
        {
            get
            {
                object obj1 = ViewState["Resizeble"];
                if (obj1 != null) return (bool)obj1;
                return false;
            }
            set
            {
                ViewState["Resizeble"] = value;
            }
        }
        /// <summary>
        /// Gets or sets the close button ID.
        /// </summary>
        /// <value>The close button ID.</value>
        [Category("Behavior")]
        [Themeable(false)]
        public virtual string CloseButtonID
        {
            get
            {
                return (string)ViewState["CloseButtonID"];
            }
            set
            {
                ViewState["CloseButtonID"] = value;
            }
        }
        [Category("Layout"), Description("Panel_HorizontalAlign"), DefaultValue(0)]
        public virtual HorizontalAlign HorizontalAlign
        {
            get
            {
                object obj2 = this.ViewState["HorizontalAlign"];
                if (obj2 != null)
                {
                    return (HorizontalAlign)obj2;
                }

                return HorizontalAlign.NotSet;
            }
            set
            {
                if ((value < HorizontalAlign.NotSet) || (value > HorizontalAlign.Justify))
                {
                    throw new ArgumentOutOfRangeException("value");
                }
                this.ViewState["HorizontalAlign"] = value;
            }
        }
        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="Window"/> is themeable.
        /// </summary>
        /// <value><c>true</c> if themeable; otherwise, <c>false</c>.</value>
        [Category("Layout")]
        public virtual bool Themeable
        {
            get
            {
                object obj1 = ViewState["Themeable"];
                if (obj1 != null) return (bool)obj1;
                return false;
            }
            set
            {
                ViewState["Themeable"] = value;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="Window"/> is single.
        /// </summary>
        /// <value><c>true</c> if single; otherwise, <c>false</c>.</value>
        [Category("Behavior")]
        [Themeable(false)]
        [DefaultValue(false)]
        public virtual bool Single
        {
            get
            {
                object obj1 = ViewState["Single"];
                if (obj1 != null) return (bool)obj1;
                return false;
            }
            set
            {
                ViewState["Single"] = value;
            }
        }

        /// <summary>
        /// Gets or sets the left.
        /// </summary>
        /// <value>The left.</value>
        [Category("Layout"), DefaultValue(typeof(Unit), ""), Description("Left postion")]
        public virtual Unit Left
        {
            get
            {
                object o = ViewState["Left"];
                if (o == null) return Unit.Empty;
                return (Unit)o;
            }
            set
            {
                ViewState["Left"] = value;
            }
        }

        /// <summary>
        /// Gets or sets the left.
        /// </summary>
        /// <value>The left.</value>
        [Category("Layout"), DefaultValue(typeof(Unit), ""), Description("Left postion")]
        public virtual Unit Top
        {
            get
            {
                object o = ViewState["Top"];
                if (o == null) return Unit.Empty;
                return (Unit)o;
            }
            set
            {
                ViewState["Top"] = value;
            }
        }

        /// <summary>
        /// Gets the name of the JS object.
        /// </summary>
        /// <value>The name of the JS object.</value>
        public string JSObjectName
        {
            get { return ClientID + "Obj"; }
        }
        /// <summary>
        /// Gets the J script close.
        /// </summary>
        /// <value>The J script close.</value>
        public string JScriptHide
        {
            get { return JSObjectName + ".Hide();"; }
        }
        public string JScriptClose
        {
            get
            {
                var closeButtonID = CloseButtonID;
                if (string.IsNullOrEmpty(closeButtonID)) return JScriptHide;



                var closeButton = FindControl(closeButtonID);
                if (closeButton == null) return JScriptHide;
                return this.Page.ClientScript.GetPostBackEventReference(closeButton, string.Empty);
                //  return string.Format("fireClick($(\"{0}\"));", closeButton.ClientID);

            }
        }
        /// <summary>
        /// Gets the J script show.
        /// </summary>
        /// <value>The J script show.</value>
        public string JScriptShow
        {
            get { return (Single) ? "hideAllDivs(); " + JSObjectName + ".Show();" : JSObjectName + ".Show();"; }
        }
        #endregion

        #region Private Properties
        /// <summary>
        /// Gets the theme image URL.
        /// </summary>
        /// <value>The theme image URL.</value>
        private string BaseImageUrl
        {
            get
            {
                return Themeable ? ControlHelper.GetThemebleUrl("window/", Page.Theme) :
                    ControlHelper.GetFullImageUrl("~/Images/Window/", Page);
            }
        }
        /// <summary>
        /// Gets the window CSS style.
        /// </summary>
        /// <value>The window CSS style.</value>
        private string WindowCssStyle
        {
            get
            {
                string style = string.Empty;

                if (Hide) style += "display: none; ";
                if (Left != Unit.Empty) style += "left: " + Left + "; ";
                if (Top != Unit.Empty) style += "top: " + Top + "; ";
                if (Width != Unit.Empty) style += "width: " + Width + "; ";
                return style;
            }
        }
        /// <summary>
        /// Gets the title HTML.
        /// </summary>
        /// <value>The title HTML.</value>
        private string TitleHtml
        {
            get
            {
                string hideIFrame = (((PageEx)Page).Browser == BrowserType.IE)
                                        ? "<IFRAME style='BORDER-RIGHT: 0px; BORDER-TOP: 0px; Z-INDEX: 100; LEFT: 0px; BORDER-LEFT: 0px; BORDER-BOTTOM: 0px; POSITION: absolute; TOP: 0px; BACKGROUND-COLOR: #ffffff' frameborder='0'></IFRAME>"
                                        : string.Empty;

                return hideIFrame + string.Format(@"
<table  onmousedown ='{1}.initMove(event);'  class='dhtmlgoodies_window_top' cellpadding='0' cellspacing='0' border='0'>
<tr>
<td style='width: 5px;'><img src='{0}title_lft.gif'/></td>
<td style='background-image: url({0}title_bg.gif);background-repeat: repeat-x;' class='dhtmlgoodies_window_title' >&nbsp;{3}</td>
<td style='width: 26px; cursor: pointer; background-image: url({0}title_bg.gif);background-repeat: repeat-x;'>{2}</td>
<td style='width: 5px;'><img src='{0}title_rght.gif' /></td>
</tr>
</table>", BaseImageUrl, JSObjectName, ButtonHtml, Title);
            }
        }
        /// <summary>
        /// Gets the button HTML.
        /// </summary>
        /// <value>The button HTML.</value>
        private string ButtonHtml
        {
            get
            {

                return string.Format(@"<img id=""buttonMinimize_{2}"" class=""minimizeButton"" src=""{0}minimize.gif"" onclick =""{1}.Minimize(event);"" /><img id=""buttonClose_{2}"" class=""closeButton"" src=""{0}close.gif"" onclick =""{3}"" />",
                        imageUrl,
                        JSObjectName, ClientID,
                        JScriptClose);
            }
        }
        /// <summary>
        /// Gets the resize image HTML.
        /// </summary>
        /// <value>The resize image HTML.</value>
        private string ResizeImageHTML
        {
            get
            {
                return string.Format("<img id='imgResize_{1}' class='resizeImage' src='{0}bottom_right.gif' onmousedown ='{2}.initResize(event);' /> ",
                        imageUrl, ClientID, JSObjectName);
            }
        }

        /// <summary>
        /// Gets the status bar HTML.
        /// </summary>
        /// <value>The status bar HTML.</value>
        private string StatusBarHtml
        {
            get
            {
                return "<div class='dhtmlgoodies_window_bottom'>" + (Resizeble ? ResizeImageHTML : string.Empty) + "</div>";
            }
        }
        /// <summary>
        /// Gets the content div.
        /// </summary>
        /// <value>The content div.</value>
        private string ContentDiv
        {
            get
            {
                string style = string.Empty;
                if (Height != Unit.Empty)
                    style += "height: " + Height + "; ";

                HorizontalAlign horizontalAlign = HorizontalAlign;
                if (horizontalAlign != HorizontalAlign.NotSet)
                {
                    TypeConverter converter = TypeDescriptor.GetConverter(typeof(HorizontalAlign));

                    style += "text-align: " + converter.ConvertToInvariantString(horizontalAlign).ToLowerInvariant() +
                             "; ";

                }

                return string.Format(@"<div id='windowContent_{1}' class='dhtmlgoodies_windowContent' style='{0}'>",
                        style, ClientID);
            }
        }
        #endregion

        /// <summary>
        /// Raises the <see cref="E:System.Web.UI.Control.Load"/> event.
        /// </summary>
        /// <param name="e">The <see cref="T:System.EventArgs"/> object that contains the event data.</param>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (!DesignMode) RegisterIncludes();

        }

        /// <summary>
        /// Raises the <see cref="E:System.Web.UI.Control.PreRender"/> event.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs"/> object that contains the event data.</param>
        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);

            if (!DesignMode)
            {
                ScriptManager.RegisterStartupScript(
                    Page, typeof(Window), "InitWindow" + ClientID, GetInitializeJsScript(), true);

            }
        }

        /// <summary>
        /// Registers the J scripts.
        /// </summary>
        private void RegisterIncludes()
        {
            var page = ((PageEx)Page);

            ControlHelper.AddCssInclude(Page, GetType(), EmbeddedResources.CssWindow);

            //Note: Test include
            // page.PageIncludes.AddJavaScript(typeof(Window), "Window.js", AppSettings.JavaScriptsUrl + "Window.js");

            page.PageIncludes.AddJavaScript(typeof(Window), EmbeddedResources.JsWindow);


        }

        private string GetInitializeJsScript()
        {
            return String.Format("\n var {1} = new Window('{0}');\n", ClientID, JSObjectName);
        }

        #region Rendering
        /// <summary>
        /// Renders the HTML opening tag of the control to the specified writer. This method is used primarily by control developers.
        /// </summary>
        /// <param name="writer">A <see cref="T:System.Web.UI.HtmlTextWriter"/> that represents the output stream to render HTML content on the client.</param>
        public override void RenderBeginTag(HtmlTextWriter writer)
        {
            if (DesignMode)
            {
                writer.WriteLine("<div> Window Cotrol ID= " + ID);
                return;
            }
            string single = Single ? " single='true' " : string.Empty;

            writer.WriteLine(String.Format("<div id='{1}' class='dhtmlgoodies_window' style='{0}' {2} >", WindowCssStyle, ClientID, single));
            writer.Write(TitleHtml);
            writer.WriteLine(@"<div class=""dhtmlgoodies_windowMiddle"">");
            writer.WriteLine(ContentDiv);

        }
        /// <summary>
        /// Renders the HTML closing tag of the control into the specified writer. This method is used primarily by control developers.
        /// </summary>
        /// <param name="writer">A <see cref="T:System.Web.UI.HtmlTextWriter"/> that represents the output stream to render HTML content on the client.</param>
        public override void RenderEndTag(HtmlTextWriter writer)
        {
            if (DesignMode)
            {
                writer.WriteLine("</div>");
                return;
            }
            writer.Write("</div></div>");
            writer.Write(StatusBarHtml);
            writer.Write("</div>");
        }



        #endregion

    }
}