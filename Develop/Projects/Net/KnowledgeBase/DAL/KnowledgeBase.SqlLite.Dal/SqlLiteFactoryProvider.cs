using System;
using System.Data.SQLite;
using KnowledgeBase.Dal;
using KnowledgeBase.DAL;
using KnowledgeBase.SqlLite.Dal.Core;
using KnowledgeBase.SqlLite.Dal.Factories;
using Savchin.Data.Common;

namespace KnowledgeBase.SqlLite.Dal
{
    /// <summary>
    /// SqlLiteFactoryProvider
    /// </summary>
    public class SqlLiteFactoryProvider : IFactoryProvider
    {
        private readonly SqlLiteFactory _factory = new SqlLiteFactory();
        private readonly string _filePath;



        /// <summary>
        /// Initializes a new instance of the <see cref="SqlLiteFactoryProvider"/> class.
        /// </summary>
        /// <param name="filePath">The file path.</param>
        public SqlLiteFactoryProvider(string filePath)
        {
            _filePath = filePath;
        }

        /// <summary>
        /// Creates the new database.
        /// </summary>
        public void CreateNewDatabase()
        {
            SQLiteConnection.CreateFile(_filePath);
            var connection= CreateConnection();
            connection.ExecuteNonQuery(Resources.Database);
        }
        #region Implementation of IFactoryProvider


        /// <summary>
        /// Creates the connection.
        /// </summary>
        /// <returns></returns>
        public DBConnection CreateConnection()
        {
            return new DBConnection(_factory, "data source=" + _filePath);
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
        /// Creates the category factory.
        /// </summary>
        /// <returns></returns>
        public ICategoryFactory CreateCategoryFactory()
        {
            return new CategoryFactory(Context);
        }

        /// <summary>
        /// Creates the knowledge factory.
        /// </summary>
        /// <returns></returns>
        public IKnowledgeFactory CreateKnowledgeFactory()
        {
            return new KnowledgeFactory(Context);
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
            throw new NotImplementedException();
        }
        /// <summary>
        /// Creates the file storage factory.
        /// </summary>
        /// <returns></returns>
        public IFileStorageFactory CreateFileStorageFactory()
        {
            throw new NotImplementedException();
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
            throw new NotSupportedException();
        }
        /// <summary>
        /// Creates the user factory.
        /// </summary>
        /// <returns></returns>
        public IUserFactory CreateUserFactory()
        {
            throw new NotSupportedException();
        }


        #endregion
    }
}
