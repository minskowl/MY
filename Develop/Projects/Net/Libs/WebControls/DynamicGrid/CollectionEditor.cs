#region Version & Copyright
/* 
 * $Id: CollectionEditor.cs 24818 2007-11-30 15:56:43Z svd $ 
 *
 * <:COPYRIGHT_LICENSE:>
 */
#endregion

using System;
using System.Text;
using System.Web.UI.WebControls;
using Savchin.Web;


namespace Savchin.Web.UI
{
    /// <summary>
    /// Represens single column colection designer
    /// </summary>
    public class CollectionEditor : StaticGrid
    {
        #region Properties

        /// <summary>
        /// Gets or sets delete image url
        /// </summary>
        public string DeleteImageUrl
        {
            get
            {
                return (string)ViewState["DeleteImageUrl"] ?? "images/delete_16.gif";
            }
            set
            {
                ViewState["DeleteImageUrl"] = value;
            }
        }

        /// <summary>
        /// Sets css class of image
        /// </summary>
        public string ImageCssClass
        {
            get
            {
                return (string)ViewState["ImageCssClass"] ?? "dynGridImage";
            }
            set
            {
                ViewState["ImageCssClass"] = value;
            }
        }

        /// <summary>
        /// Gets or sets new row prefix for newly created rows
        /// </summary>
        public string NewRowIdPrefix
        {
            get
            {
                if (ViewState["NewRowIdPrefix"] == null)
                    return "newRow";
                return ViewState["NewRowIdPrefix"] as string;
            }
            set
            {
                ViewState["NewRowIdPrefix"] = value;
            }
        }

        /// <summary>
        /// Gets or sets prompt text for new row editor
        /// </summary>
        public string NewRowPromptText
        {
            get
            {
                if (ViewState["NewRowPromptText"] == null)
                    return "New item";
                return ViewState["NewRowPromptText"] as string;
            }
            set
            {
                ViewState["NewRowPromptText"] = value;
            }
        }

        /// <summary>
        /// Gets or sets item column name
        /// </summary>
        public string ItemColumnName
        {
            get
            {
                if (ViewState["ItemColumnName"] == null)
                    return string.Empty;
                return ViewState["ItemColumnName"] as string;
            }
            set
            {
                ViewState["ItemColumnName"] = value;
            }
        }

        /// <summary>
        /// Gets or sets action column nam
        /// </summary>
        public string ActionColumnName
        {
            get
            {
                return (string)ViewState["ActionColumnName"] ?? "Action";
            }
            set
            {
                ViewState["ActionColumnName"] = value;
            }
        }

        /// <summary>
        /// Gets name of fuction for item removal
        /// </summary>
        private string JsRemoveItemFuncName
        {
            get { return ClientID + "RemoveItem"; }
        }

        #endregion


        /// <summary>
        /// Default contructor
        /// </summary>
        public CollectionEditor()
        {
            AllowEditEvents = true;
            EnableDragAndDrop = true;
            UseActionTemplate = true;

 
            LineCssClass = "grdRow";
            AltLineCssClass = "grdAltRow";
            BackgroundCssClass = "lstAltRow";
            GridCellStyle = "border-right: #CCCCDD 1px solid; border-left: #CCCCDD 1px solid; font-size:12px;";
            HeaderColumnStyle =
                "background-repeat: repeat-x;border-color:#80A1B6;border-width:1px;border-style:solid;font-size:7pt;horizontal-align: left; font-family:Verdana; font-weight:bold;text-decoration:underline;";

        }


        #region Overriden methods

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            ActionTemplate = string.Format(
                "<a href='javascript:void(0)' onclick=\"{0}\" ><img class=\"{1}\" src=\"{2}\" alt=\"Remove item\" /></a>",
                JsRemoveItemFuncName + "('{0}')",
                ImageCssClass,
                ControlHelper.GetThemebleUrl(DeleteImageUrl, Page.Theme)); ;


            Columns.Clear();
            Columns.Add(new GridColumn(ItemColumnName, new Unit(90, UnitType.Percentage), ColumnAlignment.Left));
            Columns.Add(new GridColumn(ActionColumnName, new Unit(10, UnitType.Percentage), ColumnAlignment.Center));
        }


        /// <summary>
        /// Generates script for collection editor
        /// </summary>
        /// <returns>Editor initialization code</returns>
        protected override string GetGridCreationScript()
        {
            StringBuilder builder = new StringBuilder();

            builder.AppendLine(base.GetGridCreationScript());

            builder.AppendFormat("{0}.newRowPromptText = {1};",
                JsObjName,
                JavaScriptBuilder.ConvertToJavaScriptLine(NewRowPromptText));
            builder.AppendFormat("{0}.newRowIdPrefix = '{1}';", JsObjName, NewRowIdPrefix);
            builder.AppendFormat("{0}.addNewRowEditor();", JsObjName);

            return builder.ToString();
        }

        protected override string BuildEditEventHandlerStatement()
        {
            StringBuilder builder = new StringBuilder();

            builder.AppendLine(base.BuildEditEventHandlerStatement());
            builder.AppendFormat("if ({0} == 2)\n", StageParamName);
            builder.AppendLine("{");
            builder.AppendFormat("if (!{0}.isNewRowEditor({1})) return true;\n", JsObjName, RowIdParamName);
            builder.AppendFormat("if ({0} == {1}) return false;\n", OldValueParamName, NewValueParamName);
            builder.AppendFormat("{0}.addNewRowEditor();\n", JsObjName);
            builder.AppendLine("}");

            return builder.ToString();
        }

        protected override void RegisterScripts()
        {
            base.RegisterScripts();

            Page.ClientScript.RegisterClientScriptBlock(
                typeof(CollectionEditor),
                JsRemoveItemFuncName,
                BuildDeleteFuncCode(),
                true);
        }

        /// <summary>
        /// Builds item removal function
        /// </summary>
        /// <returns>javaScript code of item removal function</returns>
        private string BuildDeleteFuncCode()
        {
            StringBuilder builder = new StringBuilder();

            builder.AppendFormat("function {0}(rowId)", JsRemoveItemFuncName);
            builder.AppendLine("{");
            builder.AppendFormat("if ({0}.isNewRowEditor(rowId)) return;", JsObjName);
            builder.AppendFormat("{0}.deleteRow(rowId);", JsObjName);
            builder.AppendLine("}");

            return builder.ToString();
        }


        #endregion
    }
}
