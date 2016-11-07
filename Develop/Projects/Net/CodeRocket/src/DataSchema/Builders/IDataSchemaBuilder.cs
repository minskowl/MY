using System;
using System.Collections.Generic;

namespace Savchin.Data.Schema.Builders
{
    /// <summary>
    /// IDataSchemaBuilder
    /// </summary>
    public interface IDataSchemaBuilder
    {
        /// <summary>
        /// Builds the database schema.
        /// </summary>
        /// <param name="connectionString">The connection string.</param>
        /// <returns></returns>
        DatabaseSchema BuildDatabaseSchema(string connectionString);

        /// <summary>
        /// Gets the errors.
        /// </summary>
        /// <value>The errors.</value>
        IList<Exception> Errors { get; }
    }
}