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
using System.IO;
using System.Text;

using Savchin.Html;

namespace Savchin.Html
{
	/// <summary>
	/// Represents an HTML document as a tree of <see cref="HtmlNode"/>s.
	/// </summary>
	public class HtmlDocument: HtmlLinkedNode
	{
	    private char _assignment;

	    private char _quote;

		/// <summary>
		/// Creates and initializes the HTML document.
		/// </summary>
		public HtmlDocument()
		{
    	    _assignment = '=';
	        _quote = '\"';
		}

		/// <summary>
		/// Creates a deep copy of the document.
		/// </summary>
		/// <returns>The document's copy.</returns>
		public override object Clone()
		{
		    HtmlDocument result = new HtmlDocument();
		    result.SetParentNode(ParentNode);
		    result.SetDocument(Document);
		    return result;
		}
	    /// <summary>
	    /// The assignement character for the HTML tag attributes.
	    /// </summary>
	    /// <value>The assignement character.</value>
	    public char Assignment
	    {
	        get
	        {
	            return _assignment;
	        }
	    }

	    /// <summary>
	    /// The document's attributes.
	    /// </summary>
	    /// <value>
	    /// Always a null value. The HTML document itself cannot have attributes.
	    /// </value>
	    public override HtmlAttributeCollection Attributes
	    {
	        get
	        {
	            return null;
	        }
	    }

	    /// <summary>
	    /// The document's name.
	    /// </summary>
	    /// <value>
	    /// Always returns "#document" string.
	    /// </value>
	    public override string Name
	    {
	        get
	        {
	            return "#document";
	        }
	        set
	        {
#if _DEBUG
				throw new NotImplementedException("HtmlDocument.Name.set");
#endif
	        }
	    }

	    /// <summary>
	    /// The quote character for the current HTML file atributes.
	    /// </summary>
	    /// <value>The quote character.</value>
	    public char Quote
	    {
	        get
	        {
	            return _quote;
	        }
	    }

	    /// <summary>
	    /// The document node's type.
	    /// </summary>
	    /// <value>
	    /// Always <see cref="HtmlNodeType.Document"/>.
	    /// </value>
	    /// <seealso cref="HtmlNodeType"/>
	    public override HtmlNodeType Type
	    {
	        get
	        {
	            return HtmlNodeType.Document;
	        }
	    }

	    /// <summary>
	    /// The document's value.
	    /// </summary>
	    /// <value>The textual representation of the HTML document.</value>
	    /// <remarks>
	    /// It is no advisable to use this property because the text
	    /// representation of an HTML may become enormous. Use Save method.
	    /// </remarks>
	    public override string Value
	    {
	        get
	        {
	            StringBuilder sb = new StringBuilder(256);

	            if(ChildNodes.Count > 0)
	            {
	                foreach(HtmlNode node in ChildNodes)
	                {
	                    sb.Append(node.ToString());
	                    sb.Append(Environment.NewLine);
	                }
	            }

	            return sb.ToString();
	        }
	        set
	        {
#if _DEBUG
				throw new NotImplementedException("HtmlDocument.Value.set");
#endif
	        }
	    }

	    /// <summary>
	    /// Loads and parses an HTML file from a <see cref="TextReader"/>.
	    /// </summary>
	    /// <param name="reader">The <see cref="TextReader"/> to read HTML
	    /// text from.</param>
	    /// <remarks>
	    /// This method disposes the <paramref name="reader"/>.
	    /// </remarks>
	    public void Load(TextReader reader)
	    {
	        using(HtmlTextReader hr = new HtmlTextReader(reader))
	        {
	            Load(hr);
	        }
	    }

	    /// <summary>
	    /// Loads and parses an HTML file from a <see cref="Stream"/>.
	    /// </summary>
	    /// <param name="source">The <see cref="Stream"/> to read HTML text from.</param>
	    /// <remarks>
	    /// This method disposes the <paramref name="source"/>.
	    /// </remarks>
	    public void Load(Stream source)
	    {
	        using(HtmlTextReader hr = new HtmlTextReader(source))
	        {
	            Load(hr);
	        }
	    }

	    /// <summary>
	    /// Loads and parses an HTML file from a <see cref="Stream"/> assuming
	    /// that the data in the specified encoding.
	    /// </summary>
	    /// <param name="source">The <see cref="Stream"/> to read HTML text from.</param>
	    /// <param name="encoding">The encoding of the data
	    /// <paramref name="source"/> stream.</param>
	    /// <remarks>
	    /// This method disposes the <paramref name="source"/>.
	    /// </remarks>
	    public void Load(Stream source, Encoding encoding)
	    {
	        using(HtmlTextReader hr = new HtmlTextReader(new StreamReader(source, encoding)))
	        {
	            Load(hr);
	        }
	    }

	    /// <summary>
	    /// Loads and parses an HTML from a text file.
	    /// </summary>
	    /// <param name="fileName">The name of the file to read HTML from.</param>
	    public void Load(string fileName)
	    {
	        using(HtmlTextReader hr = new HtmlTextReader(new StreamReader(fileName)))
	        {
	            Load(hr);
	        }
	    }

	    /// <summary>
	    /// Loads and parses an HTML from a text file assuming
	    /// that the data in the specified encoding.
	    /// </summary>
	    /// <param name="fileName">The name of the file to read HTML from.</param>
	    /// <param name="encoding">The encoding of the data
	    /// <paramref name="fileName"/> stream.</param>
	    public void Load(string fileName, Encoding encoding)
	    {
	        using(HtmlTextReader hr = new HtmlTextReader(new StreamReader(fileName, encoding)))
	        {
	            Load(hr);
	        }
	    }

	    /// <summary>
	    /// Normalizes the document.
	    /// </summary>
	    /// <remarks>
	    /// Current implementation does nothing.
	    /// </remarks>
	    public override void Normalize()
	    {}

	    /// <summary>
	    /// Creates a textual representation of an <see cref="HtmlDocument"/>.
	    /// </summary>
	    /// <returns>
	    /// The textual representation of an <see cref="HtmlDocument"/>.
	    /// </returns>
	    public override string ToString()
	    {
	        return Value;
	    }
	}
}
