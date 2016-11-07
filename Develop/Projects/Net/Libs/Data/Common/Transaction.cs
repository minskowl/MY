using System;
using System.Data;

namespace Savchin.Data.Common
{
    public class Transaction : IDisposable 
    {
        private IDbConnection _connection = null; 
        
        #region Properties
        private IDbTransaction _transaction = null;
        /// <summary>
        /// Gets or sets the real transaction.
        /// </summary>
        /// <value>The real transaction.</value>
        internal IDbTransaction RealTransaction
        {
            get { return _transaction; }
            set { _transaction = value; }
        }


        private int _transactionLevel = 0;
        /// <summary>
        /// Gets or sets the transaction level.
        /// </summary>
        /// <value>The transaction level.</value>
        public int TransactionLevel
        {
            get { return _transactionLevel; }
            set { _transactionLevel = value; }
        } 
        
        /// <summary>
        /// Gets a value indicating whether this instance is begined.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is begined; otherwise, <c>false</c>.
        /// </value>
        public bool IsBegined
        {
            get { return _transaction != null; }
        }
        #endregion

        #region Constructor
            
        /// <summary>
        /// Initializes a new instance of the <see cref="Transaction"/> class.
        /// </summary>
        /// <param name="connection">The connection.</param>
        internal Transaction(IDbConnection connection)
        {
            _connection = connection;
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            if (RealTransaction != null)
            {
                TransactionLevel = 0;
                RealTransaction.Dispose();
                RealTransaction = null;
            }
        } 
        #endregion
        
        /// <summary>
        /// Begins this instance.
        /// </summary>
        /// <returns></returns>
        public bool Begin()
        {

            if (!IsBegined)
            {
                RealTransaction = _connection.BeginTransaction();
            }

            TransactionLevel++;
            return true;
        }

        /// <summary>
        /// Begins the specified il.
        /// </summary>
        /// <param name="il">The il.</param>
        /// <returns></returns>
        public bool Begin(IsolationLevel il)
        {
            if (!IsBegined)
            {
                RealTransaction = _connection.BeginTransaction(il);
            }

            TransactionLevel++;
            return true;
        }

        /// <summary>
        /// Rollbacks this instance.
        /// </summary>
        public void Rollback()
        {
            if (!IsBegined)
                return;

            RealTransaction.Rollback();
            TransactionLevel = 0;
            RealTransaction.Dispose();
            RealTransaction = null;

        }

        /// <summary>
        /// Commits this instance.
        /// </summary>
        /// <returns></returns>
        public bool Commit()
        {
            if (!IsBegined)
                return true;
            TransactionLevel--;

            if (TransactionLevel > 0)
                return true;


            RealTransaction.Commit();

            RealTransaction.Dispose();
            RealTransaction = null;

            return true;
        }


    }
}
