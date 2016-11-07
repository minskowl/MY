using System.Collections.Generic;

namespace KnowledgeBase.BussinesLayer
{
    public interface IFileStorageManager
    {
        /// <summary>
        /// Get all.
        /// </summary>
        /// <returns></returns> 
        IList<FileStorage> GetAll();

        /// <summary>
        /// Ges the FileStorage by ID.
        /// </summary>
        /// <param name="FileStorageID">The FileStorageID.</param>
        /// <returns></returns>        
        FileStorage GetByID(System.Int16 FileStorageID);

        /// <summary>
        /// Gets the by path.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <returns></returns>
        FileStorage GetByPath(string path);
    }
}