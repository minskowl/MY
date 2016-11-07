using System;
using System.Data;
using Savchin.Data.MSSQL;

namespace Savchin.Data.Common
{
    public abstract class DbFactory : IDBFactory
    {

        /// <summary>
        /// Creates the command.
        /// </summary>
        /// <returns></returns>
        public abstract IDbCommand CreateCommand();
        /// <summary>
        /// Creates the connection.
        /// </summary>
        /// <returns></returns>
        public abstract IDbConnection CreateConnection();

        /// <summary>
        /// Creates the data adapter.
        /// </summary>
        /// <returns></returns>
        public abstract IDataAdapter CreateDataAdapter(IDbCommand command);

        /// <summary>
        /// Creates the DB convertor.
        /// </summary>
        /// <returns></returns>
        public abstract IDbConvertor CreateConvertor();
        private IDbConvertor _convertor;
        /// <summary>
        /// Gets the convertor.
        /// </summary>
        /// <value>The convertor.</value>
        public IDbConvertor Convertor
        {
            get
            {
                if (_convertor == null)
                    _convertor = CreateConvertor();
                return _convertor;
            }
        }
        /// <summary>
        /// Creates the exception convertor.
        /// </summary>
        /// <returns></returns>
        public abstract IExceptionConverter CreateExceptionConvertor();

        /// <summary>
        /// Creates the command.
        /// </summary>
        /// <param name="commandText">The command text.</param>
        /// <returns></returns>
        public abstract IDbCommand CreateCommand(string commandText);



        /// <summary>
        /// Creates the SP command.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        public IDbCommand CreateSPCommand(string name)
        {
            var com = CreateCommand();
            com.CommandType = CommandType.StoredProcedure;
            com.CommandText = name;

            return com;
        }

        /// <summary>
        /// Creates the SQL command.
        /// </summary>
        /// <param name="sqlQuery">The SQL query.</param>
        /// <returns></returns>
        public IDbCommand CreateSqlCommand(string sqlQuery)
        {
            IDbCommand com = CreateCommand();
            com.CommandType = CommandType.Text;
            com.CommandText = sqlQuery;

            return com;
        }

        #region ParaMeter Factories
        /// <summary>
        /// Creates the array parameter.
        /// </summary>
        /// <returns></returns>
        public abstract IArrayParameter CreateArrayParameter();

        /// <summary>
        /// Creates the parameter.
        /// </summary>
        /// <returns></returns>
        public abstract IDbDataParameter CreateParameter();
        /// <summary>
        /// Creates the parameter.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="value">The value.</param>
        /// <param name="direction">The direction.</param>
        /// <returns></returns>
        public IDbDataParameter CreateParameter(string name, object value, ParameterDirection direction)
        {
            IDbDataParameter par = CreateParameter();
            par.ParameterName = name;
            par.Direction = direction;
            if (value == null)
                par.Value = DBNull.Value;
            else
            {
                if (value is bool)
                {
                    if ((bool)value)
                        par.Value = 1;
                    else
                        par.Value = 0;
                }
                else
                {
                    par.Value = value;
                }
            }

            return par;

        }

        /// <summary>
        /// Creates the parameter return value.
        /// </summary>
        /// <returns></returns>
        public IDbDataParameter CreateParameterReturnValue()
        {
            IDbDataParameter par = CreateParameter();
            par.Direction = ParameterDirection.ReturnValue;
            return par;
        }
        /// <summary>
        /// Creates the parameter input.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public IDbDataParameter CreateParameterInput(string name, object value)
        {
            return CreateParameter(name, value, ParameterDirection.Input);
        } 
        #endregion

        /// <summary>
        /// Gets the default.
        /// </summary>
        /// <value>The default.</value>
        public static IDBFactory Default
        {
            get
            {
                return new MSSQL2000Factory();
            }
        }


    }
}
