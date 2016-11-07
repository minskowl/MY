using System.Collections;
using System.Collections.Generic;

namespace Savchin.Forms.ListView
{
    /// <summary>
    /// A simple-minded implementation of a Dictionary that can handle null as a key. 
    /// </summary>
    /// <typeparam name="TKey">The type of the dictionary key</typeparam>
    /// <typeparam name="TValue">The type of the values to be stored</typeparam>
    /// <remarks>This is not a full implementation and is only meant to handle
    /// collecting groups by their keys, since groups can have null as a key value.</remarks>
    internal class NullableDictionary<TKey, TValue> : Dictionary<TKey, TValue>
    {
        private bool hasNullKey;
        private TValue nullValue;

        new public TValue this[TKey key]
        {
            get
            {
                if (key == null) {
                    if (hasNullKey)
                        return nullValue;
                    else 
                        throw new KeyNotFoundException();
                } else
                    return base[key];
            }
            set
            {
                if (key == null) {
                    this.hasNullKey = true;
                    this.nullValue = value;
                } else
                    base[key] = value;
            }
        }

        new public bool ContainsKey(TKey key)
        {
            if (key == null)
                return this.hasNullKey;
            else
                return base.ContainsKey(key);
        }

        new public IList Keys
        {
            get {
                ArrayList list = new ArrayList(base.Keys);
                if (this.hasNullKey)
                    list.Add(null);
                return list;
            }
        }
    }
}