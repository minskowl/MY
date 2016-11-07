using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KnowledgeBase.BussinesLayer.Core;

namespace KnowledgeBase.BussinesLayer.Google.Managers
{
    class UserFileManager : ManagerBase, IUserFileManager 
    {
        public UserFileManager(GoogleContext context) : base(context)
        {
        }

        #region Implementation of IUserFileManager

        public IList<UserFile> GetAll()
        {
            //TODO: need investigate
            return new UserFile[0];
        }

        public UserFile GetByID(int UserFileID)
        {
            throw new NotImplementedException();
        }

        public IList<UserFile> GetByUserID(int UserID)
        {
            //TODO: need investigate
            return new UserFile[0];
        }

        public void Save(UserFile entity)
        {
            throw new NotImplementedException();
        }

        public UserFile Create(string fileName, byte[] content)
        {
            throw new NotImplementedException();
        }

        public byte[] GetData(int id)
        {
            throw new NotImplementedException();
        }

        public void SetData(int id, byte[] data)
        {
            throw new NotImplementedException();
        }

        public void Delete(UserFile entity)
        {
            throw new NotImplementedException();
        }

        public void DeleteByUserIDByFileName(int userID, string fileName)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
