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

namespace Savchin.Html
{
	/// <summary>
	/// Represents the stack of nested HTML tokens. This class is used
	/// internally by <see cref="HtmlTextReader"/> and
	/// <see cref="HtmlTextWriter"/>.
	/// </summary>
	/// <seealso cref="HtmlTextReader"/>
	/// <seealso cref="HtmlTextWriter"/>
	internal class TokenStack
	{
		private Stack _tokens;

		/// <summary>
		/// Creates and initializes a new instance of <see cref="TokenStack"/>
		/// class.
		/// </summary>
		internal TokenStack()
		{
			this._tokens = new Stack();
		}

		/// <summary>
		/// Put a <see cref="Token"/> to the top of the stack.
		/// </summary>
		/// <param name="token">THe token to push.</param>
		internal void Push(Token token)
		{
			_tokens.Push(token);
		}

		/// <summary>
		/// Take a <see cref="Token"/> off the top of the stack.
		/// </summary>
		/// <returns>
		/// A <see cref="Token"/> that was on the top of the stack.
		/// </returns>
		internal Token Pop()
		{
			return (Token)_tokens.Pop();
		}

		/// <summary>
		/// Peek at the <see cref="Token"/> at the top of the stack.
		/// </summary>
		/// <value>
		/// A <see cref="Token"/> at the top of the stack.
		/// </value>
		internal Token Top
		{
			get
			{
				if(0 == _tokens.Count)
					return Token.Null;
				return (Token)_tokens.Peek();
			}
		}

		/// <summary>
		/// Swap the <paramref name="token"/> with the <see cref="Token"/> at
		/// the top of the stack.
		/// </summary>
		/// <param name="token">The <see cref="Token"/> to put at the top of
		/// the stack.</param>
		/// <returns>
		/// A <see cref="Token"/> that was on the top of the stack.
		/// </returns>
		internal Token Swap(Token token)
		{
			Token result = null;
			if(0 != _tokens.Count)
				result = Pop();
			Push(token);
			return result;
		}

		internal int Count
		{
			get
			{
				return _tokens.Count;
			}
		}
	}
}
