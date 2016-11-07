using System.Collections.Generic;
using Savchin.Core;
using Savchin.Forms.Core.Commands;

namespace KnowledgeBase.KbTools.Commands
{
    /// <summary>
    /// CommandCollection
    /// </summary>
    public class CommandCollection
    {
        readonly Dictionary<string, ICommand> storage = new Dictionary<string, ICommand>();
        /// <summary>
        /// Gets the <see cref="ICommand"/> with the specified key.
        /// </summary>
        /// <value></value>
        public ICommand this[string key]
        {
            get { return storage.ContainsKey(key) ? storage[key] : null; }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CommandCollection"/> class.
        /// </summary>
        public CommandCollection()
        {
            storage.Add("ExportToHtml", new ExportToHtmlCommand());
        }
    }
}
