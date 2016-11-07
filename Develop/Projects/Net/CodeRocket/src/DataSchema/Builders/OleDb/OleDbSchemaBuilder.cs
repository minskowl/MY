using System;
using System.Collections.Generic;
using System.Data.OleDb;
using Savchin.Data.Schema.Builders;

namespace Savchin.Data.Schema
{
    /// <summary>
    /// OleDbSchemaBuilder
    /// </summary>
    public class OleDbSchemaBuilder : IDataSchemaBuilder
    {

        CommonServerSchemaBuilder builder;

        /// <summary>
        /// Gets the errors.
        /// </summary>
        /// <value>The errors.</value>
        public IList<Exception> Errors
        {
            get { return builder.Errors; }
        }

        /// <summary>
        /// Builds the database schema.
        /// </summary>
        /// <param name="connectionString">The connection string.</param>
        /// <returns></returns>
        public DatabaseSchema BuildDatabaseSchema(string connectionString)
        {

            using (OleDbConnection connection = new OleDbConnection(connectionString))
            {
                connection.Open();

                switch (GetServerType(connection))
                {
                    case ServerType.Mssql2000:
                    case ServerType.Mssql2005:
                        builder = new OleDbMssqlBuilder(connection);
                        break;
                    case ServerType.Other:

                    default:
                        builder = new CommonServerSchemaBuilder(connection);
                        break;
                }
                return builder.BuildDatabaseSchema();
            }

        }


        private ServerType GetServerType(OleDbConnection connection)
        {
            switch (connection.Provider)
            {
                case "SQLOLEDB":
                    return (connection.ServerVersion.StartsWith("9.")) ? ServerType.Mssql2005 : ServerType.Mssql2000;

                default:
                    return ServerType.Other;
            }
        }





    }


}
