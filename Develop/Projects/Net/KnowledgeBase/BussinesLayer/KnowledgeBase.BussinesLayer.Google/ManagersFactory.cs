using System;
using KnowledgeBase.BussinesLayer.Core;
using KnowledgeBase.BussinesLayer.Google.Managers;


namespace KnowledgeBase.BussinesLayer.Google
{
    public class ManagersFactory : IManagersFactory
    {
        #region Implementation of IManagersFactory

        public IUserManager CreateUserManager(KbContext context)
        {
            return new UserManager((GoogleContext)context);
        }

        public ICategoryManager CreateCategoryManager(KbContext context)
        {
            return new CategoryManager((GoogleContext)context);
        }

        public IFileIncludeManager CreateFileIncludeManager(KbContext context)
        {
            throw new NotImplementedException();
        }

        public IFileLinkManager CreateFileLinkManager(KbContext context)
        {
            throw new NotImplementedException();
        }

        public IFileStorageManager CreateFileStorageManager(KbContext context)
        {
            throw new NotImplementedException();
        }

        public IKeywordManager CreateKeywordManager(KbContext context)
        {
            return new KeywordManager((GoogleContext)context);
        }

        public IKnowledgeManager CreateKnowledgeManager(KbContext context)
        {
            return new KnowledgeManager((GoogleContext)context);
        }

        public IUserFileManager CreateUserFileManager(KbContext context)
        {
            return new UserFileManager((GoogleContext)context);
        }

        public IUserRightManager CreateUserRightManager(KbContext context)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
