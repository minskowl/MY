
using System;
using System.Collections.Generic;
using System.Text;

namespace CodeRocket.Commands
{
    public class CommandException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CommandException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="innerException">The inner exception.</param>
        public CommandException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}
