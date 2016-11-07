using System;
using System.IO;
using Savchin.Core;


namespace Savchin.Forms.Core
{
    /// <summary>
    /// SettingsBase
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class SettingsBase<T> where T : SettingsBase<T>
    {
        private string _filePath;
        private static readonly string defaulPath = AppDomain.CurrentDomain.BaseDirectory + "\\def.cfg";

        /// <summary>
        /// Loads this instance.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="FileNotFoundException">throw if file not found</exception>
        public static T Load()
        {
            return Load(defaulPath);
        }

        /// <summary>
        /// Loads the specified file path.
        /// </summary>
        /// <param name="filePath">The file path.</param>
        /// <exception cref="FileNotFoundException">throw if file not found</exception>
        public static T Load(string filePath)
        {
            T result = TypeSerializer<T>.FromXmlFile(filePath);
            result._filePath = filePath;
            return result;
        }

        /// <summary>
        /// Saves the specified file path.
        /// </summary>
        /// <param name="filePath">The file path.</param>
        public void Save(string filePath)
        {
            TypeSerializer<T>.ToXmlFile(filePath, this);
        }
        /// <summary>
        /// Saves this instance.
        /// </summary>
        public void Save()
        {
            Save(string.IsNullOrEmpty(_filePath) ? defaulPath : _filePath);
        }
    }
}