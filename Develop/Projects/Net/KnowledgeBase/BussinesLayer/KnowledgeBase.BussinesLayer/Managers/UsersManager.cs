

/******************************************
* Auto-generated by CodeRocket
* 08.05.2008 22:22:06
******************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using KnowledgeBase.BussinesLayer.Core;
using KnowledgeBase.BussinesLayer.Security;
using KnowledgeBase.Core;
using KnowledgeBase.DAL;

namespace KnowledgeBase.BussinesLayer.Managers
{

    /// <summary>
    /// User Manager class
    ///</summary>
    public class UserManager : ManagerBase<IUserFactory, User, UserValue>, IUserManager
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UserManager"/> class.
        /// </summary>
        public UserManager(KbContext context, IFactoryProvider provider)
            : base(context,provider.CreateUserFactory())
        {
          
        }
        /// <summary>
        /// Selects all.
        /// </summary>
        /// <returns></returns> 
        public IList<User> GetAll()
        {
            return Wrap(Factory.SelectAll());
        }
        /// <summary>
        /// Ges the by ID.
        /// </summary>
        /// <param name="UserID">The user ID.</param>
        /// <returns></returns>
        public User GetByID(Int32 UserID)
        {
            return Wrap(Factory.SelectByID(UserID));
        }

        public User Login(string login, string password)
        {
            return Wrap(Factory.Login(login, password));
        }

        /// <summary>
        /// Gets the by login.
        /// </summary>
        /// <param name="Login">The login.</param>
        /// <returns></returns>
        public User GetByLogin(string Login)
        {
            return Wrap(Factory.GetByLogin(Login));
        }

        /// <summary>
        /// Saves the specified user.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="permissions">The permissions.</param>
        public void Save(User user, Dictionary<int, Permission> permissions)
        {
            if (permissions.ContainsKey(0))
            {
                user.RootPermission = permissions[0];
                permissions.Remove(0);
            }
            else if (user.RootPermission.HasValue)
            {
                user.RootPermission = null;
            }

            var result = permissions.Keys
                .Select(categoryId => new CategoryPermission(categoryId, (short)permissions[categoryId])).ToList();



            Context.BeginTransaction();
            try
            {
                Save(user);
                Context.ManageUserRight.SaveRights(user.UserID, result);

                Context.CommitTransaction();
            }
            catch
            {
                Context.RollbackTransaction();
                throw;
            }
        }

        /// <summary>
        /// Saves the specified User.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public virtual void Save(User entity)
        {
            entity.Validate();

            if (Identifier.IsValid(entity.UserID))
            {
                if (entity.UserID != KbContext.CurrentUserId && !Context.HasUserAdminPermission)
                    throw new PermissionException("UserAdmin");

                Factory.Update(entity.ObjectValue);
            }
            else
            {
                Context.RequireUserAdminPermission();
                entity.CreationDate = DateTime.Now;
                Factory.Insert(entity.ObjectValue);
            }
        }

        /// <summary>
        /// Deletes the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public virtual void Delete(User entity)
        {
            Context.RequireUserAdminPermission();
            Factory.Delete(entity.ObjectValue);
        }


        /// <summary>
        /// Determines whether this instance can login the specified login.
        /// </summary>
        /// <param name="login">The login.</param>
        /// <param name="password">The password.</param>
        /// <returns>
        /// 	<c>true</c> if this instance can login the specified login; otherwise, <c>false</c>.
        /// </returns>
        public virtual bool CanLogin(string login, string password)
        {
            return Factory.GetByLoginByPassword(login, password) != null;
        }

     
    }
}