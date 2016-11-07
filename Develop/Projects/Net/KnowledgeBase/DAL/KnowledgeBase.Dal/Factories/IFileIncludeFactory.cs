using System;
using System.Collections.Generic;
using KnowledgeBase.DAL;

namespace KnowledgeBase.DAL
{
    public interface IFileIncludeFactory
    {
        /// <summary>
        /// Creates from user file.
        /// </summary>
        /// <param name="userFileId">The user file id.</param>
        /// <param name="fileIncludeID">The file include ID.</param>
        /// <param name="knowledgeID">The knowledge ID.</param>
        void CreateFromUserFile(int userFileId, Guid fileIncludeID, int knowledgeID);

        /// <summary>
        /// Gets the data.
        /// </summary>
        /// <param name="FileIncludeID">The file include ID.</param>
        /// <returns></returns>
        byte[] GetData(Guid FileIncludeID);

        /// <summary>
        /// Sets the data.
        /// </summary>
        /// <param name="FileIncludeID">The file include ID.</param>
        /// <param name="data">The data.</param>
        void SetData(Guid FileIncludeID, byte[] data);

        /// <summary>
        /// Deletes the name of the by knowledge ID by file.
        /// </summary>
        /// <param name="knowledgeID">The knowledge ID.</param>
        /// <param name="fileName">Name of the file.</param>
        void DeleteByKnowledgeIDByFileName(System.Int32 knowledgeID, string fileName);

        /// <summary>
        /// Creates the insert command.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        void Insert(FileIncludeValue value);

        /// <summary>
        /// Updates the specified FileInclude.
        /// </summary>
        /// <param name="value">The FileInclude value.</param>
        void Update(FileIncludeValue value);

        /// <summary>
        /// Gets FileInclude by ID.
        /// </summary>
        /// <param name="FileIncludeID">The FileIncludeID.</param>
        /// <returns></returns>
        FileIncludeValue SelectByID(System.Guid FileIncludeID);

        /// <summary>
        /// Deletes the specified FileInclude.
        /// </summary>
        /// <param name="FileIncludeID">The FileIncludeID.</param>
        void Delete(System.Guid FileIncludeID);

        /// <summary>
        /// Deletes the specified FileInclude.
        /// </summary>
        /// <param name="value">The value.</param>
        void Delete(FileIncludeValue value);

        /// <summary>
        /// Selects all FileInclude values.
        /// </summary>
        /// <returns>List of all FileInclude</returns>
        IList<FileIncludeValue> SelectAll();

        /// <summary>
        /// Selects FileInclude values KnowledgeID .
        /// ForeignKey: FK_FileIncludes_FileIncludes
        /// </summary>
        /// <param name="KnowledgeID">The KnowledgeID.</param>
        /// <returns>List of FileInclude</returns>   
        IList<FileIncludeValue> SelectByKnowledgeID(System.Int32 KnowledgeID);
    }
}