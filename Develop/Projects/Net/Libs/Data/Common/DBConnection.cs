using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.IO;
using log4net;

namespace Savchin.Data.Common
{
    /// <summary>
    /// Database Connection Class
    /// </summary>
    public class DBConnection : IDisposable, IDbConnection
    {
        #region Variables
        private static readonly ILog Log = LogManager.GetLogger(typeof(DBConnection));


        private readonly IDbConnection _connection;
        private IExceptionConverter _exceptionConverter;

        #endregion

        #region Properites

        /// <summary>
        /// Gets or sets the transaction.
        /// </summary>
        /// <value>The transaction.</value>
        public Transaction Transaction { get; set; }

        /// <summary>
        /// Gets or sets the factory.
        /// </summary>
        /// <value>The factory.</value>
        public IDBFactory Factory { get; set; }

        private readonly IDbConvertor _convertor;
        /// <summary>
        /// Gets the convertor.
        /// </summary>
        /// <value>The convertor.</value>
        public IDbConvertor Convertor
        {
            get { return _convertor; }
        }

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="DBConnection"/> class.
        /// </summary>
        /// <param name="connectionString">The connection string.</param>
        public DBConnection(string connectionString)
            : this(DbFactory.Default, connectionString)
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DBConnection"/> class.
        /// </summary>
        /// <param name="factory">The factory.</param>
        /// <param name="connectionString">The connection string.</param>
        public DBConnection(IDBFactory factory, string connectionString)
        {
            if (string.IsNullOrEmpty(connectionString))
            {
                throw new ArgumentException("connectionString is empty", "connectionString");
            }
            Factory = factory;
            _convertor = factory.CreateConvertor();
            _exceptionConverter = factory.CreateExceptionConvertor();
            _connection = factory.CreateConnection();
            Transaction = new Transaction(_connection);
            _connection.ConnectionString = connectionString;
            try
            {
                _connection.Open();
            }
            catch (SqlException ex)
            {
                Log.Fatal("Error open cobbection to ::" + connectionString, ex);
            }
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            Transaction.Dispose();
            _connection.Dispose();
            if (_exceptionConverter != null)
                _exceptionConverter = null;
        }

        #endregion

        #region Executes

        #region ExecuteReader

        /// <summary>
        /// Executes the reader.
        /// </summary>
        /// <param name="command">The CMD.</param>
        /// <returns></returns>
        public IDataReader ExecuteReader(IDbCommand command)
        {
            PrepareCommand(command);
            try
            {
                return command.ExecuteReader();
            }
            catch (DbException ex)
            {
                throw HandleException(command, ex);
            }

        }

        /// <summary>
        /// Executes the reader.
        /// </summary>
        /// <param name="commandText">The command text.</param>
        /// <returns></returns>
        public IDataReader ExecuteReader(string commandText)
        {
            return ExecuteReader(Factory.CreateCommand(commandText));
        }

        /// <summary>
        /// Executes the reader.
        /// </summary>
        /// <param name="spName">Name of the SP.</param>
        /// <param name="spParameters">The SP params.</param>
        /// <returns></returns>
        public IDataReader ExecuteReader(string spName, ParameterCollection spParameters)
        {
            IDbCommand cmd = Factory.CreateSPCommand(spName);
            foreach (DbParameter par in spParameters)
                cmd.Parameters.Add(par);

            return ExecuteReader(cmd);
        }

        /// <summary>
        /// Executes the reader.
        /// </summary>
        /// <param name="spName">Name of the SP.</param>
        /// <param name="spParameter">The SP param.</param>
        /// <returns></returns>
        public IDataReader ExecuteReader(string spName, IDataParameter spParameter)
        {
            IDbCommand cmd = Factory.CreateSPCommand(spName);
            cmd.Parameters.Add(spParameter);

            return ExecuteReader(cmd);
        }

