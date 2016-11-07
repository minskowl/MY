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
	/// Represents HTML text.
	/// </summary>
	public class HtmlText: HtmlCharData
	{
		/// <summary>
		/// Initializes an empty instance of <see cref="HtmlText"/> class.
		/// </summary>
		public HtmlText(): base()
		{
		}

		/// <summary>
		/// Initializes a new instance of <see cref="HtmlText"/> class.
		/// </summary>
		/// <param name="text">The node's text.</param>
		public HtmlText(string text): base(text)
		{
		}

		/// <summary>
		/// Creates a copy of the text element.
		/// </summary>
		/// <returns>THe text's copy.</returns>
		public override object Clone()
		{
		    HtmlText result = new HtmlText(text);
		    result.SetParentNode(ParentNode);
		    result.SetDocument(Document);
		    return result;
		}

	    /// <summary>
	    /// The node's name.
	    /// </summary>
	    /// <value>
	    /// Always returns "#text" string.
	    /// </value>
		public override string Name
		{
			get
			{
				return "#text";
			}
			set
			{
#if _DEBUG
				throw new NotImplementedException("HtmlText.Name.set");
#endif
			}
		}

	    /// <summary>
	    /// The text node's type.
	    /// </summary>
	    /// <value>
	    /// Always <see cref="HtmlNodeType.Text"/>.
	    /// </value>
	    /// <seealso cref="HtmlNodeType"/>
		public override HtmlNodeType Type
		{
		    get
		    {
		        return HtmlNodeType.Text;
		    }
		}

	    /// <summary>
	    /// Normalizes the node.
	    /// </summary>
	    public override void Normalize()
	    {
	        StringBuilder sb = new StringBuilder(text);

	        sb.Replace("  ", " ");

	        text = sb.ToString();
	    }

	    /// <summary>
	    /// Returns a textual representation of an <see cref="HtmlText"/>.
	    /// </summary>
	    /// <returns>
	    /// The textual representation of an <see cref="HtmlText"/>.
	    /// </returns>
	    public override string ToString()
	    {
	        return text;
	    }
	}
}
