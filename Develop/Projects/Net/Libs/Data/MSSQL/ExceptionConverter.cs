using System;
using System.Data.Common;
using System.Data.SqlClient;
using System.Globalization;
using System.Text.RegularExpressions;
using Savchin.Data.Common;

namespace Savchin.Data.MSSQL
{
    internal class ExceptionConverter : IExceptionConverter
    {
        private readonly ErrorMessageParser _messageParser;
        /// <summary>
        /// Initializes a new instance of the <see cref="ExceptionConverter"/> class.
        /// </summary>
        /// <param name="messageParser">The message parser.</param>
        internal ExceptionConverter(ErrorMessageParser messageParser)
        {
            _messageParser = messageParser;
        }

        /// <summary>
        /// SQLs the exception handling.
        /// </summary>
        /// <param name="exception">The exception.</param>
        /// <returns></returns>
        DbException IExceptionConverter.Convert(DbException exception)
        {
            var sqlException = (SqlException)exception;
            if (exception == null) return null;

            var col = new SqlErrorCollection();
            var message = "Unhandled Sql Exception: " + Environment.NewLine;

            foreach (System.Data.SqlClient.SqlError err in (sqlException.Errors))
            {
                var result = SqlErrorHandling(exception ,err);
                if (result != null)
                    return result;
                col.Add(err);

                message += err.Message + Environment.NewLine;
            }

            if (col.Count > 0) return new UnhandledSqlException(message, col, sqlException);

            return null;
        }

        /// <summary>
        /// SQLs the error handling. This method convert System.Data.SqlClient.SqlError to Exceptions:
        /// LongFieldException,NotUniqueException,ExceptionParentObjectNotExists,ChildObjectExistsException
        /// </summary>
        /// <param name="exception">The exception.</param>
        /// <param name="err">The SQL Error.</param>
        /// <returns></returns>
        private DbException SqlErrorHandling(DbException exception, System.Data.SqlClient.SqlError err)
        {
            DbException result = null;
            switch (err.Number)
            {
                case (int)ErrorsCode.TruncateData:
                    result = new LongFieldException();
                    break;
                case (int)ErrorsCode.InsertDuplicatedKey:
                case (int)ErrorsCode.ConstraintInsertDuplicatedKey:
                    var sp = err.Message.Split('\'');
                    var name = (sp.Length > 3) ? sp[1] + '.' + sp[3] : "";
                    result = new NotUniqueException(name, exception);
                    break;
                case (int)ErrorsCode.ConstraintStatementConflicted:
                    result = ParseConstraintStatementConflicted(err.Message);
                    break;
                case (int)ErrorsCode.InputParameterDBNull:
                    result = new SPParameterNullException("Some SP parameters is DbNull");
                    break;
                case (int)ErrorsCode.CannotInsertDBNull:
                    var m = _messageParser.CannotInsertNULLParser.Match(err.Message);
                    if (m.Success == false) return null;

                    var column = m.Groups["COLUMN"].Value;
                    var tableName = Parser.ParseTableName(m.Groups["NAME"].Value);

                    result = new MandatoryFieldEmptyException(tableName, column, exception);
                    break;

                case (int)ErrorsCode.StoredProcedureNotFind:
                    result = new SPNotFindException(err.Message, exception);
                    break;
                case (int)ErrorsCode.StoredProcedureNotFindParrameter:
                case (int)ErrorsCode.StoredProcedureExpectParrameter:
                    result = new SPIncorrectCountParameters(err.Message, exception);
                    break;
                case (int)ErrorsCode.UserDefinedValidateException:
                    result = new ValidateException(err.Message);
                    break;
                default:
                    if (err.Number > 50001) result = new UserDefinedSqlException(err);
                    break;
            }
            return result;
        }

        /// <summary>
        /// Parses the constraint statement conflicted.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <returns></returns>
        internal DbException ParseConstraintStatementConflicted(string message)
        {
            DbException result = null;
            var m = _messageParser.ConstraintStatementConflictedParser.Match(message);

            if (m.Success == false)
                return result;
            var oper = m.Groups["OPERATION"].Value.ToUpper(CultureInfo.CurrentCulture);
            //var constraintType = m.Groups["TYPE"].Value.ToUpper(CultureInfo.CurrentCulture);
            var tableName = m.Groups["TBLNAME"].Value;

            if (oper.CompareTo("INSERT") == 0 || oper.CompareTo("UPDATE") == 0)
                result = new ParentEntityNotExistsException(tableName);
            else if (oper.CompareTo("DELETE") == 0)
                result = new ChildEntityExistsException(tableName);
            return result;
        }
    }
}
