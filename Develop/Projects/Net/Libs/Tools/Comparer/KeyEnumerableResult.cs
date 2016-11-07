using System;
using System.Collections.Generic;

namespace Savchin.Comparer
{
    public class KeyEnumerableResult : CompareResultBase
    {
        private  SortedList<string, DictionaryEntryResult> _result;
        public DictionaryEntryResult this[string key]
        {
            get
            {
                return _result[key];
            }
        }

        public KeyEnumerableResult(Type objectType, string name, object source, object destination) : base(objectType, name, source, destination)
        {
        
        }

        /// <summary>
        /// Adds the specified key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        internal void Add(string key, DictionaryEntryResult value)
        {
            if(_result==null)
                _result = new SortedList<string, DictionaryEntryResult>();

            _result.Add(key, value);
        }

        public override void Sync()
        {
            foreach (var pair in _result)
            {
                if (!pair.Value.ObjectResult.IsSync) continue;

                Syncronyze(pair);
            }
        }
        private void Syncronyze(KeyValuePair<string, DictionaryEntryResult> pair)
        {
            throw new NotImplementedException();
            //switch (pair.Value.ResultType)
            //{
            //    case ResultType.Equal:
            //        break;

            //    case ResultType.New:
            //        var sourceValue = _mItemGet.Invoke(Source, new[] { pair.Key });
            //        var addMethod = ObjectType.GetMethod("Add", new[] { typeof(string), sourceValue.GetType() });
            //        addMethod.Invoke(Destination, new[] { pair.Key, sourceValue });
            //        break;
            //    case ResultType.NotEqual:
            //        pair.Value.ObjectResult.Sync();
            //        break;
            //    case ResultType.Delete:
            //        _mRemove.Invoke(Destination, new[] { pair.Key });
            //        break;
            //    default:
            //        throw new ArgumentOutOfRangeException();
            //}
        }

    }
}
