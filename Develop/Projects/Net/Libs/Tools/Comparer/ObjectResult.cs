using System;
using System.Linq;

namespace Savchin.Comparer
{
    public enum CompareMode
    {
        All,
        Marked
    }

    public class ObjectResult : CompareResultBase
    {

        #region Properties

        /// <summary>
        /// Gets the objects.
        /// </summary>
        /// <value>The objects.</value>
        public ResultCollection Results { get; internal set; }


        #endregion
        /// <summary>
        /// Initializes a new instance of the <see cref="ObjectResult"/> class.
        /// </summary>
        /// <param name="objectType">Type of the object.</param>
        /// <param name="name">The name.</param>
        /// <param name="source">The source.</param>
        /// <param name="destination">The destination.</param>
        internal ObjectResult(Type objectType, string name, object source, object destination)
            : base(objectType, name, source, destination)
        {
      
        }



        /// <summary>
        /// Syncs this instance.
        /// </summary>
        public override void Sync()
        {
            foreach (var result in Results.Select(e => e.Value).Where(e => !e.IsSync))
            {
                result.Sync();
            }
        }

        

    }
}
