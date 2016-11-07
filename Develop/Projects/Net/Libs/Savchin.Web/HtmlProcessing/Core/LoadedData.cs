using System;
using System.IO;
using System.Text;
using Savchin.IO;

namespace Savchin.Web.HtmlProcessing.Core
{
    /// <summary>
    /// LoadedData
    /// </summary>
    public class LoadedData : IWebFile
    {
        private readonly byte[] _bytes;
        private readonly Uri _url;
        private readonly string _contentType;
        #region Properties
        /// <summary>
        /// Gets a value indicating whether this instance is text file.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is text file; otherwise, <c>false</c>.
        /// </value>
        public string ContentType
        {
            get { return _contentType; }
        }

        /// <summary>
        /// Gets the bytes.
        /// </summary>
        /// <value>The bytes.</value>
        public byte[] Bytes
        {
            get { return _bytes; }
        }

        /// <summary>
        /// Gets text of html document
        /// </summary>
        /// <value></value>
        public string Text
        {
            get { throw new NotSupportedException(); }
        }

        /// <summary>
        /// Gets the text encoding.
        /// </summary>
        /// <value>The text encoding.</value>
        public Encoding TextEncoding
        {
            get { throw new NotSupportedException(); }
        }

        /// <summary>
        /// Gets the URL of current document
        /// </summary>
        /// <value>The URL.</value>
        public Uri Url
        {
            get { return _url; }
        }
        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="LoadedData"/> class.
        /// </summary>
        /// <param name="url">The URL.</param>
        /// <param name="contentType">Type of the content.</param>
        /// <param name="bytes">The bytes.</param>
        public LoadedData( Uri url,string contentType, byte[] bytes)
        {
            _contentType = contentType;
            _bytes = bytes;
            _url = url;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LoadedData"/> class.
        /// </summary>
        /// <param name="url">The URL.</param>
        /// <param name="bytes">The bytes.</param>
        public LoadedData(Uri url, byte[] bytes) 
        {
            _contentType= Mime.GetMimeType(Path.GetFileName(url.LocalPath));
            _bytes = bytes;
            _url = url;
        }
    }
}
