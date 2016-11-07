using System;

namespace BotvaSpider.Logging
{
    /// <summary>
    /// LogEntryEventArgs
    /// </summary>
    public class LogEntryEventArgs : EventArgs
    {
        private readonly LogEntry entry;



        /// <summary>
        /// Gets the entry.
        /// </summary>
        /// <value>The entry.</value>
        public LogEntry Entry
        {
            get { return entry; }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LogEntryEventArgs"/> class.
        /// </summary>
        /// <param name="entry">The entry.</param>
        public LogEntryEventArgs(LogEntry entry)
        {
            this.entry = entry;
        }
    }
}