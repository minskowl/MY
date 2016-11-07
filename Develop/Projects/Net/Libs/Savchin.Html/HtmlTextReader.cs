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
using System.Collections.Specialized;
using System.IO;
using System.Text;

namespace Savchin.Html
{
	/// <summary>
	/// A non-validating, forward-only HTML text reader.
	/// </summary>
	public class HtmlTextReader: HtmlReader, IDisposable
	{
		// Enumerator state
		private enum State
		{
			Initial,
			Tag,
			Text,
			Whitespace
		}

	    private TextReader _reader;
	    private StringBuilder _buffer;
		private char _curChar;
		private State _state;
	    private int _lineNumber;
	    private TokenStack _nestedTokens;
	    private bool _eof;

		/// <summary>
		/// Initializes all fields to default values
		/// </summary>
		/// <param name="reader">The text stream to read data from.</param>
		public HtmlTextReader(TextReader reader)
		{
		    _reader = reader;
		    _buffer = new StringBuilder(265);
			_curChar = '\0';
		    _state = State.Initial;
		    _lineNumber = 1;
		    _nestedTokens = new TokenStack();
		    _eof = false;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="HtmlTextReader"/> with
		/// the source <see cref="Stream"/>.
		/// </summary>
		/// <param name="source">The <see cref="Stream"/> to read HTML
		/// text from.</param>
		public HtmlTextReader(Stream source):
		    this(new StreamReader(source))
		{}

		/// <summary>
		/// Initializes a new instance of the <see cref="HtmlTextReader"/> with
		/// the name of the source file.
		/// </summary>
		/// <param name="fileName">The file name.</param>
		public HtmlTextReader(string fileName):
		    this(new StreamReader(fileName))
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
		    if(null == _reader)
		        throw new ObjectDisposedException("HtmlTextReader", RD.GetString("htrDisposed"));
		}

