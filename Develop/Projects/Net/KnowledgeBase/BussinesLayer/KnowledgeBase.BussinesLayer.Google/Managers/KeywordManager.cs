using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using KnowledgeBase.BussinesLayer.Core;

namespace KnowledgeBase.BussinesLayer.Google.Managers
{
    class KeywordManager : ManagerBase, IKeywordManager
    {
        public KeywordManager(GoogleContext context)
            : base(context)
        {
        }

        #region Implementation of IKeywordManager

        public DataSet GetInfoAll()
        {
            throw new NotImplementedException();
        }

        public IList<Keyword> GetAll()
        {
            //TODO: Investigate
           return new Keyword[0];
        }

        public Keyword GetByID(int keywordId)
        {
            throw new NotImplementedException();
        }

        public Keyword GetByName(string name)
        {
            throw new NotImplementedException();
        }

        public IList<Keyword> GetByListID(ICollection<int> ids)
        {
            throw new NotImplementedException();
        }

        public IList<Keyword> FindByName(string name)
        {
            throw new NotImplementedException();
        }

        public void Save(Keyword entity)
        {
            throw new NotImplementedException();
        }

        public void SetStatus(int keywordId, KeywordStatus status)
        {
            throw new NotImplementedException();
        }

        public void Delete(Keyword entity)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
