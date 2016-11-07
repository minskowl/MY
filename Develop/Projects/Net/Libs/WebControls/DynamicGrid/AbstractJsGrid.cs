using System;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

[assembly: WebResource("Savchin.Web.UI.Resources.dhtmlXCommon.js", "application/x-javascript", PerformSubstitution = true)]
[assembly: WebResource("Savchin.Web.UI.Resources.dhtmlXGrid.js", "application/x-javascript", PerformSubstitution = true)]
[assembly: WebResource("Savchin.Web.UI.Resources.dhtmlXGrid_srnd.js", "application/x-javascript", PerformSubstitution = true)]
[assembly: WebResource("Savchin.Web.UI.Resources.dhtmlXGridCell.js", "application/x-javascript", PerformSubstitution = true)]
[assembly: WebResource("Savchin.Web.UI.Resources.dhtmlXGrid_drag.js", "application/x-javascript", PerformSubstitution = true)]
[assembly: WebResource("Savchin.Web.UI.Resources.dhtmlXGrid_start.js", "application/x-javascript", PerformSubstitution = true)]
[assembly: WebResource("Savchin.Web.UI.Resources.dhtmlXGrid.css", "text/css")]


namespace Savchin.Web.UI
{
    /// <summary>
    /// Contains all functionality of dynamic grid except dynamic data loading
    /// </summary>
    [ToolboxData("<{0}:StaticGrid runat=\"server\"></{0}:StaticGrid>")]
    [PersistChildren(true)]
    public class AbstractJsGrid : WebControl, INamingContainer
    {

        private const string _scriptIncludeBlock = "DynGridInclude";

        #region Properties

        #region Appearance

        /// <summary>
        /// Gets or sets background image url
        /// </summary>
        public string HeaderBackroundImageUrl
        {
            get { return (string)ViewState["HeaderBackroundImageUrl"] ?? "images/button.gif"; }
            set { ViewState["HeaderBackroundImageUrl"] = value; }
        }

        protected string HeaderImageCssStyle
        {
            get
            {
                if (string.IsNullOrEmpty(HeaderBackroundImageUrl))
                    return string.Empty;
                return string.Format(
                    "background-image: url({0});",
                    ControlHelper.GetThemebleUrl(HeaderBackroundImageUrl, Page.Theme));
            }
        }

        /// <summary>
        /// Path to image folder
        /// </summary>
        public string ImagePath
        {
            get { return HttpContext.Current.Request.ApplicationPath + "/App_Themes/" + Page.Theme + "/DynGridImages/"; }
        }

        /// <summary>
        /// Style of ordinary line
        /// </summary>
        public string LineCssClass
        {
            get { return ViewState["LineCssClass"] as string; }
            set { ViewState["LineCssClass"] = value; }
        }

        /// <summary>
        /// Alternate line css style
        /// </summary>
        public string AltLineCssClass
        {
            get { return ViewState["AltLineCssClass"] as string; }
            set { ViewState["AltLineCssClass"] = value; }
        }

        /// <summary>
        /// Gets or sets header style
        /// </summary>
        public string HeaderColumnStyle
        {
            get
            {
                return ViewState["HeaderColumnCssStyle"] as string;
            }
            set { ViewState["HeaderColumnCssStyle"] = value; }
        }

        /// <summary>
        /// Gets or sets grid cell style
        /// </summary>
        public string GridCellStyle
        {
            get { return ViewState["GridCellStyle"] as string; }
            set { ViewState["GridCellStyle"] = value; }
        }


        /// <summary>
        /// Gets of sets background css class
        /// </summary>
        public string BackgroundCssClass
        {
            get
            {
                return ViewState["BackgroundCssClass"] as string;
            }
            set { ViewState["BackgroundCssClass"] = value; }
        }

        #endregion

        #region Behavior

        /// <summary>
        /// Gets or sets collection of columns
        /// </summary>
        public GridColumnCollection Columns
        {
            get
            {
                if (ViewState["Columns"] == null)
                    ViewState["Columns"] = new GridColumnCollection();
                return ViewState["Columns"] as GridColumnCollection;
            }
            set
            {
                ViewState["Columns"] = value;
            }
        }

