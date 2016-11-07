using System.Collections.Generic;
using System.Collections;

namespace Savchin.Comparer
{
    public  class ResultCollection : IEnumerable<KeyValuePair<string, CompareResultBase>>
    {
        #region Properties
        private bool _isEquals = true;
        private readonly Dictionary<string, CompareResultBase> _storage = new Dictionary<string, CompareResultBase>();

        /// <summary>
        /// Gets the keys.
        /// </summary>
        /// <value>The keys.</value>
        public ICollection<string> Keys
        {
            get { return _storage.Keys; }
        }
        /// <summary>
        /// Gets the values.
        /// </summary>
        /// <value>The values.</value>
        public ICollection<CompareResultBase> Values
        {
            get { return _storage.Values; }
        }
        /// <summary>
        /// Gets a value indicating whether this instance is equals.
        /// </summary>
        /// <value><c>true</c> if this instance is equals; otherwise, <c>false</c>.</value>
        public bool IsEquals
        {
            get { return _isEquals; }
        }


        /// <summary>
        /// Gets the <see cref="CompareResultBase"/> with the specified key.
        /// </summary>
        public CompareResultBase this[string key]
        {
            get
            {
                return _storage[key];
            }
        }
        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="ResultCollection"/> class.
        /// </summary>
        internal ResultCollection()
        {
        }

        /// <summary>
        /// Adds the specified key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="result">The result.</param>
        internal void Add(string key, CompareResultBase result)
        {
            _storage.Add(key, result);

            if (!result.IsEquals)
                _isEquals = false;
        }
        
        /// <summary>
        /// Uns the sync all.
        /// </summary>
        public void UnSyncAll()
        {
            foreach (var o in _storage.Values)
            {
                o.IsSync = false;
            }
        }   

        /// <summary>
        /// Uns the sync.
        /// </summary>
        /// <param name="keys">The keys.</param>
        public void UnSync(IEnumerable<KeyMatcher> keys)
        {
            foreach (var matcher in keys)
            {
                string key = matcher.Key;
                if (_storage.ContainsKey(key))
                    _storage[key].IsSync=false;
            }
        }
        /// <summary>
        /// Syncs all.
        /// </summary>
        public void SyncAll()
        {
            foreach (var o in _storage.Values)
            {
                o.SetSync();
            }
        }

        /// <summary>
        /// Syncs the specified keys.
        /// </summary>
        /// <param name="keys">The keys.</param>
        public void Sync(IEnumerable<KeyMatcher> keys)
        {
            foreach (var matcher in keys)
            {
                string key = matcher.Key;
                if (_storage.ContainsKey(key))
                    _storage[key].SetSync();
            }
        }


        /// <summary>
        /// Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.Collections.Generic.IEnumerator`1"></see> that can be used to iterate through the collection.
        /// </returns>
        IEnumerator<KeyValuePair<string, CompareResultBase>> IEnumerable<KeyValuePair<string, CompareResultBase>>.GetEnumerator()
        {
            return _storage.GetEnumerator();
        }

        ///<summary>
        ///Returns an enumerator that iterates through a collection.
        ///</summary>
        ///
        ///<returns>
        ///An <see cref="T:System.Collections.IEnumerator"></see> object that can be used to iterate through the collection.
        ///</returns>
        ///<filterpriority>2</filterpriority>
        public IEnumerator GetEnumerator()
        {
            return _storage.GetEnumerator();
        }


    }
}
