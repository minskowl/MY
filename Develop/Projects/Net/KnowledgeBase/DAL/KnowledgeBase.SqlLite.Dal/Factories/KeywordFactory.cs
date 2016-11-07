using System.Collections.Generic;
using System.Data;
using KnowledgeBase.DAL;
using Savchin.Data.Common;
using Savchin.Text;

namespace KnowledgeBase.SqlLite.Dal.Factories
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
            var matcher = name.Replace('*', '%');
            if (!matcher.Contains("%"))
                matcher = '%' + matcher + '%';

            return Select(
                Database.CreateSqlCommand(SelectQuery + " WHERE [Name] LIKE @Name ORDER BY [Name]",
                "@Name", matcher));
        }
        /// <summary>
        /// Gets the info all.
        /// </summary>
        /// <returns></returns>
        public DataSet GetInfoAll()
        {
            return Database.ExecuteDataset(Database.CreateSqlCommand(@"
SELECT Keywords.Name, Keywords.KeywordID, Keywords.KeywordTypeID, Keywords.CreationDate, 
Keywords.KeywordStatusID,KeywordStatuses.Name AS KeywordStatusName, KeywordTypes.Name AS KeywordTypeName
FROM Keywords 
INNER JOIN  KeywordStatuses ON Keywords.KeywordStatusID = KeywordStatuses.KeywordStatusID 
INNER JOIN  KeywordTypes ON Keywords.KeywordTypeID = KeywordTypes.KeywordTypeID;"));
        }
        /// <summary>
        /// Gets the name of the by.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        public KeywordValue GetByName(string name)
        {
            return SelectSingle(Database.CreateSqlCommand(
                SelectQuery + " WHERE [Name] = @Name", "@Name", name));
        }

        /// <summary>
        /// Sets the status.
        /// </summary>
        /// <param name="keywordID">The keyword ID.</param>
        /// <param name="keywordStatusID">The keyword status ID.</param>
        public void SetStatus(int keywordID, byte keywordStatusID)
        {

            var command = Database.CreateSqlCommand(@"
UPDATE [Keywords] 
SET KeywordStatusID=@KeywordStatusID 
WHERE KeywordID=@KeywordID");
            command.AddInputParameter("@KeywordID", DbType.Int32, keywordID);
            command.AddInputParameter("@KeywordStatusID", DbType.Byte, keywordStatusID);

            Database.ExecuteNonQuery(command);
        }

        /// <summary>
        /// Gets the by list ID.
        /// </summary>
        /// <param name="ids">The ids.</param>
        /// <returns></returns>
        public IList<KeywordValue> GetByListID(ICollection<int> ids)
        {
            if (ids==null || ids.Count == 0)
                return new List<KeywordValue>();

            var q = SelectQuery + " WHERE KeywordID IN (" + ids.Join(",") + ");";
            return Select(Database.CreateSqlCommand(q));

        }


    }
}
