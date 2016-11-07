using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using Savchin.Data;
using Savchin.Data.Common;

namespace KnowledgeBase.SqlLite.Dal.Core
{
    class ExceptionConverter : IExceptionConverter
    {
        #region Implementation of IExceptionConverter

        /// <summary>
        /// Converts the specified exception.
        /// </summary>
        /// <param name="exception">The exception.</param>
        /// <returns></returns>
        public DbException Convert(DbException exception)
        {
            var sqlException = (SQLiteException)exception;
            if (exception == null) return null;

            var col = new SqlErrorCollection();
            var message = "Unhandled Sql Exception: " + Environment.NewLine;

            //foreach (System.Data.SqlClient.SqlError err in (sqlException.Errors))
            //{
            //    var result = SqlErrorHandling(exception, err);
            //    if (result != null)
            //        return result;
            //    col.Add(err);

            //    message += err.Message + Environment.NewLine;
            //}

             return new UnhandledSqlException(message, col, sqlException);
        }

        #endregion
    }
}
