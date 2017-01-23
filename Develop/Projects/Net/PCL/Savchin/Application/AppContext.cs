using System;
using System.Collections;
using System.Collections.Generic;
using Savchin.Development;

namespace Savchin.Application
{
    /// <summary>
    /// Application Context
    /// </summary>
    public class AppContext
    {
        private const string keyLoadedObj = "AppContext.LoadedObjects";

        #region Properties
        /// <summary>
        /// Gets or sets the current.
        /// </summary>
        /// <value>The current.</value>
        public static AppContext CurrentApp
        {
            get;
            set;
        }


        /// <summary>
        /// Gets the session.
        /// </summary>
        /// <value>The session.</value>
        public ISession Session => Provider.Session;

        /// <summary>
        /// Gets the context.
        /// </summary>
        /// <value>The context.</value>
        public IDictionary Context => Provider.Context;

        /// <summary>
        /// Gets the state of the application.
        /// </summary>
        /// <value>The state of the application.</value>
        public IDictionary ApplicationState => Provider.ApplicationState;

        /// <summary>
        /// Gets or sets the provider.
        /// </summary>
        /// <value>The provider.</value>
        public IApplicationObjectsProvider Provider { get; }

        #endregion


        /// <summary>
        /// Initializes a new instance of the <see cref="AppContext"/> class.
        /// </summary>
        /// <param name="provider">The provider.</param>
        public AppContext(IApplicationObjectsProvider provider)
        {
            Provider = provider;
        }

  

        /// <summary>
        /// Gets the fromt context.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key">The key.</param>
        /// <param name="fabric">The fabric.</param>
        /// <returns></returns>
        public T GetFromContext<T>(object key, Func<T> fabric)
        {
            if (Context.Contains(key))
                return (T)Context[key];
            var obj = fabric();
            Context.Add(key, obj);
            return obj;
        }

        /// <summary>
        /// Gets the fromt context.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        public T GetFromContext<T>(object key)
              where T : class, new()
        {
            return GetFromContext(key, () => new T());
        }

        /// <summary>
        /// Gets from persistent context.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        public T GetFromPersistentContext<T>(Enum key)
            where T : class, new()
        {
            return GetFromPersistentContext(key, () => new T());
        }


        /// <summary>
        /// Gets from persistent context.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key">The key.</param>
        /// <param name="fabric">The fabric.</param>
        /// <returns></returns>
        public T GetFromPersistentContext<T>(Enum key, Func<T> fabric)
            where T : class
        {
            if (Context.Contains(key))
                return (T)Context[key];

            var confing = (T)Session.Get(key.ToString()) ?? fabric();

            var keyLoadedObjects = GetFromContext<List<Enum>>(keyLoadedObj);
            keyLoadedObjects.Add(key);

            Context.Add(key, confing);
            return confing;
        }

        /// <summary>
        /// Saves the pesistent context.
        /// </summary>
        public void SavePesistentContext()
        {
            if (!Context.Contains(keyLoadedObj)) return;
            var keys = (List<Enum>)Context[keyLoadedObj];
            foreach (var key in keys)
            {
                Session.Save(
                     key.ToString(),
                     Context[key]
                     );
            }
        }

        /// <summary>
        /// Logins the specified user ID.
        /// </summary>
        /// <param name="userId">The user id.</param>
        public virtual void Login(string userId,string password)
        {
            Provider.Login(userId,  password);
        }

        /// <summary>
        /// Logouts this instance.
        /// </summary>
        public virtual void Logout()
        {
            Provider.Logout();
        }

        #region Session Methods
        /// <summary>
        /// Saves to session.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        public void SaveToSession(Type type, string key, object value)
        {
            Session.Save(type.FullName + "-" + key, value);
        }

        /// <summary>
        /// Gets from session.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        public object GetFromSession(Type type, string key)
        {
            return Session.Get(type.FullName + "-" + key);
        }
        /// <summary>
        /// Removes from session.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="key">The key.</param>
        public void RemoveFromSession(Type type, string key)
        {
            Session.Remove(type.FullName + "-" + key);
        }
        #endregion
    }
}