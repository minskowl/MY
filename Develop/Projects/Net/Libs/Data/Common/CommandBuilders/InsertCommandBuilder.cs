using System.Data;
using System.Text;
using Savchin.Text;


namespace Savchin.Data.Common
{
    public class InsertCommandBuilder : ParametrizedCommandBuilder
    {

        private readonly StringBuilder _fieldList = new StringBuilder();
        private readonly StringBuilder _valueList = new StringBuilder();

        /// <summary>
        /// Initializes a new instance of the <see cref="InsertCommandBuilder"/> class.
        /// </summary>
        /// <param name="factory">The factory.</param>
        public InsertCommandBuilder(IDBFactory factory)
            : base(factory)
        {


        }

        /// <summary>
        /// Initializes a new instance of the <see cref="InsertCommandBuilder"/> class.
        /// </summary>
        /// <param name="connection">The connection.</param>
        public InsertCommandBuilder(DBConnection connection)
            : base(connection)
        {

        }


        /// <summary>
        /// Adds the parameter.
        /// </summary>
        /// <param name="fieldName">Name of the field.</param>
        /// <param name="parameterName">Name of the parameter.</param>
        /// <param name="type">The type.</param>
        public override void AddParameter(string fieldName, string parameterName, DbType type)
        {
            _fieldList.Append( _convertor.ToDbObjectName(fieldName) + ",");

            _valueList.Append(  CommandAddParameter(parameterName, type).ParameterName + ",");

        }

        /// <summary>
        /// Adds the parameter.
        /// </summary>
        /// <param name="fieldName">Name of the field.</param>
        /// <param name="parameterName">Name of the parameter.</param>
        /// <param name="type">The type.</param>
        /// <param name="value">The value.</param>
        public override void AddParameter(string fieldName, string parameterName, DbType type, object value)
        {
            _fieldList.Append(_convertor.ToDbObjectName(fieldName) + ",");

            _valueList.Append(CommandAddParameter(parameterName, type, value).ParameterName + ",");

        }

        /// <summary>
        /// Builds the command.
        /// </summary>
        /// <returns></returns>
        protected override string CreateCommandText()
        {
            return "INSERT INTO " + _convertor.ToDbObjectName(TableName) +
                                    "(" + _fieldList.ToString(0, _fieldList.Length - 1) + " ) VALUES (" +
                                    _valueList.ToString(0, _valueList.Length - 1).RemoveFinalChar() + ");";

        }
    }
}
