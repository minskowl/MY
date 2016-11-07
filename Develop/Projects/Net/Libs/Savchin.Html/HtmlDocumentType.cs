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
	/// An HTML document type.
	/// </summary>
	public class HtmlDocumentType: HtmlNode
	{
		private struct Ver
		{
			internal short major;
			internal short minor;
			internal string format;
			internal HtmlDtdType dt;
			internal string dtd;

			public Ver(short major, short minor, string format, HtmlDtdType dt, string dtd)
			{
				this.major = major;
				this.minor = minor;
				this.format = format;
				this.dt = dt;
				this.dtd = dtd;
			}
		}

		private static string IETF = "-//IETF//DTD HTML//EN";
		private static string W3C = "-//W3C//DTD HTML {0} {1}//EN";

		private static Ver[] vers = {
			new Ver(2, -1, IETF, HtmlDtdType.None, 			String.Empty),
			new Ver(3, -1, W3C,  HtmlDtdType.None, 			String.Empty),
			new Ver(4, -1, W3C,  HtmlDtdType.Strict, 		"http://www.w3.org/TR/html4/strict.dtd"),
			new Ver(4, -1, W3C,  HtmlDtdType.Transitional, 	"http://www.w3.org/TR/html4/loose.dtd"),
			new Ver(4, -1, W3C,  HtmlDtdType.Frameset, 		"http://www.w3.org/TR/html4/frameset.dtd")
		};

	    private short _major;
	    private short _minor;
	    private HtmlDtdType _dtd;


	    /// <summary>
	    /// Initializes a new instance of <see cref="HtmlDocumentType"/>
	    /// with HTML _version string and DTD URL.
	    /// </summary>
	    /// <param name="major">
	    /// The major version number.
	    /// </param>
	    /// <param name="minor">
	    /// The minor version number.
	    /// </param>
	    /// <param name="dtd">
	    /// The HTML DTD subtype.
	    /// </param>
	    /// <seealso cref="HtmlDtdType"/>
	    public HtmlDocumentType(short major, short minor, HtmlDtdType dtd):
	    	base()
	    {
	    	_major = major;
	    	_minor = minor;
	    	_dtd = dtd;
	    }

	    /// <summary>
	    /// Initializes a new instance of <see cref="HtmlDocumentType"/>.
	    /// </summary>
	    public HtmlDocumentType():
	    	this(3, 2, HtmlDtdType.None)
	    {}

	    /// <summary>
	    /// Initializes a new instance of <see cref="HtmlDocumentType"/>
	    /// with HTML _version string and DTD URL.
	    /// </summary>
	    /// <param name="major">
	    /// The major version number.
	    /// </param>
	    /// <param name="minor">
	    /// The minor version number.
	    /// </param>
	    public HtmlDocumentType(short major, short minor):
	    	this(major, minor, major >= 4 ? HtmlDtdType.Transitional : HtmlDtdType.None)
	    {}

	    /// <summary>
	    /// Creates a copy of this HTML node.
	    /// </summary>
	    /// <returns>The node's copy.</returns>
	    public override object Clone()
	    {
	        HtmlDocumentType result = new HtmlDocumentType(_major, _minor, _dtd);
		    result.SetParentNode(ParentNode);
		    result.SetDocument(Document);
	        return result;
	    }

	    /// <summary>
	    /// The qualified name of the node.
	    /// </summary>
	    /// <value>The "#doctype" text.</value>
	    public override string Name
	    {
	        get
	        {
				return "#doctype";
	        }

	        set
	        {
#if _DEBUG
				throw new NotImplementedException("HtmlDocumentType.Name.set");
#endif
	        }
	    }

	    /// <summary>
	    /// The document type node's type.
	    /// </summary>
	    /// <value>
	    /// Always <see cref="HtmlNodeType.DocumentType"/>.
	    /// </value>
	    /// <seealso cref="HtmlNodeType"/>
	    public override HtmlNodeType Type
	    {
	        get
	        {
	            return HtmlNodeType.DocumentType;
	        }
	    }

	    /// <summary>
	    /// The document type version string.
	    /// </summary>
	    /// <value>
	    /// The textual representation of the document type version.
	    /// </value>
	    public string Version
	    {
	        get
	        {
	        	return _major.ToString() + "." + (0 == _minor ? "0" : _minor.ToString("00"));
	        }

	        set
	        {
	        	int pos = value.IndexOf('.');
	        	_major = Convert.ToInt16(value.Substring(0, pos));
	        	_minor = Convert.ToInt16(value.Substring(pos + 1));
	        }
	    }

	    /// <summary>
	    /// The document type description URL.
	    /// </summary>
	    /// <value>
	    /// An <see cref="string"/> with textual representation of the DTD URL.
	    /// </value>
	    /// <remarks>
	    /// <para>Possible values are:
	    ///	<list type="table">
	    ///	<listheader>
	    ///	<term>Value</term>
	    ///	<description>Description</description>
	    ///	</listheader>
	    ///	<item>
	    ///	<term>http://www.w3.org/TR/html4/strict.dtd</term>
	    ///	<description>HTML 4.0 strict data type definition.</description>
	    ///	</item>
	    ///	<item>
	    ///	<term>http://www.w3.org/TR/html4/loose.dtd</term>
	    ///	<description>HTML 4.0 transitional data type definition.</description>
	    ///	</item>
	    ///	<item>
	    ///	<term>http://www.w3.org/TR/html4/frameset.dtd</term>
	    ///	<description>HTML 4.0 transitional with frames data type definition.</description>
	    ///	</item>
	    /// </list>
	    /// </para>
	    /// </remarks>
	    public string Dtd
	    {
	        get
	        {
	        	for(int i = 0; i < vers.Length; i++)
	        	{
	        		if(_major != vers[i].major)
						continue;
	        		if((-1 != vers[i].minor) && (_minor != vers[i].minor))
	        			continue;
					if(_dtd == vers[i].dt)
						return vers[i].dtd;
	        	}
	        	return String.Empty;
	        }
	    }

	    /// <summary>
	    /// HTML document type's public ID.
	    /// </summary>
	    public string PublicID
	    {
	    	get
	    	{
	        	StringBuilder sb = new StringBuilder(32);

	        	for(int i = 0; i < vers.Length; i++)
	        	{
	        		if(_major != vers[i].major)
						continue;
	        		if((-1 != vers[i].minor) && (_minor != vers[i].minor))
	        			continue;
					if(_dtd == vers[i].dt)
					{
	        			sb.AppendFormat(vers[i].format,
						                Version,
						                vers[i].dt == HtmlDtdType.None ? String.Empty : Enum.GetName(typeof(HtmlDtdType), vers[i].dt));
						break;
					}
	        	}
	        	return sb.ToString();
	    	}
	    }

	    /// <summary>
	    /// The the document type value.
	    /// </summary>
	    /// <value>The text.</value>
	    public override string Value
	    {
	        get
	        {
	            return String.Empty;
	        }
	        set
	        {
#if _DEBUG
				throw new NotImplementedException("HtmlDocumentType.Value.set");
#endif
	        }
	    }

	    /// <summary>
	    /// Normalizes the document type.
	    /// </summary>
	    /// <remarks>
	    /// Current implementation does nothing.
	    /// </remarks>
	    public override void Normalize()
	    {}

	    /// <summary>
	    /// Returns a textual representation of an <see cref="HtmlDocumentType"/>.
	    /// </summary>
	    /// <returns>
	    /// The textual representation of an <see cref="HtmlDocumentType"/>.
	    /// </returns>
	    public override string ToString()
	    {
	        StringBuilder sb = new StringBuilder(32);

	        Ver verInfo = vers[0];

			for(int i = 0; i < vers.Length; i++)
	        {
	        	if(_major != vers[i].major)
					continue;
	        	if((-1 != vers[i].minor) && (_minor != vers[i].minor))
	        		continue;
				if(_dtd == vers[i].dt)
				{
					verInfo = vers[i];
					break;
				}
	        }

	        sb.Append("<!DOCTYPE HTML PUBLIC \"");
	        sb.AppendFormat(PublicID);
            sb.Append('\"');
            if(verInfo.dtd.Length > 0)
            {
                sb.Append(" \"");
                sb.Append(Dtd);
                sb.Append('\"');
            }
            sb.Append('>');

	        return sb.ToString();
	    }
	}
}
