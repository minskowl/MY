using System;
using System.Data;
using System.Data.SQLite;
using Savchin.Data.Common;

namespace KnowledgeBase.SqlLite.Dal.Core
{
    /// <summary>
    /// SqlLiteFactory
    /// </summary>
    public class SqlLiteFactory : DbFactory
    {
        private readonly SQLiteFactory  _factory;

        /// <summary>
        /// Initializes a new instance of the <see cref="SqlLiteFactory"/> class.
        /// </summary>
        public SqlLiteFactory()
        {
            _factory = SQLiteFactory.Instance;
        }

        /// <summary>
        /// Gets the native factory.
        /// </summary>
        /// <value>The native factory.</value>
        public SQLiteFactory NativeFactory
        {
            get { return _factory; }
        }

        #region Overrides of DBFactory
        /// <summary>
        /// Creates the command.
        /// </summary>
        /// <param name="commandText">The command text.</param>
        /// <returns></returns>
        public override IDbCommand CreateCommand(string commandText)
        {
            return CreateSqlCommand(commandText);
        }
        /// <summary>
        /// Creates the command.
        /// </summary>
        /// <returns></returns>
        public override IDbCommand CreateCommand()
        {
            return _factory.CreateCommand();
        }

        /// <summary>
        /// Creates the connection.
        /// </summary>
        /// <returns></returns>
        public override IDbConnection CreateConnection()
        {
            return _factory.CreateConnection();
        }
        /// <summary>
        /// Creates the data adapter.
        /// </summary>
        /// <returns></returns>
        public override IDataAdapter CreateDataAdapter(IDbCommand command)
        {
            return new SQLiteDataAdapter((SQLiteCommand)command);
        }
        /// <summary>
        /// Creates the parameter.
        /// </summary>
        /// <returns></returns>
        public override IDbDataParameter CreateParameter()
        {
            return _factory.CreateParameter();
        }

        /// <summary>
        /// Creates the exception convertor.
        /// </summary>
        /// <returns></returns>
        public override IExceptionConverter CreateExceptionConvertor()
        {
            return new ExceptionConverter();
        }

        /// <summary>
        /// Creates the array parameter.
        /// </summary>
        /// <returns></returns>
        public override IArrayParameter CreateArrayParameter()
        {
            throw new NotImplementedException();
        }



        /// <summary>
        /// Creates the DB convertor.
        /// </summary>
        /// <returns></returns>
        public override IDbConvertor CreateConvertor()
        {
            return new DbConvertor();
        }

 

        #endregion
    }
}
