namespace KnowledgeBase.BussinesLayer.Core
{
    partial class KbContext
    {
        private const string SessionKeyUserManager = "UserManager";
        private const string SessionKeyCategoryManager = "CategoryManager";
        private const string SessionKeyKnowledgesManager = "KnowledgesManager";
        private const string SessionKeyUserRightsManager = "UserRightsManager";
        private const string SessionKeyKeywordManager = "KeywordManager";
        private const string SessionKeyFileStorage = "SessionKeyFileStorage";
        private const string SessionKeyFileLinkManager = "SessionKeyFileLinkManager";
        private const string SessionKeyFileIncludeManager = "SessionKeyFileIncludeManager";
        private const string SessionKeyUserFileManager = "SessionKeyUserFileManager";


        private IUserFileManager _managerUserFile;
        /// <summary>
        /// Gets the manager user file.
        /// </summary>
        /// <value>The manager user file.</value>
        public IUserFileManager ManagerUserFile
        {
            get
            {
                return _managerUserFile ?? (_managerUserFile = _factory.CreateUserFileManager(this));
            }
        }

        private IFileLinkManager _managerFileLink;
        /// <summary>
        /// Gets the manager file link.
        /// </summary>
        /// <value>The manager file link.</value>
        public IFileLinkManager ManagerFileLink
        {
            get
            {
                return _managerFileLink ?? (_managerFileLink = _factory.CreateFileLinkManager(this));
            }
        }
        private IFileIncludeManager _managerFileInclude;
        /// <summary>
        /// Gets the manager file include.
        /// </summary>
        /// <value>The manager file include.</value>
        public IFileIncludeManager ManagerFileInclude
        {
            get
            {
                return _managerFileInclude ?? (_managerFileInclude = _factory.CreateFileIncludeManager(this));
            }
        }
        /// <summary>
        /// Gets the manager file storage.
        /// </summary>
        /// <value>The manager file storage.</value>
        public IFileStorageManager ManagerFileStorage
        {
            get
            {
                var value = (IFileStorageManager)Context[SessionKeyFileStorage];
                if (value == null)
                {
                    value = _factory.CreateFileStorageManager(this);
                    Context[SessionKeyFileStorage] = value;
                }
                return value;
            }
        }


        /// <summary>
        /// Gets the manager user.
        /// </summary>
        /// <value>The manager user.</value>
        public IUserManager ManagerUser
        {
            get
            {
                var value = (IUserManager)Context[SessionKeyUserManager];
                if (value == null)
                {
                    value = _factory.CreateUserManager(this);
                    Context[SessionKeyUserManager] = value;
                }
                return value;
            }
        }
        /// <summary>
        /// Gets the manager category.
        /// </summary>
        /// <value>The manager category.</value>
        public ICategoryManager ManagerCategory
        {
            get
            {
                var value = (ICategoryManager)Context[SessionKeyCategoryManager];
                if (value == null)
                {
                    value = _factory.CreateCategoryManager(this);
                    Context[SessionKeyCategoryManager] = value;
                }
                return value;
            }
        }
        /// <summary>
        /// Gets the manager knowledge.
        /// </summary>
        /// <value>The manager knowledge.</value>
        public IKnowledgeManager ManagerKnowledge
        {
            get
            {
                var value = (IKnowledgeManager)Context[SessionKeyKnowledgesManager];
                if (value == null)
                {
                    value = _factory.CreateKnowledgeManager(this);
                    Context[SessionKeyKnowledgesManager] = value;
                }
                return value;
            }
        }

        /// <summary>
        /// Gets the manager keyword.
        /// </summary>
        /// <value>The manager keyword.</value>
        public IKeywordManager ManagerKeyword
        {
            get
            {
                var value = (IKeywordManager)Context[SessionKeyKeywordManager];
                if (value == null)
                {
                    value = _factory.CreateKeywordManager(this);
                    Context[SessionKeyKeywordManager] = value;
                }
                return value;
            }
        }
        /// <summary>
        /// Gets the manage user right.
        /// </summary>
        /// <value>The manage user right.</value>
        public IUserRightManager ManageUserRight
        {
            get
            {
                var value = (IUserRightManager)Context[SessionKeyUserRightsManager];
                if (value == null)
                {
                    value = _factory.CreateUserRightManager(this);
                    Context[SessionKeyUserRightsManager] = value;
                }
                return value;
            }
        }
        
    }
}
