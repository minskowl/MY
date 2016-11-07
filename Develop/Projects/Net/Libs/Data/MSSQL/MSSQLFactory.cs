using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using Savchin.Data.Common;

namespace Savchin.Data.MSSQL
{
    /// <summary>
    /// MssqlFactory
    /// </summary>
    public abstract class MssqlFactory : DbFactory
    {
        private readonly SqlClientFactory sqlClientFactory;
        /// <summary>
        /// Initializes a new instance of the <see cref="MssqlFactory"/> class.
        /// </summary>
        protected MssqlFactory()
        {
            sqlClientFactory = SqlClientFactory.Instance;
        }

        /// <summary>
        /// Creates the array parameter.
        /// </summary>
        /// <returns></returns>
        public override IArrayParameter CreateArrayParameter()
        {
            return new ArrayParameter();
        }

        /// <summary>
        /// Creates the parameter.
        /// </summary>
        /// <returns></returns>
        public override IDbDataParameter CreateParameter()
        {
            return sqlClientFactory.CreateParameter();
        }

        /// <summary>
        /// Creates the data adapter.
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        public override IDataAdapter CreateDataAdapter(IDbCommand command)
        {
            return new SqlDataAdapter((SqlCommand)command);
        }

        /// <summary>
        /// Creates the command.
        /// </summary>
        /// <returns></returns>
        public override IDbCommand CreateCommand()
        {
            return sqlClientFactory.CreateCommand();
        }
        /// <summary>
        /// Creates the command.
        /// </summary>
        /// <param name="commandText">The command text.</param>
        /// <returns></returns>
        public override IDbCommand CreateCommand(string commandText)
        {
            return CreateSPCommand(commandText);
        }
        /// <summary>
        /// Creates the connection.
        /// </summary>
        /// <returns></returns>
        public override IDbConnection CreateConnection()
        {
            return sqlClientFactory.CreateConnection();
        }

        /// <summary>
        /// Creates the DB convertor.
        /// </summary>
        /// <returns></returns>
        public override IDbConvertor CreateConvertor()
        {
            return new DBConvertor();
        }
    }
}
