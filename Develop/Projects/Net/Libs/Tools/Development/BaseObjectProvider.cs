using System;
using System.Security.Principal;

namespace Savchin.Development
{
    /// <summary>
    /// BaseObjectProvider
    /// </summary>
    public abstract class BaseObjectProvider
    {

        /// <summary>
        /// Gets the session.
        /// </summary>
        /// <value>The session.</value>
        public abstract ISession Session { get; }

        /// <summary>
        /// Logins the specified user id.
        /// </summary>
        /// <param name="userId">The user id.</param>
        public virtual void Login(string userId)
        {
            var identity = new GenericIdentity(userId, "UserEntity");
            var principal = new GenericPrincipal(identity, null);
            AppDomain.CurrentDomain.SetThreadPrincipal(principal);
        }

        /// <summary>
        /// Logouts this instance.
        /// </summary>
        public virtual void Logout()
        {
            Session.Abandon();
            KillPrincipal();
        }

        /// <summary>
        /// Kills the principal.
        /// </summary>
        protected virtual void KillPrincipal()
        {
            AppDomain.CurrentDomain.SetThreadPrincipal(null);
        }
    }
}