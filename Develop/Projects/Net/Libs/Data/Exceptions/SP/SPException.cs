using System;
using System.Data.Common;
using System.Runtime.Serialization;

namespace Savchin.Data
{

    public class SPException : DbException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="T:SPException"/> class.
        /// </summary>
        public SPException() : base() { }
        /// <summary>
        /// Initializes a new instance of the <see cref="T:SPException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        public SPException(string message) : base(message) { }
        /// <summary>
        /// Initializes a new instance of the <see cref="T:SPException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="innerException">The inner exception.</param>
        public SPException(string message, Exception innerException) : base(message, innerException) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:SPException"/> class.
        /// </summary>
        /// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo"></see> that holds the serialized object data about the exception being thrown.</param>
        /// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext"></see> that contains contextual information about the source or destination.</param>
        /// <exception cref="T:System.Runtime.Serialization.SerializationException">The class name is null or <see cref="P:System.Exception.HResult"></see> is zero (0). </exception>
        /// <exception cref="T:System.ArgumentNullException">The info parameter is null. </exception>
        protected SPException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        { }
    }
}
