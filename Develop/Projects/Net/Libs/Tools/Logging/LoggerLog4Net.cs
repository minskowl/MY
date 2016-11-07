#if !CLIENT

using System;
using log4net;
using log4net.Config;

namespace Savchin.Logging
{
    /// <summary>
    ///Log4Net  Implementation ILogger 
    /// </summary>
    public class LoggerLog4Net : ILogger
    {
        private readonly ILog log;

        static LoggerLog4Net()
        {
            XmlConfigurator.Configure();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LoggerLog4Net"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        public LoggerLog4Net(string name)
        {
            log = LogManager.GetLogger(name);
        }

        #region Implementation of ILogger


        /// <summary>
        /// Adds the message.
        /// </summary>
        /// <param name="severity">The serverity.</param>
        /// <param name="formatString">The format string.</param>
        /// <param name="values">The values.</param>
        public void AddMessage(Severity severity, string formatString, params object[] values)
        {
            switch (severity)
            {
                case Severity.Debug:
                    log.DebugFormat(formatString, values);
                    break;
                case Severity.Info:
                    log.InfoFormat(formatString, values);
                    break;
                case Severity.Warning:
                    log.WarnFormat(formatString, values);
                    break;
                case Severity.Error:
                    log.ErrorFormat(formatString, values);
                    break;
                case Severity.FatalError:
                    log.FatalFormat(formatString, values);
                    break;
                default:
                    throw new ArgumentOutOfRangeException("severity");
            }
        }

        /// <summary>
        /// Adds the message.
        /// </summary>
        /// <param name="severity">The severity.</param>
        /// <param name="message">The message.</param>
        public void AddMessage(Severity severity, string message)
        {
            switch (severity)
            {
                case Severity.Debug:
                    log.Debug(message);
                    break;
                case Severity.Info:
                    log.Info(message);
                    break;
                case Severity.Warning:
                    log.Warn(message);
                    break;
                case Severity.Error:
                    log.Error(message);
                    break;
                case Severity.FatalError:
                    log.Fatal(message);
                    break;
                default:
                    throw new ArgumentOutOfRangeException("severity");
            }
        }

        /// <summary>
        /// Adds the message.
        /// </summary>
        /// <param name="severity">The severity.</param>
        /// <param name="message">The message.</param>
        /// <param name="ex">The ex.</param>
        public void AddMessage(Severity severity, string message, Exception ex)
        {
            switch (severity)
            {
                case Severity.Debug:
                    log.Debug(message, ex);
                    break;
                case Severity.Info:
                    log.Info(message, ex);
                    break;
                case Severity.Warning:
                    log.Warn(message, ex);
                    break;
                case Severity.Error:
                    log.Error(message, ex);
                    break;
                case Severity.FatalError:
                    log.Fatal(message, ex);
                    break;
                default:
                    throw new ArgumentOutOfRangeException("severity");
            }
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
            switch (severity)
            {
                case Severity.Debug:
                    return log.IsDebugEnabled;
                    
                case Severity.Info:
                    return log.IsInfoEnabled;
                case Severity.Warning:
                    return log.IsWarnEnabled;
                case Severity.Error:
                    return log.IsErrorEnabled;
                case Severity.FatalError:
                    return log.IsFatalEnabled;
                default:
                    throw new ArgumentOutOfRangeException("severity");
            }
        }

        #endregion
    }
}
#endif
