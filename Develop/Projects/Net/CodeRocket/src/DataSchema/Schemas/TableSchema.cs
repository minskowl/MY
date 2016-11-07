using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Xml.Serialization;
using Savchin.Comparer;
using Savchin.Data.Schema.Collections;
using Savchin.Text;

namespace Savchin.Data.Schema
{
    /// <summary>
    /// Represents the schema for a database table
    /// </summary>
    [Serializable]
    public class TableSchema : INamedObject
    {
        private TableType tableType = TableType.TABLE;
        private bool _active = true;
        private string _name = String.Empty;
        private string _alias = String.Empty;


        #region Properties

        /// <summary>
        /// The table name
        /// </summary>
        [XmlAttribute]
        [Compare]
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }
        /// <summary>
        /// The table alias
        /// </summary>
        [XmlAttribute]
        [CompareAttribute]
        public string Alias
        {
            get
            {
                if (_alias.Length == 0 || _alias == _name) return ProperName;
                else return _alias;
            }
            set { _alias = value; }
        }
        /// <summary>
        /// Returns the table name in proper case, with all spaces removed
        /// </summary>
        /// <returns></returns>
        [Browsable(false)]
        [XmlIgnore]
        public string ProperName
        {
            get { return StringUtil.ToTrimmedProperCase(Name); }
        }

        /// <summary>
        /// Specifies whether the table is active.  Used primarily
        /// for on/off state in GUIs.  It allows for a table to still
        /// be part of a DatabaseSchema, but ignored for various reasons
        /// </summary>
        [XmlAttribute]
        [CompareAttribute]
        public bool IsActive
        {
            get { return _active; }
            set { _active = value; }
        }

        /// <summary>
        /// Property TableType (TableType)
        /// </summary>
        [XmlAttribute]
        [CompareAttribute]
        public TableType TableType
        {
            get { return tableType; }
            set { tableType = value; }
        }


        #region Columns
        private ColumnSchemaDictionary columns = new ColumnSchemaDictionary();
        /// <summary>
        /// Returns a collection of ColumnSchemas
        /// </summary>
        /// <returns></returns>
        [CompareAttribute]
        public ColumnSchemaDictionary Columns
        {
            get { return columns; }
            set { columns = value; }
        }



        /// <summary>
        /// Indexer to retrieve a ColumnSchema by name
        /// </summary>
        public ColumnSchema this[String key]
        {
            get { return (columns[key]); }
            set { columns[key] = value; }
        }

        readonly List<ColumnSchema> activeColumns = new List<ColumnSchema>();
        /// <summary>
        /// Gets the active columns.
        /// </summary>
        /// <value>The active columns.</value>
        [Browsable(false)]
        [XmlIgnore]
        public List<ColumnSchema> ActiveColumns
        {
            get { return new List<ColumnSchema>(activeColumns); }
        }

        readonly List<ColumnSchema> ordinalColumns = new List<ColumnSchema>();
        /// <summary>
        /// Returns the list of ColumnSchemas, sorted by their ordinal representation
        /// </summary>
        /// <returns></returns>
        [Browsable(false)]
        [XmlIgnore]
        public List<ColumnSchema> OrdinalColumns
        {
            get { return new List<ColumnSchema>(ordinalColumns); }
        }

        readonly SortedDictionary<string, ColumnSchema> sortedColumns = new SortedDictionary<string, ColumnSchema>();
        /// <summary>
        /// Returns the list of ColumnSchemas, sorted by their names
        /// </summary>
        /// <returns></returns>
        [Browsable(false)]
        [XmlIgnore]
        public SortedDictionary<string, ColumnSchema> SortedColumns
        {
            get { return new SortedDictionary<string, ColumnSchema>(sortedColumns); }
        }

        #endregion

        #region Procedures

        /// <summary>
        /// Returns a collection of ProcedureSchemas
        /// </summary>
        /// <returns></returns>
        [XmlIgnore]
        public ProcedureSchemaDictionary Procedures { get; set; }

        /// <summary>
        /// Gets the procedure count.
        /// </summary>
        /// <value>The procedure count.</value>
        [Browsable(false)]
        [XmlIgnore]
        public int ProcedureCount
        {
            get { return Procedures.Count; }
        }



        #endregion

        #region Keys

        PrimaryKeyColumnDictionary primaryKeys = new PrimaryKeyColumnDictionary();
        /// <summary>
        /// Returns a collection of ColumnSchemas that are primary keys
        /// </summary>
        /// <returns></returns>
        [XmlIgnore]
        public PrimaryKeyColumnDictionary PrimaryKeys
        {
            get
            {
                if (primaryKeys == null)
                {
                    primaryKeys = new PrimaryKeyColumnDictionary();
                    foreach (ColumnSchema columnSchema in columns.Values)
                    {
                        if (columnSchema.IsPrimaryKey)
                            primaryKeys.Add(columnSchema.Name, columnSchema);
                    }

                }

                return primaryKeys;
            }
        }



        /// <summary>
        /// Returns a collection of ForeignKeys
        /// </summary>
        /// <returns></returns>
        [XmlIgnore]
        public ForeignKeyDictionary ForeignKeys { get; set; }

        /// <summary>
        /// Gets the number of primary keys
        /// </summary>
        /// <returns></returns>
        [Browsable(false)]
        [XmlIgnore]
        public int PrimaryKeyCount
        {
            get
            {
                int i = 0;
                foreach (ColumnSchema c in columns.Values)
                {
                    if (c.IsPrimaryKey)
                        i++;
                }
                return i;
            }
        }




