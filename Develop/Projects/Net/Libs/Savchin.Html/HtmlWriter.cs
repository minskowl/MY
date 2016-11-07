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

namespace Savchin.Html
{
	/// <summary>
	/// An abstract base class for HTML text writers.
	/// </summary>
	public abstract class HtmlWriter
	{
	    /// <summary>
	    /// Closes the writer.
	    /// </summary>
	    public abstract void Close();

	    /// <summary>
	    /// Flushes the internal buffers.
	    /// </summary>
	    public abstract void Flush();

	    /// <summary>
	    /// Write an element to the output stream.
	    /// </summary>
	    /// <param name="name">The element's name.</param>
	    /// <param name="value">The element's value.</param>
	    /// <remarks>This method is suitable for writing simple tags.</remarks>
	    public abstract void WriteElement(string name, string value);

	    /// <summary>
	    /// Writes the start element.
	    /// </summary>
	    /// <param name="name">The name of the element.</param>
	    /// <remarks>The implementation of <see cref="HtmlWriter"/> should
	    /// maintain a stack of elements (tags) written. The implementation of
	    /// this method should store the tag on this stack so
	    /// the <see cref="WriteEndElement"/> would be able to write
	    /// the closing part of the element later on.</remarks>
	    /// <seealso cref="WriteEndElement"/>
	    public abstract void WriteStartElement(string name);

	    /// <summary>
	    /// Writes the end element (closing tag).
	    /// </summary>
	    /// <remarks>The implementation of <see cref="HtmlWriter"/> should
	    /// maintain a stack of elements (tags) written. The implementation of
	    /// this method should use this stak to retrieve the closing tag.
	    /// </remarks>
	    /// <seealso cref="WriteStartElement"/>
	    public abstract void WriteEndElement();

	    /// <summary>
	    /// Writes the attribute as a pair of 'name=value'.
	    /// </summary>
	    /// <param name="name">The attribute's name.</param>
	    /// <param name="value">The attribute's value.</param>
	    /// <remarks>The implementation of this method must encode extended
	    /// characters that the attribute's value might contain to proper
	    /// HTML entities before writing the text to the output stream.</remarks>
	    /// <seealso cref="Entity"/>
	    public abstract void WriteAttribute(string name, string value);

	    /// <summary>
	    /// Writes the text verbatim to the output stream.
	    /// </summary>
	    /// <param name="text">The text to write to the writer.</param>
	    /// <remarks>The implementation of this method must encode extended
	    /// characters that the text might contain to proper HTML entities
	    /// before writing the text to the output stream.</remarks>
	    /// <seealso cref="Entity"/>
	    public abstract void WriteText(string text);
	}
}
