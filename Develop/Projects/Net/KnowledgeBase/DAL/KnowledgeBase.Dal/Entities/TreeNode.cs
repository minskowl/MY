using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KnowledgeBase.DAL
{
    /// <summary>
    /// TreeNode
    /// </summary>
    public class TreeNode
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="TreeNode"/> class.
        /// </summary>
        /// <param name="parentId">The parent id.</param>
        /// <param name="id">The id.</param>
        public TreeNode(int parentId, int id)
        {
            ParentId = parentId;
            Id = id;
        }
        
        /// <summary>
        /// Gets or sets the parent id.
        /// </summary>
        /// <value>The parent id.</value>
        public int ParentId { get; set; }

        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        /// <value>The id.</value>
        public int Id { get; set; }
    }
}
