using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace KnowledgeBase.SqlLite.Dal.Factories
{
    static class DataReaderHelper
    {
        /// <summary>
        /// Gets the integer.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="reader">The reader.</param>
        /// <param name="ordinal">The ordinal.</param>
        /// <returns></returns>
        public static object GetInteger<T>(this IDataReader reader, int ordinal)
            where T : struct
        {
            var value = reader.GetValue(ordinal);
            return value is DBNull ? value : Convert.ChangeType(value,typeof(T));
        }
    }
}
