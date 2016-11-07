using System;
using System.Data;

namespace Savchin.Data.Common
{
    public abstract class ParametrizedCommandBuilder : CommandBuilder
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ParametrizedCommandBuilder"/> class.
        /// </summary>
        /// <param name="factory">The factory.</param>
        public ParametrizedCommandBuilder(IDBFactory factory) : base(factory)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ParametrizedCommandBuilder"/> class.
        /// </summary>
        /// <param name="connection">The connection.</param>
        public ParametrizedCommandBuilder(DBConnection connection)
            : base(connection)
        {
        }

        /// <summary>
        /// Adds the parameter.
        /// </summary>
        /// <param name="fieldName">Name of the field.</param>
        /// <param name="parameterName">Name of the parameter.</param>
        /// <param name="type">The type.</param>
        public void AddParameter(string fieldName, string parameterName, Type type)
        {
            AddParameter(fieldName,parameterName, _convertor.NetTypeToDbType(type));
        }

        /// <summary>
        /// Adds the parameter.
        /// </summary>
        /// <param name="fieldName">Name of the field.</param>
        /// <param name="parameterName">Name of the parameter.</param>
        /// <param name="value">The value.</param>
        public void AddParameter(string fieldName, string parameterName,  object value)
        {
            AddParameter(fieldName, parameterName, _convertor.NetTypeToDbType(value.GetType()), value);
        }
        /// <summary>
        /// Adds the parameter.
        /// </summary>
        /// <param name="fieldName">Name of the field.</param>
        /// <param name="type">The type.</param>
        public void AddParameter(string fieldName, Type type)
        {
            AddParameter(fieldName, _convertor.NetTypeToDbType(type));
        }
        /// <summary>
        /// Adds the parameter.
        /// </summary>
        /// <param name="fieldName">Name of the field.</param>
        /// <param name="value">The value.</param>
        public void AddParameter(string fieldName, object value)
        {
            AddParameter(fieldName, fieldName, _convertor.NetTypeToDbType(value.GetType()), value);
        } 
        /// <summary>
        /// Adds the parameter.
        /// </summary>
        /// <param name="fieldName">Name of the field.</param>
        /// <param name="type">The type.</param>
        public void AddParameter(string fieldName, DbType type)
        {
            AddParameter(fieldName, fieldName, type);
        }

        /// <summary>
        /// Adds the parameter.
        /// </summary>
        /// <param name="fieldName">Name of the field.</param>
        /// <param name="parameterName">Name of the parameter.</param>
        /// <param name="type">The type.</param>
        public abstract void AddParameter(string fieldName, string parameterName, DbType type);

        /// <summary>
        /// Adds the parameter.
        /// </summary>
        /// <param name="fieldName">Name of the field.</param>
        /// <param name="parameterName">Name of the parameter.</param>
        /// <param name="type">The type.</param>
        /// <param name="value">The value.</param>
        public abstract void AddParameter(string fieldName, string parameterName, DbType type, object value);



        protected IDbDataParameter CommandAddParameter(string parameterName, DbType type, object value)
        {
            IDbDataParameter parameter = command.CreateParameter();
            parameter.ParameterName = _convertor.ToDbParamName(parameterName);
            parameter.Direction = ParameterDirection.Input;
            parameter.DbType = type;
            parameter.Value = value;
            command.Parameters.Add(parameter);
            return parameter;
        }
        protected  IDbDataParameter CommandAddParameter(string parameterName, DbType type)
        {
            IDbDataParameter parameter = command.CreateParameter();
            parameter.ParameterName = _convertor.ToDbParamName(parameterName);
            parameter.Direction = ParameterDirection.Input;
            parameter.DbType = type;
            command.Parameters.Add(parameter);
            return parameter;
        }
    }
}
