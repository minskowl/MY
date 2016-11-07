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
using Savchin.Html;

namespace Savchin.Html
{
	/// <summary>
	/// Represents an abstract character data in HTML file.
	/// </summary>
	/// <remarks>Instances of this class' descendants cannot have attributes
	/// or child nodes.</remarks>
	public abstract class HtmlCharData: HtmlNode
	{
	    /// <summary>
	    /// The string that holds the contents of a
	    /// <see cref="HtmlCharData"/> descendant instance.
	    /// </summary>
	    /// <seealso cref="HtmlText"/>
	    /// <seealso cref="HtmlComment"/>
	    /// <seealso cref="HtmlDocumentType"/>
	    protected string text;

	    /// <summary>
	    /// Initializes a new instance of <see cref="HtmlCharData"/>.
	    /// </summary>
	    public HtmlCharData()
	    {
   		    text = String.Empty;
	    }

	    /// <summary>
	    /// Initializes a new instance of <see cref="HtmlCharData"/> with
	    /// a text string.
	    /// </summary>
	    /// <param name="text">
	    /// The text for the chaacter data element.
	    /// </param>
	    public HtmlCharData(string text)
	    {
	        this.text = text;
	    }

        /// <summary>
        /// Checks if the node has a value.
        /// </summary>
        /// <value>true if the node has a value; false oherwise.</value>
        public override bool HasValue
        {
            get
            {
                if(0 == text.Length)
                   return false;
                return true;
            }
        }

	    /// <summary>
	    /// The text's value.
	    /// </summary>
	    /// <value>The text.</value>
	    public override string Value
	    {
	        get
	        {
	            return text;
	        }
	        set
	        {
	            if(null == value)
    	            text = String.Empty;
	            else
    	            text = value;
	        }
	    }
	}
}
