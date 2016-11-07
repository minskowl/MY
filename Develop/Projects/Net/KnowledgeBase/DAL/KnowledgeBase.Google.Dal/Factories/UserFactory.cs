using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Google.GData.Client;
using Google.GData.Documents;
using KnowledgeBase.DAL;
using KnowledgeBase.Dal;

namespace KnowledgeBase.Google.Dal.Factories
{
    class UserFactory : FactoryBase, IUserFactory
    {
        private UserValue _user;
        public UserFactory(DalContext context)
            : base(context)
        {

        }

        #region Implementation of IUserFactory

        public void Insert(UserValue value)
        {
            throw new NotImplementedException();
        }

        public void Update(UserValue value)
        {
            throw new NotImplementedException();
        }

        public UserValue SelectByID(int UserID)
        {
            //Single user access
            return _user;
        }

        public void Delete(int UserID)
        {
            throw new NotSupportedException();
        }

        public void Delete(UserValue value)
        {
            throw new NotSupportedException();
        }

        public IList<UserValue> SelectAll()
        {
            throw new NotImplementedException();
        }

        public IList<UserValue> SelectByRootPermissionID(short RootPermissionID)
        {
            throw new NotImplementedException();
        }

        public UserValue GetByLoginByPassword(string Login, string Password)
        {
            throw new NotImplementedException();
        }

        public UserValue GetByLogin(string Login)
        {
            throw new NotImplementedException();
        }

        public UserValue Login(string login, string password)
        {
            try
            {
                var service = new DocumentsService("Idea Provider");
                ((GDataRequestFactory)service.RequestFactory).KeepAlive = false;
                service.setUserCredentials(login, password);
                //force the service to authenticate
                DocumentsListQuery query = new DocumentsListQuery();
                query.NumberToRetrieve = 1;
                service.Query(query);

                Service = service;
                _user = new UserValue
                {
                    Email = login,
                    Login = login,
                    Password = password
                };
                return _user;

            }
            catch (AuthenticationException e)
            {
                return null;
            }
        }

        #endregion
    }
}
