using System;
using System.Text;

namespace Savchin.Web.HtmlProcessing
{
    /// <summary>
    /// Represents common infomation about html document
    /// </summary>
    public interface IWebFile
    {
        /// <summary>
        /// Gets a value indicating whether this instance is text file.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is text file; otherwise, <c>false</c>.
        /// </value>
        string ContentType
        {
            get;
        }

        /// <summary>
        /// Gets the bytes.
        /// </summary>
        /// <value>The bytes.</value>
        byte[] Bytes
        {
            get;
        }

        /// <summary>
        /// Gets text of html document
        /// </summary>
        string Text
        {
            get;
        }

        /// <summary>
        /// Gets the text encoding.
        /// </summary>
        /// <value>The text encoding.</value>
        Encoding TextEncoding
        {
            get;
        }

        /// <summary>
        /// Gets the URL of current document
        /// </summary>
        /// <value>The URL.</value>
        Uri Url
        {
            get;
        }
    }
}