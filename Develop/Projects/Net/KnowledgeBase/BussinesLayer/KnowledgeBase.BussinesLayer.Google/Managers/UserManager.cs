using System;
using System.Collections.Generic;
using Google.GData.Client;
using Google.GData.Documents;
using KnowledgeBase.BussinesLayer.Security;

namespace KnowledgeBase.BussinesLayer.Google.Managers
{
    class UserManager : ManagerBase, IUserManager
    {
        private User _user;
        #region Implementation of IUserManager

        public UserManager(GoogleContext context)
            : base(context)
        {
        }

        public IList<User> GetAll()
        {
            throw new NotImplementedException();
        }

        public User GetByID(int userId)
        {
            return _user;
        }

        public User Login(string login, string password)
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
                _user = new User
                {
                    Email = login,
                    Login = login,
                    Password = password
                };
                return _user;

            }
            catch (AuthenticationException )
            {
                return null;
            }
        }

        public User GetByLogin(string Login)
        {
            throw new NotImplementedException();
        }

        public void Save(User user, Dictionary<int, Permission> permissions)
        {
            throw new NotImplementedException();
        }

        public void Save(User entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(User entity)
        {
            throw new NotImplementedException();
        }

        public bool CanLogin(string login, string password)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
