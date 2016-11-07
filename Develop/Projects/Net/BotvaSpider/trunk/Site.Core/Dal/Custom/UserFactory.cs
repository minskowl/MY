using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Savchin.Data.Common;
using Site.Dal.Entities;

namespace Site.Dal.Factories
{
    public partial class UserFactory
    {
        /// <summary>
        /// Gets the by login by password.
        /// </summary>
        /// <param name="Login">The login.</param>
        /// <param name="Password">The password.</param>
        /// <returns></returns>
        public UserValue GetByLoginByPassword(string Login, string Password)
        {
            IDbCommand command = database.CreateSPCommand("_User_GetByLoginByPassword");
            command.AddInputParameter("@Login", DbType.String, Login);
            command.AddInputParameter("@Password", DbType.String, Password);

            return SelectSingle(command);

        }

        /// <summary>
        /// Gets the by login.
        /// </summary>
        /// <param name="Login">The login.</param>
        /// <returns></returns>
        public UserValue GetByLogin(string Login)
        {
            IDbCommand command = database.CreateSPCommand("_User_GetByLogin");
            command.AddInputParameter("@Login", DbType.String, Login);

            return SelectSingle(command);

        }
    }
}
