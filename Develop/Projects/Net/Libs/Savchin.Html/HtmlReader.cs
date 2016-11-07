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

namespace Savchin.Html
{
	/// <summary>
	/// Represents a base class for HTML readers.
	/// </summary>
	public abstract class HtmlReader
	{
	    /// <summary>
	    /// The number of attributes in the current node.
	    /// </summary>
	    /// <value>The number of attributes.</value>
	    public abstract int AttributeCount
	    {
	        get;
	    }

	    /// <summary>
	    /// The depth of the current node (for &lt;html&gt; tag should be 0).
	    /// </summary>
	    /// <value>The current node's depth.</value>
	    public abstract int Depth
	    {
	        get;
	    }

	    /// <summary>
	    /// Checks if the reader is at the end of file.
	    /// </summary>
	    /// <value>true if it is; false otherwise.</value>
	    public abstract bool EOF
	    {
	        get;
	    }

	    /// <summary>
	    /// Checks if the current node has attributes.
	    /// </summary>
	    /// <value>true if it has; false otherwise.</value>
	    public abstract bool HasAttributes
	    {
	        get;
	    }

	    /// <summary>
	    /// Checks if the current node has a value.
	    /// </summary>
	    /// <value>true if it has; false otherwise.</value>
	    public abstract bool HasValue
	    {
	        get;
	    }

	    /// <summary>
	    /// Checks if the current node is an empty element.
	    /// </summary>
	    /// <value>true if it is; false otherwise.</value>
	    public abstract bool IsEmptyElement
	    {
	        get;
	    }

        /// <summary>
        /// Gets an attribute or the current node by it's index.
        /// </summary>
        /// <value>The string that represents the attribute's value.</value>
        public abstract string this[int index]
        {
            get;
        }

        /// <summary>
        /// Gets an attribute or the current node by it's name.
        /// </summary>
        /// <value>The string that represents the attribute's value.</value>
        public abstract string this[string index]
        {
            get;
        }

        /// <summary>
        /// The name of the current node.
        /// </summary>
        /// <value>The string that represents the value.</value>
        public abstract string Name
        {
            get;
        }

        /// <summary>
        /// The type of the current node.
        /// </summary>
        /// <value>An <see cref="HtmlNodeType"/> that determines the type of
        /// the current node.</value>
        public abstract HtmlNodeType NodeType
        {
            get;
        }

        /// <summary>
        /// A character used as quote for attribute's values.
        /// </summary>
        /// <value>The quote character.</value>
        public abstract char QuoteChar
        {
            get;
        }

        /// <summary>
        /// Gets the current node value.
        /// </summary>
        /// <value>The string that represents the node's value.</value>
        public abstract string Value
        {
            get;
        }

        /// <summary>
        /// Closes the reader.
        /// </summary>
        public abstract void Close();

	    /// <summary>
	    /// Moves the reader's position to the attribute <paramref name="index"/>.
	    /// </summary>
	    /// <param name="index">The number of the attribute.</param>
	    public abstract void MoveToAttribute(int index);

	    /// <summary>
	    /// Moves the reader's position to the attribute <paramref name="attrName"/>.
	    /// </summary>
	    /// <param name="attrName">The name of the attribute.</param>
	    public abstract void MoveToAttribute(string attrName);

	    /// <summary>
	    /// Moves the reader's position to the node's contents.
	    /// </summary>
	    /// <returns>An <see cref="HtmlNodeType"/>.</returns>
	    public abstract HtmlNodeType MoveToContent();

	    /// <summary>
	    /// Moves the reader's position to the beginning of the node.
	    /// </summary>
	    public abstract bool MoveToElement();

	    /// <summary>
	    /// Moves the reader's position to the first attribute.
	    /// </summary>
	    /// <returns>true if the node has attributes; false otherwise.</returns>
	    public abstract bool MoveToFirstAttribute();

	    /// <summary>
	    /// Moves the reader's position to the next attribute.
	    /// </summary>
	    /// <returns>true if the node has more attributes; false otherwise.</returns>
	    public abstract bool MoveToNextAttribute();

	    /// <summary>
	    /// Reads a node from the input stream.
	    /// </summary>
	    /// <returns>true if the input stream contains more data;
	    /// false otherwise.</returns>
	    /// <remarks>If the method cannot read more data from the input stream
	    /// it should return false and set the <see cref="EOF"/>
	    /// property.</remarks>
	    public abstract bool Read();
	}
}
