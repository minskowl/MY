using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Savchin.Data.Schema.Builders;

namespace Savchin.Data.Schema.Tests
{
    [TestFixture]
    public class SQLiteSchemaBuilderTests
    {
        private static readonly string connectionString = @"Data Source=" + AppDomain.CurrentDomain.BaseDirectory +
                                                           @"\Resources\test.s3db";
        /// <summary>
        /// Builds the schema test.
        /// </summary>
        [Test]
        public void BuildSchemaTest()
        {
            var builder = new SQLiteSchemaBuilder();

            var schema = builder.BuildDatabaseSchema(connectionString);

            Assert.AreEqual(schema.Tables.Count,3);
            Assert.AreEqual(builder.Errors.Count, 0);
        }
    }
}
