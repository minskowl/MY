using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Design;
using System.IO;
using System.Windows.Forms.Design;
using BotvaSpider.Core;
using Savchin.Utils;

namespace BotvaSpider.Configuration
{
    /// <summary>
    /// AppSettingsы
    /// </summary>
    public class AppSettings
    {
        #region Properties
        /// <summary>
        /// ApplicatioPath
        /// </summary>
        public static readonly string ApplicatioPath = AppDomain.CurrentDomain.BaseDirectory;
        public static readonly string LogsPath = Path.Combine(ApplicatioPath, "Logs\\");

        private static string filePath = Path.Combine(ApplicatioPath, "Spider.cfg");


        /// <summary>
        /// Gets or sets the game config.
        /// </summary>
        /// <value>The game config.</value>
        [Editor(typeof(FileNameEditor), typeof(UITypeEditor))]
        public string GameConfig { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [use main skills].
        /// </summary>
        /// <value><c>true</c> if [use main skills]; otherwise, <c>false</c>.</value>
        public bool UseMainSkills { get; set; }

        /// <summary>
        /// Gets or sets the images.
        /// </summary>
        /// <value>The images.</value>
        public ImageCollection Images { get; set; }

        /// <summary>
        /// Gets or sets the servers.
        /// </summary>
        /// <value>The servers.</value>
        public List<string> Servers { get; set; }
        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="AppSettings"/> class.
        /// </summary>
        public AppSettings()
        {
            Servers = new List<string>();
        }

        /// <summary>
        /// Creates this instance.
        /// </summary>
        /// <returns></returns>
        public static AppSettings Create()
        {
            try
            {
                return TypeSerializer<AppSettings>.FromXmlFile(filePath);

            }
            catch (Exception ex)
            {
                AppCore.LogSystem.Error("Error app settings", ex);
                var result = new AppSettings
                {
                    GameConfig = filePath
                };
                result.Save();
                return result;
            }
        }

        /// <summary>
        /// Saves this instance.
        /// </summary>
        public void Save()
        {
            TypeSerializer<AppSettings>.ToXmlFile(filePath, this);
        }
    }
}
