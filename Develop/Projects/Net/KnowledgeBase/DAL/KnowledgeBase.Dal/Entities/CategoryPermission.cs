using System;
using System.Collections.Generic;
using System.Text;

namespace KnowledgeBase.DAL
{
    /// <summary>
    /// 
    /// </summary>
    public class CategoryPermission
    {
        #region Properties
        private int categoryID;
        private short permissionID;
        /// <summary>
        /// Gets or sets the category ID.
        /// </summary>
        /// <value>The category ID.</value>
        public int CategoryID
        {
            get { return categoryID; }
            set { categoryID = value; }
        }

        /// <summary>
        /// Gets or sets the permission ID.
        /// </summary>
        /// <value>The permission ID.</value>
        public short PermissionID
        {
            get { return permissionID; }
            set { permissionID = value; }
        } 
        #endregion
        /// <summary>
        /// Initializes a new instance of the <see cref="CategoryPermission"/> class.
        /// </summary>
        public CategoryPermission()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CategoryPermission"/> class.
        /// </summary>
        /// <param name="categoryID">The category ID.</param>
        /// <param name="permissionID">The permission ID.</param>
        public CategoryPermission(int categoryID, short permissionID)
        {
            this.CategoryID = categoryID;
            this.PermissionID = permissionID;
        }


    }
}
