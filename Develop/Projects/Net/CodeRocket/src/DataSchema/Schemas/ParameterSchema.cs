using System;
using System.Collections.Generic;
using System.Text;

namespace Savchin.Data.Schema
{

    /// <summary>
    /// ParameterSchema
    /// </summary>
    public class ParameterSchema : INamedObject
    {
        private string _name = String.Empty;
        /// <summary>
        /// The table name
        /// </summary>
        public string Name
        {
            get { return this._name; }
            set { this._name = value; }
        }
    }
}
