using System.Collections.Generic;
using KnowledgeBase.DAL;

namespace KnowledgeBase.DAL
{
    public interface IFileStorageFactory
    {
        /// <summary>
        /// Creates the insert command.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        void Insert(FileStorageValue value);

        /// <summary>
        /// Updates the specified FileStorage.
        /// </summary>
        /// <param name="value">The FileStorage value.</param>
        void Update(FileStorageValue value);

        /// <summary>
        /// Gets FileStorage by ID.
        /// </summary>
        /// <param name="FileStorageID">The FileStorageID.</param>
        /// <param name="SettingsID">The SettingsID.</param>
        /// <returns></returns>
        FileStorageValue SelectByID(System.Int16 FileStorageID, System.Byte SettingsID);

        /// <summary>
        /// Deletes the specified FileStorage.
        /// </summary>
        /// <param name="FileStorageID">The FileStorageID.</param>
        /// <param name="SettingsID">The SettingsID.</param>
        void Delete(System.Int16 FileStorageID, System.Byte SettingsID);

        /// <summary>
        /// Deletes the specified FileStorage.
        /// </summary>
        /// <param name="value">The value.</param>
        void Delete(FileStorageValue value);

        /// <summary>
        /// Selects all FileStorage values.
        /// </summary>
        /// <returns>List of all FileStorage</returns>
        IList<FileStorageValue> SelectAll();

        /// <summary>
        /// Selects FileStorage values SettingsID .
        /// ForeignKey: FK_FileStorages_FileStorages
        /// </summary>
        /// <param name="SettingsID">The SettingsID.</param>
        /// <returns>List of FileStorage</returns>   
        IList<FileStorageValue> SelectBySettingsID(System.Byte SettingsID);
    }
}