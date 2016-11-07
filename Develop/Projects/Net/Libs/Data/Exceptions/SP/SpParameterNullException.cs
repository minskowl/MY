using System;
using System.Runtime.Serialization;

namespace Savchin.Data
{

    /// <summary>
    /// SPParameterNullException
    /// </summary>
    [Serializable()]
    public class SPParameterNullException : SPException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="T:SPParameterNullException"/> class.
        /// </summary>
        public SPParameterNullException()
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:SPParameterNullException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        public SPParameterNullException(string message)
            : base(message)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:SPParameterNullException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="innerException">The inner exception.</param>
        public SPParameterNullException(string message, Exception innerException)
            : base(message, innerException)
        { }
        /// <summary>
        /// Initializes a new instance of the <see cref="T:SPParameterNullException"/> class.
        /// </summary>
        /// <param name="info">The info.</param>
        /// <param name="context">The context.</param>
        protected SPParameterNullException(SerializationInfo info,
         StreamingContext context)
            : base(info, context)
        { }


    }
}