        /// <summary>
        /// Gets the primary key columns.
        /// </summary>
        /// <value>The primary key columns.</value>
        [Browsable(false)]
        [XmlIgnore]
        public ICollection<ColumnSchema> PrimaryKeyColumns
        {
            get
            {
                return primaryKeys.Values;
            }
        }
        ///// <summary>
        ///// Gets the specified primary key Column Schema by index.
        ///// </summary>
        ///// <param name="index"></param>
        ///// <returns></returns>
        ///// <remarks>
        ///// If a database table contains two foreign keys, then index 0 would retrieve
        ///// the first, and index 1 would be the second.  The index is in relation
        ///// to the number of foreign keys, not the total number of columns
        ///// </remarks>
        //public ColumnSchema GetForeignKey(int index)
        //{
        //    //ArrayList al = new ArrayList(ForeignKeys.Values);
        //    //return ((ColumnSchema)al[index]);
        //    return ForeignKeys[]
        //}
        /// <summary>
        /// Gets a value indicating whether this instance has foreign keys.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance has foreign keys; otherwise, <c>false</c>.
        /// </value>
        [Browsable(false)]
        [XmlIgnore]
        public bool HasForeignKeys
        {
            get { return (ForeignKeys.Count > 0); }
        }

        /// <summary>
        /// Gets a value indicating whether this instance has primary keys.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance has primary keys; otherwise, <c>false</c>.
        /// </value>
        [Browsable(false)]
        [XmlIgnore]
        public bool HasPrimaryKeys
        {
            get { return (PrimaryKeyCount > 0); }
        }
        #endregion


        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="TableSchema"/> class.
        /// </summary>
        public TableSchema()
        {
            ForeignKeys = new ForeignKeyDictionary();
            Procedures = new ProcedureSchemaDictionary();
        }

        #region Methods

        #region Columns
        /// <summary>
        /// Adds the specified ColumnSchema
        /// </summary>
        /// <param name="c"></param>
        public void AddColumn(ColumnSchema c)
        {
            columns[c.Name] = c;
            AddColumnInAllCollections(c);
        }

        private void AddColumnInAllCollections(ColumnSchema column)
        {
            if (ordinalColumns.Count < column.Ordinal)
            {
                int i = ordinalColumns.Count;
                while (i < column.Ordinal)
                {
                    ordinalColumns.Insert(i, null);
                    i++;
                }
            }
            if (ordinalColumns.Count == column.Ordinal)
                ordinalColumns.Add(column);
            else
                ordinalColumns[column.Ordinal] = column;

            sortedColumns.Add(column.Name, column);
            if (column.IsActive)
                activeColumns.Add(column);

            if (column.IsPrimaryKey)
                primaryKeys.Add(column.Name, column);

            column.SetTable(this);
        }

        /// <summary>
        /// Removes the specified ColumnSchema
        /// </summary>
        /// <param name="name">The name of the ColumnSchema to remove</param>
        public void RemoveColumn(string name)
        {
            columns.Remove(name);
        }

        /// <summary>
        /// Retrieves the specified ColumnSchema
        /// </summary>
        /// <param name="name">The name of the ColumnSchema to retrieve</param>
        /// <returns></returns>
        public ColumnSchema GetColumn(string name)
        {
            return columns[name];
        }
        #endregion

        #region Procedures
        /// <summary>
        /// Adds the specified ProcedureSchema
        /// </summary>
        /// <param name="c"></param>
        public void AddProcedure(ProcedureSchema c)
        {
            Procedures[c.Name] = c;
        }

        /// <summary>
        /// Removes the specified ProcedureSchema
        /// </summary>
        /// <param name="name">The name of the ProcedureSchema to remove</param>
        public void RemoveProcedure(string name)
        {
            Procedures.Remove(name);
        }

        /// <summary>
        /// Retrieves the specified ProcedureSchema
        /// </summary>
        /// <param name="name">The name of the ProcedureSchema to retrieve</param>
        /// <returns></returns>
        public ProcedureSchema GetProcedure(string name)
        {
            return Procedures[name];
        }
        #endregion

        /// <summary>
        /// Gets the specified primary key Column Schema by index.
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        /// <remarks>
        /// If a database table contains two primary keys, then index 0 would retrieve
        /// the first, and index 1 would be the second.  The index is in relation
        /// to the number of primary keys, not the total number of columns
        /// </remarks>
        public ColumnSchema GetPrimaryKey(int index)
        {
            ArrayList al = new ArrayList(PrimaryKeys.Values);
            return ((ColumnSchema)al[index]);
        }

        /// <summary>
        /// Gets the get default validator.
        /// </summary>
        /// <value>The get default validator.</value>
        public string GetDefaultValidator()
        {

            StringBuilder builder = new StringBuilder();
            foreach (ColumnSchema col in columns.Values)
            {
                if (col.NetType == "System.String" || col.NetType == "String")
                {
                    builder.Append(col.GetStringValidator());
                }
                else if (col.NetType == "Int32")
                {
                    builder.Append(col.GetIntValidator());
                }


            }
            return (builder.Length > 0) ? builder.ToString().Substring(0, builder.Length - 2) : string.Empty;

        }

        #endregion



        ///// <summary>
        ///// Returns a <see cref="T:System.String"></see> that represents the current <see cref="T:System.Object"></see>.
        ///// </summary>
        ///// <returns>
        ///// A <see cref="T:System.String"></see> that represents the current <see cref="T:System.Object"></see>.
        ///// </returns>
        //public override string ToString()
        //{
        //    return Environment.NewLine + StringUtil.ToString(this) + Environment.NewLine + Columns.ToString();
        //}

        /// <summary>
        /// Ininializes this instance.
        /// </summary>
        internal void Ininialize()
        {
            foreach (ColumnSchema column in columns)
            {
                AddColumnInAllCollections(column);
            }
        }
    }


}
