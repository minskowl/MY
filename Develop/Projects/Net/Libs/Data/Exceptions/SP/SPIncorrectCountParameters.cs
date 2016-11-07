using System;
using System.Runtime.Serialization;

namespace Savchin.Data
{
    /// <summary>
    /// SP Not Find Exception
    /// </summary>
    [Serializable()]
    public class SPIncorrectCountParameters : SPException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="T:SPIncorrectCountParameters"/> class.
        /// </summary>
        public SPIncorrectCountParameters()
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:SPIncorrectCountParameters"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        public SPIncorrectCountParameters(string message)
            : base(message)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:SPIncorrectCountParameters"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="innerException">The inner exception.</param>
        public SPIncorrectCountParameters(string message, Exception innerException)
            : base(message, innerException)
        { }
        /// <summary>
        /// Initializes a new instance of the <see cref="T:SPIncorrectCountParameters"/> class.
        /// </summary>
        /// <param name="info">The info.</param>
        /// <param name="context">The context.</param>
        protected SPIncorrectCountParameters(SerializationInfo info,
         StreamingContext context)
            : base(info, context)
        { }


    }
}
