
using Windows.Security.Credentials;
using Savchin.Development;

namespace Savchin.Application
{
    /// <summary>
    /// BaseObjectProvider
    /// </summary>
    public abstract class BaseObjectProvider
    {
        private PasswordCredential _credential;
        private PasswordVault _vault;
        /// <summary>
        /// Gets the session.
        /// </summary>
        /// <value>The session.</value>
        public abstract ISession Session { get; }

        /// <summary>
        /// Logins the specified user id.
        /// </summary>
        /// <param name="userId">The user id.</param>
        public virtual void Login(string userId, string password)
        {
            if (_vault == null)
                _vault = new PasswordVault();
            _credential = new PasswordCredential("My App", userId, password);
            _vault.Add(_credential);


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
            _vault.Remove(_credential);
            _credential = null;

        }
    }
}