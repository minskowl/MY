using System;
using System.Collections.Generic;


namespace Savchin.Data.Schema.Collections
{
    /// <summary>
    /// Association Collection class.
    /// </summary>
    [Serializable]
    public class AssociationCollection : List<ForeignKeyAssociation>
    {
        /// <summary>
        /// Gets the foreign columns.
        /// </summary>
        /// <returns></returns>
        public List<ColumnSchema> GetForeignColumns()
        {
            return GetColumns(ForeignKeySchema.TypeColumn.ForeignKey);
        }
        /// <summary>
        /// Gets the primary columns.
        /// </summary>
        /// <returns></returns>
        public List<ColumnSchema> GetPrimaryColumns()
        {
            return GetColumns(ForeignKeySchema.TypeColumn.PrimaryKey);
        }
        /// <summary>
        /// Gets the columns.
        /// </summary>
        /// <param name="table">The table.</param>
        /// <returns></returns>
        public List<ColumnSchema> GetColumns(TableSchema table)
        {
            ForeignKeySchema.TypeColumn type = ReferenceEquals(table, this[0].ForeignColumn.Table)
                                                                       ? ForeignKeySchema.TypeColumn.ForeignKey
                                                                       : ForeignKeySchema.TypeColumn.PrimaryKey;



            return GetColumns(type);
        }

        /// <summary>
        /// Gets the columns.
        /// </summary>
        /// <param name="tableName">Name of the table.</param>
        /// <returns></returns>
        public List<ColumnSchema> GetColumns(string tableName)
        {
            ForeignKeySchema.TypeColumn type = this[0].ForeignColumn.Table.Name == tableName
                                                              ? ForeignKeySchema.TypeColumn.ForeignKey
                                                              : ForeignKeySchema.TypeColumn.PrimaryKey;



            return GetColumns(type);
        }

        /// <summary>
        /// Gets the columns.
        /// </summary>
        /// <param name="columnType">Type of the column.</param>
        /// <returns></returns>
        public List<ColumnSchema> GetColumns(ForeignKeySchema.TypeColumn columnType)
        {
            List<ColumnSchema> result = new List<ColumnSchema>();

            foreach (ForeignKeyAssociation association in this)
            {
                if (columnType == ForeignKeySchema.TypeColumn.ForeignKey)
                    result.Add(association.ForeignColumn);
                else
                    result.Add(association.PrimaryColumn);
            }
            return result;
        }
    }
}
