#region Version & Copyright
/* 
 * $Id: HtmlTableEx.cs 19978 2007-08-15 09:11:28Z svd $ 
 *
 * <:COPYRIGHT_LICENSE:>
 */
#endregion

using System.Web.UI;
using System.Web.UI.HtmlControls;

namespace Savchin.Web.UI
{
    public class HtmlTableEx : HtmlTable
    {
        /// <summary>
        /// Creates the cell.
        /// </summary>
        /// <param name="control">The control.</param>
        public HtmlTableCell CreateCell(Control control)
        {
            var row = new HtmlTableRow();
            HtmlTableCell cell = new HtmlTableCell();
            row.Cells.Add(cell);

            cell.Controls.Add(control);

            Rows.Add(row);

            return cell;
        }

        /// <summary>
        /// Creates the cell.
        /// </summary>
        /// <param name="innerText">The inner text.</param>
        /// <returns></returns>
        public HtmlTableCell CreateCell(string innerText)
        {
            HtmlTableRow row = new HtmlTableRow();
            HtmlTableCell cell = new HtmlTableCell();
            row.Cells.Add(cell);

            cell.InnerText = innerText;

            Rows.Add(row);

            return cell;
        }
        /// <summary>
        /// Creates the cell.
        /// </summary>
        /// <returns></returns>
        public HtmlTableCell CreateCell()
        {
            HtmlTableRow row = new HtmlTableRow();
            HtmlTableCell cell = new HtmlTableCell();
            row.Cells.Add(cell);
            Rows.Add(row);

            return cell;
        }
        /// <summary>
        /// Creates the row.
        /// </summary>
        /// <returns></returns>
        public HtmlTableRow CreateRow()
        {
            HtmlTableRow row = new HtmlTableRow();
            
            Rows.Add(row);

            return row;
        }
        /// <summary>
        /// Creates the row.
        /// </summary>
        /// <param name="cellCount">The cell count.</param>
        /// <returns></returns>
        public HtmlTableRow CreateRow(int cellCount)
        {
            HtmlTableRow row = new HtmlTableRow();

            for (int i = 0; i < cellCount; i++ )
            {
                row.Cells.Add(new HtmlTableCell());
            }
            Rows.Add(row);
            
            return row;
        }

        /// <summary>
        /// Creates the rows.
        /// </summary>
        /// <param name="rowCount">The row count.</param>
        /// <param name="cellCount">The cell count.</param>
        public void CreateRows(int rowCount,int cellCount)
        {
            for (int i = 0; i < rowCount; i++)
            {
                CreateRow(cellCount);
            }
        }
    }
}
