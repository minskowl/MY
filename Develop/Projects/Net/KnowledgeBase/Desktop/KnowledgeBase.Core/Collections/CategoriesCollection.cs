using System.Data;
using System.Linq;
using KnowledgeBase.BussinesLayer;
using KnowledgeBase.BussinesLayer.Core;

namespace KnowledgeBase.Desktop.Collections
{
    /// <summary>
    /// CategoriesCollection
    /// </summary>
    public class CategoriesCollection : CollectionBase<Category>
    {
        private readonly ICategoryManager _manager;
        /// <summary>
        /// Initializes a new instance of the <see cref="CategoriesCollection"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        public CategoriesCollection(KbContext context)
            : base(context, context.ManagerCategory.GetAll())
        {
            _manager = context.ManagerCategory;
        }
        /// <summary>
        /// Saves the specified category.
        /// </summary>
        /// <param name="category">The category.</param>
        public void Save(Category category)
        {
            if (category.CategoryID>0)
            {
                this[IndexOf(category)] = category;
            }
            else
            {
                Add(category);
            }
            
        }
        /// <summary>
        /// Gets the info by parent category ID.
        /// </summary>
        /// <param name="ParentCategoryID">The parent category ID.</param>
        /// <returns></returns>
        public DataView GetInfoByParentCategoryID(int ParentCategoryID)
        {
            return _manager.GetShortInfoByParentCategoryID(ParentCategoryID);
        }

        /// <summary>
        /// Gets the by id.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns></returns>
        public Category GetById(int id)
        {
            return this.FirstOrDefault(e => e.CategoryID == id);
        }
        /// <summary>
        /// Inserts the item.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <param name="item">The item.</param>
        protected override void InsertItem(int index, Category item)
        {
            _manager.Save(item);
            base.InsertItem(index, item);
        }
        protected override void SetItem(int index, Category item)
        {
            _manager.Save(item);
            base.SetItem(index, item);
        }
        /// <summary>
        /// Removes the item.
        /// </summary>
        /// <param name="index">The index.</param>
        protected override void RemoveItem(int index)
        {
            _manager.Delete(this[index]);
            base.RemoveItem(index);
        }
    }
}
