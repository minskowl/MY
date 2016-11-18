using System.Collections.Generic;

namespace Savchin.UI.Interfaces
{
    public interface ICompositeObject
    {
        /// <summary>
        /// Adds the child.
        /// </summary>
        /// <param name="children">The children.</param>
        void AddChild(object children);

        /// <summary>
        /// Removes the child.
        /// </summary>
        /// <param name="children">The children.</param>
        void RemoveChild(object children);

        /// <summary>
        /// Gets the child.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        IEnumerable<T> GetChild<T>();
    }
}