#region Version & Copyright
/* 
 * $Id: OutlookPanelBar.cs 18768 2007-07-10 08:41:32Z ais $ 
 *
 * <:COPYRIGHT_LICENSE:>
 */
#endregion

using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI.WebControls;
using System.Web.UI;


namespace Savchin.Web.UI
{
    /// <summary>
    /// Class for outlook panel bar
    /// </summary>
    [ToolboxData("<{0}:OutlookPanelBar runat=\"server\"></{0}:OutlookPanelBar>")]
    public class OutlookPanelBar : Panel
    {
        #region Constants

        private const string ShowSelectionFuncName       = "ShowButtonSelection";
        private const string RemoveSelectionFuncName     = "RemoveButtonSelection";
        private const string ShowLinkSelectionFuncName   = "ShowLinkSelection";
        private const string RemoveLinkSelectionFuncName = "RemoveLinkSelection";
        private const string OnSplitterDownFuncName      = "OnSplitterDown";
        private const string OnClickFuncName             = "OnClick";
        private const string OnPanelClickedPropertyName  = "OnPanelClicked";
        private const string OnPanelClickingPropertyName = "OnPanelClicking";
        private const string RemoveTextSelectionFuncName = "RemoveTextSelection";

        private const string _selectedPanelHF  = "SelectedPanelHiddenField";
        private const string _visibleButtonsHF = "VisibleButtonsHiddenField";

        public const string SelectedIndexCookieName = "OutlookPanelBarPanelIndex";
        public const string VisibleButtonCountCookieName = "OutlookPanelBarButtonCount";

        #endregion

        #region Fields

        private List<OutlookContentPanel> _contentPanels = new List<OutlookContentPanel>();

        #endregion

        #region Properties

        #region Css classes

        /// <summary>
        /// Gets or sets cotainer table style
        /// </summary>
        public string ContainerCssClass
        {
            get { return ViewState["ContainerCssClass"] as string; }
            set { ViewState["ContainerCssClass"] = value; }
        }

        /// <summary>
        /// Gets or sets splitter css style
        /// </summary>
        public string SplitterCssClass
        {
            get { return ViewState["SplitterCssClass"] as string; }
            set { ViewState["SplitterCssClass"] = value; }
        }

        /// <summary>
        /// Gets or sets css class of title
        /// </summary>
        public string TitleCssClass
        {
            get { return ViewState["TitleCssClass"] as string; }
            set { ViewState["TitleCssClass"] = value; }
        }

        /// <summary>
        /// Gets or sets panel button css class
        /// </summary>
        public string ButtonCssClass
        {
            get { return ViewState["PanelButtonCssClass"] as string; }
            set { ViewState["PanelButtonCssClass"] = value; }
        }

        /// <summary>
        /// Gets or sets button selection Css class
        /// </summary>
        public string ButtonSelectionCssClass
        {
            get { return ViewState["ButtonSelectionCssClass"] as string; }
            set { ViewState["ButtonSelectionCssClass"] = value; } 
        }

        /// <summary>
        /// Gets or sets quick links css class
        /// </summary>
        public string QuickLinksCssClass
        {
            get { return ViewState["QuickLinksCssClass"] as string; }
            set { ViewState["QuickLinksCssClass"] = value; }
        }

        /// <summary>
        /// Gets or sets class class of slection link highlighting
        /// </summary>
        public string QuickLinksSelectionCssClass
        {
            get { return ViewState["QuickLinksSelectionCssClass"] as string; }
            set { ViewState["QuickLinksSelectionCssClass"] = value; }
        }

        /// <summary>
        /// Gets or sets panel button css style
        /// </summary>
        public string TextCssClass
        {
            get { return ViewState["TextCssStyle"] as string; }
            set { ViewState["TextCssStyle"] = value; }
        }

        /// <summary>
        /// Gets or sets big image css class
        /// </summary>
        public string BigImageCssClass
        {
            get { return ViewState["BigImageCssClass"] as string; }
            set { ViewState["BigImageCssClass"] = value; }
        }

        /// <summary>
        /// Gets or sets image css class
        /// </summary>
        public string SmallImageCssClass
        {
            get { return ViewState["SmallImageCssClass"] as string; }
            set { ViewState["SmallImageCssClass"] = value; }
        }

        #endregion

        /// <summary>
        /// Gets reference to content panels
        /// </summary>
        public IEnumerable<OutlookContentPanel> ContentPanels
        {
            get 
            { 
                foreach (Control panel in Controls)
                {
                    if (panel is OutlookContentPanel)
                        yield return panel as OutlookContentPanel;
                }
            }
        }

