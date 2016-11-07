using System.Collections.Generic;
using System.Data;
using KnowledgeBase.DAL;
using Savchin.Data.Common;
using Savchin.Data.MSSQL;

namespace KnowledgeBase.Mssql.Dal
{
    public partial class KeywordFactory : IKeywordFactory
    {
        /// <summary>
        /// Finds the name of the by.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        public IList<KeywordValue> FindByName(string name)
        {
            string matcher = name.Replace('*', '%');
            if (!matcher.Contains("%"))
                matcher = '%' + matcher + '%';

            return Select(Database.CreateSPCommand("_Keywords_FindByName", "@Name", matcher));
        }
        /// <summary>
        /// Gets the info all.
        /// </summary>
        /// <returns></returns>
        public DataSet GetInfoAll()
        {
            return Database.ExecuteDataset(Database.CreateSPCommand("_KeywordInfo_GetAll"));
        }
        /// <summary>
        /// Gets the name of the by.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        public KeywordValue GetByName(string name)
        {
            return SelectSingle(Database.CreateSPCommand("_Keywords_GetByName", "@Name", name));
        }

        /// <summary>
        /// Sets the status.
        /// </summary>
        /// <param name="keywordId">The keyword ID.</param>
        /// <param name="keywordStatusId">The keyword status ID.</param>
        public void SetStatus(int keywordId, byte keywordStatusId)
        {

            IDbCommand command = Database.CreateSPCommand("_Keywords_SetStatus");
            command.AddInputParameter("@KeywordID", DbType.Int32, keywordId);
            command.AddInputParameter("@KeywordStatusID", DbType.Byte, keywordStatusId);

            Database.ExecuteNonQuery(command);
        }

        /// <summary>
        /// Gets the by list ID.
        /// </summary>
        /// <param name="ids">The ids.</param>
        /// <returns></returns>
        public IList<KeywordValue> GetByListID(ICollection<int> ids)
        {
            if (ids.Count == 0)
                return new List<KeywordValue>();

            new ArrayParameter().Send(Database, ids);
            return Select(Database.CreateSPCommand("_Keywords_GetByListID"));
        }


    }
}
