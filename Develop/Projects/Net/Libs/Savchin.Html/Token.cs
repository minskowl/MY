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
	/// Represents an HTML token read from or being written to a stream. This
	/// class is used internally by <see cref="HtmlTextReader"/> and
	/// <see cref="HtmlTextWriter"/> classes.
	/// </summary>
	/// <seealso cref="HtmlTextReader"/>
	/// <seealso cref="HtmlTextWriter"/>
	internal class Token
	{
   	    private string _name;

        internal HtmlNodeType _type;

   	    private AttributeTable _attributes = null;

   	    private int _curAttr = -1;

   	    /// <summary>
   	    /// Initializes the new instance of <see cref="Token"/> to be used in
   	    /// <see cref="HtmlTextWriter"/>.
   	    /// </summary>
   	    /// <param name="name">The name of the token.</param>
   	    /// <seealso cref="HtmlTextWriter"/>
   	    internal Token(string name):
   	    	this(name, HtmlNodeType.None)
        {}

        /// <summary>
        /// Initializes the new instance of <see cref="Token"/> to be used in
        /// <see cref="HtmlTextReader"/>.
        /// </summary>
        /// <param name="name">The name of the token.</param>
        /// <param name="type">The type of the token.</param>
        /// <seealso cref="HtmlTextReader"/>
        /// <seealso cref="HtmlNodeType"/>
   	    internal Token(string name, HtmlNodeType type)
        {
        	this._curAttr = _curAttr;
        	this._name = name;
        	this._type = type;
        }

        internal static readonly Token Null = new Token(String.Empty, HtmlNodeType.ClosedTag);

        internal HtmlNodeType Type
        {
        	get
        	{
        		return _type;
        	}
        }

        internal bool Closed
        {
        	get
        	{
        		return _type == HtmlNodeType.ClosedTag;
        	}

        	set
        	{
        		_type = HtmlNodeType.ClosedTag;
        	}
        }

   	    internal AttributeTable Attributes
   	    {
   	    	get
   	    	{
   	    		if(null == _attributes)
   	    		{
   	    			_attributes = new AttributeTable();
   	    		}
   	    		return _attributes;
   	    	}
   	    }

        internal int CurrentAttribute
        {
        	get
        	{
        		return _curAttr;
        	}
        }

        internal void MoveToContents()
        {
        	_curAttr = -1;
        }

        internal void MoveToElement()
        {
        	_curAttr = -1;
        }

        internal bool MoveToFirstAttribute()
        {
        	if(Attributes.Count > 0)
        	{
        		_curAttr = 0;
        		return true;
        	}
        	return false;
        }

        internal bool MoveToLasAttribute()
        {
        	if(Attributes.Count > 0)
        	{
        		_curAttr = Attributes.Count - 1;
        		return true;
        	}
        	return false;
        }

        internal bool MoveToNextAttribute()
        {
        	if((Attributes.Count > 0) && (_curAttr < Attributes.Count - 1))
        	{
        		_curAttr++;
        		return true;
        	}
        	return false;
        }

        internal bool MoveToPrevAttribute()
        {
        	if((Attributes.Count > 0) && (_curAttr > 0))
        	{
        		_curAttr++;
        		return true;
        	}
        	return false;
        }

        internal bool MoveToAttribute(int index)
        {
        	if(index < Attributes.Count)
        	{
        		_curAttr = index;
        		return true;
        	}
        	return false;
        }

        internal bool MoveToAttribute(string attrName)
        {
        	int index = Attributes.Keys.IndexOf(attrName);
        	if(-1 == index)
        		throw new HtmlException(RD.GetString("badAttr"));
        	return MoveToAttribute(index);
        }

        internal string Name
        {
        	get
        	{
        		if(-1 == _curAttr)
        			return _name;
        		return Attributes.Keys[_curAttr];
        	}
        }

        internal string Value
        {
        	get
        	{
        		if(-1 == _curAttr)
        			return _name;
        		return Attributes[_curAttr];
        	}
        }

#region Comment token
        private const string CmtTypeTag = "!--";

	    private const string CmtTypeEndTag = "--";

        internal static bool IsComment(string rawToken)
        {
        	return rawToken.StartsWith(CmtTypeTag) && rawToken.EndsWith(CmtTypeEndTag);
        }

	    internal static Token ParseComment(string rawToken)
        {
        	string contents = rawToken.Substring(CmtTypeTag.Length, rawToken.Length - CmtTypeTag.Length - CmtTypeEndTag.Length);
            return new Token(contents, HtmlNodeType.Comment);
        }
#endregion

#region Document type
		private const string DocTypeTag = "!DOCTYPE";

	    internal static bool IsDocumentType(string rawToken)
	    {
	    	return rawToken.StartsWith(DocTypeTag);
	    }

	    internal static Token ParseDocumentType(string rawToken)
	    {
        	string contents = "";
            return new Token(contents, HtmlNodeType.DocumentType);
	    }
#endregion

#region End tag
		internal static bool IsEndTag(string rawToken)
	    {
	    	return '/' == rawToken[0];
	    }

	    private static readonly char[] Spaces = {' ', '\t'};

	    internal static Token ParseEndTag(string rawToken)
	    {
            if(rawToken[rawToken.Length - 1] == '/')
                throw new HtmlException(RD.GetString("orphanET"));

            string contents = rawToken.Substring(1).Trim();
			if(-1 != contents.IndexOfAny(Spaces))
                throw new HtmlException(RD.GetString("etHasSpaces"));

            return new Token(contents, HtmlNodeType.EndTag);
	    }
#endregion

#region XHTML closed tag
		internal static bool IsXHTMLTag(string rawToken)
		{
			return '/' == rawToken[rawToken.Length - 1];
		}

		internal static Token ParseXHTMLTag(string rawToken)
		{
            string contents = rawToken.Substring(0, rawToken.Length - 2).Trim();
            return Token.ParseTag(contents, HtmlNodeType.ClosedTag);
		}
#endregion

#region Tag
		private static string ExtractTagName(string rawToken)
		{
			int i = 0;

        	// Extract tag name
        	for(; i < rawToken.Length; i++)
        		if(Char.IsWhiteSpace(rawToken[i]))
        			break;

        	return rawToken.Substring(0, i);
		}

		internal static bool IsClosedTag(string rawToken)
		{
        	return Tag.IsAtomic(ExtractTagName(rawToken));
		}

		internal static Token ParseTag(string rawToken, HtmlNodeType type)
		{
        	int pos;
        	string name, val;
        	char terminator;

        	int i = 0;
            // Extract tag name
        	for(; i < rawToken.Length; i++)
        		if(Char.IsWhiteSpace(rawToken[i]))
        		break;
        	name = rawToken.Substring(0, i);

            Token result = new Token(name, type);

        	bool condition = true;
        	while(condition)
        	{
        		// Skip wihitespace
        		for(; i < rawToken.Length; i++)
        			if(!Char.IsWhiteSpace(rawToken[i]))
        			break;
        		pos = i;
        		// Parse 'till the assignement operator
        		for(; i < rawToken.Length; i++)
        			if('=' == rawToken[i])
        			break;
        		name = rawToken.Substring(pos, i - pos);

        		if(++i >= rawToken.Length)
        		{
        			condition = false;
        			continue;
        		}

        		if((rawToken[i] == '\"') ||
        		   (rawToken[i] == '\''))
        		{
        			terminator = rawToken[i++];
        			pos = i;
        			for(; i < rawToken.Length; i++)
        				if(terminator == rawToken[i])
        				break;
        		}
        		else
        		{
        			pos = i;
        			for(;i < rawToken.Length; i++)
        				if(Char.IsWhiteSpace(rawToken[i]))
        				break;
        		}
        		val = rawToken.Substring(pos, i - pos);
        		// NB: attribute's value is being decoded at this point!
        		result.Attributes[name] = Entity.MapEntitiesToChars(val);
        		if(++i >= rawToken.Length)
        		{
        			condition = false;
        			continue;
        		}
        	}

        	return result;
		}
#endregion

        /// <summary>
        /// Parses an HTML tag text with attributes.
        /// </summary>
        /// <param name="htmlFragment">The text fragment to parse.</param>
/*        private void Parse(string rawToken)
        {
        	int i, pos;
        	string name, val;
        	char terminator;

        	// Extract tag name
        	for(i = 1; i < rawToken.Length; i++)
        		if(Char.IsWhiteSpace(rawToken[i]))
        		break;
        	_name = rawToken.Substring(0, i);

        	bool condition = true;
        	while(condition)
        	{
        		// Skip wihitespace
        		for(; i < rawToken.Length; i++)
        			if(!Char.IsWhiteSpace(rawToken[i]))
        			break;
        		pos = i;
        		// Parse 'till the assignement operator
        		for(; i < rawToken.Length; i++)
        			if('=' == rawToken[i])
        			break;
        		name = rawToken.Substring(pos, i - pos);

        		if(++i >= rawToken.Length)
        		{
        			condition = false;
        			continue;
        		}

        		if((rawToken[i] == '\"') ||
        		   (rawToken[i] == '\''))
        		{
        			terminator = rawToken[i++];
        			pos = i;
        			for(; i < rawToken.Length; i++)
        				if(terminator == rawToken[i])
        				break;
        		}
        		else
        		{
        			pos = i;
        			for(;i < rawToken.Length; i++)
        				if(Char.IsWhiteSpace(rawToken[i]))
        				break;
        		}
        		val = rawToken.Substring(pos, i - pos);
        		// NB: attribute's value is being decoded at this point!
        		Attributes[name] = Entity.MapEntitiesToChars(val);
        		if(++i >= rawToken.Length)
        		{
        			condition = false;
        			continue;
        		}
        	}
        }*/
	}
}
