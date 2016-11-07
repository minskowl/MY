using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI.WebControls;
using System.Web.UI;
using System.Web;

[assembly: WebResource(Savchin.Web.UI.EmbeddedResources.JsTabControlScripts, Savchin.Web.UI.EmbeddedResources.JavaScript)]


namespace Savchin.Web.UI
{
    internal static partial class EmbeddedResources
    {
        internal const string JsTabControlScripts = namespaceName + "TabControl.TabControlScripts.js";
    }
    /// <summary>
    /// Renders outlook table control and all it panels
    /// </summary>
    [ToolboxData("<{0}:TabControl runat=\"server\"></{0}:TabControl>")]
    public class TabControl : Panel
    {
        #region Constants

        private const string _jsObjectClassName = "TabControl";
        private const string _showHotSelFuncName = "ShowHotSelection";
        private const string _hideHotSelFuncName = "HideHotSelection";
        private const string _onClickHanderName = "OnClick";
        private const string _onPanelClickHandlerName = "OnPanelClick";
        private const string _hidePanelFuncName = "HidePanel";
        private const string _showPanelFuncName = "ShowPanel";

        #endregion

        #region Fields

        private int _selectedPanelIndex = 0;
        private List<int> _hiddenButtonIndexes = null;

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets class of control container table
        /// </summary>
        public string TabControlCssClass
        {
            get { return ViewState["TabControlCssClass"] as string; }
            set { ViewState["TabControlCssClass"] = value; }
        }

        /// <summary>
        /// Gets or sets style of button row
        /// </summary>
        public string HeaderCssClass
        {
            get { return ViewState["HeaderCssClass"] as string; }
            set { ViewState["HeaderCssClass"] = value; }
        }

        /// <summary>
        /// Gets or sets class of separator between header buttons
        /// </summary>
        public string ButtonSeparatorCssClass
        {
            get { return ViewState["ButtonSeparatorCssClass"] as string; }
            set { ViewState["ButtonSeparatorCssClass"] = value; }
        }

        #region Normal button state

        /// <summary>
        /// Gets or sets class name of left button part
        /// </summary>
        public string ButtonACssClass
        {
            get { return ViewState["ButtonACssClass"] as string; }
            set { ViewState["ButtonACssClass"] = value; }
        }

        /// <summary>
        /// Gets or sets class name of left button part
        /// </summary>
        public string ButtonBCssClass
        {
            get { return ViewState["ButtonBCssClass"] as string; }
            set { ViewState["ButtonBCssClass"] = value; }
        }

        /// <summary>
        /// Gets or sets class name of left button part
        /// </summary>
        public string ButtonCCssClass
        {
            get { return ViewState["ButtonCCssClass"] as string; }
            set { ViewState["ButtonCCssClass"] = value; }
        }

        #endregion

        #region Selected button state

        /// <summary>
        /// Gets or sets class name of left button part
        /// </summary>
        public string SelButtonACssClass
        {
            get { return ViewState["SelButtonACssClass"] as string; }
            set { ViewState["SelButtonACssClass"] = value; }
        }

        /// <summary>
        /// Gets or sets class name of left button part
        /// </summary>
        public string SelButtonBCssClass
        {
            get { return ViewState["SelButtonBCssClass"] as string; }
            set { ViewState["SelButtonBCssClass"] = value; }
        }

        /// <summary>
        /// Gets or sets class name of left button part
        /// </summary>
        public string SelButtonCCssClass
        {
            get { return ViewState["SelButtonCCssClass"] as string; }
            set { ViewState["SelButtonCCssClass"] = value; }
        }

        #endregion

        #region Hot button state

        /// <summary>
        /// Gets or sets class name of left button part
        /// </summary>
        public string HotButtonACssClass
        {
            get { return ViewState["HotButtonACssClass"] as string; }
            set { ViewState["HotButtonACssClass"] = value; }
        }

        /// <summary>
        /// Gets or sets class name of left button part
        /// </summary>
        public string HotButtonBCssClass
        {
            get { return ViewState["HotButtonBCssClass"] as string; }
            set { ViewState["HotButtonBCssClass"] = value; }
        }

