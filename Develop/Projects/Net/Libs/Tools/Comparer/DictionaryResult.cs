using System;
using System.Collections.Generic;
using System.Reflection;

namespace Savchin.Comparer
{


    public class DictionaryResult : CompareResultBase
    {
        private SortedList<string, DictionaryEntryResult> _result ;
        private readonly MethodInfo _mItemGet;
        private readonly MethodInfo _mRemove;


        /// <summary>
        /// Gets the <see cref="DictionaryEntryResult"/> with the specified key.
        /// </summary>
        /// <value></value>
        public DictionaryEntryResult this[string key]
        {
            get
            {
                return _result[key];
            }
        }

        /// <summary>
        /// Gets the keys.
        /// </summary>
        /// <value>The keys.</value>
        public IList<string> Keys
        {
            get
            {
                return _result.Keys;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DictionaryResult"/> class.
        /// </summary>
        /// <param name="objectType">Type of the object.</param>
        /// <param name="name">The name.</param>
        /// <param name="source">The source.</param>
        /// <param name="destination">The destination.</param>
        internal DictionaryResult(Type objectType, string name, Object source, Object destination)
            : base(objectType, name, source, destination)
        {

           _mItemGet = ObjectType.GetMethod("get_Item", new[] { typeof(string) });
            _mRemove = ObjectType.GetMethod("Remove", new[] { typeof(string) });
        }



        /// <summary>
        /// Adds the specified key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        internal void Add(string key, DictionaryEntryResult value)
        {
            if (_result == null)
                _result = new SortedList<string, DictionaryEntryResult>();

            _result.Add(key, value);
        }

        /// <summary>
        /// Syncs this instance.
        /// </summary>
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
            switch (pair.Value.ResultType)
            {
                case ResultType.Equal:
                    break;

                case ResultType.New:
                    var sourceValue = _mItemGet.Invoke(Source, new[] { pair.Key });
                    var addMethod = ObjectType.GetMethod("Add", new[] { typeof(string), sourceValue.GetType() });
                    addMethod.Invoke(Destination, new[] { pair.Key, sourceValue });
                    break;
                case ResultType.NotEqual:
                    pair.Value.ObjectResult.Sync();
                    break;
                case ResultType.Delete:
                    _mRemove.Invoke(Destination, new[] { pair.Key });
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

    }


    public class DictionaryEntryResult
    {

        /// <summary>
        /// Gets or sets the type of the result.
        /// </summary>
        /// <value>The type of the result.</value>
        public ResultType ResultType { get; set; }
        /// <summary>
        /// Gets or sets the object result.
        /// </summary>
        /// <value>The object result.</value>
        public CompareResultBase ObjectResult { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="DictionaryEntryResult"/> class.
        /// </summary>
        /// <param name="resultType">Type of the result.</param>
        public DictionaryEntryResult(ResultType resultType)
        {
            ResultType = resultType;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DictionaryEntryResult"/> class.
        /// </summary>
        /// <param name="resultType">Type of the result.</param>
        /// <param name="result">The result.</param>
        public DictionaryEntryResult(ResultType resultType, CompareResultBase result)
        {
            ResultType = resultType;
            ObjectResult = result;
        }
    }
}