		/// <summary>
        /// Releases all the resource associated with the object.
        /// </summary>
        /// <param name="disposing"><c>true</c> to release both managed and
        /// unmanaged resources; <c>false</c> to release only unmanaged
        /// resources.</param>
        /// <remarks>
        /// <para>This method actually disposes of the object. The methods
        /// <see cref="Dispose()"/> and <see cref="Close()"/> simply call
        /// this method.</para>
        /// <para>Because <see cref="HtmlTextReader"/> does not contains
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
    		    if(null != _reader)
    		    {
	    	        _reader.Close();
	    	        _reader = null;
    		    }
	        }
	    }

		/// <summary>
		/// The number of the attributes in the current node.
		/// </summary>
		/// <exception cref="ObjectDisposedException">
		/// The <see cref="HtmlTextReader"/> object had been disposed.
		/// </exception>
		public override int AttributeCount
		{
			get
			{
			    CheckDisposed();

				return _nestedTokens.Top.Attributes.Count;
			}
		}

		/// <summary>
		/// The depth of the current node. NB: The depth of the root node is
		/// equal 0.
		/// </summary>
		/// <exception cref="ObjectDisposedException">
		/// The <see cref="HtmlTextReader"/> object had been disposed.
		/// </exception>
		public override int Depth
		{
			get
			{
			    CheckDisposed();

				return _nestedTokens.Count;
			}
		}

		/// <summary>
		/// Tells if the end of the underlying stream is reached or not.
		/// </summary>
		public override bool EOF
		{
			get
			{
				CheckDisposed();

				return _eof;
			}
		}

		/// <summary>
		/// TODO - add property description
		/// </summary>
		/// <exception cref="ObjectDisposedException">
		/// The <see cref="HtmlTextReader"/> object had been disposed.
		/// </exception>
		public override bool HasAttributes
		{
			get
			{
			    CheckDisposed();

		        return _nestedTokens.Top.Attributes.Count > 0;
			}
		}

		/// <summary>
		/// TODO - add property description
		/// </summary>
		public override bool HasValue
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>
		/// TODO - add property description
		/// </summary>
		public override bool IsEmptyElement
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>
		/// Gets the value of an attribute by its index.
		/// </summary>
		/// <param name='index'>The attribute's index.</param>
		/// <exception cref="ObjectDisposedException">
		/// The <see cref="HtmlTextReader"/> object had been disposed.
		/// </exception>
		/// <exception cref="IndexOutOfRangeException">
		/// The <paramref name="index"/> is out of range.
		/// </exception>
		/// <remarks>
		/// If the tag does not have attributes an exception occurs.
		/// </remarks>
		public override string this[int index]
		{
			get
			{
			    CheckDisposed();

			    if(index < 0 || index >= _nestedTokens.Top.Attributes.Count)
			        throw new IndexOutOfRangeException(RD.GetString("attrOOR"));

			    return _nestedTokens.Top.Attributes[index];
			}
		}

		/// <summary>
		/// Gets the value of an attribute by its name.
		/// </summary>
		/// <param name='index'>The attribute's name.</param>
		/// <exception cref="ObjectDisposedException">
		/// The <see cref="HtmlTextReader"/> object had been disposed.
		/// </exception>
		/// <exception cref="NotSupportedException">
		/// Thrown if the tag has no attributes.
		/// </exception>
		public override string this[string index]
		{
			get
			{
			    CheckDisposed();

			    return _nestedTokens.Top.Attributes[index];
			}
		}

		/// <summary>
		/// TODO - add property description
		/// </summary>
		/// <exception cref="ObjectDisposedException">
		/// The <see cref="HtmlTextReader"/> object had been disposed.
		/// </exception>
		public override string Name
		{
			get
			{
			    CheckDisposed();

		        return _nestedTokens.Top.Name;
			}
		}

		/// <summary>
		/// TODO - add property description
		/// </summary>
		/// <exception cref="ObjectDisposedException">
		/// The <see cref="HtmlTextReader"/> object had been disposed.
		/// </exception>
		public override HtmlNodeType NodeType
		{
			get
			{
			    CheckDisposed();

				return _nestedTokens.Top.Type;
			}
		}

		/// <summary>
		/// TODO - add property description
		/// </summary>
		public override char QuoteChar
		{
			get
			{
				throw new System.NotImplementedException();
			}
		}

		/// <summary>
		/// TODO:
		/// </summary>
		/// <exception cref="ObjectDisposedException">
		/// The <see cref="HtmlTextReader"/> object had been disposed.
		/// </exception>
		public override string Value
		{
		    get
		    {
			    CheckDisposed();

	            return _nestedTokens.Top.Value;
		    }
		}

		private void DropStackToTag()
		{
			if(HtmlNodeType.EndTag == _nestedTokens.Top.Type)
			{
				Token token = _nestedTokens.Pop();
				while((HtmlNodeType.Tag != _nestedTokens.Top.Type) &&
				      (0 != String.Compare(_nestedTokens.Top.Name, token.Name, true)))
				      _nestedTokens.Pop();
				_nestedTokens.Pop();
			}
		}

        private void StackCheck(Token token)
        {
        	DropStackToTag();

            switch(token.Type)
            {
                case HtmlNodeType.ClosedTag:
                case HtmlNodeType.Comment:
                case HtmlNodeType.Text:
                case HtmlNodeType.Whitespace:
                {
                    if(HtmlNodeType.Tag == _nestedTokens.Top.Type)
                        _nestedTokens.Push(token);
                    else
                        _nestedTokens.Swap(token);
                    break;
                }
                case HtmlNodeType.Tag:
                {
                    if(!Tag.IsAtomic(token.Name))
                        _nestedTokens.Push(token);
                    else
                        _nestedTokens.Swap(token);
                    break;
                }
                case HtmlNodeType.EndTag:
                {
					_nestedTokens.Push(token);
                    break;
                }
                default:
                    throw new HtmlException(RD.GetString("tagUnknown"));
            }
        }

        private void SetToken(bool isTag)
		{
		    string rawToken = _buffer.ToString();

		    if(isTag)
		    {
		        // Document type tag
		        if(Token.IsDocumentType(rawToken))
		        {
		        	StackCheck(Token.ParseDocumentType(rawToken));
		        	return;
		        }

		        // Comment tag
				if(Token.IsComment(rawToken))
				{
		        	StackCheck(Token.ParseComment(rawToken));
		        	return;
				}

		    	// End tag
		    	if(Token.IsEndTag(rawToken))
				{
		        	StackCheck(Token.ParseEndTag(rawToken));
		        	return;
				}

		        // Closed tag (XHTML-like)
		        if(Token.IsXHTMLTag(rawToken))
				{
		        	StackCheck(Token.ParseXHTMLTag(rawToken));
		        	return;
				}

		        // Tag
		        if(Token.IsClosedTag(rawToken))
			        StackCheck(Token.ParseTag(rawToken, HtmlNodeType.ClosedTag));
		        else
			        StackCheck(Token.ParseTag(rawToken, HtmlNodeType.Tag));
		    }
		    else
		    {
		    	if(rawToken.Trim().Length > 0)
		        	StackCheck(new Token(rawToken, HtmlNodeType.Text));
		        else
		        	StackCheck(new Token(rawToken, HtmlNodeType.Whitespace));
		    }
		}

        private void FlipState()
        {
        	if(State.Tag == _state)
        		_state = State.Text;
        	else
        		_state = State.Tag;
        }

        private bool ProcessChar(char current, char stop)
        {
        	if(stop == current)
        	{
        		FlipState();
        		// If an empty tag, continue parsing
        		if(0 == _buffer.Length)
        			return true;
        		// Otherwise create a token an break the circle
        		SetToken(_state != State.Tag);
        		return false;
        	}
        	_buffer.Append(current);
        	return true;
        }

        private bool StateMachine(char current)
        {
        	switch(_state)
        	{
        		case State.Initial:
					if(Char.IsWhiteSpace(_curChar) || ('<' != _curChar))
					    _state = State.Text;
					else
				    	_state = State.Tag;
					return true;
        		case State.Tag:
					return ProcessChar(current, '>');
        		case State.Text:
					return ProcessChar(current, '<');
        	}
        	throw new InvalidOperationException();
        }

        /// <summary>
		/// TODO - add method description
		/// </summary>
		/// <exception cref="ObjectDisposedException">
		/// The <see cref="HtmlTextReader"/> object had been disposed.
		/// </exception>
		/// <exception cref="HtmlException">
		/// A parsing error occured.
		/// </exception>
		public override bool Read()
		{
		    CheckDisposed();

			int read;

			_buffer.Length = 0;
			while(-1 != (read = _reader.Read()))
			{
				_curChar = (char)read;

				if(_curChar == '\n')
					_lineNumber++;

				if(!StateMachine(_curChar))
					return true;
			}

			// Unmatched tag
        	DropStackToTag();
			if(0 != _nestedTokens.Count)
				throw new HtmlException(RD.GetStringFormat("tagMismatch",
				                               _buffer.ToString(),
        	                            	   _lineNumber));

			_eof = true;
			return false;
	    }

		/// <summary>
		/// TODO - add method description
		/// </summary>
		/// <exception cref="ObjectDisposedException">
		/// The <see cref="HtmlTextReader"/> object had been disposed.
		/// </exception>
		public override bool MoveToNextAttribute()
		{
			CheckDisposed();

			return _nestedTokens.Top.MoveToNextAttribute();
		}

		/// <summary>
		/// TODO - add method description
		/// </summary>
		/// <exception cref="ObjectDisposedException">
		/// The <see cref="HtmlTextReader"/> object had been disposed.
		/// </exception>
		public override bool MoveToFirstAttribute()
		{
			CheckDisposed();

			return _nestedTokens.Top.MoveToFirstAttribute();
		}

		/// <summary>
		/// TODO - add method description
		/// </summary>
		/// <exception cref="ObjectDisposedException">
		/// The <see cref="HtmlTextReader"/> object had been disposed.
		/// </exception>
		public override bool MoveToElement()
		{
			CheckDisposed();

			_nestedTokens.Top.MoveToElement();
			return true;
		}

		/// <summary>
		/// TODO - add method description
		/// </summary>
		/// <exception cref="ObjectDisposedException">
		/// The <see cref="HtmlTextReader"/> object had been disposed.
		/// </exception>
		public override HtmlNodeType MoveToContent()
		{
			CheckDisposed();

			_nestedTokens.Top.MoveToElement();
			return _nestedTokens.Top.Type;
		}

		/// <summary>
		/// TODO - add method description
		/// </summary>
		/// <param name='attrName'>TODO - add parameter description</param>
		/// <exception cref="ObjectDisposedException">
		/// The <see cref="HtmlTextReader"/> object had been disposed.
		/// </exception>
		public override void MoveToAttribute(string attrName)
		{
			CheckDisposed();

			_nestedTokens.Top.MoveToAttribute(attrName);
		}

		/// <summary>
		/// Advances the reader to the attribute by index.
		/// </summary>
		/// <param name='index'>The index of attribute to move the reader to.</param>
		/// <exception cref="ObjectDisposedException">
		/// The <see cref="HtmlTextReader"/> object had been disposed.
		/// </exception>
		/// <exception cref="IndexOutOfRangeException">
		/// The attribute <paramref name="index"/> is out of bounds.
		/// </exception>
		public override void MoveToAttribute(int index)
		{
			CheckDisposed();

			_nestedTokens.Top.MoveToAttribute(index);
		}

		/// <summary>
		/// TODO - add method description
		/// </summary>
		/// <exception cref="ObjectDisposedException">
		/// The <see cref="HtmlTextReader"/> object had been disposed.
		/// </exception>
		public override void Close()
		{
		    CheckDisposed();

		    Dispose();
		}

		/// <summary>
		/// TODO - add method description
		/// </summary>
		public void Dispose()
		{
		    Dispose(true);
		    GC.SuppressFinalize(this);
		}
	}
}
