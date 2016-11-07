using System.Web;
using System.Web.Caching;
using KnowledgeBase.DAL;
using Savchin.Data.Common;
using Savchin.Development;

namespace KnowledgeBase.Dal
{
    public class DalMultiThreadProvider : MultiThreadProvider, IDalObjectProvider
    {
 
        private readonly Cache cache;
     
        private readonly IFactoryProvider _provider;


        /// <summary>
        /// Initializes a new instance of the <see cref="SingleThreadProvider"/> class.
        /// </summary>
        /// <param name="provider">The provider.</param>
        public DalMultiThreadProvider(IFactoryProvider provider)
        {
         
            _provider = provider;

            cache = HttpRuntime.Cache;
        }

        /// <summary>
        /// Gets the cache.
        /// </summary>
        /// <value>The cache.</value>
        public Cache Cache
        {
            get { return cache; }
        }
  
        /// <summary>
        /// Provides the connection.
        /// </summary>
        /// <returns></returns>
        public DBConnection ProvideConnection()
        {
            var connection = (DBConnection)Context["DbConnection"];
            if (connection != null) return connection;

            connection = CreateConnection();
            Context["DbConnection"] = connection;
            return connection;
        }

        /// <summary>
        /// Creates new instance connection.
        /// </summary>
        /// <returns></returns>
        public DBConnection CreateConnection()
        {
            return _provider.CreateConnection();
        }
    }
}
