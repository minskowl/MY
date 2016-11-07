using System;
using System.Collections.Generic;
using System.Reflection;
using Savchin.Core;

namespace Savchin.Comparer
{
    public class Configuration
    {
        public bool TrackDifference { get; set; }
        public CompareMode CompareMode { get; set; }
        public int LevelLimit { get; set; }

        private readonly Dictionary<Type, ComparerBase> _comparers;
        private readonly PrimitiveComparer _primitiveComparer;

        public Configuration()
        {
            _comparers = new Dictionary<Type, ComparerBase>();
            _primitiveComparer = new PrimitiveComparer(this);
        }

        /// <summary>
        /// Compares the specified type.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="source">The source.</param>
        /// <param name="destination">The destination.</param>
        /// <returns></returns>
        public CompareResultBase Compare(Type type, object source, object destination)
        {
            return GetComparer(type).Compare(source, destination, null);
        }

        internal ComparerBase GetComparer(PropertyInfo property)
        {
            var type = GetObjectTypeFromType(property);
            return GetComparer(type, property.PropertyType, property);
        }

        internal ComparerBase GetComparer(Type propertyType)
        { 
            var type = GetObjectTypeFromType(propertyType);
            return GetComparer(type, propertyType, null);
        }

        private ComparerBase GetComparer(PropertyType type, Type key, PropertyInfo property)
        {
            ComparerBase res = null;
            switch (type)
            {
                case PropertyType.Primitive:
                    _primitiveComparer.Type = key;
                    res = _primitiveComparer;
                    break;
                case PropertyType.Dictionary:
                    if (!_comparers.TryGetValue(key, out res))
                    {
                        res = new DictionaryComparer(this, key);
                        _comparers[key] = res;
                    }
                    break;
                case PropertyType.KeyEnumerable:
                    if (property != null)
                        if (!_comparers.TryGetValue(key, out res))
                        {
                            res = new KeyEnumerableComparer(this, key, property);
                            _comparers[key] = res;
                        }
                    break;
                case PropertyType.Object:
                    if (!_comparers.TryGetValue(key, out res))
                    {
                        res = new ObjectComparer(this, key);
                        _comparers[key] = res;
                    }
                    break;
                default:
                    throw new ArgumentOutOfRangeException();

            }
            return res;
        }

        private enum PropertyType
        {
            Primitive,
            Dictionary,
            KeyEnumerable,
            Object
        }
        private PropertyType GetObjectTypeFromType(Type type)
        {
            if (type.IsPrimitive || type.IsEnum || type.Namespace == "System")
                return PropertyType.Primitive;

            if (type.GetInterface("IDictionary") != null ||
                type.GetInterface("IDictionary`1") != null ||
                type.GetInterface("IDictionary`2") != null)
                return PropertyType.Dictionary;


            return PropertyType.Object;
        }
        private PropertyType GetObjectTypeFromType(PropertyInfo property)
        {
            var type = property.PropertyType;
            if (type.GetInterface("IEnumerable") != null && property.GetAttribute<CompareKeyAttribute>() != null)
                return PropertyType.KeyEnumerable;

            return GetObjectTypeFromType(type);
        }
    }
}
