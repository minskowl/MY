using System.Data.Common;
using Savchin.Data.Common;

namespace Site.Core
{
    public class ManagerBase<TFactory>
        where TFactory : class, IFactory, new()
    {
        //private static readonly ProxyGenerator generator = new ProxyGenerator();
        protected readonly TFactory factory = new TFactory();

        ///// <summary>
        ///// Creates this instance.
        ///// </summary>
        ///// <returns></returns>
        //public static TManager Create()
        //{
        //    return new TManager();
        //    // return (TManager)generator.CreateClassProxy(typeof(TManager), new CallLogIntersector());
        //}
        /// <summary>
        /// Initializes a new instance of the <see cref="ManagerBase&lt;T&gt;"/> class.
        /// </summary>
        protected ManagerBase()
        {
            factory.Database = SiteContext.Current.Connection;
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="ManagerBase&lt;TFactory&gt;"/> class.
        /// </summary>
        /// <param name="connection">The connection.</param>
        protected ManagerBase(DBConnection connection)
        {
            factory.Database = connection;
        }
    }
}