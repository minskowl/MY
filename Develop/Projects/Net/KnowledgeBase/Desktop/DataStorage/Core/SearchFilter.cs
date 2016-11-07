using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KnowledgeBase.BussinesLayer;

namespace KnowledgeBase.Desktop.Core
{
    public class SearchFilter
    {
        /// <summary>
        /// Gets or sets the text.
        /// </summary>
        /// <value>The text.</value>
        public string Text { get; set; }
        /// <summary>
        /// Gets or sets the types.
        /// </summary>
        /// <value>The types.</value>
        public IList<KnowledgeType> Types { get; set; }
        /// <summary>
        /// Gets or sets the categories.
        /// </summary>
        /// <value>The categories.</value>
        public IList<int> Categories { get; set; }
        /// <summary>
        /// Gets or sets the keywords.
        /// </summary>
        /// <value>The keywords.</value>
        public IList<int> Keywords { get; set; }
        /// <summary>
        /// Gets or sets the statuses.
        /// </summary>
        /// <value>The statuses.</value>
        public IList<KnowledgeStatus> Statuses { get; set; }
    }
}
