using System;

namespace Savchin.Net
{
    [global::System.Serializable]
    public class DownloadException : Exception
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="DownloadException"/> class.
        /// </summary>
        /// <param name="uri">The URI.</param>
        /// <param name="inner">The inner.</param>
        public DownloadException(Uri uri, Exception inner) : base(string.Format("Error download url '{0}'",uri), inner) { }
        /// <summary>
        /// Initializes a new instance of the <see cref="DownloadException"/> class.
        /// </summary>
        /// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo"/> that holds the serialized object data about the exception being thrown.</param>
        /// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext"/> that contains contextual information about the source or destination.</param>
        /// <exception cref="T:System.ArgumentNullException">
        /// The <paramref name="info"/> parameter is null.
        /// </exception>
        /// <exception cref="T:System.Runtime.Serialization.SerializationException">
        /// The class name is null or <see cref="P:System.Exception.HResult"/> is zero (0).
        /// </exception>
        protected DownloadException(
           global::System.Runtime.Serialization.SerializationInfo info,
           global::System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }
    }
}
