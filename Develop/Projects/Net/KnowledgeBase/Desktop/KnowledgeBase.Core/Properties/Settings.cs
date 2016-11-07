using System;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using Savchin.Core;

namespace KnowledgeBase.Core.Properties
{
    /// <summary>
    /// Settings
    /// </summary>
    public partial class Settings
    {
        /// <summary>
        /// HtmlEditorPath
        /// </summary>
        public readonly string HtmlEditorPath;

        /// <summary>
        /// ContentPath
        /// </summary>
        public readonly string ContentPath;

        /// <summary>
        /// 
        /// </summary>
        public readonly string LocalDatabasePath;
        /// <summary>
        /// 
        /// </summary>
        public readonly string LogsPath;



        /// <summary>
        /// ContentFolder
        /// </summary>
        public readonly string ContentFolder = "Content";
        /// <summary>
        /// Initializes a new instance of the <see cref="Settings"/> class.
        /// </summary>
        public Settings()
        {
            var dataPath = Path.Combine(AppInfo.ApplicationDataPath, Process.GetCurrentProcess().Id.ToString());

            LogsPath = Path.Combine(dataPath, "Logs\\");
            ContentPath = Path.Combine(dataPath, ContentFolder);
            Environment.SetEnvironmentVariable("APPLOGS", LogsPath);

            HtmlEditorPath =Path.Combine(AppInfo.ApplicationPath, FckEditorPath);
            LocalDatabasePath = Path.Combine(AppInfo.ApplicationPath, "Database.s3db");
        }
    }
}
