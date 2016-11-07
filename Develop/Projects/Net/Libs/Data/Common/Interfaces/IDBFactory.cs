using System.Data;
using System.Data.Common;

namespace Savchin.Data.Common
{
    /// <summary>
    /// 
    /// </summary>
    public interface IDBFactory
    {
        
        /// <summary>
        /// Creates the DB convertor.
        /// </summary>
        /// <returns></returns>
        IDbConvertor CreateConvertor();
        /// <summary>
        /// Gets the convertor.
        /// </summary>
        /// <value>The convertor.</value>
        IDbConvertor Convertor { get; }

        /// <summary>
        /// Creates the command.
        /// </summary>
        /// <returns></returns>
        IDbCommand CreateCommand();

        /// <summary>
        /// Creates the command.
        /// </summary>
        /// <param name="commandText">The command text.</param>
        /// <returns></returns>
        IDbCommand CreateCommand(string commandText);

        /// <summary>
        /// Creates the connection.
        /// </summary>
        /// <returns></returns>
        IDbConnection CreateConnection();

        /// <summary>
        /// Creates the data adapter.
        /// </summary>
        /// <returns></returns>
        IDataAdapter CreateDataAdapter(IDbCommand command);



        /// <summary>
        /// Creates the SP command.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        IDbCommand CreateSPCommand(string name);

        /// <summary>
        /// Creates the SQL command.
        /// </summary>
        /// <param name="sqlQuery">The SQL query.</param>
        /// <returns></returns>
        IDbCommand CreateSqlCommand(string sqlQuery);


        /// <summary>
        /// Creates the exception convertor.
        /// </summary>
        /// <returns></returns>
        IExceptionConverter CreateExceptionConvertor();

        #region Parameteres Factories
        /// <summary>
        /// Creates the array parameter.
        /// </summary>
        /// <returns></returns>
        IArrayParameter CreateArrayParameter();

        /// <summary>
        /// Creates the parameter.
        /// </summary>
        /// <returns></returns>
        IDbDataParameter CreateParameter();
        /// <summary>
        /// Creates the parameter.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="value">The value.</param>
        /// <param name="direction">The direction.</param>
        /// <returns></returns>
        IDbDataParameter CreateParameter(string name, object value, ParameterDirection direction);

        /// <summary>
        /// Creates the parameter return value.
        /// </summary>
        /// <returns></returns>
        IDbDataParameter CreateParameterReturnValue();

        /// <summary>
        /// Creates the parameter input.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        IDbDataParameter CreateParameterInput(string name, object value); 
        #endregion

    }
}