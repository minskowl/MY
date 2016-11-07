using System.Data;
using KnowledgeBase.DAL;
using Savchin.Data.Common;

namespace KnowledgeBase.Mssql.Dal
{
    public partial class UserFactory : IUserFactory
    {
        /// <summary>
        /// Gets the by login by password.
        /// </summary>
        /// <param name="Login">The login.</param>
        /// <param name="Password">The password.</param>
        /// <returns></returns>
        public UserValue GetByLoginByPassword(string Login, string Password)
        {
            IDbCommand command = Database.CreateSPCommand("_User_GetByLoginByPassword");
            command.AddInputParameter( "@Login", DbType.String, Login);
            command.AddInputParameter( "@Password", DbType.String, Password);

            return SelectSingle(command);

        }

        /// <summary>
        /// Gets the by login.
        /// </summary>
        /// <param name="Login">The login.</param>
        /// <returns></returns>
        public UserValue GetByLogin(string Login)
        {
            IDbCommand command = Database.CreateSPCommand("_User_GetByLogin");
            command.AddInputParameter( "@Login", DbType.String, Login);

            return SelectSingle(command);

        }

        /// <summary>
        /// Logins the specified login.
        /// Logins the specified login.
        /// </summary>
        /// <param name="login">The login.</param>
        /// <param name="password">The password.</param>
        /// <returns></returns>
        public UserValue Login(string login, string password)
        {
            var user = GetByLogin(login);
            if (user == null || user.Password != password) return null;
            return user;
        }
    }
}
