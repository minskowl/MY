using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Threading;
using KnowledgeBase.BussinesLayer.Core;
using KnowledgeBase.BussinesLayer.Security;

namespace KnowledgeBase.Desktop.Core
{
    /// <summary>
    /// CategoryNode
    /// </summary>
    public class CategoryNode : TreeNode
    {
        /// <summary>
        /// Gets a value indicating whether this instance is loaded.
        /// </summary>
        /// <value><c>true</c> if this instance is loaded; otherwise, <c>false</c>.</value>
        public bool IsLoaded
        {
            get
            {
                return (Childrens.Count != 1 || Childrens[0].Id > -1);
            }
        }
        /// <summary>
        /// Gets or sets a value indicating whether this instance is expanded.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is expanded; otherwise, <c>false</c>.
        /// </value>
        public override bool IsExpanded
        {
            get { return base.IsExpanded; }
            set
            {
                if (value) LoadItems();
                base.IsExpanded = value;

            }
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="CategoryNode"/> class.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <param name="text">The text.</param>
        /// <param name="parent">The parent.</param>
        public CategoryNode(int id, string text, TreeNode parent)
            : base(id, text, parent)
        {
            Childrens.Add(new TreeNode(-1, "Loading...", this));
        }

        /// <summary>
        /// Creates the root.
        /// </summary>
        /// <returns></returns>
        public static CategoryNode CreateRoot()
        {
            return new CategoryNode(0, "Root", null);
        }


        /// <summary>
        /// Loads the items.
        /// </summary>
        public void LoadItems()
        {
            // If there is no data
            if (IsLoaded) return;

            // Clear the Children list
            var categories = KbContext.CurrentKb.ManagerCategory.GetByParentCategoryID(Id)
                .Select(category => new CategoryNode(category.CategoryID, category.Name, this));

            // Populate the treeview thanks to the bind


            Childrens.Clear();

            foreach (var node in categories)
            {
                node.IsVisible = KbContext.CurrentKb.PermissionSet.HasVisibleChildren(node.Id);
                Childrens.Add(node);
            }
        }
        /// <summary>
        /// Finds the selected.
        /// </summary>
        /// <returns></returns>
        public CategoryNode FindSelected()
        {
            if (IsSelected) return this;
            foreach (CategoryNode item in Childrens)
            {
                var finded = item.FindSelected();
                if (finded != null)
                    return finded;
            }
            return null;
        }

        /// <summary>
        /// Finds the by id.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns></returns>
        public CategoryNode FindById(int id)
        {
            if (Id == id)
            {
                return this;
            }
            LoadItems();
            foreach (CategoryNode item in Childrens)
            {
                var finded = item.FindById(id);
                if (finded != null)
                {
                    item.IsExpanded = true;
                    return finded;
                }

            }
            return null;
        }
    }
}