        /// <summary>
        /// Gets or sets class name of left button part
        /// </summary>
        public string HotButtonCCssClass
        {
            get { return ViewState["HotButtonCCssClass"] as string; }
            set { ViewState["HotButtonCCssClass"] = value; }
        }

        #endregion

        /// <summary>
        /// Gets list of outlook tab panels
        /// </summary>
        private IEnumerable<TabPanel> TabPanels
        {
            get
            {
                foreach (Control control in Controls)
                {
                    if (control is TabPanel)
                        yield return control as TabPanel;
                }
            }
        }

        /// <summary>
        /// Gets number of panels
        /// </summary>
        private int TabPanelCount
        {
            get
            {
                int panelCount = 0;
                foreach (Control control in Controls)
                {
                    if (control is TabPanel)
                        panelCount++;
                }
                return panelCount;
            }
        }

        #region JavaScript related properties

        /// <summary>
        /// Gets or sets function to call
        /// </summary>
        public string OnPanelClick
        {
            get { return ViewState["OnPanelClick"] as string; }
            set { ViewState["OnPanelClick"] = value; }
        }

        /// <summary>
        /// Gets javascript object name
        /// </summary>
        private string JsObjectName
        {
            get { return ClientID + "Obj"; }
        }

        /// <summary>
        /// Gets button prefix name
        /// </summary>
        private string ButtonPrefix
        {
            get { return ClientID + "Button"; }
        }

        /// <summary>
        /// Gets or sets panel prefix
        /// </summary>
        private string PanelPrefix
        {
            get { return ClientID + "Content"; }
        }

        private string SepartorPrefix
        {
            get { return ClientID + "Separtor"; }
        }

        /// <summary>
        /// Gets id of selection hidden field
        /// </summary>
        private string SelectionHiddenFieldId
        {
            get { return ClientID + "SelHF"; }
        }

        /// <summary>
        /// Gets id of hidden field which stored hidden buttons
        /// </summary>
        private string HiddenButtonListFieldId
        {
            get { return ClientID + "HiddenButtonsHF"; }
        }

        /// <summary>
        /// Gets value of hidden field
        /// </summary>
        private string HiddenButtonIndexList
        {
            get
            {
                if (Page.Request[HiddenButtonListFieldId] == null)
                    return string.Empty;
                return Page.Request[HiddenButtonListFieldId];
            }
        }

