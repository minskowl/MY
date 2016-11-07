using System.Collections.Generic;
using KnowledgeBase.DAL;

namespace KnowledgeBase.BussinesLayer
{
    public interface IUserRightManager
    {
        /// <summary>
        /// Get all.
        /// </summary>
        /// <returns></returns> 
        IList<UserRight> GetAll();

        /// <summary>
        /// Ges the UserRight by ID.
        /// </summary>
        /// <param name="CategoryID">The CategoryID.</param>
        /// <param name="UserID">The UserID.</param>
        /// <returns></returns>        
        UserRight GetByID(System.Int32 CategoryID, System.Int32 UserID);

        /// <summary>
        /// Get UserRight values CategoryID .
        /// </summary>
        /// <param name="CategoryID">The CategoryID.</param>
        /// <returns>List of UserRight</returns>   
        IList<UserRight> GetByCategoryID(System.Int32 CategoryID);

        /// <summary>
        /// Get UserRight values UserID .
        /// </summary>
        /// <param name="UserID">The UserID.</param>
        /// <returns>List of UserRight</returns>   
        IList<UserRight> GetByUserID(System.Int32 UserID);

        /// <summary>
        /// Saves the rights.
        /// </summary>
        /// <param name="userID">The user ID.</param>
        /// <param name="rights">The rights.</param>
        void SaveRights(int userID, IEnumerable<CategoryPermission> rights);

        /// <summary>
        /// Deletes the specified UserRight.
        /// </summary>
        /// <param name="entity">The entity.</param>
        void Delete(UserRight entity);
    }
}