using System.Collections;
using System.Configuration;
using System.Web;
using System.Web.Security;
using Savchin.Data.Common;
using Savchin.Development;
using Site.Bl;

namespace Site.Core
{
    /// <summary>
    /// SiteContext
    /// </summary>
    public class SiteContext : AppContext
    {
        #region Properties

        private enum ContextKey
        {
            CurrentUser
        }

        private string connectionString = ConfigurationManager.ConnectionStrings["default"].ConnectionString;
        private const string keyConnection = "con";
        private static SiteContext context = new SiteContext();

        /// <summary>
        /// Gets the current.
        /// </summary>
        /// <value>The current.</value>
        public static SiteContext Current
        {
            get { return context; }
        }
        /// <summary>
        /// Gets the curren user ID.
        /// </summary>
        /// <value>The curren user ID.</value>
        public int? CurrenUserID
        {
            get
            {
                var userID = 0;
                if (!int.TryParse(HttpContext.Current.User.Identity.Name, out userID)) return null;
                return userID;
            }
        }
        /// <summary>
        /// Provides current instance of connection.
        /// </summary>
        /// <returns></returns>
        public DBConnection Connection
        {
            get
            {
                IDictionary items = Context;
                DBConnection result;
                if (items.Contains(keyConnection))
                {
                    result = (DBConnection)items[keyConnection];
                }
                else
                {
                    result = CreateConnection();
                    items[keyConnection] = result;
                }
                return result;
            }

        } 
        #endregion

        /// <summary>
        /// Creates the connection.
        /// </summary>
        /// <returns></returns>
        public DBConnection CreateConnection()
        {
            return new DBConnection(connectionString);
        }


        /// <summary>
        /// Disposes the current connection.
        /// </summary>
        public void DisposeCurrentConnection()
        {
            DBConnection connection = (DBConnection)Context[keyConnection];
            if (connection == null)
                return;
            connection.Dispose();
            Context.Remove(keyConnection);
        }
     
        /// <summary>
        /// Gets the current user ID.
        /// </summary>
        /// <value>The current user ID.</value>
        public User GetCurrentUser()
        {
            if (!Context.Contains(ContextKey.CurrentUser))
            {
                var userID = CurrenUserID;
                if (!userID.HasValue) return null;

                var result = new UserManager().GetByID(userID.Value);

                if (result == null) return null;

                Context.Add(ContextKey.CurrentUser, result);
                return result;
            }

            return (User)Context[ContextKey.CurrentUser];

        }
        /// <summary>
        /// Logouts this instance.
        /// </summary>
        public void Logout(bool withRedirect)
        {
            FormsAuthentication.SignOut();

            //if (HttpContext.Current.Session != null)
            //{
            //    HttpContext.Current.Session.Clear();
            //    HttpContext.Current.Session.Abandon();
            //}

            if (withRedirect)
                FormsAuthentication.RedirectToLoginPage();

            //HttpContext.Current.Response.Flush();
            //HttpContext.Current.Response.End();
            //HttpContext.Current.Response.Close();z

        }
        /// <summary>
        /// Logins the specified login.
        /// </summary>
        /// <param name="login">The login.</param>
        /// <param name="createPersistentCookie">if set to <c>true</c> [create persistent cookie].</param>
        public void Login(string login, bool createPersistentCookie)
        {
            Login(new UserManager().GetByLogin(login), createPersistentCookie);
        }

        /// <summary>
        /// Logins the specified user ID.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="createPersistentCookie">if set to <c>true</c> [create persistent cookie].</param>
        public void Login(User user, bool createPersistentCookie)
        {
            Context.Add(ContextKey.CurrentUser, user);
            FormsAuthentication.SetAuthCookie(user.UserID.ToString(), createPersistentCookie);

        }
    }
}
