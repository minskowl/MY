using System.Collections.Generic;

namespace KnowledgeBase.BussinesLayer
{
    public interface IUserFileManager
    {
        /// <summary>
        /// Get all.
        /// </summary>
        /// <returns></returns> 
        IList<UserFile> GetAll();

        /// <summary>
        /// Ges the UserFile by ID.
        /// </summary>
        /// <param name="UserFileID">The UserFileID.</param>
        /// <returns></returns>        
        UserFile GetByID(System.Int32 UserFileID);

        /// <summary>
        /// Get UserFile values UserID .
        /// </summary>
        /// <param name="UserID">The UserID.</param>
        /// <returns>List of UserFile</returns>   
        IList<UserFile> GetByUserID(System.Int32 UserID);

        /// <summary>
        /// Saves the specified UserFile.
        /// </summary>
        /// <param name="entity">The entity.</param>
        void Save(UserFile entity);

        /// <summary>
        /// Creates the specified knowledge ID.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <param name="content">The content.</param>
        /// <returns></returns>
        UserFile Create(string fileName, byte[] content);

        /// <summary>
        /// Gets the data.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns></returns>
        byte[] GetData(int id);

        /// <summary>
        /// Sets the data.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <param name="data">The data.</param>
        void SetData(int id, byte[] data);

        /// <summary>
        /// Deletes the specified UserFile.
        /// </summary>
        /// <param name="entity">The entity.</param>
        void Delete(UserFile entity);

        /// <summary>
        /// Deletes the name of the by user ID by file.
        /// </summary>
        /// <param name="userID">The user ID.</param>
        /// <param name="fileName">Name of the file.</param>
        void DeleteByUserIDByFileName(System.Int32 userID, string fileName);
    }
}