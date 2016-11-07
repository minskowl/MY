using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Text;
using Savchin.Data.Common;

namespace Savchin.Data.Schema.Builders
{
    internal class CommonServerSchemaBuilder
    {

        public List<Exception> Errors = new List<Exception>();
        private DatabaseSchema result;
        private string _schemaFilter;
        protected OleDbConnection connection;
        public CommonServerSchemaBuilder(OleDbConnection connection)
        {
            this.connection = connection;
        }
        /// <summary>
        /// Builds the database schema.
        /// </summary>
        /// <returns></returns>
        public virtual DatabaseSchema BuildDatabaseSchema()
        {
            result = new DatabaseSchema();
            result.ConnectionString = connection.ConnectionString;
            try
            {

                if (!BuildTables())
                    return result;


                BuildPrimaryKeys();
                BuildForeignKeys();
            }
            catch (Exception ex)
            {
                Errors.Add(ex);
            }
            return result;
        }

        /// <summary>
        /// Builds the tables.
        /// </summary>
        /// <returns></returns>
        private bool BuildTables()
        {
            DataTable schemaTables = GetOleDbSchema(OleDbSchemaGuid.Tables, "", _schemaFilter, "", "");
            if (schemaTables == null || schemaTables.Rows.Count == 0)
                return false;


            //TODO: Note sure if this is valid. It does not work for Oracle DB's
            result.Name = schemaTables.Rows[0]["TABLE_CATALOG"].ToString();
            if (result.Name == String.Empty)
                result.Name = "Unknown";

            // Build the base schema information
            foreach (DataRow dr in schemaTables.Rows)
            {
                TableType tableType = TableTypeConverter.Convert(dr["TABLE_TYPE"].ToString());
                if (tableType == TableType.TABLE || tableType == TableType.VIEW)
                {
                    try
                    {
                        result.AddTable(BuildTable(dr));
                    }
                    catch (Exception ex)
                    {
                        Errors.Add(ex);
                    }
                }
            }
            return true;
        }
        private DataTable GetReaderSchema(string tableName)
        {
            IDbCommand cmd = connection.CreateCommand();
            cmd.CommandText = "SELECT * FROM " + CommandBuilder.PreDelimeter + tableName + CommandBuilder.PostDelimeter + " WHERE 1=0 ";

            IDataReader myReader = cmd.ExecuteReader(CommandBehavior.KeyInfo | CommandBehavior.SchemaOnly);

            DataTable schemaTable = myReader.GetSchemaTable();

            myReader.Close();

            return schemaTable;
        }




        /// <summary>
        /// Builds the table.
        /// </summary>
        /// <param name="dr">The dr.</param>
        /// <returns></returns>
        private TableSchema BuildTable(DataRow dr)
        {
            TableSchema table = new TableSchema();
            table.Alias = dr["TABLE_NAME"].ToString();
            table.Name = dr["TABLE_NAME"].ToString();
            table.TableType = TableTypeConverter.Convert(dr["TABLE_TYPE"].ToString());
            BuildColumns(table);
            return table;
        }

        #region Columns
        /// <summary>
        /// Builds the columns.
        /// </summary>
        /// <param name="ts">The ts.</param>
        protected virtual void BuildColumns(TableSchema ts)
        {
          
            DataTable dt = GetOleDbSchema(OleDbSchemaGuid.Columns, ts.Name, null, null, null);
            if (dt == null)
                return;

            foreach (DataRow dr in dt.Rows)
            {
                ts.AddColumn(BuildColumn(dr));
            }

        }

        /// <summary>
        /// Builds the column.
        /// </summary>
        /// <param name="dr">The dr.</param>
        /// <returns></returns>
        private ColumnSchema BuildColumn(DataRow dr)
        {
            ColumnSchema ci = new ColumnSchema();
            ci.Alias = (string)dr["ColumnName"];
            ci.AllowNulls = (bool)dr["AllowDBNull"];
            ci.DataTypeId = (int)dr["ProviderType"];
            // ci.DataType = ProviderInfoManager.GetInstance().GetNameById(dbProviderType, ci.DataTypeId);
            // ci.DefaultTestValue = ProviderInfoManager.GetInstance().GetTestDefaultById(this.dbProviderType, ci.DataTypeId);
            // ci.DefaultValue = ProviderInfoManager.GetInstance().GetDefaultById(this.dbProviderType, ci.DataTypeId);
            ci.IsAutoIncrement = (bool)dr["IsAutoIncrement"];
            ci.IsForeignKey = false;
            ci.IsPrimaryKey = false;
            ci.IsUnique = (bool)dr["IsUnique"];
            ci.Length = (int)dr["ColumnSize"];
            ci.Name = (string)dr["ColumnName"];
            ci.DataType = dr["DataType"].ToString();
            ci.NetType = dr["DataType"].ToString();
            ci.Ordinal = (int)dr["ColumnOrdinal"];
            ci.IsReadOnly = (bool)dr["IsReadOnly"];
            return ci;
        }
        #endregion