        /// <summary>
        /// Executes the reader.
        /// </summary>
        /// <param name="spName">Name of the sp.</param>
        /// <param name="parameterName">Name of the parameter.</param>
        /// <param name="parameterValue">The parameter value.</param>
        /// <returns></returns>
        public IDataReader ExecuteReader(string spName, string parameterName, object parameterValue)
        {
            IDbCommand cmd = Factory.CreateSPCommand(spName);
            cmd.Parameters.Add(Factory.CreateParameterInput(parameterName, parameterValue));

            return ExecuteReader(cmd);
        }
        #endregion

        #region ExecuteScalar
        /// <summary>
        /// Executes the scalar.
        /// </summary>
        /// <param name="command">The CMD.</param>
        /// <returns></returns>
        public object ExecuteScalar(IDbCommand command)
        {
            PrepareCommand(command);
            try
            {
                return command.ExecuteScalar();
            }
            catch (DbException ex)
            {
                throw HandleException(command, ex);
            }

        }

        /// <summary>
        /// Executes the scalar.
        /// </summary>
        /// <param name="spName">Name of the sp.</param>
        /// <returns></returns>
        public object ExecuteScalar(string spName)
        {
            return ExecuteScalar(Factory.CreateSPCommand(spName));
        }

        /// <summary>
        /// Executes the scalar.
        /// </summary>
        /// <param name="spName">Name of the sp.</param>
        /// <param name="spParameter">The sp parameter.</param>
        /// <returns></returns>
        public object ExecuteScalar(string spName, DbParameter spParameter)
        {
            var cmd = Factory.CreateCommand(spName);
            cmd.Parameters.Add(spParameter);

            return ExecuteScalar(cmd);
        }

        /// <summary>
        /// Executes the scalar.
        /// </summary>
        /// <param name="spName">Name of the sp.</param>
        /// <param name="parameterName">Name of the parameter.</param>
        /// <param name="parameterValue">The parameter value.</param>
        /// <returns></returns>
        public object ExecuteScalar(string spName, string parameterName, object parameterValue)
        {
            var cmd = Factory.CreateCommand(spName);
            cmd.Parameters.Add(Factory.CreateParameterInput(parameterName, parameterValue));

            return ExecuteScalar(cmd);
        }
        /// <summary>
        /// Executes the scalar.
        /// </summary>
        /// <param name="spName">Name of the sp.</param>
        /// <param name="spParameters">The sp parameters.</param>
        /// <returns></returns>
        public object ExecuteScalar(string spName, ParameterCollection spParameters)
        {
            var cmd = Factory.CreateCommand(spName);
            foreach (DbParameter par in spParameters)
                cmd.Parameters.Add(par);

            return ExecuteScalar(cmd);
        }


        #endregion

        #region ExecuteNonQuery
        /// <summary>
        /// Executes the non query.
        /// </summary>
        /// <param name="command">The CMD.</param>
        /// <returns></returns>
        public int ExecuteNonQuery(IDbCommand command)
        {
            PrepareCommand(command);
            try
            {
                return command.ExecuteNonQuery();
            }
            catch (DbException ex)
            {
                throw HandleException(command, ex);
            }
        }


        /// <summary>
        /// Executes the non query.
        /// </summary>
        /// <param name="commandText">Name of the SP.</param>
        public int ExecuteNonQuery(string commandText)
        {
            return ExecuteNonQuery(Factory.CreateCommand(commandText));
        }

        /// <summary>
        /// Executes the specified SP name.
        /// </summary>
        /// <param name="commandText">Name of the SP.</param>
        /// <param name="spParameters">The SP params.</param>
        public int ExecuteNonQuery(string commandText, ParameterCollection spParameters)
        {
            var cmd = Factory.CreateCommand(commandText);
            foreach (DbParameter par in spParameters)
                cmd.Parameters.Add(par);

            return ExecuteNonQuery(cmd);

        }

