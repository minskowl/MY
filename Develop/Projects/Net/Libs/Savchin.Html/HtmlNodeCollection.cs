/*
 * Alexey A. Popov
 * Copyright (c) 2004, Alexey A. Popov
 * All rights reserved.
 *
 * Redistribution and use in source and binary forms, with or without modification, are
 * permitted provided that the following conditions are met:
 *
 * - Redistributions of source code must retain the above copyright notice, this list
 *   of conditions and the following disclaimer.
 *
 * - Redistributions in binary form must reproduce the above copyright notice, this list
 *   of conditions and the following disclaimer in the documentation and/or other materials
 *   provided with the distribution.
 *
 * - Neither the name of the Alexey A. Popov nor the names of its contributors may be used to
 *   endorse or promote products derived from this software without specific prior written
 *   permission.
 *
 * THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS &AS IS& AND ANY EXPRESS
 * OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY
 * AND FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT OWNER OR
 * CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL
 * DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE,
 * DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER
 * IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT
 * OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
 */

using System;
using System.Collections;

namespace Savchin.Html
{
	/// <summary>
	/// A strongly-typed collection of <see cref="HtmlNode"/> objects.
	/// </summary>
	[Serializable]
	public class HtmlNodeCollection : ICollection, IList, IEnumerable, ICloneable
	{
		private const int DEFAULT_CAPACITY = 16;

#region Implementation (data)
		private HtmlNode[] _array;

		private int _count = 0;

		[NonSerialized]
		private int _version = 0;
#endregion

#region Static Wrappers
		/// <summary>
		/// Creates a synchronized (thread-safe) wrapper for a
		/// <see cref="HtmlNodeCollection"/> instance.
		/// </summary>
		/// <returns>
		/// An <see cref="HtmlNodeCollection"/> wrapper that is synchronized (thread-safe).
		/// </returns>
        public static HtmlNodeCollection Synchronized(HtmlNodeCollection list)
        {
            if(list == null)
                throw new ArgumentNullException("list");
            return new SyncHtmlNodeCollection(list);
        }

