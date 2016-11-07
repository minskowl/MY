namespace KnowledgeBase.BussinesLayer.Security
{
    public interface IPermissionSet
    {
        /// <summary>
        /// Gets the <see cref="KnowledgeBase.BussinesLayer.Security.Permission"/> with the specified category ID.
        /// </summary>
        /// <value></value>
        Permission this[int categoryID] { get; }

        /// <summary>
        /// Determines whether [has visible children] [the specified id].
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns>
        /// 	<c>true</c> if [has visible children] [the specified id]; otherwise, <c>false</c>.
        /// </returns>
        bool HasVisibleChildren(int id);

        /// <summary>
        /// Gets a value indicating whether this instance can edit keywords.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance can edit keywords; otherwise, <c>false</c>.
        /// </value>
        bool CanEditKeywords { get; }
    }
}