using System;
using System.Collections.Generic;
using System.Linq;
using KnowledgeBase.BussinesLayer.Core;

namespace KnowledgeBase.BussinesLayer.Security
{
    /// <summary>
    /// PermissionSet
    /// </summary>
    [Serializable]
    public class PermissionSet : IPermissionSet
    {
        private readonly User _user;
        private Dictionary<int, Permission> _storage;
        private int _maxId;
        private readonly KbContext _context;
        /// <summary>
        /// Gets the <see cref="KnowledgeBase.BussinesLayer.Security.Permission"/> with the specified category ID.
        /// </summary>
        /// <value></value>
        public Permission this[int categoryID]
        {
            get
            {
                if (categoryID <= _maxId)
                {
                    return _storage.ContainsKey(categoryID) ? _storage[categoryID] : Permission.None;
                }

                _context.CategoryTree.Reload();
                LoadData();
                return _storage[categoryID];
            }
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="PermissionSet"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        public PermissionSet(KbContext context)
            : this(context.GetCurrentUser(), context)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PermissionSet"/> class.
        /// </summary>
        /// <param name="user">The user.</param>
        public PermissionSet(User user, KbContext context)
        {
            _context = context;
            _user = user;
            LoadData();
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
            if (this[id].HasPermission(Permission.View)) return true;
            var childrens = _context.CategoryTree.GetChild(id);
            return childrens.Length > 0 ? childrens.Any(HasVisibleChildren) : false;
        }

        /// <summary>
        /// Gets a value indicating whether this instance can edit keywords.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance can edit keywords; otherwise, <c>false</c>.
        /// </value>
        public bool CanEditKeywords
        {
            get { return _user.IsUserAdmin; }
        }

        private void LoadData()
        {
            var tree = _context.CategoryTree;
            _maxId = tree.MaxId;
            _storage = new Dictionary<int, Permission>();
            var user = _context.ManagerUser.GetByID(_user.UserID);
            if (user == null)
            {
                throw new ArgumentException("Invalid userid. ");
            }
            if (user.RootPermission.HasValue)
            {
                AddCategoryPermisssion(0, user.RootPermission.Value, tree);
            }
            var rights = _context.ManageUserRight.GetByUserID(_user.UserID);

            foreach (var right in rights)
            {
                AddCategoryPermisssion(right.CategoryID, right.Permission, tree);
            }
        }



        private void AddCategoryPermisssion(int categoryID, Permission permission, CategoryTree tree)
        {
            if (_storage.ContainsKey(categoryID))
            {
                if (_storage[categoryID] < permission)
                    _storage[categoryID] = permission;
            }
            else
            {
                _storage.Add(categoryID, permission);
            }

            foreach (var child in tree.GetChild(categoryID))
            {
                AddCategoryPermisssion(child, permission, tree);
            }

        }

    }
}
