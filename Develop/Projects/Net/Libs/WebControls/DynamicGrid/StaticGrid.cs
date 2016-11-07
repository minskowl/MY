#region Version & Copyright
/* 
 * $Id: StaticGrid.cs 23739 2007-11-06 09:31:43Z svd $ 
 *
 * <:COPYRIGHT_LICENSE:>
 */
#endregion

using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using Savchin.Web;


namespace Savchin.Web.UI
{
    /// <summary>
    /// Generates grid which uses table as source data
    /// </summary>
    [ToolboxData("<{0}:StaticGrid runat=\"server\"></{0}:StaticGrid>")]
    [PersistChildren(true)]
    public class StaticGrid : AbstractJsGrid
    {
        #region Constants

        protected const string StageParamName    = "stage";
        protected const string NewValueParamName = "newValue";
        protected const string OldValueParamName = "oldValue";
        protected const string RowIdParamName    = "rowId";

        #endregion

        #region Fields

        private List<RowItem> _rows = new List<RowItem>();
        private HiddenField _gridDataXmlHF = null;

        #endregion

        #region Properties

        #region JavaScript properties

        /// <summary>
        /// Gets or sets the max text lenght.
        /// </summary>
        /// <value>The max text lenght.</value>
        public int MaxTextLenght
        {
            get 
            {
                if (ViewState["maxTextLenght"] == null)
                    return 50;
                return (int)ViewState["maxTextLenght"]; 
            }
            set
            {
                ViewState["maxTextLenght"] = value;
            }
        }

        /// <summary>
        /// Gets collection of row items
        /// </summary>
        public List<RowItem> Rows
        {
            get { return _rows; }
        }

        #region Action column logic

        /// <summary>
        /// Specified if action template should be used for last column
        /// </summary>
        public bool UseActionTemplate
        {
            get 
            { 
                if (ViewState["UseActionTemplate"] == null)
                    return false;
                return (bool)ViewState["UseActionTemplate"];
            }
            set
            {
                ViewState["UseActionTemplate"] = value;
            }
        }

        /// <summary>
        /// Default action template
        /// </summary>
        public string ActionTemplate
        {
            get
            {
                if (ViewState["ActionTemplate"] == null)
                    return string.Empty;
                return ViewState["ActionTemplate"] as string;
            }
            set
            {
                ViewState["ActionTemplate"] = value;
            }
        }

        /// <summary>
        /// Generates name of edit event handler
        /// </summary>
        public string EditEventHandlerName
        {
            get { return ClientID + "EditEventHandler"; }
        }

        #endregion

        #endregion

        #endregion

        #region Overriden methods

        /// <summary>
        /// Adds hidden field to controls collection
        /// </summary>
        protected override void CreateChildControls()
        {
            _gridDataXmlHF    = new HiddenField();
            _gridDataXmlHF.ID = "gridDataXml";
            Controls.Add(_gridDataXmlHF);

            base.CreateChildControls();
        }

        /// <summary>
        /// Perfroms collection deserialization
        /// </summary>
        /// <param name="e">Event arguments</param>
        protected override void OnLoad(EventArgs e)
        {
 	        base.OnLoad(e);

            if (Page.IsPostBack)
                DeserializeItemCollection();
        }

        /// <summary>
        /// Generates grid creation script
        /// </summary>
        /// <returns>JavaScript code</returns>
        protected override string GetGridCreationScript()
        {
            return 
                base.GetGridCreationScript() +
                BuildObjectInitializationScript() +
                BuildDataScript();
        }

        /// <summary>
        /// Registers additional scripts for table grid
        /// </summary>
        protected override void RegisterScripts()
        {
            base.RegisterScripts();

            Page.ClientScript.RegisterOnSubmitStatement(
                typeof(StaticGrid),
                ClientID + "OnSubmitScript",
                BuildSubmitStatementScript());

            Page.ClientScript.RegisterClientScriptBlock(
                typeof(StaticGrid),
                EditEventHandlerName,
                BuildEditEventHandlerCode(),
                true);
        }

        
        #endregion

