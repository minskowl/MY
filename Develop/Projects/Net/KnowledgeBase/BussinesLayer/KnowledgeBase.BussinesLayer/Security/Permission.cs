using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using KnowledgeBase.BussinesLayer.Core;
using Savchin.Core;

namespace KnowledgeBase.BussinesLayer.Security
{
    /// <summary>
    /// Permission
    /// </summary>
    public enum Permission : short
    {
        /// <summary>
        /// View
        /// </summary>
        View = 1,
        /// <summary>
        /// Publish
        /// </summary>
        Publish = 2,
        /// <summary>
        /// Moderate
        /// </summary>
        Moderate = 3,
        /// <summary>
        /// Admin
        /// </summary>
        Admin = 4,

        /// <summary>
        /// None
        /// </summary>
        None = 0
    }


    public static class PermissionHelper
    {
        /// <summary>
        /// Determines whether the specified has permission has permission.
        /// </summary>
        /// <param name="hasPermission">The has permission.</param>
        /// <param name="requirePermission">The require permission.</param>
        /// <returns>
        /// 	<c>true</c> if the specified has permission has permission; otherwise, <c>false</c>.
        /// </returns>
        public static bool HasPermission(this Permission hasPermission, Permission requirePermission)
        {
            return (short)hasPermission >= (short)requirePermission;
        }

        #region Knowledge
        /// <summary>
        /// Determines whether this instance can view the specified permission.
        /// </summary>
        /// <param name="permission">The permission.</param>
        /// <param name="status">The status.</param>
        /// <returns>
        /// 	<c>true</c> if this instance can view the specified permission; otherwise, <c>false</c>.
        /// </returns>
        public static bool CanViewKnowledge(this Permission permission, KnowledgeStatus status)
        {
            switch (status)
            {
                case KnowledgeStatus.New:
                    return permission.HasPermission(Permission.Publish);
                case KnowledgeStatus.Active:
                    return permission.HasPermission(Permission.View);
                case KnowledgeStatus.Hide:
                    return permission.HasPermission(Permission.Moderate);
                case KnowledgeStatus.Deleted:
                    return permission.HasPermission(Permission.Admin);
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        /// <summary>
        /// Determines whether this instance [can edit knowledge] the specified permission.
        /// </summary>
        /// <param name="permission">The permission.</param>
        /// <param name="status">The status.</param>
        /// <param name="creatorId">The creator ID.</param>
        /// <returns>
        /// 	<c>true</c> if this instance [can edit knowledge] the specified permission; otherwise, <c>false</c>.
        /// </returns>
        public static bool CanEditKnowledge(this Permission permission, KnowledgeStatus status, int creatorId)
        {
            if (status == KnowledgeStatus.Deleted)
            {
                return permission.HasPermission(Permission.Admin);
            }
            return
                 (permission >= Permission.Moderate) ||
                 (permission == Permission.Publish && creatorId == KbContext.CurrentUserId);



        }

        private static readonly Dictionary<Permission, KnowledgeStatus[]> _viewableStatusesCache = new Dictionary<Permission, KnowledgeStatus[]>();
        /// <summary>
        /// Gets the viewable statuses.
        /// </summary>
        /// <param name="permission">The permission.</param>
        /// <returns></returns>
        public static KnowledgeStatus[] GetViewableStatuses(this Permission permission)
        {
            if (_viewableStatusesCache.ContainsKey(permission))
            {
                return _viewableStatusesCache[permission];
            }
            var result = EnumHelper.GetValues<KnowledgeStatus>()
                .Where(e => permission.CanViewKnowledge(e)).ToArray();
            _viewableStatusesCache[permission] = result;
            return result;
        }

        private static readonly Dictionary<Permission, KnowledgeStatus[]> _notViewableStatusesCache = new Dictionary<Permission, KnowledgeStatus[]>();

        /// <summary>
        /// Gets the not viewable statuses.
        /// </summary>
        /// <param name="permission">The permission.</param>
        /// <returns></returns>
        public static KnowledgeStatus[] GetNotViewableStatuses(this Permission permission)
        {
            if (_notViewableStatusesCache.ContainsKey(permission))
            {
                return _notViewableStatusesCache[permission];
            }
            var result = EnumHelper.GetValues<KnowledgeStatus>()
                .Where(e => !permission.CanViewKnowledge(e)).ToArray();
            _notViewableStatusesCache[permission] = result;
            return result;
        }
        #endregion



        /// <summary>
        /// Determines whether this instance [can view category] the specified permission.
        /// </summary>
        /// <param name="permission">The permission.</param>
        /// <returns>
        /// 	<c>true</c> if this instance [can view category] the specified permission; otherwise, <c>false</c>.
        /// </returns>
        public static bool CanViewCategory(this Permission permission)
        {
            return permission.HasPermission(Permission.View);
        }

        /// <summary>
        /// Determines whether this instance [can edit category] the specified permission.
        /// </summary>
        /// <param name="permission">The permission.</param>
        /// <returns>
        /// 	<c>true</c> if this instance [can edit category] the specified permission; otherwise, <c>false</c>.
        /// </returns>
        public static bool CanEditCategory(this Permission permission)
        {
            return permission.HasPermission(Permission.Admin);
        }
    }
}
