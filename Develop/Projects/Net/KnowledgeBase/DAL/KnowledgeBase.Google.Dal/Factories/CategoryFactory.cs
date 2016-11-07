using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Google.Documents;
using Google.GData.Client;
using Google.GData.Documents;
using KnowledgeBase.DAL;
using KnowledgeBase.Dal;
using Savchin.Collection.Generic;


namespace KnowledgeBase.Google.Dal.Factories
{
    class CategoryFactory : FactoryBase, ICategoryFactory
    {
        public CategoryFactory(DalContext context)
            : base(context)
        {

        }

        #region Implementation of ICategoryFactory

        public IList<CategoryValue> SelectRootLevel()
        {
            throw new NotImplementedException();
        }

        public IList<TreeNode> GetTree()
        {
            return  SelectAll().Select(e=>new TreeNode(e.ParentCategoryID??0,e.CategoryID)).OrderBy(e=>e.ParentId).ThenBy(e=>e.Id).ToArray();
        }

        public DataSet GetShortInfoByParentCategoryID(int ParentCategoryID)
        {
            throw new NotImplementedException();
        }

        public IList<CategoryValue> FindByName(string name)
        {
            throw new NotImplementedException();
        }

        public void Insert(CategoryValue value)
        {
            throw new NotImplementedException();
        }

        public void Update(CategoryValue value)
        {
            throw new NotImplementedException();
        }

        public CategoryValue SelectByID(int CategoryID)
        {
            throw new NotImplementedException();
        }

        public void Delete(int CategoryID)
        {
            throw new NotImplementedException();
        }

        public void Delete(CategoryValue value)
        {
            throw new NotImplementedException();
        }
        private BiDictionary<int, string> _mapId = new BiDictionary<int, string>();
        private int _lastId;
        private int GetInternalId(string id)
        {
            int res;
            if (_mapId.TryGetValue(id, out res))
                return res;
            _lastId++;
            _mapId.Add(_lastId, id);
            return _lastId;
        }
        private string GetGlobaId(string uri)
        {
            var u = new Uri(uri);
            return u.Segments[u.Segments.Length - 1];
        }

        public IList<CategoryValue> SelectAll()
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


            RequestSettings settings = new RequestSettings("GoogleDocumentsSample", Service.Credentials);

            var req = new DocumentsRequest(settings);
            var res = new List<CategoryValue>();
            var feed = req.GetFolders();

            foreach (var d in feed.Entries)
            {
                var cat = new CategoryValue
                              {
                                  Name = d.Title,
                                  CategoryID = GetInternalId(d.ResourceId)
                              };
                if(d.ParentFolders.Count>0)
                {
                    cat.ParentCategoryID = GetInternalId(GetGlobaId(d.ParentFolders[0]));
                }
                res.Add(cat);
                
            }
            return res;
        }

        public IList<CategoryValue> SelectByParentCategoryID(int ParentCategoryID)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
