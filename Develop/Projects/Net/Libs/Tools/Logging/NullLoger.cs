using System;

namespace Savchin.Logging
{
    /// <summary>
    /// NullLoger
    /// </summary>
    public class NullLoger : ILogger
    {
        /// <summary>
        /// Adds the message.
        /// </summary>
        /// <param name="severity">The serverity.</param>
        /// <param name="formatString">The format string.</param>
        /// <param name="values">The values.</param>
        public void AddMessage(Severity severity, string formatString, params object[] values)
        {
            
        }

        /// <summary>
        /// Adds the message.
        /// </summary>
        /// <param name="severity">The severity.</param>
        /// <param name="message">The message.</param>
        public void AddMessage(Severity severity, string message)
        {
            
        }

        /// <summary>
        /// Adds the message.
        /// </summary>
        /// <param name="severity">The severity.</param>
        /// <param name="message">The message.</param>
        /// <param name="ex">The ex.</param>
        public void AddMessage(Severity severity, string message, Exception ex)
        {
            
        }

        /// <summary>
        /// Determines whether the specified severity logger is enabled.
        /// </summary>
        /// <param name="severity">The severity.</param>
        /// <returns>
        /// 	<c>true</c> if the specified severity is enabled; otherwise, <c>false</c>.
        /// </returns>
        public bool IsEnabled(Severity severity)
        {
            return false;
        }
    }
}
