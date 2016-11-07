using System;
using System.Text;
using Savchin.IO;

namespace Savchin.Web.HtmlProcessing.Core
{
    internal class LoadedHtml : IWebFile
    {
        #region Properties
        /// <summary>
        /// Gets a value indicating whether this instance is text file.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is text file; otherwise, <c>false</c>.
        /// </value>
        public string ContentType
        {
            get { return Mime.MimeTypeHtml; }
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
        public string Text { get; private set; }

        /// <summary>
        /// Gets the text encoding.
        /// </summary>
        /// <value>The text encoding.</value>
        public Encoding TextEncoding { get; private set; }

        /// <summary>
        /// Gets the URL of current document
        /// </summary>
        /// <value>The URL.</value>
        public Uri Url { get; private set; }

        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="LoadedHtml"/> class.
        /// </summary>
        /// <param name="uri">The URI.</param>
        /// <param name="html">The HTML.</param>
        /// <param name="encoding">The encoding.</param>
        public LoadedHtml(Uri uri, string html, Encoding encoding)
        {
            Url = uri;
            Text = html;
            TextEncoding = encoding;
        }
    }
}
