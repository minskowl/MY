using System;
using System.Linq;
using System.Collections.Generic;
using System.Data;

namespace KnowledgeBase.BussinesLayer.Google.Managers
{
    class KnowledgeManager : ManagerBase, IKnowledgeManager
    {
        public KnowledgeManager(GoogleContext context)
            : base(context)
        {
        }

        #region Implementation of IKnowledgeManager

        public IList<Knowledge> GetAll()
        {
            var feed = CreateRequest().GetDocuments();
            var res = new List<Knowledge>();

            foreach (var d in feed.Entries)
            {
                var o = new Knowledge
                {
                    Title = d.Title,
                    KnowledgeID = GetInternalId(d.ResourceId),

                };
                if (d.ParentFolders.Count > 0)
                    o.CategoryID = GetInternalId(GetGlobaId(d.ParentFolders[0]));
                res.Add(o);
            }
            return res;
        }

        public Knowledge GetByID(int KnowledgeID)
        {
            return Context.Knowledges.FirstOrDefault(e => e.KnowledgeID == KnowledgeID);
        }

        public Knowledge GetByPublicID(Guid publicID)
        {
            return Context.Knowledges.FirstOrDefault(e => e.PublicID == publicID);
        }

        public IList<Knowledge> GetByCategoryID(int CategoryID)
        {
            return Context.Knowledges.Where(e => e.CategoryID == CategoryID).ToArray();
        }

        public IList<Knowledge> GetByKnowledgeTypeID(short KnowledgeTypeID)
        {
            throw new NotImplementedException();
        }

        public IList<Knowledge> GetByCreatorID(int CreatorID)
        {
            throw new NotImplementedException();
        }

        public IList<Knowledge> GetByModificatorID(int ModificatorID)
        {
            throw new NotImplementedException();
        }

        public void AddCanViewColumn(DataTable data)
        {
            var indexCanView = data.Columns.Add("CanView", typeof(bool)).Ordinal;
            foreach (DataRow row in data.Rows)
            {
                row[indexCanView] = true;
            }
        }

        public void RemoveNotViewable(DataTable result)
        {
           
        }

        public DataTable GetShortInfoByCategoryID(int categoryID)
        {
            /*
             * Knowledges.KnowledgeID, Knowledges.Title, Knowledges.CreationDate, Knowledges.KnowledgeTypeID, Knowledges.KnowledgeStatusID,
                         KnowledgeTypes.Name AS KnowledgeTypeName, Knowledges.PublicID, KnowledgeStatuses.Name AS KnowledgeStatusName,
                         Knowledges.CategoryID, Knowledges.Summary, Knowledges.CreatorID
             */
            DataTable table = new DataTable();
            table.Columns.Add("KnowledgeID", typeof(int));
            table.Columns.Add("Title", typeof(string));
            table.Columns.Add("CreationDate", typeof(DateTime));
            table.Columns.Add("KnowledgeTypeID", typeof(int));
            table.Columns.Add("KnowledgeStatusID", typeof(int));
            table.Columns.Add("KnowledgeTypeName", typeof(string));
            table.Columns.Add("PublicID", typeof(string));
            table.Columns.Add("KnowledgeStatusName", typeof(string));
            table.Columns.Add("Summary", typeof(string));
            table.Columns.Add("CreatorID", typeof(int));
            foreach (var k in GetByCategoryID(categoryID))
            {
                table.Rows.Add(k.KnowledgeID, k.Title, k.CreationDate, (int)k.KnowledgeType, (int)k.KnowledgeStatus, k.KnowledgeType.ToString(),
                    k.PublicIdString, k.KnowledgeStatus.ToString(), string.Empty, k.CreatorID);
            }


            return table;
        }

        public void AddCanEditColumn(DataTable data)
        {
            var indexCanEdit = data.Columns.Add("CanEdit", typeof(bool)).Ordinal;
            foreach (DataRow row in data.Rows)
            {
                row[indexCanEdit] = true;
            }
        }

        public DataTable Search(string text, IList<KnowledgeType> types, IList<int> categories, IList<int> keywords, IList<KnowledgeStatus> statuses)
        {
            throw new NotImplementedException();
        }

        public void Save(Knowledge entity, IList<int> keywords, IEnumerable<string> newKeywords)
        {
            throw new NotImplementedException();
        }

        public IList<int> GetKeywordsAssociations(int knowledgeID)
        {
            throw new NotImplementedException();
        }

        public void Delete(Knowledge entity)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
