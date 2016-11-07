using System;
using System.Collections;
using System.Reflection;

namespace Savchin.CommandLine
{
    /// <summary>
    /// CollectionSwitch
    /// </summary>
    class CollectionSwitch : BaseSwitch
    {
        private IList _collection;
        /// <summary>
        /// Initializes a new instance of the <see cref="CollectionSwitch"/> class.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <param name="property">The property.</param>
        /// <param name="o">The o.</param>
        public CollectionSwitch(SwitchAttribute data, PropertyInfo property, object o)
            : base(data, property, o)
        {
            _collection = (IList)property.GetValue(o, null);
            //TODO: Exctract generic type
            //property.PropertyType.g
        }

        protected override void SetValue(string value)
        {
            _collection.Add(value);
        }

        public override void Validate()
        {
            if (_collection.Count == 0 && Data.Required)
                throw new InvalidOperationException(string.Format("Switch {0} is required.", Data.Names[0]));
        }
    }
}
