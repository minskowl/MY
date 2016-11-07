using System;
using System.Linq;
using System.Collections.Generic;
using BotvaSpider.Core;
using log4net;

namespace BotvaSpider.Logging
{
    /// <summary>
    /// Logger
    /// </summary>
    public class Logger : ILogger
    {
        #region Properties
        private readonly List<LogEntry> entries = new List<LogEntry>();
        private readonly LoggerType _loggerType;
        private readonly ILog log;

        /// <summary>
        /// Gets the entries.
        /// </summary>
        /// <value>The entries.</value>
        public List<LogEntry> Entries
        {
            get { return entries; }
        }

        /// <summary>
        /// Gets the log.
        /// </summary>
        /// <value>The log.</value>
        public ILog Log
        {
            get { return log; }
        }
        
        #endregion

        /// <summary>
        /// Occurs when [entry added].
        /// </summary>
        public event EventHandler<LogEntryEventArgs> EntryAdded;

        /// <summary>
        /// Initializes a new instance of the <see cref="Logger"/> class.
        /// </summary>
        /// <param name="type">The type.</param>
        public Logger(LoggerType type)
        {
            _loggerType = type;
            log = LogManager.GetLogger(type.ToString());
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Logger"/> class.
        /// </summary>
        public Logger()
        {

        }

        #region Common Add
        /// <summary>
        /// Adds the specified type.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="title">The title.</param>
        public void Add(LogEntryType type, string title)
        {
            Add(type, title, null, null);
        }

        /// <summary>
        /// Adds the specified type.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="title">The title.</param>
        /// <param name="obj">The obj.</param>
        public void Add(LogEntryType type, string title, Object obj)
        {
            Add(type, title, null, obj);
        }

        /// <summary>
        /// Adds the specified type.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="title">The title.</param>
        /// <param name="message">The message.</param>
        public void Add(LogEntryType type, string title, string message)
        {
            Add(type, title, message, null);
        }
        /// <summary>
        /// Adds the specified type.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="title">The title.</param>
        /// <param name="message">The message.</param>
        /// <param name="obj">The obj.</param>
        public void Add(LogEntryType type, string title, string message, Object obj)
        {
            if (type == LogEntryType.Suggestion)
            {
                //Suggestion unique events
                if (entries.Any(e => e.Type == LogEntryType.Suggestion && e.Title == title && e.Message == message))
                    return;
            }

            var entry = new LogEntry
            {
                Source = _loggerType,
                Type = type,
                Title = title,
                Message = message,
                Object = obj,
                Date = DateTime.Now
            };


            if (Log != null) WriteToLog(entry);

            entries.Add(entry);

            OnEntryAdded(this, new LogEntryEventArgs(entry));
        } 
        #endregion

        #region Debug
        /// <summary>
        /// Infoes the specified text.
        /// </summary>
        /// <param name="text">The text.</param>
        public void Debug(string text)
        {
            Add(LogEntryType.Debug, text, null, null);
        }
        /// <summary>
        /// Infoes the specified text.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <param name="o">The o.</param>
        public void Debug(string text, Object o)
        {
            Add(LogEntryType.Debug, text, null, o);
        }
        /// <summary>
        /// Infoes the specified text.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <param name="message">The message.</param>
        public void Debug(string text, string message)
        {
            Add(LogEntryType.Debug, text, message, null);
        }

        /// <summary>
        /// Infoes the specified text.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <param name="message">The message.</param>
        /// <param name="o">The o.</param>
        public void Debug(string text, string message, Object o)
        {
            Add(LogEntryType.Debug, text, message, o);
        }

        /// <summary>
        /// Warns the format.
        /// </summary>
        /// <param name="format">The format.</param>
        /// <param name="args">The args.</param>
        public void DebugFormat(string format, params object[] args)
        {
            Add(LogEntryType.Debug, string.Format(format, args));
        }

        #endregion

        #region Info
        /// <summary>
        /// Infoes the specified text.
        /// </summary>
        /// <param name="text">The text.</param>
        public void Info(string text)
        {
            Add(LogEntryType.Info, text, null, null);
        }
        /// <summary>
        /// Infoes the specified text.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <param name="o">The o.</param>
        public void Info(string text, Object o)
        {
            Add(LogEntryType.Info, text, null, o);
        }
        /// <summary>
        /// Infoes the specified text.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <param name="message">The message.</param>
        public void Info(string text, string message)
        {
            Add(LogEntryType.Info, text, message, null);
        }

        /// <summary>
        /// Infoes the specified text.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <param name="message">The message.</param>
        /// <param name="o">The o.</param>
        public void Info(string text, string message, Object o)
        {
            Add(LogEntryType.Info, text, message, o);
        }

        /// <summary>
        /// Warns the format.
        /// </summary>
        /// <param name="format">The format.</param>
        /// <param name="args">The args.</param>
        public void InfoFormat(string format, params object[] args)
        {
            Add(LogEntryType.Info, string.Format(format, args));
        }

        #endregion

        #region Warm
        /// <summary>
        /// Warns the specified title.
        /// </summary>
        /// <param name="title">The title.</param>
        public void Warn(string title)
        {
            Add(LogEntryType.Warning, title, null, null);
        }
        /// <summary>
        /// Warns the specified title.
        /// </summary>
        /// <param name="title">The title.</param>
        /// <param name="message">The message.</param>
        public void Warn(string title, string message)
        {
            Add(LogEntryType.Warning, title, message, null);
        }

        /// <summary>
        /// Warns the specified title.
        /// </summary>
        /// <param name="title">The title.</param>
        /// <param name="message">The message.</param>
        /// <param name="ex">The ex.</param>
        public void Warn(string title, string message, object ex)
        {
            Add(LogEntryType.Warning, title, message, ex);
        }

        /// <summary>
        /// Adds the warn.
        /// </summary>
        /// <param name="title">The title.</param>
        /// <param name="ex">The ex.</param>
        public void Warn(string title, Exception ex)
        {
            Add(LogEntryType.Warning, title, null, ex);
        }
        /// <summary>
        /// Warns the specified title.
        /// </summary>
        /// <param name="title">The title.</param>
        /// <param name="ex">The ex.</param>
        public void Warn(string title, object ex)
        {
            Add(LogEntryType.Warning, title, null, ex);
        }
        /// <summary>
        /// Warns the format.
        /// </summary>
        /// <param name="format">The format.</param>
        /// <param name="args">The args.</param>
        public void WarnFormat(string format, params object[] args)
        {
            Add(LogEntryType.Warning, string.Format(format, args));
        }
        #endregion

        #region Suggestion
        /// <summary>
        /// Suggestions the specified title.
        /// </summary>
        /// <param name="title">The title.</param>
        /// <param name="message">The message.</param>
        public void Suggestion(string title, string message)
        {
            Add(LogEntryType.Suggestion, title, message, null);
        }

        #endregion

        #region Error

        /// <summary>
        /// Adds the error.
        /// </summary>
        /// <param name="title">The title.</param>
        public void Error(string title)
        {
            Add(LogEntryType.Error, title, null, null);
        }
        /// <summary>
        /// Errors the specified title.
        /// </summary>
        /// <param name="title">The title.</param>
        /// <param name="message">The message.</param>
        public void Error(string title, string message)
        {
            Add(LogEntryType.Error, title, message, null);
        }

        /// <summary>
        /// Errors the specified title.
        /// </summary>
        /// <param name="title">The title.</param>
        /// <param name="message">The message.</param>
        /// <param name="ex">The ex.</param>
        public void Error(string title, string message, Exception ex)
        {
            Add(LogEntryType.Error, title, message, ex);
        }

        /// <summary>
        /// Adds the error.
        /// </summary>
        /// <param name="title">The title.</param>
        /// <param name="ex">The ex.</param>
        public void Error(string title, Exception ex)
        {
            Add(LogEntryType.Error, title, null, ex);
        }


        #endregion

        /// <summary>
        /// Called when [entry added].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected virtual void OnEntryAdded(object sender, LogEntryEventArgs e)
        {
            if (EntryAdded != null)
                EntryAdded(sender, e);
        }

        private void WriteToLog(LogEntry entry)
        {
            switch (entry.Type)
            {
                case LogEntryType.Debug:
                    if (entry.Object is Exception)
                    {
                        Log.Debug(entry.Title, (Exception)entry.Object);
                    }
                    else
                    {
                        Log.Debug(entry);
                    }
                    break;
                case LogEntryType.Info:
                    if (entry.Object is Exception)
                    {
                        Log.Info(entry.Title, (Exception)entry.Object);
                    }
                    else
                    {
                        Log.Info(entry);
                    }
                    break;
                case LogEntryType.Warning:
                    if (entry.Object is Exception)
                    {
                        Log.Warn(entry.Title, (Exception)entry.Object);
                    }
                    else
                    {
                        Log.Warn(entry);
                    }
                    break;
                case LogEntryType.Error:
                    if (entry.Object is Exception)
                    {
                        Log.Error(entry.Title, (Exception)entry.Object);
                    }
                    else
                    {
                        Log.Error(entry);
                    }
                    break;
                default:
                    break;
            }
        }




    }
}
