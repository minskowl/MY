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

namespace Savchin.Html
{
	/// <summary>
	/// A simple HTML text writer (pretty printer).
	/// </summary>
	public class HtmlTextWriter: HtmlWriter, IDisposable
	{
#region Formatting rules

        /// <summary>
        /// Formatting flags.
        /// </summary>
        [Flags]
        private enum FormatFlags
        {
            /// <summary>
            /// No formatting necessary.
            /// </summary>
            None,

            /// <summary>
            /// A new line character sequence can be used after opening tag.
            /// </summary>
            BeginNewLine,

            /// <summary>
            /// A new line character sequence has to be added after closing tag.
            /// </summary>
            EndNewLine
        }

        private struct FormatRule
        {
            internal readonly string TagName;

            internal readonly FormatFlags Flags;

            internal FormatRule(string name, FormatFlags flags)
            {
                TagName = name;
                Flags = flags;
            }
        }

        private static FormatRule[] FormatRules = {
            new FormatRule(Tag.Html,  FormatFlags.BeginNewLine | FormatFlags.EndNewLine),
            new FormatRule(Tag.Head,  FormatFlags.BeginNewLine | FormatFlags.EndNewLine),
            new FormatRule(Tag.Title, FormatFlags.BeginNewLine | FormatFlags.EndNewLine),
            new FormatRule(Tag.P,     FormatFlags.EndNewLine),
            new FormatRule(Tag.Div,   FormatFlags.EndNewLine),
            new FormatRule(Tag.Body,  FormatFlags.BeginNewLine | FormatFlags.EndNewLine)
        };

	    private FormatFlags GetFlagsForTagName(string tagName)
	    {
	        for(int i = 0; i < FormatRules.Length; i++)
	            if(0 == String.Compare(FormatRules[i].TagName, tagName, true))
	                return FormatRules[i].Flags;
	        return FormatFlags.None;
	    }

#endregion

	    private TextWriter _writer;
	    private TokenStack _nestedTokens;

