using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;
using KnowledgeBase.DAL;
using Savchin.Data.Common;

namespace KnowledgeBase.Mssql.Dal
{
    public partial class UserFileFactory : IUserFileFactory
    {
        /// <summary>
        /// Gets the data.
        /// </summary>
        /// <param name="UserFileID">The user file ID.</param>
        /// <returns></returns>
        public byte[] GetData(System.Int32 UserFileID)
        {
            return Database.ExcecuteByteArray("_UserFiles_GetData", "@UserFileID", UserFileID);
        }


        /// <summary>
        /// Sets the data.
        /// </summary>
        /// <param name="UserFileID">The user file ID.</param>
        /// <param name="data">The data.</param>
        /// <returns></returns>
        public void SetData(System.Int32 UserFileID, byte[] data)
        {
            IDbCommand command=Database.CreateSPCommand("_UserFiles_SetData", "@UserFileID", UserFileID);
            command.AddInputParameter( "@Data", DbType.Binary, data);

            Database.ExecuteNonQuery(command);
        }

        /// <summary>
        /// Deletes the name of the by user ID by file.
        /// </summary>
        /// <param name="UserID">The user ID.</param>
        /// <param name="fileName">Name of the file.</param>
        public void DeleteByUserIDByFileName(System.Int32 UserID, string fileName)
        {
            IDbCommand command = Database.CreateSPCommand("_UserFiles_DeleteByUserIDByFileName", "@UserID", UserID);
            command.AddInputParameter( "@FileName", DbType.String, fileName);

            Database.ExecuteNonQuery(command);
        }
    }
}