        /// <summary>
        /// Gets or sets flag which indicated if drag and drop should be used
        /// </summary>
        public bool EnableDragAndDrop
        {
            get
            {
                if (ViewState["EnableDragAndDrop"] == null)
                    ViewState["EnableDragAndDrop"] = false;
                return (bool)ViewState["EnableDragAndDrop"];
            }
            set { ViewState["EnableDragAndDrop"] = value; }
        }

        /// <summary>
        /// Specifies if edit events allowed for control
        /// </summary>
        public virtual bool AllowEditEvents
        {
            get
            {
                if (ViewState["AllowEditEvents"] == null)
                    return false;
                return (bool)ViewState["AllowEditEvents"];
            }
            set
            {
                ViewState["AllowEditEvents"] = value;
            }
        }

        /// <summary>
        /// Gets name of javaScript object
        /// </summary>
        public virtual string JsObjName
        {
            get { return ClientID + "Obj"; }
        }

        #endregion

        #endregion

        #region Overriden methods

        #region No start and end tag rendering is requered

        public override void RenderBeginTag(HtmlTextWriter writer)
        {
        }

        public override void RenderEndTag(HtmlTextWriter writer)
        {
        }

        /// <summary>
        /// Control html code generation
        /// </summary>
        /// <param name="writer">Output object</param>
        protected override void Render(HtmlTextWriter writer)
        {
            if (Visible)
                AddGridArea(writer);
            base.Render(writer);
        }

        /// <summary>
        /// Scripts registraction
        /// </summary>
        /// <param name="e">Event information</param>
        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);

