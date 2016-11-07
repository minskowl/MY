using System;

namespace Savchin.Logging
{
    /// <summary>
    /// Defines logger interfaces
    /// </summary>
    public interface ILogger
    {
        /// <summary>
        /// Adds the message.
        /// </summary>
        /// <param name="severity">The serverity.</param>
        /// <param name="formatString">The format string.</param>
        /// <param name="values">The values.</param>
        void AddMessage(Severity severity, string formatString, params object[] values);
        /// <summary>
        /// Adds the message.
        /// </summary>
        /// <param name="severity">The severity.</param>
        /// <param name="message">The message.</param>
        void AddMessage(Severity severity, string message);
        /// <summary>
        /// Adds the message.
        /// </summary>
        /// <param name="severity">The severity.</param>
        /// <param name="message">The message.</param>
        /// <param name="ex">The ex.</param>
        void AddMessage(Severity severity, string message, Exception ex);


        /// <summary>
        /// Determines whether the specified severity logger is enabled.
        /// </summary>
        /// <param name="severity">The severity.</param>
        /// <returns>
        /// 	<c>true</c> if the specified severity is enabled; otherwise, <c>false</c>.
        /// </returns>
        bool IsEnabled(Severity severity);
    }
}