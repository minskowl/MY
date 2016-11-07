using System;
using System.Collections.Generic;
using KnowledgeBase.DAL;

namespace KnowledgeBase.DAL
{
    public interface IFileLinkFactory
    {
        /// <summary>
        /// Selects the by file storage ID by path.
        /// </summary>
        /// <param name="FileStorageID">The file storage ID.</param>
        /// <param name="Path">The path.</param>
        /// <returns></returns>
        FileLinkValue SelectByFileStorageIDByPath(Int16 FileStorageID, string Path);

        /// <summary>
        /// Gets the by public ID.
        /// </summary>
        /// <param name="publicID">The public ID.</param>
        /// <returns></returns>
        FileLinkValue GetByPublicID(Guid publicID);

        /// <summary>
        /// Creates the insert command.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        void Insert(FileLinkValue value);

        /// <summary>
        /// Updates the specified FileLink.
        /// </summary>
        /// <param name="value">The FileLink value.</param>
        void Update(FileLinkValue value);

        /// <summary>
        /// Gets FileLink by ID.
        /// </summary>
        /// <param name="FileLinkID">The FileLinkID.</param>
        /// <returns></returns>
        FileLinkValue SelectByID(System.Int32 FileLinkID);

        /// <summary>
        /// Deletes the specified FileLink.
        /// </summary>
        /// <param name="FileLinkID">The FileLinkID.</param>
        void Delete(System.Int32 FileLinkID);

        /// <summary>
        /// Deletes the specified FileLink.
        /// </summary>
        /// <param name="value">The value.</param>
        void Delete(FileLinkValue value);

        /// <summary>
        /// Selects all FileLink values.
        /// </summary>
        /// <returns>List of all FileLink</returns>
        IList<FileLinkValue> SelectAll();

        /// <summary>
        /// Selects FileLink values FileStorageID .
        /// ForeignKey: FK_FileLinks_FileStorages
        /// </summary>
        /// <param name="FileStorageID">The FileStorageID.</param>
        /// <returns>List of FileLink</returns>   
        IList<FileLinkValue> SelectByFileStorageID( System.Int16 FileStorageID);
    }
}