        /// <summary>
        /// Gets panel count
        /// </summary>
        private int ContentPanelCount
        {
            get
            {
                int count = 0;

                foreach (Control panel in Controls)
                {
                    if (panel is OutlookContentPanel)
                        count++;
                }
                return count;
            }
        }

        #region Selection panel index

        /// <summary>
        /// Specifies if selected panel should be searched in cookie
        /// </summary>
        public bool UseCookieForPanelIndex
        {
            get
            {
                if (ViewState["UseCookieForSelection"] == null)
                    return false;
                return (bool)ViewState["UseCookieForSelection"];
            }
            set
            {
                ViewState["UseCookieForSelection"] = value;
            }
        }

        /// <summary>
        /// Gets panel selected index value from cookie
        /// </summary>
        private string CookiePanelIndexValue
        {
            get 
            {
                if (Page.Request.Cookies[SelectedIndexCookieName] == null)
                    return null;
                return Page.Request.Cookies[SelectedIndexCookieName].Value; 
            }
        }

        /// <summary>
        /// Gets value of selected panel index from Form parameters (hidden field)
        /// </summary>
        private string FormPanelIndexValue
        {
            get { return Page.Request.Form[_selectedPanelHF]; }
        }

        /// <summary>
        /// Gets index of selected panel
        /// </summary>
        public int SelectedPanelIndex
        {
            get 
            {
                if (UseCookieForPanelIndex && !string.IsNullOrEmpty(CookiePanelIndexValue))
                    return int.Parse(CookiePanelIndexValue);
                if (string.IsNullOrEmpty(FormPanelIndexValue))
                    return 0;
                return int.Parse(FormPanelIndexValue); 
            }
        }

        #endregion

        #region Visible button count

        /// <summary>
        /// Get or set value with specified if cookie should be used for storing
        /// visible button count
        /// </summary>
        public bool UseCookieForVisibleButtonCount
        {
            get
            {
                if (ViewState["UseCookieForVisibleButtonCount"] == null)
                    return false;
                return (bool)ViewState["UseCookieForVisibleButtonCount"];
            }
            set
            {
                ViewState["UseCookieForVisibleButtonCount"] = value;
            }
        }

        /// <summary>
        /// Gets the cookie visible button count value.
        /// </summary>
        /// <value>The cookie visible button count value.</value>
        private string CookieVisibleButtonCountValue
        {
            get 
            {
                if (Page.Request.Cookies[VisibleButtonCountCookieName] == null)
                    return null;
                return Page.Request.Cookies[VisibleButtonCountCookieName].Value; 
            }
        }

        /// <summary>
        /// Gets value of visible button count from hidden field
        /// </summary>
        private string FormVisibleButtonCountValue
        {
            get { return Page.Request.Form[_visibleButtonsHF]; }
        }

        /// <summary>
        /// Gets count of visible buttons
        /// </summary>
        public int VisibleButtonCount
        {
            get
            {
                if (UseCookieForVisibleButtonCount && !string.IsNullOrEmpty(CookieVisibleButtonCountValue))
                    return int.Parse(CookieVisibleButtonCountValue);
                if (string.IsNullOrEmpty(FormVisibleButtonCountValue))
                    return ContentPanelCount;
                return int.Parse(FormVisibleButtonCountValue);
            }
        }

        #endregion

        #region Java script properties


        /// <summary>
        /// Gets or sets name of event handler for panel clicking event
        /// </summary>
        public string OnPanelClicking
        {
            get { return ViewState["OnPanelClicking"] as string; }
            set { ViewState["OnPanelClicking"] = value; }
        }

        /// <summary>
        /// Event handler func name
        /// </summary>
        public string OnPanelClicked
        {
            get { return ViewState["OnPanelClicked"] as string; }
            set { ViewState["OnPanelClicked"] = value; }
        }


        /// <summary>
        /// Gets id of splitter
        /// </summary>
        private string SplitterID
        {
            get { return ClientID + "SplitterId"; }
        }

        /// <summary>
        /// Gets name of java script object
        /// </summary>
        private string JsObjectName
        {
            get { return ClientID + "Obj"; }
        }

        /// <summary>
        /// Gets or setts button suffix
        /// </summary>
        private string ButtonSuffix
        {
            get { return ClientID + "ButtonSuffix"; }
        }

        /// <summary>
        /// Gets or sets quick link suffix
        /// </summary>
        private string QuickLinkSuffix
        {
            get { return ClientID + "QuickLinkSuffix"; }
        }

