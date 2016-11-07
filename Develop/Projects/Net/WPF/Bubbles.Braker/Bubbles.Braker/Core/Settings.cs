using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using Bubbles.Braker;
using Savchin.Utils;

namespace Savchin.Bubbles.Core
{
    [Serializable]
    public class Settings
    {
        private const string fileName = "Bubbles.cfg";
        public const string AppName = "Bubble Braker";

        public static readonly string AppPath = AppDomain.CurrentDomain.BaseDirectory;
        private static readonly string ConfigPath = Path.Combine(AppPath, fileName);
        public  static readonly string LogPath = Path.Combine(AppPath, "Logs");
        [XmlAttribute]
        public ShiftStrategy Strategy { get; set; }



        /// <summary>
        /// Initializes a new instance of the <see cref="Settings"/> class.
        /// </summary>
        public Settings()
        {
        }

        /// <summary>
        /// Saves this instance.
        /// </summary>
        public void Save()
        {
            TypeSerializer<Settings>.ToXmlFile(ConfigPath, this);
        }

        /// <summary>
        /// Loads this instance.
        /// </summary>
        /// <returns></returns>
        public static Settings Load()
        {
            return TypeSerializer<Settings>.FromXmlFile(ConfigPath);
        }

        /// <summary>
        /// Creates this instance.
        /// </summary>
        /// <returns></returns>
        public static Settings Create()
        {
            Settings result = null;
            try
            {
                result = Load();
            }
            catch(Exception ex)
            {
                App.Current.Log.Error("Error load settings",ex);
            }
            if (result == null)
                result = new Settings { Strategy = ShiftStrategy.Standart };

            return result;
        }
    }
}
