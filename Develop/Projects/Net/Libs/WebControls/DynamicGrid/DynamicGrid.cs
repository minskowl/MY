#region Version & Copyright
/* 
 * $Id$ 
 *
 * <:COPYRIGHT_LICENSE:>
 */
#endregion

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Savchin.Web.UI
{
    /// <summary>
    /// Dynamic grid control
    /// </summary>
    [ToolboxData("<{0}:DynamicGrid runat=\"server\"></{0}:DynamicGrid>")]
    [PersistChildren(true)]
    public partial class DynamicGrid : AbstractJsGrid
    {
        #region Fields

        private DynamicGridInfoProvider _provider = null;
        private HiddenField _idListField = null;
        private HiddenField _allItemsSelected = null;
        private HiddenField _orderedItemListField = null;

        #endregion

        #region Properties

        #region Info

        /// <summary>
        /// Returns info provider
        /// </summary>
        public DynamicGridInfoProvider InfoProvider
        {
            get { return _provider; }
        }

        #endregion


        /// <summary>
        /// Sets sort direction
        /// </summary>
        public SortDirection SortDir
        {
            get
            {
                if (ViewState["AscSorting"] == null)
                    ViewState["AscSorting"] = SortDirection.Ascending;
                return (SortDirection)ViewState["AscSorting"];
            }
            set { ViewState["AscSorting"] = value; }
        }


        /// <summary>
        /// Specifies if smart rendering should be enabled
        /// </summary>
        [DefaultValue(false)]
        public bool EnableSmartRendering
        {
            get
            {
                if (ViewState["EnableSmartRendering"] == null)
                    ViewState["EnableSmartRendering"] = false;
                return (bool)ViewState["EnableSmartRendering"];
            }
            set { ViewState["EnableSmartRendering"] = value; }
        }


        /// <summary>
        /// Item to be sorted by
        /// </summary>
        public string SortMember
        {
            get { return ViewState["SortMember"] as string; }
            set { ViewState["SortMember"] = value; }
        }

        /// <summary>
        /// Gets or sets additiona xml request query parameters
        /// </summary>
        public string AdditionalXmlQueryParams
        {
            get { return ViewState["AdditionalXmlQueryParams"] as string; }
            set { ViewState["AdditionalXmlQueryParams"] = value; }
        }

        /// <summary>
        /// Type of data to get
        /// </summary>
        public string AssociatedDataKey
        {
            get { return ViewState["AssociatedDataKey"] as string; }
            set { ViewState["AssociatedDataKey"] = value; }
        }


        /// <summary>
        /// Gets or sets the data source id.
        /// </summary>
        /// <value>The data source id.</value>
        public string DataSourceID
        {
            get { return ViewState["DataSourceID"] as string; }
            set { ViewState["DataSourceID"] = value; }
        }

        /// <summary>
        /// Gets or sets number of items inside grid
        /// </summary>
        public int RowCount
        {
            get
            {
                if (ViewState["ItemCount"] == null)
                    return 0;
                return (int)ViewState["ItemCount"];
            }
            set { ViewState["ItemCount"] = value; }
        }

        /// <summary>
        /// Specifies if ordered list should be stored during
        /// post back
        /// </summary>
        public bool StoreOrderedList
        {
            get
            {
                if (ViewState["StoreOrderedList"] == null)
                    ViewState["StoreOrderedList"] = false;
                return (bool)ViewState["StoreOrderedList"];
            }
            set { ViewState["StoreOrderedList"] = value; }
        }

        /// <summary>
        /// Gets ordered list of item ids
        /// </summary>
        public string[] SortedIdList
        {
            get
            {
                if (!StoreOrderedList)
                    return new string[0];
                return _orderedItemListField.Value.Split(new char[] { ',' },
                    StringSplitOptions.RemoveEmptyEntries);
            }
        }

        /// <summary>
        /// Approximate grid height
        /// </summary>
        public int AproxHeight
        {
            get { return 35 + 5 + 20 * RowCount; }
        }

        /// <summary>
        /// Specifies if header checkbox is selected
        /// </summary>
        public bool IsAllItemsSelected
        {
            get
            {
                if (_allItemsSelected == null)
                    return false;
                if (string.IsNullOrEmpty(_allItemsSelected.Value))
                    return false;
                return bool.Parse(_allItemsSelected.Value);
            }
        }

        /// <summary>
        /// Gets list of seleted items
        /// </summary>
        public long[] SelectedItems
        {
            get
            {
                List<long> idList = new List<long>();

                foreach (string id in _idListField.Value.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    if (id.StartsWith("temp_"))
                        continue;
                    idList.Add(long.Parse(id));
                }

                return idList.ToArray();
            }
        }

        /// <summary>
        /// Gets java script call for grid ordered list
        /// </summary>
        public string JsGetOrderedIdListFuncCall
        {
            get { return JsObjName + ".getQuestionsOrderedIdList()"; }
        }

        #endregion

        #region Construction

        /// <summary>
        /// Default constructor
        /// </summary>
        public DynamicGrid()
        {
            _provider = new DynamicGridInfoProvider(this);
        }

        #endregion

        #region Public methods




        /// <summary>
        /// Sets default background css style
        /// </summary>
        public void SetDefaultBackground()
        {
            BackgroundCssClass = "objBox";
        }

        /// <summary>
        /// Returns pattern for header links
        /// </summary>
        /// <returns>Post back event reference</returns>
        public string GetPostEventReferencePattern()
        {
            return "javascript:" + Page.ClientScript.GetPostBackEventReference(
                this,
                "{0}");
        }

        /// <summary>
        /// Adds check box at specified position
        /// </summary>
        /// <param name="columnIndex">Index for column to add</param>
        /// <param name="width">Width of column</param>
        public void AddCheckBoxColumn(int columnIndex, Unit width)
        {
            GridColumn column = new GridColumn(GetCheckBoxHtml(columnIndex), width, ColumnAlignment.Center);
            column.ColumnType = "ch";
            column.MinWidth = 30;

            Columns.Insert(columnIndex, column);
        }

        /// <summary>
        /// Generate html code for column
        /// </summary>
        /// <param name="columnIndex">Index if check box column</param>
        /// <returns>Html code for column checkbox</returns>
        private string GetCheckBoxHtml(int columnIndex)
        {
            return string.Format("<input id='{0}' name='{0}' type='checkbox' onclick='{1}'></input>",
                InfoProvider.HeaderCheckBoxID,
                InfoProvider.HeaderCheckBoxClickHandlerName + "(" + columnIndex + ")"); 
        }

        #endregion

        #region Overriden methods

        /// <summary>
        /// Header attributes (grid style sheet)
        /// </summary>
        protected override void CreateChildControls()
        {
            _idListField = new HiddenField();
            _idListField.ID = "SelectedItemsHF";
            Controls.Add(_idListField);

            _allItemsSelected = new HiddenField();
            _allItemsSelected.ID = "AllItemsSelectedHF";
            Controls.Add(_allItemsSelected);

            _orderedItemListField = new HiddenField();
            _orderedItemListField.ID = "OrderedItemListHF";
            Controls.Add(_orderedItemListField);

            base.CreateChildControls();
        }

        /// <summary>
        /// Scripts registraction
        /// </summary>
        protected override void RegisterScripts()
        {
            UpdateColumnTemplates();

            base.RegisterScripts();

            string submitScript = GetSubmitScript();

            if (!string.IsNullOrEmpty(submitScript))
                Page.ClientScript.RegisterOnSubmitStatement(
                        typeof(DynamicGrid), ClientID + "OnSubmit", submitScript);

            Page.ClientScript.RegisterStartupScript(typeof(DynamicGrid), ClientID + "Sort", GetSortScript(), true);

            RegisterPopupScript();
        }

        #endregion

        #region Code generation methods

        /// <summary>
        /// Adds hyperlink template for headers to sort
        /// </summary>
        private void UpdateColumnTemplates()
        {
            foreach (GridColumn column in Columns)
            {
                if (column.ApplySort)
                {
                    column.HeaderPattern = "<a href=\"#\" onclick=\"" + InfoProvider.SortFuncName + "('{0}')\">{1}</a>";
                }
            }
        }


        /// <summary>
        /// Generates java script code for grid
        /// </summary>
        /// <returns>Script code of dynamic grid</returns>
        protected override string GetGridCreationScript()
        {
            StringBuilder builder = new StringBuilder();

            builder.AppendFormat("var {0} = '{1}';\n", InfoProvider.XmlSourceUrlObj, GetXmlSourceUrl());
            builder.AppendLine(base.GetGridCreationScript());

            BuildLoadXmlScript(builder);
            BuildCheckBoxColumnScript(builder);

            return builder.ToString();
        }

        /// <summary>
        /// Generates script for xml data loading
        /// </summary>
        /// <param name="builder">Builder object to use</param>
        private void BuildLoadXmlScript(StringBuilder builder)
        {
            builder.AppendFormat("{0}.init();\n", JsObjName);

            if (EnableSmartRendering)
                builder.AppendFormat("{0}.enableSmartRendering(true, {1}, 50);\n", JsObjName, RowCount);

            builder.AppendFormat("{0}.loadXML(\"{1}\");", JsObjName, GetXmlSourceUrl());
        }



        /// <summary>
        /// Gets script for grid reloading
        /// </summary>
        /// <returns>Java script code</returns>
        public string GetGridReloadScript()
        {
            return GetGridReloadScript(string.Empty);
        }

        /// <summary>
        /// Gets reload script which accepts additional query params
        /// provided by java script
        /// </summary>
        /// <param name="queryParamsFuncName">Function name</param>
        /// <returns>Reload script</returns>
        public string GetGridReloadScript(string queryParamsFuncName)
        {
            StringBuilder builder = new StringBuilder();

            if (!string.IsNullOrEmpty(queryParamsFuncName))
                builder.AppendFormat("var {0}orderedItemsList = {0}.getQuestionsOrderedIdList();\n", JsObjName);

            builder.AppendFormat("{0}.clearAll();\n", JsObjName);

            if (string.IsNullOrEmpty(queryParamsFuncName))
                builder.AppendFormat("{0}.loadXML('{1}');\n", JsObjName, GetXmlSourceUrl());
            else
                builder.AppendFormat(
                    "{0}.loadXML('{1}' + {2}({0}orderedItemsList));\n", JsObjName, GetXmlSourceUrl(), queryParamsFuncName);
            return builder.ToString();
        }


        /// <summary>
        /// Retruns url to xml data source
        /// </summary>
        /// <returns>Url</returns>
        private string GetXmlSourceUrl()
        {
            return PageEx.GetDymanicDataRequestUrl(this);
        }

        /// <summary>
        /// Generates submit script code
        /// </summary>
        /// <returns>Submit script code</returns>
        protected string GetSubmitScript()
        {
            StringBuilder builder = new StringBuilder();

            if (CheckBoxWasAdded())
            {
                builder.AppendFormat(
                    "document.getElementById('{0}').value = {1}.getCheckedRows(0);\n",
                    _idListField.ClientID,
                    JsObjName);
                builder.AppendFormat(
                    "document.getElementById('{0}').value = {1}.checked;\n",
                    _allItemsSelected.ClientID,
                    InfoProvider.HeaderCheckBoxObjName);
            }

            if (StoreOrderedList)
                builder.AppendFormat(
                    "document.getElementById('{0}').value = {1}.getQuestionsOrderedIdList();\n",
                    _orderedItemListField.ClientID,
                    JsObjName);

            return builder.ToString();
        }


        /// <summary>
        /// Generates java script for check box column
        /// </summary>
        private void BuildCheckBoxColumnScript(StringBuilder builder)
        {
            if (!CheckBoxWasAdded())
                return;

            for (int index = 0; index < Columns.Count; index++)
            {
                if (Columns[index].ColumnType == "ch")
                    break;
            }

            builder.AppendFormat("{0} = document.getElementById('{1}');\n", InfoProvider.HeaderCheckBoxObjName, InfoProvider.HeaderCheckBoxID);
            builder.AppendFormat("{0}.onCheckBoxCreated = {1};\n", JsObjName, InfoProvider.CheckBoxCreatedFuncName);
            builder.AppendFormat("{0}.checkBoxItemPrefix = '{1}';\n", JsObjName, InfoProvider.RowCheckBoxPrefix);

            // Builder check/uncheck function
            builder.AppendLine();
            builder.AppendFormat("function {0}(cellObj)\n", InfoProvider.CheckBoxCreatedFuncName);
            builder.AppendLine("{");
            builder.AppendFormat(" cellObj.disabledF({0}.checked);", InfoProvider.HeaderCheckBoxObjName);
            builder.AppendLine("}");

            builder.AppendLine();
            builder.AppendFormat("function {0}(index)\n", InfoProvider.HeaderCheckBoxClickHandlerName);
            builder.AppendLine("{");
            builder.AppendLine("chkBoxes = document.getElementsByTagName('input');");
            builder.AppendLine(" for (i = 0; i < chkBoxes.length; i++)");
            builder.AppendLine("{");
            builder.AppendLine("if (chkBoxes[i].type.toLowerCase() != 'checkbox') continue;");
            builder.AppendFormat("if (chkBoxes[i].id.indexOf('{0}') != 0) continue;\n", InfoProvider.RowCheckBoxPrefix);
            builder.AppendFormat("chkBoxes[i].disabled = {0}.checked;\n", InfoProvider.HeaderCheckBoxObjName);
            builder.AppendLine("}");
            builder.AppendLine("}");
        }

        /// <summary>
        /// Generates sort script code for current grid
        /// </summary>
        /// <returns>Java script code</returns>
        private string GetSortScript()
        {
            StringBuilder builder = new StringBuilder();

            builder.AppendFormat("function {0}(columnKey)\n", InfoProvider.SortFuncName);
            builder.AppendLine("{");
            builder.AppendFormat("{0}.updateSort(columnKey);\n", JsObjName);
            builder.AppendLine("var sortExpr = '';");
            builder.AppendFormat("if ({0}.curColumnKey) " +
                " sortExpr ='&columnKey=' + escape({0}.curColumnKey) + '&sortAsc=' + {0}.isAscSort;\n", JsObjName);
            builder.AppendFormat("{0}.clearAll();\n", JsObjName);
            builder.AppendFormat("{0}.enableSmartRendering(true, {1}, 50);\n", JsObjName, RowCount);
            builder.AppendFormat("{0}.loadXML({1} + sortExpr);\n", JsObjName, InfoProvider.XmlSourceUrlObj);
            builder.AppendLine("}");

            return builder.ToString();
        }

        /// <summary>
        /// Checks if checkbox column was added
        /// </summary>
        /// <returns>True if column was found</returns>
        private bool CheckBoxWasAdded()
        {
            foreach (GridColumn column in Columns)
            {
                if (column.ColumnType == "ch")
                    return true;
            }
            return false;
        }


        #endregion
    }
}
