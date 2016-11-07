using System.Collections;
using System.Web;
using System.Web.Caching;
using System.Web.Security;
using Savchin.Development;

namespace Savchin.Web.Development
{
    /// <summary>
    /// WebProviders
    /// </summary>
    public class WebProvider : BaseObjectProvider, IApplicationObjectsProvider
    {    
        readonly WebSession session= new WebSession();
        readonly WebApplicationState applicationState= new WebApplicationState();
        /// <summary>
        /// Gets the cache.
        /// </summary>
        /// <value>The cache.</value>
        public Cache Cache
        {
            get { return HttpRuntime.Cache; }
        }

        /// <summary>
        /// Gets the session.
        /// </summary>
        /// <value>The session.</value>
        public override ISession Session
        {
            get { return session; }
        }

        /// <summary>
        /// Gets the context.
        /// </summary>
        /// <value>The context.</value>
        public IDictionary Context
        {
            get { return HttpContext.Current.Items; }
        }

        /// <summary>
        /// Gets the state of the application.
        /// </summary>
        /// <value>The state of the application.</value>
        public IDictionary ApplicationState
        {
            get { return applicationState; }
        }


        /// <summary>
        /// Logins the specified user id.
        /// </summary>
        /// <param name="userId">The user id.</param>
        public override void Login(string userId)
        {
            FormsAuthentication.SetAuthCookie(userId, true);
        }

        /// <summary>
        /// Kills the principal.
        /// </summary>
        protected override void KillPrincipal()
        {
            FormsAuthentication.SignOut();
            FormsAuthentication.RedirectToLoginPage();
        }
    }
}