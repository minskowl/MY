using System;
using Savchin.Data.Schema.Collections;

namespace Savchin.Data.Schema
{
    /// <summary>
    /// ProcedureSchema
    /// </summary>
    [Serializable]
    public class ProcedureSchema : INamedObject
    {
        private string _name = String.Empty;
        private ParameterSchemaDictionary parameters = new ParameterSchemaDictionary();
        /// <summary>
        /// The table name
        /// </summary>
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        /// <summary>
        /// Adds the specified ColumnSchema
        /// </summary>
        /// <param name="c"></param>
        public void AddParameter(ParameterSchema c)
        {
            parameters[c.Name] = c;
        }

        /// <summary>
        /// Removes the specified ColumnSchema
        /// </summary>
        /// <param name="name">The name of the ColumnSchema to remove</param>
        public void RemoveParameter(string name)
        {
            this.parameters.Remove(name);
        }

        /// <summary>
        /// Retrieves the specified ColumnSchema
        /// </summary>
        /// <param name="name">The name of the ColumnSchema to retrieve</param>
        /// <returns></returns>
        public ParameterSchema GetParameter(string name)
        {
            return this.parameters[name];
        }

        /// <summary>
        /// Indexer to retrieve a ColumnSchema by name
        /// </summary>
        public ParameterSchema this[String key]
        {
            get { return (parameters[key]); }
            set { parameters[key] = value; }
        }

        /// <summary>
        /// Returns a collection of Parameters
        /// </summary>
        /// <returns></returns>
        public ParameterSchemaDictionary Parameters
        {
            get { return this.parameters; }
            set { this.parameters = value; }
        }
    }
}
