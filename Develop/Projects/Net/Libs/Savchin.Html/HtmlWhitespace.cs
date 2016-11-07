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

using Savchin.Html;

namespace Savchin.Html
{
	/// <summary>
	/// Represents a significant whitespace in HTML file.
	/// </summary>
	public class HtmlWhitespace: HtmlCharData
	{
		/// <summary>
		/// Initializes a new instance of <see cref="HtmlWhitespace"/>
		/// </summary>
		public HtmlWhitespace(): base()
		{}

		/// <summary>
		/// Initializes a new instance of <see cref="HtmlWhitespace"/>
		/// with a proper whitespace text.
		/// </summary>
		/// <param name="text">
		/// The whitespace text.
		/// </param>
		public HtmlWhitespace(string text): base(text)
		{}

		/// <summary>
		/// The node name.
		/// </summary>
		/// <value>Always &quot;#whitespace&quot;.</value>
		public override string Name
		{
			get
			{
				return "#whitespace";
			}

			set
			{
#if _DEBUG
				throw new NotImplementedException("HtmlWhitespace.Name.set");
#endif
			}
		}

	    /// <summary>
	    /// The whitespace node's type.
	    /// </summary>
	    /// <value>
	    /// Always <see cref="HtmlNodeType.Whitespace"/>.
	    /// </value>
	    /// <seealso cref="HtmlNodeType"/>
		public override HtmlNodeType Type
		{
		    get
		    {
		        return HtmlNodeType.Whitespace;
		    }
		}

		/// <summary>
	    /// Creates a copy of the whitespace.
		/// </summary>
		public override object Clone()
		{
			HtmlWhitespace result = new HtmlWhitespace(text);
		    result.SetParentNode(ParentNode);
		    result.SetDocument(Document);
		    return result;
		}

		/// <summary>
		/// Normalizes the whitespace.
		/// </summary>
		/// <remarks>
		/// Current implementation turns the whitespace to a space.
		/// </remarks>
		public override void Normalize()
		{
			text = " ";
		}

	    /// <summary>
	    /// Returns a textual representation of an <see cref="HtmlWhitespace"/>.
	    /// </summary>
	    /// <returns>
	    /// The textual representation of an <see cref="HtmlWhitespace"/>.
	    /// </returns>
		public override string ToString()
		{
		    return text;
		}
	}
}
