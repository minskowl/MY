using System.Collections.Generic;
using System.Data;
using KnowledgeBase.DAL;

namespace KnowledgeBase.BussinesLayer
{
    public interface ICategoryManager
    {
        /// <summary>
        /// Selects all.
        /// </summary>
        /// <returns></returns> 
        List<Category> GetAll();

        /// <summary>
        /// Gets the by ID.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns></returns>
        Category GetByID(System.Int32 id);

        /// <summary>
        /// Gets the root level.
        /// </summary>
        /// <returns></returns>
        IList<Category> GetRootLevel();

        /// <summary>
        /// Gets the by parent category ID.
        /// </summary>
        /// <param name="parentCategoryID">The parent category ID.</param>
        /// <returns></returns>
        IList<Category> GetByParentCategoryID(int parentCategoryID);

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
        DataView GetShortInfoByParentCategoryID(int ParentCategoryID);

        /// <summary>
        /// Checks the is parent.
        /// </summary>
        /// <param name="categoryID">The category ID.</param>
        /// <param name="childCategoryId">The child category id.</param>
        /// <returns></returns>
        bool CheckIsParent(int categoryID, int childCategoryId);

        /// <summary>
        /// Finds the name of the by.
        /// </summary>
        /// <param name="matcher">The matcher.</param>
        /// <returns></returns>
        List<Category> FindByName(string matcher);

        /// <summary>
        /// Saves the specified Category.
        /// </summary>
        /// <param name="entity">The entity.</param>
        void Save(Category entity);

        /// <summary>
        /// Deletes the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        void Delete(Category entity);
    }
}