using System;
using System.Data;
using NUnit.Framework;
using Savchin.Data.Common;

namespace DataTest
{
    [TestFixture]
    public class InsertCommandBuilderTests
    {
        [Test]
        public void AddParameter()
        {

            InsertCommandBuilder builder = new InsertCommandBuilder(TestSettings.CreateConnection());
            builder.TableName = "Table1";
            builder.AddParameter("p1",DbType.Date);
            builder.AddParameter("p2",typeof( DateTime));
            builder.AddParameter("p3",1);
            builder.AddParameter("p4","par4",  true);
            IDbCommand command = builder.Build();
            Assert.AreEqual(command.CommandType, CommandType.Text);
            Assert.IsNotNull(command.Parameters["@p1"]);
            Assert.IsNotNull(command.Parameters["@p2"]);
            Assert.IsNotNull(command.Parameters["@p3"]);
            Assert.IsNotNull(command.Parameters["@par4"]);
        }
    }
}