		/// <summary>
		/// Initializes a new instance of the <see cref="HtmlTextWriter"/> with
		/// the text stream to write data to.
		/// </summary>
		/// <param name="writer">
		/// The <see cref="TextWriter"/> to write data to.> to write data to.
		/// </param>
		public HtmlTextWriter(TextWriter writer)
		{
		    _writer = writer;
		    _nestedTokens = new TokenStack();
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="HtmlTextWriter"/> with
		/// the <see cref="Stream"/> to write data to using proper text encoding.
		/// </summary>
		/// <param name="stream">
		/// The target <see cref="Stream"/> to write data to.
		/// </param>
		/// <param name="encoding">The <see cref="Encoding"/> for the
		/// output text.</param>
		public HtmlTextWriter(Stream stream, Encoding encoding):
		    this(new StreamWriter(stream, encoding))
	    {}

		/// <summary>
		/// Initializes a new instance of the <see cref="HtmlTextWriter"/> with
		/// the the name of the file to write data to using proper text
		/// encoding.
		/// </summary>
		/// <param name="fileName">
		/// The name of the file to write data to.
		/// </param>
		/// <param name="encoding">The <see cref="Encoding"/> for the
		/// output text.</param>
	    public HtmlTextWriter(string fileName, Encoding encoding):
		    this(new StreamWriter(fileName, false, encoding))
	    {}

	    /// <summary>
	    /// Checks if the object has been disposed of and if yes, throws
	    /// an <see cref="ObjectDisposedException"/>
	    /// </summary>
	    /// <remarks>
	    /// <para>This method gets called at the beginning of most other methods
	    /// and property accessors.</para>
	    /// <para>If a derived class uses it's own managed or <see cref="IDisposable"/>
	    /// resources, override this method and call the base method as the last
	    /// sentence of the overriden method.</para>
	    /// </remarks>
	    /// <exception cref="ObjectDisposedException">
	    /// The object has already been disposed of.
	    /// </exception>
	    /// <example>These examples show how to override this method in a
	    /// derived class:
	    /// <include file='examples.xml' path='docs/doc[@id="Disposable"]'/>
	    /// </example>
	    /// <seealso cref="Dispose(bool)"/>
	    protected virtual void CheckDisposed()
	    {
	        if(null == _writer)
	            throw new ObjectDisposedException(RD.GetString("htwDisposed"));
	    }

		/// <summary>
        /// Releases all the resource associated with the object.
        /// </summary>
        /// <remarks>
        /// <param name="disposing"><c>true</c> to release both managed and
        /// unmanaged resources; <c>false</c> to release only unmanaged
        /// resources.</param>
        /// <para>This method actually disposes of the object. The methods
        /// <see cref="Dispose()"/> and <see cref="Close()"/> simply call
        /// this method.</para>
        /// <para>Because <see cref="HtmlTextWriter"/> does not contains
        /// unmanaged resources it implements only Disposable pattern.</para>
        /// </remarks>
        /// <example>These examples show how to override this method in a
        /// derived class:
	    /// <include file='examples.xml' path='docs/doc[@id="Disposable"]'/>
        /// </example>
	    /// <seealso cref="Dispose()"/>
	    /// <seealso cref="Close()"/>
	    /// <seealso cref="CheckDisposed()"/>
	    protected virtual void Dispose(bool disposing)
		{
		    if(disposing)
		    {
	            _writer.Close();
	            _writer = null;
		    }
		}

	    /// <summary>
	    /// Closes the associated text stream.
	    /// </summary>
	    /// <seealso cref="Dispose()"/>
	    public override void Close()
	    {
	        CheckDisposed();

	        Dispose();
	    }

		/// <summary>
		/// Releases all the resource associated with the object.
		/// </summary>
	    /// <seealso cref="Dispose(bool)"/>
		public void Dispose()
		{
		    // FIXME: If I really need it?
		    Flush();
	        Dispose(true);
	        GC.SuppressFinalize(this);
		}

	    /// <summary>
	    /// Flushes the internal buffers.
	    /// </summary>
	    public override void Flush()
	    {
	        CheckDisposed();

	        // Close all tokens accumelated on stack
	        while(Token.Null != _nestedTokens.Top)
	            WriteEndElement();
	        _writer.Flush();
	    }

	    private void CheckClosed()
	    {
	        CheckDisposed();

	        if(!_nestedTokens.Top.Closed)
	        {
	            _writer.Write('>');

    	        FormatFlags flags = GetFlagsForTagName(_nestedTokens.Top.Name);
	            if(0 != (FormatFlags.BeginNewLine & flags))
	                _writer.WriteLine();

	            _nestedTokens.Top.Closed = true;
	        }
	    }

	    /// <summary>
	    /// Write an element to the output stream.
	    /// </summary>
	    /// <param name="name">The element's name.</param>
	    /// <param name="value">The element's value.</param>
	    /// <remarks>This method is suitable for writing simple tags.</remarks>
	    public override void WriteElement(string name, string value)
	    {
	        CheckClosed();

	        _writer.Write('<');
	        _writer.Write(name);
	        _writer.Write('>');
	        InternalWriteText(value);
	        _writer.Write("</");
	        _writer.Write(name);
	        _writer.Write('>');

	        FormatFlags flags = GetFlagsForTagName(_nestedTokens.Top.Name);
	        if(0 != (FormatFlags.BeginNewLine & flags))
	            _writer.WriteLine();
	    }

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
	    public override void WriteStartElement(string name)
	    {
	        CheckClosed();

	        _nestedTokens.Push(new Token(name));
	        _writer.Write('<');
	        _writer.Write(name);
	    }

	    /// <summary>
	    /// Writes the end element (closing tag).
	    /// </summary>
	    /// <remarks>The implementation of <see cref="HtmlWriter"/> should
	    /// maintain a stack of elements (tags) written. The implementation of
	    /// this method should use this stak to retrieve the closing tag.
	    /// </remarks>
	    /// <seealso cref="WriteStartElement"/>
	    public override void WriteEndElement()
	    {
	        CheckClosed();

	        _writer.Write("</");
	        _writer.Write(_nestedTokens.Top.Name);
	        _writer.Write('>');

	        FormatFlags flags = GetFlagsForTagName(_nestedTokens.Top.Name);
	        if(0 != (FormatFlags.EndNewLine & flags))
	            _writer.WriteLine();

	        _nestedTokens.Pop();
	    }

	    /// <summary>
	    /// Writes the attribute as a pair of 'name=value'.
	    /// </summary>
	    /// <param name="name">The attribute's name.</param>
	    /// <param name="value">The attribute's value.</param>
	    public override void WriteAttribute(string name, string value)
	    {
	        CheckDisposed();

	        if(_nestedTokens.Top.Closed)
	            throw new HtmlException(RD.GetString("orphanAttr"));

	        _writer.Write(' ');
	        _writer.Write(name);
	        _writer.Write('=');
	        _writer.Write('\"');
	        InternalWriteText(value);
	        _writer.Write('\"');
	    }

	    private void InternalWriteText(string text)
	    {
	        for(int i = 0; i < text.Length; i++)
	        {
	            _writer.Write(Entity.MapCharToEntity(text[i]));
	        }
	    }

	    /// <summary>
	    /// Writes the text verbatim to the output stream.
	    /// </summary>
	    /// <param name="text">The text to write to the writer.</param>
	    public override void WriteText(string text)
	    {
	        CheckClosed();

            InternalWriteText(text);
	    }
	}
}