        /// <summary>
        /// Executes the specified SP name.
        /// </summary>
        /// <param name="commandText">Name of the SP.</param>
        /// <param name="spParameter">The SP params.</param>
        public int ExecuteNonQuery(string commandText, DbParameter spParameter)
        {
            var cmd = Factory.CreateCommand(commandText);
            cmd.Parameters.Add(spParameter);

            return ExecuteNonQuery(cmd);
        }

        /// <summary>
        /// Executes the non query.
        /// </summary>
        /// <param name="commandText">Name of the sp.</param>
        /// <param name="parameterName">Name of the parameter.</param>
        /// <param name="parameterValue">The parameter value.</param>
        public int ExecuteNonQuery(string commandText, string parameterName, object parameterValue)
        {
            var cmd = Factory.CreateCommand(commandText);
            cmd.Parameters.Add(Factory.CreateParameterInput(parameterName, parameterValue));

            return ExecuteNonQuery(cmd);
        }


        #endregion

        #region ExecuteIntList

        private IList<int> ExtractIntList(IDataReader reader)
        {
            IList<int> res = new List<int>();
            try
            {
                while (reader.Read())
                {
                    res.Add(reader.GetInt32(0));
                }
            }
            finally
            {
                reader.Close();
                reader.Dispose();
            }
            return res;
        }
        /// <summary>
        /// Executes the int list.
        /// </summary>
        /// <param name="command">The command.</param>
        /// <returns></returns>
        public IList<int> ExecuteIntList(IDbCommand command)
        {
            return ExtractIntList(ExecuteReader(command));
        }
        /// <summary>
        /// Executes the int list.
        /// </summary>
        /// <param name="spName">Name of the sp.</param>
        /// <param name="spParameters">The sp parameters.</param>
        /// <returns></returns>
        public IList<int> ExecuteIntList(string spName, ParameterCollection spParameters)
        {
            return ExtractIntList(ExecuteReader(spName, spParameters));
        }

        /// <summary>
        /// Executes the int list.
        /// </summary>
        /// <param name="spName">Name of the sp.</param>
        /// <param name="spParameter">The sp parameter.</param>
        /// <returns></returns>
        public IList<int> ExecuteIntList(string spName, DbParameter spParameter)
        {
            return ExtractIntList(ExecuteReader(spName, spParameter));
        }

        /// <summary>
        /// Executes the int list.
        /// </summary>
        /// <param name="spName">Name of the sp.</param>
        /// <param name="parameterName">Name of the parameter.</param>
        /// <param name="parameterValue">The parameter value.</param>
        /// <returns></returns>
        public IList<int> ExecuteIntList(string spName, string parameterName, object parameterValue)
        {
            return ExtractIntList(ExecuteReader(spName, Factory.CreateParameterInput(parameterName, parameterValue)));
        }
        #endregion

        #region ExcecuteByteArray
        /// <summary>
        /// Excecutes the byte array.
        /// </summary>
        /// <param name="spName">Name of the sp.</param>
        /// <param name="spParameter">The sp parameter.</param>
        /// <returns></returns>
        public byte[] ExcecuteByteArray(string spName, DbParameter spParameter)
        {
            var result = ExecuteScalar(spName, spParameter);
            return result is DBNull ? null : (byte[])result;
        }

        /// <summary>
        /// Excecutes the byte array.
        /// </summary>
        /// <param name="spName">Name of the sp.</param>
        /// <param name="parameterName">Name of the parameter.</param>
        /// <param name="parameterValue">The parameter value.</param>
        /// <returns></returns>
        public byte[] ExcecuteByteArray(string spName, string parameterName, object parameterValue)
        {
            var result = ExecuteScalar(spName, parameterName, parameterValue);
            return result is DBNull ? null : (byte[])result;
        }
        /// <summary>
        /// Excecutes the byte array.
        /// </summary>
        /// <param name="command">The command.</param>
        /// <returns></returns>
        public byte[] ExcecuteByteArray(IDbCommand command)
        {
            var result = ExecuteScalar(command);
            return result is DBNull ? null : (byte[]) result;
        }

        #endregion