        #region Xml deserialization

        /// <summary>
        /// Deserializes item collection
        /// </summary>
        private void DeserializeItemCollection()
        {
            Rows.Clear();

            if (_gridDataXmlHF == null)
                return;
            if (string.IsNullOrEmpty(_gridDataXmlHF.Value))
                return;

            XmlDocument document = new XmlDocument();
            document.LoadXml(_gridDataXmlHF.Value);

            foreach (XmlNode rowNode in document.DocumentElement.SelectNodes("row"))
            {
                RowItem rowItem = new RowItem();
                rowItem.ID = rowNode.Attributes["rowId"].Value;

                foreach (XmlNode cellNode in rowNode.SelectNodes("cell"))
                {
                    rowItem.Values.Add(cellNode.InnerText);
                }

                Rows.Add(rowItem);
            }
        }

        #endregion

        #region Html code generation

        /// <summary>
        /// Generates script which contains data to load
        /// </summary>
        /// <returns>Script code</returns>
        private string BuildDataScript()
        {
            StringBuilder builder = new StringBuilder();

            foreach (RowItem row in Rows)
            {
                builder.AppendFormat("{0}.addRowWithTemplate('{1}', {2});\n", 
                    JsObjName, 
                    row.ID, 
                    row.ToJavaParamArray());
            }

            return builder.ToString();
        }

        #endregion

        #region Java Script registration

        /// <summary>
        /// Builds object initialization script
        /// </summary>
        /// <returns>Initialization script</returns>
        private string BuildObjectInitializationScript()
        {
            StringBuilder builder = new StringBuilder();

            builder.AppendFormat("{0}.init();\n", JsObjName);
            builder.AppendFormat("{0}.maxTextLenght = {1};", JsObjName, MaxTextLenght);
            builder.AppendFormat(
                "{0}.useActionTemplate = {1};\n", 
                JsObjName, 
                UseActionTemplate ? "true" : "false");
            builder.AppendFormat(
                "{0}.actionTemplate = {1};\n", 
                JsObjName,
                JavaScriptBuilder.ConvertToJavaScriptLine(ActionTemplate));
            builder.AppendFormat(
                "{0}.setOnEditCellHandler({1});\n",
                JsObjName,
                EditEventHandlerName);

            return builder.ToString();
        }

        /// <summary>
        /// Geneates script for post back processing
        /// </summary>
        /// <returns>JavaScript code block</returns>
        private string BuildSubmitStatementScript()
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendFormat("document.getElementById('{0}').value = {1}.serializeData();", _gridDataXmlHF.ClientID, JsObjName);
            return builder.ToString();
        }

        /// <summary>
        /// Builds event handler code
        /// </summary>
        /// <returns>JavaScript code block</returns>
        private string BuildEditEventHandlerCode()
        {
            StringBuilder builder = new StringBuilder();

            builder.AppendFormat(
                "function {0}({1}, {2}, cellIndex, {3}, {4}, escPressed)\n", 
                EditEventHandlerName,
                StageParamName, 
                RowIdParamName,
                NewValueParamName, 
                OldValueParamName);
            builder.AppendLine("{");
            if (UseActionTemplate)
                builder.AppendFormat("if (stage == 0) return {0} != cellIndex;\n", Columns.Count - 1);
            builder.AppendLine("if (stage == 2 && escPressed) return false;\n");
            builder.AppendLine(BuildEditEventHandlerStatement());
            builder.AppendLine("return true");
            builder.AppendLine("}");

            return builder.ToString();
        }

        /// <summary>
        /// Generates additional code for edit event
        /// </summary>
        /// <returns>JavaScript code</returns>
        protected virtual string BuildEditEventHandlerStatement()
        {
            return string.Empty;
        }

        #endregion
    }
}
