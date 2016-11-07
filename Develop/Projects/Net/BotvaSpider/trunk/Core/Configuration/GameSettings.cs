using System;
using System.ComponentModel;
using System.Drawing.Design;
using System.IO;
using BotvaSpider.Editors;
using Savchin.Utils;

namespace BotvaSpider.Configuration
{
    /// <summary>
    /// GameSettings
    /// </summary>
    public class GameSettings
    {
        public event EventHandler SettingsChanged;
        private static string defaultConfig = Path.Combine(AppSettings.ApplicatioPath, "default.game.cfg");
        private string filePath;
        /// <summary>
        /// Gets or sets the database path.
        /// </summary>
        /// <value>The database path.</value>
        [Editor(typeof(MdbFileEditor), typeof(UITypeEditor))]
        public string DatabasePath { get; set; }

        /// <summary>
        /// Gets or sets the server settings.
        /// </summary>
        /// <value>The server settings.</value>
        public ServerSettings ServerSettings { get; set; }


        /// <summary>
        /// Gets or sets the watin settings.
        /// </summary>
        /// <value>The watin settings.</value>
        public WatinSettings WatinSettings { get; set; }

        /// <summary>
        /// Gets or sets the botva settings.
        /// </summary>
        /// <value>The botva settings.</value>
        public BotvaSettings BotvaSettings { get; set; }



        /// <summary>
        /// Saves this instance.
        /// </summary>
        public void Save()
        {
            OnSettingsChanged(EventArgs.Empty);
            TypeSerializer<GameSettings>.ToXmlFile(filePath, this);
        }

        /// <summary>
        /// Gets the configs.
        /// </summary>
        /// <returns></returns>
        public static string[] GetConfigs()
        {
            return Directory.GetFiles(AppSettings.ApplicatioPath, "*.game.cfg");
        }

        /// <summary>
        /// Loads the specified file path.
        /// </summary>
        /// <param name="filePath">The file path.</param>
        /// <returns></returns>
        public static GameSettings Load(string filePath)
        {
            var result = TypeSerializer<GameSettings>.FromXmlFile(filePath);
            result.filePath = filePath;
            return result;
        }

        public string GetDatabaseFullPath()
        {
            if (string.IsNullOrEmpty(DatabasePath)) return string.Empty;

            return (Path.IsPathRooted(DatabasePath)) ?
            DatabasePath : Path.Combine(AppSettings.ApplicatioPath, DatabasePath);
        }

        /// <summary>
        /// Creates this instance.
        /// </summary>
        /// <returns></returns>
        public static GameSettings Create(string configName)
        {
            return new GameSettings
                       {
                           ServerSettings = ServerSettings.Create(),
                           WatinSettings = WatinSettings.Create(),
                           BotvaSettings = new BotvaSettings(),
                           filePath = configName
                       };

        }

        /// <summary>
        /// Raises the <see cref="E:SettingsChanged"/> event.
        /// </summary>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected virtual void OnSettingsChanged(EventArgs e)
        {
            if (SettingsChanged != null)
                SettingsChanged(this, e);
        }
    }
}