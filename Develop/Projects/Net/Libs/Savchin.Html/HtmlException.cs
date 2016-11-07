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
using System.Runtime.Serialization;

namespace Savchin.Html
{
	/// <summary>
	/// Returns detailed information about the last HTML parse exception.
	/// </summary>
	[Serializable]
	public class HtmlException : System.Exception
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="HtmlException"/>
		/// class.
		/// </summary>
		public HtmlException()
		{
		}

		/// <summary>
		///Initializes a new instance of the <see cref="HtmlException"/>
		/// class with a specified error message.
		/// </summary>
		/// <param name="message">The message that describes the error.</param>
		public HtmlException(string message): base(message)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="HtmlException"/>
		/// class with serialized data.
		/// </summary>
		/// <param name="info">The <see cref="SerializationInfo"/> that
		/// holds the serialized object data about the exception being
		/// thrown.</param>
		/// <param name="context">The <see cref="StreamingContext"/>
		/// that contains contextual information about the source or
		/// destination.</param>
		public HtmlException(SerializationInfo info, StreamingContext context):
		    base(info, context)
		{
		}

		/// <summary>
		/// Initializes a new instance of the Exception class with
		/// a specified error message and a reference to the inner
		/// exception that is the cause of this exception.
		/// </summary>
		/// <param name="message">The error message that explains
		/// the reason for the exception.</param>
		/// <param name="innerException">The exception that is the cause
		/// of the current exception. If the innerException parameter
		/// is not a null reference (Nothing in Visual Basic),
		/// the current exception is raised in a catch block that
		/// handles the inner exception.</param>
		public HtmlException(string message, Exception innerException):
		    base(message, innerException)
		{
		}
	}
}
