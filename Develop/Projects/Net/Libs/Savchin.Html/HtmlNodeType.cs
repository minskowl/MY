using System;

namespace Savchin.Html
{
	/// <summary>
	/// Description of HtmlNodeType.
	/// </summary>
	public enum HtmlNodeType
	{
	    /// <summary>
	    /// The node is undefined.
	    /// </summary>
	    None,

	    /// <summary>
	    /// The node is attribute.
	    /// </summary>
	    Attribute,

	    /// <summary>
	    /// The node is an HTML comment
	    /// </summary>
	    Comment,

	    /// <summary>
	    /// The node is the document
	    /// </summary>
	    Document,

	    /// <summary>
	    /// THe node is the document's type
	    /// </summary>
	    DocumentType,

	    /// <summary>
	    /// The node is an (opening) tag.
	    /// </summary>
	    Tag,

	    /// <summary>
	    /// The node is a closing tag.
	    /// </summary>
	    EndTag,

	    /// <summary>
	    /// The node is a closed tag that contains neither value nor
	    /// child nodes.
	    /// </summary>
	    ClosedTag,

	    /// <summary>
	    /// The node is readable text.
	    /// </summary>
	    Text,

	    /// <summary>
	    /// The node contains of whitespaces.
	    /// </summary>
	    Whitespace
	}
}
