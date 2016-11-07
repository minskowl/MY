using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KnowledgeBase.DAL;
using KnowledgeBase.Dal;

namespace KnowledgeBase.Google.Dal.Factories
{
    class UserFileFactory : FactoryBase, IUserFileFactory
    {
        public UserFileFactory(DalContext context) : base(context)
        {
        }

        #region Implementation of IUserFileFactory

        public byte[] GetData(int UserFileID)
        {
            throw new NotImplementedException();
        }

        public void SetData(int UserFileID, byte[] data)
        {
            throw new NotImplementedException();
        }

        public void DeleteByUserIDByFileName(int UserID, string fileName)
        {
            throw new NotImplementedException();
        }

        public void Insert(UserFileValue value)
        {
            throw new NotImplementedException();
        }

        public void Update(UserFileValue value)
        {
            throw new NotImplementedException();
        }

        public UserFileValue SelectByID(int UserFileID)
        {
            throw new NotImplementedException();
        }

        public void Delete(int UserFileID)
        {
            throw new NotImplementedException();
        }

        public void Delete(UserFileValue value)
        {
            throw new NotImplementedException();
        }

        public IList<UserFileValue> SelectAll()
        {
            throw new NotImplementedException();
        }

        public IList<UserFileValue> SelectByUserID(int UserID)
        {
            //TODO: Need ivestigate
            return new UserFileValue[0];
        }

        #endregion
    }
}
