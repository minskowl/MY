using System;
using System.Text;

namespace Savchin.Web.HtmlProcessing.Core
{
    /// <summary>
    /// Helper class for update html saving
    /// </summary>
    internal class GenericWebFile : IWebFile
    {
        #region Fields

        private string _updatedHtml = string.Empty;
        private IWebFile _sourceFile = null;

        #endregion

        #region Construction

        /// <summary>
        /// Initializes a new instance of the  class.
        /// </summary>
        /// <param name="updatedHtml">The updated HTML.</param>
        /// <param name="sourceFile">The source file.</param>
        internal GenericWebFile(string updatedHtml, IWebFile sourceFile)
        {
            _updatedHtml = updatedHtml;
            _sourceFile  = sourceFile;
        }

        #endregion

        #region IWebFile Members

        /// <summary>
        /// Gets a value indicating whether this instance is text file.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is text file; otherwise, <c>false</c>.
        /// </value>
        public string ContentType
        {
            get { return _sourceFile.ContentType; }
        }

        /// <summary>
        /// Gets the URL of current document
        /// </summary>
        /// <value>The URL.</value>
        public Uri Url
        {
            get { return _sourceFile.Url; }
        }

        /// <summary>
        /// Gets the text encoding.
        /// </summary>
        /// <value>The text encoding.</value>
        public Encoding TextEncoding
        {
            get { return _sourceFile.TextEncoding; }
        }

        /// <summary>
        /// Gets the bytes.
        /// </summary>
        /// <value>The bytes.</value>
        public byte[] Bytes
        {
            get { return TextEncoding.GetBytes(Text); }
        }

        /// <summary>
        /// Gets text of html document
        /// </summary>
        /// <value></value>
        public string Text
        {
            get { return _updatedHtml; }
        }

        #endregion
    }
}