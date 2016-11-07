using System.Collections.Generic;
using System.Data;
using Savchin.Data.Common;

namespace KnowledgeBase.Dal
{
    /// <summary>
    /// FactoryEntityBases
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class DbFactoryBase<T> : FactoryBase where T : class
    {
        /// <summary>
        /// Gets the database.
        /// </summary>
        /// <value>The database.</value>
        protected DBConnection Database
        {
            get { return Context.Connection; }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DbDbFactoryBase{T}"/> class.
        /// </summary>
        protected DbFactoryBase(DalContext context)
            : base(context)
        {
        
        }

        /// <summary>
        /// Selects the data set.
        /// </summary>
        /// <param name="command">The command.</param>
        /// <returns></returns>
        protected DataSet SelectDataSet(IDbCommand command)
        {
            return Database.ExecuteDataset(command);
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
            using (IDataReader reader = Database.ExecuteReader(command))
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
            using (IDataReader reader = Database.ExecuteReader(spName, parameterName, parameterValue))
                return Exctract(reader);
        }

        /// <summary>
        /// Selects the specified sp name.
        /// </summary>
        /// <param name="spName">Name of the sp.</param>
        /// <returns></returns>
        protected List<T> Select(string spName)
        {
            using (IDataReader reader = Database.ExecuteReader(spName))
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
            using (IDataReader reader = Database.ExecuteReader(spName, parameterName, parameterValue))
                return ExctractSingle(reader);
        }
        /// <summary>
        /// Selects the single.
        /// </summary>
        /// <param name="command">The command.</param>
        /// <returns></returns>
        protected T SelectSingle(IDbCommand command)
        {
            using (IDataReader reader = Database.ExecuteReader(command))
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
            return (reader.Read())?MapObject(reader): null;
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
