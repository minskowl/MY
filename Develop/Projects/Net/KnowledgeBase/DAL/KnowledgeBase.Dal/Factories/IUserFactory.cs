using System.Collections.Generic;
using KnowledgeBase.DAL;

namespace KnowledgeBase.DAL
{
    public interface IUserFactory
    {
        /// <summary>
        /// Creates the insert command.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        void Insert(UserValue value);

        /// <summary>
        /// Updates the specified User.
        /// </summary>
        /// <param name="value">The User value.</param>
        void Update(UserValue value);

        /// <summary>
        /// Gets User by ID.
        /// </summary>
        /// <param name="UserID">The UserID.</param>
        /// <returns></returns>
        UserValue SelectByID(System.Int32 UserID);

        /// <summary>
        /// Deletes the specified User.
        /// </summary>
        /// <param name="UserID">The UserID.</param>
        void Delete(System.Int32 UserID);

        /// <summary>
        /// Deletes the specified User.
        /// </summary>
        /// <param name="value">The value.</param>
        void Delete(UserValue value);

        /// <summary>
        /// Selects all User values.
        /// </summary>
        /// <returns>List of all User</returns>
        IList<UserValue> SelectAll();

        /// <summary>
        /// Selects User values RootPermissionID .
        /// ForeignKey: FK_Users_Permissions
        /// </summary>
        /// <param name="RootPermissionID">The RootPermissionID.</param>
        /// <returns>List of User</returns>   
        IList<UserValue> SelectByRootPermissionID( System.Int16 RootPermissionID);

        /// <summary>
        /// Gets the by login by password.
        /// </summary>
        /// <param name="Login">The login.</param>
        /// <param name="Password">The password.</param>
        /// <returns></returns>
        UserValue GetByLoginByPassword(string Login, string Password);

        /// <summary>
        /// Gets the by login.
        /// </summary>
        /// <param name="Login">The login.</param>
        /// <returns></returns>
        UserValue GetByLogin(string Login);

        /// <summary>
        /// Logins the specified login.
        /// </summary>
        /// <param name="login">The login.</param>
        /// <param name="password">The password.</param>
        /// <returns></returns>
        UserValue Login(string login, string password);
    }
}