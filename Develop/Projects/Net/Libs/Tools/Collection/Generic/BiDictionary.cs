using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;


namespace Savchin.Collection.Generic
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T1">The type of the 1.</typeparam>
    /// <typeparam name="T2">The type of the 2.</typeparam>
    public class BiDictionary<T1, T2> : IDictionary<T1,T2>
    {
        readonly Dictionary<T1, T2> dictionary1 = new Dictionary<T1, T2>();
        readonly Dictionary<T2, T1> dictionary2 = new Dictionary<T2, T1>();

        public bool ContainsKey(T1 key)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Adds the specified value1.
        /// </summary>
        /// <param name="value1">The value1.</param>
        /// <param name="value2">The value2.</param>
        public void Add(T1 value1, T2 value2)
        {
            dictionary1.Add(value1, value2);
            dictionary2.Add(value2, value1);
        }

        /// <summary>
        /// Removes the element with the specified key from the <see cref="T:System.Collections.Generic.IDictionary`2"/>.
        /// </summary>
        /// <param name="key">The key of the element to remove.</param>
        /// <returns>
        /// true if the element is successfully removed; otherwise, false.  This method also returns false if <paramref name="key"/> was not found in the original <see cref="T:System.Collections.Generic.IDictionary`2"/>.
        /// </returns>
        /// <exception cref="T:System.ArgumentNullException">
        /// 	<paramref name="key"/> is null.</exception>
        /// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Collections.Generic.IDictionary`2"/> is read-only.</exception>
        public bool Remove(T1 key)
        {
            if(dictionary1.ContainsKey(key))
            {
                dictionary2.Remove(dictionary1[key]);
                dictionary1.Remove(key);
            }
            return true;
        }

        /// <summary>
        /// Gets the value associated with the specified key.
        /// </summary>
        /// <param name="key">The key whose value to get.</param>
        /// <param name="value">When this method returns, the value associated with the specified key, if the key is found; otherwise, the default value for the type of the <paramref name="value"/> parameter. This parameter is passed uninitialized.</param>
        /// <returns>
        /// true if the object that implements <see cref="T:System.Collections.Generic.IDictionary`2"/> contains an element with the specified key; otherwise, false.
        /// </returns>
        /// <exception cref="T:System.ArgumentNullException">
        /// 	<paramref name="key"/> is null.</exception>
        public bool TryGetValue(T1 key, out T2 value)
        {
            return dictionary1.TryGetValue(key, out value);
        }
        /// <summary>
        /// Tries the get value.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public bool TryGetValue(T2 key, out T1 value)
        {
            return dictionary2.TryGetValue(key, out value);
        }
        /// <summary>
        /// Gets or sets the <see cref="T2"/> with the specified key.
        /// </summary>
        /// <value></value>
        public T2 this[T1 key]
        {
            get { return dictionary1[key]; }
            set { dictionary1[key]=value; }
        }

        /// <summary>
        /// Gets an <see cref="T:System.Collections.Generic.ICollection`1"/> containing the keys of the <see cref="T:System.Collections.Generic.IDictionary`2"/>.
        /// </summary>
        /// <value></value>
        /// <returns>An <see cref="T:System.Collections.Generic.ICollection`1"/> containing the keys of the object that implements <see cref="T:System.Collections.Generic.IDictionary`2"/>.</returns>
        public ICollection<T1> Keys
        {
            get { return dictionary1.Keys; }
        }

        /// <summary>
        /// Gets an <see cref="T:System.Collections.Generic.ICollection`1"/> containing the values in the <see cref="T:System.Collections.Generic.IDictionary`2"/>.
        /// </summary>
        /// <value></value>
        /// <returns>An <see cref="T:System.Collections.Generic.ICollection`1"/> containing the values in the object that implements <see cref="T:System.Collections.Generic.IDictionary`2"/>.</returns>
        public ICollection<T2> Values
        {
            get { return dictionary1.Values; }
        }

        /// <summary>
        /// Gets or sets the <see cref="T1"/> with the specified key.
        /// </summary>
        /// <value></value>
        public T1 this[T2 key]
        {
            get { return dictionary2[key]; }
            set { dictionary2[key] = value; }
        }



        /// <summary>
        /// Removes all items from the <see cref="T:System.Collections.Generic.ICollection`1"/>.
        /// </summary>
        /// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Collections.Generic.ICollection`1"/> is read-only. </exception>
        public void Clear()
        {
            dictionary1.Clear();
            dictionary1.Clear();
        }



        /// <summary>
        /// Gets the number of elements contained in the <see cref="T:System.Collections.Generic.ICollection`1"/>.
        /// </summary>
        /// <value></value>
        /// <returns>The number of elements contained in the <see cref="T:System.Collections.Generic.ICollection`1"/>.</returns>
        public int Count
        {
            get { return dictionary1.Count; }
        }

        /// <summary>
        /// Gets a value indicating whether the <see cref="T:System.Collections.Generic.ICollection`1"/> is read-only.
        /// </summary>
        /// <value></value>
        /// <returns>true if the <see cref="T:System.Collections.Generic.ICollection`1"/> is read-only; otherwise, false.</returns>
        public bool IsReadOnly
        {
            get { return false; }
        }

        IEnumerator<KeyValuePair<T1, T2>> IEnumerable<KeyValuePair<T1, T2>>.GetEnumerator()
        {
            throw new NotImplementedException();
        }

        public IEnumerator GetEnumerator()
        {
            return ((IEnumerable<KeyValuePair<T1, T2>>) this).GetEnumerator();
        }

        void ICollection<KeyValuePair<T1, T2>>.Add(KeyValuePair<T1, T2> item)
        {
            throw new NotImplementedException();
        }

        bool ICollection<KeyValuePair<T1, T2>>.Contains(KeyValuePair<T1, T2> item)
        {
            throw new NotImplementedException();
        }

        void ICollection<KeyValuePair<T1, T2>>.CopyTo(KeyValuePair<T1, T2>[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        bool ICollection<KeyValuePair<T1, T2>>.Remove(KeyValuePair<T1, T2> item)
        {
            throw new NotImplementedException();
        }
    }
}