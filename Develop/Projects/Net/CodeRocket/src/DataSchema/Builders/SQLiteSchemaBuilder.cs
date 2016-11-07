using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using Savchin.Data.MSSQL;

namespace Savchin.Data.Schema.Builders
{
    /// <summary>
    /// SQLiteSchemaBuilder
    /// </summary>
    public class SQLiteSchemaBuilder : IDataSchemaBuilder
    {

        private const string ColumnTableName = "TABLE_NAME";
        private const string ColumnTableType = "TABLE_TYPE";
        private const string ColumnColumnName = "COLUMN_NAME";

        private readonly DBConvertor _convertor = new DBConvertor();
        readonly List<Exception> _errors = new List<Exception>();
        private DataTable _schemaTables;
        private DataTable _schemaColumns;

        /// <summary>
        /// Gets the errors.
        /// </summary>
        /// <value>The errors.</value>
        public IList<Exception> Errors
        {
            get { return _errors; }
        }
        private DatabaseSchema result;
        private SQLiteConnection connection;
        /// <summary>
        /// Builds the database schema.
        /// </summary>
        /// <param name="connectionString">The connection string.</param>
        /// <returns></returns>
        public DatabaseSchema BuildDatabaseSchema(string connectionString)
        {
            _errors.Clear();

            result = new DatabaseSchema();
            using (connection = new SQLiteConnection(connectionString))
            {
                result.ConnectionString = connectionString;
                result.ProviderType = ProviderType.SqlLite;

                connection.Open();

                if (!LoadSchema())
                    return result;

                if (!BuildTables())
                    return result;
            }

            return result;
        }

        private bool LoadSchema()
        {
            _schemaTables = GetSchema("TABLES");
            _schemaColumns = GetSchema("COLUMNS");
            return _schemaTables != null && _schemaColumns != null && _schemaTables.Rows.Count > 0;
        }

        private bool BuildTables()
        {

            result.Name = _schemaTables.Rows[0]["TABLE_CATALOG"].ToString();
            if (result.Name == String.Empty)
                result.Name = "Unknown";

            // Build the base schema information
            foreach (DataRow dr in _schemaTables.Rows)
            {
                var tableType = TableTypeConverter.Convert(dr[ColumnTableType].ToString());
                if (tableType == TableType.TABLE || tableType == TableType.VIEW)
                {
                    try
                    {
                        result.AddTable(BuildTable(dr, tableType));
                    }
                    catch (Exception ex)
                    {
                        Errors.Add(ex);
                    }
                }
            }
            return true;
        }

        /// <summary>
        /// Builds the table.
        /// </summary>
        /// <param name="dr">The dr.</param>
        /// <param name="type">The type.</param>
        /// <returns></returns>
        private TableSchema BuildTable(DataRow dr, TableType type)
        {
            var table = new TableSchema
                            {
                                Alias = dr[ColumnTableName].ToString(),
                                Name = dr[ColumnTableName].ToString(),
                                TableType = type
                            };

            BuildColumns(table);
            return table;
        }


        /// <summary>
        /// Builds the columns.
        /// </summary>
        /// <param name="ts">The ts.</param>
        protected void BuildColumns(TableSchema ts)
        {
            var view = new DataView(_schemaColumns)
                           {
                               RowFilter = (ColumnTableName + "='" + ts.Name + "'")
                           };
            var schema = GetReaderShema(ts.Name);

            foreach (DataRowView dr in view)
            {
                var rows = schema.Select("ColumnName='" + (string)dr[ColumnColumnName] + "'");
                ts.AddColumn(BuildColumn(dr, rows[0]));
            }

        }

        /// <summary>
        /// Gets the reader shema.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        private DataTable GetReaderShema(string name)
        {
            var command = connection.CreateCommand();
            command.CommandText = "SELECT * FROM [" + name + "] WHERE 1=0";
            var reader = command.ExecuteReader();
            var result = reader.GetSchemaTable();
            reader.Close();
            return result;
        }

        /// <summary>
        /// Builds the column.
        /// </summary>
        /// <param name="dr">The dr.</param>
        /// <param name="rs">The rs.</param>
        /// <returns></returns>
        private ColumnSchema BuildColumn(DataRowView dr, DataRow rs)
        {
            var ci = new ColumnSchema();
            ci.Ordinal = (int)rs["ColumnOrdinal"];
            ci.Name = ci.Alias = (string)rs["ColumnName"];




            ci.DataTypeId = (int)rs["ProviderType"];
            ci.DataType = ((string)rs["DataTypeName"]).ToLower();
            var netType = (Type)rs["DataType"];
            ci.NetType = netType.FullName;
            ci.DbDataType = _convertor.NetTypeToDbType(netType);


            ci.Length = (int)rs["ColumnSize"];
            ci.Precision = (byte)GetShort(rs["NumericPrecision"]);
            ci.Scale = (byte)GetShort(rs["NumericScale"]);
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
                ci.DataTypeFull = string.Format("[{0}]", ci.DataType);
            }
            ci.AllowNulls = (bool)rs["AllowDBNull"];
            ci.IsAutoIncrement = (bool)rs["IsAutoIncrement"];
            ci.IsReadOnly = (bool)rs["IsReadOnly"];

            ci.IsForeignKey = false;
            ci.IsPrimaryKey = (bool)rs["IsKey"];
            ci.IsUnique = (bool)rs["IsUnique"];

            return ci;
        }

        private short GetShort(object value)
        {
            return GetShort(value, 0);
        }

        private short GetShort(object value, short def)
        {
            return value is DBNull ? def : (short)value;
        }

        private byte GetByte(object value)
        {
            return GetByte(value, 0);
        }

        private byte GetByte(object value, byte def)
        {
            return value is DBNull ? def : (byte)value;
        }
        private DataTable GetSchema(string s)
        {
            return connection.GetSchema(s);
        }


        /// <summary>
        /// Converts the string representation of a table type to a TableType enum
        /// </summary>
        protected static class TableTypeConverter
        {


            /// <summary>
            /// Converts the string representation of a table type to a TableType enum
            /// </summary>
            /// <param name="tableType">The string representation to convert</param>
            /// <returns></returns>
            public static TableType Convert(string tableType)
            {
                switch (tableType.ToUpper())
                {
                    case "SYSTEM_TABLE":
                        return TableType.SYSTEM_TABLE;
                    case "TABLE":
                        return TableType.TABLE;
                    case "VIEW":
                        return TableType.VIEW;

                    default:
                        throw new Exception("TableType " + tableType + " is not supported.");
                }
            }
        }
    }
}
