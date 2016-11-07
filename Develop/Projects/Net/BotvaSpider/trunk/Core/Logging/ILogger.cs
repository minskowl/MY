using System;
using System.Collections.Generic;

namespace BotvaSpider.Logging
{
    public interface ILogger
    {
        /// <summary>
        /// Gets the entries.
        /// </summary>
        /// <value>The entries.</value>
        List<LogEntry> Entries { get; }

        /// <summary>
        /// Occurs when [entry added].
        /// </summary>
        event EventHandler<LogEntryEventArgs> EntryAdded;
    }
}