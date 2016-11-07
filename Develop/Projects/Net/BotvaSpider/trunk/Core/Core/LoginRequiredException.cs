using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BotvaSpider.Core
{
    [global::System.Serializable]
    public class LoginRequiredException : Exception
    {
        //
        // For guidelines regarding the creation of new exception types, see
        //    http://msdn.microsoft.com/library/default.asp?url=/library/en-us/cpgenref/html/cpconerrorraisinghandlingguidelines.asp
        // and
        //    http://msdn.microsoft.com/library/default.asp?url=/library/en-us/dncscol/html/csharp07192001.asp
        //

        /// <summary>
        /// Initializes a new instance of the <see cref="LoginRequiredException"/> class.
        /// </summary>
        public LoginRequiredException() { }
        /// <summary>
        /// Initializes a new instance of the <see cref="LoginRequiredException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        public LoginRequiredException(string message) : base(message) { }
        /// <summary>
        /// Initializes a new instance of the <see cref="LoginRequiredException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="inner">The inner.</param>
        public LoginRequiredException(string message, Exception inner) : base(message, inner) { }
        protected LoginRequiredException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }
    }
}
