using System;

using System.Web.Security;

using KnowledgeBase.BussinesLayer;
using KnowledgeBase.BussinesLayer.Core;
using KnowledgeBase.SiteCore.Providers;

namespace KnowledgeBase.SiteCore.Providers
{
    /// <summary>
    /// SiteSecurityProvider
    /// </summary>
    public class SiteSecurityProvider : SecurityProviderBase
    {

        /// <summary>
        /// Validates the user.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <param name="password">The password.</param>
        /// <returns></returns>
        public override bool ValidateUser(string email, string password)
        {
            return KbContext.CurrentKb.ManagerUser.CanLogin(email, password);
        }



        /// <summary>
        /// Gets the user.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <param name="userIsOnline">if set to <c>true</c> [user is online].</param>
        /// <returns></returns>
        public override MembershipUser GetUser(string username, bool userIsOnline)
        {
            return CreateMembershipUser(GetUser(username));
        }
        /// <summary>
        /// Resets the password.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <param name="answer">The answer.</param>
        /// <returns></returns>
        public override string ResetPassword(string username, string answer)
        {
            User user = GetUser(username);
            if (user.SecurityAnswer == answer)
                return user.Password;
            throw new MembershipPasswordException();

        }


        private User GetUser(string login)
        {
            return KbContext.CurrentKb.ManagerUser.GetByLogin(login);

        }
        /// <summary>
        /// Creates the membership user.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns></returns>
        private MembershipUser CreateMembershipUser(User user)
        {
            if (user == null)
            {
                return null;
            }

            return new MembershipUser(
                "SiteSecurityProvider",
                user.Login,
                user.UserID,
                user.Email,
                user.SecurityQuestion,
                "",
                true,
                false,
                user.CreationDate,
                DateTime.MinValue,
                DateTime.MinValue,
                DateTime.MinValue,
                DateTime.MinValue
                );
        }
    }
}