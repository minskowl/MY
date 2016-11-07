using System.Collections.Generic;
using KnowledgeBase.DAL;

namespace KnowledgeBase.DAL
{
    public interface IUserFileFactory
    {
        /// <summary>
        /// Gets the data.
        /// </summary>
        /// <param name="UserFileID">The user file ID.</param>
        /// <returns></returns>
        byte[] GetData(System.Int32 UserFileID);

        /// <summary>
        /// Sets the data.
        /// </summary>
        /// <param name="UserFileID">The user file ID.</param>
        /// <param name="data">The data.</param>
        /// <returns></returns>
        void SetData(System.Int32 UserFileID, byte[] data);

        /// <summary>
        /// Deletes the name of the by user ID by file.
        /// </summary>
        /// <param name="UserID">The user ID.</param>
        /// <param name="fileName">Name of the file.</param>
        void DeleteByUserIDByFileName(System.Int32 UserID, string fileName);

        /// <summary>
        /// Creates the insert command.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        void Insert(UserFileValue value);

        /// <summary>
        /// Updates the specified UserFile.
        /// </summary>
        /// <param name="value">The UserFile value.</param>
        void Update(UserFileValue value);

        /// <summary>
        /// Gets UserFile by ID.
        /// </summary>
        /// <param name="UserFileID">The UserFileID.</param>
        /// <returns></returns>
        UserFileValue SelectByID(System.Int32 UserFileID);

        /// <summary>
        /// Deletes the specified UserFile.
        /// </summary>
        /// <param name="UserFileID">The UserFileID.</param>
        void Delete(System.Int32 UserFileID);

        /// <summary>
        /// Deletes the specified UserFile.
        /// </summary>
        /// <param name="value">The value.</param>
        void Delete(UserFileValue value);

        /// <summary>
        /// Selects all UserFile values.
        /// </summary>
        /// <returns>List of all UserFile</returns>
        IList<UserFileValue> SelectAll();

        /// <summary>
        /// Selects UserFile values UserID .
        /// ForeignKey: FK_UserFiles_Users
        /// </summary>
        /// <param name="UserID">The UserID.</param>
        /// <returns>List of UserFile</returns>   
        IList<UserFileValue> SelectByUserID(System.Int32 UserID);
    }
}