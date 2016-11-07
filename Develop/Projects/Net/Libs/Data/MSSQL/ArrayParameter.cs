using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using Savchin.Data.Common;
using Savchin.Data.Common.CommandBuilders;


namespace Savchin.Data.MSSQL
{
    /// <summary>
    /// ArrayParameter
    /// </summary>
    public class ArrayParameter : IArrayParameter
    {
        private DBConnection _connection;
        private IDbCommand _insertCommand;
        private PropertyInfo[] _fields;
        private SendParameter _sendStrategy;

        private delegate void SendParameter(object obj);

        /// <summary>
        /// Gets or sets the name of the table.
        /// </summary>
        /// <value>The name of the table.</value>
        public string TableName { get; set; }


        /// <summary>
        /// Sends the specified _connection.
        /// </summary>
        /// <param name="connection">The _connection.</param>
        /// <param name="parameters">The parameters.</param>
        public void Send(DBConnection connection, IEnumerable parameters)
        {
            if (parameters == null)
                return;

            _sendStrategy = null;
            _connection = connection;


            foreach (object parameter in parameters)
            {
                if (_sendStrategy == null)
                    _sendStrategy = Prepare(parameter.GetType());

                _sendStrategy(parameter);

            }
        }
        /// <summary>
        /// Sends the specified connection.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="connection">The connection.</param>
        /// <param name="parameters">The parameters.</param>
        public void Send<T>(DBConnection connection, IEnumerable<T> parameters)
        {
            if (parameters == null)
                return;

            _sendStrategy = Prepare(typeof(T)); 
            _connection = connection;


            foreach (object parameter in parameters)
            {
                _sendStrategy(parameter);
            }
        }


        private SendParameter Prepare(Type type)
        {
            if (string.IsNullOrEmpty(TableName))
                TableName = "#" + type.Name;

            return type.IsPrimitive ?
                PreparePrimitive(type) : PrepareComplex(type);
        }

        #region Primitives

        private void SendPrimitive(object parameter)
        {
            ((IDbDataParameter)_insertCommand.Parameters["@value"]).Value = parameter;
            _connection.ExecuteNonQuery(_insertCommand);
        }


        private SendParameter PreparePrimitive(Type parameterType)
        {
            var builder = new CreateTableCommandBuilder(_connection.Factory) {TableName = TableName};
            builder.AddColumn("value", parameterType);
            _connection.ExecuteNonQuery(builder.Build());

            var builderInsert = new InsertCommandBuilder(_connection.Factory) {TableName = TableName};
            builderInsert.AddParameter("value", parameterType);
            _insertCommand = builderInsert.Build();
            //insertCommand.Prepare();

            return SendPrimitive;
        }

        #endregion

        #region Complex Types
        private SendParameter PrepareComplex(Type parameterType)
        {
            _fields = parameterType.GetProperties();


            CreateTemproraryTable();
            CreateInsertCommand();
            return SendComplex;
        }

        private void SendComplex(object parameter)
        {
            foreach (PropertyInfo field in _fields)
                ((IDbDataParameter)_insertCommand.Parameters["@" + field.Name]).Value = field.GetValue(parameter, null);

            _connection.ExecuteNonQuery(_insertCommand);
        }

        /// <summary>
        /// Creates the insert command.
        /// </summary>
        /// <returns></returns>
        private void CreateInsertCommand()
        {
            var builder = new InsertCommandBuilder(_connection.Factory);
            builder.TableName = TableName;

            foreach (PropertyInfo field in _fields)
            {
                builder.AddParameter(field.Name, field.PropertyType);
            }


            _insertCommand = builder.Build();
            //insertCommand.Prepare();
        }

        /// <summary>
        /// Creates the temprorary table.
        /// </summary>
        private void CreateTemproraryTable()
        {
            var builder = new CreateTableCommandBuilder(_connection.Factory);
            builder.TableName = TableName;

            foreach (PropertyInfo field in _fields)
            {
                builder.AddColumn(field.Name, field.PropertyType);
            }
            _connection.ExecuteNonQuery(builder.Build());
        }

        #endregion


    }
}
