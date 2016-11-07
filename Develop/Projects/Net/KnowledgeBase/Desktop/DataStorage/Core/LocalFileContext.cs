using KnowledgeBase.BussinesLayer;
using KnowledgeBase.BussinesLayer.Core;
using KnowledgeBase.BussinesLayer.Managers;
using KnowledgeBase.BussinesLayer.Security;
using KnowledgeBase.Dal;
using KnowledgeBase.SqlLite.Dal;

namespace KnowledgeBase.Desktop.Core
{
    /// <summary>
    /// LocalFileContext
    /// </summary>
    internal class LocalFileContext : KbContext
    {
        /// <summary>
        /// FileFilter
        /// </summary>
        public const string FileFilter = ".s3db|*.s3db";
        private readonly User _currentUser;
        private readonly AdminPermisssionSet _permisssionSet = new AdminPermisssionSet();
        /// <summary>
        /// Initializes a new instance of the <see cref="LocalFileContext"/> class.
        /// </summary>
        /// <param name="provider">The provider.</param>
        /// <param name="p">The p.</param>
        private LocalFileContext(IDalObjectProvider provider, SqlLiteFactoryProvider p)
            : base(provider, new ManagersFactory(p))
        {
            _currentUser = new User { Login = "local" };

        }

        /// <summary>
        /// Inits the specified file path.
        /// </summary>
        /// <param name="filePath">The file path.</param>
        public static LocalFileContext OpenDatabase(string filePath)
        {
            var p = new SqlLiteFactoryProvider(filePath);
            var provider = new DalMultiThreadProvider(p);
            var context = new LocalFileContext(provider, p);
            p.Context = context;
          
            context.Login();
            return context;
        }

        /// <summary>
        /// Creates the database.
        /// </summary>
        /// <param name="filePath">The file path.</param>
        /// <returns></returns>
        public static LocalFileContext CreateDatabase(string filePath)
        {
            var p = new SqlLiteFactoryProvider(filePath);
            p.CreateNewDatabase();
            var provider = new DalMultiThreadProvider(p);
            var context = new LocalFileContext(provider, p);
            p.Context = context;
            return context;
        }
        /// <summary>
        /// Gets the permissions for category.
        /// </summary>
        /// <param name="categoryId">The category ID.</param>
        /// <returns></returns>
        public override Permission GetPermissionsForCategory(int categoryId)
        {
            return Permission.Admin;
        }
        /// <summary>
        /// Gets the permission set.
        /// </summary>
        /// <value>The permission set.</value>
        public override IPermissionSet PermissionSet
        {
            get { return _permisssionSet; }
        }
        /// <summary>
        /// Gets a value indicating whether this instance has user admin permission.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance has user admin permission; otherwise, <c>false</c>.
        /// </value>
        public override bool HasUserAdminPermission
        {
            get { return false; }
        }


        /// <summary>
        /// Gets the current user.
        /// </summary>
        /// <returns></returns>
        public override User GetCurrentUser()
        {
            return _currentUser;
        }


        private void Login()
        {
            Login(_currentUser);
        }
    }
}
