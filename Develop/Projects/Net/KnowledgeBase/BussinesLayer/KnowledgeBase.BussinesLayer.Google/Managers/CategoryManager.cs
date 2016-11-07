using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using KnowledgeBase.BussinesLayer.Security;
using KnowledgeBase.DAL;

namespace KnowledgeBase.BussinesLayer.Google.Managers
{
    class CategoryManager : ManagerBase, ICategoryManager
    {

        #region Implementation of ICategoryManager

        public CategoryManager(GoogleContext context)
            : base(context)
        {
        }

        public List<Category> GetAll()
        {
            //var query = new DocumentsListQuery();
            //query.ShowFolders=true;

            //DocumentsFeed feed = Service.Query(query);
            //var res= new List<CategoryValue>();
            //foreach (var d in feed.Entries.Cast<DocumentEntry>().Where(e => e.IsFolder))
            //{

            //   res.Add(new CategoryValue
            //               {
            //                   Name = d.Title.Text,

            //               }); 
            //}
            //return res;


            var feed = CreateRequest().GetFolders();

            var res = new List<Category>();
            foreach (var d in feed.Entries)
            {
                var cat = new Category
                {
                    Name = d.Title,
                    CategoryID = GetInternalId(d.ResourceId)
                };
                if (d.ParentFolders.Count > 0)
                {
                    cat.ParentCategoryID = GetInternalId(GetGlobaId(d.ParentFolders[0]));
                }
                res.Add(cat);

            }
            return res;
        }

        public Category GetByID(int id)
        {
            return Context.Categories.FirstOrDefault(e => e.CategoryID == id);
        }

        public IList<Category> GetRootLevel()
        {
            throw new NotImplementedException();
        }

        public IList<Category> GetByParentCategoryID(int parentCategoryID)
        {
            var childrens = Context.CategoryTree.GetChild(parentCategoryID);
            if (childrens.Length == 0) return new Category[0];

            var permissions = Context.PermissionSet;
            var visibleCategories = childrens.Where(e => permissions[e].HasPermission(Permission.View)).ToArray();

            if (visibleCategories.Length == 0) return new Category[0];

            return Context.Categories.Where(e => visibleCategories.Contains(e.CategoryID)).ToArray();
        }

        public IList<TreeNode> GetTree()
        {
            return Context.Categories.Select(e => new TreeNode(e.ParentCategoryID ?? 0, e.CategoryID)).OrderBy(e => e.ParentId).ThenBy(e => e.Id).ToArray();
        }

        public DataView GetShortInfoByParentCategoryID(int ParentCategoryID)
        {
            DataTable table = new DataTable();
            table.Columns.Add("CategoryID", typeof(int));
            table.Columns.Add("ParentCategoryID", typeof(int));
            table.Columns.Add("Name", typeof(string));
            table.Columns.Add("CreationDate", typeof(DateTime));
            table.Columns.Add("cntKnowledges", typeof(int));
            table.Columns.Add("cntSubCategories", typeof(int));

            foreach (var cat in GetByParentCategoryID(ParentCategoryID))
            {
                table.Rows.Add(cat.CategoryID, cat.ParentCategoryID, cat.Name, cat.CreationDate, 0, 0);
            }


            return table.DefaultView;
        }

        public bool CheckIsParent(int categoryID, int childCategoryId)
        {
            throw new NotImplementedException();
        }

        public List<Category> FindByName(string matcher)
        {
            throw new NotImplementedException();
        }

        public void Save(Category entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(Category entity)
        {
            throw new NotImplementedException();
        }

        #endregion

     

    }
}
