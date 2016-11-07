using System;
using System.Collections;
using System.Reflection;

namespace Savchin.Comparer
{
    internal class DictionaryComparer : ComparerBase
    {
        private MethodInfo _mItemGet;
     
        private MethodInfo _mContains;
        private PropertyInfo _prKeys;

        public DictionaryComparer(Configuration configuration,  Type type) : base(configuration,  type)
        {
            _prKeys = Type.GetProperty("Keys");
            _mContains = Type.GetMethod("ContainsKey");

            _mItemGet = Type.GetMethod("get_Item", new[] { typeof(string) });
        }

        public override CompareResultBase Compare(object source, object destionation, string name)
        {
            var res = new DictionaryResult(Type, name, source, destionation);
            if (!res.IsComparable) return res;

            var sourceKeys = (IEnumerable)_prKeys.GetValue(source, null);
            var destinationsKeys = (IEnumerable)_prKeys.GetValue(destionation, null);


            var param = new object[1];

            CompareResultBase objectResult;
            foreach (var key in sourceKeys)
            {
                param[0] = key;
                var sourceValue = _mItemGet.Invoke(source, param);

                if ((bool)_mContains.Invoke(destionation, param))// Exists in Source and Destination
                {
                    var destionationValue = _mItemGet.Invoke(destionation, param);

                    objectResult = Configuration.GetComparer(destionationValue.GetType()).Compare(sourceValue, destionationValue);


                    if (!objectResult.IsEquals)
                    {
                        res.IsEquals = false;
                        if (!Configuration.TrackDifference) return res;
                    }

                    if (Configuration.TrackDifference)
                    {
                        var dictionaryEntryResult = new DictionaryEntryResult(objectResult.IsEquals ? ResultType.Equal : ResultType.NotEqual, objectResult);
                        res.Add(key.ToString(), dictionaryEntryResult);
                    }



                }
                else// Exists  Only in Source 
                {
                    if (Configuration.TrackDifference)
                    {
                        objectResult = Configuration.GetComparer(sourceValue.GetType()).Compare(sourceValue, null);
                        var dictionaryEntryResult = new DictionaryEntryResult(ResultType.New, objectResult);
                        res.Add(key.ToString(), dictionaryEntryResult);
                    }

                    res.IsEquals = false;
                    if (!Configuration.TrackDifference) return res;
                }

            }
            // Exists  Only in Destination 
            foreach (var key in destinationsKeys)
            {
                param[0] = key;
                if ((bool)_mContains.Invoke(source, param)) continue;

                if (Configuration.TrackDifference)
                {
                    var destionationValue = _mItemGet.Invoke(destionation, param);


                    objectResult = Configuration.GetComparer(destionationValue.GetType()).Compare(null, destionationValue);

                    var dictionaryEntryResult = new DictionaryEntryResult(ResultType.Delete, objectResult);
                    res.Add(key.ToString(), dictionaryEntryResult);
                }

                res.IsEquals = false;
                if (!Configuration.TrackDifference) return res;
            }

            return res;
        }
    }
}