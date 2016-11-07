using Savchin.Data.Common;

namespace DataTest
{
    internal class TestSettings
    {
        public const string ConnectionString=@"Data Source=MONSTER\MSSQL2000;Integrated Security=True";
        
        public static DBConnection CreateConnection()
        {
            return new DBConnection(ConnectionString);
            
        }
    }
}
