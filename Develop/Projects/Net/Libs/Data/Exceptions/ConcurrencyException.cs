using System;
using System.Data.Common;
using System.Runtime.Serialization;

namespace Savchin.Data
{
    /// <summary>
    /// Indicates that a concurrency excepetion has occured
    /// </summary>
    [Serializable]
    public class ConcurrencyException : DbException 
    {
        public ConcurrencyException()
        { }

        public ConcurrencyException(string tableName)
            : base("Concurrency error for " + tableName + ". No records where updated during last query but at least 1 was expected")
        { }

        public ConcurrencyException(string message, Exception inner)
            : base(message, inner)
        { }

        protected ConcurrencyException(
          SerializationInfo info,
          StreamingContext context)
            : base(info, context)
        { }
    }
}
