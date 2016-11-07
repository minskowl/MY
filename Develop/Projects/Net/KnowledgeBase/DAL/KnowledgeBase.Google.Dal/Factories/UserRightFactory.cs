using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KnowledgeBase.DAL;
using KnowledgeBase.Dal;

namespace KnowledgeBase.Google.Dal.Factories
{
    class UserRightFactory : FactoryBase, IUserRightFactory
    {
        public UserRightFactory(DalContext context) : base(context)
        {
        }

        #region Implementation of IUserRightFactory

        public void SaveRights(int userID, IEnumerable<CategoryPermission> rights)
        {
            throw new NotImplementedException();
        }

        public void Insert(UserRightValue value)
        {
            throw new NotImplementedException();
        }

        public void Update(UserRightValue value)
        {
            throw new NotImplementedException();
        }

        public UserRightValue SelectByID(int CategoryID, int UserID)
        {
            throw new NotImplementedException();
        }

        public void Delete(int CategoryID, int UserID)
        {
            throw new NotImplementedException();
        }

        public void Delete(UserRightValue value)
        {
            throw new NotImplementedException();
        }

        public IList<UserRightValue> SelectAll()
        {
            throw new NotImplementedException();
        }

        public IList<UserRightValue> SelectByCategoryID(int CategoryID)
        {
            throw new NotImplementedException();
        }

        public IList<UserRightValue> SelectByUserID(int UserID)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
