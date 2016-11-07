using System;
using System.Data;
using System.Xml.Serialization;

using Savchin.Comparer;
using Savchin.Text;

namespace Savchin.Data.Schema
{
    /// <summary>
    /// Represents the schema for a column in a table
    /// </summary>
    /// 
    [Serializable]
    public class ColumnSchema : INamedObject
    {

        private int _dataTypeId;

        private bool _isForeignKey;
        private bool _isPrimaryKey;



        private string _name = String.Empty;
        /// <summary>
        /// The column name
        /// </summary>
        [XmlAttribute]
        [Compare]
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        private string _alias = String.Empty;
        /// <summary>
        /// The column alias
        /// </summary>
        [XmlAttribute]
        [CompareAttribute]
        public string Alias
        {
            get
            {
                if (_alias.Length == 0 || _alias == _name)
                    return ProperName;
                else
                    return _alias;
            }
            set { _alias = value; }
        }

        private string _netType = String.Empty;
        /// <summary>
        /// The .NET Object equivalent for the column
        /// </summary>
        [XmlAttribute]
        [CompareAttribute]
        public string NetType
        {
            get { return _netType; }
            set { _netType = value; }
        }

        private string _dataTypeFull = String.Empty;
        /// <summary>
        /// Gets or sets the data type full.
        /// </summary>
        /// <value>The data type full.</value>
        [XmlAttribute]
        [CompareAttribute]
        public string DataTypeFull
        {
            get { return _dataTypeFull; }
            set { _dataTypeFull = value; }
        }

        private DbType _dbDataType = DbType.Object;
        /// <summary>
        /// Gets the type of the db data.
        /// </summary>
        /// <value>The type of the db data.</value>
        [XmlAttribute]
        [CompareAttribute]
        public DbType DbDataType
        {
            get { return _dbDataType; }
            set { _dbDataType = value; }
        }

        private string _dataType = String.Empty;
        /// <summary>
        /// The database type
        /// </summary>
        [XmlAttribute]
        [CompareAttribute]
        public string DataType
        {
            get { return _dataType; }
            set { _dataType = value; }
        }

        private int _length;
        /// <summary>
        /// The column length
        /// </summary>
        [XmlAttribute]
        [CompareAttribute]
        public int Length
        {
            get { return _length; }
            set { _length = value; }
        }

        private byte _precision;
        /// <summary>
        /// Gets or sets the precision.
        /// </summary>
        /// <value>The precision.</value>
        [XmlAttribute]
        [CompareAttribute]
        public byte Precision
        {
            get { return _precision; }
            set { _precision = value; }
        }
        private byte _scale;
        /// <summary>
        /// Gets or sets the scale.
        /// </summary>
        /// <value>The scale.</value>
        [XmlAttribute]
        [CompareAttribute]
        public byte Scale
        {
            get { return _scale; }
            set { _scale = value; }
        }
        private bool _active = true;
        /// <summary>
        /// Specifies whether the column is active.  Used primarily
        /// for on/off state in GUIs.  It allows for a column to still
        /// be part of a TableSchema, but ignored for various reasons
        /// </summary>
        [XmlAttribute]
        public bool IsActive
        {
            get { return _active; }
            set { _active = value; }
        }

        private bool _isReadOnly;
        /// <summary>
        /// Specifies whether the column is readonly
        /// </summary>
        [XmlAttribute]
        [CompareAttribute]
        public bool IsReadOnly
        {
            get { return _isReadOnly; }
            set { _isReadOnly = value; }
        }

        private string _lowValue = null;
        /// <summary>
        /// Gets or sets the low value.
        /// </summary>
        /// <value>The low value.</value>
        [XmlAttribute]
        [CompareAttribute]
        public string LowValue
        {
            get { return _lowValue; }
            set { _lowValue = value; }
        }


        private string _highValue = null;
        /// <summary>
        /// Gets or sets the high value.
        /// </summary>
        /// <value>The high value.</value>
        [XmlAttribute]
        [CompareAttribute]
        public string HighValue
        {
            get { return _highValue; }
            set { _highValue = value; }
        }

        private int _ordinal;
        /// <summary>
        /// The ordinal number of the column
        /// </summary>
        [XmlAttribute]
        [CompareAttribute]
        public int Ordinal
        {
            get { return _ordinal; }
            set { _ordinal = value; }
        }

