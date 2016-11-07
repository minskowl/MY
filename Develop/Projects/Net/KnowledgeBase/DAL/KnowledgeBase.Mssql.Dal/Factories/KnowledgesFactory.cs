using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using KnowledgeBase.DAL;
using Savchin.Data.Common;
using Savchin.Text;

namespace KnowledgeBase.Mssql.Dal
{

    public partial class KnowledgeFactory : IKnowledgeFactory
    {
        /// <summary>
        /// Gets the by public ID.
        /// </summary>
        /// <param name="publicID">The public ID.</param>
        /// <returns></returns>
        public KnowledgeValue GetByPublicID(Guid publicID)
        {
            return SelectSingle("_Knowledges_GetByPublicID", "@PublicID", publicID);
        }

        /// <summary>
        /// Searches the specified text.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <param name="types">The types.</param>
        /// <param name="categories">The categories.</param>
        /// <param name="keywords">The keywords.</param>
        /// <param name="statuses">The statuses.</param>
        /// <returns></returns>
        public DataSet Search(string text, IList<int> types, IList<int> categories, IList<int> keywords, IList<short> statuses)
        {
            var whereBuilder = new StringBuilder();
            var joinBuilder = new StringBuilder();
            var fieldsBuilder = new StringBuilder();

            if (!string.IsNullOrEmpty(text))
            {
                text = text.Replace('*', '%').Replace("'", "''");
                if (!text.Contains("%"))
                {
                    text = '%' + text + '%';
                }
                whereBuilder.AppendFormat("([Summary] LIKE '{0}' OR [Title] LIKE '{0}' ) AND ", text);
            }

            if (types != null && types.Count > 0)
            {
                whereBuilder.AppendFormat(" ( [KnowledgeTypeID] IN({0}) ) AND ", StringUtil.Join(types, ","));
            }

            if (statuses != null && statuses.Count > 0)
            {
                whereBuilder.AppendFormat(" ( [KnowledgeStatusID] IN({0}) ) AND ", StringUtil.Join(statuses, ","));
            }

            if (keywords != null && keywords.Count > 0)
            {
                string ids = StringUtil.Join(keywords, ",");
                joinBuilder.AppendLine(
                    "INNER JOIN [KnowledgeKeywords] ON [KnowledgeKeywords].[KnowledgeID] = [KnowledgeInfo].[KnowledgeID] ");
                whereBuilder.AppendFormat(" ( [KnowledgeKeywords].[KeywordID] IN({0}) ) AND ", ids);
                fieldsBuilder.AppendFormat(@"(
 SELECT COUNT(*) 
 FROM [KnowledgeKeywords] AS t1 
 WHERE t1.[KnowledgeID]=[KnowledgeInfo].[KnowledgeID] AND t1.[KeywordID] IN({0})
 ) AS KeywordRank ", ids);
            }
            else
            {
                fieldsBuilder.Append(" 1 AS KeywordRank");
            }


            string where = whereBuilder.ToString();
            if (!string.IsNullOrEmpty(where))
                where = "WHERE " + where.Substring(0, where.Length - 4);

            string sql = string.Format(@"
SELECT 
[KnowledgeInfo].*,Categories.Name AS CategoryName,
{0}
FROM [KnowledgeInfo]  
INNER JOIN [Categories] ON Categories.CategoryID=KnowledgeInfo.CategoryID
{1}
{2}
ORDER BY KeywordRank
", fieldsBuilder, joinBuilder, where);
            return Database.ExecuteDataset(Database.Factory.CreateSqlCommand(sql));
        }

        /// <summary>
        /// Gets the short info by category ID.
        /// </summary>
        /// <param name="CategoryID">The category ID.</param>
        /// <returns></returns>
        public DataSet GetShortInfoByCategoryID(System.Int32 CategoryID)
        {
            return Database.ExecuteDataset("_Knowledges_GetShortInfoByCategoryID", "@CategoryID", CategoryID);
        }


        /// <summary>
        /// Saves the keywords associations.
        /// </summary>
        /// <param name="knowledgeID">The knowledge ID.</param>
        /// <param name="keywords">The keywords.</param>
        public void SaveKeywordsAssociations(int knowledgeID, IEnumerable<int> keywords)
        {
            IArrayParameter arrayParameter = Database.Factory.CreateArrayParameter();
            arrayParameter.Send(Database, keywords);
            Database.ExecuteNonQuery("_Knowledges_SaveKeywordsAssociations", "@KnowledgeID", knowledgeID);
        }

        /// <summary>
        /// Gets the keywords associations.
        /// </summary>
        /// <param name="knowledgeID">The knowledge ID.</param>
        /// <returns></returns>
        public IList<int> GetKeywordsAssociations(int knowledgeID)
        {
            return Database.ExecuteIntList("_Knowledges_GetKeywordsAssociations", "@KnowledgeID", knowledgeID);
        }
    }
}