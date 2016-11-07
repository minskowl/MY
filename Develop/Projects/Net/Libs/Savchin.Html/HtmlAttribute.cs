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
using System.Text;

namespace Savchin.Html
{
	/// <summary>
	/// Represents an HTML tag attribute.
	/// </summary>
	/// <seealso cref="HtmlAttributeCollection"/>
	public class HtmlAttribute: HtmlNode
	{
		private string _name;

		private string _value;

		private char _assignment;

		private char _quote;

		/// <summary>
		/// Creates and initializes an instance of <see cref="HtmlAttribute"/>.
		/// </summary>
		/// <param name="doc">The owner document.</param>
		/// <param name="name">The name of the attribute.</param>
		/// <param name="val">The attribute's value.</param>
		public HtmlAttribute(HtmlDocument doc, string name, string val)
		{
			_name = name;
			_value = val;
		    if(null != doc)
			    _assignment = doc.Assignment;
		    else
			    _assignment = '=';
		    if(null != doc)
			    _quote = doc.Quote;
		    else
			    _quote = '\"';
		}

	    /// <summary>
	    /// Creates a deep copy of the attribute.
	    /// </summary>
	    /// <returns>The attribute's copy.</returns>
	    public override object Clone()
	    {
	        HtmlAttribute result = new HtmlAttribute(Document, _name, _value);
		    result.SetParentNode(ParentNode);
		    result.SetDocument(Document);
	        return result;
	    }

	    /// <summary>
	    /// Checks if the HTML attribute has its own attributes.
	    /// </summary>
	    /// <value>Always false.</value>
	    public override bool HasAttributes
	    {
	        get
	        {
	            return false;
	        }
	    }

	    /// <summary>
	    /// The attributes.
	    /// </summary>
	    /// <value>
	    /// Always returns null.
	    /// </value>
	    public override HtmlAttributeCollection Attributes
	    {
	        get
	        {
	        	return null;
	        }
	    }

	    /// <summary>
	    /// The node's child nodes.
	    /// </summary>
	    /// <value>
	    /// Always a null value.
	    /// </value>
	    public override HtmlNodeCollection ChildNodes
	    {
	        get
	        {
	            return null;
	        }
	    }

	    /// <summary>
	    /// Checks if the attribute is a known attribute.
	    /// </summary>
	    /// <returns>true if the attribute is a known attribute;
	    /// false otherwise.</returns>
	    public override bool IsKnown
	    {
	        get
	        {
	            return Attribute.IsKnown(this);
	        }
	    }

		/// <summary>
		/// The name of the attribute.
		/// </summary>
		/// <value>A string with the attribute's name.</value>
		public override string Name
		{
			get
			{
				return _name;
			}

			set
			{
				_name = value;
			}
		}

	    /// <summary>
	    /// The attribute node's type.
	    /// </summary>
	    /// <value>
	    /// Always <see cref="HtmlNodeType.Attribute"/>.
	    /// </value>
	    /// <seealso cref="HtmlNodeType"/>
		public override HtmlNodeType Type
		{
		    get
		    {
		        return HtmlNodeType.Attribute;
		    }
		}

        /// <summary>
        /// Checks if the attribute has value.
        /// </summary>
        /// <value>true if the attribute has value; false otherwise.</value>
        public override bool HasValue
        {
            get
            {
                if((null == _value) ||
                   (0 == _value.Length))
                   return false;
                return true;
            }
        }

		/// <summary>
		/// The attribute's value.
		/// </summary>
		/// <value>A string with the attribute's value.</value>
		public override string Value
		{
			get
			{
				return _value;
			}

			set
			{
				_value = value;
			}
		}

		/// <summary>
		/// Assignment operator.
		/// </summary>
		/// <remarks>
		/// By default the attribute inherits the value of this property
		/// from the parent document.
		/// </remarks>
		/// <value>A string with text of assignment, usually "=".</value>
		public char Assignment
		{
			get
			{
				return _assignment;
			}

			set
			{
				_assignment = value;
			}
		}

		/// <summary>
		/// Attrubute's surrounding quotes.
		/// </summary>
		/// <remarks>
		/// By default the attribute inherits the value of this property
		/// from the parent document.
		/// </remarks>
		/// <value>A string with text of quote, usually "\"".</value>
		public char Quote
		{
			get
			{
				return _quote;
			}

			set
			{
				_quote = value;
			}
		}

	    /// <summary>
	    /// This special method allows the implementation to change the node's
	    /// owner document.
	    /// </summary>
	    /// <param name="document">An <see cref="HtmlDocument"/> that becomes
	    /// the node's owner.</param>
	    /// <seealso cref="HtmlNode.Document"/>
	    protected override void SetDocument(HtmlDocument document)
	    {
	        base.SetDocument(document);
		    if(null != document)
			    _assignment = document.Assignment;
		    else
			    _assignment = '=';
		    if(null != document)
			    _quote = document.Quote;
		    else
			    _quote = '\"';
	    }

		private static bool HasSpaces(string str)
		{
		    for(int i = 0; i < str.Length; i++)
		        if(Char.IsWhiteSpace(str[i]))
		            return true;
            return false;
		}

	    /// <summary>
	    /// Normalizes the attribute.
	    /// </summary>
	    /// <remarks>
	    /// Current implementation does nothing.
	    /// </remarks>
	    public override void Normalize()
	    {}

		/// <summary>
		/// Obtains the textual representation of this attribute.
		/// </summary>
		/// <returns>The HTML fragment with properly formatted
		/// attribute's name and value.</returns>
		public override string ToString()
		{
			StringBuilder sb = new StringBuilder();

			sb.Append(_name);
			sb.Append(_assignment);
		    if(HasSpaces(_value))
			    sb.Append(_quote);
			sb.Append(_value);
		    if(HasSpaces(_value))
			    sb.Append(_quote);

			return sb.ToString();
		}
	}
}
