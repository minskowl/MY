using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using KnowledgeBase.DAL;
using KnowledgeBase.Dal;

namespace KnowledgeBase.Google.Dal.Factories
{
    class KeywordFactory : FactoryBase, IKeywordFactory
    {
        public KeywordFactory(DalContext context) : base(context)
        {
        }

        #region Implementation of IKeywordFactory

        public IList<KeywordValue> FindByName(string name)
        {
            throw new NotImplementedException();
        }

        public DataSet GetInfoAll()
        {
            throw new NotImplementedException();
        }

        public KeywordValue GetByName(string name)
        {
            throw new NotImplementedException();
        }

        public void SetStatus(int keywordID, byte keywordStatusID)
        {
            throw new NotImplementedException();
        }

        public IList<KeywordValue> GetByListID(ICollection<int> ids)
        {
            throw new NotImplementedException();
        }

        public void Insert(KeywordValue value)
        {
            throw new NotImplementedException();
        }

        public void Update(KeywordValue value)
        {
            throw new NotImplementedException();
        }

        public KeywordValue SelectByID(int KeywordID)
        {
            throw new NotImplementedException();
        }

        public void Delete(int KeywordID)
        {
            throw new NotImplementedException();
        }

        public void Delete(KeywordValue value)
        {
            throw new NotImplementedException();
        }

        public IList<KeywordValue> SelectAll()
        {
            //TODO: Need ivestigate
            return new KeywordValue[0];
        }

        public IList<KeywordValue> SelectByKeywordStatusID(byte KeywordStatusID)
        {
            throw new NotImplementedException();
        }

        public IList<KeywordValue> SelectByKeywordTypeID(short KeywordTypeID)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
