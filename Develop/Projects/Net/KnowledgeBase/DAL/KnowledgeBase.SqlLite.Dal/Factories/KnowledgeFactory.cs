using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using KnowledgeBase.DAL;
using Savchin.Data.Common;
using Savchin.Text;

namespace KnowledgeBase.SqlLite.Dal.Factories
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
            var command = Database.CreateSqlCommand(SelectQuery + "	WHERE PublicID=@PublicID;");
            command.AddInputParameter("@PublicID", DbType.Guid, publicID);
            return SelectSingle(command);
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
                var ids = keywords.Join(",");
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


            var where = whereBuilder.ToString();
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
           //return Database.ExecuteDataset(Database.Factory.CreateSqlCommand(sql));

            var result = new DataSet();
            var table = result.Tables.Add("data");
            table.Columns.Add("KnowledgeID", typeof(int));
            table.Columns.Add("Title", typeof(string));
            table.Columns.Add("CreationDate", typeof(DateTime));
            table.Columns.Add("KnowledgeTypeID");
            table.Columns.Add("KnowledgeStatusID", typeof(byte));
            table.Columns.Add("KnowledgeTypeName", typeof(string));
            table.Columns.Add("PublicID", typeof(string));
            table.Columns.Add("KnowledgeStatusName", typeof(string));
            table.Columns.Add("CategoryID", typeof(int));
            table.Columns.Add("Summary", typeof(string));
            table.Columns.Add("CreatorID", typeof(int));
            table.Columns.Add("CategoryName", typeof(string));
            table.Columns.Add("KeywordRank", typeof(int));
            using (var reader = Database.ExecuteReader(sql))
                while (reader.Read())
                {
                    table.Rows.Add(new object[]
                                       {
                                           reader.GetInteger<int>(0),
                                           reader.GetString(1),
                                           reader.GetDateTime(2),
                                           reader.GetValue(3),
                                           reader.GetInteger<byte>(4),
                                           reader.GetValue(5),
                                           reader.GetValue(6),
                                           reader.GetValue(7),
                                           reader.GetInteger<int>(8),
                                            reader.GetValue(9),
                                           reader.GetInteger<int>(10),
                                           reader.GetString(11),
                                            reader.GetInteger<int>(12),
                                       });
                }
            return result;
        }

        /// <summary>
        /// Gets the short info by category ID.
        /// </summary>
        /// <param name="CategoryID">The category ID.</param>
        /// <returns></returns>
        public DataSet GetShortInfoByCategoryID(System.Int32 CategoryID)
        {
            var command = Database.CreateSqlCommand(@"
SELECT * FROM KnowledgeInfo WHERE CategoryID=@CategoryID; ");
            command.AddInputParameter("@CategoryID", DbType.Int32, CategoryID);

            //var result= SelectDataSet(command); ;
            //result.Tables[0].Columns["CategoryID"].DataType=typeof(int);
            var result = new DataSet();
            var table = result.Tables.Add("data");
            table.Columns.Add("KnowledgeID", typeof(int));
            table.Columns.Add("Title", typeof(string));
            table.Columns.Add("CreationDate", typeof(DateTime));
            table.Columns.Add("KnowledgeTypeID");
            table.Columns.Add("KnowledgeStatusID", typeof(byte));
            table.Columns.Add("KnowledgeTypeName", typeof(string));
            table.Columns.Add("PublicID", typeof(string));
            table.Columns.Add("KnowledgeStatusName", typeof(string));
            table.Columns.Add("CategoryID", typeof(int));
            table.Columns.Add("Summary", typeof(string));
            table.Columns.Add("CreatorID", typeof(int));

            using (var reader = Database.ExecuteReader(command))
                while (reader.Read())
                {
                    table.Rows.Add(new object[]
                                       {
                                           reader.GetInteger<int>(0),
                                           reader.GetString(1),
                                           reader.GetDateTime(2),
                                           reader.GetValue(3),
                                           reader.GetInteger<byte>(4),
                                           reader.GetValue(5),
                                           reader.GetValue(6),
                                           reader.GetValue(7),
                                           reader.GetInteger<int>(8),
                                            reader.GetValue(9),
                                           reader.GetInteger<int>(10),
                                       });
                }
            return result;

        
        }


        /// <summary>
        /// Saves the keywords associations.
        /// </summary>
        /// <param name="knowledgeID">The knowledge ID.</param>
        /// <param name="keywords">The keywords.</param>
        public void SaveKeywordsAssociations(int knowledgeID, IEnumerable<int> keywords)
        {
            const string deleteQuery = "DELETE FROM [KnowledgeKeywords] WHERE [KnowledgeID]=@KnowledgeID ";


            if (keywords.Count() == 0)
            {
                Database.ExecuteNonQuery(Database.CreateSqlCommand(deleteQuery, "@KnowledgeID", knowledgeID));
            }
            else
            {
                Context.BeginTransaction();
                try
                {
                    var existed = GetKeywordsAssociations(knowledgeID);
                    var forDelete = existed.Except(keywords).ToArray();
                    if (forDelete.Length > 0)
                    {
                        var query = deleteQuery + " AND [KeywordID] IN ( " + forDelete.Join(",") + ");";
                        Database.ExecuteNonQuery(Database.CreateSqlCommand(query, "@KnowledgeID", knowledgeID));
                    }
                    var forInsert = keywords.Except(existed).ToArray();
                    if (forInsert.Length > 0)
                    {
                        var inserCommand = Database.CreateSqlCommand(
                            "INSERT INTO [KnowledgeKeywords] ([KnowledgeID],[KeywordID]) VALUES( @KnowledgeID,@KeywordID);");

                        inserCommand.AddInputParameter("@KnowledgeID", knowledgeID);
                        var keywordParam = inserCommand.AddInputParameter("@KeywordID", 0);
                        foreach (var keyword in keywords.Except(existed))
                        {
                            keywordParam.Value = keyword;
                            Database.ExecuteNonQuery(inserCommand);
                        }
                    }


                    Context.CommitTransaction();
                }
                catch
                {
                    Context.RollbackTransaction();
                    throw;
                }
            }

        }

        /// <summary>
        /// Gets the keywords associations.
        /// </summary>
        /// <param name="knowledgeID">The knowledge ID.</param>
        /// <returns></returns>
        public IList<int> GetKeywordsAssociations(int knowledgeID)
        {
            var command =
                Database.CreateSqlCommand(
                    "SELECT [KeywordID] FROM [KnowledgeKeywords] WHERE [KnowledgeKeywords].[KnowledgeID]=@KnowledgeID;",
                    "@KnowledgeID", knowledgeID);
            return Database.ExecuteIntList(command);
        }

        partial void BeforeInser(KnowledgeValue value)
        {
            value.PublicID = Guid.NewGuid();
        }
    }
}
