using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using KnowledgeBase.DAL;
using Savchin.Data.Common;

namespace KnowledgeBase.SqlLite.Dal.Factories
{
    public partial class UserFileFactory : IUserFileFactory
    {

        /// <summary>
        /// Selects UserFile values UserID .
        /// ForeignKey: FK_UserFiles_Users
        /// </summary>
        /// <param name="UserID">The UserID.</param>
        /// <returns>List of UserFile</returns>   
        public IList<UserFileValue> SelectByUserID(System.Int32 UserID)
        {
            return SelectAll();
        }

        /// <summary>
        /// Gets the data.
        /// </summary>
        /// <param name="UserFileID">The user file ID.</param>
        /// <returns></returns>
        public byte[] GetData(Int32 UserFileID)
        {
            var command = Database.CreateSqlCommand("SELECT Data FROM UserFiles WHERE UserFileID=@UserFileID", "@UserFileID",
                                      UserFileID);
            return Database.ExcecuteByteArray(command);
        }


        /// <summary>
        /// Sets the data.
        /// </summary>
        /// <param name="UserFileID">The user file ID.</param>
        /// <param name="data">The data.</param>
        /// <returns></returns>
        public void SetData(System.Int32 UserFileID, byte[] data)
        {
            var command = Database.CreateSqlCommand("UPDATE UserFiles SET Data=@Data WHERE UserFileID=@UserFileID",
                "@UserFileID", UserFileID);
            command.AddInputParameter("@Data", DbType.Binary, data);

            Database.ExecuteNonQuery(command);
        }

        /// <summary>
        /// Deletes the name of the by user ID by file.
        /// </summary>
        /// <param name="UserID">The user ID.</param>
        /// <param name="fileName">Name of the file.</param>
        public void DeleteByUserIDByFileName(System.Int32 UserID, string fileName)
        {
            var command = Database.CreateSqlCommand(@"DELETE FROM dbo.UserFiles WHERE FileName=@FileName",
                "@FileName", fileName);

            Database.ExecuteNonQuery(command);
        }
    }
}
