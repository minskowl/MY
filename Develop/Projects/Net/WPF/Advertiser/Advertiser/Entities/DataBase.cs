using System;
using System.Collections.Generic;
using System.IO;
using Advertiser.Core;
using Savchin.Core;
using Savchin.Logging;

namespace Advertiser.Entities
{
    public class DataBase
    {
        private string _fileName = DefaultFileName;
        public const string DefaultFileName = "database.xml";
        public string FileName
        {
            get { return _fileName; }
        }
        public List<Login> Logins { get; set; }
        public List<Wheels> Wheels { get; set; }
        public List<string> WheelsManufaturers { get; set; }
        public List<Phone> Phones { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="DataBase"/> class.
        /// </summary>
        public DataBase()
        {
            Logins = new List<Login>();
            Wheels = new List<Wheels>();
            WheelsManufaturers = new List<string>();
        }

        /// <summary>
        /// Loads this instance.
        /// </summary>
        /// <returns></returns>
        public static DataBase Load(string file = DefaultFileName)
        {
            try
            {
                var result = TypeSerializer<DataBase>.FromXmlFile(file);
                result._fileName = file;

                var directory = Path.GetDirectoryName(file);
                if (!string.IsNullOrWhiteSpace(directory))
                    Directory.SetCurrentDirectory(directory);
                return result;
            }
            catch (Exception ex)
            {
                AdvContext.Current.Log.AddMessage(Severity.Warning, "Error load database", ex);
            }
            return new DataBase();
        }

        /// <summary>
        /// Saves this instance.
        /// </summary>
        public void Save()
        {
            TypeSerializer<DataBase>.ToXmlFile(_fileName, this);
        }
    }
}
