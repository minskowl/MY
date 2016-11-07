using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using KnowledgeBase.DAL;
using Savchin.Data.Common;

namespace KnowledgeBase.SqlLite.Dal.Factories
{
    public partial class FileIncludeFactory : IFileIncludeFactory
    {
        /// <summary>
        /// Creates from user file.
        /// </summary>
        /// <param name="userFileId">The user file id.</param>
        /// <param name="fileIncludeID">The file include ID.</param>
        /// <param name="knowledgeID">The knowledge ID.</param>
        public void CreateFromUserFile(int userFileId, Guid fileIncludeID, int knowledgeID)
        {
            throw new NotImplementedException();
            //IDbCommand command = Database.CreateSPCommand("_FileIncludes_CreateFromUserFile", "@UserFileId", userFileId);
            //command.AddInputParameter("@FileIncludeID", DbType.Guid, fileIncludeID);
            //command.AddInputParameter("@KnowledgeID", DbType.Int32, knowledgeID);
            //Database.ExecuteNonQuery(command);
        }

        /// <summary>
        /// Gets the data.
        /// </summary>
        /// <param name="FileIncludeID">The file include ID.</param>
        /// <returns></returns>
        public byte[] GetData(Guid FileIncludeID)
        {
            var command = Database.CreateSqlCommand("SELECT Data FROM FileIncludes WHERE FileIncludeID=@FileIncludeID", "@FileIncludeID", FileIncludeID);
            return Database.ExcecuteByteArray(command);
        }


        /// <summary>
        /// Sets the data.
        /// </summary>
        /// <param name="FileIncludeID">The file include ID.</param>
        /// <param name="data">The data.</param>
        public void SetData(Guid FileIncludeID, byte[] data)
        {

            var command = Database.CreateSqlCommand("UPDATE FileIncludes SET Data=@Data WHERE FileIncludeID=@FileIncludeID", "@FileIncludeID", FileIncludeID);
            command.AddInputParameter("@Data", DbType.Binary, data);

            Database.ExecuteNonQuery(command);
        }

        /// <summary>
        /// Deletes the name of the by knowledge ID by file.
        /// </summary>
        /// <param name="knowledgeID">The knowledge ID.</param>
        /// <param name="fileName">Name of the file.</param>
        public void DeleteByKnowledgeIDByFileName(System.Int32 knowledgeID, string fileName)
        {
            var command = Database.CreateSqlCommand(@"DELETE FROM FileIncludes
WHERE KnowledgeID=@KnowledgeID AND FileName=@FileName", "@KnowledgeID", knowledgeID);
            command.AddInputParameter("@FileName", DbType.String, fileName);

            Database.ExecuteNonQuery(command);
        }
    }
}
