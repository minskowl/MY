using System.Linq;

namespace Savchin.Web.HtmlProcessing.Core
{
    /// <summary>
    /// Provides information about attribute/value pair
    /// </summary>
    public class AttributeValueInfo
    {

        #region Properties

        /// <summary>
        /// Gets or sets the attribute.
        /// </summary>
        /// <value>The attribute.</value>
        public string Attribute { get; set; }

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        /// <value>The value.</value>
        public string Value { get; set; }

        /// <summary>
        /// Gets or sets the delimeter.
        /// </summary>
        /// <value>The delimeter.</value>
        public string Delimeter { get; set; }

        private static readonly string[] ReferenceAttributes = new string[] { "href", "src", "background", "action" };

        public AttributeValueInfo()
        {
            Delimeter = string.Empty;
            Value = string.Empty;
            Attribute = string.Empty;
        }

        /// <summary>
        /// Gets a value indicating whether this instance has file rererence.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance has file rererence; otherwise, <c>false</c>.
        /// </value>
        public bool HasFileReference
        {
            get
            {
                // TODO: Add css resolving here
                return ReferenceAttributes.Contains(Attribute.ToLower());
            }
        }

        /// <summary>
        /// Gets or sets the file reference URL.
        /// </summary>
        /// <value>The file reference URL.</value>
        public string FileReferenceUrl
        {
            get
            {
                if (HasFileReference)
                    return Value;
                // TODO: Get css reference
                return string.Empty;
            }
            set
            {
                if (HasFileReference)
                    Value = value;
                // TODO: Update Css reference
            }
        }

        #endregion

        #region Overriden methods

        /// <summary>
        /// Returns a <see cref="T:System.String"></see> that represents the current <see cref="T:System.Object"></see>.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.String"></see> that represents the current <see cref="T:System.Object"></see>.
        /// </returns>
        public override string ToString()
        {
            return Attribute + "=" + Delimeter + Value + Delimeter;
        }

        #endregion
    }
}