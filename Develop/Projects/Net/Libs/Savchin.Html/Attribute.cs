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
	/// HTML attributes operations and rules.
	/// </summary>
	internal sealed class Attribute
	{
#region Known attributes

		/// <summary>
		/// Abbreviation.
		/// </summary>
		internal const string Abbr 				= "abbr";

		/// <summary>
		/// TODO: Add attribute description.
		/// </summary>
		internal const string Accept 				= "accept";

		/// <summary>
		/// TODO: Add attribute description.
		/// </summary>
		internal const string AcceptCharset 		= "accept_charset";

		/// <summary>
		/// TODO: Add attribute description.
		/// </summary>
		internal const string AccessKey 			= "accesskey";

		/// <summary>
		/// TODO: Add attribute description.
		/// </summary>
		internal const string Action 				= "action";

		/// <summary>
		/// TODO: Add attribute description.
		/// </summary>
		internal const string AddDate 			= "add_date";

		/// <summary>
		/// Align.
		/// </summary>
		internal const string Align 				= "align";

		/// <summary>
		/// Active link Color for &lt;<see cref="Tag.Body"/>&gt; tag.
		/// </summary>
		internal const string Alink 				= "alink";

		/// <summary>
		/// Alternative text.
		/// </summary>
		internal const string Alt 				= "alt";

		/// <summary>
		/// Specifies thr URL of the JAR or ZIP file for the
		/// &lt;<see cref="Tag.Applet"/>&gt; tag.
		/// </summary>
		internal const string Archive 			= "archive";

		/// <summary>
		/// TODO: Add attribute description.
		/// </summary>
		internal const string Axis 				= "axis";

		/// <summary>
		/// TODO: Add attribute description.
		/// </summary>
		internal const string Background 			= "background";

		/// <summary>
		/// TODO: Add attribute description.
		/// </summary>
		internal const string BgColor 			= "bgcolor";

		/// <summary>
		/// TODO: Add attribute description.
		/// </summary>
		internal const string Bgproperties 		= "bgproperties";

		/// <summary>
		/// TODO: Add attribute description.
		/// </summary>
		internal const string Border 				= "border";

		/// <summary>
		/// TODO: Add attribute description.
		/// </summary>
		internal const string BorderColor 		= "bordercolor";

		/// <summary>
		/// TODO: Add attribute description.
		/// </summary>
		internal const string BottomMargin 		= "bottommargin";

		/// <summary>
		/// TODO: Add attribute description.
		/// </summary>
		internal const string CellPadding 		= "cellpadding";

		/// <summary>
		/// TODO: Add attribute description.
		/// </summary>
		internal const string CellSpacing 		= "cellspacing";

		/// <summary>
		/// TODO: Add attribute description.
		/// </summary>
		internal const string Char 				= "char";

		/// <summary>
		/// TODO: Add attribute description.
		/// </summary>
		internal const string CharOff 			= "charoff";

		/// <summary>
		/// TODO: Add attribute description.
		/// </summary>
		internal const string CharSet 			= "charset";

		/// <summary>
		/// TODO: Add attribute description.
		/// </summary>
		internal const string Checked 			= "checked";

		/// <summary>
		/// TODO: Add attribute description.
		/// </summary>
		internal const string Cite 				= "cite";

		/// <summary>
		/// TODO: Add attribute description.
		/// </summary>
		internal const string Class 				= "class";

		/// <summary>
		/// TODO: Add attribute description.
		/// </summary>
		internal const string ClassId 			= "classid";

		/// <summary>
		/// TODO: Add attribute description.
		/// </summary>
		internal const string Clear 				= "clear";

		/// <summary>
		/// TODO: Add attribute description.
		/// </summary>
		internal const string Code 				= "code";

		/// <summary>
		/// TODO: Add attribute description.
		/// </summary>
		internal const string CodeBase 			= "codebase";

		/// <summary>
		/// TODO: Add attribute description.
		/// </summary>
		internal const string CodeType 			= "codetype";

		/// <summary>
		/// TODO: Add attribute description.
		/// </summary>
		internal const string Color 				= "color";

		/// <summary>
		/// TODO: Add attribute description.
		/// </summary>
		internal const string Cols 				= "cols";

		/// <summary>
		/// TODO: Add attribute description.
		/// </summary>
		internal const string ColSpan 			= "colspan";

		/// <summary>
		/// TODO: Add attribute description.
		/// </summary>
		internal const string Compact 			= "compact";

		/// <summary>
		/// TODO: Add attribute description.
		/// </summary>
		internal const string Content 			= "content";

		/// <summary>
		/// TODO: Add attribute description.
		/// </summary>
		internal const string Coords 				= "coords";

		/// <summary>
		/// TODO: Add attribute description.
		/// </summary>
		internal const string Data 				= "data";

		/// <summary>
		/// TODO: Add attribute description.
		/// </summary>
		internal const string DataFld 			= "datafld";

		/// <summary>
		/// TODO: Add attribute description.
		/// </summary>
		internal const string DataFormatAs 		= "dataformatas";

		/// <summary>
		/// TODO: Add attribute description.
		/// </summary>
		internal const string DataPageSize 		= "datapagesize";

		/// <summary>
		/// TODO: Add attribute description.
		/// </summary>
		internal const string DataSrc 			= "datasrc";

		/// <summary>
		/// TODO: Add attribute description.
		/// </summary>
		internal const string DateTime 			= "datetime";

		/// <summary>
		/// TODO: Add attribute description.
		/// </summary>
		internal const string Declare 			= "declare";

		/// <summary>
		/// TODO: Add attribute description.
		/// </summary>
		internal const string Defer 				= "defer";

		/// <summary>
		/// TODO: Add attribute description.
		/// </summary>
		internal const string Dir 				= "dir";

		/// <summary>
		/// TODO: Add attribute description.
		/// </summary>
		internal const string Disabled 			= "disabled";

		/// <summary>
		/// TODO: Add attribute description.
		/// </summary>
		internal const string Encoding 			= "encoding";

		/// <summary>
		/// TODO: Add attribute description.
		/// </summary>
		internal const string EncType 			= "enctype";

		/// <summary>
		/// TODO: Add attribute description.
		/// </summary>
		internal const string Event 				= "event";
		/// <summary>
		/// TODO: Add attribute description.
		/// </summary>
		internal const string Face 				= "face";

		/// <summary>
		/// TODO: Add attribute description.
		/// </summary>
		internal const string For 				= "for";

		/// <summary>
		/// TODO: Add attribute description.
		/// </summary>
		internal const string Frame 				= "frame";

		/// <summary>
		/// TODO: Add attribute description.
		/// </summary>
		internal const string FrameBorder 		= "frameborder";

		/// <summary>
		/// TODO: Add attribute description.
		/// </summary>
		internal const string FrameSpacing 		= "framespacing";

		/// <summary>
		/// TODO: Add attribute description.
		/// </summary>
		internal const string GridX 				= "gridx";

		/// <summary>
		/// TODO: Add attribute description.
		/// </summary>
		internal const string GridY 				= "gridy";

		/// <summary>
		/// TODO: Add attribute description.
		/// </summary>
		internal const string Headers 			= "headers";

		/// <summary>
		/// TODO: Add attribute description.
		/// </summary>
		internal const string Height 				= "height";

		/// <summary>
		/// TODO: Add attribute description.
		/// </summary>
		internal const string Href 				= "href";

		/// <summary>
		/// TODO: Add attribute description.
		/// </summary>
		internal const string HrefLang 			= "hreflang";

		/// <summary>
		/// TODO: Add attribute description.
		/// </summary>
		internal const string HSpace 				= "hspace";

		/// <summary>
		/// TODO: Add attribute description.
		/// </summary>
		internal const string HttpEquiv 			= "http_equiv";

		/// <summary>
		/// TODO: Add attribute description.
		/// </summary>
		internal const string Id 					= "id";

		/// <summary>
		/// TODO: Add attribute description.
		/// </summary>
		internal const string IsMap 				= "ismap";

		/// <summary>
		/// TODO: Add attribute description.
		/// </summary>
		internal const string Label 				= "label";

		/// <summary>
		/// TODO: Add attribute description.
		/// </summary>
		internal const string Lang 				= "lang";

		/// <summary>
		/// TODO: Add attribute description.
		/// </summary>
		internal const string Language 			= "language";

		/// <summary>
		/// TODO: Add attribute description.
		/// </summary>
		internal const string LastModified 		= "last_modified";

		/// <summary>
		/// TODO: Add attribute description.
		/// </summary>
		internal const string LastVisit 			= "last_visit";

		/// <summary>
		/// TODO: Add attribute description.
		/// </summary>
		internal const string LeftMargin 			= "leftmargin";

		/// <summary>
		/// TODO: Add attribute description.
		/// </summary>
		internal const string Link 				= "link";

		/// <summary>
		/// TODO: Add attribute description.
		/// </summary>
		internal const string LongDesc 			= "longdesc";

		/// <summary>
		/// TODO: Add attribute description.
		/// </summary>
		internal const string LowSrc 				= "lowsrc";

		/// <summary>
		/// TODO: Add attribute description.
		/// </summary>
		internal const string MarginHeight 		= "marginheight";

		/// <summary>
		/// TODO: Add attribute description.
		/// </summary>
		internal const string MarginWidth 		= "marginwidth";

		/// <summary>
		/// TODO: Add attribute description.
		/// </summary>
		internal const string MaxLength 			= "maxlength";

		/// <summary>
		/// TODO: Add attribute description.
		/// </summary>
		internal const string Media 				= "media";

		/// <summary>
		/// TODO: Add attribute description.
		/// </summary>
		internal const string Method 				= "method";

		/// <summary>
		/// TODO: Add attribute description.
		/// </summary>
		internal const string Methods 			= "methods";

		/// <summary>
		/// TODO: Add attribute description.
		/// </summary>
		internal const string Multiple 			= "multiple";

		/// <summary>
		/// TODO: Add attribute description.
		/// </summary>
		internal const string N 					= "n";

		/// <summary>
		/// TODO: Add attribute description.
		/// </summary>
		internal const string Name 				= "name";

		/// <summary>
		/// TODO: Add attribute description.
		/// </summary>
		internal const string NoHref 				= "nohref";

		/// <summary>
		/// TODO: Add attribute description.
		/// </summary>
		internal const string NoResize 			= "noresize";

		/// <summary>
		/// TODO: Add attribute description.
		/// </summary>
		internal const string NoShade 			= "noshade";

		/// <summary>
		/// TODO: Add attribute description.
		/// </summary>
		internal const string NoWrap 				= "nowrap";

		/// <summary>
		/// TODO: Add attribute description.
		/// </summary>
		internal const string Object 				= "object";

		/// <summary>
		/// TODO: Add attribute description.
		/// </summary>
		internal const string OnAfterUpdate 		= "onafterupdate";

		/// <summary>
		/// TODO: Add attribute description.
		/// </summary>
		internal const string OnBeforeUnload 		= "onbeforeunload";

		/// <summary>
		/// TODO: Add attribute description.
		/// </summary>
		internal const string OnBeforeUpdate 		= "onbeforeupdate";

		/// <summary>
		/// TODO: Add attribute description.
		/// </summary>
		internal const string OnBlur 				= "onblur";

		/// <summary>
		/// TODO: Add attribute description.
		/// </summary>
		internal const string OnChange 			= "onchange";

		/// <summary>
		/// TODO: Add attribute description.
		/// </summary>
		internal const string OnClick 			= "onclick";

		/// <summary>
		/// TODO: Add attribute description.
		/// </summary>
		internal const string OnDataAvailable 	= "ondataavailable";

		/// <summary>
		/// TODO: Add attribute description.
		/// </summary>
		internal const string OnDatasetChanged 	= "ondatasetchanged";

		/// <summary>
		/// TODO: Add attribute description.
		/// </summary>
		internal const string OnDatasetComplete 	= "ondatasetcomplete";

		/// <summary>
		/// TODO: Add attribute description.
		/// </summary>
		internal const string OnDblClick 			= "ondblclick";

		/// <summary>
		/// TODO: Add attribute description.
		/// </summary>
		internal const string OnErrorUpdate 		= "onerrorupdate";

		/// <summary>
		/// TODO: Add attribute description.
		/// </summary>
		internal const string OnFocus 			= "onfocus";

		/// <summary>
		/// TODO: Add attribute description.
		/// </summary>
		internal const string OnKeyDown 			= "onkeydown";

		/// <summary>
		/// TODO: Add attribute description.
		/// </summary>
		internal const string OnKeyPress 			= "onkeypress";

		/// <summary>
		/// TODO: Add attribute description.
		/// </summary>
		internal const string OnKeyUp 			= "onkeyup";

		/// <summary>
		/// TODO: Add attribute description.
		/// </summary>
		internal const string OnLoad 				= "onload";

		/// <summary>
		/// TODO: Add attribute description.
		/// </summary>
		internal const string OnMouseDown 		= "onmousedown";

		/// <summary>
		/// TODO: Add attribute description.
		/// </summary>
		internal const string OnMouseMove 		= "onmousemove";

		/// <summary>
		/// TODO: Add attribute description.
		/// </summary>
		internal const string OnMouseOut 			= "onmouseout";

		/// <summary>
		/// TODO: Add attribute description.
		/// </summary>
		internal const string OnMouseOver 		= "onmouseover";

		/// <summary>
		/// TODO: Add attribute description.
		/// </summary>
		internal const string OnMouseUp 			= "onmouseup";

		/// <summary>
		/// TODO: Add attribute description.
		/// </summary>
		internal const string OnReset 			= "onreset";

		/// <summary>
		/// TODO: Add attribute description.
		/// </summary>
		internal const string OnRowEnter 			= "onrowenter";

		/// <summary>
		/// TODO: Add attribute description.
		/// </summary>
		internal const string OnRowExit 			= "onrowexit";

		/// <summary>
		/// TODO: Add attribute description.
		/// </summary>
		internal const string OnSelect 			= "onselect";

		/// <summary>
		/// TODO: Add attribute description.
		/// </summary>
		internal const string OnSubmit 			= "onsubmit";

		/// <summary>
		/// TODO: Add attribute description.
		/// </summary>
		internal const string OnUnload 			= "onunload";

		/// <summary>
		/// TODO: Add attribute description.
		/// </summary>
		internal const string Profile 			= "profile";

		/// <summary>
		/// TODO: Add attribute description.
		/// </summary>
		internal const string Prompt 				= "prompt";

		/// <summary>
		/// TODO: Add attribute description.
		/// </summary>
		internal const string RbSpan 				= "rbspan";

		/// <summary>
		/// TODO: Add attribute description.
		/// </summary>
		internal const string ReadOnly 			= "readonly";

		/// <summary>
		/// TODO: Add attribute description.
		/// </summary>
		internal const string Rel 				= "rel";

		/// <summary>
		/// TODO: Add attribute description.
		/// </summary>
		internal const string Rev 				= "rev";

		/// <summary>
		/// TODO: Add attribute description.
		/// </summary>
		internal const string RightMargin 		= "rightmargin";

		/// <summary>
		/// TODO: Add attribute description.
		/// </summary>
		internal const string Rows 				= "rows";

		/// <summary>
		/// TODO: Add attribute description.
		/// </summary>
		internal const string RowSpan 			= "rowspan";

		/// <summary>
		/// TODO: Add attribute description.
		/// </summary>
		internal const string Rules 				= "rules";

		/// <summary>
		/// TODO: Add attribute description.
		/// </summary>
		internal const string Scheme 				= "scheme";

		/// <summary>
		/// TODO: Add attribute description.
		/// </summary>
		internal const string Scope 				= "scope";

		/// <summary>
		/// TODO: Add attribute description.
		/// </summary>
		internal const string Scrolling 			= "scrolling";

		/// <summary>
		/// TODO: Add attribute description.
		/// </summary>
		internal const string SdaForm 			= "sdaform";

		/// <summary>
		/// TODO: Add attribute description.
		/// </summary>
		internal const string SdaPref 			= "sdapref";

		/// <summary>
		/// TODO: Add attribute description.
		/// </summary>
		internal const string SdaSuff 			= "sdasuff";

		/// <summary>
		/// TODO: Add attribute description.
		/// </summary>
		internal const string Selected 			= "selected";

		/// <summary>
		/// TODO: Add attribute description.
		/// </summary>
		internal const string Shape 				= "shape";

		/// <summary>
		/// TODO: Add attribute description.
		/// </summary>
		internal const string ShowGrid 			= "showgrid";

		/// <summary>
		/// TODO: Add attribute description.
		/// </summary>
		internal const string ShowGridX 			= "showgridx";

		/// <summary>
		/// TODO: Add attribute description.
		/// </summary>
		internal const string ShowGridY 			= "showgridy";

		/// <summary>
		/// TODO: Add attribute description.
		/// </summary>
		internal const string Size 				= "size";

		/// <summary>
		/// TODO: Add attribute description.
		/// </summary>
		internal const string Span 				= "span";

		/// <summary>
		/// TODO: Add attribute description.
		/// </summary>
		internal const string Src 				= "src";

		/// <summary>
		/// TODO: Add attribute description.
		/// </summary>
		internal const string Standby 			= "standby";

		/// <summary>
		/// TODO: Add attribute description.
		/// </summary>
		internal const string Start 				= "start";

		/// <summary>
		/// TODO: Add attribute description.
		/// </summary>
		internal const string Style 				= "style";

		/// <summary>
		/// TODO: Add attribute description.
		/// </summary>
		internal const string Summary 			= "summary";

		/// <summary>
		/// TODO: Add attribute description.
		/// </summary>
		internal const string TabIndex 			= "tabindex";

		/// <summary>
		/// TODO: Add attribute description.
		/// </summary>
		internal const string Target 				= "target";

		/// <summary>
		/// TODO: Add attribute description.
		/// </summary>
		internal const string Text 				= "text";

		/// <summary>
		/// An auxiliary tag's title text. Useful for
		/// &lt;<see cref="Tag.A"/>&gt; tags. For
		/// &lt;<see cref="Tag.Img"/>&gt; tags use
		/// &lt;<see cref="Attribute.Alt"/>&gt; attribute.
		/// </summary>
		internal const string Title 				= "title";

		/// <summary>
		/// TODO: Add attribute description.
		/// </summary>
		internal const string TopMargin 			= "topmargin";

		/// <summary>
		/// TODO: Add attribute description.
		/// </summary>
		internal const string Type 				= "type";

		/// <summary>
		/// TODO: Add attribute description.
		/// </summary>
		internal const string UseMap 				= "usemap";

		/// <summary>
		/// TODO: Add attribute description.
		/// </summary>
		internal const string VAlign 				= "valign";

		/// <summary>
		/// TODO: Add attribute description.
		/// </summary>
		internal const string Value 				= "value";

		/// <summary>
		/// TODO: Add attribute description.
		/// </summary>
		internal const string ValueType 			= "valuetype";

		/// <summary>
		/// TODO: Add attribute description.
		/// </summary>
		internal const string Version 			= "version";

		/// <summary>
		/// Visited link Color for &lt;<see cref="Tag.Body"/>&gt; tag.
		/// </summary>
		internal const string VLink 				= "vlink";

		/// <summary>
		/// TODO: Add attribute description.
		/// </summary>
		internal const string VSpace 				= "vspace";

		/// <summary>
		/// TODO: Add attribute description.
		/// </summary>
		internal const string Width 				= "width";

		/// <summary>
		/// TODO: Add attribute description.
		/// </summary>
		internal const string Wrap 				= "wrap";

		/// <summary>
		/// TODO: Add attribute description.
		/// </summary>
		internal const string XmlLang 			= "xml_lang";

		/// <summary>
		/// TODO: Add attribute description.
		/// </summary>
		internal const string XmlSpace 			= "xml_space";

		/// <summary>
		/// TODO: Add attribute description.
		/// </summary>
		internal const string XmlNS 				= "xmlns";

		/// <summary>
		/// TODO: Add attribute description.
		/// </summary>
		internal const string Urn 				= "urn";

#endregion

#region Known attributes set

        /// <summary>
        /// Known attributes set.
        /// </summary>
        internal static readonly string[] KnownAttributes = new String[] {
            Abbr,
            Accept,
            AcceptCharset,
            AccessKey,
            Action,
            AddDate,
            Align,
            Alink,
            Alt,
            Archive,
            Axis,
            Background,
            BgColor,
            Bgproperties,
            Border,
            BorderColor,
            BottomMargin,
            CellPadding,
            CellSpacing,
            Char,
            CharOff,
            CharSet,
            Checked,
            Cite,
            Class,
            ClassId,
            Clear,
            Code,
            CodeBase,
            CodeType,
            Color,
            Cols,
            ColSpan,
            Compact,
            Content,
            Coords,
            Data,
            DataFld,
            DataFormatAs,
            DataPageSize,
            DataSrc,
            DateTime,
            Declare,
            Defer,
            Dir,
            Disabled,
            Encoding,
            EncType,
            Event,
            Face,
            For,
            Frame,
            FrameBorder,
            FrameSpacing,
            GridX,
            GridY,
            Headers,
            Height,
            Href,
            HrefLang,
            HSpace,
            HttpEquiv,
            Id,
            IsMap,
            Label,
            Lang,
            Language,
            LastModified,
            LastVisit,
            LeftMargin,
            Link,
            LongDesc,
            LowSrc,
            MarginHeight,
            MarginWidth,
            MaxLength,
            Media,
            Method,
            Methods,
            Multiple,
            N,
            Name,
            NoHref,
            NoResize,
            NoShade,
            NoWrap,
            Object,
            OnAfterUpdate,
            OnBeforeUnload,
            OnBeforeUpdate,
            OnBlur,
            OnChange,
            OnClick,
            OnDataAvailable,
            OnDatasetChanged,
            OnDatasetComplete,
            OnDblClick,
            OnErrorUpdate,
            OnFocus,
            OnKeyDown,
            OnKeyPress,
            OnKeyUp,
            OnLoad,
            OnMouseDown,
            OnMouseMove,
            OnMouseOut,
            OnMouseOver,
            OnMouseUp,
            OnReset,
            OnRowEnter,
            OnRowExit,
            OnSelect,
            OnSubmit,
            OnUnload,
            Profile,
            Prompt,
            RbSpan,
            ReadOnly,
            Rel,
            Rev,
            RightMargin,
            Rows,
            RowSpan,
            Rules,
            Scheme,
            Scope,
            Scrolling,
            SdaForm,
            SdaPref,
            SdaSuff,
            Selected,
            Shape,
            ShowGrid,
            ShowGridX,
            ShowGridY,
            Size,
            Span,
            Src,
            Standby,
            Start,
            Style,
            Summary,
            TabIndex,
            Target,
            Text,
            Title,
            TopMargin,
            Type,
            UseMap,
            VAlign,
            Value,
            ValueType,
            Version,
            VLink,
            VSpace,
            Width,
            Wrap,
            XmlLang,
            XmlSpace,
            XmlNS,
            Urn
        };

#endregion

	    /// <summary>
	    /// Checks if the attribute name is a name of a known attribute.
	    /// </summary>
	    /// <param name="name">The name of the attribute to check.</param>
	    /// <returns>
	    /// true is the name is the name of a known attribute; false otherwise.
	    /// </returns>
	    internal static bool IsKnown(string name)
	    {
	        for(int i = 0; i < KnownAttributes.Length; i++)
	            if(0 == String.Compare(name, KnownAttributes[i], true))
	                return true;

	        return false;
	    }

	    /// <summary>
	    /// Checks if the attribute is a known attribute.
	    /// </summary>
	    /// <param name="node">The attribute to check.</param>
	    /// <returns>
	    /// true is the attribute is a known node; false otherwise.
	    /// </returns>
	    internal static bool IsKnown(HtmlAttribute node)
	    {
	        return IsKnown(node.Name);
	    }
	}
}
