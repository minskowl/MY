#region Version & Copyright
/* 
 * $Id: RowItem.cs 23739 2007-11-06 09:31:43Z svd $ 
 *
 * <:COPYRIGHT_LICENSE:>
 */
#endregion

using System.Collections.Generic;
using System.Text;
using Savchin.Web;


namespace Savchin.Web.UI
{
    /// <summary>
    /// Represents row data
    /// </summary>
    public class RowItem
    {
        #region Fields

        private string       _id             = string.Empty;
        private List<string> _values         = new List<string>();

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets id of data row
        /// </summary>
        public string ID
        {
            get { return _id;  }
            set { _id = value; }
        }

        /// <summary>
        /// Gets list of values
        /// </summary>
        public List<string> Values
        {
            get { return _values; }
        }

        #endregion

        #region Construction

        /// <summary>
        /// Default contructor
        /// </summary>
        public RowItem()
        {
        }

        /// <summary>
        /// Initializes instance with specified values
        /// </summary>
        /// <param name="id">Id of item</param>
        /// <param name="values">List of item values</param>
        public RowItem(string id, params string[] values)
        {
            ID = id;

            foreach (string value in values)
            {
                Values.Add(value);
            }
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Converts data to javaScript parameters array
        /// </summary>
        /// <returns>Add row function parametes</returns>
        public string ToJavaParamArray()
        {
            StringBuilder builder = new StringBuilder();

            bool isFirstItem = true;
            builder.Append("[");

            foreach (string value in Values)
            {
                if (isFirstItem)
                    isFirstItem = false;
                else
                    builder.Append(", ");

                builder.Append(JavaScriptBuilder.ConvertToJavaScriptLine(value));
            }
            builder.Append("]");

            return builder.ToString();
        }

        #endregion
    }
}
