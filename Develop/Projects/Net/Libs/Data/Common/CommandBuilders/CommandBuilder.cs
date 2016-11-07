using System;
using System.Data;

namespace Savchin.Data.Common
{
    public abstract class CommandBuilder : ICommandBuilder
    {

        public const char PreDelimeter = '[';
        public const char PostDelimeter = ']';

        protected IDbCommand command;
        readonly protected IDBFactory _factory;
        readonly protected IDbConvertor _convertor;
        protected string tableName;

        /// <summary>
        /// Gets or sets the name of the table.
        /// </summary>
        /// <value>The name of the table.</value>
        public string TableName
        {
            get { return tableName; }
            set { tableName = value; }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ParametrizedCommandBuilder"/> class.
        /// </summary>
        /// <param name="factory">The factory.</param>
        public CommandBuilder(IDBFactory factory)
        {
            if (factory == null)
                throw new ArgumentNullException("factory");

            _factory = factory;
            _convertor = factory.Convertor;
            command = _factory.CreateCommand();
            command.CommandType = CommandType.Text;

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ParametrizedCommandBuilder"/> class.
        /// </summary>
        /// <param name="connection">The connection.</param>
        public CommandBuilder(DBConnection connection)
            : this(connection.Factory)
        {

        }



        /// <summary>
        /// Builds the command.
        /// </summary>
        /// <returns></returns>
        public IDbCommand Build()
        {
            if (string.IsNullOrEmpty(command.CommandText))
            {
                command.CommandText = CreateCommandText();
            }
            return command;
        }

        protected abstract string CreateCommandText();

    }
}