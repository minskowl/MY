using KnowledgeBase.BussinesLayer.Core;
using KnowledgeBase.DAL;

namespace KnowledgeBase.BussinesLayer.Managers
{
    public class ManagersFactory : IManagersFactory
    {
        private IFactoryProvider _factoryProvider;

        public ManagersFactory(IFactoryProvider factoryProvider)
        {
            _factoryProvider = factoryProvider;
        }

        #region Implementation of IManagersFactory

        /// <summary>
        /// Creates the user manager.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns></returns>
        public IUserManager CreateUserManager(KbContext context)
        {
            return new UserManager(context,_factoryProvider);
        }

        /// <summary>
        /// Creates the category manager.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns></returns>
        public ICategoryManager CreateCategoryManager(KbContext context)
        {
            return new CategoryManager(context, _factoryProvider);
        }

        /// <summary>
        /// Creates the file include manager.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns></returns>
        public IFileIncludeManager CreateFileIncludeManager(KbContext context)
        {
            return new FileIncludeManager(context,_factoryProvider);
        }

        /// <summary>
        /// Creates the file link manager.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns></returns>
        public IFileLinkManager CreateFileLinkManager(KbContext context)
        {
            return new FileLinkManager(context,_factoryProvider);
        }

        /// <summary>
        /// Creates the file storage manager.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns></returns>
        public IFileStorageManager CreateFileStorageManager(KbContext context)
        {
            return new FileStorageManager(context,_factoryProvider);
        }

        /// <summary>
        /// Creates the keyword manager.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns></returns>
        public IKeywordManager CreateKeywordManager(KbContext context)
        {
            return new KeywordManager(context,_factoryProvider);
        }

        /// <summary>
        /// Creates the knowledge manager.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns></returns>
        public IKnowledgeManager CreateKnowledgeManager(KbContext context)
        {
            return new KnowledgeManager(context,_factoryProvider);
        }

        /// <summary>
        /// Creates the user file manager.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns></returns>
        public IUserFileManager CreateUserFileManager(KbContext context)
        {
            return new UserFileManager(context,_factoryProvider);
        }

        /// <summary>
        /// Creates the user right manager.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns></returns>
        public IUserRightManager CreateUserRightManager(KbContext context)
        {
            return new UserRightManager(context,_factoryProvider);
        }

        #endregion
    }
}
