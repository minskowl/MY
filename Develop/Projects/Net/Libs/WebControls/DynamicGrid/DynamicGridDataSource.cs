#region Version & Copyright
/* 
 * $Id: DynamicGridDataSource.cs 24224 2007-11-20 15:08:13Z svd $ 
 *
 * <:COPYRIGHT_LICENSE:>
 */
#endregion

using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Xml;

namespace Savchin.Web.UI
{
    /// <summary>
    /// DynamicGrid DataSource class. AJAX data source
    /// </summary>
    public class DynamicGridDataSource : Control
    {
        #region Constants

        protected const string RootElementName = "rows";
        protected const string RowElementName = "row";
        protected const string CellElementName = "cell";
        protected const string RowIdAttrName = "id";
        protected const string PosAttrName = "pos";

        #endregion

        #region Properties
        private string _actionTemplate = string.Empty;
        /// <summary>
        /// Gets or sets action template
        /// </summary>
        public string ActionTemplate
        {
            get { return _actionTemplate; }
            set { _actionTemplate = value; }
        }

        /// <summary>
        /// Specifies if action template should be used
        /// </summary>
        protected virtual bool UseActionTemplate
        {
            get { return !string.IsNullOrEmpty(ActionTemplate); }
        }

        private readonly List<string> _dataColumns = new List<string>();
        /// <summary>
        /// Gets the cells.
        /// </summary>
        /// <value>The cells.</value>
        public List<string> DataColumns
        {
            get { return _dataColumns; }
        }

        private string _dataKey = string.Empty;
        /// <summary>
        /// Gets or sets the row id.
        /// </summary>
        /// <value>The row id.</value>
        public string DataKey
        {
            get { return _dataKey; }
            set { _dataKey = value; }
        }
        #endregion


        /// <summary>
        /// Gets the data table.
        /// </summary>
        /// <returns></returns>
        public virtual DataTable GetDataTable(DataRequestEventArgs args)
        {
            return null;
        }

        /// <summary>
        /// Gets the data XML.
        /// </summary>
        /// <returns></returns>
        public virtual string GetDataXml(DataRequestEventArgs args)
        {

            DataTable table = GetDataTable(args);


            StringBuilder builder = new StringBuilder();

            using (XmlWriter writer = XmlWriter.Create(builder))
            {
                writer.WriteStartElement(RootElementName);
                writer.WriteAttributeString(PosAttrName, (args.StartIndex ?? 0).ToString());

                if (table != null)
                    ProcessRows(writer, table.Rows);

                writer.WriteEndElement();
            }

            return builder.ToString();
        }

        /// <summary>
        /// Processes the rows.
        /// </summary>
        /// <param name="writer">The writer.</param>
        /// <param name="rows">The rows.</param>
        private void ProcessRows(XmlWriter writer, DataRowCollection rows)
        {
            foreach (DataRow row in rows)
            {
                writer.WriteStartElement(RowElementName);
                writer.WriteAttributeString(RowIdAttrName, row[DataKey].ToString());

                foreach (string cell in DataColumns)
                {
                    writer.WriteElementString(CellElementName,
                                              ProcessColumn(writer, row, cell));
                }

                if (UseActionTemplate)
                    writer.WriteElementString(CellElementName, GetActionColumn(row));

                writer.WriteEndElement();
            }
        }

        /// <summary>
        /// Processes the column.
        /// </summary>
        /// <param name="writer">The writer.</param>
        /// <param name="row">The row.</param>
        /// <param name="columnName">Name of the column.</param>
        protected virtual string ProcessColumn(XmlWriter writer, DataRow row, string columnName)
        {
            return HttpUtility.HtmlEncode(row[columnName].ToString());
        }

        /// <summary>
        /// Gets the action column.
        /// </summary>
        /// <param name="row">The row.</param>
        /// <returns></returns>
        private string GetActionColumn(DataRow row)
        {
            return string.Format(GetActionTemplate(row[DataKey].ToString(), row), row[DataKey]);
        }

        /// <summary>
        /// Gets action template for specified row
        /// </summary>
        /// <param name="rowId">Id of row to check</param>
        /// <param name="row">The row.</param>
        /// <returns>Action template to use</returns>
        protected virtual string GetActionTemplate(string rowId, DataRow row)
        {
            return ActionTemplate;
        }
    }
}
