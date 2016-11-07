using System.Collections.Generic;
using System.Data;
using Savchin.Data.Common;

namespace Site.Core
{
    public interface IFactory
    {
        /// <summary>
        /// Gets or sets the database.
        /// </summary>
        /// <value>The database.</value>
        DBConnection Database { get; set; }
    }

    public abstract class FactoryEntityBase<T> : IFactory where T : class
    {
        protected DBConnection database;
        /// <summary>
        /// Gets or sets the database.
        /// </summary>
        /// <value>The database.</value>
        public DBConnection Database
        {
            get { return database; }
            set { database = value; }
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="FactoryEntityBase&lt;T&gt;"/> class.
        /// </summary>
        protected FactoryEntityBase()
        {

        }




        /// <summary>
        /// Selects the data set.
        /// </summary>
        /// <param name="command">The command.</param>
        /// <returns></returns>
        protected DataSet SelectDataSet(IDbCommand command)
        {
            return database.ExecuteDataset(command);
        }

        #region Selects values



        #region Select List
        /// <summary>
        /// Selects the specified command.
        /// </summary>
        /// <param name="command">The command.</param>
        /// <returns></returns>
        protected List<T> Select(IDbCommand command)
        {
            using (IDataReader reader = database.ExecuteReader(command))
                return Exctract(reader);
        }

        /// <summary>
        /// Selects the specified sp name.
        /// </summary>
        /// <param name="spName">Name of the sp.</param>
        /// <param name="parameterName">Name of the parameter.</param>
        /// <param name="parameterValue">The parameter value.</param>
        /// <returns></returns>
        protected List<T> Select(string spName, string parameterName, object parameterValue)
        {
            using (IDataReader reader = database.ExecuteReader(spName, parameterName, parameterValue))
                return Exctract(reader);
        }

        /// <summary>
        /// Selects the specified sp name.
        /// </summary>
        /// <param name="spName">Name of the sp.</param>
        /// <returns></returns>
        protected List<T> Select(string spName)
        {
            using (IDataReader reader = database.ExecuteReader(spName))
                return Exctract(reader);
        }


        #endregion

        /// <summary>
        /// Selects the single.
        /// </summary>
        /// <param name="spName">Name of the sp.</param>
        /// <param name="parameterName">Name of the parameter.</param>
        /// <param name="parameterValue">The parameter value.</param>
        /// <returns></returns>
        protected T SelectSingle(string spName, string parameterName, object parameterValue)
        {
            using (IDataReader reader = database.ExecuteReader(spName, parameterName, parameterValue))
                return ExctractSingle(reader);
        }
        /// <summary>
        /// Selects the single.
        /// </summary>
        /// <param name="command">The command.</param>
        /// <returns></returns>
        protected T SelectSingle(IDbCommand command)
        {
            using (IDataReader reader = database.ExecuteReader(command))
            {
                return ExctractSingle(reader);
            }
        }
        #endregion


        /// <summary>
        /// Exctracts the specified reader.
        /// </summary>
        /// <param name="reader">The reader.</param>
        /// <returns></returns>
        private List<T> Exctract(IDataReader reader)
        {
            var result = new List<T>();
            InitOrdinals(reader);
            while (reader.Read())
                result.Add(MapObject(reader));


            return result;
        }

        /// <summary>
        /// Exctracts the single.
        /// </summary>
        /// <param name="reader">The reader.</param>
        /// <returns></returns>
        private T ExctractSingle(IDataReader reader)
        {
            InitOrdinals(reader);
            return (reader.Read()) ? MapObject(reader) : null;
        }

        protected abstract void InitOrdinals(IDataReader reader);
        /// <summary>
        /// Maps the objects.
        /// </summary>
        /// <param name="reader">The reader.</param>
        /// <returns></returns>
        protected abstract T MapObject(IDataReader reader);

    }
}