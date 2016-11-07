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
using System.IO;
using System.Text;

using Savchin.Html;

namespace Savchin.Html
{
	/// <summary>
	/// Represents an HTML element (tag).
	/// </summary>
	public class HtmlElement: HtmlLinkedNode
	{
	    private string _name;

		/// <summary>
		/// Initalizes an instance of an <see cref="HtmlElement"/>.
		/// </summary>
		public HtmlElement()
		{
		    _name = String.Empty;
		}

		/// <summary>
		/// Initializes all fields to default values
		/// </summary>
		/// <param name="name">The element (tag) name.</param>
		public HtmlElement(string name)
		{
		    _name = name;
		}

		/// <summary>
		/// Creates a deep copy of the HTML element.
		/// </summary>
		/// <returns>The element's copy.</returns>
		public override object Clone()
		{
		    HtmlElement result = new HtmlElement(_name);
		    result.Attributes.AddRange(Attributes);
		    result.ChildNodes.AddRange(ChildNodes);
		    result.SetParentNode(ParentNode);
		    result.SetDocument(Document);
		    return result;
		}

		/// <summary>
		/// The element name, an HTML tag
		/// </summary>
		/// <value>The textual representation of the tag.</value>
		/// <remarks>
		/// The case of tag is being kept. Although all comparsions etc.
		/// are case-intensive.
		/// </remarks>
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
	    /// The element node's type.
	    /// </summary>
	    /// <value>
	    /// Always <see cref="HtmlNodeType.Tag"/>.
	    /// </value>
	    /// <seealso cref="HtmlNodeType"/>
		public override HtmlNodeType Type
		{
		    get
		    {
		        return HtmlNodeType.Tag;
		    }
		}

	    /// <summary>
	    /// The element's value.
	    /// </summary>
	    /// <value>The textual representation of the element - i.e.
	    /// a properly formatted HTML tag.</value>
	    public override string Value
	    {
	        get
	        {
	            StringBuilder sb = new StringBuilder();

	            if(ChildNodes.Count > 0)
	            {
	                for(int i = 0; i < ChildNodes.Count; i++)
	                    sb.Append(ChildNodes[i].ToString());
	            }

	            return sb.ToString();
	        }

	        set
	        {
	            StringReader reader = new StringReader(value);
	            HtmlReader hr = new HtmlTextReader(reader);

	            Load(hr);
	        }
	    }

	    /// <summary>
	    /// Parses an HTML tag text with attributes.
	    /// </summary>
	    /// <param name="htmlFragment">The text fragment to parse.</param>
	    internal void Parse(string htmlFragment)
	    {
		    int i, pos;
		    string name, val;
		    char terminator;

		    RemoveAll();

		    // Extract tag name
		    for(i = 1; i < htmlFragment.Length; i++)
		        if(Char.IsWhiteSpace(htmlFragment[i]))
		            break;
	        Name = htmlFragment.Substring(0, i);

		    bool condition = true;
		    while(condition)
		    {
		        // Skip wihitespace
		        for(; i < htmlFragment.Length; i++)
		            if(!Char.IsWhiteSpace(htmlFragment[i]))
		                break;
		        pos = i;
		        // Parse 'till the assignement operator
		        for(; i < htmlFragment.Length; i++)
		            if('=' == htmlFragment[i])
		                break;
		        name = htmlFragment.Substring(pos, i - pos);

		        if(++i >= htmlFragment.Length)
		        {
		            condition = false;
		            continue;
		        }

		        if((htmlFragment[i] == '\"') ||
		           (htmlFragment[i] == '\''))
	            {
	                terminator = htmlFragment[i++];
                    pos = i;
                    for(; i < htmlFragment.Length; i++)
	                    if(terminator == htmlFragment[i])
	                        break;
                }
                else
                {
                    pos = i;
                    for(;i < htmlFragment.Length; i++)
                        if(Char.IsWhiteSpace(htmlFragment[i]))
                            break;
                }
                val = htmlFragment.Substring(pos, i - pos);
                HtmlAttribute attr = new HtmlAttribute(Document, name, val);
                AppendAttribute(attr);
		        if(++i >= htmlFragment.Length)
		        {
		            condition = false;
		            continue;
		        }
            }
	    }

	    /// <summary>
	    /// Creates a textual representation of a <see cref="HtmlElement"/>.
	    /// </summary>
	    /// <returns>
	    /// The textual representation of a <see cref="HtmlElement"/>.
	    /// </returns>
	    public override string ToString()
	    {
	        if((null == _name) ||
	           (_name.Length == 0))
	            throw new HtmlException(RD.GetString("noTagName"));

	        StringBuilder sb = new StringBuilder();

	        sb.Append('<');
	        sb.Append(Name);
	        if(Attributes.Count > 0)
	        {
	            sb.Append(' ');
	            for(int i = 0; i < Attributes.Count; i++)
	            {
	                sb.Append(Attributes[i].ToString());
	                if(i != Attributes.Count - 1)
	                    sb.Append(' ');
	            }
	        }
	        sb.Append('>');
	        if(FirstChild is HtmlElement)
	            sb.Append(Environment.NewLine);

/*	        if(ChildNodes.Count > 0)
	        {
	            for(int i = 0; i < ChildNodes.Count; i++)
	                sb.Append(ChildNodes[i].ToString());
	        }*/

	        sb.Append(Value);

	        if(!Tag.IsAtomic(Name))
	        {
	            sb.Append("</");
	            sb.Append(Name);
	            sb.Append('>');
	        }
	        if(LastChild is HtmlElement)
	            sb.Append(Environment.NewLine);

	        return sb.ToString();
	    }
	}
}