		/// <summary>
		/// Creates a read-only wrapper for a
		/// <see cref="HtmlNodeCollection"/> instance.
		/// </summary>
		/// <returns>
		/// An <see cref="HtmlNodeCollection"/> wrapper that is read-only.
		/// </returns>
        public static HtmlNodeCollection ReadOnly(HtmlNodeCollection list)
        {
            if(list == null)
                throw new ArgumentNullException("list");
            return new ReadOnlyHtmlNodeCollection(list);
        }
#endregion

#region Construction
		/// <summary>
		/// Initializes a new instance of the <see cref="HtmlNodeCollection"/> class
		/// that is empty and has the default initial capacity.
		/// </summary>
		public HtmlNodeCollection()
		{
			_array = new HtmlNode[DEFAULT_CAPACITY];
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="HtmlNodeCollection"/> class
		/// that has the specified initial capacity.
		/// </summary>
		/// <param name="capacity">
		/// The number of elements that the new <see cref="HtmlNodeCollection"/> is initially capable of storing.
		///	</param>
		public HtmlNodeCollection(int capacity)
		{
			_array = new HtmlNode[capacity];
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="HtmlNodeCollection"/> class
		/// that contains elements copied from the specified <see cref="HtmlNodeCollection"/>.
		/// </summary>
		/// <param name="c">The <see cref="HtmlNodeCollection"/> whose elements are copied to the new collection.</param>
		public HtmlNodeCollection(HtmlNodeCollection c)
		{
			_array = new HtmlNode[c.Count];
			AddRange(c);
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="HtmlNodeCollection"/> class
		/// that contains elements copied from the specified <see cref="HtmlNode"/> array.
		/// </summary>
		/// <param name="a">The <see cref="HtmlNode"/> array whose elements are copied to the new list.</param>
		public HtmlNodeCollection(HtmlNode[] a)
		{
			_array = new HtmlNode[a.Length];
			AddRange(a);
		}

        protected enum Tag {
            Default
        }

        protected HtmlNodeCollection(Tag t)
        {
            _array = null;
        }
#endregion

#region Operations (type-safe ICollection)
		/// <summary>
		/// Gets the number of elements actually contained in the <see cref="HtmlNodeCollection"/>.
		/// </summary>
		public virtual int Count
		{
			get
			{
				return _count;
			}
		}

		/// <summary>
		/// Copies the entire <see cref="HtmlNodeCollection"/> to a one-dimensional
		/// <see cref="HtmlNode"/> array.
		/// </summary>
		/// <param name="array">The one-dimensional <see cref="HtmlNode"/> array to copy to.</param>
		public virtual void CopyTo(HtmlNode[] array)
		{
			this.CopyTo(array, 0);
		}

		/// <summary>
		/// Copies the entire <see cref="HtmlNodeCollection"/> to a one-dimensional
		/// <see cref="HtmlNode"/> array, starting at the specified index of the target array.
		/// </summary>
		/// <param name="array">The one-dimensional <see cref="HtmlNode"/> array to copy to.</param>
		/// <param name="start">The zero-based index in <paramref name="array"/> at which copying begins.</param>
		public virtual void CopyTo(HtmlNode[] array, int start)
		{
			if(_count > array.GetUpperBound(0) + 1 - start)
				throw new ArgumentException(RD.GetString("noRoom"));

			Array.Copy(_array, 0, array, start, _count);
		}

		/// <summary>
		/// Gets a value indicating whether access to the collection is synchronized (thread-safe).
		/// </summary>
		/// <returns>true if access to the ICollection is synchronized (thread-safe); otherwise, false.</returns>
        public virtual bool IsSynchronized
        {
            get
            {
            	return _array.IsSynchronized;
            }
        }

        /// <summary>
		/// Gets an object that can be used to synchronize access to the collection.
		/// </summary>
        public virtual object SyncRoot
        {
            get
            {
            	return _array.SyncRoot;
            }
        }
#endregion

#region Operations (type-safe IList)
		/// <summary>
		/// Gets or sets the <see cref="HtmlNode"/> at the specified index.
		/// </summary>
		/// <param name="index">The zero-based index of the element to get or set.</param>
		/// <exception cref="ArgumentOutOfRangeException">
		/// <para><paramref name="index"/> is less than zero</para>
		/// <para>-or-</para>
		/// <para><paramref name="index"/> is equal to or greater than <see cref="HtmlNodeCollection.Count"/>.</para>
		/// </exception>
		public virtual HtmlNode this[int index]
		{
			get
			{
				ValidateIndex(index); // throws
				return _array[index];
			}

			set
			{
				ValidateIndex(index); // throws
				++_version;
				_array[index] = value;
			}
		}

		/// <summary>
		/// Adds a <see cref="HtmlNode"/> to the end of the <see cref="HtmlNodeCollection"/>.
		/// </summary>
		/// <param name="item">The <see cref="HtmlNode"/> to be added to the end of the <see cref="HtmlNodeCollection"/>.</param>
		/// <returns>The index at which the value has been added.</returns>
		public virtual int Add(HtmlNode item)
		{
			if(_count == _array.Length)
				EnsureCapacity(_count + 1);

			_array[_count] = item;
			_version++;

			return _count++;
		}

		/// <summary>
		/// Removes all elements from the <see cref="HtmlNodeCollection"/>.
		/// </summary>
		public virtual void Clear()
		{
			++_version;
			_array = new HtmlNode[DEFAULT_CAPACITY];
			_count = 0;
		}

		/// <summary>
		/// Creates a shallow copy of the <see cref="HtmlNodeCollection"/>.
		/// </summary>
		public virtual object Clone()
		{
			HtmlNodeCollection newColl = new HtmlNodeCollection(_count);
			Array.Copy(_array, 0, newColl._array, 0, _count);
			newColl._count = _count;
			newColl._version = _version;

			return newColl;
		}

		/// <summary>
		/// Determines whether a given <see cref="HtmlNode"/> is in the
		/// <see cref="HtmlNodeCollection"/>.
		/// </summary>
		/// <param name="item">The <see cref="HtmlNode"/> to check for.</param>
		/// <returns><c>true</c> if <paramref name="item"/> is found in the
		/// <see cref="HtmlNodeCollection"/>; otherwise, <c>false</c>.</returns>
		public virtual bool Contains(HtmlNode item)
		{
			for(int i=0; i != _count; ++i)
				if(_array[i].Equals(item))
					return true;
			return false;
		}

		/// <summary>
		/// Returns the zero-based index of the first occurrence of a <see cref="HtmlNode"/>
		/// in the <see cref="HtmlNodeCollection"/>.
		/// </summary>
		/// <param name="item">The <see cref="HtmlNode"/> to locate in the <see cref="HtmlNodeCollection"/>.</param>
		/// <returns>
		/// The zero-based index of the first occurrence of <paramref name="item"/>
		/// in the entire <see cref="HtmlNodeCollection"/>, if found; otherwise, -1.
		///	</returns>
		public virtual int IndexOf(HtmlNode item)
		{
			for(int i=0; i != _count; ++i)
				if(_array[i].Equals(item))
					return i;
			return -1;
		}

		/// <summary>
		/// Inserts an element into the <see cref="HtmlNodeCollection"/> at the specified index.
		/// </summary>
		/// <param name="index">The zero-based index at which <paramref name="item"/> should be inserted.</param>
		/// <param name="item">The <see cref="HtmlNode"/> to insert.</param>
		/// <exception cref="ArgumentOutOfRangeException">
		/// <para><paramref name="index"/> is less than zero</para>
		/// <para>-or-</para>
		/// <para><paramref name="index"/> is equal to or greater than <see cref="HtmlNodeCollection.Count"/>.</para>
		/// </exception>
		public virtual void Insert(int index, HtmlNode item)
		{
			ValidateIndex(index, true); // throws

			if(_count == _array.Length)
				EnsureCapacity(_count + 1);

			if(index < _count)
			{
				Array.Copy(_array, index, _array, index + 1, _count - index);
			}

			_array[index] = item;
			_count++;
			_version++;
		}

		/// <summary>
		/// Removes the first occurrence of a specific <see cref="HtmlNode"/> from the <see cref="HtmlNodeCollection"/>.
		/// </summary>
		/// <param name="item">The <see cref="HtmlNode"/> to remove from the <see cref="HtmlNodeCollection"/>.</param>
		/// <exception cref="ArgumentException">
		/// The specified <see cref="HtmlNode"/> was not found in the <see cref="HtmlNodeCollection"/>.
		/// </exception>
		public virtual void Remove(HtmlNode item)
		{
			int i = IndexOf(item);
			if(i < 0)
				throw new ArgumentException(RD.GetString("noItem"));

			++_version;
			RemoveAt(i);
		}

		/// <summary>
		/// Removes the element at the specified index of the <see cref="HtmlNodeCollection"/>.
		/// </summary>
		/// <param name="index">The zero-based index of the element to remove.</param>
		/// <exception cref="ArgumentOutOfRangeException">
		/// <para><paramref name="index"/> is less than zero</para>
		/// <para>-or-</para>
		/// <para><paramref name="index"/> is equal to or greater than <see cref="HtmlNodeCollection.Count"/>.</para>
		/// </exception>
		public virtual void RemoveAt(int index)
		{
			ValidateIndex(index); // throws

			_count--;

			if(index < _count)
			{
				Array.Copy(_array, index + 1, _array, index, _count - index);
			}

			// We can't set the deleted entry equal to null, because it might be a value type.
			// Instead, we'll create an empty single-element array of the right type and copy it
			// over the entry we want to erase.
			HtmlNode[] temp = new HtmlNode[1];
			Array.Copy(temp, 0, _array, _count, 1);
			_version++;
		}

		/// <summary>
		/// Gets a value indicating whether the collection has a fixed size.
		/// </summary>
		/// <value>true if the collection has a fixed size; otherwise, false. The default is false</value>
        public virtual bool IsFixedSize
        {
            get
            {
            	return false;
            }
        }

		/// <summary>
		/// gets a value indicating whether the IList is read-only.
		/// </summary>
		/// <value>true if the collection is read-only; otherwise, false. The default is false</value>
        public virtual bool IsReadOnly
        {
            get
            {
            	return false;
            }
        }
#endregion

#region Operations (type-safe IEnumerable)
		/// <summary>
		/// Returns an enumerator that can iterate through the <see cref="HtmlNodeCollection"/>.
		/// </summary>
		/// <returns>An <see cref="Enumerator"/> for the entire <see cref="HtmlNodeCollection"/>.</returns>
		public virtual Enumerator GetEnumerator()
		{
			return new Enumerator(this);
		}
#endregion

#region Public helpers (just to mimic some nice features of ArrayList)
		/// <summary>
		/// Gets or sets the number of elements the <see cref="HtmlNodeCollection"/> can contain.
		/// </summary>
		public virtual int Capacity
		{
			get
			{
				return _array.Length;
			}

			set
			{
				if(value < _count)
					value = _count;

				if(value != _array.Length)
				{
					if(value > 0)
					{
						HtmlNode[] temp = new HtmlNode[value];
						Array.Copy(_array, temp, _count);
						_array = temp;
					}
					else
					{
						_array = new HtmlNode[DEFAULT_CAPACITY];
					}
				}
			}
		}

		/// <summary>
		/// Adds the elements of another <see cref="HtmlNodeCollection"/> to the current <see cref="HtmlNodeCollection"/>.
		/// </summary>
		/// <param name="x">The <see cref="HtmlNodeCollection"/> whose elements should be added to the end of the current <see cref="HtmlNodeCollection"/>.</param>
		/// <returns>The new <see cref="HtmlNodeCollection.Count"/> of the <see cref="HtmlNodeCollection"/>.</returns>
		public virtual int AddRange(HtmlNodeCollection x)
		{
			if(_count + x.Count >= _array.Length)
				EnsureCapacity(_count + x.Count);

			Array.Copy(x._array, 0, _array, _count, x.Count);
			_count += x.Count;
			_version++;

			return _count;
		}

		/// <summary>
		/// Adds the elements of a <see cref="HtmlNode"/> array to the current <see cref="HtmlNodeCollection"/>.
		/// </summary>
		/// <param name="x">The <see cref="HtmlNode"/> array whose elements should be added to the end of the <see cref="HtmlNodeCollection"/>.</param>
		/// <returns>The new <see cref="HtmlNodeCollection.Count"/> of the <see cref="HtmlNodeCollection"/>.</returns>
		public virtual int AddRange(HtmlNode[] x)
		{
			if(_count + x.Length >= _array.Length)
				EnsureCapacity(_count + x.Length);

			Array.Copy(x, 0, _array, _count, x.Length);
			_count += x.Length;
			_version++;

			return _count;
		}

		/// <summary>
		/// Sets the capacity to the actual number of elements.
		/// </summary>
		public virtual void TrimToSize()
		{
			this.Capacity = _count;
		}

#endregion

#region Implementation (helpers)

		/// <exception cref="ArgumentOutOfRangeException">
		/// <para><paramref name="index"/> is less than zero</para>
		/// <para>-or-</para>
		/// <para><paramref name="index"/> is equal to or greater than <see cref="HtmlNodeCollection.Count"/>.</para>
		/// </exception>
		private void ValidateIndex(int i)
		{
			ValidateIndex(i, false);
		}

		/// <exception cref="ArgumentOutOfRangeException">
		/// <para><paramref name="index"/> is less than zero</para>
		/// <para>-or-</para>
		/// <para><paramref name="index"/> is equal to or greater than <see cref="HtmlNodeCollection.Count"/>.</para>
		/// </exception>
		private void ValidateIndex(int index, bool allowEqualEnd)
		{
			int max = (allowEqualEnd)?(_count):(_count-1);
			if(index < 0 || index > max)
				throw new ArgumentOutOfRangeException("index", (object)index, RD.GetString("outOfRange"));
		}

		private void EnsureCapacity(int min)
		{
			int newCapacity = ((_array.Length == 0) ? DEFAULT_CAPACITY : _array.Length * 2);
			if(newCapacity < min)
				newCapacity = min;

			this.Capacity = newCapacity;
		}

#endregion

#region Implementation (ICollection)
		void ICollection.CopyTo(Array array, int start)
		{
			Array.Copy(_array, 0, array, start, _count);
		}
#endregion

#region Implementation (IList)
		object IList.this[int i]
		{
			get
			{
				return (object)this[i];
			}

			set
			{
				this[i] = (HtmlNode)value;
			}
		}

		int IList.Add(object x)
		{
			return this.Add((HtmlNode)x);
		}

    	bool IList.Contains(object x)
		{
			return this.Contains((HtmlNode)x);
		}

		int IList.IndexOf(object x)
		{
			return this.IndexOf((HtmlNode)x);
		}

		void IList.Insert(int pos, object x)
		{
			this.Insert(pos, (HtmlNode)x);
		}

		void IList.Remove(object x)
		{
			this.Remove((HtmlNode)x);
		}

		void IList.RemoveAt(int pos)
		{
			this.RemoveAt(pos);
		}
#endregion

#region Implementation (IEnumerable)
		IEnumerator IEnumerable.GetEnumerator()
		{
			return (IEnumerator)(this.GetEnumerator());
		}
#endregion

#region Nested enumerator class
		/// <summary>
		/// Supports simple iteration over a <see cref="HtmlNodeCollection"/>.
		/// </summary>
		public class Enumerator : IEnumerator
		{
#region Implementation (data)
			private HtmlNodeCollection _collection;

			private int _index;

			private int _version;
#endregion

#region Construction
			/// <summary>
			/// Initializes a new instance of the <c>Enumerator</c> class.
			/// </summary>
			/// <param name="tc"></param>
			internal Enumerator(HtmlNodeCollection tc)
			{
				_collection = tc;
				_index = -1;
				_version = tc._version;
			}
#endregion

#region Operations (type-safe IEnumerator)
			/// <summary>
			/// Gets the current element in the collection.
			/// </summary>
			public HtmlNode Current
			{
				get
				{
					return _collection[_index];
				}
			}

			/// <summary>
			/// Advances the enumerator to the next element in the collection.
			/// </summary>
			/// <exception cref="InvalidOperationException">
			/// The collection was modified after the enumerator was created.
			/// </exception>
			/// <returns>
			/// <c>true</c> if the enumerator was successfully advanced to the next element;
			/// <c>false</c> if the enumerator has passed the end of the collection.
			/// </returns>
			public bool MoveNext()
			{
				if(_version != _collection._version)
					throw new InvalidOperationException(RD.GetString("collModified"));

				++_index;
				return (_index < _collection.Count) ? true : false;
			}

			/// <summary>
			/// Sets the enumerator to its initial position, before the first element in the collection.
			/// </summary>
			public void Reset()
			{
				_index = -1;
			}
#endregion

#region Implementation (IEnumerator)
			object IEnumerator.Current
			{
				get
				{
					return (object)(this.Current);
				}
			}
#endregion
		}
#endregion

#region Nested Syncronized Wrapper class
        private class SyncHtmlNodeCollection : HtmlNodeCollection
        {
#region Implementation (data)
            private HtmlNodeCollection _collection;

            private object _root;
#endregion

#region Construction
            internal SyncHtmlNodeCollection(HtmlNodeCollection list) : base(Tag.Default)
            {
                _root = list.SyncRoot;
                _collection = list;
            }
#endregion

#region Type-safe ICollection
            public override void CopyTo(HtmlNode[] array)
            {
                lock(this._root)
                    _collection.CopyTo(array);
            }

            public override void CopyTo(HtmlNode[] array, int start)
            {
                lock(this._root)
                    _collection.CopyTo(array,start);
            }
            public override int Count
            {
                get
                {
                    lock(this._root)
                        return _collection.Count;
                }
            }

            public override bool IsSynchronized
            {
                get
                {
                	return true;
                }
            }

            public override object SyncRoot
            {
                get
                {
                	return this._root;
                }
            }
#endregion

#region Type-safe IList
            public override HtmlNode this[int i]
            {
                get
                {
                    lock(this._root)
                        return _collection[i];
                }

                set
                {
                    lock(this._root)
                        _collection[i] = value;
                }
            }

            public override int Add(HtmlNode x)
            {
                lock(this._root)
                    return _collection.Add(x);
            }

            public override void Clear()
            {
                lock(this._root)
                    _collection.Clear();
            }

            public override bool Contains(HtmlNode x)
            {
                lock(this._root)
                    return _collection.Contains(x);
            }

            public override int IndexOf(HtmlNode x)
            {
                lock(this._root)
                    return _collection.IndexOf(x);
            }

            public override void Insert(int pos, HtmlNode x)
            {
                lock(this._root)
                    _collection.Insert(pos,x);
            }

            public override void Remove(HtmlNode x)
            {
                lock(this._root)
                    _collection.Remove(x);
            }

            public override void RemoveAt(int pos)
            {
                lock(this._root)
                    _collection.RemoveAt(pos);
            }

            public override bool IsFixedSize
            {
                get {return _collection.IsFixedSize;}
            }

            public override bool IsReadOnly
            {
                get {return _collection.IsReadOnly;}
            }
#endregion

#region Type-safe IEnumerable
            public override Enumerator GetEnumerator()
            {
                lock(_root)
                    return _collection.GetEnumerator();
            }
#endregion

#region Public Helpers
            // (just to mimic some nice features of ArrayList)
            public override int Capacity
            {
                get
                {
                    lock(this._root)
                        return _collection.Capacity;
                }

                set
                {
                    lock(this._root)
                        _collection.Capacity = value;
                }
            }

            public override int AddRange(HtmlNodeCollection x)
            {
                lock(this._root)
                    return _collection.AddRange(x);
            }

            public override int AddRange(HtmlNode[] x)
            {
                lock(this._root)
                    return _collection.AddRange(x);
            }
#endregion
        }
#endregion

#region Nested Read Only Wrapper class
        private class ReadOnlyHtmlNodeCollection : HtmlNodeCollection
        {
#region Implementation (data)
            private HtmlNodeCollection _collection;
#endregion

#region Construction
            internal ReadOnlyHtmlNodeCollection(HtmlNodeCollection list) : base(Tag.Default)
            {
                _collection = list;
            }
#endregion

#region Type-safe ICollection
            public override void CopyTo(HtmlNode[] array)
            {
                _collection.CopyTo(array);
            }

            public override void CopyTo(HtmlNode[] array, int start)
            {
                _collection.CopyTo(array,start);
            }
            public override int Count
            {
                get
                {
                	return _collection.Count;
                }
            }

            public override bool IsSynchronized
            {
                get
                {
                	return _collection.IsSynchronized;
                }
            }

            public override object SyncRoot
            {
                get
                {
                	return this._collection.SyncRoot;
                }
            }
#endregion

#region Type-safe IList
            public override HtmlNode this[int i]
            {
                get
                {
                	return _collection[i];
                }

                set
                {
                	throw new NotSupportedException(RD.GetString("readOnlyCollection"));
                }
            }

            public override int Add(HtmlNode x)
            {
                throw new NotSupportedException(RD.GetString("readOnlyCollection"));
            }

            public override void Clear()
            {
                throw new NotSupportedException(RD.GetString("readOnlyCollection"));
            }

            public override bool Contains(HtmlNode x)
            {
                return _collection.Contains(x);
            }

            public override int IndexOf(HtmlNode x)
            {
                return _collection.IndexOf(x);
            }

            public override void Insert(int pos, HtmlNode x)
            {
                throw new NotSupportedException(RD.GetString("readOnlyCollection"));
            }

            public override void Remove(HtmlNode x)
            {
                throw new NotSupportedException(RD.GetString("readOnlyCollection"));
            }

            public override void RemoveAt(int pos)
            {
                throw new NotSupportedException(RD.GetString("readOnlyCollection"));
            }

            public override bool IsFixedSize
            {
                get
                {
                	return true;
                }
            }

            public override bool IsReadOnly
            {
                get
                {
                	return true;
                }
            }
#endregion

#region Type-safe IEnumerable
            public override Enumerator GetEnumerator()
            {
                return _collection.GetEnumerator();
            }
#endregion

#region Public Helpers
            // (just to mimic some nice features of ArrayList)
            public override int Capacity
            {
          		get
          		{
          			return _collection.Capacity;
          		}

                set
                {
                	throw new NotSupportedException(RD.GetString("readOnlyCollection"));
                }
            }

            public override int AddRange(HtmlNodeCollection x)
            {
                throw new NotSupportedException(RD.GetString("readOnlyCollection"));
            }

            public override int AddRange(HtmlNode[] x)
            {
                throw new NotSupportedException(RD.GetString("readOnlyCollection"));
            }
#endregion
        }
#endregion
	}
}
