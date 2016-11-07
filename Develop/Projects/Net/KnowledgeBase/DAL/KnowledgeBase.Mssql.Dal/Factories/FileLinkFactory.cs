using System;
using System.Data;
using KnowledgeBase.DAL;
using Savchin.Data.Common;

namespace KnowledgeBase.Mssql.Dal
{
    public partial class FileLinkFactory : IFileLinkFactory
    {
        /// <summary>
        /// Selects the by file storage ID by path.
        /// </summary>
        /// <param name="FileStorageID">The file storage ID.</param>
        /// <param name="Path">The path.</param>
        /// <returns></returns>
        public FileLinkValue SelectByFileStorageIDByPath(Int16 FileStorageID, string Path)
        {
            IDbCommand command = Database.CreateSPCommand("_FileLinks_GetByFileStorageIDByPath");
            command.AddInputParameter( "@FileStorageID", DbType.Int16, FileStorageID);
            command.AddInputParameter( "@Path", DbType.String, Path);
            return SelectSingle(command);
        }

        /// <summary>
        /// Gets the by public ID.
        /// </summary>
        /// <param name="publicID">The public ID.</param>
        /// <returns></returns>
        public FileLinkValue GetByPublicID(Guid publicID)
        {
            return SelectSingle("_FileLinks_GetByPublicID", "@PublicID", publicID);
        }

    }
}
