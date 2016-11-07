using System;
using System.Collections.Generic;
using System.Data;
using KnowledgeBase.DAL;
using Savchin.Data.Common;

namespace KnowledgeBase.SqlLite.Dal.Factories
{
    public partial class CategoryFactory : ICategoryFactory
    {
        #region Implementation of ICategoryFactory

        /// <summary>
        /// Selects the root level.
        /// </summary>
        /// <returns></returns>
        public IList<CategoryValue> SelectRootLevel()
        {
            return Select(Database.CreateSqlCommand(SelectQuery + "  WHERE ifnull(ParentCategoryID,0) =0;"));
        }

        /// <summary>
        /// Gets the tree.
        /// </summary>
        /// <returns></returns>
        public IList<TreeNode> GetTree()
        {
            var result = new List<TreeNode>();
            var commnad = Database.CreateSqlCommand(
                "SELECT ifnull([ParentCategoryID],0),[CategoryID] FROM [Categories] ORDER BY 1,2; ");

            using (var reader = Database.ExecuteReader(commnad))
                while (reader.Read())
                    result.Add(new TreeNode(reader.GetInt32(0), reader.GetInt32(1)));
            return result;
        }

        /// <summary>
        /// Gets the short info by parent category ID.
        /// </summary>
        /// <param name="ParentCategoryID">The parent category ID.</param>
        /// <returns></returns>
        public DataSet GetShortInfoByParentCategoryID(int ParentCategoryID)
        {
            var command = Database.CreateSqlCommand(@"SELECT * FROM   CategoriesInfo
WHERE ifnull(ParentCategoryID,0)=@ParentCategoryID;");
            command.AddInputParameter("@ParentCategoryID", DbType.Int32, ParentCategoryID);

            var result = new DataSet();
            var table = result.Tables.Add("data");
            table.Columns.Add("CategoryID", typeof(int));
            table.Columns.Add("ParentCategoryID", typeof(int));
            table.Columns.Add("Name", typeof(string));
            table.Columns.Add("CreationDate", typeof(DateTime));
            table.Columns.Add("cntKnowledges", typeof(int));
            table.Columns.Add("cntSubCategories", typeof(int));
            using (var reader = Database.ExecuteReader(command))
                while (reader.Read())
                {
                    table.Rows.Add(new object[]
                                       {
                                           reader.GetInteger<int>(0),
                                           reader.GetInteger<int>(1),
                                           reader.GetString(2),
                                           reader.GetDateTime(3),
                                           reader.GetInteger<int>(4),
                                           reader.GetInteger<int>(5)
                                       });
                }
            return result;
        }

        /// <summary>
        /// Finds the name of the by.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        public IList<CategoryValue> FindByName(string name)
        {
            var matcher = name.Replace('*', '%');
            if (!matcher.Contains("%"))
                matcher = '%' + matcher + '%';

            var command = Database.CreateSqlCommand(SelectQuery + " WHERE [Name] LIKE @Name;");
            command.AddInputParameter("@Name", DbType.String, matcher);
            return Select(command);
        }

        #endregion
    }
}
