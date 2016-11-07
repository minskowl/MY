using System;
using System.Linq;
using System.Reflection;
using Savchin.Core;

namespace Savchin.Comparer
{
    internal class ObjectComparer : ComparerBase
    {
        private readonly PropertyInfo[] _properties;

        /// <summary>
        /// Initializes a new instance of the <see cref="ObjectComparer"/> class.
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        /// <param name="type">The type.</param>
        public ObjectComparer(Configuration configuration, Type type)
            : base(configuration, type)
        {
            _properties = type.GetProperties().Where(IsComparable).ToArray();
        }

        /// <summary>
        /// Compares the specified source.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="destination">The destination.</param>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        public override CompareResultBase Compare(object source, object destination, string name)
        {
            var res = new ObjectResult(Type, name, source, destination);
            if (Configuration.TrackDifference)
                res.Results = new ResultCollection();

            if (res.IsComparable)
                foreach (var property in _properties)
                {
                    if (CompareProperty(property, source, destination, res) == false)
                    {
                        res.IsEquals = false;
                        if (!Configuration.TrackDifference) break;
                    }

                }
            return res;
        }

        private bool? CompareProperty(PropertyInfo property, object sourceObject, object destinationObject, ObjectResult compareResult)
        {
            var source = property.GetValue(sourceObject, null);
            var destination = property.GetValue(destinationObject, null);
            var propertyResult = Configuration.GetComparer(property).Compare(source, destination, property.Name);

            if (Configuration.TrackDifference)
                compareResult.Results.Add(property.Name, propertyResult);
            return propertyResult.IsEquals;
        }




        private bool IsComparable(PropertyInfo property)
        {
            return Configuration.CompareMode == CompareMode.Marked ? property.HasAttribute(true, typeof(CompareAttribute)) : !property.HasAttribute(true, typeof(IgnoreCompareAttribute));
        }
    }
}