        /// <summary>
        /// Gets title cells suffix
        /// </summary>
        private string TitleSuffix
        {
            get { return ClientID + "TitleSuffix"; }
        }

        /// <summary>
        /// Gets or sets button row suffix
        /// </summary>
        private string ButtonRowSuffix
        {
            get { return ClientID + "ButtonRowSuffix"; }
        }


        private string OnButtonUpHandlerName
        {
            get { return ClientID + "OnButtonUpHandler"; }
        }

        /// <summary>
        /// Gets handler name
        /// </summary>
        private string OnButtonUpHandlerText
        {
            get
            {
                return string.Format(
                    "function {0}(evnt){{ {1}.{2}({3}, {0});}}",
                    OnButtonUpHandlerName,
                    JsObjectName,
                    "OnSplitterUp",
                    OnMoveHandlerName);
            }
        }

        /// <summary>
        /// Gets name of on move handler
        /// </summary>
        private string OnMoveHandlerName
        {
            get { return ClientID + "OnMoveHandler"; }
        }

        /// <summary>
        /// Gets handler name
        /// </summary>
        private string OnMoveHandlerText
        {
            get
            {
                return string.Format(
                    "function {0}(evnt){{ {1}.{2}(evnt);}}",
                    OnMoveHandlerName,
                    JsObjectName,
                    "OnSplitterMove");
            }
        }

        /// <summary>
        /// Gets list of container div ids
        /// </summary>
        private string ContentRowSuffix
        {
            get
            {
                return ClientID + "ContentRowSuffix";
            }
        }

        /// <summary>
        /// Gets object creation java script
        /// </summary>
        private string ObjectCreationScript
        {
            get
            {
                StringBuilder builder = new StringBuilder();

                builder.AppendFormat("var {0} = new OutlookPanelBar();\n", JsObjectName);

                builder.AppendFormat("{0}._buttonCount = {1};\n", JsObjectName, ContentPanelCount);
                builder.AppendFormat("{0}._splitterId = '{1}';\n", JsObjectName, SplitterID);

                builder.AppendFormat("{0}._buttonRowSuffix = '{1}';\n", JsObjectName, ButtonRowSuffix);
                builder.AppendFormat("{0}._buttonSuffix = '{1}';\n", JsObjectName, ButtonSuffix);
                builder.AppendFormat("{0}._quickLinkSuffix = '{1}';\n", JsObjectName, QuickLinkSuffix);
                builder.AppendFormat("{0}._titleSuffix = '{1}';\n", JsObjectName, TitleSuffix);
                builder.AppendFormat("{0}._contentRowSuffix = '{1}';\n", JsObjectName, ContentRowSuffix);

                builder.AppendFormat("{0}._buttonClass = '{1}';\n", JsObjectName, ButtonCssClass);
                builder.AppendFormat("{0}._buttonHightClass = '{1}';\n", JsObjectName, ButtonSelectionCssClass);
                builder.AppendFormat("{0}._quickLinkClass = '{1}';\n", JsObjectName, QuickLinksCssClass);
                builder.AppendFormat("{0}._quickLinkHighClass = '{1}';\n", JsObjectName, QuickLinksSelectionCssClass);

                builder.AppendFormat("{0}._selectedPanelHF = '{1}';\n", JsObjectName, _selectedPanelHF);
                builder.AppendFormat("{0}._visibleButtonHF = '{1}';\n", JsObjectName, _visibleButtonsHF);
                builder.AppendFormat("{0}._useCookieForPanelIndex = {1};\n", JsObjectName, UseCookieForPanelIndex ? "true" : "false");
                builder.AppendFormat("{0}._selectedPanelcookieName = '{1}';\n", JsObjectName, SelectedIndexCookieName);
                builder.AppendFormat("{0}._useCookieForVisibleButtonCount = {1};\n", JsObjectName, UseCookieForVisibleButtonCount ? "true" : "false");
                builder.AppendFormat("{0}._buttonCountCookieName = '{1}';\n", JsObjectName, VisibleButtonCountCookieName);


                if (!string.IsNullOrEmpty(OnPanelClicked))
                    builder.AppendFormat("{0}.{1} = {2};",
                        JsObjectName,
                        OnPanelClickedPropertyName,
                        OnPanelClicked);

                if (!string.IsNullOrEmpty(OnPanelClicking))
                    builder.AppendFormat("{0}.{1} = {2};",
                        JsObjectName,
                        OnPanelClickingPropertyName,
                        OnPanelClicking);

                return builder.ToString();
            }
        }

