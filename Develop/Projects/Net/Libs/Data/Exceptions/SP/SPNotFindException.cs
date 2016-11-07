using System;
using System.Runtime.Serialization;

namespace Savchin.Data
{
    /// <summary>
    /// SP Not Find Exception
    /// </summary>
    [Serializable()]
    public class SPNotFindException : SPException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="T:SPNotFindException"/> class.
        /// </summary>
        public SPNotFindException()
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:SPNotFindException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        public SPNotFindException(string message)
            : base(message)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:SPNotFindException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="innerException">The inner exception.</param>
        public SPNotFindException(string message, Exception innerException)
            : base(message, innerException)
        { }
        /// <summary>
        /// Initializes a new instance of the <see cref="T:SPNotFindException"/> class.
        /// </summary>
        /// <param name="info">The info.</param>
        /// <param name="context">The context.</param>
        protected SPNotFindException(SerializationInfo info,
         StreamingContext context)
            : base(info, context)
        { }


    }
}
