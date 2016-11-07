using KnowledgeBase.Dal;
using Savchin.Data.Common;

namespace KnowledgeBase.DAL
{
    /// <summary>
    /// IFactoryProvider
    /// </summary>
    public interface IFactoryProvider
    {

        /// <summary>
        /// Creates the connection.
        /// </summary>
        /// <returns></returns>
        DBConnection CreateConnection();
        /// <summary>
        /// Gets or sets the context.
        /// </summary>
        /// <value>The context.</value>
        DalContext Context { get; set; }

        /// <summary>
        /// Creates the category factory.
        /// </summary>
        /// <returns></returns>
        ICategoryFactory CreateCategoryFactory();

        /// <summary>
        /// Creates the file include factory.
        /// </summary>
        /// <returns></returns>
        IFileIncludeFactory CreateFileIncludeFactory();

        /// <summary>
        /// Creates the file link factory.
        /// </summary>
        /// <returns></returns>
        IFileLinkFactory CreateFileLinkFactory();

        /// <summary>
        /// Creates the keyword factory.
        /// </summary>
        /// <returns></returns>
        IKeywordFactory CreateKeywordFactory();

        /// <summary>
        /// Creates the user file factory.
        /// </summary>
        /// <returns></returns>
        IUserFileFactory CreateUserFileFactory();

        /// <summary>
        /// Creates the user right factory.
        /// </summary>
        /// <returns></returns>
        IUserRightFactory CreateUserRightFactory();

        /// <summary>
        /// Creates the file storage factory.
        /// </summary>
        /// <returns></returns>
        IFileStorageFactory CreateFileStorageFactory();

        /// <summary>
        /// Creates the user factory.
        /// </summary>
        /// <returns></returns>
        IUserFactory CreateUserFactory();

        /// <summary>
        /// Creates the knowledge factory.
        /// </summary>
        /// <returns></returns>
        IKnowledgeFactory CreateKnowledgeFactory();
    }
}
