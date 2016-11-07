using System;

namespace Savchin.Comparer
{
    public class PrimitiveResult : CompareResultBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PrimitiveResult"/> class.
        /// </summary>
        /// <param name="objectType">Type of the object.</param>
        /// <param name="name">The name.</param>
        /// <param name="source">The source.</param>
        /// <param name="destination">The destination.</param>
        public PrimitiveResult(Type objectType, string name, object source, object destination) : base(objectType, name, source, destination)
        {

        }

        /// <summary>
        /// Syncs this instance.
        /// </summary>
        public override void Sync()
        {
            var propertyInfo = ObjectType.GetProperty(Name);
            if (propertyInfo!=null && propertyInfo.CanWrite)
            {
                var newValue = propertyInfo.GetValue(Source, null);
                propertyInfo.SetValue(Destination, newValue, null);
            }
        }
    }
}
