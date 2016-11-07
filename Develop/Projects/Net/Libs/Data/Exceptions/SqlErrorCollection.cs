using System;
using System.Collections;

namespace Savchin.Data
{
    /// <summary>
    /// SqlErrorCollection class.
    /// </summary>
    

    [Serializable()]
    public class SqlErrorCollection : ArrayList
    {
        /// <summary>
        /// Adds the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        internal void Add(System.Data.SqlClient.SqlError value)
        {
            Add(new SqlError(value));
        }


    }
}
