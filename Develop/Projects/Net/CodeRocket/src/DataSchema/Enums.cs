using System;


namespace Savchin.Data.Schema
{

    /// <summary>
    /// ProviderType
    /// </summary>
    public  enum ProviderType
    {
        /// <summary>
        /// OleDb
        /// </summary>
        OleDb,
        /// <summary>
        /// SqlLite
        /// </summary>
        SqlLite
    }

    /// <summary>
    /// The type of table
    /// </summary>
    public enum TableType
    {
        /// <summary>
        /// A system view table
        /// </summary>
        SYSTEM_VIEW,
        /// <summary>
        /// A system table
        /// </summary>
        SYSTEM_TABLE,
        /// <summary>
        /// A table
        /// </summary>
        TABLE,
        /// <summary>
        /// A view table
        /// </summary>
        VIEW,
        /// <summary>
        /// A alias to a Table
        /// </summary>
        SYNONYM
    }

    
}
