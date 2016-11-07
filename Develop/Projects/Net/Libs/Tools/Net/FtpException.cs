using System;

namespace Savchin.Net
{
    /// <summary>
    /// FtpException
    /// </summary>
    public class FtpException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FtpException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        public FtpException(string message) : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FtpException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="innerException">The inner exception.</param>
        public FtpException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}