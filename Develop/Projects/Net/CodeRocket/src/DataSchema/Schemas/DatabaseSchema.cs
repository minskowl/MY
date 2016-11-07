using System;
using System.Collections;
using System.ComponentModel;
using System.Data.Common;
using System.Data.OleDb;
using System.Data.SQLite;
using System.Xml.Serialization;
using Savchin.Core;
using Savchin.Data.Schema.Builders;
using Savchin.Data.Schema.Collections;
using Savchin.Comparer;
using System.Web.UI.Design;
using System.Drawing.Design;

namespace Savchin.Data.Schema
{

    /// <summary>
    /// Represents the schema for a database
    /// </summary>
    /// 
    [Serializable]
    public class DatabaseSchema
    {
        /// <summary>
        /// Extension
        /// </summary>
        public const string Extension = "schema";
        private string filePath = String.Empty;
        private string name = String.Empty;
        private string connectionString = String.Empty;
        private ProviderType providerType = ProviderType.OleDb;

        /// <summary>
        /// Creates the builder.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns></returns>
        public static IDataSchemaBuilder CreateBuilder(ProviderType type)
        {
            switch (type)
            {
                case ProviderType.OleDb:
                    return new OleDbSchemaBuilder();

                case ProviderType.SqlLite:
                    return new SQLiteSchemaBuilder();
                default:
                    throw new ArgumentOutOfRangeException("type");
            }
        }

        /// <summary>
        /// Gets the factory.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns></returns>
        public static DbProviderFactory GetFactory(ProviderType type)
        {
            switch (type)
            {
                case ProviderType.OleDb:
                    return OleDbFactory.Instance;
                case ProviderType.SqlLite:
                    return SQLiteFactory.Instance;
                default:
                    throw new ArgumentOutOfRangeException("type");
            }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public DatabaseSchema()
        {
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="name">The name of the database</param>
        public DatabaseSchema(string name)
        {
            Name = name;
        }

        /// <summary>
        /// The database name
        /// </summary>
        /// <value>The name.</value>
        [XmlAttribute()]
        [Compare]
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        /// <summary>
        /// Gets or sets the connection string.
        /// </summary>
        /// <value>The connection string.</value>
        [XmlAttribute]
        [CompareAttribute]
        [Editor(typeof(ConnectionStringEditor), typeof(UITypeEditor))]
        public string ConnectionString
        {
            get { return connectionString; }
            set { connectionString = value; }
        }
        /// <summary>
        /// The database connectionstring
        /// </summary>
        [XmlAttribute]
        [CompareAttribute]
        public ProviderType ProviderType
        {
            get { return providerType; }
            set { providerType = value; }
        }




        /// <summary>
        /// Adds the specified table schema to the database
        /// </summary>
        /// <param name="table">The TableSchema to add</param>
        public void AddTable(TableSchema table)
        {
            tables.Add(table.Name, table);
        }

        /// <summary>
        /// Gets the specified table schema
        /// </summary>
        /// <param name="tableName">Name of the table.</param>
        /// <returns>TableSchema</returns>
        public TableSchema GetTable(string tableName)
        {
            return tables[tableName];
        }

        /// <summary>
        /// Indexer to retrieve a TableSchema
        /// </summary>
        public TableSchema this[String key]
        {
            get { return (tables[key]); }
            set { tables[key] = value; }
        }


        private TableSchemaDictionary tables = new TableSchemaDictionary();

        /// <summary>
        /// Returns a dictionary of the database's TableSchemas
        /// </summary>
        /// <returns></returns>
        [Compare]
        public TableSchemaDictionary Tables
        {
            get { return tables; }
            set { tables = value; }
        }

        /// <summary>
        /// Returnes a SortedList of TableSchemas, sorted by the table names
        /// </summary>
        /// <returns>SortedList</returns>
        [XmlIgnore]
        [Browsable(false)]
        public SortedList SortedTables
        {
            get
            {
                SortedList sl = new SortedList();
                foreach (TableSchema t in tables.Values)
                {
                    sl.Add(t.Name, t);
                }
                return sl;
            }
        }

        ForeignKeyDictionary foreignKeys = new ForeignKeyDictionary();
        /// <summary>
        /// 
        /// Returns a collection of ForeignKeys
        /// </summary>
        /// <returns></returns>
        [CompareAttribute]
        public ForeignKeyDictionary ForeignKeys
        {
            get { return foreignKeys; }
            set { foreignKeys = value; }
        }




        /// <summary>
        /// Saves the specified file path.
        /// </summary>
        /// <param name="newFilePath">The new file path.</param>
        public void Save(string newFilePath)
        {
            if (newFilePath == null) throw new ArgumentNullException("newFilePath");
            TypeSerializer<DatabaseSchema>.ToXmlFile(newFilePath, this);
        }

        /// <summary>
        /// Saves this instance.
        /// </summary>
        public void Save()
        {
            Save(filePath);
        }
        /// <summary>
        /// Loads the specified file path.
        /// </summary>
        /// <param name="filePath">The file path.</param>
        /// <returns></returns>
        public static DatabaseSchema Load(string filePath)
        {
            DatabaseSchema result = TypeSerializer<DatabaseSchema>.FromXmlFile(filePath);
            result.Initialize(filePath);
            return result;
        }

        /// <summary>
        /// Initializes the specified file path.
        /// </summary>
        /// <param name="filePath">The file path.</param>
        internal void Initialize(string filePath)
        {
            this.filePath = filePath;

            foreach (TableSchema table in tables)
            {
                table.Ininialize();
            }
            foreach (ForeignKeySchema key in foreignKeys)
            {
                key.Initialize(this);
            }
        }
    }
}
