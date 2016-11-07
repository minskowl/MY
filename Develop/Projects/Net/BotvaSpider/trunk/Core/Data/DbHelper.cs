using System;
using System.Data;
using System.Data.Common;
using BotvaSpider.Core;


namespace BotvaSpider.Data
{
    static class DbHelper
    {
        /// <summary>
        /// Adds the parameter.
        /// </summary>
        /// <param name="command">The command.</param>
        /// <param name="filter">The filter.</param>
        public static void AddParameter(this DbCommand command,LevelFilter filter)
        {
            command.AddParameter("@LevelFrom", filter.LevelFrom);
            command.AddParameter("@LevelTo", filter.LevelTo);
        }

        /// <summary>
        /// Adds the parameter.
        /// </summary>
        /// <param name="command">The command.</param>
        /// <param name="name">The name.</param>
        /// <param name="value">The value.</param>
        public static void AddParameter(this DbCommand command, string name, object value)
        {
            var parameter = command.CreateParameter();
            parameter.ParameterName = name;
            parameter.Value = value;
            command.Parameters.Add(parameter);
        }

        /// <summary>
        /// Adds the parameter.
        /// </summary>
        /// <param name="command">The command.</param>
        /// <param name="name">The name.</param>
        /// <param name="type">The type.</param>
        /// <param name="value">The value.</param>
        public static void AddParameter(this DbCommand command, string name, DbType type, object value)
        {
            var parameter = command.CreateParameter();
            parameter.ParameterName = name;
            parameter.DbType = type;
            parameter.Value = value;
            command.Parameters.Add(parameter);
        }

        /// <summary>
        /// Adds the parameter.
        /// </summary>
        /// <param name="command">The command.</param>
        /// <param name="name">The name.</param>
        public static DbParameter AddParameter(this DbCommand command, string name)
        {
            var parameter = command.CreateParameter();
            parameter.ParameterName = name;
            command.Parameters.Add(parameter);
            return parameter;
        }

        /// <summary>
        /// Adds the parameter.
        /// </summary>
        /// <param name="command">The command.</param>
        /// <param name="name">The name.</param>
        /// <param name="type">The type.</param>
        /// <returns></returns>
        public static DbParameter AddParameter(this DbCommand command, string name, DbType type)
        {
            var parameter = command.CreateParameter();
            parameter.ParameterName = name;
            parameter.DbType = type;
            command.Parameters.Add(parameter);
            return parameter;
        }
        /// <summary>
        /// Creates the command.
        /// </summary>
        /// <param name="connection">The connection.</param>
        /// <param name="text">The text.</param>
        /// <returns></returns>
        public static DbCommand CreateCommand(this DbConnection connection, string text)
        {
            var command = connection.CreateCommand();
            command.CommandText = text;
            return command;
        }

        /// <summary>
        /// Gets the inserted ID.
        /// </summary>
        /// <param name="connection">The connection.</param>
        /// <returns></returns>
        public static int GetInsertedID(this DbConnection connection)
        {
            var command = connection.CreateCommand();
            command.CommandText = "SELECT @@IDENTITY";
            return (int)command.ExecuteScalar();
        }
        /// <summary>
        /// Gets the tick date time.
        /// </summary>
        /// <param name="reader">The reader.</param>
        /// <param name="ordinal">The ordinal.</param>
        /// <returns></returns>
        public static DateTime GetTickDateTime(this DbDataReader reader, int ordinal)
        {
            return new DateTime(reader.GetInt64(ordinal));
        }

    }
}