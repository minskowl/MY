using System;

namespace Savchin.Comparer
{
    abstract class ComparerBase
    {
        public Type Type { get;  set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ComparerBase"/> class.
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        /// <param name="type">The type.</param>
        protected ComparerBase(Configuration configuration, Type type)
        {
            Type = type;
            Configuration = configuration;
        }


        public Configuration Configuration { get; private set; }

        /// <summary>
        /// Compares the specified source.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="destination">The destination.</param>
        /// <returns></returns>
        public CompareResultBase Compare(object source, object destination)
        {
            return this.Compare(source, destination, string.Empty);
        }

        public abstract CompareResultBase Compare(object source, object destination, string propertyName);
    }
}
