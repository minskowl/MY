namespace Savchin.Core
{
    /// <summary>
    /// ICopiable
    /// </summary>
    /// <typeparam name="T">Type of object</typeparam>
    public interface ICopiable<in T>
    {
        /// <summary>
        /// Copies state to detination object.
        /// </summary>
        /// <param name="destination">The destination.</param>
        void Copy(T destination);
    }

    /// <summary>
    /// ICopiable
    /// </summary>
    public interface ICopiable
    {
        /// <summary>
        /// Copies state to detination object.
        /// </summary>
        /// <param name="destination">The destination.</param>
        void Copy(object destination);
    }
}
