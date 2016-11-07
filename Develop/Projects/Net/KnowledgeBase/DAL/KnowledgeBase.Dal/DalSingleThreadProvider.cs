using System.Web.Caching;
using KnowledgeBase.DAL;
using Savchin.Data.Common;
using Savchin.Development;

namespace KnowledgeBase.Dal
{
    /// <summary>
    /// SingleThreadProvider
    /// </summary>
    public class DalSingleThreadProvider : SingleThreadProvider, IDalObjectProvider
    {
        readonly DBConnection database;
        readonly Cache cache = new Cache();

        private IFactoryProvider _provider;

        /// <summary>
        /// Initializes a new instance of the <see cref="SingleThreadProvider"/> class.
        /// </summary>
        /// <param name="provider">The provider.</param>
        public DalSingleThreadProvider(IFactoryProvider provider)
        {
            _provider = provider;
            database = CreateConnection();
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
        /// Gets the factory provider.
        /// </summary>
        /// <value>The factory provider.</value>
        public IFactoryProvider FactoryProvider
        {
            get { return _provider; }
        }

        /// <summary>
        /// Provides the connection.
        /// </summary>
        /// <returns></returns>
        public DBConnection ProvideConnection()
        {
            return database;
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
