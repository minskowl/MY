using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KnowledgeBase.Desktop.Core
{
    /// <summary>
    /// 
    /// </summary>
    public class ObjectIdentifierEventArgs : EventArgs
    {
        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        /// <value>The id.</value>
        public int Id { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ObjectIdentifierEventArgs"/> class.
        /// </summary>
        /// <param name="id">The id.</param>
        public ObjectIdentifierEventArgs(int id)
        {
            Id = id;
        }
    }
}
