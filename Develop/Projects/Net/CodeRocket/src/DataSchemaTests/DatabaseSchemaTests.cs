using System.Collections.Generic;
using NUnit.Framework;
using Savchin.Data.Schema.Tests.Core;

namespace Savchin.Data.Schema.Tests
{
    public class DatabaseSchemaTests : TestBase
    {
        [Test]
        public void LoadTest()
        {
            DatabaseSchema schema = DatabaseSchema.Load(Helper.SchemaPath);
            Assert.AreEqual(6, schema.Tables.Count);
            foreach (TableSchema table in schema.Tables)
            {
                Assert.IsNotNull(table.Columns);
                Assert.Greater(table.Columns.Count, 1, "Tables has columns less 1");
            }
            Assert.AreEqual(6, schema.ForeignKeys.Count);
            foreach (ForeignKeySchema key in schema.ForeignKeys)
            {
                Assert.IsNotNull(key.Associations);
                Assert.Greater(key.Associations.Count, 0);
            }

            TableSchema categoriesTable = schema.Tables["Categories"];
            Assert.IsNotNull(categoriesTable);

            Assert.AreEqual(4, categoriesTable.Columns.Count);
            Assert.AreEqual(2, categoriesTable.ForeignKeys.Count);
            Assert.IsTrue(categoriesTable.HasForeignKeys);

            ForeignKeySchema selfKey = categoriesTable.ForeignKeys["FK_Categories_Categories"];

            Assert.AreSame(schema.ForeignKeys["FK_Categories_Categories"], selfKey);
            Assert.AreSame(selfKey.PrimaryTable, categoriesTable);
            Assert.AreSame(selfKey.ForeignTable, categoriesTable);

            Assert.AreEqual(1, selfKey.Associations.Count);

            ForeignKeyAssociation association = selfKey.Associations[0];
            Assert.AreSame(association.Key, selfKey);

            Assert.IsNotNull(association.ForeignColumn);
            Assert.IsNotNull(association.PrimaryColumn);


            Assert.IsNotEmpty(association.ForeignColumnName);
            Assert.IsNotEmpty(association.PrimaryColumnName);

            List<ColumnSchema> columns = selfKey.Associations.GetColumns(selfKey.PrimaryTable);
            Assert.AreEqual(1, columns.Count);

            ColumnSchema primaryKeyColumn = columns[0];

            Assert.IsNotNull(primaryKeyColumn);
            Assert.AreEqual("CategoryID", primaryKeyColumn.Name);
        }
    }
}
