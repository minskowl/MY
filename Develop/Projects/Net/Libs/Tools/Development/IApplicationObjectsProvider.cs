using System.Collections;

namespace Savchin.Development
{
    /// <summary>
    /// IApplicationObjectsProvider
    /// </summary>
    public interface IApplicationObjectsProvider
    {
        /// <summary>
        /// Gets the session.
        /// </summary>
        /// <value>The session.</value>
        ISession Session { get; }

        /// <summary>
        /// Gets the context.
        /// </summary>
        /// <value>The context.</value>
        IDictionary Context { get; }

        /// <summary>
        /// Gets the state of the application.
        /// </summary>
        /// <value>The state of the application.</value>
        IDictionary ApplicationState { get; }

        /// <summary>
        /// Logins the specified user id.
        /// </summary>
        /// <param name="userId">The user id.</param>
        void Login(string userId);

        /// <summary>
        /// Logouts this instance.
        /// </summary>
        void Logout();
    }
}