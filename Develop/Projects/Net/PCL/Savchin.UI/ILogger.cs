using System;

namespace CI.Common.Logging
{
    public interface ILogger
    {
        /// <summary>
        /// Gets a value indicating whether this instance is debug enabled.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is debug enabled; otherwise, <c>false</c>.
        /// </value>
        bool IsTraceEnabled { get; }

        /// <summary>
        /// Gets a value indicating whether this instance is information enabled.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is information enabled; otherwise, <c>false</c>.
        /// </value>
        bool IsInfoEnabled { get; }

        /// <summary>
        /// Gets a value indicating whether this instance is warning enabled.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is warning enabled; otherwise, <c>false</c>.
        /// </value>
        bool IsWarningEnabled { get; }

        /// <summary>
        /// Gets a value indicating whether this instance is debug enabled.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is debug enabled; otherwise, <c>false</c>.
        /// </value>
        bool IsDebugEnabled { get; }

        /// <summary>
        /// Traces the specified format.
        /// </summary>
        /// <param name="message">The message.</param>
        void Trace(string message);

        /// <summary>
        /// Exceptions the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        void Error(string message);

        /// <summary>
        /// Errors the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="exception">The exception.</param>
        void Error(string message, Exception exception);

        /// <summary>
        /// Errors the specified exception.
        /// </summary>
        /// <param name="exception">The exception.</param>
        void Error(Exception exception);

        /// <summary>
        /// Exceptions the specified format string.
        /// </summary>
        /// <param name="format">The format string.</param>
        /// <param name="arguments">The arguments.</param>
        void Error(string format, params object[] arguments);

        /// <summary>
        /// Informations the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        void Info(string message);

        /// <summary>
        /// Informations the specified format string.
        /// </summary>
        /// <param name="format">The format string.</param>
        /// <param name="arguments">The arguments.</param>
        void Info(string format, params object[] arguments);

        /// <summary>
        /// Warnings the specified exception.
        /// </summary>
        /// <param name="exception">The exception.</param>
        void Warning(Exception exception);

        /// <summary>
        /// Warnings the specified text.
        /// </summary>
        /// <param name="text">The text.</param>
        void Warning(string text);

        /// <summary>
        /// Warnings the specified format string.
        /// </summary>
        /// <param name="format">The format string.</param>
        /// <param name="arguments">The arguments.</param>
        void Warning(string format, params object[] arguments);

        /// <summary>
        /// Debugs the specified format string.
        /// </summary>
        /// <param name="format">The format string.</param>
        /// <param name="arguments">The arguments.</param>
        void Debug(string format, params object[] arguments);

        /// <summary>
        /// Debugs the specified object.
        /// </summary>
        /// <param name="value">The object.</param>
        void Debug(object value);

        /// <summary>
        /// Traces the specified format string.
        /// </summary>
        /// <param name="format">The format string.</param>
        /// <param name="arguments">The arguments.</param>
        void Trace(string format, params object[] arguments);

        /// <summary>
        /// Fatals the specified text.
        /// </summary>
        /// <param name="text">The text.</param>
        void Fatal(string text);

        /// <summary>
        /// Fatals the specified text.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <param name="exception">The exception.</param>
        void Fatal(string text, Exception exception);

        /// <summary>
        /// Fatals the specified exception.
        /// </summary>
        /// <param name="exception">The exception.</param>
        void Fatal(Exception exception);
    }

   

}
