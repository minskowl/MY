namespace Savchin.Threading.DataStructures.LockFree
{
    /// <summary>
    /// Defines a factory interface to be implemented by classes
    /// that creates new poolable objects
    /// </summary>
    public abstract class PoolableObjectFactory
    {
        #region Public Abstract Methods
        /// <summary>
        /// Create a new instance of a poolable object
        /// </summary>
        /// <returns>Instance of user defined PoolableObject derived type</returns>
        public abstract PoolableObject CreatePoolableObject();
        #endregion
    }
}