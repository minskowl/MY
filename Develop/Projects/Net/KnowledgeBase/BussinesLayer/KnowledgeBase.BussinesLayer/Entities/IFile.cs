using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KnowledgeBase.BussinesLayer
{
    public interface IFile
    {
        /// <summary>
        /// Gets or sets the name of the file.
        /// </summary>
        /// <value>The name of the file.</value>
        String FileName { get; set; }

        /// <summary>
        /// Gets or sets the size.
        /// </summary>
        /// <value>The size.</value>
        Int32 Size { get; set; }

        /// <summary>
        /// Gets the query string.
        /// </summary>
        /// <value>The query string.</value>
        String QueryString { get; }

        /// <summary>
        /// Gets the ID.
        /// </summary>
        /// <value>The ID.</value>
        Object ID { get;  }
    }
}
