using System;
using System.Xml.Serialization;
using Savchin.Comparer;
using Savchin.Data.Schema.Collections;

namespace Savchin.Data.Schema
{
    /// <summary>
    /// ForeignKey
    /// </summary>
    [Serializable]
    public class ForeignKeySchema : INamedObject
    {
        /// <summary>
        /// TypeColumn
        /// </summary>
        public enum TypeColumn
        {
            /// <summary>
            /// PrimaryKey
            /// </summary>
            PrimaryKey,
            /// <summary>
            /// ForeignKey
            /// </summary>
            ForeignKey
        }

        private string _name;
        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <value>The name.</value>
        [XmlAttribute]
        [Compare]
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }
        private string _foreignTableName;
        /// <summary>
        /// Gets or sets the name of the foreign table.
        /// </summary>
        /// <value>The name of the foreign table.</value>
        [XmlAttribute]
        [Compare]
        public string ForeignTableName
        {
            get { return _foreignTableName; }
            set { _foreignTableName = value; }
        }

        private string _primaryTableName;
        /// <summary>
        /// Gets or sets the name of the primary table.
        /// </summary>
        /// <value>The name of the primary table.</value>
        [XmlAttribute]
        [CompareAttribute]
        public string PrimaryTableName
        {
            get { return _primaryTableName; }
            set { _primaryTableName = value; }
        }

        private AssociationCollection associations = new AssociationCollection();
        /// <summary>
        /// Gets or sets the association.
        /// </summary>
        /// <value>The association.</value>
        [Compare]
        public AssociationCollection Associations
        {
            get { return associations; }
            set { associations = value; }
        }

        private DatabaseSchema _schema;
        /// <summary>
        /// Gets the schema.
        /// </summary>
        /// <value>The schema.</value>
        [XmlIgnore]
        public DatabaseSchema Schema
        {
            get { return _schema; }
        }


        /// <summary>
        /// Gets or sets the foreign table.
        /// </summary>
        /// <value>The foreign table.</value>
        [XmlIgnore]
        public TableSchema ForeignTable
        {
            get { return _schema[_foreignTableName]; }
        }

        /// <summary>
        /// Gets or sets the foreign table.
        /// </summary>
        /// <value>The foreign table.</value>
        [XmlIgnore]
        public TableSchema PrimaryTable
        {
            get { return _schema[_primaryTableName];  }
        }





        /// <summary>
        /// Initializes a new instance of the <see cref="ForeignKeySchema"/> class.
        /// </summary>
        public ForeignKeySchema()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ForeignKeySchema"/> class.
        /// </summary>
        /// <param name="schema">The schema.</param>
        /// <param name="name">The name.</param>
        /// <param name="primaryTableName">Name of the primary table.</param>
        /// <param name="foreignTableName">Name of the foreign table.</param>
        public ForeignKeySchema(DatabaseSchema schema, string name, string primaryTableName, string foreignTableName)
        {
            _schema = schema;
            _name = name;
            _primaryTableName = primaryTableName;
            _foreignTableName = foreignTableName;
        }

        /// <summary>
        /// Initializes the specified schema.
        /// </summary>
        /// <param name="schema">The schema.</param>
        internal void Initialize(DatabaseSchema schema)
        {
            _schema = schema;
            PrimaryTable.ForeignKeys.Add(this);
            ForeignTable.ForeignKeys.Add(this);
            foreach (ForeignKeyAssociation association in associations)
            {
                association.Key = this;
            }
        }
    }
}
