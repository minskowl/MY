namespace Savchin.Comparer
{
    internal class PrimitiveComparer : ComparerBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PrimitiveComparer"/> class.
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        internal PrimitiveComparer(Configuration configuration)
            : base(configuration, null)
        {

        }

        public override CompareResultBase Compare(object source, object destination, string name)
        {
            return new PrimitiveResult(Type, name, source, destination) { IsEquals = Equals(source, destination) };
        }
    }
}