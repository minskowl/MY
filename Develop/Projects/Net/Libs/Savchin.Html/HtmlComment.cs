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
	/// Represents an HTML comment.
	/// </summary>
	public class HtmlComment: HtmlCharData
	{
		/// <summary>
		/// Intializes an empty instance of <see cref="HtmlComment"/>.
		/// </summary>
		public HtmlComment():
			base()
		{}

		/// <summary>
		/// Creates and intializes a new instance of <see cref="HtmlComment"/>.
		/// </summary>
		/// <param name="comment">The text of the comment.</param>
		public HtmlComment(string comment):
			base(comment)
		{}

	    /// <summary>
	    /// Creates a copy of the comment.
	    /// </summary>
	    /// <returns>The comment's copy.</returns>
	    public override object Clone()
	    {
	        HtmlComment result = new HtmlComment(text);
		    result.SetParentNode(ParentNode);
		    result.SetDocument(Document);
		    return result;
	    }

	    /// <summary>
	    /// The node's name.
	    /// </summary>
	    /// <value>
	    /// Always returns "#comment" string.
	    /// </value>
		public override string Name
		{
			get
			{
				return "#comment";
			}

			set
			{
#if _DEBUG
				throw new NotImplementedException("HtmlComment.Name.set");
#endif
			}
		}

	    /// <summary>
	    /// The comment node's type.
	    /// </summary>
	    /// <value>
	    /// Always <see cref="HtmlNodeType.Comment"/>.
	    /// </value>
	    /// <seealso cref="HtmlNodeType"/>
		public override HtmlNodeType Type
		{
		    get
		    {
		        return HtmlNodeType.Comment;
		    }
		}

	    /// <summary>
	    /// Normalizes the comment.
	    /// </summary>
	    /// <remarks>
	    /// Current implementation does nothing.
	    /// </remarks>
	    public override void Normalize()
	    {}

	    /// <summary>
	    /// Returns a textual representation of an <see cref="HtmlComment"/>.
	    /// </summary>
	    /// <returns>
	    /// The textual representation of an <see cref="HtmlComment"/>.
	    /// </returns>
	    public override string ToString()
	    {
	        StringBuilder sb = new StringBuilder(32);

	        sb.Append("<!-- ");
	        sb.Append(text);
	        sb.Append(" -->");

	        return sb.ToString();
	    }
	}
}
