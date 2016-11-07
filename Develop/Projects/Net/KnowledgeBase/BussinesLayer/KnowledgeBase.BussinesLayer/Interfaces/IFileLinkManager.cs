using System;
using System.Collections.Generic;

namespace KnowledgeBase.BussinesLayer
{
    public interface IFileLinkManager
    {
        /// <summary>
        /// Get all.
        /// </summary>
        /// <returns></returns> 
        IList<FileLink> GetAll();

        /// <summary>
        /// Ges the FileLink by ID.
        /// </summary>
        /// <param name="FileLinkID">The FileLinkID.</param>
        /// <returns></returns>        
        FileLink GetByID(System.Int32 FileLinkID);

        /// <summary>
        /// Gets the by public ID.
        /// </summary>
        /// <param name="publicID">The public ID.</param>
        /// <returns></returns>
        FileLink GetByPublicID(Guid publicID);

        /// <summary>
        /// Gets the by file storage ID by path.
        /// </summary>
        /// <param name="FileStorageID">The file storage ID.</param>
        /// <param name="Path">The path.</param>
        /// <returns></returns>
        FileLink GetByFileStorageIDByPath(Int16 FileStorageID, string Path);

        /// <summary>
        /// Get FileLink values FileStorageID .
        /// </summary>
        /// <param name="FileStorageID">The FileStorageID.</param>
        /// <returns>List of FileLink</returns>   
        IList<FileLink> GetByFileStorageID(System.Int16 FileStorageID);

        /// <summary>
        /// Gets the by path.
        /// </summary>
        /// <param name="filePath">The file path.</param>
        /// <returns></returns>
        FileLink GetByPath(string filePath);

        /// <summary>
        /// Saves the specified FileLink.
        /// </summary>
        /// <param name="entity">The entity.</param>
        void Save(FileLink entity);

        /// <summary>
        /// Deletes the specified FileLink.
        /// </summary>
        /// <param name="entity">The entity.</param>
        void Delete(FileLink entity);
    }
}