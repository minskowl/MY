using KnowledgeBase.BussinesLayer.Core;
using KnowledgeBase.BussinesLayer.Security;

namespace KnowledgeBase.Desktop.Core
{
    static class SecurityAgent
    {
        /// <summary>
        /// Checks the permission.
        /// </summary>
        /// <param name="categoryId">The category id.</param>
        /// <param name="permission">The permission.</param>
        /// <returns></returns>
        public static bool CheckPermission(int categoryId, Permission permission)
        {
            if (KbContext.CurrentKb.HasPermission(categoryId, permission)) return true;
            Messages.ShowSecurityAlert(string.Format("Operation required {0} permissions.",permission));
            return false;
        }

        /// <summary>
        /// Determines whether [has admin rights] [the specified category id].
        /// </summary>
        /// <param name="categoryId">The category id.</param>
        /// <returns>
        /// 	<c>true</c> if [has admin rights] [the specified category id]; otherwise, <c>false</c>.
        /// </returns>
        public static bool HasAdminRights(int categoryId)
        {
            return CheckPermission(categoryId,Permission.Admin);
        }
    }
}
