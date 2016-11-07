using System.Data;
using NUnit.Framework;
using Savchin.Data.Common;

namespace DataTest
{
    [TestFixture]
    public class DBConnectionTests
    {
        [Test]
        public void  Test()
        {
            DBConnection connection = TestSettings.CreateConnection();
            InsertCommandBuilder builder = new InsertCommandBuilder(connection);
            builder.TableName = "Table1";
            builder.AddParameter("value","1");
            IDbCommand command=   builder.Build();
            Assert.IsNotNull(command);
            Assert.AreEqual( 1,connection.ExecuteNonQuery(command));
            
        }
    }
}