        /// <summary>
        /// Builds the primary keys.
        /// </summary>
        private void BuildPrimaryKeys()
        {
            // Get the primary key information
            DataTable pkeys = GetOleDbSchema(OleDbSchemaGuid.Primary_Keys, "", _schemaFilter, "", "");

            foreach (DataRow dr in pkeys.Rows)
            {
                TableSchema tableSchema;
                if (result.Tables.TryGetValue(dr["TABLE_NAME"].ToString(), out tableSchema))
                {
                    ColumnSchema columnSchema = tableSchema[dr["COLUMN_NAME"].ToString()];

                    columnSchema.IsPrimaryKey = true;
                    //tableSchema.AddColumn(columnSchema);
                }



            }
        }
        /// <summary>
        /// Builds the foreign keys.
        /// </summary>
        private void BuildForeignKeys()
        {
            // Get the foreign key information

            DataTable fkeys = GetOleDbSchema(OleDbSchemaGuid.Foreign_Keys, "", _schemaFilter, "", "");
            foreach (DataRow dr in fkeys.Rows)
            {
                try
                {
                    TableSchema primaryKeyTable;
                    TableSchema foreignKeyTable;
                    string fkTableName = dr["FK_TABLE_NAME"].ToString();
                    string pkTableName = dr["PK_TABLE_NAME"].ToString();
                    string fkName = dr["FK_NAME"].ToString();

                    if (result.ForeignKeys.ContainsKey(fkName))
                        continue;

                    if (result.Tables.TryGetValue(pkTableName, out primaryKeyTable) &&
                        result.Tables.TryGetValue(fkTableName, out foreignKeyTable))
                    {
                        ForeignKeySchema fk = new ForeignKeySchema(result, fkName, pkTableName, fkTableName);
                        result.ForeignKeys.Add(fkName, fk);
                        primaryKeyTable.ForeignKeys.Add(fkName, fk);
                        if (!foreignKeyTable.ForeignKeys.ContainsKey(fkName))
                            foreignKeyTable.ForeignKeys.Add(fkName, fk);
                    }
                }
                catch (Exception ex)
                {
                    Errors.Add(ex);
                }
            }


            foreach (DataRow dr in fkeys.Rows)
            {
                try
                {
                    string fkName = dr["FK_NAME"].ToString();
                    string primaryColumnName = dr["PK_COLUMN_NAME"].ToString();
                    string foreignColumnName = dr["FK_COLUMN_NAME"].ToString();

                    ForeignKeySchema key;
                    if (result.ForeignKeys.TryGetValue(fkName, out key))
                    {
                        key.Associations.Add(new ForeignKeyAssociation(key, primaryColumnName, foreignColumnName));
                    }
                }
                catch (Exception ex)
                {
                    Errors.Add(ex);
                }
            }
        }


        /// <summary>
        /// Gets the OLE db schema.
        /// </summary>
        /// <param name="guid">GUID.</param>
        /// <param name="filterCatalog">Filter catalog.</param>
        /// <param name="filterSchema">Filter schema.</param>
        /// <param name="filterName">Name of the filter.</param>
        /// <param name="filterType">Filter type.</param>
        /// <returns></returns>
        public DataTable GetOleDbSchema(Guid guid, string filterCatalog, string filterSchema, string filterName, string filterType)
        {
            return connection.GetOleDbSchemaTable(guid, GetFilters(guid, filterCatalog, filterSchema, filterName, filterType));
        }

        private static object[] GetFilters(Guid guid, string filterCatalog, string filterSchema, string filterName, string filterType)
        {
            // Different OleDbSchemaGuid's require a different number of parameters. 
            // These parameter depend on what we are trying to retirve from the database
            // so this function returns the correct parameter sets. 
            // This should be a Switch statement, but the compiler did not like it. 

            filterCatalog = filterCatalog == string.Empty ? null : filterCatalog;
            filterSchema = filterSchema == string.Empty ? null : filterSchema;
            filterName = filterName == string.Empty ? null : filterName;
            filterType = filterType == string.Empty ? null : filterType;


            if (guid.Equals(OleDbSchemaGuid.Tables))
                return new object[] { filterCatalog, filterSchema, filterName, filterType };
            if (guid.Equals(OleDbSchemaGuid.Views))
                return new object[] { filterCatalog, filterSchema, filterName };
            if (guid.Equals(OleDbSchemaGuid.Primary_Keys))
                return new object[] { filterCatalog, filterSchema, filterName };
            if (guid.Equals(OleDbSchemaGuid.Foreign_Keys))
                return new object[] { filterCatalog, filterSchema, filterName, filterCatalog, filterSchema, filterName };
            if (guid.Equals(OleDbSchemaGuid.Columns))
                return new object[] { filterCatalog, filterSchema, filterName, string.Empty };
            return null;
        }

        /// <summary>
        /// Converts the string representation of a table type to a TableType enum
        /// </summary>
        protected class TableTypeConverter
        {
            private TableTypeConverter()
            {
            }

            /// <summary>
            /// Converts the string representation of a table type to a TableType enum
            /// </summary>
            /// <param name="tableType">The string representation to convert</param>
            /// <returns></returns>
            public static TableType Convert(string tableType)
            {
                switch (tableType.ToUpper(new System.Globalization.CultureInfo("en-US")))
                {
                    case "SYSTEM TABLE":
                    case "ACCESS TABLE":
                        return TableType.SYSTEM_TABLE;
                    case "SYSTEM VIEW":
                        return TableType.SYSTEM_VIEW;
                    case "TABLE":
                        return TableType.TABLE;
                    case "VIEW":
                        return TableType.VIEW;
                    case "SYNONYM":
                        return TableType.SYNONYM;
                    default:
                        throw new Exception("TableType " + tableType + " is not supported.");
                }
            }
        }
    }
}
