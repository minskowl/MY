namespace Savchin.Threading.DataStructures.LockFree
{
    /// <summary>
    /// Poolable object type. One can define new poolable types by deriving
    /// from this class.
    /// </summary>
    public class PoolableObject
    {
        #region Public Constructors
        /// <summary>
        /// Default constructor. Poolable types need to have a no-argument 
        /// constructor for the poolable object factory to easily create 
        /// new poolable objects when required.
        /// </summary>
        public PoolableObject()
        {
            Initialize();
        }
        #endregion

        #region Public Virtual Methods
        /// <summary>
        /// Called when a poolable object is being returned from the pool
        /// to caller.
        /// </summary>
        public virtual void Initialize()
        {
            LinkedObject = null;
        }
        /// <summary>
        /// Called when a poolable object is being returned back to the pool.
        /// </summary>
        public virtual void UnInitialize()
        {
            LinkedObject = null;
            if (++_val == long.MaxValue) _val = 0;
        }
        #endregion

        #region Internal Data Members
#if NET_11
        internal object LinkedObject;
#else
        internal volatile PoolableObject LinkedObject;
#endif
        internal long _val = 0;
        #endregion
    }
}