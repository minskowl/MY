using KnowledgeBase.DAL;
using Savchin.Data.Common;
using Savchin.Development;
using System.Web.Caching;

namespace KnowledgeBase.Dal
{
    public interface IDalObjectProvider : IApplicationObjectsProvider
    {
         
        /// <summary>
        /// Provides current instance of connection.
        /// </summary>
        /// <returns></returns>
        DBConnection ProvideConnection();
        /// <summary>
        /// Creates new instance connection.
        /// </summary>
        /// <returns></returns>
        DBConnection CreateConnection();

        /// <summary>
        /// Gets the cache.
        /// </summary>
        /// <value>The cache.</value>
        Cache Cache { get; }


    }
}
