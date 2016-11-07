using System.Collections.Generic;
using System.Data;
using KnowledgeBase.DAL;
using Savchin.Core;

namespace KnowledgeBase.DAL
{
    public interface ICategoryFactory
    {
        /// <summary>
        /// Selects the root level.
        /// </summary>
        /// <returns></returns>
        IList<CategoryValue> SelectRootLevel();

        /// <summary>
        /// Gets the tree.
        /// </summary>
        /// <returns></returns>
        IList<TreeNode> GetTree();

        /// <summary>
        /// Gets the short info by parent category ID.
        /// </summary>
        /// <param name="ParentCategoryID">The parent category ID.</param>
        /// <returns></returns>
        DataSet GetShortInfoByParentCategoryID(int ParentCategoryID);

        /// <summary>
        /// Finds the name of the by.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        IList<CategoryValue> FindByName(string name);

        /// <summary>
        /// Creates the insert command.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        void Insert(CategoryValue value);

        /// <summary>
        /// Updates the specified Category.
        /// </summary>
        /// <param name="value">The Category value.</param>
        void Update(CategoryValue value);

        /// <summary>
        /// Gets Category by ID.
        /// </summary>
        /// <param name="CategoryID">The CategoryID.</param>
        /// <returns></returns>
        CategoryValue SelectByID(System.Int32 CategoryID);

        /// <summary>
        /// Deletes the specified Category.
        /// </summary>
        /// <param name="CategoryID">The CategoryID.</param>
        void Delete(System.Int32 CategoryID);

        /// <summary>
        /// Deletes the specified Category.
        /// </summary>
        /// <param name="value">The value.</param>
        void Delete(CategoryValue value);

        /// <summary>
        /// Selects all Category values.
        /// </summary>
        /// <returns>List of all Category</returns>
        IList<CategoryValue> SelectAll();

        /// <summary>
        /// Selects Category values ParentCategoryID .
        /// ForeignKey: FK_Categories_Categories
        /// </summary>
        /// <param name="ParentCategoryID">The ParentCategoryID.</param>
        /// <returns>List of Category</returns>   
        IList<CategoryValue> SelectByParentCategoryID(System.Int32 ParentCategoryID);
    }
}