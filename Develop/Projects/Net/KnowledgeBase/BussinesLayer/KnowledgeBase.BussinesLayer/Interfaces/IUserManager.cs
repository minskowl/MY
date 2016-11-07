using System;
using System.Collections.Generic;
using KnowledgeBase.BussinesLayer.Security;

namespace KnowledgeBase.BussinesLayer
{
    public interface IUserManager
    {
        /// <summary>
        /// Selects all.
        /// </summary>
        /// <returns></returns> 
        IList<User> GetAll();

        /// <summary>
        /// Ges the by ID.
        /// </summary>
        /// <param name="UserID">The user ID.</param>
        /// <returns></returns>
        User GetByID(Int32 UserID);

        User Login(string login, string password);

        /// <summary>
        /// Gets the by login.
        /// </summary>
        /// <param name="Login">The login.</param>
        /// <returns></returns>
        User GetByLogin(string Login);

        /// <summary>
        /// Saves the specified user.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="permissions">The permissions.</param>
        void Save(User user, Dictionary<int, Permission> permissions);

        /// <summary>
        /// Saves the specified User.
        /// </summary>
        /// <param name="entity">The entity.</param>
        void Save(User entity);

        /// <summary>
        /// Deletes the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        void Delete(User entity);

        /// <summary>
        /// Determines whether this instance can login the specified login.
        /// </summary>
        /// <param name="login">The login.</param>
        /// <param name="password">The password.</param>
        /// <returns>
        /// 	<c>true</c> if this instance can login the specified login; otherwise, <c>false</c>.
        /// </returns>
        bool CanLogin(string login, string password);
    }
}