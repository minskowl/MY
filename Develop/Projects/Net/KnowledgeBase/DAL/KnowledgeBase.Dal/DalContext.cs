using System.Web.Caching;
using Savchin.Data.Common;
using Savchin.Development;

namespace KnowledgeBase.Dal
{

    /// <summary>
    /// Application Context
    /// </summary>
    public class DalContext : AppContext
    {
        #region Properties
        private readonly IDalObjectProvider _provider;

        private static DalContext _currentDal;

        /// <summary>
        /// Gets or sets the current.
        /// </summary>
        /// <value>The current.</value>
        public static DalContext CurrentDal
        {
            get { return _currentDal; }
            set
            {
                _currentDal = value;
                CurrentApp = value;
            }
        }

        /// <summary>
        /// Gets the connection.
        /// </summary>
        /// <value>The connection.</value>
        public DBConnection Connection
        {
            get { return _provider.ProvideConnection(); }
        }

        /// <summary>
        /// Gets the cache.
        /// </summary>
        /// <value>The cache.</value>
        public Cache Cache
        {
            get { return _provider.Cache; }
        }
        #endregion



        /// <summary>
        /// Initializes a new instance of the <see cref="DalContext"/> class.
        /// </summary>
        /// <param name="provider">The provider.</param>
        public DalContext(IDalObjectProvider provider)
            : base(provider)
        {
            _provider = provider;

        }


        /// <summary>
        /// Begins the transaction.
        /// </summary>
        public void BeginTransaction()
        {
            Connection.Transaction.Begin();
        }
        /// <summary>
        /// Commits the transaction.
        /// </summary>
        public void CommitTransaction()
        {
            Connection.Transaction.Commit();
        }

        /// <summary>
        /// Rollbacks the transaction.
        /// </summary>
        public void RollbackTransaction()
        {
            Connection.Transaction.Rollback();
        }
    }


}