        #region ExcecuteStream

        /// <summary>
        /// Excecutes the stream.
        /// </summary>
        /// <param name="spName">Name of the sp.</param>
        /// <param name="parameterName">Name of the parameter.</param>
        /// <param name="parameterValue">The parameter value.</param>
        /// <returns></returns>
        public Stream ExcecuteStream(string spName, string parameterName, object parameterValue)
        {
            byte[] buffer = ExcecuteByteArray(spName, parameterName, parameterValue);
            if (buffer == null) return null;

            return new MemoryStream(buffer);
        }
        #endregion

        /// <summary>
        /// Executes the dataset.
        /// </summary>
        /// <param name="spName">Name of the sp.</param>
        /// <param name="spParameter">The sp parameter.</param>
        /// <returns></returns>
        public DataSet ExecuteDataset(string spName, IDataParameter spParameter)
        {
            IDbCommand cmd = Factory.CreateSPCommand(spName);
            cmd.Parameters.Add(spParameter);

            return ExecuteDataset(cmd);
        }


        /// <summary>
        /// Executes the dataset.
        /// </summary>
        /// <param name="spName">Name of the sp.</param>
        /// <param name="parameterName">Name of the parameter.</param>
        /// <param name="parameterValue">The parameter value.</param>
        /// <returns></returns>
        public DataSet ExecuteDataset(string spName, string parameterName, object parameterValue)
        {
            IDbCommand cmd = Factory.CreateSPCommand(spName);
            cmd.Parameters.Add(Factory.CreateParameterInput(parameterName, parameterValue));

            return ExecuteDataset(cmd);
        }

        /// <summary>
        /// Executes the dataset.
        /// </summary>
        /// <param name="command">The command.</param>
        /// <returns></returns>
        public DataSet ExecuteDataset(IDbCommand command)
        {

       
            PrepareCommand(command);
            var adapter = Factory.CreateDataAdapter(command);
            var result = new DataSet();
            try
            {
                adapter.Fill(result);
                return result;
            }
            catch (DbException ex)
            {
                throw HandleException(command, ex);
            }

        }
       
        private void PrepareCommand(IDbCommand command)
        {
            if (command == null) throw new ArgumentNullException("command");

            if (Log.IsDebugEnabled)
                Log.Debug(_convertor.DbCommandToString(command));

            command.Transaction = Transaction.RealTransaction;
            if (_connection.State == ConnectionState.Closed)
                _connection.Open();
            command.Connection = _connection;

        }

        private DbException HandleException(IDbCommand command, DbException ex)
        {
            var result = _exceptionConverter.Convert(ex);
            if (Log.IsErrorEnabled)
                Log.Error(_convertor.DbCommandToString(command), result);
            return result;
        }
        #endregion

        #region Factories
        /// <summary>
        /// Creates the SP command.
        /// </summary>
        /// <param name="spName">Name of the sp.</param>
        /// <returns></returns>
        public IDbCommand CreateSPCommand(string spName)
        {
            return Factory.CreateSPCommand(spName);
        }

        /// <summary>
        /// Creates the SP command.
        /// </summary>
        /// <param name="spName">Name of the sp.</param>
        /// <param name="parameterName">Name of the parameter.</param>
        /// <param name="parameterValue">The parameter value.</param>
        /// <returns></returns>
        public IDbCommand CreateSPCommand(string spName, string parameterName, object parameterValue)
        {
            var command = Factory.CreateSPCommand(spName);
            command.Parameters.Add(Factory.CreateParameterInput(parameterName, parameterValue));
            return command;
        }

        /// <summary>
        /// Creates the SQL command.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <returns></returns>
        public IDbCommand CreateSqlCommand(string query)
        {
            return Factory.CreateSqlCommand(query);
        }

