using System.Collections.Generic;
using KnowledgeBase.DAL;

namespace KnowledgeBase.DAL
{
    public interface IUserRightFactory
    {
        /// <summary>
        /// Saves the rights.
        /// </summary>
        /// <param name="userID">The user ID.</param>
        /// <param name="rights">The rights.</param>
        void SaveRights(int userID, IEnumerable<CategoryPermission> rights);

        /// <summary>
        /// Creates the insert command.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        void Insert(UserRightValue value);

        /// <summary>
        /// Updates the specified UserRight.
        /// </summary>
        /// <param name="value">The UserRight value.</param>
        void Update(UserRightValue value);

        /// <summary>
        /// Gets UserRight by ID.
        /// </summary>
        /// <param name="CategoryID">The CategoryID.</param>
        /// <param name="UserID">The UserID.</param>
        /// <returns></returns>
        UserRightValue SelectByID(System.Int32 CategoryID, System.Int32 UserID);

        /// <summary>
        /// Deletes the specified UserRight.
        /// </summary>
        /// <param name="CategoryID">The CategoryID.</param>
        /// <param name="UserID">The UserID.</param>
        void Delete(System.Int32 CategoryID, System.Int32 UserID);

        /// <summary>
        /// Deletes the specified UserRight.
        /// </summary>
        /// <param name="value">The value.</param>
        void Delete(UserRightValue value);

        /// <summary>
        /// Selects all UserRight values.
        /// </summary>
        /// <returns>List of all UserRight</returns>
        IList<UserRightValue> SelectAll();

        /// <summary>
        /// Selects UserRight values CategoryID .
        /// ForeignKey: FK_UserRights_Categories
        /// </summary>
        /// <param name="CategoryID">The CategoryID.</param>
        /// <returns>List of UserRight</returns>   
        IList<UserRightValue> SelectByCategoryID(System.Int32 CategoryID);

        /// <summary>
        /// Selects UserRight values UserID .
        /// ForeignKey: FK_UserRights_Users
        /// </summary>
        /// <param name="UserID">The UserID.</param>
        /// <returns>List of UserRight</returns>   
        IList<UserRightValue> SelectByUserID(System.Int32 UserID);
    }
}