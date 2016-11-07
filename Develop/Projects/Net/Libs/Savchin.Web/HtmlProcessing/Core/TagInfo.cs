using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace Savchin.Web.HtmlProcessing.Core
{
    /// <summary>
    /// Provides tag information
    /// </summary>
    public class TagInfo
    {
        #region Properties

        /// <summary>
        /// Gets the name of the tag.
        /// </summary>
        /// <value>The name of the tag.</value>
        public string TagName { get; set; }

        /// <summary>
        /// Gets or sets the closing tag.
        /// </summary>
        /// <value>The closing tag.</value>
        public string ClosingSymbol { get; set; }

        private readonly List<AttributeValueInfo> _attributes = new List<AttributeValueInfo>();
        /// <summary>
        /// Gets the attributes.
        /// </summary>
        /// <value>The attributes.</value>
        public List<AttributeValueInfo> Attributes
        {
            get { return _attributes; }
        }
        /// <summary>
        /// Gets a value indicating whether this instance has file reference.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance has file reference; otherwise, <c>false</c>.
        /// </value>
        public bool HasFileReference
        {
            get { return _attributes.Any(e => e.HasFileReference); }
        }

        /// <summary>
        /// Gets or sets the file rerefence URL.
        /// </summary>
        /// <value>The file rerefence URL.</value>
        public AttributeValueInfo FileRerefenceAttribute
        {
            get
            {
                return _attributes.Where(e => e.HasFileReference).FirstOrDefault();
            }
        }

        /// <summary>
        /// Gets the <see cref="AttributeValueInfo"/> with the specified attribute name.
        /// </summary>
        /// <value></value>
        public AttributeValueInfo this[string attributeName]
        {
            get
            {
                return _attributes.Where(e => e.Attribute == attributeName).FirstOrDefault();
            }
        }
        #endregion


        /// <summary>
        /// Initializes a new instance of the <see cref="TagInfo"/> class.
        /// </summary>
        public TagInfo()
        {
            ClosingSymbol = string.Empty;
            TagName = string.Empty;
        }

        #region Overriden methods

        /// <summary>
        /// Returns a <see cref="T:System.String"></see> that represents the current <see cref="T:System.Object"></see>.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.String"></see> that represents the current <see cref="T:System.Object"></see>.
        /// </returns>
        public override string ToString()
        {
            var builder = new StringBuilder();

            builder.Append("<");
            builder.Append(TagName);

            foreach (var pair in Attributes)
            {
                builder.Append(" " + pair);
            }

            builder.Append(ClosingSymbol);
            return builder.ToString();
        }

        #endregion
    }
}