using System;
using System.Collections.Generic;

namespace KnowledgeBase.BussinesLayer
{
    public interface IFileIncludeManager
    {
        /// <summary>
        /// Get all.
        /// </summary>
        /// <returns></returns> 
        IList<FileInclude> GetAll();

        /// <summary>
        /// Ges the FileInclude by ID.
        /// </summary>
        /// <param name="FileIncludeID">The FileIncludeID.</param>
        /// <returns></returns>        
        FileInclude GetByID(System.Guid FileIncludeID);

        /// <summary>
        /// Get FileInclude values KnowledgeID .
        /// </summary>
        /// <param name="KnowledgeID">The KnowledgeID.</param>
        /// <returns>List of FileInclude</returns>   
        IList<FileInclude> GetByKnowledgeID(System.Int32 KnowledgeID);

        /// <summary>
        /// Saves the specified FileInclude.
        /// </summary>
        /// <param name="entity">The entity.</param>
        void Save(FileInclude entity);

        /// <summary>
        /// Gets the data.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns></returns>
        byte[] GetData(Guid id);

        /// <summary>
        /// Sets the data.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <param name="data">The data.</param>
        void SetData(Guid id, byte[] data);

        /// <summary>
        /// Creates from user file.
        /// </summary>
        /// <param name="userFileId">The user file id.</param>
        /// <param name="fileIncludeID">The file include ID.</param>
        /// <param name="knowledgeID">The knowledge ID.</param>
        void CreateFromUserFile(int userFileId, Guid fileIncludeID, int knowledgeID);

        /// <summary>
        /// Creates the file include.
        /// </summary>
        /// <param name="knowledgeID">The knowledge ID.</param>
        /// <param name="fileName">Name of the file.</param>
        /// <param name="content">The content.</param>
        /// <returns></returns>
        FileInclude Create(int knowledgeID, string fileName, byte[] content);

        /// <summary>
        /// Deletes the specified FileInclude.
        /// </summary>
        /// <param name="entity">The entity.</param>
        void Delete(FileInclude entity);

        /// <summary>
        /// Deletes the name of the by knowledge ID by file.
        /// </summary>
        /// <param name="knowledgeID">The knowledge ID.</param>
        /// <param name="fileName">Name of the file.</param>
        void DeleteByKnowledgeIDByFileName(System.Int32 knowledgeID, string fileName);
    }
}