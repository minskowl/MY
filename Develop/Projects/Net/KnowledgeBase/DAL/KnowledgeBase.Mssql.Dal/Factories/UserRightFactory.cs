using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using KnowledgeBase.DAL;
using Savchin.Data.Common;

namespace KnowledgeBase.Mssql.Dal
{
    public partial class UserRightFactory : IUserRightFactory
    {
        /// <summary>
        /// Saves the rights.
        /// </summary>
        /// <param name="userID">The user ID.</param>
        /// <param name="rights">The rights.</param>
        public void SaveRights(int userID, IEnumerable<CategoryPermission> rights)
        {
            IDbCommand command = Database.CreateSPCommand("_UserRights_SaveRights");
            command.AddInputParameter( "@UserID", DbType.Int32, userID);

            IArrayParameter arrayParameter = Database.Factory.CreateArrayParameter();
            arrayParameter.Send(Database, rights);
            Database.ExecuteNonQuery(command);
        }

    }
}
