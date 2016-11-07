using System;
using KnowledgeBase.DAL;
using KnowledgeBase.Dal;
using KnowledgeBase.Google.Dal.Factories;
using Savchin.Data.Common;

namespace KnowledgeBase.Google.Dal
{
    public class GoogleFactoryProvider : IFactoryProvider
    {
        #region Implementation of IFactoryProvider

        public DBConnection CreateConnection()
        {
            return null;
        }

        public DalContext Context
        {
            get;
            set;
        }

        public ICategoryFactory CreateCategoryFactory()
        {
            return new CategoryFactory(Context);
        }

        public IFileIncludeFactory CreateFileIncludeFactory()
        {
            throw new NotImplementedException();
        }

        public IFileLinkFactory CreateFileLinkFactory()
        {
            throw new NotImplementedException();
        }

        public IKeywordFactory CreateKeywordFactory()
        {
            return new KeywordFactory(Context);
        }

        public IUserFileFactory CreateUserFileFactory()
        {
            return new UserFileFactory(Context);
        }

        public IUserRightFactory CreateUserRightFactory()
        {
            return new UserRightFactory(Context);
        }

        public IFileStorageFactory CreateFileStorageFactory()
        {
            throw new NotImplementedException();
        }

        public IUserFactory CreateUserFactory()
        {
            return new UserFactory(Context);
        }

        public IKnowledgeFactory CreateKnowledgeFactory()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
