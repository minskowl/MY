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
using System.Collections.Specialized;

namespace Savchin.Html
{
	/// <summary>
	/// Represents a table of attributes. Each attribute has a name and a value.
	/// Although the values can be accessed by their names or indices, it is not
	/// possible to add an attribute by index.
	/// </summary>
	/// <seealso cref="HtmlTextReader"/>
	/// <seealso cref="HtmlTextWriter"/>
	/// <seealso cref="Token"/>
	internal class AttributeTable
	{
		private Hashtable _hashtable;

		private StringCollection _keys;

		/// <summary>
		/// Initializes a new instance of <see cref="AttributeTable"/> class.
		/// </summary>
		internal AttributeTable()
		{
			this._hashtable = new Hashtable();
			this._keys = new StringCollection();
		}

		/// <summary>
		/// Gets or sets the value of an attribute by its name.
		/// </summary>
		/// <value>
		/// The attribute's value.
		/// </value>
		internal string this[string key]
		{
			get
			{
				return (string)_hashtable[key];
			}

			set
			{
				_keys.Add(key);
				_hashtable.Add(key, value);
			}
		}

		/// <summary>
		/// Gets the value of an attribute by its index.
		/// </summary>
		/// <value>
		/// The attribute's value.
		/// </value>
		internal string this[int index]
		{
			get
			{
				return this[(string)_keys[index]];
			}
		}

		internal StringCollection Keys
		{
			get
			{
				return _keys;
			}
		}

		/// <summary>
		/// Gets the size of the <see cref="AttributeTable"/>.
		/// </summary>
		/// <value>
		/// The number of attributes in the table.
		/// </value>
		internal int Count
		{
			get
			{
				return _hashtable.Count;
			}
		}
	}
}
