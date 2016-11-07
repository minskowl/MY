using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Web.Caching;
using KnowledgeBase.DAL;
using KnowledgeBase.Dal;
using Savchin.Data.Common;
using Savchin.Web.Development;
using KnowledgeBase.Core;

namespace KnowledgeBase.SiteCore.Providers
{
    public class DalWebProvider : WebProvider, IDalObjectProvider
    {
        private const string keyConnection = "ApplicationContext_Connection";
        
        /// Gets the cache.
        /// </summary>
        /// <value>The cache.</value>
        public Cache Cache
        {
            get { return HttpRuntime.Cache; }
        }

        private IFactoryProvider _factoryProvider;
        public IFactoryProvider FactoryProvider
        {
            get { return _factoryProvider; }
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="WebProvider"/> class.
        /// </summary>
        /// <param name="connectionString">The connection string.</param>
        public DalWebProvider(IFactoryProvider provider)
        {
            _factoryProvider = provider;
        }
        public DBConnection CreateConnection()
        {
            return _factoryProvider.CreateConnection();
        }
        /// <summary>
        /// Provides current instance of connection.
        /// </summary>
        /// <returns></returns>
        public DBConnection ProvideConnection()
        {
            IDictionary items = HttpContext.Current.Items;
            DBConnection result;
            if (items.Contains(keyConnection))
            {
                result = (DBConnection)items[keyConnection];
            }
            else
            {
                result = CreateConnection();
                items[keyConnection] = result;
            }
            return result;
        }



        /// <summary>
        /// Disposes the current connection.
        /// </summary>
        public void DisposeCurrentConnection()
        {
            DBConnection connection = (DBConnection)HttpContext.Current.Items[keyConnection];
            if (connection == null)
                return;
            connection.Dispose();
            HttpContext.Current.Items.Remove(keyConnection);
        }
    }
}
