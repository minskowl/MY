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
	/// HTML tag operations and rules.
	/// </summary>
	internal sealed class Tag
	{
#region Valid tags

		/// <summary>
		/// The anchor. Marks either a hypelink or a hypelink's target.
		/// </summary>
		internal const string A			= "a";

		/// <summary>
		/// TODO: Add tag description.
		/// </summary>
		internal const string Abbr		= "abbr";

		/// <summary>
		/// TODO: Add tag description.
		/// </summary>
		internal const string Acronym		= "acronym";

		/// <summary>
		/// TODO: Add tag description.
		/// </summary>
		internal const string Address		= "address";

		/// <summary>
		/// TODO: Add tag description.
		/// </summary>
		internal const string Align		= "align";

		/// <summary>
		/// <para>Java applet container tag.</para>
		/// <para>See &lt;<see cref="Tag.Param"/>&gt;.</para>
		/// </summary>
		internal const string Applet		= "applet";

		/// <summary>
		/// TODO: Add tag description.
		/// </summary>
		internal const string Area		= "area";

		/// <summary>
		/// TODO: Add tag description.
		/// </summary>
		internal const string B			= "b";

		/// <summary>
		/// TODO: Add tag description.
		/// </summary>
		internal const string Base		= "base";

		/// <summary>
		/// TODO: Add tag description.
		/// </summary>
		internal const string Basefont	= "basefont";

		/// <summary>
		/// TODO: Add tag description.
		/// </summary>
		internal const string Bdo			= "bdo";

		/// <summary>
		/// TODO: Add tag description.
		/// </summary>
		internal const string Bgsound		= "bgsound";

		/// <summary>
		/// TODO: Add tag description.
		/// </summary>
		internal const string Big			= "big";

		/// <summary>
		/// TODO: Add tag description.
		/// </summary>
		internal const string Blink		= "blink";

		/// <summary>
		/// TODO: Add tag description.
		/// </summary>
		internal const string Blockquote	= "blockquote";

		/// <summary>
		/// TODO: Add tag description.
		/// </summary>
		internal const string Body		= "body";

		/// <summary>
		/// TODO: Add tag description.
		/// </summary>
		internal const string Br			= "br";

		/// <summary>
		/// TODO: Add tag description.
		/// </summary>
		internal const string Button		= "button";

		/// <summary>
		/// TODO: Add tag description.
		/// </summary>
		internal const string Caption		= "caption";

		/// <summary>
		/// TODO: Add tag description.
		/// </summary>
		internal const string Center		= "center";

		/// <summary>
		/// TODO: Add tag description.
		/// </summary>
		internal const string Cite		= "cite";

		/// <summary>
		/// TODO: Add tag description.
		/// </summary>
		internal const string Code		= "code";

		/// <summary>
		/// TODO: Add tag description.
		/// </summary>
		internal const string Col			= "col";

		/// <summary>
		/// TODO: Add tag description.
		/// </summary>
		internal const string Colgroup	= "colgroup";

		/// <summary>
		/// TODO: Add tag description.
		/// </summary>
		internal const string Comment		= "comment";

		/// <summary>
		/// TODO: Add tag description.
		/// </summary>
		internal const string Dd			= "dd";

		/// <summary>
		/// TODO: Add tag description.
		/// </summary>
		internal const string Del			= "del";

		/// <summary>
		/// TODO: Add tag description.
		/// </summary>
		internal const string Dfn			= "dfn";

		/// <summary>
		/// TODO: Add tag description.
		/// </summary>
		internal const string Dir			= "dir";

		/// <summary>
		/// TODO: Add tag description.
		/// </summary>
		internal const string Div			= "div";

		/// <summary>
		/// TODO: Add tag description.
		/// </summary>
		internal const string Dl			= "dl";

		/// <summary>
		/// TODO: Add tag description.
		/// </summary>
		internal const string Dt			= "dt";

		/// <summary>
		/// TODO: Add tag description.
		/// </summary>
		internal const string Em			= "em";

		/// <summary>
		/// <para>Plugin embedding tag.</para>
		/// <para>See &lt;<see cref="Applet"/>&gt;,
		/// &lt;<see cref="Bgsound"/>&gt;
		/// &lt;<see cref="Object"/>&gt;</para>
		/// </summary>
		internal const string Embed		= "embed";

		/// <summary>
		/// TODO: Add tag description.
		/// </summary>
		internal const string Fieldset	= "fieldset";

		/// <summary>
		/// TODO: Add tag description.
		/// </summary>
		internal const string Font		= "font";

		/// <summary>
		/// TODO: Add tag description.
		/// </summary>
		internal const string Form		= "form";

		/// <summary>
		/// TODO: Add tag description.
		/// </summary>
		internal const string Frame		= "frame";

		/// <summary>
		/// TODO: Add tag description.
		/// </summary>
		internal const string Frameset	= "frameset";

		/// <summary>
		/// TODO: Add tag description.
		/// </summary>
		internal const string H1			= "h1";

		/// <summary>
		/// TODO: Add tag description.
		/// </summary>
		internal const string H2			= "h2";

		/// <summary>
		/// TODO: Add tag description.
		/// </summary>
		internal const string H3			= "h3";

		/// <summary>
		/// TODO: Add tag description.
		/// </summary>
		internal const string H4			= "h4";

		/// <summary>
		/// TODO: Add tag description.
		/// </summary>
		internal const string H5			= "h5";

		/// <summary>
		/// TODO: Add tag description.
		/// </summary>
		internal const string H6			= "h6";

		/// <summary>
		/// TODO: Add tag description.
		/// </summary>
		internal const string Head		= "head";

		/// <summary>
		/// TODO: Add tag description.
		/// </summary>
		internal const string Hr			= "hr";

		/// <summary>
		/// TODO: Add tag description.
		/// </summary>
		internal const string Html		= "html";

		/// <summary>
		/// TODO: Add tag description.
		/// </summary>
		internal const string I			= "i";

		/// <summary>
		/// TODO: Add tag description.
		/// </summary>
		internal const string Iframe		= "iframe";

		/// <summary>
		/// TODO: Add tag description.
		/// </summary>
		internal const string Ilayer		= "ilayer";

		/// <summary>
		/// TODO: Add tag description.
		/// </summary>
		internal const string Img			= "img";

		/// <summary>
		/// TODO: Add tag description.
		/// </summary>
		internal const string Input		= "input";

		/// <summary>
		/// TODO: Add tag description.
		/// </summary>
		internal const string Ins			= "ins";

		/// <summary>
		/// TODO: Add tag description.
		/// </summary>
		internal const string Isindex		= "isindex";

		/// <summary>
		/// TODO: Add tag description.
		/// </summary>
		internal const string Kbd			= "kbd";

		/// <summary>
		/// TODO: Add tag description.
		/// </summary>
		internal const string Keygen		= "keygen";

		/// <summary>
		/// TODO: Add tag description.
		/// </summary>
		internal const string Label		= "label";

		/// <summary>
		/// TODO: Add tag description.
		/// </summary>
		internal const string Layer		= "layer";

		/// <summary>
		/// TODO: Add tag description.
		/// </summary>
		internal const string Legend		= "legend";

		/// <summary>
		/// TODO: Add tag description.
		/// </summary>
		internal const string Li			= "li";

		/// <summary>
		/// TODO: Add tag description.
		/// </summary>
		internal const string Link		= "link";

		/// <summary>
		/// TODO: Add tag description.
		/// </summary>
		internal const string Listing		= "listing";

		/// <summary>
		/// TODO: Add tag description.
		/// </summary>
		internal const string Map			= "map";

		/// <summary>
		/// TODO: Add tag description.
		/// </summary>
		internal const string Marquee		= "marquee";

		/// <summary>
		/// TODO: Add tag description.
		/// </summary>
		internal const string Menu		= "menu";

		/// <summary>
		/// TODO: Add tag description.
		/// </summary>
		internal const string Meta		= "meta";

		/// <summary>
		/// TODO: Add tag description.
		/// </summary>
		internal const string Multicol	= "multicol";

		/// <summary>
		/// TODO: Add tag description.
		/// </summary>
		internal const string Nobr		= "nobr";

		/// <summary>
		/// TODO: Add tag description.
		/// </summary>
		internal const string Noembed		= "noembed";

		/// <summary>
		/// TODO: Add tag description.
		/// </summary>
		internal const string Noframes	= "noframes";

		/// <summary>
		/// TODO: Add tag description.
		/// </summary>
		internal const string Nolayer		= "nolayer";

		/// <summary>
		/// TODO: Add tag description.
		/// </summary>
		internal const string Nosave		= "nosave";

		/// <summary>
		/// TODO: Add tag description.
		/// </summary>
		internal const string Noscript	= "noscript";

		/// <summary>
		/// <para>Object tag. In the past used for ActiveX objects embedding.
		/// In HTML 4.0 supercedes &lt;<see cref="Img"/>&gt; tag and other
		/// multimedia tags.</para>
		/// <para>See &lt;<see cref="Applet"/>&gt;,
		/// &lt;<see cref="Bgsound"/>&gt;,
		/// &lt;<see cref="Applet"/>&gt;,
		/// &lt;<see cref="Param"/>&gt;.</para>
		/// </summary>
		internal const string Object		= "object";

		/// <summary>
		/// TODO: Add tag description.
		/// </summary>
		internal const string Ol			= "ol";

		/// <summary>
		/// TODO: Add tag description.
		/// </summary>
		internal const string Optgroup	= "optgroup";

		/// <summary>
		/// TODO: Add tag description.
		/// </summary>
		internal const string Option		= "option";

		/// <summary>
		/// TODO: Add tag description.
		/// </summary>
		internal const string P			= "p";

		/// <summary>
		/// TODO: Add tag description.
		/// </summary>
		internal const string Param		= "param";

		/// <summary>
		/// TODO: Add tag description.
		/// </summary>
		internal const string Plaintext	= "plaintext";

		/// <summary>
		/// TODO: Add tag description.
		/// </summary>
		internal const string Pre			= "pre";

		/// <summary>
		/// TODO: Add tag description.
		/// </summary>
		internal const string Q			= "q";

		/// <summary>
		/// TODO: Add tag description.
		/// </summary>
		internal const string Rb			= "rb";

		/// <summary>
		/// TODO: Add tag description.
		/// </summary>
		internal const string Rbc			= "rbc";

		/// <summary>
		/// TODO: Add tag description.
		/// </summary>
		internal const string Rp			= "rp";

		/// <summary>
		/// TODO: Add tag description.
		/// </summary>
		internal const string Rt			= "rt";

		/// <summary>
		/// TODO: Add tag description.
		/// </summary>
		internal const string Rtc			= "rtc";

		/// <summary>
		/// TODO: Add tag description.
		/// </summary>
		internal const string Ruby		= "ruby";

		/// <summary>
		/// TODO: Add tag description.
		/// </summary>
		internal const string S			= "s";

		/// <summary>
		/// TODO: Add tag description.
		/// </summary>
		internal const string Samp		= "samp";

		/// <summary>
		/// TODO: Add tag description.
		/// </summary>
		internal const string Script		= "script";

		/// <summary>
		/// TODO: Add tag description.
		/// </summary>
		internal const string Select		= "select";

		/// <summary>
		/// TODO: Add tag description.
		/// </summary>
		internal const string Server		= "server";

		/// <summary>
		/// TODO: Add tag description.
		/// </summary>
		internal const string Servlet		= "servlet";

		/// <summary>
		/// TODO: Add tag description.
		/// </summary>
		internal const string Small		= "small";

		/// <summary>
		/// TODO: Add tag description.
		/// </summary>
		internal const string Spacer		= "spacer";

		/// <summary>
		/// TODO: Add tag description.
		/// </summary>
		internal const string Span		= "span";

		/// <summary>
		/// TODO: Add tag description.
		/// </summary>
		internal const string Strike		= "strike";

		/// <summary>
		/// TODO: Add tag description.
		/// </summary>
		internal const string Strong		= "strong";

		/// <summary>
		/// TODO: Add tag description.
		/// </summary>
		internal const string Style		= "style";

		/// <summary>
		/// TODO: Add tag description.
		/// </summary>
		internal const string Sub			= "sub";

		/// <summary>
		/// TODO: Add tag description.
		/// </summary>
		internal const string Sup			= "sup";

		/// <summary>
		/// TODO: Add tag description.
		/// </summary>
		internal const string Table		= "table";

		/// <summary>
		/// TODO: Add tag description.
		/// </summary>
		internal const string Tbody		= "tbody";

		/// <summary>
		/// TODO: Add tag description.
		/// </summary>
		internal const string Td			= "td";

		/// <summary>
		/// TODO: Add tag description.
		/// </summary>
		internal const string Textarea	= "textarea";

		/// <summary>
		/// TODO: Add tag description.
		/// </summary>
		internal const string Tfoot		= "tfoot";

		/// <summary>
		/// TODO: Add tag description.
		/// </summary>
		internal const string Th			= "th";

		/// <summary>
		/// TODO: Add tag description.
		/// </summary>
		internal const string Thead		= "thead";

		/// <summary>
		/// TODO: Add tag description.
		/// </summary>
		internal const string Title		= "title";

		/// <summary>
		/// TODO: Add tag description.
		/// </summary>
		internal const string Tr			= "tr";

		/// <summary>
		/// TODO: Add tag description.
		/// </summary>
		internal const string Tt			= "tt";

		/// <summary>
		/// TODO: Add tag description.
		/// </summary>
		internal const string U			= "u";

		/// <summary>
		/// TODO: Add tag description.
		/// </summary>
		internal const string Ul			= "ul";

		/// <summary>
		/// TODO: Add tag description.
		/// </summary>
		internal const string Var			= "var";

		/// <summary>
		/// TODO: Add tag description.
		/// </summary>
		internal const string Wbr			= "wbr";

		/// <summary>
		/// TODO: Add tag description.
		/// </summary>
		internal const string Xmp			= "xmp";

#endregion

#region Known tags set

        /// <summary>
        /// A set of known tags.
        /// </summary>
        internal static readonly string[] KnownTags = new string[] {
            A,
            Abbr,
            Acronym,
            Address,
            Align,
            Applet,
            Area,
            B,
            Base,
            Basefont,
            Bdo,
            Bgsound,
            Big,
            Blink,
            Blockquote,
            Body,
            Br,
            Button,
            Caption,
            Center,
            Cite,
            Code,
            Col,
            Colgroup,
            Comment,
            Dd,
            Del,
            Dfn,
            Dir,
            Div,
            Dl,
            Dt,
            Em,
            Embed,
            Fieldset,
            Font,
            Form,
            Frame,
            Frameset,
            H1,
            H2,
            H3,
            H4,
            H5,
            H6,
            Head,
            Hr,
            Html,
            I,
            Iframe,
            Ilayer,
            Img,
            Input,
            Ins,
            Isindex,
            Kbd,
            Keygen,
            Label,
            Layer,
            Legend,
            Li,
            Link,
            Listing,
            Map,
            Marquee,
            Menu,
            Meta,
            Multicol,
            Nobr,
            Noembed,
            Noframes,
            Nolayer,
            Nosave,
            Noscript,
            Object,
            Ol,
            Optgroup,
            Option,
            P,
            Param,
            Plaintext,
            Pre,
            Q,
            Rb,
            Rbc,
            Rp,
            Rt,
            Rtc,
            Ruby,
            S,
            Samp,
            Script,
            Select,
            Server,
            Servlet,
            Small,
            Spacer,
            Span,
            Strike,
            Strong,
            Style,
            Sub,
            Sup,
            Table,
            Tbody,
            Td,
            Textarea,
            Tfoot,
            Th,
            Thead,
            Title,
            Tr,
            Tt,
            U,
            Ul,
            Var,
            Wbr,
            Xmp
        };

#endregion

	    /// <summary>
	    /// Checks if the node name is a name of a known node.
	    /// </summary>
	    /// <param name="name">The name of the node to check.</param>
	    /// <returns>
	    /// true is the name is the name of a known node; false otherwise.
	    /// </returns>
	    internal static bool IsKnown(string name)
	    {
	        for(int i = 0; i < KnownTags.Length; i++)
	            if(0 == String.Compare(name, KnownTags[i], true))
	                return true;

	        return false;
	    }

	    /// <summary>
	    /// Checks if the node is a known node.
	    /// </summary>
	    /// <param name="node">The node to check.</param>
	    /// <returns>
	    /// true is the node is a known node; false otherwise.
	    /// </returns>
	    internal static bool IsKnown(HtmlNode node)
	    {
	        return IsKnown(node.Name);
	    }

		/// <summary>
		/// Checks if the tag is atomic, i.e. if it has a closing tag or not.
		/// </summary>
		/// <param name="nodeName">The name of the tag.</param>
		/// <returns>
		/// <list type="table">
		///	<listheader>
		///		<term>Value</term>
		///		<description>Description</description>
		///	</listheader>
		///	<item>
		///		<term>true</term>
		///		<description>The tag has no closing tag. It can be, for instance,
		/// 	&lt;img&gt; or &lt;br&gt; tag.</description>
		///	</item>
		///	<item>
		///		<term>false</term>
		///		<description>The tag has a closing tag. It can be, for instance,
		/// 	&lt;a&gt; or &lt;strong&gt; tag.</description>
		///	</item>
		/// </list>
		/// </returns>
		internal static bool IsAtomic(string nodeName)
		{
			switch(nodeName.ToLower())
			{
				case Meta:
				case Br:
				case Hr:
				case Img:
				case Link:
				case Base:
				case Basefont:
				case Area:
				case Param:
				case Option:
				case Input:
					return true;
				default:
					return false;
			}
		}
	}
}
