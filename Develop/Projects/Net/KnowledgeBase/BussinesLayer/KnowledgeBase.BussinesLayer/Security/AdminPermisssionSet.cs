namespace KnowledgeBase.BussinesLayer.Security
{
    public  class AdminPermisssionSet : IPermissionSet
    {
        #region Implementation of IPermissionSet

        /// <summary>
        /// Gets the <see cref="KnowledgeBase.BussinesLayer.Security.Permission"/> with the specified category ID.
        /// </summary>
        /// <value></value>
        public Permission this[int categoryID]
        {
            get { return Permission.Admin; }
        }

        /// <summary>
        /// Determines whether [has visible children] [the specified id].
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns>
        /// 	<c>true</c> if [has visible children] [the specified id]; otherwise, <c>false</c>.
        /// </returns>
        public bool HasVisibleChildren(int id)
        {
            return true;
        }

        /// <summary>
        /// Gets a value indicating whether this instance can edit keywords.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance can edit keywords; otherwise, <c>false</c>.
        /// </value>
        public bool CanEditKeywords
        {
            get { return true; }
        }

        #endregion
    }
}