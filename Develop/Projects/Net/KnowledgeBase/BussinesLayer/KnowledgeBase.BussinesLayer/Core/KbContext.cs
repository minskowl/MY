using System;
using System.Collections.Generic;
using System.Threading;
using System.Web.Caching;
using KnowledgeBase.BussinesLayer.Security;
using KnowledgeBase.Dal;

namespace KnowledgeBase.BussinesLayer.Core
{
    public partial class KbContext : DalContext
    {

        #region Properties

        private const string SessionKeyCurrentParentUserId = "CurrentParentUserId";
        private const string SessionKeyCurrentKnowledgeID = "SessionKeyCurrentKnowledgeID";
        private const string ApplicationKeySettingsID = "SettingsID";

        private readonly IManagersFactory _factory;
   

        private static KbContext _current;
        /// <summary>
        /// Gets or sets the current kb.
        /// </summary>
        /// <value>The current kb.</value>
        public static KbContext CurrentKb
        {
            get { return _current; }
            set
            {
                _current = value;
                CurrentDal = value;

            }
        }
        /// <summary>
        /// Gets or sets the settings ID.
        /// </summary>
        /// <value>The settings ID.</value>
        public byte SettingsId
        {
            get
            {
                var o = ApplicationState[ApplicationKeySettingsID];
                return o == null ? (byte)0 : (byte)o;
            }
            set { ApplicationState[ApplicationKeySettingsID] = value; }
        }

        /// <summary>
        /// Gets the current user ID.
        /// </summary>
        /// <value>The current user ID.</value>
        public static int CurrentUserId
        {
            get
            {
                int result;
                return int.TryParse(Thread.CurrentPrincipal.Identity.Name, out result) ? result : 1;
            }
        }

        /// <summary>
        /// Gets or sets the current knowledge ID.
        /// </summary>
        /// <value>The current knowledge ID.</value>
        public int? CurrentKnowledgeID
        {
            get { return (int?)Session.Get(SessionKeyCurrentKnowledgeID); }
            set { Session.Save(SessionKeyCurrentKnowledgeID, value); }
        }

  

        /// <summary>
        /// Gets the storages.
        /// </summary>
        /// <value>The storages.</value>
        public IList<FileStorage> Storages
        {
            get
            {
                var storages = (IList<FileStorage>)ApplicationState["FileStorages"];
                if (storages == null)
                {
                    storages = ManagerFileStorage.GetAll();
                    ApplicationState["FileStorages"] = storages;
                }
                return storages;
            }
        }
        public CategoryTree CategoryTree
        {
            get
            {
                var result = (CategoryTree)Cache[CasheKey.CategoryTree.ToString()];
                if (result == null)
                {
                    result = new CategoryTree(this);
                    Cache.Add(
                        CasheKey.CategoryTree.ToString(),
                        result,
                        null,
                        DateTime.Now.AddMinutes(5),
                        Cache.NoSlidingExpiration,
                        CacheItemPriority.High,
                        null);
                }
                return result;
            }
        }



        #endregion

        #region Constructors



        /// <summary>
        /// Initializes the <see cref="KbContext"/> class.
        /// </summary>
        /// <param name="provider">The provider.</param>
        /// <param name="factory">The factory.</param>
        public KbContext(IDalObjectProvider provider, IManagersFactory factory)
            : base(provider)
        {
            if (provider == null) throw new ArgumentNullException("provider");
            if (factory == null) throw new ArgumentNullException("factory");

            _factory = factory;

        }

        #endregion

        #region Interface

        #region Authentication

        /// <summary>
        /// Logins the specified login.
        /// </summary>
        /// <param name="login">The login.</param>
        /// <param name="password">The password.</param>
        /// <returns></returns>
        public virtual bool Login(string login, string password)
        {
            var user = ManagerUser.Login(login, password);
            Login(user);
            return true;
        }

        /// <summary>
        /// Logins the specified user.
        /// </summary>
        /// <param name="user">The user.</param>
        public void Login(User user)
        {
            Context[SessionKeyCurrentParentUserId] = user.UserID;
            Login(user.UserID.ToString());
        }



        /// <summary>
        /// Sets the current user ID. Use this method only imitate login of user.
        /// </summary>
        /// <param name="userId">The user id.</param>
        public void SetCurrentUserId(long userId)
        {
            Login(userId.ToString());
        }
        #endregion


        /// <summary>
        /// Saves to session.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        public void SessionSave(SessionKey key, object value)
        {
            Session.Save(key.ToString(), value);
        }
        /// <summary>
        /// Restores from session.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        public object SessionRestore(SessionKey key)
        {
            return Session.Get(key.ToString());
        }
        #endregion



        /// <summary>
        /// Gets the current user.
        /// </summary>
        /// <returns></returns>
        public virtual User GetCurrentUser()
        {
            return ManagerUser.GetByID(CurrentUserId);
        }

        #region Permissions

        /// <summary>
        /// Gets the permission set.
        /// </summary>
        /// <value>The permission set.</value>
        public virtual IPermissionSet PermissionSet
        {
            get
            {
                return GetFromPersistentContext(SessionKey.PermmissionSet, () => new PermissionSet(this));

            }
        }
        /// <summary>
        /// Gets the permissions for category.
        /// </summary>
        /// <param name="categoryId">The category ID.</param>
        /// <returns></returns>
        public virtual Permission GetPermissionsForCategory(int categoryId)
        {
            return PermissionSet[categoryId];
        }


        /// <summary>
        /// Determines whether the specified category ID has permission.
        /// </summary>
        /// <param name="categoryId">The category ID.</param>
        /// <param name="permission">The permission.</param>
        /// <returns>
        /// 	<c>true</c> if the specified category ID has permission; otherwise, <c>false</c>.
        /// </returns>
        public bool HasPermission(int categoryId, Permission permission)
        {
            return GetPermissionsForCategory(categoryId).HasPermission(permission);
        }

        /// <summary>
        /// Requires the permission.
        /// </summary>
        /// <param name="categoryId">The category ID.</param>
        /// <param name="permission">The permission.</param>
        public Permission RequirePermission(int categoryId, Permission permission)
        {
            var hasPermission = GetPermissionsForCategory(categoryId);
            if (!hasPermission.HasPermission(permission))
                throw new PermissionException(permission);
            return hasPermission;
        }
        /// <summary>
        /// Gets a value indicating whether this instance has user admin permission.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance has user admin permission; otherwise, <c>false</c>.
        /// </value>
        public virtual bool HasUserAdminPermission
        {
            get
            {
                return GetCurrentUser().IsUserAdmin;
            }
        }

        /// <summary>
        /// Requires the user admin permission.
        /// </summary>
        public void RequireUserAdminPermission()
        {
            if (!HasUserAdminPermission)
                throw new PermissionException("UserAdmin");
        }
        #endregion


    }
}