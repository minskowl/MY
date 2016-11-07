using KnowledgeBase.Dal;
using KnowledgeBase.DAL;
using Savchin.Data.Common;
using Savchin.Data.MSSQL;

namespace KnowledgeBase.Mssql.Dal
{
    /// <summary>
    /// 
    /// </summary>
    public class MssqlFactoryProvider : IFactoryProvider
    {

        private readonly MSSQL2000Factory _factory = new MSSQL2000Factory();
        private readonly string _connectionString;

        /// <summary>
        /// Initializes a new instance of the <see cref="MssqlFactoryProvider"/> class.
        /// </summary>
        /// <param name="connectionString">The connection string.</param>
        public MssqlFactoryProvider(string connectionString)
        {
            _connectionString = connectionString;
        }


        /// <summary>
        /// Gets or sets the context.
        /// </summary>
        /// <value>The context.</value>
        public DalContext Context
        {
            get;
            set;
        }
        /// <summary>
        /// Creates the connection.
        /// </summary>
        /// <returns></returns>
        public DBConnection CreateConnection()
        {
            return new DBConnection(_factory, _connectionString);
        }
        /// <summary>
        /// Creates the category factory.
        /// </summary>
        /// <returns></returns>
        public ICategoryFactory CreateCategoryFactory()
        {
            return new CategoryFactory(Context);
        }

        /// <summary>
        /// Creates the file include factory.
        /// </summary>
        /// <returns></returns>
        public IFileIncludeFactory CreateFileIncludeFactory()
        {
            return new FileIncludeFactory(Context);
        }

        /// <summary>
        /// Creates the file link factory.
        /// </summary>
        /// <returns></returns>
        public IFileLinkFactory CreateFileLinkFactory()
        {
            return new FileLinkFactory(Context);
        }

        /// <summary>
        /// Creates the keyword factory.
        /// </summary>
        /// <returns></returns>
        public IKeywordFactory CreateKeywordFactory()
        {
            return new KeywordFactory(Context);
        }

        /// <summary>
        /// Creates the user file factory.
        /// </summary>
        /// <returns></returns>
        public IUserFileFactory CreateUserFileFactory()
        {
            return new UserFileFactory(Context);
        }

        /// <summary>
        /// Creates the user right factory.
        /// </summary>
        /// <returns></returns>
        public IUserRightFactory CreateUserRightFactory()
        {
            return new UserRightFactory(Context);
        }

        /// <summary>
        /// Creates the file storage factory.
        /// </summary>
        /// <returns></returns>
        public IFileStorageFactory CreateFileStorageFactory()
        {
            return new FileStorageFactory(Context);
        }

        /// <summary>
        /// Creates the user factory.
        /// </summary>
        /// <returns></returns>
        public IUserFactory CreateUserFactory()
        {
            return new UserFactory(Context);
        }

        /// <summary>
        /// Creates the knowledge factory.
        /// </summary>
        /// <returns></returns>
        public IKnowledgeFactory CreateKnowledgeFactory()
        {
            return new KnowledgeFactory(Context);
        }
    }
}