        #endregion

        #endregion

        #region Public methods

        /// <summary>
        /// Gets string for panel click
        /// </summary>
        /// <param name="panelIndex">Index of panel to click</param>
        /// <returns>Script code</returns>
        public string GetPanelClickScript(int panelIndex)
        {
            return JsObjectName + "." + OnClickFuncName + "(" + panelIndex + ");";
        }

        #endregion

        #region Overriden methods

        /// <summary>
        /// Register object java scripts
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);
            
            RegisterScriptReference();
            RegisterOnMoveHandler();
            RegisterOnButtonUpHander();
            RegisterObjectCreationScript();
            RegisterHiddenFields();
        }

        /// <summary>
        /// Renders control
        /// </summary>
        /// <param name="writer">Output to use</param>
        protected override void Render(HtmlTextWriter writer)
        {
            WriteBeginTag(writer);

            RenderTitles(writer);
            RenderContentDivs(writer);
            RenderSplitter(writer);
            RenderPanelButtons(writer);
            RenderQuickLinks(writer);

            WriteEndTag(writer);
        }

        #endregion

        #region Html code generation

        /// <summary>
        /// Renders title
        /// </summary>
        /// <param name="writer">Writer to use</param>
        private void RenderTitles(HtmlTextWriter writer)
        {
            int index = 0;

            foreach (OutlookContentPanel panel in ContentPanels)
            {
                writer.WriteBeginTag("tr");
                writer.WriteAttribute("id", TitleSuffix + index);

                if (SelectedPanelIndex != index)
                    writer.WriteAttribute("style", "display:none");
                writer.Write(">");
                writer.WriteBeginTag("td");
                writer.WriteAttribute("class", TitleCssClass);
                writer.Write(">");

                panel.RenderTitleHtml(writer, SmallImageCssClass, TitleCssClass);

                writer.WriteEndTag("td");
                writer.WriteEndTag("tr");

                index++;
            }
        }

        /// <summary>
        /// Renders image quick links
        /// </summary>
        /// <param name="writer">Writer to use</param>
        private void RenderQuickLinks(HtmlTextWriter writer)
        {
            int index = 0;

            writer.WriteBeginTag("tr");
            writer.Write(">");
            writer.WriteBeginTag("td");
            writer.WriteAttribute("class", QuickLinksCssClass);
            writer.WriteAttribute("align", "right"); // Can it be moved to css file?
            writer.Write(">");
            writer.Write("<table><tr>");

            foreach (OutlookContentPanel panel in ContentPanels)
            {
                writer.WriteBeginTag("td");
                writer.WriteAttribute("id", QuickLinkSuffix + index);
                writer.WriteAttribute("onmouseover", JsObjectName + "." + ShowLinkSelectionFuncName + "(" + index + ")");
                writer.WriteAttribute("onmouseout", JsObjectName + "." + RemoveLinkSelectionFuncName + "(" + index + ")");
                writer.WriteAttribute("onclick", JsObjectName + "." + OnClickFuncName + "(" + index + ");");

                if (index < VisibleButtonCount)
                    writer.WriteAttribute("style", "display:none");
                writer.Write(">");
                panel.RenderQuickLinkHtml(writer, SmallImageCssClass);   
                writer.WriteEndTag("td");

                index++;
            }

            writer.Write("</tr></table>");
            writer.WriteEndTag("td");
            writer.WriteEndTag("tr");
        }

        /// <summary>
        /// Renders big buttons
        /// </summary>
        /// <param name="writer">Writer to use</param>
        private void RenderPanelButtons(HtmlTextWriter writer)
        {
            int index = 0;

            foreach (OutlookContentPanel panel in ContentPanels)
            {
                writer.WriteBeginTag("tr");
                writer.WriteAttribute("id", ButtonRowSuffix + index);
                if (index >= VisibleButtonCount)
                    writer.WriteAttribute("style", "display:none");
                writer.Write(">");
                writer.WriteBeginTag("td");
                writer.WriteAttribute("id", ButtonSuffix + index);
                writer.WriteAttribute("class", ButtonCssClass);
                writer.WriteAttribute("onmouseover", JsObjectName + "." + ShowSelectionFuncName + "(" + index + ");");
                writer.WriteAttribute("onmouseout", JsObjectName + "." + RemoveSelectionFuncName + "(" + index + ");");
                writer.WriteAttribute("onclick", JsObjectName + "." + OnClickFuncName + "(" + index + ");");
                writer.Write(">");
                panel.RenderButtonHtml(writer, BigImageCssClass, TextCssClass);
                writer.WriteEndTag("td");
                writer.WriteEndTag("tr");

                index++;
            }
        }

        /// <summary>
        /// Generates spliter html code
        /// </summary>
        /// <param name="writer">Output object to use</param>
        private void RenderSplitter(HtmlTextWriter writer)
        {
                writer.WriteBeginTag("tr");
                writer.WriteAttribute("id", SplitterID);
                writer.WriteAttribute("onmousedown", JsObjectName + "." + OnSplitterDownFuncName + 
                    "(" + OnMoveHandlerName + ", " + OnButtonUpHandlerName + ")");
                writer.Write(">");

                writer.WriteBeginTag("td");
                writer.WriteAttribute("class", SplitterCssClass);
                //writer.WriteAttribute("align", "center");
                // <img id="hideBigButtons" class="splitterShow" src="App_Themes/Blue/Images/splitterDots.gif"/>
                // <img id="showBigButtons" class="splitterShow" style="display:none" src="App_Themes/Blue/Images/splitterDots3.GIF"/>
                writer.WriteEndTag("td");
                writer.WriteEndTag("tr");
        }

        /// <summary>
        /// Adds container divs
        /// </summary>
        /// <param name="writer">Writer object to use</param>
        private void RenderContentDivs(HtmlTextWriter writer)
        {
            int index = 0;

            foreach (OutlookContentPanel panel in ContentPanels)
            {
                writer.WriteBeginTag("tr");
                writer.WriteAttribute("id", ContentRowSuffix + index);
                if (SelectedPanelIndex != index)
                    writer.WriteAttribute("style", "display:none");
                writer.Write("><td>");
                panel.RenderControl(writer);
                writer.WriteLine("<td></tr>");

                index++;
            }
        }

        /// <summary>
        /// We use table as container for navigation panel
        /// </summary>
        /// <param name="writer">Output to use</param>
        private void WriteBeginTag(HtmlTextWriter writer)
        {
            writer.WriteBeginTag("table");
            writer.WriteAttribute("cellpadding", "0");
            writer.WriteAttribute("cellspacing", "0");
            writer.WriteAttribute("width", Width.ToString());
            writer.WriteAttribute("height", Height.ToString());
            writer.WriteAttribute("class", ContainerCssClass);
            writer.WriteAttribute("onselectstart", JsObjectName + "." + RemoveTextSelectionFuncName + "()");
            writer.WriteLine(">");
        }

        /// <summary>
        /// Closing table tag rendering
        /// </summary>
        /// <param name="writer">Writer to use</param>
        private void WriteEndTag(HtmlTextWriter writer)
        {
            writer.WriteEndTag("table");
        }


        #endregion

        #region Java script generation registration

        /// <summary>
        /// Registers client scripts
        /// </summary>
        private void RegisterScriptReference()
        {
            Type type = GetType();

            if (Page.ClientScript.IsClientScriptBlockRegistered(type,"OutlookPanelBarScript"))
                return;

            Page.ClientScript.RegisterClientScriptBlock(type,"OutlookPanelBarScript",string.Empty);

            Page.ClientScript.RegisterClientScriptResource(this.GetType(),EmbeddedResources.JsOutlookPanelBar);
        }

        /// <summary>
        /// Registers script for object creation
        /// </summary>
        private void RegisterObjectCreationScript()
        {
            Page.ClientScript.RegisterStartupScript(
                this.GetType(),
                "ObjectCreation",
                ObjectCreationScript,
                true);
        }

        /// <summary>
        /// Registers handler text
        /// </summary>
        private void RegisterOnMoveHandler()
        {
            Page.ClientScript.RegisterClientScriptBlock(
                this.GetType(),
                OnMoveHandlerName,
                OnMoveHandlerText,
                true);
        }

        /// <summary>
        /// Registers on button up hander
        /// </summary>
        private void RegisterOnButtonUpHander()
        {
            Page.ClientScript.RegisterClientScriptBlock(
                this.GetType(),
                OnButtonUpHandlerName,
                OnButtonUpHandlerText,
                true);
        }

        /// <summary>
        /// Registers hidden fields
        /// </summary>
        private void RegisterHiddenFields()
        {
            Page.ClientScript.RegisterHiddenField(
                _selectedPanelHF,
                SelectedPanelIndex.ToString());
            Page.ClientScript.RegisterHiddenField(
                _visibleButtonsHF,
                VisibleButtonCount.ToString());
        }

        #endregion
    }
}
