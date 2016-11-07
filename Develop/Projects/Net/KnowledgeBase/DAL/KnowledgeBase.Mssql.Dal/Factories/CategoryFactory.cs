using System.Collections.Generic;
using System.Data;
using KnowledgeBase.DAL;
using Savchin.Core;
using Savchin.Data.Common;

namespace KnowledgeBase.Mssql.Dal
{
    public partial class CategoryFactory : ICategoryFactory
    {
        /// <summary>
        /// Selects the root level.
        /// </summary>
        /// <returns></returns>
        public IList<CategoryValue> SelectRootLevel()
        {
            return Select(Database.CreateSPCommand("_Category_GetRootLevel"));
        }
        /// <summary>
        /// Gets the tree.
        /// </summary>
        /// <returns></returns>
        public IList<TreeNode> GetTree()
        {
            var result = new List<TreeNode>();
            using (var reader = Database.ExecuteReader("_Categories_GetTree"))
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
            var command = Database.CreateSPCommand("_Cagegory_GetShortInfoByParentCategoryID");
            command.AddInputParameter("@ParentCategoryID", DbType.Int32, ParentCategoryID);

            return SelectDataSet(command);
        }

        /// <summary>
        /// Finds the name of the by.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        public IList<CategoryValue> FindByName(string name)
        {
            string matcher = name.Replace('*', '%');
            if (!matcher.Contains("%"))
                matcher = '%' + matcher + '%';

            return Select(Database.CreateSPCommand("_Category_FindByName", "@Name", matcher));
        }
    }
}
