using System;
using System.Data.Common;

namespace Savchin.Data
{

    /// <summary>
    /// SqlUserDefinedException
    /// </summary>
    /// 

    [Serializable()]
    public class UserDefinedSqlException : DbException
    {
        #region Properties
        private int lineNumber;
        /// <summary>
        /// Gets the line number.
        /// </summary>
        /// <value>The line number.</value>
        public int LineNumber
        {
            get { return lineNumber; }
        }
        private int number;
        /// <summary>
        /// Gets the number.
        /// </summary>
        /// <value>The number.</value>
        public int Number
        {
            get { return number; }
        }
        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="T:UserDefinedSqlException"/> class.
        /// </summary>
        /// <param name="error">The error.</param>
        internal UserDefinedSqlException(System.Data.SqlClient.SqlError error)
            : base("Sql UserDefined Exception: " + error.Message)
        {
            lineNumber = error.LineNumber;
            number = error.Number;
        }
    }
}