        private bool _allowNulls;
        /// <summary>
        /// Specifies whether the column allows nulls
        /// </summary>
        [XmlAttribute]
        [CompareAttribute]
        public bool AllowNulls
        {
            get { return _allowNulls; }
            set { _allowNulls = value; }
        }
        private bool _isUnique;
        /// <summary>
        /// Specifies whether the column is unique
        /// </summary>
        [XmlAttribute]
        [CompareAttribute]
        public bool IsUnique
        {
            get { return _isUnique; }
            set { _isUnique = value; }
        }

        private bool _isAutoIncrement;
        /// <summary>
        /// Specifies whether the column is an autoincrement column
        /// </summary>
        [XmlAttribute]
        [CompareAttribute]
        public bool IsAutoIncrement
        {
            get { return _isAutoIncrement; }
            set { _isAutoIncrement = value; }
        }

        /// <summary>
        /// Specifies whether the column is a primary key
        /// </summary>
        [XmlAttribute]
        [CompareAttribute]
        public bool IsPrimaryKey
        {
            get { return _isPrimaryKey; }
            set { _isPrimaryKey = value; }
        }


        /// <summary>
        /// Specifies whether the column is a foreign key
        /// </summary>
        [XmlAttribute]
        [CompareAttribute]
        public bool IsForeignKey
        {
            get { return _isForeignKey; }
            set { _isForeignKey = value; }
        }

        /// <summary>
        /// Specifies whether the column is a key (primary or foreign)
        /// </summary>
        [XmlIgnore]
        public bool IsKey
        {
            get
            {
                if (IsPrimaryKey || IsForeignKey) return true;
                else return false;
            }
        }


        /// <summary>
        /// The numeric id (assigned by ADO.NET) for the specified data type
        /// </summary>
        [XmlAttribute]
        public int DataTypeId
        {
            get { return _dataTypeId; }
            set { _dataTypeId = value; }
        }

        private string _defaultValue = String.Empty;
        /// <summary>
        /// Gets or sets the default value.
        /// </summary>
        [XmlAttribute]
        [CompareAttribute]
        public string DefaultValue
        {
            get { return _defaultValue; }
            set { _defaultValue = value; }
        }


        /// <summary>
        /// Returns a proper case rendition of the column name, with 
        /// all spaces removed
        /// </summary>
        /// <returns></returns>
        public string ProperName
        {
            get { return StringUtil.ToTrimmedProperCase(Name); }
        }

        /// <summary>
        /// Returns a .NET field name for the specified column, in the format of
        /// _columnName
        /// </summary>
        /// <returns></returns>
        public string MemberName
        {
            get { return "_" + ProperName; }
        }



        private bool _isSelected;
        /// <summary>
        /// Gets or sets a value indicating whether this instance is selected.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is selected; otherwise, <c>false</c>.
        /// </value>
        [XmlIgnore]
        public bool IsSelected
        {
            get { return _isSelected; }
            set { _isSelected = value; }
        }

        private TableSchema _table;
        /// <summary>
        /// Gets the table.
        /// </summary>
        /// <value>The table.</value>
        [XmlIgnore]
        public TableSchema Table
        {
            get { return _table; }
        }


        /// <summary>
        /// Returns a <see cref="T:System.String"/> that represents the current <see cref="T:System.Object"/>.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.String"/> that represents the current <see cref="T:System.Object"/>.
        /// </returns>
        public override string ToString()
        {
            return StringUtil.ToString(this);
        }


        /// <summary>
        /// Gets the int validator.
        /// </summary>
        /// <returns></returns>
        public string GetIntValidator()
        {
            string res = "";
            if (LowValue.Trim().Length > 0)
                res += " " + Name + "< " + LowValue + " ||";
            if (HighValue.Trim().Length > 0)
                res += " " + Name + "> " + HighValue + " ||";
            if (IsPrimaryKey && IsAutoIncrement == false)
                res += " " + Name + "==0 ||";

            return res;
        }

        /// <summary>
        /// Gets the string validator.
        /// </summary>
        /// <returns></returns>
        public string GetStringValidator()
        {
            string res = "";

            if (AllowNulls)
                res += " ( " + Name + "!= null && " + Name + ".Length >" + Length + " ) ||";
            else
                res += " " + Name + ".Length >" + Length + "||";

            if (!string.IsNullOrEmpty(LowValue))
            {
                if (AllowNulls)
                    res += " ( " + Name + "!= null && " + Name + ".Length <" + LowValue + " ) ||";
                else
                    res += " " + Name + ".Length <" + LowValue + "||";
            }

            return res;
        }

        /// <summary>
        /// Sets the table.
        /// </summary>
        /// <param name="parent">The parent.</param>
        internal void SetTable(TableSchema parent)
        {
            _table = parent;
        }
    }
}
