using System;
using System.Collections;
using System.Reflection;
using Savchin.Core;

namespace Savchin.Comparer
{
    class KeyEnumerableComparer : ComparerBase
    {
        private PropertyInfo _prKey;
        private Type _dataType;
        private readonly string _keyName;
        public KeyEnumerableComparer(Configuration configuration, Type type, PropertyInfo property)
            : base(configuration, type)
        {
            _keyName = property.GetAttribute<CompareKeyAttribute>().KeyPropertyName;


        }
        public override CompareResultBase Compare(object source, object destination, string propertyName)
        {
            var res = new KeyEnumerableResult(Type, propertyName, source, destination);
            if (!res.IsComparable)
                return res;

            Hashtable sourceMap = ToMap((IEnumerable)source);
            Hashtable destionationMap = ToMap((IEnumerable)destination);
         

            var sourceIsEmpty = sourceMap.Count == 0;
            var destionationIsEmpty = destionationMap.Count == 0;

            if (sourceIsEmpty && destionationIsEmpty)
            {
                res.IsEquals = true;
                return res;
            }

            if (sourceIsEmpty || destionationIsEmpty)
            {
                res.IsEquals = false;
                return res;
            }


            CompareMap(sourceMap, destionationMap,res);
            return res;
        }

        private void CompareMap(Hashtable sourceMap, Hashtable destionationMap, KeyEnumerableResult res)
        {

            foreach (var key in sourceMap.Keys)
            {
                var d = destionationMap[key];
                var s = sourceMap[key];

                if (d == null)
                {

                    if (Configuration.TrackDifference)
                    {
                        var objectResult = Configuration.GetComparer(_dataType).Compare(s, null, null);
                        var dictionaryEntryResult = new DictionaryEntryResult(ResultType.New, objectResult);
                        res.Add(key.ToString(), dictionaryEntryResult);
                    }
                    res.IsEquals = false;
                    if (!Configuration.TrackDifference) return;
                }
                else
                {
                    var objectResult = Configuration.GetComparer(_dataType).Compare(s, d);
                    if (!objectResult.IsEquals)
                    {
                        res.IsEquals = false;
                        if (!Configuration.TrackDifference) return;
                    }

                    if (Configuration.TrackDifference)
                    {
                        var dictionaryEntryResult = new DictionaryEntryResult(objectResult.IsEquals ? ResultType.Equal : ResultType.NotEqual, objectResult);
                        res.Add(key.ToString(), dictionaryEntryResult);
                    }
                }



            }

            // Exists  Only in Destination 
            foreach (var key in destionationMap.Keys)
            {
                if (sourceMap.ContainsKey(key)) continue;

                if (Configuration.TrackDifference)
                {
                    var d = destionationMap[key];
                    var objectResult = Configuration.GetComparer(_dataType).Compare(null, d);
                    var dictionaryEntryResult = new DictionaryEntryResult(ResultType.Delete, objectResult);
                    res.Add(key.ToString(), dictionaryEntryResult);
                }

                res.IsEquals = false;
                if (!Configuration.TrackDifference) return;
            }


        }


        private Hashtable ToMap(IEnumerable data)
        {
            var map = new Hashtable();
            foreach (var o in data)
            {
                if (_prKey == null)//Initialization
                {
                    _dataType = o.GetType();
                    _prKey = _dataType.GetProperty(_keyName);
                }

                var key = _prKey.GetValue(o, null);
                map[key] = o;
            }
            return map;
        }
   
    }
}
