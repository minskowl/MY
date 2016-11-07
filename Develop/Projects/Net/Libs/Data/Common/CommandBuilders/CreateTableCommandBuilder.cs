using System;
using System.Collections.Generic;
using System.Text;
using Savchin.Text;

namespace Savchin.Data.Common.CommandBuilders
{
    public class CreateTableCommandBuilder : CommandBuilder
    {
        List<TableColumn> columns= new List<TableColumn>();

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateTableCommandBuilder"/> class.
        /// </summary>
        /// <param name="factory">The factory.</param>
        public CreateTableCommandBuilder(IDBFactory factory) : base(factory)
        {
        }

        /// <summary>
        /// Adds the column.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="dataType">Type of the data.</param>
        public void AddColumn(string name, Type dataType)
        {
            columns.Add(new TableColumn(name, dataType));
        }

        /// <summary>
        /// Creates the command text.
        /// </summary>
        /// <returns></returns>
        protected override string CreateCommandText()
        {
            StringBuilder builder =new StringBuilder( "CREATE TABLE " + tableName + "( ");
 

            foreach (TableColumn column in columns)
            {
                builder.AppendLine(_convertor.ToDbObjectName(column.Name) + " " +
                                   _convertor.NetTypeToDataBaseType(column.DataType) + ",");
               
            }

            return StringUtil.RemoveFinalChar(builder.ToString()) + " )";

        }
    }
}
