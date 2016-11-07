using System;
using System.Data;

namespace Savchin.Data.Common
{
    /// <summary>
    /// DataTable Helper class
    /// </summary>
    public static class DataTableHelper
    {
        /// <summary>
        /// Removes the rows.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <param name="where">The where predicate.</param>
        public static void RemoveRows(this DataTable data, Predicate<DataRow> where)
        {
            var i = 0;
            while (data.Rows.Count > i)
            {
                var row = data.Rows[i];

                if (where(row))
                {
                    i++;
                }
                else
                {
                    data.Rows.RemoveAt(i);
                }

            }
        }
    }
}
