using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KnowledgeBase.BussinesLayer.Core;


namespace KnowledgeBase.BussinesLayer
{
    public interface IManagersFactory
    {
        /// <summary>
        /// Creates the user manager.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns></returns>
        IUserManager CreateUserManager(KbContext context);
        /// <summary>
        /// Creates the category manager.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns></returns>
        ICategoryManager CreateCategoryManager(KbContext context);

        /// <summary>
        /// Creates the file include manager.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns></returns>
        IFileIncludeManager CreateFileIncludeManager(KbContext context);

        /// <summary>
        /// Creates the file link manager.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns></returns>
        IFileLinkManager CreateFileLinkManager(KbContext context);

        /// <summary>
        /// Creates the file storage manager.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns></returns>
        IFileStorageManager CreateFileStorageManager(KbContext context);

        /// <summary>
        /// Creates the keyword manager.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns></returns>
        IKeywordManager CreateKeywordManager(KbContext context);
        /// <summary>
        /// Creates the knowledge manager.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns></returns>
        IKnowledgeManager CreateKnowledgeManager(KbContext context);

        /// <summary>
        /// Creates the user file manager.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns></returns>
        IUserFileManager CreateUserFileManager(KbContext context);

        /// <summary>
        /// Creates the user right manager.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns></returns>
        IUserRightManager CreateUserRightManager(KbContext context);
    }
}