            if (Visible)
                RegisterScripts();
        }

        /// <summary>
        /// Registers scripts used by control
        /// </summary>
        protected virtual void RegisterScripts()
        {
            RegisterCommonScripts();
            RegisterObjectCreationScript();
        }

        protected virtual string GetGridCreationScript()
        {
            StringBuilder builder = new StringBuilder();

            BuildObjectCreationScript(builder);

            BuildColumnInitScript(builder);
            BuildAppearanceScript(builder);
            BuildBehaviorScript(builder);
            BuildResize(builder);
            return builder.ToString();
        }

        protected void BuildObjectCreationScript(StringBuilder builder)
        {
            builder.AppendFormat("var {0} = new dhtmlXGridObject('{1}');\n", JsObjName, ClientID);
        }

        /// <summary>
        /// Generates script for grid column initialization
        /// </summary>
        /// <param name="builder">Output to use</param>
        protected void BuildColumnInitScript(StringBuilder builder)
        {
            builder.AppendFormat("{0}.setHeader({1}, '|');\n", JsObjName, Columns.GetTextList());


            if (Columns.IsPixelWidth)
                builder.AppendFormat("{0}.setInitWidths(\"{1}\");\n", JsObjName, Columns.GetWidthsList());
            else
                builder.AppendFormat("{0}.setInitWidthsP(\"{1}\");\n", JsObjName, Columns.GetWidthsList());

            GridColumnCollection collection = Columns;

            for (int i = 0; i < collection.Count; i++)
            {
                if (collection[i].MinWidth.HasValue)
                    builder.AppendFormat("{0}.setColumnMinWidth({1}, {2});",
                        JsObjName, collection[i].MinWidth.Value, i);
            }
            builder.AppendFormat("{0}.setColAlign(\"{1}\");\n", JsObjName, Columns.GetAlignmentsList());
            builder.AppendFormat("{0}.setColTypes(\"{1}\");\n", JsObjName, Columns.GetTypeList());
            builder.AppendFormat("{0}.setStyle(\"{1}\", null, \"{2}\", \"{2}\");\n", JsObjName, HeaderImageCssStyle + HeaderColumnStyle, GridCellStyle ?? "null");
            builder.AppendFormat("{0}.enableColumnAutoSize(false);\n", JsObjName);
        }


        /// <summary>
        /// Generates behaviour script
        /// </summary>
        /// <param name="builder">Builder to use</param>
        protected void BuildBehaviorScript(StringBuilder builder)
        {
            builder.AppendFormat("{0}.enableCellIds(true);\n", JsObjName);
            builder.AppendFormat("{0}.enableBuffering(50);\n", JsObjName);
            builder.AppendFormat(
                "{0}.enableEditEvents({1},{1},{1});\n",
                JsObjName,
                AllowEditEvents ? "true" : "false");


            if (EnableDragAndDrop)
                builder.AppendFormat("{0}.enableDragAndDrop(true);\n", JsObjName);
        }

        /// <summary>
        /// Initializes grid apparance settings
        /// </summary>
        /// <param name="builder">Builder object to use</param>
        protected void BuildAppearanceScript(StringBuilder builder)
        {
            builder.AppendFormat("{0}.setImagePath(\"{1}\");\n", JsObjName, ImagePath);
            if (!string.IsNullOrEmpty(BackgroundCssClass))
                builder.AppendFormat("{0}.objBox.className = '{1}';\n", JsObjName, BackgroundCssClass);
            builder.AppendFormat("{0}.enableAlterCss(\"{1}\",\"{2}\");\n", JsObjName, LineCssClass, AltLineCssClass);
        }

        /// <summary>
        /// Builds the resize.
        /// </summary>
        /// <param name="builder">The builder.</param>
        protected void BuildResize(StringBuilder builder)
        {
            if (((PageEx)Page).Browser == BrowserType.FireFox)
                builder.AppendLine("document.addEventListener('panelchanged', function(event) {" + JsObjName + ".setSizes();  }, false);");
        }

        #endregion


        #region Html generation

        /// <summary>
        /// Frame for dymanic grid control
        /// </summary>
        /// <param name="output">Output object to use</param>
        private void AddGridArea(HtmlTextWriter output)
        {
            output.WriteBeginTag("div");
            output.WriteAttribute("id", ClientID);
            output.WriteAttribute("width", Width.ToString());
            output.WriteAttribute("height", Height.ToString());
            output.WriteAttribute("onresize", JsObjName + ".setSizes();");
            output.Write("/>");
            if (DesignMode)
                output.Write("Dynamic Grid: " + ClientID);

            output.WriteEndTag("div");

        }

        #endregion

        #region JavaScript generation

        /// <summary>
        /// Peforms common script registraction
        /// </summary>
        private void RegisterCommonScripts()
        {
            if (Page.ClientScript.IsClientScriptBlockRegistered(_scriptIncludeBlock))
                return;

            Page.ClientScript.RegisterClientScriptResource(
                typeof(AbstractJsGrid), "Savchin.Web.UI.Resources.dhtmlXCommon.js");
            Page.ClientScript.RegisterClientScriptResource(
                typeof(AbstractJsGrid), "Savchin.Web.UI.Resources.dhtmlXGrid.js");
            Page.ClientScript.RegisterClientScriptResource(
                typeof(AbstractJsGrid), "Savchin.Web.UI.Resources.dhtmlXGrid_srnd.js");
            Page.ClientScript.RegisterClientScriptResource(
                typeof(AbstractJsGrid), "Savchin.Web.UI.Resources.dhtmlXGridCell.js");
            Page.ClientScript.RegisterClientScriptResource(
                typeof(AbstractJsGrid), "Savchin.Web.UI.Resources.dhtmlXGrid_drag.js");
            Page.ClientScript.RegisterClientScriptResource(
                typeof(AbstractJsGrid), "Savchin.Web.UI.Resources.dhtmlXGrid_start.js");

            Page.ClientScript.RegisterClientScriptBlock(typeof(AbstractJsGrid), _scriptIncludeBlock,
                string.Empty);

            ControlHelper.AddCssInclude(Page, typeof(AbstractJsGrid), "Savchin.Web.UI.Resources.dhtmlXGrid.css");
        }

        private void RegisterObjectCreationScript()
        {
            Page.ClientScript.RegisterStartupScript(
                typeof(AbstractJsGrid),
                 "Object creation script " + ClientID,
                GetGridCreationScript(),
                true);
        }

        #endregion

        #endregion
    }
}
