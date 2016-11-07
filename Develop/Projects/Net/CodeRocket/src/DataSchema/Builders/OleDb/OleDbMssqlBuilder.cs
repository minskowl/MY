using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Text;
using Savchin.Data.Common;
using Savchin.Data.MSSQL;

namespace Savchin.Data.Schema.Builders
{
    internal class OleDbMssqlBuilder : CommonServerSchemaBuilder
    {
        DBConvertor convertor = new DBConvertor();
        private DataTable columns = new DataTable();

        public override DatabaseSchema BuildDatabaseSchema()
        {
            IDbCommand command = connection.CreateCommand();
            command.CommandType = CommandType.Text;
            command.CommandText = @"
select 
column_id,
o.name as table_name,
c.name as column_name,
user_type_id,
TYPE_NAME(user_type_id) as type_name,
max_length,
precision,
scale,
is_nullable,
is_identity,
is_computed
from sys.objects as o 
inner join sys.columns as c on  c.object_id=o.object_id
where o.type IN ('U','V')
";
            using (IDataReader reader = command.ExecuteReader())
            {
                columns.Load(reader);
            }

            return base.BuildDatabaseSchema();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CommonServerSchemaBuilder"/> class.
        /// </summary>
        /// <param name="connection">The connection.</param>
        public OleDbMssqlBuilder(OleDbConnection connection)
            : base(connection)
        {

        }
        protected override void BuildColumns(TableSchema ts)
        {
            DataView view = new DataView(columns);
            view.RowFilter = "table_name='" + ts.Name + "'";
            foreach (DataRowView dr in view)
            {
                ts.AddColumn(BuildColumn(dr));
            }

        }
        /// <summary>
        /// Builds the column.
        /// </summary>
        /// <param name="dr">The dr.</param>
        /// <returns></returns>
        private ColumnSchema BuildColumn(DataRowView dr)
        {
            ColumnSchema ci = new ColumnSchema();
            ci.Ordinal = (int)dr["column_id"];
            ci.Alias = (string)dr["column_name"];
            ci.Name = (string)dr["column_name"];

   

            ci.DataTypeId = (int)dr["user_type_id"];
            ci.DataType = ((string)dr["type_name"]).ToLower();
                     Type netType = convertor.DataBaseTypeToNetType(ci.DataType);

            ci.NetType = netType.FullName;
            ci.DbDataType = convertor.NetTypeToDbType(netType);


            ci.Length = (short)dr["max_length"];
            ci.Precision = (byte)dr["precision"];
            ci.Scale = (byte)dr["scale"];
            if (ci.DataType.StartsWith("n"))
            {
                ci.Length = ci.Length / 2;
                ci.DataTypeFull = string.Format("[{0}]({1})",
                    ci.DataType,
                    ci.Length);
            }
            else if (ci.DataType == "decimal" || ci.DataType == "numeric")
            {
                ci.DataTypeFull = string.Format("[{0}]({1},{2})",
                    ci.DataType,
                    ci.Precision,
                    ci.Scale);
            }
            else
            {
                ci.DataTypeFull = string.Format("[{0}]",ci.DataType);
            }
            ci.AllowNulls = (bool)dr["is_nullable"];
            ci.IsAutoIncrement = (bool)dr["is_identity"];
            ci.IsReadOnly = (bool)dr["is_computed"];

            ci.IsForeignKey = false;
            ci.IsPrimaryKey = false;
            ci.IsUnique = false;

            return ci;
        }

    }
}