        /// <summary>
        /// Gets list of hidden button indexes
        /// </summary>
        public List<int> HiddenButtonIndexes
        {
            get
            {
                if (_hiddenButtonIndexes != null)
                    return _hiddenButtonIndexes;

                _hiddenButtonIndexes = new List<int>();

                foreach (string indexValue in HiddenButtonIndexList.Split(
                    new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    _hiddenButtonIndexes.Add(int.Parse(indexValue));
                }

                return _hiddenButtonIndexes;
            }
        }

        /// <summary>
        /// Generates panel creation script
        /// </summary>
        private string PanelCreationScript
        {
            get
            {
                StringBuilder builder = new StringBuilder();
                builder.AppendFormat(
                    "var {0} = new {1}('{2}', '{3}', '{4}',  '{5}', '{6}', '{7}', '{8}', '{9}', '{10}', '{11}', '{12}', '{13}', {14}, '{15}', '{16}');",
                    JsObjectName,
                    _jsObjectClassName,

                    ButtonACssClass,
                    SelButtonACssClass,
                    HotButtonACssClass,

                    ButtonBCssClass,
                    SelButtonBCssClass,
                    HotButtonBCssClass,

                    ButtonCCssClass,
                    SelButtonCCssClass,
                    HotButtonCCssClass,

                    ButtonPrefix,
                    SelectionHiddenFieldId,
                    PanelPrefix,
                    TabPanelCount,
                    HiddenButtonListFieldId,
                    SepartorPrefix
                    );

                if (!string.IsNullOrEmpty(OnPanelClick))
                    builder.AppendFormat(
                        "{0}.{1} = {2};",
                        JsObjectName,
                        _onPanelClickHandlerName,
                        OnPanelClick);
                return builder.ToString();
            }
        }

        #endregion

        #region Control state properties

        /// <summary>
        /// Gets index of selected tab
        /// </summary>
        public int SelectedPanelIndex
        {
            get
            {
                if (!string.IsNullOrEmpty(Page.Request[SelectionHiddenFieldId]))
                    _selectedPanelIndex = int.Parse(Page.Request[SelectionHiddenFieldId]);
                return _selectedPanelIndex;
            }
            set
            {
                _selectedPanelIndex = value;
            }
        }

        #endregion

        #endregion

        #region Public methods

        /// <summary>
        /// Generates script for panel displaying
        /// </summary>
        /// <param name="panelIndex">Index of panel to display</param>
        /// <returns>JavaScript code</returns>
        public string GetShowPanelScript(int panelIndex)
        {
            return string.Format("{0}.{1}('{2}');", JsObjectName, _showPanelFuncName, panelIndex);
        }

        /// <summary>
        /// Generates script code to hide panel
        /// </summary>
        /// <param name="panelIndex">Index of panel to hide</param>
        /// <returns>JavaScript code to hide panel</returns>
        public string GetHidePanelScript(int panelIndex)
        {
            return string.Format("{0}.{1}('{2}');", JsObjectName, _hidePanelFuncName, panelIndex);
        }

        /// <summary>
        /// Gets script for moving user to next panel
        /// </summary>
        /// <returns></returns>
        public string GetGoToNextPanelFuncCall()
        {
            return string.Format("{0}.MoveToNextPanel();", JsObjectName);
        }

        /// <summary>
        /// Gets script for moving user to previous panel
        /// </summary>
        /// <returns></returns>
        public string GetGoToPrevPanelFuncCall()
        {
            return string.Format("{0}.MoveToPrevPanel();", JsObjectName);
        }

        #endregion

        #region Overriden methods

        /// <summary>
        /// Registers client scripts
        /// </summary>
        /// <param name="e">Event arguments</param>
        protected override void OnPreRender(EventArgs e)
        {
            Type type = GetType();
            Page.ClientScript.RegisterClientScriptResource(type, EmbeddedResources.JsTabControlScripts);
            Page.ClientScript.RegisterClientScriptBlock(type, "PanelCreationScript", PanelCreationScript, true);

            Page.ClientScript.RegisterHiddenField(SelectionHiddenFieldId, SelectedPanelIndex.ToString());
            Page.ClientScript.RegisterHiddenField(HiddenButtonListFieldId, HiddenButtonIndexList);

            base.OnPreRender(e);
        }

        /// <summary>
        /// Renders control
        /// </summary>
        /// <param name="writer">Writer to use</param>
        protected override void Render(HtmlTextWriter writer)
        {
            WriteBeginTag(writer);

            RenderHeader(writer);
            RenderContentPanels(writer);

            WriteEndTag(writer);
        }

        #endregion


        #region Html generation methods

        /// <summary>
        /// Renders content panels
        /// </summary>
        /// <param name="writer">Writer to use</param>
        private void RenderContentPanels(HtmlTextWriter writer)
        {
            int panelIndex = 0;

            foreach (TabPanel panel in TabPanels)
            {
                RenderPanel(panelIndex++, panel, writer);
            }
        }

        /// <summary>
        /// Renders panel
        /// </summary>
        /// <param name="panel">Panel to render</param>
        /// <param name="writer">Writer to use</param>
        private void RenderPanel(int panelIndex, TabPanel panel, HtmlTextWriter writer)
        {
            writer.WriteBeginTag("tr");
            writer.WriteAttribute("id", PanelPrefix + panelIndex);
            if (panelIndex != SelectedPanelIndex)
                writer.WriteAttribute("style", "display:none");
            writer.Write(">");
            writer.WriteBeginTag("td");
            writer.Write(">");
            panel.RenderControl(writer);
            writer.WriteEndTag("td");
            writer.WriteEndTag("tr");
        }

        /// <summary>
        /// Renders header control
        /// </summary>
        /// <param name="writer">Writer to use</param>
        private void RenderHeader(HtmlTextWriter writer)
        {
            writer.WriteBeginTag("tr");
            writer.WriteAttribute("class", HeaderCssClass);
            writer.Write(">");
            writer.WriteBeginTag("td");
            writer.WriteAttribute("align", "left");
            writer.Write(">");
            writer.WriteBeginTag("table");
            writer.WriteAttribute("style", "cursor:pointer");
            writer.WriteAttribute("cellpadding", "0");
            writer.WriteAttribute("cellspacing", "0");
            writer.Write(">");
            writer.WriteBeginTag("tr");
            writer.Write(">");

            int index = 0;

            foreach (TabPanel panel in TabPanels)
            {
                RenderHeaderButton(panel, index++, writer);
            }

            writer.WriteEndTag("tr");
            writer.WriteEndTag("table");
            writer.WriteEndTag("td");
            writer.WriteEndTag("tr");
        }

        /// <summary>
        /// Renders outlook panel using specified writer object
        /// </summary>
        /// <param name="panel">Panel to render</param>
        /// <param name="writer">Writer to use</param>
        private void RenderHeaderButton(TabPanel panel, int panelIndex, HtmlTextWriter writer)
        {
            writer.WriteBeginTag("td");
            writer.WriteAttribute("id", SepartorPrefix + panelIndex);
            writer.WriteAttribute("class", ButtonSeparatorCssClass);
            if (HiddenButtonIndexes.Contains(panelIndex))
                writer.WriteAttribute("style", "display:none;");
            writer.Write(">&nbsp;");
            writer.WriteEndTag("td");

            writer.WriteBeginTag("td");
            writer.Write(">");
            writer.WriteBeginTag("table");
            if (HiddenButtonIndexes.Contains(panelIndex))
                writer.WriteAttribute("style", "display:none;");
            writer.WriteAttribute("id", ButtonPrefix + panelIndex);
            writer.WriteAttribute("onclick", JsObjectName + "." + _onClickHanderName + "(" + panelIndex + ")");
            writer.WriteAttribute("onmouseover", JsObjectName + "." + _showHotSelFuncName + "(" + panelIndex + ")");
            writer.WriteAttribute("onmouseout", JsObjectName + "." + _hideHotSelFuncName + "(" + panelIndex + ")");
            writer.WriteAttribute("cellpadding", "0");
            writer.WriteAttribute("cellspacing", "0");
            writer.Write(">");
            writer.WriteBeginTag("tr");
            writer.Write(">");

            writer.WriteBeginTag("td");
            writer.WriteAttribute("id", ButtonPrefix + "A" + panelIndex);
            writer.WriteAttribute("class", SelectedPanelIndex == panelIndex ? SelButtonACssClass : ButtonACssClass);
            writer.Write(">");
            writer.WriteEndTag("td");

            writer.WriteBeginTag("td");
            writer.WriteAttribute("id", ButtonPrefix + "B" + panelIndex);
            writer.WriteAttribute("class", SelectedPanelIndex == panelIndex ? SelButtonBCssClass : ButtonBCssClass);
            writer.Write(">");
            writer.Write(HttpUtility.HtmlEncode(panel.PanelTitle).Replace(" ", "&nbsp;"));
            writer.WriteEndTag("td");

            writer.WriteBeginTag("td");
            writer.WriteAttribute("id", ButtonPrefix + "C" + panelIndex);
            writer.WriteAttribute("class", SelectedPanelIndex == panelIndex ? SelButtonCCssClass : ButtonCCssClass);
            writer.Write(">");
            writer.WriteEndTag("td");

            writer.WriteEndTag("tr");
            writer.WriteEndTag("table");
            writer.WriteEndTag("td");
        }

        /// <summary>
        /// Creates begin tag of control
        /// </summary>
        /// <param name="writer">Writer to use</param>
        private void WriteBeginTag(HtmlTextWriter writer)
        {
            writer.WriteBeginTag("table");
            writer.WriteAttribute("class", TabControlCssClass);
            writer.WriteAttribute("cellpadding", "0");
            writer.WriteAttribute("cellspacing", "0");
            writer.WriteAttribute("style", string.Format("width:{0};height:{1};", Width, Height));
            writer.Write(">");
        }

        /// <summary>
        /// Renders end tag
        /// </summary>
        /// <param name="writer">Writer to use</param>
        private void WriteEndTag(HtmlTextWriter writer)
        {
            writer.WriteEndTag("table");
        }


        #endregion
    }
}
