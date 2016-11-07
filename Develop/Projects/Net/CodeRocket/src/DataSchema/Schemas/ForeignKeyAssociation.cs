using System.Xml.Serialization;
using Savchin.Comparer;
using Savchin.Text;

namespace Savchin.Data.Schema
{
    /// <summary>
    /// Summary description for ForeignKeyAssociation.
    /// </summary>
    public class ForeignKeyAssociation
    {
        private string _foreignColumnName;
        /// <summary>
        /// Gets or sets the name of the foreign column.
        /// </summary>
        /// <value>The name of the foreign column.</value>
        [XmlAttribute()]
        [Compare]
        public string ForeignColumnName
        {
            get { return _foreignColumnName; }
            set { _foreignColumnName = value; }
        }

        private string _primaryColumnName;
        /// <summary>
        /// Gets or sets the name of the primary column.
        /// </summary>
        /// <value>The name of the primary column.</value>
        [XmlAttribute()]
        [Compare]
        public string PrimaryColumnName
        {
            get { return _primaryColumnName; }
            set { _primaryColumnName = value; }
        }

        private ForeignKeySchema _key;
        /// <summary>
        /// Gets or sets the _key.
        /// </summary>
        /// <value>The _key.</value>
        [XmlIgnore]
        public ForeignKeySchema Key
        {
            get { return _key; }
            set { _key = value; }
        }

        /// <summary>
        /// Gets the primary column.
        /// </summary>
        /// <value>The primary column.</value>
        [XmlIgnore]
        public ColumnSchema PrimaryColumn
        {
            get { return _key.PrimaryTable[_primaryColumnName]; }
        }

        /// <summary>
        /// Gets the foreign column.
        /// </summary>
        /// <value>The foreign column.</value>
        [XmlIgnore]
        public ColumnSchema ForeignColumn
        {
            get { return _key.ForeignTable[_foreignColumnName]; }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ForeignKeyAssociation"/> class.
        /// </summary>
        public ForeignKeyAssociation()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ForeignKeyAssociation"/> class.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="primaryColumnName">Name of the primary column.</param>
        /// <param name="foreignColumnName">Name of the foreign column.</param>
        public ForeignKeyAssociation(ForeignKeySchema key, string primaryColumnName, string foreignColumnName)
        {
            _key = key;
            _foreignColumnName = foreignColumnName;
            _primaryColumnName = primaryColumnName;
        }


    }
}
