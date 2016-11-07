using System;

namespace Savchin.Collection.Sorting
{
    /// <summary>
    /// Exception thrown when an expression cannot be parsed.
    /// </summary>
    [global::System.Serializable]
    public class ParserException : ApplicationException
    {
        /// <summary>
        /// Gets the position at which the error was found (as a zero-based index). -1 if position not applicable or not known.
        /// </summary>
        public int Position { get; private set; }

        /// <summary>
        /// Gets the expression being parsed when error was found (null if expression not known).
        /// </summary>
        public String Expression { get; private set; }

        public ParserException() { Position = -1;  }

        /// <summary>
        /// Creates a new ParserException specifying the position, expression and a custom message.
        /// </summary>
        /// <param name="position">Position in expression that error was found.</param>
        /// <param name="expression">Expression being parsed.</param>
        /// <param name="message">Custom error message</param>
        public ParserException(int position, String expression, String message)
            : base(message)
        {
            Position = position;
            Expression = expression;
        }
        public ParserException(string message) : base(message) { Position = -1; }
        public ParserException(string message, Exception inner) : base(message, inner) { Position = -1; }
        /// <summary>
        /// Initializes a new instance of the <see cref="ParserException"/> class.
        /// </summary>
        /// <param name="info">The object that holds the serialized object data.</param>
        /// <param name="context">The contextual information about the source or destination.</param>
        protected ParserException(
             global::System.Runtime.Serialization.SerializationInfo info,
             global::System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }
    }
}