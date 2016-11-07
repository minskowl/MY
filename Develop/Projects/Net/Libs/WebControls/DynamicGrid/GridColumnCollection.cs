#region Version & Copyright
/* 
 * $Id$ 
 *
 * <:COPYRIGHT_LICENSE:>
 */
#endregion

using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI.WebControls;
using Savchin.Web;


namespace Savchin.Web.UI
{
    /// <summary>
    /// Stored collection of grid columns
    /// </summary>
    [Serializable]
    public class GridColumnCollection : List<GridColumn>
    {
        #region Construction

        /// <summary>
        /// Default constructor. Peforms no initialization
        /// </summary>
        public GridColumnCollection()
        {
        }

        #endregion

        #region Properties

        /// <summary>
        /// Checks if items are specified in pixels
        /// </summary>
        public bool IsPixelWidth
        {
            get 
            {
                if (this.Count < 1)
                    return false;
                return this[0].Width.Type == UnitType.Pixel; 
            }
        }

        #endregion

        /// <summary>
        /// Returns list of column types
        /// </summary>
        /// <returns>Coma separated column type list</returns>
        public string GetTextList()
        {
            if (Count < 1)
                return string.Empty;

            StringBuilder builder = new StringBuilder();

            for (int i = 0; i < (Count - 1); i++)
            {
                    builder.Append(GetColumnText(i) + "|");
            }
            builder.Append(GetColumnText(Count - 1));

            return  JavaScriptBuilder.ConvertToJavaScriptLine(builder.ToString());
        }


        /// <summary>
        /// Wraps specified text with link
        /// </summary>
        /// <param name="index">Column index</param>
        /// <returns>Text of column</returns>
        private string GetColumnText(int index)
        {
            if (string.IsNullOrEmpty(this[index].HeaderPattern))
                return this[index].Text;
            return string.Format(
                this[index].HeaderPattern, 
                this[index].DataKey, 
                this[index].Text);
        }


        /// <summary>
        /// Gets column type list string
        /// </summary>
        /// <returns>Pixed width list</returns>
        public string GetWidthsList()
        {
            if (Count < 1)
                return string.Empty;

            StringBuilder builder = new StringBuilder();

            for (int i = 0; i < (Count - 1); i++)
            {
                builder.Append(this[i].Width.Value.ToString() + ",");
            }
            builder.Append(this[Count - 1].Width.Value.ToString());

            return builder.ToString();
        }

        /// <summary>
        /// Returns list of alignments
        /// </summary>
        /// <returns>Alignments list</returns>
        public string GetAlignmentsList()
        {
            if (Count < 1)
                return string.Empty;

            StringBuilder builder = new StringBuilder();

            for (int i = 0; i < (Count - 1); i++)
            {
                builder.Append(this[i].Alignment.ToString().ToLower() + ",");
            }
            builder.Append(this[Count - 1].Alignment.ToString().ToLower());

            return builder.ToString();
        }


        /// <summary>
        /// Returns column type list
        /// </summary>
        /// <returns>List of types</returns>
        public string GetTypeList()
        {
            if (Count < 1)
                return string.Empty;

            StringBuilder builder = new StringBuilder();

            for (int i = 0; i < (Count - 1); i++)
            {
                builder.Append(this[i].ColumnType + ",");
            }
            builder.Append(this[Count - 1].ColumnType);

            return builder.ToString();
        }

    }
}
