using System;

namespace KnowledgeBase.Desktop.Core
{
    public enum ObjectType
    {
        Category,
        Knowledge,
        File,
        Keyword,
        User
    }

    /// <summary>
    /// ItemParams
    /// </summary>
    public class ItemParams
    {
        /// <summary>
        /// Gets or sets the type.
        /// </summary>
        /// <value>The type.</value>
        public ObjectType Type { get; set; }

        /// <summary>
        /// Gets or sets the parent id.
        /// </summary>
        /// <value>The parent id.</value>
        public Object Id { get; set; }

        /// <summary>
        /// Gets or sets the int id.
        /// </summary>
        /// <value>The int id.</value>
        public int IntId { get { return (int)Id; } }

        /// <summary>
        /// Initializes a new instance of the <see cref="ItemParams"/> class.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="id">The parent id.</param>
        public ItemParams(ObjectType type, Object id)
        {
            Type = type;
            Id = id;
        }
    }
}