        /// <summary>
        /// Creates the SQL command.
        /// </summary>
        /// <param name="spName">Name of the sp.</param>
        /// <param name="parameterName">Name of the parameter.</param>
        /// <param name="parameterValue">The parameter value.</param>
        /// <returns></returns>
        public IDbCommand CreateSqlCommand(string spName, string parameterName, object parameterValue)
        {
            var command = Factory.CreateSqlCommand(spName);
            command.Parameters.Add(Factory.CreateParameterInput(parameterName, parameterValue));
            return command;
        }
        #endregion
     
        #region IDbConnection
        ///<summary>
        ///Begins a database transaction.
        ///</summary>
        ///
        ///<returns>
        ///An object representing the new transaction.
        ///</returns>
        ///<filterpriority>2</filterpriority>
        IDbTransaction IDbConnection.BeginTransaction()
        {
            Transaction.Begin();
            return Transaction.RealTransaction;
        }

        ///<summary>
        ///Begins a database transaction with the specified <see cref="T:System.Data.IsolationLevel"></see> value.
        ///</summary>
        ///
        ///<returns>
        ///An object representing the new transaction.
        ///</returns>
        ///
        ///<param name="il">One of the <see cref="T:System.Data.IsolationLevel"></see> values. </param><filterpriority>2</filterpriority>
        IDbTransaction IDbConnection.BeginTransaction(IsolationLevel il)
        {
            Transaction.Begin(il);
            return Transaction.RealTransaction;
        }

        ///<summary>
        ///Closes the connection to the database.
        ///</summary>
        ///<filterpriority>2</filterpriority>
        void IDbConnection.Close()
        {
            throw new NotImplementedException();
        }

        ///<summary>
        ///Changes the current database for an open Connection object.
        ///</summary>
        ///
        ///<param name="databaseName">The name of the database to use in place of the current database. </param><filterpriority>2</filterpriority>
        void IDbConnection.ChangeDatabase(string databaseName)
        {
            _connection.ChangeDatabase(databaseName);
        }

        ///<summary>
        ///Creates and returns a Command object associated with the connection.
        ///</summary>
        ///
        ///<returns>
        ///A Command object associated with the connection.
        ///</returns>
        ///<filterpriority>2</filterpriority>
        IDbCommand IDbConnection.CreateCommand()
        {
            return Factory.CreateCommand();
        }

        ///<summary>
        ///Opens a database connection with the settings specified by the ConnectionString property of the provider-specific Connection object.
        ///</summary>
        ///<filterpriority>2</filterpriority>
        void IDbConnection.Open()
        {
            _connection.Open();
        }

        ///<summary>
        ///Gets or sets the string used to open a database.
        ///</summary>
        ///
        ///<returns>
        ///A string containing connection settings.
        ///</returns>
        ///<filterpriority>2</filterpriority>
        string IDbConnection.ConnectionString
        {
            get { return _connection.ConnectionString; }
            set { _connection.ConnectionString = value; }
        }

        ///<summary>
        ///Gets the time to wait while trying to establish a connection before terminating the attempt and generating an error.
        ///</summary>
        ///
        ///<returns>
        ///The time (in seconds) to wait for a connection to open. The default value is 15 seconds.
        ///</returns>
        ///<filterpriority>2</filterpriority>
        int IDbConnection.ConnectionTimeout
        {
            get { return _connection.ConnectionTimeout; }
        }

        ///<summary>
        ///Gets the name of the current database or the database to be used after a connection is opened.
        ///</summary>
        ///
        ///<returns>
        ///The name of the current database or the name of the database to be used once a connection is open. The default value is an empty string.
        ///</returns>
        ///<filterpriority>2</filterpriority>
        string IDbConnection.Database
        {
            get { return _connection.Database; }
        }

        ///<summary>
        ///Gets the current state of the connection.
        ///</summary>
        ///
        ///<returns>
        ///One of the <see cref="T:System.Data.ConnectionState"></see> values.
        ///</returns>
        ///<filterpriority>2</filterpriority>
        ConnectionState IDbConnection.State
        {
            get { return _connection.State; }
        }


        #endregion
    }
}
