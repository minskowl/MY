using System;
using System.Configuration;
using System.IO;
using KnowledgeBase.BussinesLayer.Core;
using KnowledgeBase.Core.Properties;
using KnowledgeBase.Desktop.Core;
using Spring.Context;
using Spring.Context.Support;
using log4net;
using log4net.Config;

namespace KnowledgeBase.Core
{
    public static class AppCore
    {
        #region Properties

        private static IApplicationContext _ctx;

        /// <summary>
        /// Log
        /// </summary>
        public static readonly ILog Log;
        /// <summary>
        /// Settings
        /// </summary>
        public static readonly Settings Settings;

        /// <summary>
        /// Gets or sets the workspace.
        /// </summary>
        /// <value>The workspace.</value>
        public static Workspace Workspace { get; private set; }
        #endregion


        /// <summary>
        /// Initializes the <see cref="AppCore"/> class.
        /// </summary>
        static AppCore()
        {
            XmlConfigurator.Configure();
            Log = LogManager.GetLogger("App");
            try
            {
                Settings = Settings.Default;
            }
            catch (TypeInitializationException e)
            {
                var ex = e.InnerException as ConfigurationErrorsException;
                if (ex == null) throw;

                ex = ex.InnerException as ConfigurationErrorsException;
                if (ex!=null && File.Exists(ex.Filename))
                {
                    File.Delete(ex.Filename);
                    Settings = Settings.Default;
                    //ConfigurationManager.RefreshSection("");
                }
                throw;
            }
         
        }

        /// <summary>
        /// Gets the object.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T GetObject<T>()
        {
            return (T)_ctx.GetObject(typeof(T).Name);
        }

        /// <summary>
        /// Logins the specified user.
        /// </summary>
        public static void Login(KbContext context)
        {
            Workspace = new Workspace(context);
        }

        /// <summary>
        /// Inits this instance.
        /// </summary>
        public static void Init()
        {
    
            _ctx = ContextRegistry.GetContext();
        }


        public static void Dispose()
        {
            if (_ctx != null)
            {
                _ctx.Dispose();
                _ctx = null;
            }

        }
    }
}
