using System;
using System.Collections.Generic;
using System.Linq;

namespace BotvaSpider.Logging
{
    internal class CompositLogger : ILogger
    {
        private List<ILogger> innerLoggers= new List<ILogger>();
        /// <summary>
        /// Gets the entries.
        /// </summary>
        /// <value>The entries.</value>
        public List<LogEntry> Entries
        {
            get {
                IEnumerable<LogEntry> results = new List<LogEntry>();
                foreach (var logger in innerLoggers)
                {
                  results=  results.Union(logger.Entries);
                }
                return results.OrderBy(e => e.Date).ToList();
            }
        }

        public event EventHandler<LogEntryEventArgs> EntryAdded;

 

        /// <summary>
        /// Adds the specified logger.
        /// </summary>
        /// <param name="logger">The logger.</param>
        public void Add(ILogger logger)
        {
            if (logger == null) throw new ArgumentNullException("logger");
            innerLoggers.Add(logger);
            logger.EntryAdded += OnEntryAdded;
        }

 
        protected virtual void OnEntryAdded(object sender, LogEntryEventArgs e)
        {
            if (EntryAdded != null)
                EntryAdded(sender, e);
        }
    }
}
